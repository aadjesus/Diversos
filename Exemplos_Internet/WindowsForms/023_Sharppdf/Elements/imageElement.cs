using System;
using System.IO;
using System.Drawing;
using System.Text;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF.Elements
{
	/// <summary>
	/// A Class that implements a PDF image.
	/// </summary>
	public sealed class imageElement : pdfElement
	{

		private pdfImageReference _ObjectXReference;
		
		/// <summary>
		/// Image's Reference
		/// </summary>
		public pdfImageReference ObjectXReference
		{
			get
			{
					return _ObjectXReference;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="imageReference">Image's Reference</param>
		/// <param name="newCoordX">X position in the PDF document</param>
		/// <param name="newCoordY">Y position in the PDF document</param>
		public imageElement(pdfImageReference imageReference, int newCoordX, int newCoordY)
		{	
			_ObjectXReference = imageReference;
			_height = _ObjectXReference.height;
			_width = _ObjectXReference.width;	
			_coordX = newCoordX;
			_coordY = newCoordY;			
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="imageReference">Image's Reference</param>
		/// <param name="newCoordX">X position in the PDF document</param>
		/// <param name="newCoordY">Y position in the PDF document</param>
		/// <param name="newHeight">New height of the image</param>
		/// <param name="newWidth">New width of the image</param>
		public imageElement(pdfImageReference imageReference, int newCoordX, int newCoordY, int newHeight, int newWidth)
		{
			_ObjectXReference = imageReference;
			_height = newHeight;
			_width = newWidth;				
			_coordX = newCoordX;
			_coordY = newCoordY;		
		}

		/// <summary>
		/// Method that returns the PDF codes to write the image in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public override string getText()
		{
			StringBuilder resultImage = new StringBuilder();
            StringBuilder imageContent = new StringBuilder();            
            imageContent.Append("q" + Convert.ToChar(13) + Convert.ToChar(10));
			imageContent.Append(_width.ToString() + " 0 0 " + _height.ToString() + " " + _coordX.ToString() + " " + _coordY.ToString() + " cm" + Convert.ToChar(13) + Convert.ToChar(10));
            imageContent.Append("/I" + _ObjectXReference.ObjectID.ToString() + " Do" + Convert.ToChar(13) + Convert.ToChar(10));
            imageContent.Append("Q" + Convert.ToChar(13) + Convert.ToChar(10));			
            resultImage.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
            resultImage.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
            resultImage.Append("/Length " + imageContent.Length.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
            resultImage.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
            resultImage.Append("stream" + Convert.ToChar(13) + Convert.ToChar(10));
            resultImage.Append(imageContent.ToString());
            resultImage.Append("endstream" + Convert.ToChar(13) + Convert.ToChar(10));
            resultImage.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			imageContent = null;
            return resultImage.ToString();            
		}

		/// <summary>
		/// Method that clones the element
		/// </summary>
		/// <returns>Cloned object</returns>
		public override object Clone()
		{
			return new imageElement(this._ObjectXReference, this._coordX, this._coordY, this._height, this._width);
		}

	}
}
