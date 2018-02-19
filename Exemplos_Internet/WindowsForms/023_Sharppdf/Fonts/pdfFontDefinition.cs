using System;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Class that represents a definition for a PDF font
	/// </summary>
	public class pdfFontDefinition
	{

		private string _fontName = "";
		private string _fullFontName = "";
		private string _familyName = "";
		private string _fontWeight = "";
		private int _italicAngle = 0;		
		private bool _isFixedPitch = false;
		private string _characterSet = "";
		private int _underlinePosition = 0;
		private int _underlineThickness = 0;
		private string _encodingScheme = "";
		private int _capHeight = 0;
		private int _ascender = 0;
		private int _descender = 0;
		private int _StdHW = 0;
		private int _StdVW = 0;
		private int[] _fontBBox = {0,0,0,0};
		private int _fontHeight = 0;		
		private object[] _fontMetrics = new object[65536];


		/// <summary>
		/// Name of the font
		/// </summary>
		public string fontName
		{
			get
			{
				return _fontName;
			}
			set
			{
				_fontName = value;
			}			
		}

		/// <summary>
		/// Full name of the font
		/// </summary>
		public string fullFontName
		{
			get
			{
				return _fullFontName;
			}
			set
			{
				_fullFontName = value;
			}			
		}

		/// <summary>
		/// Family of the font
		/// </summary>
		public string familyName
		{
			get
			{
				return _familyName;
			}
			set
			{
				_familyName = value;
			}			
		}

		/// <summary>
		/// Font's Weight
		/// </summary>
		public string fontWeight
		{
			get
			{
				return _fontWeight;
			}
			set
			{
				_fontWeight = value;
			}
		}

		/// <summary>
		/// Property that shows if the character uses a fixed spacing
		/// </summary>
		public bool isFixedPitch
		{
			get
			{
				return _isFixedPitch;
			}
			set
			{
				_isFixedPitch = value;
			}
		}

		/// <summary>
		/// Font's Italic Angle
		/// </summary>
		public int italicAngle
		{
			get
			{
				return _italicAngle;
			}
			set
			{
				_italicAngle = value;
			}
		}

		/// <summary>
		/// Name of the character set
		/// </summary>
		public string characterSet
		{
			get
			{
				return _characterSet;
			}
			set
			{
				_characterSet = value;
			}
		}

		/// <summary>
		/// Position of the underline symbol
		/// </summary>
		public int underlinePosition
		{
			get
			{
				return _underlinePosition;
			}
			set
			{
				_underlinePosition = value;
			}		
		}

		/// <summary>
		/// Thickness of the underline symbol
		/// </summary>
		public int underlineThickness
		{
			get
			{
				return _underlineThickness;
			}
			set
			{
				_underlineThickness = value;
			}
		}

		/// <summary>
		/// Name of the encoding scheme
		/// </summary>
		public string encodingScheme
		{
			get
			{
				return _encodingScheme;
			}
            set
			{
				_encodingScheme = value;
			}
		}

		/// <summary>
		/// Font's Cap Height
		/// </summary>
		public int capHeight
		{
			get
			{
				return _capHeight;
			}
			set
			{
				_capHeight = value;
			}
		}

		/// <summary>
		/// Font's Ascender
		/// </summary>
		public int ascender
		{
			get
			{
				return _ascender;
			}
			set
			{
				_ascender = value;
			}		
		}

		/// <summary>
		/// Font's Descender
		/// </summary>
		public int descender
		{
			get
			{
				return _descender;
			}
			set
			{
				_descender = value;
			}
		}

		/// <summary>
		/// Font's StdHW
		/// </summary>
		public int StdHW
		{
			get
			{
				return _StdHW;
			}
			set
			{
				_StdHW = value;
			}
		}

		/// <summary>
		/// Font's StdVW
		/// </summary>
		public int StdVW
		{
			get
			{
				return _StdVW;
			}
			set
			{
				_StdVW = value;
			}
		}

		/// <summary>
		/// Font's BBox
		/// </summary>
		public int[] fontBBox
		{
			get
			{
				return _fontBBox;
			}
			set
			{
				_fontBBox = value;
			}
		}

		/// <summary>
		/// Height of the font
		/// </summary>
		public int fontHeight
		{
			get
			{
				return _fontHeight;
			}
			set
			{
				_fontHeight = value;
			}
		}

		/// <summary>
		/// Width of all characters
		/// </summary>
		public object[] fontMetrics
		{
			get
			{
				return _fontMetrics; 
			}
			set
			{
				_fontMetrics = value;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfFontDefinition()
		{
			
		}

		/// <summary>
		/// Method that validates a font definition
		/// </summary>
		/// <returns>Boolean value that shows if the object is a valid font definition</returns>
		public bool validateFontDefinition()
		{
			return true;
		}
	}
}
