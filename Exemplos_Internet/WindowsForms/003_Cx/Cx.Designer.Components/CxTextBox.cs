using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;
using Cx.WinForm;

namespace Cx.Designer.Components
{
	[CxComponentName("CxTextBox")]				
	[CxExplicitEvent("TextSet")]
	public partial class CxTextBox : UserControl, ICxVisualComponentClass
	{
		[CxComponentProperty]
		public string Label
		{
			get { return lblLabel.Text; }
			set { lblLabel.Text = value; }
		}

		public CxTextBox()
		{
			InitializeComponent();
			EventHelpers.Transform(this, tbText, "LostFocus", "Text").To("TextSet");
		}

		[CxConsumer]
		public void OnEnableState(object sender, CxEventArgs<bool> args)
		{
			Enabled = args.Data;
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}
	}
}
