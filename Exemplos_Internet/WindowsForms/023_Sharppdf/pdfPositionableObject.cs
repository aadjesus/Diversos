using System;

namespace sharpPDF
{
	/// <summary>
	/// Generic pdf object placed at x,y coordinates
	/// </summary>
	public abstract class pdfPositionableObject
	{
		/// <summary>
		/// The X coordinate of the object
		/// </summary>
		protected int _coordX;
		/// <summary>
		/// The Y coordinate of the object
		/// </summary>
		protected int _coordY;
		
		/// <summary>
		/// The X coordinate of the object
		/// </summary>
		public int coordX
		{
			get
			{
				return _coordX;
			}
			set
			{
				_coordX = value;
			}

		}

		/// <summary>
		/// The Y coordinate of the object
		/// </summary>
		public int coordY
		{
			get
			{
				return _coordY;
			}
			set
			{
				_coordY = value;
			}

		}

	}
}
