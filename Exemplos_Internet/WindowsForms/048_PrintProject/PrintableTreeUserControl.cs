using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace PrintProject
{
    public partial class PrintableTreeUserControl : PrintProject.PrintableUserControl
    {
        public PrintableTreeUserControl()
        {
            InitializeComponent();
        }

        protected override IPrintable GetPrintableComponent()
        {
            return treeList;
        }
    }
}

