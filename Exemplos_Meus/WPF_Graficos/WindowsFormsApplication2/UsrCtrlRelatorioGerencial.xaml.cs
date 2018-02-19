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
using DevExpress.Xpf.Charts;
using System.Windows.Controls.Primitives;
using System.Data;
using Globus5.WPF.Comum.ws.gestaoDeFrotaOnline;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;

namespace Globus5.WPF.Sistemas.GestaoDeFrotaOnline
{
    /// <summary>
    /// Interaction logic for UsrCtrlRelatorioGerencial.xaml
    /// </summary>
    public partial class UsrCtrlRelatorioGerencial : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor Default
        /// </summary>
        public UsrCtrlRelatorioGerencial()
        {
            InitializeComponent();

        }

        #endregion

        #region Atributos

        private List<DadosGrafico> _listaDadosGrafico = new List<DadosGrafico>();
        public Globus5.WPF.Sistemas.GestaoDeFrotaOnline.Resultados.HistoricoDeDeteccoes _historicoDeDeteccoes;
        private UsrCtrlGraficoRelatorioGerencial _usrCtrlGraficoRelatorioGerencial = null;

        #endregion

        #region Metodos

        /// <summary>
        /// Popula lista do grafico
        /// </summary>
        /// <param name="linhasTabela">Objeto do tipo object</param>
        internal void PopulaGrafico(IEnumerable<DadosGrafico> linhasTabela)
        {
            try
            {
                usrCtrlGraficoRelatorioGerencial.ListaGrafico.AddRange(linhasTabela.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// (Get) Retorna a lista com os graficos marcados para impressa
        /// </summary>
        internal ChartControl[] ListaGraficosParaImpressao
        {
            get
            {
                return this.dockLayoutManager.GetItems()
                    .OfType<LayoutPanel>()
                    .Select(s => (s.Content as UsrCtrlGraficoRelatorioGerencial))
                    .Where(w => w.tglBtnImprimir.IsChecked ?? true)
                    .Select(s => s.ChartControl)
                    .ToArray();
            }
        }

        internal void Imprimir()
        {
            DevExpress.Xpf.Printing.SimpleLink simpleLink = new DevExpress.Xpf.Printing.SimpleLink();


            simpleLink.DetailCount = ListaGraficosParaImpressao.Count();
            simpleLink.DetailTemplate = (DataTemplate)Resources["Data"];
            simpleLink.CreateDetail += delegate(object sender, DevExpress.Xpf.Printing.CreateAreaEventArgs e)
            {
                ChartControl chartControl = ListaGraficosParaImpressao[e.DetailIndex];

                VisualBrush visualBrush = new VisualBrush(chartControl);
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                drawingContext.DrawRectangle(visualBrush, null, new Rect(0, 0, chartControl.ActualWidth, chartControl.ActualHeight));
                drawingContext.Close();
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)chartControl.ActualWidth, (int)chartControl.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(drawingVisual);
                e.Data = renderTargetBitmap;
            };

            simpleLink.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
            simpleLink.Landscape = true;
            simpleLink.CreateDocument(true);

            Window window = new Window();

            DevExpress.Xpf.Printing.DocumentPreview documentPreview = new DevExpress.Xpf.Printing.DocumentPreview();
            documentPreview.Model = new DevExpress.Xpf.Printing.LinkPreviewModel(simpleLink);

            window.ShowInTaskbar = false;
            window.Title = "Impressão relatório gerencial";

            window.WindowState = WindowState.Maximized;
            window.Content = documentPreview;

            window.ShowDialog();
        }

        internal void PopulaHistoricoDeDeteccoes(Resultados.HistoricoDeDeteccoes historicoDeDeteccoes)
        {
            _historicoDeDeteccoes = historicoDeDeteccoes;

            //this.chtCtrlRankingDeLinhas.UsrCtrlRelatorioGerencial = this;
            //this.chtCtrlRankingDeLinhasDias.UsrCtrlRelatorioGerencial = this;
            //this.chtCtrlLinhasFaixaHoraria.UsrCtrlRelatorioGerencial = this;
            //this.chtCtrlPontoDeControle.UsrCtrlRelatorioGerencial = this;
            //this.chtCtrlVeiculos.UsrCtrlRelatorioGerencial = this;
            //this.chtCtrlOcorrencias.UsrCtrlRelatorioGerencial = this;
            //this.chtCtrlOcorrencias.UsrCtrlRelatorioGerencial = this;
        }

        internal void LimpaDocPanel()
        {
            try
            {
                this.usrCtrlGraficoRelatorioGerencial.LimpaControles();
                this.dockLayoutManager.GetItems()
                    .OfType<LayoutPanel>()
                    .Where(w => w.ShowCloseButton)
                    .ToList()
                    .ForEach(f => f.Closed = true);
                this.dockLayoutManager.ClosedPanels.Clear();
            }
            catch (Exception)
            {
            }
        }

        internal void HabilitarBotaoDeImpressao()
        {
            _historicoDeDeteccoes.smplBtnImprimir.Enabled = ListaGraficosParaImpressao.Count() != 0;
        }

        #endregion

        #region Metodos tela

        private void btnParar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.rwDfntnConsultando.Height = new GridLength(0);
                this.usrCtrlGraficoRelatorioGerencial.LiberaNavegacao = false;
                _historicoDeDeteccoes.PararConsulta();
            }
            catch (Exception)
            {
            }
        }

        #endregion

    }

}

//namespace Globus5.WPF.Sistemas.GestaoDeFrotaOnline.Resultados
//{
//    public class HistoricoDeDeteccoes
//    {
//        public void PararConsulta()
//        {

//        }
//        public FGlobus.Componentes.WinForms.SimpleButtonBGM smplBtnImprimir = new FGlobus.Componentes.WinForms.SimpleButtonBGM();
//    }
//}