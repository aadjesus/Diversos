using System;
using System.IO;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts;

namespace sharpPDF.Fonts.AFM
{
	/// <summary>
	/// Class that reads an afm file to import the font metric into the PDF font object.
	/// </summary>
	internal class afmFontReader : FontReader
	{

		private char[] csDelimiterToken = {' ','\t'};
		private char[] csSemicolonToken = {';'};
		StreamReader _afmStream = null;

		/// <summary>
		/// Class's Constructor
		/// </summary>
		/// <param name="fontReference">Font Reference</param>
		public afmFontReader(string fontReference):base(fontReference)
		{
			try {
				_afmStream = new StreamReader(GetType().Assembly.GetManifestResourceStream("sharpPDF.Fonts.AFM." + _fontReference + ".afm"));				
			} catch (Exception ex) {
				throw new pdfGenericIOException(ex.Message, ex);
			}
		}

		/// <summary>
		/// Method that returs the definition of a font
		/// </summary>
		/// <returns>Font definition object</returns>
		public override pdfFontDefinition getFontDefinition() {

			pdfFontDefinition myDefinition = new pdfFontDefinition();
			string strLine = null;
			string[] afmValues = null;
			pdfCharacterMetric myChar = null;
			bool startCharMetric = false;
			
			try {				

				strLine = _afmStream.ReadLine();

				while (strLine != null) {					
					
					afmValues = strLine.Split(this.csDelimiterToken);
					
					switch (afmValues[0]) {
							//Font Name
						case "FontName":
							myDefinition.fontName = afmValues[1].Trim();
							break;
							//Font Full Name
						case "FullName":
							myDefinition.fullFontName = afmValues[1].Trim();
							break;
							//Family Name
						case "FamilyName":
							myDefinition.familyName = afmValues[1].Trim();
							break;
							//Font Weight
						case "Weight":
							myDefinition.fontWeight = afmValues[1].Trim();
							break;
							//Italic Angle
						case "ItalicAngle":
							myDefinition.italicAngle = int.Parse(afmValues[1]);
							break;
							//Is Fixed Pitch
						case "IsFixedPitch":
							myDefinition.isFixedPitch = bool.Parse(afmValues[1]);
							break;
							//CharacterSet
						case "CharacterSet":
							myDefinition.characterSet = afmValues[1].Trim();
							break;
							//Font BBox
						case "FontBBox":
							myDefinition.fontBBox[0] = int.Parse(afmValues[1]);
							myDefinition.fontBBox[1] = int.Parse(afmValues[2]);
							myDefinition.fontBBox[2] = int.Parse(afmValues[3]);
							myDefinition.fontBBox[3] = int.Parse(afmValues[4]);
							myDefinition.fontHeight = Convert.ToInt32(Math.Round((((double)myDefinition.fontBBox[3] - (double)myDefinition.fontBBox[1]) / 1000)));
							if (myDefinition.fontHeight == 0) {
								myDefinition.fontHeight = 1;
							}
							break;
							//Underline Position
						case "UnderlinePosition":
							myDefinition.underlinePosition = int.Parse(afmValues[1]);
							break;
							//Underline Thickness
						case "UnderlineThickness":
							myDefinition.underlineThickness = int.Parse(afmValues[1]);
							break;
							//Encoding Scheme
						case "EncodingScheme":
							myDefinition.encodingScheme = afmValues[1].Trim();
							break;
							//Cap Height
						case "CapHeight":
							myDefinition.capHeight = int.Parse(afmValues[1]);
							break;
							//Font Height
						/*
						case "XHeight":
							myDefinition.fontHeight = int.Parse(afmValues[1]);
							break;
						*/
							//Font Ascender
						case "Ascender":
							myDefinition.ascender = int.Parse(afmValues[1]);
							break;
							//Font Descender
						case "Descender":
							myDefinition.descender = int.Parse(afmValues[1]);
							break;
							//Font StdHW
						case "StdHW":
							myDefinition.StdHW = int.Parse(afmValues[1]);
							break;
							//Font StdVW
						case "StdVW":
							myDefinition.StdVW = int.Parse(afmValues[1]);
							break;
							//Star Character Metric
						case "StartCharMetrics":
							startCharMetric = true;
							break;
							//End Character Metric
						case "EndCharMetrics":
							startCharMetric = false;
							break;
							//Font's Character Metric
						case "C":
							if (startCharMetric) {
								myChar = getCharacterMetric(strLine);
								myDefinition.fontMetrics[myChar.charIndex] = myChar.charWidth;
							}
							break;
					}

					strLine = _afmStream.ReadLine();

				}

			} catch (Exception ex) {
				throw new pdfBadFontFileException();
			}
			return myDefinition;
		}
		
		/// <summary>
		/// Method that returns the metric of a single character
		/// </summary>
		/// <param name="characterMetric">String that contains character info</param>
		/// <returns>Character Metric Object</returns>
		private pdfCharacterMetric getCharacterMetric(string characterMetric) {
			
			string[] charTokens = characterMetric.Split(this.csSemicolonToken);
			string[] tokenValues;

			pdfCharacterMetric myChar = null;

			int charWidth = 0;
			int charIndex = 0;
			string charName = null;
			
			foreach(string charToken in charTokens) {
				tokenValues = charToken.Trim().Split(this.csDelimiterToken);
				switch (tokenValues[0]) {
					case "C":
						charIndex = int.Parse(tokenValues[1]);
						break;
					case "WX":
						charWidth = int.Parse(tokenValues[1]);
						break;
					case "N":
						charName = tokenValues[1];
						break;
				}
			}

			charTokens = null;			
			tokenValues = null;

			myChar = new pdfCharacterMetric(charName, GlyphConverter.UnicodeFromGlyph(charName), charWidth);

			return myChar;

		}

		/// <summary>
		/// Class's destructor
		/// </summary>
		public override void Dispose() {
			if (_afmStream != null) {
				_afmStream.Close();
				_afmStream = null;
			}			
		}

	}
}
