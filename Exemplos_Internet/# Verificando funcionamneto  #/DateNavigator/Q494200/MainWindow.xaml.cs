using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Globalization;

namespace SchedulerDateNavigatorCustomizationWpf {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            schedulerControl1.ApplyTemplate();
            schedulerControl1.Start = DateTime.Today;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            dateNavigator1.SpecialDates.Add(DateTime.Today.AddDays(-1));
        }
    }
}