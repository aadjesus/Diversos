using System;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.Fonts;

namespace sharpPDF.Tables
{
	/// <summary>
	/// Class that represents the style of a table's row
	/// </summary>
	public class pdfTableStyle
	{
		#region Protected_Properties

		/// <summary>
		/// Style's font
		/// </summary>
		protected pdfAbstractFont _fontReference;
		/// <summary>
		/// Font's size
		/// </summary>
		protected int _fontSize;
		/// <summary>
		/// Font's color
		/// </summary>
		protected pdfColor _fontColor;
		/// <summary>
		/// Background color
		/// </summary>
		protected pdfColor _bgColor;

		#endregion

		/// <summary>
		/// Type of the Font
		/// </summary>
		public pdfAbstractFont fontReference
		{
			get
			{
				return _fontReference;
			}			
		}

		/// <summary>
		/// Size of the Font
		/// </summary>
		public int fontSize
		{
			get
			{
				return _fontSize;
			}
		}

		/// <summary>
		/// Color of the Font
		/// </summary>
		public pdfColor fontColor
		{
			get
			{
				return _fontColor;
			}
		}

		/// <summary>
		/// Color of the BackGround
		/// </summary>
		public pdfColor bgColor
		{
			get
			{
				return _bgColor;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfTableStyle(pdfAbstractFont fontReference, int fontSize, pdfColor fontColor, pdfColor bgColor)
		{		
			_fontReference = fontReference;
			_fontSize = fontSize;
			_fontColor = fontColor;
			_bgColor = bgColor;
		}
	}
}
