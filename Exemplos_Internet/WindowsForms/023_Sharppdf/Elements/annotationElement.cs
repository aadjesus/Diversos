//*****************************************************************
//******************Inserted by smeyer82 (2004)********************
//*****************************************************************

using System;
using System.Collections;
using System.Text;

using sharpPDF;
using sharpPDF.Enumerators;

namespace sharpPDF.Elements
{

	/// <summary>
	/// Creates an annotation element.
	/// </summary>
	public sealed class annotationElement : pdfElement
	{
		private string _content;
		private Enumerators.predefinedAnnotationStyle _style;
		private ArrayList _styleNames = new ArrayList();
		private bool _open = false;

		/// <summary>
		/// Initializes the array of style names
		/// </summary>
		internal void initializeStyleNames()
		{
			_styleNames.Insert((int)predefinedAnnotationStyle.csNone, "Note");
			_styleNames.Insert((int)predefinedAnnotationStyle.csComment, "Comment");
			_styleNames.Insert((int)predefinedAnnotationStyle.csKey, "Key");
			_styleNames.Insert((int)predefinedAnnotationStyle.csNote, "Note");
			_styleNames.Insert((int)predefinedAnnotationStyle.csHelp, "Help");
			_styleNames.Insert((int)predefinedAnnotationStyle.csNewParagraph, "NewParagraph");
			_styleNames.Insert((int)predefinedAnnotationStyle.csParagraph, "Paragraph");
			_styleNames.Insert((int)predefinedAnnotationStyle.csInsert, "Insert");
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newContent">Content of the annotation element</param>
		/// <param name="newCoordX">X position on page</param>
		/// <param name="newCoordY">Y position on page</param>
		/// <param name="newColor">The color of the annotation element</param>
		/// <param name="newStyle">The style of the annotation element</param>
		public annotationElement(string newContent, int newCoordX, int newCoordY, pdfColor newColor, Enumerators.predefinedAnnotationStyle newStyle):this(newContent, newCoordX, newCoordY, newColor, newStyle, false)
		{
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newContent">Content of the annotation element</param>
		/// <param name="newCoordX">X position on page</param>
		/// <param name="newCoordY">Y position on page</param>
		/// <param name="newColor">The color of the annotation element</param>
		/// <param name="newStyle">The style of the annotation element</param>
		/// <param name="open">Makes the annotation element open or closes at starttime</param>
		public annotationElement(string newContent, int newCoordX, int newCoordY, pdfColor newColor, Enumerators.predefinedAnnotationStyle newStyle, bool open)
		{
			_content = newContent;
			_coordX = newCoordX;
			_coordY = newCoordY;
			_strokeColor = newColor;
			_style = newStyle;
			_open = open;
			_height = 0;
			_width = 0;

			initializeStyleNames();
		}

		/// <summary>
		/// Sets the content of the annotation
		/// </summary>
		public string content
		{
			get
			{
				return _content;
			}
			
			set 
			{
				_content = value;
			}
		}

		/// <summary>
		/// Sets the color of the annotation element
		/// </summary>
		public pdfColor color
		{
			get
			{
				return _strokeColor;
			}

			set
			{
				_strokeColor = value;
			}
		}

		/// <summary>
		/// The style of the Annotation
		/// </summary>
		public predefinedAnnotationStyle style
		{
			get
			{
				return _style;
			}
			set
			{
				_style = value;
			}
		}

		/// <summary>
		/// Method that returns the PDF codes to write the annotation in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public override string getText()
		{
			StringBuilder result = new StringBuilder();

			result.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			result.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			
			result.Append("/Type /Annot" + Convert.ToChar(13) + Convert.ToChar(10));
			result.Append("/Subtype /Text" + Convert.ToChar(13) + Convert.ToChar(10));
			result.Append("/Rect [" + _coordX.ToString() + " 0 0 " + _coordY.ToString() + "]" + Convert.ToChar(13) + Convert.ToChar(10));
			result.Append("/Contents (" + _content + ")" + Convert.ToChar(13) + Convert.ToChar(10));
			
			if (_open)
                result.Append("/Open true" + Convert.ToChar(13) + Convert.ToChar(10));

			if (_strokeColor.isColor())
			{
				result.Append("/C [" + _strokeColor.rColor + " " +_strokeColor.gColor + " " + _strokeColor.bColor + "]" + Convert.ToChar(13) + Convert.ToChar(10));
			}
			if (_style != Enumerators.predefinedAnnotationStyle.csNone)
			{
				result.Append("/Name /" + _styleNames[(int)_style] + Convert.ToChar(13) + Convert.ToChar(10));
			}
			result.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			result.Append("endobj" +Convert.ToChar(13) + Convert.ToChar(10));

			return result.ToString();
		}

		/// <summary>
		/// Method that clones the element
		/// </summary>
		/// <returns>Cloned object</returns>
		public override object Clone()
		{
			return new annotationElement((string)this._content.Clone(), this._coordX, this._coordY, (pdfColor)this._strokeColor.Clone(), this._style);
		}

	}
}
