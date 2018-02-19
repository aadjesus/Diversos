using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Real;

namespace SampleProject.Extensions
{
	/// <summary>
	/// Summary description for DataColumnDialog.
	/// </summary>
	public class DataColumnDialog : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdUp;
		private System.Windows.Forms.Button cmdDown;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataColumnDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataColumnDialog));
			this.grid = new SourceGrid2.Grid();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdUp = new System.Windows.Forms.Button();
			this.cmdDown = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid.ContextMenuStyle = ((SourceGrid2.ContextMenuStyle)(((SourceGrid2.ContextMenuStyle.AutoSize | SourceGrid2.ContextMenuStyle.ClearSelection) 
				| SourceGrid2.ContextMenuStyle.CopyPasteSelection)));
			this.grid.CustomSort = false;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(4, 4);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(176, 208);
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = 0;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(132, 220);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(48, 220);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdUp
			// 
			this.cmdUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdUp.Image")));
			this.cmdUp.Location = new System.Drawing.Point(184, 4);
			this.cmdUp.Name = "cmdUp";
			this.cmdUp.Size = new System.Drawing.Size(24, 23);
			this.cmdUp.TabIndex = 3;
			this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
			// 
			// cmdDown
			// 
			this.cmdDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdDown.Image")));
			this.cmdDown.Location = new System.Drawing.Point(184, 32);
			this.cmdDown.Name = "cmdDown";
			this.cmdDown.Size = new System.Drawing.Size(24, 23);
			this.cmdDown.TabIndex = 4;
			this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
			// 
			// DataColumnDialog
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(212, 247);
			this.Controls.Add(this.cmdDown);
			this.Controls.Add(this.cmdUp);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.grid);
			this.Name = "DataColumnDialog";
			this.Text = "DataColumnDialog";
			this.Load += new System.EventHandler(this.DataColumnDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Data.DataTable m_DataTable;
		public System.Data.DataTable DataTable
		{
			get{return m_DataTable;}
			set{m_DataTable = value;}
		}

		private GridDataTable.CellColumnTemplate[] m_TemplateColumns;

		private void DataColumnDialog_Load(object sender, System.EventArgs e)
		{
			if (m_DataTable == null)
				throw new ApplicationException("DataTable is null");

			if (m_TemplateColumns == null)
				m_TemplateColumns = GridDataTable.GetColumnsFromDataTable(m_DataTable, true);

			grid.Redim(m_DataTable.Columns.Count+1, 3);
			grid.FixedRows = 1;
			grid.Selection.SelectionMode = SourceGrid2.GridSelectionMode.Row;

			grid[0, 0] = new Cells.Header("Visible");
			grid[0, 1] = new Cells.Header("Caption");
			grid[0, 2] = new Cells.Header("Order");

			int l_LastPosition = m_TemplateColumns.Length+1;
			for (int c = 0; c < m_DataTable.Columns.Count; c++)
			{
				int l_CurrentPosition = l_LastPosition;
				bool l_bVisible = false;
				for (int j = 0; j < m_TemplateColumns.Length; j++)
				{
					if (m_TemplateColumns[j].DataColumn == m_DataTable.Columns[c])
					{
						l_bVisible = true;
						l_CurrentPosition = j+1;
						break;
					}
				}
				if (l_bVisible==false)
					l_LastPosition++;
				grid[c+grid.FixedRows, 0] = new Cells.CheckBox(l_bVisible);
				grid[c+grid.FixedRows, 0].Tag = m_DataTable.Columns[c];
				grid[c+grid.FixedRows, 1] = new Cells.Cell(m_DataTable.Columns[c].Caption, typeof(string));
				grid[c+grid.FixedRows, 1].DataModel.AllowNull = false;
				grid[c+grid.FixedRows, 2] = new Cells.Cell(l_CurrentPosition);
			}

			Sort();

			grid.AutoStretchColumnsToFitWidth = true;
			grid.AutoSizeAll();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				int l_CountVisible = 0;
				for (int r = 1; r < grid.RowsCount; r++)
				{
					if (grid[r,0].Value.Equals(true))
						l_CountVisible++;
				}

				m_TemplateColumns = new GridDataTable.CellColumnTemplate[l_CountVisible];
				int l_IndexTemplate = 0;
				for (int r = 1; r < grid.RowsCount; r++)
				{
					if (grid[r,0].Value.Equals(true))
					{
						m_TemplateColumns[l_IndexTemplate] = new GridDataTable.CellColumnTemplate((System.Data.DataColumn)grid[r, 0].Tag, grid[r, 1].DisplayText );
						l_IndexTemplate++;
					}
				}

				DialogResult = DialogResult.OK;
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this, err, "Error");
			}
		}

		private void Sort()
		{
			grid.SortRangeRows(new SourceGrid2.Range(grid.FixedRows, 0, grid.RowsCount-1, grid.ColumnsCount-1), 2, true, null);
		}

		private void cmdUp_Click(object sender, System.EventArgs e)
		{
			int l_FocusRow = grid.FocusCellPosition.Row;
			if (grid.FocusCell!=null && l_FocusRow > grid.FixedRows)
			{
				int l_Order1 = (int)grid[l_FocusRow, 2].Value;
				int l_Order2 = (int)grid[l_FocusRow-1, 2].Value;

				grid[l_FocusRow-1, 2].Value = l_Order1;
				grid[l_FocusRow, 2].Value = l_Order2;

				Sort();

				grid[l_FocusRow-1, 0].Focus();
			}
		}

		private void cmdDown_Click(object sender, System.EventArgs e)
		{
			int l_FocusRow = grid.FocusCellPosition.Row;
			if (grid.FocusCell!=null && l_FocusRow < (grid.RowsCount-1))
			{
				int l_Order1 = (int)grid[l_FocusRow, 2].Value;
				int l_Order2 = (int)grid[l_FocusRow+1, 2].Value;

				grid[l_FocusRow+1, 2].Value = l_Order1;
				grid[l_FocusRow, 2].Value = l_Order2;

				Sort();

				grid[l_FocusRow+1, 0].Focus();
			}
		}
	
		public GridDataTable.CellColumnTemplate[] TemplateColumns
		{
			get{return m_TemplateColumns;}
			set{m_TemplateColumns = value;}
		}
	}
}
