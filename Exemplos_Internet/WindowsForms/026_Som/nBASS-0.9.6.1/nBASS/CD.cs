using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace nBASS
{
#if(true)
	[Flags]
	public enum CDSetupFlags 
	{
		Default = DeviceSetupFlags.Default,
		LeaveVolume = DeviceSetupFlags.LeaveVolume,
		Volume1000 = DeviceSetupFlags.Volume1000,
	}
	/// <summary>
	/// CD class. Can be used by itself.
	/// </summary>
	public class CD : ChannelBase
	{
		private bool disposed = false;

		/// <summary>
		/// Initialize the CD functions, must be called before any other CD
		/// functions. The volume is initially set to 100 (the maximum), use
		/// BASS_ChannelSetAttributes() to adjust it.
		/// </summary>
		/// <param name="drive">The CD drive, for example: "d:" </param>
		/// <param name="flags">CDSetupFlags</param>
		public CD(string drive , CDSetupFlags flags) : base(IntPtr.Zero)
		{
			Init(drive, flags);	
		}

		/// <summary>
		/// Initialize the CD functions, must be called before any other CD
		/// functions. The volume is initially set to 100 (the maximum), use
		/// ChannelAttributes to adjust it. This will use the default drive.
		/// </summary>
		/// <param name="flags">CDSetupFlags</param>
		public CD(CDSetupFlags flags) : base(IntPtr.Zero)
		{
			Init(flags);
		}

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

					// this is a known problem
					this.Stop();
					this.Stop();
					_Free();

					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>
		/// Gets/Sets the current position
		/// </summary>
		[ReadOnly(true)]
		public override long Position 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				return _GetPosition(IntPtr.Zero);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				_SetPosition(IntPtr.Zero, value);
			}
		}

		public enum CDID 
		{
			//  CD ID flags, use with BASS_CDGetID
			IDENTITY = 0,
			UPC = 1,
			CDDB = 2,
			CDDB2 = 3,
		}

		#region CD
		[DllImport("bass.dll", EntryPoint = "BASS_CDInit")]
		static extern int _Init(
			StringBuilder drive,
			int flags);//OK return bool

		void Init(string drive, CDSetupFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("CD");

			StringBuilder sbdrv = new StringBuilder(drive);
			if (_Init(sbdrv, (int) flags) == 0) throw new BASSException();
		}

		void Init(CDSetupFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("CD");

			if (_Init(null, (int) flags) == 0) throw new BASSException();
		}

		// Free resources used by the CD
		[DllImport("bass.dll", EntryPoint = "BASS_CDFree")]
		static extern void _Free();//OK

		void Free()
		{
			_Free();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_CDInDrive")]
		static extern int _InDrive();//OK, return bool

		/// <summary>
		/// Check if there is a CD in the drive.
		/// </summary>
		public bool InDrive 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("CD");

				return Helper.Int2Bool(_InDrive());
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_CDDoor")]
		static extern int _Door(int dopen);//OK, dopen is bool, returns bool

		/// <summary>
		/// Opens or closes the CD door.
		/// </summary>
		/// <param name="open">open the door = true</param>
		public void Door(bool open)
		{
			if (this.disposed)
				throw new ObjectDisposedException("CD");

			if (_Door( Helper.Bool2Int(open) ) == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_CDGetID")]
		static extern IntPtr _GetID(int id);//OK, return LPSTR

		/// <summary>
		/// Retrieves identification info from the CD in the drive.
		/// </summary>
		/// <param name="id">CDID</param>
		/// <returns>The identification</returns>
		public string GetID(CDID id)
		{
			if (this.disposed)
				throw new ObjectDisposedException("CD");

			IntPtr idstr = _GetID( (int) id);
			if (idstr == IntPtr.Zero) throw new BASSException();
			return Marshal.PtrToStringAnsi( idstr);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_CDGetTracks")]
		static extern int _GetTracks();//OK err on -1 check

		/// <summary>
		/// The numbers of tracks on the CD
		/// </summary>
		public int Tracks 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("CD");

				int tracks = _GetTracks();
				if (tracks < 0) throw new BASSException();
				return tracks;
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_CDPlay")]
		static extern int _Play(int track, int pLoop, int wait);//OK, loop & wait is bool, returns bool

		/// <summary>
		/// Play a CD track.
		/// </summary>
		/// <param name="track">Track number to play (1=first)</param>
		/// <param name="loop">Loop the track</param>
		/// <param name="wait">don//t return until playback has started
		/// (some drives will always wait anyway)</param>
		public void Play(int track, bool loop, bool wait)
		{
			if (this.disposed)
				throw new ObjectDisposedException("CD");

			if (_Play( track, Helper.Bool2Int(loop), Helper.Bool2Int(wait)) == 0)
				throw new BASSException();
			base.StartTimer(); //only start if no exception
		}

		[DllImport("bass.dll", EntryPoint = "BASS_CDGetTrackLength")]
		static extern int _GetTrackLength(int track);//OK retrun uint -1 err 

		/// <summary>
		/// Retrieves the playback length (in milliseconds) of a cd track.
		/// </summary>
		/// <param name="track">The CD track (1=first)</param>
		/// <returns>The playback length (in milliseconds)</returns>
		public int GetTrackLength(int track)
		{
			if (this.disposed)
				throw new ObjectDisposedException("CD");

			int len = _GetTrackLength( track);
			if (len < 0) throw new BASSException();
			return len;
		}

		#endregion

	}
#endif
}
