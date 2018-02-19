// Copyright © Oleg V. Polikarpotchkin 2008-2009
// <copyright file="WellLogWindow.xaml.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-08</date>
// <summary>OpenWPFChart library. WellLogControl Sample.</summary>
// <revision>$Id: WellLogWindow.xaml.cs 19247 2009-03-22 07:34:44Z unknown $</revision>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32; // For OpenFileDialog and SaveFileDialog
using OpenWPFChart.Helpers;
using OpenWPFChart.Parts;

namespace WellLogControlSample
{
	/// <summary>
	/// WellLogControl Sample main Window.
	/// </summary>
	public partial class WellLogWindow : Window
	{
		public WellLogWindow()
		{
			InitializeComponent();

			// Bind to File.Open Command
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenCommandHandler));
			// Bind to View.Zoom Command
			InputBindings.Add(new KeyBinding(NavigationCommands.Zoom, Key.Add // + at Numeric Pad
				, ModifierKeys.Control) { CommandParameter = 1.2 });
			InputBindings.Add(new KeyBinding(NavigationCommands.Zoom, Key.Subtract // - at Numeric Pad
				, ModifierKeys.Control) { CommandParameter = -1.2 });
			CommandBindings.Add(new CommandBinding(NavigationCommands.Zoom, ZoomCommandHandler));
			//  Bind to Properties Command
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Properties
				, PropertiesExecutedCommandHandler, PropertiesCanExecuteCommandHandler));
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

		#region Load WellLog Data from file
		/// <summary>
		/// Loads WellLog Data from the file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		private bool loadWellLogData(string fileName)
		{
			string depthAxisLabel;
			ObservableCollection<ItemData> curveDataCollection = null;
			try
			{
				curveDataCollection = parseFile(fileName, out depthAxisLabel);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}

			if (curveDataCollection == null || curveDataCollection.Count == 0)
			{ // Clear Chart UI.
				chart.ItemsSource = null;
				chart.DepthAxisTitle = null;
				return false;
			}

			// Set Depth Axis label.
			chart.DepthAxisTitle = depthAxisLabel;

			// Depth Scale.
			double xMin = (curveDataCollection[0] as SampledCurveData<double, double>).Points.First<DataPoint<double, double>>().X;
			double xMax = (curveDataCollection[0] as SampledCurveData<double, double>).Points.Last<DataPoint<double, double>>().X;
			ChartScale depthScale = new ChartLinearScale()
			{
				Start = xMin,
				Stop = xMax,
				Scale = 96 / (0.0254 * (chart.DepthScale != null ? chart.DepthScale.Scale : 200.0)), // Should be 1 cm per (DepthScale/100) m
				TickStep = 1,
				LongTickRate = 10,
				LongTickAnchor = 1000
			};

			// Create DataViewCollection.
			ObservableCollection<ItemDataView> dataViewCollection = new ObservableCollection<ItemDataView>();
			ArrayExtension curveDecorations = FindResource("curveDecorations") as ArrayExtension;
			for (int i = 0; i < curveDataCollection.Count; ++i)
			{
				SampledCurveDataView dataView = new SampledCurveDataView() 
				{ 
					ItemData = curveDataCollection[i],
					HorizontalScale = depthScale,
					VerticalScale = CreateVerticalScale(curveDataCollection[i]),
					Orientation = OpenWPFChart.Parts.Orientation.Vertical,
					VisualCue = typeof(PolylineSampledCurve)
				};

				// Set Default Curve Decorations.
				int decIndex = i % curveDecorations.Items.Count;
				CurveDecorations decorations = curveDecorations.Items[decIndex] as CurveDecorations;
				decorations.CurvePen.Freeze();
				dataView.Pen = decorations.CurvePen;
				IPointMarker iPointMarker = dataView as IPointMarker;
				if (iPointMarker == null)
					continue;
				decorations.PointMarker.Freeze();
				iPointMarker.PointMarker = decorations.PointMarker;
				iPointMarker.PointMarkerVisible = decorations.PointMarkerVisible;

				dataViewCollection.Add(dataView);
			}

			// Set Chart Data Source.
			chart.ItemsSource = dataViewCollection;

			chart.SelectedIndex = 0;

			return true;
		}

		/// <summary>
		/// Parses the CurveData file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="depthAxisLabel">Label of the Depth axis.</param>
		/// <returns></returns>
		/// <exception cref="Exception">Misc IO and file parser exceptions.</exception>
		/// <remarks>
		/// 	<para>WellLog data file is the rectangular table of fields separated by either space or tab.
		///			Data file must contain one header line and one or more series points lines.</para>
		/// 	<para>Header line is the list of string column names ( &gt; 1) and looks like</para>
		/// 	<para>name1 name2 name3 name4</para>
		/// 	<para>First column is treated as depth.</para>
		/// 	<para>Series points line is the list of series values and looks like</para>
		/// 	<para>1000 -0.35 10 48.0</para>
		/// 	<para>Token counts in the header and in each series line must be equal.</para>
		/// 	<para>Each column values must be convertible to <c>double</c>.</para>
		/// </remarks>
		private ObservableCollection<ItemData> parseFile(string fileName, out string depthAxisLabel)
		{
			depthAxisLabel = null;
			char[] separators = { ' ', '\t' };
			string[] curveNames = null;
			List<DataPoint<double, double>>[] curvePoints = null;
			int n = 0;
			string ln;
			using (StreamReader reader = new StreamReader(fileName))
			{
				while ((ln = reader.ReadLine()) != null)
				{
					n++;
					string[] tokens = ln.Split(separators, StringSplitOptions.RemoveEmptyEntries);
					if (tokens.Length == 0)
						continue;

					if (curveNames == null)
					{
						curveNames = new string[tokens.Length - 1];
						Array.Copy(tokens, 1, curveNames, 0, tokens.Length - 1);
						depthAxisLabel = tokens[0];

						curvePoints = new List<DataPoint<double, double>>[curveNames.Length];
						for (int i = 0; i < curvePoints.Length; ++i)
							curvePoints[i] = new List<DataPoint<double, double>>();
					}
					else
					{
						if (tokens.Length != curveNames.Length + 1)
							throw new Exception(string.Format("Invalid token count at line {0}", n));

						double x, y;
						if (!double.TryParse(tokens[0], out x))
							throw new Exception(string.Format("Invalid token 0 at line {0}", n));

						for (int i = 1; i < tokens.Length; ++i)
						{
							if (!double.TryParse(tokens[i], out y))
								throw new Exception(string.Format("Invalid token {0} at line {1}", i, n));

							curvePoints[i - 1].Add(new DataPoint<double, double>(x, y));
						}
					}
				}
			}

			ObservableCollection<ItemData> curveDataList = new ObservableCollection<ItemData>();
			for (int i = 0; i < curveNames.Length; ++i)
			{
				// Sort points
				List<DataPoint<double, double>> points = (from pt in curvePoints[i] orderby pt.X select pt).ToList();

				SampledCurveData<double, double> curveData = new SampledCurveData<double, double>()
				{
					ItemName = curveNames[i],
					Points = points
				};

				curveDataList.Add(curveData);
			}

			return curveDataList;
		}

		/// <summary>
		/// Sets Chart vertical scale basing on selected item.
		/// </summary>
		private ChartLinearScale CreateVerticalScale(ItemData itemData)
		{
			SampledCurveData<double, double> sampledNumericCurveData = itemData as SampledCurveData<double, double>;
			if (sampledNumericCurveData != null)
			{
				IEnumerable<double> ordinates = from pt in sampledNumericCurveData.Points select pt.Y;
				double min = ordinates.Min();
				double max = ordinates.Max();
				double scale = chart.ChartWidth / Math.Abs(max - min);
				double tickStep = Math.Abs(max - min) / 20;

				return new ChartLinearScale()
				{
					Start = max,
					Stop = min,
					Scale = scale,
					TickStep = tickStep,
					LongTickRate = 5
				};
			}
			return null;
		}
		#endregion Load WellLog Data from file

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

		#region Menu Actions Handlers
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
			if (!loadWellLogData(fileName))
				// Remove from MRU list
				mnuRecentFiles.Items.Remove(item);
		}

		/// <summary>
		/// Hides/shows all Curves PointMarkers.
		/// Handles the Click event of the mnuShowPointMarkers menu item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuViewShowPointMarkers_Click(object sender, RoutedEventArgs e)
		{
			ObservableCollection<ItemDataView> curveDataCollection = chart.ItemsSource as ObservableCollection<ItemDataView>;
			if (curveDataCollection == null)
				return;
			bool isCkecked = mnuViewShowPointMarkers.IsChecked;
			foreach (IPointMarker item in curveDataCollection)
			{
				if (item != null)
					item.PointMarkerVisible = isCkecked;
			}
		}

		/// <summary>
		/// Shows the dlgChartScales dialog.
		/// Handles the Click event of the mnuViewChartScales menu item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuViewChartScales_Click(object sender, RoutedEventArgs e)
		{
			dlgChartScales dlg = new dlgChartScales(chart.DepthScale.Scale, chart.ChartWidth) { Owner = this };
			if (dlg.ShowDialog() == true)
			{
				chart.DepthScale.Scale = dlg.DepthScale;
				chart.ChartWidth = dlg.ChartWidth;
			}
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
		/// Handles the Click event of the Exit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		#endregion Menu Actions Handlers

		#region Commands Handlers
		/// <summary>
		/// Execute <see cref="ApplicationCommands.Open"/> command.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
		private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			if (dlg.ShowDialog() == true)
			{
				if (loadWellLogData(dlg.FileName))
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

			chart.DepthScale.Scale /= zoom;
			chart.ChartWidth *= zoom;
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
			{ // Show CurveProperties dialog.
				// Get GenericDataTemplateSelectorItems.
				Collection<GenericDataTemplateSelectorItem> templateSelectorItems = null;
				GenericDataTemplateSelector templateSelector = chart.ItemTemplateSelector as GenericDataTemplateSelector;
				if (templateSelector != null)
					templateSelectorItems = templateSelector.SelectorItems;

				ItemPropertiesDialog dlg = new ItemPropertiesDialog(itemDataView, templateSelectorItems)
				{
					Owner = this,
					VerticalScaleTabHeader = "Log Scale",
					HorizontalScaleTabHeader = "Depth Scale",
					AllowSwapHorizontalLinLogScale = false
				};
				if (dlg.ShowDialog() == true)
				{
					// Curve Name
					itemDataView.ItemData.ItemName = dlg.ItemName;

					#region Depth Scale
					ChartScale depthScale = itemDataView.HorizontalScale;
					depthScale.Start = dlg.HorizontalScaleStart;
					depthScale.Stop = dlg.HorizontalScaleStop;
					depthScale.Scale = dlg.HorizontalScaleScale;
					#endregion Depth Scale

					#region Value Scale
					ChartScale valueScale = itemDataView.VerticalScale;
					valueScale.Start = dlg.VerticalScaleStart;
					valueScale.Stop = dlg.VerticalScaleStop;
					valueScale.Scale = dlg.VerticalScaleScale;
					if (valueScale.GetType() == typeof(ChartLinearScale)
							&& dlg.VerticalScaleVeriety == ChartScaleVerieties.Logarithmic)
					{
						itemDataView.VerticalScale = new ChartLogarithmicScale(
							Convert.ToDouble(valueScale.Start)
							, Convert.ToDouble(valueScale.Stop)
							, chart.ChartWidth);
					}
					else if (valueScale.GetType() == typeof(ChartLogarithmicScale)
							&& dlg.VerticalScaleVeriety == ChartScaleVerieties.Linear)
					{
						itemDataView.VerticalScale = new ChartLinearScale(
							Convert.ToDouble(valueScale.Start)
							, Convert.ToDouble(valueScale.Stop)
							, chart.ChartWidth);
					}
					#endregion Value Scale

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
		#endregion Commands Handlers
	}

	/// <summary>
	/// Curve Decorations
	/// </summary>
	[Serializable]
	public class CurveDecorations
	{
		public string CurveName { get; set; }
		public Pen CurvePen { get; set; }
		public Drawing PointMarker { get; set; }
		public bool PointMarkerVisible { get; set; }
	}
}
