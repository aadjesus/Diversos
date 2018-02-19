using System;
using System.Collections;
using System.Drawing;

using sharpPDF.Bookmarks;
using sharpPDF.Collections;
using sharpPDF.Elements;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts;
using sharpPDF.PDFControls;
using sharpPDF.Tables;

namespace sharpPDF
{

	/// <summary>
	/// Class that represents a base page of the pdf document
	/// </summary>
	public abstract class pdfBasePage : IAbsoluteContainer
	{

		/// <summary>
		/// Elements of the Page
		/// </summary>
		protected ArrayList _elements;
		/// <summary>
		/// Container Document
		/// </summary>
		protected pdfDocument _containerDoc;

		internal ArrayList elements
		{
			get
			{
				return _elements;
			}
		}

		internal pdfBasePage(pdfDocument Container)
		{
			_containerDoc = Container;
			_elements = new ArrayList();
		}
		#region IContainer

		#region Image		

		/// <summary>
		/// Method that adds an image to the page object
		/// </summary>
		/// <param name="imageReference">Image's reference</param>
		/// <param name="X">X position of the image in the page</param>
		/// <param name="Y">Y position of the image in the page</param>		
		public void addImage(pdfImageReference imageReference, int X, int Y) {			
			imageElement objImage = new imageElement(imageReference, X, Y);
			_elements.Add(objImage);
			objImage = null;			
		}
		/// <summary>
		/// Method that adds an image to the page object
		/// </summary>
		/// <param name="imageReference">Image's reference</param>
		/// <param name="X">X position of the image in the page</param>
		/// <param name="Y">Y position of the image in the page</param>
		/// <param name="height">New height of the image</param>
		/// <param name="width">New width of the image</param>
		public void addImage(pdfImageReference imageReference, int X, int Y, int height, int width) {
			imageElement objImage = new imageElement(imageReference, X, Y, height, width);
			_elements.Add(objImage);
			objImage = null;
		}

		#endregion

		#region Text

		/// <summary>
		/// Method that adds a text element to the page object
		/// </summary>
		/// <param name="newText">Text</param>
		/// <param name="X">X position of the text in the page</param>
		/// <param name="Y">Y position of the text in the page</param>
		/// <param name="fontReference">Font's reference into the PDF document</param>
		/// <param name="fontSize">Font's size</param>
		public void addText(string newText, int X, int Y, pdfAbstractFont fontReference, int fontSize) {
			this.addText(newText, X, Y, fontReference, fontSize, pdfColor.Black);
		}

		/// <summary>
		/// Method that adds a text element to the page object
		/// </summary>
		/// <param name="newText">Text</param>
		/// <param name="X">X position of the text in the page</param>
		/// <param name="Y">Y position of the text in the page</param>
		/// <param name="fontReference">Font's reference into the PDF document</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontColor">Font's color</param>
		public void addText(string newText, int X, int Y, pdfAbstractFont fontReference, int fontSize, pdfColor fontColor) {
			_elements.Add(new textElement(newText, fontSize, fontReference, X, Y, fontColor));
		}
		
		#endregion

		#region Paragraph

		/// <summary>
		/// Method that adds a paragraph to the base page
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph in the page</param>
		/// <param name="y">Y position of the paragraph in the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragrah's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		public void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth)
		{
			this.addParagraph(newText, x, y, fontReference, fontSize, lineHeight, parWidth, pdfColor.Black, predefinedAlignment.csLeft);
		}

		/// <summary>
		/// Method that adds a paragraph to the base page
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph in the page</param>
		/// <param name="y">Y position of the paragraph in the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragrah's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parAlign">Align of the paragraph</param>
		public void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, predefinedAlignment parAlign)
		{
			this.addParagraph(newText, x, y, fontReference, fontSize, lineHeight, parWidth, pdfColor.Black, parAlign);
		}

		/// <summary>
		/// Method that adds a paragraph to the base page
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph in the page</param>
		/// <param name="y">Y position of the paragraph in the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragrah's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		public void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, pdfColor textColor)
		{
			this.addParagraph(newText, x, y, fontReference, fontSize, lineHeight, parWidth, textColor, predefinedAlignment.csLeft);
		}
		
