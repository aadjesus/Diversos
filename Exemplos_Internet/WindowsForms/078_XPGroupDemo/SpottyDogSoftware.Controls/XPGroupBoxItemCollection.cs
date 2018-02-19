using System;
using System.Collections;
using System.Reflection;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Delegate invoked when the collection is updated
	/// </summary>
	public delegate void CollectionUpdatedEventHandler(object sender, EventArgs e);
	
	/// <summary>
	/// A collection of XPGroupBoxItems. Used by the XPGroupBox control to maintain
	/// a list of the XPGroupBoxItems contained.
	/// </summary>
	public class XPGroupBoxItemCollection : CollectionBase, IList
	{
		/// <summary>
		/// Raised when a Segment is clicked on the control
		/// </summary>
		public event CollectionUpdatedEventHandler CollectionUpdated;

		/// <summary>
		/// Return the item at the specified index.
		/// </summary>
		public XPGroupBoxItem this[int index]
		{
			get
			{
				return (XPGroupBoxItem)List[index];
			}
		}
		/// <summary>
		/// Adds elements to the list.
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(XPGroupBoxItem[] items)
		{
			foreach (XPGroupBoxItem item in items)
			{
				this.List.Add(item);
			}
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Add one element to the list.
		/// </summary>
		/// <param name="item"></param>
		public void Add(XPGroupBoxItem item)
		{
			List.Add(item);
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Inserts an item at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, XPGroupBoxItem item)
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
		public void CopyTo(XPGroupBoxItem[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>
		/// When implemented by a class, determines the index of a specific item in the IList.
		/// </summary>
		/// <param name="item"></param>
		public int IndexOf(XPGroupBoxItem item)
		{
			return this.List.IndexOf(item);
		}

		/// <summary>
		/// Remove the item from the list
		/// </summary>
		/// <param name="value"></param>
		public void Remove(XPGroupBoxItem value)
		{
			this.List.Remove(value);
			OnCollectionUpdated(new EventArgs());
		}

		/// <summary>
		/// Determine if the list contains the item
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(XPGroupBoxItem value)
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
