using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TFS_API_AdvancedBuildActions.IValueConverters
{
    public class UriToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Uri) { 
            if (parameter.Equals("Port"))
                return ((Uri)value).Port;
            else if (parameter.Equals("Host"))
                return ((Uri)value).Host;
            else
                return null;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
