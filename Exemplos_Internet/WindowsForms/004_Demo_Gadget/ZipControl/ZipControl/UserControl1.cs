using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
namespace ZipControl
{
    
    [Guid("1FAC78B4-B53D-11DB-A52E-328155D89593")]
    [ComVisible(true)]
    public partial class UserControl1 : UserControl
    {
        ZipperOptions[] profiles;
        string output;
        string zipperPath;
        string outFilePrototype = Path.GetTempPath() + "temp.";
        string outFile;
        string initialArguments;
        string finalArguments;

        List<string> filePaths=new List<string> ();
        delegate void endOperation();

        public void setProfileSettings(string profileNoString, string exe, string param, string ext)
        {
            int profileNo = int.Parse(profileNoString);
            //MessageBox.Show(exe); 
            if (exe.CompareTo("")!=0)
            {
                //MessageBox.Show("Set profile no"+profileNo+ " to "+exe); 
                profiles[profileNo] = new ZipperOptions();
                profiles[profileNo].path = exe;
                profiles[profileNo].param = param;
                profiles[profileNo].extension = ext;

                if (File.Exists(exe)) comboBox1.Items[profileNo] = profileNo.ToString() + ":  " + ext + "  (" + param + ")";
                else comboBox1.Items[profileNo] = "[DISABLED]";
                if (profileNo == comboBox1.SelectedIndex) chooseProfile(profileNo); 
            }
        }
        private void chooseProfile(int i)
        {
            if (profiles[i] == null)
            {
                MessageBox.Show("The selected profile does not exist.");
                return;
            }
            if  ((profiles[i].path != "")&&(profiles[i].extension!=""))
            {
                zipperPath = profiles[i].path;
                initialArguments = profiles[i].param;
                outFile = outFilePrototype + profiles[i].extension;  
            }
            else
            {
                MessageBox.Show("The selected profile does not contain sufficient settings."); 
            }
        }
        public UserControl1()
        {
            InitializeComponent();
            changeState(0); 
            lstFiles.DragEnter += new DragEventHandler(lstFiles_DragEnter);
            lstFiles.DragDrop += new DragEventHandler(lstFiles_DragDrop);
            lstResult.ItemDrag += new ItemDragEventHandler(lstResult_ItemDrag);  
        }

        void lstResult_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] files = new String[1];
            files[0] =outFile;
            DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy | DragDropEffects.Move);
        }

        void lstFiles_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (a != null)
                {
                    // Extract string from first array element
                    // (ignore all files except first if number of files are dropped).
                    string s = a.GetValue(0).ToString();
                    
                    if (!lstFiles.Items.Contains(s))
                    {
                        string filename = System.IO.Path.GetFileName(s);
                        lstFiles.Items.Add(filename);
                        filePaths.Add(s); 
                    }
                }
                changeState(0);   
            }
            catch (Exception ex)
            {

            }
        }

        void lstFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count > 0)
            {
 
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                changeState(1); 
                worker.RunWorkerAsync();
            }
        }



        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Process();
        }

        private void Process()
        {
            output = "";
            FileInfo fi = new FileInfo(outFile);
            fi.Delete();
            Process rarProcess;
            rarProcess = new Process();
            rarProcess.StartInfo.FileName = zipperPath;
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < lstFiles.Items.Count; i++) b.Append(filePaths[i] + " ");
            rarProcess.StartInfo.Arguments = initialArguments + " " + outFile + " " + b + " " + finalArguments; ;
            //MessageBox.Show(rarProcess.StartInfo.Arguments);  
            rarProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            rarProcess.StartInfo.CreateNoWindow = true;
            rarProcess.StartInfo.UseShellExecute = false;
            rarProcess.StartInfo.RedirectStandardOutput = true;
            rarProcess.OutputDataReceived += new DataReceivedEventHandler(commandPromptProcess_OutputDataReceived);
            rarProcess.EnableRaisingEvents = true; 
            rarProcess.Start();
            rarProcess.BeginOutputReadLine();
            rarProcess.WaitForExit();
            endOperation end = new endOperation(stopProgress); 
            progressBar1.Invoke(end);  

        }
        void stopProgress()
        {
            changeState(2); 
            //MessageBox.Show(output);
        }

        void commandPromptProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            output += e.Data+Environment.NewLine  ; 
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            profiles = new ZipperOptions[3];
            setProfileSettings("0", @"C:\Program Files\WinRAR\rar.exe", @"a -ep1","rar");
            setProfileSettings("1", @"C:\Downloads\ace32.exe", "a -r","ace");
            setProfileSettings("2", @"C:\Program Files\WinZip\wzzip.exe", "-rp", "zip");
            chooseProfile(0);
            comboBox1.SelectedIndex = 0;  

            ListViewItem it=new ListViewItem();
            it.ImageIndex=0;
            lstResult.View = View.LargeIcon;
            lstResult.Scrollable = false; 
            
            lstResult.SmallImageList = imageList1;
            lstResult.LargeImageList = imageList1; 
            lstResult.Items.Add(it);
            lstResult.Scrollable = false; 
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }

        private void lstFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            filePaths.RemoveAt(lstFiles.SelectedIndex);
            lstFiles.Items.RemoveAt(lstFiles.SelectedIndex);
            changeState(0); 

        }
        private void clearresult()
        { 
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseProfile(comboBox1.SelectedIndex);
            changeState(0); 
        }
        private void changeState(int newState)
        {
        //waiting for input
        if (newState==0)
        {
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            lstResult.Visible =false;
            btnGo.Visible = true;
        }
        //processing
        if (newState==1)
        {
            progressBar1.MarqueeAnimationSpeed = 100;
            progressBar1.Visible = true;
            btnGo.Visible = false; 
            lstResult.Visible=false;  
        }
        // finished 
        if (newState == 2)
        {
            progressBar1.Visible = false;
            btnGo.Visible = false;
            lstResult.Visible = true;  
        }
        }


    }
    class ZipperOptions
    {
        public string path;
        public string param;
        public string extension;
    }
}