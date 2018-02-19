using System;
using System.Collections;
using System.IO;
using System.Text;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts.TTF;

namespace sharpPDF.Fonts
{
	/// <summary>
	/// Class that represents a True Type Font
	/// </summary>
	public sealed class pdfTrueTypeFont : pdfAbstractFont
	{
		ArrayList _usedCharacters = new ArrayList();
		int[] _glyphMapping = new int[65535];
		string _fontReference;		
		int _descriptorObjectID;
		int _toUnicodeObjectID;
		int _descendantObjectID;
		int _streamObjectID;
		byte[] _subsetStream;

		internal int descriptorObjectID
		{
			get
			{
				return _descriptorObjectID;
			}
			set
			{
				_descriptorObjectID = value;
			}
		}

		internal int toUnicodeObjectID
		{
			get
			{
				return _toUnicodeObjectID;
			}
			set
			{
				_toUnicodeObjectID = value;
			}
		}

		internal int descendantObjectID
		{
			get
			{
				return _descendantObjectID;
			}
			set
			{
				_descendantObjectID = value;
			}
		}

		internal int streamObjectID
		{
			get
			{
				return _streamObjectID;
			}
			set
			{
				_streamObjectID = value;
			}
		}

		internal byte[] subsetStream
		{
			get
			{
				return _subsetStream;
			}
		}

		/// <summary>
		/// Class's costructor
		/// </summary>
		/// <param name="fontDefinition">Definition of the Font</param>
		/// <param name="fontNumber">Font's number</param>
		/// <param name="encodingType">Encoding's Type</param>
		/// <param name="fontReference">Font's reference name</param>
		internal pdfTrueTypeFont(pdfFontDefinition fontDefinition, int fontNumber, documentFontEncoding encodingType, string fontReference):base(fontDefinition,fontNumber,encodingType)
		{			
			_fontReference = fontReference;			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string getText()
		{
			StringBuilder content  = new StringBuilder();
			content.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Type /Font" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Subtype /Type0" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Name /F" + _fontNumber.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/BaseFont /" + _fontDefinition.fontName + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/DescendantFonts [" + _descendantObjectID.ToString() + " 0 R]" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Encoding /Identity-H" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/ToUnicode " + _toUnicodeObjectID.ToString() + " 0 R" +  Convert.ToChar(13) + Convert.ToChar(10));
			content.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));				
			//ToUnicode Object
			return content.ToString();
		}
		
