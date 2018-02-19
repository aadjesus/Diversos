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
using Cx.Common;
using Cx.EventArgs;
using Cx.Exceptions;
using Cx.Interfaces;
using Cx.WinForm;

namespace Cx.Designer.Components
{
	[CxComponentName("ListView")]
	[CxExplicitEvent("ItemChecked")]
	public partial class ListView : UserControl, ICxVisualComponentClass
	{
		protected EventHelper checkedEvent;
		protected List<ItemValue> headerText;
		protected List<ItemValue> headerWidths;

		[CxComponentProperty]
		public string Label
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}

		[CxComponentProperty]
		public string ShowCheckBoxes
		{
			get { return lvListView.CheckBoxes.ToString(); }
			set { lvListView.CheckBoxes = Convert.ToBoolean(value); }
		}

		[CxComponentProperty]
		public List<ItemValue> HeaderText
		{
			get { return headerText; }
			set { headerText = value; }
		}

		[CxComponentProperty]
		public List<ItemValue> HeaderWidths
		{
			get { return headerWidths; }
			set { headerWidths = value; }
		}

		//[CxComponentProperty]
		//public string CtrlAnchor
		//{
		//    get { return lvListView.Anchor.ToString(); }
		//    set
		//    {
		//        Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), value);
		//    }
		//}

		public ListView()
		{
			InitializeComponent();
			headerText = new List<ItemValue>();
			headerWidths = new List<ItemValue>();
			checkedEvent = EventHelpers.CreateEvent<CxObjectState>(this, "ItemChecked");
			lvListView.ItemChecked += new ItemCheckedEventHandler(OnItemChecked);
		}

		[CxConsumer]
		public void OnData(object sender, CxEventArgs<IEnumerable> args)
		{
			lvListView.Items.Clear();
			InitializeColumns();
			lvListView.Sorting = SortOrder.Ascending;

			foreach (object obj in args.Data)
			{
				ListViewItem lvi = new ListViewItem(obj.ToString());
				lvi.Tag = obj;
				lvListView.Items.Add(lvi);
			}
		}

		[CxConsumer]
		public void OnClearAllCheckState(object sender, System.EventArgs args)
		{
			foreach (ListViewItem lvi in lvListView.Items)
			{
				lvi.Checked = false;
			}
		}

		[CxConsumer]
		public void OnSetCheckState(object sender, CxEventArgs<object> args)
		{
			foreach (ListViewItem lvi in lvListView.Items)
			{
				if (lvi.Tag == args.Data)
				{
					lvi.Checked = true;
					break;
				}
			}
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}

		protected void InitializeColumns()
		{
			lvListView.Columns.Clear();
			bool useWidths = false;

			if (headerWidths.Count != 0)
			{
				Verify.IsTrue(headerText.Count == headerWidths.Count, "Header width item count does not match header text item count.");
				useWidths = true;
			}

			for (int i = 0; i < headerText.Count; i++)
			{
				ColumnHeader ch = new ColumnHeader();
				ch.Text = headerText[i].Text;

				if (useWidths)
				{
					ch.Width = Convert.ToInt32(headerWidths[i].Text);
				}

				lvListView.Columns.Add(ch);
			}
		}

		protected void OnItemChecked(object sender, ItemCheckedEventArgs e)
		{
			CxObjectState state = new CxObjectState(e.Item.Tag, e.Item.Checked);
			checkedEvent.Fire(state);
		}
	}
}
