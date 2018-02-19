using System;
using System.IO;
using System.Drawing;
using System.Text;

using sharpPDF.Exceptions;

namespace sharpPDF.Elements
{
	/// <summary>
	/// Class that represents an image reference inside the document.
	/// </summary>
	public sealed class pdfImageReference : IWritable
	{

		private int _ObjectID;
		private int _height;
		private int _width;
		private byte[] _content;

		/// <summary>
		/// Object's ID
		/// </summary>
		internal int ObjectID
		{
			get
			{
				return _ObjectID;
			}
			set
			{
				_ObjectID = value;
			}
		}

		/// <summary>
		/// Image's Height
		/// </summary>
		public int height
		{
			get
			{
				return _height;
			}
		}

		/// <summary>
		/// Image's Width
		/// </summary>
		public int width
		{
			get
			{
				return _width;
			}
		}

		/// <summary>
		/// Image's bytes
		/// </summary>
		internal byte[] content
		{
			get
			{
				return _content;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="imageName">Name of the image file</param>
		internal pdfImageReference(string imageName)
		{
			try 
			{
				Image myImage = Image.FromFile(imageName);
				MemoryStream outStream = new MemoryStream();			
				myImage.Save(outStream,System.Drawing.Imaging.ImageFormat.Jpeg);
				_content = new byte[outStream.Length];
				_content = outStream.ToArray();
				myImage.Dispose();
				myImage = Image.FromStream(outStream);
				_height = myImage.Height;
				_width = myImage.Width;
				myImage.Dispose();
				outStream.Close();
				outStream = null;
				myImage = null;
			} 
			catch (System.IO.FileNotFoundException ex) 
			{
				throw new pdfImageNotFoundException("Image "+ imageName +" not found!", ex);
			} 
			catch (System.IO.IOException ex) 
			{
				throw new pdfImageIOException("Generic IO error on the image  "+ imageName, ex);
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="myImage">System.Drawing.Image object</param>
		internal pdfImageReference(Image myImage)
		{
			try 
			{				
				MemoryStream outStream = new MemoryStream();			
				myImage.Save(outStream,System.Drawing.Imaging.ImageFormat.Jpeg);
				_content = new byte[outStream.Length];
				_content = outStream.ToArray();
				myImage.Dispose();
				myImage = Image.FromStream(outStream);
				_height = myImage.Height;
				_width = myImage.Width;			
				myImage.Dispose();
				outStream.Close();
				outStream = null;
				myImage = null;
			} 
			catch (System.IO.IOException ex) 
			{
				throw new pdfImageIOException("Generic IO error on the image!", ex);
			}
		}

		#region IWritable

		/// <summary>
		/// Method that returns the PDF codes to write the image reference in the document.
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public string getText()
		{
			StringBuilder resultImage = new StringBuilder();
			resultImage.Append(_ObjectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Type /XObject" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Subtype /Image" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Name /I" + _ObjectID.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Filter /DCTDecode" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Width " + _width.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Height " + _height.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/BitsPerComponent 8" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/ColorSpace /DeviceRGB" + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append("/Length " + _content.Length.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			resultImage.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			return resultImage.ToString();  
		}

		#endregion
	}
}
