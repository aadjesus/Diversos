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
    /// PCM format preceeded with RIFF header (like in WAV files stored on disk).
    /// </summary>
    [SoundFormatClass("WAV-RIFF")]
    public class RiffSoundFormat
        : PcmSoundFormat
    {
        public override string Description
        {
            get 
            {
                return "RIFF " + base.Description;
            }
        }

        public RiffSoundFormat(PcmSoundFormat rawPcmSoundFormat)
            : base(rawPcmSoundFormat.SampleRate, rawPcmSoundFormat.BitsPerSample, rawPcmSoundFormat.Channels)
        {
        }
    }
}
