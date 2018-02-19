using System;

namespace sharpPDF.Fonts.TTF
{
	/// <summary>
	/// Font Header Table (head)
	/// </summary>
	internal struct FontHeaderTable 
	{
		public int flags;
		public int unitsPerEm;
		public short xMin;
		public short yMin;
		public short xMax;
		public short yMax;
		public int macStyle;
	}
}