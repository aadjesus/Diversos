//*****************************************************************
//*****************Generated with SharpDeveloper*******************
//*****************************************************************

using System;
using System.Collections;
using System.Text;

using sharpPDF.Tables;

namespace sharpPDF.Collections
{

	/// <summary>
	/// Class that represents a list of rows
	/// </summary>
	[Serializable()]
	public class rowList : CollectionBase {
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		public rowList()
		{
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">Another rowList</param>
		public rowList(rowList val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">An array of pdfTableRow</param>
		public rowList(pdfTableRow[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's Indexer
		/// </summary>
		public pdfTableRow this[int index] {
			get {
				return ((pdfTableRow)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		/// Method that adds a row to the list
		/// </summary>
		/// <param name="val">Row object</param>
		/// <returns></returns>
		public int Add(pdfTableRow val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// Method that adds row to the list
		/// </summary>
		/// <param name="val">Array of row objects</param>
		public void AddRange(pdfTableRow[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that adds rows to the list
		/// </summary>
		/// <param name="val">Another rowList</param>
		public void AddRange(rowList val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that shows if a row is contained into the collection
		/// </summary>
		/// <param name="val">pdfTableRow object</param>
		/// <returns>Value that tells if the object is contained</returns>
		public bool Contains(pdfTableRow val)
		{
			return List.Contains(val);
		}

		/// <summary>
		/// Method that copies the row into an array
		/// </summary>
		/// <param name="array">Array of rows</param>
		/// <param name="index">Start index</param>
		public void CopyTo(pdfTableRow[] array, int index)
		{
			List.CopyTo(array, index);
		}

		/// <summary>
		/// Method that returns the index of a row object
		/// </summary>
		/// <param name="val">Row object</param>
		/// <returns>Index of the row inside the list</returns>
		public int IndexOf(pdfTableRow val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// Method that inserts a row object at a defined index
		/// </summary>
		/// <param name="index">Index of the row</param>
		/// <param name="val">Row object</param>
		public void Insert(int index, pdfTableRow val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		/// Method that retursn the enumerator of the list
		/// </summary>
		/// <returns>ColumnEnumerator object</returns>
		public new rowEnumerator GetEnumerator()
		{
			return new rowEnumerator(this);
		}
		
		/// <summary>
		/// Method that removes a row object
		/// </summary>
		/// <param name="val">Row to remove</param>
		public void Remove(pdfTableRow val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		/// Class that represents the rowList's enumerator
		/// </summary>
		public class rowEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			/// Class's constructor
			/// </summary>
			/// <param name="mappings">Mappings</param>
			public rowEnumerator(rowList mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			/// The current element of the collection
			/// </summary>
			public pdfTableRow Current {
				get {
					return ((pdfTableRow)(baseEnumerator.Current));
				}
			}
			
			/// <summary>
			/// The current element of the collection
			/// </summary>
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			/// <summary>
			/// Method that moves to the next element of the collection
			/// </summary>
			/// <returns>Value that tells of the operation was succesfully</returns>
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			
			/// <summary>
			/// Method that moves the enumerator to the first position of the collection
			/// </summary>
			public void Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}
