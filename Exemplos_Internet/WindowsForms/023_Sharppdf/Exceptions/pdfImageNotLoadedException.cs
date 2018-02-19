using System;

namespace sharpPDF.Exceptions {
	/// <summary>
	/// Exception that represents a nonexistent image
	/// </summary>
	public class pdfImageNotLoadedException : pdfException {
		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfImageNotLoadedException():base("The image reference is not found inside the document!",null) {
		}
	}
}
