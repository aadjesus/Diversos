using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nwindDataSet.Categories' table. You can move, or remove it, as needed.
            //this.categoriesTableAdapter.Fill(this.nwindDataSet.Categories);

            XtraReport2 p = new XtraReport2();

            WinControlContainer table = new WinControlContainer();
            table.WinControl = gridControl1;
            table.Width = 750;
            gridControl1.Width = 500;
            table.KeepTogether = true;
            XRChart xchart = new XRChart();
            ChartControl dummy = (ChartControl)this.chartControl1.Clone();
            xchart.DataSource = dummy.DataSource;
            
            xchart.Series.Add(dummy.Series[0]);
            //WinControlContainer chart = new WinControlContainer();
            //chart.WinControl = this.chartControl1;
            //this.chartControl1.Width = 500;
            xchart.Width = 750;
            xchart.Height = 200;
            xchart.KeepTogether = true;
            p.Bands.GetBandByType(typeof(DetailBand)).Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { table, xchart });
            p.ShowPreviewDialog();

            dummy.Dispose();
            
            
            XtraReport1 report = new XtraReport1();
            report.ShowPreviewDialog();
        }
    }
}