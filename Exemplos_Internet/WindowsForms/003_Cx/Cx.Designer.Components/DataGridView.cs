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
	[CxComponentName("DataGridView")]
	[CxExplicitEvent("ItemSelected")]
	[CxExplicitEvent("Data")]
	public partial class DataGridView : UserControl, ICxVisualComponentClass
	{
		protected List<ItemValue> headerText;
		protected List<ItemValue> headerWidths;
		protected List<ItemValue> dataPropertyNames;
		protected bool editable;

		[CxComponentProperty]
		public string Label
		{
			get { return label1.Text; }
			set { label1.Text = value; }
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

		[CxComponentProperty]
		public List<ItemValue> DataPropertyNames
		{
			get { return dataPropertyNames; }
			set { dataPropertyNames = value; }
		}

		[CxComponentProperty]
		public bool Editable
		{
			get { return editable; }
			set
			{
				editable = value;

				if (editable)
				{
					EnableEdits();
				}
				else
				{
					DisableEdits();
				}
			}
		}

		protected EventHelper itemSelected;
		protected EventHelper data;

		protected BindingSource bsData;

		public DataGridView()
		{
			InitializeComponent();
			headerText = new List<ItemValue>();
			headerWidths = new List<ItemValue>();
			dataPropertyNames = new List<ItemValue>();

			Editable = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			dataGridView1.SelectionChanged += new EventHandler(OnSelectionChanged);
			itemSelected = EventHelpers.CreateEvent<object>(this, "ItemSelected");
			data = EventHelpers.CreateEvent<IEnumerable>(this, "Data");
			bsData = new BindingSource();
		}

		protected void OnSelectionChanged(object sender, System.EventArgs e)
		{
			itemSelected.Fire(bsData.Current);
		}

		protected void EnableEdits()
		{
			dataGridView1.AllowUserToAddRows = true;
			dataGridView1.AllowUserToDeleteRows = true;
			dataGridView1.RowHeadersVisible = true;

			foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
			{
				dgvc.ReadOnly = false;
			}
		}

		protected void DisableEdits()
		{
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.RowHeadersVisible = false;

			foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
			{
				dgvc.ReadOnly = false;
			}
		}

		[CxConsumer]
		public void OnData(object sender, CxEventArgs<IEnumerable> args)
		{
			bsData.DataSource = args.Data;

			if (headerText.Count == 0)
			{
				dataGridView1.AutoGenerateColumns = true;
			}
			else
			{
				dataGridView1.AutoGenerateColumns = false;
				InitializeColumns();
			}

			dataGridView1.DataSource = bsData;
			dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
		}

		/// <summary>
		/// Without data binding, we have a more complicated process for sending data to the model.
		/// </summary>
		[CxConsumer]
		public void OnRequestData(object sender, System.EventArgs args)
		{
			data.Fire(bsData.DataSource);
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}

		protected void InitializeColumns()
		{
			dataGridView1.Columns.Clear();
			bool useWidths = false;

			if (headerWidths.Count != 0)
			{
				Verify.IsTrue(headerText.Count == headerWidths.Count, "Header width item count does not match header text item count.");
				useWidths = true;
			}

			for (int i = 0; i < headerText.Count; i++)
			{
				DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
				dgvc.HeaderText = headerText[i].Text;
				dgvc.DataPropertyName = dataPropertyNames[i].Text;
				dgvc.ReadOnly = !editable;

				if (useWidths)
				{
					dgvc.Width = Convert.ToInt32(headerWidths[i].Text);
				}

				dataGridView1.Columns.Add(dgvc);
			}
		}
	}
}
