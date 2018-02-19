using System;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// AdvancedChannel this class is not directly used
	/// </summary>
	public abstract class AdvancedChannel : Channel
	{
		private bool disposed = false;

		internal AdvancedChannel(IntPtr handle): base(handle) {}

		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				try
				{
					if (disposing)
					{
						// release any managed resources
					}

					// release any unmanaged resources

					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		#region Other Common

		// Setup a user DSP function on a channel. When multiple DSP functions
		// are used on a channel, they are called in the order that they were added.
		// handle:  channel handle(HMUSIC / HSTREAM)
		// proc   : User defined callback function
		// user   : The //user// value passed to the callback function
		// RETURN : DSP handle (NULL=error)
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSetDSP")]
		static extern IntPtr _SetDSP(IntPtr handle, DSPCallBack proc, int user);//TODO: Make callback

		// Remove a DSP function from a channel
		// handle : channel handle(HMUSIC / HSTREAM)
		// dsp    : Handle of DSP to remove */
		// RETURN : BASSTRUE / BASSFALSE
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelRemoveDSP")]
		static extern int _RemoveDSP(IntPtr handle, IntPtr dsp);//TODO: OK,

		// Setup a sync on a channel. Multiple syncs may be used per channel.
		// handle : Channel handle (currently there are only HMUSIC/HSTREAM syncs)
		// atype  : Sync type (BASS_SYNC_xxx type & flags)
		// param  : Sync parameters (see the BASS_SYNC_xxx type description)
		// proc   : User defined callback function (use AddressOf SYNCPROC)
		// user   : The //user// value passed to the callback function
		// Return : Sync handle(Null = Error)
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSetSync")]
		static extern IntPtr _SetSync(IntPtr handle, 
			int atype, long param, GetSyncCallBack proc, int user);//TODO: OK

		void OnGetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user) //internal
		{
			OnEnd();
		}

		private IntPtr HSYNC;
		private GetSyncCallBack getSync;

		private EventHandler streamendstore;

		public virtual event EventHandler End 
		{
			add 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				streamendstore += value;
				getSync += new GetSyncCallBack( OnGetSyncCallBack );
				HSYNC = _SetSync(base.Handle, 2, 0, getSync , 0);
			}
			remove
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				streamendstore -= value;
				getSync -= new GetSyncCallBack( OnGetSyncCallBack );
				_RemoveSync(base.Handle, HSYNC);
			}
		}

		protected virtual void OnEnd()
		{
			if (streamendstore != null) streamendstore(this, null);
		}

		// Remove a sync from a channel
		// handle : channel handle(HMUSIC/HSTREAM)
		// sync   : Handle of sync to remove
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelRemoveSync")]
		static extern int _RemoveSync(IntPtr handle, IntPtr sync);//TODO: OK retrun bool

		// Retrieves upto "length" bytes of the channel//s current sample data. This is
		// useful if you wish to "visualize" the sound.
		// handle:  Channel handle(HMUSIC / HSTREAM, or RECORDCHAN)
		// buffer : Location to write the data
		// length : Number of bytes wanted, or a BASS_DATA_xxx flag
		// RETURN : Number of bytes actually written to the buffer (-1=error)
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData")]
		static extern int _GetData(IntPtr handle, float[] buffer, int Length);//TODO: OK return dword

		/// <summary>
		/// Retrieves upto "length" bytes of the channel//s current sample data. This is
		/// useful if you wish to "visualize" the sound.
		/// </summary>
		/// <param name="buffer">A buffer to place retrieved data</param>
		/// <param name="flags">ChannelDataFlags</param>	
		public int GetData(float[] buffer, ChannelDataFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			int output = _GetData(base.Handle, buffer, (int) flags);
			if ( output < 0) throw new BASSException();
			return output;
		}

		static int GetDataLength(ChannelDataFlags flags)
		{
			switch(flags)
			{
				case ChannelDataFlags.FFT512:
					return 256;
				case ChannelDataFlags.FFT1024:
					return 512;
				case ChannelDataFlags.FFT2048:
					return 1024;
				case ChannelDataFlags.SFFT512:
					return 512;
				case ChannelDataFlags.SFFT1024:
					return 1024;
				case ChannelDataFlags.SFFT2048:
					return 2048;
				default:
					return 1024;
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData")]
		static extern int _GetData(IntPtr handle, [Out] byte[] buffer, int Length);//TODO: OK return dword

		/// <summary>
		/// Retrieves upto "length" bytes of the channel//s current sample data. 8-bit data 
		/// </summary>
		/// <param name="buffer">A buffer to place retrieved data</param>
		/// <param name="length">length in bytes</param>
		public int GetData(byte[] buffer, int length)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			int output = _GetData(base.Handle, buffer, length);
			if ( output < 0) throw new BASSException();
			return output;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData")]
		static extern int _GetData(IntPtr handle, [Out] short[] buffer, int Length);//TODO: OK return dword

		/// <summary>
		/// Retrieves upto "length" bytes of the channel//s current sample data. 16-bit data
		/// </summary>
		/// <param name="buffer">A buffer to place retrieved data</param>
		/// <param name="length">length in bytes</param>
		public int GetData(short[] buffer, int length)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			int output = _GetData(base.Handle, buffer, length);
			if ( output < 0) throw new BASSException();
			return output;
		}

		// 
		// handle : channel handle(HMUSIC / HSTREAM, or RECORDCHAN)
		// RETURN : LOWORD=left level (0-128) HIWORD=right level (0-128) (-1=error)
		//          Use LoWord and HiWord functions on return function.
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetLevel")]
		static extern int _GetLevel(IntPtr handle);//OK return dword

		/// <summary>
		/// Calculate a channel's current left output level.
		/// </summary>
		public int LeftLevel 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int result = _GetLevel(base.Handle);
				if(result < 0) return 0;/*throw new BASSException();*/
				return Helper.LoWord(result);
			}
		}

		/// <summary>
		/// Calculate a channel's current right output level.
		/// </summary>
		public int RightLevel 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int result = _GetLevel(base.Handle);
				if(result < 0) return 0;/*throw new BASSException();*/
				return Helper.HiWord(result);
			}
		}

#if(true)
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSetFX")]
		static extern IntPtr _SetFX(IntPtr handle, int atype);//OK

		/// <summary>
		/// Setup a DX8 effect on a channel. Can only be used when the channel
		/// is not playing. Use FX.Parameters to set the effect parameters.
		/// Obviously requires DX8.
		/// </summary>
		/// <param name="chanfx">ChannelFX</param>
		/// <returns>An FX object</returns>
		public FX SetFX(ChannelFX chanfx)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			IntPtr fx = _SetFX(base.Handle, (int)chanfx);
			if (fx == IntPtr.Zero) throw new BASSException();
			return new FX(fx, chanfx);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelRemoveFX")]
		static extern int _RemoveFX(IntPtr handle, IntPtr fx);//OK

		/// <summary>
		/// Remove a DX8 effect from a channel. Can only be used when the
		/// channel is not playing.
		/// </summary>
		/// <param name="fx">The FX object to remove</param>
		public void RemoveFX(FX fx)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if(_RemoveFX(base.Handle, fx.Handle) == 0)
				throw new BASSException();
		}
#endif

		#endregion

	}
}
