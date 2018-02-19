using System;
using System.Collections;
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
	[CxComponentName("List")]
	public partial class List : UserControl, ICxVisualComponentClass
	{
		[CxComponentProperty]
		public string Label
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}

		//[CxComponentProperty]
		//public string CtrlAnchor
		//{
		//    get { return lbList.Anchor.ToString(); }
		//    set
		//    {
		//        Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), value);
		//    }
		//}

		public List()
		{
			InitializeComponent();
			EventHelpers.Transform(this, lbList, "SelectedValueChanged", "SelectedItem").To("SelectedItemChanged");
		}

		[CxConsumer]
		public void OnData(object sender, CxEventArgs<IEnumerable> args)
		{
			lbList.Items.Clear();

			foreach (object obj in args.Data)
			{
				lbList.Items.Add(obj);
			}

			// if we want to do sorting, we can't use a DataSource.
			lbList.Sorted = true;
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}
	}
}
