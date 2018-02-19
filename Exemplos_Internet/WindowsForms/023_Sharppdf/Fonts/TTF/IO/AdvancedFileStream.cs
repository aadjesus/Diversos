using System;
using System.IO;
using System.Text;

using sharpPDF;
using sharpPDF.Exceptions;

namespace sharpPDF.Fonts.TTF.IO
{
	/// <summary>
	/// Class thet represents an extension of FileStream that reads/stores TTF File Format
	/// (Big-Endian and TTF variables, Based on Microsoft TTF Specifications)
	/// </summary>
	internal class AdvancedFileStream : FileStream
	{

		/// <summary>
		/// Class's Constructor
		/// </summary>
		/// <param name="fileName">Name of the file</param>
		/// <param name="fileMode">File Mode</param>
		public AdvancedFileStream(string fileName,FileMode fileMode):base(fileName, fileMode)
		{
			
		}

		/// <summary>
		/// Method that reads a string from the stream
		/// </summary>
		/// <param name="length">Lenght of the string</param>
		/// <returns>String</returns>
		public string readString(int length)
		{		
			try 
			{
				byte[] buffer = new byte[length];
				StringBuilder resultString = new StringBuilder();
				base.Read(buffer,0,buffer.Length);			
				for(int i = 0; i < length; i++)
				{
					resultString.Append(Convert.ToChar(buffer[i]));
				}
				return resultString.ToString();
			} 
			catch (IOException ex) 
			{
				throw new pdfGenericIOException(ex.Message,ex);
			}			
		}

		/// <summary>
		/// Method that reads an unicode string from the stream
		/// </summary>
		/// <param name="length">Length of the string</param>
		/// <returns>String</returns>
		public string readUnicodeString(int length)
		{
			byte[] buffer = new byte[length];
			StringBuilder resultString = new StringBuilder();
			base.Read(buffer,0,buffer.Length);			
			for(int i = 0; i < length; i+=2)
			{
				resultString.Append(Convert.ToChar((buffer[i] << 8) + buffer[i+1]));
			}
			return resultString.ToString();
		}

		/// <summary>
		/// Method that reads an Unsigned Long Big-Endian from the stream
		/// </summary>
		/// <returns>Unsigned Long</returns>
		public int readULong_BE()
		{
			byte[] buffer = new byte[4];
			int returnValue = 0;
			base.Read(buffer,0,buffer.Length);			
			returnValue = (buffer[0] << 24) + (buffer[1] << 16) + (buffer[2] << 8) + buffer[3];
			buffer = null;
			return returnValue;
		}

		/// <summary>
		/// Method that reads a Short Big-Endian
		/// </summary>
		/// <returns>Short</returns>
		public short readShort_BE()
		{
			byte[] buffer = new byte[2];
			short returnValue = 0;
			base.Read(buffer,0,buffer.Length);			
			returnValue = (short)((buffer[0] << 8) + buffer[1]);
			buffer = null;
			return returnValue;
		}

		/// <summary>
		/// Method that reads a Unsigned Short Big-Endian
		/// </summary>
		/// <returns>Unsigned Short</returns>
		public int readUShort_BE()
		{
			byte[] buffer = new byte[2];
			int returnValue = 0;
			base.Read(buffer,0,buffer.Length);			
			returnValue = (buffer[0] << 8) + buffer[1];
			buffer = null;
			return returnValue;
		}

		/// <summary>
		/// Method that reads an Integer
		/// </summary>
		/// <returns>Integer</returns>
		public int readInt()
		{
			byte[] buffer = new byte[2];
			int returnValue = 0;
			base.Read(buffer,0,buffer.Length);			
			returnValue = (buffer[1]) + buffer[0];
			buffer = null;
			return returnValue;
		}

		/// <summary>
		/// Method that reads an Integer Big-Endian
		/// </summary>
		/// <returns>Integer</returns>
		public int readInt_BE()
		{
			byte[] buffer = new byte[2];
			int returnValue = 0;
			base.Read(buffer,0,buffer.Length);			
			returnValue = (buffer[0] << 8) + buffer[1];
			buffer = null;
			return returnValue;
		}

		/// <summary>
		/// Method that read a Byte
		/// </summary>
		/// <returns>Byte</returns>
		public byte readByte()
		{
			byte[] buffer = new byte[1];
			byte returnValue = 0;
			base.Read(buffer,0,buffer.Length);			
			returnValue = buffer[0];
			buffer = null;
			return returnValue;
		}

		/// <summary>
		/// Method that read an array of Bytes
		/// </summary>
		/// <param name="length">Lenght of the array</param>
		/// <returns>Byte Array</returns>
		public byte[] readByteArray(int length)
		{
			byte[] buffer = new byte[length];
			base.Read(buffer,0,buffer.Length);
			return buffer;
		}

		/// <summary>
		/// Method that read an array of Bytes Big-Endian
		/// </summary>
		/// <param name="length">Lenght of the array</param>
		/// <returns>Byte Array</returns>
		public byte[] readByteArray_BE(int length)
		{
			byte[] buffer = new byte[length];
			base.Read(buffer,0,buffer.Length);
			return Encoding.Convert(Encoding.BigEndianUnicode, Encoding.Unicode, buffer,0,buffer.Length);
		}

		/// <summary>
		/// Method that sets the position of the stream
		/// </summary>
		/// <param name="offset"></param>
		public void SetPosition(long offset)
		{
			base.Position = offset;
		}

		/// <summary>
		/// Method that skip a number of Bytes
		/// </summary>
		/// <param name="length">Bytes to skip</param>
		public void SkipBytes(long length)
		{
			base.Position += length;
		}		

	}
}
