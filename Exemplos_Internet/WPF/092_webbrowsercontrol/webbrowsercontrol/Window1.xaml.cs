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

namespace webbrowsercontrol
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            myBrowser.Navigate(new Uri("http://www.google.com"));
        }

        private void btnExternal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri ui = new Uri(txtLoad.Text.Trim(), UriKind.RelativeOrAbsolute);
            myBrowser.Navigate(ui);
        }

        private void btnInternal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           // myBrowser.Navigate(new Uri("http://www.c-sharpcorner.com"));

            myBrowser.Navigate(new Uri("http://localhost:3460/Default.aspx"));
            
        }
    }
}
