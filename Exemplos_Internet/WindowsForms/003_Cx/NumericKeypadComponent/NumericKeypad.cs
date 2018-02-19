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

namespace NumericKeypadComponent
{
	
	[CxComponentName("Numeric Keypad")]
	public partial class NumericKeypad : UserControl, ICxVisualComponentClass
	{
		// In this case, since there are 11 buttons that all fire into the same handler when clicked,
		// it's better to implement our event here rather than use an event transformation.
		[CxEvent] public event CxCharDlgt KeypadEvent;

		public NumericKeypad()
		{
			InitializeComponent();
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}

		protected void KeypadClick(object sender, EventArgs e)
		{
			string padItem = (string)((Control)sender).Tag;
			RaiseKeypadEvent(padItem[0]);
		}

		protected virtual void RaiseKeypadEvent(char c)
		{
			EventHelpers.Fire(KeypadEvent, this, new CxEventArgs<char>(c));
		}
	}
}
