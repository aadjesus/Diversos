// <copyright file="BoolToVisibilityConverter.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-20</date>
// <summary>OpenWPFChart library. A bool to Visibility value converter.</summary>
// <revision>$Id: BoolToVisibilityConverter.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Converts a bool value to Visibility.
	/// </summary>
	[ValueConversion(/*sourceType*/ typeof(bool), /*targetType*/ typeof(Visibility))]
	public class BoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Converts a bool value to Visibility.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(Visibility))
				return null;
			if (value is bool)
			{
				return (bool)value ? Visibility.Visible : Visibility.Collapsed;
			}
			return Visibility.Hidden;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
