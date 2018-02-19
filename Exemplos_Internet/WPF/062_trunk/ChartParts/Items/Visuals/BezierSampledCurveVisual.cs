// <copyright file="BezierSampledCurveVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart  library. Bezier SampledCurve Visual.</summary>
// <revision>$Id: BezierSampledCurveVisual.cs 19127 2009-03-21 16:06:57Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Bezier Sampled Curve visual class draws the curve passed in by Points data
	/// as poly Bezier curve.
	/// <para>If PointMarkerGeometry property is set the data point markers are drawn.</para>
	/// </summary>
	public class BezierSampledCurveVisual : ItemVisual
	{
		/// <summary>
		/// Renders the Curve.
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

			// Curve points
			List<Point> points = new List<Point>();
			foreach (DataPoint<double, double> originalPoint in from pt in curveData.Points orderby pt.X select pt)
			{
				Point pt; // point in pixels with respect of orientation.
				if (curveDataView.Orientation == Orientation.Horizontal)
					pt = new Point(hScale.ToPixels(originalPoint.X), vScale.ToPixels(originalPoint.Y));
				else // Orientation == Orientation.Vertical
					pt = new Point(vScale.ToPixels(originalPoint.Y), hScale.ToPixels(originalPoint.X));
				points.Add(pt);
			}
			if (points.Count == 0)
				return; // Nothing to draw

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			if (pointMarkerDrawing != null)
			{ // Add Curve point markers.
				for (int i = 0; i < points.Count; ++i)
				{
					Point pt = points[i];
					if (isInsideArea(pt, areaSize))
					{
						Drawing marker = pointMarkerDrawing.Clone();
						marker.Freeze();

						ChartPointVisual pointMarker = new ChartPointVisual(marker);
						pointMarker.Transform = new TranslateTransform(pt.X, pt.Y);
						Children.Add(pointMarker);
					}
				}
			}

			// Bezier points
			Point[] bezierPoints = BezierPoints(points.ToArray());

			// Clipping region
			RectangleGeometry clip;
			if (curveDataView.Orientation == Orientation.Horizontal)
				clip = new RectangleGeometry(new Rect(0, 0
					, hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			else
				clip = new RectangleGeometry(new Rect(0, 0
					, vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			dc.PushClip(clip);

			if (bezierPoints.Length == 0)
				return; // Nothing to draw

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				ctx.BeginFigure(points[0], false /* is filled */, false /* is closed */);
				ctx.PolyBezierTo(bezierPoints, true /* is stroked */, true /* is smooth join */);
			}
			geometry.Freeze();

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
			ChartDateTimeScale hScale = curveDataView.HorizontalScale as ChartDateTimeScale;
			ChartScale vScale = curveDataView.VerticalScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(DateTime))
				|| curveData.Points == null || curveDataView.Pen == null)
				return;

			// Curve points
			List<Point> points = new List<Point>();
			foreach (DataPoint<DateTime, double> originalPoint in from pt in curveData.Points orderby pt.X select pt)
			{
				Point pt; // point in pixels with respect of orientation.
				if (curveDataView.Orientation == Orientation.Horizontal)
					pt = new Point(hScale.ToPixels(originalPoint.X), vScale.ToPixels(originalPoint.Y));
				else // Orientation == Orientation.Vertical
					pt = new Point(vScale.ToPixels(originalPoint.Y), hScale.ToPixels(originalPoint.X));
				points.Add(pt);
			}
			if (points.Count == 0)
				return; // Nothing to draw

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			if (pointMarkerDrawing != null)
			{ // Add Curve point markers.
				for (int i = 0; i < points.Count; ++i)
				{
					Point pt = points[i];
					if (isInsideArea(pt, areaSize))
					{
						Drawing marker = pointMarkerDrawing.Clone();
						marker.Freeze();

						ChartPointVisual pointMarker = new ChartPointVisual(marker);
						pointMarker.Transform = new TranslateTransform(pt.X, pt.Y);
						Children.Add(pointMarker);
					}
				}
			}

			// Bezier points
			Point[] bezierPoints = BezierPoints(points.ToArray());

			// Clipping region
			RectangleGeometry clip;
			if (curveDataView.Orientation == Orientation.Horizontal)
				clip = new RectangleGeometry(new Rect(0, 0
					, hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			else
				clip = new RectangleGeometry(new Rect(0, 0
					, vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			dc.PushClip(clip);

			if (bezierPoints.Length == 0)
				return; // Nothing to draw

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				ctx.BeginFigure(points[0], false /* is filled */, false /* is closed */);
				ctx.PolyBezierTo(bezierPoints, true /* is stroked */, true /* is smooth join */);
			}
			geometry.Freeze();

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

			// Curve points
			List<Point> points = new List<Point>();
			foreach (DataPoint<object, double> originalPoint in curveData.Points)
			{
				Point pt; // point in pixels with respect of orientation.
				if (curveDataView.Orientation == Orientation.Horizontal)
					pt = new Point(hScale.ToPixels(originalPoint.X), vScale.ToPixels(originalPoint.Y));
				else // Orientation == Orientation.Vertical
					pt = new Point(vScale.ToPixels(originalPoint.Y), hScale.ToPixels(originalPoint.X));
				points.Add(pt);
			}
			if (points.Count == 0)
				return; // Nothing to draw

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			// Chart area size.
			Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

			if (pointMarkerDrawing != null)
			{ // Add Curve point markers.
				for (int i = 0; i < points.Count; ++i)
				{
					Point pt = points[i];
					if (isInsideArea(pt, areaSize))
					{
						Drawing marker = pointMarkerDrawing.Clone();
						marker.Freeze();

						ChartPointVisual pointMarker = new ChartPointVisual(marker);
						pointMarker.Transform = new TranslateTransform(pt.X, pt.Y);
						Children.Add(pointMarker);
					}
				}
			}

			// Bezier points
			Point[] bezierPoints = BezierPoints(points.ToArray());

			// Clipping region
			RectangleGeometry clip;
			if (curveDataView.Orientation == Orientation.Horizontal)
				clip = new RectangleGeometry(new Rect(0, 0
					, hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop)));
			else
				clip = new RectangleGeometry(new Rect(0, 0
					, vScale.ToPixels(vScale.Stop), hScale.ToPixels(hScale.Stop)));
			dc.PushClip(clip);

			if (bezierPoints.Length == 0)
				return; // Nothing to draw

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				ctx.BeginFigure(points[0], false /* is filled */, false /* is closed */);
				ctx.PolyBezierTo(bezierPoints, true /* is stroked */, true /* is smooth join */);
			}
			geometry.Freeze();

			dc.DrawGeometry(Brushes.Transparent, curveDataView.Pen, geometry);
		}

		/// <summary>
		/// Get Bezier Points.
		/// </summary>
		/// <param name="points">Knot Bezier spline points.</param>
		/// <returns>The array of points that specify control points and destination points for one 
		/// or more Bezier curves. The number of points == (points.Length - 1) * 3.</returns>
		private static Point[] BezierPoints(Point[] points)
		{
			int n = points.Length - 1;
			if (n < 1)
				return new Point[0];

			// Calculate first Bezier control points
			// Right hand side vector
			double[] r = new double[n];

			// Set right hand side X values
			for (int i = 1; i < n - 1; ++i)
				r[i] = 4 * points[i].X + 2 * points[i + 1].X;
			r[0] = points[0].X + 2 * points[1].X;
			r[n - 1] = (8 * points[n - 1].X + points[n].X) / 2.0;
			// Get first control points X-values
			double[] x = Solve(r);

			// Set right hand side Y values
			for (int i = 1; i < n - 1; ++i)
				r[i] = 4 * points[i].Y + 2 * points[i + 1].Y;
			r[0] = points[0].Y + 2 * points[1].Y;
			r[n - 1] = (8 * points[n - 1].Y + points[n].Y) / 2.0;
			// Get first control points Y-values
			double[] y = Solve(r);

			Point[] bezierPoints = new Point[n * 3];
			for (int i = 0; i < n; ++i)
			{
				// First control point
				bezierPoints[3 * i] = new Point(x[i], y[i]);
				// Second control point
				if (i < n - 1)
					bezierPoints[3 * i + 1] = new Point(2 * points[i + 1].X - x[i + 1], 2 * points[i + 1].Y - y[i + 1]);
				else
					bezierPoints[3 * i + 1] = new Point((points[n].X + x[n - 1]) / 2, (points[n].Y + y[n - 1]) / 2);
				// Bezier knot point
				bezierPoints[3 * i + 2] = points[i + 1];
			}

			return bezierPoints;
		}

		/// <summary>
		/// Solves a tridiagonal system for one of coordinates of first Bezier control point.
		/// </summary>
		/// <param name="rhs">Right hand side vector.</param>
		/// <returns>Solution vector.</returns>
		static double[] Solve(double[] rhs)
		{
			int n = rhs.Length;
			double[] x = new double[n]; // Solution vector.
			double[] tmp = new double[n]; // Temp workspace.

			double b = 2.0;
			x[0] = rhs[0] / b;
			for (int i = 1; i < n; i++) // Decomposition and forward substitution.
			{
				tmp[i] = 1 / b;
				b = (i < n - 1 ? 4.0 : 3.5) - tmp[i];
				x[i] = (rhs[i] - x[i - 1]) / b;
			}
			for (int i = 1; i < n; i++)
				x[n - i - 1] -= tmp[n - i] * x[n - i]; // Backsubstitution.

			return x;
		}
	}
}
