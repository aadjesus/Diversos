using System;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Class that represents an abstract PDF font.
	/// </summary>
	public abstract class pdfAbstractFont : IWritable
	{

		/// <summary>
		/// ID of the object
		/// </summary>
		protected int _objectID;
		/// <summary>
		/// Number of the font
		/// </summary>
		protected int _fontNumber;
		/// <summary>
		/// Font's definition object
		/// </summary>
		protected pdfFontDefinition _fontDefinition;
		/// <summary>
		/// Font's encoding type
		/// </summary>
		protected documentFontEncoding _encodingType;

		/// <summary>
		/// Font's ID
		/// </summary>
		internal int objectID
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
		/// Font's Number
		/// </summary>
		internal int fontNumber
		{
			get
			{
				return _fontNumber;
			}
		}

		/// <summary>
		/// Font's Definition Object
		/// </summary>
		internal pdfFontDefinition fontDefinition
		{
			get
			{
				return _fontDefinition;
			}
		}

		/// <summary>
		/// Font's encoding type
		/// </summary>
		internal documentFontEncoding encodingType
		{
			get
			{
				return _encodingType;
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="fontDefinition">Font's Definition Object</param>
		/// <param name="fontNumber">Font's Number</param>
		/// <param name="encodingType">Font's encoding type</param>
		internal pdfAbstractFont(pdfFontDefinition fontDefinition, int fontNumber, documentFontEncoding encodingType)
		{
			_fontDefinition = fontDefinition;
			_fontNumber = fontNumber;
			_encodingType = encodingType;
		}		

		/// <summary>
		/// Method that returns the PDF codes to write the generic pdf font in the document. It must be implemented by the derived class
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public abstract string getText();

		/// <summary>
		/// Method that returns the Encoded String.The Encoding depends on the font object
		/// </summary>
		/// <param name="strText">String to Encode</param>
		/// <returns>Encoded String</returns>
		public abstract string encodeText(string strText);

		/// <summary>
		/// Method that returns a string without the unsupported characters
		/// </summary>
		/// <param name="strText">Text to Clean</param>
		/// <returns>Cleaned Text</returns>
		public abstract string cleanText(string strText);

		/// <summary>
		/// Method that returns the horizontal space of a single word. It depends on the font object
		/// </summary>
		/// <param name="strWord">Word</param>
		/// <param name="fontSize">Font's size</param>
		/// <returns>Word's Width</returns>
		public abstract int getWordWidth(string strWord, int fontSize);

		/// <summary>
		/// Method that returns a cropped word
		/// </summary>
		/// <param name="strWord">Word</param>
		/// <param name="maxLengh">Maximum length</param>
		/// <param name="fontSize">Font's size</param>
		/// <returns>The cropped word</returns>
		public abstract string cropWord(string strWord, int maxLengh, int fontSize);
		
	}
}
