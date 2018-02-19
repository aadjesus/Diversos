using System;

namespace sharpPDF.Exceptions
{
	/// <summary>
	/// Exception that represents a row longer than maximum table space
	/// </summary>
	public class pdfBadRowHeightException : pdfException
	{
		/// <summary>
		/// Class's Costructor
		/// </summary>
		public pdfBadRowHeightException():base ("The height of the row exceed the maximum height", null)
		{
		}
	}
}
