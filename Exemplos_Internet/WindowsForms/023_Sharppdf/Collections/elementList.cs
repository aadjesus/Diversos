//*****************************************************************
//*****************Generated with SharpDeveloper*******************
//*****************************************************************

using System;
using System.Collections;
using System.Text;

using sharpPDF.Elements;

namespace sharpPDF.Collections
{

	/// <summary>
	/// Class that represents a colledtion of pdfElements
	/// </summary>
	[Serializable()]	
	public class elementList : CollectionBase {
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		public elementList()
		{
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">Another elementList</param>
		public elementList(elementList val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">An array of pdfElement</param>
		public elementList(pdfElement[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's Indexer
		/// </summary>
		public pdfElement this[int index] {
			get {
				return ((pdfElement)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		/// Method that adds an element to the list
		/// </summary>
		/// <param name="val">Column object</param>
		/// <returns></returns>
		public int Add(pdfElement val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// Method that adds element to the list
		/// </summary>
		/// <param name="val">Array of column objects</param>
		public void AddRange(pdfElement[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that adds elements to the list
		/// </summary>
		/// <param name="val">Another columnList</param>
		public void AddRange(elementList val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that shows if a element is contained into the collection
		/// </summary>
		/// <param name="val">pdfElement object</param>
		/// <returns>Value that tells if the object is contained</returns>
		public bool Contains(pdfElement val)
		{
			return List.Contains(val);
		}

		/// <summary>
		/// Method that copies the elements into an array
		/// </summary>
		/// <param name="array">Array of elementf</param>
		/// <param name="index">Start index</param>
		public void CopyTo(pdfElement[] array, int index)
		{
			List.CopyTo(array, index);
		}

		/// <summary>
		/// Method that returns the index of a element object
		/// </summary>
		/// <param name="val">Element object</param>
		/// <returns>Index of the element inside the list</returns>
		public int IndexOf(pdfElement val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// Method that inserts a element object at a defined index
		/// </summary>
		/// <param name="index">Index of the element</param>
		/// <param name="val">Element object</param>
		public void Insert(int index, pdfElement val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		/// Method that retursn the enumerator of the list
		/// </summary>
		/// <returns>ElementEnumerator object</returns>
		public new elementEnumerator GetEnumerator()
		{
			return new elementEnumerator(this);
		}
		
		/// <summary>
		/// Method that removes a element object
		/// </summary>
		/// <param name="val">Element to remove</param>
		public void Remove(pdfElement val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		/// Class that represents the elementList's enumerator
		/// </summary>
		public class elementEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			/// Class's constructor
			/// </summary>
			/// <param name="mappings">Mappings</param>
			public elementEnumerator(elementList mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			/// The current element of the collection
			/// </summary>
			public pdfElement Current {
				get {
					return ((pdfElement)(baseEnumerator.Current));
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
