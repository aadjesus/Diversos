using System;

namespace DividerPanel
{
	/// <summary>
	/// Summary description for DividerPanelDesigner.
	/// </summary>
	public class DividerPanelDesigner : System.Windows.Forms.Design.ScrollableControlDesigner
	{
		public DividerPanelDesigner()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			properties.Remove("BorderStyle");
		}

	}
}