		/// <summary>
		/// Method that adds a paragraph to the base page
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph in the page</param>
		/// <param name="y">Y position of the paragraph in the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragrah's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		/// <param name="parAlign">Align of the paragraph</param>
		public void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, pdfColor textColor, predefinedAlignment parAlign) {
			_elements.Add(new paragraphElement(textAdapter.formatParagraph(ref newText, fontSize, fontReference, parWidth, 0, lineHeight, parAlign), parWidth, lineHeight, fontSize, fontReference, x, y, textColor));
		}
		#endregion
		
		#region Checked_Paragraph

		/// <summary>
		/// Method that adds a paragraph to the base page with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the page</param>
		/// <param name="y">Y position of the paragraph inside the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		public string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight)
		{
			return this.addParagraph(newText, x, y, fontReference, fontSize, lineHeight, parWidth, parHeight, pdfColor.Black, predefinedAlignment.csLeft);
		}

		/// <summary>
		/// Method that adds a paragraph to the base page with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the page</param>
		/// <param name="y">Y position of the paragraph inside the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		public string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, pdfColor textColor)
		{
			return this.addParagraph(newText, x, y, fontReference, fontSize, lineHeight, parWidth, parHeight, textColor, predefinedAlignment.csLeft);
		}

		/// <summary>
		/// Method that adds a paragraph to the base page with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the page</param>
		/// <param name="y">Y position of the paragraph inside the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		public string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, predefinedAlignment parAlign)
		{
			return this.addParagraph(newText, x, y, fontReference, fontSize, lineHeight, parWidth, parHeight, pdfColor.Black, parAlign);
		}

		/// <summary>
		/// Method that adds a paragraph to the base page with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the page</param>
		/// <param name="y">Y position of the paragraph inside the page</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		public string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, pdfColor textColor, predefinedAlignment parAlign)
		{
			_elements.Add(new paragraphElement(new paragraphLineList(textAdapter.formatParagraph(ref newText, fontSize, fontReference, parWidth, Convert.ToInt32(Math.Floor(((double)parHeight/(double)lineHeight))), lineHeight, parAlign)), parWidth, lineHeight, fontSize, fontReference, x, y, textColor));
			return newText;
		}
		
		#endregion		
		
		#region Line

		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		public void drawLine(int X, int Y, int X1, int Y1) {
			this.drawLine(X, Y, X1, Y1, predefinedLineStyle.csNormal, pdfColor.Black, 1);
		}

		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		/// <param name="lineColor">Line's color</param>
		public void drawLine(int X, int Y, int X1, int Y1, pdfColor lineColor) {
			this.drawLine(X, Y, X1, Y1, predefinedLineStyle.csNormal, lineColor, 1);
		}

		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		/// <param name="lineColor">Line's color</param>
		/// <param name="lineWidth">Line's size</param>
		public void drawLine(int X, int Y, int X1, int Y1, pdfColor lineColor, int lineWidth) {
			this.drawLine(X, Y, X1, Y1, predefinedLineStyle.csNormal, lineColor, lineWidth);
		}
		
		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		/// <param name="lineStyle">Line's style</param>
		public void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle) {
			this.drawLine(X, Y, X1, Y1, lineStyle, pdfColor.Black, 1);
		}

		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		/// <param name="lineStyle">Line's style</param>
		/// <param name="lineWidth">Line's size</param>
		public void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle, int lineWidth) {
			this.drawLine(X, Y, X1, Y1, lineStyle, pdfColor.Black, lineWidth);
		}

		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		/// <param name="lineStyle">Line's style</param>
		/// <param name="lineColor">Line's color</param>
		public void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle, pdfColor lineColor) {
			this.drawLine(X, Y, X1, Y1, lineStyle, lineColor, 1);
		}

		/// <summary>
		/// Method that adds a line to the page object
		/// </summary>
		/// <param name="X">X position of the line in the page</param>
		/// <param name="Y">Y position of the line in the page</param>
		/// <param name="X1">X1 position of the line in the page</param>
		/// <param name="Y1">Y1 position of the line in the page</param>
		/// <param name="lineStyle">Line's style</param>
		/// <param name="lineColor">Line's color</param>
		/// <param name="lineWidth">Line's size</param>
		public void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle, pdfColor lineColor, int lineWidth) {
			_elements.Add(new lineElement(X, Y, X1, Y1, lineWidth, lineStyle, lineColor));
		}

		#endregion

		#region Rectangle

		/// <summary>
		/// Method that adds a rectangle to the page object
		/// </summary>
		/// <param name="X">X position of the rectangle in the page</param>
		/// <param name="Y">Y position of the rectangle in the page</param>
		/// <param name="X1">X1 position of the rectangle in the page</param>
		/// <param name="Y1">Y1 position of the rectangle in the page</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Rectancle's color</param>
		public void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor) {
			this.drawRectangle(X, Y, X1, Y1, strokeColor, fillColor, 1, predefinedLineStyle.csNormal);
		}

		/// <summary>
		/// Method that adds a rectangle to the page object
		/// </summary>
		/// <param name="X">X position of the rectangle in the page</param>
		/// <param name="Y">Y position of the rectangle in the page</param>
		/// <param name="X1">X1 position of the rectangle in the page</param>
		/// <param name="Y1">Y1 position of the rectangle in the page</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Rectancle's color</param>
		/// <param name="borderWidth">Border's size</param>
		public void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor, int borderWidth) {
			this.drawRectangle(X, Y, X1, Y1, strokeColor, fillColor, borderWidth, predefinedLineStyle.csNormal);
		}

		/// <summary>
		/// Method that adds a rectangle to the page object
		/// </summary>
		/// <param name="X">X position of the rectangle in the page</param>
		/// <param name="Y">Y position of the rectangle in the page</param>
		/// <param name="X1">X1 position of the rectangle in the page</param>
		/// <param name="Y1">Y1 position of the rectangle in the page</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Rectancle's color</param>
		/// <param name="borderStyle">Border's style</param>
		public void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle borderStyle) {
			this.drawRectangle(X, Y, X1, Y1, strokeColor, fillColor, 1, borderStyle);
		}

		/// <summary>
		/// Method that adds a rectangle to the page object
		/// </summary>
		/// <param name="X">X position of the rectangle in the page</param>
		/// <param name="Y">Y position of the rectangle in the page</param>
		/// <param name="X1">X1 position of the rectangle in the page</param>
		/// <param name="Y1">Y1 position of the rectangle in the page</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Rectancle's color</param>
		/// <param name="borderWidth">Border's size</param>
		/// <param name="borderStyle">Border's style</param>
		public void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor, int borderWidth, predefinedLineStyle borderStyle) {
			_elements.Add(new rectangleElement(X, Y, X1, Y1, strokeColor, fillColor, borderWidth, borderStyle));
		}

		#endregion

		#region Circle

		/// <summary>
		/// Method that adds a circle to the page object
		/// </summary>
		/// <param name="X">X position of the circle in the page</param>
		/// <param name="Y">Y position of the circle in the page</param>
		/// <param name="ray">Circle's ray</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Circle's color</param>
		public void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor) {
			this.drawCircle(X, Y, ray, strokeColor, fillColor, predefinedLineStyle.csNormal, 1);
		}

		/// <summary>
		/// Method that adds a circle to the page object
		/// </summary>
		/// <param name="X">X position of the circle in the page</param>
		/// <param name="Y">Y position of the circle in the page</param>
		/// <param name="ray">Circle's ray</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Circle's color</param>
		/// <param name="borderWidth">Border's size</param>
		public void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor, int borderWidth) {
			this.drawCircle(X, Y, ray, strokeColor, fillColor, predefinedLineStyle.csNormal, borderWidth);
		}

		/// <summary>
		/// Method that adds a circle to the page object
		/// </summary>
		/// <param name="X">X position of the circle in the page</param>
		/// <param name="Y">Y position of the circle in the page</param>
		/// <param name="ray">Circle's ray</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Circle's color</param>
		/// <param name="borderStyle">Border's style</param>
		public void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle borderStyle) {
			this.drawCircle(X, Y, ray, strokeColor, fillColor, borderStyle, 1);
		}

		/// <summary>
		/// Method that adds a circle to the page object
		/// </summary>
		/// <param name="X">X position of the circle in the page</param>
		/// <param name="Y">Y position of the circle in the page</param>
		/// <param name="ray">Circle's ray</param>
		/// <param name="strokeColor">Border's color</param>
		/// <param name="fillColor">Circle's color</param>
		/// <param name="borderStyle">Border's style</param>
		/// <param name="borderWidth">Border's size</param>
		public void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle borderStyle, int borderWidth) {
			_elements.Add(new circleElement(X, Y, ray, strokeColor, fillColor, borderWidth, borderStyle));			
		}

		#endregion

		#region Annotation
		/// <summary>
		/// Adds an annotation to the page object
		/// </summary>
		/// <param name="newContent">Content of the annotation element</param>
		/// <param name="newCoordX">X position on page</param>
		/// <param name="newCoordY">Y position on page</param>
		/// <param name="open">Sets the annotation to open or closed</param>
		public void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open)
		{
			this.addAnnotation(newContent, newCoordX, newCoordY, open, pdfColor.LightGray, predefinedAnnotationStyle.csComment);
		}

		/// <summary>
		/// Adds an annotation to the page object
		/// </summary>
		/// <param name="newContent">Content of the annotation element</param>
		/// <param name="newCoordX">X position on page</param>
		/// <param name="newCoordY">Y position on page</param>
		/// <param name="open">Sets the annotation to open or closed</param>
		/// <param name="newColor">The color of the annotation object</param>
		public void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open, pdfColor newColor)
		{
			this.addAnnotation(newContent, newCoordX, newCoordY, open, newColor, predefinedAnnotationStyle.csComment);
		}

		/// <summary>
		/// Adds an annotation to the page object
		/// </summary>
		/// <param name="newContent">Content of the annotation element</param>
		/// <param name="newCoordX">X position on page</param>
		/// <param name="newCoordY">Y position on page</param>
		/// <param name="open">Sets the annotation to open or closed</param>
		/// <param name="newStyle">The style of the annotation element</param>
		public void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open, Enumerators.predefinedAnnotationStyle newStyle)
		{
			this.addAnnotation(newContent, newCoordX, newCoordY, open, pdfColor.LightGray, newStyle);
		}

		/// <summary>
		/// Adds an annotation to the page object
		/// </summary>
		/// <param name="newContent">Content of the annotation element</param>
		/// <param name="newCoordX">X position on page</param>
		/// <param name="newCoordY">Y position on page</param>
		/// <param name="open">Sets the annotation to open or closed</param>
		/// <param name="newColor">The color of the annotation object</param>
		/// <param name="newStyle">The style of the annotation element</param>
		public void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open, pdfColor newColor, Enumerators.predefinedAnnotationStyle newStyle)
		{			
			_elements.Add(new annotationElement(newContent, newCoordX, newCoordY, newColor, newStyle, open));		
		}
		#endregion

		/// <summary>
		/// Method that adds a pdfControl into the page
		/// </summary>
		/// <param name="MyControl">PdfControl object</param>
		public void addControl(pdfControl MyControl)
		{
			this._elements.AddRange(MyControl.GetBasicElements());
		}

		/// <summary>
		/// Method that adds a generic element into the page
		/// </summary>
		/// <param name="MyElement">PdfElement object</param>
		public void addElement(pdfElement MyElement)
		{
			this._elements.Add(MyElement);
		}

		#endregion

		#region Tables

		/// <summary>
		/// Method that adds a table to the page
		/// </summary>
		/// <param name="myTable">Table's object</param>
		public void addTable(pdfTable myTable)
		{
			_elements.AddRange(myTable.GetBasicElements());
		}

		/// <summary>
		/// Method that adds a table to the page, with a check on the maximum height
		/// </summary>
		/// <param name="myTable">Table's object</param>
		/// <param name="tabHeight">Maximum height of the table</param>
		public pdfTable addTable(pdfTable myTable, int tabHeight)
		{
			pdfTable ReturnedTable = myTable.CropTable(tabHeight);
			_elements.AddRange(myTable.GetBasicElements());
			return ReturnedTable;
		}

		#endregion
	}
}
