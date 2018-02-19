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
using System.IO;
using Istrib.Sound.Filters;
using System.Threading;
using Istrib.Sound.Formats;

namespace Istrib.Sound
{
    /// <summary>
    /// This class writes raw PCM data (without WAV header) to OutputStream. However you can use
    /// RiffFilter to prefix the first chunk of data with RIFF WAV header (as in .WAV files stored
    /// on disk). 
    /// </summary>
    /// <remarks>
    /// <p>The class is extensible: you may add more filters which are not included in
    /// this assembly (e.g. for compression or sound effects). Istrib.Sound.Mp3 assembly contains
    /// MP3 compression filter which allows you for example to record sound directly from 
    /// your microphone to MP3 file.
    /// </p><p>
    /// By default all events fired by this class use System.Threading.SynchronizationContext (are
    /// automatically marshalled e.g. in Windows.Forms applications).</p>
    /// </remarks>
    public partial class SoundCapture : ISoundFilterPipeline, IDisposable
    {
        /// <summary>
        /// Fired synchronously just before a next captured chunk of data 
        /// is written to OutputStream.
        /// </summary>
        public event EventHandler ChunkCaptured;
        public event EventHandler Started;
        public event EventHandler Stopped;

        private PcmSoundFormat waveFormat = PcmSoundFormat.Pcm22kHz16bitMono;
        private SoundCaptureDevice captureDevice = SoundCaptureDevice.Default;
        private Stream outputStream;
        private bool capturing = false;

        private SoundFilterPipeline filters = new SoundFilterPipeline();

        private SynchronizationContext syncContext;
        private object StartStopLock = new object();

        /// <summary>
        /// Format of the PCM data (Sample rate, bits per sample, channel count).
        /// This cannot be modified once capturing started.
        /// </summary>
        public PcmSoundFormat WaveFormat
        {
            get
            {
                return waveFormat;
            }
            set
            {
                if (filters.WiredUp)
                {
                    throw new InvalidOperationException("WaveFormat property cannot be set after Start() method has been called.");
                }

                waveFormat = value;
                cachedInputFormat = null; //DirectX format to be recreated when needed.
            }
        }

        /// <summary>
        /// Stream to which the captured data is written by this component.
        /// The stream must be open and assigned to this property before capture starts.
        /// </summary>
        public Stream OutputStream
        {
            get
            {
                return outputStream;
            }
            set
            {
                if (filters.WiredUp)
                {
                    throw new InvalidOperationException("OutputStream property cannot be set after Start() method has been called.");
                }

                outputStream = value;
            }
        }

        /// <summary>
        /// Gets or sets a capture device (e.g a microphone) from which the sound is to be captured.
        /// This property cannot be set while capturing, but may be set between Stop() and next Start()
        /// method call.
        /// </summary>
        /// <remarks>Thread-safe.</remarks>
        public SoundCaptureDevice CaptureDevice
        {
            get
            {
                return captureDevice;
            }
            set
            {
                if (capturing)
                {
                    throw new InvalidOperationException("CaptureDevice property cannot be set while capturing.");
                }

                lock (StartStopLock)
                {
                    if (capturing)
                    {
                        throw new InvalidOperationException("CaptureDevice property cannot be set while capturing.");
                    }

                    captureDevice = value;
                }
            }
        }

        /// <summary>
        /// Specifies pipeline filters and their order. Filters allow modification
        /// of captured stream before it is written to the Output stream.
        /// This list cannot be modified after the first call to Start method.
        /// </summary>
        public IList<SoundFilter> Filters
        {
            get 
            {
                return filters.Filters;
            }
        }

