using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.Serialization;
using SourceLibrary.Windows.Forms;

namespace SourceGrid2
{
	/// <summary>
	/// Represent the selected cells of the grid.
	/// </summary>
	public class Selection : ICollection
	{
		private GridRangeCollection m_RangeList = new GridRangeCollection();
		private GridVirtual m_Grid;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Grid"></param>
		public Selection(GridVirtual p_Grid)
		{
			m_Grid = p_Grid;

			m_iImageCut = m_MenuImageList.Images.Count;
			m_MenuImageList.Images.Add(CommonImages.Cut);

			m_iImageCopy = m_MenuImageList.Images.Count;
			m_MenuImageList.Images.Add(CommonImages.Copy);

			m_iImagePaste = m_MenuImageList.Images.Count;
			m_MenuImageList.Images.Add(CommonImages.Paste);

			m_iImageClear = m_MenuImageList.Images.Count;
			m_MenuImageList.Images.Add(CommonImages.Clear);

			m_iImageFormatCells = m_MenuImageList.Images.Count;
			m_MenuImageList.Images.Add(CommonImages.Properties);
		}


		/// <summary>
		/// Linked grid
		/// </summary>
		public GridVirtual Grid
		{
			get{return m_Grid;}
		}


		/// <summary>
		/// Returns the union of all the selected range as Position collection
		/// </summary>
		/// <returns></returns>
		public PositionCollection GetCellsPositions()
		{
			if (m_PositionListCache==null)
			{
				m_PositionListCache = new PositionCollection();
				for (int i = 0; i < m_RangeList.Count; i++)
				{
					PositionCollection l_tmp = m_RangeList[i].GetCellsPositions();
					for (int j = 0; j < l_tmp.Count; j++)
						if (m_PositionListCache.Contains(l_tmp[j])==false)
							m_PositionListCache.Add(l_tmp[j]);
				}
			}

			return m_PositionListCache;
		}

		/// <summary>
		/// Returns the union of all the selected range as Position collection
		/// </summary>
		/// <returns></returns>
		public CellVirtualCollection GetCells()
		{
			if (m_CellBaseListCache==null)
			{
				m_CellBaseListCache = new CellVirtualCollection();
				PositionCollection l_Positions = GetCellsPositions();
				for (int i = 0; i < l_Positions.Count; i++)
				{
					m_CellBaseListCache.Add(m_Grid.GetCell(l_Positions[i]));
				}
			}

			return m_CellBaseListCache;
		}

		/// <summary>
		/// Returns the cell at the specific position
		/// </summary>
		public Range this[int index]
		{
			get{return m_RangeList[index];}
		}

		/// <summary>
		/// Indicates if the specified cell is selected
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <returns></returns>
		public bool Contains(Position p_Cell)
		{
			if (p_Cell.IsEmpty())
				return false;

			for (int r = 0; r < m_RangeList.Count; r++)
			{
				if ( this[r].Contains(p_Cell) )
					return true;
			}

			return false;
		}

		/// <summary>
		/// Indicates if the specified range of cells is selected
		/// </summary>
		/// <param name="p_Range"></param>
		/// <returns></returns>
		public bool Contains(Range p_Range)
		{
			if (Count<=0)
				return false;

			//prima cerco se è presente un range esattamente come quello richiesto
			if (m_RangeList.Contains(p_Range))
				return true;

			//se non ho trovato uguale provo a cercare cella per cella
			PositionCollection l_SearchList = p_Range.GetCellsPositions();
			for (int i = 0; i < l_SearchList.Count; i++)
			{
				bool l_bFound = false;
				for (int r = 0; r < Count; r++)
				{
					if (this[r].Contains(l_SearchList[i]))
					{
						l_bFound = true;
						break;
					}
				}
				if (l_bFound==false)
					return false;
			}

			return true;
		}

