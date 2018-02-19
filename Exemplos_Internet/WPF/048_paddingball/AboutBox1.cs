using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PaddingBall
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
        }
        [STAThread]
        public static void Main1()
        {
            AboutBox1 ab1 = new AboutBox1();
            ab1.ShowDialog();

        }
 
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
