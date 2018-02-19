using System;

namespace sharpPDF.Tables
{
	/// <summary>
	/// Arguments of a columnTableEventHandler
	/// </summary>
	public class columnTableEventArgs : EventArgs
	{
		/// <summary>
		/// Column that generates the event
		/// </summary>
		protected pdfTableColumn _column;

		/// <summary>
		/// Column that generates the event
		/// </summary>
		public pdfTableColumn Column
		{
			get
			{
				return this._column;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="column">Column that generates the event</param>
		public columnTableEventArgs(pdfTableColumn column)
		{
			this._column = column;
		}
	}
}