		/// <summary>
		/// Indicates if the specified row is selected
		/// </summary>
		/// <param name="p_Row"></param>
		/// <returns></returns>
		public bool ContainsRow(int p_Row)
		{
			for (int r = 0; r < m_RangeList.Count; r++)
			{
				if ( this[r].ContainsRow(p_Row) )
					return true;
			}

			return false;
		}
		/// <summary>
		/// Indicates if the specified column is selected
		/// </summary>
		/// <param name="p_Column"></param>
		/// <returns></returns>
		public bool ContainsColumn(int p_Column)
		{
			for (int r = 0; r < m_RangeList.Count; r++)
			{
				if ( this[r].ContainsColumn(p_Column) )
					return true;
			}

			return false;
		}
		#region Add/Remove/Clear

		private PositionCollection m_PositionListCache = null;
		private CellVirtualCollection m_CellBaseListCache = null;

		private void ClearCache()
		{
			m_CellBaseListCache = null;
			m_PositionListCache = null;
		}

		/// <summary>
		/// deseleziona tutte le celle tranne quella passata in input
		/// </summary>
		/// <param name="p_CellLeaveThisCellSelected"></param>
		public void Clear(Position p_CellLeaveThisCellSelected)
		{
			if (Count>0)
			{
				m_RangeList.Clear();
			
				m_RangeList.Add( new Range(p_CellLeaveThisCellSelected) );

				OnSelectionChange(new SelectionChangeEventArgs(SelectionChangeEventType.Clear, Range.Empty));
			}
		}

		/// <summary>
		/// Deselect all the cells
		/// </summary>
		public void Clear()
		{
			if (Count>0)
			{
				m_RangeList.Clear();

				OnSelectionChange(new SelectionChangeEventArgs(SelectionChangeEventType.Clear, Range.Empty));
			}
		}

		/// <summary>
		/// Select the specified cell and add the cell to the collection.
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <returns></returns>
		public void Add(Position p_Cell)
		{
			AddRange( new Range(p_Cell) );
		}

		/// <summary>
		/// Select the specified Range of cells
		/// </summary>
		/// <param name="p_Range"></param>
		public void AddRange(Range p_Range)
		{
			if (p_Range.IsEmpty() == false)
			{
				Range l_RangeToSelect = p_Range;

				//Apply SelectionMode
				if (m_SelMode == GridSelectionMode.Row)
				{
					if (m_Grid.ColumnsCount>0)
						l_RangeToSelect = new Range(p_Range.Start.Row, 0, p_Range.End.Row, m_Grid.ColumnsCount-1);
				}
				else if (m_SelMode == GridSelectionMode.Col)
				{
					if (m_Grid.RowsCount>0)
						l_RangeToSelect = new Range(0, p_Range.Start.Column, m_Grid.RowsCount-1, p_Range.End.Column);
				}

				if (Contains(l_RangeToSelect) == false)
				{
					m_RangeList.Add(l_RangeToSelect);

					OnSelectionChange(new SelectionChangeEventArgs(SelectionChangeEventType.Add, l_RangeToSelect));
				}
			}
		}

		
		/// <summary>
		/// Deselect and remove from the collection the specified range of cells
		/// </summary>
		/// <param name="p_Range"></param>
		public void RemoveRange(Range p_Range)
		{
			if (p_Range.IsEmpty() == false)
			{
				Range l_RangeToDeselect = p_Range;

				//Apply SelectionMode
				if (m_SelMode == GridSelectionMode.Row)
				{
					if (m_Grid.ColumnsCount>0)
						l_RangeToDeselect = new Range(p_Range.Start.Row, 0, p_Range.End.Row, m_Grid.ColumnsCount-1);
				}
				else if (m_SelMode == GridSelectionMode.Col)
				{
					if (m_Grid.RowsCount>0)
						l_RangeToDeselect = new Range(0, p_Range.Start.Column, m_Grid.RowsCount-1, p_Range.End.Column);
				}

				//TODO bisognerebbe ottimizzare questo metodo
				// per ora scompatto i range correnti in tante celle e poi le celle contenute nel range da deselezionare le rimuovo
				PositionCollection l_CurrentRanges = GetCellsPositions();
				m_RangeList.Clear();
				bool l_bFound = false;
				for (int i = 0; i < l_CurrentRanges.Count; i++)
				{
					if (l_RangeToDeselect.Contains(l_CurrentRanges[i])==false)
						m_RangeList.Add( new Range(l_CurrentRanges[i]) );
					else
						l_bFound = true;
				}

				if (l_bFound)
					OnSelectionChange(new SelectionChangeEventArgs(SelectionChangeEventType.Remove, l_RangeToDeselect));
			}
		}
		/// <summary>
		/// Deselect and remove from the collection the specified cell
		/// </summary>
		/// <param name="p_Cell"></param>
		public void Remove(Position p_Cell)
		{
			RemoveRange( new Range(p_Cell) );
		}

