using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;
using Cx.WinForm;

namespace Cx.Designer.Components
{
	[CxComponentName("CxMenu")]
	public class CxMenu : ICxVisualComponentClass
	{
		/// <summary>
		/// Currently, assumes that the menu strip is already assigned to the form.
		/// We're not processing any additional XML metadata here to create the menubar.
		/// At some point, we'll look at a more sophisticated approach to creating and managing
		/// context-specific menus.
		/// </summary>
		public void Register(object obj, ICxVisualComponent comp)
		{
			Form form=(Form)obj;

			if (form.MainMenuStrip != null)
			{
				MenuStrip ms = form.MainMenuStrip;

				foreach (ToolStripItem tsi in ms.Items)
				{
					if (tsi is ToolStripMenuItem)
					{
						ToolStripMenuItem tsmi = (ToolStripMenuItem)tsi;
						RegisterEvent(tsmi);
						RegisterChildItems(tsmi);
					}
				}
			}
		}

		protected void RegisterChildItems(ToolStripMenuItem tsmi)
		{
			foreach (ToolStripItem tsi in tsmi.DropDownItems)
			{
				if (tsi is ToolStripMenuItem)
				{
					ToolStripMenuItem tsmiChild = (ToolStripMenuItem)tsi;
					RegisterEvent(tsmiChild);
					RegisterChildItems(tsmiChild);
				}
			}
		}

		protected void RegisterEvent(ToolStripMenuItem tsmi)
		{
			EventHelpers.Transform(this, tsmi, "Click").To(tsmi.Name + "Click");
		}
	}
}
