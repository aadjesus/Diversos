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
	[CxComponentName("CxComboBox")]
	[CxExplicitEvent("ItemSelected")]
	public partial class CxComboBox : UserControl, ICxVisualComponentClass
	{
		protected EventHelper itemSelected;
		[CxComponentProperty]
		public string Label
		{
			get { return lblLabel.Text; }
			set { lblLabel.Text = value; }
		}

		public CxComboBox()
		{
			InitializeComponent();
			itemSelected = EventHelpers.Transform(this, cbComboBox, "SelectedIndexChanged", "SelectedItem").To("ItemSelected");
		}

		[CxConsumer]
		public void OnData(object sender, CxEventArgs<IEnumerable> args)
		{
			cbComboBox.DataSource = args.Data;

			if (cbComboBox.SelectedItem != null)
			{
				itemSelected.Fire(cbComboBox.SelectedItem);
			}
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}
	}
}