		#endregion

		/// <summary>
		/// Searches for the specified Cell and returns the zero-based index of the first occurrence that starts at the specified index and contains the specified number of elements.
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <returns></returns>
		public int IndexOf(Cells.ICellVirtual p_Cell)
		{
			return GetCells().IndexOf(p_Cell);
		}


		#region SelectionChange event
		/// <summary>
		/// Fired when a cell is added from the selection or removed from the selection
		/// </summary>
		public event SelectionChangeEventHandler SelectionChange;

		/// <summary>
		/// Fired when a cell is added from the selection or removed from the selection
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectionChange(SelectionChangeEventArgs e)
		{
//			#warning Temporaneo
//			if (e.EventType == SelectionChangeEventType.Add)
//				System.Diagnostics.Debug.WriteLine("Selection.AddRange " + e.Range.ToString());
//			else if (e.EventType == SelectionChangeEventType.Remove)
//				System.Diagnostics.Debug.WriteLine("Selection.RemoveRange " + e.Range.ToString());
//			else if (e.EventType == SelectionChangeEventType.Clear)
//				System.Diagnostics.Debug.WriteLine("Selection.Clear");

			ClearCache();

			if (e.EventType == SelectionChangeEventType.Add || e.EventType == SelectionChangeEventType.Remove)
			{
				m_Grid.InvalidateRange(e.Range);
			}
			else //clear
				m_Grid.InvalidateCells();

			if (SelectionChange!=null)
				SelectionChange(this,e);
		}

		#endregion

		#region SelectionMode
		private GridSelectionMode m_SelMode = GridSelectionMode.Cell;
		/// <summary>
		/// Selection type
		/// </summary>
		public GridSelectionMode SelectionMode
		{
			get{return m_SelMode;}
			set{m_SelMode = value;}
		}

		private bool m_bEnableMultiSelection = true;

		/// <summary>
		/// True=Enable multi selection with the Ctrl key or Shift Key or with mouse.
		/// </summary>
		public bool EnableMultiSelection
		{
			get{return m_bEnableMultiSelection;}
			set{m_bEnableMultiSelection = value;}
		}

		#endregion

		/// <summary>
		/// Returns the range of the current selection. If the user has selected non contiguous cells this method returns a range to contains all the selected cells.
		/// </summary>
		/// <returns></returns>
		public Range GetRange()
		{
			if (Count > 0)
			{
				int l_row1 = int.MaxValue;
				int l_col1 = int.MaxValue;
				int l_row2 = int.MinValue;
				int l_col2 = int.MinValue;
				foreach ( Range r in this)
				{
					if (l_row1 > r.Start.Row)
						l_row1 = r.Start.Row;

					if (l_col1 > r.Start.Column)
						l_col1 = r.Start.Column;

					if (l_row2 < r.Start.Row)
						l_row2 = r.Start.Row;

					if (l_col2 < r.Start.Column)
						l_col2 = r.Start.Column;


					if (l_row1 > r.End.Row)
						l_row1 = r.End.Row;

					if (l_col1 > r.End.Column)
						l_col1 = r.End.Column;

					if (l_row2 < r.End.Row)
						l_row2 = r.End.Row;

					if (l_col2 < r.End.Column)
						l_col2 = r.End.Column;
				}

				return new Range(l_row1, l_col1, l_row2, l_col2);
			}
			else
				return Range.Empty;
		}


