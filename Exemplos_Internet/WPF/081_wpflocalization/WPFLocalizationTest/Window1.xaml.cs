using System;
using System.Collections.Generic;
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
using WPFLocalization;
using System.Globalization;

namespace WPFLocalizationTest
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LocalizationManager.UICulture = new CultureInfo(((FrameworkElement)sender).Tag as string);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
        }
    }
}