        /// <summary>
        /// True if the component is currently capturing sound.
        /// Setting calls Start() or Stop() method.
        /// </summary>
        public bool Capturing
        {
            get
            {
                return capturing;
            }
            set
            {
                if (value)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }

        /// <summary>
        /// Summary status of all Start()-Stop() capturing sessions (that is from the beginning
        /// of this object life.
        /// </summary>
        public SoundStreamingStatus Status
        {
            get
            {
                return new SoundStreamingStatus(sampleByteCount);
            }
        }

        /// <summary>
        /// Starts or resumes capturing. If sound capturing was stopped then the new sound will
        /// be appended to the OutputStream.
        /// </summary>
        /// <remarks>Thread-safe.</remarks>
        public void Start()
        {
            if (!capturing)
            {
                lock (StartStopLock)
                {
                    if (!capturing)
                    {
                        capturing = true;
                        WireUpFilterPipelineIfNeeded();

                        try
                        {
                            filters.NotifyStreamingStartedResumed();

                            InitDirectSound();
                            CreateCaptureBuffer();
                            StartDirectSoundCapturing(); //This starts a separate notification thread.
                        }
                        catch
                        {
                            Stop(); //Clean up if there was an error.
                            throw;
                        }

                        FireEvent(Started);
                    }
                }
            }
        }

        /// <summary>
        /// Stops or suspends capturing. Start method may be than called again to append a new
        /// piece of sound to the OutputStream.
        /// This method is also called automatically during object disposal.
        /// </summary>
        /// <remarks>Thread-safe.</remarks>
        public void Stop()
        {
            if (capturing)
            {
                lock (StartStopLock)
                {
                    if (capturing)
                    {
                        StopDirectSoundCapturing(); //Stop DirectX capturing, the notification thread is still alive

                        capturing = false; //This flag will be now read by the notification thread
                        if (notificationListenerThread != null) //May be null if Stop() is called after inclomplete Start() call (exception within Start()).
                        {
                            //Force the notification to process. It will read capturing == false, 
                            //read the capture data tail, dispose DirectX objects, signal directSoundDisposedEvent and exit.
                            SignalNotificationArrivalEvent();
                            directSoundDisposedEvent.WaitOne(Timeout.Infinite, true); //Wait until the notification thread exits.

                            //DirectSound objects are now disposed. Close also this event. The capturing session is ended.
                            directSoundDisposedEvent.Close();
                            directSoundDisposedEvent = null;
                        }
                        else
                        {
                            //The notification thread did not have chance to run, but some Dx objects
                            //can be not disposed (e.g. exception thrown within Start()), dispose them on this thread:
                            DisposeDirectSound();
                        }

                        filters.NotifyStreamingStoppedSuspended(Status);

                        FireEvent(Stopped);
                    }
                }
            }
        }

        /// <summary>
        /// Stops capturing if it currenlty runs.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        private void WireUpFilterPipelineIfNeeded()
        {
            if (!filters.WiredUp)
            {
                filters.WireUp(WaveFormat, outputStream);
            }
        }

        private void FireEvent(EventHandler eventToBeFired)
        {
            if (eventToBeFired != null)
            {
                if (syncContext == null)
                {
                    eventToBeFired(this, new EventArgs());
                }
                else
                {
                    syncContext.Send((obj) => eventToBeFired(this, new EventArgs()), null);
                }
            }
        }

        /// <summary>
        /// Constructs the capture component which is then able to capture many sound pieces
        /// and append then to OutputStream. The OutputStream property must be set prior
        /// to calling Start() method.
        /// </summary>
        public SoundCapture()
            : this(PcmSoundFormat.Pcm22kHz16bitMono, null, true)
        {
        }

        /// <summary>
        /// Constructs the capture component which is then able to capture many sound pieces
        /// and append then to provided outputStream.
        /// </summary>
        /// <param name="waveFormat">Format of raw PCM wave data (how the sound is to be captured).
        /// See SoundFormat static properties.</param>
        /// <param name="outputStream">Stream to which the captured data is written by this component.
        /// It must be open before capture starts.</param>
        public SoundCapture(PcmSoundFormat waveFormat, Stream outputStream)
            : this(waveFormat, outputStream, true)
        {
        }

        /// <summary>
        /// Constructs the capture component which is then able to capture many sound pieces
        /// and append then to provided outputStream.
        /// </summary>
        /// <param name="waveFormat">Format of raw PCM wave data (how the sound is to be captured).
        /// See SoundFormat static properties.</param>
        /// <param name="outputStream">Stream to which the captured data is written by this component.
        /// It must be open before capture starts.</param>
        /// <param name="useSynchronizationContext">False disables event marshalling to the synchronization
        /// context thread.</param>
        public SoundCapture(PcmSoundFormat waveFormat, Stream outputStream, bool useSynchronizationContext)
        {
            if (useSynchronizationContext)
            {
                this.syncContext = SynchronizationContext.Current;
            }
            this.WaveFormat = waveFormat;
            this.OutputStream = outputStream;
        }
    }
}