		#region Selected Rows/Columns
		/// <summary>
		/// Returns an array of the rows selected
		/// </summary>
		public RowInfo[] SelectedRows
		{
			get
			{
				ArrayList l_List = new ArrayList();
				Range l_Range = GetRange();
				if (l_Range.IsEmpty() == false)
				{
					for (int r = l_Range.Start.Row; r <= l_Range.End.Row; r++)
					{
						if (ContainsRow(r))
							l_List.Add(Grid.Rows[r]);
					}
				}
				RowInfo[] ret = new RowInfo[l_List.Count];
				for (int r = 0; r < ret.Length; r++)
					ret[r] = (RowInfo)(l_List[r]);

				return ret;
			}
		}
		/// <summary>
		/// Returns an array of the columns selected
		/// </summary>
		public ColumnInfo[] SelectedColumns
		{
			get
			{
				ArrayList l_List = new ArrayList();
				Range l_Range = GetRange();
				if (l_Range.IsEmpty() == false)
				{
					for (int c = l_Range.Start.Column; c <= l_Range.End.Column; c++)
					{
						if (ContainsColumn(c))
							l_List.Add(Grid.Columns[c]);
					}
				}
				ColumnInfo[] ret = new ColumnInfo[l_List.Count];
				for (int c = 0; c < ret.Length; c++)
					ret[c] = (ColumnInfo)(l_List[c]);

				return ret;
			}
		}
		#endregion

		#region ContextMenu
		#region ImageContextMenu
		//image for menu
		private ImageList m_MenuImageList = new ImageList();

		private int m_iImageCut;
		private int m_iImageCopy;
		private int m_iImagePaste;
		private int m_iImageClear;
		private int m_iImageFormatCells;
		#endregion

		/// <summary>
		/// Returns the ContextMenu used when the user Right-Click on a selected cell.
		/// </summary>
		/// <returns></returns>
		public virtual MenuCollection GetContextMenus()
		{
			MenuCollection l_Array = new MenuCollection();

			bool l_EnableCopyPasteSelection = false;
			if ( (m_Grid.ContextMenuStyle & ContextMenuStyle.CopyPasteSelection) == ContextMenuStyle.CopyPasteSelection)
				l_EnableCopyPasteSelection = true;

			bool l_EnableClearSelection = false;
			if ( (m_Grid.ContextMenuStyle & ContextMenuStyle.ClearSelection) == ContextMenuStyle.ClearSelection)
				l_EnableClearSelection = true;

//			bool l_EnablePropertySelection = false;
//			if ( (m_Grid.ContextMenuStyle & ContextMenuStyle.PropertySelection) == ContextMenuStyle.PropertySelection)
//				l_EnablePropertySelection = true;

			if (m_ContextMenuItems!=null && m_ContextMenuItems.Count > 0)
			{
				foreach(MenuItem m in m_ContextMenuItems)
					l_Array.Add(m);

				if (l_EnableClearSelection || l_EnableCopyPasteSelection ) //|| l_EnablePropertySelection)
					l_Array.Add(new MenuItem("-"));
			}

			if (l_EnableCopyPasteSelection)
			{
//				//CUT (not implemented)
//				MenuItem l_mnCut = new MenuItemImage("Cut", new EventHandler(Selection_Cut), m_MenuImageList, m_iImageCut);
//				l_mnCut.Enabled = false;
//				l_Array.Add(l_mnCut);

				//COPY 
				MenuItem l_mnCopy = new MenuItemImage("Copy", new EventHandler(Selection_Copy), m_MenuImageList, m_iImageCopy);
				l_Array.Add(l_mnCopy);

				//PASTE
				MenuItem l_mnPaste = new MenuItemImage("Paste", new EventHandler(Selection_Paste), m_MenuImageList, m_iImagePaste);
				l_mnPaste.Enabled = IsValidClipboardForPaste();
				l_Array.Add(l_mnPaste);
			}

			if (l_EnableClearSelection)
			{
				if (l_EnableCopyPasteSelection)// && l_EnablePropertySelection)
					l_Array.Add(new MenuItem("-"));

				MenuItem l_mnClear = new MenuItemImage("Clear", new EventHandler(Selection_ClearValues), m_MenuImageList, m_iImageClear);
				l_Array.Add(l_mnClear);
			}
//			if (l_EnablePropertySelection)
//			{
//				MenuItem l_mnFormatCells = new MenuItem("Format Cells ...", new EventHandler(Selection_FormatCells));
//				m_Grid.SetMenuImage(l_mnFormatCells,m_iImageFormatCells);
//				l_Array.Add(l_mnFormatCells);
//			}

			return l_Array;
		}


