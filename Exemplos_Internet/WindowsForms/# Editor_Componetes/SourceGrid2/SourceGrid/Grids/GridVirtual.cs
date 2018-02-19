using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SourceGrid2.Cells;

namespace SourceGrid2
{
	/// <summary>
	/// A abstract Grid control to support large virtual data. You must override GetCell and SetCell methods.
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public class GridVirtual : CustomScrollControl
	{
		#region Constructor
		/// <summary>
		/// Grid constructor
		/// </summary>
		public GridVirtual()
		{
			m_TopPanel = new GridSubPanel(this, true);
			m_TopPanel.TabStop = false;
			m_LeftPanel = new GridSubPanel(this, true);
			m_LeftPanel.TabStop = false;
			m_TopLeftPanel = new GridSubPanel(this, true);
			m_TopLeftPanel.TabStop = false;
			m_ScrollablePanel = new GridSubPanel(this, true);
			m_ScrollablePanel.TabStop = false;
			m_HiddenFocusPanel = new GridSubPanel(this, false);
			m_HiddenFocusPanel.TabStop = true; //questo è l'unico pannello a poter ricevere il tab

			m_Rows = new RowInfo.RowInfoCollection(this);
			m_Rows.RowHeightChanged += new RowInfoEventHandler(m_Rows_RowHeightChanged);
			m_Rows.RowsAdded += new IndexRangeEventHandler(m_Rows_RowsAdded);
			m_Columns = new ColumnInfo.ColumnInfoCollection(this);
			m_Columns.ColumnWidthChanged += new ColumnInfoEventHandler(m_Columns_ColumnWidthChanged);
			m_Columns.ColumnsAdded += new IndexRangeEventHandler(m_Columns_ColumnsAdded);

			m_Rows.RowsRemoved += new IndexRangeEventHandler(Rows_RowsRemoved);
			m_Columns.ColumnsRemoved += new IndexRangeEventHandler(Columns_ColumnsRemoved);

			SuspendLayoutGrid();

			Controls.Add(m_HiddenFocusPanel);
			m_HiddenFocusPanel.Location = new Point(0,0);
			m_HiddenFocusPanel.Size = new Size(2,2);

			m_TopLeftPanel.Location = new Point(0,0);

			Controls.Add(m_ScrollablePanel);
			Controls.Add(m_TopLeftPanel);
			Controls.Add(m_TopPanel);
			Controls.Add(m_LeftPanel);

			//hide this panel
			m_HiddenFocusPanel.SendToBack();
			m_HiddenFocusPanel.TabIndex = 0;

			Size = new System.Drawing.Size(200, 200);

			m_Selection = new Selection(this);

			ContextMenuStyle = 0;

			ResumeLayoutGrid();
		}

		#endregion

		#region Menu

		/// <summary>
		/// Create the standard contextmenu based on the current selection, current focuscell and current grid settings
		/// </summary>
		/// <returns></returns>
		public MenuCollection GetGridContextMenus()
		{
			MenuCollection l_BuiltInMenu = new MenuCollection();

			//se nel context menu sono presenti già dei menu aggiungo un separatore
			if (ContextMenu.MenuItems.Count>0)
			{
				MenuItem l_menuBreak = new MenuItem("-");
				l_BuiltInMenu.Add(l_menuBreak);
			}

			//selection context menu
			if (m_Selection.Count > 0)
			{
				MenuCollection l_SelectionMenus = m_Selection.GetContextMenus();
				foreach(MenuItem m in l_SelectionMenus)
					l_BuiltInMenu.Add(m);
			}

			//focus cell context menu
			Position l_ContextMenuCell = m_MouseDownPosition;//prima guardo se esiste una cella che ha ricevuto il mousedown (in questo modo gestisco anche le celle Selectable==false)
			if (l_ContextMenuCell.IsEmpty()) //altrimenti uso la cella con il focus
				l_ContextMenuCell = m_FocusPosition;
			if (l_ContextMenuCell.IsEmpty() == false &&
				(ContextMenuStyle & ContextMenuStyle.CellContextMenu) == ContextMenuStyle.CellContextMenu)
			{
				ICellVirtual l_tmp = GetCell(l_ContextMenuCell.Row, l_ContextMenuCell.Column);
				l_tmp.OnContextMenuPopUp(new PositionContextMenuEventArgs(l_ContextMenuCell, l_tmp, l_BuiltInMenu));
			}

			bool l_bAllowChangeCellHeight = false;
			if ( (ContextMenuStyle & ContextMenuStyle.RowResize) == ContextMenuStyle.RowResize)
				l_bAllowChangeCellHeight = true;

			bool l_bAllowChangeCellWidth = false;
			if ( (ContextMenuStyle & ContextMenuStyle.ColumnResize) == ContextMenuStyle.ColumnResize)
				l_bAllowChangeCellWidth = true;

			bool l_bAllowAutoSize = false;
			if ( (ContextMenuStyle & ContextMenuStyle.AutoSize) == ContextMenuStyle.AutoSize)
				l_bAllowAutoSize = true;
			
			//context menu for setting height and width
			if ((l_bAllowChangeCellHeight == true || l_bAllowChangeCellWidth == true)
				&& (RowsCount > 0 || ColumnsCount > 0) )
			{
				if (l_BuiltInMenu.Count>0)
				{
					MenuItem l_menuBreak = new MenuItem("-");
					l_BuiltInMenu.Add(l_menuBreak);
				}

				if (l_bAllowChangeCellHeight)
				{
					l_BuiltInMenu.Add(new MenuItem("Column Width ...",new EventHandler(Menu_ColumnWidth)));
				}
				if (l_bAllowChangeCellHeight)
				{
					l_BuiltInMenu.Add(new MenuItem("Row Height ...",new EventHandler(Menu_RowHeight)));
				}

				if (l_BuiltInMenu.Count>0)
				{
					MenuItem l_menuBreak2 = new MenuItem("-");
					l_BuiltInMenu.Add(l_menuBreak2);
				}
				if (l_bAllowChangeCellHeight && m_FocusPosition.IsEmpty() == false && l_bAllowAutoSize)
				{
					l_BuiltInMenu.Add(new MenuItem("AutoSize Column Width ...",new EventHandler(Menu_AutoSizeColumnWidth)));
				}
				if (l_bAllowChangeCellHeight && m_FocusPosition.IsEmpty() == false && l_bAllowAutoSize)
				{
					l_BuiltInMenu.Add(new MenuItem("AutoSize Row Height ...",new EventHandler(Menu_AutoSizeRowHeight)));
				}
				if (l_bAllowAutoSize)
				{
					l_BuiltInMenu.Add(new MenuItem("AutoSize All ...",new EventHandler(Menu_AutoSizeAll)));
				}
			}

			return l_BuiltInMenu;
		}


		private void Menu_AutoSizeColumnWidth(object sender, EventArgs e)
		{
			if (m_FocusPosition.IsEmpty() == false)
				AutoSizeColumn(m_FocusPosition.Column,m_AutoSizeMinWidth);
		}
		private void Menu_AutoSizeRowHeight(object sender, EventArgs e)
		{
			if (m_FocusPosition.IsEmpty() == false)
				AutoSizeRow(m_FocusPosition.Row,m_AutoSizeMinHeight);
		}
		private void Menu_AutoSizeAll(object sender, EventArgs e)
		{
			AutoSizeAll(m_AutoSizeMinHeight,m_AutoSizeMinWidth);
		}

		private void Menu_ColumnWidth(object sender, EventArgs e)
		{
			if (m_FocusPosition.IsEmpty() == false)
				ShowColumnWidthSettings(m_FocusPosition.Column);
			else
				ShowColumnWidthSettings(0);
		}
		private void Menu_RowHeight(object sender, EventArgs e)
		{
			if (m_FocusPosition.IsEmpty() == false)
				ShowRowHeightSettings(m_FocusPosition.Row);
			else
				ShowRowHeightSettings(0);
		}


		private ContextMenuStyle m_ContextMenuStyle = 0; //qui la variabile viene messa a None e poi nel costruttore viene reimposta usando la property che in più aggancia il contextmenu all'evento popup

		/// <summary>
		/// Context Menu flags enum ( default = ContextMenuStyle.AllowAutoSize | ContextMenuStyle.AllowColumnResize | ContextMenuStyle.AllowRowResize ).
		/// </summary>
		public ContextMenuStyle ContextMenuStyle
		{
			get{return m_ContextMenuStyle;}
			set
			{
				m_ContextMenuStyle = value;
				if (m_ContextMenuStyle != 0 && !(base.ContextMenu is GridContextMenu))
					ContextMenu = new GridContextMenu(this);
			}
		}

		/// <summary>
		/// Gets or sets the shortcut menu associated with the control.
		/// </summary>
		public override ContextMenu ContextMenu
		{
			get{return base.ContextMenu;}
			set
			{
				base.ContextMenu = value;
				if (!(base.ContextMenu is GridContextMenu) && ContextMenuStyle != 0)
					ContextMenuStyle = 0;
			}
		}
		#endregion

		#region AutoSize
		private int m_AutoSizeMinHeight = 10;
		/// <summary>
		/// Indicates the minimun height when autosize row
		/// </summary>
		public int AutoSizeMinHeight
		{
			get{return m_AutoSizeMinHeight;}
			set{m_AutoSizeMinHeight = value;}
		}
		private int m_AutoSizeMinWidth = 10;
		/// <summary>
		/// Indicates the minimun when autosize col
		/// </summary>
		public int AutoSizeMinWidth
		{
			get{return m_AutoSizeMinWidth;}
			set{m_AutoSizeMinWidth = value;}
		}

		/// <summary>
		/// Auto size the specified column with the max required width of each cell
		/// </summary>
		/// <param name="p_Col"></param>
		/// <param name="p_MinWidth"></param>
		public virtual void AutoSizeColumn(int p_Col, int p_MinWidth)
		{
			AutoSizeColumnRange(p_Col, p_MinWidth, 0, RowsCount-1);
		}
		/// <summary>
		/// Auto size the specified column with the max required width of each cell
		/// </summary>
		/// <param name="p_Col"></param>
		/// <param name="p_MinWidth"></param>
		/// <param name="p_StartRow"></param>
		/// <param name="p_EndRow"></param>
		public virtual void AutoSizeColumnRange(int p_Col, int p_MinWidth, int p_StartRow, int p_EndRow)
		{
            if ((Columns[p_Col].AutoSizeMode & SourceGrid2.AutoSizeMode.EnableAutoSize) == SourceGrid2.AutoSizeMode.EnableAutoSize)
			{
				int l_minWidth = p_MinWidth;
				using (Graphics l_graphics = CreateGraphics())
				{
					for (int r = p_StartRow; r <= p_EndRow; r++)
					{
						if (GetCell(r,p_Col) != null)
						{
							Size l_size = GetCell(r,p_Col).CalculateRequiredSize(new Position(r,p_Col) ,l_graphics);
							if (l_size.Width > l_minWidth)
								l_minWidth = l_size.Width;
						}
					}
				}
				Columns[p_Col].Width = l_minWidth;
			}
		}

		/// <summary>
		/// Auto size the specified row with the max required height of each cell
		/// </summary>
		/// <param name="p_Row"></param>
		/// <param name="p_MinHeight"></param>
		public virtual void AutoSizeRow(int p_Row, int p_MinHeight)
		{
			AutoSizeRowRange(p_Row, p_MinHeight, 0, ColumnsCount-1);
		}

		/// <summary>
		/// Auto size the specified row with the max required height of each cell
		/// </summary>
		/// <param name="p_Row"></param>
		/// <param name="p_MinHeight"></param>
		/// <param name="p_StartCol"></param>
		/// <param name="p_EndCol"></param>
		public virtual void AutoSizeRowRange(int p_Row, int p_MinHeight, int p_StartCol, int p_EndCol)
		{
            if ((Rows[p_Row].AutoSizeMode & SourceGrid2.AutoSizeMode.EnableAutoSize) == SourceGrid2.AutoSizeMode.EnableAutoSize)
			{
				int l_minHeight = p_MinHeight;
				using (Graphics l_graphics = CreateGraphics())
				{
					for (int c = p_StartCol; c <= p_EndCol; c++)
					{
						if (GetCell(p_Row,c) != null)
						{
							Size l_size = GetCell(p_Row,c).CalculateRequiredSize(new Position(p_Row,c),l_graphics);
							if (l_size.Height > l_minHeight)
								l_minHeight = l_size.Height;
						}
					}
				}
				Rows[p_Row].Height = l_minHeight;
			}
		}

		/// <summary>
		/// Auto size all the columns and all the rows with the required width and height
		/// </summary>
		/// <param name="p_MinHeight"></param>
		/// <param name="p_MinWidth"></param>
		public virtual void AutoSizeAll(int p_MinHeight, int p_MinWidth)
		{
			if (RowsCount > 0 && ColumnsCount > 0)
				AutoSizeRange(p_MinHeight, p_MinWidth, CompleteRange);
		}

		/// <summary>
		/// Auto size all the columns and all the rows with the required width and height
		/// </summary>
		/// <param name="p_MinHeight"></param>
		/// <param name="p_MinWidth"></param>
		/// <param name="p_RangeToAutoSize"></param>
		public virtual void AutoSizeRange(int p_MinHeight, int p_MinWidth, Range p_RangeToAutoSize)
		{
			if (p_RangeToAutoSize.IsEmpty() == false)
			{
				bool l_bOldRedraw = Redraw;
				bool l_bOldAutoCalculateTop = Rows.AutoCalculateTop;
				bool l_bOldAutoCalculateLeft = Columns.AutoCalculateLeft;
				try
				{
					Redraw = false;
					Rows.AutoCalculateTop = false;
					Columns.AutoCalculateLeft = false;

					for (int c = p_RangeToAutoSize.End.Column; c >= p_RangeToAutoSize.Start.Column ; c--)
						AutoSizeColumnRange(c,p_MinWidth, p_RangeToAutoSize.Start.Row, p_RangeToAutoSize.End.Row);
					for (int r = p_RangeToAutoSize.End.Row; r >= p_RangeToAutoSize.Start.Row ; r--)
						AutoSizeRowRange(r,p_MinHeight, p_RangeToAutoSize.Start.Column, p_RangeToAutoSize.End.Column);
				}
				finally
				{
					//aggiorno top e left
					Rows.AutoCalculateTop = l_bOldAutoCalculateTop;
					Columns.AutoCalculateLeft = l_bOldAutoCalculateLeft;
					//ridisegno
					Redraw = l_bOldRedraw;
				}

				//questo codice deve essere fatto dopo AutoCalculateTop e AutoCalculateLeft
				if (AutoStretchColumnsToFitWidth)
					StretchColumnsToFitWidth();
				if (AutoStretchRowsToFitHeight)
					StretchRowsToFitHeight();
			}
		}

		/// <summary>
		/// Auto size all the columns and all the rows with the required width and height
		/// </summary>
		public virtual void AutoSizeAll()
		{
			AutoSizeAll(AutoSizeMinHeight,AutoSizeMinWidth);
		}

		/// <summary>
		/// Auto size the columns and the rows currently visible
		/// </summary>
		/// <param name="p_UseAllColumns">If true this method AutoSize all the columns using the data in the current rows visible, otherwise autosize only visible columns</param>
		public virtual void AutoSizeView(bool p_UseAllColumns)
		{
			AutoSizeView(p_UseAllColumns, AutoSizeMinHeight, AutoSizeMinWidth);
		}
		/// <summary>
		/// Auto size the columns and the rows currently visible
		/// </summary>
		/// <param name="p_UseAllColumns">If true this method AutoSize all the columns using the data in the current rows visible, otherwise autosize only visible columns</param>
		/// <param name="p_AutoSizeMinHeight"></param>
		/// <param name="p_AutoSizeMinWidth"></param>
		public virtual void AutoSizeView(bool p_UseAllColumns, int p_AutoSizeMinHeight, int p_AutoSizeMinWidth)
		{
			Range l_Range = RangeAtAbsRect(RectangleRelativeToAbsolute(ClientRectangle));
			if (l_Range.IsEmpty() == false)
			{
				if (p_UseAllColumns)
					AutoSizeRange(p_AutoSizeMinHeight, p_AutoSizeMinWidth, new Range(l_Range.Start.Row, 0, l_Range.End.Row, ColumnsCount-1));
				else
					AutoSizeRange(p_AutoSizeMinHeight, p_AutoSizeMinWidth, l_Range);
			}
		}

		private bool m_bAutoStretchColumnsToFitWidth = false;
		/// <summary>
		/// True to auto stretch the columns width to always fit the available space, also when the contents of the cell is smaller.
		/// False to leave the original width of the columns
		/// </summary>
		public bool AutoStretchColumnsToFitWidth
		{
			get{return m_bAutoStretchColumnsToFitWidth;}
			set{m_bAutoStretchColumnsToFitWidth = value;}
		}
		private bool m_bAutoStretchRowsToFitHeight = false;
		/// <summary>
		/// True to auto stretch the rows height to always fit the available space, also when the contents of the cell is smaller.
		/// False to leave the original height of the rows
		/// </summary>
		public bool AutoStretchRowsToFitHeight
		{
			get{return m_bAutoStretchRowsToFitHeight;}
			set{m_bAutoStretchRowsToFitHeight = value;}
		}

		/// <summary>
		/// stretch the columns width to always fit the available space, also when the contents of the cell is smaller.
		/// </summary>
		public virtual void StretchColumnsToFitWidth()
		{
			//calcolo la grandezza attuale
			if (ColumnsCount>0)
			{
                
				int l_CurrentPos = Columns.Right + 4; //più 4 per non arrivare proprio a filo
				if (DisplayRectangle.Width > l_CurrentPos)
				{
					int l_Count = 0;
					for (int i = 0; i < ColumnsCount; i++)
					{
                        if ((Columns[i].AutoSizeMode & SourceGrid2.AutoSizeMode.EnableStretch) == SourceGrid2.AutoSizeMode.EnableStretch)
							l_Count++;
					}

					if (l_Count > 0)
					{
						int l_DeltaPerCol = (DisplayRectangle.Width - l_CurrentPos) / l_Count;
						for (int i = 0; i < ColumnsCount; i++)
						{

                            if ((Columns[i].AutoSizeMode & SourceGrid2.AutoSizeMode.EnableStretch) == SourceGrid2.AutoSizeMode.EnableStretch)
								Columns[i].Width += l_DeltaPerCol;
						}
					}
				}
			}
		}

		/// <summary>
		/// stretch the rows height to always fit the available space, also when the contents of the cell is smaller.
		/// </summary>
		public virtual void StretchRowsToFitHeight()
		{
			//calcolo la grandezza attuale
			if (RowsCount>0)
			{
				int l_CurrentPos = Rows.Bottom + 4; //più 4 per non arrivare proprio a filo
				if (DisplayRectangle.Height > l_CurrentPos)
				{
					int l_Count = 0;
					for (int i = 0; i < RowsCount; i++)
					{
                        if ((Rows[i].AutoSizeMode & SourceGrid2.AutoSizeMode.EnableStretch) == SourceGrid2.AutoSizeMode.EnableStretch)
							l_Count++;
					}

					if (l_Count > 0)
					{
						int l_DeltaPerRow = (DisplayRectangle.Height - l_CurrentPos) / l_Count;
						for (int i = 0; i < RowsCount; i++)
						{
                            if ((Rows[i].AutoSizeMode & SourceGrid2.AutoSizeMode.EnableStretch) == SourceGrid2.AutoSizeMode.EnableStretch)
								Rows[i].Height += l_DeltaPerRow;
						}
					}
				}
			}
		}

		/// <summary>
		/// Raises the System.Windows.Forms.Control.Resize event.  
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);

