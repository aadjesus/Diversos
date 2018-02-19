// WPF CurveChart library.
// Copyright © Oleg V. Polikarpotchkin 2008

using System;
using System.ComponentModel;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;

namespace ovp.CurveChart
{
	/// <summary>
	/// CurveChart control
	/// </summary>
	[TemplatePart(Name = "PART_Chart", Type = typeof(Chart))]
	public class CurveChart : Control
	{
		#region Dependency properties
		#region HorizontalScale
		/// <summary>
		/// Identifies the <see cref="HorizontalScale"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalScaleProperty
			= Chart.HorizontalScaleProperty.AddOwner(typeof(CurveChart)
				, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
		/// <summary>
		/// Gets or sets the <see cref="HorizontalScale"/> <see cref="Chart"/> property.
		/// </summary>
		/// <value>ChartScale value</value>
		public ChartScale HorizontalScale
		{
			get { return (ChartScale)GetValue(HorizontalScaleProperty); }
			set { SetValue(HorizontalScaleProperty, value); }
		}
		#endregion HorizontalScale

		#region VerticalScale
		/// <summary>
		/// Identifies the <see cref="VerticalScale"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalScaleProperty
			= Chart.VerticalScaleProperty.AddOwner(typeof(CurveChart)
				, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
		/// <summary>
		/// Gets or sets the <see cref="VerticalScale"/> <see cref="Chart"/> property.
		/// </summary>
		/// <value>Finite positive value</value>
		public ChartScale VerticalScale
		{
			get { return (ChartScale)GetValue(VerticalScaleProperty); }
			set { SetValue(VerticalScaleProperty, value); }
		}
		#endregion VerticalScale

		#region Grids
		#region HorizontalGrid
		#region HorizontalGridVisibility
		/// <summary>
		/// Identifies the <see cref="HorizontalGridVisibility"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalGridVisibilityProperty
			= Chart.HorizontalGridVisibilityProperty.AddOwner(typeof(CurveChart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks
						, FrameworkPropertyMetadataOptions.AffectsRender));
		/// <summary>
		/// Gets or sets the HorizontalGridVisibility <see cref="CurveChart"/> property.
		/// </summary>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility HorizontalGridVisibility
		{
			get { return (GridVisibility)GetValue(HorizontalGridVisibilityProperty); }
			set { SetValue(HorizontalGridVisibilityProperty, value); }
		}
		#endregion HorizontalGridVisibility

		#region HorizontalGridPen
		/// <summary>
		/// Identifies the <see cref="HorizontalGridPen"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalGridPenProperty
			= Chart.HorizontalGridPenProperty.AddOwner(typeof(CurveChart)
				, new FrameworkPropertyMetadata(
					new Pen(Brushes.Gray, 0.5) { DashStyle = new DashStyle(new double[] { 2, 4 }, 0) }
					, FrameworkPropertyMetadataOptions.AffectsRender));
		/// <summary>
		/// Gets or sets the HorizontalGridPen <see cref="CurveChart"/> property.
		/// </summary>
		/// <value>The pen.</value>
		public Pen HorizontalGridPen
		{
			get { return (Pen)GetValue(HorizontalGridPenProperty); }
			set { SetValue(HorizontalGridPenProperty, value); }
		}
		#endregion HorizontalGridPen
		#endregion HorizontalGrid

		#region VerticalGrid
		#region VerticalGridVisibility
		/// <summary>
		/// Identifies the <see cref="VerticalGridVisibility"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalGridVisibilityProperty
			= Chart.VerticalGridVisibilityProperty.AddOwner(typeof(CurveChart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks
				, FrameworkPropertyMetadataOptions.AffectsRender));
		/// <summary>
		/// Gets or sets the VerticalGridVisibility <see cref="CurveChart"/> property.
		/// </summary>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility VerticalGridVisibility
		{
			get { return (GridVisibility)GetValue(VerticalGridVisibilityProperty); }
			set { SetValue(VerticalGridVisibilityProperty, value); }
		}
		#endregion VerticalGridVisibility

		#region VerticalGridPen
		/// <summary>
		/// Identifies the <see cref="VerticalGridPen"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalGridPenProperty
			= Chart.VerticalGridPenProperty.AddOwner(typeof(CurveChart)
				, new FrameworkPropertyMetadata(
					new Pen(Brushes.Gray, 0.5) { DashStyle = new DashStyle(new double[] { 2, 4 }, 0) }
					, FrameworkPropertyMetadataOptions.AffectsRender));
		/// <summary>
		/// Gets or sets the VerticalGridPen <see cref="CurveChart"/> property.
		/// </summary>
		/// <value>The pen.</value>
		public Pen VerticalGridPen
		{
			get { return (Pen)GetValue(VerticalGridPenProperty); }
			set { SetValue(VerticalGridPenProperty, value); }
		}
		#endregion VerticalGridPen
		#endregion VerticalGrid
		#endregion Grids
		#endregion Dependency properties

		/// <summary>
		/// Class constructor
		/// </summary>
		static CurveChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CurveChart)
				, new FrameworkPropertyMetadata(typeof(CurveChart)));
		}

		/// <summary>
		/// Chart PART
		/// </summary>
		private Chart chart;

		/// <summary>
		/// Gets references to named parts.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			chart = Template.FindName("PART_Chart", this) as Chart;
		}

		[BindableAttribute(true)]
		public ItemCollection Items 
		{
			get { return chart.Items; }
		}
		[BindableAttribute(true)]
		public IEnumerable ItemsSource
		{
			get { return chart.ItemsSource; }
			set { chart.ItemsSource = value; }
		}
	}
}
