using System;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF.Tables
{
	/// <summary>
	/// A Class that implements an abstract table's header object
	/// </summary>
	public class pdfTableHeader : pdfTableRow
	{

		/// <summary>
		/// Event generated when a column is added to the table's header
		/// </summary>
		internal event columnTableEventHandler ColumnAdded;
		
		/// <summary>
		/// Property that tells if the pdfTableHeader has to be showed
		/// </summary>
		protected bool _visible;

		/// <summary>
		/// Property that tells if the pdfTableHeader has to be showed
		/// </summary>
		public bool visible
		{
			get
			{
				return _visible;
			}
			set
			{
				_visible = value;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		internal pdfTableHeader(pdfTable Table, pdfTableStyle headerStyle):base(Table)
		{
			_visible = true;
			_rowHeight = Table.cellpadding * 2;
			_rowWidth = Table.cellpadding * 2;
			_rowStyle = headerStyle;
		}

		/// <summary>
		/// Method that adds a new column
		/// </summary>
		/// <param name="columnWidth">Width of the column</param>
		public void addColumn(int columnWidth)
		{
			this.addColumn(columnWidth, predefinedAlignment.csLeft, predefinedVerticalAlignment.csMiddle);
		}

		/// <summary>
		/// Method that adds a new column
		/// </summary>
		/// <param name="columnWidth">Width of the column</param>
		/// <param name="columnAlign">Alignment of the column</param>
		public void addColumn(int columnWidth, predefinedAlignment columnAlign)
		{
			this.addColumn(columnWidth, columnAlign, predefinedVerticalAlignment.csMiddle);
		}

		/// <summary>
		/// Method that adds a new column
		/// </summary>
		/// <param name="columnWidth">Width of the column</param>
		/// <param name="columnAlign">Alignment of the column</param>
		/// <param name="columnVerticalAlign">Vertical alignment of the column</param>
		public void addColumn(int columnWidth, predefinedAlignment columnAlign, predefinedVerticalAlignment columnVerticalAlign)
		{
			_cols.Add(new pdfTableColumn(this, columnWidth - (_containerTable.cellpadding * 2), columnAlign, columnVerticalAlign, _rowStyle));			
			_cols[_cols.Count - 1].ColumnChanged += new columnTableEventHandler(columnChanged);
			this._rowWidth = this.calculateWidth();
			if (this.ColumnAdded != null) {
				this.ColumnAdded(this, new columnTableEventArgs(_cols[_cols.Count-1]));
			}
		}
		
		/// <summary>
		/// Method that clones the pdfTableHeader Object
		/// </summary>
		/// <returns>Cloned Object</returns>
		public override object Clone() {
			pdfTableHeader ReturnedRow = new pdfTableHeader(_containerTable, new pdfTableStyle(this._rowStyle.fontReference, this._rowStyle.fontSize, this._rowStyle.fontColor, this._rowStyle.bgColor));
			ReturnedRow.rowVerticalAlign = this._rowVerticalAlignment;			
			ReturnedRow.visible = this._visible;
			foreach(pdfTableColumn Col in _cols) 
			{
				ReturnedRow._cols.Add((pdfTableColumn)Col.Clone());				
			}
			ReturnedRow.rowHeight = this._rowHeight;
			ReturnedRow._rowWidth = this._rowWidth;
			return ReturnedRow;
		}

	}
}
