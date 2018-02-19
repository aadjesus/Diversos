using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.DataNodes;
using DevExpress.XtraPrinting;

namespace ExportGridProject
{
    public class MyGrid : DevExpress.Xpf.Grid.GridControl
    {
        public void ExportGrid()
        {
            SaveFileDialog xlsDialog = new SaveFileDialog();
            xlsDialog.Filter = "PDF Files | *.PDF";
            xlsDialog.DefaultExt = "PDF";

            bool? result = xlsDialog.ShowDialog();
            if (result == true)
            {
                System.IO.Stream fileStream = xlsDialog.OpenFile();

                VisualDataNodeLink link = new VisualDataNodeLink(this.View as IRootDataNodeSource);
                link.ExportServiceUri = "../ExportService1.svc";
                link.CreateDocument(false);
                link.ExportToPdf(fileStream, new PdfExportOptions(), false);
                fileStream.Close();
            }
        }
    }
}
