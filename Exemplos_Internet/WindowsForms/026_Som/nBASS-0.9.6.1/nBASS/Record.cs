using System;
using System.Runtime.InteropServices;

namespace nBASS
{
#if(true)

	[Flags()]
	public enum InputFlags 
	{
		//  BASS_RecordSetInput flags
		OFF = 0x10000,
		ON = 0x20000,
		LEVEL = 0x40000,
	}

	[Flags]
	public enum RecordFlags 
	{
		Default = DeviceSetupFlags.Default,
		Mono = DeviceSetupFlags.Mono,
		EightBits = DeviceSetupFlags.EightBits,
	}

	public enum WaveFormat 
	{
		// ******************************************************************
		// * defines for formats field of BASS_RECORDINFO (from MMSYSTEM.H) *
		// ******************************************************************
		_1M08 = 0x001, //  11.025 kHz, Mono,   8-bit
		_1S08 = 0x002, //  11.025 kHz, Stereo, 8-bit
		_1M16 = 0x004, //  11.025 kHz, Mono,   16-bit
		_1S16 = 0x008, //  11.025 kHz, Stereo, 16-bit
		_2M08 = 0x010, //  22.05  kHz, Mono,   8-bit
		_2S08 = 0x020, //  22.05  kHz, Stereo, 8-bit
		_2M16 = 0x040, //  22.05  kHz, Mono,   16-bit
		_2S16 = 0x080, //  22.05  kHz, Stereo, 16-bit
		_4M08 = 0x100, //  44.1   kHz, Mono,   8-bit
		_4S08 = 0x200, //  44.1   kHz, Stereo, 8-bit
		_4M16 = 0x400, //  44.1   kHz, Mono,   16-bit
		_4S16 = 0x800, //  44.1   kHz, Stereo, 16-bit
	}

	/// <summary>
	/// Summary description for Record.
	/// </summary>
	public class Record : ChannelBase
	{
		private bool disposed = false;

		public Record(int device) : base(new IntPtr(1))
		{
			if (_Init(device) == 0) throw new BASSException();
		}

		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				try
				{
					if (disposing)
					{
						// free any managed resources
					}

					// free any unmanaged resources
					_Free();

					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		#region Recording

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

			int output = _GetData(this.Handle, buffer, (int) flags);
			if ( output < 0) throw new BASSException();
			return output;
		}

		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetData")]
		static extern int _GetData(IntPtr handle, byte[] buffer, int Length);//TODO: OK return dword

		/// <summary>
		/// Retrieves upto "length" bytes of the channel's current sample data. 
		/// </summary>
		/// <param name="buffer">A buffer to place retrieved data</param>
		/// <param name="length">length in bytes</param>
		public int GetData(byte[] buffer, int length)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			int output = _GetData(this.Handle, buffer, length);
			if ( output < 0) throw new BASSException();
			return output;
		}
		
		// Calculate a channel//s current output level.
		// handle : channel handle(HMUSIC / HSTREAM, or RECORDCHAN)
		// RETURN : LOWORD=left level (0-128) HIWORD=right level (0-128) (-1=error)
		//          Use LoWord and HiWord functions on return function.
		[DllImport("bass.dll", EntryPoint = "BASS_ChannelGetLevel")]
		static extern int _GetLevel(IntPtr handle);//OK return dword

