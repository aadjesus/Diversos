using System;
using System.ComponentModel;
using System.Timers;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// ChannelBase. The class is not used directly.
	/// </summary>
	public abstract class ChannelBase : IDisposable
	{
		private bool disposed = false;

		IntPtr handle;
		Timer progresstimer;
		BASS owner;

		internal ChannelBase(IntPtr handle)
		{
			this.handle = handle;
			progresstimer = new Timer(20);
			progresstimer.Elapsed += new ElapsedEventHandler(ProgressTimerElapsed);
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
				this.disposed = true;
			}
		}

		~ChannelBase()
		{
			Dispose(false);
		}

		public double ProgressInterval 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				return progresstimer.Interval;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				this.progresstimer.Interval = value;
			}
		}

		public BASS Owner 
		{
			get
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				return owner;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				owner = value;
				//progresstimer.SynchronizingObject = owner.ParentForm;
			}
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

		void ProgressTimerElapsed(object sender, ElapsedEventArgs e)
		{
			OnProgress();
		}

		protected void StartTimer()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			progresstimer.Start();
		}

		public event BASSProgessHandler Progress;

		protected virtual void OnProgress() 
		{
			if (Progress != null) Progress(this);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelBytes2Seconds")]
		static extern float _Bytes2Seconds(IntPtr handle, long pos);//OK

		/// <summary>
		/// Translate a byte position into time (seconds)
		/// </summary>
		/// <param name="pos">The position to translate</param>
		/// <returns>The millisecond position</returns>
		public float Bytes2Seconds(long pos)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			float result = _Bytes2Seconds(this.handle, pos);
			if (result < 0) throw new BASSException();
			return result;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSeconds2Bytes")]
		static extern long _Seconds2Bytes(IntPtr handle, float pos);//OK make decimal

		/// <summary>
		/// Translate a time (seconds) position into bytes 
		/// </summary>
		/// <param name="pos">The position to translate</param>
		/// <returns>The byte position</returns>
		public long Seconds2Bytes(float pos)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			long result = _Seconds2Bytes(this.handle, pos);
			if (result < 0) throw new BASSException();
			return result;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelIsActive")]
		static extern int _IsActive(IntPtr handle);//OK return dword

		/// <summary>
		/// Gets the objects current State
		/// </summary>
		public State ActivityState 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				return (State)_IsActive(this.handle);
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSetAttributes")]
		static extern int _SetAttributes(IntPtr handle, int freq, int volume, int pan);//OK handle is dword return bool

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetAttributes")]
		static extern int _GetAttributes(IntPtr handle,
			ref int freq, 
			ref int volume, 
			ref int pan);//OK handle is dword return bool

		/// <summary>
		/// Gets/Sets a channel's attributes. The actual setting may not be exactly
		/// as specified, depending on the accuracy of the device and drivers.
		/// </summary>
		/// <remarks>Only the volume can be adjusted for the CD "channel", but not all
		/// soundcards allow controlling of the CD volume level.</remarks>
		public virtual ChannelAttributes Attributes 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int freq = 0;
				int volume = 0;
				int panning = 0;
				if (_GetAttributes( this.handle, ref freq, ref volume, ref panning) == 0)
					throw new BASSException();
				return new ChannelAttributes(freq, volume, panning);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				if (_SetAttributes( this.handle, value.freq, value.volume, value.panning) == 0)
					throw new BASSException();
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelStop")]
		static extern int _Stop(IntPtr handle);//OK return bool

		/// <summary>
		/// Stop a channel.
		/// </summary>
		public virtual void Stop()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			progresstimer.Stop();
			if (_Stop(this.handle) == 0) throw new BASSException();		
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelPause")]
		static extern int _Pause(IntPtr handle);//OK return bool

		/// <summary>
		/// Pause a channel.
		/// </summary>
		public void Pause()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_Pause(this.handle) == 0) throw new BASSException();		
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelResume")]
		static extern int _Resume(IntPtr handle);//OK return bool

		/// <summary>
		/// Resume a paused channel.
		/// </summary>
		public void Resume()
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_Resume(this.handle) == 0) throw new BASSException();		
		}

		// handle : Channel handle (HCHANNEL/HMUSIC/HSTREAM, or CDCHANNEL)
		// pos    : the position
		//          if HCHANNEL: position in bytes
		//          if HMUSIC: LOWORD=order HIWORD=row ... use MAKELONG(order,row)
		//          if HSTREAM: position in bytes, file streams only
		//          if CDCHANNEL: position in milliseconds from start of track
		/// <summary>
		/// Used to override in derived classes
		/// </summary>
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelSetPosition")]
		protected static extern int _SetPosition(IntPtr handle, long pos);//OK retrn bool

		// Get the current playback position of a channel.
		// handle : Channel handle (HCHANNEL/HMUSIC/HSTREAM, or CDCHANNEL)
		// RETURN : the position (-1=error)
		//          if HCHANNEL: position in bytes
		//          if HMUSIC: LOWORD=order HIWORD=row (use GetLoWord(position), GetHiWord(Position))
		//          if HSTREAM: total bytes played since the stream was last flushed
		//          if CDCHANNEL: position in milliseconds from start of track
		/// <summary>
		/// Use to override in derived classes
		/// </summary>
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetPosition")]
		protected static extern long _GetPosition(IntPtr handle);//OK use decimal

		/// <summary>
		/// Gets/Sets the current playback position of a channel.
		/// </summary>
		public abstract long Position 
		{
			get;
			set;
		}
	}
}
