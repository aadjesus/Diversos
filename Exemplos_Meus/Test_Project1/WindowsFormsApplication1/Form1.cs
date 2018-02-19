using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textEdit1.Text = "";
            buttonEdit1.Text = "";
            dateEdit1.Text = "";
            checkEdit1.Checked = false;
            radioGroup1.SelectedIndex = -1;
            timeEdit1.Text = "";
            calcEdit1.Text = "";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Confirma gravação");
            simpleButton2.PerformClick();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Confirma exclusão!");
            simpleButton2.PerformClick();
        }
    }
}
