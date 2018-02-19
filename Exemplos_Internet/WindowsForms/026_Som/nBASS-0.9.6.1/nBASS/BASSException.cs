using System;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// Summary description for BASSException.
	/// </summary>
	public class BASSException : Exception
	{
		protected Error err;

		public BASSException() : this(GetErrorCode())
		{
		}

		internal BASSException(int code) : base(GetErrorDescription((Error)code))
		{
			err = (Error) code;
		}

		/// <summary>
		/// Get the error state
		/// </summary>
		public Error ErrorState
		{
			get 
			{
				return err;
			}
		}

		/// <summary>
		/// Get an description for the error that occurred
		/// </summary>
		public string ErrorDescription 
		{
			get 
			{
				return GetErrorDescription(err);
			}
		}

		// Get the BASS_ERROR_xxx error code. Use this function to get the reason for an error.
		[DllImport("bass.dll", EntryPoint = "BASS_ErrorGetCode")]
		static extern int _ErrorGetCode(); //OK

		protected static int GetErrorCode()
		{
			return _ErrorGetCode();
		}

		protected static string GetErrorDescription(Error error) 
		{
			switch (error)
			{
				case Error.BASS_OK:
					return "All is OK";
				case Error.MEM:
					return "Memory Error";
				case Error.FILEOPEN:
					return "Can't Open the File";
				case Error.DRIVER:
					return "Can't Find a Free Sound Driver";
				case Error.BUFLOST:
					return "The Sample Buffer Was Lost - Please Report This!";
				case Error.HANDLE:
					return "Invalid Handle";
				case Error.FORMAT:
					return "Unsupported Format";
				case Error.POSITION:
					return "Invalid Playback Position";
				case Error.INIT:
					return "BASS.Init Has Not Been Successfully Called";
				case Error.START:
					return "BASS_Start Has Not Been Successfully Called";
				case Error.INITCD:
					return "Can't Initialize CD";
				case Error.CDINIT:
					return "BASS_CDInit Has Not Been Successfully Called";
				case Error.NOCD:
					return "No CD in drive";
				case Error.CDTRACK:
					return "Can't Play the Selected CD Track";
				case Error.ALREADY:
					return "Already Initialized";
				case Error.CDVOL:
					return "CD Has No Volume Control";
				case Error.NOPAUSE:
					return "Not Paused";
				case Error.NOTAUDIO:
					return "Not An Audio Track";
				case Error.NOCHAN:
					return "Can't Get a Free Channel";
				case Error.ILLTYPE:
					return "An Illegal Type Was Specified";
				case Error.ILLPARAM:
					return "An Illegal Parameter Was Specified";
				case Error.NO3D:
					return "No 3D Support";
				case Error.NOEAX:
					return "No EAX Support";
				case Error.DEVICE:
					return "Illegal Device Number";
				case Error.NOPLAY:
					return "Not Playing";
				case Error.FREQ:
					return "Illegal Sample Rate";
				case Error.NOA3D:
					return "A3D.DLL is Not Installed";
				case Error.NOTFILE:
					return "The Stream is Not a File Stream (WAV/MP3)";
				case Error.NOHW:
					return "No Hardware Voices Available";
				case Error.EMPTY:
					return "The MOD music has no sequence data";
				case Error.NONET:
					return "No Internet connection could be opened";
				case Error.CREATE:
					return "Couldn't create the file";
				case Error.NOFX:
					return "Effects are not enabled";
				case Error.PLAYING:
					return "The channel is playing";
				case Error.NOTAVAIL:
					return "The requested data is not available";
				case Error.DECODE:
					return "The channel is a 'decoding channel' ";
				case Error.WMA_LICENSE:
					return "the file is protected";
				case Error.UNKNOWN:
					return "Some Other Mystery Error";
				default:
					goto case Error.BASS_OK;
			}
		}

		// ***********************************************
		// * Error codes returned by BASS_GetErrorCode() *
		// ***********************************************
		public enum Error
		{
			BASS_OK = 0, // all is OK
			MEM = 1, // memory error
			FILEOPEN = 2, // can// t open the file
			DRIVER = 3, // can// t find a free sound driver
			BUFLOST = 4, // the sample buffer was lost - please report this!
			HANDLE = 5, // invalid handle
			FORMAT = 6, // unsupported format
			POSITION = 7, // invalid playback position
			INIT = 8, // BASS_Init has not been successfully called
			START = 9, // BASS_Start has not been successfully called
			INITCD = 10, // can// t initialize CD
			CDINIT = 11, // BASS_CDInit has not been successfully called
			NOCD = 12, // no CD in drive
			CDTRACK = 13, // can// t play the selected CD track
			ALREADY = 14, // already initialized
			CDVOL = 15, // CD has no volume control
			NOPAUSE = 16, // not paused
			NOTAUDIO = 17, // not an audio track
			NOCHAN = 18, // can// t get a free channel
			ILLTYPE = 19, // an illegal type was specified
			ILLPARAM = 20, // an illegal parameter was specified
			NO3D = 21, // no 3D support
			NOEAX = 22, // no EAX support
			DEVICE = 23, // illegal device number
			NOPLAY = 24, // not playing
			FREQ = 25, // illegal sample rate
			NOA3D = 26, // A3D.DLL is not installed
			NOTFILE = 27, // the stream is not a file stream (WAV/MP3)
			NOHW = 29, // no hardware voices available
			EMPTY = 31, // the MOD music has no sequence data
			NONET = 32, // no internet connection could be opened
			CREATE = 33, // couldn// t create the file
			NOFX = 34, // effects are not enabled
			PLAYING = 35, // the channel is playing
			NOTAVAIL = 37, // requested data is not available
			DECODE = 38, // the channel is a "decoding channel"
			DX = 39, // a sufficient DirectX version is not installed
			TIMEOUT = 40, // connection timedout
			WMA_LICENSE	= 1000,	// the file is protected
			UNKNOWN = -1, // some other mystery error	
		}
	}
}

