using System;

namespace sharpPDF.Fonts.TTF
{
	/// <summary>
	/// OS/2 Table (os/2)
	/// </summary>
	internal struct OSTable 
	{			
		public short xAvgCharWidth;			
		public int usWeightClass;			
		public int usWidthClass;			
		public short fsType;			
		public short ySubscriptXSize;			
		public short ySubscriptYSize;			
		public short ySubscriptXOffset;
		public short ySubscriptYOffset;
		public short ySuperscriptXSize;
		public short ySuperscriptYSize;
		public short ySuperscriptXOffset;			
		public short ySuperscriptYOffset;
		public short yStrikeoutSize;
		public short yStrikeoutPosition;
		public short sFamilyClass;
		public byte[] panose;
		public string achVendID;
		public int fsSelection;
		public int usFirstCharIndex;
		public int usLastCharIndex;
		public short sTypoAscender;
		public short sTypoDescender;
		public short sTypoLineGap;
		public int usWinAscent;
		public int usWinDescent;
		public int ulCodePageRange1;
		public int ulCodePageRange2;
		public int sCapHeight;
	}
}