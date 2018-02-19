using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SampleProject.Extensions
{
	/// <summary>
	/// A grid control that support load from a datatable.
	/// </summary>
	public class GridDataTable : SourceGrid2.GridVirtual
	{
		public GridDataTable()
		{
			Selection.AutoClear = false;
			Selection.ClearCells += new EventHandler(Selection_ClearCells);
			ContextMenuStyle = SourceGrid2.ContextMenuStyle.CopyPasteSelection;
			m_MenuDelete = new MenuItem("Delete", new EventHandler(MenuDelete_Click));
			Selection.ContextMenuItems = new SourceGrid2.MenuCollection();
			Selection.ContextMenuItems.Add(m_MenuDelete);
		}

		private System.Windows.Forms.MenuItem m_MenuDelete;
		private System.Data.DataTable m_DataTable;

		private CellColumnTemplate[] m_DataCell;
		private CellHeaderDataTable m_HeaderCell;
		private CellColHeaderDataTable m_ColHeaderCell;
		private CellRowHeaderDataTable m_RowHeaderCell;

		private GridDataTableStyle m_Style = GridDataTableStyle.Default;
		private bool m_EnableEdit = true;
		private bool m_EnableDelete = true;

		private ArrayList m_DataSelection = null; //array of System.Data.DataRow

		protected override void Dispose(bool disposing)
		{
			UnLoadDataSource();

			base.Dispose (disposing);
		}


		public virtual void UnLoadDataSource()
		{
			if (m_DataTable != null)
			{
				m_DataTable.RowChanged -= new System.Data.DataRowChangeEventHandler(m_DataTable_RowChanged);
				m_DataTable.RowDeleted -= new System.Data.DataRowChangeEventHandler(m_DataTable_RowDeleted);

				m_DataTable = null;
			}
			m_Sort = null;
		}

		public virtual void LoadDataSource(System.Data.DataTable p_Table)
		{
			LoadDataSource(p_Table, GridDataTableStyle.Default, GetColumnsFromDataTable(p_Table, m_EnableEdit));
		}

		public virtual void LoadDataSource(System.Data.DataTable p_Table, 
											GridDataTableStyle p_Style,
											CellColumnTemplate[] p_DataColumns)
		{
			//unload data source
			UnLoadDataSource();

			m_DataTable = p_Table;
			m_DataTable.RowChanged += new System.Data.DataRowChangeEventHandler(m_DataTable_RowChanged);
			m_DataTable.RowDeleted += new System.Data.DataRowChangeEventHandler(m_DataTable_RowDeleted);

			m_Style = p_Style;

			if ( (p_Style & GridDataTableStyle.ColumnHeader) == GridDataTableStyle.ColumnHeader)
				FixedRows = 1;
			else
				FixedRows = 0;
			if ( (p_Style & GridDataTableStyle.RowHeader) == GridDataTableStyle.RowHeader)
				FixedColumns = 1;
			else
				FixedColumns = 0;
			Redim(m_DataTable.Rows.Count+FixedRows, p_DataColumns.Length+FixedColumns);
			Selection.SelectionMode = SourceGrid2.GridSelectionMode.Row;

			//Col Header Cell Template
			m_ColHeaderCell = new CellColHeaderDataTable(m_DataTable);
			m_ColHeaderCell.BindToGrid(this);

			//Row Header Cell Template
			m_RowHeaderCell = new CellRowHeaderDataTable();
			m_RowHeaderCell.BindToGrid(this);

			//Header Cell Template (0,0 cell)
			m_HeaderCell = new CellHeaderDataTable();
			m_HeaderCell.BindToGrid(this);

			//Data Cell Template (one for each column
			m_DataCell = p_DataColumns;
			for (int i = 0; i < m_DataCell.Length; i++)
				m_DataCell[i].BindToGrid(this);

			RefreshDataSelection();
		}

		private string m_Sort;
		protected virtual void RefreshDataSelection()
		{
			m_DataSelection = new ArrayList(m_DataTable.Select(null, m_Sort));
			RowsCount = m_DataSelection.Count + FixedRows;
		}

		public static CellColumnTemplate[] GetColumnsFromDataTable(System.Data.DataTable p_Table, bool p_EnableEdit)
		{
			CellColumnTemplate[] l_Cells;
			//Data Cell Template (one for each column
			l_Cells = new CellColumnTemplate[p_Table.Columns.Count];
			for (int c = 0; c < p_Table.Columns.Count; c++)
			{
				l_Cells[c] = new CellColumnTemplate(p_Table.Columns[c], p_Table.Columns[c].Caption);
				l_Cells[c].EnableEdit = p_EnableEdit;
			}
			return l_Cells;
		}

		public override SourceGrid2.Cells.ICellVirtual GetCell(int p_iRow, int p_iCol)
		{
			try
			{
				if (m_DataTable!=null)
				{
					if (p_iRow<FixedRows && p_iCol<FixedColumns)
						return m_HeaderCell;
					else if (p_iRow < FixedRows)
						return m_ColHeaderCell;
					else if (p_iCol < FixedColumns)
						 return m_RowHeaderCell;
					else
						return m_DataCell[p_iCol - FixedColumns];
				}
				else
					return null;
			}
			catch(Exception err)
			{
				System.Diagnostics.Debug.Assert(false, err.Message);
				return null;
			}		
		}

		public System.Data.DataTable DataTable
		{
			get{return m_DataTable;}
		}

		public GridDataTableStyle GridStyle
		{
			get{return m_Style;}
		}

		public bool EnableEdit
		{
			get{return m_EnableEdit;}
			set{m_EnableEdit = value;RefreshCellsStyle();}
		}
		public bool EnableDelete
		{
			get{return m_EnableDelete;}
			set{m_EnableDelete = value;RefreshCellsStyle();}
		}

		private void RefreshCellsStyle()
		{
			if (m_DataCell!=null)
			{
				for (int i = 0; i < m_DataCell.Length; i++)
					m_DataCell[i].EnableEdit = m_EnableEdit;
			}
			if (m_EnableDelete)
				m_MenuDelete.Enabled = true;
			else
				m_MenuDelete.Enabled = false;
		}

		public override void SetCell(int p_iRow, int p_iCol, SourceGrid2.Cells.ICellVirtual p_Cell)
		{
			throw new ApplicationException("Cannot set cell for this kind of grid");
		}


		protected override void OnSortingRangeRows(SourceGrid2.SortRangeRowsEventArgs e)
		{
			base.OnSortingRangeRows (e);

			string l_SortMode;
			if (e.Ascending)
				l_SortMode = " ASC";
			else
				l_SortMode = " DESC";

			m_Sort = m_DataTable.Columns[e.AbsoluteColKeys-FixedColumns].ColumnName + l_SortMode;

			RefreshDataSelection();
		}

		public System.Data.DataRow FocusDataRow
		{
			get
			{
				if (FocusCellPosition.IsEmpty() || FocusCellPosition.Row < FixedRows || (FocusCellPosition.Row-1) >= m_DataSelection.Count)
					return null;
				else
				{
					return (System.Data.DataRow)m_DataSelection[FocusCellPosition.Row-1];
				}
			}
		}
		public System.Data.DataColumn FocusDataColumn
		{
			get
			{
				if (FocusCellPosition.IsEmpty() || FocusCellPosition.Column < FixedColumns || (FocusCellPosition.Column-1) >= m_DataCell.Length)
					return null;
				else
				{
					return m_DataCell[FocusCellPosition.Column-1].DataColumn;
				}
			}
		}

		private void Selection_ClearCells(object sender, EventArgs e)
		{
			if ( (m_Style & GridDataTableStyle.KeyCancDeleteRow) == GridDataTableStyle.KeyCancDeleteRow &&
				m_EnableDelete)
				DeleteFocusDataRow();
		}
		private void MenuDelete_Click(object sender, EventArgs e)
		{
			if (m_EnableDelete)
				DeleteFocusDataRow();
		}
		public void DeleteFocusDataRow()
		{
			try
			{
				if (FocusDataRow!=null)
				{
					if (MessageBox.Show(this, "Are you sure to delete selected row?","Delete Row", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						FocusDataRow.Delete();
						RefreshDataSelection();
					}
				}
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this, err, "Error");
			}
		}


		public void AddDataRow()
		{
			System.Data.DataRow l_NewRow = m_DataTable.NewRow();
			m_DataTable.Rows.Add(l_NewRow);
			m_DataSelection.Add(l_NewRow);
			Rows.Insert(Rows.Count);
			Rows[Rows.Count-1].Focus();
		}

		private void m_DataTable_RowChanged(object sender, System.Data.DataRowChangeEventArgs e)
		{
			//InvalidateCells();
		}
		private void m_DataTable_RowDeleted(object sender, System.Data.DataRowChangeEventArgs e)
		{
			InvalidateCells();
		}

		#region Cell class
		public class CellColumnTemplate : SourceGrid2.Cells.Virtual.CellVirtual
		{
			private System.Data.DataColumn m_DataColumn;
			private string m_ColumnCaption;

			public CellColumnTemplate(System.Data.DataColumn p_DataColumn, string p_ColumnCaption)
			{
				m_ColumnCaption = p_ColumnCaption;
				m_DataColumn = p_DataColumn;

				DataModel = SourceGrid2.Utility.CreateDataModel(m_DataColumn.DataType); 
				DataModel.AllowNull = m_DataColumn.AllowDBNull;
				DataModel.NullDisplayString = "<NULL>";
			}

			public string ColumnCaption
			{
				get{return m_ColumnCaption;}
				set{m_ColumnCaption = value;}
			}

			public System.Data.DataColumn DataColumn
			{
				get{return m_DataColumn;}
			}

			public override bool EnableEdit
			{
				get{return base.EnableEdit & !m_DataColumn.ReadOnly;}
				set
				{
					base.EnableEdit = value & !m_DataColumn.ReadOnly;
				}
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				GridDataTable l_GridDataTable = ((GridDataTable)Grid);
				System.Data.DataRow l_Row = (System.Data.DataRow)(l_GridDataTable.m_DataSelection[p_Position.Row-Grid.FixedRows]);
				object tmp = l_Row[m_DataColumn];
				if (System.DBNull.Value == tmp)
					return null;
				else
					return tmp;
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				GridDataTable l_GridDataTable = ((GridDataTable)Grid);
				System.Data.DataRow l_Row = (System.Data.DataRow)(l_GridDataTable.m_DataSelection[p_Position.Row-Grid.FixedRows]);
				if (p_Value == null)
					l_Row[m_DataColumn] = System.DBNull.Value;
				else
					l_Row[m_DataColumn] = p_Value;

				OnValueChanged(new SourceGrid2.PositionEventArgs(p_Position, this));
			}
		}

		private class CellHeaderDataTable : SourceGrid2.Cells.Virtual.Header
		{
			public CellHeaderDataTable()
			{
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return null;
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("This cell cannot be modified");
			}		
		}

		private class CellRowHeaderDataTable : SourceGrid2.Cells.Virtual.RowHeader
		{
			public CellRowHeaderDataTable()
			{
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return p_Position.Row;
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("This cell cannot be modified");
			}	
		}

		private class CellColHeaderDataTable : SourceGrid2.Cells.Virtual.ColumnHeader
		{
			private System.Data.DataTable m_DataTable;
			public CellColHeaderDataTable(System.Data.DataTable p_DataTable)
			{
				m_DataTable = p_DataTable;
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return GetColCaption(p_Position.Column);
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("This cell cannot be modified");
			}

			private string m_ColumnSort = null;
			private bool m_bAscending = false;

			private string GetColCaption(int p_Column)
			{
				return ((GridDataTable)Grid).m_DataCell[p_Column-Grid.FixedColumns].ColumnCaption;
			}

			public override SourceGrid2.SortStatus GetSortStatus(SourceGrid2.Position p_Position)
			{
				if (GetColCaption(p_Position.Column) == m_ColumnSort)
				{
					if (m_bAscending)
						return new SourceGrid2.SortStatus (SourceGrid2.GridSortMode.Ascending, true);
					else
						return new SourceGrid2.SortStatus (SourceGrid2.GridSortMode.Descending, true);
				}
				else
					return new SourceGrid2.SortStatus (SourceGrid2.GridSortMode.None, true);
			}

			public override void SetSortMode(SourceGrid2.Position p_Position, SourceGrid2.GridSortMode p_Mode)
			{
				if (p_Mode == SourceGrid2.GridSortMode.Ascending)
				{
					m_ColumnSort = GetColCaption(p_Position.Column);
					m_bAscending = true;
				}
				else if (p_Mode == SourceGrid2.GridSortMode.Descending)
				{
					m_ColumnSort = GetColCaption(p_Position.Column);
					m_bAscending = false;
				}
				else
					m_ColumnSort = null;
			}
		}
		#endregion

		public enum GridDataTableStyle
		{
			None = 0,
			KeyCancDeleteRow = 4,
			RowHeader = 16,
			ColumnHeader = 32,
			Default = KeyCancDeleteRow|RowHeader|ColumnHeader
		}
	}
}

