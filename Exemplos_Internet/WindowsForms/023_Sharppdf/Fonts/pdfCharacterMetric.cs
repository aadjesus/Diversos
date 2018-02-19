using System;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Class that represent a character metric
	/// </summary>
	internal class pdfCharacterMetric
	{
		private string _charName;
		private int _charIndex;
		private int _charWidth;		

		/// <summary>
		/// Character Name
		/// </summary>
		public string charName
		{
			get
			{
				return _charName;
			}
		}

		/// <summary>
		/// Character Index
		/// </summary>
		public int charIndex
		{
			get
			{
				return _charIndex;
			}
		}

		/// <summary>
		/// Character Width
		/// </summary>
		public int charWidth
		{
			get
			{
				return _charWidth;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="charName">Character Name</param>
		/// <param name="charIndex">Character Index</param>
		/// <param name="charWidth">Character Width</param>
		public pdfCharacterMetric(string charName, int charIndex, int charWidth)
		{
			_charName = charName;
			_charIndex = charIndex;
			_charWidth = charWidth;
		}
	}
}
