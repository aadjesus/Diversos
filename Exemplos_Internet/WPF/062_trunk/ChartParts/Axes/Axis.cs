// <copyright file="Axis.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Axis element abstract base class.</summary>
// <revision>$Id: Axis.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Diagnostics;
using System.ComponentModel; // for ISupportInitialize
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Axis element abstract base class.
	/// </summary>
	/// <remarks>
	/// Axis element derived classes display <see cref="ChartScale"/> objects data with ticks, tick labels, etc.
	/// </remarks>
	public abstract class Axis : FrameworkElement, ISupportInitialize
	{
		#region Dependency properties
		#region AxisScale
		/// <summary>
		/// Identifies the <see cref="AxisScale"/> dependency property.
		/// </summary>
		/// <remarks>Can contain any object of <see cref="ChartScale"/> derived type.</remarks>
		public static readonly DependencyProperty AxisScaleProperty
			= DependencyProperty.Register("AxisScale", typeof(ChartScale), typeof(Axis)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					, AxisScalePropertyChanged));
		/// <summary>
		/// Gets or sets the reference to a <see cref="ChartScale"/> object displayed by this element.
		/// This is a dependency property.
		/// </summary>
		/// <value><see cref="AxisScaleProperty"/> value</value>
		public ChartScale AxisScale
		{
			get { return (ChartScale)GetValue(AxisScaleProperty); }
			set { SetValue(AxisScaleProperty, value); }
		}
		/// <summary>
		/// Called when <see cref="AxisScaleProperty"/> changes.
		/// </summary>
		/// <param name="sender"/>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		private static void AxisScalePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Axis ths = sender as Axis;
			if (ths == null)
				return;

			ChartScale scale = e.OldValue as ChartScale;
			if (scale != null)
				scale.PropertyChanged -= ths.ScalePropertyChanged;
			scale = e.NewValue as ChartScale;
			if (scale != null)
				scale.PropertyChanged += ths.ScalePropertyChanged;

			ths.CoerceValue(LabelFormatProperty);
		}
		/// <summary>
		/// Handles the PropertyChanged event of the ChartScale object.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void ScalePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!initializing)
			{
				InvalidateVisual();
				InvalidateMeasure();
			}
		}
		#endregion AxisScale

		#region ContentLayout
		/// <summary>
		/// Identifies the <see cref="ContentLayout"/> dependency property.
		/// </summary>
		/// <remarks>
		/// <see cref="AxisContentLayout"/> flag <c>enum</c> defines where axis ticks and labels 
		/// appear relative to axis line.
		/// E.g. axis ticks might be drawn above or below the axis line or might be
		/// centered relative to the line.
		/// </remarks>
		public static readonly DependencyProperty ContentLayoutProperty
			= DependencyProperty.RegisterAttached("ContentLayout", typeof(AxisContentLayout), typeof(Axis)
				, new FrameworkPropertyMetadata(AxisContentLayout.AtLeftOrBelow
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				, ContentLayout_Validate);

		/// <summary>
		/// Gets or sets the Axis layout.
		/// </summary>
		/// <remarks>
		/// <see cref="AxisContentLayout"/> flag <c>enum</c> defines where axis ticks and labels 
		/// appear relative to axis line.
		/// E.g. axis ticks might be drawn above or below the axis line or might be
		/// centered relative to the line.
		/// </remarks>
		/// <value><see cref="ContentLayoutProperty"/> value.</value>
		public AxisContentLayout ContentLayout
		{
			get { return (AxisContentLayout)GetValue(ContentLayoutProperty); }
			set { SetValue(ContentLayoutProperty, value); }
		}

		/// <summary>
		/// Gets the Axis layout.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns><see cref="ContentLayoutProperty"/> value.</returns>
		public static AxisContentLayout GetContentLayout(DependencyObject element)
		{
			return (AxisContentLayout)element.GetValue(ContentLayoutProperty);
		}
		
		/// <summary>
		/// Sets the Axis layout.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">New <see cref="ContentLayoutProperty"/> value.</param>
		public static void SetContentLayout(DependencyObject element, AxisContentLayout value)
		{
			element.SetValue(ContentLayoutProperty, value);
		}
		
		/// <summary>
		/// Validates suggested property value.
		/// </summary>
		/// <param name="value">suggested property value</param>
		/// <returns></returns>
		private static bool ContentLayout_Validate(Object value)
		{
			AxisContentLayout layout = (AxisContentLayout)value;
			return (((layout & AxisContentLayout.AtLeftOrBelow) > 0 || (layout & AxisContentLayout.AtRightOrAbove) > 0)
				&& (((layout & AxisContentLayout.AtLeftOrBelow) == 0) || ((layout & AxisContentLayout.AtRightOrAbove) == 0)));
		}
		#endregion ContentLayout

		#region Ticks
		#region TickLength
		/// <summary>
		/// Identifies the TickLength dependency property.
		/// </summary>
		/// <remarks>
		/// Represents the length of regular (not long) ticks in pixels.
		/// </remarks>
		public static readonly DependencyProperty TickLengthProperty
			= DependencyProperty.RegisterAttached("TickLength", typeof(double), typeof(Axis)
				, new FrameworkPropertyMetadata(5.0
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				, TickLength_Validate);
		/// <summary>
		/// Gets or sets the TickLength property.
		/// </summary>
		/// <remarks>
		/// Represents the length of regular (not long) ticks in pixels.
		/// </remarks>
		/// <value>Any finite positive double value.</value>
		public double TickLength
		{
			get { return (double)GetValue(TickLengthProperty); }
			set { SetValue(TickLengthProperty, value); }
		}
		/// <summary>
		/// Gets the TickLength.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static double GetTickLength(DependencyObject element)
		{
			return (double)element.GetValue(TickLengthProperty);
		}
		/// <summary>
		/// Sets the TickLength.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetTickLength(DependencyObject element, double value)
		{
			element.SetValue(TickLengthProperty, value);
		}
		/// <summary>
		/// Validates suggested value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool TickLength_Validate(Object value)
		{
			double x = (double)value;
			return (!double.IsInfinity(x) && x > 0);
		}
		#endregion TickLength

		#region LongTickLength
		/// <summary>
		/// Identifies the LongTickLength dependency property.
		/// </summary>
		/// <remarks>
		/// Represents the length of long ticks in pixels.
		/// </remarks>
		public static readonly DependencyProperty LongTickLengthProperty
			= DependencyProperty.RegisterAttached("LongTickLength", typeof(double), typeof(Axis)
				, new FrameworkPropertyMetadata(11.0
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					, null, LongTickLength_Coerce)
				, LongTickLength_Validate);
		/// <summary>
		/// Gets or sets the LongTickLength property.
		/// </summary>
		/// <remarks>
		/// Represents the length of long ticks in pixels.
		/// </remarks>
		/// <value>Any finite positive double value.</value>
		public double LongTickLength
		{
			get { return (double)GetValue(LongTickLengthProperty); }
			set { SetValue(LongTickLengthProperty, value); }
		}
		/// <summary>
		/// Gets the LongTickLength.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static double GetLongTickLength(DependencyObject element)
		{
			return (double)element.GetValue(LongTickLengthProperty);
		}
		/// <summary>
		/// Sets the LongTickLength.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetLongTickLength(DependencyObject element, double value)
		{
			element.SetValue(LongTickLengthProperty, value);
		}
		/// <summary>
		/// Coerce LongTickLength value.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		private static object LongTickLength_Coerce(DependencyObject d, object value)
		{
			Axis ths = d as Axis;
			if (ths == null)
				return value;

			double tickLength = ths.TickLength;
			double longTickLength = (double)value;
			if (longTickLength > tickLength)
				return value;
			return tickLength + 4;
		}
		/// <summary>
		/// Validates suggested value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool LongTickLength_Validate(Object value)
		{
			double x = (double)value;
			return (!double.IsInfinity(x) && x > 0);
		}
		#endregion LongTickLength
		#endregion Ticks

		#region Pen
		/// <summary>
		/// Identifies the Pen dependency property.
		/// </summary>
		/// <remarks>
		/// The pen used to draw axis line and tick marks. 
		/// Also, pen brush used to draw axis tick labels.
		/// </remarks>
		public static readonly DependencyProperty PenProperty
			= DependencyProperty.RegisterAttached("Pen", typeof(Pen), typeof(Axis)
				, new FrameworkPropertyMetadata(new Pen(Brushes.Black, 1)
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				);
		/// <summary>
		/// Gets or sets the Pen property.
		/// </summary>
		/// <remarks>
		/// The pen used to draw axis line and tick marks. 
		/// Also, pen brush used to draw axis tick labels.
		/// </remarks>
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

		#region Font
		#region FontFamily
		/// <summary>
		/// FontFamily property is inherited from <see cref="T:System.Windows.Controls.TextBlock"/> element.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public static readonly DependencyProperty FontFamilyProperty
			= TextBlock.FontFamilyProperty.AddOwner(typeof(Axis)
				, new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				);
		/// <summary>
		/// Gets or sets the FontFamily property.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public FontFamily FontFamily
		{
			get { return (FontFamily)GetValue(FontFamilyProperty); }
			set { SetValue(FontFamilyProperty, value); }
		}
		/// <summary>
		/// Gets the FontFamily.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>FontFamily value</returns>
		public static FontFamily GetFontFamily(UIElement element)
		{
			return (FontFamily)element.GetValue(FontFamilyProperty);
		}
		/// <summary>
		/// Sets the FontFamily.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetFontFamily(UIElement element, FontFamily value)
		{
			element.SetValue(FontFamilyProperty, value);
		}
		#endregion FontFamily

		#region FontSize
		/// <summary>
		/// FontSize property is inherited from <see cref="System.Windows.Controls.TextBlock"/> element.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public static readonly DependencyProperty FontSizeProperty
			= TextBlock.FontSizeProperty.AddOwner(typeof(Axis)
				, new FrameworkPropertyMetadata(SystemFonts.MessageFontSize
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				);
		/// <summary>
		/// Gets or sets the FontSize property.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}
		/// <summary>
		/// Gets the FontSize.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static double GetFontSize(UIElement element)
		{
			return (double)element.GetValue(FontSizeProperty);
		}
		/// <summary>
		/// Sets the FontSize.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetFontSize(UIElement element, double value)
		{
			element.SetValue(FontSizeProperty, value);
		}
		#endregion FontSize

		#region FontStretch
		/// <summary>
		/// FontStretch property is inherited from <see cref="System.Windows.Controls.TextBlock"/> element.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public static readonly DependencyProperty FontStretchProperty
			= TextBlock.FontStretchProperty.AddOwner(typeof(Axis)
				, new FrameworkPropertyMetadata(FontStretches.Normal
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				);
		/// <summary>
		/// Gets or sets the FontStretch property.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public FontStretch FontStretch
		{
			get { return (FontStretch)GetValue(FontStretchProperty); }
			set { SetValue(FontStretchProperty, value); }
		}
		/// <summary>
		/// Gets the FontStretch.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static FontStretch GetFontStretch(UIElement element)
		{
			return (FontStretch)element.GetValue(FontStretchProperty);
		}
		/// <summary>
		/// Sets the FontStretch.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetFontStretch(UIElement element, FontStretch value)
		{
			element.SetValue(FontStretchProperty, value);
		}
		#endregion FontStretch

		#region FontStyle
		/// <summary>
		/// FontStyle property is inherited from <see cref="System.Windows.Controls.TextBlock"/> element.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public static readonly DependencyProperty FontStyleProperty
			= TextBlock.FontStyleProperty.AddOwner(typeof(Axis)
				, new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				);
		/// <summary>
		/// Gets or sets the FontStyle property.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public FontStyle FontStyle
		{
			get { return (FontStyle)GetValue(FontStyleProperty); }
			set { SetValue(FontStyleProperty, value); }
		}
		/// <summary>
		/// Gets the FontStyle.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static FontStyle GetFontStyle(UIElement element)
		{
			return (FontStyle)element.GetValue(FontStyleProperty);
		}
		/// <summary>
		/// Sets the FontStyle.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetFontStyle(UIElement element, FontStyle value)
		{
			element.SetValue(FontStyleProperty, value);
		}
		#endregion FontStyle

		#region FontWeight
		/// <summary>
		/// FontWeight property is inherited from <see cref="System.Windows.Controls.TextBlock"/> element.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public static readonly DependencyProperty FontWeightProperty
			= TextBlock.FontWeightProperty.AddOwner(typeof(Axis)
				, new FrameworkPropertyMetadata(FontWeights.Normal
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				);
		/// <summary>
		/// Gets or sets the FontWeight property.
		/// </summary>
		/// <remarks>Used to draw axis tick labels.</remarks>
		public FontWeight FontWeight
		{
			get { return (FontWeight)GetValue(FontWeightProperty); }
			set { SetValue(FontWeightProperty, value); }
		}
		/// <summary>
		/// Sets the FontWeight.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetFontWeight(UIElement element, FontWeight value)
		{
			element.SetValue(FontWeightProperty, value);
		}
		/// <summary>
		/// Gets the FontWeight.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static FontWeight GetFontWeight(UIElement element)
		{
			return (FontWeight)element.GetValue(FontWeightProperty);
		}
		#endregion FontWeight
		#endregion Font

		#region Label
		#region LabelFormat
		/// <summary>
		/// LabelFormat dependency property.
		/// </summary>
		/// <remarks>Standard format string.</remarks>
		public static readonly DependencyProperty LabelFormatProperty
			= DependencyProperty.Register("LabelFormat", typeof(string), typeof(Axis)
				, new FrameworkPropertyMetadata("G"
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets the LabelFormat property.
		/// </summary>
		/// <remarks>Standard format string.</remarks>
		public string LabelFormat
		{
			get { return (string)GetValue(LabelFormatProperty); }
			set { SetValue(LabelFormatProperty, value); }
		}
		#endregion LabelFormat

		#region LabelMargin
		/// <summary>
		/// LabelMargin dependency property.
		/// </summary>
		/// <remarks>Defines the space between long tick and its label.</remarks>
		public static readonly DependencyProperty LabelMarginProperty
			= DependencyProperty.RegisterAttached("LabelMargin", typeof(double), typeof(Axis)
				, new FrameworkPropertyMetadata(3.0
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
						| FrameworkPropertyMetadataOptions.Inherits
					)
				, LabelMargin_Validate);
		/// <summary>
		/// Gets or sets the LabelMargin property.
		/// </summary>
		/// <remarks>Defines the space between long tick and its label.</remarks>
		public double LabelMargin
		{
			get { return (double)GetValue(LabelMarginProperty); }
			set { SetValue(LabelMarginProperty, value); }
		}
		/// <summary>
		/// Gets the LabelMargin.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static double GetLabelMargin(DependencyObject element)
		{
			return (double)element.GetValue(LabelMarginProperty);
		}
		/// <summary>
		/// Sets the LabelMargin.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="value">The value.</param>
		public static void SetLabelMargin(DependencyObject element, double value)
		{
			element.SetValue(LabelMarginProperty, value);
		}
		/// <summary>
		/// Validates suggested value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool LabelMargin_Validate(Object value)
		{
			return (double)value >= 0.0;
		}
		#endregion LabelMargin
		#endregion Label
		#endregion Dependency properties

		#region ISupportInitialize Members
		/// <exclude/>
		protected bool initializing;
		/// <summary>
		/// Starts the initialization process for this element.
		/// </summary>
		public override void BeginInit()
		{
			base.BeginInit();
			initializing = true;
		}
		/// <summary>
		/// Indicates that the initialization process for the element is complete.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// 	<see cref="M:System.Windows.FrameworkElement.EndInit"/> was called without <see cref="M:System.Windows.FrameworkElement.BeginInit"/> having previously been called on the element.
		/// </exception>
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
