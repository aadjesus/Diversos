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
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Charts;

namespace WPF_Graficos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            usrCtrlGraficoGerencial.ListaGrafico = new WPF_Graficos.UsrCtrlGraficoGerencial.DadosGrafico[] 
            { 
                new WPF_Graficos.UsrCtrlGraficoGerencial.DadosGrafico() { DescOcorHorario="DescOcorHorario", PesoOcorHorario = 1,                                                        CodigoLinha = "001", Dia="01", Hora="01", Localidade="01", PrefixoVeic="01",NomeLinha="a-001", TotalPeso = 1, PesoOcorMensagem = 0, PesoOcorProgNaoRea = 0, PesoOcorReaNaoProg =  0},
                new WPF_Graficos.UsrCtrlGraficoGerencial.DadosGrafico() { DescOcorHorario="DescOcorHorario", PesoOcorHorario = 2,                                                        CodigoLinha = "001", Dia="01", Hora="02", Localidade="02", PrefixoVeic="01",NomeLinha="a-001", TotalPeso = 1, PesoOcorMensagem = 0, PesoOcorProgNaoRea = 0, PesoOcorReaNaoProg =  0},
                new WPF_Graficos.UsrCtrlGraficoGerencial.DadosGrafico() {                                                                                                                CodigoLinha = "002", Dia="22", Hora="22", Localidade="03", PrefixoVeic="01",NomeLinha="b-002", TotalPeso = 1, PesoOcorMensagem = 1, PesoOcorProgNaoRea = 0, PesoOcorReaNaoProg =  0},
                new WPF_Graficos.UsrCtrlGraficoGerencial.DadosGrafico() {                                                                                                                CodigoLinha = "002", Dia="22", Hora="22", Localidade="05", PrefixoVeic="01",NomeLinha="b-002", TotalPeso = 2, PesoOcorMensagem = 0, PesoOcorProgNaoRea = 1, PesoOcorReaNaoProg =  0},
                new WPF_Graficos.UsrCtrlGraficoGerencial.DadosGrafico() {                                                        DescOcorComboio="DescOcorComboio",  PesoOcorComboio = 5,CodigoLinha = "002", Dia="22", Hora="12", Localidade="05", PrefixoVeic="12",NomeLinha="c-002", TotalPeso = 3, PesoOcorMensagem = 0, PesoOcorProgNaoRea = 0, PesoOcorReaNaoProg =  1},
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartControl chartControl1 = usrCtrlGraficoGerencial.ChartControl;
            Form1 aa = new Form1();
           
        }

        void sl_CreateDetail(object sender, CreateAreaEventArgs e)
        {
        }


    }
}
