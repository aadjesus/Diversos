//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  lukasz@istrib.org
//
//  Copyright (C) 2008-2009 Lukasz Kwiecinski. 
//

using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.DirectX.DirectSound;
using System.Threading;

namespace Istrib.Sound
{
    partial class SoundCapture
    {
        private BufferPositionNotify[] positionNotify = new BufferPositionNotify[NumberRecordNotifications + 1];

        private const int NumberRecordNotifications = 16;

        private CaptureBuffer applicationBuffer = null;
        
        private Capture applicationDevice = null;
        private Notify applicationNotify = null;

        /// <summary>
        /// Releases notification listener. The event is signaled either by DirectX engine
        /// or by Stop() operation (the latter to dispose DirectX within notification thread).
        /// </summary>
        private AutoResetEvent notificationArrivalEvent = null;
        /// <summary>
        /// This event is signaled by notification thread just after DirectX objects have been
        /// disposed, that is when the capturing session has been definitely finished.
        /// </summary>
        private AutoResetEvent directSoundDisposedEvent = null;
        private Thread notificationListenerThread = null;
        
        private int captureBufferSize = 0;
        private int nextCaptureOffset = 0;
        
        private WaveFormat? cachedInputFormat = null;
        
        private int sampleByteCount = 0;
        private int notifySize = 0;

        /// <summary>
        /// Converts Istrib.Sound.SoundFormat to DirectX InputFormat.
        /// </summary>
        private WaveFormat InputFormat
        {
            get
            {
                if (!cachedInputFormat.HasValue)
                {
                    WaveFormat result = new WaveFormat();
                    result.SamplesPerSecond = WaveFormat.SampleRate;
                    result.FormatTag = WaveFormatTag.Pcm;
                    result.Channels = WaveFormat.Channels;
                    result.BitsPerSample = WaveFormat.BitsPerSample;
                    result.BlockAlign = WaveFormat.BlockAlign;
                    result.AverageBytesPerSecond = WaveFormat.AverageBytesPerSecond;
                    
                    cachedInputFormat = result;
                }

                return cachedInputFormat.Value;
            }
        }

        private void SignalNotificationArrivalEvent()
        {
            if (null != notificationArrivalEvent)
                notificationArrivalEvent.Set();
        }

        private void SignalDirectSoundDisposedEvent()
        {
            if (null != directSoundDisposedEvent)
                directSoundDisposedEvent.Set();
        }

        private void InitDirectSound()
        {
            captureBufferSize = 0;
            notifySize = 0;

            // Create DirectSound.Capture using the preferred capture device
            try
            {
                applicationDevice = captureDevice == SoundCaptureDevice.Default
                    ? new Capture()
                    : new Capture(captureDevice.DriverGuid);
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("Unable to initialize Capture Device. Is the device '" + CaptureDevice.Description + "' plugged in?");
            }
        }

        /// <summary>
        /// Creates a capture buffer and sets the format 
        /// </summary>
        private void CreateCaptureBuffer()
        {
            CaptureBufferDescription dscheckboxd = new CaptureBufferDescription();

            if (null != applicationNotify)
            {
                applicationNotify.Dispose();
                applicationNotify = null;
            }
            if (null != applicationBuffer)
            {
                applicationBuffer.Dispose();
                applicationBuffer = null;
            }

            if (0 == InputFormat.Channels)
            {
                throw new ArgumentException("Audio Channels cannot be zero (use 1 - mono, 2 - stereo, etc.).");
            }

            // Set the notification size
            notifySize = (1024 > InputFormat.AverageBytesPerSecond / 8) ? 1024 : (InputFormat.AverageBytesPerSecond / 8);
            notifySize -= notifySize % InputFormat.BlockAlign;

            // Set the buffer sizes
            captureBufferSize = notifySize * NumberRecordNotifications;

            // Create the capture buffer
            dscheckboxd.BufferBytes = captureBufferSize;
            dscheckboxd.Format = InputFormat; // Set the format during creation

            try
            {
                applicationBuffer = new CaptureBuffer(dscheckboxd, applicationDevice);
            }
            catch (ApplicationException ex)
            {
                //Yeah, I know, D i r e c t X managed...
                throw new ApplicationException("The sound capturing device is not ready. Is '" + CaptureDevice + "' plugged in?");
            }

            nextCaptureOffset = 0;

            InitDirectSoundNotifications();
        }