		public int LeftLevel 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int result = _GetLevel(base.Handle);
				if(result < 0) throw new BASSException();
				return Helper.LoWord(result);
			}
		}

		public int RightLevel 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				int result = _GetLevel(base.Handle);
				if(result < 0) throw new BASSException();
				return Helper.HiWord(result);
			}
		}

		// Get the text description of a recording device. This function can be
		// used to enumerate the available devices.
		// devnum : The device (0=first)
		// RETURN : The text description of the device (NULL=error)
		[DllImport("bass.dll", EntryPoint = "BASS_RecordGetDeviceDescription")]
		static extern IntPtr _GetDeviceDescription(int devnum);//OK, return LPSTR

		public static string GetDeviceDescription(int device)
		{
			IntPtr desc = _GetDeviceDescription(device);
			if (desc == IntPtr.Zero) throw new BASSException();
			return Marshal.PtrToStringAnsi(desc);
		}

		public static int DeviceCount 
		{
			get 
			{
				int counter = 0;
				while (_GetDeviceDescription(counter) != IntPtr.Zero) counter++;
				return counter;
			}
		}

		// Initialize a recording device.
		// device : Device to use (0=first, -1=default)
		[DllImport("bass.dll", EntryPoint = "BASS_RecordInit")]
		static extern int _Init(int device);//OK return bool err

		// Free all resources used by the recording device.
		[DllImport("bass.dll", EntryPoint = "BASS_RecordFree")]																	
		static extern void _Free();//OK

		// Retrieve some information on the recording device being used.
		// info   : Pointer to store info at
		[DllImport("bass.dll", EntryPoint = "BASS_RecordGetInfo")]
		static extern void _GetInfo(ref RecordInfo info);//OK

		public RecordInfo Info 
		{
			get 
			{
				if (this.disposed)
					throw new ObjectDisposedException(this.ToString());

				RecordInfo ri = new RecordInfo();
				ri.size = Marshal.SizeOf(ri);
				_GetInfo( ref ri);
				return ri;
			}
		}

		// Get the text description of a recording input.
		// inputn : Input number (0=first)
		// RETURN : The text description (0=error)
		[DllImport("bass.dll", EntryPoint = "BASS_RecordGetInputName")]
		static extern IntPtr _GetInputName(int inputn);//OK, returns LPSTR

		public string GetInputName(int inputn)
		{
			IntPtr name = _GetInputName( inputn );
			if (name == IntPtr.Zero) throw new BASSException();
			return Marshal.PtrToStringAnsi( name );
		}

		public int InputCount 
		{
			get 
			{
				int counter = 0;
				while (_GetInputName(counter) != IntPtr.Zero) counter++;
				return counter;
			}
		}

		// Adjust the setting of a recording input.
		// inputn : Input number (0=first)
		// setting: BASS_INPUT flags (if BASS_INPUT_LEVEL used, LOWORD=volume)
		[DllImport("bass.dll", EntryPoint = "BASS_RecordSetInput")]
		static extern int _SetInput(int inputn, int setting);//OK, return bool

		public void SetInput(int inputn, InputFlags flags)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			//TODO: LOWORD STUFF TO BE DONE
			if (_SetInput(inputn, (int) flags) == 0) throw new BASSException();
		}

		// Retrieve the setting of a recording input.
		// inputn : Input number (0=first)
		// RETURN : The setting (LOWORD=volume, with BASS_INPUT_OFF flag if off, -1=error)
		[DllImport("bass.dll", EntryPoint = "BASS_RecordGetInput")]
		static extern int _GetInput(int inputn);//OK, err dword

		//TODO: blah 
		//public Get

		// Start recording.
		// freq   : Sampling rate
		// flags  : BASS_SAMPLE_8BITS/MONO flags (optional HIWORD=update period)
		// proc   : User defined function to receive the recorded data
		// user   : The //user// value passed to the callback function
		[DllImport("bass.dll", EntryPoint = "BASS_RecordStart")]
		static extern int _Start(int freq, int flags, GetRecordCallBack proc, int user);////OK, but wants change

		public void Start(int freq, RecordFlags flags, RecordCallback callback, int user)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_Start(freq, (int) flags, new GetRecordCallBack( InternalCallback ), (int) flags) == 0 )
				throw new BASSException();
			this.callback = callback;
		}

		public void Start(int freq, RecordFlags flags, RecordCallback2 callback, int user)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_Start(freq, (int) flags, new GetRecordCallBack( InternalCallback ), (int) flags) == 0 )
				throw new BASSException();
			this.callback2 = callback;
		}

		public void Start(int freq, RecordFlags flags, GetRecordCallBack callback, int user)
		{
			if (this.disposed)
				throw new ObjectDisposedException(this.ToString());

			if (_Start(freq, (int) flags, callback, (int) flags) == 0 )
				throw new BASSException();
		}

		private RecordCallback callback;
		private RecordCallback2 callback2;

		private int InternalCallback(IntPtr pbuffer, int length, int user)
		{
			if (((RecordFlags)user & RecordFlags.EightBits) != 0)
			{
				byte[] buffer = new byte[length];
				Marshal.Copy( pbuffer, buffer, 0, length);
				return Helper.Bool2Int(callback(buffer, length, user));
			}
			else 
			{
				//Console.WriteLine(length);
				short[] buffer = new short[length];
				Marshal.Copy( pbuffer, buffer, 0, length);
				return Helper.Bool2Int(callback2(buffer, length, user));
			}
			
		}

		#endregion

		#region Inherited method hiding

		public override ChannelAttributes Attributes 
		{
			set
			{
				value = null;
			}
		}

		public override long Position 
		{
			get {return 0; }
			set {;}
		}

		#endregion

	}

#endif
}
