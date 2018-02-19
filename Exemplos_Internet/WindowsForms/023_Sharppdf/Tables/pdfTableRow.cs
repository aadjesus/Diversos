using System;
using System.Collections;

using sharpPDF.Collections;
using sharpPDF.Elements;
using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF.Tables
{
	/// <summary>
	/// A Class that implements an abstract table's row object
	/// </summary>
	public class pdfTableRow : IEnumerable, ISeparable, ICloneable
	{

		#region Protected_Properties

		/// <summary>
		/// Columns of the row
		/// </summary>
		protected columnList _cols = new columnList();
		/// <summary>
		/// Height of the row
		/// </summary>
		protected int _rowHeight = 0;
		/// <summary>
		/// Width of the row
		/// </summary>
		protected int _rowWidth = 0;
		/// <summary>
		/// Style of the row
		/// </summary>
		protected pdfTableStyle _rowStyle = null;
		/// <summary>
		/// Container table
		/// </summary>		
		protected pdfTable _containerTable = null;
		/// <summary>
		/// Vertical alignment of the row
		/// </summary>
		protected predefinedVerticalAlignment _rowVerticalAlignment;
		/// <summary>
		/// Start X position of the row
		/// </summary>
		protected int _startX = 0;
		/// <summary>
		/// Start Y position of the row
		/// </summary>
		protected int _startY = 0;

		#endregion

		#region ~Ctor

		/// <summary>
		/// Class's constructor
		/// </summary>
		internal pdfTableRow(pdfTable containerTable)
		{
			_containerTable = containerTable;
			_rowStyle = containerTable.GetCurrentStyle();
			_rowVerticalAlignment = predefinedVerticalAlignment.csMiddle;
			
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="tableHeader">Row Template based on Table's Header</param>
		internal pdfTableRow(pdfTableHeader tableHeader):this(tableHeader.containerTable)
		{
			foreach(pdfTableColumn Col in tableHeader)
			{
				_cols.Add(new pdfTableColumn(this, Col.columnWidth, Col.columnAlign, Col.columnVerticalAlign, _rowStyle));
				_cols[_cols.Count - 1].ColumnChanged += new columnTableEventHandler(columnChanged);
			}
			_rowWidth = tableHeader.rowWidth;
			_rowHeight = this._containerTable.cellpadding * 2;
		}

		#endregion

		#region Internal_Properties

		/// <summary>
		/// Table that contains the row
		/// </summary>
		internal pdfTable containerTable
		{
			get
			{
				return _containerTable;
			}
		}
		
		/// <summary>
		/// Start X position of the row
		/// </summary>
		internal int startX
		{
			get
			{
				return _startX;
			}
			set
			{
				_startX = value;
			}
		}

		/// <summary>
		/// Start Y position of the row
		/// </summary>
		internal int startY
		{
			get
			{
				return _startY;
			}
			set
			{
				_startY = value;
			}
		}

		#endregion

		#region Public_Properties

		/// <summary>
		/// Indexer of the pdfTableRow that represents its columns
		/// </summary>
		public pdfTableColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= _cols.Count) {
					throw new pdfBadColumnIndexException();
				} else {
					return _cols[index];
				}
			}
		}

		/// <summary>
		/// The number of columns
		/// </summary>
		public int columnsCount
		{	
			get
			{
				return _cols.Count;
			}
		}

		/// <summary>
		/// Row's height
		/// </summary>
		public int rowHeight
		{
			get
			{
				return _rowHeight;
			}
			set
			{				
				_rowHeight = Math.Max(this._rowHeight, value);
			}
		}

		/// <summary>
		/// Row's width
		/// </summary>
		public int rowWidth
		{
			get
			{				
				return _rowWidth;
			}
		}

		/// <summary>
		/// Row's style
		/// </summary>
		public pdfTableStyle rowStyle
		{
			get
			{
				return _rowStyle;
			}
			set
			{
				_rowStyle = value;
				foreach (pdfTableColumn Col in this._cols)
				{
					Col.columnStyle = value;
				}
			}
		}

		/// <summary>
		/// Row's Vertical Alignment
		/// </summary>
		public predefinedVerticalAlignment rowVerticalAlign
		{
			get
			{
				return this._rowVerticalAlignment;
			}
			set
			{
				this._rowVerticalAlignment = value;
				foreach (pdfTableColumn Col in this._cols)
				{
					Col.columnVerticalAlign = value;
				}
			}
		}				

		/// <summary>
		/// Enumerator of the column's collection
		/// </summary>
		/// <returns>A IEnumerator of the indexed property</returns>
		public IEnumerator GetEnumerator()
		{
			return _cols.GetEnumerator();
		}

		#endregion

		#region Internal_Methods

		internal void addColumn(pdfTableColumn column)
		{
			_cols.Add(column);
			column.ColumnChanged += new columnTableEventHandler(columnChanged);
			_rowWidth = _containerTable.tableHeader.rowWidth;
		}

		internal int calculateWidth()
		{
			int result = 0;
			result = (_containerTable.cellpadding * _cols.Count * 2);
			foreach(pdfTableColumn Col in _cols)
			{
				result += Col.columnWidth;
			}
			return result;
		}

		internal int calculateHeight()
		{
			int result = 0;
			foreach(pdfTableColumn Col in _cols)
			{
				result = Math.Max(result, Col.columnHeight + (_containerTable.cellpadding * 2));
			}
			return Math.Max(result, _rowHeight);
		}

		#endregion

		#region ISeparable

		/// <summary>
		/// Method that returns the basic components of the pdfTableRom
		/// </summary>
		/// <returns>Collection of pdfElements</returns>
		public elementList GetBasicElements()
		{
			elementList ReturnedElements = new elementList();
			ReturnedElements.Add(new rectangleElement(_startX, _startY, _startX + _rowWidth , _startY - _rowHeight, _containerTable.borderColor, _rowStyle.bgColor, _containerTable.borderSize));
			foreach(pdfTableColumn Col in _cols)
			{	
				Col.startX = _startX + _containerTable.cellpadding;
				Col.startY = _startY - _containerTable.cellpadding;
				ReturnedElements.AddRange(Col.GetBasicElements());
				_startX += Col.columnWidth + (_containerTable.cellpadding * 2);
				if (_cols.IndexOf(Col) < (_cols.Count - 1)) {
					ReturnedElements.Add(new lineElement(_startX, _startY, _startX,  _startY - this.rowHeight, _containerTable.borderSize, _containerTable.borderColor));					
				}
				Col.startX = 0;
				Col.startY = 0;
			}
			return ReturnedElements;
		}

		#endregion

		#region Protected_Methods

		/// <summary>
		/// Method that change the row's height after the changing of a column
		/// </summary>
		/// <param name="sender">Column that generates the event</param>
		/// <param name="e">Arguments of the event</param>
		protected void columnChanged(object sender, columnTableEventArgs e)
		{
			this._rowHeight = Math.Max(this._rowHeight, (e.Column.columnHeight + _containerTable.cellpadding * 2));
		}

		#endregion

		#region ICloneable

		/// <summary>
		/// Method that clones the pdfTableRow object
		/// </summary>
		/// <returns>Cloned Row</returns>
		public virtual object Clone() 
		{
			pdfTableRow ReturnedRow = new pdfTableRow(_containerTable);
			ReturnedRow.rowStyle = new pdfTableStyle(this._rowStyle.fontReference, this._rowStyle.fontSize, this._rowStyle.fontColor, this._rowStyle.bgColor);
			ReturnedRow.rowVerticalAlign = this._rowVerticalAlignment;
			foreach(pdfTableColumn Col in _cols) 
			{
				ReturnedRow._cols.Add((pdfTableColumn)Col.Clone());				
			}
			ReturnedRow.rowHeight = this._rowHeight;
			ReturnedRow._rowWidth = this._rowWidth;
			return ReturnedRow;
		}

		#endregion

	}
}
