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
	/// Class that represents a list of paragraph's lines
	/// </summary>
	[Serializable()]
	public class paragraphLineList : CollectionBase {
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		public paragraphLineList()
		{
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">Another paragraphLineList</param>
		public paragraphLineList(paragraphLineList val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="val">An array of paragraphLine</param>
		public paragraphLineList(paragraphLine[] val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		/// Class's Indexer
		/// </summary>
		public paragraphLine this[int index] {
			get {
				return ((paragraphLine)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		/// Method that adds a paragraphLine to the list
		/// </summary>
		/// <param name="val">ParagraphLine object</param>
		/// <returns></returns>
		public int Add(paragraphLine val)
		{
			return List.Add(val);
		}
		
		/// <summary>
		/// Method that adds paragraphLines to the list
		/// </summary>
		/// <param name="val">Array of paragraphLine objects</param>
		public void AddRange(paragraphLine[] val)
		{
			for (int i = 0; i < val.Length; i++) {
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that adds paragraphLines to the list
		/// </summary>
		/// <param name="val">Another paragraphLineList</param>
		public void AddRange(paragraphLineList val)
		{
			for (int i = 0; i < val.Count; i++)
			{
				this.Add(val[i]);
			}
		}
		
		/// <summary>
		/// Method that shows if a paragraphLine is contained into the collection
		/// </summary>
		/// <param name="val">ParagraphLine object</param>
		/// <returns>Value that tells if the object is contained</returns>
		public bool Contains(paragraphLine val)
		{
			return List.Contains(val);
		}

		/// <summary>
		/// Method that copies the paragraphLines into an array
		/// </summary>
		/// <param name="array">Array of paragraphLines</param>
		/// <param name="index">Start index</param>
		public void CopyTo(paragraphLine[] array, int index)
		{
			List.CopyTo(array, index);
		}

		/// <summary>
		/// Method that returns the index of a paragraphLine object
		/// </summary>
		/// <param name="val">ParagraphLine object</param>
		/// <returns>Index of the paragraphLine inside the list</returns>
		public int IndexOf(paragraphLine val)
		{
			return List.IndexOf(val);
		}
		
		/// <summary>
		/// Method that inserts a paragraphLine object at a defined index
		/// </summary>
		/// <param name="index">Index of the paragraphLine</param>
		/// <param name="val">ParagraphLine object</param>
		public void Insert(int index, paragraphLine val)
		{
			List.Insert(index, val);
		}
		
		/// <summary>
		/// Method that retursn the enumerator of the list
		/// </summary>
		/// <returns>ParagraphLineEnumerator object</returns>
		public new paragraphLineEnumerator GetEnumerator()
		{
			return new paragraphLineEnumerator(this);
		}
		
		/// <summary>
		/// Method that removes a paragraphLine object
		/// </summary>
		/// <param name="val">ParagraphLine to remove</param>
		public void Remove(paragraphLine val)
		{
			List.Remove(val);
		}
		
		/// <summary>
		/// Class that represents the paragraphLineList's enumerator
		/// </summary>
		public class paragraphLineEnumerator : IEnumerator
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			/// Class's constructor
			/// </summary>
			/// <param name="mappings">Mappings</param>
			public paragraphLineEnumerator(paragraphLineList mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			/// The current element of the collection
			/// </summary>
			public paragraphLine Current {
				get {
					return ((paragraphLine)(baseEnumerator.Current));
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
