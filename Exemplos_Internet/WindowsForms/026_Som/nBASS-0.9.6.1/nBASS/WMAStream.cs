using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

namespace nBASS
{
	public enum WMAStreamFlags 
	{
		Default = SampleInfoFlags.Default,
		Loop = SampleInfoFlags.Loop,
		ThreeDee = SampleInfoFlags.ThreeDee,
		FX = SampleInfoFlags.FX,
		//Decode,
		//AutoFree,
	}

	/// <summary>
	/// Summary description for WMAStream.
	/// </summary>
	public class WMAStream : AdvancedChannel
	{
		private bool disposed = false;

		internal WMAStream(IntPtr handle): base(handle){}
		
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
					_Free(base.Handle);

					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		#region DllImports

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_StreamFree")]
		extern static void _Free(IntPtr hwma);

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_StreamGetLength")]
		extern static long _GetLength(IntPtr handle);

		/// <summary>
		/// Retrieve the playback length (in bytes) of a WMA stream.
		/// </summary>
		public long Length 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				long output = _GetLength(base.Handle);
				if (output < 0) throw new WMAException();
				return output;
			}
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_StreamGetTags")]
		extern static IntPtr _GetTags(IntPtr handle, int tags);

		/// <summary>
		/// Retrieve the WMA tags, if available.
		/// </summary>
		/// <returns>An string array containing the tags</returns>
		public string[] GetTags()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			ArrayList tags = new ArrayList();
			IntPtr ptag = _GetTags(base.Handle, 0);
			do 
			{
				string tag = Marshal.PtrToStringAnsi(ptag);
				if (tag == "") break;
				else 
				{
					tags.Add(tag);
					ptag = new IntPtr(ptag.ToInt32() + (tag.Length + 1));
				}
			}
			while(true);

			string[] output = new string[tags.Count];
			for (int i = 0; i < output.Length; i++)
				output[i] = (string) tags[i];
			return output;
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_StreamPlay")]
		extern static int _Play(IntPtr handle, int flush, int flags);

		/// <summary>
		/// Play a WMA stream.
		/// </summary>
		/// <param name="flush">restart from the beginning.</param>
		/// <param name="loop">loop the file</param>
		public void Play(bool flush, bool loop)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			int flags = (int)WMAStreamFlags.Loop;
			if (!loop) flags = 0;
			if (_Play(base.Handle, Helper.Bool2Int(flush), flags) == 0)
				throw new WMAException();
			base.StartTimer();
		}

		public override void Stop()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			base.Stop();
			this.Position = 0;
		}
		
		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_ChannelSetPosition")]
		extern static int _SetWMAPosition(IntPtr handle, long pos);

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_ChannelGetPosition")]
		extern static long _GetWMAPosition(IntPtr handle);

		/// <summary>
		/// Get/Set the current playback position of a WMA channel in bytes
		/// </summary>
		public override long Position 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				long output = _GetWMAPosition(base.Handle);
				if (output < 0) throw new WMAException();
				return output;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				if (_SetWMAPosition(base.Handle, value) == 0) 
					throw new WMAException();
			}
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_ChannelSetSync")]
		static extern IntPtr _SetSync(IntPtr handle, 
			int atype, long param, GetSyncCallBack proc, int user);//TODO: OK

		void OnGetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user) //internal
		{
			OnEnd();
		}

		private IntPtr HSYNC;
		private GetSyncCallBack getSync;

		private EventHandler streamendstore;

		public override event EventHandler End 
		{
			add 
			{
				streamendstore += value;
				getSync += new GetSyncCallBack( OnGetSyncCallBack );
				HSYNC = _SetSync(base.Handle, 2, 0, getSync , 0);
			}
			remove
			{
				streamendstore -= value;
				getSync -= new GetSyncCallBack( OnGetSyncCallBack );
				_RemoveSync(base.Handle, HSYNC);
			}
		}

		protected override void OnEnd()
		{
			if (streamendstore != null) streamendstore(this, null);
		}

		// Remove a sync from a channel
		// handle : channel handle(HMUSIC/HSTREAM)
		// sync   : Handle of sync to remove
		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_ChannelRemoveSync")]
		static extern int _RemoveSync(IntPtr handle, IntPtr sync);//TODO: OK retrun bool

		//TODO: IWMReader
		//void *BASSWMADEF(BASS_WMA_GetIWMReader)(HSTREAM handle);
		/* Retrieve the IWMReader interface of a WMA stream. This allows direct
		access to the WMFSDK functions.
		handle : Channel handle
		RETURN : Pointer to the IWMReader object interface (NULL=error) */

		#endregion
	}
}
