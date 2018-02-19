using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Microsoft.TeamFoundation.Build.Client;

namespace TFS_API_AdvancedBuildActions.IValueConverters
{
    public class ProcessTemplateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IProcessTemplate)
                return (IProcessTemplate)value;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
