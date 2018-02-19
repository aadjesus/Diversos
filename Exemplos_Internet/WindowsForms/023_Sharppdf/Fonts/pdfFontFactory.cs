using System;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts.AFM;
using sharpPDF.Fonts.TTF;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Class that represents a Font Factory
	/// </summary>
	internal abstract class pdfFontFactory
	{

		private readonly static string[] predefinedFontName = {"None","Helvetica","Helvetica-Bold","Helvetica-Oblique","Helvetica-BoldOblique","Courier","Courier-Bold","Courier-Oblique","Courier-BoldOblique","Times-Roman","Times-Bold","Times-Italic","Times-BoldItalic"};
		
		/// <summary>
		/// Method that returns an abstract font object[pdfAbstractFont Factory]
		/// </summary>
		/// <param name="fontReference">Font Name(for predefined fonts) or Font File</param>
		/// <param name="fontNumber">Number of the font inside the PDF</param>
		/// <param name="fontType">Type of the font</param>
		/// <returns>Abstract Font Object</returns>
		public static pdfAbstractFont getFontObject(string fontReference, int fontNumber, documentFontType fontType )
		{
			pdfAbstractFont fontObject = null;			
			FontReader fontReader = getFontReader(fontReference, fontType);	
			switch (fontType) {
				case documentFontType.csPredefinedfont:
				default:
					fontObject = new pdfPredefinedFont(fontReader.getFontDefinition(), fontNumber, documentFontEncoding.csWinAnsiEncoding);
					break;
				case documentFontType.csTrueTypeFont:
					fontObject = new pdfTrueTypeFont(fontReader.getFontDefinition(), fontNumber, documentFontEncoding.csIdentityH, fontReference);
					break;
			}
			fontReader = null;			
			return fontObject;
		}		

		/// <summary>
		/// Method that returns a generic font reader[FontReader Factory]
		/// </summary>
		/// <param name="fontReference">Font Name(for predefined fonts) or Font File</param>
		/// <param name="fontType">Type of the font</param>
		/// <returns>Generic Font Reader Object</returns>
		private static FontReader getFontReader(string fontReference, documentFontType fontType)
		{			
			FontReader fontReader;
			switch (fontType) {
				case documentFontType.csPredefinedfont:
				default:
					fontReader = new afmFontReader(fontReference);				
					break;
				case documentFontType.csTrueTypeFont:
					fontReader = new ttfFontReader(fontReference);
					break;
			}
			return fontReader;
		}

		/// <summary>
		/// Method that returns that font name for a predefined font
		/// </summary>
		/// <param name="fontStyle">Font style</param>
		/// <returns>Predefined font name</returns>
		public static string getPredefinedFontName(predefinedFont fontStyle)
		{
			return pdfFontFactory.predefinedFontName[Convert.ToInt32(fontStyle)];
		}

		/// <summary>
		/// Method that returns if the font reference is a predefined font
		/// </summary>
		/// <param name="fontReference">Font Reference</param>
		/// <returns>Boolean value that shows if the font reference is a predefined font</returns>
		public static bool isPredefinedFont(string fontReference)
		{			
			return ((fontReference == "Helvetica") ||
					(fontReference == "Helvetica-Bold") ||
					(fontReference == "Helvetica-Oblique") ||
					(fontReference == "Helvetica-BoldOblique") ||
					(fontReference == "Courier") ||
					(fontReference == "Courier-Bold") ||
					(fontReference == "Courier-Oblique") ||
					(fontReference == "Courier-BoldOblique") ||
					(fontReference == "Times-Roman") ||
					(fontReference == "Times-Bold") ||
					(fontReference == "Times-Italic") ||
					(fontReference =="Times-BoldItalic"));
		}
	}
}
