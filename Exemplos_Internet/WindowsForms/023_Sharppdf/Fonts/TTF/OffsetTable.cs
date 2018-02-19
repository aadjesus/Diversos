using System;

namespace sharpPDF.Fonts.TTF
{
	/// <summary>
	/// Offset Table
	/// </summary>
	internal struct OffsetTable
	{
		public float Version;
		public int NumberOfTables;
		public int SearchRange;
		public int EntrySelector;
		public int RangeShift;
	}
}