        /// <summary>
        /// Inits the notifications on the capture buffer which are handled
        //  in the notify thread.
        /// </summary>
        private void InitDirectSoundNotifications()
        {
            if (null == applicationBuffer)
                throw new NullReferenceException("DirectSound capture buffer not initialized.");

            // Create a thread to monitor the notify events
            if (null == notificationListenerThread)
            {
                // Create a notification event, for when the sound stops playing
                notificationArrivalEvent = new AutoResetEvent(false);
                directSoundDisposedEvent = new AutoResetEvent(false);

                notificationListenerThread = new Thread(new ThreadStart(ListenDirectSoundNotifications));
                notificationListenerThread.Start();
            }

            // Setup the notification positions
            for (int i = 0; i < NumberRecordNotifications; i++)
            {
                positionNotify[i].Offset = (notifySize * i) + notifySize - 1;
                positionNotify[i].EventNotifyHandle = notificationArrivalEvent.Handle;
            }

            applicationNotify = new Notify(applicationBuffer);

            // Tell DirectSound when to notify the app. The notification will come in the from 
            // of signaled events that are handled in the notify thread.
            applicationNotify.SetNotificationPositions(positionNotify, NumberRecordNotifications);
        }

        private void StartDirectSoundCapturing()
        {
            applicationBuffer.Start(true);
        }

        private void StopDirectSoundCapturing()
        {
            if (applicationBuffer != null)
            {
                applicationBuffer.Stop();
            }
        }

        private void DisposeDirectSound()
        {
            if (null != applicationNotify)
            {
                applicationNotify.Dispose();
                applicationNotify = null;
            }
            if (null != applicationBuffer)
            {
                applicationBuffer.Dispose();
                applicationBuffer = null;
            }
            if (null != applicationDevice)
            {
                applicationDevice.Dispose();
                applicationDevice = null;
            }
            if (null != notificationArrivalEvent)
            {
                notificationArrivalEvent.Close();
                notificationArrivalEvent = null; 
            }

            notificationListenerThread = null;
        }

        /// <summary>
        /// This thread is waken up by DirectX notification when the capture buffer is filled
        /// in with the next chunk of data. The thread pushes the capture buffer contents to
        /// the output stream.
        /// 
        /// The thread lives only until the current capturing is stopped.
        /// Next capturing starts a new thread (with a new NotificationEvent).
        /// 
        /// Exit of this thread is the last operatin within capturing session. DirectX
        /// objects disposal is performed on thread exit. After disposal the DirectSoundDisposedEvent
        /// is signaled notifying the end of capturing session.
        /// </summary>
        private void ListenDirectSoundNotifications()
        {
            try
            {
                while (capturing)
                {
                    //Sit here and wait for a message to arrive
                    notificationArrivalEvent.WaitOne(Timeout.Infinite, true);
                    RecordCapturedData(!capturing); //Flush only when we are finishing.

                    FireEvent(ChunkCaptured);
                }
            }
            finally
            {
                DisposeDirectSound();
                SignalDirectSoundDisposedEvent();
            }
        }

        /// <summary>
        /// Copies data from the capture buffer to the output buffer 
        /// </summary>
        private void RecordCapturedData(bool flush)
        {
            byte[] CaptureData = null;
            int ReadPos;
            int CapturePos;
            int LockSize;

            applicationBuffer.GetCurrentPosition(out CapturePos, out ReadPos);
            LockSize = ReadPos - nextCaptureOffset;
            if (LockSize < 0)
                LockSize += captureBufferSize;

            // Block align lock size so that we are always write on a boundary
            LockSize -= (LockSize % notifySize);

            if (0 == LockSize)
                return;

            // Read the capture buffer.
            CaptureData = (byte[])applicationBuffer.Read(nextCaptureOffset, typeof(byte), LockFlag.None, LockSize);

            filters.Input.Write(CaptureData, 0, CaptureData.Length);
            
            // Update the number of samples, in bytes, of the file so far.
            sampleByteCount += CaptureData.Length;

            // Move the capture offset along
            nextCaptureOffset += CaptureData.Length;
            nextCaptureOffset %= captureBufferSize; // Circular buffer

            if (flush)
            {
                filters.Input.Flush();
            }
        }
    }
}
