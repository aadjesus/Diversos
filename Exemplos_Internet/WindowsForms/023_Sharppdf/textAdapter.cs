using System;
using System.Collections;
using System.Text;

using sharpPDF.Collections;
using sharpPDF.Elements;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts;

namespace sharpPDF
{
	/// <summary>
	/// Abstract class that implements different functions used for text and paragraph
	/// </summary>
	public abstract class textAdapter
	{

		/// <summary>
		/// Method that format a HEX string
		/// </summary>
		/// <param name="strText">input text string</param>
		/// <returns>HEX text string</returns>
		public static string HEXFormatter(string strText)
		{
			StringBuilder returnHexText = new StringBuilder();
			foreach(char myChar in strText.ToCharArray())
			{
				returnHexText.Append(Convert.ToByte(myChar).ToString("X2"));			
			}
			return returnHexText.ToString();
		}

		/// <summary>
		/// Static method that checks special characters into a string
		/// </summary>
		/// <param name="strText">Input Text</param>
		/// <returns>Formatted Text</returns>
		public static string checkText(string strText)
		{
			string checkedString = strText;
			checkedString = checkedString.Replace(@"\",@"\\");
			checkedString = checkedString.Replace(@"(",@"\(");
			checkedString = checkedString.Replace(@")",@"\)");
			return checkedString;
		}
		
		private static paragraphLine createNewLine(string text, predefinedAlignment parAlign, int parWidth, int lineLength, int lineHeight, pdfAbstractFont fontType)
		{
			paragraphLine ReturnedLine;
			switch (parAlign) {
				case predefinedAlignment.csLeft: default:
					ReturnedLine = new paragraphLine(text,lineHeight,0, fontType);
					break;
				case predefinedAlignment.csRight:
					ReturnedLine = new paragraphLine(text,lineHeight,parWidth - lineLength, fontType);
					break;
				case predefinedAlignment.csCenter:
					ReturnedLine = new paragraphLine(text,lineHeight,Convert.ToInt32(Math.Round(((double)(parWidth - (double)lineLength) / 2d))), fontType);
					break;
			}	
			if (((pdfAbstractFont)fontType) is pdfTrueTypeFont) {
				((pdfTrueTypeFont)fontType).addCharacters(text);
			}
			return ReturnedLine;
		}

		/// <summary>
		/// Static method thats format a paragraph
		/// </summary>
		/// <param name="strText">Input Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontType">Font's type</param>
		/// <param name="parWidth">Paragrapfh's width</param>
		/// <param name="lineHeight">Line's height</param>
		/// <param name="maxLines">Maximum number of lines in a paragraph</param>
		/// <returns>IEnumerable interface that cointains paragraphLine objects</returns>
		public static paragraphLineList formatParagraph(ref string strText,int fontSize,pdfAbstractFont fontType,int parWidth, int maxLines,int lineHeight)
		{
			return formatParagraph(ref strText,fontSize,fontType,parWidth,maxLines,lineHeight,predefinedAlignment.csLeft);	
		}

		/// <summary>
		/// Static method thats format a paragraph
		/// </summary>
		/// <param name="strText">Input Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontType">Font's type</param>
		/// <param name="parWidth">Paragrapfh's width</param>
		/// <param name="lineHeight">Line's height</param>
		/// <param name="parAlign">Paragraph's Alignment</param>
		/// <param name="maxLines">Number of maximum lines of the paragraph</param>
		/// <returns>IEnumerable interface that cointains paragraphLine objects</returns>
		public static paragraphLineList formatParagraph(ref string strText,int fontSize,pdfAbstractFont fontType,int parWidth,int maxLines, int lineHeight,predefinedAlignment parAlign)
		{			
			string[] lines = strText.Replace("\r","").Split(new char[1]{(char)10});
			string[] words;
			string word;
			bool finished = false;
			int lineLength;
			StringBuilder lineString = new StringBuilder();	
			paragraphLineList resultParagraph = new paragraphLineList();
			lineLength = 0;
			int countLine = 0;
			int countWord = 0;
			while (!finished && (countLine < lines.Length)) {
				words = lines[countLine].Split(" ".ToCharArray());
				countWord = 0;
				while (!finished && (countWord < words.Length)) {
					word = fontType.cleanText(words[countWord]);					
					if (word.Trim() != "") {
						if ((fontType.getWordWidth(word + " ",fontSize) + lineLength) > parWidth) {	
							if (lineLength == 0) {
								resultParagraph.Add(textAdapter.createNewLine(fontType.cropWord(word, parWidth, fontSize), parAlign, parWidth, parWidth,  lineHeight, fontType));
								strText.Remove(0, words[countWord].Length).Trim();
								countWord++;								
							} else {
								resultParagraph.Add(textAdapter.createNewLine(lineString.ToString().Trim(), parAlign, parWidth, lineLength,  lineHeight, fontType));								
								lineString.Remove(0,lineString.Length);
								lineLength = 0;					                            	
							}
							if ((resultParagraph.Count) == maxLines && maxLines > 0) {
								finished = true;
							}
						} else {
							lineString.Append(word + " ");						
							lineLength += fontType.getWordWidth(word + " ",fontSize);
							strText = strText.Remove(0,words[countWord].Length).Trim();	
							countWord++;
						}
					} else {
						countWord++;
					}
				}
				countLine++;
				if (!finished) {
					if (lineLength > 0) {					
						resultParagraph.Add(textAdapter.createNewLine(lineString.ToString().Trim(), parAlign, parWidth, lineLength, lineHeight, fontType));
						lineString.Remove(0,lineString.Length);
						lineLength = 0;					
					} else {																
						resultParagraph.Add(new paragraphLine("",lineHeight,0,fontType));
					}
					if ((resultParagraph.Count) == maxLines && maxLines > 0) {
						finished = true;
					}				
				}
				words = null;
			}
			return resultParagraph;	
		}

	}
}
