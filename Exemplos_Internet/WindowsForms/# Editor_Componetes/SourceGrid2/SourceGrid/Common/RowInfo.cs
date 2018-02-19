using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace SourceGrid2
{
	/// <summary>
	/// Row Information
	/// </summary>
	public class RowInfo
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Grid"></param>
		private RowInfo(GridVirtual p_Grid)
		{
			m_Grid = p_Grid;
		}

		private int m_Height = Utility.DefaultCellHeight;
		/// <summary>
		/// Height of the current row
		/// </summary>
		public int Height
		{
			get{return m_Height;}
			set
			{
				if (value<=0)
					value=1;

				if (m_Height != value)
				{
					m_Height = value;
					if (m_Grid!=null)
						m_Grid.Rows.InvokeRowHeightChanged(new RowInfoEventArgs(this));
				}
			}
		}

		private int m_Top;
		/// <summary>
		/// Top absolute position of the current row
		/// </summary>
		public int Top
		{
			get{return m_Top;}
		}

		/// <summary>
		/// Bottom of the row (Top+Height)
		/// </summary>
		public int Bottom
		{
			get{return Top+Height;}
		}
		//private int m_Index;
		/// <summary>
		/// Index of the current row
		/// </summary>
		public int Index
		{
			get{return m_Grid.Rows.IndexOf(this);}
		}

		private GridVirtual m_Grid;
		/// <summary>
		/// Attached Grid
		/// </summary>
		public GridVirtual Grid
		{
			get{return m_Grid;}
		}


		/// <summary>
		/// Returns all the cells at current row position
		/// </summary>
		/// <returns></returns>
		public Cells.ICellVirtual[] GetCells()
		{
			if (m_Grid == null)
				throw new SourceGridException("Invalid Grid object");

			int l_CurrentRow = Index;

			Cells.ICellVirtual[] l_Cells = new Cells.ICellVirtual[m_Grid.Columns.Count];
			for (int c = 0; c < m_Grid.Columns.Count; c++)
				l_Cells[c] = m_Grid.GetCell(l_CurrentRow, c);

			return l_Cells;
		}

		/// <summary>
		/// Set the specified cells at the current row position
		/// </summary>
		/// <param name="p_Cells"></param>
		public void SetCells(params Cells.ICellVirtual[] p_Cells)
		{
			if (m_Grid == null)
				throw new SourceGridException("Invalid Grid object");

			if (p_Cells!=null)
			{
				int l_CurrentRow = Index;

				for (int c = 0; c < p_Cells.Length; c++)
					m_Grid.SetCell(l_CurrentRow, c, p_Cells[c]);
			}
		}

		/// <summary>
		/// Move the Focus to the first cell not fixed of the current row
		/// </summary>
		/// <returns></returns>
		public bool Focus()
		{
			if (Grid.ColumnsCount > Grid.FixedColumns && Grid.RowsCount > Index)
				return Grid.SetFocusCell(new Position(Index, Grid.FixedColumns));
			else
				return false;
		}

		/// <summary>
		/// Gets or sets if the current row is selected. If only a column of the row is selected this property returns true.
		/// </summary>
		public bool Select
		{
			get{return Grid.Selection.ContainsRow(Index);}
			set
			{
				if (Grid.ColumnsCount > 0 && Grid.RowsCount > Index)
				{
					if (value)
						Grid.Selection.AddRange(new Range(Index, 0, Index, Grid.ColumnsCount-1));
					else
						Grid.Selection.RemoveRange(new Range(Index, 0, Index, Grid.ColumnsCount-1));
				}
			}
		}

		private object m_Tag;
		/// <summary>
		/// A property that the user can use to insert custom informations associated to a specific row
		/// </summary>
		public object Tag
		{
			get{return m_Tag;}
			set{m_Tag = value;}
		}

		private AutoSizeMode m_AutoSizeMode = AutoSizeMode.EnableAutoSize | AutoSizeMode.EnableStretch;
		/// <summary>
		/// Flags for autosize and stretch
		/// </summary>
		public AutoSizeMode AutoSizeMode
		{
			get{return m_AutoSizeMode;}
			set{m_AutoSizeMode = value;}
		}

		#region RowInfoCollection
		/// <summary>
		/// Collection of RowInfo
		/// </summary>
		public class RowInfoCollection : ICollection
		{
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="p_grid"></param>
			public RowInfoCollection(GridVirtual p_grid)
			{
				m_Grid = p_grid;
			}

			private GridVirtual m_Grid;
			/// <summary>
			/// Attached Grid
			/// </summary>
			public GridVirtual Grid
			{
				get{return m_Grid;}
			}

			private ArrayList m_List = new ArrayList();
			#region Comparer
			private RowInfoTopComparer m_Comparer = new RowInfoTopComparer();
			public class RowInfoTopComparer : IComparer
			{
				public System.Int32 Compare ( System.Object x , System.Object y )
				{
					return ((RowInfo)x).Top.CompareTo( ((RowInfo)y).Top);
				}
			}
			#endregion

			/// <summary>
			/// Calculate the Row that have the Top value smaller or equal than the point p_Y, or -1 if not found found. ExactMatch = false
			/// </summary>
			/// <param name="p_Y">Absolute point to search</param>
			/// <returns></returns>
			public int RowAtPoint(int p_Y)
			{
				return RowAtPoint(p_Y, false);
			}
			/// <summary>
			/// Calculate the Row that have the Top value smaller or equal than the point p_Y, or -1 if not found found.
			/// </summary>
			/// <param name="p_Y">Y Coordinate to search for a row</param>
			/// <param name="p_ExactMatch">True to returns only exact position. For example if you use a point outside the range and this value is true no row is returned otherwise the nearest row is returned.</param>
			/// <returns></returns>
			public int RowAtPoint(int p_Y,bool p_ExactMatch)
			{
				//Restituisce la righa con il Left uguale a quello passato o la righa con il Left minore a quallo passato.
				// o -1 se tutte le righe hanno il Left maggiore
				int l_IndexFound;

				RowInfo l_Find = new RowInfo(null);
				l_Find.m_Top = p_Y;
				int l_ObjFound = m_List.BinarySearch(l_Find,m_Comparer);
				if (l_ObjFound>=0) //trovato il valore uguale
					l_IndexFound = l_ObjFound;
				else
				{
					l_ObjFound = ~l_ObjFound; //bitwise operator to return the nearest index
					if (l_ObjFound<=0)
						l_IndexFound = -1; //nessuna righa compatibile
					else if (l_ObjFound <= m_List.Count)
						l_IndexFound = l_ObjFound-1; //trovata una righa compatibile
					else
						l_IndexFound = -1; //non dovrebbe mai capitare
				}

				//se è stato richiesto un exactMatch verifico che il punto sia compreso tra il minimo e il massimo
				if (p_ExactMatch && l_IndexFound>=0)
				{
					if (p_Y > Bottom ||
						p_Y < Top)
						l_IndexFound = -1;
				}

				return l_IndexFound;
			}

			/// <summary>
			/// Returns true if the range passed is valid
			/// </summary>
			/// <param name="p_StartIndex"></param>
			/// <param name="p_Count"></param>
			/// <returns></returns>
			public bool IsValidRange(int p_StartIndex, int p_Count)
			{
				if (p_StartIndex < Count && p_StartIndex >= 0 &&
					p_Count > 0 && (p_StartIndex+p_Count) <= Count)
					return true;
				else
					return false;
			}

			/// <summary>
			/// Returns true if the range passed is valid for insert method
			/// </summary>
			/// <param name="p_StartIndex"></param>
			/// <param name="p_Count"></param>
			/// <returns></returns>
			public bool IsValidRangeForInsert(int p_StartIndex, int p_Count)
			{
				if (p_StartIndex <= Count && p_StartIndex >= 0 &&
					p_Count > 0)
					return true;
				else
					return false;
			}


			#region Insert/Remove Methods

			/// <summary>
			/// Insert a row at the specified position using the specified cells
			/// </summary>
			/// <param name="p_Index"></param>
			/// <param name="p_Cells">The new row values</param>
			public void Insert(int p_Index, params Cells.ICellVirtual[] p_Cells)
			{
				Insert(p_Index);

				this[p_Index].SetCells(p_Cells);
			}

			/// <summary>
			/// Insert a row at the specified position
			/// </summary>
			/// <param name="p_Index"></param>
			public void Insert(int p_Index)
			{
				InsertRange(p_Index, 1);
			}

			/// <summary>
			/// Remove a row at the speicifed position
			/// </summary>
			/// <param name="p_Index"></param>
			public void Remove(int p_Index)
			{
				RemoveRange(p_Index, 1);
			}


			/// <summary>
			/// Insert the specified number of rows at the specified position
			/// </summary>
			/// <param name="p_StartIndex"></param>
			/// <param name="p_Count"></param>
			public void InsertRange(int p_StartIndex, int p_Count)
			{
				if (IsValidRangeForInsert(p_StartIndex, p_Count)==false)
					throw new SourceGridException("Invalid index");

				//TODO si potrebbe ottimizzare aumentando la capacity
				for (int r = 0; r < p_Count; r++)
				{
					m_List.Insert(p_StartIndex+r, new RowInfo(m_Grid));
				}

				if (AutoCalculateTop)
					CalculateTop(p_StartIndex);

				OnRowsAdded(new IndexRangeEventArgs(p_StartIndex, p_Count));
			}

			/// <summary>
			/// Remove the RowInfo at the specified positions
			/// </summary>
			/// <param name="p_StartIndex"></param>
			/// <param name="p_Count"></param>
			public void RemoveRange(int p_StartIndex, int p_Count)
			{
				if (IsValidRange(p_StartIndex, p_Count)==false)
					throw new SourceGridException("Invalid index");

				//azzero le informazioni legate alla griglia
				for (int r = p_StartIndex; r < p_StartIndex+p_Count; r++)
				{
					this[r].m_Grid = null;
					//this[r].m_Index = -1;
				}

				m_List.RemoveRange(p_StartIndex, p_Count);

				if (AutoCalculateTop)
					CalculateTop(p_StartIndex);

				OnRowsRemoved(new IndexRangeEventArgs(p_StartIndex, p_Count));
			}

			#endregion

			/// <summary>
			/// Move a row from one position to another position
			/// </summary>
			/// <param name="p_CurrentRowPosition"></param>
			/// <param name="p_NewRowPosition"></param>
			public void Move(int p_CurrentRowPosition, int p_NewRowPosition)
			{
				if (p_CurrentRowPosition == p_NewRowPosition)
					return;

				int l_RowMin, l_RowMax;
				if (p_CurrentRowPosition < p_NewRowPosition)
				{
					l_RowMin = p_CurrentRowPosition;
					l_RowMax = p_NewRowPosition;
				}
				else
				{
					l_RowMin = p_NewRowPosition;
					l_RowMax = p_CurrentRowPosition;
				}

				for (int r = l_RowMin; r < l_RowMax; r++)
				{
					Swap(r, r + 1);
				}
			}

			/// <summary>
			/// Change the position of row 1 with row 2.
			/// </summary>
			/// <param name="p_RowIndex1"></param>
			/// <param name="p_RowIndex2"></param>
			public void Swap(int p_RowIndex1, int p_RowIndex2)
			{
				if (p_RowIndex1 == p_RowIndex2)
					return;

				RowInfo l_Row1 = this[p_RowIndex1];
				Cells.ICellVirtual[] l_Cells1 = l_Row1.GetCells();
				RowInfo l_Row2 = this[p_RowIndex2];
				Cells.ICellVirtual[] l_Cells2 = l_Row2.GetCells();

				m_List[p_RowIndex1] = l_Row2;
				m_List[p_RowIndex2] = l_Row1;

				l_Row1.SetCells(new Cells.ICellVirtual[l_Cells1.Length]);
				l_Row2.SetCells(new Cells.ICellVirtual[l_Cells1.Length]);
				l_Row1.SetCells(l_Cells1);
				l_Row2.SetCells(l_Cells2);

				if (AutoCalculateTop)
					CalculateTop(0);
			}

			/// <summary>
			/// Fired when the number of rows change
			/// </summary>
			public event IndexRangeEventHandler RowsAdded;

			/// <summary>
			/// Fired when the number of rows change
			/// </summary>
			/// <param name="e"></param>
			protected virtual void OnRowsAdded(IndexRangeEventArgs e)
			{
				if (RowsAdded!=null)
					RowsAdded(this, e);
			}

			/// <summary>
			/// Fired when the number of columns change
			/// </summary>
			public event IndexRangeEventHandler RowsRemoved;

			/// <summary>
			/// Fired when the number of columns change
			/// </summary>
			/// <param name="e"></param>
			protected virtual void OnRowsRemoved(IndexRangeEventArgs e)
			{
				if (RowsRemoved!=null)
					RowsRemoved(this, e);
			}

			/// <summary>
			/// Indexer. Returns a RowInfo at the specified position
			/// </summary>
			public RowInfo this[int p]
			{
				get{return (RowInfo)m_List[p];}
			}

			/// <summary>
			/// Recalculate all the top positions from the specified index
			/// </summary>
			/// <param name="p_StartIndex"></param>
			public void CalculateTop(int p_StartIndex)
			{
				if (Count > 0)
				{
					int l_CurrentTop = 0;
					if (p_StartIndex != 0)
						l_CurrentTop = this[p_StartIndex-1].Top+this[p_StartIndex-1].Height;

					for (int r = p_StartIndex; r < Count; r++)
					{
						this[r].m_Top = l_CurrentTop;
						l_CurrentTop += this[r].m_Height;
					}
				}
			}


			/// <summary>
			/// Returns the maximum bottom value of the rows. Calculated with Rows[lastRow].Bottom or 0 if no rows are presents.
			/// </summary>
			public int Bottom
			{
				get
				{
					if (Count <= 0)
						return 0;
					else
						return this[Count-1].Bottom;
				}
			}

			/// <summary>
			/// Returns the minimum top value of the rows. Calculated with Rows[0].Top or 0 if no rows are presents.
			/// </summary>
			public int Top
			{
				get
				{
					if (Count <= 0)
						return 0;
					else
						return this[0].Top;
				}
			}

			/// <summary>
			/// Fired when the user change the Height property of one of the Row
			/// </summary>
			public event RowInfoEventHandler RowHeightChanged;

			/// <summary>
			/// Execute the RowHeightChanged event
			/// </summary>
			/// <param name="e"></param>
			public void InvokeRowHeightChanged(RowInfoEventArgs e)
			{
				if (AutoCalculateTop)
					CalculateTop(e.Row.Index);

				if (RowHeightChanged!=null)
					RowHeightChanged(this, e);
			}

			private bool m_bAutoCalculateTop = true;
			/// <summary>
			/// Indicates if auto recalculate top position when height value change. Default = true. Can be used when you need to change many times Height value for example for an AutoSize operation to increase performance.
			/// </summary>
			public bool AutoCalculateTop
			{
				get{return m_bAutoCalculateTop;}
				set
				{
					m_bAutoCalculateTop = value;
					if (m_bAutoCalculateTop)
						CalculateTop(0);
				}
			}

			public int IndexOf(RowInfo p_Info)
			{
				return m_List.IndexOf(p_Info);
			}

			#region ICollection
			public virtual void CopyTo ( System.Array array , System.Int32 index )
			{
				m_List.CopyTo(array,index);
			}
			public int Count
			{
				get{return m_List.Count;}
			}
			public bool IsSynchronized
			{
				get{return m_List.IsSynchronized;}
			}
			public object SyncRoot
			{
				get{return m_List.SyncRoot;}
			}
			public virtual System.Collections.IEnumerator GetEnumerator (  )
			{
				return m_List.GetEnumerator();
			}
			#endregion
		}

		#endregion
	}
}
