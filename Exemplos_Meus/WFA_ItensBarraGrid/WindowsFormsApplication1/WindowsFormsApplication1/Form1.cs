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


        private void itemBarraItens1_ClickBotao(object sender)
        {
            MessageBox.Show("1");
        }

        private void buttonEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F8)
                MessageBox.Show("2");
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            panelControl1.Height = (int)spinEdit1.Value;

            if (panelControl1.Height + panelControl2.Height > (groupControl1.Height-23) )
            {
                panelControl1.Height = (groupControl1.Height - 23) - panelControl2.MinimumSize.Height;
            }
        }
    }
}
