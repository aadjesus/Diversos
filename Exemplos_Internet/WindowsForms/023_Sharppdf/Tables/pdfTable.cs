using System;
using System.Collections;

using sharpPDF.Collections;
using sharpPDF.Elements;
using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.PDFControls;

namespace sharpPDF.Tables
{
	/// <summary>
	/// A Class that implements an abstract table object
	/// </summary>
	public class pdfTable : pdfPositionableObject, IEnumerable, ISeparable
	{
		#region Protected_Properties

		/// <summary>
		/// Container document
		/// </summary>
		protected pdfDocument _containerDocument;
		/// <summary>
		/// Table's header
		/// </summary>
		protected pdfTableHeader _tableHeader;
		/// <summary>
		/// Table's row style
		/// </summary>
		protected pdfTableStyle _rowStyle;
		/// <summary>
		/// Table's alternate row style
		/// </summary>
		protected pdfTableStyle _alternateRowStyle;
		/// <summary>
		/// Variable that sets the current row style
		/// </summary>
		protected bool _isAlternateStyle = false;
		/// <summary>
		/// Table's rows
		/// </summary>
		protected rowList _rows = new rowList();
		/// <summary>
		/// Table's border size
		/// </summary>
		protected int _borderSize;
		/// <summary>
		/// Table's border color
		/// </summary>
		protected pdfColor _borderColor;
		/// <summary>
		/// Table's cellpadding
		/// </summary>
		protected int _cellpadding;

		#endregion

		#region Public_Properties

		/// <summary>
		/// Border's Size
		/// </summary>
		public int borderSize
		{
			get
			{
				return _borderSize;
			}
			set
			{
				_borderSize = value;
			}
		}

