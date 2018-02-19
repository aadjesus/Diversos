using System;
using System.Text;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.Fonts;

namespace sharpPDF.Elements
{
	/// <summary>
	/// A Class that implements simple PDF text.
	/// </summary>
	public sealed class textElement : pdfElement
	{
		private string _content;
		private int _fontSize;		
		private pdfAbstractFont _fontType;		

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newContent">Text's content</param>
		/// <param name="newFontSize">Font's size</param>
		/// <param name="newFontType">Font's type</param>
		/// <param name="newCoordX">X position of the text in the page</param>
		/// <param name="newCoordY">Y position of the text in the page</param>
		public textElement(string newContent, int newFontSize, pdfAbstractFont newFontType, int newCoordX, int newCoordY):this(newContent, newFontSize, newFontType, newCoordX, newCoordY, pdfColor.Black)
		{				
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newContent">Text's content</param>
		/// <param name="newFontSize">Font's size</param>
		/// <param name="newFontType">Font's type</param>
		/// <param name="newCoordX">X position of the text in the page</param>
		/// <param name="newCoordY">Y position of the text in the page</param>
		/// <param name="newStrokeColor">Font's color</param>
		public textElement(string newContent, int newFontSize, pdfAbstractFont newFontType, int newCoordX, int newCoordY, pdfColor newStrokeColor)
		{
			_content = newContent;
			_fontSize = newFontSize;
			_fontType = newFontType;
			_coordX = newCoordX;
			_coordY = newCoordY;
			_strokeColor = newStrokeColor;
			_height = newFontType.fontDefinition.fontHeight * newFontSize;
			_width = newFontType.getWordWidth(newContent,newFontSize);
			if (newFontType is pdfTrueTypeFont) {
				((pdfTrueTypeFont)newFontType).addCharacters(newContent);
			}
		}

		/// <summary>
		/// Text's content
		/// </summary>
		public string content
		{
			get
			{
				return _content;
			}

			set
			{
				_content = value;
			}

		}

		/// <summary>
		/// Font's size
		/// </summary>
		public int fontSize
		{
			get
			{
				return _fontSize;
			}

			set
			{
				_fontSize = value;
			}
		}

		/// <summary>
		/// Font's type
		/// </summary>
		public pdfAbstractFont fontType
		{
			get
			{
				return fontType;
			}

			set
			{
				_fontType = value;
			}
		}

		/// <summary>
		/// Method that returns the PDF codes to write the text in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public override string getText()
		{
			StringBuilder resultText = new StringBuilder();
			StringBuilder hexContent = new StringBuilder();
			resultText.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
            resultText.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
            resultText.Append("/Filter [/ASCIIHexDecode]" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("q" + Convert.ToChar(13) + Convert.ToChar(10));
            hexContent.Append("BT" + Convert.ToChar(13) + Convert.ToChar(10));
            hexContent.Append("/F" + _fontType.fontNumber.ToString() + " " + _fontSize.ToString() + " Tf" + Convert.ToChar(13) + Convert.ToChar(10));
            if(_strokeColor.isColor()) {
				hexContent.Append(_strokeColor.rColor + " " + _strokeColor.gColor + " " + _strokeColor.bColor + " rg" + Convert.ToChar(13) + Convert.ToChar(10));            
			}
            hexContent.Append(_coordX.ToString() + " " + _coordY.ToString() + " Td" + Convert.ToChar(13) + Convert.ToChar(10));
            if (_fontType is pdfTrueTypeFont) {
				hexContent.Append("(" + textAdapter.checkText(_fontType.encodeText(_content)) + ") Tj" + Convert.ToChar(13) + Convert.ToChar(10));
			} else {
				hexContent.Append("(" + _fontType.encodeText(textAdapter.checkText(_content)) + ") Tj" + Convert.ToChar(13) + Convert.ToChar(10));
            }
			hexContent.Append("ET" + Convert.ToChar(13) + Convert.ToChar(10));
            hexContent.Append("Q");
			resultText.Append("/Length " + ((hexContent.Length * 2) + 1).ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			resultText.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
            resultText.Append("stream" + Convert.ToChar(13) + Convert.ToChar(10));
            resultText.Append(textAdapter.HEXFormatter(hexContent.ToString()) + ">" + Convert.ToChar(13) + Convert.ToChar(10));			
			resultText.Append("endstream" + Convert.ToChar(13) + Convert.ToChar(10));            
            resultText.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent = null;
			return resultText.ToString();
		}

		/// <summary>
		/// Method that clones the element
		/// </summary>
		/// <returns>Cloned object</returns>
		public override object Clone()
		{
			return new textElement((string)_content.Clone(), _fontSize, _fontType, _coordX, _coordY, (pdfColor)_strokeColor.Clone());
		}
	}
}
