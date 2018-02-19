// <copyright file="CelsiusVsFahrenheitConverter.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Converts temperature from Cesius to Fahrenheit and vice versa.</summary>
// <revision>$Id: CelsiusVsFahrenheitConverter.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Globalization;
using System.Windows.Data;
using OpenWPFChart.Parts;

namespace TemperatureSample
{
	/// <summary>
	/// Converts temperature from Cesius to Fahrenheit and vice versa.
	/// </summary>
	[ValueConversion(/*sourceType*/ typeof(double), /*targetType*/ typeof(double))]
	public class CelsiusVsFahrenheitConverter : IValueConverter 
	{
		/// <summary>
		/// Converts temperature scale from Celsius to Fahrenheit.
		/// </summary>
		/// <param name="t">Temperature in Celsius.</param>
		/// <returns>Temperature in Fahrenheit.</returns>
		private static double CelsiusToFahrenheit(double t)
		{
			return t * 9 / 5 + 32;
		}

		/// <summary>
		/// Converts temperature scale from Fahrenheit to Celsius.
		/// </summary>
		/// <param name="t">Temperature in Fahrenheit.</param>
		/// <returns>Temperature in Celsius.</returns>
		private static double FahrenheitToCelsius(double t)
		{
			return (t - 32) * 5 / 9;
		}

		/// <summary>
		/// Converts temperature scale from Celsius to Fahrenheit
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		public object Convert(object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (value == null || value.GetType() != typeof(ChartLinearScale))
				return value;
			ChartLinearScale scale = value as ChartLinearScale;
			return new ChartLinearScale()
			{
				Start = CelsiusToFahrenheit((double)scale.Start),
				Stop = CelsiusToFahrenheit((double)scale.Stop),
				Scale = scale.Scale * 5 / 9,
				TickStep = scale.TickStep,
				LongTickAnchor = scale.LongTickAnchor,
				LongTickRate = scale.LongTickRate
			};
		}

		/// <summary>
		/// Converts a temperature value from Fahrenheit to Celsius.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		public object ConvertBack(object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (value == null || value.GetType() != typeof(ChartLinearScale))
				return value;
			ChartLinearScale scale = value as ChartLinearScale;
			return new ChartLinearScale()
			{
				Start = FahrenheitToCelsius((double)scale.Start),
				Stop = FahrenheitToCelsius((double)scale.Stop),
				Scale = scale.Scale * 9 / 5,
				TickStep = scale.TickStep,
				LongTickAnchor = scale.LongTickAnchor,
				LongTickRate = scale.LongTickRate
			};
		}
	}
}
