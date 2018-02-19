// <copyright file="ItemCountToBoolConverter.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-03</date>
// <summary>OpenWPFChart library. WellLogSample ChartScales dialog.
// </summary>
// <revision>$Id: dlgChartScales.xaml.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Globalization; // For CultureInfo
using System.ComponentModel; // For INotifyPropertyChanged
using System.Windows;

namespace WellLogControlSample
{
	/// <summary>
	/// WellLogSample ChartScales dialog.
	/// </summary>
	public partial class dlgChartScales : Window
	{
		/// <summary>
		/// This dialog private data cache.
		/// </summary>
		class DialogData : INotifyPropertyChanged, IDataErrorInfo
		{
			public DialogData(double depthScale, double chartWidth)
			{
				this.depthScale = depthScale;
				this.chartWidth = chartWidth;
			}

			double depthScale;
			public double DepthScale
			{
				get { return depthScale; }
				set
				{
					if (depthScale != value)
					{
						if (value <= 0.0)
							throw new ArgumentException("DepthScale must be positive");
						depthScale = value;
						Notify("DepthScale");
					}
				}
			}

			double chartWidth;
			public double ChartWidth
			{
				get { return chartWidth; }
				set
				{
					if (chartWidth != value)
					{
						if (value <= 0.0)
							throw new ArgumentException("ChartWidth must be positive");
						chartWidth = value;
						Notify("ChartWidth");
					}
				}
			}

			#region INotifyPropertyChanged Members
			public event PropertyChangedEventHandler PropertyChanged;
			void Notify(string prop)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(prop));
				}
			}
			#endregion INotifyPropertyChanged Members

			#region IDataErrorInfo Members
			string IDataErrorInfo.Error
			{
				get { return null; }
			}

			string IDataErrorInfo.this[string columnName]
			{
				get
				{
					switch (columnName)
					{
						case "DepthScale":
							if (DepthScale <= 0)
								return "DepthScale must be positive";
							break;
						case "ChartWidth":
							if (ChartWidth <= 0)
								return "ChartWidth must be positive";
							break;
					}
					return null;
				}
			}
			#endregion IDataErrorInfo Members
		}

		DialogData data;

		public dlgChartScales(double depthScale, double chartWidth)
		{
			InitializeComponent();

			data = new DialogData(depthScale, chartWidth);
			data.PropertyChanged += data_PropertyChanged;
			DataContext = data;
		}

		/// <summary>
		/// Gets the DepthScale.
		/// </summary>
		/// <value>The DepthScale.</value>
		public double DepthScale
		{
			get { return data.DepthScale; }
		}

		/// <summary>
		/// Gets the ChartWidth.
		/// </summary>
		/// <value>The ChartWidth.</value>
		public double ChartWidth
		{
			get { return data.ChartWidth; }
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
