// <copyright file="ScatteredPointsVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-09</date>
// <summary>OpenWPFChart library. ScatteredPointsVisual draws the set of scattered points.</summary>
// <revision>$Id: ScatteredPointsVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ScatteredPointsVisual draws the set of scattered points.
	/// </summary>
	public class ScatteredPointsVisual : ItemVisual
	{
		/// <summary>
		/// Renders the Curve
		/// </summary>
		protected internal override void Render()
		{
			Children.Clear();

			using (DrawingContext dc = RenderOpen())
			{
				ScatteredPointsDataView dataView = ItemDataView as ScatteredPointsDataView;
				if (dataView == null)
					return;

				// Render the Points in (x - double, y - double) coordinates.
				ScatteredPointsData<double, double> data = ItemDataView.ItemData as ScatteredPointsData<double, double>;
				if (data != null)
				{
					Render(dc, dataView, data);
					return;
				}
				// Render the Points in (x - DateTime, y - double) coordinates.
				ScatteredPointsData<DateTime, double> DTNData = ItemDataView.ItemData as ScatteredPointsData<DateTime, double>;
				if (DTNData != null)
				{
					Render(dc, dataView, DTNData);
					return;
				}
				// Render the Points in (x - object, y - double) coordinates.
				ScatteredPointsData<object, double> ONData = ItemDataView.ItemData as ScatteredPointsData<object, double>;
				if (ONData != null)
				{
					Render(dc, dataView, ONData);
					return;
				}
			}
		}

		/// <summary>
		/// Renders the Points in (x - double, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="dataView">The data view.</param>
		/// <param name="data">The Points data.</param>
		private void Render(DrawingContext dc, ScatteredPointsDataView dataView
			, ScatteredPointsData<double, double> data)
		{
			ChartScale hScale = dataView.HorizontalScale, vScale = dataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(double))
				|| data.Points == null)
				return;

			// Point marker drawing.
			IPointMarker iPointMarker = dataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = iPointMarker.PointMarker;
			if (!iPointMarker.PointMarkerVisible || pointMarkerDrawing == null)
				return;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Loop by points
			foreach (DataPoint<double, double> pt in from pt in data.Points orderby pt.X select pt)
			{
				double x, y;
				if (dataView.Orientation == Orientation.Horizontal)
				{
					x = hScale.ToPixels(pt.X);
					y = vScale.ToPixels(pt.Y);
					if (!isInsideArea(new Point(x, y), areaSize))
						continue;
				}
				else // Orientation == Orientation.Vertical
				{
					y = hScale.ToPixels(pt.X);
					x = vScale.ToPixels(pt.Y);
					if (!isInsideArea(new Point(y, x), areaSize))
						continue;
				}

				Drawing marker = pointMarkerDrawing.Clone();
				marker.Freeze();

				ChartPointVisual pointMarker = new ChartPointVisual(marker);
				pointMarker.Transform = new TranslateTransform(x, y);
				Children.Add(pointMarker);
			}
		}

		/// <summary>
		/// Renders the Points in (x - DateTime, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="dataView">The data view.</param>
		/// <param name="data">The data.</param>
		private void Render(DrawingContext dc, ScatteredPointsDataView dataView, ScatteredPointsData<DateTime, double> data)
		{
			ChartScale hScale = dataView.HorizontalScale, vScale = dataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(DateTime))
				|| data.Points == null)
				return;

			// Point marker drawing.
			IPointMarker iPointMarker = dataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = iPointMarker.PointMarker;
			if (!iPointMarker.PointMarkerVisible || pointMarkerDrawing == null)
				return;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Loop by points
			foreach (DataPoint<DateTime, double> pt in from pt in data.Points orderby pt.X select pt)
			{
				double x, y; // coordinates in pixels
				if (dataView.Orientation == Orientation.Horizontal)
				{
					x = hScale.ToPixels(pt.X);
					y = vScale.ToPixels(pt.Y);
					if (!isInsideArea(new Point(x, y), areaSize))
						continue;
				}
				else // Orientation == Orientation.Vertical
				{
					y = hScale.ToPixels(pt.X);
					x = vScale.ToPixels(pt.Y);
					if (!isInsideArea(new Point(y, x), areaSize))
						continue;
				}

				Drawing marker = pointMarkerDrawing.Clone();
				marker.Freeze();

				ChartPointVisual pointMarker = new ChartPointVisual(marker);
				pointMarker.Transform = new TranslateTransform(x, y);
				Children.Add(pointMarker);
			}
		}

		/// <summary>
		/// Renders the Points in (x - object, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="dataView">The data view.</param>
		/// <param name="data">The Points data.</param>
		private void Render(DrawingContext dc, ScatteredPointsDataView dataView
			, ScatteredPointsData<object, double> data)
		{
			ChartSeriesScale hScale = dataView.HorizontalScale as ChartSeriesScale;
			ChartScale vScale = dataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(object))
				|| data.Points == null)
				return;

			// Point marker drawing.
			IPointMarker iPointMarker = dataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = iPointMarker.PointMarker;
			if (!iPointMarker.PointMarkerVisible || pointMarkerDrawing == null)
				return;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Loop by points
			foreach (DataPoint<object, double> pt in data.Points)
			{
				double x, y; // coordinates in pixels
				try
				{
					if (dataView.Orientation == Orientation.Horizontal)
					{
						x = hScale.ToPixels(pt.X);
						y = vScale.ToPixels(pt.Y);
						if (!isInsideArea(new Point(x, y), areaSize))
							continue;
					}
					else // Orientation == Orientation.Vertical
					{
						y = hScale.ToPixels(pt.X);
						x = vScale.ToPixels(pt.Y);
						if (!isInsideArea(new Point(y, x), areaSize))
							continue;
					}
				}
				catch (ArgumentException)
				{// pt.X isn't in the hScale Series.
					continue;
				}

				Drawing marker = pointMarkerDrawing.Clone();
				marker.Freeze();

				ChartPointVisual pointMarker = new ChartPointVisual(marker);
				pointMarker.Transform = new TranslateTransform(x, y);
				Children.Add(pointMarker);
			}
		}
	}
}
