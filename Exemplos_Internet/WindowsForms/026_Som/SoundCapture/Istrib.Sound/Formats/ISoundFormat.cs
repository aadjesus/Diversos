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
    /// Interface of a type which represents some class of sound format (e.g. PCM Wave or MP3).
    /// Instances of such type represent a specific, parametrized format (e.g. 8kHz, 8bit, Stereo Wave).
    /// </summary>
    /// <remarks>
    /// Format describes how the data is encoded (format type) and the encoding parameters (format properties).
    /// A specific format can contain information about format of sound from which
    /// the data was generated. E.g. MP3 format would be described by different structure (format type)
    /// but it could contain information about waveform from which the MP3 was encoded.
    /// </remarks>
    public interface ISoundFormat
    {
        string FormatClassName
        {
            get;
        }

        string Description
        {
            get;
        }
    }
}
