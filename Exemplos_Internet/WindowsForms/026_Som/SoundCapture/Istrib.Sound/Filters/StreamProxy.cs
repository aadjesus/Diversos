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

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// Pretends to be a single stream but allows the underlaying stream to be replaced
    /// many times.
    /// </summary>
    internal class StreamProxy
        : Stream
    {
        private Stream target = null;

        /// <summary>
        /// The actual stream the data is written to.
        /// </summary>
        public Stream Target
        {
            get
            {
                return target;
            }
            set
            {
                if (value != target)
                {
                    if (target != null)
                    {
                        target.Flush();
                    }
                    target = value;
                }
            }
        }

        public override bool CanRead
        {
            get { return target.CanRead; }
        }

        public override bool CanSeek
        {
            get { return target.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return target.CanWrite; }
        }

        public override void Flush()
        {
            target.Flush();
        }

        public override long Length
        {
            get { return target.Length; }
        }

        public override long Position
        {
            get
            {
                return target.Position;
            }
            set
            {
                target.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return target.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return target.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            target.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            target.Write(buffer, offset, count);
        }
    }
}
