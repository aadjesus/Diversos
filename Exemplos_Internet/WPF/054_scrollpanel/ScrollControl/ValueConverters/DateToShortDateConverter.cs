using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ScrollControl
{
    /// <summary>
    /// Converts from standard DateTime to formattetd Date string
    /// in the format 01 Jul 07 for example
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateToShortDateConverter : IValueConverter
    {
        #region Data
        public static DateToShortDateConverter Instance = new DateToShortDateConverter();
        #endregion

        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            return ((DateTime)value).ToString("dd MMM yy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
        #endregion
    }
}