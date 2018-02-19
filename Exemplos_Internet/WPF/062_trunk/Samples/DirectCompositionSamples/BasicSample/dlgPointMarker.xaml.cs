// WPF CurveChart.
// Copyright © Oleg V. Polikarpotchkin 2008

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ovp.CurveChart
{
	/// <summary>
	/// Dialog to select a PointMarker from internal list.
	/// </summary>
	public partial class dlgPointMarker : Window, INotifyPropertyChanged
	{
		public dlgPointMarker(Color color)
		{
			InitializeComponent();

			strokeColor = color;
			fillColor = color;
			DataContext = this;
		}

		/// <summary>
		/// Gets the marker geometry.
		/// </summary>
		/// <value>The marker geometry.</value>
		public Geometry MarkerGeometry
		{
			get 
			{
				if (btnOK.IsEnabled)
					return lbxMarkers.SelectedItem as Geometry;
				else
					return null; 
			}
		}

		Color strokeColor;
		/// <summary>
		/// Gets or sets the marker stroke color.
		/// </summary>
		/// <value>The color of the marker stroke.</value>
		public Color MarkerStrokeColor
		{
			get { return strokeColor; }
			set
			{
				if (strokeColor != value)
				{
					strokeColor = value;
					Notify("MarkerStrokeColor");
				}
			}
		}

		Color fillColor;
		/// <summary>
		/// Gets or sets the marker fill color.
		/// </summary>
		/// <value>The color of the marker fill.</value>
		public Color MarkerFillColor
		{
			get { return fillColor; }
			set
			{
				if (fillColor != value)
				{
					fillColor = value;
					Notify("MarkerFillColor");
				}
			}
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

		private void lbxMarkers_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			btnOK.IsEnabled = true;
		}

		private void btnMarkerStrokeColor_Click(object sender, RoutedEventArgs e)
		{
			MarkerStrokeColor = selectColor(MarkerStrokeColor);
		}

		private void btnMarkerFillColor_Click(object sender, RoutedEventArgs e)
		{
			MarkerFillColor = selectColor(MarkerFillColor);
		}

		/// <summary>
		/// Handles the Click event of the btnOK control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
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
		#endregion
	}
}
