using System;

namespace sharpPDF.Exceptions
{
	/// <summary>
	/// Exception that represents a generic error during the I/O.
	/// </summary>
	public class pdfGenericIOException : Exception
	{
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="message">Message for the exception</param>
		/// <param name="ex">Inner exception</param>
		public pdfGenericIOException(string message, Exception ex):base(message,ex)
		{
		}
	}
}
