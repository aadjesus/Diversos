using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace WFA_TraducaoTela
{
    public partial class Form1 : Form
    {
        string culture = "";
        public Form1(string culture)
        {
            CultureInfo ci = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;            
            InitializeComponent();

            checkBox1_CheckedChanged(checkBox1, null);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = checkBox1.Checked
                ? "Código"
                : "Informe o código";
        }


    }
}
