using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace PrintProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void barButtonItemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            (xtraTabControl1.SelectedTabPage.Controls[0] as PrintableUserControl).ShowPrintPreview();
        }
    }
}