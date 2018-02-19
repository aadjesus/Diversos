using System;

namespace sharpPDF.Exceptions
{
	/// <summary>
	/// Exception that represents an error during the I/O on the buffer.
	/// </summary>
	public class pdfBadFontFileException : pdfException
	{
		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfBadFontFileException():base("The font file is bad formatted", null)
		{
			
		}
	}
}
