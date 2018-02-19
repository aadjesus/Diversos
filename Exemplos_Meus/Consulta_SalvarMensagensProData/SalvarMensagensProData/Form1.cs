using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalvarMensagensProData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //integracao.sMensagens
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           // dataSet1.Tables

            //sMensagensBindingSource.DataSource

            try
            {
                new integracao.IntegracaoWS()
                    .SalvarMensagensProData(sMensagensBindingSource.List
                        .OfType<integracao.sMensagens>()
                        .ToArray());
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }
        }
    }
}
