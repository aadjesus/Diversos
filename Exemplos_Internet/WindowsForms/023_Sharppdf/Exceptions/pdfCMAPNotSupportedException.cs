using System;

namespace sharpPDF.Exceptions
{
	/// <summary>
	/// Exception that represents an incompatible Character Mapping Table[The Font MUST be Windows/Unicode Or MAC].
	/// </summary>
	public class pdfCMAPNotSupportedException : pdfException
	{
		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfCMAPNotSupportedException():base("The CMAP of the font file is not supported", null)
		{
			
		}
	}
}
