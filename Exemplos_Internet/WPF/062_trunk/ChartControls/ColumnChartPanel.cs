// <copyright file="ColumnChartPanel.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin.
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-14</date>
// <summary>Custom Panel for the ColumnChart Control.</summary>
// <revision>$Id: ColumnChartPanel.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Controls
{
	/// <summary>
	/// ColumnChartPanel for the ColumnChart Control.
	/// </summary>
	/// <remarks>
	/// ColumnChartPanel shifts its <see cref="OpenWPFChart.Parts.ColumnChartItemDataView"/>-based 
	/// children by column width value.
	/// <para>ColumnChartPanel sizes according to its contents.</para>
	/// </remarks>
	public class ColumnChartPanel : Panel
	{
		/// <summary>
		/// When overridden in a derived class, measures the size in layout required for child 
		/// elements and determines a size for the <see cref="T:System.Windows.FrameworkElement"/>-derived 
		/// class.
		/// </summary>
		/// <param name="availableSize">The available size that this element can give to child elements. 
		/// Infinity can be specified as a value to indicate that the element will size to whatever content 
		/// is available.</param>
		/// <returns>Empty Size.</returns>
		/// <remarks>Calls Measure method on all the Children and returns the Empty size.</remarks>
		protected override Size MeasureOverride(Size availableSize)
		{
			foreach (UIElement child in Children)
			{
				child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
			}
			return Size.Empty;
		}

		/// <summary>
		/// When overridden in a derived class, positions child elements and determines a size 
		/// for a <see cref="T:System.Windows.FrameworkElement"/> derived class.
		/// </summary>
		/// <param name="finalSize">The final area within the parent that this element should 
		/// use to arrange itself and its children.</param>
		/// <returns>The actual size used.</returns>
		/// <remarks>
		/// Shifts <see cref="T:OpenWPFChart.Parts.ColumnChartItemDataView"/> to group the columns
		/// with equal abscissas.
		/// </remarks>
		protected override Size ArrangeOverride(Size finalSize)
		{
			double columnWidth = 0d;
			int visibleChildrenCount = 0;
			foreach (UIElement child in Children)
			{
				ColumnChartItemDataView itemDataView = getItemDataView(child);
				if (itemDataView == null)
					continue;
				if (visibleChildrenCount++ == 0)
					columnWidth = itemDataView.ColumnWidth;
				else
				{
					// All items should have the same ColumnWidth property value.
					Debug.Assert(columnWidth == itemDataView.ColumnWidth, "columnWidth == itemDataView.ColumnWidth");
				}
			}
			if (visibleChildrenCount == 0)
				return finalSize;

			double offset = -0.5 * columnWidth * (visibleChildrenCount - 1);
			foreach (UIElement child in Children)
			{
				if (getItemDataView(child) == null)
					continue;

				child.Arrange(new Rect(new Point(offset, 0), child.DesiredSize));
				offset += columnWidth;
			}
			return finalSize;
		}

		/// <summary>
		/// Get the <see cref="T:OpenWPFChart.Parts.ColumnChartItemDataView"/> object.
		/// </summary>
		/// <remarks>
		/// Get the <see cref="T:OpenWPFChart.Parts.ColumnChartItemDataView"/> object from 
		/// <paramref name="item"/> DataContext.
		/// </remarks>
		/// <param name="item">The <see cref="T:System.Windows.ItemsControl"/> item container.</param>
		/// <returns><see cref="T:OpenWPFChart.Parts.ColumnChartItemDataView"/> object or null.</returns>
		static ColumnChartItemDataView getItemDataView(UIElement item)
		{
			FrameworkElement fe = item as FrameworkElement;
			if (fe == null)
				return null;
			return fe.DataContext as ColumnChartItemDataView;
		}
	}
}
