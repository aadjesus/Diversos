using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// A value converter that converts a string representing a resource key into the referenced 
	/// Drawing after retrieving it from the application's resources.
	/// </summary>
	[ValueConversion(typeof(string), typeof(Drawing))]
	public class DrawingResourceKeyConverter : IValueConverter {

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (value != null) {
				if (value is string) {
					if (targetType == typeof(Drawing)) {
						// Get the resource with a key specified as the value.
						return (Drawing)Application.Current.Resources[value];
					} else {
						throw new ArgumentException("The target type of the value to convert must be a Drawing.", "targetType");
					}
				} else {
					throw new ArgumentException("The value to convert must be a string.", "value");
				}
			} else {
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}

		#endregion

	}

}