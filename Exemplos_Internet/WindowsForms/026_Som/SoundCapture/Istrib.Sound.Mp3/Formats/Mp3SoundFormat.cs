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

namespace Istrib.Sound.Formats
{
    [SoundFormatClass("MP3")]
    public partial class Mp3SoundFormat
        : SoundFormatBase, IEquatable<Mp3SoundFormat>
    {
        /// <summary>
        /// In this library the MP3 format can be acquired only by compressing this subset if
        /// WAV formats. However not each source format is compatible with each bit rate.
		/// Use Mp3BitRate.CompatibleSourceFormats property or PcmSoundFormat.GetCompatibleMp3BitRates() method
		/// to get allowed combinations.
        /// </summary>
        public static IEnumerable<PcmSoundFormat> AllSourceFormats
        {
			get
			{
				foreach (MpegVersion version in MpegVersion.All)
				{
					foreach (PcmSoundFormat sourceFormat in version.CompatibleSourceFormats)
					{
						yield return sourceFormat;
					}
				}
			}
        }

        /// <summary>
        /// WAV format of data from which the MP3 will be created.
        /// </summary>
        public PcmSoundFormat SourceFormat
        {
            get;
            private set;
        }

        /// <summary>
        /// MPE bit rate.
        /// </summary>
		public Mp3BitRate BitRate
        {
            get;
            private set;
        }

        public override string Description
        {
            get 
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} kbit/s MP3", BitRate);
        }

        public static bool operator==(Mp3SoundFormat format1, Mp3SoundFormat format2)
        {
            return object.ReferenceEquals(format1, format2)
                || (!ReferenceEquals(format1, null) && format1.Equals(format2));
        }

        public static bool operator !=(Mp3SoundFormat format1, Mp3SoundFormat format2)
        {
            return !(format1 == format2);
        }

        #region IEquatable<Mp3SoundFormat> Members

        public bool Equals(Mp3SoundFormat other)
        {
            return other != null
                && other.BitRate == BitRate
                && other.SourceFormat == SourceFormat;
        }

        #endregion

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Mp3SoundFormat);
        }

        public override int GetHashCode()
        {
            return BitRate.GetHashCode() * SourceFormat.GetHashCode();
        }

        /// <summary>
        /// Creates MP3 format.
        /// </summary>
        /// <param name="bitRate">MPE bit rate (kbit/s).</param>
        /// <param name="sourceFormat">Format of WAV data from which MP3 data is created.</param>
		/// <param name="quality">Quality preset.</param>
        public Mp3SoundFormat(Mp3BitRate bitRate, PcmSoundFormat sourceFormat)
        {
            this.BitRate = bitRate;
            this.SourceFormat = sourceFormat;
        }
    }
}
