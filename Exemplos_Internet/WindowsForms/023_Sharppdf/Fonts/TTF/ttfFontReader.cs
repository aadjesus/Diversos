using System;
using System.Collections;
using System.IO;
using System.Text;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using sharpPDF.Fonts;
using sharpPDF.Fonts.TTF.IO;

namespace sharpPDF.Fonts.TTF
{
	/// <summary>
	/// Descrizione di riepilogo per ttfFontReader.
	/// </summary>
	internal class ttfFontReader : FontReader
	{
		private AdvancedFileStream _fontStream = null;		
		private OffsetTable _offsetTable = new OffsetTable();
		private Hashtable _directoryTables = new Hashtable();		
		private NamesTable _namesTable = new NamesTable();
		private FontHeaderTable _headTable = new FontHeaderTable();
		private HorizontalHeaderTable _hheaTable = new HorizontalHeaderTable();
		private OSTable _osTable = new OSTable();
		private PostScriptTable _postTable = new PostScriptTable();
		private int[] _GlyphWidths;
		private ArrayList _CMAPTable = new ArrayList();
		private bool _disposed = false;

		#region ~Ctor

		/// <summary>
		/// Class's Constructor
		/// </summary>
		/// <param name="fontReference">Name of the font file</param>
		public ttfFontReader(string fontReference):base(fontReference)
		{
			//Check if it is a really TTF file
			if (!(_fontReference.ToLower().EndsWith(".ttf"))) {
				throw new pdfBadFontFileException();
			}
			try {
				_fontStream = new AdvancedFileStream(_fontReference,FileMode.Open);				
			} catch (Exception ex) {
				throw new pdfGenericIOException(ex.Message, ex);
			}
		}

		/// <summary>
		/// Method that Dispose the object
		/// </summary>
		public override void Dispose() {
			if (!_disposed) {
				_directoryTables = null;
				_CMAPTable = null;
				if (_fontStream != null) {
					_fontStream.Close();
					_fontStream = null;
				}
				this._disposed = true;
			}
		}

		#endregion

