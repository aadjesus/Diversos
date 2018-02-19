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

namespace OutlookWpfCalendar.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalendarViewButtonClick(object sender, RoutedEventArgs e)
        {
            CalendarViewWindow window = new CalendarViewWindow();
            window.Show();
        }

        private void HorizontalRangePanelButtonClick(object sender, RoutedEventArgs e)
        {
            HorizontalRangePanelWindow window = new HorizontalRangePanelWindow();
            window.Show();
        }

        private void VerticalRangePanelButtonClick(object sender, RoutedEventArgs e)
        {
            VerticalRangePanelWindow window = new VerticalRangePanelWindow();
            window.Show();
        }

        private void RestyledListBoxButtonClick(object sender, RoutedEventArgs e)
        {
            RestyledListBoxWindow window = new RestyledListBoxWindow();
            window.Show();
        }
    }
}
