using System;

using sharpPDF.Collections;
using sharpPDF.Elements;
using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.Fonts;

namespace sharpPDF.Tables
{
	/// <summary>
	/// A Class that implements an abstract table's column object
	/// </summary>
	public class pdfTableColumn : IRelativeContainer, ISeparable, ICloneable
	{

		/// <summary>
		/// Event generated when the content or the height of a column is changed
		/// </summary>
		internal event columnTableEventHandler ColumnChanged;

		#region Protected_Properties
		
		/// <summary>
		/// Current X position inside the column
		/// </summary>
		protected int _currentX = 0;
		/// <summary>
		/// Current Y position inside the column
		/// </summary>
		protected int _currentY = 0;
		/// <summary>
		/// Column's width
		/// </summary>
		protected int _columnWidth = 0;
		/// <summary>
		/// Column's height
		/// </summary>
		protected int _columnHeight = 0;
		/// <summary>
		/// Column's Alignment
		/// </summary>
		protected predefinedAlignment _columnAlign;
		/// <summary>
		/// Column's Vertical Alignment
		/// </summary>
		protected predefinedVerticalAlignment _columnVerticalAlign;
		/// <summary>
		/// Column's Style
		/// </summary>
		protected pdfTableStyle _columnStyle = null;
		/// <summary>
		/// Column's elements
		/// </summary>
		protected elementList _content = new elementList(); 
		/// <summary>
		/// Container row
		/// </summary>
		protected pdfTableRow _containerRow;
		/// <summary>
		/// Start X position of the column
		/// </summary>
		protected int _startX = 0;
		/// <summary>
		/// Start Y position of the column
		/// </summary>
		protected int _startY = 0;
	
		#endregion

		#region Public_Properties

		/// <summary>
		/// Column's width
		/// </summary>
		public int columnWidth
		{
			get
			{
				return _columnWidth;
			}
		}

		/// <summary>
		/// Column's height
		/// </summary>
		public int columnHeight
		{
			get
			{
				return Math.Max(_columnHeight, _currentY);
			}
			set
			{
				if ((value - (_containerRow.containerTable.cellpadding * 2)) > _columnHeight) {
					_columnHeight = value - (_containerRow.containerTable.cellpadding * 2);
					if (this.ColumnChanged != null) {
						this.ColumnChanged(this, new columnTableEventArgs(this));
					}
				}
			}
		}

		/// <summary>
		/// Column's content align
		/// </summary>
		public predefinedAlignment columnAlign
		{
			get
			{
				return _columnAlign;
			}
			set
			{
				_columnAlign = value;
			}
		}

		/// <summary>
		/// Column's content vertical align
		/// </summary>
		public predefinedVerticalAlignment columnVerticalAlign
		{
			get
			{
				return _columnVerticalAlign;
			}
			set
			{
				_columnVerticalAlign = value;
			}
		}

		/// <summary>
		/// Column's style
		/// </summary>
		public pdfTableStyle columnStyle
		{
			get
			{
				return _columnStyle;
			}
			set
			{
				_columnStyle = value;
			}
		}

		#endregion

		#region Internal_Properties

		/// <summary>
		/// Container of the column
		/// </summary>
		internal pdfTableRow containerRow
		{
			get
			{
				return _containerRow;
			}
		}

		/// <summary>
		/// Start X position of the column
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
		/// Start Y position of the column
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

		#region ~Ctor

		internal pdfTableColumn(pdfTableRow containerRow, int columnWidth, predefinedAlignment columnAlign, pdfTableStyle columnStyle):this(containerRow, columnWidth, columnAlign, predefinedVerticalAlignment.csMiddle, columnStyle)
		{		
		}

		internal pdfTableColumn(pdfTableRow containerRow, int columnWidth, predefinedAlignment columnAlign, predefinedVerticalAlignment columnVerticalAlign, pdfTableStyle columnStyle)
		{
			_containerRow = containerRow;
			_columnWidth = columnWidth;
			_columnAlign = columnAlign;			
			_columnVerticalAlign = columnVerticalAlign;
			_columnStyle = columnStyle;
		}

		#endregion
		
		#region TextObjectsWithPredefinedStyle

		/// <summary>
		/// Method that adds a text to the column with the predefined column's style settings
		/// </summary>
		/// <param name="newText">Text to add</param>
		public void addText(string newText)
		{
			this.addText(newText, _columnStyle.fontReference, _columnStyle.fontSize, _columnStyle.fontColor);
		}

