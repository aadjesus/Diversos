using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SampleProject.Extensions
{
	/// <summary>
	/// A grid control that support load from a array.
	/// </summary>
	public class GridArray : SourceGrid2.GridVirtual
	{
		private System.Array m_Array;

		private GridArrayStyle m_Style = GridArrayStyle.Default;
		private bool m_EnableEdit = true;

		private SourceGrid2.Cells.Virtual.CellVirtual m_ColHeaderCell;
		private SourceGrid2.Cells.Virtual.CellVirtual m_RowHeaderCell;
		private SourceGrid2.Cells.Virtual.CellVirtual m_HeaderCell;
		private SourceGrid2.Cells.Virtual.CellVirtual m_DataCell;

		public virtual void LoadDataSource(System.Array p_Array)
		{
			LoadDataSource(p_Array, GridArrayStyle.Default, new CellArrayTemplate(p_Array), new CellColumnHeaderTemplate(), new CellRowHeaderTemplate(), new CellHeaderTemplate());
			RefreshCellsStyle();
		}

		public virtual void LoadDataSource(System.Array p_Array, 
											GridArrayStyle p_Style,
											SourceGrid2.Cells.Virtual.CellVirtual p_CellDataTemplate,
											SourceGrid2.Cells.Virtual.CellVirtual p_CellColumnHeader,
											SourceGrid2.Cells.Virtual.CellVirtual p_CellRowHeader,
											SourceGrid2.Cells.Virtual.CellVirtual p_CellHeader)
		{
			m_Style = p_Style;

			m_Array = p_Array;
			if ( (p_Style & GridArrayStyle.ColumnHeaderIndex) == GridArrayStyle.ColumnHeaderIndex)
				FixedRows = 1;
			else
				FixedRows = 0;
			if ( (p_Style & GridArrayStyle.RowHeaderIndex) == GridArrayStyle.RowHeaderIndex)
				FixedColumns = 1;
			else
				FixedColumns = 0;

			Redim(m_Array.GetLength(0) + FixedRows, m_Array.GetLength(1) + FixedColumns);

			//Col Header Cell Template
			m_ColHeaderCell = p_CellColumnHeader;
			m_ColHeaderCell.BindToGrid(this);

			//Row Header Cell Template
			m_RowHeaderCell = p_CellRowHeader;
			m_RowHeaderCell.BindToGrid(this);

			//Header Cell Template (0,0 cell)
			m_HeaderCell = p_CellHeader;
			m_HeaderCell.BindToGrid(this);

			//Data Cell Template
			m_DataCell = p_CellDataTemplate;
			m_DataCell.BindToGrid(this);
		}

		public override SourceGrid2.Cells.ICellVirtual GetCell(int p_iRow, int p_iCol)
		{
			try
			{
				if (m_Array!=null)
				{
					if (p_iRow<FixedRows && p_iCol<FixedColumns)
						return m_HeaderCell;
					else if (p_iRow < FixedRows)
						return m_ColHeaderCell;
					else if (p_iCol < FixedColumns)
						 return m_RowHeaderCell;
					else
						return m_DataCell;
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

		public System.Array Array
		{
			get{return m_Array;}
		}

		public GridArrayStyle GridStyle
		{
			get{return m_Style;}
		}

		public bool EnableEdit
		{
			get{return m_EnableEdit;}
			set{m_EnableEdit = value;RefreshCellsStyle();}
		}

		private void RefreshCellsStyle()
		{
			if (m_DataCell!=null)
			{
				m_DataCell.EnableEdit = m_EnableEdit;
			}
		}

		public override void SetCell(int p_iRow, int p_iCol, SourceGrid2.Cells.ICellVirtual p_Cell)
		{
			throw new ApplicationException("Cannot set cell for this kind of grid");
		}

		#region Cell class
		public class CellArrayTemplate : SourceGrid2.Cells.Virtual.CellVirtual
		{
			private System.Array m_Array;
			public CellArrayTemplate(System.Array p_Array)
			{
				m_Array = p_Array;
				Type l_ElementType = m_Array.GetType().GetElementType();
				DataModel = SourceGrid2.Utility.CreateDataModel(l_ElementType);
			}
			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return m_Array.GetValue(p_Position.Row-Grid.FixedRows, p_Position.Column-Grid.FixedColumns);
			}
			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				m_Array.SetValue(p_Value, p_Position.Row-Grid.FixedRows, p_Position.Column-Grid.FixedColumns);
				OnValueChanged(new SourceGrid2.PositionEventArgs(p_Position, this));
			}
		}

		private class CellColumnHeaderTemplate : SourceGrid2.Cells.Virtual.ColumnHeader
		{
			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return p_Position.Column-Grid.FixedColumns;
			}
			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("Cannot change this kind of cell");
			}

			public override SourceGrid2.SortStatus GetSortStatus(SourceGrid2.Position p_Position)
			{
				return new SourceGrid2.SortStatus (SourceGrid2.GridSortMode.None, false);
			}

			public override void SetSortMode(SourceGrid2.Position p_Position, SourceGrid2.GridSortMode p_Mode)
			{
			}		
		}

		private class CellRowHeaderTemplate : SourceGrid2.Cells.Virtual.RowHeader
		{	
			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return p_Position.Row-Grid.FixedRows;
			}
			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("Cannot change this kind of cell");
			}		
		}

		private class CellHeaderTemplate : SourceGrid2.Cells.Virtual.Header
		{
			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return null;
			}
			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("Cannot change this kind of cell");
			}		
		}
		#endregion
		public enum GridArrayStyle
		{
			None = 0,
			RowHeaderIndex = 1,
			ColumnHeaderIndex = 2,
			Default = RowHeaderIndex|ColumnHeaderIndex
		}
	}
}

