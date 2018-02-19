using System;
using System.Collections;
using System.Text;
using System.Drawing;

using sharpPDF.Elements;
using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using sharpPDF.Fonts;

namespace sharpPDF
{
	/// <summary>
	/// A Class that implements a PDF page.
	/// </summary>
	public class pdfPage : pdfBasePage, IWritable
	{
	
		private int _height;
		private int _width;
		private int _objectID;
		private int _pageTreeID;		

		/// <summary>
		/// Page's ID
		/// </summary>
		public int objectID 
		{
			get
			{
				return _objectID;
			}

			set
			{
				_objectID = value;
			}

		}

		/// <summary>
		/// PageTree's ID
		/// </summary>
		public int pageTreeID 
		{
			get
			{
				return _pageTreeID;
			}

			set
			{
				_pageTreeID = value;
			}

		}

		/// <summary>
		/// Page's height
		/// </summary>
		public int height
		{
			get
			{
				return _height;
			}
		}

		/// <summary>
		/// Page's width
		/// </summary>
		public int width
		{
			get
			{
				return _width;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		internal pdfPage(pdfDocument containerDoc):base(containerDoc)
		{
			_height = 792;
            _width = 612;			
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		internal pdfPage(predefinedPageSize predefinedSize, pdfDocument containerDoc):base(containerDoc)
		{
			switch (predefinedSize) {
				case predefinedPageSize.csSharpPDFFormat:
					_height = 792;
					_width = 612;			
					break;
				case predefinedPageSize.csA1Page:
					_height = 2288;
					_width = 1655;			
					break;
				case predefinedPageSize.csA2Page:
					_height = 1684;
					_width = 1191;			
					break;
				case predefinedPageSize.csA3Page:
					_height = 1191;
					_width = 842;			
					break;
				case predefinedPageSize.csA4Page:
					_height = 842;
					_width = 595;			
					break;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="newHeight">Page's height</param>
		/// <param name="newWidth">Page's width</param>
		/// <param name="containerDoc">Container Document</param>
		internal pdfPage(int newHeight, int newWidth, pdfDocument containerDoc):base(containerDoc)
		{
			_height = newHeight;
            _width = newWidth;
		}

		/// <summary>
		/// Class's distructor
		/// </summary>
		~pdfPage()
		{
			_containerDoc = null;
			_elements = null;
		}

		/// <summary>
		/// Private method that returns the fonts object to the page object
		/// </summary>
		/// <returns>A string with fonts reference</returns>
		private string addFonts()
		{
			StringBuilder resultString = new StringBuilder();
			foreach (pdfAbstractFont font in _containerDoc._fonts.Values) {
				resultString.Append("/F" + font.fontNumber.ToString() + " " + font.objectID.ToString() +" 0 R " ); 
			}
			return resultString.ToString();			
		}

		/// <summary>
		/// Private method that returns the images object to the page object
		/// </summary>
		/// <returns>A string with image reference</returns>
		private string addImages()
		{
			StringBuilder resultString = new StringBuilder();
			foreach (pdfImageReference image in _containerDoc._images.Values) 
			{
				resultString.Append("/I" + image.ObjectID.ToString() + " " + image.ObjectID.ToString() +" 0 R " ); 
			}
			return resultString.ToString();			
		}
	
		/// <summary>
		/// Method that returns the PDF codes to write the page in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public string getText() 
		{
			StringBuilder pageContent = new StringBuilder();
            StringBuilder objContent = new StringBuilder();
            StringBuilder imgContent = new StringBuilder();        
			StringBuilder annotContent = new StringBuilder();
            pageContent.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("/Type /Page" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("/Parent " + _pageTreeID.ToString() + " 0 R" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("/Resources <<" + Convert.ToChar(13) + Convert.ToChar(10));
			if (_containerDoc._fonts.Count > 0) {
				pageContent.Append("/Font <<" + addFonts() + ">>" + Convert.ToChar(13) + Convert.ToChar(10));
			}
            if (_containerDoc._images.Count > 0)
			{
				pageContent.Append("/XObject <<" + addImages() + ">>" + Convert.ToChar(13) + Convert.ToChar(10));
			}
            pageContent.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("/MediaBox [0 0 " + _width + " " + _height + "]" + Convert.ToChar(13) + Convert.ToChar(10));
			pageContent.Append("/CropBox [0 0 " + _width + " " + _height + "]" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("/Rotate 0" + Convert.ToChar(13) + Convert.ToChar(10));
			pageContent.Append("/ProcSet [/PDF /Text /ImageC]" + Convert.ToChar(13) + Convert.ToChar(10));
			foreach( pdfElement item in _elements) {
				if (item is annotationElement) {
					// smeyer82: Get all annotation elements
					annotContent.Append(item.objectID.ToString() + " 0 R ");
				} else {
					objContent.Append(item.objectID.ToString() + " 0 R ");				
				}
			}
			if (objContent.Length > 0)
			{
				pageContent.Append("/Contents [" + objContent.ToString() + "]" + Convert.ToChar(13) + Convert.ToChar(10));
			}
			//smeyer82: create annotation entrys
			if (annotContent.Length > 0)
			{
				pageContent.Append("/Annots [" + annotContent.ToString()+ "]" + Convert.ToChar(13) + Convert.ToChar(10));
			}
            pageContent.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
            pageContent.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			objContent = null;
			imgContent = null;
            return pageContent.ToString();
		}
	}
}