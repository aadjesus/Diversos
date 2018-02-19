using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace PrintProject
{
    public partial class PrintableUserControl : UserControl
    {
        public PrintableUserControl()
        {
            InitializeComponent();
        }

        public void ShowPrintPreview()
        {
            printableComponentLink.Component = GetPrintableComponent();
            printableComponentLink.ShowPreviewDialog();
        }

        protected virtual IPrintable GetPrintableComponent()
        {
            return null;
        }
    }
}
