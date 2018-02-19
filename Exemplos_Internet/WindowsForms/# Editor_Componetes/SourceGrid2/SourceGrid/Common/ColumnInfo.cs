using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace SourceGrid2
{
	/// <summary>
	/// Column Information
	/// </summary>
	public class ColumnInfo
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Grid"></param>
		private ColumnInfo(GridVirtual p_Grid)
		{
			m_Grid = p_Grid;
		}

		private int m_Width = Utility.DefaultCellWidth;
		/// <summary>
		/// Width of the current Column
		/// </summary>
		public int Width
		{
			get{return m_Width;}
			set
			{
				if (value<=0)
					value=1;

				if (m_Width != value)
				{
					m_Width = value;
					if (m_Grid!=null)
						m_Grid.Columns.InvokeColumnWidthChanged(new ColumnInfoEventArgs(this));
				}
			}
		}

		private int m_Left;
		/// <summary>
		/// Left absolute position of the current Column
		/// </summary>
		public int Left
		{
			get{return m_Left;}
		}

		/// <summary>
		/// Right of the column (Left+Width)
		/// </summary>
		public int Right
		{
			get{return Left+Width;}
		}
		//private int m_Index;
		/// <summary>
		/// Index of the current Column
		/// </summary>
		public int Index
		{
			get{return m_Grid.Columns.IndexOf(this);}
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
		/// Returns all the cells at current column position
		/// </summary>
		/// <returns></returns>
		public Cells.ICellVirtual[] GetCells()
		{
			if (m_Grid == null)
				throw new SourceGridException("Invalid Grid object");

			int l_CurrentCol = Index;

			Cells.ICellVirtual[] l_Cells = new Cells.ICellVirtual[m_Grid.Rows.Count];
			for (int r = 0; r < m_Grid.Rows.Count; r++)
				l_Cells[r] = m_Grid.GetCell(r, l_CurrentCol);

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
				int l_CurrentCol = Index;

				for (int r = 0; r < p_Cells.Length; r++)
					m_Grid.SetCell(r, l_CurrentCol, p_Cells[r]);
			}
		}

		/// <summary>
		/// Move the Focus to the first cell not fixed of the current row
		/// </summary>
		/// <returns></returns>
		public bool Focus()
		{
			if (Grid.ColumnsCount > Index && Grid.RowsCount > Grid.FixedRows)
				return Grid.SetFocusCell(new Position(Grid.FixedRows, Index));
			else
				return false;
		}

		/// <summary>
		/// Gets or sets if the current row is selected. If only a column of the row is selected this property returns true.
		/// </summary>
		public bool Select
		{
			get{return Grid.Selection.ContainsColumn(Index);}
			set
			{
				if (Grid.ColumnsCount > Index && Grid.RowsCount > 0)
				{
					if (value)
						Grid.Selection.AddRange(new Range(0, Index, Grid.RowsCount-1, Index));
					else
						Grid.Selection.RemoveRange(new Range(0, Index, Grid.RowsCount-1, Index));
				}
			}
		}


		private object m_Tag;
		/// <summary>
		/// A property that the user can use to insert custom informations associated to a specific column
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

		#region ColumnInfoCollection
		/// <summary>
		/// Collection of ColumnInfo
		/// </summary>
		public class ColumnInfoCollection : ICollection
		{
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="p_grid"></param>
			public ColumnInfoCollection(GridVirtual p_grid)
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
			private ColumnInfoLeftComparer m_Comparer = new ColumnInfoLeftComparer();
			public class ColumnInfoLeftComparer : IComparer
			{
				public System.Int32 Compare ( System.Object x , System.Object y )
				{
					return ((ColumnInfo)x).Left.CompareTo( ((ColumnInfo)y).Left);
				}
			}
			#endregion

			/// <summary>
			/// Calculate the Column that have the Left value smaller or equal than the point p_X, or -1 if not found found. ExactMatch = false
			/// </summary>
			/// <param name="p_X">Absolute point to search</param>
			/// <returns></returns>
			public int ColumnAtPoint(int p_X)
			{
				return ColumnAtPoint(p_X, false);
			}
			/// <summary>
			/// Calculate the Column that have the Left value smaller or equal than the point p_X, or -1 if not found found.
			/// </summary>
			/// <param name="p_X">X Coordinate to search for a column</param>
			/// <param name="p_ExactMatch">True to returns only exact position. For example if you use a point outside the range and this value is true no column is returned otherwise the nearest column is returned.</param>
			/// <returns></returns>
			public int ColumnAtPoint(int p_X,bool p_ExactMatch)
			{
				//Restituisce la righa con il Left uguale a quello passato o la righa con il Left minore a quallo passato.
				// o -1 se tutte le righe hanno il Left maggiore
				int l_IndexFound;

				ColumnInfo l_Find = new ColumnInfo(null);
				l_Find.m_Left = p_X;
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
					if (p_X > Right ||
						p_X < Left)
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
			/// Insert a column at the specified position using the specified cells
			/// </summary>
			/// <param name="p_Index"></param>
			/// <param name="p_Cells">The new column values</param>
			public void Insert(int p_Index, params Cells.ICellVirtual[] p_Cells)
			{
				Insert(p_Index);

				this[p_Index].SetCells(p_Cells);
			}

			/// <summary>
			/// Insert a column at the specified position
			/// </summary>
			/// <param name="p_Index"></param>
			public void Insert(int p_Index)
			{
				InsertRange(p_Index, 1);
			}

			/// <summary>
			/// Remove a column at the speicifed position
			/// </summary>
			/// <param name="p_Index"></param>
			public void Remove(int p_Index)
			{
				RemoveRange(p_Index, 1);
			}

			/// <summary>
			/// Insert the specified number of Columns at the specified position
			/// </summary>
			/// <param name="p_StartIndex"></param>
			/// <param name="p_Count"></param>
			public void InsertRange(int p_StartIndex, int p_Count)
			{
				if (IsValidRangeForInsert(p_StartIndex, p_Count)==false)
					throw new SourceGridException("Invalid index");

				//TODO si potrebbe ottimizzare aumentando la capacity
				for (int c = 0; c < p_Count; c++)
				{
					m_List.Insert(p_StartIndex+c,new ColumnInfo(m_Grid));
				}

				if (AutoCalculateLeft)
					CalculateLeft(p_StartIndex);

				OnColumnsAdded(new IndexRangeEventArgs(p_StartIndex, p_Count));
			}

			/// <summary>
			/// Remove the ColumnInfo at the specified positions
			/// </summary>
			/// <param name="p_StartIndex"></param>
			/// <param name="p_Count"></param>
			public void RemoveRange(int p_StartIndex, int p_Count)
			{
				if (IsValidRange(p_StartIndex, p_Count)==false)
					throw new SourceGridException("Invalid index");

				//azzero le informazioni legate alla griglia
				for (int c = p_StartIndex; c < p_StartIndex+p_Count; c++)
				{
					this[c].m_Grid = null;
					//this[c].m_Index = -1;
				}

				m_List.RemoveRange(p_StartIndex, p_Count);

				if (AutoCalculateLeft)
					CalculateLeft(p_StartIndex);

				OnColumnsRemoved(new IndexRangeEventArgs(p_StartIndex, p_Count));
			}


			#endregion

			/// <summary>
			/// Move a column from one position to another position
			/// </summary>
			/// <param name="p_CurrentColumnPosition"></param>
			/// <param name="p_NewColumnPosition"></param>
			public void Move(int p_CurrentColumnPosition, int p_NewColumnPosition)
			{
				if (p_CurrentColumnPosition == p_NewColumnPosition)
					return;

				int l_ColumnMin, l_ColumnMax;
				if (p_CurrentColumnPosition < p_NewColumnPosition)
				{
					l_ColumnMin = p_CurrentColumnPosition;
					l_ColumnMax = p_NewColumnPosition;
				}
				else
				{
					l_ColumnMin = p_NewColumnPosition;
					l_ColumnMax = p_CurrentColumnPosition;
				}

				for (int r = l_ColumnMin; r < l_ColumnMax; r++)
				{
					Swap(r, r + 1);
				}
			}

			/// <summary>
			/// Change the position of column 1 with column 2.
			/// </summary>
			/// <param name="p_ColumnIndex1"></param>
			/// <param name="p_ColumnIndex2"></param>
			public void Swap(int p_ColumnIndex1, int p_ColumnIndex2)
			{
				if (p_ColumnIndex1 == p_ColumnIndex2)
					return;

				ColumnInfo l_Column1 = this[p_ColumnIndex1];
				Cells.ICellVirtual[] l_Cells1 = l_Column1.GetCells();
				ColumnInfo l_Column2 = this[p_ColumnIndex2];
				Cells.ICellVirtual[] l_Cells2 = l_Column2.GetCells();

				m_List[p_ColumnIndex1] = l_Column2;
				m_List[p_ColumnIndex2] = l_Column1;

				l_Column1.SetCells(new Cells.ICellVirtual[l_Cells1.Length]);
				l_Column2.SetCells(new Cells.ICellVirtual[l_Cells1.Length]);
				l_Column1.SetCells(l_Cells1);
				l_Column2.SetCells(l_Cells2);

				if (AutoCalculateLeft)
					CalculateLeft(0);
			}

			/// <summary>
			/// Fired when the number of columns change
			/// </summary>
			public event IndexRangeEventHandler ColumnsAdded;

			/// <summary>
			/// Fired when the number of columns change
			/// </summary>
			/// <param name="e"></param>
			protected virtual void OnColumnsAdded(IndexRangeEventArgs e)
			{
				if (ColumnsAdded!=null)
					ColumnsAdded(this, e);
			}

			/// <summary>
			/// Fired when the number of columns change
			/// </summary>
			public event IndexRangeEventHandler ColumnsRemoved;

			/// <summary>
			/// Fired when the number of columns change
			/// </summary>
			/// <param name="e"></param>
			protected virtual void OnColumnsRemoved(IndexRangeEventArgs e)
			{
				if (ColumnsRemoved!=null)
					ColumnsRemoved(this, e);
			}

			/// <summary>
			/// Indexer. Returns a ColumnInfo at the specified position
			/// </summary>
			public ColumnInfo this[int p]
			{
				get{return (ColumnInfo)m_List[p];}
			}

			/// <summary>
			/// Recalculate all the Left positions from the specified index
			/// </summary>
			/// <param name="p_StartIndex"></param>
			public void CalculateLeft(int p_StartIndex)
			{
				if (Count > 0)
				{
					int l_CurrentLeft = 0;
					if (p_StartIndex != 0)
						l_CurrentLeft = this[p_StartIndex-1].Left+this[p_StartIndex-1].Width;

					for (int c = p_StartIndex; c < Count; c++)
					{
						this[c].m_Left = l_CurrentLeft;
						l_CurrentLeft += this[c].m_Width;
					}
				}
			}

			/// <summary>
			/// Returns the maximum right value of the columns. Calculated with Columns[lastCol].Right or 0 if no columns are presents.
			/// </summary>
			public int Right
			{
				get
				{
					if (Count <= 0)
						return 0;
					else
						return this[Count-1].Right;
				}
			}
			/// <summary>
			/// Returns the minimum left value of the columns. Calculated with Columns[0].Left or 0 if no columns are presents.
			/// </summary>
			public int Left
			{
				get
				{
					if (Count <= 0)
						return 0;
					else
						return this[0].Left;
				}
			}
			/// <summary>
			/// Fired when the user change the Width property of one of the Column
			/// </summary>
			public event ColumnInfoEventHandler ColumnWidthChanged;

			/// <summary>
			/// Execute the RowHeightChanged event
			/// </summary>
			/// <param name="e"></param>
			public void InvokeColumnWidthChanged(ColumnInfoEventArgs e)
			{
				if (AutoCalculateLeft)
					CalculateLeft(e.Column.Index);

				if (ColumnWidthChanged!=null)
					ColumnWidthChanged(this, e);
			}

			private bool m_bAutoCalculateLeft = true;
			/// <summary>
			/// Indicates if auto recalculate left position when width value change. Default = true. Can be used when you need to change many Width value for example for an AutoSize operation to increase performance.
			/// </summary>
			public bool AutoCalculateLeft
			{
				get{return m_bAutoCalculateLeft;}
				set
				{
					m_bAutoCalculateLeft = value;
					if (m_bAutoCalculateLeft)
						CalculateLeft(0);
				}
			}

			public int IndexOf(ColumnInfo p_Info)
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
