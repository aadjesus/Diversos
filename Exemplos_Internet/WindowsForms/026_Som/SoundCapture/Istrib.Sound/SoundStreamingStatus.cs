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

namespace Istrib.Sound
{
    /// <summary>
    /// Status describing streaming process.
    /// </summary>
    public struct SoundStreamingStatus
    {
        private int bytesPassed;

        /// <summary>
        /// Number of bytes passed through the stream.
        /// </summary>
        public int BytesPassed
        {
            get
            {
                return bytesPassed;
            }
        }

        public SoundStreamingStatus(int bytesPassed)
        {
            this.bytesPassed = bytesPassed;
        }
    }
}
