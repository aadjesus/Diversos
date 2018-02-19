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
using System.Collections.ObjectModel;
using System.IO;
using Istrib.Sound.Formats;

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// Logic of pipeline to be used by a pipeline component (like SoundCapture).
    /// </summary>
    internal class SoundFilterPipeline
    {
        private FilterCollection filters = new FilterCollection();
        private Stream output;
        private ISoundFormat inputFormat;

        /// <summary>
        /// Indicates if the pipeline is wired up.
        /// The pipeline cannot be modified after it has been wired up.
        /// </summary>
        public bool WiredUp
        {
            get
            {
                return output != null;
            }
        }

        /// <summary>
        /// Specifies pipeline filters and their order.
        /// This list cannot be modified after the pipeline has been wired up.
        /// </summary>
        public IList<SoundFilter> Filters
        {
            get
            {
                return filters;
            }
        }

        /// <summary>
        /// Writeable Stream which is the Pipeline entry point. Null until wiring up.
        /// Output if the pipeline contains no filters.
        /// </summary>
        public Stream Input
        {
            get
            {
                if (!WiredUp)
                {
                    return null;
                }

                return filters.Count > 0
                    ? filters[0].Input
                    : output;
            }
        }

        /// <summary>
        /// Format of data written to the Input stream.
        /// </summary>
        public ISoundFormat InputFormat
        {
            get
            {
                return inputFormat;
            }
        }

        /// <summary>
        /// Writeable Stream into which the result of the pipeline is written.
        /// Null until wiring up.
        /// </summary>
        public Stream Output
        {
            get
            {
                return output;
            }
        }

        /// <summary>
        /// This method has no effect after it has been called for the first time.
        /// </summary>
        /// <param name="output">Writeable Stream into which the result of the pipeline is written.</param>
        public void WireUp(ISoundFormat inputFormat, Stream output)
        {
            if (!WiredUp)
            {
                filters.WiredUp = true;
                this.output = output;
                this.inputFormat = inputFormat;

                if (filters.Count > 0)
                {
                    if (filters[0].RequiredInputFormatType != inputFormat.GetType())
                    {
                        throw new ArgumentException("Invalid Sound Filter Pipeline specification. The first filter '"
                                + filters[0] + "' must accept input data in format '" + inputFormat.FormatClassName + "' but it accepts format '"
                                + SoundFormatBase.GetSourceFormatClassName(filters[0].RequiredInputFormatType) + "'.");
                    }

                    filters[0].InputFormat = this.InputFormat;

                    for (int i = 1; i < filters.Count; i++)
                    {
                        if (filters[i - 1].OutputFormat.GetType() != filters[i].RequiredInputFormatType)
                        {
                            throw new ArgumentException("Invalid Sound Filter Pipeline specification. Filter '"
                                + filters[i] + "' requires input data to be in '"
                                + SoundFormatBase.GetSourceFormatClassName(filters[i].RequiredInputFormatType)
                                + "' format, but its input is connected with filter '" + filters[i - 1]
                                + "' which output format is '" + filters[i - 1].OutputFormat.FormatClassName + "'.");
                        }

                        filters[i - 1].Output = filters[i].Input;
                        filters[i].InputFormat = filters[i - 1].OutputFormat;
                    }

                    filters[filters.Count - 1].Output = this.Output;
                }
            }
        }

        /// <summary>
        /// To be called each time the streaming process is started or resumed.
        /// </summary>
        public void NotifyStreamingStartedResumed()
        {
            foreach (SoundFilter filter in filters)
            {
                filter.OnStreamingStartedResumed();
            }
        }

        /// <summary>
        /// To be called each time the streaming process is stopped or suspended.
        /// </summary>
        public void NotifyStreamingStoppedSuspended(SoundStreamingStatus streamingStatus)
        {
            foreach (SoundFilter filter in filters)
            {
                filter.OnStreamingStoppedSuspended(streamingStatus);
            }
        }

        public SoundFilterPipeline()
        {
            filters.WiredUp = false;
        }

        private class FilterCollection
            : Collection<SoundFilter>
        {
            public bool WiredUp
            {
                get;
                set;
            }

            protected override void ClearItems()
            {
                OnModifying();
                base.ClearItems();
            }

            protected override void InsertItem(int index, SoundFilter item)
            {
                OnModifying();
                base.InsertItem(index, item);
            }

            protected override void RemoveItem(int index)
            {
                OnModifying();
                base.RemoveItem(index);
            }

            protected override void SetItem(int index, SoundFilter item)
            {
                OnModifying();
                base.SetItem(index, item);
            }

            private void OnModifying()
            {
                if (WiredUp)
                {
                    throw new InvalidOperationException("Cannot add/remove/replace SoundFilter after the pipeline is wired up.");
                }
            }
        }
    }
}
