// <copyright file="Grid.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Grid element class. Wraps GridVisual object.</summary>
// <revision>$Id: Grid.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.ComponentModel; // For ISupportInitialize
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Grid element class. Wraps GridVisual object.
	/// </summary>
	public class Grid : FrameworkElement, ISupportInitialize
	{
		#region Dependency properties
		#region HorizontalScale
		/// <summary>
		/// Identifies the HorizontalScale dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalScaleProperty
			= DependencyProperty.Register("HorizontalScale", typeof(ChartScale), typeof(Grid)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					, ScalePropertyChanged
					)
				);
		/// <summary>
		/// Gets or sets the HorizontalScale property.
		/// </summary>
		/// <value>HorizontalScaleProperty.</value>
		public ChartScale HorizontalScale
		{
			get { return (ChartScale)GetValue(HorizontalScaleProperty); }
			set { SetValue(HorizontalScaleProperty, value); }
		}
		#endregion HorizontalScale

		#region VerticalScale
		/// <summary>
		/// Identifies the VerticalScale dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalScaleProperty
			= DependencyProperty.Register("VerticalScale", typeof(ChartScale), typeof(Grid)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					, ScalePropertyChanged
					)
				);
		/// <summary>
		/// Gets or sets the VerticalScale property.
		/// </summary>
		/// <value>VerticalScaleProperty.</value>
		public ChartScale VerticalScale
		{
			get { return (ChartScale)GetValue(VerticalScaleProperty); }
			set { SetValue(VerticalScaleProperty, value); }
		}
		#endregion VerticalScale

		#region Orientation
		/// <summary>
		/// Identifies the Orientation dependency property.
		/// </summary>
		/// <remarks>Defines the direction in which grid lines are drawn: Vertical or Horizontal.</remarks>
		public static readonly DependencyProperty OrientationProperty
			= DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Grid)
				, new FrameworkPropertyMetadata(Orientation.Vertical
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets the Orientation property.
		/// </summary>
		/// <remarks>Defines the direction in which grid lines are drawn: Vertical or Horizontal.</remarks>
		/// <value>OrientationProperty</value>
		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
		#endregion Orientation

		#region GridVisibility
		/// <summary>
		/// Identifies the GridVisibility dependency property.
		/// </summary>
		/// <remarks>Defines which grid lines should be visible.</remarks>
		public static readonly DependencyProperty GridVisibilityProperty
			= DependencyProperty.Register("GridVisibility", typeof(GridVisibility), typeof(Grid)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				, GridVisibility_Validate);
		/// <summary>
		/// Gets or sets the GridVisibility property.
		/// </summary>
		/// <remarks>Defines which grid lines should be visible.</remarks>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility GridVisibility
		{
			get { return (GridVisibility)GetValue(GridVisibilityProperty); }
			set { SetValue(GridVisibilityProperty, value); }
		}
		/// <summary>
		/// Validates suggested property value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool GridVisibility_Validate(Object value)
		{
			GridVisibility x = (GridVisibility)value;
			return (x == GridVisibility.AllTicks || x == GridVisibility.LongTicks || x == GridVisibility.Hidden);
		}
		#endregion Visibility

		#region Pen
		/// <summary>
		/// Identifies the Pen dependency property.
		/// </summary>
		public static readonly DependencyProperty PenProperty
			= DependencyProperty.RegisterAttached("Pen", typeof(Pen), typeof(Grid)
				, new FrameworkPropertyMetadata(
					new Pen(Brushes.Gray, 0.5) { DashStyle = new DashStyle(new double[] { 2, 4 }, 0) }
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits)
				);
		/// <summary>
		/// Gets or sets the Pen property.
		/// </summary>
		/// <value>PenProperty.</value>
		public Pen Pen
		{
			get { return (Pen)GetValue(PenProperty); }
			set { SetValue(PenProperty, value); }
		}
		/// <summary>
		/// Gets the pen.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static Pen GetPen(DependencyObject element)
		{
			return (Pen)element.GetValue(PenProperty);
		}
		/// <summary>
		/// Sets the pen.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetPen(DependencyObject element, Pen value)
		{
			element.SetValue(PenProperty, value);
		}
		#endregion Pen

		/// <summary>
		/// Called when one of ScaleProperties changes.
		/// </summary>
		/// <param name="sender">Dependency Object</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void ScalePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Grid ths = sender as Grid;
			if (ths == null)
				return;

			ChartScale scale = e.OldValue as ChartScale;
			if (scale != null)
				scale.PropertyChanged -= ths.ChartScalePropertyChanged;
			scale = e.NewValue as ChartScale;
			if (scale != null)
				scale.PropertyChanged += ths.ChartScalePropertyChanged;
		}

		/// <summary>
		/// Handles the PropertyChanged event of the ChartScale object.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void ChartScalePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!initializing)
			{
				InvalidateVisual();
				InvalidateMeasure();
			}
		}
		#endregion Dependency properties

		/// <summary>
		/// Renders the Grid.
		/// </summary>
		protected override void OnRender(DrawingContext dc)
		{
			ChartScale verticalScale = VerticalScale;
			ChartScale horizontalScale = HorizontalScale;
			if (verticalScale == null || !verticalScale.IsConsistent
				|| horizontalScale == null || !horizontalScale.IsConsistent
				|| Pen == null || GridVisibility == GridVisibility.Hidden)
				return; // Nothing to draw

			if (Orientation == Orientation.Vertical)
			{
				double gridLineLength = verticalScale.ToPixels(verticalScale.Stop);
				// Draw grid lines
				foreach (ScaleTick tick in horizontalScale.Ticks())
				{
					if (!tick.IsLong && GridVisibility == GridVisibility.LongTicks)
						continue;
					double tickPos = horizontalScale.ToPixels(tick.Value);
					dc.DrawLine(Pen, new Point(tickPos, 0), new Point(tickPos, gridLineLength));
				}
			}
			else // Orientation == Orientation.Horizontal
			{
				double gridLineLength = horizontalScale.ToPixels(horizontalScale.Stop);
				// Draw grid lines
				foreach (ScaleTick tick in verticalScale.Ticks())
				{
					if (!tick.IsLong && GridVisibility == GridVisibility.LongTicks)
						continue;
					double tickPos = verticalScale.ToPixels(tick.Value);
					dc.DrawLine(Pen, new Point(0, tickPos), new Point(gridLineLength, tickPos));
				}
			}
		}

		#region Layout Overrides
		/// <inheritdoc />
		protected override Size MeasureOverride(Size availableSize)
		{
			ChartScale verticalScale = VerticalScale;
			ChartScale horizontalScale = HorizontalScale;
			if (verticalScale == null || !verticalScale.IsConsistent
				|| horizontalScale == null || !horizontalScale.IsConsistent
				|| Pen == null || GridVisibility == GridVisibility.Hidden)
				return new Size(0, 0);

			double hSize = horizontalScale.ToPixels(horizontalScale.Stop);
			double vSize = verticalScale.ToPixels(verticalScale.Stop);
			return new Size(hSize, vSize);
		}

		/// <inheritdoc />
		protected override Size ArrangeOverride(Size finalSize)
		{
			return finalSize;
		}
		#endregion Layout Overrides

		#region ISupportInitialize Members
		bool initializing;
		/// <inheritdoc />
		public override void BeginInit()
		{
			base.BeginInit();
			initializing = true;
		}

		/// <inheritdoc />
		public override void EndInit()
		{
			base.EndInit();
			initializing = false;
			InvalidateVisual();
			InvalidateMeasure();
		}
		#endregion ISupportInitialize Members
	}
}
