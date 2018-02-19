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
using Istrib.Sound.Formats;
using System.IO;

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// This filter accepts and outputs some data. The only thing it does
    /// is a buffering. All data streamed within a single Start()-Stop() session
    /// is buffered and available in RewriteData(...) method. The RewriteData method
    /// is responsible for passing the data further to the next element of filter pipeline.
    /// </summary>
    public abstract class BufferFilter
        : SoundFilter, IDisposable
    {
        private string bufferFilePath = null;
        private FileStream cachedBufferStream = null;

        private StreamProxy inputStreamProxy = new StreamProxy();

        private FileStream BufferStream
        {
            get
            {
                if (cachedBufferStream == null)
                {
                    CreateBuffer();
                }

                return cachedBufferStream;
            }
        }

        public override System.IO.Stream Input
        {
            get 
            {
                if (inputStreamProxy.Target == null)
                {
                    inputStreamProxy.Target = BufferStream;
                }

                return inputStreamProxy;
            }
        }

        /// <summary>
        /// Called at the end of each streaming Start()-Stop() session passing all
        /// data streamed in input reader.
        /// Must synchronously read all data from the input and write it to the output. 
        /// Usually some transformation is performed in-between.
        /// </summary>
        /// <param name="input">Read to end to get all data gathered during Start()-Stop() streaming
        /// session.</param>
        /// <param name="output">Write transformed data to this writer.</param>
        protected abstract void RewriteData(BinaryReader input, BinaryWriter output);

        protected internal override void OnStreamingStoppedSuspended(SoundStreamingStatus streamingStatus)
        {
            try
            {
                base.OnStreamingStoppedSuspended(streamingStatus);
                BufferStream.Position = 0; //Rewind to the beginning.

                BinaryWriter output = new BinaryWriter(Output);
                BinaryReader input = new BinaryReader(BufferStream);
                RewriteData(input, output);

                output.Flush();
            }
            finally
            {
                DestroyBuffer();
            }
        }

        private void CreateBuffer()
        {
            bufferFilePath = Path.GetTempFileName();
            cachedBufferStream = new FileStream(bufferFilePath, FileMode.Create, FileAccess.ReadWrite);
        }

        private void DestroyBuffer()
        {
            inputStreamProxy.Target = null;

            if (cachedBufferStream != null)
            {
                cachedBufferStream.SetLength(0);
                cachedBufferStream.Close();
                cachedBufferStream = null;
            }

            if (bufferFilePath != null && File.Exists(bufferFilePath))
            {
                File.Delete(bufferFilePath);
            }
        }

        public void Dispose()
        {
            DestroyBuffer();
        }
    }
}
