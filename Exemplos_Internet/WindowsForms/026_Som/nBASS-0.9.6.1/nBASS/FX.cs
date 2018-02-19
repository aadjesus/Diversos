#define TESTING

using System;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// Summary description for FX.
	/// </summary>
	public class FX
	{
		IntPtr handle;
		ChannelFX type;

		internal FX(IntPtr handle, ChannelFX type)
		{
			this.handle = handle;
			this.type = type;
		}

		/// <summary>
		/// Gets the type of effect that has been applied
		/// </summary>
		public ChannelFX FXType 
		{
			get {return type;}
		}

		internal IntPtr Handle 
		{
			get { return this.handle;}
		}



		public enum FXPhase 
		{
			Negative180 = 0,
			Negative90 = 1,
			Zero = 2,
			Positive90 = 3,
			Positive180 = 4,
		}
		
		// Set the parameters of a DX8 effect.
		// handle : FX handle
		// par    : Pointer to the parameter structure
		[DllImport("bass.dll", EntryPoint = "BASS_FXSetParameters")]
		static extern int _SetParameters(IntPtr handle, IntPtr fxparam);//return bool

		// Retrieve the parameters of a DX8 effect.
		// handle : FX handle
		// par    : Pointer to the parameter structure
		[DllImport("bass.dll", EntryPoint = "BASS_FXGetParameters")]
		static extern int _GetParameters(IntPtr handle, IntPtr fxparam); //return bool

		public object Parameters 
		{
			get 
			{
				object param = null;
				switch (this.FXType)
				{
					case ChannelFX.Chorus:
						param = new FXCHORUS();
						break;
					case ChannelFX.Compressor:
						param = new FXCOMPRESSOR();
						break;
					case ChannelFX.Distortion:
						param = new FXDISTORTION();
						break;
					case ChannelFX.Echo:
						param = new FXECHO();
						break;
					case ChannelFX.Flanger:
						param = new FXFLANGER();
						break;
					case ChannelFX.Gargle:
						param = new FXGARGLE();
						break;
					case ChannelFX.I3DL2Reverb:
						param = new FXI3DL2REVERB();
						break;
					case ChannelFX.ParametricEQ:
						param = new FXPARAMEQ();
						break;
					case ChannelFX.Reverb:
						param = new FXREVERB();
						break;
				}
				IntPtr alloc = Marshal.AllocHGlobal(Marshal.SizeOf(param));

				Marshal.StructureToPtr(param, alloc, true);

				if (_GetParameters( this.handle, alloc) == 0)
					throw new BASSException();

				Marshal.PtrToStructure(alloc, param); // ????
				Marshal.FreeHGlobal(alloc);
				return param;
			}
			set 
			{
				IntPtr paramptr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
				Marshal.StructureToPtr(value, paramptr, true);
				if (_SetParameters( this.handle, paramptr) == 0)
				{
					Marshal.FreeHGlobal(paramptr);
					throw new BASSException();
				} else Marshal.FreeHGlobal(paramptr);
			}
		}


	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXCHORUS
	{ // DSFXChorus
		public float fWetDryMix ;
		public float fDepth ;
		public float fFeedback ;
		public float fFrequency ;
		public uint lWaveform ; // 0=triangle, 1=sine
		public float fDelay ;
		public uint lPhase ; // BASS_FX_PHASE_xxx
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXCOMPRESSOR 
	{ // DSFXCompressor
		public float fGain ;
		public float fAttack ;
		public float fRelease ;
		public float fThreshold ;
		public float fRatio ;
		public float fPredelay ;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXDISTORTION
	{ // DSFXDistortion
		public float fGain ;
		public float fEdge ;
		public float fPostEQCenterFrequency ;
		public float fPostEQBandwidth ;
		public float fPreLowpassCutoff ;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXECHO
	{ // DSFXEcho
		public float fWetDryMix ;
		public float fFeedback ;
		public float fLeftDelay ;
		public float fRightDelay ;
		public int lPanDelay;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXFLANGER
	{ // DSFXFlanger
		public float fWetDryMix ;
		public float fDepth ;
		public float fFeedback ;
		public float fFrequency ;
		public uint lWaveform ; // 0=triangle, 1=sine
		public float fDelay ;
		public uint lPhase ; // BASS_FX_PHASE_xxx
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXGARGLE
	{ // DSFXGargle
		public uint dwRateHz ; // Rate of modulation in hz
		public uint dwWaveShape ; // 0=triangle, 1=square
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXI3DL2REVERB
	{ // DSFXI3DL2Reverb
		public int lRoom ; // [-10000, 0]      default: -1000 mB
		public int lRoomHF ; // [-10000, 0]      default: 0 mB
		public float flRoomRolloffFactor ; // [0.0, 10.0]      default: 0.0
		public float flDecayTime ; // [0.1, 20.0]      default: 1.49s
		public float flDecayHFRatio ; // [0.1, 2.0]       default: 0.83
		public int lReflections ; // [-10000, 1000]   default: -2602 mB
		public float flReflectionsDelay ; // [0.0, 0.3]       default: 0.007 s
		public int lReverb ; // [-10000, 2000]   default: 200 mB
		public float flReverbDelay ; // [0.0, 0.1]       default: 0.011 s
		public float flDiffusion ; // [0.0, 100.0]     default: 100.0 %
		public float flDensity ; // [0.0, 100.0]     default: 100.0 %
		public float flHFReference ; // [20.0, 20000.0]  default: 5000.0 Hz
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXPARAMEQ
	{ // DSFXParamEq
		public float fCenter ;
		public float fBandwidth ;
		public float fGain ;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class FXREVERB
	{ // DSFXWavesReverb
		public float fInGain ; // [-96.0,0.0]            default: 0.0 dB
		public float fReverbMix ; // [-96.0,0.0]            default: 0.0 db
		public float fReverbTime ; // [0.001,3000.0]         default: 1000.0 ms
		public float fHighFreqRTRatio ; // [0.001,0.999]          default: 0.001
	}

}
