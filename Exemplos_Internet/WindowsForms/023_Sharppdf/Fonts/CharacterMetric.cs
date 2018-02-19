using System;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Structure that represents a single character metric
	/// </summary>
	public struct CharacterMetric : IComparable
	{

		/// <summary>
		/// Character's Mapping code
		/// </summary>
		public int characterMapping;
		/// <summary>
		/// Character's width
		/// </summary>
		public int characterWidth;

		/// <summary>
		/// Method that compare character metrics on the characterMapping property
		/// </summary>
		/// <param name="obj">Object to compare</param>
		/// <returns>Boolean value that returns the result of the check</returns>
		public int CompareTo(object obj)
		{
			if (obj is CharacterMetric) {
				return characterMapping.CompareTo(((CharacterMetric)obj).characterMapping);
			} else {
				throw new ArgumentException("The object is not a CharacterMetric!");
			}
		}		
	}
}
