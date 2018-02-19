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

namespace SampleApplication
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
    }

    public sealed class Teste
    {
        public static void Show(string messageBoxText)
        {
            MessageBox.Show(messageBoxText);
        }
    }

    public class IntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string strValue = value as string;
            if (strValue != null)
            {
                int result;
                bool converted = int.TryParse(strValue, out result);
                if (converted)
                {
                    return result;
                }
            }
            return null;
        }
    }

}
