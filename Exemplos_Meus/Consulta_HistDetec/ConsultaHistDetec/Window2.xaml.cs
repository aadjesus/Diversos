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
using System.Windows.Shapes;
using FGlobus.Util;
using ConsultaHistDetec.gfo;
using FGlobus.Componentes.WinForms.ws.seguranca;
using FGlobus.Componentes.WinForms;

namespace ConsultaHistDetec
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {


            seg.SegurancaWS segWP = new seg.SegurancaWS();
            segWP.Proxy = Inicializacao.WebProxyApp;
            segWP.Timeout = System.Threading.Timeout.Infinite;

            segWP.HeaderAutenticacaoValue = new seg.HeaderAutenticacao();
            segWP.HeaderAutenticacaoValue.ValorChaveCRUD = "ValorChaveCRUD";

            var concedenteDTO = segWP.ValidarAcessoAoSistema("GFO", FGlobus.Util.Util.Estacao, "GFO", FGlobus.Util.Util.UsuarioOS, false);
            bool xxx = segWP.VerificarDataLimiteDeUso("GFO");
            segWP.LiberarLicenca("AAUGUSTO", "GFO", "ALESSANDRO.AUGUSTO");

            if (concedenteDTO == null || xxx)
            {

            }

            gfo.GestaoDeFrotaOnLineWS aa = new gfo.GestaoDeFrotaOnLineWS();
            //aa.ConectaUsuarioWap();
            gridControl1.DataSource = aa.GenericoRetornarTodos<MensagemValidadorDTO>();
            }
            catch (Exception ex)
             {

                throw ex;
            }



        }
    }
}
