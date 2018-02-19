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
using DevExpress.Xpf.Editors;

namespace WpfApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dateNavigator1_Loaded(object sender, RoutedEventArgs e)
        {
            dateNavigator1.SpecialDates.Add(new DateTime(2013, 8, 5));
            dateNavigator1.SpecialDates.Add(new DateTime(2013, 8, 6));
            dateNavigator1.SpecialDates.Add(DateTime.Now);
            //dateNavigator1.Holidays.Add(new DateTime(2013, 8, 7));
            //dateNavigator1.Holidays.Add(new DateTime(2013, 8, 8));
            //dateNavigator1.HighlightSpecialDates = true;
        }
    }
}
