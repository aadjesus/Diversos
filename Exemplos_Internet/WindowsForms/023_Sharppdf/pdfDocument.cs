using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections;

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
	/// A Class that implements a PDF document.
	/// </summary>
	public class pdfDocument : IDisposable
	{
		private string _title;
		private string _author;
		private bool _openBookmark;
		private pdfHeader _header;
		private pdfInfo _info;
		private pdfOutlines _outlines;
		private pdfPageTree _pageTree;
		private pdfTrailer _trailer;		
		private ArrayList _pages = null;
		private pdfPageMarker _pageMarker = null;
		private pdfPersistentPage _persistentPage = null;

		internal fontHashtable _fonts = new fontHashtable();
		internal imageHashtable _images = new imageHashtable();

		/// <summary>
		/// Document's page marker
		/// </summary>
		public pdfPageMarker pageMarker
		{
			set
			{
				_pageMarker = value;				
			}
		}

		/// <summary>
		/// Document's persistent page
		/// </summary>
		public pdfPersistentPage persistentPage
		{
			get
			{
				return _persistentPage;
			}			
		}

		/// <summary>
		/// Collection of pdf's page
		/// </summary>
		public pdfPage this[int index]
		{
			get
			{
				return (pdfPage)_pages[index];
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="author">Author of the document</param>
		/// <param name="title">Title of the document</param>
		public pdfDocument(string title, string author)
		{			
			_title = title;
			_author = author;
			_openBookmark = false;
			_outlines = new pdfOutlines();
			_pages = new ArrayList();
			_persistentPage = new pdfPersistentPage(this);

		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="author">Author of the document</param>
		/// <param name="title">Title of the document</param>
		/// <param name="openBookmark">Allow to show directly bookmarks near the document</param>
		public pdfDocument(string title, string author, bool openBookmark)
		{			
			_title = title;
			_author = author;
			_openBookmark = openBookmark;
			_outlines = new pdfOutlines();
			_pages = new ArrayList();
			_persistentPage = new pdfPersistentPage(this);
		}

		/// <summary>
		/// Dispose Method
		/// </summary>
		public void Dispose()
		{			
			_header = null;
			_info = null;
			_outlines = null;
            _fonts = null;
            _pages = null;
            _pageTree = null;
            _trailer = null;
			_title = null;
			_author = null;
			_pageMarker = null;
			_persistentPage = null;

		}
		
		#region Font_Mananagment		
		
		/// <summary>
		/// Method thata adds a Font Reference of a True Type file inside the pdfDocument
		/// </summary>
		/// <param name="fileName">Name of the font's file</param>
		/// <param name="fontName">Font's reference name</param>
		public void addTrueTypeFont(string fileName, string fontName)
		{
			if (!(isFontLoaded(fontName))) {
				_fonts.Add(fontName,pdfFontFactory.getFontObject(fileName, _fonts.Count + 1,  documentFontType.csTrueTypeFont));
			}
		}

		/// <summary>
		/// Method that loads a font inside the document
		/// </summary>
		/// <param name="fontName">Font's reference name</param>
		/// <param name="fontObject">pdfAbstractFont object</param>
		internal void loadFont(string fontName, pdfAbstractFont fontObject)
		{
			_fonts.Add(fontName, fontObject);
		}

		/// <summary>
		/// Methot that shows if a font is loaded inside the document
		/// </summary>
		/// <param name="fontName">Font's reference name</param>
		/// <returns>Booleand value that tells if the font is loaded</returns>
		internal bool isFontLoaded(string fontName)
		{
			return _fonts.ContainsKey(fontName);
		}

		/// <summary>
		/// Method that return the pdfAbstractFont Object Reference of a Standard Font
		/// </summary>
		/// <param name="fontType">Predefined Font</param>
		/// <returns>pdfAbstractFont Object</returns>
		public pdfAbstractFont getFontReference(predefinedFont fontType)
		{
			return this.getFontReference(pdfFontFactory.getPredefinedFontName(fontType));
		}

		/// <summary>
		/// Method that return the pdfAbstractFont Object Reference of a Font
		/// </summary>
		/// <param name="fontReference">Font's reference name</param>
		/// <returns>pdfAbstractFont Object</returns>
		public pdfAbstractFont getFontReference(string fontReference)
		{
			if (!(isFontLoaded(fontReference))) {
				if (pdfFontFactory.isPredefinedFont(fontReference)) {
					_fonts.Add(fontReference, pdfFontFactory.getFontObject(fontReference, _fonts.Count + 1,  documentFontType.csPredefinedfont));	
				} else {
					throw new pdfFontNotLoadedException();
				}
			}
			return _fonts[fontReference];
		}

		#endregion

		#region Image_Management

		/// <summary>
		/// Method that adds an Image Reference to the document
		/// </summary>
		/// <param name="fileName">Image File Name</param>
		/// <param name="imageName">Name of the reference</param>
		public void addImageReference(string fileName, string imageName)
		{
			if (!(isImageReferenceLoaded(imageName))) {
				_images.Add(imageName, new pdfImageReference(fileName));
			}
		}

		/// <summary>
		/// Method that adds an Image Reference to the document
		/// </summary>
		/// <param name="imageObject">Image Object</param>
		/// <param name="imageName">Name of the reference</param>
		public void addImageReference(Image imageObject, string imageName)
		{
			if (!(isImageReferenceLoaded(imageName))) {
				_images.Add(imageName, new pdfImageReference(imageObject));
			}
		}

		internal bool isImageReferenceLoaded(string imageName)
		{
			return _images.ContainsKey(imageName);
		}

		/// <summary>
		/// Method that returns an pdfImageReference object
		/// </summary>
		/// <param name="imageName">Image's reference name</param>
		/// <returns>pdfImageReference Object</returns>
		public pdfImageReference getImageReference(string imageName)
		{
			if (this.isImageReferenceLoaded(imageName)) {
				return (this._images[imageName]);
			} else {
				throw new pdfImageNotLoadedException();
			}
		}

		#endregion

		/// <summary>
		/// Method that writes the PDF document on the stream
		/// </summary>
		/// <param name="outStream">Output stream</param>
		public void createPDF(Stream outStream)
		{
			BufferedStream _myBuffer;
			long _bufferLength = 0;

			initializeObjects();            
            try
			{
                //Bufferedstream's initialization 
				_myBuffer = new BufferedStream(outStream);
				
				//PDF's definition
				_bufferLength += writeToBuffer(_myBuffer , @"%PDF-1.5" + Convert.ToChar(13) + Convert.ToChar(10));	         
				
				//PDF's header object
				_trailer.addObject(_bufferLength.ToString());
				_bufferLength += writeToBuffer(_myBuffer, _header.getText());

				//PDF's info object
				_trailer.addObject(_bufferLength.ToString());
				_bufferLength += writeToBuffer(_myBuffer, _info.getText());

				//PDF's outlines object
				_trailer.addObject(_bufferLength.ToString());
				_bufferLength += writeToBuffer(_myBuffer, _outlines.getText());

				//PDF's bookmarks
				foreach(pdfBookmarkNode Node in _outlines.getBookmarks())
				{
					_trailer.addObject(_bufferLength.ToString());
					_bufferLength += writeToBuffer(_myBuffer, Node.getText());
				}

				//Fonts's initialization
				foreach (pdfAbstractFont font in _fonts.Values)
				{
					_trailer.addObject(_bufferLength.ToString());
					_bufferLength += writeToBuffer(_myBuffer, font.getText());
					if (font is pdfTrueTypeFont) {
						_trailer.addObject(_bufferLength.ToString());
						_bufferLength += writeToBuffer(_myBuffer, ((pdfTrueTypeFont)font).getFontDescriptorText());
						_trailer.addObject(_bufferLength.ToString());
						_bufferLength += writeToBuffer(_myBuffer, ((pdfTrueTypeFont)font).getFontDescendantText());
						_trailer.addObject(_bufferLength.ToString());
						_bufferLength += writeToBuffer(_myBuffer, ((pdfTrueTypeFont)font).getToUnicodeText());						
						//Font's Stream						
						_trailer.addObject(_bufferLength.ToString());
						_bufferLength += writeToBuffer(_myBuffer, ((pdfTrueTypeFont)font).getStreamText());
						_bufferLength += writeToBuffer(_myBuffer, "stream" + Convert.ToChar(13) + Convert.ToChar(10));
						_bufferLength += writeToBuffer(_myBuffer, ((pdfTrueTypeFont)font).subsetStream);
						_bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(13).ToString());
						_bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(10).ToString());
						_bufferLength += writeToBuffer(_myBuffer, "endstream" + Convert.ToChar(13) + Convert.ToChar(10));
						_bufferLength += writeToBuffer(_myBuffer, "endobj" + Convert.ToChar(13) + Convert.ToChar(10));						
					}
				}
				
				//PDF's pagetree object
				_trailer.addObject(_bufferLength.ToString());
				_bufferLength += writeToBuffer(_myBuffer, _pageTree.getText());

				//Generation of PDF's pages
				foreach(pdfPage page in _pages)
				{
					_trailer.addObject(_bufferLength.ToString());
					_bufferLength += writeToBuffer(_myBuffer, page.getText());

					foreach (pdfElement element in page.elements)
					{
						_trailer.addObject(_bufferLength.ToString());
						_bufferLength += writeToBuffer(_myBuffer, element.getText());						
					}

				}

				//PDF's Images
				foreach (pdfImageReference image in _images.Values)
				{
					_trailer.addObject(_bufferLength.ToString());
					_bufferLength += writeToBuffer(_myBuffer, image.getText());
					_bufferLength += writeToBuffer(_myBuffer, "stream" + Convert.ToChar(13) + Convert.ToChar(10));
					_bufferLength += writeToBuffer(_myBuffer, image.content);
					_bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(13).ToString());
					_bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(10).ToString());
					_bufferLength += writeToBuffer(_myBuffer, "endstream" + Convert.ToChar(13) + Convert.ToChar(10));
					_bufferLength += writeToBuffer(_myBuffer, "endobj" + Convert.ToChar(13) + Convert.ToChar(10));
				}

				//PDF's trailer object
				_trailer.xrefOffset = _bufferLength;
				_bufferLength += writeToBuffer(_myBuffer, _trailer.getText());
				//Buffer's flush
				_myBuffer.Flush();	
				//Free
				_myBuffer.Close();
				_myBuffer = null;            
			 } catch (IOException ex) {
                throw new pdfWritingErrorException("Errore nella scrittura del PDF",ex);			
			}
		}

		/// <summary>
		/// Method that writes the PDF document on a file
		/// </summary>
		/// <param name="outputFile">String that represents the name of the output file</param>
		public void createPDF(string outputFile)
		{	
			FileStream _myFileOut;
			try
			{				
				_myFileOut = new FileStream(outputFile, FileMode.Create);
				createPDF(_myFileOut);
				_myFileOut.Close();
			} catch (IOException exIO) {
				throw new pdfWritingErrorException("Errore nella scrittura del file",exIO);
			} catch (pdfWritingErrorException exPDF) {
				throw new pdfWritingErrorException("Errore nella scrittura del PDF",exPDF);
			}
			
		}		

		/// <summary>
		/// Private method for the initialization of all PDF objects
		/// </summary>
		private void initializeObjects()
		{
			//Page's counters
			int	pageIndex = 1;
			int	pageNum = _pages.Count;

			int counterID = 0;
			//header			
			_header = new pdfHeader(_openBookmark);
            _header.objectIDHeader = 1;						
			_header.objectIDInfo = 2;
            _header.objectIDOutlines = 3;
			//Info
			_info = new pdfInfo(_title, _author);
			_info.objectIDInfo = 2;	
			//Outlines			
			_outlines.objectIDOutlines = 3;
			counterID = 4;
			//Bookmarks	
			counterID = _outlines.initializeOutlines(counterID);			
			//fonts
			foreach(pdfAbstractFont font in _fonts.Values)
			{
				font.objectID = counterID;
				counterID++;
				if (font is pdfTrueTypeFont) {
					((pdfTrueTypeFont)font).descriptorObjectID = counterID;
					counterID++;
					((pdfTrueTypeFont)font).descendantObjectID = counterID;
					counterID++;
					((pdfTrueTypeFont)font).toUnicodeObjectID = counterID;
					counterID++;
					((pdfTrueTypeFont)font).streamObjectID = counterID;
					counterID++;
				}
			}
			//pagetree
            _pageTree = new pdfPageTree();
			_pageTree.objectID = counterID;
            _header.pageTreeID = counterID;
            counterID++;
			
			//pages
            foreach(pdfPage page in _pages)
			{
                page.objectID = counterID;
				page.pageTreeID = _pageTree.objectID;
				//page.addFonts(_fonts);
                _pageTree.addPage(counterID);
                counterID++;
				
				//Add page's Marker
				if (_pageMarker != null) {
					page.addText(_pageMarker.getMarker(pageIndex, pageNum),_pageMarker.coordX, _pageMarker.coordY,_pageMarker.fontType, _pageMarker.fontSize, _pageMarker.fontColor);
				}

				//Add persistent elements
				if (_persistentPage.elements.Count > 0) {
					page.elements.InsertRange(0,_persistentPage.elements);
				}

				//page's elements
                foreach (pdfElement element in page.elements)
				{
                    element.objectID = counterID;
                    counterID++;					
				}

				//Update page's index counter
				pageIndex += 1;
			}

			//images
			foreach (pdfImageReference image in _images.Values) {
				image.ObjectID = counterID;
				counterID++;  
			}

			//trailer
			_trailer = new pdfTrailer(counterID - 1);			
		}

		/// <summary>
		/// Method that creates a new page
		/// </summary>
		/// <returns>New PDF's page</returns>
		public pdfPage addPage()
		{
			_pages.Add(new pdfPage(this));
			return (pdfPage)_pages[_pages.Count - 1];
		}

		/// <summary>
		/// Method that creates a new page
		/// </summary>
		/// <param name="predefinedSize">The standard page's size</param>
		/// <returns>New PDF's page</returns>
		public pdfPage addPage(predefinedPageSize predefinedSize)
		{
			_pages.Add(new pdfPage(predefinedSize, this));
			return (pdfPage)_pages[_pages.Count - 1];
		}

		/// <summary>
		/// Method that creates a new page
		/// </summary>
		/// <returns>New PDF's page</returns>
		/// <param name="height">Height of the new page</param>
		/// <param name="width">Width of the new page</param>
		public pdfPage addPage(int height, int width)
		{
			_pages.Add(new pdfPage(height, width, this));
			return (pdfPage)_pages[_pages.Count - 1];
		}

		/// <summary>
		/// Method that adds a bookmark to the document
		/// </summary>
		/// <param name="Bookmark">Bookmark object</param>
		public void addBookmark(pdfBookmarkNode Bookmark)
		{
			_outlines.addBookmark(Bookmark);
		}

		/// <summary>
		/// Method that writes into the buffer a string
		/// </summary>
		/// <param name="myBuffer">Output Buffer</param>
		/// <param name="stringContent">String that contains the informations</param>
		/// <returns>The number of the bytes written in the Buffer</returns>
		private long writeToBuffer(BufferedStream myBuffer, string stringContent)
		{
			ASCIIEncoding myEncoder = new ASCIIEncoding();
			byte[] arrTemp;
			try	{
				arrTemp = myEncoder.GetBytes(stringContent);					
				myBuffer.Write(arrTemp, 0, arrTemp.Length);
				return arrTemp.Length;
			} catch (IOException ex) {
				throw new pdfBufferErrorException("Errore nella scrittura del Buffer", ex);
			}			
		}

		/// <summary>
		/// Method that writes into the buffer a string
		/// </summary>
		/// <param name="myBuffer">Output Buffer</param>
		/// <param name="byteContent">A Byte array that contains the informations</param>
		/// <returns>The number of the bytes written in the Buffer</returns>
		private long writeToBuffer(BufferedStream myBuffer, byte[] byteContent)
		{
			try	{
				myBuffer.Write(byteContent, 0, byteContent.Length);
				return byteContent.Length;
			} catch (IOException ex) {
				throw new pdfBufferErrorException("Errore nella scrittura del Buffer", ex);
			}					
		}		
		
	}
}
