using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Native;

namespace DevExpressReportingTest
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }


        public void ReplaceDummyControl(Control ctrl)
        {
            winControlContainer1.WinControl = ctrl;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Control ctrl = winControlContainer1.WinControl;
            //Metafile mf = XRControlPaint.GetControlImage(ctrl, WinControlDrawMethod_Utils.UseWMPaint, WinControlImageType_Utils.Metafile) as Metafile;
            //xrPictureBox1.Image = mf;
            //xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        }
    }
}
