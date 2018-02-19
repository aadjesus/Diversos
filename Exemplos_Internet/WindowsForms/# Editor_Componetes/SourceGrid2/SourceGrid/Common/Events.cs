using System;
using System.ComponentModel;
using System.Collections;
namespace SourceGrid2
{
	public delegate void ValidatingEventHandler(object sender, ValidatingEventArgs e);
	public delegate void ValidatingCellEventHandler(object sender, ValidatingCellEventArgs e);


	public delegate void CellEventHandler(object sender, CellEventArgs e);
//	public delegate void CellCancelEventHandler(object sender, CellCancelEventArgs e);
	
	public class ScrollPositionChangedEventArgs : EventArgs
	{
		private int m_NewValue;
		private int m_OldValue;
		public int NewValue
		{
			get{return m_NewValue;}
		}
		public int OldValue
		{
			get{return m_OldValue;}
		}
		public int Delta
		{
			get{return m_OldValue-m_NewValue;}
		}

		public ScrollPositionChangedEventArgs(int p_NewValue, int p_OldValue)
		{
			m_NewValue = p_NewValue;
			m_OldValue = p_OldValue;
		}
	}
	public delegate void ScrollPositionChangedEventHandler(object sender, ScrollPositionChangedEventArgs e);


	public class RowEventArgs : EventArgs
	{
		protected int m_Row;
		public int Row
		{
			get{return m_Row;}
			set{m_Row = value;}
		}
		public RowEventArgs(int p_Row)
		{
			m_Row = p_Row;
		}
	}
	public class ColumnEventArgs : EventArgs
	{
		protected int m_Column;
		public int Column
		{
			get{return m_Column;}
			set{m_Column = value;}
		}
		public ColumnEventArgs(int p_Column)
		{
			m_Column = p_Column;
		}
	}

	public class ValidatingEventArgs : System.ComponentModel.CancelEventArgs
	{
		private object m_NewValue;
		public ValidatingEventArgs(object p_NewValue):base(false)
		{
			m_NewValue = p_NewValue;
		}
		public object NewValue
		{
			get{return m_NewValue;}
			set{m_NewValue = value;}
		}
	}

	public class ValidatingCellEventArgs : ValidatingEventArgs
	{
		private Cells.ICellVirtual m_Cell;
		public ValidatingCellEventArgs(Cells.ICellVirtual p_Cell, object p_NewValue):base(p_NewValue)
		{
			m_Cell = p_Cell;
		}

		public Cells.ICellVirtual Cell
		{
			get{return m_Cell;}
		}
	}

	public class CellEventArgs : EventArgs
	{
		private Cells.ICellVirtual m_Cell;
		public CellEventArgs(Cells.ICellVirtual p_Cell)
		{
			m_Cell = p_Cell;
		}

		public Cells.ICellVirtual Cell
		{
			get{return m_Cell;}
			set{m_Cell = value;}
		}

	}
	
//	public class CellCancelEventArgs : CellEventArgs
//	{
//		public CellCancelEventArgs(Cell p_Cell):base(p_Cell)
//		{
//		}
//
//		private bool m_bCancel = false;
//		public bool Cancel
//		{
//			get{return m_bCancel;}
//			set{m_bCancel = value;}
//		}
//	}
//
//	public class CellArrayEventArgs : EventArgs
//	{
//		private Cell[] m_Cell;
//		public CellArrayEventArgs(Cell[] p_Cell)
//		{
//			m_Cell = p_Cell;
//		}
//
//		public Cell[] Cells
//		{
//			get{return m_Cell;}
//			set{m_Cell = value;}
//		}
//
//	}

	public class RowInfoEventArgs : EventArgs
	{
		private RowInfo m_RowInfo;
		public RowInfoEventArgs(RowInfo p_RowInfo)
		{
			m_RowInfo = p_RowInfo;
		}

		public RowInfo Row
		{
			get{return m_RowInfo;}
		}
	}

	public delegate void RowInfoEventHandler(object sender, RowInfoEventArgs e);

	public class ColumnInfoEventArgs : EventArgs
	{
		private ColumnInfo m_ColumnInfo;
		public ColumnInfoEventArgs(ColumnInfo p_ColumnInfo)
		{
			m_ColumnInfo = p_ColumnInfo;
		}

		public ColumnInfo Column
		{
			get{return m_ColumnInfo;}
		}
	}

	public delegate void ColumnInfoEventHandler(object sender, ColumnInfoEventArgs e);

	public class IndexRangeEventArgs : EventArgs
	{
		private int m_iStartIndex;
		private int m_iCount;

		public IndexRangeEventArgs(int p_iStartIndex, int p_iCount)
		{
			m_iStartIndex = p_iStartIndex;
			m_iCount = p_iCount;
		}

		public int StartIndex
		{
			get{return m_iStartIndex;}
		}

		public int Count
		{
			get{return m_iCount;}
		}
	}

	public delegate void IndexRangeEventHandler(object sender, IndexRangeEventArgs e);

	public class PositionEventArgs : EventArgs
	{
		private Position m_Position;
		private Cells.ICellVirtual m_Cell;
		public PositionEventArgs(Position p_Position, Cells.ICellVirtual p_Cell)
		{
			m_Position = p_Position;
			m_Cell = p_Cell;
		}

		public GridVirtual Grid
		{
			get{return m_Cell.Grid;}
		}

		public Cells.ICellVirtual Cell
		{
			get{return m_Cell;}
			set{m_Cell = value;} //this set method is used for GettingCellEvent
		}

