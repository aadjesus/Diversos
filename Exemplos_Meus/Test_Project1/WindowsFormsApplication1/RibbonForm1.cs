using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace WindowsFormsApplication1
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonForm1()
        {
            InitializeComponent();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

            new Form1() { MdiParent = this }.Show();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            new XtraForm1() { MdiParent = this }.Show();
        }
    }
}