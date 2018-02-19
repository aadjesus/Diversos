using System;
using System.Collections;
using System.IO;
using System.Text;

using sharpPDF;
using sharpPDF.Exceptions;

namespace sharpPDF.Fonts.TTF.IO
{
	/// <summary>
	/// Class thet represents an extension of MemoryStream that writes/stores bytes in TTF File Format
	/// (Big-Endian and TTF variables, Based on Microsoft TTF Specifications)
	/// </summary>
	internal class AdvancedMemoryStream : MemoryStream
	{

		private Hashtable winansi = new Hashtable();

		/// <summary>
		/// Class's constructor
		/// </summary>
		public AdvancedMemoryStream():base()
		{
			winansi[8364] = 128;
			winansi[8218] = 130;
			winansi[402] = 131;
			winansi[8222] = 132;
			winansi[8230] = 133;
			winansi[8224] = 134;
			winansi[8225] = 135;
			winansi[710] = 136;
			winansi[8240] = 137;
			winansi[352] = 138;
			winansi[8249] = 139;
			winansi[338] = 140;
			winansi[381] = 142;
			winansi[8216] = 145;
			winansi[8217] = 146;
			winansi[8220] = 147;
			winansi[8221] = 148;
			winansi[8226] = 149;
			winansi[8211] = 150;
			winansi[8212] = 151;
			winansi[732] = 152;
			winansi[8482] = 153;
			winansi[353] = 154;
			winansi[8250] = 155;
			winansi[339] = 156;
			winansi[382] = 158;
			winansi[376] = 159;

		}

		/// <summary>
		/// Method that reads a string
		/// </summary>
		/// <param name="length">Lengtf of the string</param>
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
		/// Method that writes a string
		/// </summary>
		/// <param name="stringValue">String to write</param>
		public void writeString(string stringValue)
		{
			byte[] buffer = new byte[stringValue.Length];
			for (int k = 0; k < stringValue.Length; ++k) {
				if (stringValue[k] < 128 || (stringValue[k] >= 160 && stringValue[k] <= 255)) {
					buffer[k] = Convert.ToByte((int)stringValue[k]);
				} else {
					buffer[k] = Convert.ToByte((int)winansi[stringValue[k]]);
				}
			}			
			try {
				base.Write(buffer, 0, buffer.Length);
			} catch (Exception ex) {
				throw new pdfGenericIOException(ex.Message, ex);
			}
		}

		/// <summary>
		/// Method that read an Unicode String
		/// </summary>
		/// <param name="length">Length of the string</param>
		/// <returns>Unicode String</returns>
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
		/// Method that writes a Unicode string
		/// </summary>
		/// <param name="stringValue">Unicode String to write</param>
		public void writeUnicodeString(string stringValue)
		{
			base.Write(Encoding.Unicode.GetBytes(stringValue), 0, Encoding.Unicode.GetBytes(stringValue).Length);
		}

		/// <summary>
		/// Method that reads an Unsigned Long Big-Endian
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
		/// Method that write an Unsigned Long Big-Endian
		/// </summary>
		/// <param name="intValue">Unsigned Long</param>
		public void writeULong_BE(int intValue)
		{
			byte[] buffer = new byte[4];
			buffer[0] = (byte)(intValue >> 24);
			buffer[1] = (byte)((intValue >> 16) & 0x000000ff);
			buffer[2] = (byte)((intValue >> 8) & 0x000000ff);
			buffer[3] = (byte)(intValue & 0x000000ff);
			base.Write(buffer, 0, buffer.Length);
			buffer = null;	
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
		/// Method that writes a Short Big-Endian
		/// </summary>
		/// <param name="shortValue">Short</param>
		public void writeShort_BE(short shortValue)
		{
			byte[] buffer = new byte[2];
			buffer[0] = (byte)(shortValue >> 8);
			buffer[1] = (byte)(shortValue & 0x00ff);
			base.Write(buffer, 0, buffer.Length);
			buffer = null;	
		}

		/// <summary>
		/// Method that reads an Unsigned Short Big-Endian
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
		/// Method that writes an Unsigned Short Big-Endian
		/// </summary>
		/// <param name="ushortValue">Unsigned Short</param>
		public void writeUShort_BE(int ushortValue)
		{
			byte[] buffer = new byte[2];
			buffer[0] = (byte)(ushortValue >> 8);
			buffer[1] = (byte)(ushortValue & 0x00ff);
			base.Write(buffer, 0, buffer.Length);
			buffer = null;	
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
		/// Method that writes an Integer
		/// </summary>
		/// <param name="intValue">Integer</param>
		public void writeInt(int intValue)
		{
			byte[] buffer = new byte[2];
			buffer[0] = (byte)(intValue & 0x00ff);
			buffer[1] = (byte)(intValue >> 8);			
			base.Write(buffer, 0, buffer.Length);
			buffer = null;	
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
		/// Method that writes an Integer Big-Endian
		/// </summary>
		/// <param name="intValue">Integer</param>
		public void writeInt_BE(int intValue)
		{
			byte[] buffer = new byte[2];
			buffer[0] = (byte)(intValue >> 8);
			buffer[1] = (byte)(intValue & 0x00ff);
			base.Write(buffer, 0, buffer.Length);
			buffer = null;			
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
		/// Method that writes an array of bytes
		/// </summary>
		/// <param name="byteArrayValue">Byte Array</param>
		public void writeByteArray(byte[] byteArrayValue)
		{
			base.Write(byteArrayValue, 0, byteArrayValue.Length);
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
		/// Method that writes an array of bytes Big-Endia
		/// </summary>
		/// <param name="byteArrayValue">Byte Array</param>
		public void writeByteArray_BE(byte[] byteArrayValue)
		{
			byte[] buffer = Encoding.Convert(Encoding.Unicode, Encoding.BigEndianUnicode, byteArrayValue, 0, byteArrayValue.Length);
			base.Write(buffer, 0, buffer.Length);
			buffer = null;
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
