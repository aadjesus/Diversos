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

namespace ImprovedNestedControlDesignerLibrary.Designers
{
	public class TestControlDesigner : ParentControlDesigner
	{
		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize(component);

			if (this.Control is ImprovedTestControl)
			{
				this.EnableDesignMode(((ImprovedTestControl)this.Control).WorkingArea, "WorkingArea");
                this.EnableDesignMode(((ImprovedTestControl)this.Control).WorkingArea1, "WorkingArea1");
			}
		}

		public override bool CanParent(System.Windows.Forms.Control control)
		{
			return control is System.Windows.Forms.Control;
		}

		public override bool CanParent(System.Windows.Forms.Design.ControlDesigner controlDesigner)
		{
			if (controlDesigner != null && controlDesigner.Control is System.Windows.Forms.Control)
			{
				return true;
			}
			return false;
		}

	}
}
