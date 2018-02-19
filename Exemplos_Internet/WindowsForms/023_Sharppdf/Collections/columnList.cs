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
	/// Class that represents a list of columns
	/// </summary>
	[Serializable()]
	public class columnList : CollectionBase {
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		public columnList()
		{
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">Another columnList</param>
		public columnList(columnList val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">An array of pdfTableColumn</param>
		public columnList(pdfTableColumn[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's Indexer
		/// </summary>
		public pdfTableColumn this[int index] {
			get {
				return ((pdfTableColumn)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		/// Method that adds a columns to the list
		/// </summary>
		/// <param name="val">Column object</param>
		/// <returns></returns>
		public int Add(pdfTableColumn val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// Method that adds columns to the list
		/// </summary>
		/// <param name="val">Array of column objects</param>
		public void AddRange(pdfTableColumn[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that adds columns to the list
		/// </summary>
		/// <param name="val">Another columnList</param>
		public void AddRange(columnList val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that shows if a columns is contained into the collection
		/// </summary>
		/// <param name="val">pdfTableColumn object</param>
		/// <returns>Value that tells if the object is contained</returns>
		public bool Contains(pdfTableColumn val)
		{
			return List.Contains(val);
		}

		/// <summary>
		/// Method that copies the columns into an array
		/// </summary>
		/// <param name="array">Array of columns</param>
		/// <param name="index">Start index</param>
		public void CopyTo(pdfTableColumn[] array, int index)
		{
			List.CopyTo(array, index);
		}

		/// <summary>
		/// Method that returns the index of a column object
		/// </summary>
		/// <param name="val">Column object</param>
		/// <returns>Index of the column inside the list</returns>
		public int IndexOf(pdfTableColumn val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// Method that inserts a column object at a defined index
		/// </summary>
		/// <param name="index">Index of the column</param>
		/// <param name="val">Column object</param>
		public void Insert(int index, pdfTableColumn val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		/// Method that retursn the enumerator of the list
		/// </summary>
		/// <returns>ColumnEnumerator object</returns>
		public new columnEnumerator GetEnumerator()
		{
			return new columnEnumerator(this);
		}
		
		/// <summary>
		/// Method that removes a column object
		/// </summary>
		/// <param name="val">Column to remove</param>
		public void Remove(pdfTableColumn val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		/// Class that represents the columnList's enumerator
		/// </summary>
		public class columnEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;

			/// <summary>
			/// Class's constructor
			/// </summary>
			/// <param name="mappings">Mappings</param>
			public columnEnumerator(columnList mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}

			/// <summary>
			/// The current element of the collection
			/// </summary>
			public pdfTableColumn Current {
				get {
					return ((pdfTableColumn)(baseEnumerator.Current));
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
