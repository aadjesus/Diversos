using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiThreadExemplo
{
    public partial class clsfrmPrincipal : Form
    {

        private delegate void delegateInsereTexto(string Texto);

        private bool blnAguardaThread = false;

        public clsfrmPrincipal()
        {

            InitializeComponent();

        }

        private void clsfrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {

            //* Fecha a aplicação
            System.Environment.Exit(System.Environment.ExitCode);

        }

        private void fncMostraMensagem(string strMensagem)
        {

            //* Mostra um mensagem box
            MessageBox.Show(strMensagem);

            return;

        }

        private void butExecutarSemThread_Click(object sender, EventArgs e)
        {

            //* Limpa a textbox
            textBoxEvento.Text = "";

            for (long lngQuantiadeMensagens = 0; lngQuantiadeMensagens < 5; lngQuantiadeMensagens++)
            {

                //* Executa o metodo
                fncMostraMensagem(lngQuantiadeMensagens.ToString());

                //* Insere o indice no textbox
                textBoxEvento.Text += lngQuantiadeMensagens.ToString() + Environment.NewLine;

            }

            return;

        }

        private void butExecutarThread_Click(object sender, EventArgs e)
        {

            System.Threading.Thread[] threadMensagem;

            //* Limpa a textbox
            textBoxEvento.Text = "";

            for (long lngQuantiadeMensagens = 0; lngQuantiadeMensagens < 5; lngQuantiadeMensagens++)
            {

                //* Incrementa a quantidade de itens do array
                threadMensagem = new System.Threading.Thread[lngQuantiadeMensagens + 1];

                //* Cria uma nova ThreadStart com o metodo fncMensagem
                System.Threading.ThreadStart threadstartMensagem = delegate { fncMostraMensagem(lngQuantiadeMensagens.ToString()); };

                //* Instancia a Thread
                threadMensagem[lngQuantiadeMensagens] = new System.Threading.Thread(threadstartMensagem);

                //* Inicia a Thread
                threadMensagem[lngQuantiadeMensagens].Start();

                //* Insere o indice no textbox
                textBoxEvento.Text += lngQuantiadeMensagens.ToString() + Environment.NewLine;

            }

            return;

        }

        //**************************************************************

        private void fncInsereTextoEvento(string strTexto)
        {

            //* Insere o texto na textbox
            textBoxEvento.Text += strTexto;

        }

        private void fncInsereTextoThread(bool blnInsereDelegate)
        {

            for (long lngCountMensagem = 0; lngCountMensagem < 10; lngCountMensagem++)
            {
                //* Verifica se deve inserir o delegate
                if (blnInsereDelegate == true)
                {

                    //* Insere o texto na textbox
                    this.Invoke(new delegateInsereTexto(fncInsereTextoEvento), lngCountMensagem.ToString() + Environment.NewLine);

                }
                else
                {
                    //* Insere o texto na textbox
                    textBoxEvento.Text += lngCountMensagem.ToString() + Environment.NewLine;

                }

            }

            return;

        }

        private void butNaoInteragirThreads_Click(object sender, EventArgs e)
        {

            System.Threading.Thread threadMensagem;

            System.Threading.ThreadStart threadstartMensagem;

            //* Limpa a textbox
            textBoxEvento.Text = "";

            //* Cria uma nova ThreadStart com o metodo fncInsereMensagemTexto não sincronizado
            threadstartMensagem = delegate { fncInsereTextoThread(false); };

            //* Instancia a Thread
            threadMensagem = new System.Threading.Thread(threadstartMensagem);

            //* Inicia a Thread
            threadMensagem.Start();

        }

        private void butInteragirThreads_Click(object sender, EventArgs e)
        {

            System.Threading.Thread threadMensagem;

            System.Threading.ThreadStart threadstartMensagem;

            //* Limpa a textbox
            textBoxEvento.Text = "";

            //* Cria uma nova ThreadStart com o metodo fncInsereMensagemTexto não sincronizado
            threadstartMensagem = delegate { fncInsereTextoThread(true); };

            //* Instancia a Thread
            threadMensagem = new System.Threading.Thread(threadstartMensagem);

            //* Inicia a Thread
            threadMensagem.Start();

        }

        //********************************************************************************

        private void fncInsereMensagemTextoSincronizado(long lngIndex, bool blnSincronizar)
        {

            for (long lngCountMensagem = 0; lngCountMensagem < 10; lngCountMensagem++)
            {
                //* Bloqueia o bloco atual pela thread
                lock (this)
                {

                    //* Verifica se deve executar a sincronizaçao entre thread
                    if (blnSincronizar == true)
                    {

                        System.Threading.Monitor.Pulse(this);

                        blnAguardaThread = true;

                    }

                    //* Insere o texto na textbox
                    this.Invoke(new delegateInsereTexto(fncInsereTextoEvento), "Thread " + lngIndex.ToString() + ": " + lngCountMensagem.ToString() + Environment.NewLine);

                    //* Verifica se deve executar a sincronizaçao entre thread
                    //* Verifica se deve aguardar outra thread ser executada
                    if (blnSincronizar == true && blnAguardaThread == true)
                    {

                        blnAguardaThread = false;

                        //* Aguarda a outra thread terminar
                        System.Threading.Monitor.Wait(this);

                    }

                    
                }

            }

            return;

        }

        private void butNaoSincronizarThread_Click(object sender, EventArgs e)
        {

            System.Threading.Thread[] arrthreadMensagem;

            System.Threading.ThreadStart[] arrthreadstartMensagem;

            //* Limpa a textbox
            textBoxEvento.Text = "";

            //* Redimensiona o array
            arrthreadMensagem = new System.Threading.Thread[2];
            arrthreadstartMensagem = new System.Threading.ThreadStart[2];

            //* Cria uma nova ThreadStart com o metodo fncInsereMensagemTexto não sincronizado
            arrthreadstartMensagem[0] = delegate { fncInsereMensagemTextoSincronizado(0,false); };

            //* Instancia a Thread
            arrthreadMensagem[0] = new System.Threading.Thread(arrthreadstartMensagem[0]);

            //* Inicia a Thread
            arrthreadMensagem[0].Start();

            //* Cria uma nova ThreadStart com o metodo fncInsereMensagemTexto não sincronizado
            arrthreadstartMensagem[1] = delegate { fncInsereMensagemTextoSincronizado(1, false); };

            //* Instancia a Thread
            arrthreadMensagem[1] = new System.Threading.Thread(arrthreadstartMensagem[1]);

            //* Inicia a Thread
            arrthreadMensagem[1].Start();

            return;
            
        }

        private void butSincronizarThread_Click(object sender, EventArgs e)
        {

            System.Threading.Thread[] arrthreadMensagem;

            System.Threading.ThreadStart[] arrthreadstartMensagem;

            //* Limpa a textbox
            textBoxEvento.Text = "";

            //* Redimensiona o array
            arrthreadMensagem = new System.Threading.Thread[2];
            arrthreadstartMensagem = new System.Threading.ThreadStart[2];

            //* Cria uma nova ThreadStart com o metodo fncInsereMensagemTexto sicronizado
            arrthreadstartMensagem[0] = delegate { fncInsereMensagemTextoSincronizado(0, true); };

            //* Instancia a Thread
            arrthreadMensagem[0] = new System.Threading.Thread(arrthreadstartMensagem[0]);

            //* Cria uma nova ThreadStart com o metodo fncInsereMensagemTexto sincronizado
            arrthreadstartMensagem[1] = delegate { fncInsereMensagemTextoSincronizado(1, true); };

            //* Instancia a Thread
            arrthreadMensagem[1] = new System.Threading.Thread(arrthreadstartMensagem[1]);

            //* Inicia a Thread
            arrthreadMensagem[0].Start();

            //* Inicia a Thread
            arrthreadMensagem[1].Start();

            return;
            
        }

    }
}