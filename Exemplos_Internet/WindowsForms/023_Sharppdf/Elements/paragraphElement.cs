using System;
using System.Text;
using System.Collections;

using sharpPDF.Collections;
using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.Fonts;

namespace sharpPDF.Elements
{
	/// <summary>
	/// A Class that implements a PDF paragraph.
	/// </summary>
	public sealed class paragraphElement : pdfElement
	{
		private paragraphLineList _content;
		private int _fontSize;		
		private int _lineHeight;
		private pdfAbstractFont _fontType;		

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newContent">Text of the paragraph</param>
		/// <param name="paragraphWidth">Width of the paragraph</param>
		/// <param name="lineHeight">Height of a single line</param>
		/// <param name="newFontSize">Font's size</param>
		/// <param name="newFontType">Font's type</param>
		/// <param name="newCoordX">X position in the PDF document</param>
		/// <param name="newCoordY">Y position in the PDF document</param>
		public paragraphElement(paragraphLineList newContent, int paragraphWidth, int lineHeight, int newFontSize, pdfAbstractFont newFontType, int newCoordX, int newCoordY):this(newContent, paragraphWidth, lineHeight, newFontSize, newFontType, newCoordX, newCoordY, pdfColor.Black)
		{
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newContent">Text of the paragraph</param>
		/// <param name="paragraphWidth">Width of the paragraph</param>
		/// <param name="lineHeight">Height of a single line</param>
		/// <param name="newFontSize">Font's size</param>
		/// <param name="newFontType">Font's type</param>
		/// <param name="newCoordX">X position in the PDF document</param>
		/// <param name="newCoordY">Y position in the PDF document</param>
		/// <param name="newStrokeColor">Font's color</param>
		public paragraphElement(paragraphLineList newContent, int paragraphWidth, int lineHeight, int newFontSize, pdfAbstractFont newFontType, int newCoordX, int newCoordY, pdfColor newStrokeColor)
		{
			_content = newContent;
			_fontSize = newFontSize;
			_fontType = newFontType;
			_coordX = newCoordX;
			_coordY = newCoordY;
			_strokeColor = newStrokeColor;
			_width = paragraphWidth;
			_height = lineHeight * newContent.Count;
			_lineHeight = lineHeight;			
		}

		/// <summary>
		/// IEnumerable interface that contains paragraph's lines
		/// </summary>
		public paragraphLineList content
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
		/// Method that returns the PDF codes to write the paragraph in the document
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
            if(_strokeColor.isColor())
				hexContent.Append(_strokeColor.rColor + " " + _strokeColor.gColor + " " + _strokeColor.bColor + " rg" + Convert.ToChar(13) + Convert.ToChar(10));            
            hexContent.Append(_coordX.ToString() + " " + _coordY.ToString() + " Td" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("14 TL" + Convert.ToChar(13) + Convert.ToChar(10));
			foreach (paragraphLine line in _content) {
					hexContent.Append(line.getText());
			}
            hexContent.Append("ET" + Convert.ToChar(13) + Convert.ToChar(10));
            hexContent.Append("Q" + Convert.ToChar(13) + Convert.ToChar(10));
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
			paragraphLineList newContent = new paragraphLineList();
			foreach(paragraphLine myLine in this._content)
			{
				newContent.Add((paragraphLine)myLine.Clone());
			}
			return new paragraphElement(newContent, this._width, this._lineHeight, this._fontSize, this._fontType, this._coordX, this._coordY, (pdfColor)this._strokeColor.Clone());
		}
	}
}
