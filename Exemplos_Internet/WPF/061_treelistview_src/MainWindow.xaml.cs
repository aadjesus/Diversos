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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SampleApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            xisto1010.DataContext = new DateTime(2009,12,5,15,15,0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 sss = new Window1();
            sss.Show();
        }
    }

    public class FormatConverter : IValueConverter
    {
        #region Convert

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string formatString = parameter as string;

            if (formatString.IndexOf("MaiorQueZero") > 0)
            {
                return (int)value > 0;
            }
            else
                if (formatString.IndexOf("Param.") != -1)
                {
                    string[] _cor = value.ToString().Split(new char[] { ';' }, 4);
                    try
                    {
                        return _cor[System.Convert.ToInt16(formatString.ToString().Substring(6, 1))];
                    }
                    catch (Exception)
                    {
                        return value.ToString();
                    }

                }
                else if (formatString != null)
                {
                    return string.Format(culture, formatString, value);
                }
                else
                {
                    return value.ToString();
                }
        }

        #endregion

        #region IValueConverter Members

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
