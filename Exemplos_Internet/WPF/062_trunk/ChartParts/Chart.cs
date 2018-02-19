// WPF CurveChart library.
// Copyright © Oleg V. Polikarpotchkin 2008

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ovp.CurveChart
{
	/// <summary>
	/// Chart contains two axis grids at its bottom z-order and the list of curve visuals in its base class.
	/// </summary>
	public class Chart : ListBox
	{
		#region Dependency properties
		#region HorizontalScale
		/// <summary>
		/// Identifies the <see cref="HorizontalScale"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalScaleProperty
			= CurveVisual.HorizontalScaleProperty.AddOwner(typeof(Chart)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure 
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
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
			= CurveVisual.VerticalScaleProperty.AddOwner(typeof(Chart)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
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

		#region Grids Visibility and Pen
		#region HorizontalGrid
		#region HorizontalGridVisibility
		/// <summary>
		/// Identifies the <see cref="HorizontalGridVisibility"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalGridVisibilityProperty
			= DependencyProperty.Register("HorizontalGridVisibility", typeof(GridVisibility), typeof(Chart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks, FrameworkPropertyMetadataOptions.AffectsRender)
				, Visibility_Validate);
		/// <summary>
		/// Gets or sets the HorizontalGridVisibility <see cref="Chart"/> property.
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
			= DependencyProperty.Register("HorizontalGridPen", typeof(Pen), typeof(Chart)
				, new FrameworkPropertyMetadata(
					new Pen(Brushes.Gray, 0.5) { DashStyle = new DashStyle(new double[]{2, 4}, 0)}
					, FrameworkPropertyMetadataOptions.AffectsRender));
		/// <summary>
		/// Gets or sets the HorizontalGridPen <see cref="Chart"/> property.
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
			= DependencyProperty.Register("VerticalGridVisibility", typeof(GridVisibility), typeof(Chart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks, FrameworkPropertyMetadataOptions.AffectsRender)
				, Visibility_Validate);
		/// <summary>
		/// Gets or sets the VerticalGridVisibility <see cref="Chart"/> property.
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
			= DependencyProperty.Register("VerticalGridPen", typeof(Pen), typeof(Chart)
				, new FrameworkPropertyMetadata(
					new Pen(Brushes.Gray, 0.5) { DashStyle = new DashStyle(new double[]{2, 4}, 0)}
					, FrameworkPropertyMetadataOptions.AffectsRender));
		/// <summary>
		/// Gets or sets the VerticalGridPen <see cref="Chart"/> property.
		/// </summary>
		/// <value>The pen.</value>
		public Pen VerticalGridPen
		{
			get { return (Pen)GetValue(VerticalGridPenProperty); }
			set { SetValue(VerticalGridPenProperty, value); }
		}
		#endregion VerticalGridPen
		#endregion VerticalGrid

		/// <summary>
		/// Validates suggested <see cref="Visibility"/> property value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool Visibility_Validate(Object value)
		{
			GridVisibility x = (GridVisibility)value;
			return (x == GridVisibility.AllTicks || x == GridVisibility.LongTicks || x == GridVisibility.Hidden);
		}
		#endregion Grids Visibility and Pen
		
		#region CurveOverCursor
		///// <summary>
		///// Identifies the <see cref="CurveOverCursor"/> dependency property.
		///// </summary>
		//public static readonly DependencyProperty CurveOverCursorProperty
		//    = DependencyProperty.RegisterAttached("CurveOverCursor", typeof(Cursor), typeof(Chart)
		//        , new FrameworkPropertyMetadata(null
		//            , FrameworkPropertyMetadataOptions.Inherits)
		//        );
		///// <summary>
		///// Gets or sets the <see cref="CurveOverCursor"/> <see cref="Chart"/> property.
		///// </summary>
		///// <value>Cursor value</value>
		//public Cursor CurveOverCursor
		//{
		//    get { return (Cursor)GetValue(CurveOverCursorProperty); }
		//    set { SetValue(CurveOverCursorProperty, value); }
		//}
		///// <summary>
		///// Gets the CurveOverCursor property.
		///// </summary>
		///// <param name="element">The element.</param>
		///// <returns></returns>
		//public static Cursor GetCurveOverCursor(UIElement element)
		//{
		//    return (Cursor)element.GetValue(CurveOverCursorProperty);
		//}
		///// <summary>
		///// Sets the CurveOverCursor property.
		///// </summary>
		///// <param name="element">The element.</param>
		///// <param name="value">The value.</param>
		//public static void SetCurveOverCursor(UIElement element, Cursor value)
		//{
		//    element.SetValue(CurveOverCursorProperty, value);
		//}
		#endregion CurveOverCursor
		#endregion Dependency properties

		/// <summary>
		/// Initializes the <see cref="Chart"/> class.
		/// </summary>
		static Chart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Chart)
				, new FrameworkPropertyMetadata(typeof(Chart)));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chart"/> class.
		/// </summary>
		//public Chart()
		//{
		//    CurveClickEventEventHandler d = CurveClicked;
		//    this.AddHandler(Curve.ClickEvent, d);
		//}

		/// <summary>
		/// Responds to the mouse move event by changing cursor when it's over a curve.
		/// </summary>
		/// <param name="e"></param>
		//protected override void OnMouseMove(MouseEventArgs e)
		//{
		//    base.OnMouseMove(e);

		//    HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
		//    if (result != null && (result.VisualHit is CurveVisual || result.VisualHit is ChartPoint))
		//        Cursor = CurveOverCursor;
		//    else
		//        Cursor = null;
		//}

		/// <summary>
		/// Curve clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		//private void CurveClicked(Object sender, CurveClickEventArgs e)
		//{
		//    Trace.WriteLine(string.Format("***** Chart.CurveClicked Source={0} OriginalSource={1}", e.Source, e.OriginalSource));
		//    if (e.MouseButton.LeftButton == MouseButtonState.Pressed)
		//    {
		//        Curve curve = findParentCurve(e.OriginalSource as DependencyObject);
		//        if (curve != null)
		//            SelectedItem = curve.CurveData;
		//    }
		//}

		/// <summary>
		/// Finds the curve which is the parent of DependencyObject given.
		/// </summary>
		/// <param name="d">The DependencyObject.</param>
		/// <returns></returns>
		//private static Curve findParentCurve(DependencyObject d)
		//{
		//    if (d == null)
		//        return null;

		//    Curve curve = d as Curve;
		//    if (curve != null)
		//        return curve;

		//    return findParentCurve(VisualTreeHelper.GetParent(d));
		//}
	}
}
