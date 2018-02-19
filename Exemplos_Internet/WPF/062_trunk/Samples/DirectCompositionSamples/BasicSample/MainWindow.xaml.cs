// <copyright file="MainWindow.xaml.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library.
// Basic Sample: Chart with two axes is composed in MainWindow.
// </summary>
// <revision>$Id: MainWindow.xaml.cs 19488 2009-03-23 08:40:46Z unknown $</revision>

using System;
using System.Collections; // for ArrayList
using System.Collections.Generic;
using System.Collections.ObjectModel; // For ObservableCollection
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32; // For OpenFileDialog and SaveFileDialog
using OpenWPFChart.Helpers;
using OpenWPFChart.Parts;

namespace BasicSample
{
	/// <summary>
	/// Basic Sample main Window.
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. 
		/// This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> 
		/// is set to true internally.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> 
		/// that contains the event data.</param>
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			// Load MRU files list
			ArrayList mru = Properties.Settings.Default.MRU;
			if (mru != null)
			{
				foreach (string fileName in mru)
				{
					MenuItem item = new MenuItem(){ Header = fileName };
					item.Click += mnuFileMRU_Click;
					mnuRecentFiles.Items.Add(item);
				}
			}
		}

		/// <summary>
		/// Saves the settings.
		/// Raises the <see cref="E:System.Windows.Window.Closed"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			// MRU files list.
			ArrayList mru = null;
			if (mnuRecentFiles.Items.Count > 0)
			{
				mru = new ArrayList();
				foreach (MenuItem item in mnuRecentFiles.Items)
				{
					mru.Add(item.Header as string);
				}
			}
			Properties.Settings.Default.MRU = mru;

			Properties.Settings.Default.Save();
		}

		#region Properties
		#region Dependency Properties
		#region HorizontalScale
		/// <summary>
		/// Identifies the HorizontalScale dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalScaleProperty
			= DependencyProperty.Register("HorizontalScale", typeof(ChartScale), typeof(MainWindow)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets the HorizontalScale property.
		/// </summary>
		/// <value/>
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
			= DependencyProperty.Register("VerticalScale", typeof(ChartScale), typeof(MainWindow)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets the VerticalScale property.
		/// </summary>
		/// <value/>
		public ChartScale VerticalScale
		{
			get { return (ChartScale)GetValue(VerticalScaleProperty); }
			set { SetValue(VerticalScaleProperty, value); }
		}
		#endregion VerticalScale
		#endregion Dependency Properties

		#region Axes
		/// <summary>
		/// Gets the horizontal axis.
		/// </summary>
		/// <value>The horizontal axis.</value>
		Axis HorizontalAxis
		{
			get { return findAxis(hAxisHost); }
		}

		/// <summary>
		/// Gets the vertical axis.
		/// </summary>
		/// <value>The vertical axis.</value>
		Axis VerticalAxis
		{
			get { return findAxis(vAxisHost); }
		}

		/// <summary>
		/// Finds an axis in its container Visual Tree.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <returns></returns>
		Axis findAxis(DependencyObject parent)
		{
			Axis axis = parent as Axis;
			if (axis != null)
				return axis;
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
			{
				DependencyObject item = VisualTreeHelper.GetChild(parent, i);
				axis = findAxis(item);
				if (axis != null)
					return axis;
			}
			return null;
		}
		#endregion Axes
		#endregion Properties

		#region Scales Manipulations
		#region Swap Lin/Log Scale Command
		/// <summary>
		/// Command to Swap Linear/Logarithmic Axis.
		/// </summary>
		class SwapLinLogScaleCommandClass : ICommand
		{
			public delegate void ExecuteDelegate(object parameter);
			public delegate bool CanExecuteDelegate(object parameter);

			ExecuteDelegate execute;
			CanExecuteDelegate canExecute;

			public SwapLinLogScaleCommandClass(ExecuteDelegate executeDelegate
				, CanExecuteDelegate canExecuteDelegate)
			{
				execute = executeDelegate;
				canExecute = canExecuteDelegate;
			}

			public void Execute(object parameter)
			{
				execute(parameter);
			}

			public bool CanExecute(object parameter)
			{
				return canExecute(parameter);
			}

			public event EventHandler CanExecuteChanged;
			public void NotifyChanged()
			{
				if (CanExecuteChanged != null)
					CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		ICommand swapScaleCommand;
		/// <summary>
		/// Gets the SwapLinLogScaleCommand.
		/// </summary>
		/// <value/>
		public ICommand SwapLinLogScaleCommand
		{
			get
			{
				if (swapScaleCommand == null)
				{
					swapScaleCommand 
						= new SwapLinLogScaleCommandClass(SwapScale, CanSwapScale);
				}
				return swapScaleCommand;
			}
		}
		/// <summary>
		/// SwapLinLogScaleCommandClass CanExecute handler.
		/// </summary>
		/// <param name="parameter">string "Vertical" for the Vertical axis; whatever els for Horizontal axis.</param>
		/// <returns>
		/// 	<c>true</c> if CanExecute; otherwise, <c>false</c>.
		/// </returns>
		bool CanSwapScale(object parameter)
		{
			bool isVertical = parameter != null && parameter is string 
				&& (string)parameter == "Vertical";
			ChartScale scale = null;
			double extent;
			if (isVertical)
			{
				scale = VerticalScale;
				extent = chart.ActualHeight > 0 ? chart.ActualHeight : ActualHeight;
			}
			else
			{
				scale = HorizontalScale;
				extent = chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth;
			}
			ChartScale newScale = alternativeScale(scale, extent);
			return (newScale != null && newScale.IsConsistent) ? true : false;
		}
		/// <summary>
		/// SwapLinLogScaleCommandClass Execute handler.
		/// </summary>
		/// <param name="parameter">string "Vertical" for the Vertical axis; whatever els for Horizontal axis.</param>
		void SwapScale(object parameter)
		{
			bool isVertical = parameter != null && parameter is string 
				&& (string)parameter == "Vertical";
			if (isVertical)
				SwapVerticalScale(VerticalScale);
			else
				SwapHoirizontalScale(HorizontalScale);
		}

		void SwapVerticalScale(ChartScale scaleToSwap)
		{
			double extent = chart.ActualHeight > 0 ? chart.ActualHeight : ActualHeight;
			ChartScale scale = alternativeScale(scaleToSwap, extent);
			foreach (var obj in chart.Items)
			{
				ItemDataView itemDataView = obj as ItemDataView;
				if (itemDataView == null)
				{
					Item item = obj as Item;
					if (item != null)
						itemDataView = item.ItemDataView;
				}
				if (itemDataView != null)
					itemDataView.VerticalScale = scale;
			}
			VerticalScale = scale;
		}

		void SwapHoirizontalScale(ChartScale scaleToSwap)
		{
			double extent = chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth;
			ChartScale scale = alternativeScale(scaleToSwap, extent);
			foreach (var obj in chart.Items)
			{
				ItemDataView itemDataView = obj as ItemDataView;
				if (itemDataView == null)
				{
					Item item = obj as Item;
					if (item != null)
						itemDataView = item.ItemDataView;
				}
				if (itemDataView != null)
					itemDataView.HorizontalScale = scale;
			}
			HorizontalScale = scale;
		}

		#endregion Swap Lin/Log Scale Command

		/// <summary>
		/// Gets given Scale alternative.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="extent">The extent.</param>
		/// <returns></returns>
		ChartScale alternativeScale(ChartScale scale, double extent)
		{
			if (scale is ChartLinearScale)
				return alternativeScale(scale as ChartLinearScale, extent);
			else if (scale is ChartLogarithmicScale)
				return alternativeScale(scale as ChartLogarithmicScale, extent);
			return null;
		}

		/// <summary>
		/// Gets given ChartLinearScale alternative.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="extent">The extent.</param>
		/// <returns></returns>
		ChartScale alternativeScale(ChartLinearScale scale, double extent)
		{
			ChartLogarithmicScale newScale = new ChartLogarithmicScale(Convert.ToDouble(scale.Start)
				, Convert.ToDouble(scale.Stop), extent);
			return newScale.IsConsistent ? newScale : null;
		}

		/// <summary>
		/// Gets given ChartLogarithmicScale alternative.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="extent">The extent.</param>
		/// <returns></returns>
		ChartScale alternativeScale(ChartLogarithmicScale scale, double extent)
		{
			ChartLinearScale newScale = new ChartLinearScale(Convert.ToDouble(scale.Start)
				, Convert.ToDouble(scale.Stop), extent);
			return newScale.IsConsistent ? newScale : null;
		}
		#endregion Scales Manipulations

		/// <summary>
		/// Loads CurveData from the file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns><c>true</c> if data loaded; otherwise <c>false</c>.</returns>
		/// <exception cref="Exception">Misc IO and file parser exceptions.</exception>
		bool loadCurveData(string fileName)
		{
			ObservableCollection<ItemData> dataCollection = null;
			FileInfo fi = new FileInfo(fileName);
			try
			{
				if (fi.Extension.ToLower() == ".txt")
					dataCollection = SpaceSeparatedDataFileParser.Parse(fileName);
				else if (fi.Extension.ToLower() == ".xml")
					dataCollection = XMLDataFileParser.Parse(fileName);

				if (dataCollection == null || dataCollection.Count == 0)
				{
					MessageBox.Show("Data wasn't loaded");
					return false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}

			// Set X-Chart Scale
			ChartScale horizontalScale = null;
			if (dataCollection[0] is SampledCurveData<double, double>)
			{
				IEnumerable<DataPoint<double, double>> points = (dataCollection[0] as SampledCurveData<double, double>).Points;
				double xMin = points.First<DataPoint<double, double>>().X;
				double xMax = points.Last<DataPoint<double, double>>().X;
				horizontalScale = new ChartLinearScale(xMin, xMax, chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth);
			}
			else if (dataCollection[0] is SampledCurveData<DateTime, double>)
			{
				IEnumerable<DataPoint<DateTime, double>> points = (dataCollection[0] as SampledCurveData<DateTime, double>).Points;
				DateTime xMin = points.First<DataPoint<DateTime, double>>().X;
				DateTime xMax = points.Last<DataPoint<DateTime, double>>().X;
				horizontalScale = new ChartDateTimeScale(xMin, xMax, chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth);
			}
			else if (dataCollection[0] is SampledCurveData<object, double>)
			{
				IEnumerable<DataPoint<object, double>> points = (dataCollection[0] as SampledCurveData<object, double>).Points;
				object xMin = points.First<DataPoint<object, double>>().X;
				object xMax = points.Last<DataPoint<object, double>>().X;
				horizontalScale = new ChartSeriesScale(from pt in points select pt.X
					, xMin, xMax, chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth);
			}
			else if (dataCollection[0] is ScatteredPointsData<double, double>)
			{
				IEnumerable<DataPoint<double, double>> points = (dataCollection[0] as ScatteredPointsData<double, double>).Points;
				object xMin = points.First<DataPoint<double, double>>().X;
				object xMax = points.Last<DataPoint<double, double>>().X;
				horizontalScale = new ChartLinearScale(xMin, xMax, chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth);
			}
			else
			{
				MessageBox.Show(string.Format("There is no appropriate data in the {0} file", fileName)
					, "Open Data File");
				return false;
			}

			if (!horizontalScale.IsConsistent)
				throw new ArgumentException(string.Format("Invalid X scale in {0} file", fileName));
			HorizontalScale = horizontalScale;

			// Set Y-Chart Scale
			double yMax = double.MinValue, yMin = double.MaxValue;
			for (int i = 0; i < dataCollection.Count; ++i)
			{
				IEnumerable<double> ordinates = null;
				if (dataCollection[i] is SampledCurveData<double, double>)
					ordinates = from pt in (dataCollection[i] as SampledCurveData<double, double>).Points select pt.Y;
				else if (dataCollection[i] is SampledCurveData<DateTime, double>)
					ordinates = from pt in (dataCollection[i] as SampledCurveData<DateTime, double>).Points select pt.Y;
				else if (dataCollection[0] is SampledCurveData<object, double>)
					ordinates = from pt in (dataCollection[i] as SampledCurveData<object, double>).Points select pt.Y;
				else if (dataCollection[0] is ScatteredPointsData<double, double>)
					ordinates = from pt in (dataCollection[i] as ScatteredPointsData<double, double>).Points select pt.Y;
				else
					continue;
				
				double min = ordinates.Min();
				if (min < yMin)
					yMin = min;
				double max = ordinates.Max();
				if (max > yMax)
					yMax = max;
			}

			ChartScale verticalScale = new ChartLinearScale(yMax, yMin
				, chart.ActualHeight > 0 ? chart.ActualHeight : ActualHeight);
			if (!verticalScale.IsConsistent)
				throw new ArgumentException(string.Format("Invalid Y scale in {0} file", fileName));
			VerticalScale = verticalScale;

			// PointMarker
			GeometryDrawing pointMarker = new GeometryDrawing(Brushes.Brown, new Pen(Brushes.Red, 1)
				, new EllipseGeometry(new Point(0, 0), 4, 4));
			pointMarker.Freeze();

			// Create DataViewCollection.
			ObservableCollection<ItemDataView> dataViewCollection = new ObservableCollection<ItemDataView>();
			for (int i = 0; i < dataCollection.Count; ++i)
			{
				if (dataCollection[i] is SampledCurveData<double, double>
					|| dataCollection[i] is SampledCurveData<DateTime, double>
					|| dataCollection[i] is SampledCurveData<object, double>)
				{
					SampledCurveDataView dataView = new SampledCurveDataView()
					{
						ItemData = dataCollection[i],
						HorizontalScale = this.HorizontalScale,
						VerticalScale = this.VerticalScale,
						PointMarker = pointMarker
					};

					dataViewCollection.Add(dataView);
				}
				else if (dataCollection[i] is ScatteredPointsData<double, double>)
				{
					ScatteredPointsDataView dataView = new ScatteredPointsDataView()
					{
						ItemData = dataCollection[i],
						HorizontalScale = this.HorizontalScale,
						VerticalScale = this.VerticalScale,
						PointMarker = pointMarker
					};

					dataViewCollection.Add(dataView);
				}
			}

			DataContext = dataViewCollection;

			(SwapLinLogScaleCommand as SwapLinLogScaleCommandClass).NotifyChanged();

			return true;
		}

		#region Chart Events Handlers
		/// <summary>
		/// Handles the SelectionChanged event of the ListBox (chart) control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void chart_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			Trace.WriteLine(string.Format("***** MainWindow.chart_SelectionChanged Source={0} OriginalSource={0}", e.Source, e.OriginalSource));
		}

		/// <summary>
		/// Handles the MouseEnterItem event of the OpenWPFChart.Item element.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OpenWPFChart.MouseItemEventArgs"/> instance containing the event data.</param>
		private void item_Enter(object sender, MouseItemEventArgs e)
		{
			Cursor = Cursors.Hand;
		}

		/// <summary>
		/// Handles the MouseLeaveItem event of the OpenWPFChart.Item element.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OpenWPFChart.MouseItemEventArgs"/> instance containing the event data.</param>
		private void item_Leave(object sender, MouseItemEventArgs e)
		{
			Cursor = null;
		}

		/// <summary>
		/// Handles the VisualCueChanged event of the OpenWPFChart.Item element.
		/// <para>Resets the Chart ItemTemplateSelector.</para>
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void item_VisualCueChanged(object sender, RoutedEventArgs e)
		{
			DataTemplateSelector old = chart.ItemTemplateSelector;
			chart.ItemTemplateSelector = null;
			chart.ItemTemplateSelector = old;
		}
		#endregion Chart Events Handlers

		#region Menu actions handlers
		/// <summary>
		/// Handles the Click event of the MRU menu subitems.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuFileMRU_Click(Object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			if (item == null)
				return;
			string fileName = item.Header as string;
			if (!loadCurveData(fileName))
			{ // Remove from MRU list
				mnuRecentFiles.Items.Remove(item);
			}
		}

		/// <summary>
		/// Prints the "panel".
		/// Handles the Click event of the mnuFilePrint item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuFilePrint_Click(object sender, RoutedEventArgs e)
		{
			//PrintDocumentImageableArea area = null;
			//XpsDocumentWriter xpsdw = PrintQueue.CreateXpsDocumentWriter(ref area);
			//if (xpsdw != null)
			//{
			//    xpsdw.Write(panel);
			//}

			PrintDialog dlg = new System.Windows.Controls.PrintDialog();
			if (dlg.ShowDialog() == true)
			{
				// Get selected printer capabilities.
				PrintCapabilities capabilities = dlg.PrintQueue.GetPrintCapabilities(dlg.PrintTicket);

				// Get scale of the print wrt to screen of Chart.
				double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / panel.ActualWidth
					, capabilities.PageImageableArea.ExtentHeight / panel.ActualHeight);

				// Save old transform.
				Transform savedTransform = panel.LayoutTransform;
				// Scale the Chart.
				panel.LayoutTransform = new ScaleTransform(scale, scale);

				// Get the size of the printer page.
				Size sz = new Size(capabilities.PageImageableArea.ExtentWidth
					, capabilities.PageImageableArea.ExtentHeight);

				// Update the layout of the Chart to the printer page size.
				panel.Measure(sz);
				panel.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth
					, capabilities.PageImageableArea.OriginHeight), sz));

				// Print the Chart to printer to fit on the one page.
				dlg.PrintVisual(panel, "Print the Chart");
				
				// Restore old transform.
				panel.LayoutTransform = savedTransform;
			}
		}

		/// <summary>
		/// Handles the Click event of the Exit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuFileExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Handles the Click event of the mnuViewInterpolator items.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuViewInterpolator_Click(object sender, RoutedEventArgs e)
		{
			MenuItem mnuItem = sender as MenuItem;
			if (mnuItem == null)
				return;

			switch (mnuItem.Header.ToString())
			{
				case "_Polyline":
					setVisualCue(typeof(PolylineSampledCurve));
					break;
				case "_Bezier":
					setVisualCue(typeof(BezierSampledCurve));
					break;
				case "_Spline":
					setVisualCue(typeof(SplineSampledCurve));
					break;
			}
		}

		/// <summary>
		/// Sets the VisualCue property on all ItemDataView items of SampledCurveDataView type.
		/// </summary>
		/// <param name="cue">The cue.</param>
		private void setVisualCue(object cue)
		{
			ObservableCollection<ItemDataView> dataViewCollection = DataContext as ObservableCollection<ItemDataView>;
			if (dataViewCollection == null)
				return;
			foreach (ItemDataView item in dataViewCollection)
			{
				SampledCurveDataView sampledCurveDataView = item as SampledCurveDataView;
				if (sampledCurveDataView != null)
					sampledCurveDataView.VisualCue = cue;
			}
		}

		#region Axis Properties
		/// <summary>
		/// Handles the Click event of the AxisProperties menu items.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuViewAxisProperties_Click(object sender, RoutedEventArgs e)
		{
			Axis axis = null;
			if (sender == hAxisPropertiesMnuItem)
				axis = HorizontalAxis;
			else if (sender == vAxisPropertiesMnuItem)
				axis = VerticalAxis;
			else
				return;
			if (axis == null)
				return;

			if (axis is LinearAxis)
				setAxisProperties(axis as LinearAxis);
			else if (axis is LogarithmicAxis)
				setAxisProperties(axis as LogarithmicAxis);
			else if (axis is DateTimeAxis)
				setAxisProperties(axis as DateTimeAxis);
			else if (axis is SeriesAxis)
				setAxisProperties(axis as SeriesAxis);
		}

		/// <summary>
		/// Show LinearAxisProperties dialog.
		/// </summary>
		/// <param name="axis">The axis.</param>
		void setAxisProperties(LinearAxis axis)
		{
			AxisPropertiesDialog dlg = new AxisPropertiesDialog(axis) { Owner = this };
			if (dlg.ShowDialog() != true)
				return;

			LinearAxisDataView axisData = dlg.AxisData as LinearAxisDataView;
			axis.BeginInit();
			axis.AxisScale.Start = dlg.Start;
			axis.AxisScale.Stop = dlg.Stop;
			axis.AxisScale.Scale = dlg.Scale;
			axis.ContentLayout = axisData.ContentLayout;
			ChartLinearScale linearScale = axis.AxisScale as ChartLinearScale;
			linearScale.TickStep = axisData.TickStep;
			linearScale.LongTickAnchor = axisData.LongTickAnchor;
			linearScale.LongTickRate = axisData.LongTickRate;
			Axis.SetTickLength(panel, axisData.TickLength);
			Axis.SetLongTickLength(panel, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(panel, axisData.LabelMargin);
			Axis.SetPen(panel, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(panel, axisData.FontFamily);
			Axis.SetFontSize(panel, axisData.FontSize);
			Axis.SetFontStretch(panel, axisData.FontStretch);
			Axis.SetFontStyle(panel, axisData.FontStyle);
			Axis.SetFontWeight(panel, axisData.FontWeight);
			axis.EndInit();
		}

		/// <summary>
		/// Show LogarithmicAxisProperties dialog.
		/// </summary>
		/// <param name="axis">The axis.</param>
		void setAxisProperties(LogarithmicAxis axis)
		{
			AxisPropertiesDialog dlg = new AxisPropertiesDialog(axis) { Owner = this };
			if (dlg.ShowDialog() != true)
				return;

			LogarithmicAxisDataView axisData = dlg.AxisData as LogarithmicAxisDataView;
			axis.BeginInit();
			axis.AxisScale.Start = dlg.Start;
			axis.AxisScale.Stop = dlg.Stop;
			axis.AxisScale.Scale = dlg.Scale;
			axis.ContentLayout = axisData.ContentLayout;
			ChartLogarithmicScale linearScale = axis.AxisScale as ChartLogarithmicScale;
			linearScale.TickMask = axisData.TickMask;
			Axis.SetTickLength(panel, axisData.TickLength);
			Axis.SetLongTickLength(panel, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(panel, axisData.LabelMargin);
			Axis.SetPen(panel, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(panel, axisData.FontFamily);
			Axis.SetFontSize(panel, axisData.FontSize);
			Axis.SetFontStretch(panel, axisData.FontStretch);
			Axis.SetFontStyle(panel, axisData.FontStyle);
			Axis.SetFontWeight(panel, axisData.FontWeight);
			axis.EndInit();
		}

		/// <summary>
		/// Show DateTimeAxisProperties dialog.
		/// </summary>
		/// <param name="axis">The axis.</param>
		void setAxisProperties(DateTimeAxis axis)
		{
			AxisPropertiesDialog dlg = new AxisPropertiesDialog(axis) { Owner = this };
			if (dlg.ShowDialog() != true)
				return;

			DateTimeAxisDataView axisData = dlg.AxisData as DateTimeAxisDataView;
			axis.BeginInit();
			axis.AxisScale.Start = dlg.Start;
			axis.AxisScale.Stop = dlg.Stop;
			axis.AxisScale.Scale = dlg.Scale;
			axis.ContentLayout = axisData.ContentLayout;
			ChartDateTimeScale linearScale = axis.AxisScale as ChartDateTimeScale;
			linearScale.TickUnits = axisData.TickUnits;
			linearScale.TickStep = axisData.TickStep;
			linearScale.LongTickAnchor = axisData.LongTickAnchor;
			linearScale.LongTickRate = axisData.LongTickRate;
			Axis.SetTickLength(panel, axisData.TickLength);
			Axis.SetLongTickLength(panel, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(panel, axisData.LabelMargin);
			Axis.SetPen(panel, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(panel, axisData.FontFamily);
			Axis.SetFontSize(panel, axisData.FontSize);
			Axis.SetFontStretch(panel, axisData.FontStretch);
			Axis.SetFontStyle(panel, axisData.FontStyle);
			Axis.SetFontWeight(panel, axisData.FontWeight);
			axis.EndInit();
		}

		/// <summary>
		/// Show SeriesAxisProperties dialog.
		/// </summary>
		/// <param name="axis">The axis.</param>
		void setAxisProperties(SeriesAxis axis)
		{
			AxisPropertiesDialog dlg = new AxisPropertiesDialog(axis) { Owner = this };
			if (dlg.ShowDialog() != true)
				return;

			SeriesAxisDataView axisData = dlg.AxisData as SeriesAxisDataView;
			axis.BeginInit();
			axis.AxisScale.Start = dlg.Start;
			axis.AxisScale.Stop = dlg.Stop;
			axis.AxisScale.Scale = dlg.Scale;
			axis.ContentLayout = axisData.ContentLayout;
			ChartSeriesScale seriesScale = axis.AxisScale as ChartSeriesScale;
			seriesScale.LongTickAnchor = axisData.LongTickAnchor;
			seriesScale.LongTickRate = axisData.LongTickRate;
			Axis.SetTickLength(panel, axisData.TickLength);
			Axis.SetLongTickLength(panel, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(panel, axisData.LabelMargin);
			Axis.SetPen(panel, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(panel, axisData.FontFamily);
			Axis.SetFontSize(panel, axisData.FontSize);
			Axis.SetFontStretch(panel, axisData.FontStretch);
			Axis.SetFontStyle(panel, axisData.FontStyle);
			Axis.SetFontWeight(panel, axisData.FontWeight);
			axis.EndInit();
		}
		#endregion Axis Properties
		#endregion Menu actions handlers

		#region Command Handlers
		/// <summary>
		/// Execute <see cref="ApplicationCommands.Open"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
		private void OpenExecutedCommandHandler(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
			if (dlg.ShowDialog() == true)
			{
				if (loadCurveData(dlg.FileName))
				{
					// Add to MRU list
					foreach (MenuItem item in mnuRecentFiles.Items)
					{
						if (dlg.FileName == item.Header as string)
							return;
					}

					MenuItem newItem = new MenuItem() { Header = dlg.FileName };
					newItem.Click += mnuFileMRU_Click;
					mnuRecentFiles.Items.Add(newItem);
				}
			}
		}

		/// <summary>
		/// Executes <see cref="NavigationCommands.Zoom"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
		private void ZoomExecutedCommandHandler(object sender, ExecutedRoutedEventArgs e)
		{
			double zoom = 1.2;
			if (e.Parameter != null)
			{
				try
				{
					zoom = Convert.ToDouble(e.Parameter);
				}
				catch (Exception ex)
				{
					Trace.WriteLine("ZoomCommandHandler " + ex.Message);
					return;
				}
			}

			if (zoom == 0.0)
				return;
			if (zoom < 0.0)
				zoom = -1.0 / zoom;

			HorizontalScale.Scale *= zoom;
			VerticalScale.Scale *= zoom;
			foreach (ItemDataView item in chart.Items)
			{
				item.HorizontalScale.Scale *= zoom;
				item.VerticalScale.Scale *= zoom;
			}
		}

		#region Properties command handlers
		/// <summary>
		/// Executes <see cref="ApplicationCommands.Properties"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> 
		///		instance containing the event data.</param>
		void PropertiesExecutedCommandHandler(object sender, ExecutedRoutedEventArgs e)
		{
			ContextMenu mnu = (ContextMenu)FindResource("chartItemCtxMnu");
			Debug.Assert(mnu != null, "mnu != null");
			ListBoxItem listBoxItem = e.OriginalSource as ListBoxItem;
			Debug.Assert(listBoxItem != null, "listBoxItem != null");
			ItemDataView itemDataView = listBoxItem.DataContext as ItemDataView;
			Debug.Assert(itemDataView != null, "itemDataView != null");

			// ContextMenu is assigned in OpenWPFChart.Item object.
			if (mnu.Tag is ItemVisual)
			{ // Show CurveProperties dialog.
				// Get GenericDataTemplateSelectorItems.
				Collection<GenericDataTemplateSelectorItem> templateSelectorItems = null;
				GenericDataTemplateSelector templateSelector = chart.ItemTemplateSelector as GenericDataTemplateSelector;
				if (templateSelector != null)
					templateSelectorItems = templateSelector.SelectorItems;

				// Show the dialog.
				ItemPropertiesDialog dlg = new ItemPropertiesDialog(itemDataView, templateSelectorItems) { Owner = this };
				if (dlg.ShowDialog() == true)
				{
					// Curve Name
					itemDataView.ItemData.ItemName = dlg.ItemName;

					#region HorizontalScale
					ChartScale hScale = itemDataView.HorizontalScale;
					hScale.Start = dlg.HorizontalScaleStart;
					hScale.Stop = dlg.HorizontalScaleStop;
					hScale.Scale = dlg.HorizontalScaleScale;
					if ((hScale.GetType() == typeof(ChartLinearScale)
							&& dlg.HorizontalScaleVeriety == ChartScaleVerieties.Logarithmic)
						|| (hScale.GetType() == typeof(ChartLogarithmicScale)
							&& dlg.HorizontalScaleVeriety == ChartScaleVerieties.Linear))
					{
						SwapHoirizontalScale(hScale);
					}
					#endregion HorizontalScale

					#region VerticalScale
					ChartScale vScale = itemDataView.VerticalScale;
					vScale.Start = dlg.VerticalScaleStart;
					vScale.Stop = dlg.VerticalScaleStop;
					vScale.Scale = dlg.VerticalScaleScale;
					if ((vScale.GetType() == typeof(ChartLinearScale)
							&& dlg.VerticalScaleVeriety == ChartScaleVerieties.Logarithmic)
						|| (vScale.GetType() == typeof(ChartLogarithmicScale)
							&& dlg.VerticalScaleVeriety == ChartScaleVerieties.Linear))
					{
						SwapVerticalScale(vScale);
					}
					#endregion HorizontalScale

					// Curve Color
					CurveDataView curveDataView = itemDataView as CurveDataView;
					if (curveDataView != null)
					{
						Color color = Colors.Black;
						SolidColorBrush solidColorBrush = curveDataView.Pen.Brush as SolidColorBrush;
						if (solidColorBrush != null)
							color = solidColorBrush.Color;
						if (solidColorBrush == null || dlg.CurveColor != color)
							curveDataView.Pen = new Pen(new SolidColorBrush(dlg.CurveColor), 1);
					}

					// PointMarkerVisibility
					IPointMarker iPointMarker = curveDataView as IPointMarker;
					if (iPointMarker != null)
						iPointMarker.PointMarkerVisible = dlg.PointMarkerVisible;

					// VisualCue
					if (dlg.VisualCue != null)
						itemDataView.VisualCue = dlg.VisualCue;
				}
				e.Handled = true;
			}
			else if (mnu.Tag is ChartPointVisual)
			{ // Show ChartPointProperties dialog.
				IPointMarker iPointMarker = itemDataView as IPointMarker;
				if (iPointMarker != null)
				{
					ChartPointPropertiesDialog dlg = new ChartPointPropertiesDialog(iPointMarker.PointMarker) { Owner = this };
					if (dlg.ShowDialog() == true)
					{
						if (dlg.PointMarker != iPointMarker.PointMarker)
						{
							iPointMarker.PointMarker = dlg.PointMarker;
						}
					}
				}
				e.Handled = true;
			}
		}
		#endregion Properties command handlers
		#endregion Command Handlers
	}
}
