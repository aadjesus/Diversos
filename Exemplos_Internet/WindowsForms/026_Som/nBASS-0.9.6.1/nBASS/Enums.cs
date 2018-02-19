using System;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// Summary description for Enums.
	/// </summary>

	[Flags()]
	public enum ChannelDataFlags : long
	{
		//  BASS_ChannelGetData flags
		[MarshalAs(UnmanagedType.U4)]
		FFT512 = 0x80000000, //  512 sample FFT
		[MarshalAs(UnmanagedType.U4)]
		FFT1024 = 0x80000001, //  1024 FFT
		[MarshalAs(UnmanagedType.U4)]
		FFT2048 = 0x80000002, //  2048 FFT
		[MarshalAs(UnmanagedType.U4)]
		SFFT512 = 0x80000010, //  512 sample FFT
		[MarshalAs(UnmanagedType.U4)]
		SFFT1024 = 0x80000011, //  1024 stereo sample FFT
		[MarshalAs(UnmanagedType.U4)]
		SFFT2048 = 0x80000012, //  2048 stereo FFT

	}

	[Flags()]
	public enum MusicFlags 
	{
		// ***************
		// * Music flags *
		// ***************
		NormalRamping			= 0x0001, //  normal ramping
		SensitiveRamping		= 0x0002, //  sensitive ramping
		//  Ramping doesn// t take a lot of extra processing and improve
		//  the sound quality by removing "clicks". Sensitive ramping will
		//  leave sharp attacked samples, unlike normal ramping.
		Loop					= 0x0004, //  loop music
		FastTracker2Mode	= 0x0010, //  play .MOD as FastTracker 2 does
		ProTracker1Mode	= 0x0020, //  play .MOD as ProTracker 1 does
		Mono					= 0x0040, //  force mono mixing (less CPU usage)
		ThreeDee				= 0x0080, //  enable 3D functionality
		PositionReset		= 0x0100, //  stop all notes when moving position
		SurroundMode1		= 0x0200, // surround sound
		SurroundMode2		= 0x0400, // surround sound (mode 2)
		StopOnBackJump		= 0x0800, // stop the music on a backwards jump effect
		FX						= 0x1000, // enable DX8 effects
		CalculateLength	= 0x2000, // calculate playback length
		DecodeOnly			= 0x200000, // don// t play the music, only decode (BASS_ChannelGetData)
		NoLoadSample		= 0x400000, //  don// t load the samples
	}

	[Flags()]
	public enum StreamFlags
	{
		Default = DeviceSetupFlags.Default,
		EightBits = DeviceSetupFlags.EightBits, // use 8 bit resolution, else 16 bit
		Mono = DeviceSetupFlags.Mono, // use mono, else stereo
		ThreeDee = DeviceSetupFlags.ThreeDee, // enable 3D functionality
		FX = SampleInfoFlags.FX,
		HalfRate = Stream.MP3.HALFRATE,
		SetPosition = Stream.MP3.SETPOS,
		AutoFree = Stream.Mode2.AUTOFREE,
		DecodeOnly = Stream.Mode2.DECODE,
	}

	public enum StreamPlayFlags 
	{
		Default = 0,
		Loop = SampleInfoFlags.Loop,
	}

	[Flags()]
	public enum SampleInfoFlags 
	{
		// *********************
		// * Sample info flags *
		// *********************
		Default		= 0x0000,
		EightBits	= 0x0001, //  8 bit, else 16 bit
		Mono			= 0x0002, //  mono, else stereo
		Loop			= 0x0004, //  looped
		ThreeDee		= 0x0008, //  3D functionality enabled
		Software		= 0x0010, //  it// s NOT using hardware mixing
		MuteMax		= 0x0020, //  muted at max distance (3D only)
		VAM			= 0x0040, //  uses the DX7 voice allocation & management
		FX				= 0x0080, //  the DX8 effects are enabled
		OverrideVolume		= 0x00010000, //  override lowest volume
		OverridePosition	= 0x00020000, //  override longest playing
		OverrideDistance	= 0x00030000 //  override furthest from listener (3D only)
	}

	enum SyncType : long
	{

		// **********************************************************************
		// * Sync types (with BASS_ChannelSetSync() "param" and SYNCPROC "data" *
		// * definitions) & flags.                                              *
		// **********************************************************************
		//  Sync when a music or stream reaches a position.
		//  if HMUSIC...
		//  param: LOWORD=order (0=first, -1=all) HIWORD=row (0=first, -1=all)
		//  data : LOWORD=order HIWORD=row
		//  if HSTREAM...
		//  param: position in bytes
		//  data : not used
		[MarshalAs(UnmanagedType.U4)]
		POS = 0,
		[MarshalAs(UnmanagedType.U4)]
		MUSICPOS = 0,
		//  Sync when an instrument (sample for the non-instrument based formats)
		//  is played in a music (not including retrigs).
		//  param: LOWORD=instrument (1=first) HIWORD=note (0=c0...119=b9, -1=all)
		//  data : LOWORD=note HIWORD=volume (0-64)
		[MarshalAs(UnmanagedType.U4)]
		MUSICINST = 1,
		//  Sync when a music or file stream reaches the end.
		//  param: not used
		//  data : not used
		[MarshalAs(UnmanagedType.U4)]
		END = 2,
		//  Sync when the "sync" effect (XM/MTM/MOD: E8x/Wxx, IT/S3M: S2x) is used.
		//  param: 0:data=pos, 1:data="x" value
		//  data : param=0: LOWORD=order HIWORD=row, param=1: "x" value
		[MarshalAs(UnmanagedType.U4)]
		MUSICFX = 3,
		//  FLAG: post a Windows message (instead of callback)
		//  When using a window message "callback", the message to post is given in the "proc"
		//  parameter of BASS_ChannelSetSync, and is posted to the window specified in the BASS_Init
		//  call. The message parameters are: WPARAM = data, LPARAM = user.
		[MarshalAs(UnmanagedType.U4)]
		META = 4,
		//  Sync when metadata is received in a Shoutcast stream.
		//  param: not used
		//  data : pointer to the metadata
		[MarshalAs(UnmanagedType.U4)]
		MESSAGE = 0x20000000,
		// FLAG: sync at mixtime, else at playtime
		[MarshalAs(UnmanagedType.U4)]
		MIXTIME = 0x40000000,
		//  FLAG: sync only once, else continuously
		[MarshalAs(UnmanagedType.U4)]
		ONETIME = 0x80000000,
	}

	public enum ChannelFX 
	{

		// DX8 effect types, use with BASS_ChannelSetFX
		Chorus = 0 , // GUID_DSFX_STANDARD_CHORUS
		Compressor = 1 , // GUID_DSFX_STANDARD_COMPRESSOR
		Distortion = 2 , // GUID_DSFX_STANDARD_DISTORTION
		Echo = 3 , // GUID_DSFX_STANDARD_ECHO
		Flanger = 4 , // GUID_DSFX_STANDARD_FLANGER
		Gargle = 5 , // GUID_DSFX_STANDARD_GARGLE
		I3DL2Reverb = 6 , // GUID_DSFX_STANDARD_I3DL2REVERB
		ParametricEQ = 7 , // GUID_DSFX_STANDARD_PARAMEQ
		Reverb = 8 , // GUID_DSFX_WAVES_REVERB
	}

	public enum State 
	{
		//  BASS_ChannelIsActive return values
		Stopped = 0,
		Playing = 1,
		Stalled = 2,
		Paused  = 3,
	}

	[Flags()]
	public enum DeviceSetupFlags 
	{
		// **********************
		// * Device setup flags *
		// **********************
		Default =		0x0000,
		EightBits =		0x0001, // use 8 bit resolution, else 16 bit
		Mono =			0x0002, // use mono, else stereo
		ThreeDee =		0x0004, // enable 3D functionality
		//  If the BASS_DEVICE_3D flag is not specified when initilizing BASS,
		//  then the 3D flags (BASS_SAMPLE_3D and BASS_MUSIC_3D) are ignored when
		//  loading/creating a sample/stream/music.
		LeaveVolume =	0x0020, // leave volume as it is 32
		NoThread =		0x0080, // update buffers manually (using BASS_Update)
		Latency =		0x0100, // calculate device latency (BASS_INFO struct)
		Volume1000 =	0x0200, // 0-1000 volume range (else 0-100)
	}

	public enum Channel3DMode 
	{
		// ********************
		// * 3D channel modes *
		// ********************
		Normal = 0,
		//  normal 3D processing
		Relative = 1,
		//  The channel// s 3D position (position/velocity/orientation) are relative to
		//  the listener. When the listener// s position/velocity/orientation is changed
		//  with BASS_Set3DPosition, the channel// s position relative to the listener does
		//  not change.
		Off = 2,
		//  Turn off 3D processing on the channel, the sound will be played
		//  in the center.
	}

	public enum EAXEnvironment 
	{
		// ****************************************************
		// * EAX environments, use with BASS_SetEAXParameters *
		// ****************************************************
		Generic = 0,
		PaddedCell,
		Room,
		Bathroom,
		LivingRoom,
		StoneRoom,
		Auditorium,
		ConcertHall,
		Cave,
		Arena,
		Hangar,
		CarpetedHallway,
		Hallway,
		StoneCorridor,
		Alley,
		Forest,
		City,
		Mountains,
		Quarry,
		Plain,
		ParkingLot,
		SewerPipe,
		Underwater,
		Drugged,
		Dizzy,
		Psychotic,
		//  total number of environments
		//COUNT = 26,
	}

	// *************************************************************
	// * EAX presets, usage: BASS_SetEAXParametersVB(EAX_PRESET_xxx) *
	// *************************************************************
	public enum BASSEAXPreset 
	{
		//Off = 0,
		Generic = 1, // same as above just starting at 1. 
		PaddedCell,
		Room,
		Bathroom,
		LivingRoom,
		StoneRoom,
		Auditorium,
		ConcertHall,
		Cave,
		Arena,
		Hangar,
		CarpetedHallway,
		Hallway,
		StoneCorridor,
		Alley,
		Forest,
		City,
		Mountains,
		Quarry,
		Plain,
		ParkingLot,
		SewerPipe,
		Underwater,
		Drugged,
		Dizzy,
		Psychotic,
	}

	public enum Algorithm3DMode 
	{
		// **********************************************************************
		// * software 3D mixing algorithm modes (used with BASS_Set3DAlgorithm) *
		// **********************************************************************
		//  default algorithm (currently translates to BASS_3DALG_OFF)
		Default = 0,
		//  Uses normal left and right panning. The vertical axis is ignored except for
		// scaling of volume due to distance. Doppler shift and volume scaling are still
		// applied, but the 3D filtering is not performed. This is the most CPU efficient
		// software implementation, but provides no virtual 3D audio effect. Head Related
		// Transfer Function processing will not be done. Since only normal stereo panning
		// is used, a channel using this algorithm may be accelerated by a 2D hardware
		// voice if no free 3D hardware voices are available.
		Off = 1,
		//  This algorithm gives the highest quality 3D audio effect, but uses more CPU.
		//  Requires Windows 98 2nd Edition or Windows 2000 that uses WDM drivers, if this
		//  mode is not available then BASS_3DALG_OFF will be used instead.
		Full = 2,
		//  This algorithm gives a good 3D audio effect, and uses less CPU than the FULL
		//  mode. Requires Windows 98 2nd Edition or Windows 2000 that uses WDM drivers, if
		//  this mode is not available then BASS_3DALG_OFF will be used instead.
		Light = 3,
	}

	[Flags()]
	public enum DeviceCaps
	{
		// ***********************************
		// * BASS_INFO flags (from DSOUND.H) *
		// ***********************************
		ContinuousRate = 0x010,
		//  supports all sample rates between min/maxrate
		EmulateDriver = 0x020,
		//  device does NOT have hardware DirectSound support
		Certified = 0x040,
		//  device driver has been certified by Microsoft
		//  The following flags tell what type of samples are supported by HARDWARE
		//  mixing, all these formats are supported by SOFTWARE mixing
		SecondaryMono = 0x100, //  mono
		SecondaryStereo = 0x200, //  stereo
		Secondary8Bit = 0x400, //  8 bit
		Secondary16Bit = 0x800, //  16 bit
	}

	[Flags()]
	public enum DeviceRecCaps
	{
		// *****************************************
		// * BASS_RECORDINFO flags (from DSOUND.H) *
		// *****************************************
		EmulateDriver = DeviceCaps.EmulateDriver,
		//  device does NOT have hardware DirectSound recording support
		Certified = DeviceCaps.Certified,
		//  device driver has been certified by Microsoft
	}

	public enum DirectSoundObject 
	{
		// **************************************************************
		// * DirectSound interfaces (for use with BASS_GetDSoundObject) *
		// **************************************************************
		DS = 1, //  DirectSound
		DS3DL = 2 // IDirectSound3DListener
	}

	public enum VoiceAllocation 
	{
		// ******************************
		// * DX7 voice allocation flags *
		// ******************************
		//  Play the sample in hardware. If no hardware voices are available then
		//  the "play" call will fail
		Hardware = 1,
		//  Play the sample in software (ie. non-accelerated). No other VAM flags
		// may be used together with this flag.
		Software = 2
	}

	[Flags()]
	public enum VoiceManagementFlags
	{
		// ******************************
		// * DX7 voice management flags *
		// ******************************
		//  These flags enable hardware resource stealing... if the hardware has no
		//  available voices, a currently playing buffer will be stopped to make room for
		//  the new buffer. NOTE: only samples loaded/created with the BASS_SAMPLE_VAM
		//  flag are considered for termination by the DX7 voice management.

		//  If there are no free hardware voices, the buffer to be terminated will be
		//  the one with the least time left to play.
		TERM_TIME = 4,
		//  If there are no free hardware voices, the buffer to be terminated will be
		//  one that was loaded/created with the BASS_SAMPLE_MUTEMAX flag and is beyond
		//  it // s max distance. If there are no buffers that match this criteria, then the
		//  "play" call will fail.
		TERM_DIST = 8,
		//  If there are no free hardware voices, the buffer to be terminated will be
		//  the one with the lowest priority.
		PRIO = 16,
	}
}
