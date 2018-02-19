using System;
using System.Collections;

using sharpPDF.Collections;

namespace sharpPDF.PDFControls
{
	/// <summary>
	/// Base class for all PDF Controls.
	/// </summary>
	public abstract class pdfControl : pdfPositionableObject, ISeparable
	{

		/// <summary>
		/// Height of the control
		/// </summary>
		protected int _height;
        /// <summary>
		/// Width of the control
		/// </summary>
		protected int _width;
		
		/// <summary>
		/// Height of the control
		/// </summary>
		public int Height
		{
			get
			{
				return _height;
			}
			set
			{
				_height = value;
			}
		}

		/// <summary>
		/// Width of the control
		/// </summary>
		public int Width
		{
			get
			{
				return _width;
			}
			set
			{
				_width = value;
			}
		}

		/// <summary>
		/// Control's Container
		/// </summary>
		protected pdfDocument _containerDocument;

		/// <summary>
		/// Class's Constructor
		/// </summary>
		/// <param name="container">Container Document</param>
		public pdfControl(pdfDocument container)
		{
			this._containerDocument = container;
		}

		#region ISeparable

		/// <summary>
		/// Method that returns a collection of basics elements
		/// </summary>
		/// <returns>Collection of elements</returns>
		public abstract elementList GetBasicElements();

		#endregion

	}
}
