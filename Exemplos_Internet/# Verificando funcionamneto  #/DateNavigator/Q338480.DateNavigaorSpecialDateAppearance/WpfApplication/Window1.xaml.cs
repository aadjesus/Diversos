using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Globalization;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public class ThemeNameConverterExtension : MarkupExtension, IValueConverter
        {
            static ThemeNameConverterExtension instance = new ThemeNameConverterExtension();
            public override object ProvideValue(IServiceProvider serviceProvider)
            {
                return instance;
            }

            #region IValueConverter Members
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                ListBoxItem listBoxItem = value as ListBoxItem;

                if (listBoxItem != null)
                    return listBoxItem.Content;

                return value;
            }
            #endregion
        }
    }
}
