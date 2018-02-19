// <copyright file="LogarithmicAxis.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Logarithmic Axis element class.</summary>
// <revision>$Id: LogarithmicAxis.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Globalization;  // For CultureInfo
using System.Windows;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Logarithmic Axis element class.
	/// </summary>
	/// <remarks>
	/// Displays the ChartLogarithmicScale data.
	/// </remarks>
	public class LogarithmicAxis : Axis
	{
		// This Axis type doesn't use LabelFormat property and, so, hasn't in its coercion.

		/// <summary>
		/// Renders the Axis
		/// </summary>
		protected override void OnRender(DrawingContext dc)
		{
			ChartLogarithmicScale axisScale = AxisScale as ChartLogarithmicScale;
			if (axisScale == null || !axisScale.CompatibleWith(typeof(double)) || Pen == null)
				return; // Nothing to draw

			double lineLength = axisScale.ToPixels(axisScale.Stop);

			double tickLength = TickLength, longTickLength = LongTickLength;
			bool centerTicks = (ContentLayout & AxisContentLayout.TicksCentered) > 0;
			double fontSize = FontSize;

			Typeface typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
			// Label "10"
			FormattedText ftLabel10 = new FormattedText("10",
				CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
				typeface, fontSize, Pen.Brush);

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
					double tickPos = axisScale.ToPixels((double)tick.Value);

					if (tick.IsLong)
					{ // Draw long tick and label
						// Long tick
						dc.DrawLine(Pen, new Point(tickPos, 0), new Point(tickPos, longTickLength));

						// Label
						string label = Math.Log10((double)tick.Value).ToString();
						FormattedText ftLabel = new FormattedText(label
							, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight
							, typeface, fontSize / 1.5, Pen.Brush);

						double labelWigth = ftLabel10.Width + ftLabel.Width + 1;
						double labelPos = tickPos - labelWigth / 2;
						if (labelPos > lastLabelPos && labelPos + labelWigth <= lineLength)
						{ // Prevents labels from overlapping and from drawing out of Axis bounds
							dc.DrawText(ftLabel10, new Point(labelPos, labelTop));
							dc.DrawText(ftLabel, new Point(labelPos + ftLabel10.Width + 1, labelTop));
							lastLabelPos = labelPos + labelWigth;
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
					double tickPos = axisScale.ToPixels((double)tick.Value);

					if (tick.IsLong)
					{ // Draw long tick and label
						// Long tick
						if (centerTicks)
							dc.DrawLine(Pen, new Point(tickPos, baseLine - longTickLength / 2)
								, new Point(tickPos, baseLine + longTickLength / 2));
						else
							dc.DrawLine(Pen, new Point(tickPos, baseLine), new Point(tickPos, baseLine - longTickLength));

						// Label
						string label = Math.Log10((double)tick.Value).ToString();
						FormattedText ftLabel = new FormattedText(label
							, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight
							, typeface, fontSize / 1.5, Pen.Brush);

						double labelWigth = ftLabel10.Width + ftLabel.Width + 1;
						double labelPos = tickPos - labelWigth / 2;
						if (labelPos > lastLabelPos && labelPos + labelWigth <= lineLength)
						{ // Prevents labels from overlapping and from drawing out of Axis bounds
							dc.DrawText(ftLabel10, new Point(labelPos, 0));
							dc.DrawText(ftLabel, new Point(labelPos + ftLabel10.Width + 1, 0));
							lastLabelPos = labelPos + labelWigth;
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
		/// <summary>
		/// When overridden in a derived class, measures the size in layout required for child 
		/// elements and determines a size for the 
		/// <see cref="T:System.Windows.FrameworkElement"/>-derived class.
		/// </summary>
		/// <param name="availableSize">The available size that this element can give to child 
		/// elements. Infinity can be specified as a value to indicate that the element will size 
		/// to whatever content is available.</param>
		/// <returns>
		/// The size that this element determines it needs during layout, based on its calculations 
		/// of child element sizes.
		/// </returns>
		protected override Size MeasureOverride(Size availableSize)
		{
			ChartLogarithmicScale axisScale = AxisScale as ChartLogarithmicScale;
			if (axisScale == null || !axisScale.CompatibleWith(typeof(double)) || Pen == null)
				return new Size(0, 0);

			// Axis line length
			double lineLength = axisScale.ToPixels(axisScale.Stop);
			// Axis height
			FormattedText ftLabel = new FormattedText("1"
				, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight
				, new Typeface(FontFamily, FontStyle, FontWeight, FontStretch)
				, FontSize, Pen.Brush);
			double height = LongTickLength + LabelMargin + ftLabel.Height;

			return new Size(lineLength, height);
		}

		/// <summary>
		/// When overridden in a derived class, positions child elements and determines 
		/// a size for a <see cref="T:System.Windows.FrameworkElement"/> derived class.
		/// </summary>
		/// <param name="finalSize">The final area within the parent that this element 
		/// should use to arrange itself and its children.</param>
		/// <returns>The actual size used.</returns>
		protected override Size ArrangeOverride(Size finalSize)
		{
			return finalSize;
		}
		#endregion Layout Overrides
	}
}
