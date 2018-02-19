using System;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF
{
	/// <summary>
	/// A Class that implements a PDF color.
	/// </summary>
	public class pdfColor : ICloneable
	{
		private string _rColor;
		private string _gColor;
		private string _bColor;

		#region Standard_Colors

		/// <summary>
		/// No Color
		/// </summary>
		public readonly static pdfColor NoColor = new pdfColor("","","");
		/// <summary>
		/// Black Color
		/// </summary>
		public readonly static pdfColor Black = new pdfColor("0","0","0");
		/// <summary>
		/// White Color
		/// </summary>
		public readonly static pdfColor White = new pdfColor("1","1","1");
		/// <summary>
		/// Red Color
		/// </summary>
		public readonly static pdfColor Red = new pdfColor("1","0","0");
		/// <summary>
		/// Light Red Color
		/// </summary>
		public readonly static pdfColor LightRed = new pdfColor("1",".75",".75");
		/// <summary>
		/// Dark Red Color
		/// </summary>
		public readonly static pdfColor DarkRed = new pdfColor(".5","0","0");
		/// <summary>
		/// Orange Color
		/// </summary>
		public readonly static pdfColor Orange = new pdfColor("1",".5","0");
		/// <summary>
		/// Light Orange Color
		/// </summary>
		public readonly static pdfColor LightOrange = new pdfColor("1",".75","0");
		/// <summary>
		/// Dark Orange Color
		/// </summary>
		public readonly static pdfColor DarkOrange = new pdfColor("1",".25","0");
		/// <summary>
		/// Yellow Color
		/// </summary>
		public readonly static pdfColor Yellow = new pdfColor("1","1",".25");
		/// <summary>
		/// Light Yellow Color
		/// </summary>
		public readonly static pdfColor LightYellow = new pdfColor("1","1",".75");
		/// <summary>
		/// Dark Yellow Color
		/// </summary>
		public readonly static pdfColor DarkYellow = new pdfColor("1","1","0");
		/// <summary>
		/// Blue Color
		/// </summary>
		public readonly static pdfColor Blue = new pdfColor("0","0","1");
		/// <summary>
		/// Light Blue Color
		/// </summary>
		public readonly static pdfColor LightBlue = new pdfColor(".1",".3",".75");
		/// <summary>
		/// Dark Blue Color
		/// </summary>
		public static pdfColor DarkBlue = new pdfColor("0","0",".5");
		/// <summary>
		/// Green Color
		/// </summary>
		public readonly static pdfColor Green = new pdfColor("0","1","0");
		/// <summary>
		/// Light Green Color
		/// </summary>
		public readonly static pdfColor LightGreen = new pdfColor(".75","1",".75");
		/// <summary>
		/// Dark Green Color
		/// </summary>
		public readonly static pdfColor DarkGreen = new pdfColor("0",".5","0");
		/// <summary>
		/// Cyan Color
		/// </summary>
		public readonly static pdfColor Cyan = new pdfColor("0",".5","1");
		/// <summary>
		/// Light Cyan Color
		/// </summary>
		public readonly static pdfColor LightCyan = new pdfColor(".2",".8","1");
		/// <summary>
		/// Dark Cyan Color
		/// </summary>
		public readonly static pdfColor DarkCyan = new pdfColor("0",".4",".8");
		/// <summary>
		/// Purple Color
		/// </summary>
		public readonly static pdfColor Purple = new pdfColor(".5","0","1");
		/// <summary>
		/// Light Purple Color
		/// </summary>
		public readonly static pdfColor LightPurple = new pdfColor(".75",".45",".95");
		/// <summary>
		/// Dark Purple Color
		/// </summary>
		public readonly static pdfColor DarkPurple = new pdfColor(".4",".1",".5");
		/// <summary>
		/// Gray Color
		/// </summary>
		public readonly static pdfColor Gray = new pdfColor(".5",".5",".5");
		/// <summary>
		/// Light Gray Color
		/// </summary>
		public readonly static pdfColor LightGray = new pdfColor(".75",".75",".75");
		/// <summary>
		/// Dark Gray Color
		/// </summary>
		public readonly static pdfColor DarkGray = new pdfColor(".25",".25",".25");		

		#endregion

		/// <summary>
		/// R property of RGB color
		/// </summary>
		internal string rColor
		{
			get
			{
				return _rColor;
			}
		}

		/// <summary>
		/// G property of RGB color
		/// </summary>
		internal string gColor
		{
			get
			{
				return _gColor;
			}
		}

		/// <summary>
		/// B property of RGB color
		/// </summary>
		internal string bColor
		{
			get
			{
				return _bColor;
			}				
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="HEXColor">Hex Color</param>
		public pdfColor(string HEXColor) 
		{
			_rColor = formatColorComponent(int.Parse(HEXColor.Substring(0,2), System.Globalization.NumberStyles.HexNumber));
			_gColor = formatColorComponent(int.Parse(HEXColor.Substring(2,2), System.Globalization.NumberStyles.HexNumber));
			_bColor = formatColorComponent(int.Parse(HEXColor.Substring(4,2), System.Globalization.NumberStyles.HexNumber));			
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="rColor">Red component of the color</param>
		/// <param name="gColor">Green component of the color</param>
		/// <param name="bColor">Blue component of the color</param>
		public pdfColor(int rColor, int gColor, int bColor)
		{
			_rColor = formatColorComponent(rColor);
			_gColor = formatColorComponent(gColor);
			_bColor = formatColorComponent(bColor);
		}

		/// <summary>
		/// Internal constructor for the creation of the standard colors
		/// </summary>
		/// <param name="rColor">Red component of the color</param>
		/// <param name="gColor">Green component of the color</param>
		/// <param name="bColor">Blue component of the color</param>
		internal pdfColor(string rColor, string gColor, string bColor)
		{
			_rColor = rColor;
			_gColor = gColor;
			_bColor = bColor;
		}

		/// <summary>
		/// Method that formats a int color value with the pdf color format
		/// </summary>
		/// <param name="colorValue">Component of the color</param>
		/// <returns>Formatted component of the color</returns>
		private string formatColorComponent(int colorValue)
		{
			int colorComponent;
			colorComponent = Convert.ToInt32(Math.Round(((Convert.ToSingle(colorValue) / 255) * 100)));
			string resultValue;			
			if (colorComponent == 0 || colorComponent < 0)
			{
				resultValue = "0";
			}
			else if (colorComponent < 100) 
			{				
				resultValue = "." + colorComponent.ToString();
				if (resultValue[resultValue.Length - 1] == '0') 
				{
					resultValue = resultValue.Substring(0,resultValue.Length - 1);
				}
			} 
			else 
			{
				resultValue = "1";
			}
			return resultValue;
		}

		/// <summary>
		/// Method that validates the color
		/// </summary>
		/// <returns>Boolean value that represents the validity of the color</returns>
		internal bool isColor()
		{
			return (_rColor != "" && _gColor != "" && _bColor != "");
		}

		#region ICloneable
		
		/// <summary>
		/// Method that clones the pdfColorObject
		/// </summary>
		/// <returns>Cloned Object</returns>
		public object Clone()
		{
			return new pdfColor(this._rColor, this._gColor, this._bColor);
		}

		#endregion
	}
}
