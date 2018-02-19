using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfHorseRace
{
	#region DoubleToIntegerConverter

	[ValueConversion( typeof( double ), typeof( int ) )]
	public class DoubleToIntegerConverter : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			return (int)(double)value;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "Cannot convert back" );
		}
	}

	#endregion // DoubleToIntegerConverter	

	#region RaceTrackWidthConverter

	/// <summary>
	/// Determines how wide a RaceHorse's progress indicator should be, based on how far into the race it is.
	/// </summary>
	public class RaceHorseProgressIndicatorWidthConverter : IMultiValueConverter
	{
		public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
		{
			int percentComplete = (int)values[0];
			double availableWidth = (double)values[1];
			double width = Math.Floor( availableWidth * (percentComplete / 100.0) );
			// Reduce the final width by a little bit to avoid a minor rendering overlap.
			return percentComplete == 100 ? width - 4 : width;
		}

		public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "Cannot convert back" );
		}
	}

	#endregion // RaceTrackWidthConverter	
}