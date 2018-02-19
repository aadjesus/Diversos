using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace PhysicsHost
{
    /// <summary>
    /// A long date to short date converter
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class LongDateToShortDateConverter : IValueConverter
    {
        #region Convert
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToShortDateString();
        }
        #endregion

        #region ConvertBack
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
        #endregion
    }
}
