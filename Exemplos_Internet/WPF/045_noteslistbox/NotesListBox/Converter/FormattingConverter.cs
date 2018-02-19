using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;



namespace NotesListBox
{
    /// <summary>
    /// Provides a OneWay simple formatting converter
    /// </summary>
    public class FormattingConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string formatString = parameter as string;
            if (formatString != null)
            {
                return string.Format(culture, formatString, value);
            }
            else
            {
                return value.ToString();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("can not convert back");
        }

        #endregion
    }
}
