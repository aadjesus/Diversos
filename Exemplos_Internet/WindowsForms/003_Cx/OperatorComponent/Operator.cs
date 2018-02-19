using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Attributes;
using Cx.Interfaces;
using Cx.WinForm;

namespace OperatorComponent
{
	[CxComponentName("Basic Math Operators")]
	[CxExplicitEvent("btnPlus.Click")]
	[CxExplicitEvent("btnMinus.Click")]
	[CxExplicitEvent("btnMultiply.Click")]
	[CxExplicitEvent("btnDivide.Click")]
	[CxExplicitEvent("btnEqual.Click")]
	[CxExplicitEvent("btnClear.Click")]
	public partial class Operator : UserControl, ICxVisualComponentClass
	{
		public Operator()
		{
			InitializeComponent();
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}
	}
}