			//prima era su OnLayour ma questo evento ceniva richiamato troppo spesso, anche ad esempio quando si aggiungevano controlli ...

			if (AutoStretchColumnsToFitWidth || AutoStretchRowsToFitHeight)
			{
				bool l_bOldRedraw = Redraw;
				bool l_bOldAutoCalculateTop = Rows.AutoCalculateTop;
				bool l_bOldAutoCalculateLeft = Columns.AutoCalculateLeft;
				try
				{
					AutoSizeAll();
					if (AutoStretchColumnsToFitWidth)
						StretchColumnsToFitWidth();
					if (AutoStretchRowsToFitHeight)
						StretchRowsToFitHeight();
				}
				finally
				{
					//aggiorno top e left
					Rows.AutoCalculateTop = l_bOldAutoCalculateTop;
					Columns.AutoCalculateLeft = l_bOldAutoCalculateLeft;
					//ridisegno
					Redraw = l_bOldRedraw;
				}
			}
		}



		#endregion

		#region ColumnWidth/RowHeight form setting
		/// <summary>
		/// Display the form for customize column's width
		/// </summary>
		/// <param name="p_col"></param>
		public virtual void ShowColumnWidthSettings(int p_col)
		{
			if (ColumnsCount > 0 && p_col >= 0 && p_col < ColumnsCount)
			{
				frmCellSize l_frmCellSize = new frmCellSize();
				l_frmCellSize.LoadSetting(this,p_col,-1,CellSizeMode.Col);
				l_frmCellSize.ShowDialog();
			}
		}
		/// <summary>
		/// Dsplay the form for customize row's height
		/// </summary>
		/// <param name="p_row"></param>
		public virtual void ShowRowHeightSettings(int p_row)
		{
			if (RowsCount > 0 && p_row >= 0 && p_row < RowsCount)
			{
				frmCellSize l_frmCellSize = new frmCellSize();
				l_frmCellSize.LoadSetting(this,-1,p_row,CellSizeMode.Row);
				l_frmCellSize.ShowDialog();
			}
		}

		#endregion

		#region Redim, AddRow/Col, RemoveRow/Col

		/// <summary>
		/// Set the number of columns and rows
		/// </summary>
		public void Redim(int p_Rows, int p_Cols)
		{
			//TODO da ottimizzare ridimensionando la matrice in una sola volta
			bool l_bOldRedraw = Redraw;
			try
			{
				Redraw = false;

				RowsCount = p_Rows;
				ColumnsCount = p_Cols;
			}
			finally
			{
				Redraw = l_bOldRedraw;
			}
		}


		private void Rows_RowsRemoved(object sender, IndexRangeEventArgs e)
		{
			Range l_RemovedRange = new Range(e.StartIndex, 0, e.StartIndex+e.Count-1, ColumnsCount-1);


			if (l_RemovedRange.Contains(FocusCellPosition))
				SetFocusCell(Position.Empty);
			if (l_RemovedRange.Contains(m_MouseCellPosition))
				m_MouseCellPosition = Position.Empty;
			if (l_RemovedRange.Contains(m_MouseDownPosition))
				m_MouseDownPosition = Position.Empty;
		}

		private void Columns_ColumnsRemoved(object sender, IndexRangeEventArgs e)
		{
			Range l_RemovedRange = new Range(0, e.StartIndex, RowsCount-1, e.StartIndex+e.Count-1);


			if (l_RemovedRange.Contains(FocusCellPosition))
				SetFocusCell(Position.Empty);
			if (l_RemovedRange.Contains(m_MouseCellPosition))
				m_MouseCellPosition = Position.Empty;
			if (l_RemovedRange.Contains(m_MouseDownPosition))
				m_MouseDownPosition = Position.Empty;
		}
		#endregion

		#region Cell to Rectangle

		/// <summary>
		/// Get the rectangle of the cell respect to the client area visible, the grid DisplayRectangle.
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public Rectangle PositionToDisplayRect(Position p_Position)
		{
			return RangeToDisplayRect(new Range(p_Position));
		}

		/// <summary>
		/// Get the Rectangle of the cell respect all the scrollable area. This method cannot use Row/Col Span.
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public virtual Rectangle PositionToAbsoluteRect(Position p_Position)
		{
			return RangeToAbsoluteRect( new Range(p_Position) );
		}

		/// <summary>
		/// Returns the absolute rectangle relative to the total scrollable area of the specified Range. Returns a 0 rectangle if the Range is not valid
		/// </summary>
		/// <param name="p_Range"></param>
		/// <returns></returns>
		public virtual Rectangle RangeToAbsoluteRect(Range p_Range)
		{
			if (p_Range.IsEmpty())
				return new Rectangle(0,0,0,0);
			
			int l_Left = Columns[p_Range.Start.Column].Left;
			int l_Top = Rows[p_Range.Start.Row].Top;

			return new Rectangle(l_Left, //x 
								l_Top,  //y
								Columns[p_Range.End.Column].Right-l_Left,	 //width
								Rows[p_Range.End.Row].Bottom-l_Top);  //height
		}

		/// <summary>
		/// Returns the relative rectangle to the current scrollable area of the specified Range. Returns a 0 rectangle if the Range is not valid
		/// </summary>
		/// <param name="p_Range"></param>
		/// <returns></returns>
		public Rectangle RangeToDisplayRect(Range p_Range)
		{
			if (p_Range.IsEmpty())
				return new Rectangle(0,0,0,0);

			Rectangle l_Absolute = RangeToAbsoluteRect(p_Range);
			Rectangle l_Display = RectangleAbsoluteToRelative(l_Absolute);

			CellPositionType l_Type = GetPositionType(p_Range.Start);
			if (l_Type == CellPositionType.FixedTopLeft)
				return new Rectangle(l_Absolute.X, l_Absolute.Y, l_Absolute.Width, l_Absolute.Height);
			else if (l_Type == CellPositionType.FixedTop)
				return new Rectangle(l_Display.X, l_Absolute.Y, l_Absolute.Width, l_Absolute.Height);
			else if (l_Type == CellPositionType.FixedLeft)
				return new Rectangle(l_Absolute.X, l_Display.Y, l_Absolute.Width, l_Absolute.Height);
			else if (l_Type == CellPositionType.Scrollable)
				return l_Display;
			else
				return new Rectangle(0,0,0,0);
		}

		/// <summary>
		/// Indicates if the specified cell is visible.
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public bool IsCellVisible(Position p_Position)
		{
			Point l_ScrollPosition;
			return !(GetScrollPositionToShowCell(p_Position, out l_ScrollPosition));
		}

		/// <summary>
		/// Return the scroll position that must be set to show a specific cell.
		/// </summary>
		/// <param name="p_Position"></param>
		/// <param name="p_NewScrollPosition"></param>
		/// <returns>Return false if the cell is already visible, return true is the cell is not currently visible.</returns>
		private bool GetScrollPositionToShowCell(Position p_Position, out Point p_NewScrollPosition)
		{


			Rectangle l_cellRect = PositionToDisplayRect(p_Position);
			Point l_newCustomScrollPosition = new Point(CustomScrollPosition.X,CustomScrollPosition.Y);
			bool l_ApplyScroll = false;
			Rectangle l_ClientRectangle = DisplayRectangle;
			if (l_cellRect.Location.X < Columns[Math.Min(FixedColumns, p_Position.Column)].Left )
			{
				l_newCustomScrollPosition.X -= l_cellRect.Location.X - Columns[Math.Min(FixedColumns, p_Position.Column)].Left;
				l_ApplyScroll = true;
			}
			if (l_cellRect.Location.Y < Rows[Math.Min(FixedRows, p_Position.Row)].Top)
			{
				l_newCustomScrollPosition.Y -= l_cellRect.Location.Y - Rows[Math.Min(FixedRows, p_Position.Row)].Top;
				l_ApplyScroll = true;
			}
			if (l_cellRect.Right > l_ClientRectangle.Right)
			{
				l_newCustomScrollPosition.X -= l_cellRect.Right-l_ClientRectangle.Right;
				l_ApplyScroll = true;
			}
			if (l_cellRect.Bottom >l_ClientRectangle.Bottom)
			{
				l_newCustomScrollPosition.Y -= l_cellRect.Bottom-l_ClientRectangle.Bottom;
				l_ApplyScroll = true;
			}

			p_NewScrollPosition = l_newCustomScrollPosition;
			return l_ApplyScroll;
		}


		/// <summary>
		/// Scroll the view to show the cell passed
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns>Returns true if the Cell passed was already visible, otherwise false</returns>
		public bool ShowCell(Position p_Position)
		{
			Point l_newCustomScrollPosition;
			if (GetScrollPositionToShowCell(p_Position, out l_newCustomScrollPosition))
			{
				CustomScrollPosition = l_newCustomScrollPosition;
				//il problema di refresh si verifica solo in caso di FixedRows e ColumnsCount maggiori di 0
				if (FixedRows > 0 || FixedColumns > 0)
					InvalidateCells();

				return false;
			}
			return true;
		}


		/// <summary>
		/// Force a cell to redraw. If Redraw is set to false this function has no effects
		/// </summary>
		/// <param name="p_Position"></param>
		public virtual void InvalidateCell(Position p_Position)
		{
			if (Redraw && p_Position.IsEmpty() == false)
			{
				Rectangle l_GridRectangle = PositionToDisplayRect(p_Position);
				GridSubPanel l_Panel = PanelAtPosition(p_Position);

				l_Panel.Invalidate(l_Panel.RectangleGridToPanel(l_GridRectangle), false);
			}
		}

		/// <summary>
		/// Force a range of cells to redraw. If Redraw is set to false this function has no effects
		/// </summary>
		/// <param name="p_Range"></param>
		public void InvalidateRange(Range p_Range)
		{
			p_Range = Range.Intersect(p_Range, CompleteRange); //to ensure the range is valid
			if (Redraw && p_Range.IsEmpty() == false)
			{
				Rectangle l_GridRectangle = RangeToDisplayRect(p_Range);
				Invalidate(l_GridRectangle,true);
			}
		}
		#endregion

		#region Focus

		private event PositionCancelEventHandler m_CellGotFocus;
		/// <summary>
		/// Fired before a cell receive the focus (FocusCell is populated after this event, use e.Cell to read the cell that will receive the focus)
		/// </summary>
		public event PositionCancelEventHandler CellGotFocus
		{
			add{m_CellGotFocus += value;}
			remove{m_CellGotFocus -= value;}
		}

		private event PositionCancelEventHandler m_CellLostFocus;
		/// <summary>
		/// Fired before a cell lost the focus
		/// </summary>
		public event PositionCancelEventHandler CellLostFocus
		{
			add{m_CellLostFocus += value;}
			remove{m_CellLostFocus -= value;}
		}

		/// <summary>
		/// Fired when a cell receive the focus
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCellGotFocus(PositionCancelEventArgs e)
		{
			if (e.Cancel)
				return;

			//Evento Got Focus
			if (m_CellGotFocus != null)
				m_CellGotFocus(this,e);
			if (e.Cancel)
				return;

			e.Cancel = !(SetFocusOnCells());
			if (e.Cancel)
				return;

			//N.B. E' importante impostare prima la variabile m_FocusCell e dopo chiamare l'evento OnEnter, altrimenti nel caso in cui la cella sia impostata in edit sul focus, l'eseguzione va in loop (cerca di fare l'edit ma per far questo è necessario avere il focus ...)
			m_FocusPosition = e.Position; //logicamente è questa l'istruzione che imposta la focus cell
			e.Cell.OnFocusEntered(e);
		}

		/// <summary>
		/// Fired when a cell lost the focus
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCellLostFocus(PositionCancelEventArgs e)
		{
			if (e.Cancel)
				return;

			bool l_FocusContainer = true;
			if (ContainsFocus) // se la griglia ha il Focus cerco di impostare il focus sulle celle (il codice in realtà lo imposta su un pannello) in modo da forzare un eventuale Validated di qualche controllo figlio. Questo bisogna farlo però solo se la griglia ha il focus altrimenti sposto il focus senza motivo sulla griglia.
				l_FocusContainer = SetFocusOnCells(); //questo scatena un EndEdit sul Validate dell'editor (anche se successivamente io chiamo un endEdit forzatamente)

			if (l_FocusContainer)
			{
				//e riesco a finire l'eventuale operazione di edit
				if (e.Cell.EndEdit(false) == false)
					e.Cancel = true;
			}
			else
				e.Cancel = true;
			
			if (e.Cancel)
				return;

			//evento Lost Focus
			if (m_CellLostFocus != null)
				m_CellLostFocus(this,e);
			if (e.Cancel)
				return;

			m_FocusPosition = Position.Empty; //logicamente è questa l'istruzione che imposta la focus cell null
			e.Cell.OnFocusLeft(e);
		}

		private Position m_FocusPosition = Position.Empty;
		/// <summary>
		/// returns the active cell
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Position FocusCellPosition
		{
			get{return m_FocusPosition;}
		}

		/// <summary>
		/// Returns the row that have the focus. If no row is selected return null.
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RowInfo FocusRow
		{
			get
			{
				if (FocusCellPosition.IsEmpty() || FocusCellPosition.Row >= RowsCount)
					return null;
				return Rows[FocusCellPosition.Row];
			}
		}
		/// <summary>
		/// Returns the column that have the focus. If no column is selected return null.
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ColumnInfo FocusColumn
		{
			get
			{
				if (FocusCellPosition.IsEmpty() || FocusCellPosition.Column >= ColumnsCount)
					return null;
				return Columns[FocusCellPosition.Column];
			}
		}

		/// <summary>
		/// Change the focus of the grid. The calls order is: (the user select CellX) Grid.CellGotFocus(CellX), CellX.Enter, (the user select CellY), Grid.CellLostFocus(CellX), CellX.Leave, Grid.CellGotFocus(CellY), CellY.Enter. If Control key is not pressed deselect others cells
		/// </summary>
		/// <param name="p_CellToSetFocus">Must be a valid cell linked to the grid or null of you want to remove the focus</param>
		/// <returns>Return true if the grid can select the cell specified, otherwise false</returns>
		public bool SetFocusCell(Position p_CellToSetFocus)
		{
			//deseleziono le celle precedentemente selezionate
			bool l_bControlPress = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
			//se control non è stato premuto deseleziono tutte le celle precedentemente selezionate
			if ( l_bControlPress == false || Selection.EnableMultiSelection == false) 
				return SetFocusCell(p_CellToSetFocus,true);
			else
				return SetFocusCell(p_CellToSetFocus,false);
		}

		/// <summary>
		/// Change the focus of the grid. 
		/// The calls order is: 
		/// 
		/// (the user select CellX) 
		/// CellX.FocusEntering
		/// Grid.CellGotFocus(CellX), 
		/// CellX.FocusEntered, 
		/// 
		/// (the user select CellY), 
		/// CellY.FocusEntering 
		/// CellX.FocusLeaving
		/// Grid.CellLostFocus(CellX), 
		/// CellX.FocusLeft,
		/// Grid.CellGotFocus(CellY), 
		/// CellY.FocusEntered
		/// </summary>
		/// <param name="p_CellToSetFocus">Must be a valid cell linked to the grid or null of you want to remove the focus</param>
		/// <param name="p_DeselectOtherCells">True to deselect others selected cells</param>
		/// <returns>Return true if the grid can select the cell specified, otherwise false</returns>
		public virtual bool SetFocusCell(Position p_CellToSetFocus, bool p_DeselectOtherCells)
		{
			if (p_CellToSetFocus != FocusCellPosition)
			{
				//N.B. E' importante chiamare prima Entering in modo tale da poter controllare il cancel e nel caso è impostato a true non viene scatenato un LostFocus sulla cella precedente

				//New Focus Cell Entering
				ICellVirtual l_CellToFocus = GetCell(p_CellToSetFocus);
				PositionCancelEventArgs l_NewEventArg = new PositionCancelEventArgs(p_CellToSetFocus, l_CellToFocus);
				if (p_CellToSetFocus.IsEmpty() == false && 
					l_CellToFocus != null )
				{
					l_CellToFocus.OnFocusEntering(l_NewEventArg);
					if (l_NewEventArg.Cancel)
						return false;
				}

				//se la cella non può ricevere il focus non posso continuare
				if (l_CellToFocus != null && l_CellToFocus.CanReceiveFocus==false)
					return false;

				//Old Focus Cell Leaving and Left
				if (FocusCellPosition.IsEmpty() == false && GetCell(FocusCellPosition) != null)
				{
					PositionCancelEventArgs l_OldEventArg = new PositionCancelEventArgs(FocusCellPosition, GetCell(FocusCellPosition));

					l_OldEventArg.Cell.OnFocusLeaving(l_OldEventArg);
					if (l_OldEventArg.Cancel)
						return false;

					OnCellLostFocus(l_OldEventArg);
					if (l_OldEventArg.Cancel)
						return false;
				}

				//Deselect Old Cells
				if (p_DeselectOtherCells)
					Selection.Clear();

				//New Focus Cell Entered
				if (p_CellToSetFocus.IsEmpty() == false && 
					l_CellToFocus != null )
				{
					OnCellGotFocus(l_NewEventArg);

					return (!l_NewEventArg.Cancel);
				}
				else
					return true;
			}
			else
			{
				//put anyway the focus on the cells
				if (p_CellToSetFocus.IsEmpty() == false)
					SetFocusOnCells();

				return true;
			}
		}


		/// <summary>
		/// Get the real position for the specified position. For example when p_Position is a merged cell this method returns the starting position of the merged cells.
		/// Usually this method returns the same cell specified as parameter. This method is used for processing arrow keys, to find a valid cell when the focus is in a merged cell.
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public virtual Position GetStartingPosition(Position p_Position)
		{
			return p_Position;
		}

		/// <summary>
		/// Raises the System.Windows.Forms.Control.Leave event.  
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave (e);

			if ( (m_FocusStyle & FocusStyle.RemoveFocusCellOnLeave) == FocusStyle.RemoveFocusCellOnLeave)
			{
				SetFocusCell(Position.Empty, false);
			}

			if ( (m_FocusStyle & FocusStyle.RemoveSelectionOnLeave) == FocusStyle.RemoveSelectionOnLeave)
			{
				Selection.Clear();
			}		
		}

		private FocusStyle m_FocusStyle = FocusStyle.None;

		/// <summary>
		/// Specify the behavior of the focus and selection. Default is FocusStyle.None.
		/// </summary>
		public FocusStyle FocusStyle
		{
			get{return m_FocusStyle;}
			set{m_FocusStyle = value;}
		}

		#endregion

		#region Selection
		/// <summary>
		/// indica l'ultima cella su cui il mouse è stato spostato 
		/// serve per la gestione dell'evento Cell.MouseLeave e MouseEnter
		/// </summary>
		private Position m_MouseCellPosition = Position.Empty;

		/// <summary>
		/// The cell position currently under the mouse cursor (row, col). If you MouseDown on a cell this cell is the MouseCellPosition until an MouseUp is fired
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Position MouseCellPosition
		{
			get{return m_MouseCellPosition;}
		}

		/// <summary>
		/// Fired when the cell under the mouse change. For internal use only.
		/// </summary>
		/// <param name="p_Cell"></param>
		protected void ChangeMouseCell(Position p_Cell)
		{
			if (m_MouseCellPosition != p_Cell)
			{
				if (m_MouseCellPosition.IsEmpty() == false &&
					m_MouseCellPosition != m_MouseDownPosition) //se la cella che sta perdento il mouse è anche quella che ha ricevuto un eventuale evento di MouseDown non scateno il MouseLeave (che invece verrà scatenato dopo il MouseUp)
				{
					ICellVirtual l_OldCell = GetCell(m_MouseCellPosition.Row, m_MouseCellPosition.Column);
					if (l_OldCell!=null)
						l_OldCell.OnMouseLeave(new PositionEventArgs(m_MouseCellPosition, l_OldCell));
				}

				m_MouseCellPosition = p_Cell;
				if (m_MouseCellPosition.IsEmpty() == false)
				{
					ICellVirtual l_NewCell = GetCell(m_MouseCellPosition.Row, m_MouseCellPosition.Column);
					if (l_NewCell!=null)
						l_NewCell.OnMouseEnter(new PositionEventArgs(m_MouseCellPosition, l_NewCell));
				}
			}
		}
		/// <summary>
		/// Change the cell currently under the mouse
		/// </summary>
		/// <param name="p_MouseDownCell"></param>
		/// <param name="p_MouseCell"></param>
		protected void ChangeMouseDownCell(Position p_MouseDownCell, Position p_MouseCell)
		{
			m_MouseDownPosition = p_MouseDownCell;
			ChangeMouseCell(p_MouseCell);
		}

		/// <summary>
		/// Fired when the selection eith the mouse is finished
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseSelectionFinish(RangeEventArgs e)
		{
			m_OldMouseSelectionRange = Range.Empty;
		}

		/// <summary>
		/// Returns the cells that are selected with the mouse. Range.Empty if no cells are selected
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Range MouseSelectionRange
		{
			get{return m_MouseSelectionRange;}
		}

		/// <summary>
		/// Fired when the mouse selection must be canceled
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnUndoMouseSelection(RangeEventArgs e)
		{
			Selection.RemoveRange(e.Range);
		}

		/// <summary>
		/// Fired when the mouse selection is succesfully finished
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnApplyMouseSelection(RangeEventArgs e)
		{
			Selection.AddRange(e.Range);
		}

		/// <summary>
		/// Fired when the mouse selection change
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseSelectionChange(EventArgs e)
		{
			Range l_MouseRange = MouseSelectionRange;

			OnUndoMouseSelection(new RangeEventArgs(m_OldMouseSelectionRange));

			OnApplyMouseSelection(new RangeEventArgs(l_MouseRange));

			m_OldMouseSelectionRange = l_MouseRange;
		}

		/// <summary>
		/// Fired when the mouse selection finish
		/// </summary>
		protected void MouseSelectionFinish()
		{
			if (m_MouseSelectionRange != Range.Empty)
				OnMouseSelectionFinish(new RangeEventArgs(m_OldMouseSelectionRange));

			m_MouseSelectionRange = Range.Empty;
		}

		/// <summary>
		/// Fired when the corner of the mouse selection change. For internal use only.
		/// </summary>
		/// <param name="p_Corner"></param>
		protected virtual void ChangeMouseSelectionCorner(Position p_Corner)
		{
			bool l_bChange = false;
			if (m_MouseSelectionRange.Start != m_FocusPosition)
			{
				l_bChange = true;
			}
			if (m_MouseSelectionRange.End != p_Corner)
			{
				l_bChange = true;
			}

			m_MouseSelectionRange = new Range(m_FocusPosition, p_Corner);

			if (l_bChange)
				OnMouseSelectionChange(EventArgs.Empty);
		}

		private Range m_MouseSelectionRange = Range.Empty;
		private Range m_OldMouseSelectionRange = Range.Empty;

		private Selection m_Selection;
		/// <summary>
		/// return the selected cells
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Selection Selection
		{
			get{return m_Selection;}
		}
		#endregion

		#region Mouse Properties

		/// <summary>
		/// Represents the cell that receive the mouse down event
		/// </summary>
		private Position m_MouseDownPosition = Position.Empty; 

		/// <summary>
		/// Represents the cell that have received the MouseDown event. You can use this cell for contextmenu logic. Can be null.
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Position MouseDownPosition
		{
			get{return m_MouseDownPosition;}
		}


		#endregion

		#region Special Keys

		private GridSpecialKeys m_GridSpecialKeys = GridSpecialKeys.Default;

		/// <summary>
		/// Special keys that the grid can handle. You can change this enum to block or allow some special keys function. For example to disable Ctrl+C Copy operation remove from this enum the GridSpecialKeys.Ctrl_C.
		/// </summary>
		public GridSpecialKeys SpecialKeys
		{
			get{return m_GridSpecialKeys;}
			set{m_GridSpecialKeys = value;}
		}

		/// <summary>
		/// Process Delete, Ctrl+C, Ctrl+V, Up, Down, Left, Right, Tab keys 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void ProcessSpecialGridKey(KeyEventArgs e)
		{
			bool l_enableCtrl_C,l_enableCtrl_V,l_enableCtrl_X,l_enableDelete,l_enableArrows,l_enableTab,l_enablePageDownUp;
			 l_enableCtrl_C = l_enableCtrl_V = l_enableCtrl_X = l_enableDelete = l_enableArrows = l_enableTab = l_enablePageDownUp = false;

			if ( (SpecialKeys & GridSpecialKeys.Arrows) == GridSpecialKeys.Arrows)
				l_enableArrows = true;
			if ( (SpecialKeys & GridSpecialKeys.Ctrl_C) == GridSpecialKeys.Ctrl_C)
				l_enableCtrl_C = true;
			if ( (SpecialKeys & GridSpecialKeys.Ctrl_V) == GridSpecialKeys.Ctrl_V)
				l_enableCtrl_V = true;
			if ( (SpecialKeys & GridSpecialKeys.Ctrl_X) == GridSpecialKeys.Ctrl_X)
				l_enableCtrl_X = true;
			if ( (SpecialKeys & GridSpecialKeys.Delete) == GridSpecialKeys.Delete)
				l_enableDelete = true;
			if ( (SpecialKeys & GridSpecialKeys.PageDownUp) == GridSpecialKeys.PageDownUp)
				l_enablePageDownUp = true;
			if ( (SpecialKeys & GridSpecialKeys.Tab) == GridSpecialKeys.Tab)
				l_enableTab = true;

			//Selection Keys 
			if (m_Selection.Count > 0)
			{
				if (e.KeyCode == Keys.Delete && l_enableDelete)
				{
					m_Selection.ClearValues();
				}
				else if (e.Control && e.KeyCode == Keys.C && l_enableCtrl_C)
				{
					m_Selection.OnClipboardCopy();
				}
				else if (e.Control && e.KeyCode == Keys.V  && l_enableCtrl_V)
				{
					m_Selection.OnClipboardPaste();
				}
				else if (e.Control && e.KeyCode == Keys.X  && l_enableCtrl_X)
				{
					m_Selection.OnClipboardCut();
				}
			}

			if (m_FocusPosition.IsEmpty() == false)
			{
				ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
				if (l_FocusCell!=null)
				{
					l_FocusCell.OnKeyDown( new PositionKeyEventArgs(m_FocusPosition, l_FocusCell, e) );

					#region Process ArrowKey For navigate into cells, tab and PgDown/Up
					ICellVirtual tmp = null;
					Position l_NewPosition = Position.Empty;
					if (e.KeyCode == Keys.Down && l_enableArrows)
					{
						int tmpRow = m_FocusPosition.Row;
						tmpRow++;
						while (tmp == null && tmpRow < RowsCount)
						{
							l_NewPosition = new Position(tmpRow, m_FocusPosition.Column);
							//verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
							if (GetStartingPosition(l_NewPosition) == m_FocusPosition)
								tmp = null;
							else
							{
								tmp = GetCell(l_NewPosition);
								if (tmp != null && tmp.CanReceiveFocus == false)
									tmp = null;
							}

							tmpRow++;
						}
					}
					else if (e.KeyCode == Keys.Up && l_enableArrows)
					{
						int tmpRow = m_FocusPosition.Row;
						tmpRow--;
						while (tmp == null && tmpRow >= 0)
						{
							l_NewPosition = new Position(tmpRow, m_FocusPosition.Column);
							//verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
							if (GetStartingPosition(l_NewPosition) == m_FocusPosition)
								tmp = null;
							else
							{
								tmp = GetCell(l_NewPosition);
								if (tmp != null && tmp.CanReceiveFocus == false)
									tmp = null;
							}

							tmpRow--;
						}
					}
					else if (e.KeyCode == Keys.Right && l_enableArrows)
					{
						int tmpCol = m_FocusPosition.Column;
						tmpCol++;
						while (tmp == null && tmpCol < ColumnsCount)
						{
							l_NewPosition = new Position(m_FocusPosition.Row, tmpCol);
							//verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
							if (GetStartingPosition(l_NewPosition) == m_FocusPosition)
								tmp = null;
							else
							{
								tmp = GetCell(l_NewPosition);
								if (tmp != null && tmp.CanReceiveFocus == false)
									tmp = null;
							}

							tmpCol++;
						}
					}
					else if (e.KeyCode == Keys.Left && l_enableArrows)
					{
						int tmpCol = m_FocusPosition.Column;
						tmpCol--;
						while (tmp == null && tmpCol >= 0)
						{
							l_NewPosition = new Position(m_FocusPosition.Row, tmpCol);
							//verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
							if (GetStartingPosition(l_NewPosition) == m_FocusPosition)
								tmp = null;
							else
							{
								tmp = GetCell(l_NewPosition);
								if (tmp != null && tmp.CanReceiveFocus == false)
									tmp = null;
							}
							
							tmpCol--;
						}
					}
					else if (e.KeyCode == Keys.Tab && l_enableTab)//se è premuto tab e non ho trovato nessuna cella provo a muovermi sulla riga in basso e partendo nuovamente dall'inizio ricerco una cella valida
					{
						int tmpRow = m_FocusPosition.Row;
						int tmpCol = m_FocusPosition.Column;
						//indietro
						if (e.Modifiers == Keys.Shift)
						{
							tmpCol--;
							while (tmp == null && tmpRow >= 0)
							{
								while (tmp == null && tmpCol >= 0)
								{
									l_NewPosition = new Position(tmpRow,tmpCol);
									//verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
									if (GetStartingPosition(l_NewPosition) == m_FocusPosition)
										tmp = null;
									else
									{
										tmp = GetCell(l_NewPosition);
										if (tmp != null && tmp.CanReceiveFocus == false)
											tmp = null;
									}

									tmpCol--;
								}

								tmpRow--;
								tmpCol = ColumnsCount-1;
							}					
						}
						else //avanti
						{
							tmpCol++;
							while (tmp == null && tmpRow < RowsCount)
							{
								while (tmp == null && tmpCol < ColumnsCount)
								{
									l_NewPosition = new Position(tmpRow,tmpCol);
									//verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
									if (GetStartingPosition(l_NewPosition) == m_FocusPosition)
										tmp = null;
									else
									{
										tmp = GetCell(l_NewPosition);
										if (tmp != null && tmp.CanReceiveFocus == false)
											tmp = null;
									}

									tmpCol++;
								}

								tmpRow++;
								tmpCol = 0;
							}
						}
					}
					else if ( (e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown) 
						&& l_enablePageDownUp)
					{
						Point l_FocusPoint = PositionToDisplayRect(m_FocusPosition).Location;
						l_FocusPoint.Offset(1,1); //in modo da entrare nella cella

						if (e.KeyCode == Keys.PageDown)
							CustomScrollPageDown();
						else if (e.KeyCode == Keys.PageUp)
							CustomScrollPageUp();

						l_NewPosition = PositionAtPoint(l_FocusPoint,false);
						tmp = GetCell(l_NewPosition);
						if (tmp != null && tmp.CanReceiveFocus==false)
							tmp = null;
					}

					if (tmp!=null && l_NewPosition.IsEmpty() == false)
						SetFocusCell(l_NewPosition);
					#endregion
				}
			}
		}


		#endregion

		#region Scroll
		/// <summary>
		/// Position of the scrollbars
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Point GridScrollPosition
		{
			get{return CustomScrollPosition;}
			set{CustomScrollPosition = value;}
		}

		#endregion

		#region Grid Events (Click, MouseDown, MouseMove, ...)

		//N.B. Gli argomenti degli eventi di Paint non sono convertiti rispetto alle coordinate relative della griglia
		// mentre gli argomenti degli altri eventi (ad esempio MouseDown, MouseMove, ...) sono convertiti rispetto alle coordinate della GridContainer nei vari pannelli GridSubPanel)

		#region Paint Events
		/// <summary>
		/// Fired when draw Left Panel
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnTopLeftPanelPaint(PaintEventArgs e)
		{
			PanelPaint(TopLeftPanel, e);
		}

		/// <summary>
		/// Fired when draw Left Panel
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnLeftPanelPaint(PaintEventArgs e)
		{
			PanelPaint(LeftPanel, e);
		}

		/// <summary>
		/// Fired when draw Top Panel
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnTopPanelPaint(PaintEventArgs e)
		{
			PanelPaint(TopPanel, e);
		}

		/// <summary>
		/// Fired when draw scrollable panel
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnScrollablePanelPaint(PaintEventArgs e)
		{
			PanelPaint(ScrollablePanel, e);
		}


		/// <summary>
		/// Draw the specified region of cells in PaintEventArgs to the GridSubPanel specified
		/// </summary>
		/// <param name="p_Panel"></param>
		/// <param name="e"></param>
		protected virtual void PanelPaint(GridSubPanel p_Panel, PaintEventArgs e)
		{
			if (!Redraw)
				return;

			//Draw BackColor (I manually need to draw back color because a use Opaque ControlStyles)
			e.Graphics.Clear(BackColor);

			//DrawCells
			Range l_Range = p_Panel.RangeAtDisplayRect(p_Panel.RectanglePanelToGrid(e.ClipRectangle));
			if (l_Range.Start.IsEmpty()==false)
				PaintRange(p_Panel, e, l_Range);
		}

		/// <summary>
		/// Draw a range of cells in the specified panel
		/// </summary>
		/// <param name="p_Panel"></param>
		/// <param name="e"></param>
		/// <param name="p_Range"></param>
		protected virtual void PaintRange(GridSubPanel p_Panel, PaintEventArgs e, Range p_Range)
		{
			Rectangle l_DrawRect;
			l_DrawRect = p_Panel.RectangleGridToPanel(PositionToDisplayRect(p_Range.Start));
			Rectangle l_AbsRect = PositionToAbsoluteRect(p_Range.Start);
			int l_DeltaX = l_AbsRect.Left - l_DrawRect.Left;
			int l_DeltaY = l_AbsRect.Top - l_DrawRect.Top;

			for (int r = p_Range.Start.Row; r <= p_Range.End.Row; r++)
			{
				int l_Top = Rows[r].Top-l_DeltaY;
				int l_Height = Rows[r].Height;

				for (int c = p_Range.Start.Column; c <= p_Range.End.Column; c++)
				{
					//Ottimizzazione per non dover chiamare ogni volta RectangleGridToPanel e CellToDisplayRect
					l_DrawRect.Location = new Point(Columns[c].Left-l_DeltaX, l_Top);
					l_DrawRect.Size = new Size(Columns[c].Width, l_Height);

					ICellVirtual l_Cell = GetCell(r,c);
					if (l_Cell!=null)
						PaintCell(p_Panel, e, l_Cell, new Position(r,c), l_DrawRect);
				}
			}
		}

		/// <summary>
		/// Draw the specified Cell
		/// </summary>
		/// <param name="p_Panel"></param>
		/// <param name="e"></param>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="p_PanelDrawRectangle"></param>
		protected virtual void PaintCell(GridSubPanel p_Panel,PaintEventArgs e, ICellVirtual p_Cell, Position p_CellPosition, Rectangle p_PanelDrawRectangle)
		{
			p_Cell.VisualModel.DrawCell(p_Cell, p_CellPosition, e, p_PanelDrawRectangle);
		}

		#endregion

		#region MouseEvents
		/// <summary>
		/// MouseDown event
		/// </summary>
		public new event MouseEventHandler MouseDown;
		/// <summary>
		/// MouseDown event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridMouseDown(MouseEventArgs e)
		{
			if (MouseDown!=null)
				MouseDown(this, e);

			//verifico che l'eventuale edit sia terminato altrimenti esco
			if (FocusCellPosition.IsEmpty() == false)
			{
				ICellVirtual l_FocusCell = GetCell(FocusCellPosition);
				if (l_FocusCell != null && l_FocusCell.IsEditing(FocusCellPosition))
				{
					if (l_FocusCell.EndEdit(false) == false)
						return;
				}
			}

			//scateno eventi di MouseDown e seleziono la cella
			Position l_Position = PositionAtPoint( new Point(e.X, e.Y) );
			if (l_Position.IsEmpty() == false)
			{
				ICellVirtual l_CellMouseDown = GetCell(l_Position);
				if (l_CellMouseDown != null)
				{
					ChangeMouseDownCell(l_Position, l_Position);

					//Cell.OnMouseDown
					PositionMouseEventArgs l_EventArgs = new PositionMouseEventArgs(l_Position, l_CellMouseDown, e);
					l_CellMouseDown.OnMouseDown(l_EventArgs);

					bool l_bShiftPress = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
				
					if (l_bShiftPress == false || FocusCellPosition.IsEmpty() )
					{//normale gestione del focus della cella
						if (Selection.Contains(m_MouseDownPosition) == false || e.Button == MouseButtons.Left) //solo se non è stata ancora selezionata
							SetFocusCell(m_MouseDownPosition);
					}
					else //gestione speciale caso shift
					{
						Selection.Clear(FocusCellPosition);
						Range l_Range = new Range(FocusCellPosition, MouseDownPosition);
						Selection.AddRange(l_Range);
					}
				}
			}
			else
				ChangeMouseDownCell(Position.Empty, Position.Empty);
		}
		/// <summary>
		/// MouseUp event
		/// </summary>
		public new event MouseEventHandler MouseUp;
		/// <summary>
		/// MouseUp event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridMouseUp(MouseEventArgs e)
		{
			if (MouseUp!=null)
				MouseUp(this, e);

			//questo è per assicurarsi che la selezione precedentemente fatta tramite mouse venga effettivamente deselezionata
			MouseSelectionFinish();

			if (m_MouseDownPosition.IsEmpty() == false)
			{
				ICellVirtual l_MouseDownCell = GetCell(m_MouseDownPosition);
				if (l_MouseDownCell!=null)
				{
					l_MouseDownCell.OnMouseUp(new PositionMouseEventArgs(m_MouseDownPosition, l_MouseDownCell, e) );
				}
				ChangeMouseDownCell(Position.Empty, PositionAtPoint(new Point(e.X, e.Y)));
			}
		}

		/// <summary>
		/// MouseMove event
		/// </summary>
		public new event MouseEventHandler MouseMove;
		/// <summary>
		/// MouseMove event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridMouseMove(MouseEventArgs e)
		{
			if (MouseMove!=null)
				MouseMove(this, e);

			Position l_PointPosition = PositionAtPoint(new Point(e.X, e.Y));
			ICellVirtual l_CellPosition = GetCell(l_PointPosition);

			//Call MouseMove on the cell that receive tha MouseDown event
			if (m_MouseDownPosition.IsEmpty() == false)
			{
				ICellVirtual l_MouseDownCell = GetCell(m_MouseDownPosition);
				if (l_MouseDownCell!=null)
				{
					l_MouseDownCell.OnMouseMove(new PositionMouseEventArgs(m_MouseDownPosition, l_MouseDownCell, e));
				}
			}
			else //se non ho nessuna cella attualmente che ha ricevuto un mousedown, l'evento di MouseMove viene segnalato sulla cella correntemente sotto il Mouse
			{
				// se non c'è nessuna cella MouseDown cambio la cella corrente sotto il Mouse
				ChangeMouseCell(l_PointPosition);//in ogni caso cambio la cella corrente
				if (l_PointPosition.IsEmpty() == false)
				{
					if (l_CellPosition != null)
					{
						// I call MouseMove on the current cell only if there aren't any cells under the mouse
						l_CellPosition.OnMouseMove(new PositionMouseEventArgs(l_PointPosition, l_CellPosition, e));
					}
				}
			}


			#region Mouse Multiselection
			if (e.Button == MouseButtons.Left && Selection.EnableMultiSelection)
			{
				Position l_SelCornerPos = l_PointPosition; // PositionAtPoint(new Point(e.X,e.Y),false);
				if (l_SelCornerPos.IsEmpty() == false)
				{
					ICellVirtual l_SelCorner = l_CellPosition; //GetCell(l_SelCornerPos);
					if (l_SelCorner!=null)
					{
						//Only if there is a FocusCell
						// and the user start the selection with a cell (m_MouseDownCell!=null)
						if (m_FocusPosition.IsEmpty() == false)
						{
							ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
							if (l_FocusCell != null && l_FocusCell.IsEditing(m_FocusPosition) ==false)
							{
								if (m_MouseDownPosition.IsEmpty() == false && Selection.Contains(m_MouseDownPosition))
								{
									ChangeMouseSelectionCorner(l_SelCornerPos);
									ShowCell(l_SelCornerPos);
								}
							}
						}
					}
				}
			}
			#endregion
		}
		/// <summary>
		/// MouseWheel event
		/// </summary>
		public new event MouseEventHandler MouseWheel;
		/// <summary>
		/// MouseWheel event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridMouseWheel(MouseEventArgs e)
		{
			if (MouseWheel!=null)
				MouseWheel(this, e);

			try
			{
				if (e.Delta >= 120 || e.Delta <= -120)
				{
					Point t = CustomScrollPosition;
					int l_NewY = t.Y + 
						(SystemInformation.MouseWheelScrollLines*6) * 
						Math.Sign(e.Delta) ;

					//check that the value is between max and min
					if (l_NewY>0)
						l_NewY = 0;
					if (l_NewY < (-base.MaximumVScroll) )
						l_NewY = -base.MaximumVScroll;

					CustomScrollPosition = new Point(t.X,l_NewY);
				}
			}
			catch(Exception)
			{
				//error
			}
		}
		/// <summary>
		/// MouseLeave event
		/// </summary>
		public new event EventHandler MouseLeave;
		/// <summary>
		/// MouseLeave event attached to a Panel
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPanelMouseLeave(EventArgs e)
		{
			if (MouseLeave!=null)
				MouseLeave(this, e);

			ChangeMouseCell(Position.Empty);

			//questo è per assicurarsi che la selezione del mouse venga effettivamente deselezionata
			MouseSelectionFinish();

			//Questo non serve perchè anche se esco dalla grigila comunque deve lanciare un eventuale MouseUp ad esempio in seguito
			//per assicurarsi che se lascio il controllo anche la cella con l'eventuale MouseDown deve essere deferenziata
			//m_MouseDownCell = null;
		}
		/// <summary>
		/// MouseEnter event
		/// </summary>
		public new event EventHandler MouseEnter;
		/// <summary>
		/// MouseEnter event attached to a Panel
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPanelMouseEnter(EventArgs e)
		{
			if (MouseEnter!=null)
				MouseEnter(this, e);
		}

		/// <summary>
		/// Mouse Hover
		/// </summary>
		public new event EventHandler MouseHover;
		/// <summary>
		/// Mouse Hover
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridMouseHover(EventArgs e)
		{
			if (MouseHover!=null)
				MouseHover(this, e);
		}

		#endregion

		#region Drag Events
		/// <summary>
		/// DragDrop event
		/// </summary>
		public new event DragEventHandler DragDrop;
		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPanelDragDrop(DragEventArgs e)
		{
			if (DragDrop!=null)
				DragDrop(this, e);
		}
		/// <summary>
		/// DragEnter event
		/// </summary>
		public new event DragEventHandler DragEnter;
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPanelDragEnter(DragEventArgs e)
		{
			if (DragEnter!=null)
				DragEnter(this, e);
		}
		/// <summary>
		/// DragLeave event
		/// </summary>
		public new event EventHandler DragLeave;
		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPanelDragLeave(EventArgs e)
		{
			if (DragLeave!=null)
				DragLeave(this, e);
		}
		/// <summary>
		/// DragOver event
		/// </summary>
		public new event DragEventHandler DragOver;
		/// <summary>
		/// DragOver event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPanelDragOver(DragEventArgs e)
		{
			if (DragOver!=null)
				DragOver(this, e);
		}
		#endregion


		#region ClickEvents
		/// <summary>
		/// Click event
		/// </summary>
		public new event EventHandler Click;
		/// <summary>
		/// Click event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridClick(EventArgs e)
		{
			if (Click!=null)
				Click(this, e);

			//Se ho precedentemente scatenato un MouseDown su una cella 
			// e se questa corrisponde alla cella sotto il puntatore del mouse (non posso usare MouseCellPosition perchè questa viene aggiornata solo quando non si ha una cella come MouseDownPosition
			if (m_MouseDownPosition.IsEmpty() == false && 
				m_MouseDownPosition == PositionAtPoint(this.PointToClient(Control.MousePosition)) /* MouseCellPosition && 
				m_MouseDownCell.Focused == true //tolto altrimenti non funzionava per le celle Selectable==false*/)
			{
				ICellVirtual l_MouseDownCell = GetCell(m_MouseDownPosition);
				if (l_MouseDownCell!=null)
				{
					l_MouseDownCell.OnClick(new PositionEventArgs(m_MouseDownPosition, l_MouseDownCell));
				}
			}		
		}
		/// <summary>
		/// DoubleClick event
		/// </summary>
		public new event EventHandler DoubleClick;
		/// <summary>
		/// Double-Click event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridDoubleClick(EventArgs e)
		{
			if (DoubleClick!=null)
				DoubleClick(this, e);

			if (m_MouseDownPosition.IsEmpty() == false)
			{
				ICellVirtual l_MouseDownCell = GetCell(m_MouseDownPosition);
				if (l_MouseDownCell!=null)
				{
					l_MouseDownCell.OnDoubleClick(new PositionEventArgs(m_MouseDownPosition, l_MouseDownCell));
				}
			}
		}
		#endregion

		#region Keys
		/// <summary>
		/// KeyDown event
		/// </summary>
		public new event KeyEventHandler KeyDown;
		/// <summary>
		/// KeyDown event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridKeyDown(KeyEventArgs e)
		{
			if (KeyDown!=null)
				KeyDown(this, e);

			ProcessSpecialGridKey(e);
		}
		/// <summary>
		/// KeyUp event
		/// </summary>
		public new event KeyEventHandler KeyUp;
		/// <summary>
		/// KeyUp event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridKeyUp(KeyEventArgs e)
		{
			if (KeyUp!=null)
				KeyUp(this, e);

			if (m_FocusPosition.IsEmpty() == false)
			{
				ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
				if (l_FocusCell!=null)
					l_FocusCell.OnKeyUp(new PositionKeyEventArgs(m_FocusPosition, l_FocusCell, e) );
			}
		}
		/// <summary>
		/// KeyPress event
		/// </summary>
		public new event KeyPressEventHandler KeyPress;
		/// <summary>
		/// KeyPress event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnGridKeyPress(KeyPressEventArgs e)
		{
			if (KeyPress!=null)
				KeyPress(this, e);

			//solo se diverso da tab e da a capo ( e non è un comando di copia/incolla)
			if (m_FocusPosition.IsEmpty() || e.KeyChar == '\t' || e.KeyChar == 13 ||
				e.KeyChar == 3 || e.KeyChar == 22 || e.KeyChar == 24)
			{
			}
			else
			{
				ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
				if (l_FocusCell!=null)
					l_FocusCell.OnKeyPress( new PositionKeyPressEventArgs(m_FocusPosition, l_FocusCell, e) );
			}
		}
		#endregion

		#endregion

		#region Export Functions
		/// <summary>
		/// Export the grid contents in html format
		/// </summary>
		/// <param name="p_Export"></param>
		public virtual void ExportHTML(IHTMLExport p_Export)
		{
			System.Xml.XmlTextWriter l_Writer = new System.Xml.XmlTextWriter(p_Export.Stream,System.Text.Encoding.UTF8);
			
			//write HTML and BODY
			if ( (p_Export.Mode & ExportHTMLMode.HTMLAndBody) == ExportHTMLMode.HTMLAndBody)
			{
				l_Writer.WriteStartElement("html");
				l_Writer.WriteStartElement("body");
			}

			l_Writer.WriteStartElement("table");

			l_Writer.WriteAttributeString("cellspacing","0");
			l_Writer.WriteAttributeString("cellpadding","0");

			for (int r = 0; r < RowsCount; r++)
			{
				l_Writer.WriteStartElement("tr");

				for (int c = 0; c < ColumnsCount; c++)
				{
					ICellVirtual l_Cell = GetCell(r,c);
					Position l_Pos = new Position(r,c);
					ExportHTMLCell(l_Pos, l_Cell, p_Export, l_Writer);
				}

				//tr
				l_Writer.WriteEndElement();
			}

			//table
			l_Writer.WriteEndElement();

			//write end HTML and BODY
			if ( (p_Export.Mode & ExportHTMLMode.HTMLAndBody) == ExportHTMLMode.HTMLAndBody)
			{
				//body
				l_Writer.WriteEndElement();
				//html
				l_Writer.WriteEndElement();
			}

			l_Writer.Flush();
		}

		/// <summary>
		/// Export the specified cell to HTML
		/// </summary>
		/// <param name="p_CurrentPosition"></param>
		/// <param name="p_Cell"></param>
		/// <param name="p_Export"></param>
		/// <param name="p_Writer"></param>
		protected virtual void ExportHTMLCell(Position p_CurrentPosition, ICellVirtual p_Cell, IHTMLExport p_Export, System.Xml.XmlTextWriter p_Writer)
		{
			if (p_Cell != null)
				p_Cell.VisualModel.ExportHTML(p_Cell, p_CurrentPosition, p_Export, p_Writer);
		}

		#endregion

		#region Controls linked
		private LinkedControlsList m_LinkedControls = new LinkedControlsList();

		/// <summary>
		/// List of controls that are linked to a specific cell position. For example is used for editors controls. Key=Control, Value=Position. The controls are automatically removed from the list when they are removed from the Grid.Controls collection
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LinkedControlsList LinkedControls
		{
			get{return m_LinkedControls;}
		}

		/// <summary>
		/// OnHScrollPositionChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnHScrollPositionChanged(ScrollPositionChangedEventArgs e)
		{
			base.OnHScrollPositionChanged (e);

			if (Redraw)
				RefreshLinkedControlsBounds();
		}

		/// <summary>
		/// OnVScrollPositionChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnVScrollPositionChanged(ScrollPositionChangedEventArgs e)
		{
			base.OnVScrollPositionChanged (e);

			if (Redraw)
				RefreshLinkedControlsBounds();
		}

		/// <summary>
		/// Refresh the linked controls bounds
		/// </summary>
		public virtual void RefreshLinkedControlsBounds()
		{
			foreach (DictionaryEntry e in m_LinkedControls)
			{
				Position l_CellPosition = (Position)e.Value;
				Control l_Control = (Control)e.Key;
				ICellVirtual l_Cell = GetCell(l_CellPosition);
				GridSubPanel l_Panel = PanelAtPosition(l_CellPosition);
				if (l_Panel==null)
					throw new SourceGridException("Invalid position, panel not found");

				if (l_Cell!=null || LinkedControls.UseCellBorder==false)
					l_Control.Bounds =  l_Cell.VisualModel.Border.RemoveBorderFromRectanlge( l_Panel.RectangleGridToPanel(PositionToDisplayRect(l_CellPosition)) );
				else
					l_Control.Bounds =  l_Panel.RectangleGridToPanel(PositionToDisplayRect(l_CellPosition));
			}
		}

		/// <summary>
		/// Fired when you remove a linked control from the grid.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved (e);

			if (LinkedControls.ContainsKey(e.Control))
				LinkedControls.Remove(e.Control);
		}

		#endregion

		#region Layout

		/// <summary>
		/// Temporarily suspends the layout logic for the control and all the children panels controls.
		/// </summary>
		public virtual void SuspendLayoutGrid()
		{
			SuspendLayout();
			m_TopLeftPanel.SuspendLayout();
			m_LeftPanel.SuspendLayout();
			m_TopPanel.SuspendLayout();
			m_ScrollablePanel.SuspendLayout();
		}

		/// <summary>
		/// Resumes normal layout logic to current control and children controls and forces an immediate layout of pending layout requests.
		/// </summary>
		public virtual void ResumeLayoutGrid()
		{
			m_ScrollablePanel.ResumeLayout();
			m_TopPanel.ResumeLayout();
			m_LeftPanel.ResumeLayout();
			m_TopLeftPanel.ResumeLayout();
			ResumeLayout(true);
			//RefreshGridLayout(); non serve perchè chiamo automaticamente un Refresh sull'evento OnLayout scatenato da resumeLayout
		}

		/// <summary>
		/// Recalculate the scrollbar position and value based on the current cells, scroll client area, linked controls and more. If redraw == false this method has not effect. This method is called when you put Redraw = true;
		/// </summary>
		protected virtual void RefreshGridLayout()
		{
			CustomScrollArea = new Size(Columns.Right, Rows.Bottom);

			CalculatePanelsLocation();

			InvalidateCells();

			//aggiorna gli eventuali linked controls
			RefreshLinkedControlsBounds();
		}

		private bool m_bRedraw = true;

		/// <summary>
		/// If false the cells are not redrawed. Set False to increase performance when adding many cells, after adding the cells remember to set this property to true. 
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Redraw
		{
			get{return m_bRedraw;}
			set
			{
				m_bRedraw = value;
				if (m_bRedraw)
				{
					RefreshGridLayout();
					ResumeLayoutGrid();
				}
				else
					SuspendLayoutGrid();
			}
		}


		/// <summary>
		/// Invalidate all the cells.
		/// </summary>
		public virtual void InvalidateCells()
		{
			InvalidateScrollableArea();
		}

		/// <summary>
		/// OnLayout Method
		/// </summary>
		/// <param name="levent"></param>
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout (levent);

			if (Redraw)
				RefreshGridLayout();
		}

		#endregion

		#region Sort
		/// <summary>
		/// Sort a range of the grid
		/// </summary>
		/// <param name="p_RangeToSort">Range to sort</param>
		/// <param name="p_AbsoluteColKeys">Index of the column relative to the grid to use as sort keys, must be between start and end col of the range</param>
		/// <param name="p_bAsc">Ascending true, Descending false</param>
		/// <param name="p_CellComparer">CellComparer, if null the default comparer will be used</param>
		public void SortRangeRows(IRangeLoader p_RangeToSort, 
			int p_AbsoluteColKeys, 
			bool p_bAsc,
			IComparer p_CellComparer)
		{
			Range l_Range = p_RangeToSort.GetRange(this);
			SortRangeRows(l_Range,p_AbsoluteColKeys,p_bAsc,p_CellComparer);
		}

		/// <summary>
		/// Sort a range of the grid.
		/// </summary>
		/// <param name="p_Range"></param>
		/// <param name="p_AbsoluteColKeys">Index of the column relative to the grid to use as sort keys, must be between start and end col</param>
		/// <param name="p_bAscending">Ascending true, Descending false</param>
		/// <param name="p_CellComparer">CellComparer, if null the default ValueCellComparer comparer will be used</param>
		public void SortRangeRows(Range p_Range,
			int p_AbsoluteColKeys, 
			bool p_bAscending,
			IComparer p_CellComparer)
		{
			bool l_oldRedraw = Redraw;
			Redraw = false;
			try
			{
				OnSortingRangeRows(new SortRangeRowsEventArgs(p_Range, p_AbsoluteColKeys, p_bAscending, p_CellComparer));
			}
			finally
			{
				Redraw = l_oldRedraw;
			}
		}

		/// <summary>
		/// Fired when calling SortRangeRows method
		/// </summary>
		public event SortRangeRowsEventHandler SortingRangeRows;

		/// <summary>
		/// Fired when calling SortRangeRows method
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSortingRangeRows(SortRangeRowsEventArgs e)
		{
			if (SortingRangeRows!=null)
				SortingRangeRows(this, e);
		}

		#endregion

		#region ProcessCmdKey
		/// <summary>
		/// Processes a command key. 
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(
			ref Message msg,
			Keys keyData
			)
		{
			bool l_EnableEscape = false;
			if ( (SpecialKeys & GridSpecialKeys.Escape) == GridSpecialKeys.Escape)
				l_EnableEscape = true;
			bool l_EnableEnter = false;
			if ( (SpecialKeys & GridSpecialKeys.Enter) == GridSpecialKeys.Enter)
				l_EnableEnter = true;
			bool l_EnableTab = false;
			if ( (SpecialKeys & GridSpecialKeys.Tab) == GridSpecialKeys.Tab)
				l_EnableTab = true;


			if (keyData == Keys.Escape && l_EnableEscape)
			{
				ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
				if (l_FocusCell!=null && l_FocusCell.IsEditing(m_FocusPosition))
				{
					if (l_FocusCell.EndEdit(true))
						return true;
				}
			}

			//in questo caso il tasto viene sempre considerato processato 
			if (keyData == Keys.Enter && l_EnableEnter)
			{
				ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
				if (l_FocusCell!=null && l_FocusCell.IsEditing(m_FocusPosition))
				{
					l_FocusCell.EndEdit(false);

					return true;
				}
			}

			//in questo caso il tasto viene considerato processato 
			// solo se la cella era in editing e l'editing non riesce
			if (keyData == Keys.Tab && l_EnableTab)
			{
				ICellVirtual l_FocusCell = GetCell(m_FocusPosition);
				if (l_FocusCell!=null && l_FocusCell.IsEditing(m_FocusPosition))
				{
					//se l'editing non riesce considero il tasto processato 
					// altrimenti no, in questo modo il tab ha effetto anche per lo spostamento
					if (l_FocusCell.EndEdit(false) == false)
						return true;

					//altrimenti scateno anche il muovimento della cella
					ProcessSpecialGridKey(new KeyEventArgs(keyData));
					return true; //considero il tasto processato altrimenti si sposta ancora il focus
				}
			}

			return base.ProcessCmdKey(ref msg,keyData);
		}

		#endregion

		#region GetCell/SetCell

		/// <summary>
		/// Return the Cell at the specified Row and Col position. Simply call GettingCell event.
		/// </summary>
		/// <param name="p_iRow"></param>
		/// <param name="p_iCol"></param>
		/// <returns></returns>
		public virtual ICellVirtual GetCell(int p_iRow, int p_iCol)
		{
			if (GettingCell!=null)
			{
				PositionEventArgs e = new PositionEventArgs(new Position(p_iRow, p_iCol), null);
				GettingCell(this, e);
				return e.Cell;
			}
			else
				return null;
		}

		/// <summary>
		/// Set the specified cell int he specified position. Simply call SettingCell event.
		/// </summary>
		/// <param name="p_iRow"></param>
		/// <param name="p_iCol"></param>
		/// <param name="p_Cell"></param>
		public virtual void SetCell(int p_iRow, int p_iCol, ICellVirtual p_Cell)
		{
			if (SettingCell!=null)
				SettingCell(this, new PositionEventArgs(new Position(p_iRow, p_iCol), p_Cell));
		}

		/// <summary>
		/// Fired when GetCell is called with GridVirtual class. Use the e.Cell property to set the cell class
		/// </summary>
		public event PositionEventHandler GettingCell;

		/// <summary>
		/// Fired when SetCell is called with GridVirtual class. Read the e.Cell property to get the cell class
		/// </summary>
		public event PositionEventHandler SettingCell;
		#endregion

		#region Panels
		private GridSubPanel m_LeftPanel;
		private GridSubPanel m_TopPanel;
		private GridSubPanel m_TopLeftPanel;
		private GridSubPanel m_ScrollablePanel;
		//questo è un pannello nascosto per gestire il focus della cella. Gli editor adesso vengono inseriti nei panelli a seconda della posizione delle celle e quindi per poter rimuovere il focus dalla cella bisogna spostare il focus su un controllo parallelo che non sia parent dell'editor.
		private GridSubPanel m_HiddenFocusPanel;

		/// <summary>
		/// Not scrollable left panel (For RowHeader)
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridSubPanel LeftPanel
		{
			get{return m_LeftPanel;}
		}
		/// <summary>
		/// Not scrollable top panel (For ColHeader)
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridSubPanel TopPanel
		{
			get{return m_TopPanel;}
		}
		/// <summary>
		/// Not scrollable top+left panel (For Row or Col Header)
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridSubPanel TopLeftPanel
		{
			get{return m_TopLeftPanel;}
		}
		/// <summary>
		/// Scrollable panel for normal scrollable cells
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridSubPanel ScrollablePanel
		{
			get{return m_ScrollablePanel;}
		}

		/// <summary>
		/// Hidden panl for innternal use only. I use this panel to catch mouse and keyboard events.
		/// </summary>
		protected GridSubPanel HiddenFocusPanel
		{
			get{return m_HiddenFocusPanel;}
		}

		/// <summary>
		/// Recalculate panel position
		/// </summary>
		private void CalculatePanelsLocation()
		{
			int l_Height = 0;
			if (Rows.Count >= FixedRows && FixedRows > 0)
				l_Height = Rows[FixedRows-1].Bottom;

			int l_Width = 0;
			if (Columns.Count >= FixedColumns && FixedColumns > 0)
				l_Width = Columns[FixedColumns-1].Right;

			Rectangle l_DisplayRectangle = DisplayRectangle;
			m_TopLeftPanel.Size = new Size(l_Width, l_Height);
			m_LeftPanel.Location = new Point(0, l_Height);
			m_LeftPanel.Size = new Size(l_Width, l_DisplayRectangle.Height-l_Height);
			m_TopPanel.Location = new Point(l_Width, 0);
			m_TopPanel.Size = new Size(l_DisplayRectangle.Width-l_Width, l_Height);
			m_ScrollablePanel.Location = new Point(l_Width, l_Height);
			m_ScrollablePanel.Size = new Size(l_DisplayRectangle.Width-l_Width, l_DisplayRectangle.Height-l_Height);
		}

		/// <summary>
		/// Get the panels that contains the specified cells position. Returns null if the position is not valid
		/// </summary>
		/// <param name="p_CellPosition"></param>
		/// <returns></returns>
		public GridSubPanel PanelAtPosition(Position p_CellPosition)
		{
			if (p_CellPosition.IsEmpty() == false)
			{
				CellPositionType l_Type = GetPositionType(p_CellPosition);
				if (l_Type == CellPositionType.FixedTopLeft)
					return TopLeftPanel;
				else if (l_Type == CellPositionType.FixedLeft)
					return LeftPanel;
				else if (l_Type == CellPositionType.FixedTop)
					return TopPanel;
				else if (l_Type == CellPositionType.Scrollable)
					return ScrollablePanel;
				else
					return null;
			}
			else
				return null;
		}


		/// <summary>
		/// Set the focus on the control that contains the cells.
		/// </summary>
		public bool SetFocusOnCells()
		{
			return HiddenFocusPanel.Focus();
		}

		/// <summary>
		/// Returns true if the cells have the focus.
		/// </summary>
		/// <returns></returns>
		public bool CellsContainsFocus
		{
			get{return HiddenFocusPanel.ContainsFocus;}
		}

		/// <summary>
		/// Invalidate the scrollable area
		/// </summary>
		protected override void InvalidateScrollableArea()
		{
			m_ScrollablePanel.Invalidate(true);
			m_TopLeftPanel.Invalidate(true);
			m_LeftPanel.Invalidate(true);
			m_TopPanel.Invalidate(true);
		}
		#endregion

		#region Rows, Columns
		/// <summary>
		/// Indicates the number of columns
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColumnsCount
		{
			get{return m_Columns.Count;}
			set
			{
				if (ColumnsCount<value)
					m_Columns.InsertRange(ColumnsCount,value-ColumnsCount);
				else if (ColumnsCount>value)
					m_Columns.RemoveRange(value, ColumnsCount-value);
			}
		}

		/// <summary>
		/// Indicates the number of rows
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RowsCount
		{
			get{return m_Rows.Count;}
			set
			{
				if (RowsCount<value)
					m_Rows.InsertRange(RowsCount,value-RowsCount);
				else if (RowsCount>value)
					m_Rows.RemoveRange(value, RowsCount-value);
			}
		}

		private int m_FixedRows = 0;
		/// <summary>
		/// Indicates how many rows are not scrollable
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int FixedRows
		{
			get{return m_FixedRows;}
			set{m_FixedRows = value;}
		}
		private int m_FixedCols = 0;
		/// <summary>
		/// Indicates how many cols are not scrollable
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int FixedColumns
		{
			get{return m_FixedCols;}
			set{m_FixedCols = value;}
		}

		private RowInfo.RowInfoCollection m_Rows;

		/// <summary>
		/// RowsCount informations
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RowInfo.RowInfoCollection Rows
		{
			get{return m_Rows;}
		}

		private ColumnInfo.ColumnInfoCollection m_Columns;

		/// <summary>
		/// Columns informations
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ColumnInfo.ColumnInfoCollection Columns
		{
			get{return m_Columns;}
		}


		private void m_Rows_RowHeightChanged(object sender, RowInfoEventArgs e)
		{
			if (Redraw)
				RefreshGridLayout();
		}

		private void m_Columns_ColumnWidthChanged(object sender, ColumnInfoEventArgs e)
		{
			if (Redraw)
				RefreshGridLayout();
		}

		private void m_Rows_RowsAdded(object sender, IndexRangeEventArgs e)
		{
			if (Redraw)
				RefreshGridLayout();
		}

		private void m_Columns_ColumnsAdded(object sender, IndexRangeEventArgs e)
		{
			if (Redraw)
				RefreshGridLayout();
		}

		/// <summary>
		/// Returns the type of a cell position
		/// </summary>
		/// <param name="p_CellPosition"></param>
		/// <returns></returns>
		public CellPositionType GetPositionType(Position p_CellPosition)
		{
			if (p_CellPosition.IsEmpty())
				return CellPositionType.Empty;
			else if (p_CellPosition.Row < FixedRows && p_CellPosition.Column < FixedColumns)
				return CellPositionType.FixedTopLeft;
			else if (p_CellPosition.Row < FixedRows)
				return CellPositionType.FixedTop;
			else if (p_CellPosition.Column < FixedColumns)
				return CellPositionType.FixedLeft;
			else
				return CellPositionType.Scrollable;
		}

		/// <summary>
		/// Returns a Range that represents the complete cells of the grid
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Range CompleteRange
		{
			get
			{
				if (RowsCount > 0 && ColumnsCount > 0)
					return new Range(0,0,RowsCount-1, ColumnsCount-1);
				else
					return Range.Empty;
			}
		}
		#endregion

		#region Position Serach (PositionAtPoint)
		/// <summary>
		/// Returns the cell at the specified grid view relative point (the point must be relative to the grid display region), SearchInFixedCells = true. Return Empty if no valid cells are found
		/// </summary>
		/// <param name="p_RelativeViewPoint">Point</param>
		/// <returns></returns>
		public virtual Position PositionAtPoint(Point p_RelativeViewPoint)
		{
			return PositionAtPoint(p_RelativeViewPoint,true);
		}

		/// <summary>
		/// Returns the cell at the specified grid view relative point (the point must be relative to the grid display region)
		/// </summary>
		/// <param name="p_RelativeViewPoint">Point</param>
		/// <param name="p_bSearchInFixedCells">True if you want to consider fixed cells in the search</param>
		/// <returns></returns>
		public virtual Position PositionAtPoint(Point p_RelativeViewPoint, bool p_bSearchInFixedCells)
		{
			Position l_Found = Position.Empty;
			if (p_bSearchInFixedCells)
			{
				l_Found = TopLeftPanel.PositionAtPoint(p_RelativeViewPoint);
				if (l_Found.IsEmpty())
				{
					l_Found = LeftPanel.PositionAtPoint(p_RelativeViewPoint);
					if (l_Found.IsEmpty())
					{
						l_Found = TopPanel.PositionAtPoint(p_RelativeViewPoint);
					}
				}
			}

			if (l_Found.IsEmpty())
				l_Found = ScrollablePanel.PositionAtPoint(p_RelativeViewPoint);

			return l_Found;
		}
		#endregion

		#region RangeSearch (RangeAtRectangle)
		/// <summary>
		/// Returns a range of cells inside an absolute rectangle
		/// </summary>
		/// <param name="p_AbsoluteRect"></param>
		/// <returns></returns>
		public Range RangeAtAbsRect(Rectangle p_AbsoluteRect)
		{
			int l_Start_R, l_Start_C, l_End_R, l_End_C;

			l_Start_R = Rows.RowAtPoint(p_AbsoluteRect.Y);
			l_Start_C = Columns.ColumnAtPoint(p_AbsoluteRect.X);

			l_End_R = Rows.RowAtPoint(p_AbsoluteRect.Bottom);
			l_End_C = Columns.ColumnAtPoint(p_AbsoluteRect.Right);

			if (l_Start_R==Position.c_EmptyIndex || l_Start_C==Position.c_EmptyIndex 
				|| l_End_C==Position.c_EmptyIndex || l_End_R==Position.c_EmptyIndex)
				return Range.Empty;

			return new Range(l_Start_R, l_Start_C, l_End_R, l_End_C);
		}
		#endregion

		#region ToolTip and Cursor
		/// <summary>
		/// True to activate the tooltiptext
		/// </summary>
		public bool GridToolTipActive
		{
			get{return ScrollablePanel.ToolTipActive;}
			set{ScrollablePanel.ToolTipActive = value;LeftPanel.ToolTipActive=value;TopPanel.ToolTipActive=value;TopLeftPanel.ToolTipActive=value;}
		}

		/// <summary>
		/// Cursor for the container of the cells. This property is used when you set a cursor to a specified cell.
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cursor GridCursor
		{
			get{return ScrollablePanel.Cursor;}
			set{ScrollablePanel.Cursor = value;LeftPanel.Cursor=value;TopPanel.Cursor=value;TopLeftPanel.Cursor=value;}
		}

		/// <summary>
		/// ToolTip text of the container of the cells. This property is used when you set a tooltip to a specified cell.
		/// </summary>
		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string GridToolTipText
		{
			get{return ScrollablePanel.ToolTipText;}
			set{ScrollablePanel.ToolTipText = value;LeftPanel.ToolTipText=value;TopPanel.ToolTipText=value;TopLeftPanel.ToolTipText=value;}
		}
		#endregion

		#region Events MouseEnter/Leave/Wheel
		//questi eventi non sono gestiti a livello di Panel perchè devono fare riferimento all'intero controllo

		/// <summary>
		/// Fired when a user scroll with the mouse wheel
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel (e);
			OnGridMouseWheel(e);
		}

		#endregion

		#region Abstract Methods
		/// <summary>
		/// Return the Cell at the specified Row and Col position. This method is called for sort operations and for Move operations. If position is Empty return null. This method calls GetCell(int p_iRow, int p_iCol)
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public ICellVirtual GetCell(Position p_Position)
		{
			if (p_Position.IsEmpty())
				return null;
			else
				return GetCell(p_Position.Row, p_Position.Column);
		}

		/// <summary>
		/// Set the specified cell int he specified position. This method calls SetCell(int p_iRow, int p_iCol, ICellVirtual p_Cell)
		/// </summary>
		/// <param name="p_Position"></param>
		/// <param name="p_Cell"></param>
		public void SetCell(Position p_Position, ICellVirtual p_Cell)
		{
			SetCell(p_Position.Row, p_Position.Column, p_Cell);
		}
		#endregion

	}
}