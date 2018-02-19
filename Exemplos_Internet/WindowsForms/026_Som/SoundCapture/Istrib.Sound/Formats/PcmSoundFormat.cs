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

namespace Istrib.Sound.Formats
{
    /// <summary>
    /// Raw wave PCM format - without any headers, just sample sequence.
    /// </summary>
    [SoundFormatClass("WAV-PCM")]
    public class PcmSoundFormat
        : SoundFormatBase, IEquatable<PcmSoundFormat>
    {
        public static readonly PcmSoundFormat Pcm8kHz8bitMono = new PcmSoundFormat(8000, 8, 1);
        public static readonly PcmSoundFormat Pcm8kHz8bitStereo = new PcmSoundFormat(8000, 8, 2);
        public static readonly PcmSoundFormat Pcm8kHz16bitMono = new PcmSoundFormat(8000, 16, 1);
        public static readonly PcmSoundFormat Pcm8kHz16bitStereo = new PcmSoundFormat(8000, 16, 2);

        public static readonly PcmSoundFormat Pcm11kHz8bitMono = new PcmSoundFormat(11025, 8, 1);
        public static readonly PcmSoundFormat Pcm11kHz8bitStereo = new PcmSoundFormat(11025, 8, 2);
        public static readonly PcmSoundFormat Pcm11kHz16bitMono = new PcmSoundFormat(11025, 16, 1);
        public static readonly PcmSoundFormat Pcm11kHz16bitStereo = new PcmSoundFormat(11025, 16, 2);

        public static readonly PcmSoundFormat Pcm22kHz8bitMono = new PcmSoundFormat(22050, 8, 1);
        public static readonly PcmSoundFormat Pcm22kHz8bitStereo = new PcmSoundFormat(22050, 8, 2);
        public static readonly PcmSoundFormat Pcm22kHz16bitMono = new PcmSoundFormat(22050, 16, 1);
        public static readonly PcmSoundFormat Pcm22kHz16bitStereo = new PcmSoundFormat(22050, 16, 2);

        public static readonly PcmSoundFormat Pcm44kHz8bitMono = new PcmSoundFormat(44100, 8, 1);
        public static readonly PcmSoundFormat Pcm44kHz8bitStereo = new PcmSoundFormat(44100, 8, 2);
        public static readonly PcmSoundFormat Pcm44kHz16bitMono = new PcmSoundFormat(44100, 16, 1);
        public static readonly PcmSoundFormat Pcm44kHz16bitStereo = new PcmSoundFormat(44100, 16, 2);

        public static readonly PcmSoundFormat Pcm48kHz8bitMono = new PcmSoundFormat(48000, 8, 1);
        public static readonly PcmSoundFormat Pcm48kHz8bitStereo = new PcmSoundFormat(48000, 8, 2);
        public static readonly PcmSoundFormat Pcm48kHz16bitMono = new PcmSoundFormat(48000, 16, 1);
        public static readonly PcmSoundFormat Pcm48kHz16bitStereo = new PcmSoundFormat(48000, 16, 2);

        /// <summary>
        /// List of most popular Pcm formats.
        /// </summary>
        public static readonly IEnumerable<PcmSoundFormat> StandardFormats = new PcmSoundFormat[]
        {
            Pcm8kHz8bitMono,
            Pcm8kHz8bitStereo,
            Pcm8kHz16bitMono,
            Pcm8kHz16bitStereo,

            Pcm11kHz8bitMono,
            Pcm11kHz8bitStereo,
            Pcm11kHz16bitMono,
            Pcm11kHz16bitStereo,

            Pcm22kHz8bitMono,
            Pcm22kHz8bitStereo,
            Pcm22kHz16bitMono,
            Pcm22kHz16bitStereo,

            Pcm44kHz8bitMono,
            Pcm44kHz8bitStereo,
            Pcm44kHz16bitMono,
            Pcm44kHz16bitStereo,

            Pcm48kHz8bitMono,
            Pcm48kHz8bitStereo,
            Pcm48kHz16bitMono,
            Pcm48kHz16bitStereo,
        };

        private int sampleRate;
        private short bitsPerSample;
        private short channels;

        /// <summary>
        /// Samples per second
        /// </summary>
        public int SampleRate
        {
            get
            {
                return sampleRate;
            }
        }

        public short BitsPerSample
        {
            get
            {
                return bitsPerSample;
            }
        }

        /// <summary>
        /// 1 - mono, 2 - stereo.
        /// </summary>
        public short Channels
        {
            get
            {
                return channels;
            }
        }

        public short BlockAlign
        {
            get
            {
                return (short)(Channels * (BitsPerSample / 8));
            }
        }

        public int AverageBytesPerSecond
        {
            get
            {
                return BlockAlign * SampleRate;
            }
        }

        public override string Description
        {
            get
            {
                return ToString();
            }
        }

        public static bool operator ==(PcmSoundFormat format1, PcmSoundFormat format2)
        {
            return format1.Equals(format2);
        }

        public static bool operator !=(PcmSoundFormat format1, PcmSoundFormat format2)
        {
            return !(format1 == format2);
        }

        public bool Equals(PcmSoundFormat other)
        {
            return ReferenceEquals(other, this)
                || (!ReferenceEquals(other, null)
                    && this.bitsPerSample == other.bitsPerSample
                    && this.channels == other.channels
                    && this.sampleRate == other.sampleRate);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PcmSoundFormat))
            {
                return false;
            }

            return Equals((PcmSoundFormat)obj);
        }

        public override int GetHashCode()
        {
            return bitsPerSample * channels * sampleRate;
        }

        public override string ToString()
        {
            string channelsText;
            switch (channels)
            {
                case 1:
                    channelsText = "Mono";
                    break;
                case 2:
                    channelsText = "Stereo";
                    break;
                default:
                    channelsText = channels + " channels";
                    break;
            };

            return string.Format("{0} Hz, {1} bit, {2}", sampleRate, bitsPerSample, channelsText);
        }

        public PcmSoundFormat(int sampleRate, short bitsPerSample, short channels)
        {
            this.sampleRate = sampleRate;
            this.bitsPerSample = bitsPerSample;
            this.channels = channels;
        }
    }
}
