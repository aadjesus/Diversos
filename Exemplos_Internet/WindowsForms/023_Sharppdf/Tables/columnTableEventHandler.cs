using System;

namespace sharpPDF.Tables
{
	/// <summary>
	/// Delegate that represents a event generated when a column is added to the header or when it's changed
	/// </summary>
	/// <param name="sender">Object that generates the event</param>
	/// <param name="e">Arguments of the event</param>
	public delegate void columnTableEventHandler(object sender, columnTableEventArgs e);	

}
