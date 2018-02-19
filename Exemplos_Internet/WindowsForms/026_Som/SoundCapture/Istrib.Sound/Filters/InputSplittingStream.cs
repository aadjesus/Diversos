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
	/// Buffers written data and notifies client when a next chunk of data has been written
	/// to this stream.
	/// This stream is write-only.
	/// </summary>
	public class InputSplittingStream
		: Stream
	{
		/// <summary>
		/// Fired after the next written chunk is ready for consumption.
		/// Also on stream disposal and flush.
		/// </summary>
		public event EventHandler<ChunkWrittenEventArgs> ChunkWritten;
		/// <summary>
		/// Fired on object disposal after all data has been written.
		/// </summary>
		public event EventHandler Finished;

		private MemoryStream buffer = new MemoryStream();
		private long chunkSize = 1024;

		/// <summary>
		/// How large chunks are notified.
		/// </summary>
		public long ChunkSize
		{
			get
			{
				return chunkSize;
			}
			set
			{
				chunkSize = value;
			}
		}

		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return buffer.CanWrite;
			}
		}

		public override void Flush()
		{
			buffer.Flush();
			TrackChunk(true);
		}

		public override long Length
		{
			get
			{
				return buffer.Length;
			}
		}

		public override long Position
		{
			get
			{
				return buffer.Position;
			}
			set
			{
				throw new NotImplementedException("InputSplittingStream does not support reading or seeking.");
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException("InputSplittingStream does not support reading or seeking.");
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException("InputSplittingStream does not support reading or seeking.");
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException("InputSplittingStream does not support length setting.");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this.buffer.Write(buffer, offset, count);
			TrackChunk();
		}

		protected override void Dispose(bool disposing)
		{
			TrackChunk(true);
			base.Dispose(disposing);
			OnFinished();
		}

		private void TrackChunk()
		{
			TrackChunk(false);
		}

		private void TrackChunk(bool forceChunkNotification)
		{
			if (forceChunkNotification || buffer.Length >= ChunkSize)
			{
				OnChunkWritten(buffer.ToArray());
				buffer.Position = 0;
				buffer.SetLength(0);
			}
		}

		private void OnChunkWritten(byte[] chunk)
		{
			if (ChunkWritten == null)
			{
				throw new InvalidOperationException("InputSplittingStream.ChunkWritten event not handled. Data written to this stream would be lost.");
			}

			if (chunk.Length > 0)
			{
				ChunkWritten(this, new ChunkWrittenEventArgs(chunk));
			}
		}

		private void OnFinished()
		{
			if (Finished != null)
			{
				Finished(this, new EventArgs());
			}
		}

		public class ChunkWrittenEventArgs
				  : System.EventArgs
		{
			private byte[] chunk;

			public byte[] Chunk
			{
				get
				{
					return chunk;
				}
			}

			internal ChunkWrittenEventArgs(byte[] chunk)
			{
				this.chunk = chunk;
			}
		}
	}
}
