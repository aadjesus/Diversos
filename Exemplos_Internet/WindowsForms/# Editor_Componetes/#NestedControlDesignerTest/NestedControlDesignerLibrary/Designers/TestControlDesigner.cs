// ******************************************************************
//
//	If this code works it was written by:
//		Henry Minute
//		MamSoft / Manniff Computers
//		© 2008 - 2009...
//
//	if not, I have no idea who wrote it.
//
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;

namespace NestedControlDesignerLibrary.Designers
{
	public class TestControlDesigner : ParentControlDesigner
	{
		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize(component);

			if (this.Control is TestControl)
			{
				this.EnableDesignMode(((TestControl)this.Control).WorkingArea, "WorkingArea");
			}
		}
	}
}
