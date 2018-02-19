// Copyright © Oleg V. Polikarpotchkin 2008
// <copyright file="MainWindow.xaml.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library.
// Temperature Chart Sample. Chart is composed in MainWindow.
// It has three axis - one X-axis for dates and two Y-axes for temperature in Celsius and Fahrenheit.
// </summary>
// <revision>$Id: MainWindow.xaml.cs 19488 2009-03-23 08:40:46Z unknown $</revision>

using System;
using System.Collections; // for ArrayList
using System.Collections.Generic;
using System.Collections.ObjectModel; // For ObservableCollection
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32; // For OpenFileDialog and SaveFileDialog
using OpenWPFChart.Helpers;
using OpenWPFChart.Parts;

namespace TemperatureSample
{
	/// <summary>
	/// OpenWPFChart library Temperature Chart Sample main Window.
	/// </summary>
	/// <remarks>
	/// Chart is composed in MainWindow. It has three axis - one X-axis for dates and 
	/// two Y-axes for temperature in Celsius and Fahrenheit.
	/// </remarks>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set to true internally.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
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

		#region Chart Events Handlers
		/// <summary>
		/// Handles the SelectionChanged event of the chart control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void chart_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			Trace.WriteLine(string.Format("***** MainWindow.chart_SelectionChanged Source={0} OriginalSource={0}", e.Source, e.OriginalSource));
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

		/// <summary>
		/// Loads CurveData from the file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <exception cref="Exception">Misc IO and file parser exceptions.</exception>
		private bool loadCurveData(string fileName)
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

			// Set Chart Scales
			ChartScale horizontalScale = null, verticalScale = null;
			if (dataCollection[0] is SampledCurveData<double, double>)
			{ // abscissa and ordinate are Numeric.
				SampledCurveData<double, double> xData = dataCollection[0] as SampledCurveData<double, double>;

				// Abscissa Min, Max
				double xMin = xData.Points.First<DataPoint<double, double>>().X;
				double xMax = xData.Points.Last<DataPoint<double, double>>().X;
				// Ordinate Min, Max
				double yMax = double.MinValue, yMin = double.MaxValue;
				for (int i = 0; i < dataCollection.Count; ++i)
				{
					IEnumerable<double> ordinates = from pt in (dataCollection[i] as SampledCurveData<double, double>).Points select pt.Y;
					double min = ordinates.Min();
					if (min < yMin)
						yMin = min;
					double max = ordinates.Max();
					if (max > yMax)
						yMax = max;
				}

				// Attach ChartScales
				// X-scale.
				horizontalScale = new ChartLinearScale(xMin, xMax
					, chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth);
				// Y-scale.
				verticalScale = new ChartLinearScale(yMax, yMin
					, chart.ActualHeight > 0 ? chart.ActualHeight : ActualHeight);
			}
			else if (dataCollection[0] is SampledCurveData<DateTime, double>)
			{ // abscissa is DateTime and ordinate is Numeric.
				SampledCurveData<DateTime, double> xData = dataCollection[0] as SampledCurveData<DateTime, double>;

				// Abscissa Min, Max
				DateTime xMin = xData.Points.First<DataPoint<DateTime, double>>().X;
				DateTime xMax = xData.Points.Last<DataPoint<DateTime, double>>().X;
				// Ordinate Min, Max
				double yMax = double.MinValue, yMin = double.MaxValue;
				for (int i = 0; i < dataCollection.Count; ++i)
				{
					IEnumerable<double> ordinates = from pt in (dataCollection[i] as SampledCurveData<DateTime, double>).Points select pt.Y;
					double min = ordinates.Min();
					if (min < yMin)
						yMin = min;
					double max = ordinates.Max();
					if (max > yMax)
						yMax = max;
				}

				// Attach ChartScales
				// X-scale.
				horizontalScale = new ChartDateTimeScale(xMin, xMax
					, chart.ActualWidth > 0 ? chart.ActualWidth : ActualWidth);
				// Y-scale.
				verticalScale = new ChartLinearScale(yMax, yMin
					, chart.ActualHeight > 0 ? chart.ActualHeight : ActualHeight);
			}
			else
			{
				MessageBox.Show(string.Format("There is no appropriate data in the {0} file", fileName)
					, "Open Data File");
				return false;
			}

			if (horizontalScale == null || !horizontalScale.IsConsistent)
				throw new ArgumentException(string.Format("Invalid X scale in {0} file", fileName));
			HorizontalScale = horizontalScale;
			if (verticalScale == null || !verticalScale.IsConsistent)
				throw new ArgumentException(string.Format("Invalid Y scale in {0} file", fileName));
			VerticalScale = verticalScale;

			// PointMarker.
			GeometryDrawing pointMarker = new GeometryDrawing(Brushes.Brown, new Pen(Brushes.Red, 1)
				, new EllipseGeometry(new Point(0, 0), 4, 4));
			pointMarker.Freeze();

			// Create DataViewCollection.
			ObservableCollection<ItemDataView> dataViewCollection = new ObservableCollection<ItemDataView>();
			for (int i = 0; i < dataCollection.Count; ++i)
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

			DataContext = dataViewCollection;

			return true;
		}

		#region Menu actions handlers
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
			if (!loadCurveData(fileName))
				// Remove from MRU list
				mnuRecentFiles.Items.Remove(item);
		}

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
					newItem.Click += mru_Click;
					mnuRecentFiles.Items.Add(newItem);
				}
			}
		}

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
			{ // Show CurveProperties dialog.
				// Get GenericDataTemplateSelectorItems.
				Collection<GenericDataTemplateSelectorItem> templateSelectorItems = null;
				GenericDataTemplateSelector templateSelector = chart.ItemTemplateSelector as GenericDataTemplateSelector;
				if (templateSelector != null)
					templateSelectorItems = templateSelector.SelectorItems;

				// Show the dialog.
				ItemPropertiesDialog dlg = new ItemPropertiesDialog(itemDataView, templateSelectorItems) 
				{ 
					Owner = this,
					AllowSwapVetricalLinLogScale = false,
					AllowSwapHorizontalLinLogScale = false
				};
				if (dlg.ShowDialog() == true)
				{
					// Curve Name
					itemDataView.ItemData.ItemName = dlg.ItemName;

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

		/// <summary>
		/// Execute <see cref="NavigationCommands.Zoom"/> command.
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
		}
		#endregion Command Handlers
	}
}
