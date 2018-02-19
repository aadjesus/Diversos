using System;
using System.Text;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.Fonts;

namespace sharpPDF.Elements
{
	/// <summary>
	/// A Class that implements a PDF paragraph's line.
	/// </summary>
	public sealed class paragraphLine : IWritable, ICloneable
	{
		private string _strLine;
		private int _lineLeftMargin;
		private int _lineTopMargin;
		private pdfAbstractFont _fontType;

		/// <summary>
		/// The left margin of the line
		/// </summary>
		public int LineLeftMargin
		{
			get
			{
				return _lineLeftMargin;
			}
		}

		/// <summary>
		/// The top margin of the line
		/// </summary>
		public int LineTopMargin
		{
			get
			{
				return _lineTopMargin;
			}
		}

		/// <summary>
		/// The text of the line
		/// </summary>
		public string StrLine
		{
			get
			{
				return _strLine;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="strLine">Text of the line</param>
		/// <param name="lineTopMargin">Top margin</param>
		/// <param name="lineLeftMargin">Left margin</param>
		/// <param name="fontType">Font Type</param>
		public paragraphLine(string strLine, int lineTopMargin, int lineLeftMargin, pdfAbstractFont fontType)
		{
			_strLine = strLine;
			_lineTopMargin = lineTopMargin;
			_lineLeftMargin = lineLeftMargin;
			_fontType = fontType;
		}

		/// <summary>
		/// Method that returns the PDF codes to write the paragraph's line in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public string getText()
		{
			StringBuilder resultString = new StringBuilder();			
			resultString.Append(_lineLeftMargin.ToString() + " -"+ _lineTopMargin.ToString() + " Td" + Convert.ToChar(13) + Convert.ToChar(10));
			if (_fontType is pdfTrueTypeFont) {				
				resultString.Append("(" + textAdapter.checkText(_fontType.encodeText(_strLine)) + ") Tj" + Convert.ToChar(13) + Convert.ToChar(10));
			} else {
				resultString.Append("(" + _fontType.encodeText(textAdapter.checkText(_strLine)) + ") Tj" + Convert.ToChar(13) + Convert.ToChar(10));				
			}			
			resultString.Append("-" + _lineLeftMargin.ToString().Replace(",",".") + " 0 Td" + Convert.ToChar(13) + Convert.ToChar(10));			
			return resultString.ToString();
		}

		#region ICloneable

		/// <summary>
		/// Method that clones the element
		/// </summary>
		/// <returns>Cloned object</returns>
		public object Clone() {
			return new paragraphLine((string)this._strLine.Clone(), this._lineTopMargin, this._lineLeftMargin, this._fontType);
		}

		#endregion
	}
}
