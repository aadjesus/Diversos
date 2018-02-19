using System;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace nBASS
{
	/// <summary>
	/// Provides common channel methods
	/// </summary>
	public class Channel : ChannelBase
	{
		private bool disposed = false;

		internal Channel(IntPtr handle): base(handle){}

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

		#region Common Channel

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetFlags")]
		public static extern int _GetFlags(IntPtr handle);//OK return dword

		/// <summary>
		/// Get some info about a channel.
		/// </summary>
		public string Flags 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				int result = _GetFlags( base.Handle);
				if (result < 0) throw new BASSException();
				SampleInfoFlags flags = (SampleInfoFlags) result;
				return Helper.PrintFlags(flags);
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSet3DAttributes")]
		static extern int _Set3DAttributes(IntPtr handle, int mode, 
			float min, float max, int iangle, int oangle, int outvol);//OK handle is dword retun bool

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGet3DAttributes")]
		static extern int _Get3DAttributes(IntPtr handle,
			ref int mode, 
			ref float min, 
			ref float max, 
			ref int iangle, 
			ref int oangle, 
			ref int outvol);//OK return bool

		/// <summary>
		/// Gets/Sets a channel's 3D attributes.
		/// </summary>
		public Channel3DAttributes Attributes3D 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				int mode = 0;
				float min = 0;
				float max = 0; 
				int iangle = 0;
				int oangle = 0; 
				int outvol = 0;

				if (_Get3DAttributes( base.Handle, ref mode, ref min, ref max,
					ref iangle, ref oangle, ref outvol) == 0) 
					throw new BASSException();
				return new Channel3DAttributes(mode, min, max, iangle, oangle, outvol);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				if (_Set3DAttributes( base.Handle, value.mode, value.min, value.max,
					value.iangle, value.oangle, value.outvol) == 0)
					throw new BASSException();

			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSet3DPosition")]
		static extern int _Set3DPosition(IntPtr handle,
			ref Vector3D pos, 
			ref Vector3D orient, 
			ref Vector3D vel);//ok retun bool ,  how do we handle nulls???

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGet3DPosition")]
		static extern int _Get3DPosition(IntPtr handle,
			out Vector3D pos, 
			out Vector3D orient, 
			out Vector3D vel);//ok retun bool

		/// <summary>
		/// Gets/Sets a channel's 3D Position
		/// </summary>
		public virtual Channel3DPosition Position3D
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				Vector3D pos, orient, vel;
				if (_Get3DPosition( base.Handle, out pos, out orient, out vel) == 0)
					throw new BASSException();
				return new Channel3DPosition(pos, orient, vel);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				Vector3D pos = value.pos;
				Vector3D orient = value.orient;
				Vector3D vel = value.vel;
				if (_Set3DPosition( base.Handle, ref pos, ref orient, ref vel) == 0)
					throw new BASSException();
			}
		}

		// 
		// handle : channel handle(HCHANNEL / HSTREAM / HMUSIC)
		// mix    : 
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSetEAXMix")]
		static extern int _SetEAXMix(IntPtr handle, float mix);//OK return bool

		// Get the wet(reverb)/dry(no reverb) mix ratio on the channel.
		// handle:    channel handle(HCHANNEL / HSTREAM / HMUSIC)
		// mix    : Pointer to store the ratio at
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetEAXMix")]
		static extern int _GetEAXMix(IntPtr handle, ref float mix);//OK retrun bool

		/// <summary>
		/// Set the wet(reverb)/dry(no reverb) mix ratio on the channel. By default
		/// the distance of the sound from the listener is used to calculate the mix.
		/// </summary>
		/// <value>The ratio (0.0=reverb off, 1.0=max reverb, -1.0=let EAX calculate
		/// the reverb mix based on the distance)</value>
		/// <remarks>The channel must have 3D functionality enabled for the EAX environment
		/// to have any affect on it.</remarks> 
		[ReadOnly(true)]
		public float ChannelEAXMix 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				float output = 0;
				if (_GetEAXMix(base.Handle, ref output) == 0) 
					throw new BASSException();
				return output;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				if (_SetEAXMix( base.Handle , value) == 0)
					throw new BASSException();
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets/Sets the current position
		/// </summary>
		[ReadOnly(true)]
		public override long Position 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				long result = _GetPosition(base.Handle );
				if (result < 0) throw new BASSException();
				return result;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Channel");

				if (_SetPosition(base.Handle, value) == 0)
					throw new BASSException();
			}
		}

		#endregion
	}

}
