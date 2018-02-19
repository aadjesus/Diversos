namespace sharpPDF.Enumerators
{
	/// <summary>
	/// Enumerator that implements annotation styles.
	/// inserted by smeyer82 (2004)
	/// </summary>	
	public enum predefinedAnnotationStyle : short
	{
		/// <summary>
		/// None
		/// </summary>
		csNone = 0,
		/// <summary>
		/// Annotation comment
		/// </summary>
		csComment = 1,
		/// <summary>
		/// Annotation key
		/// </summary>
		csKey = 2,
		/// <summary>
		/// Annotation note
		/// </summary>
		csNote = 3,
		/// <summary>
		/// Annotation help
		/// </summary>
		csHelp = 4,
		/// <summary>
		/// Annotation new paragraph
		/// </summary>
		csNewParagraph = 5,
		/// <summary>
		/// Annotation paragraph
		/// </summary>
		csParagraph = 6,
		/// <summary>
		/// Annotation insert
		/// </summary>
		csInsert = 7
	}
}