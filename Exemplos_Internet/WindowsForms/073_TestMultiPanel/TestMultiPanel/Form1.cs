using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestMultiPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
                multiPanel1.SelectedPage = pageButtons;
            else if (comboBox3.SelectedIndex == 1)
                multiPanel1.SelectedPage = pageCheckboxes;
            else
                multiPanel1.SelectedPage = pageComboBoxes;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;
        }
    }
}