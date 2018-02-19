using System;

using sharpPDF.Fonts;
using sharpPDF.Enumerators;
using sharpPDF.Elements;
using sharpPDF.PDFControls;

namespace sharpPDF
{
	/// <summary>
	/// Interface that represents an Absolute Container of controls or elements.
	/// </summary>
	public interface IAbsoluteContainer
	{
		#region Standard_Elements

		#region Image

		/// <summary>
		/// Method that adds an Image inside the Container
		/// </summary>
		/// <param name="imageReference">Image's reference</param>
		/// <param name="X">X position of the image</param>
		/// <param name="Y">Y position of the image</param>
		void addImage(pdfImageReference imageReference, int X, int Y);

		/// <summary>
		/// Method that adds an Image inside the Container
		/// </summary>
		/// <param name="imageReference">Image's reference</param>
		/// <param name="X">X position of the image</param>
		/// <param name="Y">Y position of the image</param>
		/// <param name="height">Height of the image</param>
		/// <param name="width">Width of the image</param>
		void addImage(pdfImageReference imageReference, int X, int Y, int height, int width);

		#endregion

		#region Text

		/// <summary>
		/// Method that adds a Text to the Container
		/// </summary>
		/// <param name="newText">String of thext</param>
		/// <param name="X">X position of the text</param>
		/// <param name="Y">Y position of the text</param>
		/// <param name="fontReference">Font's reference Object</param>
		/// <param name="fontSize">Font's size</param>
		void addText(string newText, int X, int Y, pdfAbstractFont fontReference, int fontSize);

		/// <summary>
		/// Method that adds a Text to the Container
		/// </summary>
		/// <param name="newText">String of thext</param>
		/// <param name="X">X position of the text</param>
		/// <param name="Y">Y position of the text</param>
		/// <param name="fontReference">Font's reference Object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontColor">Font's color</param>
		void addText(string newText, int X, int Y, pdfAbstractFont fontReference, int fontSize, pdfColor fontColor);

		#endregion

		#region Paragraph

		/// <summary>
		/// Method that adds a Paragraph to the Container
		/// </summary>
		/// <param name="newText">Paragraph's Text</param>
		/// <param name="x">X position of the paragraph</param>
		/// <param name="y">Y position of the paragraph</param>
		/// <param name="fontReference">Font's reference object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of the lines of the paragraph</param>
		/// <param name="parWidth">Width of the paragraph</param>
		void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth);

