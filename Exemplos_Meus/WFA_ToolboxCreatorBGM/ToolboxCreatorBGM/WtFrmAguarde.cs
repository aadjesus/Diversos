using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace ToolboxCreatorBGM
{
    public partial class WtFrmAguarde : WaitForm
    {
        /// <summary>
        /// Construtor default
        /// </summary>
        public WtFrmAguarde()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }
    }
}