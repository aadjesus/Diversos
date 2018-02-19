using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GridExample;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.DataNodes;
using DevExpress.Xpf.Grid;
using DevExpress.Utils;
using System.Windows.Resources;


namespace SLGridExample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            grid.DataSource = new List<TestData>() {
                new TestData() { PlainText = "LMA AG", MemoText = "Mercedes-Benz SLK \n 2004 \n Silver", BooleanMember = true, Image = GetImage("/SLGridExample;component/Images/1.png") },
                new TestData() { PlainText = "Western Motors", MemoText ="Rolls-Royce Corniche \n 1975 \n Snowy whight", BooleanMember = false, Image = GetImage("/SLGridExample;component/Images/2.png") },
                new TestData() { PlainText = "Sun car rent", MemoText = "Ford Ranger FX-4\n 1999 \n Red rock", BooleanMember = true, Image = GetImage("/SLGridExample;component/Images/3.png") }
            };
        }

        ImageSource GetImage(string path)
        {

            StreamResourceInfo sr = Application.GetResourceStream(new Uri(path, UriKind.Relative));
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(sr.Stream);

            return bmp;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPrintPreview(grid);
        }

        private void ShowPrintPreview(GridControl grid)
        {
            DocumentPreview preview = new DocumentPreview();
            PrintableControlLink link = new PrintableControlLink(grid.View as IPrintableControl);
            link.ExportServiceUri = "../ReportService.svc";
            LinkPreviewModel model = new LinkPreviewModel(link);
            preview.Model = model;

            link.CreateDocument(true);
            DXDialog dlg = new DXDialog();
            dlg.Content = preview;
            dlg.Height = 640;
            dlg.Left = 150;
            dlg.Top = 150;
            dlg.ShowDialog();
        }
    }
}