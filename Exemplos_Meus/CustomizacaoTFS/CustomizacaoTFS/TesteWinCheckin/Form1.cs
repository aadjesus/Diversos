using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TesteWinCheckin.wsCheckin;
using System.IO;
using TFSObjects;


namespace TesteWinCheckin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = textBox1.Text;
            var res = openFileDialog1.ShowDialog();            
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open);
            StreamReader sw = new StreamReader(fs);            
            string s = sw.ReadToEnd();
            fs.Close();
            Servicos.WorkItemValidation.ValidaWorkItem(s);
            

        }

        

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task.BuscaResponsavel(@"Globus\CGS - Cargas",Task.tipoTask.EspecificacaoNegocio);           
        }
    }
}
