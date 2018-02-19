using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class FormTelaAguarde : Form
    {
        public FormTelaAguarde()
        {
            InitializeComponent();


            //const int sLen = 30, Loops = 5000;
            //DateTime sTime, eTime;
            //int i;
            //string sSource = new String('X', sLen);
            //string sDest = "";
            //// 
            //// Time string concatenation.
            //// 
            //sTime = DateTime.Now;
            //for (i = 0; i < Loops; i++) 
            //    sDest += sSource;

            //eTime = DateTime.Now;
            //Console.WriteLine("Concatenation took " + (eTime - sTime).TotalSeconds + " seconds.");
            //// 
            //// Time StringBuilder.
            //// 
            //sTime = DateTime.Now;
            ////System.Text.StringBuilder sb = new System.Text.StringBuilder((int)(sLen * Loops * 1.1));
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //for (i = 0; i < Loops; i++) 
            //    sb.Append(sSource);

            //sDest = sb.ToString();
            //eTime = DateTime.Now;
            //Console.WriteLine("String Builder took " + (eTime - sTime).TotalSeconds + " seconds.");
            //// 
            //// Make the console window stay open
            //// so that you can see the results when running from the IDE.
            //// 
            //Console.WriteLine();
            //Console.Write("Press Enter to finish ... ");
            //Console.Read();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000);
                    worker.ReportProgress(i * 10);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();

                if (!this.splashScreenManager1.IsSplashFormVisible)
                    this.splashScreenManager1.ShowWaitForm();

                this.splashScreenManager1.SendCommand(null, backgroundWorker1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (sender == backgroundWorker1)
                resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
            else
                label1.Text = (e.ProgressPercentage.ToString() + "%");

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Label label = sender == backgroundWorker1
                ? resultLabel
                : label1;

            if (e.Cancelled == true)
            {
                label.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                label.Text = "Error: " + e.Error.Message;
            }
            else
            {
                label.Text = "Done!";
            }

            if (sender == backgroundWorker1)
            {
                if (this.splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();                
            }
            else
            {
                if (this.splashScreenManager2.IsSplashFormVisible)
                    splashScreenManager2.CloseWaitForm();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy != true)
            {
                backgroundWorker2.RunWorkerAsync();

                if (!this.splashScreenManager2.IsSplashFormVisible)
                    this.splashScreenManager2.ShowWaitForm();

                this.splashScreenManager2.SendCommand(null, backgroundWorker2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.WorkerSupportsCancellation == true)
            {
                backgroundWorker2.CancelAsync();
            }
        }


    }
}

