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
//  LAME ( LAME Ain't an Mp3 Encoder ) 
//  You must call the fucntion "beVersion" to obtain information  like version 
//  numbers (both of the DLL and encoding engine), release date and URL for 
//  lame_enc's homepage. All this information should be made available to the 
//  user of your product through a dialog box or something similar.
//  You must see all information about LAME project and legal license infos at 
//  http://www.mp3dev.org/  The official LAME site
//
//
//  About Thomson and/or Fraunhofer patents:
//  Any use of this product does not convey a license under the relevant 
//  intellectual property of Thomson and/or Fraunhofer Gesellschaft nor imply 
//  any right to use this product in any finished end user or ready-to-use final 
//  product. An independent license for such use is required. 
//  For details, please visit http://www.mp3licensing.com.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Istrib.Sound
{
    using Formats;
    using System.IO;

    /// <summary>
    /// A SoundCapture builder which attaches appropriate filters to the build SoundCapture object
    /// then uses it to capture sound from a system device.
    /// </summary>
    public partial class Mp3SoundCapture : Component
    {
        public enum Outputs
        {
            RawPcm,
            Wav,
            Mp3
        }

        private delegate void StopMethod();

        public event EventHandler Starting;
        public event EventHandler Started;
        public event EventHandler Stopping;
        public event EventHandler<StoppedEventArgs> Stopped;

        private SoundCaptureDevice device;

        /// <summary>
        /// Created one per capturing session.
        /// </summary>
        private CapturingProcess capturing = null;
        private object CaptureLock = new object();

        private System.Threading.SynchronizationContext syncContext;

        /// <summary>
        /// Gets or sets a capture device (e.g a microphone) from which the sound is to be captured.
        /// Null means the default system device (it is the default value of this property, never null).
        /// </summary>
        public SoundCaptureDevice CaptureDevice
        {
            get
            {
                if (device == null)
                {
                    device = SoundCaptureDevice.Default;
                }

                return device;
            }
            set
            {
                device = value;
            }
        }

        /// <summary>
        /// Format of the PCM data (Sample rate, bits per sample, channel count).
        /// The default value is: PCM 22kHz, 16bit/sample, Mono.
        /// </summary>
        public PcmSoundFormat WaveFormat
        {
            get;
            set;
        }

        /// <summary>
        /// MPE bit rate (kbit/sec).
        /// 128 kbit/s by default.
        /// </summary>
        public Mp3BitRate Mp3BitRate
        {
            get;
            set;
        }

        /// <summary>
        /// If true, then the captured data is buffered on disk and normalized (volume is adusted)
        /// before it is written to the output.
        /// False by default.
        /// </summary>
        public bool NormalizeVolume
        {
            get;
            set;
        }

        /// <summary>
        /// The general format of captured sound data.
        /// MP3 by default.
        /// </summary>
        public Outputs OutputType
        {
            get;
            set;
        }

        /// <summary>
        /// True if the component is currently capturing sound.
        /// </summary>
        public bool Capturing
        {
            get
            {
                if (capturing == null)
                {
                    return false;
                }

                lock (CaptureLock)
                {
                    return capturing != null && capturing.Process.Capturing;
                }
            }
        }

        /// <summary>
        /// Gets or sets value indicating if events fired by this component are
        /// automatically marshalled using System.Threading.SynchronizationContext.
        /// True by default.
        /// </summary>
        public bool UseSynchronizationContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating if Stop() method waits until all captured data
        /// is written to the output stream. If false the stop method exits immetiately after
        /// stopping process has been initiated - the Stopped event notifies later about
        /// last chunk data written to the output stream or about a problem that occurred in
        /// the meantime.
        /// True by default.
        /// </summary>
        /// <remarks>
        /// Stopping process may last some time especially when NormalizeVolume is true.
        /// The sound is normalized after the call to Stop() method, then - if you use MP3 output
        /// type - all data must be yet compressed. All that can take time. 
        /// </remarks>
        public bool WaitOnStop
        {
            get;
            set;
        }

        /// <summary>
        /// Asynchronously starts capturing.
        /// </summary>
        /// <param name="outputStream">A writeable stream where the captured data will be
        /// written (in formate specified by OutputType property).</param>
        public void Start(Stream outputStream)
        {
            Start(outputStream, false);
        }

        /// <summary>
        /// Asynchronously starts capturing to a file.
        /// The file is ready (closed) when Stopped event if fired (or after you stop capturing
        /// when WaitOnStop is true).
        /// </summary>
        /// <param name="outputFilePath"></param>
        public void Start(string outputFilePath)
        {
            Start(new FileStream(outputFilePath, FileMode.Create, FileAccess.Write), true);
        }

        /// <summary>
        /// Asynchronously starts capturing.
        /// </summary>
        /// <param name="outputStream">A writeable stream where the captured data will be
        /// written (in formate specified by OutputType property).</param>
        private void Start(Stream outputStream, bool ownsOutputStream)
        {
            if (capturing == null)
            {
                lock (CaptureLock)
                {
                    if (capturing == null)
                    {
                        try
                        {
                            capturing = new CapturingProcess();
                            capturing.OwnsOutputStream = ownsOutputStream;

                            FireEvent(Starting);

                            SoundCapture capture = new SoundCapture(WaveFormat, outputStream, UseSynchronizationContext);
                            capturing.Process = capture;

                            capture.CaptureDevice = CaptureDevice;

                            if (NormalizeVolume)
                            {
                                capture.Filters.Add(new Filters.PcmNormalizerFilter());
                            }

                            if (OutputType == Outputs.Mp3)
                            {
                                Filters.Mp3CompressingFilter compressor = new Filters.Mp3CompressingFilter();
                                compressor.Mp3BitRate = Mp3BitRate;
                                capture.Filters.Add(compressor);
                            }
                            else if (OutputType == Outputs.Wav)
                            {
                                capture.Filters.Add(new Filters.RiffFilter());
                            }
                            //Else a raw PCM will go off.

                            capture.Started += (o, e) => { if (Started != null) Started(this, e); };
                            //Stopped is fired by this component itself.

                            capture.Start();
                        }
                        catch
                        {
                            Stop();
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stops capturing process. If WaitOnStop property is false then
        /// exits immediately - the output stream can be then closed only after Stopped event
        /// is fired.
        /// </summary>
        /// <remarks>
        /// Stopping process may last some time especially when NormalizeVolume is true.
        /// The sound is normalized after the call to Stop() method, then - if you use MP3 output
        /// type - all data must be yet compressed. All that can take time. 
        /// </remarks>
        public void Stop()
        {
            if (capturing != null)
            {
                lock (CaptureLock)
                {
                    CapturingProcess capturingObject = capturing;
                    capturing = null;

                    FireEvent(Stopping);

                    if (WaitOnStop)
                    {
                        PerformStop(capturingObject);
                    }
                    else
                    {
                        StopMethod stopping = () =>
                            {
                                PerformStop(capturingObject);  
                            };
                        stopping.BeginInvoke(null, null);
                    }
                }
            }
        }

        private void PerformStop(CapturingProcess capturing)
        {
            try
            {
                capturing.Process.Stop();
            }
            catch (Exception ex)
            {
                try
                {
                    capturing.CloseOwnedOutputStream();
                }
                finally
                {
                    //Stop should not be called before the owned out stream is closed.
                    FireStopped(capturing, ex);
                }

                throw;
            }

            try
            {
                capturing.CloseOwnedOutputStream();
            }
            finally
            {
                //Stop should not be called before the owned out stream is closed.
                FireStopped(capturing, null);
            }
        }

        private void FireStopped(CapturingProcess capturing, Exception exception)
        {
            if (Stopped != null)
            {
                if (!UseSynchronizationContext || syncContext == null)
                {
                    Stopped(this, new StoppedEventArgs(capturing, exception));
                }
                else
                {
                    syncContext.Send((obj) => Stopped(this, new StoppedEventArgs(capturing, exception)), null);
                }
            }
        }

        private void FireEvent(EventHandler eventToBeFired)
        {
            if (eventToBeFired != null)
            {
                if (!UseSynchronizationContext || syncContext == null)
                {
                    eventToBeFired(this, new EventArgs());
                }
                else
                {
                    syncContext.Send((obj) => eventToBeFired(this, new EventArgs()), null);
                }
            }
        }

        private void Initialize()
        {
            this.syncContext = System.Threading.SynchronizationContext.Current;
            this.CaptureDevice = SoundCaptureDevice.Default;
            this.Mp3BitRate = Mp3BitRate.BitRate128;
            this.NormalizeVolume = false;
            this.OutputType = Outputs.Mp3;
            this.UseSynchronizationContext = true;
            this.WaveFormat = PcmSoundFormat.Pcm22kHz16bitMono;
            this.WaitOnStop = true;
        }

        public Mp3SoundCapture()
        {
            Initialize();
            InitializeComponent();
        }

        public Mp3SoundCapture(IContainer container)
        {
            Initialize();
            container.Add(this);

            InitializeComponent();
        }

        public class StoppedEventArgs
            : EventArgs
        {
            /// <summary>
            /// This property contains an exception thrown during stopping operation
            /// if not all data could be written to the output stream.
            /// </summary>
            public Exception Exception
            {
                get;
                private set;
            }

            /// <summary>
            /// True if all captured data has been successfully written to the output stream.
            /// </summary>
            public bool Success
            {
                get
                {
                    return Exception != null;
                }
            }

            /// <summary>
            /// Name of the file to which all data has just been written (or writing was aborted by an error).
            /// Null if capturing was not to a file stream.
            /// </summary>
            public string OutputFileName
            {
                get;
                private set;
            }

            /// <summary>
            /// The stream to which all data has just been written (or writing was aborted by an error).
            /// </summary>
            public Stream OutputStream
            {
                get;
                private set;
            }

            internal StoppedEventArgs(CapturingProcess capturing, Exception exception)
            {
                FileStream fileStream = capturing.Process == null
                    ? null
                    : capturing.Process.OutputStream as FileStream;

                this.OutputFileName =  fileStream == null
                    ? null
                    : fileStream.Name;

                this.OutputStream = capturing.Process == null
                    ? null
                    : capturing.Process.OutputStream;
                
                this.Exception = exception;
            }
        }

        internal class CapturingProcess
        {
            public SoundCapture Process
            {
                get;
                set;
            }

            public bool OwnsOutputStream
            {
                get;
                set;
            }

            public void CloseOwnedOutputStream()
            {
                if (OwnsOutputStream && Process != null && Process.OutputStream != null)
                {
                    Process.OutputStream.Close();
                }
            }
        }
    }
}
