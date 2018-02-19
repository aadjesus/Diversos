using System;
using System.Collections;
using System.Reflection;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Extends the CollectionBase - PieSegmentCollection.
	/// </summary>
	public class PieSegmentCollection : CollectionBase, IList
	{
		//
		/// <summary>
		/// Return the item at the specified index.
		/// </summary>
		public PieSegment this[int index]
		{
			get
			{
				return (PieSegment)List[index];
			}
		}
		/// <summary>
		/// Adds elements to the list.
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(PieSegment[] items)
		{
			foreach (PieSegment item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Add one element to the list.
		/// </summary>
		/// <param name="item"></param>
		public void Add(PieSegment item)
		{
			List.Add(item);
		}

		/// <summary>
		/// Inserts an item at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, PieSegment item)
		{
			this.List.Insert(index, item);
		}

		/// <summary>
		/// Copies a range of elements from the ArrayList to a compatible one-dimensional Array, 
		/// starting at the specified index of the target array.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(PieSegment[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>
		/// When implemented by a class, determines the index of a specific item in the IList.
		/// </summary>
		/// <param name="item"></param>
		public int IndexOf(PieSegment item)
		{
			return this.List.IndexOf(item);
		}

		/// <summary>
		/// Remove the item from the list
		/// </summary>
		/// <param name="value"></param>
		public void Remove(PieSegment value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Determine if the list contains the item
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(PieSegment value)
		{
			return this.List.Contains(value);
		}
	}
}
