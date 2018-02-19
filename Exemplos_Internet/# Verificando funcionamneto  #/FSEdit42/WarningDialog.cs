using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAS.Tasks.Examples.FSEdit
{
    public partial class WarningDialog : Form
    {
        private bool bShowAgain;

        public WarningDialog(String strMessage)
        {
            InitializeComponent();
            warningMessage.Text = strMessage;
            bShowAgain = true;
        }

        public bool ShowAgain
        {
            get { return bShowAgain; }
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            bShowAgain = !checkBox1.Checked;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            String strTemp = string.Empty;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            String strTemp = string.Empty;
        }
    }
}