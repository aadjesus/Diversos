using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace WCF_Client_SvcUtil
{
    public partial class FOptions : Form
    {
        public FOptions()
        {
            InitializeComponent();
        }

        private void FOptions_Load(object sender, EventArgs e)
        {
            //Apply Aero Glass effect if set in user settings
            if (Properties.Settings.Default.StartWithAereoGlass)
                glassProvider.ApplyGlass();

            Properties.Settings.Default.Save();
        }

        private void btnBrowseForSvcUtilFolder_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = txtSvcUtilFolder.Text;

            DialogResult result = folderBrowser.ShowDialog(this);
            while (result.Equals(DialogResult.OK) && !File.Exists(folderBrowser.SelectedPath + "\\" + Properties.Settings.Default.SvcUtilFilename))
            {
                MessageBox.Show("Wrong path.");
                result = folderBrowser.ShowDialog(this);
            }
            if (result.Equals(DialogResult.OK))
                txtSvcUtilFolder.Text = folderBrowser.SelectedPath;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }
    }
}