		/// <summary>
		/// Method that adds a Paragraph to the Container
		/// </summary>
		/// <param name="newText">Paragraph's Text</param>
		/// <param name="x">X position of the paragraph</param>
		/// <param name="y">Y position of the paragraph</param>
		/// <param name="fontReference">Font's reference object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of the lines of the paragraph</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parAlign">Alignment of the paragraph</param>
		void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, predefinedAlignment parAlign);

		/// <summary>
		/// Method that adds a Paragraph to the Container
		/// </summary>
		/// <param name="newText">Paragraph's Text</param>
		/// <param name="x">X position of the paragraph</param>
		/// <param name="y">Y position of the paragraph</param>
		/// <param name="fontReference">Font's reference object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of the lines of the paragraph</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="textColor">Color of the paragraph</param>
		void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, pdfColor textColor);

		/// <summary>
		/// Method that adds a Paragraph to the Container
		/// </summary>
		/// <param name="newText">Paragraph's Text</param>
		/// <param name="x">X position of the paragraph</param>
		/// <param name="y">Y position of the paragraph</param>
		/// <param name="fontReference">Font's reference object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of the lines of the paragraph</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <param name="textColor">Color of the paragraph</param>
		void addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, pdfColor textColor, predefinedAlignment parAlign);

		#endregion
		
		#region Checked_Paragraph

		/// <summary>
		/// Method that adds a paragraph to the Container with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the container</param>
		/// <param name="y">Y position of the paragraph inside the container</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight);

		/// <summary>
		/// Method that adds a paragraph to the Container with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the container</param>
		/// <param name="y">Y position of the paragraph inside the container</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, pdfColor textColor);

		/// <summary>
		/// Method that adds a paragraph to the Container with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the container</param>
		/// <param name="y">Y position of the paragraph inside the container</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, predefinedAlignment parAlign);

		/// <summary>
		/// Method that adds a paragraph to the Container with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="x">X position of the paragraph inside the container</param>
		/// <param name="y">Y position of the paragraph inside the container</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		string addParagraph(string newText, int x, int y, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, pdfColor textColor, predefinedAlignment parAlign);

		#endregion
		
		#region Line

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		void drawLine(int X, int Y, int X1, int Y1);

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		/// <param name="lineColor">Line's color</param>
		void drawLine(int X, int Y, int X1, int Y1, pdfColor lineColor);		

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		/// <param name="lineColor">Line's color</param>
		/// <param name="lineWidth">Line's width</param>
		void drawLine(int X, int Y, int X1, int Y1, pdfColor lineColor, int lineWidth);

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		/// <param name="lineStyle">Line's style</param>
		void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle);

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		/// <param name="lineStyle">Line's style</param>
		/// <param name="lineWidth">Line's width</param>
		void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle, int lineWidth);

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		/// <param name="lineColor">Line's color</param>
		/// <param name="lineStyle">Line's style</param>
		void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle, pdfColor lineColor);

		/// <summary>
		/// Method that draws a line on the Container
		/// </summary>
		/// <param name="X">X position of the line inside the Container</param>
		/// <param name="Y">Y position of the line inside the Container</param>
		/// <param name="X1">X1 position of the line inside the Container</param>
		/// <param name="Y1">Y1 position of the line inside the Container</param>
		/// <param name="lineColor">Line's color</param>
		/// <param name="lineStyle">Line's style</param>
		/// <param name="lineWidth">Line's width</param>
		void drawLine(int X, int Y, int X1, int Y1, predefinedLineStyle lineStyle, pdfColor lineColor, int lineWidth);

		#endregion
		
		#region Rectangle

		/// <summary>
		/// Method that draws a rectangle on the Container
		/// </summary>
		/// <param name="X">X position of the rectangle inside the Container</param>
		/// <param name="Y">Y position of the rectangle inside the Container</param>
		/// <param name="X1">X1 position of the rectangle inside the Container</param>
		/// <param name="Y1">Y1 position of the rectangle inside the Container</param>
		/// <param name="strokeColor">Color of the rectangle's border</param>
		/// <param name="fillColor">Inner color of the rectangle</param>
		void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor);

		/// <summary>
		/// Method that draws a rectangle on the Container
		/// </summary>
		/// <param name="X">X position of the rectangle inside the Container</param>
		/// <param name="Y">Y position of the rectangle inside the Container</param>
		/// <param name="X1">X1 position of the rectangle inside the Container</param>
		/// <param name="Y1">Y1 position of the rectangle inside the Container</param>
		/// <param name="strokeColor">Color of the rectangle's border</param>
		/// <param name="fillColor">Inner color of the rectangle</param>
		/// <param name="borderWidth">Width of the rectangle's border</param>
		void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor, int borderWidth);

		/// <summary>
		/// Method that draws a rectangle on the Container
		/// </summary>
		/// <param name="X">X position of the rectangle inside the Container</param>
		/// <param name="Y">Y position of the rectangle inside the Container</param>
		/// <param name="X1">X1 position of the rectangle inside the Container</param>
		/// <param name="Y1">Y1 position of the rectangle inside the Container</param>
		/// <param name="strokeColor">Color of the rectangle's border</param>
		/// <param name="fillColor">Inner color of the rectangle</param>
		/// <param name="borderStyle">Style of the rectangle's border</param>
		void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle borderStyle);

		/// <summary>
		/// Method that draws a rectangle on the Container
		/// </summary>
		/// <param name="X">X position of the rectangle inside the Container</param>
		/// <param name="Y">Y position of the rectangle inside the Container</param>
		/// <param name="X1">X1 position of the rectangle inside the Container</param>
		/// <param name="Y1">Y1 position of the rectangle inside the Container</param>
		/// <param name="strokeColor">Color of the rectangle's border</param>
		/// <param name="fillColor">Inner color of the rectangle</param>
		/// <param name="borderWidth">Width of the rectangle's border</param>
		/// <param name="borderStyle">Style of the rectangle's border</param>
		void drawRectangle(int X, int Y, int X1, int Y1, pdfColor strokeColor, pdfColor fillColor, int borderWidth, predefinedLineStyle borderStyle);

		#endregion
		
		#region Circle
		
		/// <summary>
		/// Method that adds a circle to the Container
		/// </summary>
		/// <param name="X">X position of the circle inside the Container</param>
		/// <param name="Y">Y position of the circle inside the Container</param>
		/// <param name="ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of the circle's border</param>
		/// <param name="fillColor">Inner color of the circle</param>
		void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor);
		
		/// <summary>
		/// Method that adds a circle to the Container
		/// </summary>
		/// <param name="X">X position of the circle inside the Container</param>
		/// <param name="Y">Y position of the circle inside the Container</param>
		/// <param name="ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of the circle's border</param>
		/// <param name="fillColor">Inner color of the circle</param>
		/// <param name="borderWidth">Width of the circle's border</param>
		void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor, int borderWidth);
		
		/// <summary>
		/// Method that adds a circle to the Container
		/// </summary>
		/// <param name="X">X position of the circle inside the Container</param>
		/// <param name="Y">Y position of the circle inside the Container</param>
		/// <param name="ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of the circle's border</param>
		/// <param name="fillColor">Inner color of the circle</param>
		/// <param name="borderStyle">Style of the circle's border</param>
		void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle borderStyle);
		
		/// <summary>
		/// Method that adds a circle to the Container
		/// </summary>
		/// <param name="X">X position of the circle inside the Container</param>
		/// <param name="Y">Y position of the circle inside the Container</param>
		/// <param name="ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of the circle's border</param>
		/// <param name="fillColor">Inner color of the circle</param>
		/// <param name="borderStyle">Style of the circle's border</param>
		/// <param name="borderWidth">Width of the circle's border</param>	
		void drawCircle(int X, int Y, int ray, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle borderStyle, int borderWidth);

		#endregion

		#region Annotation
		
		/// <summary>
		/// Method that adds an annotations to the Container
		/// </summary>
		/// <param name="newContent">Text of the annotation</param>
		/// <param name="newCoordX">X position of the annotation inside the Container</param>
		/// <param name="newCoordY">Y position of the annotation inside the Container</param>
		/// <param name="open">Value that teels if the annotation is opened as default</param>
		void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open);
				
		/// <summary>
		/// Method that adds an annotations to the Container
		/// </summary>
		/// <param name="newContent">Text of the annotation</param>
		/// <param name="newCoordX">X position of the annotation inside the Container</param>
		/// <param name="newCoordY">Y position of the annotation inside the Container</param>
		/// <param name="open">Value that teels if the annotation is opened as default</param>
		/// <param name="newColor">Color of the annotation</param>
		void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open, pdfColor newColor);
		
		/// <summary>
		/// Method that adds an annotations to the Container
		/// </summary>
		/// <param name="newContent">Text of the annotation</param>
		/// <param name="newCoordX">X position of the annotation inside the Container</param>
		/// <param name="newCoordY">Y position of the annotation inside the Container</param>
		/// <param name="open">Value that teels if the annotation is opened as default</param>
		/// <param name="newStyle">Style of the annotation</param>
		void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open, Enumerators.predefinedAnnotationStyle newStyle);
		
		/// <summary>
		/// Method that adds an annotations to the Container
		/// </summary>
		/// <param name="newContent">Text of the annotation</param>
		/// <param name="newCoordX">X position of the annotation inside the Container</param>
		/// <param name="newCoordY">Y position of the annotation inside the Container</param>
		/// <param name="open">Value that teels if the annotation is opened as default</param>
		/// <param name="newColor">Color of the annotation</param>
		/// <param name="newStyle">Style of the annotation</param>
		void addAnnotation(string newContent, int newCoordX, int newCoordY, bool open, pdfColor newColor, Enumerators.predefinedAnnotationStyle newStyle);
        
		#endregion

		/// <summary>
		/// Method that adds a generic element to the Container
		/// </summary>
		/// <param name="MyElement">pdfElement Object</param>
		void addElement(pdfElement MyElement);

		#endregion

		#region Controls

		/// <summary>
		/// Method that adds a pdfControl to the Container
		/// </summary>
		/// <param name="MyControl">pdfControl Object</param>
		void addControl(pdfControl MyControl);

		#endregion		
	}
}
