using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace nBASS
{

	/// <summary>
	/// Summary description for Stream.
	/// </summary>
	public class Stream : AdvancedChannel
	{
		#region Enums

		[Flags()]
			public enum Mode2 // wtf ??? 
		{
			AUTOFREE = 262144, //  automatically free the stream when it stop/ends
			RESTRATE = 524288, //  restrict the download rate of internet file streams
			BLOCK = 1048576, //  download & play internet file stream (MPx) in small blocks
			DECODE = 0x200000, //  don't play the stream, only decode (BASS_ChannelGetData)
			META = 0x400000, //  request metadata from a Shoutcast stream
		}

		[Flags()]
			public enum MP3 
		{
			HALFRATE = 65536, //  reduced quality MP3/MP2/MP1 (half sample rate)
			SETPOS = 131072, //  enable pin-point seeking on the MP3/MP2/MP1/OGG
		}

		private enum Tag
		{
			// **********************************************
			// * BASS_StreamGetTags flags : what// s returned *
			// **********************************************
			ID3 = 0, // ID3v1 tags : 128 byte block
			ID3V2 = 1, // ID3v2 tags : variable length block
			OGG = 2, // OGG comments : array of null-terminated strings
			HTTP = 3, // HTTP headers : array of null-terminated strings
			ICY = 4, // ICY headers : array of null-terminated strings
			META = 5, // ICY metadata : null-terminated string
		}



		public enum Mode
		{
			//the position to retrieve (0=decoding, 1=download, 2=end)
			Decoding = 0,
			Download = 1,
			End = 2,
		}

		#endregion

		private bool disposed = false;

		internal Stream(IntPtr handle):base(handle)
		{
		}
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				try
				{
					if (disposing)
					{
						// free managed resources
					}

					// free unmanaged resources
					Free();

					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		#region Unique Instance
		// Free a sample stream//s resources.
		// stream:    stream handle
		[DllImport("bass.dll", EntryPoint = "BASS_StreamFree")]
		private static extern void _Free(IntPtr handle);//OK

		void Free()
		{
			_Free(base.Handle);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamGetLength")]
		private static extern long _GetLength(IntPtr handle);
		
		/// <summary>
		/// Retrieves the playback length (in bytes) of a file stream. It's not always
		/// possible to 100% accurately guess the length of a stream, so the length returned
		/// may be only an approximation when using some WAV codecs.
		/// </summary>
		public virtual long Length 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Stream");

				long result = _GetLength(base.Handle);
				if (result < 0) throw new BASSException();
				return result;
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamGetTags")]
		static extern IntPtr _GetTags(IntPtr handle, 
			[MarshalAs(UnmanagedType.U4)] Tag tags);//OK returns LPSTR
		
		public ID3v1Tag TagID3 
		{
			get 
			{
				byte[] bytetag = new byte[128];
				IntPtr ptr = _GetTags(base.Handle, Tag.ID3);
				if (ptr != IntPtr.Zero) 
					Marshal.Copy(ptr, bytetag, 0, bytetag.Length);
				return new ID3v1Tag(bytetag);
			}
		}

		public IntPtr TagID3V2 
		{
			get 
			{
				return _GetTags(base.Handle, Tag.ID3V2);
			}
		}

		public string[] TagOGG 
		{
			get 
			{
				return GetTagGen(Tag.OGG);
			}
		}

		public string[] TagHTTP 
		{
			get 
			{
				return GetTagGen(Tag.HTTP);
			}
		}

		public string[] TagICY
		{
			get 
			{
				return GetTagGen(Tag.ICY);
			}
		}

		public string TagMETA
		{
			get 
			{
				IntPtr ptr = _GetTags(base.Handle, Tag.META);
				if (ptr == IntPtr.Zero)
					throw new BASSException();
				return Marshal.PtrToStringAnsi(ptr);
			}
		}

		private string[] GetTagGen(Tag ttag)
		{
			ArrayList tags = new ArrayList();
			IntPtr ptr = _GetTags(base.Handle, ttag);
			if (ptr == IntPtr.Zero)
				throw new BASSException();
			do 
			{
				string tag = Marshal.PtrToStringAnsi(ptr);
				if (tag == "") break;
				else 
				{
					tags.Add(tag);
					ptr = new IntPtr(ptr.ToInt32() + (tag.Length + 1));
				}
			}
			while(true);

			string[] output = new string[tags.Count];
			for (int i = 0; i < output.Length; i++)
				output[i] = (string) tags[i];
			return output;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamPreBuf")]
		static extern int _PreBuf(IntPtr handle);//OK return bool err

		/// <summary>
		/// Pre-buffer initial sample data ready for playback.
		/// </summary>
		public void PreBuffer()
		{
			if (this.disposed)
				throw new ObjectDisposedException("Stream");

			if ( _PreBuf( base.Handle ) == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamPlay")]
		static extern int _Play(IntPtr handle, int flush, int flags);//OK, flush is bool, return bool

		/// <summary>
		/// Play a sample stream, optionally flushing the buffer first.
		/// </summary>
		/// <param name="flush">Flush buffer contents. If you stop a stream and then want to
		/// continue it from where it stopped, don't flush it. Flushing
		/// a file stream causes it to restart from the beginning.</param>
		/// <param name="flags">StreamFlags</param>
		public void Play(bool flush, StreamPlayFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("Stream");

			if (_Play(base.Handle, Helper.Bool2Int( flush), (int) flags) == 0)
				throw new BASSException();
			//start progress timer
			base.StartTimer();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamGetFilePosition")]
		static extern int _GetFilePosition(IntPtr handle, int mode);//OK returns dwoord so look for err

		/// <summary>
		/// Retrieves the file position of the decoding, the download (if streaming from
		/// the internet), or the end (total length). Obviously only works with file streams.
		/// </summary>
		/// <param name="mode">The position to retrieve (0=decoding, 1=download, 2=end)</param>
		/// <returns>The position</returns>
		public int GetFilePosition(Mode mode)
		{
			if (this.disposed)
				throw new ObjectDisposedException("Stream");

			int result = _GetFilePosition( base.Handle, (int) mode);
			if (result < 0) throw new BASSException();
			return result;
		}

		#endregion

	}
}
