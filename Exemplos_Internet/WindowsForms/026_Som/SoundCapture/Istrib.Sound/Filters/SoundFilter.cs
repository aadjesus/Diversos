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
using Istrib.Sound.Formats;

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// Part of a filter pipeline. Filter reads data from one stream (connected to
    /// output of the filter preceeding in the pipeline), processes it (e.g. compresses)
    /// and writes it to the output stream (possibly connected to the successor in the pipeline).
    /// A pipeline component (e.g. SoundCapture) is responsible for wiring filters up.
    /// </summary>
    public abstract class SoundFilter
    {
        /// <summary>
        /// Type of ISoundFormat relating to format of data which can be written to the
        /// Input stream.
        /// </summary>
        public abstract Type RequiredInputFormatType
        {
            get;
        }

        /// <summary>
        /// Writeable Stream into which the raw data is written.
        /// The filter must it then process it and write result
        /// to the Output stream.
        /// </summary>
        public abstract Stream Input
        {
            get;
        }

        /// <summary>
        /// Actual format of data written into Input stream.
        /// This property must is initialized by filter pipeline.
        /// </summary>
        public ISoundFormat InputFormat
        {
            get;
            internal set;
        }

        /// <summary>
        /// Writeable Stream where the processed data is written after reading it from
        /// the Input stream. This property must is initialized by filter pipeline.
        /// </summary>
        public Stream Output
        {
            get;
            internal set;
        }

        /// <summary>
        /// Format of data produced by this filter and written to the Output stream.
        /// The value may be computed basing on value of InputFormat property.
        /// </summary>
        public abstract ISoundFormat OutputFormat
        {
            get;
        }

        /// <summary>
        /// Called each time the streaming process is started or resumed.
        /// </summary>
        internal protected virtual void OnStreamingStartedResumed()
        {
        }

        /// <summary>
        /// Called each time the streaming process is stopped or suspended.
        /// </summary>
        /// <param name="streamingStatus">Current status of streaming process (summary of all
        /// stop-resume sessions).</param>
        internal protected virtual void OnStreamingStoppedSuspended(SoundStreamingStatus streamingStatus)
        {
        }
    }
}
