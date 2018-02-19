using System;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// Provides methods for playing audio samples.
	/// </summary>
	public class Sample : IDisposable
	{
		private bool disposed = false;
		IntPtr handle;

		internal Sample(IntPtr handle)
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
					// free managed resources
				}

				// free unmanaged resources
				_Free(this.handle);

				this.disposed = true;
			}
		}

		~Sample()
		{
			Dispose(false);
		}

		internal IntPtr Handle 
		{
			get
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				return this.handle;
			}
		}
	
		#region Instance

		// Free a sample//s resources.
		// handle:    Sample handle
		[DllImport("bass.dll", EntryPoint = "BASS_SampleFree")]
		static extern void _Free(IntPtr handle);

		[DllImport("bass.dll", EntryPoint = "BASS_SampleGetInfo")]
		static extern int GetInfo(IntPtr handle, ref SampleInfo info); //return bool er code

		[DllImport("bass.dll", EntryPoint = "BASS_SampleSetInfo")]
		static extern int SetInfo(IntPtr handle, ref SampleInfo info);//return bool er code

		/// <summary>
		/// Sets/Retrieves a sample's current default attributes.
		/// </summary>
		/// <value>
		/// The SampleInfo struct
		/// </value>
		public SampleInfo Info 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Sample");

				SampleInfo si = new SampleInfo();
				if (GetInfo(this.handle, ref si) == 0) throw new BASSException();
				return si;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("Sample");

				if (SetInfo(this.handle, ref value) == 0) throw new BASSException();
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SamplePlay")]
		static extern IntPtr _Play(IntPtr handle); //return  HCHANNEL handle else err code

		/// <summary>
		/// Play a sample, using the sample's default attributes.
		/// </summary>
		/// <returns>A channel object</returns>
		public Channel Play()
		{
			IntPtr channel = _Play(this.handle); 
			if (channel == IntPtr.Zero) throw new BASSException();
			return new Channel(channel);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SamplePlayEx")]
		static extern IntPtr _PlayEx(IntPtr handle, int start, int freq, int volume, int pan, int pLoop); //loop is bool

		/// <summary>
		/// Play a sample, using specified attributes.
		/// </summary>
		/// <param name="start">Playback start position (in samples, not bytes)</param>
		/// <param name="freq">Playback Rate(-1 = Default)</param>
		/// <param name="volume">Volume (-1=default, 0=silent, 100=max)</param>
		/// <param name="panning">pan position(-101 = Default, -100 = Left, 0 = middle, 100 = Right)</param>
		/// <param name="loop">1 = Loop sample (-1=default)</param>
		/// <returns></returns>
		public Channel Play(int start, int freq, int volume, int panning, bool loop)
		{
			if (this.disposed)
				throw new ObjectDisposedException("Sample");

			//what should we do with the channel pointer???
			IntPtr channel = _PlayEx(this.handle, start, freq, volume, panning, Helper.Bool2Int( loop));
			if (channel == IntPtr.Zero) throw new BASSException();
			return new Channel(channel);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SamplePlay3D")]
		static extern IntPtr _Play3D(IntPtr handle, 
			ref Vector3D pos,
			ref Vector3D orient,
			ref Vector3D vel);

		/// <summary>
		/// Play a 3D sample, setting it's 3D position, orientation and velocity.
		/// </summary>
		/// <param name="p3d">A Channel3DPosition object to contain 3D parameters</param>
		/// <returns></returns>
		public Channel Play3D( Channel3DPosition p3d ) 
		{
			if (this.disposed)
				throw new ObjectDisposedException("Sample");

			Vector3D pos = p3d.pos;
			Vector3D orient = p3d.orient;
			Vector3D vel = p3d.vel;
			//what should we do with the channel pointer???
			IntPtr channel = _Play3D(this.handle, ref pos, ref orient, ref vel);
			if (channel == IntPtr.Zero) throw new BASSException();
			return new Channel(channel);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SamplePlay3DEx")]
		static extern IntPtr _Play3DEx(IntPtr handle, ref Vector3D pos,
			ref Vector3D orient, ref Vector3D vel, 
			int start, int freq, int volume, int pLoop); //loop is bool

		/// <summary>
		/// Play a 3D sample, using specified attributes.
		/// </summary>
		/// <param name="p3d">A Channel3DPosition object to contain 3D parameters</param>
		/// <param name="start">Playback start position (in samples, not bytes)</param>
		/// <param name="freq">Playback Rate(-1 = Default)</param>
		/// <param name="volume">Volume (-1=default, 0=silent, 100=max)</param>
		/// <param name="loop">1 = Loop sample (-1=default)</param>
		/// <returns></returns>
		public Channel Play3D( Channel3DPosition p3d ,
			int start, int freq, int volume, bool loop) 
		{
			if (this.disposed)
				throw new ObjectDisposedException("Sample");

			Vector3D pos = p3d.pos;
			Vector3D orient = p3d.orient;
			Vector3D vel = p3d.vel;
			IntPtr channel = _Play3DEx(this.handle, ref pos, ref orient, ref vel,
				start, freq, volume, Helper.Bool2Int(loop));
			if (channel == IntPtr.Zero) throw new BASSException(); //what happens if sound is playing?
			return new Channel(channel);
		}

		// 
		// handle : Handle of sample to stop
		[DllImport("bass.dll", EntryPoint = "BASS_SampleStop")]
		static extern int _Stop(IntPtr handle); //returns bool

		/// <summary>
		/// Stops all instances of a sample. For example, if a sample is playing
		/// simultaneously 3 times, calling this function will stop all 3 of them,
		/// which is obviously simpler than calling Channel.Stop() 3 times.
		/// </summary>
		public void Stop()
		{
			if (this.disposed)
				throw new ObjectDisposedException("Sample");

			if (_Stop(this.handle) == 0) throw new BASSException();
		}

		#endregion

	}


}
