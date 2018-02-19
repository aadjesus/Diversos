// <copyright file="ItemVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. OpenWPFChart Item Visual's abstract base class.</summary>
// <revision>$Id: ItemVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.ComponentModel; // For ISupportInitialize
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// OpenWPFChart Item Visual's abstract base class.
	/// </summary>
	public abstract class ItemVisual : DrawingVisual
	{
		#region Dependency properties
		#region ItemDataView
		/// <summary>
		/// Identifies the ItemDataView dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemDataViewProperty
			= Item.ItemDataViewProperty.AddOwner(typeof(ItemVisual));
		/// <summary>
		/// Gets or sets the ItemDataView property.
		/// </summary>
		/// <value>ItemData value</value>
		public ItemDataView ItemDataView
		{
			get { return (ItemDataView)GetValue(ItemDataViewProperty); }
			set { SetValue(ItemDataViewProperty, value); }
		}
		#endregion ItemData
		#endregion Dependency properties

		/// <summary>
		/// Render Curve Visual.
		/// </summary>
		protected internal abstract void Render();

		/// <summary>
		/// Checks the 'pt' is inside the area. All coordinates come in pixels.
		/// </summary>
		/// <param name="pt">The pt.</param>
		/// <param name="area">The area.</param>
		/// <returns>
		/// 	<c>true</c> if the specified pt is inside the area; otherwise, <c>false</c>.
		/// </returns>
		protected static bool isInsideArea(Point pt, Size area)
		{
			Debug.Assert(area.Width >= 0, "area.Width >= 0");
			Debug.Assert(area.Height >= 0, "area.Height >= 0");
			if (pt.X < 0 || pt.X > area.Width || pt.Y < 0 || pt.Y > area.Height)
				return false;
			return true;
		}
	}
}