		/// <summary>
		/// Border's color
		/// </summary>
		public pdfColor borderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				_borderColor = value;
			}
		}

		/// <summary>
		/// Table's header
		/// </summary>
		public pdfTableHeader tableHeader
		{
			get
			{
				return _tableHeader;
			}
		}

		/// <summary>
		/// The style of a table's row
		/// </summary>
		public pdfTableStyle rowStyle
		{
			get
			{
				return _rowStyle;
			}
		}

		/// <summary>
		/// The alternate style of a table's row
		/// </summary>
		public pdfTableStyle alternateRowStyle
		{
			get
			{
				return _alternateRowStyle;
			}
		}

		/// <summary>
		/// The cellpadding of the table
		/// </summary>
		public int cellpadding
		{
			get
			{
				return _cellpadding;
			}
		}

		/// <summary>
		/// Table's rows
		/// </summary>
		public pdfTableRow this[int index]
		{
			get
			{
				if (index < 0 || index >= _rows.Count) {
					throw new pdfBadRowIndexException();
				} else {
					return _rows[index];
				}
			}
		}
		
		/// <summary>
		/// The number of rows
		/// </summary>
		public int rowsCount
		{
			get
			{
				return _rows.Count;
			}
		}

		#endregion

		#region Internal_Properties

		internal pdfDocument containerDocument
		{
			get
			{
				return _containerDocument;
			}
		}

		#endregion

		#region ~Ctor

		/// <summary>
		/// Class's Constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		public pdfTable(pdfDocument containerDocument):this(containerDocument, 1, pdfColor.Black, 5, new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White))
		{

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		/// <param name="border">Width of the border</param>
		/// <param name="borderColor">Color of the border</param>
		public pdfTable(pdfDocument containerDocument, int border, pdfColor borderColor):this(containerDocument, border, borderColor, 5, new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White))
		{

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		/// <param name="cellpadding">Cellpadding of the table</param>
		public pdfTable(pdfDocument containerDocument, int cellpadding):this(containerDocument, 1, pdfColor.Black, cellpadding, new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White))
		{

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		/// <param name="border">Width of the border</param>
		/// <param name="borderColor">Color of the border</param>
		/// <param name="cellpadding">Cellpadding of the table</param>
		public pdfTable(pdfDocument containerDocument, int border, pdfColor borderColor, int cellpadding):this(containerDocument, border, borderColor, cellpadding, new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White))
		{

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		/// <param name="border">Width of the border</param>
		/// <param name="borderColor">Color of the border</param>
		/// <param name="cellpadding">Cellpadding of the table</param>
		/// <param name="headerStyle">Style of the header</param>
		public pdfTable(pdfDocument containerDocument, int border, pdfColor borderColor, int cellpadding, pdfTableStyle headerStyle):this(containerDocument, border, borderColor, cellpadding, headerStyle, new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White), new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White))
		{

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		/// <param name="border">Width of the border</param>
		/// <param name="borderColor">Color of the border</param>
		/// <param name="cellpadding">Cellpadding of the table</param>
		/// <param name="headerStyle">Style of the header</param>
		/// <param name="rowStyle">Style of the rows</param>
		public pdfTable(pdfDocument containerDocument, int border, pdfColor borderColor, int cellpadding, pdfTableStyle headerStyle, pdfTableStyle rowStyle):this(containerDocument, border, borderColor, cellpadding, headerStyle, rowStyle, new pdfTableStyle(containerDocument.getFontReference(predefinedFont.csHelvetica), 10, pdfColor.Black, pdfColor.White))
		{

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="containerDocument">Container of the table</param>
		/// <param name="border">Width of the border</param>
		/// <param name="borderColor">Color of the border</param>
		/// <param name="cellpadding">Cellpadding of the table</param>
		/// <param name="headerStyle">Style of the header</param>
		/// <param name="rowStyle">Style of the rows</param>
		/// <param name="alternateRowStyle">Alternate style of the rows</param>
		public pdfTable(pdfDocument containerDocument, int border, pdfColor borderColor, int cellpadding, pdfTableStyle headerStyle, pdfTableStyle rowStyle, pdfTableStyle alternateRowStyle)
		{
			_containerDocument = containerDocument;
			_tableHeader = new pdfTableHeader(this, headerStyle);
			_tableHeader.ColumnAdded += new columnTableEventHandler(columnAdded);
			_rowStyle = rowStyle;
			_alternateRowStyle = alternateRowStyle;
			_borderSize = border;
			_borderColor = borderColor;
			_cellpadding = cellpadding;
		}

		#endregion

		#region Public_Methods

		/// <summary>
		/// Method that creates a new row
		/// </summary>
		/// <returns>A new pdfTableRowObject</returns>
		public pdfTableRow createRow()
		{
			return new pdfTableRow(_tableHeader);
		}


		/// <summary>
		/// Method to add a new row into the table
		/// </summary>
		/// <param name="newRow">New row</param>
		public void addRow(pdfTableRow newRow)
		{
			_rows.Add(newRow);			
		}

		/// <summary>
		/// Enumerator for table's rows
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return _rows.GetEnumerator();
		}		
		
		/// <summary>
		/// Method that returns the basic elements of the table
		/// </summary>
		/// <returns>Collection of basic elements</returns>
		public elementList GetBasicElements()
		{
			int startX = _coordX;
			int startY = _coordY;
			elementList ReturnedElements = new elementList();
			if (_tableHeader.visible) 
			{
				this._tableHeader.startX = startX;
				this._tableHeader.startY = startY;
				ReturnedElements.AddRange(this._tableHeader.GetBasicElements());
				this._tableHeader.startX = 0;
				this._tableHeader.startY = 0;
			}
			startY -= _tableHeader.rowHeight;
			foreach(pdfTableRow Row in _rows)
			{
				Row.startX = startX;
				Row.startY = startY;
				ReturnedElements.AddRange(Row.GetBasicElements());
				Row.startX = 0;
				Row.startY = 0;
				startY -= Row.rowHeight;
			}			
			return ReturnedElements;
		}

		/// <summary>
		/// Method that crop the table at the defined table's height
		/// </summary>
		/// <param name="tabHeight">Maximum height of the table</param>
		/// <returns>Table outside the maximum height</returns>
		public pdfTable CropTable(int tabHeight)
		{
			int rowIndex = 0;
			int currentHeight = 0;
			bool finished = false;
			pdfTable ResultTable = null;
			if (_tableHeader.visible) 
			{
				tabHeight -= tableHeader.rowHeight;
			}
			while (rowIndex < _rows.Count && currentHeight <= tabHeight && !finished)
			{				
				if (_rows[rowIndex].rowHeight > tabHeight) {
					throw new pdfBadRowHeightException();
				} else if (_rows[rowIndex].rowHeight <= tabHeight - currentHeight) {
					currentHeight += _rows[rowIndex].rowHeight;
					rowIndex++;
				} else {
					finished = true;
				}
			}
			if (rowIndex < _rows.Count) {
				ResultTable = new pdfTable(_containerDocument);
				ResultTable._borderColor = _borderColor;
				ResultTable._borderSize = _borderSize;
				ResultTable._cellpadding = _cellpadding;
				ResultTable._rowStyle = _rowStyle;
				ResultTable._tableHeader = (pdfTableHeader)this._tableHeader.Clone();				
				ResultTable._rowStyle = this._rowStyle;
				ResultTable._alternateRowStyle = this._alternateRowStyle;
				while (rowIndex < _rows.Count) 
				{
					ResultTable._rows.Add(_rows[rowIndex]);
					_rows.RemoveAt(rowIndex);
				}
			}
			return ResultTable;
		}

		#endregion

		#region Internal_Methods

		internal pdfTableStyle GetCurrentStyle()
		{			
			if (this._isAlternateStyle && this._alternateRowStyle != null) {
				this._isAlternateStyle = false;
				return this._alternateRowStyle;				
			} else {
				this._isAlternateStyle = true;
				return this._rowStyle;				
			}
		}					

		#endregion

		#region Protected_Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void columnAdded(object sender, columnTableEventArgs e)
		{
			foreach (pdfTableRow myRow in _rows)
			{
				myRow.addColumn(new pdfTableColumn(myRow, e.Column.columnWidth, e.Column.columnAlign, e.Column.columnVerticalAlign, myRow.rowStyle));				
			}
		}

		#endregion

	}
}
