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

namespace DisplayMultipleValuesInCombobox
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public CompanyViewModel _objViewModel;

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _objViewModel = new CompanyViewModel();
            SourceContext();
            _objViewModel.LoadData();
           
        }

        //The source(i.e. Datacontext) will be the entire CompanyViewModel
        private void SourceContext()
        {
            this.DataContext = _objViewModel;
        }

    }
}
