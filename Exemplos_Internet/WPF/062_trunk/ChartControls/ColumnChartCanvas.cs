// <copyright file="ColumnChartCanvas.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin.
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-13</date>
// <summary>Canvas for the ColumnChart Control.</summary>

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Controls
{
	/// <summary>
	/// Canvas for the ColumnChart Control.
	/// </summary>
	/// <remarks>
	/// ColumnChartCanvas shifts its <see cref="OpenWPFChart.Parts.ColumnChartItemDataView"/>-based children by
	/// column width value.
	/// <para>ColumnChartCanvas sizes according to its contents.</para>
	/// </remarks>
	public class ColumnChartCanvas : Canvas
	{
		/// <summary>
		/// Invoked when the <see cref="T:System.Windows.Media.VisualCollection"/> of a visual object
		/// is modified.
		/// </summary>
		/// <param name="visualAdded">The <see cref="T:System.Windows.Media.Visual"/> that was added
		/// to the collection.</param>
		/// <param name="visualRemoved">The <see cref="T:System.Windows.Media.Visual"/> that was 
		/// removed from the collection.</param>
		/// <remarks>
		/// This method offsets all <see cref="OpenWPFChart.Parts.ColumnChartItemDataView"/> panel items
		/// by setting their Canvas.Left property.
		/// </remarks>
		protected override void OnVisualChildrenChanged(DependencyObject visualAdded
			, DependencyObject visualRemoved)
		{
			double columnWidth = 0d;
			int visibleChildrenCount = 0;
			foreach (UIElement child in Children)
			{
				ColumnChartItemDataView itemDataView = findItemDataView(child);
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
			if (visibleChildrenCount < 2)
				// Doesn't have to shift single item.
				return;

			double offset = -0.5 * columnWidth * (visibleChildrenCount - 1);
			foreach (UIElement child in Children)
			{
				if (findItemDataView(child) == null)
					continue;

				SetLeft(child, offset);
				offset += columnWidth;
			}
		}

		/// <summary>
		/// Finds the item data view.
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <param name="item">The <see cref="T:System.Windows.ItemsControl"/> item container.</param>
		/// <returns></returns>
		static ColumnChartItemDataView findItemDataView(UIElement item)
		{
			FrameworkElement fe = item as FrameworkElement;
			if (fe == null)
				return null;
			return fe.DataContext as ColumnChartItemDataView;
		}
	}
}
