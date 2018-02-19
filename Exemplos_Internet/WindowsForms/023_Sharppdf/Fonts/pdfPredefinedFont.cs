using System;
using System.Text;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Class that representa a predefined font
	/// </summary>
	public sealed class pdfPredefinedFont : pdfAbstractFont
	{
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="fontDefinition">Font's Definition Object</param>
		/// <param name="fontNumber">Number of the font into the PDF document</param>
		/// <param name="encodingType">Font's encoding type</param>
		internal pdfPredefinedFont(pdfFontDefinition fontDefinition, int fontNumber, documentFontEncoding encodingType):base(fontDefinition,fontNumber, encodingType)
		{
	
		}

		/// <summary>
		/// Method that returns the PDF codes to write the Font in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public override string getText()
		{
			StringBuilder content  = new StringBuilder();
			content.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Type /Font" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Subtype /Type1" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Name /F" + _fontNumber.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/BaseFont /" + _fontDefinition.fontName + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Encoding /WinAnsiEncoding" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			return content.ToString();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strText"></param>
		/// <returns></returns>
		public override string encodeText(string strText)
		{
			char[] arrChar = strText.ToCharArray();
			StringBuilder returnText = new StringBuilder();
			for (int i = 0; i < arrChar.Length; i++)
			{
				if (Convert.ToInt32(arrChar[i]) < 0 || Convert.ToInt32(arrChar[i]) > 255) { //Character Must Converted in PDF CODE					
					returnText.Append(GlyphConverter.pdfCodeFromUnicode(Convert.ToInt32(arrChar[i])));
				} else { //Normal ASCII Character
					returnText.Append(arrChar[i]);
				}
			}
			return returnText.ToString();
		}

		/// <summary>
		/// Method that removes from the text the unsupported characters
		/// </summary>
		/// <param name="strText">Text to clean</param>
		/// <returns>Cleaned Text</returns>
		public override string cleanText(string strText) {
			StringBuilder cleanedWord = new StringBuilder();
			foreach(char myChar in strText.ToCharArray()) {
				if ((Convert.ToInt32(myChar) >= 0) && (Convert.ToInt32(myChar) <= 255)) {
					cleanedWord.Append(myChar);
				} else {
					if (GlyphConverter.pdfCodeFromUnicode(Convert.ToInt32(myChar)) != "") {
						cleanedWord.Append(myChar);
					}
				}
			}
			return cleanedWord.ToString();	
		}

		/// <summary>
		/// Method that returns the width of a string
		/// </summary>
		/// <param name="strWord">Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <returns></returns>
		public override int getWordWidth(string strWord, int fontSize)
		{
			double returnWeight = 0;
			foreach(char myChar in strWord.ToCharArray()) {
				if (((Convert.ToInt32(myChar) >= 0) && (Convert.ToInt32(myChar) <= 255)) || (GlyphConverter.pdfCodeFromUnicode(Convert.ToInt32(myChar)) != "")) {
					returnWeight += (int)_fontDefinition.fontMetrics[(int)myChar];				
				}
			}
			return Convert.ToInt32(Math.Round(returnWeight * fontSize /1000));
		}

		/// <summary>
		/// Method that returns a cropped word
		/// </summary>
		/// <param name="strWord">Word</param>
		/// <param name="maxLength">Maximum length</param>
		/// <param name="fontSize">Font's size</param>		
		/// <returns>The cropped word</returns>
		public override string cropWord(string strWord, int maxLength, int fontSize)
		{			
			StringBuilder CroppedWord = new StringBuilder();
			strWord = cleanText(strWord);
			int i = 0;
			if (getWordWidth(strWord, fontSize) <= maxLength || strWord.Length == 0) {
				return strWord;
			} else {
				while (getWordWidth(CroppedWord.ToString()+ strWord[i] + "...", fontSize) <= maxLength && i < strWord.Length)
				{
					CroppedWord.Append(strWord[i]);
					i++;
				}
				if (CroppedWord.Length == 0) {
					return "";
				} else {
					return CroppedWord.ToString() + "...";
				}
			}	
		}


	}
}
