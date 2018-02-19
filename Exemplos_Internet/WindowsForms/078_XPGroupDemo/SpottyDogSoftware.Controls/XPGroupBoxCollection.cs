using System;
using System.Collections;
using System.Reflection;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// A collection of XPGroupBoxes - used by the XPGroupBoxContainer to provide a means
	/// to order the boxes.
	/// </summary>
	public class XPGroupBoxCollection : CollectionBase, IList
	{
		/// <summary>
		/// Raised when a Segment is clicked on the control
		/// </summary>
		public event CollectionUpdatedEventHandler CollectionUpdated;

		/// <summary>
		/// Return the item at the specified index.
		/// </summary>
		public XPGroupBox this[int index]
		{
			get
			{
				return (XPGroupBox)List[index];
			}
		}
		/// <summary>
		/// Adds elements to the list.
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(XPGroupBox[] items)
		{
			foreach (XPGroupBox item in items)
			{
				this.List.Add(item);
			}
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Add one element to the list.
		/// </summary>
		/// <param name="item"></param>
		public void Add(XPGroupBox item)
		{
			List.Add(item);
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Inserts an item at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, XPGroupBox item)
		{
			this.List.Insert(index, item);
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Copies a range of elements from the ArrayList to a compatible one-dimensional Array, 
		/// starting at the specified index of the target array.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(XPGroupBox[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>
		/// When implemented by a class, determines the index of a specific item in the IList.
		/// </summary>
		/// <param name="item"></param>
		public int IndexOf(XPGroupBox item)
		{
			return this.List.IndexOf(item);
		}

		/// <summary>
		/// Remove the item from the list
		/// </summary>
		/// <param name="value"></param>
		public void Remove(XPGroupBox value)
		{
			this.List.Remove(value);
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Determine if the list contains the item
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(XPGroupBox value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Handles the OnUpdate event 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCollectionUpdated(EventArgs e)
		{
			if (CollectionUpdated != null) 
			{
				// Invokes the delegates. 
				CollectionUpdated(this, e);
			}
		}

	}
}
