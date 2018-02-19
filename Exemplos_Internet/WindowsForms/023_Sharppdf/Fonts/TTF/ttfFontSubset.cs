//*****************************************************************
//******************Code inspired by iTextSharp********************
//*****************************************************************
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
	/// Class that creates a new TTF font file stream, based on a subset of chars
	/// </summary>
	internal class ttfFontSubset : IDisposable
	{

		#region Static_Properties

		internal static int[] entrySelectors = {0,0,1,1,2,2,2,2,3,3,3,3,3,3,3,3,4,4,4,4,4};

		internal static int FORMAT_OFFSET = 51;

		#endregion

		private string _baseFileName;		
		private AdvancedFileStream _fontStream;		
		private Hashtable _directoryTables;
		private bool _isShortLocaTable;
		private int[] _locaTable;
		private Hashtable _usedGlyphs;
		private ArrayList _glyphKeys;
		private int[] _newLocaTable;
		private byte[] _newLocaTableOut;
		private byte[] _newGlyfTable;
		private int _realGlyfSize;
		private int _realLocaSize;
		private int _oldGlyfOffset;
		private int _directoryOffset = 0;
		private bool _disposed = false;

		#region ~Ctor

		private AdvancedMemoryStream _subsetStream;

		public ttfFontSubset(string baseFileName, Hashtable usedCharacters)
		{
			_baseFileName = baseFileName;
			_usedGlyphs = usedCharacters;
			_glyphKeys = new ArrayList(_usedGlyphs.Keys);
			_subsetStream = new AdvancedMemoryStream();
						
		}

		public void Dispose() {
			if (!this._disposed) {
				if (this._subsetStream != null) {
					this._subsetStream.Close();
					this._subsetStream = null;
				}
				this._glyphKeys = null;
				this._disposed = true;
			}
		}

		#endregion

		/// <summary>
		/// Method that returns a byte-stream of the True Type Font subset
		/// </summary>
		/// <returns></returns>
		public byte[] getFontFileStream()
		{
			byte[] resultFont = null;
			try 
			{
				_fontStream = new AdvancedFileStream(_baseFileName, FileMode.Open);
				_subsetStream = new AdvancedMemoryStream();
				CreateDirectoryTable();
				ReadLocaTable();
				FlatGlyphs();
				CreateGlyphTables();
				LocaOldTobytesLocaNew();				
				CreateFontSubset();
				resultFont = _subsetStream.ToArray();				
			} 
			catch (IOException ex) 
			{
				throw new sharpPDF.Exceptions.pdfGenericIOException(ex.Message, ex);
			}
			finally
			{
				if (_fontStream != null) {
					_fontStream.Close();
					_fontStream = null;
				}
				if (_subsetStream != null) {
					_subsetStream.Close();
					_subsetStream = null;
				}
			}			
			return resultFont;
		}

		/// <summary>
		/// Methotd that creates the Directory Table
		/// </summary>
		private void CreateDirectoryTable() {
			DirectoryTable tempTable;
			_directoryTables = new Hashtable();
			_fontStream.SetPosition(_directoryOffset);
			_fontStream.SkipBytes(4);
			int nTables = _fontStream.readUShort_BE();
			_fontStream.SkipBytes(6);
			for (int i = 0; i < nTables; i++) {
				tempTable = ReadDirectoryTable();
				_directoryTables.Add(tempTable.Tag.ToLower(), tempTable);
			}			
		}

		/// <summary>
		/// Methdo that reads the Directory Table
		/// </summary>
		/// <returns></returns>
		private DirectoryTable ReadDirectoryTable() {
			DirectoryTable myTable = new DirectoryTable();
			myTable.Tag = _fontStream.readString(4);
			myTable.Checksum = _fontStream.readULong_BE();
			myTable.Offset = _fontStream.readULong_BE();
			myTable.Length = _fontStream.readULong_BE();
			return myTable;
		}

		/// <summary>
		/// Method that reads the Loca Table
		/// </summary>
		private void ReadLocaTable() {
			
			DirectoryTable tempTable;
			int i, entries;
			if ((_directoryTables.Contains("head")) && (_directoryTables.Contains("loca"))) {
				tempTable = (DirectoryTable)_directoryTables["head"];				
				_fontStream.SetPosition(tempTable.Offset + ttfFontSubset.FORMAT_OFFSET);
				_isShortLocaTable = (_fontStream.readUShort_BE() == 0);
				tempTable = (DirectoryTable)_directoryTables["loca"];			
				_fontStream.SetPosition(tempTable.Offset);
				if (_isShortLocaTable) {
					entries = tempTable.Length / 2;
					_locaTable = new int[entries];
					for (i = 0; i < entries; i++)
						_locaTable[i] = _fontStream.readUShort_BE() * 2;
				}
				else {
					entries = tempTable.Length / 4;
					_locaTable = new int[entries];
					for (i = 0; i < entries; i++)
						_locaTable[i] = _fontStream.readULong_BE();
				}
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that converts the Loca Table to bytes
		/// </summary>
		protected void LocaOldTobytesLocaNew() {
			int newValue;
			int arrayIndex = 0;
			if (_isShortLocaTable)
				_realLocaSize = _newLocaTable.Length * 2;
			else
				_realLocaSize = _newLocaTable.Length * 4;
			_newLocaTableOut = new byte[(_realLocaSize + 3) & (~3)];
			for (int i = 0; i < _newLocaTable.Length; i++) {				
				if (_isShortLocaTable) {					
					newValue = _newLocaTable[i] / 2;
					_newLocaTableOut[arrayIndex] = (byte)(newValue >> 8);
					arrayIndex++;
					_newLocaTableOut[arrayIndex] = (byte)(newValue & 0x00ff);
					arrayIndex++;									
				} else {					
					newValue = _newLocaTable[i];
					_newLocaTableOut[arrayIndex] = (byte)(newValue >> 24);
					arrayIndex++;
					_newLocaTableOut[arrayIndex] = (byte)((newValue >> 16) & 0x000000ff);
					arrayIndex++;
					_newLocaTableOut[arrayIndex] = (byte)((newValue >> 8) & 0x000000ff);
					arrayIndex++;
					_newLocaTableOut[arrayIndex] = (byte)(newValue & 0x000000ff);
					arrayIndex++;
				}
				
			}
		}

		/// <summary>
		/// Method that creates the font subset
		/// </summary>
		protected void CreateFontSubset() {
			DirectoryTable tableLocation;
			int subsetFontSize = 0;
			string[] tableNames;
			
			/* Standard Names Without CMAP - Actually not supported
			
			tableNames = new string[]{"cvt ", "fpgm", "glyf", "head",
														"hhea", "hmtx", "loca", "maxp", "prep"};
			
			*/

			tableNames = new string[]{"cmap", "cvt ", "fpgm", "glyf", "head",
													  "hhea", "hmtx", "loca", "maxp", "prep"};
			int tablesUsed = 2;
			int length = 0;
			foreach(string name in tableNames) {			
				if (name.Equals("glyf") || name.Equals("loca"))
					continue;
				if (_directoryTables.Contains(name)) {
					tableLocation = (DirectoryTable)_directoryTables[name];					
					subsetFontSize += (tableLocation.Length + 3) & (~3);
					tablesUsed++;
				}
			}
			subsetFontSize += _newLocaTableOut.Length;
			subsetFontSize += _newGlyfTable.Length;
			int iref = 16 * tablesUsed + 12;
			subsetFontSize += iref;			
			_subsetStream.writeULong_BE(0x00010000);
			_subsetStream.writeUShort_BE(tablesUsed);
			int selector = entrySelectors[tablesUsed];
			_subsetStream.writeUShort_BE((1 << selector) * 16);
			_subsetStream.writeUShort_BE(selector);
			_subsetStream.writeUShort_BE((tablesUsed - (1 << selector)) * 16);
			foreach(string name in tableNames) {
				if (_directoryTables.Contains(name)) {					
					tableLocation = (DirectoryTable)_directoryTables[name];
					_subsetStream.writeString(name);
					if (name.Equals("glyf")) {
						_subsetStream.writeULong_BE(CalculateChecksum(_newGlyfTable));
						length = _realGlyfSize;						
					}
					else if (name.Equals("loca")) {
						_subsetStream.writeULong_BE(CalculateChecksum(_newLocaTableOut));
						length = _realLocaSize;						
					}
					else {
						_subsetStream.writeULong_BE(tableLocation.Checksum);
						length = tableLocation.Length;
					}
					_subsetStream.writeULong_BE(iref);
					_subsetStream.writeULong_BE(length);
					iref += (length + 3) & (~3);
				}
			}
			foreach(string name in tableNames) {
				if (_directoryTables.Contains(name)) {					
					tableLocation = (DirectoryTable)_directoryTables[name];
					if (name.Equals("glyf")) {
						_subsetStream.writeByteArray(_newGlyfTable);
						_newGlyfTable = null;
					}
					else if (name.Equals("loca")) {						
						_subsetStream.writeByteArray(_newLocaTableOut);						
						_newLocaTableOut = null;
					}
					else {
						_fontStream.SetPosition(tableLocation.Offset);
						byte[] tempArray  = new byte[(tableLocation.Length + 3) & (~3)];
						tempArray.Initialize();
						_fontStream.readByteArray(tableLocation.Length).CopyTo(tempArray, 0);						
						_subsetStream.writeByteArray(tempArray);										
					}
				}
			}
		}

		/// <summary>
		/// Method that checks the Glyphs
		/// </summary>
		protected void FlatGlyphs() {
			DirectoryTable glyfTable;			
			if (_directoryTables.Contains("glyf")) {				
				glyfTable = (DirectoryTable)_directoryTables["glyf"];
				_oldGlyfOffset = glyfTable.Offset;
				if (!_usedGlyphs.ContainsKey(0)) {
					_usedGlyphs.Add(0, null);
					_glyphKeys.Add(0);
				}
				for (int i = 0; i < _glyphKeys.Count; i++) {
					CheckGlyphComp((int)_glyphKeys[i]);
				}
			} else {
				throw new pdfBadFontFileException();
			}
		}

		/// <summary>
		/// Method that checks a single Glyph's composition
		/// </summary>
		/// <param name="glyph">Glyph code</param>
		protected void CheckGlyphComp(int glyph) {
			int start = _locaTable[glyph];
			if (start == _locaTable[glyph + 1]) // no contour
				return;
			_fontStream.SetPosition(_oldGlyfOffset + start);
			int numContours = _fontStream.readShort_BE();
			if (numContours >= 0)
				return;
			_fontStream.SkipBytes(8);
			bool finished = false;
			int skip;
			while (!finished) {
				int flags = _fontStream.readUShort_BE();
				int cGlyph = _fontStream.readUShort_BE();				
				if (!_usedGlyphs.ContainsKey(cGlyph)) {
					_usedGlyphs.Add(cGlyph, null);
					_glyphKeys.Add(cGlyph);
				}				
				if ((flags & (int)glyphType.csMoreComponents) == 0)
					finished = true;
				skip = 0;
				if ((flags & (int)glyphType.csTwoArgs) != 0)
					skip = 4;
				else
					skip = 2;
				if ((flags & (int)glyphType.csSingleScale) != 0)
					skip += 2;
				else if ((flags & (int)glyphType.csDoubleScale) != 0)
					skip += 4;
				if ((flags & (int)glyphType.csTwoByTwo) != 0)
					skip += 8;
				_fontStream.SkipBytes(skip);
			}
		}

		/// <summary>
		/// Method that creates the new Glyph Table
		/// </summary>
		protected void CreateGlyphTables() {
			_newLocaTable = new int[_locaTable.Length];
			int[] activeGlyphs = new int[_glyphKeys.Count];
			for (int i = 0; i < activeGlyphs.Length; ++i)
				activeGlyphs[i] = (int)_glyphKeys[i];
			Array.Sort(activeGlyphs);
			_realGlyfSize = 0;
			foreach(int glyph in _glyphKeys) {
				_realGlyfSize += _locaTable[glyph + 1] - _locaTable[glyph];
			}
			_newGlyfTable = new byte[(_realGlyfSize + 3) & (~3)];
			int glyfPtr = 0;
			int listGlyf = 0;
			for (int i = 0; i < _newLocaTable.Length; ++i) {
				_newLocaTable[i] = glyfPtr;
				if (listGlyf < activeGlyphs.Length && activeGlyphs[listGlyf] == i) {
					listGlyf++;
					_newLocaTable[i] = glyfPtr;
					int start = _locaTable[i];
					int len = _locaTable[i + 1] - start;
					if (len > 0) {
						_fontStream.SetPosition(_oldGlyfOffset + start);
						_fontStream.readByteArray(len).CopyTo(_newGlyfTable,glyfPtr);
						glyfPtr += len;
					}
				}
			}
		}		
		
		/// <summary>
		/// Method that calculates the checksum for the subset
		/// </summary>
		/// <param name="byteArray">byte array</param>
		/// <returns>Checksum</returns>
		protected int CalculateChecksum(byte[] byteArray) {
			int arrLength = byteArray.Length / 4;
			int[] checksumArray = new int[] {0, 0, 0, 0};			
			for (int i = 0; i < arrLength; i=i+4) {
				checksumArray[3] += (int)byteArray[i] & 0xff;
				checksumArray[2] += (int)byteArray[i+1] & 0xff;
				checksumArray[1] += (int)byteArray[i+2] & 0xff;
				checksumArray[0] += (int)byteArray[i+3] & 0xff;
			}
			return checksumArray[0] + (checksumArray[1] << 8) + (checksumArray[2] << 16) + (checksumArray[3] << 24);
		}
		
	}
}
