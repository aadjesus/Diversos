using System;
using System.Runtime.InteropServices;

namespace nBASS
{
	/// <summary>
	/// Summary description for WMAException.
	/// </summary>
	public class WMAException : BASSException
	{
		public WMAException() : base(GetErrorCode())
		{
		}

		[DllImport("basswma.dll", EntryPoint = "BASS_WMA_ErrorGetCode")]
		private static extern int _GetErrorCode();
	
		protected new static int GetErrorCode()
		{
			return _GetErrorCode();
		}
	}
}
