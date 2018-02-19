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

namespace NestedControlDesignerLibrary
{
	/// <summary>
	/// A test Control to demonstrate allowing nested Controls
	/// to accept child controls at design time.
	/// </summary>
	[
	Designer(typeof(NestedControlDesignerLibrary.Designers.TestControlDesigner))
	]
	public partial class TestControl : UserControl
	{
		public TestControl()
		{
			InitializeComponent();
		}

		#region TestControl PROPERTIES ..........................
		/// <summary>
		/// Surface the Caption to allow the user to 
		/// change it
		/// </summary>
		[
		Category("Appearance"),
		DefaultValue(typeof(String), "Test Control")
		]
		public string Caption
		{
			get
			{
				return this.lblCaption.Text;
			}

			set
			{
				this.lblCaption.Text = value;
			}
		}

		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
		]
		/// <summary>
		/// This property is essential to allowing the designer to work and
		/// the DesignerSerializationVisibility Attribute (above) is essential
		/// in allowing serialization to take place at design time.
		/// </summary>
		public Panel WorkingArea
		{
			get
			{
				return this.pnlWorkingArea;
			}
		}
		#endregion
	}
}
