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
using System.Windows.Shapes;
using System.IO;


namespace adhoc
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Show();
        }


        private void OnNavigateToStream(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(@"pack://application:,,,/page.htm");
            Stream source = Application.GetContentStream(uri).Stream;
            b.NavigateToStream(source);

        }


        private void OnNavigateToString(object sender, RoutedEventArgs e)
        {
            //b.NavigateToString("<html><h2><b>Navigated to a string</b></p></h2></html>");
            b.NavigateToString("Navigated to a string");
        }

        private void OnGoBack(object sender, RoutedEventArgs e)
        {
            if (b.CanGoBack)
            {
                b.GoBack();
            }
            else
            {
                MessageBox.Show("Cannot Go back");
            }
        }

        private void OnGoForward(object sender, RoutedEventArgs e)
        {
            if (b.CanGoForward)
            {
                b.GoForward();
            }
            else
            {
                MessageBox.Show("Cannot Go Forward");
            }
        }


        private void OnNavigateToCNN(object sender, RoutedEventArgs e)
        {
            b.Navigate(new Uri("http://www.live.com", UriKind.RelativeOrAbsolute));
        }

        private void OnNavigateToMSNBC(object sender, RoutedEventArgs e)
        {
            b.Navigate(new Uri("pack://siteoforigin:,,,/htmlpage1.htm", UriKind.RelativeOrAbsolute));

        }

    }
}