using System;
using System.Threading;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace nBASS
{

	/// <summary>
	/// Provides methods for initializing the BASS engine.
	/// </summary>
	public class BASS : IDisposable
	{
		private bool disposed = false;
		private Form form;

		#region Constructors / Destructor
		/// <summary>
		/// Constructor for BASS
		/// </summary>
		/// <param name="device">Device to use (0=first, -1=default, -2=no sound)</param>
		/// <param name="freq">Output sample rate</param>
		/// <param name="flags">DeviceSetupFlags for device initialization</param>
		/// <param name="hwnd"> Owner window (0=current foreground window)</param>
		/// <remarks>The "no sound" device (device=-2), allows loading
		/// and "playing" of MOD musics only (all sample/stream
		/// functions and most other functions fail). This is so
		/// that you can still use the MOD musics as synchronizers
		/// when there is no soundcard present. When using device -2,
		/// you should still set the other arguments as you would do
		/// normally.</remarks> 
		public BASS(int device, int freq, DeviceSetupFlags flags, IntPtr hwnd)
		{
			if (Convert.ToSingle(BASS.Version) < 1.6) 
				throw new NotSupportedException("BASS version 1.6+ required. You have BASS version " + BASS.Version);
			Init(device, freq, flags, hwnd);
			form = (Form)Form.FromHandle(hwnd);
		}

//		public BASS()
//		{
//
//			if (BASS.Version != "1.6") 
//				throw new NotSupportedException("BASS version 1.6 required. You have BASS version " + BASS.Version);
//			Init(-1, 44100, DeviceSetupFlags.Default, new IntPtr(0));
//			//form = (Form)Form.FromHandle(iwin32.Handle);
//		}

//		public BASS(IWin32Window window): this(-1, 44100, DeviceSetupFlags.Default, window.Handle)
//		{
//			form = (Form)Form.FromHandle(window.Handle);
//		}

		public Form ParentForm 
		{
			get { return form;} 
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
				_Free();

				this.disposed = true;
			}
		}

		~BASS()
		{
			Dispose(false);
		}

		#endregion

		#region WMA Construction
		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_StreamCreateFile")]
		extern static IntPtr _LoadWMAStream(int mem, IntPtr filename, int offset, int length, int flags);

		/// <summary>
		/// Create a sample stream from a WMA file (or URL).
		/// </summary>
		/// <param name="mem">TRUE = Stream file from memory</param>
		/// <param name="filename">Filename (mem=FALSE) or memory location (mem=TRUE)</param>
		/// <param name="offset">ignored (set to 0)</param>
		/// <param name="length">Data length (only used if mem=TRUE)</param>
		/// <param name="flags">WMAStreamFlags</param>
		/// <returns>WMAStream object</returns>
		public WMAStream LoadWMAStream(bool mem, string filename, int offset, int length, WMAStreamFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr wmastr = _LoadWMAStream( Helper.Bool2Int(mem), Marshal.StringToHGlobalAnsi(filename),
				offset, length, (int) flags);
			if ( wmastr == IntPtr.Zero) throw new WMAException();
			return new WMAStream(wmastr);
		}
		#endregion

		#region Sample Construction
		[DllImport("bass.dll", EntryPoint = "BASS_SampleLoad")]
		static extern IntPtr _LoadSample(int mem, IntPtr file, int offset, 
			int Length, int max, int flags);//OK, file is LPSTR, mem is bool, return HSAMPLE handle error code

		/// <summary>
		///Load a WAV/MP3/MP2/MP1 sample. If you//re loading a sample with 3D
		///functionality, then you should use BASS_GetInfo and BASS_SetInfo to set
		///the default 3D parameters. You can also use these two functions to set
		///the sample//s default frequency/volume/pan/looping.
		/// </summary>
		/// <param name="mem">Load sample from memory</param>
		/// <param name="filename">Filename or memory location</param>
		/// <param name="offset">File offset to load the sample from </param>
		/// <param name="length">Data length</param>
		/// <param name="max">Maximum number of simultaneous playbacks (1-65535)</param>
		/// <param name="flags">SampleInfoFlags</param>
		/// <returns></returns>
		public Sample LoadSample(bool mem, string filename, int offset, 
			int length, int max, SampleInfoFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr handle = _LoadSample( Helper.Bool2Int( mem), Marshal.StringToHGlobalAnsi( filename), offset,
				length, max, (int) flags);
			if (handle == IntPtr.Zero) throw new BASSException();
			Sample output = new Sample( handle );
			return output;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SampleCreate")]
		static extern IntPtr _CreateSample(int Length, int freq, int max, int flags); //OK

		/// <summary>
		/// Create a sample. This function allows you to generate custom samples, or
		/// load samples that are not in the WAV format. A pointer is returned to the
		/// memory location at which you should write the sample's data. After writing
		/// the data, call BASS_SampleCreateDone to get the new sample's handle.
		/// </summary>
		/// <param name="length">The sample's length (in samples, NOT bytes)</param>
		/// <param name="freq">default sample rate</param>
		/// <param name="max">Maximum number of simultaneous playbacks (1-65535)</param>
		/// <param name="flags">SampleInfoFlags</param>
		/// <returns></returns>
		public Sample CreateSample(short[] data, int freq, int max, SampleInfoFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr memloc = _CreateSample(data.Length, freq, max, (int) flags);
			Marshal.Copy(data, 0, memloc, data.Length);
			if (memloc == IntPtr.Zero) throw new BASSException();
			IntPtr sample = _CreateSampleDone(); /// ???????????
			if (sample == IntPtr.Zero) throw new BASSException();
			return new Sample( sample );
		}

		// Finished creating a new sample.
		// Return: The New sample //s handle (NULL=error)
		[DllImport("bass.dll", EntryPoint = "BASS_SampleCreateDone")]
		static extern IntPtr _CreateSampleDone();

		//how do we handle all the handles????
		//		public void CreateDone()
		//		{
		//			IntPtr sample = _CreateDone();
		//			if (sample == IntPtr.Zero) throw new BASSException();
		//		}
		#endregion

		#region Music Construction
		[DllImport("bass.dll", EntryPoint = "BASS_MusicLoad")]
		static extern IntPtr _LoadMusic(int mem, IntPtr file, int offset, int Length, int flags);//OK, bool mem, return point to HMUSIC

		/// <summary>
		/// Load a music (MO3/XM/MOD/S3M/IT/MTM). The amplification and pan
		/// seperation are initially set to 50, use BASS_MusicSetAmplify()
		/// and BASS_MusicSetPanSep() to adjust them.
		/// </summary>
		/// <param name="mem">Load music from memory</param>
		/// <param name="filename">Filename or memory location </param>
		/// <param name="offset">File offset to load the music from </param>
		/// <param name="length">Data length ( 0=use to end of file)</param>
		/// <param name="flags">MusicFlags</param>
		/// <returns></returns>
		public Music LoadMusic(bool mem, string filename, int offset, int length, MusicFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr pfile = Marshal.StringToHGlobalAnsi(filename); //must be like this
			IntPtr mus = _LoadMusic( Helper.Bool2Int(mem), pfile,
				offset, length, (int) flags);
			if (mus == IntPtr.Zero) throw new BASSException();
			Music output = new Music(mus);
			output.Owner = this;
			return output;
		}

		#endregion

		#region Stream Construction
		// 
		// freq   : 
		// flags  : BASS_SAMPLE_xxx flags (only the 8BITS/MONO/3D flags are used)
		// proc   : User defined stream writing function pass using "AddressOf STREAMPROC"
		// user   : The //user// value passed to the callback function
		// RETURN : The created stream//s handle (NULL=error)
		[DllImport("bass.dll", EntryPoint = "BASS_StreamCreate")]
		static extern IntPtr _CreateStream(int freq, int flags, StreamCallBack proc, int user);//OK

		//not properly implemented yet
		/// <summary>
		/// Create a user sample stream.
		/// </summary>
		/// <param name="freq">Stream playback rate</param>
		/// <param name="flags">StreamFlags</param>
		/// <param name="proc">StreamCallBack delegate</param>
		/// <param name="user">The "user" value passed to the callback function</param>
		/// <returns></returns>
		Stream CreateStream(int freq, StreamFlags flags, StreamCallBack proc, int user)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr handle = _CreateStream(freq, (int) flags, proc, user);
			if (handle == IntPtr.Zero) throw new BASSException();
			Stream output = new Stream( handle );
			output.Owner = this;
			return output;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamCreateFile")]
		static extern IntPtr _CreateStreamFile(int mem, IntPtr file, int offset,
			int Length, int flags);//OK, mem is bool

		/// <summary>
		/// Create a sample stream from an MP3/MP2/MP1/OGG or WAV file.
		/// </summary>
		/// <param name="mem">Stream file from memory</param>
		/// <param name="filename">Filename or memory location</param>
		/// <param name="offset">File offset of the stream data</param>
		/// <param name="length">File length (0=use whole file)</param>
		/// <param name="flags">StreamFlags</param>
		/// <returns></returns>
		public Stream LoadStream(bool mem, string filename, int offset,
			int length, StreamFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr handle = _CreateStreamFile( Helper.Bool2Int( mem), Marshal.StringToHGlobalAnsi( filename), 
				offset, length, (int) flags);
			if (handle == IntPtr.Zero) throw new BASSException();
			Stream output = new Stream( handle );
			output.Owner = this;
			return output;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_StreamCreateURL")]
		static extern IntPtr _CreateStreamURL(string url, int offset, int flags, string save);//OK

		/// <summary>
		/// Create a sample stream from an MP3/MP2/MP1/OGG or WAV file on the internet.
		/// </summary>
		/// <param name="url">The URL (beginning with "http://" or "ftp://")</param>
		/// <param name="offset">File offset of start streaming from</param>
		/// <param name="flags">StreamFlags</param>
		/// <param name="savefile">Filename to save the streamed file as locally (""=don//t save)</param>
		/// <returns></returns>
		public Stream CreateStreamFromURL(string url, int offset, StreamFlags flags, string savefile)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr handle = _CreateStreamURL(url, offset, (int) flags, savefile);
			if (handle == IntPtr.Zero) throw new BASSException();
			Stream output = new Stream( handle );
			output.Owner = this;
			return output;
		}

		#endregion

		#region Main Functions

		[DllImport("bass.dll", EntryPoint = "BASS_GetVersion")]
		static extern int _GetVersion();//OK

		/// <summary>
		/// Retrieve the version number of BASS DLL that is used.
		/// </summary>
		public static string Version 
		{
			get 
			{
				int ver = _GetVersion();
				return String.Format("{0}.{1}", Helper.LoWord(ver), Helper.HiWord(ver));
			}
		}
		
		[DllImport("bass.dll", EntryPoint = "BASS_GetDeviceDescription")]
		static extern IntPtr _GetDeviceDescription(int devnum);//OK, Ansi note, returns char *WINAPI == LPSTR???

		/// <summary>
		/// Gets a text description of a device. This function can be used to enumerate the available devices.
		/// </summary>
		/// <param name="device">The device (0=First)</param>
		/// <returns>The text description of the device</returns>
		public string GetDeviceDescription (int device)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			IntPtr desc = _GetDeviceDescription(device);
			if (desc == IntPtr.Zero) throw new BASSException();
			return Marshal.PtrToStringAnsi( desc );
		}

		/// <summary>
		/// Number of available devices
		/// </summary>
		public int DeviceCount 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				int count = 0;
				try 
				{
					while (_GetDeviceDescription(count++) != IntPtr.Zero) {}
				} 
				catch (BASSException) {} // what is count or do i need to minus 1?
				return count - 1;
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SetBufferLength")]
		static extern float _SetBufferLength(float Length);//OK
		
		/// <summary>
		/// Set the amount that BASS mixes ahead new musics/streams.
		/// Changing this setting does not affect musics/streams
		/// that have already been loaded/created. Increasing the
		/// buffer length, decreases the chance of the sound
		/// possibly breaking-up on slower computers, but also
		/// requires more memory. The default length is 0.5 secs.
		/// </summary>
		/// <param name="length">The buffer length in seconds</param>
		/// <returns>The actual new buffer length</returns>
		public float SetBufferLength(float length)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			return _SetBufferLength(length);
		}


		// Set the global music/sample/stream volume levels.
		// musvol : MOD music global volume level (0-100, -1=leave current)
		// samvol : Sample global volume level (0-100, -1=leave current)
		// strvol : Stream global volume level (0-100, -1=leave current)
		[DllImport("bass.dll", EntryPoint = "BASS_SetGlobalVolumes")]
		static extern void _SetGlobalVolumes(int musvol, int samvol, int strvol);//OK

		// Retrive the global music/sample/stream volume levels.
		// musvol : MOD music global volume level (NULL=don//t retrieve it)
		// samvol : Sample global volume level (NULL=don//t retrieve it)
		// strvol : Stream global volume level (NULL=don//t retrieve it)
		[DllImport("bass.dll", EntryPoint = "BASS_GetGlobalVolumes")]
		unsafe static extern void _GetGlobalVolumes(
			int* musvol,
			int* samvol, 
			int* strvol);//OK


		/// <summary>
		/// Gets/sets music volume
		/// </summary>
		unsafe public int MusicVolume 
		{
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				_SetGlobalVolumes(value, -1, -1);
			}
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				int output = 0;
				_GetGlobalVolumes( &output, null, null);
				return output;
			}
		}

		/// <summary>
		/// Gets/sets sample volume
		/// </summary>
		unsafe public int SampleVolume 
		{
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				_SetGlobalVolumes( -1, value, -1);
			}
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				int output = 0;
				_GetGlobalVolumes( null, &output, null);
				return output;
			}
		}

		/// <summary>
		/// Gets/sets stream volume
		/// </summary>
		unsafe public int StreamVolume 
		{
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				_SetGlobalVolumes( -1, -1, value);
			}
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				int output = 0;
				_GetGlobalVolumes(null, null, &output);
				return output;
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SetLogCurves")]
		static extern void _SetLogCurves(int volume, int pan);//OK, but want to change

		public enum Curve 
		{
			Linear = 0, 
			Logarithmic = 1,
		}

		/// <summary>
		/// Make the volume/panning values translate to a logarithmic curve,
		/// or a linear "curve" (the default)
		/// </summary>
		/// <param name="volume">Volume Curve</param>
		/// <param name="panning">Panning Curve</param>
		public void SetLogCurves(Curve volume, Curve panning)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			_SetLogCurves( (int)volume , (int)panning);
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SetCLSID")]
		static extern void _SetCLSID(ref Guid clsid); //OK

		/// <summary>
		/// Set the class identifier of the object to create, that will be used
		/// to initialize DirectSound.
		/// </summary>
		public Guid CLSID
		{
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				_SetCLSID(ref value);
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_Init")]
		static extern int _Init(int device, int freq, int flags, IntPtr win);//OK, return bool
		
		void Init(int device, int freq, DeviceSetupFlags flags, IntPtr hwnd)
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			if (_Init( device, freq, (int) flags, hwnd) == 0) throw new BASSException();
		}
					
		// Free all resources used by the digital output, including  all musics and samples.
		[DllImport("bass.dll", EntryPoint = "BASS_Free")]
		static extern void _Free();//OK

		void Free()
		{
			_Free();
		}

		// Retrieve a pointer to a DirectSound interface. This can be used by
		// advanced users to "plugin" external functionality.
		// object : The interface to retrieve (BASS_OBJECT_xxx)
		// RETURN : A pointer to the requested interface (NULL=error)
		//UPGRADE_NOTE: object was upgraded to object_Renamed. Click for more: //ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1061"//
		[DllImport("bass.dll", EntryPoint = "BASS_GetDSoundObject")]
		static extern IntPtr GetDSoundObject(uint dsobject); //???????? dsobject is handle

		[DllImport("bass.dll", EntryPoint = "BASS_GetInfo")]
		static extern void _GetInfo(
			ref BASSInfo info);//OK

		/// <summary>
		/// Retrieve some information on the device being used.
		/// </summary>
		[TypeConverter("System.ComponentModel.ExpandableObjectConverter")]
		public BASSInfo Info 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				BASSInfo bi = new BASSInfo();
				bi.Size = Marshal.SizeOf( typeof(BASSInfo)); // workinto struct
				_GetInfo( ref bi );
				return bi;
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_Update")]
		static extern int _Update();//OK, rtruns bool, err code

		/// <summary>
		/// Update the MUSIC/STREAM channel buffers.
		/// </summary>
		public void Update()
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			if (_Update() == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_GetCPU")]
		static extern float _GetCPU();//OK

		/// <summary>
		/// Get the current CPU usage of BASS. This includes the time taken to mix
		/// the MOD musics and sample streams, and also the time taken by any user
		/// DSP functions. It does not include plain sample mixing which is done by
		/// the output device (hardware accelerated) or DirectSound (emulated). Audio
		/// CD playback requires no CPU usage.
		/// </summary>
		public float CPUUsage 
		{
			get { return _GetCPU();}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_Start")]
		static extern int _Start();//OK, return bool, err code

		/// <summary>
		/// Start the digital output.
		/// </summary>
		public void Start()
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			if (_Start() == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_Stop")]
		static extern int _Stop();//OK, return bool, err code

		/// <summary>
		/// Stop the digital output, stopping all musics/samples/streams.
		/// </summary>
		public void Stop()
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			if (_Stop() == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_Pause")]
		static extern int _Pause();//OK return bool, err code

		/// <summary>
		/// Stop the digital output, pausing all musics/samples/
		/// streams. Use <see cref="Start()"/>Start to resume the digital output.
		/// </summary>
		public void Pause()
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			if (_Pause() == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SetVolume")]
		static extern int _SetVolume(int volume);//OK return bool err code

		[DllImport("bass.dll", EntryPoint = "BASS_GetVolume")]
		static extern int _GetVolume();//OK

		/// <summary>
		/// Gets/sets the digital output master volume.
		/// </summary>
		public int MasterVolume 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				int output = _GetVolume();
				if (output == -1) throw new BASSException();
				return output;
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				if (_SetVolume(value) == 0) throw new BASSException();
			}
		}

		#endregion

		#region BASS3D

		/// <summary>
		/// This function is a workaround, because VB/C# doesn,t support multiple comma seperated
		/// paramaters for each Global Const, simply pass the BASSEAXPreset value to this function
		/// instead of BASS_SetEasParamaets as you would to in C++
		/// </summary>
		public BASSEAXPreset EAXPreset
		{
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				if (SetEAXParametersEx( value ) == 0) throw new BASSException();
			}
		}

		int SetEAXParametersEx(BASSEAXPreset preset) //object refPreset
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			//  
			switch (preset)
			{
				case BASSEAXPreset.Generic:
					return _SetEAXParameters((int) EAXEnvironment.Generic, 0.5F, 1.493F, 0.5F);
				case BASSEAXPreset.PaddedCell:
					return _SetEAXParameters((int) EAXEnvironment.PaddedCell, 0.25F, 0.1F, 0F);
				case BASSEAXPreset.Room:
					return _SetEAXParameters((int) EAXEnvironment.Room, 0.417F, 0.4F, 0.666F);
				case BASSEAXPreset.Bathroom:
					return _SetEAXParameters((int) EAXEnvironment.Bathroom, 0.653F, 1.499F, 0.166F);
				case BASSEAXPreset.LivingRoom:
					return _SetEAXParameters((int) EAXEnvironment.LivingRoom, 0.208F, 0.478F, 0F);
				case BASSEAXPreset.StoneRoom:
					return _SetEAXParameters((int) EAXEnvironment.StoneRoom, 0.5F, 2.309F, 0.888F);
				case BASSEAXPreset.Auditorium:
					return _SetEAXParameters((int) EAXEnvironment.Auditorium, 0.403F, 4.279F, 0.5F);
				case BASSEAXPreset.ConcertHall:
					return _SetEAXParameters((int) EAXEnvironment.ConcertHall, 0.5F, 3.961F, 0.5F);
				case BASSEAXPreset.Cave:
					return _SetEAXParameters((int) EAXEnvironment.Cave, 0.5F, 2.886F, 1.304F);
				case BASSEAXPreset.Arena:
					return _SetEAXParameters((int) EAXEnvironment.Arena, 0.361F, 7.284F, 0.332F);
				case BASSEAXPreset.Hangar:
					return _SetEAXParameters((int) EAXEnvironment.Hangar, 0.5F, 10F, 0.3F);
				case BASSEAXPreset.CarpetedHallway:
					return _SetEAXParameters((int) EAXEnvironment.CarpetedHallway, 0.153F, 0.259F, 2F);
				case BASSEAXPreset.Hallway:
					return _SetEAXParameters((int) EAXEnvironment.Hallway, 0.361F, 1.493F, 0F);
				case BASSEAXPreset.StoneCorridor:
					return _SetEAXParameters((int) EAXEnvironment.StoneCorridor, 0.444F, 2.697F, 0.638F);
				case BASSEAXPreset.Alley:
					return _SetEAXParameters((int) EAXEnvironment.Alley, 0.25F, 1.752F, 0.776F);
				case BASSEAXPreset.Forest:
					return _SetEAXParameters((int) EAXEnvironment.Forest, 0.111F, 3.145F, 0.472F);
				case BASSEAXPreset.City:
					return _SetEAXParameters((int) EAXEnvironment.City, 0.111F, 2.767F, 0.224F);
				case BASSEAXPreset.Mountains:
					return _SetEAXParameters((int) EAXEnvironment.Mountains, 0.194F, 7.841F, 0.472F);
				case BASSEAXPreset.Quarry:
					return _SetEAXParameters((int) EAXEnvironment.Quarry, 1F, 1.499F, 0.5F);
				case BASSEAXPreset.Plain:
					return _SetEAXParameters((int) EAXEnvironment.Plain, 0.097F, 2.767F, 0.224F);
				case BASSEAXPreset.ParkingLot:
					return _SetEAXParameters((int) EAXEnvironment.ParkingLot, 0.208F, 1.652F, 1.5F);
				case BASSEAXPreset.SewerPipe:
					return _SetEAXParameters((int) EAXEnvironment.SewerPipe, 0.652F, 2.886F, 0.25F);
				case BASSEAXPreset.Underwater:
					return _SetEAXParameters((int) EAXEnvironment.Underwater, 1F, 1.499F, 0F);
				case BASSEAXPreset.Drugged:
					return _SetEAXParameters((int) EAXEnvironment.Drugged, 0.875F, 8.392F, 1.388F);
				case BASSEAXPreset.Dizzy:
					return _SetEAXParameters((int) EAXEnvironment.Dizzy, 0.139F, 17.234F, 0.666F);
				case BASSEAXPreset.Psychotic:
					return _SetEAXParameters((int) EAXEnvironment.Psychotic, 0.486F, 7.563F, 0.806F);
				default:
					goto case BASSEAXPreset.Generic;
			}	
		}
	
		[DllImport("bass.dll", EntryPoint = "BASS_Set3DAlgorithm")]
		static extern void _Set3DAlgorithm(int algo); //OK

		/// <summary>
		/// Set the 3D algorithm for software mixed 3D channels (does not affect
		/// hardware mixed channels). Changing the mode only affects subsequently
		/// created or loaded samples/streams/musics, not those that already exist.
		/// Requires DirectX 7 or above.
		/// </summary>
		public Algorithm3DMode Algorithm3D 
		{
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				_Set3DAlgorithm( (int) value);
			}
		}

		[DllImport("bass.dll", EntryPoint = "BASS_Set3DFactors")]
		static extern int _Set3DFactors(
			ref float distf, 
			ref float rollf, 
			ref float doppf);//OK return bool err code

		[DllImport("bass.dll", EntryPoint = "BASS_Get3DFactors")]
		static extern int _Get3DFactors(
			out float distf, 
			out float rollf, 
			out float doppf);//OK return bool err code

		/// <summary>
		/// Gets/sets the factors that affect the calculations of 3D sound.
		/// </summary>
		public BASS3DFactors Factors3D 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				float distf, rollf, doppf;
				if (_Get3DFactors( out distf , out rollf, out doppf) == 0)
					throw new BASSException();
				return new BASS3DFactors( distf, rollf, doppf);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				float distf = value.distf;
				float rollf = value.rollf;
				float doppf = value.doppf;

				if (_Set3DFactors( ref distf , ref rollf, ref doppf) == 0)
					throw new BASSException();
			}
		}

		// NOTE   : front & top must both be set in a single call
		[DllImport("bass.dll", EntryPoint = "BASS_Set3DPosition")]
		static extern int _Set3DPosition(
			ref Vector3D pos, 
			ref Vector3D vel,
			ref Vector3D front, 
			ref Vector3D top);//OK return bool err code

		// NOTE   : front & top must both be retrieved in a single call
		[DllImport("bass.dll", EntryPoint = "BASS_Get3DPosition")]
		static extern int _Get3DPosition(
			out Vector3D pos, 
			out Vector3D vel,
			out Vector3D front, 
			out Vector3D top);//OK return bool err code

		/// <summary>
		/// Gets/sets the position/velocity/orientation of the listener.
		/// </summary>
		public BASS3DPosition Position3D
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				Vector3D pos,vel,front,top;
				//Vector3D pos = new Vector3D();
				//Vector3D vel  = new Vector3D();
				//Vector3D front = new Vector3D();
				//Vector3D top = new Vector3D();;
				if (_Get3DPosition(out pos, out vel, out front, out top) == 0)
					throw new BASSException();
				return new BASS3DPosition(pos, vel, top, front);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				Vector3D pos = value.pos;
				Vector3D vel = value.vel;
				Vector3D front = value.front;
				Vector3D top = value.top;
				if (_Set3DPosition( ref pos, ref vel, ref front, ref top) == 0)
					throw new BASSException();
			}
		}
		
		[DllImport("bass.dll", EntryPoint = "BASS_Apply3D")]
		static extern int _Apply3D();//OK return bool err code

		/// <summary>
		/// Apply changes made to the 3D system. This must be called to apply any changes
		/// made with BASS.Factors3D, BASS.Position3D, BASS.Channel3DAttributes or
		/// BASS.Channel3DPosition. It improves performance to have DirectSound do all the
		/// required recalculating at the same time like this, rather than recalculating after
		/// every little change is made. 
		/// </summary>
		/// <remarks>This is automatically called when starting a 3D
		/// sample with Sample.Play3D.</remarks>
		public void Apply3D()
		{
			if (this.disposed)
				throw new ObjectDisposedException("BASS");

			if (_Apply3D() == 0) throw new BASSException();
		}

		[DllImport("bass.dll", EntryPoint = "BASS_SetEAXParameters")]
		static extern int _SetEAXParameters(
			int env, 
			float vol, 
			float decay, 
			float damp);//OK return bool err code

		[DllImport("bass.dll", EntryPoint = "BASS_GetEAXParameters")]
		static extern int _GetEAXParameters(
			out int env, 
			out float vol, 
			out float decay, 
			out float damp);//OK return bool err code


		/// <summary>
		/// Get/Set the current EAX parameters. Obviously, EAX functions
		/// have no effect if no EAX supporting device (ie. SB Live) is used.
		/// </summary>
		public BASSEAXParameters EAXParameters 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				int env;
				float vol, decay, damp;
				if (_GetEAXParameters( out env, out vol, out decay, out damp) == 0)
					throw new BASSException();
				return new BASSEAXParameters( (EAXEnvironment) env, vol, decay, damp);
			}
			set 
			{
				if (this.disposed)
					throw new ObjectDisposedException("BASS");

				if(_SetEAXParameters(value.env, value.vol, value.decay, value.damp) == 0)
					throw new BASSException();
			}
		}

		#endregion
	
	}
}
