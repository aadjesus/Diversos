using System;

namespace sharpPDF.Fonts.TTF
{
	/// <summary>
	/// Directory Table
	/// </summary>
	internal struct DirectoryTable
	{
		public string Tag;
		public int Checksum;
		public int Offset;
		public int Length;
	}
}