		/// <summary>
		/// Method that returns the PDF font descriptor
		/// </summary>
		/// <returns>String that contains PDF commands</returns>
		public string getFontDescriptorText()
		{
			StringBuilder content = new StringBuilder();			
			content.Append(_descriptorObjectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Type /FontDescriptor" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/FontName /" + _fontDefinition.fontName + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/StemV 80" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Descent " + _fontDefinition.descender.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Ascent " + _fontDefinition.ascender.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/ItalicAngle " + _fontDefinition.italicAngle.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/CapHeight " + _fontDefinition.capHeight.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Flags 32" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/FontFile2 " + _streamObjectID.ToString() + " 0 R" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/FontBBox [" + _fontDefinition.fontBBox[0].ToString() + " " + _fontDefinition.fontBBox[1].ToString() + " " + _fontDefinition.fontBBox[2].ToString() + " " + _fontDefinition.fontBBox[3].ToString() + "]" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			return content.ToString();
		}

		/// <summary>
		/// Method that returns the PDF font descendant
		/// </summary>
		/// <returns>String that contains PDF commands</returns>
		public string getFontDescendantText()
		{
			StringBuilder content = new StringBuilder();			
			content.Append(_descendantObjectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Type /Font" + Convert.ToChar(13) + Convert.ToChar(10));			
			content.Append("/BaseFont /" + _fontDefinition.fontName + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/CIDSystemInfo <</Ordering (Identity)" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Registry (Adobe)" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Supplement 0" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/CIDToGIDMap /Identity" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/FontDescriptor " + _descriptorObjectID.ToString() + " 0 R" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Subtype /CIDFontType2" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/DW 1000" +  Convert.ToChar(13) + Convert.ToChar(10));
			if (_usedCharacters.Count > 0) {				
				content.Append("/W [");				
				CharacterMetric oldMetric;
				CharacterMetric tempMetric;
				content.Append(getMetric((char)_usedCharacters[0]).characterMapping.ToString() + "[" + getMetric((char)_usedCharacters[0]).characterWidth.ToString());
				oldMetric = getMetric((char)_usedCharacters[0]);
				for(int i = 1; i < _usedCharacters.Count; i++)
				{	
					tempMetric = getMetric((char)_usedCharacters[i]);
					if (tempMetric.characterMapping != (oldMetric.characterMapping + 1)) {
						content.Append("]" + tempMetric.characterMapping.ToString() + "[" + tempMetric.characterWidth.ToString());
					} else {
						content.Append(" " + tempMetric.characterWidth.ToString());
					}
					oldMetric = tempMetric;
				}
				content.Append("]]" + Convert.ToChar(13) + Convert.ToChar(10));
			}
			content.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			return content.ToString();
		}

		/// <summary>
		/// Method that returns the PDF font ToUnicode Tag
		/// </summary>
		/// <returns>String that contains PDF commands</returns>
		public string getToUnicodeText() {
			StringBuilder content = new StringBuilder();
			StringBuilder hexContent = new StringBuilder();			
			
			//Hex Content
			hexContent.Append("/CIDInit /ProcSet findresource begin" + Convert.ToChar(13) + Convert.ToChar(10));			
			hexContent.Append("12 dict begin" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("begincmap" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("/CIDSystemInfo" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("<< /Registry (Adobe)" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("/Ordering (UCS)" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("/Supplement 0" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append(">> def" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("/CMapName /Adobe-Identity-UCS def" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("/CMapType 2 def" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("1 begincodespacerange" + Convert.ToChar(13) + Convert.ToChar(10));
			if (_usedCharacters.Count > 1) {
				hexContent.Append("<"+ getMetric((char)_usedCharacters[0]).characterMapping.ToString("X4").ToLower() +"><"+ getMetric((char)_usedCharacters[_usedCharacters.Count-1]).characterMapping.ToString("X4").ToLower() +">" + Convert.ToChar(13) + Convert.ToChar(10));
			} else {
				hexContent.Append("<>" + Convert.ToChar(10) + Convert.ToChar(13));
			}
			hexContent.Append("endcodespacerange" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append(_usedCharacters.Count.ToString() + " beginbfrange" + Convert.ToChar(13) + Convert.ToChar(10));
			if (_usedCharacters.Count > 0) {							
				foreach(char myChar in _usedCharacters)
				{					
					hexContent.Append("<"+ getMetric(myChar).characterMapping.ToString("X4").ToLower() +"><"+ getMetric(myChar).characterMapping.ToString("X4").ToLower() +"><"+ ((int)myChar).ToString("X4").ToLower() +">" + Convert.ToChar(13) + Convert.ToChar(10));
				}				
			}
			hexContent.Append("endbfrange" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("endcmap" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("CMapName currentdict /CMap defineresource pop" + Convert.ToChar(13) + Convert.ToChar(10));
			hexContent.Append("end end");
			
			//Unicode Text Object
			content.Append(_toUnicodeObjectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Filter [/ASCIIHexDecode]" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("/Length " + hexContent.Length.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("stream" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append(textAdapter.HEXFormatter(hexContent.ToString()) + ">" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("endstream" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			return content.ToString();
		}
		
		/// <summary>
		/// Method that returns the PDF font stream
		/// </summary>
		/// <returns>String that contains PDF commands</returns>
		public string getStreamText() {
			this.createFontSubset();
			StringBuilder content = new StringBuilder();
			content.Append(_streamObjectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
			content.Append("<</Length "+ _subsetStream.Length.ToString() + ">>" + Convert.ToChar(13) + Convert.ToChar(10));
			return content.ToString();
		}

		/// <summary>
		/// Method that creates the PDF font subset
		/// </summary>
		private void createFontSubset() {			
			ttfFontSubset _fontSubset = null;
			try {
				Hashtable usedGlyph = new Hashtable();
				for (int i = 0; i<_usedCharacters.Count;i++)
				{
					usedGlyph.Add(getMetric((char)_usedCharacters[i]).characterMapping,new int[3] {getMetric((char)_usedCharacters[i]).characterMapping, getMetric((char)_usedCharacters[i]).characterWidth, (char)_usedCharacters[i]});
				}
				_fontSubset = new sharpPDF.Fonts.TTF.ttfFontSubset(_fontReference, usedGlyph);
				this._subsetStream = _fontSubset.getFontFileStream();
			} catch (Exception e) {
				throw new pdfBadFontFileException();
			} finally {
				if (_fontSubset != null) {
					_fontSubset = null;
				}
			}
		}

		/// <summary>
		/// Method that encodes the text
		/// </summary>
		/// <param name="strText">String to encode</param>
		/// <returns>Encoded string</returns>
		public override string encodeText(string strText)
		{			
			char[] arrChar = strText.ToCharArray();
			char tempChar;
			StringBuilder returnText = new StringBuilder();
			for (int i = 0; i < arrChar.Length; i++)
			{
				if (((CharacterMetric)_fontDefinition.fontMetrics[(int)arrChar[i]]).characterMapping != 0) {				
					tempChar = (char)((CharacterMetric)_fontDefinition.fontMetrics[(int)arrChar[i]]).characterMapping;
					returnText.Append((char)(tempChar >> 8));
					returnText.Append((char)(tempChar & 0x00ff));
				}
			}			
            return returnText.ToString();
		}

		/// <summary>
		/// Method that adds new characters to the "used characters" collection
		/// </summary>
		/// <param name="myText">Text</param>
		public void addCharacters(string myText)
		{
			bool found;
			int i;
			foreach(char myChar in myText.ToCharArray()) {
				if (!(_usedCharacters.Contains(myChar)) && (((CharacterMetric)_fontDefinition.fontMetrics[myChar]).characterMapping != 0))
				{	
					found = false;
					i = 0;
					//Search the position into the ArrayList with the characterMapping Value
					while (i < _usedCharacters.Count && !(found)) {
						if (getMetric(myChar).characterMapping < getMetric((char)_usedCharacters[i]).characterMapping) {
							found = true;
						} else {
							i++;
						}
					}
					if (!(found)) {
						_usedCharacters.Add(myChar);
					} else {
						_usedCharacters.Insert(i, myChar);
					}
				}
			}
		}

		private CharacterMetric getMetric(char myChar)
		{
			return (CharacterMetric)_fontDefinition.fontMetrics[(int)myChar];
		}

		/// <summary>
		/// Method that removes the unsupported characters from the text
		/// </summary>
		/// <param name="strText">Text to clean</param>
		/// <returns>Cleaned text</returns>
		public override string cleanText(string strText) {
			StringBuilder cleanedWord = new StringBuilder();
			foreach(char myChar in strText.ToCharArray()) {
				if (getMetric(myChar).characterMapping != 0) {				
					cleanedWord.Append(myChar);
				}
			}
			return cleanedWord.ToString();
		}


		/// <summary>
		/// Method that returns the width of a string
		/// </summary>
		/// <param name="strWord">Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <returns>Text's Width</returns>
		public override int getWordWidth(string strWord, int fontSize)
		{
			double returnWeight = 0;
			foreach(char myChar in strWord.ToCharArray()) {
				returnWeight += ((CharacterMetric)_fontDefinition.fontMetrics[(int)myChar]).characterWidth;
			}
			return Convert.ToInt32(Math.Round(returnWeight * fontSize /1000));
		}

		/// <summary>
		/// Method that returns a cropped word
		/// </summary>
		/// <param name="strWord">Word</param>
		/// <param name="maxLength">Maximum length</param>
		/// <param name="fontSize">Font's size</param>
		/// <returns>The cropped word</returns>
		public override string cropWord(string strWord, int maxLength, int fontSize)
		{
			StringBuilder CroppedWord = new StringBuilder();
			strWord = cleanText(strWord);
			int i = 0;
			if (getWordWidth(strWord, fontSize) <= maxLength || strWord.Length == 0) {
				return strWord;
			} else {				
				while (getWordWidth(CroppedWord.ToString()+ strWord[i] + "...", fontSize) <= maxLength && i < strWord.Length)
				{
					CroppedWord.Append(strWord[i]);
					i++;
				}
				if (CroppedWord.Length == 0) {
					return "";
				} else {
					return CroppedWord.ToString() + "...";
				}
			}			
		}

	}
}
