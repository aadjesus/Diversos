// WPF CurveChart.
// Copyright © Oleg V. Polikarpotchkin 2008
/*
 */

using System.ComponentModel; // For INotifyPropertyChanged
using System.Windows;
using System.Windows.Media;

namespace ovp.CurveChart
{
	/// <summary>
	/// Interaction logic for dlgChartPointProperties.xaml
	/// </summary>
	public partial class dlgChartPointProperties : Window
	{
		/// <summary>
		/// This dialog private data cache.
		/// </summary>
		class DialogData : INotifyPropertyChanged
		{
			public DialogData(Drawing pointMarker)
			{
				this.pointMarker = pointMarker;
			}

			Drawing pointMarker;
			public Drawing PointMarker
			{
				get { return pointMarker; }
				set
				{
					if (pointMarker != value)
					{
						pointMarker = value;
						Notify("PointMarker");
					}
				}
			}

			// INotifyPropertyChanged Members
			public event PropertyChangedEventHandler PropertyChanged;
			void Notify(string prop)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(prop));
				}
			}
		}

		DialogData data;

		public dlgChartPointProperties(Drawing pointMarker)
		{
			InitializeComponent();

			data = new DialogData(pointMarker);
			data.PropertyChanged += data_PropertyChanged;

			DataContext = data;
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
		/// Gets the point marker.
		/// </summary>
		/// <value>The point marker.</value>
		public Drawing PointMarker
		{
			get { return data.PointMarker; }
		}

		/// <summary>
		/// Selects the PointMarker.
		/// Handles the Click event of the btnPointMarker control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnPointMarker_Click(object sender, RoutedEventArgs e)
		{
			Color color = Colors.Transparent;
			GeometryDrawing drawing = PointMarker as GeometryDrawing;
			if (drawing != null)
			{
				SolidColorBrush curveBrush = drawing.Brush as SolidColorBrush;
				if (curveBrush != null)
					color = curveBrush.Color;
			}
			dlgPointMarker dlg = new dlgPointMarker(color) { Owner = this };
			if (dlg.ShowDialog() == true)
			{
				data.PointMarker = new GeometryDrawing(new SolidColorBrush(dlg.MarkerFillColor)
					, new Pen(new SolidColorBrush(dlg.MarkerStrokeColor), 1), dlg.MarkerGeometry);
			}
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
