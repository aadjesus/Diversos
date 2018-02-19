// <copyright file="AxisPropertiesDialog.xaml.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-06</date>
// <summary>OpenWPFChart library. Coordinate Axis Properties dialog.</summary>
// <revision>$Id: AxisPropertiesDialog.xaml.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Coordinate Axis Properties dialog.
	/// </summary>
	public partial class AxisPropertiesDialog : Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AxisPropertiesDialog"/> class.
		/// </summary>
		/// <param name="axis">The axis.</param>
		public AxisPropertiesDialog(Axis axis)
		{
			InitializeComponent();

			if (axis is LinearAxis)
				dataView = new LinearAxisDataView(axis as LinearAxis);
			else if (axis is LogarithmicAxis)
				dataView = new LogarithmicAxisDataView(axis as LogarithmicAxis);
			else if (axis is DateTimeAxis)
				dataView = new DateTimeAxisDataView(axis as DateTimeAxis);
			else if (axis is SeriesAxis)
				dataView = new SeriesAxisDataView(axis as SeriesAxis);
			if (dataView != null)
			{
				dataView.PropertyChanged += data_PropertyChanged;
				DataContext = dataView;
			}
		}

		/// <summary>
		/// The ChartScaleControl defined in a DataTemplate.
		/// </summary>
		ChartScaleControl chartScaleControl;

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Window.ContentRendered"/> event.
		/// </summary>
		/// <remarks>Finds the ChartScaleControl defined in a DataTemplate.</remarks>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnContentRendered(EventArgs e)
		{
			base.OnContentRendered(e);

			chartScaleControl = findChartScaleControl(this);
			if (chartScaleControl != null)
				chartScaleControl.PropertyChanged += data_PropertyChanged;
		}

		/// <summary>
		/// Finds the ChartScaleControl in VisualTree.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <returns></returns>
		static ChartScaleControl findChartScaleControl(DependencyObject parent)
		{
			if (parent is ChartScaleControl)
				return parent as ChartScaleControl;
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				ChartScaleControl scale = findChartScaleControl(child);
				if (scale != null)
					return scale;
			}
			return null;
		}

		/// <summary>
		/// Enable OK button.
		/// Handles the PropertyChanged event of the data control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void data_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			btnOK.IsEnabled = true;
		}

		#region AxisDataView
		AxisDataView dataView;
		/// <summary>
		/// Gets the axis data.
		/// </summary>
		public AxisDataView AxisData
		{
			get { return dataView; }
		}
		#endregion AxisDataView

		#region ChartScaleControl properties accessors
		public object Start
		{
			get { return chartScaleControl.Start; }
		}

		public object Stop
		{
			get { return chartScaleControl.Stop; }
		}

		public double Scale
		{
			get { return chartScaleControl.Scale; }
		}
		#endregion ChartScaleControl properties accessors

		/// <summary>
		/// Handles the Click event of the btnPenColor control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnPenColor_Click(object sender, RoutedEventArgs e)
		{
			dataView.PenColor = selectColor(dataView.PenColor);
		}

		/// <summary>
		/// Selects the color with System.Windows.Forms.ColorDialog.
		/// </summary>
		/// <param name="color">The WPF color.</param>
		/// <returns></returns>
		private static Color selectColor(Color color)
		{
			System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
			dlg.Color = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
			if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return color;
			return Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
		}

		/// <summary>
		/// Handles the Click event of the btnFont control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnFont_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Samples.WPFNotepad.FontChoice choice 
				= new Microsoft.Samples.WPFNotepad.FontChoice();

			choice.FontFamily = dataView.FontFamily;
			choice.FontStyle = dataView.FontStyle;
			choice.FontWeight = dataView.FontWeight;
			choice.FontStretch = dataView.FontStretch;
			choice.FontSize = dataView.FontSize;
			choice.Foreground = new SolidColorBrush(dataView.PenColor);

			Microsoft.Samples.WPFNotepad.FontChooser fontChooser =
				new Microsoft.Samples.WPFNotepad.FontChooser(choice) { Owner = this };
			fontChooser.FontChosen += ApplyPlainTextFontChoice;
			fontChooser.ShowDialog();
		}

		/// <summary>
		/// Applies the plain text font choice.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		void ApplyPlainTextFontChoice(object sender, Microsoft.Samples.WPFNotepad.FontChooserDialogAppliedEventsArgs e)
		{
			dataView.FontFamily = e.FontChoice.FontFamily;
			dataView.FontStyle = e.FontChoice.FontStyle;
			dataView.FontWeight = e.FontChoice.FontWeight;
			dataView.FontStretch = e.FontChoice.FontStretch;
			dataView.FontSize = e.FontChoice.FontSize;
		}

		/// <summary>
		/// Applyes changes and closes.
		/// Handles the Click event of the btnOK control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
