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
using DevExpress.Wpf.Charts;
using DevExpress.Wpf.Printing.UI;
using System.ComponentModel;
using System.Drawing.Printing;
using DevExpress.Wpf.Printing;
using System.IO;
using System.Windows.Markup;
using DevExpress.Wpf.Grid;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(ChartControl sssss)
        {

            InitializeComponent();
            _sssss = sssss;
        }
        ChartControl _sssss;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var link = new SimpleLink();
            //link.Detail = (DataTemplate)Resources["TestTemplate1"];
            //link.DetailCount = 1;

            //PrintPreview.PrintingSystem = link.PrintingSystem;
            //link.CreateDocument(true);

            
            var link = new SimpleLink();
            link.Detail = Resources["PrintingTemplate"] as DataTemplate;
            link.DetailCount = 1;
            link.CreateDetail += link_CreateDetail;

            
            PrintPreview1.PrintingSystem = link.PrintingSystem;
            link.CreateDocument(true);            

            //link.ShowPrintPreviewDialog(this);

            
        }

        void link_CreateDetail(object sender, DevExpress.Wpf.Printing.CreateAreaEventArgs e)
        {
            e.Data = new { Canvas = _sssss };
        }

    }
}
