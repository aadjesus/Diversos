// <copyright file="HardcodedCurveVisual.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Hardcoded Curve Visual.</summary>
// <revision>$Id: HardcodedCurveVisual.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Hardcoded Curve visual class draws the curve passed in by CurveDelegate.
	/// </summary>
	public class HardcodedCurveVisual : ItemVisual
	{
		/// <summary>
		/// Renders the Curve. 
		/// </summary>
		protected internal override void Render()
		{
			using (DrawingContext dc = RenderOpen())
			{
				CurveDataView curveDataView = ItemDataView as CurveDataView;
				if (curveDataView == null)
					return;

				// Render the Curve in (x - Numeric, y - Numeric) coordinates.
				HardcodedCurveData<double, double> curveData = ItemDataView.ItemData as HardcodedCurveData<double, double>;
				if (curveData != null)
				{
					Render(dc, curveDataView, curveData);
					return;
				}
				// Render the Curve in (x - DateTime, y - Numeric) coordinates.
				HardcodedCurveData<DateTime, double> curveDTNData = ItemDataView.ItemData as HardcodedCurveData<DateTime, double>;
				if (curveDTNData != null)
				{
					Render(dc, curveDataView, curveDTNData);
					return;
				}
			}
		}

		/// <summary>
		/// Renders the Curve in (x - double, y - double) coordinates.
		/// </summary>
		/// <remarks>
		/// This method draws every Curve pixel. Not very efficient.
		/// </remarks>
		private void Render(DrawingContext dc, CurveDataView curveDataView
			, HardcodedCurveData<double, double> curveData)
		{
			HardcodedCurveData<double, double>.CurveDelegate curveDelegate = curveData.Curve;
			ChartScale hScale = curveDataView.HorizontalScale, vScale = curveDataView.VerticalScale;
			if (vScale == null || !vScale.IsConsistent || hScale == null || !hScale.IsConsistent
				|| curveDelegate == null || curveDataView.Pen == null)
				return;

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				bool figureStarted = false;
				if (curveDataView.Orientation == Orientation.Horizontal)
				{
					int n = (int)hScale.ToPixels(hScale.Stop); // X-axis pixel count
					double step = (Convert.ToDouble(hScale.Stop) - Convert.ToDouble(hScale.Start)) / n; // X-axis step

					for (int i = 0; i < n; ++i)
					{
						// Curve value
						double y = curveDelegate(Convert.ToDouble(hScale.Start) + i * step);

						if (fitIntoRange(y, vScale))
						{ // Curve value fits into Scale range
							if (!figureStarted)
							{ // Curve figure isn't yet started.
								ctx.BeginFigure(new Point(i, vScale.ToPixels(y))
									, false /* is filled */, false /* is closed */);
								figureStarted = true;
							}
							else
								ctx.LineTo(new Point(i, vScale.ToPixels(y))
									, true /* is stroked */, true /* is smooth join */);
						}
						else // Curve value doesn't fit into Scale range
							figureStarted = false;
					}
				}
				else // Orientation == Orientation.Vertical
				{
					int n = (int)vScale.ToPixels(vScale.Stop); // Y-axis pixel count
					double step = (Convert.ToDouble(vScale.Stop) - Convert.ToDouble(vScale.Start)) / n; // Y-axis step

					for (int i = 0; i < n; ++i)
					{
						// Curve value
						double y = curveDelegate(Convert.ToDouble(vScale.Start) + i * step);

						if (fitIntoRange(y, hScale))
						{ // Curve value fits into Scale range
							if (!figureStarted)
							{ // Curve figure isn't yet started.
								ctx.BeginFigure(new Point(hScale.ToPixels(y), i)
									, false /* is filled */, false /* is closed */);
								figureStarted = true;
							}
							else
								ctx.LineTo(new Point(hScale.ToPixels(y), i)
									, true /* is stroked */, true /* is smooth join */);
						}
						else // Curve value doesn't fit into Scale range
							figureStarted = false;
					}
				}
			}
			geometry.Freeze();

			dc.DrawGeometry(Brushes.Transparent, curveDataView.Pen, geometry);
		}

		/// <summary>
		/// Renders the Curve in (x - DateTime, y - Numeric) coordinates.
		/// </summary>
		/// <remarks>
		/// This method draws every Curve pixel. Not very efficient.
		/// </remarks>
		private void Render(DrawingContext dc, CurveDataView curveDataView
			, HardcodedCurveData<DateTime, double> curveData)
		{
			HardcodedCurveData<DateTime, double>.CurveDelegate curveDelegate = curveData.Curve;
			ChartDateTimeScale hScale = curveDataView.HorizontalScale as ChartDateTimeScale;
			ChartScale vScale = curveDataView.VerticalScale;
			if (vScale == null || !vScale.IsConsistent || hScale == null || !hScale.IsConsistent
				|| curveDelegate == null || curveDataView.Pen == null)
				return;

			// Curve figure geometry
			StreamGeometry geometry = new StreamGeometry();
			using (StreamGeometryContext ctx = geometry.Open())
			{
				bool figureStarted = false;
				if (curveDataView.Orientation == Orientation.Horizontal)
				{
					int n = (int)hScale.ToPixels(hScale.Stop); // X-axis pixel count
					long ticksStep = (((DateTime)hScale.Stop).Ticks - ((DateTime)hScale.Start).Ticks) / n; // X-axis step in Ticks

					for (int i = 0; i < n; ++i)
					{
						// Curve value
						double y = curveDelegate(((DateTime)hScale.Start).AddTicks(i * ticksStep));

						if (fitIntoRange(y, vScale))
						{ // Curve value fits into Scale range
							if (!figureStarted)
							{ // Curve figure isn't yet started.
								ctx.BeginFigure(new Point(i, vScale.ToPixels(y))
									, false /* is filled */, false /* is closed */);
								figureStarted = true;
							}
							else
								ctx.LineTo(new Point(i, vScale.ToPixels(y))
									, true /* is stroked */, true /* is smooth join */);
						}
						else // Curve value doesn't fit into Scale range
							figureStarted = false;
					}
				}
				else // Orientation == Orientation.Vertical
				{
					int n = (int)hScale.ToPixels(hScale.Stop); // Y-axis pixel count
					long ticksStep = (((DateTime)hScale.Stop).Ticks - ((DateTime)hScale.Start).Ticks) / n; // Y-axis step in Ticks

					for (int i = 0; i < n; ++i)
					{
						// Curve value
						double y = curveDelegate(((DateTime)hScale.Start).AddTicks(i * ticksStep));

						if (fitIntoRange(y, vScale))
						{ // Curve value fits into Scale range
							if (!figureStarted)
							{ // Curve figure isn't yet started.
								ctx.BeginFigure(new Point(i, vScale.ToPixels(y))
									, false /* is filled */, false /* is closed */);
								figureStarted = true;
							}
							else
								ctx.LineTo(new Point(i, vScale.ToPixels(y))
									, true /* is stroked */, true /* is smooth join */);
						}
						else // Curve value doesn't fit into Scale range
							figureStarted = false;
					}
				}
			}
			geometry.Freeze();

			dc.DrawGeometry(Brushes.Transparent, curveDataView.Pen, geometry);
		}

		/// <summary>
		/// Checks if 'x' fits into ChartScale range
		/// </summary>
		/// <param name="y">coordinate value in pixels</param>
		/// <param name="scale"></param>
		/// <returns></returns>
		static bool fitIntoRange(double y, ChartScale scale)
		{
			double start = Convert.ToDouble(scale.Start), stop = Convert.ToDouble(scale.Stop);
			if (start < stop)
			{
				if (y >= start && y <= stop)
					return true;
			}
			else // start > stop
			{
				if (y >= stop && y <= start)
					return true;
			}
			return false;
		}
	}
}
