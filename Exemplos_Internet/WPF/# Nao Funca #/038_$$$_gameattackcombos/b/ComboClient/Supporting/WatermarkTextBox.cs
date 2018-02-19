using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// A TextBox with watermark text displayed when no user input is supplied.
	/// </summary>
	public class WatermarkTextBox : TextBox {

		/// <summary>
		/// The watermark text dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkTextProperty =
			DependencyProperty.Register(
				"WatermarkText",
				typeof(string),
				typeof(WatermarkTextBox),
				new PropertyMetadata(string.Empty)
			);

		/// <summary>
		/// The watermark brush dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkBrushProperty =
			DependencyProperty.Register(
				"WatermarkBrush",
				typeof(Brush),
				typeof(WatermarkTextBox),
				new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender)
			);



		/// <summary>
		/// Gets or sets the watermark text for this TextBox.
		/// </summary>
		public string WatermarkText {
			get { return (string)GetValue(WatermarkTextProperty); }
			set { SetValue(WatermarkTextProperty, value); }
		}

		/// <summary>
		/// Gets or sets the watermark brush for this TextBox.
		/// </summary>
		public Brush WatermarkBrush {
			get { return (Brush)GetValue(WatermarkBrushProperty); }
			set { SetValue(WatermarkBrushProperty, value); }
		}


		/// <summary>
		/// A static constructor to override the default style for this control.
		/// </summary>
		static WatermarkTextBox() {
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(WatermarkTextBox),
				new FrameworkPropertyMetadata(typeof(WatermarkTextBox))
			);
		}

	}

}