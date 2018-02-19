using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutenticacaoWebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                intWs.IntegracaoWS intws = new intWs.IntegracaoWS();
                intws.SalvarOuAlterarListaTabelaIntegradora2(
                    new intWs.AutenticacaoWebService()
                    {
                        Token = this.textBox1.Text
                    },
                    new intWs.TabelaIntegradora[] { new intWs.TabelaIntegradora() });

                MessageBox.Show("ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat(ex));
                string token = "";
                string _token = "";
                decimal shortCode=0;
                //throw new Exception(
                //    string.Concat("Erro: Acesso não autorizado.", "\n calc:", token, "\n info:", _token, , "\n sho:" ,shortCode));
            }

        }
    }


}
