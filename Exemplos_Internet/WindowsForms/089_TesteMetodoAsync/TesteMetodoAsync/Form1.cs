using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TesteMetodoAsync
{
    public partial class Form1 : Form
    {
        private List<Xto> _tabelaXto = new List<Xto>();        
        private delegate void DelegateVisualizacaoProgress(bool visible);
        private delegate void DelegateMsgLbl(string msg);

        public Form1()
        {
            InitializeComponent();
        }

        private void VisualizacaoProgressBar(bool visible)
        {
            this.pnlProgress.Visible = visible;
            this.button1.Enabled = !visible;
        }

        private void MsgLbl(String msg)
        {
            this.lblProgressBar.Text = msg;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VisualizacaoProgressBar(true);
            
            DelegateVisualizacaoProgress dVisualizacaoProgress = new DelegateVisualizacaoProgress(VisualizacaoProgressBar);
            DelegateMsgLbl dMsgLbl = new DelegateMsgLbl(MsgLbl);

            Action dAcao = delegate()
            {
                int total = 1000000;

                for (int i = 1; i <= total; i++)
                {
                    _tabelaXto.Add(new Xto() { Codigo = i, Descricao = String.Format("Descrição {0}", i) });                    

                    if (this.lblProgressBar.InvokeRequired)
                        this.lblProgressBar.Invoke(dMsgLbl, new object[] { String.Format("Gravando {0} de {1}.", i, total) });
                }
            };

            Action<IAsyncResult> dAcaoConcluida = delegate(IAsyncResult ar)
            {
                if (this.pnlProgress.InvokeRequired)
                    this.pnlProgress.Invoke(dVisualizacaoProgress, new object[] { false });

                MessageBox.Show("Foi!");
            };

            new MethodInvoker(dAcao).BeginInvoke(new AsyncCallback(dAcaoConcluida), null);
        }
    }

    public partial class Xto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}

