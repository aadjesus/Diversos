using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnviarSMS.GFO;
using EnviarSMS.human;
using System.Windows.Threading;

namespace EnviarSMS
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);            
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            gfo.ConectaUsuarioWap();
            this.label1.Content = "Consultando";
            mensagensSmsDTO = gfo.RetornarMensagensSmsNaoEnviadas();
            gridControl1.DataSource = mensagensSmsDTO;
            if (mensagensSmsDTO.Length > 0)
            {
                this.label1.Content = "Enviando";
                enviarSMS(buttonWebService);
                this.labelQtde.Content = "Qtde :" + ListBox1.Items.Count.ToString("000");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            //FGlobus.Comum.ChaveCRUD.LiberaAcessoChaveCRUD();
            gfo.ConectaUsuarioWap();
            mensagensSmsDTO = gfo.RetornarMensagensSmsNaoEnviadas();
            gridControl1.DataSource = mensagensSmsDTO;
        }


        private MensagensSmsDTO[] mensagensSmsDTO;
        private GFO.GestaoDeFrotaOnLineWS gfo = new EnviarSMS.GFO.GestaoDeFrotaOnLineWS();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void buttonWebService_Click(object sender, RoutedEventArgs e)
        { 
        }

        private object RetornaParametrosDoSms(string msg)
        {
            int po = msg.ToUpper().IndexOf("PO");
            int ln = msg.ToUpper().IndexOf("LN");
            string ponto = "";
            string linha = "";
            if (po == 0 && ln > 0)
            {
                ponto = msg.Substring(po + 2, ln - 2);
                linha = msg.Substring(ln + 2);
            }
            else if (ln == 0 && po > 0)
            {
                ponto = msg.Substring(po + 2);
                linha = msg.Substring(ln + 2, po - 2);
            }
            else if (po == 0 && ln == -1)
                ponto = msg.Substring(po + 2);

            return new object[] 
            {
                ponto, 
                linha
            };

        }

        private string RetornarCorpoDoSms(object parametros)
        {
            try
            {
                int ponto = Convert.ToInt16(((object[])(parametros))[0]);
                string linha = Convert.ToString(((object[])(parametros))[1]);
                string msg = "";

                sRetornoSMS[] retornoSMS = gfo.RetornarVeiculosQueVaoChegarNoLocalDaLinha(ponto, linha);
                foreach (var j in retornoSMS)
                {
                    msg += (string.IsNullOrEmpty(msg) ? "" : "\n") +
                        j.NomeAbrevLinha.Trim() +
                        (j.Sentido.Equals("V") ? "<" : ">") +
                        j.TempoPrevisto;
                }
                return msg;
            }
            catch (Exception)
            {
                return null;
            }


        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            Form1 x = new Form1();
            x.Show();
        }

        private void enviarSMS(object sender)
        {
            List<MensagensSmsDTO> baixarMensagensSms = new List<MensagensSmsDTO>();
            int idsms = 0;
            try
            {
                foreach (var i in mensagensSmsDTO)
                {
                    idsms = i.IdSms;

                    ParametrosSmsDTO parametrosSmsDTO = gfo.RetornarParametrosSmsPorChave(i.CodIntParam);

                    string mensagem = RetornarCorpoDoSms(RetornaParametrosDoSms(i.Mensagem.Trim()));
                    bool baixaSms = true;
                    if (string.IsNullOrEmpty(mensagem))
                        i.StatusEnvio = "Dados incorretos";
                    else
                    {
                        if (sender == buttonWebService)
                        {
                            SendSmsRequest request = new SendSmsRequest();
                            request.msg = mensagem;
                            request.account = parametrosSmsDTO.Conta;
                            request.code = parametrosSmsDTO.CodigoIntegracao;
                            request.mobile = i.NumCelUsuario;
                            request.id = i.IdIntegradora.ToString();
                            request.callbackOption = 0;

                            Sms_BindingImplService sms = new Sms_BindingImplService();
                            i.StatusEnvio = sms.sendSms(request);
                            ListBox1.Items.Add(i.StatusEnvio);
                        }
                        else
                        {
                            string uri = "http://system.human.com.br/GatewayIntegration/msgSms.do?";
                            uri += "dispatch=" + "send";
                            uri += "&account=" + parametrosSmsDTO.Conta;
                            uri += "&code=" + parametrosSmsDTO.CodigoIntegracao;
                            uri += "&msg=" + mensagem;
                            uri += "&to=" + i.NumCelUsuario;
                            uri += "&callbackOption=" + "0";
                            //WebBrowser1.Source = new Uri("http://aguiamay.com.br/human/Default.aspx?id=ss7&from=4444444444&to=sss28413&msg=PO 4008&account=sasdfsassBGM.HG&dataHoraReceb=13/1/sdsd2010 19:00");
                            //WebBrowser1.Refresh() ;
                            
                            i.StatusEnvio = "a";// ((mshtml.HTMLDocumentClass)(WebBrowser1.Document)).activeElement.outerText;
                        }
                    }

                    if (baixaSms)
                        baixarMensagensSms.Add(i);
                }

                if (baixarMensagensSms.Count > 0)
                    gfo.BaixarMensagensSms(baixarMensagensSms.ToArray());

            }
            catch (Exception)
            {
                MessageBox.Show("Erro no envio msg id: " + idsms.ToString());
            }



        }


    }
}

// 
