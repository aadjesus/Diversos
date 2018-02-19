using System;

namespace sharpPDF.Exceptions
{
	/// <summary>
	/// Exception that represents a nonexistent font
	/// </summary>
	public class pdfFontNotLoadedException : pdfException
	{
		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfFontNotLoadedException():base("The font reference is not found inside the document!",null)
		{
		}
	}
}
