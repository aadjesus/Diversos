using System;

namespace SourceGrid2
{
	/// <summary>
	/// Selection Mode
	/// </summary>
	public enum GridSelectionMode
	{
		Cell = 1,
		Row = 2,
		Col = 3
	}

	/// <summary>
	/// Sort Mode
	/// </summary>
	public enum GridSortMode
	{
		None = 0,
		Ascending = 1,
		Descending = 2
	}

	/// <summary>
	/// ContextMenu
	/// </summary>
	[Flags]
	public enum ContextMenuStyle
	{
		None = 0,
		ColumnResize = 1,
		RowResize = 2,
		AutoSize = 4,
		ClearSelection = 8,
		CopyPasteSelection = 16,
		CellContextMenu = 32
	}


	/// <summary>
	/// Editable Cell mode
	/// </summary>
	[Flags]
	public enum EditableMode
	{
		/// <summary>
		/// No edit support
		/// </summary>
		None = 0,
		/// <summary>
		/// Edit the cell with F2 key ( 1 )
		/// </summary>
		F2Key = 1,
		/// <summary>
		/// Edit the cell with a double click (2)
		/// </summary>
		DoubleClick = 2,
		/// <summary>
		/// Edit a cell with a single Key (4)
		/// </summary>
		SingleClick = 4,
		/// <summary>
		/// Edit the cell pressing any keys (8 + F2Key)
		/// </summary>
		AnyKey = 9,
		/// <summary>
		/// Edit the cell when it receive the focus (16)
		/// </summary>
		Focus = 16,
		/// <summary>
		/// DoubleClick + F2Key
		/// </summary>
		Default = DoubleClick | F2Key | AnyKey
	}

	/// <summary>
	/// Border Style
	/// </summary>
	public enum CommonBorderStyle
	{
		Normal = 1,
		Raised = 2,
		Inset = 3
	}

	/// <summary>
	/// Type of resize of the cells
	/// </summary>
	[Flags]
	public enum CellResizeMode
	{
		None = 0,
		Height = 1,
		Width = 2,
		Both = 3
	}

	/// <summary>
	/// Flags for the export html features
	/// </summary>
	[Flags]
	public enum ExportHTMLMode
	{
		None = 0,
		HTMLAndBody = 1,
		GridBackColor = 2,
		CellBackColor = 4,
		RectangleBorder = 8,
		CellForeColor = 16,
		CellImages = 32,
		Default = (HTMLAndBody | GridBackColor | CellBackColor | RectangleBorder | CellForeColor | CellImages)
	}

	/// <summary>
	/// Special keys that the grid can handle. You can change this enum to block or allow some special keys function.
	/// </summary>
	[Flags]
	public enum GridSpecialKeys
	{
		/// <summary>
		/// No keys
		/// </summary>
		None = 0,
		/// <summary>
		/// Ctrl+C for Copy selection operation
		/// </summary>
		Ctrl_C = 1,
		/// <summary>
		/// Ctrl+V for paste selection operation
		/// </summary>
		Ctrl_V = 2,
		/// <summary>
		/// Ctrl+X for cut selection operation
		/// </summary>
		Ctrl_X = 4,
		/// <summary>
		/// Delete key, for Clear selection operation
		/// </summary>
		Delete = 8,
		/// <summary>
		/// Arrows keys, for moving focus cell operation
		/// </summary>
		Arrows = 16,
		/// <summary>
		/// Tab and Shift+Tab keys, for moving focus cell operation
		/// </summary>
		Tab = 32,
		/// <summary>
		/// PageDown and PageUp keys, for page operation
		/// </summary>
		PageDownUp = 64,
		/// <summary>
		/// Enter key, for apply editing operation
		/// </summary>
		Enter = 128,
		/// <summary>
		/// Escape key, for cancel editing operation
		/// </summary>
		Escape = 256,
		/// <summary>
		/// Default: Arrows|Ctrl_C|Ctrl_V|Ctrl_X|Delete|Tab|PageDownUp
		/// </summary>
		Default = Arrows|Ctrl_C|Ctrl_V|Ctrl_X|Delete|Tab|PageDownUp|Enter|Escape
	}


	/// <summary>
	/// Position type of the cell. Look at the .vsd diagram for details.
	/// </summary>
	public enum CellPositionType
	{
		/// <summary>
		/// Empty Cell
		/// </summary>
		Empty = 0,
		/// <summary>
		/// Fixed Top+Left Cell
		/// </summary>
		FixedTopLeft = 1,
		/// <summary>
		/// Fixed Top Cell
		/// </summary>
		FixedTop = 2,
		/// <summary>
		/// Fixed Left cell
		/// </summary>
		FixedLeft = 3,
		/// <summary>
		/// Scrollable Cell
		/// </summary>
		Scrollable = 4
	}

	public enum SelectionChangeEventType
	{
		Add = 1,
		Remove = 2,
		Clear = 3
	}


	public enum DrawCellStatus
	{
		Focus,
		Selected,
		Normal
	}

	public enum FocusStyle
	{
		None = 0,
		/// <summary>
		/// Remove the focus cell when the grid lost the focus
		/// </summary>
		RemoveFocusCellOnLeave = 1,
		/// <summary>
		/// Remove the selection when the grid lost the focus
		/// </summary>
		RemoveSelectionOnLeave = 2
	}

	[Flags]
	public enum AutoSizeMode
	{
		None = 0,
		/// <summary>
		/// Enable the AutoSize
		/// </summary>
		EnableAutoSize = 1,
		/// <summary>
		/// Enable Stretch operation
		/// </summary>
		EnableStretch = 2
	}
}
