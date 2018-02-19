using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace nBASS
{
	[Flags]
	public enum RateFlags
	{
		Default = 0,
		Mono = SampleInfoFlags.Mono,
	}

	[Flags]
	public enum EncoderFlags
	{
		Default = 0,
		Mono = SampleInfoFlags.Mono,
		Tags = 0x10000
	}

	public enum WMATag 
	{
		Title,// Content title. 
		Author,// Name of the content author. 
		Description,// Description of the content. 
		Rating ,//Content rating. 
		Copyright,// Content copyright message. 
		WMAlbumTitle,// Album title. 
		WMPromotionURL,// URL to an HTML page containing related information. 
		WMAlbumCoverURL,// URL to an HTML page containing an image of the album cover. 
		WMGenre,// Genre of the music. 
		WMYear,// Year of publication of the music. 
	}

	/// <summary>
	/// Summary description for WMAEncoder.
	/// </summary>
	public class WMAEncoder : IDisposable
	{
		private bool disposed = false;
		IntPtr handle;

		internal WMAEncoder(IntPtr handle)
		{
			this.handle = handle;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					// free any managed resources
				}

				// free any unmanaged resources
				_Close(this.Handle);

				this.disposed = true;
			}
		}

		~WMAEncoder()
		{
			Dispose(false);
		}

		internal IntPtr Handle { get { return handle; }}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeGetRates")]
		private extern static IntPtr _GetRates(int freq, int flags);

		/// <summary>
		/// Retrieve a list of the encoding bitrates available for a
		/// specified input sample format.
		/// </summary>
		/// <param name="freq"> Sampling rate</param>
		/// <param name="flags">RateFlags</param>
		/// <returns>an array of bitrates</returns>
		public static int[] GetRates(int freq, RateFlags flags)
		{
			IntPtr prates = _GetRates(freq, (int) flags);
			if (prates == IntPtr.Zero) throw new WMAException();
			ArrayList rates = new ArrayList();
			int offset = 0;
			while(true)
			{
				int rate = Marshal.ReadInt32(prates, offset);
				if (rate == 0) break;
				else 
				{
					rates.Add(rate);
					offset += Marshal.SizeOf(rate); //the size of int32???
				}
			}
			int[] output = new int[rates.Count];
			for (int i = 0; i < output.Length; i++) 
				output[i] = (int) rates[i];
			return output;
		}
	
		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeOpenFile")]
		private extern static IntPtr _OpenFile(int freq, int flags, int bitrate, string filename);

		/// <summary>
		///  Initialize WMA encoding to a file.
		/// </summary>
		/// <param name="freq">Sampling rate</param>
		/// <param name="flags">EncoderFlags</param>
		/// <param name="bitrate">Encoding bitrate</param>
		/// <param name="filename">Filename</param>
		/// <returns>WMAEncoder object</returns>
		public static WMAEncoder OpenEncoderFile(int freq, EncoderFlags flags, int bitrate, string filename)
		{
			IntPtr hWMAenc = _OpenFile(freq, (int) flags, bitrate, filename);
			if (hWMAenc == IntPtr.Zero) throw new WMAException();
			return new WMAEncoder(hWMAenc);
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeOpenNetwork")]
		private extern static IntPtr _OpenNetwork(int freq, int flags, int bitrate, int port, int clients);

		/// <summary>
		/// Initialize WMA encoding to the network.
		/// </summary>
		/// <param name="freq">Sampling rate</param>
		/// <param name="flags">EncoderFlags</param>
		/// <param name="bitrate">Encoding bitrate</param>
		/// <param name="port">ort number for clients to conenct to (0=let system choose)</param>
		/// <param name="clients">Maximum number of clients that can connect</param>
		/// <returns>WMAEncoder object</returns>
		public static WMAEncoder OpenEncoderNetwork(int freq, EncoderFlags flags, int bitrate, int port, int clients)
		{
			IntPtr hWMAenc = _OpenNetwork(freq, (int) flags, bitrate, port, clients);
			if (hWMAenc == IntPtr.Zero) throw new WMAException();
			return new WMAEncoder(hWMAenc);
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeGetPort")]
		private extern static int _GetPort(IntPtr handle);

		/// <summary>
		/// Retrieve the port for clients to connect to a network encoder.
		/// </summary>
		public int Port 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int output = _GetPort(this.Handle);
				if (output == 0) throw new WMAException();
				return output;
			}
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeGetClients")]
		private extern static int _GetClients(IntPtr handle);

		/// <summary>
		/// Retrieve the number of clients connected.
		/// </summary>
		public int Clients 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int output = _GetClients(this.Handle);
				if (output == 0) throw new WMAException();
				return output;
			}
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeSetTag")]
		private static extern int _SetTag(IntPtr handle, string tag, string text);

		/// <summary>
		/// Set a tag. Requires that the BASS_WMA_ENCODE_TAGS flag was used in
		/// the BASS_WMA_EncodeOpenFile/Network call. 
		/// </summary>
		/// <param name="tag">WMATag</param>
		/// <param name="text">The tag's text</param>
		public void SetTag(WMATag tag, string text)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_SetTag(this.Handle, tag.ToString(), text) == 0)
				throw new WMAException();
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeSetTag")]
		private static extern int _SetTagDone(IntPtr handle, int tag, int text);

		/// <summary>
		/// Finish setting tags
		/// </summary>
		public void SetTagDone()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_SetTagDone(this.Handle, 0, 0) == 0)
				throw new WMAException();
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeWrite")]
		private static extern int _Write(IntPtr handle, 
			IntPtr pbuffer, int length);

		/// <summary>
		/// Encodes sample data, and writes it to the file or network.
		/// </summary>
		/// <param name="buffer">The buffer containing the sample data.</param>
		/// <param name="length">The number of BYTES in the buffer.</param>
		public void Write(short[] buffer, int length)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			IntPtr pbuffer = Marshal.AllocHGlobal(length * Marshal.SizeOf(typeof(short)));
			Marshal.Copy(buffer, 0, pbuffer, length);
			if (_Write(this.Handle, pbuffer, length) == 0)
			{
				Marshal.FreeHGlobal(pbuffer);
				throw new WMAException();
			}
			Marshal.FreeHGlobal(pbuffer);
		}

		public void Write(byte[] buffer, int length)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			IntPtr pbuffer = Marshal.AllocHGlobal(length * Marshal.SizeOf(typeof(byte)));
			Marshal.Copy(buffer, 0, pbuffer, length);
			if (_Write(this.Handle, pbuffer, length) == 0)
			{
				Marshal.FreeHGlobal(pbuffer);
				throw new WMAException();
			}
			Marshal.FreeHGlobal(pbuffer);
		}

		public void Write(IntPtr pbuffer, int length) //tempory to test passing pointer directly, works
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_Write(this.Handle, pbuffer, length) == 0)
				throw new WMAException();
		}

		//BOOL BASSWMADEF(BASS_WMA_EncodeWrite)(HWMENCODE handle, void *buffer, DWORD length);
	
		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_EncodeClose")]
		private static extern void _Close(IntPtr handle);

		/// <summary>
		/// Finish encoding and close the file or network port.
		/// </summary>
		public void Close()
		{
			Dispose();
		}
	}
}