		/// <summary>
		/// Method that adds a paragraph to the column with the predefined column's style settings
		/// </summary>
		/// <param name="newText">Text to add</param>
		/// <param name="lineHeight">Height of a paragraph line</param>
		/// <param name="parAlign">Alignment of the paragraph. Remember that this is the alignment of the paragraph object and it "overloads" the column's alignment.</param>
		public void addParagraph(string newText, int lineHeight, predefinedAlignment parAlign)
		{
			this.addParagraph(newText, _columnStyle.fontReference, _columnStyle.fontSize, lineHeight, _columnWidth, _columnStyle.fontColor, parAlign);
		}

		/// <summary>
		/// Method that adds a paragraph to the column with the predefined column's style settings
		/// </summary>
		/// <param name="newText">Text to add</param>
		/// <param name="lineHeight">Height of a paragraph line</param>
		/// <param name="parAlign">Alignment of the paragraph. Remember that this is the alignment of the paragraph object and it "overloads" the column's alignment.</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		public string addParagraph(string newText, int lineHeight, predefinedAlignment parAlign, int parHeight)
		{
			return this.addParagraph(newText, _columnStyle.fontReference, _columnStyle.fontSize, lineHeight, _columnWidth, parHeight, _columnStyle.fontColor, parAlign);
		}

		#endregion

		#region IRelativeContainer

		#region Image

		/// <summary>
		/// Method that adds an image to the column
		/// </summary>
		/// <param name="imageReference">Image reference</param>
		public void addImage(pdfImageReference imageReference)
		{
			this.addImage(imageReference, imageReference.height, imageReference.width);		
		}

		/// <summary>
		/// Method that adds an image to the column
		/// </summary>
		/// <param name="imageReference">Image reference</param>
		/// <param name="height">New height of the image</param>
		/// <param name="width">New width of the image</param>
		public void addImage(pdfImageReference imageReference, int height, int width)
		{
			imageElement TempImageElement;
			if (width > _columnWidth) {				
				height = Convert.ToInt32(Math.Round((((double)_columnWidth * (double)height) / (double)width)));
				width = _columnWidth;
			}
			TempImageElement = new imageElement(imageReference, _currentX, _currentY + height, height, width);
			_content.Add(TempImageElement);			
			_currentY += TempImageElement.height;
			if (this.ColumnChanged != null) {
				this.ColumnChanged(this, new columnTableEventArgs(this));
			}
			TempImageElement = null;
		}
		#endregion

		#region Text

		/// <summary>
		/// Method that adds a text to the column
		/// </summary>
		/// <param name="newText">Text</param>
		/// <param name="fontReference">Font of the text</param>
		/// <param name="fontSize">Font's size</param>
		public void addText(string newText, pdfAbstractFont fontReference, int fontSize)
		{
			this.addText(newText, fontReference, fontSize, _columnStyle.fontColor);
		}

		/// <summary>
		/// Method that adds a text to the column
		/// </summary>
		/// <param name="newText">Text</param>
		/// <param name="fontReference">Font of the text</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontColor">Font's color</param>
		public void addText(string newText, pdfAbstractFont fontReference, int fontSize, pdfColor fontColor)
		{
			int textWidth = fontReference.getWordWidth(newText, fontSize);
			if (textWidth > _columnWidth) {
				_content.Add(new textElement(fontReference.cropWord(newText, _columnWidth, fontSize), fontSize, fontReference, _currentX, _currentY + (fontReference.fontDefinition.fontHeight * fontSize), fontColor));
			} else {
				_content.Add(new textElement(newText, fontSize, fontReference, _currentX, _currentY + (fontReference.fontDefinition.fontHeight * fontSize), fontColor));
			}
			_currentY += _content[_content.Count - 1].height;
			if (this.ColumnChanged != null) {
				this.ColumnChanged(this, new columnTableEventArgs(this));
			}
		}

		#endregion

		#region Paragraph

		/// <summary>
		/// Method that adds a paragraph to the column
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="fontReference">Text's font</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parAlign">Alignment of the paragraph</param>
		public void addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, sharpPDF.Enumerators.predefinedAlignment parAlign)
		{
			this.addParagraph(newText, fontReference, fontSize, lineHeight, parWidth, _columnStyle.fontColor, parAlign);
		}

