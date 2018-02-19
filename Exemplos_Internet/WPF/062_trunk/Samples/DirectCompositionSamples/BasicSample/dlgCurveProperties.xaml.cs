// WPF CurveChart.
// Copyright © Oleg V. Polikarpotchkin 2008
/*
 */

using System.ComponentModel; // For INotifyPropertyChanged
using System.Windows;
using System.Windows.Media;
using ovp.CurveChart;

namespace ovp.CurveChart
{
	/// <summary>
	/// Interaction logic for dlgCurveProperties.xaml
	/// </summary>
	public partial class dlgCurveProperties : Window
	{
		/// <summary>
		/// This dialog private data cache.
		/// </summary>
		class DialogData : INotifyPropertyChanged
		{
			public DialogData(string curveName, Color curveColor)
			{
				this.curveName = curveName;
				this.curveColor = curveColor;
			}

			string curveName;
			public string CurveName 
			{
				get { return curveName; }
				set
				{
					if (curveName != value)
					{
						curveName = value;
						Notify("CurveName");
					}
				}
			}

			Color curveColor;
			public Color CurveColor
			{
				get { return curveColor; }
				set
				{
					if (curveColor != value)
					{
						curveColor = value;
						Notify("CurveColor");
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

		/// <summary>
		/// Initializes a new instance of the <see cref="dlgCurveProperties"/> class.
		/// </summary>
		/// <param name="item">The curve item.</param>
		public dlgCurveProperties(Item item)
		{
			InitializeComponent();


			Color color = Colors.Black;
			CurveData curveData = item.ItemData as CurveData;
			if (curveData != null)
			{
				SolidColorBrush solidColorBrush = curveData.Pen.Brush as SolidColorBrush;
				if (solidColorBrush != null)
					color = solidColorBrush.Color;
			}
			data = new DialogData(item.ItemData.ItemName, color);
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
		/// Gets the name of the curve.
		/// </summary>
		/// <value>The name of the curve.</value>
		public string CurveName
		{
			get { return data.CurveName; }
		}

		/// <summary>
		/// Gets the color of the curve.
		/// </summary>
		/// <value>The color of the curve.</value>
		public Color CurveColor
		{
			get { return data.CurveColor; }
		}

		/// <summary>
		/// Select new Curve Color.
		/// Handles the Click event of the btnCurveColor control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnCurveColor_Click(object sender, RoutedEventArgs e)
		{
			data.CurveColor = selectColor(data.CurveColor);
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
