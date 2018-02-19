using System;
using System.Collections;
using System.Drawing;

using sharpPDF.Elements;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts;

namespace sharpPDF
{
	/// <summary>
	/// Class that represents a persistent page.
	/// All its objects are inherited by all document's pages.
	/// </summary>
	public class pdfPersistentPage : pdfBasePage
	{

		/// <summary>
		/// Class's constructor
		/// </summary>
		public pdfPersistentPage(pdfDocument containerDoc):base(containerDoc)
		{

		}

		/// <summary>
		/// Class's distructor
		/// </summary>
		~pdfPersistentPage()
		{			
			_containerDoc = null;
			_elements = null;
		}

	}
}
