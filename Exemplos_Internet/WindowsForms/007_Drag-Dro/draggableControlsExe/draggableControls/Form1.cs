using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using draggableControls.Resources;

namespace draggableControls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void designMode_Click(object sender, EventArgs e)
        {
            dragControl1.DesignMode(true);
            
        }

        private void controlMode_Click(object sender, EventArgs e)
        {
            dragControl1.DesignMode(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}