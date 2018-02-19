using System;
using System.Collections;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //
            //
            foreach (DictionaryEntry item in System.Environment.GetEnvironmentVariables())
            {
                DataRow dataRow = dataTable1.NewRow();
                dataRow[0] = item.Key;
                dataRow[1] = item.Value;
                dataTable1.Rows.Add(dataRow);
            }

            //dataGridView1.da

            
        }
    }
}
