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
using ConsultaHistDetec.seg;

namespace ConsultaHistDetec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    int[] listaCodLocalidade = new int[] { 1 };
        //    int[] listaCodIntLinha = new int[] { 1 };
        //    int[] listaCodigoVeic = new int[] { 1 };

        //    //http://localhost:54021/ControleWS.asmx
        //    //gfo.GestaoDeFrotaOnLineWS webservice = new gfo.GestaoDeFrotaOnLineWS();
        //    //webservice.Url = "";
        //    //webservice.ConectaUsuarioWap();

        //    //gridControl1.DataSource = webservice.RetornarHistoricoDeDeteccoes(
        //    //            new DateTime(2010, 10, 1, 8, 0, 0),
        //    //            new DateTime(2010, 10, 1, 9, 0, 0),
        //    //            "A",
        //    //            listaCodLocalidade,
        //    //            listaCodIntLinha,
        //    //            listaCodigoVeic);

        //    //webservice.RetornarUsuarios

        //    //gfo.GestaoDeFrotaOnLineWS aa = new gfo.GestaoDeFrotaOnLineWS();
        //    //aa.ConectaUsuarioWap();

        //    seg.SegurancaWS conts = new seg.SegurancaWS();
        //    conts.HeaderAutenticacaoValue = new HeaderAutenticacao();
        //    conts.HeaderAutenticacaoValue.ValorChaveCRUD = "ValorChaveCRUD";

        //    conts.LogarUsuario("BGMRODOTEC", "1", "GFO");

        //    ctrl.ControleWS cont = new ctrl.ControleWS();
        //    //cont.SetUsuarioSessao("xxxxxxx");

        //    ctrl.SistemaDTO vars = (ctrl.SistemaDTO)cont.GenericoConsultaPorChave("SistemaDTO", new ctrl.SistemaDTO() { Sistema="GFO"});
        //    cont.GenericoSalvarOuAlterar(vars);

        //    if (vars == null)
        //    {

        //    }


        //}
    }
}
