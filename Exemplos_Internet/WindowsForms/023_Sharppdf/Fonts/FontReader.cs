using System;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Abstract Class for a generic Font Readers
	/// </summary>
	internal abstract class FontReader : IDisposable
	{
		protected string _fontReference;

		/// <summary>
		/// Class's Constructor
		/// </summary>
		/// <param name="fontReference">Font Reference</param>
		public FontReader(string fontReference)
		{
			_fontReference = fontReference;
		}

		/// <summary>
		/// Method that returs the definition of a font
		/// </summary>
		/// <returns>Font definition object</returns>
		public abstract pdfFontDefinition getFontDefinition();

		/// <summary>
		/// Method that releases all the resources of the FontReader
		/// </summary>
		public abstract void Dispose();		

	}
}
