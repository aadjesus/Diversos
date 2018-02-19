using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            localhost2.GestaoDeFrotaOnLineWS xxx = new localhost2.GestaoDeFrotaOnLineWS();
            var xx = xxx.GenericoRetornarTodos("ValorChaveCRUD", "ItinerarioDTO");           

            MessageBox.Show(xx.Count().ToString());
        }
        
        private void ShowProgressBar(int percentage)
        {
            form1.progressBar1.Value = currentCount;// percentage;
            form1.progressBar1.Refresh();
        }

        int currentCount = 0;
        int totalCount = 1000000;
        delegate void MyDelegate(int percentage);

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentCount++;
            Invoke(new MyDelegate(ShowProgressBar), (Int32)(currentCount * 100 / totalCount));
        
        }


    }
}
