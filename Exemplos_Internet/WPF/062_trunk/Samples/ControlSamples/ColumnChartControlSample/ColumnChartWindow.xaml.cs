// <copyright file="ColumnChartWindow.xaml.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-10</date>
// <summary>OpenWPFChart library. ColumnChart Control use Sample.</summary>
// <revision>$Id: ColumnChartWindow.xaml.cs 19488 2009-03-23 08:40:46Z unknown $</revision>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32; // For OpenFileDialog and SaveFileDialog
using OpenWPFChart.Helpers;
using OpenWPFChart.Parts;

namespace ColumnChartControlSample
{
	/// <summary>
	/// ColumnChart Control use Sample main Window.
	/// </summary>
	public partial class ColumnChartWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnChartWindow"/> class.
		/// </summary>
		public ColumnChartWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. 
		/// This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> 
		/// is set to true internally.
		/// </summary>
		/// <remarks>
		/// Loads the settings.
		/// </remarks>
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
					MenuItem item = new MenuItem() { Header = fileName };
					item.Click += mru_Click;
					mnuRecentFiles.Items.Add(item);
				}
			}
			mnuViewShowHideCurves.IsChecked = Properties.Settings.Default.ShowCurves;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Window.Closed"/> event.
		/// </summary>
		/// <remarks>
		/// Saves the settings.
		/// </remarks>
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
			Properties.Settings.Default.ShowCurves = mnuViewShowHideCurves.IsChecked;

			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Loads ItemData from the file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <exception cref="Exception">Misc IO and file parser exceptions.</exception>
		private bool loadItemData(string fileName)
		{
			// Parse the Data File.
			string xAxisLabel = null;
			ObservableCollection<ItemData> dataCollection = null;
			FileInfo fi = new FileInfo(fileName);
			try
			{
				if (fi.Extension.ToLower() == ".txt")
					dataCollection = SpaceSeparatedDataFileParser.Parse(fileName, out xAxisLabel);
				else if (fi.Extension.ToLower() == ".xml")
					dataCollection = XMLDataFileParser.Parse(fileName, out xAxisLabel);

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

			// Clear Chart Data Source. It's required because otherwise an exception
			// on Chart Scales setting could be thrown.
			chart.ItemsSource = null;

			// Set X-Chart Scale
			double extent = chart.ChartAreaRenderSize.Width > 0 ? chart.ChartAreaRenderSize.Width : ActualWidth;
			if (dataCollection[0] is SampledCurveData<double, double>)
			{
				IEnumerable<DataPoint<double, double>> points = (dataCollection[0] as SampledCurveData<double, double>).Points;
				double xMin = points.First<DataPoint<double, double>>().X;
				double xMax = points.Last<DataPoint<double, double>>().X;
				ChartLinearScale scale = new ChartLinearScale(xMin, xMax, extent);
				double totalColumnWidth = dataCollection.Count * (double)scale.FromPixels(chart.ColumnWidth);
				scale.Start = xMin - totalColumnWidth / 2;
				scale.Stop = xMax + totalColumnWidth / 2;
				chart.HorizontalScale = scale;
			}
			else if (dataCollection[0] is SampledCurveData<DateTime, double>)
			{
				IEnumerable<DataPoint<DateTime, double>> points = (dataCollection[0] as SampledCurveData<DateTime, double>).Points;
				DateTime xMin = points.First<DataPoint<DateTime, double>>().X;
				DateTime xMax = points.Last<DataPoint<DateTime, double>>().X;
				ChartDateTimeScale scale = new ChartDateTimeScale(xMin, xMax, extent);
				scale.Start = (DateTime)scale.FromPixels(-dataCollection.Count * chart.ColumnWidth / 2);
				scale.Stop = (DateTime)scale.FromPixels(scale.ToPixels(xMax) + dataCollection.Count * chart.ColumnWidth / 2);
				chart.HorizontalScale = scale;
			}
			else if (dataCollection[0] is SampledCurveData<object, double>)
			{
				IEnumerable<DataPoint<object, double>> points = (dataCollection[0] as SampledCurveData<object, double>).Points;
				object xMin = points.First<DataPoint<object, double>>().X;
				object xMax = points.Last<DataPoint<object, double>>().X;
				IEnumerable<object> series = from pt in points select pt.X;
				ChartSeriesScale scale = new ChartSeriesScale(series, xMin, xMax, extent);
				scale.Start = scale.FromPixels(-dataCollection.Count * chart.ColumnWidth / 2);
				scale.Stop = scale.FromPixels(scale.ToPixels(xMax) + dataCollection.Count * chart.ColumnWidth / 2);
				chart.HorizontalScale = scale;
			}
			// ScatteredPointsData<,> not supported
			else
			{
				MessageBox.Show(string.Format("There is no appropriate data in the {0} file", fileName)
					, "Open Data File");
				return false;
			}

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
				// ScatteredPointsData<,> not supported
				else
					continue;

				double min = ordinates.Min();
				if (min < yMin)
					yMin = min;
				double max = ordinates.Max();
				if (max > yMax)
					yMax = max;
			}

			// Chart VerticalScale.
			chart.VerticalScale = new ChartLinearScale(yMax, yMin
				, chart.ChartAreaRenderSize.Height > 0 ? chart.ChartAreaRenderSize.Height : ActualHeight);

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
					ColumnChartItemDataView dataView = new ColumnChartItemDataView()
					{
						ItemData = dataCollection[i],
						HorizontalScale = chart.HorizontalScale,
						VerticalScale = chart.VerticalScale,
						IsCurveVisible = mnuViewShowHideCurves.IsChecked,
						PointMarker = pointMarker
					};

					dataViewCollection.Add(dataView);
				}
				// ScatteredPointsData<,> not supported
			}

			// Set Chart Data Source.
			chart.ItemsSource = dataViewCollection;
			// Set Horizontal Axis label.
			chart.HorizontalAxisTitle = xAxisLabel;

			// Reguery SwapLinLogScaleCommand
			((SwapLinLogScaleCommandImpl)SwapLinLogScaleCommand).NotifyChanged();

			return true;
		}

		#region Swap Lin/Log Scale Command
		/// <summary>
		/// Command to Swap Linear/Logarithmic Axis.
		/// </summary>
		class SwapLinLogScaleCommandImpl : ICommand
		{
			ColumnChartWindow owner;

			/// <summary>
			/// Initializes a new instance of the <see cref="SwapLinLogScaleCommandImpl"/> class.
			/// </summary>
			/// <param name="owner">The owner.</param>
			public SwapLinLogScaleCommandImpl(ColumnChartWindow owner)
			{
				this.owner = owner;
			}

			#region ICommand inplementation
			/// <summary>
			/// Defines the method to be called when the command is invoked.
			/// </summary>
			/// <param name="parameter">string "Vertical" for the Vertical axis; whatever else for Horizontal axis.</param>
			public void Execute(object parameter)
			{
				bool isVertical = parameter != null && parameter is string
					&& (string)parameter == "Vertical";

				if (isVertical)
					owner.chart.VerticalScale = alternativeScale(owner.chart.VerticalScale, Extent.Height);
				else
					owner.chart.HorizontalScale = alternativeScale(owner.chart.HorizontalScale, Extent.Width);
			}

			/// <summary>
			/// Defines the method that determines whether the command can execute in its current state.
			/// </summary>
			/// <param name="parameter">string "Vertical" for the Vertical axis; whatever else for Horizontal axis.</param>
			/// <returns>
			/// <c>true</c> if this command can be executed; otherwise, <c>false</c>.
			/// </returns>
			public bool CanExecute(object parameter)
			{
				bool isVertical = parameter != null && parameter is string
					&& (string)parameter == "Vertical";

				ChartScale scale = null;
				double extent;
				if (isVertical)
				{
					scale = owner.chart.VerticalScale;
					extent = Extent.Height;
				}
				else
				{
					scale = owner.chart.HorizontalScale;
					extent = Extent.Width;
				}
				return alternativeScale(scale, extent) != null ? true : false;
			}

			public event EventHandler CanExecuteChanged;
			public void NotifyChanged()
			{
				if (CanExecuteChanged != null)
					CanExecuteChanged(this, EventArgs.Empty);
			}
			#endregion ICommand inplementation

			/// <summary>
			/// Gets given Scale alternative.
			/// </summary>
			/// <param name="scale">The scale.</param>
			/// <param name="extent">The extent.</param>
			/// <returns></returns>
			static ChartScale alternativeScale(ChartScale scale, double extent)
			{
				ChartScale newScale = null;
				if (scale is ChartLinearScale)
					newScale = new ChartLogarithmicScale(Convert.ToDouble(scale.Start)
						, Convert.ToDouble(scale.Stop), extent);
				else if (scale is ChartLogarithmicScale)
					newScale = new ChartLinearScale(Convert.ToDouble(scale.Start)
						, Convert.ToDouble(scale.Stop), extent);

				return (newScale != null && newScale.IsConsistent) ? newScale : null;
			}

			/// <summary>
			/// Gets the scale extent in pixels.
			/// </summary>
			Size Extent
			{
				get
				{
					return owner.chart.ChartAreaRenderSize.IsEmpty
						? owner.RenderSize : owner.chart.ChartAreaRenderSize;
				}
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
					swapScaleCommand = new SwapLinLogScaleCommandImpl(this);
				return swapScaleCommand;
			}
		}
		#endregion Swap Lin/Log Scale Command

		#region OpenWPFChart.Item element events handlers
		/// <summary>
		/// Handles the MouseEnterItem event of the OpenWPFChart.Item element.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OpenWPFChart.MouseItemEventArgs"/> 
		/// instance containing the event data.</param>
		private void chartItem_EnterItem(object sender, MouseItemEventArgs e)
		{
			Cursor = Cursors.Hand;
		}

		/// <summary>
		/// Handles the MouseLeaveItem event of the OpenWPFChart.Item element.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OpenWPFChart.MouseItemEventArgs"/> 
		/// instance containing the event data.</param>
		private void chartItem_LeaveItem(object sender, MouseItemEventArgs e)
		{
			Cursor = null;
		}
		#endregion OpenWPFChart.Item element events handlers

		#region Menu actions handlers
		/// <summary>
		/// Handles the Click event of the MRU menu subitems.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mru_Click(Object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			if (item == null)
				return;
			string fileName = item.Header as string;
			if (!loadItemData(fileName))
				// Remove from MRU list
				mnuRecentFiles.Items.Remove(item);
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
		/// Sets the VisualCue property on all Items.
		/// </summary>
		/// <param name="cue">The cue.</param>
		private void setVisualCue(object cue)
		{
			foreach (object objItem in chart.Items)
			{
				ItemDataView itemDataView = objItem as ItemDataView;
				if (itemDataView == null)
				{
					Item item = objItem as Item;
					if (item != null)
						itemDataView = item.ItemDataView;
				}
				if (itemDataView != null)
					itemDataView.VisualCue = cue;
			}
		}

		/// <summary>
		/// Handles the Click event of the mnuViewShowHideCurves menu item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> 
		/// instance containing the event data.</param>
		private void mnuViewShowHideCurves_Click(object sender, RoutedEventArgs e)
		{
			if (chart == null)
				return;

			foreach (object obj in chart.Items)
			{
				ColumnChartItemDataView dataView = obj as ColumnChartItemDataView;
				if (dataView != null)
					dataView.IsCurveVisible = mnuViewShowHideCurves.IsChecked;
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
				axis = chart.HorizontalAxis;
			else if (sender == vAxisPropertiesMnuItem)
				axis = chart.VerticalAxis;
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
			Axis.SetTickLength(chart, axisData.TickLength);
			Axis.SetLongTickLength(chart, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(chart, axisData.LabelMargin);
			Axis.SetPen(chart, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(chart, axisData.FontFamily);
			Axis.SetFontSize(chart, axisData.FontSize);
			Axis.SetFontStretch(chart, axisData.FontStretch);
			Axis.SetFontStyle(chart, axisData.FontStyle);
			Axis.SetFontWeight(chart, axisData.FontWeight);
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
			Axis.SetTickLength(chart, axisData.TickLength);
			Axis.SetLongTickLength(chart, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(chart, axisData.LabelMargin);
			Axis.SetPen(chart, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(chart, axisData.FontFamily);
			Axis.SetFontSize(chart, axisData.FontSize);
			Axis.SetFontStretch(chart, axisData.FontStretch);
			Axis.SetFontStyle(chart, axisData.FontStyle);
			Axis.SetFontWeight(chart, axisData.FontWeight);
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
			Axis.SetTickLength(chart, axisData.TickLength);
			Axis.SetLongTickLength(chart, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(chart, axisData.LabelMargin);
			Axis.SetPen(chart, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(chart, axisData.FontFamily);
			Axis.SetFontSize(chart, axisData.FontSize);
			Axis.SetFontStretch(chart, axisData.FontStretch);
			Axis.SetFontStyle(chart, axisData.FontStyle);
			Axis.SetFontWeight(chart, axisData.FontWeight);
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
			Axis.SetTickLength(chart, axisData.TickLength);
			Axis.SetLongTickLength(chart, axisData.LongTickLength);
			axis.LabelFormat = axisData.LabelFormat;
			Axis.SetLabelMargin(chart, axisData.LabelMargin);
			Axis.SetPen(chart, new Pen()
			{
				Brush = new SolidColorBrush(axisData.PenColor),
				Thickness = axisData.PenThickness
			});
			Axis.SetFontFamily(chart, axisData.FontFamily);
			Axis.SetFontSize(chart, axisData.FontSize);
			Axis.SetFontStretch(chart, axisData.FontStretch);
			Axis.SetFontStyle(chart, axisData.FontStyle);
			Axis.SetFontWeight(chart, axisData.FontWeight);
			axis.EndInit();
		}
		#endregion Axis Properties

		/// <summary>
		/// Handles the Click event of the Exit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		#endregion Menu actions handlers

		#region Command Handlers
		/// <summary>
		/// Execute <see cref="ApplicationCommands.Open"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
		private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
			if (dlg.ShowDialog() == true)
			{
				if (loadItemData(dlg.FileName))
				{
					// Add to MRU list
					foreach (MenuItem item in mnuRecentFiles.Items)
					{
						if (dlg.FileName == item.Header as string)
							return;
					}

					MenuItem newItem = new MenuItem() { Header = dlg.FileName };
					newItem.Click += mru_Click;
					mnuRecentFiles.Items.Add(newItem);
				}
			}
		}

		#region Properties command handlers
		/// <summary>
		/// Executes <see cref="ApplicationCommands.Properties"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
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
			{ // Show ItemVisual Properties dialog.
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
					chart.HorizontalScale = hScale;
					if ((hScale.GetType() == typeof(ChartLinearScale)
							&& dlg.HorizontalScaleVeriety == ChartScaleVerieties.Logarithmic)
						|| (hScale.GetType() == typeof(ChartLogarithmicScale)
							&& dlg.HorizontalScaleVeriety == ChartScaleVerieties.Linear))
					{
						SwapLinLogScaleCommand.Execute(null);
					}
					#endregion HorizontalScale

					#region VerticalScale
					ChartScale vScale = itemDataView.VerticalScale;
					vScale.Start = dlg.VerticalScaleStart;
					vScale.Stop = dlg.VerticalScaleStop;
					vScale.Scale = dlg.VerticalScaleScale;
					chart.VerticalScale = vScale;
					if ((vScale.GetType() == typeof(ChartLinearScale)
							&& dlg.VerticalScaleVeriety == ChartScaleVerieties.Logarithmic)
						|| (vScale.GetType() == typeof(ChartLogarithmicScale)
							&& dlg.VerticalScaleVeriety == ChartScaleVerieties.Linear))
					{
						SwapLinLogScaleCommand.Execute("Vertical");
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
			{ // Show ChartPointVisual Properties dialog.
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

		/// <summary>
		/// Checks the <see cref="ApplicationCommands.Properties"/> command can be executed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
		private void PropertiesCanExecuteCommandHandler(object sender, CanExecuteRoutedEventArgs e)
		{
			ListBoxItem listBoxItem = e.OriginalSource as ListBoxItem;
			if (listBoxItem != null)
			{
				ItemDataView itemDataView = listBoxItem.DataContext as ItemDataView;
				if (itemDataView != null)
				{
					e.CanExecute = true;
					return;
				}
			}
			e.CanExecute = false;
		}
		#endregion Properties command handlers

		/// <summary>
		/// Execute <see cref="NavigationCommands.Zoom"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
		private void ZoomCommandHandler(object sender, ExecutedRoutedEventArgs e)
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

			chart.HorizontalScale.Scale *= zoom;
			chart.VerticalScale.Scale *= zoom;
		}
		#endregion Command Handlers
	}
}
