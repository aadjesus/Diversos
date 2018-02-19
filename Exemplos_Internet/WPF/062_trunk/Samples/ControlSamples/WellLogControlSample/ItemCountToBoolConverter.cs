// <copyright file="ItemCountToBoolConverter.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-02</date>
// <summary>OpenWPFChart library. An int to bool value converter.</summary>
// <revision>$Id: ItemCountToBoolConverter.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Globalization;
using System.Windows.Data;

namespace WellLogControlSample
{
	/// <summary>
	/// Converts an int value to bool.
	/// </summary>
	[ValueConversion(/*sourceType*/ typeof(int), /*targetType*/ typeof(bool))]
	public class ItemCountToBoolConverter : IValueConverter
	{
		/// <summary>
		/// Converts an int value to bool.
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
			if (targetType != typeof(bool))
				return null;
			if (value is int)
			{
				return (int)value == 0 ? false : true;
			}
			return null;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
