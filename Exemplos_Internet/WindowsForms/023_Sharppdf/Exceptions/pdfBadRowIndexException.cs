using System;

namespace sharpPDF.Exceptions
{
	/// <summary>
	/// Exception that represents an error during an access on the pdfTable's rows with a bad index
	/// </summary>
	public class pdfBadRowIndexException : pdfException
	{
		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfBadRowIndexException():base ("The row index does not exist", null)
		{
		}
	}
}
