using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Relatorio_Dev_DataSet
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }

        private void Espaco1Linha2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        }

        private void Tabela1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue("ID") != null)
                ((XRTable)sender).Bookmark = Convert.ToString(this.GetCurrentColumnValue("ID"));
        }

        private void Tabela2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (DetailReport.GetCurrentColumnValue("ID") != null)
            ((XRTable)sender).Bookmark = Convert.ToString(DetailReport.GetCurrentColumnValue("ID"));
        }

        private void Tabela3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (DetailReport1.GetCurrentColumnValue("ID") != null)
                ((XRTable)sender).Bookmark = Convert.ToString(DetailReport1.GetCurrentColumnValue("ID"));
        }

    }
}
