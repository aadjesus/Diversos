using System;

namespace sharpPDF.Enumerators
{
	/// <summary>
	/// Enumerator that implements the glyph type
	/// </summary>
	public enum glyphType : short
	{		
		/// <summary>
		/// Composite Glyph with two arguments
		/// </summary>
		csTwoArgs = 1,		
		/// <summary>
		/// This indicates that there is a simple scale for the glyph
		/// </summary>
		csSingleScale = 8,
		/// <summary>
		/// Indicates at least one more glyph after this one
		/// </summary>
		csMoreComponents = 32,
		/// <summary>
		/// This indicates that there is a double scale for the glyph
		/// </summary>
		csDoubleScale = 64,
		/// <summary>
		/// There is a  2 by 2 transormation that will be used to scale the component
		/// </summary>
		csTwoByTwo = 128
	}
}
