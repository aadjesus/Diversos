// <copyright file="ColumnChartItemVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-10</date>
// <summary>OpenWPFChart library Column Chart Item visual.</summary>
// <revision>$Id: ColumnChartItemVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Column Chart Item visual class.
	/// </summary>
	/// <remarks>
	/// Draws the data passed in by Points property as columns.
	/// </remarks>
	public class ColumnChartItemVisual : ItemVisual
	{
		/// <summary>
		/// Renders the ColumnChartItem
		/// </summary>
		protected internal override void Render()
		{
			Children.Clear();

			using (DrawingContext dc = RenderOpen())
			{
				ColumnChartItemDataView itemDataView = ItemDataView as ColumnChartItemDataView;
				if (itemDataView == null)
					return;

				// Render the ColumnChartItem in (x - double, y - double) coordinates.
				SampledCurveData<double, double> itemData = ItemDataView.ItemData as SampledCurveData<double, double>;
				if (itemData != null)
				{
					Render(dc, itemDataView, itemData);
					return;
				}
				// Render the ColumnChartItem in (x - DateTime, y - double) coordinates.
				SampledCurveData<DateTime, double> itemDTNData = ItemDataView.ItemData as SampledCurveData<DateTime, double>;
				if (itemDTNData != null)
				{
					Render(dc, itemDataView, itemDTNData);
					return;
				}
				// Render the ColumnChartItem in (x - object, y - double) coordinates.
				SampledCurveData<object, double> itemONData = ItemDataView.ItemData as SampledCurveData<object, double>;
				if (itemONData != null)
				{
					Render(dc, itemDataView, itemONData);
					return;
				}
			}
		}

		/// <summary>
		/// Renders the ColumnChartItem in (x - double, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="itemDataView">The item data view.</param>
		/// <param name="itemData">The item data.</param>
		private void Render(DrawingContext dc, ColumnChartItemDataView itemDataView
			, SampledCurveData<double, double> itemData)
		{
			ChartScale hScale = itemDataView.HorizontalScale, vScale = itemDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(double))
				|| itemData.Points == null)
				return;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Clipping region
			//RectangleGeometry clip;
			//if (itemDataView.Orientation == Orientation.Horizontal)
			//    clip = new RectangleGeometry(new Rect(0, 0
			//        , hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			//else
			//    clip = new RectangleGeometry(new Rect(0, 0
			//        , vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			//dc.PushClip(clip);

			// Loop by points.
			foreach (DataPoint<double, double> pt in from pt in itemData.Points orderby pt.X select pt)
			{
				double x, y;
				bool isPtInsideArea;
				if (itemDataView.Orientation == Orientation.Horizontal)
				{
					x = hScale.ToPixels(pt.X);
					y = vScale.ToPixels(pt.Y);
					isPtInsideArea = isInsideArea(new Point(x, y), areaSize);
				}
				else // Orientation == Orientation.Vertical
				{
					y = hScale.ToPixels(pt.X);
					x = vScale.ToPixels(pt.Y);
					isPtInsideArea = isInsideArea(new Point(y, x), areaSize);
				}

				// Draw the column.
				StreamGeometry geometry = new StreamGeometry();
				using (StreamGeometryContext ctx = geometry.Open())
				{
					double xStart = x - itemDataView.ColumnWidth / 2;
					double xStop = xStart + itemDataView.ColumnWidth;
					ctx.BeginFigure(new Point(xStart, areaSize.Height)
						, true /* is filled */, true /* is closed */);
					ctx.PolyLineTo(new Point[] 
						{ 
							new Point(xStart, y), 
							new Point(xStop, y),
							new Point(xStop, areaSize.Height)
						}
						, false /* is stroked */, false /* is smooth join */);
				}
				geometry.Freeze();
				dc.DrawGeometry(itemDataView.ColumnBrush, null, geometry);
			}
		}

		/// <summary>
		/// Renders the ColumnChartItem in (x - DateTime, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="itemDataView">The item data view.</param>
		/// <param name="itemData">The item data.</param>
		private void Render(DrawingContext dc, ColumnChartItemDataView itemDataView
			, SampledCurveData<DateTime, double> itemData)
		{
			ChartScale hScale = itemDataView.HorizontalScale, vScale = itemDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(DateTime))
				|| itemData.Points == null)
				return;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Clipping region
			//RectangleGeometry clip;
			//if (itemDataView.Orientation == Orientation.Horizontal)
			//    clip = new RectangleGeometry(new Rect(0, 0
			//        , hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			//else
			//    clip = new RectangleGeometry(new Rect(0, 0
			//        , vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			//dc.PushClip(clip);

			// Loop by points.
			foreach (DataPoint<DateTime, double> pt in from pt in itemData.Points orderby pt.X select pt)
			{
				double x, y; // coordinates in pixels
				bool isPtInsideArea;
				if (itemDataView.Orientation == Orientation.Horizontal)
				{
					x = hScale.ToPixels(pt.X);
					y = vScale.ToPixels(pt.Y);
					isPtInsideArea = isInsideArea(new Point(x, y), areaSize);
				}
				else // Orientation == Orientation.Vertical
				{
					y = hScale.ToPixels(pt.X);
					x = vScale.ToPixels(pt.Y);
					isPtInsideArea = isInsideArea(new Point(y, x), areaSize);
				}

				// Draw the column.
				StreamGeometry geometry = new StreamGeometry();
				using (StreamGeometryContext ctx = geometry.Open())
				{
					double xStart = x - itemDataView.ColumnWidth / 2;
					double xStop = xStart + itemDataView.ColumnWidth;
					ctx.BeginFigure(new Point(xStart, areaSize.Height)
						, true /* is filled */, true /* is closed */);
					ctx.PolyLineTo(new Point[] 
						{ 
							new Point(xStart, y), 
							new Point(xStop, y),
							new Point(xStop, areaSize.Height)
						}
						, false /* is stroked */, false /* is smooth join */);
				}
				geometry.Freeze();
				dc.DrawGeometry(itemDataView.ColumnBrush, null, geometry);
			}
		}

		/// <summary>
		/// Renders the ColumnChartItem in (x - object, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="itemDataView">The item data view.</param>
		/// <param name="itemData">The item data.</param>
		private void Render(DrawingContext dc, ColumnChartItemDataView itemDataView
			, SampledCurveData<object, double> itemData)
		{
			ChartSeriesScale hScale = itemDataView.HorizontalScale as ChartSeriesScale;
			ChartScale vScale = itemDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(object))
				|| itemData.Points == null)
				return;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Clipping region
			//RectangleGeometry clip;
			//if (itemDataView.Orientation == Orientation.Horizontal)
			//    clip = new RectangleGeometry(new Rect(0, 0
			//        , hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			//else
			//    clip = new RectangleGeometry(new Rect(0, 0
			//        , vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			//dc.PushClip(clip);

			// Loop by points.
			foreach (DataPoint<object, double> pt in itemData.Points)
			{
				double x, y; // coordinates in pixels
				bool isPtInsideArea;
				try
				{
					if (itemDataView.Orientation == Orientation.Horizontal)
					{
						x = hScale.ToPixels(pt.X);
						y = vScale.ToPixels(pt.Y);
						isPtInsideArea = isInsideArea(new Point(x, y), areaSize);
					}
					else // Orientation == Orientation.Vertical
					{
						y = hScale.ToPixels(pt.X);
						x = vScale.ToPixels(pt.Y);
						isPtInsideArea = isInsideArea(new Point(y, x), areaSize);
					}
				}
				catch (ArgumentException)
				{// pt.X isn't in the hScale Series.
					continue;
				}

				// Draw the column.
				StreamGeometry geometry = new StreamGeometry();
				using (StreamGeometryContext ctx = geometry.Open())
				{
					double xStart = x - itemDataView.ColumnWidth / 2;
					double xStop = xStart + itemDataView.ColumnWidth;
					ctx.BeginFigure(new Point(xStart, areaSize.Height)
						, true /* is filled */, true /* is closed */);
					ctx.PolyLineTo(new Point[] 
						{ 
							new Point(xStart, y), 
							new Point(xStop, y),
							new Point(xStop, areaSize.Height)
						}
						, false /* is stroked */, false /* is smooth join */);
				}
				geometry.Freeze();
				dc.DrawGeometry(itemDataView.ColumnBrush, null, geometry);
			}
		}
	}
}