		/// <summary>
		/// Method that reads the informations from the font file
		/// </summary>
		private void readFontFile()
		{
			DirectoryTable tempTable;			
			try {				
				readOffsetTable();
				_fontStream.Seek(12,SeekOrigin.Begin);
				for (int i = 0; i < _offsetTable.NumberOfTables; i++) {
					tempTable = readDirectoryTable();
					_directoryTables.Add(tempTable.Tag.ToLower(), tempTable);
				}
				readNamesTable();
				readFontHeaderTable();
				readHorizontalHeaderTable();
				readOsTable();
				readPostScriptTable();
				readGlyphWidths();
				readCMAP();
			} catch (Exception ex) {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that returns the definition of the font
		/// </summary>
		/// <returns>Font Definition object</returns>
		public override pdfFontDefinition getFontDefinition()
		{
			pdfFontDefinition myDefinition = new pdfFontDefinition();			
			try {
				//Fill All Font's Table
				readFontFile();
				//Creation of FontDefinition
				myDefinition.fontName = "BBCDEE+" + _namesTable.FontName;
				myDefinition.fullFontName = _namesTable.FullName;
				myDefinition.familyName = _namesTable.FamilyName;
				myDefinition.ascender = (int)_osTable.sTypoAscender * 1000 / _headTable.unitsPerEm;
				myDefinition.descender = (int)_osTable.sTypoDescender * 1000 / _headTable.unitsPerEm;
				myDefinition.capHeight = (int)_osTable.sCapHeight * 1000 / _headTable.unitsPerEm;
				myDefinition.fontBBox = new int[4]{
					(int)(_headTable.xMin * 1000 / _headTable.unitsPerEm),
					(int)(_headTable.yMin * 1000 / _headTable.unitsPerEm),
					(int)(_headTable.xMax * 1000 / _headTable.unitsPerEm),
					(int)(_headTable.yMax * 1000 / _headTable.unitsPerEm)
				};
				myDefinition.fontHeight = Convert.ToInt32(Math.Round((((double)myDefinition.fontBBox[3] - (double)myDefinition.fontBBox[1]) / 1000)));				
				if (myDefinition.fontHeight == 0) {
					myDefinition.fontHeight = 1;
				}
				myDefinition.italicAngle = (int)_postTable.ItalicAngle * 1000 / _headTable.unitsPerEm;
				myDefinition.underlinePosition = _postTable.UnderlinePosition;
				myDefinition.underlineThickness = _postTable.UnderlineThickness;
				myDefinition.isFixedPitch = _postTable.IsFixedPitch;
				CMAPTable myCMAP = null;
				//Get CMAP Platform=3, Encoding=1 Windows - Unicode
				myCMAP = getCMAP(3,1);
				if (myCMAP == null) {
					//Get CMAP Platform=1, Endoding=0 Mac
					myCMAP = getCMAP(1,0);
				}
				if (myCMAP != null) {						
					for (int i = 0; i < 65535;i++)
					{	
						CharacterMetric myMetric = new CharacterMetric();
						if (myCMAP.Mapping.ContainsKey(i)) {
							myMetric.characterMapping = ((int[])myCMAP.Mapping[i])[0];
							myMetric.characterWidth = ((int[])myCMAP.Mapping[i])[1];						
						} else {
							myMetric.characterMapping = 0;
							myMetric.characterWidth = 0;
						}
						myDefinition.fontMetrics[i] = myMetric;					
					}
				} else {
					throw new pdfCMAPNotSupportedException();
				}
				myCMAP = null;
			} catch (pdfCMAPNotSupportedException ex) {
				throw new pdfCMAPNotSupportedException();
			} catch (Exception ex) {
				throw new pdfBadFontFileException();
			} finally {
				if (_fontStream != null) {
					_fontStream.Close();
				}
			}
			return myDefinition;
		}

		/// <summary>
		/// Method that returns the a stream of the font file
		/// </summary>
		/// <returns>Byte aray with the stream of the font file</returns>
		public byte[] getFontStream()
		{
			byte[] returnBytes;
			_fontStream.SetPosition(0);
			returnBytes = new byte[_fontStream.Length];
            _fontStream.Read(returnBytes,0,returnBytes.Length);
			return returnBytes;
		}

		/// <summary>
		/// Method that reads the Offset Table
		/// </summary>
		private void readOffsetTable()
		{	
			/*---<Temporary>---*/
			_fontStream.SetPosition(0);			
			_fontStream.SkipBytes(4); //Font Version
			_offsetTable.Version = 1.0f;
			/*---</Temporary>---*/
			_offsetTable.NumberOfTables = _fontStream.readUShort_BE();			
			_offsetTable.SearchRange = _fontStream.readUShort_BE();			
			_offsetTable.EntrySelector = _fontStream.readUShort_BE();			
			_offsetTable.RangeShift = _fontStream.readUShort_BE();
		}

		/// <summary>
		/// Method that reads the Names Table
		/// </summary>
		private void readNamesTable()
		{
			if (_directoryTables.ContainsKey("name")) {
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["name"]).Offset); //Set the offset
                _fontStream.SkipBytes(2);
				int platformID;
				int platformEncodingID;
				int languageID;
				int nameID;
				int length;
				int offset;
				int numTables = _fontStream.readUShort_BE();
				int startOffset = _fontStream.readUShort_BE();
				long currentStreamPosition;
				int i = 0;
				bool founded = false;
				while (i<numTables && !(founded))
				{
					platformID = _fontStream.readUShort_BE();
					platformEncodingID = _fontStream.readUShort_BE();
					languageID = _fontStream.readUShort_BE();
					nameID = _fontStream.readUShort_BE();
					length = _fontStream.readUShort_BE();
					offset = _fontStream.readUShort_BE();
					currentStreamPosition = _fontStream.Position;
					_fontStream.SetPosition(((DirectoryTable)_directoryTables["name"]).Offset + startOffset + offset);
					switch (nameID) {
						case 1: //FamilyName
							if ((platformID == 0 || platformID == 3 || (platformID == 2 && platformEncodingID == 1))){
								_namesTable.FamilyName = _fontStream.readUnicodeString(length);
							}
							else {
								_namesTable.FamilyName = _fontStream.readString(length);
							}
							break;
						case 4: //FullName
							if ((platformID == 0 || platformID == 3 || (platformID == 2 && platformEncodingID == 1))){
								_namesTable.FullName = _fontStream.readUnicodeString(length);
							}
							else {
								_namesTable.FullName = _fontStream.readString(length);
							}
							break;
						case 6:	//FontName						
							if ((platformID == 0 || platformID == 3 || (platformID == 2 && platformEncodingID == 1))) {
								_namesTable.FontName = _fontStream.readUnicodeString(length).Replace(" ","");
							} else {
								_namesTable.FontName = _fontStream.readString(length).Replace(" ","");
							}							
							break;						
					}
					if (_namesTable.FontName != null && _namesTable.FamilyName != null && _namesTable.FullName != null) {
						founded = true;
					} else {
						_fontStream.SetPosition(currentStreamPosition);
						i++;
					}					
				}
				//Check the names
				if (!founded) {
					throw new pdfBadFontFileException();
				}
			} else {
				throw new pdfBadFontFileException();
			}			
		}

