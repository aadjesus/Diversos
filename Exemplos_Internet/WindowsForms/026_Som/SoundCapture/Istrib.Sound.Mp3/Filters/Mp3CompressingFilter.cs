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
using System.Linq;
using System.Text;

namespace Istrib.Sound.Filters
{
    using Formats;
    using System.IO;

    /// <summary>
    /// This filter accepts raw PCM data, compresses it and outputs MP3 data.
    /// </summary>
    public class Mp3CompressingFilter
        : SoundFilter
    {
        private InputSplittingStream input = new InputSplittingStream();

        /// <summary>
        /// Created separately for each streaming Start()-Stop() session.
        /// </summary>
        private Yeti.MMedia.Mp3.Mp3Writer mp3Writer = null;

		public Mp3BitRate Mp3BitRate
        {
            get;
            set;
        }

        public override Type RequiredInputFormatType
        {
            get { return typeof(PcmSoundFormat); }
        }

        public override Stream Input
        {
            get { return input; }
        }

        public override ISoundFormat OutputFormat
        {
            get 
            { 
                return new Mp3SoundFormat(Mp3BitRate, (PcmSoundFormat)InputFormat); 
            }
        }

        private WaveLib.WaveFormat YetiWaveFormat
        {
            get
            {
                PcmSoundFormat inputFormat = (PcmSoundFormat)InputFormat;
                return new WaveLib.WaveFormat(
                    inputFormat.SampleRate,
                    inputFormat.BitsPerSample,
                    inputFormat.Channels);
            }
        }

        protected override void OnStreamingStartedResumed()
        {
            base.OnStreamingStartedResumed();

            WaveLib.WaveFormat waveFormat = YetiWaveFormat;
            Mp3SoundFormat outputFormat = (Mp3SoundFormat)OutputFormat;

            mp3Writer = new Yeti.MMedia.Mp3.Mp3Writer(Output,
                waveFormat, new Yeti.Lame.BE_CONFIG(waveFormat, outputFormat.BitRate, 
					Yeti.Lame.LAME_QUALITY_PRESET.LQP_NORMAL_QUALITY));
        }

        private void CompressData(object sender, InputSplittingStream.ChunkWrittenEventArgs e)
        {
            mp3Writer.Write(e.Chunk, 0, e.Chunk.Length);
        }

        protected override void OnStreamingStoppedSuspended(SoundStreamingStatus streamingStatus)
        {
            base.OnStreamingStoppedSuspended(streamingStatus);
            if (mp3Writer != null)
            {
                mp3Writer.Close();
                mp3Writer = null;
            }
        }

        public Mp3CompressingFilter()
        {
            this.Mp3BitRate = Mp3BitRate.BitRate128;
            this.input.ChunkWritten += new EventHandler<InputSplittingStream.ChunkWrittenEventArgs>(CompressData);
        }
    }
}
