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
using ConsultaHistDetec.seg;

namespace ConsultaHistDetec
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //    //
            //    //

            try
            {
                //gfo.GestaoDeFrotaOnLineWS aa = new gfo.GestaoDeFrotaOnLineWS();
                //aa.Url = "http://192.1.1.7:8183/Alessandro.Augusto/gestaodefrotaonlinews.asmx";
                //aa.ConectaUsuarioWap();
            
                //seg.SegurancaWS conts = new seg.SegurancaWS();
                //conts.UserAgent = "QWEQWEQW";
                //conts.Url = "http://192.1.1.7:8183/Alessandro.Augusto/SegurancaWS.asmx";
                //conts.Url = "http://localhost:54021/SegurancaWS.asmx";
            
                             
                //conts.LogarUsuario(txtUsuario.Text.Trim(), txtSenha.Text.Trim(), "GFO","");

                ctrl.ControleWS cont = new ctrl.ControleWS();
                //cont.Url = "http://192.1.1.7:8183/Alessandro.Augusto/ControleWS.asmx";
                //cont.Url = "http://localhost:54021/ControleWS.asmx";

                //ctrl.SistemaDTO vars = (ctrl.SistemaDTO)cont.GenericoConsultaPorChave("SistemaDTO", new ctrl.SistemaDTO() { Sistema = "GFO" });
                ctrl.SistemaDTO vars = new ctrl.SistemaDTO();
                vars.DescricaoDoSistema = txtNovaDesc.Text.Trim();


                
                cont.UserAgent = "XXXXXX";

                cont.GenericoSalvarOuAlterar(vars);
                
                MessageBox.Show("ok");
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message);
            }


        }

        void cont_GenericoSalvarOuAlterarCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("1");
        }
    }
}
