using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;

namespace WCF_Client_SvcUtil
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            //Apply Aero Glass effect if set in user settings
            if (Properties.Settings.Default.StartWithAereoGlass)
                glassProvider.ApplyGlass();
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FOptions frmOptions = new FOptions();
            frmOptions.ShowDialog(this);
            frmOptions = null;
        }

        private void btnOutputDirectory_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowser.ShowDialog(this).Equals(DialogResult.OK))
                txtOutputDirectory.Text = dlgFolderBrowser.SelectedPath;
        }

        private void generate_Click(object sender, EventArgs e)
        {
            //Show the wait control
            waitControl.Visible = true;
            txtProcOutput.Clear();

            //Run the background worker thread
            generationWorker.RunWorkerAsync();
        }

        private void generationWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Create and start SvcUtil
            Process process = new Process() { StartInfo = CreateStartInfo() };
            process.Start();

            //Update the output textbox
            generationWorker.ReportProgress(50, process.StandardOutput.ReadToEnd());
            e.Result = process.StandardOutput.ReadToEnd();

            //Stop the background worker thread when SvcUtil has exited
            process.WaitForExit();
        }

        private void generationWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtProcOutput.Text = e.UserState.ToString();
        }

        private void generationWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Hide the wait control
            waitControl.Visible = false;
        }

        private ProcessStartInfo CreateStartInfo()
        {
            //Build the arguments
            StringBuilder sbArguments = new StringBuilder();
            sbArguments.Append("/directory:" + txtOutputDirectory.Text);
            sbArguments.Append(" /out:" + txtProxyFilename.Text);
            sbArguments.Append(" /config:" + txtConfigFilename.Text);
            if (txtProxyFilename.Text.EndsWith(".cs")) sbArguments.Append(" /language:cs");
            if (txtProxyFilename.Text.EndsWith(".vb")) sbArguments.Append(" /language:vb");
            if (cbAsync.Checked) sbArguments.Append(" " + cbAsync.Text);
            if (cbMergeConfig.Checked) sbArguments.Append(" " + cbMergeConfig.Text);
            if (cbSerializable.Checked) sbArguments.Append(" " + cbSerializable.Text);
            sbArguments.Append(" " + txtServiceAddress.Text);

            string svcUtilFullFileName =
                Properties.Settings.Default.SvcUtilPath + "\\" +
                Properties.Settings.Default.SvcUtilFilename;

            //Return the start information for SvcUtil
            return new ProcessStartInfo()
            {
                FileName = svcUtilFullFileName,
                Arguments = sbArguments.ToString(),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
        }
    }
}
