using System;

namespace nBASS
{
	/// <summary>
	/// Summary description for Delegates.
	/// </summary>
	delegate void DSPCallBack( IntPtr handle, int channel, int buffer, int Length, int user); //TODO
	// CALLBACK FUNCTION !!!

	//  VB doesn// t support pointers, so you should copy the buffer into an array,
	//  process it, and then copy it back into the buffer.

	//  DSP callback function. NOTE: A DSP function should obviously be as quick as
	//  possible... other DSP functions, streams and MOD musics can not be processed
	//  until it// s finished.
	//  handle : The DSP handle
	//  channel: Channel that the DSP is being applied to
	//  buffer : Buffer to apply the DSP to
	//  length : Number of bytes in the buffer
	//  user   : The // user//  parameter given when calling BASS_ChannelSetDSP
	
	/// <summary>
	/// Used for updating progress, just passes the channelbase derived object
	/// </summary>
	public delegate void BASSProgessHandler(ChannelBase channel);
	delegate void GetSyncCallBack(IntPtr handle, IntPtr channel, int data, int user); //internal
	//public delegate void SyncCallBack( IntPtr handle, int channel, int data, int user); //TODO, see above replced
	// CALLBACK FUNCTION !!! Really an event handler.

	// Similarly in here, write what to do when sync function
	// is called, i.e screen flash etc.

	//  NOTE: a sync callback function should be very
	//  quick (eg. just posting a message) as other syncs cannot be processed
	//  until it has finished.
	//  handle : The sync that has occured (Stream or  Music)
	//  channel: Channel that the sync occured in
	//  data   : Additional data associated with the sync// s occurance
	//  user   : The // user//  parameter given when calling BASS_ChannelSetSync */

	delegate int StreamCallBack( IntPtr handle,  int buffer, int Length, int user); //TODO
	// CALLBACK FUNCTION !!!

	// In here you can write a function to write out to a file, or send over the
	// internet etc, and stream into a BASS Buffer on the client, its up to you.
	// This function must return the number of bytes written out, so that BASS,
	// knows where to carry on sending from.

	//  NOTE: A stream function should obviously be as quick
	//  as possible, other streams (and MOD musics) can// t be mixed until it// s finished.
	//  handle : The stream that needs writing
	//  buffer : Buffer to write the samples in
	//  length : Number of bytes to write
	//  user   : The // user//  parameter value given when calling BASS_StreamCreate
	//  RETURN : Number of bytes written. If less than "length" then the
	//           stream is assumed to be at the end, and is stopped

	public delegate bool RecordCallback2(short[] buffer, int length, int user);
	public delegate bool RecordCallback(byte[] buffer, int length, int user);
	public delegate int GetRecordCallBack( IntPtr pbuffer, int length, int user); //TODO
	
	// CALLBACK FUNCTION !!!

	//  Recording callback function.
	//  buffer : Buffer containing the recorded samples
	//  length : Number of bytes
	//  user   : The // user//  parameter value given when calling BASS_RecordStart
	//  RETURN : BASSTRUE = continue recording, BASSFALSE = stop

	

}
