using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.DataNodes;

namespace GridSample
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using wcfService;
    using System.Collections.Generic;
    using System.Windows;
    using System;
 

    /// <summary>
    /// Home page for the application.
    /// </summary>
    public partial class Home : Page
    {
        DBServiceClient db = new DBServiceClient();
        List<SelectClientResult> ClientList = new List<SelectClientResult>();
        /// <summary>
        /// Creates a new <see cref="Home"/> instance.
        /// </summary>
        public Home()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.HomePageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Export_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = ".xls";
            s.Filter = "Excel Files (2007/2010) | *.xlsx| Excel Files (97/2003) | *.xls";
            bool? x = s.ShowDialog();
            if (x == true)
            {
                System.IO.Stream FileStream = s.OpenFile(); 
                PrintableControlLink link = new PrintableControlLink((object)dgClient.View as IPrintableControl);
                link.ExportServiceUri = "../ETExportService.svc";
                link.CreateDocument(false);
                link.ExportProgress += new EventHandler<DevExpress.Xpf.Printing.Export.DocumentExportingServiceProgressEventArgs>(link_ExportProgress);
                switch (s.FilterIndex)
                {
                    case 1: link.ExportToXlsx(FileStream, new DevExpress.XtraPrinting.XlsxExportOptions(), true);
                        break;
                    case 2: link.ExportToXls(FileStream, new DevExpress.XtraPrinting.XlsExportOptions(), true);
                        break;
                    default: link.ExportToXls(FileStream, new DevExpress.XtraPrinting.XlsExportOptions(), true);
                        break;
                }
            }
        }

        void link_ExportProgress(object sender, DevExpress.Xpf.Printing.Export.DocumentExportingServiceProgressEventArgs e)
        {
            MessageBox.Show("Exported Succesfully");
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            db.GetClientListCompleted += new System.EventHandler<GetClientListCompletedEventArgs>(db_GetClientListCompleted);
            db.GetClientListAsync();
        }

        void db_GetClientListCompleted(object sender, GetClientListCompletedEventArgs e)
        {
            dgClient.ItemsSource = e.Result;
        }
    }
}