		/// <summary>
		/// Method that adds a paragraph to the column
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="fontReference">Text's font</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="textColor">Font's color</param>
		/// <param name="parAlign">Alignment of the paragraph</param>
		public void addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, pdfColor textColor, sharpPDF.Enumerators.predefinedAlignment parAlign)
		{
			if (parWidth > _columnWidth) 
			{				
				parWidth = _columnWidth;
			}			
			paragraphElement TempParagraph = new paragraphElement(textAdapter.formatParagraph(ref newText, fontSize, fontReference, parWidth, 0, lineHeight, parAlign), parWidth, lineHeight, fontSize, fontReference, _currentX, _currentY, textColor);
			_content.Add(TempParagraph);
			_currentY += (TempParagraph.content.Count * lineHeight);
			if (this.ColumnChanged != null) {
				this.ColumnChanged(this, new columnTableEventArgs(this));
			}
			TempParagraph = null;
		}

		#endregion

		#region Checked_Paragraph


		/// <summary>
		/// Method that adds a paragraph to the column with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="fontReference">Paragraph's font</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="parAlign">Paragraph's Alignment</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		public string addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, sharpPDF.Enumerators.predefinedAlignment parAlign)
		{
			return this.addParagraph(newText, fontReference, fontSize, lineHeight, parWidth, parHeight, _columnStyle.fontColor, parAlign);
		}

		/// <summary>
		/// Method that adds a paragraph to the column with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="fontReference">Paragraph's font</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// /// <param name="textColor">Text's color</param>
		/// <param name="parAlign">Paragraph's Alignment</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		public string addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, pdfColor textColor, sharpPDF.Enumerators.predefinedAlignment parAlign)
		{
			if (parWidth > _columnWidth) 
			{				
				parWidth = _columnWidth;
			}
			paragraphElement TempParagraph = new paragraphElement(new paragraphLineList(textAdapter.formatParagraph(ref newText, fontSize, fontReference, parWidth, Convert.ToInt32(Math.Floor(((double)parHeight/(double)lineHeight))), lineHeight, parAlign)), parWidth, lineHeight, fontSize, fontReference, _currentX, _currentY, textColor);
			_content.Add(TempParagraph);
			_currentY += (TempParagraph.height);
			if (this.ColumnChanged != null) {
				this.ColumnChanged(this, new columnTableEventArgs(this));
			}
			TempParagraph = null;			
			return newText;
		}

		#endregion

		#endregion

		#region Public_Methods

		/// <summary>
		/// Method that adds a break inside the column
		/// </summary>
		/// <param name="brHeight">Height of the break</param>
		public void insertBreak(int brHeight)
		{
			this._currentY += brHeight;
		}

		#endregion

		#region ISeparable

		/// <summary>
		/// Method that returns the basic components of the pdfTableColumn
		/// </summary>
		/// <returns>Collection of pdfElements</returns>
		public elementList GetBasicElements()
		{
			elementList ResultElements = new elementList(_content);				
			foreach(pdfElement Elem in ResultElements)
			{
				switch (this._columnAlign) {
					case predefinedAlignment.csLeft:default:
						Elem.coordX += _startX;
						break;
					case predefinedAlignment.csCenter:
						Elem.coordX += _startX + Convert.ToInt32(Math.Round((double)(this._columnWidth - Elem.width)/2));
						break;
					case predefinedAlignment.csRight:
						Elem.coordX += _startX + (this._columnWidth - Elem.width);
						break;
				}
				switch (this._columnVerticalAlign) {
					case predefinedVerticalAlignment.csTop:default:
						Elem.coordY = _startY - Elem.coordY;
						break;
					case predefinedVerticalAlignment.csBottom:
						Elem.coordY = _startY - Elem.coordY - (this._containerRow.rowHeight - this._currentY - (this._containerRow.containerTable.cellpadding * 2));
						break;
					case predefinedVerticalAlignment.csMiddle:
						Elem.coordY = _startY - Elem.coordY - Convert.ToInt32(Math.Round((((double)this._containerRow.rowHeight - (double)this._currentY - ((double)this._containerRow.containerTable.cellpadding * 2d))/2d)));
						break;
				}
			}
			if (!this._containerRow.rowStyle.Equals(this._columnStyle)) {
				ResultElements.Insert(0, new rectangleElement(_startX - this._containerRow.containerTable.cellpadding, _startY + this._containerRow.containerTable.cellpadding, _startX + _columnWidth + this._containerRow.containerTable.cellpadding ,_startY + this._containerRow.containerTable.cellpadding - this._containerRow.rowHeight, this._containerRow.containerTable.borderColor, _columnStyle.bgColor, this._containerRow.containerTable.borderSize));
			}	
			return ResultElements;
		}

		#endregion

		#region ICloneable

		/// <summary>
		/// Method that clones the pdfTableColumn object
		/// </summary>
		/// <returns>Cloned Column</returns>
		public object Clone()
		{
			pdfTableColumn ReturnedColumn = new pdfTableColumn(_containerRow, _columnWidth, _columnAlign, _columnVerticalAlign, _columnStyle);
			ReturnedColumn._columnHeight = this.columnHeight;			
			ReturnedColumn._currentX = this._currentX;
			ReturnedColumn._currentY = this._currentY;
			foreach(pdfElement Elem in _content)
			{
				ReturnedColumn._content.Add((pdfElement)Elem.Clone());
			}
			return ReturnedColumn;
		}

		#endregion
	}
}