		public Position Position
		{
			get{return m_Position;}
		}
	}
	public delegate void PositionEventHandler(object sender, PositionEventArgs e);

	public class PositionMouseEventArgs : PositionEventArgs
	{
		private System.Windows.Forms.MouseEventArgs m_MouseArgs;
		public PositionMouseEventArgs(Position p_Position, Cells.ICellVirtual p_Cell, System.Windows.Forms.MouseEventArgs p_MouseArgs):base(p_Position, p_Cell)
		{
			m_MouseArgs = p_MouseArgs;
		}

		public System.Windows.Forms.MouseEventArgs MouseEventArgs
		{
			get{return m_MouseArgs;}
			set{m_MouseArgs = value;}
		}
	}

	public delegate void PositionMouseEventHandler(object sender, PositionMouseEventArgs e);


	public class PositionContextMenuEventArgs : PositionEventArgs
	{
		private MenuCollection m_ContextMenu;
		public PositionContextMenuEventArgs(Position p_Position, Cells.ICellVirtual p_Cell, MenuCollection p_ContextMenu):base(p_Position, p_Cell)
		{
			m_ContextMenu = p_ContextMenu;
		}

		public MenuCollection ContextMenu
		{
			get{return m_ContextMenu;}
			set{m_ContextMenu = value;}
		}
	}

	public delegate void PositionContextMenuEventHandler(object sender, PositionContextMenuEventArgs e);
	

	public class PositionKeyPressEventArgs : PositionEventArgs
	{
		private System.Windows.Forms.KeyPressEventArgs m_KeyPressArgs;
		public PositionKeyPressEventArgs(Position p_Position, Cells.ICellVirtual p_Cell, System.Windows.Forms.KeyPressEventArgs p_KeyPressArge):base(p_Position, p_Cell)
		{
			m_KeyPressArgs = p_KeyPressArge;
		}

		public System.Windows.Forms.KeyPressEventArgs KeyPressEventArgs
		{
			get{return m_KeyPressArgs;}
			set{m_KeyPressArgs = value;}
		}
	}

	public delegate void PositionKeyPressEventHandler(object sender, PositionKeyPressEventArgs e);

	public class PositionKeyEventArgs : PositionEventArgs
	{
		private System.Windows.Forms.KeyEventArgs m_KeyArgs;
		public PositionKeyEventArgs(Position p_Position, Cells.ICellVirtual p_Cell, System.Windows.Forms.KeyEventArgs p_KeyArge):base(p_Position, p_Cell)
		{
			m_KeyArgs = p_KeyArge;
		}

		public System.Windows.Forms.KeyEventArgs KeyEventArgs
		{
			get{return m_KeyArgs;}
			set{m_KeyArgs = value;}
		}
	}

	public delegate void PositionKeyEventHandler(object sender, PositionKeyEventArgs e);


	public class PositionCancelEventArgs : PositionEventArgs
	{
		public PositionCancelEventArgs(Position p_Position, Cells.ICellVirtual p_Cell):base(p_Position, p_Cell)
		{
		}

		private bool m_bCancel = false;
		public bool Cancel
		{
			get{return m_bCancel;}
			set{m_bCancel = value;}
		}
	}

	public delegate void PositionCancelEventHandler(object sender, PositionCancelEventArgs e);


	public class RangeEventArgs : EventArgs
	{
		private Range m_GridRange;
		public RangeEventArgs(Range p_GridRange)
		{
			m_GridRange = p_GridRange;
		}

		public Range Range
		{
			get{return m_GridRange;}
		}
	}

	public delegate void RangeEventHandler(object sender, RangeEventArgs e);

	public class SelectionChangeEventArgs : EventArgs
	{
		public SelectionChangeEventArgs(SelectionChangeEventType p_Type, Range p_Range)
		{
			m_Type = p_Type;
			m_Range = p_Range;
		}

		private Range m_Range;
		private SelectionChangeEventType m_Type;

		public Range Range
		{
			get{return m_Range;}
		}

		public SelectionChangeEventType EventType
		{
			get{return m_Type;}
		}
	}

	public delegate void SelectionChangeEventHandler(object sender, SelectionChangeEventArgs e);

	public class EditExceptionEventArgs : EventArgs
	{
		public EditExceptionEventArgs(Exception p_Exception)
		{
			m_Exception = p_Exception;
		}

		private Exception m_Exception;

		public Exception Exception
		{
			get{return m_Exception;}
		}
	}

	public delegate void EditExceptionEventHandler(object sender, EditExceptionEventArgs e);

	public class SortRangeRowsEventArgs : EventArgs
	{
		private Range m_Range;
		private int m_AbsoluteColKeys;
		private bool m_bAscending;
		private IComparer m_CellComparer;

		public SortRangeRowsEventArgs(Range p_Range,
			int p_AbsoluteColKeys, 
			bool p_bAscending,
			IComparer p_CellComparer)
		{
			m_Range = p_Range;
			m_AbsoluteColKeys = p_AbsoluteColKeys;
			m_bAscending = p_bAscending;
			m_CellComparer = p_CellComparer;
		}

		public Range Range
		{
			get{return m_Range;}
		}
		public int AbsoluteColKeys
		{
			get{return m_AbsoluteColKeys;}
		}
		public bool Ascending
		{
			get{return m_bAscending;}
		}
		public IComparer CellComparer
		{
			get{return m_CellComparer;}
		}
	}

	public delegate void SortRangeRowsEventHandler(object sender, SortRangeRowsEventArgs e);
}
