// <copyright file="SplineSampledCurveVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Spline SampledCurve Visual.</summary>
// <revision>$Id: SplineSampledCurveVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using NumericalRecipes;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Spline Sampled Curve visual class draws the curve passed in by SplineSampledCurveData.CurveDelegate.
	/// </summary>
	public class SplineSampledCurveVisual : ItemVisual
	{
		/// <summary>
		/// Initializes the <see cref="SplineSampledCurveVisual"/> class.
		/// </summary>
		static SplineSampledCurveVisual()
		{
			ItemDataViewProperty.OverrideMetadata(typeof(SplineSampledCurveVisual)
					, new FrameworkPropertyMetadata(ItemDataViewChanged));
		}

		/// <summary>
		/// Called when ItemDataView changed.
		/// </summary>
		/// <param name="sender">Dependency Object</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		protected static void ItemDataViewChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			SplineSampledCurveVisual ths = sender as SplineSampledCurveVisual;
			if (ths == null)
				return;

			ths.Spline = null;
			// Create new Spline.
			SampledCurveData<double, double> dataDD = ths.ItemDataView.ItemData as SampledCurveData<double, double>;
			if (dataDD != null)
			{
				ths.Spline = new Spline(from pt in dataDD.Points select new Point(Convert.ToDouble(pt.X), Convert.ToDouble(pt.Y)));
				return;
			}
			SampledCurveData<DateTime, double> dataDtD = ths.ItemDataView.ItemData as SampledCurveData<DateTime, double>;
			if (dataDtD != null)
			{
				// Abscissa is converted from DateTime to double!
				ths.Spline = new Spline(from pt in dataDtD.Points select new Point(Convert.ToDateTime(pt.X).Ticks, Convert.ToDouble(pt.Y)));
				return;
			}
			SampledCurveData<double, DateTime> dataDDt = ths.ItemDataView.ItemData as SampledCurveData<double, DateTime>;
			if (dataDDt != null)
			{
				// Abscissa is converted from DateTime to double!
				ths.Spline = new Spline(from pt in dataDDt.Points select new Point(Convert.ToDouble(pt.X), Convert.ToDateTime(pt.Y).Ticks));
				return;
			}
			SampledCurveData<DateTime, DateTime> dataDtDt = ths.ItemDataView.ItemData as SampledCurveData<DateTime, DateTime>;
			if (dataDtDt != null)
			{
				// Abscissa is converted from DateTime to double!
				ths.Spline = new Spline(from pt in dataDtDt.Points select new Point(Convert.ToDateTime(pt.X).Ticks, Convert.ToDateTime(pt.Y).Ticks));
				return;
			}
			SampledCurveData<object, double> dataOD = ths.ItemDataView.ItemData as SampledCurveData<object, double>;
			if (dataOD != null)
			{
				// Abscissa is converted from object to index in series!
				List<Point> points = new List<Point>();
				int index = 0;
				foreach (DataPoint<object, double> pt in dataOD.Points)
				{
					points.Add(new Point(index++, Convert.ToDouble(pt.Y)));
				}
				ths.Spline = new Spline(points);
				return;
			}
		}

		#region Spline
		Spline spline;
		/// <summary>
		/// Gets or sets the Spline property.
		/// </summary>
		/// <value>The Spline object.</value>
		private Spline Spline
		{
			get { return spline; }
			set
			{
				if (spline != value)
					spline = value;
			}
		}
		#endregion Spline


		/// <summary>
		/// Cubic Spline Polyline approximation tolerance.
		/// </summary>
		const double tolerance = 0.5;

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
				|| Spline == null || curveDataView.Pen == null)
				return;

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;

			if (pointMarkerDrawing != null)
			{ // Draw PointMarker.
				// Chart area size.
				Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

				foreach (DataPoint<double, double> pt in curveData.Points)
				{
					double x, y;
					if (curveDataView.Orientation == Orientation.Horizontal)
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

			// Approximate the Spline with a Polyline.
			List<Point> points = GetSplinePolyLineApproximation(Spline, tolerance);

			// Curve points
			Point? startPoint = null;
			List<Point> linePoints = new List<Point>();
			foreach (Point pt in points)
			{
				double x, y;
				if (curveDataView.Orientation == Orientation.Horizontal)
				{
					x = hScale.ToPixels(pt.X);
					y = vScale.ToPixels(pt.Y);
				}
				else // Orientation == Orientation.Vertical
				{
					y = hScale.ToPixels(pt.X);
					x = vScale.ToPixels(pt.Y);
				}

				if (!startPoint.HasValue)
					startPoint = new Point(x, y);
				else
					linePoints.Add(new Point(x, y));
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
			ChartDateTimeScale hScale = curveDataView.HorizontalScale as ChartDateTimeScale;
			ChartScale vScale = curveDataView.VerticalScale as ChartScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(DateTime))
				|| Spline == null || curveDataView.Pen == null)
				return;

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;
			if (pointMarkerDrawing != null)
			{ // Draw PointMarker.
				// Chart area size.
				Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

				foreach (DataPoint<DateTime, double> pt in curveData.Points)
				{
					double x, y;
					if (curveDataView.Orientation == Orientation.Horizontal)
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

			// Approximate the Spline with a Polyline.
			List<Point> points = GetSplinePolyLineApproximation(Spline, tolerance);

			// Curve points
			Point? startPoint = null;
			List<Point> linePoints = new List<Point>();
			foreach (Point pt in points)
			{
				double x, y;
				if (curveDataView.Orientation == Orientation.Horizontal)
				{
					x = hScale.ToPixels(new DateTime((long)pt.X));
					y = vScale.ToPixels(pt.Y);
				}
				else // Orientation == Orientation.Vertical
				{
					y = hScale.ToPixels(new DateTime((long)pt.X));
					x = vScale.ToPixels(pt.Y);
				}

				if (!startPoint.HasValue)
					startPoint = new Point(x, y);
				else
					linePoints.Add(new Point(x, y));
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
			ChartScale vScale = curveDataView.VerticalScale as ChartScale;
			if (vScale == null || !vScale.CompatibleWith(typeof(double))
				|| hScale == null || !hScale.CompatibleWith(typeof(object))
				|| Spline == null || curveDataView.Pen == null)
				return;

			// Curve point marker drawing.
			IPointMarker iPointMarker = curveDataView as IPointMarker;
			Debug.Assert(iPointMarker != null, "iPointMarker != null");
			Drawing pointMarkerDrawing = null;
			if (iPointMarker.PointMarkerVisible)
				pointMarkerDrawing = iPointMarker.PointMarker;
			if (pointMarkerDrawing != null)
			{ // Draw PointMarker.
				// Chart area size.
				Size areaSize = new Size(hScale.ToPixels(hScale.Stop), vScale.ToPixels(vScale.Stop));

				foreach (DataPoint<object, double> pt in curveData.Points)
				{
					double x, y;
					if (curveDataView.Orientation == Orientation.Horizontal)
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

			// Approximate the Spline with a Polyline.
			List<Point> splinePoints = GetSplinePolyLineApproximation(Spline, tolerance);
			// Abscissas in splinePoints reflects the index of x-element on X-axis.

			// Curve points
			List<DataPoint<object, double>> curvePoints = curveData.Points.ToList();
			Point? startPoint = null;
			List<Point> linePoints = new List<Point>();
			foreach (Point pt in splinePoints)
			{
				double x, y;
				if (curveDataView.Orientation == Orientation.Horizontal)
				{
					x = splineXtoCurveX(hScale, pt.X, curvePoints);
					y = vScale.ToPixels(pt.Y);
				}
				else // Orientation == Orientation.Vertical
				{
					y = splineXtoCurveX(hScale, pt.X, curvePoints);
					x = vScale.ToPixels(pt.Y);
				}

				if (!startPoint.HasValue)
					startPoint = new Point(x, y);
				else
					linePoints.Add(new Point(x, y));
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
		/// Convert spline abscissa to Curve abscissa.
		/// </summary>
		/// <param name="hScale">The X-axis scale.</param>
		/// <param name="splineX">The spline point X value.</param>
		/// <param name="curvePoints">Original curve points.</param>
		/// <returns></returns>
		private static double splineXtoCurveX(ChartSeriesScale hScale, double splineX
			, List<DataPoint<object, double>> curvePoints)
		{
			int n = (int)splineX;
			if (n >= curvePoints.Count - 1)
				return hScale.ToPixels(curvePoints[n].X);

			double delta = splineX - n;
			return hScale.ToPixels(curvePoints[n].X) * (1 - delta)
				+ hScale.ToPixels(curvePoints[n + 1].X) * delta;
		}

		/// <summary>
		/// Approximate the spline with the PolyLine with the tolerance given.
		/// </summary>
		/// <param name="spline">The spline.</param>
		/// <param name="tolerance">The tolerance, i.e. the maximum distance from the spline
		///		to the approximating polyline.</param>
		/// <returns>List of points of the PolyLine approximating the spline 
		///		with the tolerance given.</returns>
		static List<Point> GetSplinePolyLineApproximation(Spline spline, double tolerance)
		{
			List<Point> points = new List<Point>();
			points.Add(spline.Points[0]);
			// Loop by the spline subintervals.
			for (int i = 1; i < spline.Points.Length; ++i)
			{
				Collection<Point> cpPoints = GetApproximation(spline.Points[i - 1], spline.Points[i]
					, spline.SecondDerivative[i - 1], spline.SecondDerivative[i], tolerance);
				// Copy points but the first one.
				for (int j = 1; j < cpPoints.Count; ++j)
				{
					points.Add(cpPoints[j]);
				}
			}
			return points;
		}

		/// <summary>
		/// Approximate the cubic polynomial with the PolyLine with the tolerance given.
		/// </summary>
		/// <param name="pt1">Cubic polynomial left point.</param>
		/// <param name="pt2">Cubic polynomial right point.</param>
		/// <param name="y21">Cubic polynomial second derivative at the left point.</param>
		/// <param name="y22">Cubic polynomial second derivative at the right point.</param>
		/// <param name="tolerance">The tolerance, i.e. the maximum distance from the spline
		///		to the approximating polyline.</param>
		/// <returns>List of points of the PolyLine approximating the cubic 
		///		polynomial with the tolerance given.</returns>
		static Collection<Point> GetApproximation(Point pt1, Point pt2, double y21, double y22, double tolerance)
		{
			double x1 = pt1.X, x2 = pt2.X;
			double y1 = pt1.Y, y2 = pt2.Y;

			// Subinterval polynomial coefficients.
			double a = (y22 - y21) / (6 * (x2 - x1));
			double b = (y21 - 6 * a * x1) / 2;
			double c = (y2 - x2 * x2 * (a * x2 + b) - y1 + x1 * x1 * (a * x1 + b)) / (x2 - x1);
			double d = y1 - x1 * (x1 * (a * x1 + b) + c);
			if (a == 0)
				a = double.Epsilon;

			return CubicPolynomialPolylineApproximation.Approximate(new Polynomial(new double[] { d, c, b, a })
				, x1, x2, tolerance);
		}
	}
}
