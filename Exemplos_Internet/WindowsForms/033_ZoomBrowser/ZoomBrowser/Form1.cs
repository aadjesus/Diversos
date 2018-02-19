using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZoomBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

       

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ZoomValue.Enabled = false;
            this.myBrowser1.Navigate("http://google.com");
            this.myBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(myBrowser1_DocumentCompleted);
        }

        void myBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.ZoomValue.Enabled = true;
        }

        private void ZoomValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.myBrowser1.Zoom(int.Parse(this.ZoomValue.Text));
        }
    }
}