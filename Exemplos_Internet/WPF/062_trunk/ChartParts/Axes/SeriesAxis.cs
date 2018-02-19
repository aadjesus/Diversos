// <copyright file="SeriesAxis.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-23</date>
// <summary>OpenWPFChart library. Series Axis element class.</summary>
// <revision>$Id: SeriesAxis.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Diagnostics;
using System.ComponentModel; // For DesignerProperties
using System.Globalization;  // For CultureInfo
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Series Axis element class.
	/// </summary>
	/// <remarks>
	/// Displays the ChartSeriesScale data.
	/// </remarks>
	public class SeriesAxis : Axis
	{
		// This Axis type doesn't use LabelFormat property and, so, hasn't in its coercion.

		/// <summary>
		/// Renders the Axis.
		/// </summary>
		protected override void OnRender(DrawingContext dc)
		{
			ChartSeriesScale axisScale = AxisScale as ChartSeriesScale;
			if (axisScale == null || !axisScale.CompatibleWith(typeof(object)) || Pen == null)
				return; // Nothing to draw

			// Axis line length
			double lineLength = axisScale.ToPixels(axisScale.Stop);

			double tickLength = TickLength, longTickLength = LongTickLength;
			bool centerTicks = (ContentLayout & AxisContentLayout.TicksCentered) > 0;
			double fontSize = FontSize;
			Typeface typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

			if ((ContentLayout & AxisContentLayout.AtLeftOrBelow) > 0)
			{
				// Draw axis line
				if (centerTicks)
					dc.DrawLine(Pen, new Point(0, longTickLength / 2), new Point(lineLength, longTickLength / 2));
				else
					dc.DrawLine(Pen, new Point(0, 0), new Point(lineLength, 0));

				//if (DesignerProperties.GetIsInDesignMode(this))
				//    return;

				// Draw ticks and labels
				double labelTop = longTickLength + LabelMargin;
				double lastLabelPos = 0; // Store right bound of the last label drawn to prevent labels from overlapping.
				foreach (ScaleTick tick in axisScale.Ticks())
				{
					double tickPos = axisScale.ToPixels(tick.Value);

					if (tick.IsLong)
					{ // Draw long tick and label
						// Long tick
						dc.DrawLine(Pen, new Point(tickPos, 0), new Point(tickPos, longTickLength));

						// Label
						string label = tick.Value.ToString();
						FormattedText ftLabel = new FormattedText(label
							, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight
							, typeface, fontSize, Pen.Brush);

						double labelPos = tickPos - ftLabel.Width / 2;
						if (labelPos > lastLabelPos && labelPos + ftLabel.Width <= lineLength)
						{ // Prevents labels from overlapping and from drawing out of Axis bounds
							dc.DrawText(ftLabel, new Point(labelPos, labelTop));
							lastLabelPos = labelPos + ftLabel.Width;
						}
					}
					else
					{ // Draw regular tick
						if (centerTicks)
							dc.DrawLine(Pen, new Point(tickPos, (longTickLength - tickLength) / 2)
								, new Point(tickPos, (longTickLength + tickLength) / 2));
						else
							dc.DrawLine(Pen, new Point(tickPos, 0), new Point(tickPos, tickLength));
					}
				}
			}
			else if ((ContentLayout & AxisContentLayout.AtRightOrAbove) > 0)
			{
				double baseLine;
				if (centerTicks)
					baseLine = longTickLength / 2 + fontSize + LabelMargin;
				else
					baseLine = longTickLength + fontSize + LabelMargin;

				// Draw axis line
				dc.DrawLine(Pen, new Point(0, baseLine), new Point(Math.Abs(lineLength), baseLine));

				//if (DesignerProperties.GetIsInDesignMode(this))
				//    return;

				// Draw ticks and labels
				double lastLabelPos = 0; // Store right bound of the last label drawn to prevent labels from overlapping.
				foreach (ScaleTick tick in axisScale.Ticks())
				{
					double tickPos = axisScale.ToPixels(tick.Value);

					if (tick.IsLong)
					{ // Draw long tick and label
						// Long tick
						if (centerTicks)
							dc.DrawLine(Pen, new Point(tickPos, baseLine - longTickLength / 2)
								, new Point(tickPos, baseLine + longTickLength / 2));
						else
							dc.DrawLine(Pen, new Point(tickPos, baseLine), new Point(tickPos, baseLine - longTickLength));

						// Label
						string label = tick.Value.ToString();
						FormattedText ftLabel = new FormattedText(label
							, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight
							, typeface, fontSize, Pen.Brush);

						double labelPos = tickPos - ftLabel.Width / 2;
						if (labelPos > lastLabelPos && labelPos + ftLabel.Width <= Math.Abs(lineLength))
						{ // Prevents labels from overlapping and from drawing out of Axis bounds
							dc.DrawText(ftLabel, new Point(labelPos, 0));
							lastLabelPos = labelPos + ftLabel.Width;
						}
					}
					else
					{ // Draw regular tick
						if (centerTicks)
							dc.DrawLine(Pen, new Point(tickPos, baseLine - tickLength / 2)
								, new Point(tickPos, baseLine + tickLength / 2));
						else
							dc.DrawLine(Pen, new Point(tickPos, baseLine), new Point(tickPos, baseLine - tickLength));
					}
				}
			}
		}

		#region Layout Overrides
		/// <inheritdoc />
		protected override Size MeasureOverride(Size availableSize)
		{
			ChartSeriesScale axisScale = AxisScale as ChartSeriesScale;
			if (axisScale == null || !axisScale.CompatibleWith(typeof(object)) || Pen == null)
				return new Size(0, 0);

			// Axis line length
			double lineLength = axisScale.ToPixels(axisScale.Stop);
			// Axis height
			FormattedText ftLabel = new FormattedText("A"
				, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight
				, new Typeface(FontFamily, FontStyle, FontWeight, FontStretch)
				, FontSize, Pen.Brush);
			double height = LongTickLength + LabelMargin + ftLabel.Height;

			return new Size(lineLength, height);
		}

		/// <inheritdoc />
		protected override Size ArrangeOverride(Size finalSize)
		{
			return finalSize;
		}
		#endregion Layout Overrides
	}
}
