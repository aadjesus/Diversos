using System;

namespace sharpPDF.Fonts.TTF
{
	/// <summary>
	/// Horizontal Header Table (hhea)
	/// </summary>
	internal struct HorizontalHeaderTable 
	{
		public short Ascender;
		public short Descender;
		public short LineGap;
		public int advanceWidthMax;
		public short minLeftSideBearing;
		public short minRightSideBearing;
		public short xMaxExtent;
		public short caretSlopeRise;
		public short caretSlopeRun;
		public int numberOfHMetrics;
	}
}