		/// <summary>
		/// Method that reads a Directory Table
		/// </summary>
		/// <returns>Structure that represents the Directory Table</returns>
		private DirectoryTable readDirectoryTable()
		{
			DirectoryTable myTable = new DirectoryTable();
			myTable.Tag = _fontStream.readString(4);
			myTable.Checksum = _fontStream.readULong_BE();
			myTable.Offset = _fontStream.readULong_BE();
			myTable.Length = _fontStream.readULong_BE();
			return myTable;
		}

		/// <summary>
		/// Method that reads the FontHeader Table
		/// </summary>
		private void readFontHeaderTable()
		{			
			if (_directoryTables.ContainsKey("head")) {								
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["head"]).Offset); //Set the offset
				_fontStream.SkipBytes(16); //Table Version
				_headTable.flags = _fontStream.readUShort_BE();
				_headTable.unitsPerEm = _fontStream.readUShort_BE();
				_fontStream.SkipBytes(16);
				_headTable.xMin = _fontStream.readShort_BE();
				_headTable.yMin = _fontStream.readShort_BE();
				_headTable.xMax = _fontStream.readShort_BE();
				_headTable.yMax = _fontStream.readShort_BE();
				_headTable.macStyle = _fontStream.readUShort_BE();
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that reads the HorizontalHeader Table
		/// </summary>
		private void readHorizontalHeaderTable()
		{
			if (_directoryTables.ContainsKey("hhea")) {
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["hhea"]).Offset); //Set the offset
				_fontStream.SkipBytes(4); //Table Version
				_hheaTable.Ascender =_fontStream.readShort_BE();
				_hheaTable.Descender = _fontStream.readShort_BE();
				_hheaTable.LineGap = _fontStream.readShort_BE();
				_hheaTable.advanceWidthMax = _fontStream.readUShort_BE();
				_hheaTable.minLeftSideBearing = _fontStream.readShort_BE();
				_hheaTable.minRightSideBearing = _fontStream.readShort_BE();
				_hheaTable.xMaxExtent = _fontStream.readShort_BE();
				_hheaTable.caretSlopeRise = _fontStream.readShort_BE();
				_hheaTable.caretSlopeRun = _fontStream.readShort_BE();
				_fontStream.SkipBytes(12);
				_hheaTable.numberOfHMetrics = _fontStream.readUShort_BE();				
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that reads the OS Table
		/// </summary>
		private void readOsTable()
		{
			if (_directoryTables.ContainsKey("os/2")) {
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["os/2"]).Offset); //Set the offset
				int osVersion = _fontStream.readShort_BE();				
				_osTable.xAvgCharWidth = _fontStream.readShort_BE();
				_osTable.usWeightClass = _fontStream.readUShort_BE();
				_osTable.usWidthClass = _fontStream.readUShort_BE();
				_osTable.fsType = _fontStream.readShort_BE();
				_osTable.ySubscriptXSize = _fontStream.readShort_BE();
				_osTable.ySubscriptYSize = _fontStream.readShort_BE();
				_osTable.ySubscriptXOffset = _fontStream.readShort_BE();
				_osTable.ySubscriptYOffset = _fontStream.readShort_BE();
				_osTable.ySuperscriptXSize = _fontStream.readShort_BE();
				_osTable.ySuperscriptYSize = _fontStream.readShort_BE();
				_osTable.ySuperscriptXOffset = _fontStream.readShort_BE();
				_osTable.ySuperscriptYOffset = _fontStream.readShort_BE();
				_osTable.yStrikeoutSize = _fontStream.readShort_BE();
				_osTable.yStrikeoutPosition = _fontStream.readShort_BE();
				_osTable.sFamilyClass = _fontStream.readShort_BE();
				_osTable.panose = _fontStream.readByteArray(10);
				_fontStream.SkipBytes(16);				
				_osTable.achVendID = _fontStream.readString(4);
				_osTable.fsSelection = _fontStream.readUShort_BE();
				_osTable.usFirstCharIndex = _fontStream.readUShort_BE();
				_osTable.usLastCharIndex = _fontStream.readUShort_BE();
				_osTable.sTypoAscender = _fontStream.readShort_BE();
				_osTable.sTypoDescender = _fontStream.readShort_BE();
				_osTable.sTypoLineGap = _fontStream.readShort_BE();
				_osTable.usWinAscent = _fontStream.readUShort_BE();
				_osTable.usWinDescent = _fontStream.readUShort_BE();
				_osTable.ulCodePageRange1 = 0;
				_osTable.ulCodePageRange2 = 0;
				if (osVersion > 0) {
					_osTable.ulCodePageRange1 = _fontStream.readInt_BE();
					_osTable.ulCodePageRange2 = _fontStream.readInt_BE();
				}
				if (osVersion > 1) {
					_fontStream.SkipBytes(2);
					_osTable.sCapHeight = _fontStream.readShort_BE();
				} else {
					_osTable.sCapHeight = (int)(0.7 * _headTable.unitsPerEm);
				}
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that reads the PostScript Table
		/// </summary>
		private void readPostScriptTable()
		{
			if (_directoryTables.ContainsKey("post")) {
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["post"]).Offset); //Set the offset
				_fontStream.SkipBytes(4); //Table Version
				short mantissa = _fontStream.readShort_BE();
				int fraction = _fontStream.readUShort_BE();
				_postTable.ItalicAngle = (double)mantissa + (double)fraction / 16384.0;
				_postTable.UnderlinePosition = _fontStream.readInt_BE();
				_postTable.UnderlineThickness = _fontStream.readInt_BE();				
				_postTable.IsFixedPitch = (_fontStream.readInt_BE() != 0);
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that reads the Glyph's Widths
		/// </summary>
		private void readGlyphWidths()
		{
			if (_directoryTables.ContainsKey("hmtx")) {
				_GlyphWidths = new int[_hheaTable.numberOfHMetrics];
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["hmtx"]).Offset);
				for(int i = 0; i < _hheaTable.numberOfHMetrics; i++)
				{
					_GlyphWidths[i] = (int)((_fontStream.readUShort_BE() * 1000) / _headTable.unitsPerEm);
					_fontStream.SkipBytes(2);
				}
			} else {
				throw new pdfBadFontFileException();
            }
		}

		/// <summary>
		/// Method that reads the CMAP
		/// </summary>
		private void readCMAP()
		{
			if (_directoryTables.ContainsKey("cmap")) {
				_fontStream.SetPosition(((DirectoryTable)_directoryTables["cmap"]).Offset);
				_fontStream.SkipBytes(2); //Table Version
				int tblNumber = _fontStream.readUShort_BE();
				int platformID;
				int encodingID;
				int offset;
				int format;
				for (int i = 0; i < tblNumber; i++)
				{
					platformID = _fontStream.readUShort_BE();
					encodingID = _fontStream.readUShort_BE();
					offset = _fontStream.readULong_BE();
					_CMAPTable.Add(new CMAPTable(platformID,encodingID,offset));
				}
				foreach(CMAPTable CMAP in _CMAPTable)
				{
					_fontStream.SetPosition(((DirectoryTable)_directoryTables["cmap"]).Offset + CMAP.Offset);
					format = _fontStream.readUShort_BE();
					switch (CMAP.PlatformID) {
						case 1: //Apple
							switch (format) {
								case 0:
									CMAP.Mapping = readFormat0();
									break;
								case 4:
									CMAP.Mapping = readFormat4();
									break;
								case 6:
									CMAP.Mapping = readFormat6();
									break;
							}
							break;
						case 3: //Microsoft
							if (format == 4) {
								CMAP.Mapping = readFormat4();
							}
							break;
					}
				}
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that read the TTF Format 0
		/// </summary>
		/// <returns>Hashtable with glyph's informations</returns>
		private Hashtable readFormat0()
		{
			int[] glyphInfo;
			Hashtable returnTable = new Hashtable();
			_fontStream.SkipBytes(2); //Skip Version
			_fontStream.SkipBytes(2); //Skip Length
			for (int i = 0; i < 256; i++)
			{
				glyphInfo = new int[2];
				glyphInfo[0] = _fontStream.readByte();
				glyphInfo[1] = _GlyphWidths[glyphInfo[0]];
				returnTable.Add(i,glyphInfo);
				glyphInfo = null;
			}
			return returnTable;
		}

		/// <summary>
		/// Method that read the TTF Format 4
		/// </summary>
		/// <returns>Hashtable with glyph's informations</returns>
		private Hashtable readFormat4()
		{
			int i,j,glyph,idx;
			Hashtable returnTable = new Hashtable();
			int[] glyphInfo;
			int table_lenght = _fontStream.readUShort_BE();
			_fontStream.SkipBytes(2);
			int seg =  _fontStream.readUShort_BE() / 2;
			_fontStream.SkipBytes(6);
			int[] end = new int[seg];
			for (i = 0; i < seg; i++) {
				end[i] = _fontStream.readUShort_BE();
			}
			_fontStream.SkipBytes(2);
			int[] start = new int[seg];
			for (i = 0; i < seg; i++) {
				start[i] = _fontStream.readUShort_BE();
			}
			int[] delta = new int[seg];
			for (i = 0; i < seg; i++) {
				delta[i] = _fontStream.readUShort_BE();
			}
			int[] ro = new int[seg];
			for (i = 0; i < seg; i++) {
				ro[i] = _fontStream.readUShort_BE();
			}
			int[] glyphId = new int[table_lenght / 2 - 8 - seg * 4];
			for (i = 0; i < glyphId.Length; i++) {
				glyphId[i] = _fontStream.readUShort_BE();
			}
			for (i = 0; i < seg; i++) {
				for (j = start[i]; j <= end[i] && j != 0xFFFF; j++) {
					if (ro[i] == 0) {
						glyph = (j + delta[i]) & 0xFFFF;
					}
					else {
						idx = i + ro[i] / 2 - seg + j - start[i];
						glyph = (glyphId[idx] + delta[i]) & 0xFFFF;
					}
					glyphInfo = new int[2];
					glyphInfo[0] = glyph;
					glyphInfo[1] = _GlyphWidths[glyphInfo[0]];
					returnTable.Add(j, glyphInfo);
				}
			}
			return returnTable;
		}

		/// <summary>
		/// Method that read the TTF Format 6
		/// </summary>
		/// <returns>Hashtable with glyph's informations</returns>
		private Hashtable readFormat6()
		{
			int[] glyphInfo;
			Hashtable returnTable = new Hashtable();
			_fontStream.SkipBytes(2); //Skip Version
			_fontStream.SkipBytes(2); //Skip Length
			int start = _fontStream.readUShort_BE();
			int count = _fontStream.readUShort_BE();
			for (int i = 0; i < count; i++)
			{
				glyphInfo = new int[2];
				glyphInfo[0] = _fontStream.readByte();
				glyphInfo[1] = _GlyphWidths[glyphInfo[0]];
				returnTable.Add(i + start,glyphInfo);
				glyphInfo = null;
			}
			return returnTable;
		}

		/// <summary>
		/// Method that returns the CMAP table
		/// </summary>
		/// <param name="platformID">ID of the platform</param>
		/// <param name="encodingID">ID of the encoding</param>
		/// <returns>CMAP object</returns>
		private CMAPTable getCMAP(int platformID, int encodingID)
		{
			CMAPTable returnCMAP = null;
			int i = 0;
			while (i<_CMAPTable.Count && returnCMAP == null)
			{
				if (((CMAPTable)_CMAPTable[i]).PlatformID == platformID && ((CMAPTable)_CMAPTable[i]).EncodingID == encodingID) {
					returnCMAP = (CMAPTable)_CMAPTable[i];
				}
				i++;
			}
			return returnCMAP;
		}		

	}
}