		private MenuCollection m_ContextMenuItems = null;

		/// <summary>
		/// ContextMenu of the selected cells. Null if no contextmenu is active.
		/// </summary>
		public MenuCollection ContextMenuItems
		{
			get{return m_ContextMenuItems;}
			set{m_ContextMenuItems = value;}
		}

		#endregion

		#region Clipboard
		private bool m_bAutoCopyPaste = true;
		/// <summary>
		/// True to enable the default copy/paste operations
		/// </summary>
		public bool AutoCopyPaste
		{
			get{return m_bAutoCopyPaste;}
			set{m_bAutoCopyPaste = value;}
		}
		/// <summary>
		/// Copy event
		/// </summary>
		public event EventHandler ClipboardCopy;
		/// <summary>
		/// Paste Event
		/// </summary>
		public event EventHandler ClipboardPaste;
		/// <summary>
		/// Cut event
		/// </summary>
		public event EventHandler ClipboardCut;

		/// <summary>
		/// Cut the content of the selected cells. NOT YET IMPLEMENTED.
		/// </summary>
		public void OnClipboardCut()
		{
			try
			{
				if (ClipboardCut!=null)
					ClipboardCut(this,EventArgs.Empty);
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"Clipboard copy error");
			}		
		}
		/// <summary>
		/// Copy the content of the selected cells
		/// </summary>
		public void OnClipboardCopy()
		{
			try
			{
				if (ClipboardCopy!=null)
					ClipboardCopy(this,EventArgs.Empty);

				if (m_bAutoCopyPaste)
				{
					if (Count>0)
					{
						//Clipboard text format
						Range l_Range = GetRange();
						System.Text.StringBuilder l_TabBuffer = new System.Text.StringBuilder();
						for (int r = l_Range.Start.Row; r <= l_Range.End.Row; r++)
						{
							for (int c = l_Range.Start.Column;c <= l_Range.End.Column; c++)
							{
								//devo controllare che la cella sia selezionata perchè la find trova soltanto gli estremi
								if ( m_Grid.GetCell(r,c) != null && m_Grid.Selection.Contains(new Position(r,c)) )
								{
									if ( m_Grid.GetCell(r,c).DataModel != null)
										l_TabBuffer.Append(m_Grid.GetCell(r,c).DataModel.ValueToString(m_Grid.GetCell(r,c).GetValue(new Position(r,c)) ));
									else
										l_TabBuffer.Append(m_Grid.GetCell(r,c).GetDisplayText(new Position(r,c)));
									//l_TabBuffer.Append(m_Grid[r,c].Value.ToString());
								}

								if (c<l_Range.End.Column)
								{
									l_TabBuffer.Append("\t");
								}
							}
							if (r<l_Range.End.Row)
							{
								l_TabBuffer.Append("\x0D\x0A");
							}
						}
						DataObject l_dataObj = new DataObject();
						l_dataObj.SetData(DataFormats.Text,l_TabBuffer.ToString());

						Clipboard.SetDataObject(l_dataObj,true);
					}
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"Clipboard copy error");
			}
		}
		/// <summary>
		/// Paste the content of the selected cells
		/// </summary>
		public void OnClipboardPaste()
		{
			try
			{
				if (ClipboardPaste!=null)
					ClipboardPaste(this,EventArgs.Empty);

				if (m_bAutoCopyPaste)
				{
					if (IsValidClipboardForPaste() && Count > 0)
					{
						IDataObject l_dtObj = Clipboard.GetDataObject();
						string l_buffer = (string)l_dtObj.GetData(DataFormats.Text,true);
						//tolgo uno dei due caratteri di a capo per usare lo split
						l_buffer = l_buffer.Replace("\x0D\x0A","\x0A");
						string[] l_buffRows = l_buffer.Split('\x0A','\x0D');

						Range l_Range = GetRange();
						for (int r = l_Range.Start.Row; r < Math.Min(l_Range.Start.Row+l_buffRows.Length,m_Grid.RowsCount); r++)
						{
							if (l_buffRows[r-l_Range.Start.Row].Length>0)
							{
								string[] l_buffCols = l_buffRows[r-l_Range.Start.Row].Split('\t');
								for (int c = l_Range.Start.Column; c < Math.Min(l_Range.Start.Column+l_buffCols.Length,m_Grid.ColumnsCount); c++)
								{
									Cells.ICellVirtual l_Cell = m_Grid.GetCell(r,c);
									if (l_Cell != null && l_Cell.DataModel != null)
									{
										l_Cell.DataModel.SetCellValue(l_Cell, new Position(r,c), l_buffCols[c-l_Range.Start.Column]);
									}
								}
							}
						}
					}
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"Clipboard paste error");
			}
		}

