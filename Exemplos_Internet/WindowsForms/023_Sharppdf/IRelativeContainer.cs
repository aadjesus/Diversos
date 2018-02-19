using System;

using sharpPDF.Fonts;
using sharpPDF.Enumerators;
using sharpPDF.Elements;
using sharpPDF.PDFControls;

namespace sharpPDF
{
	/// <summary>
	/// Interface that represents a Relative Container some basics elements.
	/// </summary>
	public interface IRelativeContainer
	{

		#region Standard_Elements

		#region Image

		/// <summary>
		/// Method that adds an Image inside the Container
		/// </summary>
		/// <param name="imageReference">Image's reference</param>
		void addImage(pdfImageReference imageReference);

		/// <summary>
		/// Method that adds an Image inside the Container
		/// </summary>
		/// <param name="imageReference">Image's reference</param>
		/// <param name="height">Height of the image</param>
		/// <param name="width">Width of the image</param>
		void addImage(pdfImageReference imageReference, int height, int width);

		#endregion

		#region Text

		/// <summary>
		/// Method that adds a Text to the Container
		/// </summary>
		/// <param name="newText">String of thext</param>
		/// <param name="fontReference">Font's reference Object</param>
		/// <param name="fontSize">Font's size</param>
		void addText(string newText, pdfAbstractFont fontReference, int fontSize);

		/// <summary>
		/// Method that adds a Text to the Container
		/// </summary>
		/// <param name="newText">String of thext</param>
		/// <param name="fontReference">Font's reference Object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontColor">Font's color</param>
		void addText(string newText, pdfAbstractFont fontReference, int fontSize, pdfColor fontColor);

		#endregion

		#region Paragraph

		/// <summary>
		/// Method that adds a Paragraph to the Container
		/// </summary>
		/// <param name="newText">Paragraph's Text</param>
		/// <param name="fontReference">Font's reference object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of the lines of the paragraph</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parAlign">Alignment of the paragraph</param>
		void addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, predefinedAlignment parAlign);

		/// <summary>
		/// Method that adds a Paragraph to the Container
		/// </summary>
		/// <param name="newText">Paragraph's Text</param>
		/// <param name="fontReference">Font's reference object</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of the lines of the paragraph</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <param name="textColor">Color of the paragraph</param>
		void addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, pdfColor textColor, predefinedAlignment parAlign);

		#endregion
		
		#region Checked_Paragraph

		/// <summary>
		/// Method that adds a paragraph to the Container with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		string addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, predefinedAlignment parAlign);

		/// <summary>
		/// Method that adds a paragraph to the Container with a check on its maximum height
		/// </summary>
		/// <param name="newText">Text of the paragraph</param>
		/// <param name="fontReference">Font of the paragraph</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="lineHeight">Height of paragraph's lines</param>
		/// <param name="parWidth">Width of the paragraph</param>
		/// <param name="parHeight">Maximum height of the paragraph</param>
		/// <param name="textColor">Text's color</param>
		/// <param name="parAlign">Align of the paragraph</param>
		/// <returns>Text out of the paragraph's maximum height</returns>
		string addParagraph(string newText, pdfAbstractFont fontReference, int fontSize, int lineHeight, int parWidth, int parHeight, pdfColor textColor, predefinedAlignment parAlign);

		#endregion

		#endregion

	}
}
