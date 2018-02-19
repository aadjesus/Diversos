using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace GGG.GameAttackCombos.Client {

	/// <summary>
	/// A value converter that converts a string representing a resource key into the referenced 
	/// Drawing after retrieving it from the application's resources.
	/// </summary>
	[ValueConversion(typeof(string), typeof(Drawing))]
	public class DrawingResourceKeyConverter : IValueConverter {

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
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
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}

		#endregion
	}

}