		/// <summary>
		/// Returns if the current content of the Clipboard is valid for Paste operations
		/// </summary>
		/// <returns></returns>
		public bool IsValidClipboardForPaste()
		{
			IDataObject l_dtObj = Clipboard.GetDataObject();
			return l_dtObj.GetDataPresent(DataFormats.Text,true);
		}

		private void Selection_Cut(object sender, EventArgs e)
		{
			OnClipboardCut();
		}
		private void Selection_Paste(object sender, EventArgs e)
		{
			OnClipboardPaste();
		}
		private void Selection_Copy(object sender, EventArgs e)
		{
			OnClipboardCopy();
		}
		#endregion
		#region ClearValues
		private void Selection_ClearValues(object sender, EventArgs e)
		{
			ClearValues();
		}
		/// <summary>
		/// Clear all the selected cells with a valid Model.
		/// </summary>
		public void ClearValues()
		{
			try
			{
				if (ClearCells!=null)
					ClearCells(this, EventArgs.Empty);

				if (AutoClear)
				{
					PositionCollection l_Cells = GetCellsPositions();
					foreach(Position c in l_Cells)
					{
						if (Grid.GetCell(c)!=null)
						{
							if (Grid.GetCell(c).DataModel != null)
								Grid.GetCell(c).DataModel.ClearCell(Grid.GetCell(c), c);
						}
					}
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"Clear error");
			}
		}
	
		private bool m_bAutoClear = true;
		/// <summary>
		/// True to enable the default clear operation
		/// </summary>
		public bool AutoClear
		{
			get{return m_bAutoClear;}
			set{m_bAutoClear = value;}
		}


		/// <summary>
		/// Clear event
		/// </summary>
		public event EventHandler ClearCells;

		#endregion
		#region ICollection Members

		/// <summary>
		/// 
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)m_RangeList).IsSynchronized;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Count
		{
			get
			{
				return m_RangeList.Count;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(Array array, int index)
		{
			((ICollection)m_RangeList).CopyTo(array,index);
		}

		/// <summary>
		/// 
		/// </summary>
		public object SyncRoot
		{
			get
			{
				return ((ICollection)m_RangeList).SyncRoot;
			}
		}

		#endregion

		#region IEnumerable Members

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return m_RangeList.GetEnumerator();
		}

		#endregion
	}

}
