// <copyright file="ItemData.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. ItemData is the abstract base class of all Data classes.</summary>
// <revision>$Id: ItemData.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.ComponentModel; // For INotifyPropertyChanged

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// <see cref="ItemData"/> is the abstract base class of all Data classes.
	/// </summary>
	public abstract class ItemData : INotifyPropertyChanged
	{
		#region ItemName
		string itemName;
		/// <summary>
		/// Gets or sets the <see cref="ItemName"/> property.
		/// </summary>
		/// <remarks>The name of the item.</remarks>
		/// <value>Item Name string or null.</value>
		public string ItemName 
		{
			get { return itemName; }
			set
			{
				if (itemName != value)
				{
					itemName = value;
					NotifyPropertyChanged("ItemName");
				}
			}
		}
		#endregion ItemName

		/// <summary>
		/// Gets the type of the abscissa.
		/// </summary>
		/// <value>The abscissa base type.</value>
		/// <remarks>
		/// <see cref="ItemData"/> represents two-dimensional data and each dimension is expressed 
		/// in its distinct base type (e.g. <see langword="double"/>, <see langword="DateTime"/> 
		/// or <see langword="object"/>).
		/// </remarks>
		/// <returns>The base type of the abscissa (e.g. <see langword="double"/>,
		/// <see langword="DateTime"/> or <see langword="object"/>).</returns>
		public abstract Type TypeOfAbscissa { get; }

		/// <summary>
		/// Gets the type of the ordinate.
		/// </summary>
		/// <value>The ordinate base type.</value>
		/// <remarks>
		/// <see cref="ItemData"/> represents two-dimensional data and each dimension is expressed 
		/// in its distinct base type (e.g. <see langword="double"/>, <see langword="DateTime"/> 
		/// or <see langword="object"/>).
		/// </remarks>
		/// <returns>The base type of the ordinate (e.g. <see langword="double"/>,
		/// <see langword="DateTime"/> or <see langword="object"/>).</returns>
		public abstract Type TypeOfOrdinate { get; }

		/// <summary>
		/// Determines whether the set of abscissas of this item is equal to the one of 
		/// the specified <paramref name="item"/>.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>
		/// 	<c>true</c> if two sets of abscissas are equa; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool IsAbscissasEqual(ItemData item);

		/// <summary>
		/// Determines whether the set of ordinates of this item is equal to the one of 
		/// the specified <paramref name="item"/>.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>
		/// 	<c>true</c> if two sets of ordinates are equa; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool IsOrdinatesEqual(ItemData item);

		#region INotifyPropertyChanged Members
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) 
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion INotifyPropertyChanged Members
	}
}
