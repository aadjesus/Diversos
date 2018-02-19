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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImprovedNestedControlDesignerLibrary
{
	[
	Designer(typeof(ImprovedNestedControlDesignerLibrary.Designers.WorkingAreaDesigner))
	]
	public partial class WorkingAreaControl : Panel
	{
		public WorkingAreaControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}
