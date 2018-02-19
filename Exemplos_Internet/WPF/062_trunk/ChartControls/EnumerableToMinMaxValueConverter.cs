// <copyright file="EnumerableToMinMaxValueConverter.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. 
// EnumerableMinMaxValueConverter gets either Min or Max value of the enumeration depending on "parameter" value.
// </summary>
// <revision>$Id: EnumerableToMinMaxValueConverter.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Controls
{
	/// <summary>
	/// Gets either Min or Max value of the enumeration depending on "parameter" value.
	/// </summary>
	[ValueConversion(/*sourceType*/ typeof(IEnumerable<DataPoint<double, double>>), /*targetType*/ typeof(string))]
	[ValueConversion(/*sourceType*/ typeof(IEnumerable<DataPoint<DateTime, double>>), /*targetType*/ typeof(string))]
	public class EnumerableMinMaxValueConverter : IValueConverter
	{
		/// <summary>
		/// Converts a value to either Min or Max values of the sequence depending on "parameter" value.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.
		/// If not defined method returns Min value; if is "true" method returns Max value.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(string))
				return null;
			if (value is IEnumerable<DataPoint<double, double>>)
			{
				// y-values.
				var y = from pt in (value as IEnumerable<DataPoint<double, double>>) select pt.Y;
				if (y.Count() == 0)
					return null;
				if (parameter is string && (string)parameter == "true")
					return y.Max().ToString();
				else
					return y.Min().ToString();
			}
			if (value is IEnumerable<DataPoint<DateTime, double>>)
			{
				// y-values.
				var y = from pt in (value as IEnumerable<DataPoint<DateTime, double>>) select pt.Y;
				if (y.Count() == 0)
					return null;
				if (parameter is string && (string)parameter == "true")
					return y.Max().ToString();
				else
					return y.Min().ToString();
			}
			return null;
		}
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
