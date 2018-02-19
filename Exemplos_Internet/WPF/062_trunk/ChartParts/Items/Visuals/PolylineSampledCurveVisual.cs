// <copyright file="PolylineSampledCurveVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Polyline Sampled Curve visual draws the curve passed in by Points data as polyline.</summary>
// <revision>$Id: PolylineSampledCurveVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Polyline Sampled Curve visual class draws the curve passed in by Points data
	/// as polyline.
	/// <para>If PointMarkerGeometry property is set the data point markers are drawn.</para>
	/// </summary>
	public class PolylineSampledCurveVisual : ItemVisual
	{
		/// <summary>
		/// Renders the Curve
		/// </summary>
		protected internal override void Render()
		{
			Children.Clear();

			using (DrawingContext dc = RenderOpen())
			{
				SampledCurveDataView curveDataView = ItemDataView as SampledCurveDataView;
				if (curveDataView == null)
					return;

				// Render the Curve in (x - double, y - double) coordinates.
				SampledCurveData<double, double> curveData = ItemDataView.ItemData as SampledCurveData<double, double>;
				if (curveData != null)
				{
					Render(dc, curveDataView, curveData);
					return;
				}
				// Render the Curve in (x - DateTime, y - double) coordinates.
				SampledCurveData<DateTime, double> curveDTNData = ItemDataView.ItemData as SampledCurveData<DateTime, double>;
				if (curveDTNData != null)
				{
					Render(dc, curveDataView, curveDTNData);
					return;
				}
				// Render the Curve in (x - object, y - double) coordinates.
				SampledCurveData<object, double> curveONData = ItemDataView.ItemData as SampledCurveData<object, double>;
				if (curveONData != null)
				{
					Render(dc, curveDataView, curveONData);
					return;
				}
			}
		}

		/// <summary>
		/// Renders the Curve in (x - double, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="curveDataView">The curve data view.</param>
		/// <param name="curveData">The curve data.</param>
		private void Render(DrawingContext dc, SampledCurveDataView curveDataView
			, SampledCurveData<double, double> curveData)
		{
			ChartScale hScale = curveDataView.HorizontalScale, vScale = curveDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(double))
				|| curveData.Points == null || curveDataView.Pen == null)
				return;

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Curve points
			Point? startPoint = null;
			List<Point> linePoints = new List<Point>();
			foreach (DataPoint<double, double> pt in from pt in curveData.Points orderby pt.X select pt)
			{
				double x, y;
				bool isPtInsideArea;
				if (curveDataView.Orientation == Orientation.Horizontal)
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

				if (!startPoint.HasValue)
					startPoint = new Point(x, y);
				else
					linePoints.Add(new Point(x, y));

				if (pointMarkerDrawing != null && isPtInsideArea)
				{ // Curve point marker.
					Drawing marker = pointMarkerDrawing.Clone();
					marker.Freeze();

					ChartPointVisual pointMarker = new ChartPointVisual(marker);
					pointMarker.Transform = new TranslateTransform(x, y);
					Children.Add(pointMarker);
				}
			}
			if (!startPoint.HasValue)
				return; // Nothing to draw

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				ctx.BeginFigure(startPoint.Value, false /* is filled */, false /* is closed */);
				ctx.PolyLineTo(linePoints, true /* is stroked */, true /* is smooth join */);
			}
			geometry.Freeze();

			// Clipping region
			RectangleGeometry clip;
			if (curveDataView.Orientation == Orientation.Horizontal)
				clip = new RectangleGeometry(new Rect(0, 0
					, hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			else
				clip = new RectangleGeometry(new Rect(0, 0
					, vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			dc.PushClip(clip);

			dc.DrawGeometry(Brushes.Transparent, curveDataView.Pen, geometry);
		}

		/// <summary>
		/// Renders the Curve in (x - DateTime, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="curveDataView">The curve data view.</param>
		/// <param name="curveData">The curve data.</param>
		private void Render(DrawingContext dc, SampledCurveDataView curveDataView
			, SampledCurveData<DateTime, double> curveData)
		{
			ChartScale hScale = curveDataView.HorizontalScale, vScale = curveDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(DateTime))
				|| curveData.Points == null || curveDataView.Pen == null)
				return;

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Curve points
			Point? startPoint = null;
			List<Point> linePoints = new List<Point>();
			foreach (DataPoint<DateTime, double> pt in from pt in curveData.Points orderby pt.X select pt)
			{
				double x, y; // coordinates in pixels
				bool isPtInsideArea;
				if (curveDataView.Orientation == Orientation.Horizontal)
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

				if (!startPoint.HasValue)
					startPoint = new Point(x, y);
				else
					linePoints.Add(new Point(x, y));

				if (pointMarkerDrawing != null && isPtInsideArea)
				{ // Curve point marker.
					Drawing marker = pointMarkerDrawing.Clone();
					marker.Freeze();

					ChartPointVisual pointMarker = new ChartPointVisual(marker);
					pointMarker.Transform = new TranslateTransform(x, y);
					Children.Add(pointMarker);
				}
			}
			if (!startPoint.HasValue)
				return; // Nothing to draw

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				ctx.BeginFigure(startPoint.Value, false /* is filled */, false /* is closed */);
				ctx.PolyLineTo(linePoints, true /* is stroked */, true /* is smooth join */);
			}
			geometry.Freeze();

			// Clipping region
			RectangleGeometry clip;
			if (curveDataView.Orientation == Orientation.Horizontal)
				clip = new RectangleGeometry(new Rect(0, 0
					, hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			else
				clip = new RectangleGeometry(new Rect(0, 0
					, vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			dc.PushClip(clip);

			dc.DrawGeometry(Brushes.Transparent, curveDataView.Pen, geometry);
		}

		/// <summary>
		/// Renders the Curve in (x - object, y - double) coordinates.
		/// </summary>
		/// <param name="dc">The dc.</param>
		/// <param name="curveDataView">The curve data view.</param>
		/// <param name="curveData">The curve data.</param>
		private void Render(DrawingContext dc, SampledCurveDataView curveDataView
			, SampledCurveData<object, double> curveData)
		{
			ChartSeriesScale hScale = curveDataView.HorizontalScale as ChartSeriesScale;
			ChartScale vScale = curveDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(object))
				|| curveData.Points == null || curveDataView.Pen == null)
				return;

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			// Curve points
			Point? startPoint = null;
			List<Point> linePoints = new List<Point>();
			foreach (DataPoint<object, double> pt in curveData.Points)
			{
				double x, y; // coordinates in pixels
				bool isPtInsideArea;
				try
				{
					if (curveDataView.Orientation == Orientation.Horizontal)
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

				if (!startPoint.HasValue)
					startPoint = new Point(x, y);
				else
					linePoints.Add(new Point(x, y));

				if (pointMarkerDrawing != null && isPtInsideArea)
				{ // Curve point marker.
					Drawing marker = pointMarkerDrawing.Clone();
					marker.Freeze();

					ChartPointVisual pointMarker = new ChartPointVisual(marker);
					pointMarker.Transform = new TranslateTransform(x, y);
					Children.Add(pointMarker);
				}
			}
			if (!startPoint.HasValue)
				return; // Nothing to draw

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				ctx.BeginFigure(startPoint.Value, false /* is filled */, false /* is closed */);
				ctx.PolyLineTo(linePoints, true /* is stroked */, true /* is smooth join */);
			}
			geometry.Freeze();

			// Clipping region
			RectangleGeometry clip;
			if (curveDataView.Orientation == Orientation.Horizontal)
				clip = new RectangleGeometry(new Rect(0, 0
					, hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			else
				clip = new RectangleGeometry(new Rect(0, 0
					, vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			dc.PushClip(clip);

			dc.DrawGeometry(Brushes.Transparent, curveDataView.Pen, geometry);
		}
	}
}
