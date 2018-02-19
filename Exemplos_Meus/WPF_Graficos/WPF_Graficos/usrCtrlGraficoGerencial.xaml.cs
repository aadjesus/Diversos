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
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.Charts;
using System.Reflection;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Docking;
using System.ComponentModel;

namespace WPF_Graficos
{
    /// <summary>
    /// Interaction logic for usrCtrlGraficoGerencial.xaml
    /// </summary>
    public partial class UsrCtrlGraficoGerencial : UserControl
    {
        #region class

        public class Grafico
        {
            public string NomeArgument { get; set; }
            public string ArgumentDataMember { get; set; }
            public double ValueDataMember { get; set; }
        }

        public class DadosGrafico
        {
            public DadosGrafico()
            {
                CodigoLinha = "";
                NomeLinha = "";
                Dia = "";
                Hora = "";
                Localidade = "";
                DescLocalidade = "";
                PrefixoVeic = "";
                PlacaAtualVeic = "";
                TotalPeso = 0;
            }

            public string CodigoLinha { get; set; }
            public string NomeLinha { get; set; }
            public string Dia { get; set; }
            public string Hora { get; set; }
            public string Localidade { get; set; }
            public string DescLocalidade { get; set; }
            public string PrefixoVeic { get; set; }
            public string PlacaAtualVeic { get; set; }
            public int PesoOcorHorario { get; set; }
            public int PesoOcorComboio { get; set; }
            public int PesoOcorMensagem { get; set; }
            public int PesoOcorProgNaoRea { get; set; }
            public int PesoOcorReaNaoProg { get; set; }
            public int TotalPeso { get; set; }
            public string DescOcorHorario { get; set; }
            public string DescOcorComboio { get; set; }
            public string DescOcorMensagem { get; set; }
            public string DescOcorProgNaoRea { get; set; }
            public string DescOcorReaNaoProg { get; set; }
        }

        #endregion

        #region Contrutor

        /// <summary>
        /// Construtor defalt
        /// </summary>
        public UsrCtrlGraficoGerencial()
        {
            InitializeComponent();
        }

        #endregion

        #region Atributos

        private IEnumerable<DadosGrafico> _listaGrafico;
        private IEnumerable<DadosGrafico> _listaGraficoSerie;
        private List<Grafico> _parametrosOcorrencia = new List<Grafico>();
        private ChartControl _chartControl;
        private SeriesPoint _seriesPoint;
        private string _nomeArgument = "";
        private int _mouseClicouNaSerie;
        private const int CLICOU_NA_SERIE = 200;
        private List<object> _parametros = new List<object>() 
        {
            new string[] {"CodigoLinha","NomeLinha",     "Linha: "},
            new string[] {"Dia",        "Dia",           "Dia: "},
            new string[] {"Hora",       "Hora",          "Hora: "},
            new string[] {"Localidade", "DescLocalidade","Local: " },
            new string[] {"PrefixoVeic","PlacaAtualVeic","Veículo: "},
            new string[] {"",           "",              "Ocorrência: "},
        };

        #endregion

        #region Propriedades

        protected UsrCtrlGraficoGerencial CopiaGraficoGerencial;

        public IEnumerable<DadosGrafico> ListaGrafico
        {
            get { return _listaGrafico; }
            set { _listaGrafico = value; }
        }

        protected List<Grafico> ParametrosOcorrencia
        {
            get { return _parametrosOcorrencia; }
            set { _parametrosOcorrencia = value; }
        }

        public ChartControl ChartControl
        {
            get { return _chartControl; }
            set { _chartControl = value; }
        }

        protected SeriesPoint SeriesPoint
        {
            get { return _seriesPoint; }
            set { _seriesPoint = value; }
        }

        protected string NomeArgument
        {
            get { return _nomeArgument; }
            set { _nomeArgument = value; }
        }

        #endregion

        #region Metodos tela
        
        private void ChartControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                _chartControl = sender as ChartControl;
                if (_chartControl != null)
                {
                    _seriesPoint = _chartControl.CalcHitInfo(e.GetPosition(_chartControl)).SeriesPoint;

                    if (_seriesPoint == null)
                    {
                        this.popupInfoSerie.IsOpen = false;
                        Cursor = Cursors.Arrow;
                    }
                    else
                    {
                        _nomeArgument = _seriesPoint.Argument;
                        this.popupInfoSerie.Placement = PlacementMode.Mouse;
                        this.popupInfoSerie.IsOpen = true;

                        string nomePropriedade = "";
                        string descPropriedade = "";
                        if (this.comboBoxTipoGrafico.SelectedIndex.Equals(5))
                        {
                            nomePropriedade = RetornaNomeCampo(_seriesPoint.Value, _seriesPoint.Argument);
                            descPropriedade = nomePropriedade;
                        }
                        else
                        {
                            nomePropriedade = RetornaNomeCampo(0);
                            descPropriedade = RetornaNomeCampo(1);
                        }

                        _listaGraficoSerie = _listaGrafico
                                .Where(w => RetornaValorPropriedade<string>(w, nomePropriedade) != null &&
                                            RetornaValorPropriedade<string>(w, nomePropriedade).Equals(_nomeArgument));

                        this.contentControlTotalSerie.DataContext = RetornaDados(_listaGraficoSerie,
                            descPropriedade);

                        Cursor = Cursors.Hand;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ChartControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.popupInfoSerie.IsOpen = false;
        }

        private void comboBoxTipoGrafico_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                #region Delegate de agrupamento
                
                Func<string, string, IEnumerable<Grafico>> delegateGrupo = delegate(string nomePropriedadeGrupo, string nomePropriedadePeso)
                {
                    try
                    {
                        return _listaGrafico
                            .Where(w => !String.IsNullOrEmpty(RetornaValorPropriedade<string>(w, nomePropriedadeGrupo)))
                            .GroupBy(g => RetornaValorPropriedade<string>(g, nomePropriedadeGrupo), (grupo, lista) => new
                            {
                                ArgumentDataMember = grupo.ToString(),
                                ValueDataMember = Math.Round(lista
                                    .Average(a => RetornaValorPropriedade<int>(a, nomePropriedadePeso)), 2)
                            })
                            .Select(s => new Grafico()
                            {
                                NomeArgument = nomePropriedadeGrupo,
                                ArgumentDataMember = s.ArgumentDataMember,
                                ValueDataMember = s.ValueDataMember
                            });
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                };

                #endregion

                IEnumerable<Grafico> listaGrupo = null;

                if (this.comboBoxTipoGrafico.SelectedIndex.Equals(5))
                {
                    // Agrupa as ocorrencias em uma lista
                    listaGrupo = delegateGrupo("DescOcorHorario", "PesoOcorHorario")
                        .Union(delegateGrupo("DescOcorComboio", "PesoOcorComboio"))
                        .Union(delegateGrupo("DescOcorMensagem", "PesoOcorMensagem"))
                        .Union(delegateGrupo("DescOcorProgNaoRea", "PesoOcorProgNaoRea"))
                        .Union(delegateGrupo("DescOcorReaNaoProg", "PesoOcorReaNaoProg"));

                    // Cria lista com as ocorrencias
                    _parametrosOcorrencia = listaGrupo
                        .Select(s => new Grafico()
                        {
                            NomeArgument = s.NomeArgument,
                            ArgumentDataMember = s.ArgumentDataMember,
                            ValueDataMember = s.ValueDataMember
                        })
                        .ToList();
                }
                else
                    listaGrupo = delegateGrupo(RetornaNomeCampo(0), "TotalPeso");

                this.brStckdSeries3DBarra.DataFilters.Clear();
                this.brStckdSeries3DPiza.DataFilters.Clear();

                this.brStckdSeries3DBarra.DataSource = listaGrupo.ToArray();
                this.brStckdSeries3DPiza.DataSource = listaGrupo.ToArray();

                this.brStckdSeries3DBarra.UpdateLayout();
                this.brStckdSeries3DPiza.UpdateLayout();

                this.contentControlTotalGeral.DataContext = RetornaDados(_listaGrafico, null);
                this.labelQtdeSerie.Content = listaGrupo.Count();
                this.spinFiltrar.MaxValue = listaGrupo.Count();
                this.spinFiltrar.Value = this.spinFiltrar.MaxValue ?? 0;
            }
        }

        private void ChartControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ClicouNaSerie(e.Timestamp) && _seriesPoint != null)
            {
                string nameLayoutPanel = String.Concat("Serie_", _seriesPoint.Argument, _seriesPoint.GetHashCode());

                DockLayoutManager dockLayoutManager = this.Parent as DockLayoutManager;

                LayoutPanel layoutPanel = dockLayoutManager.GetItem(nameLayoutPanel) as LayoutPanel;
                LayoutGroup layoutGroup = dockLayoutManager.GetItem("layoutGroupPrincipal") as LayoutGroup;
                TabbedGroup tabbedGroup = dockLayoutManager.GetItem("tabbedGroup") as TabbedGroup;

                if (tabbedGroup == null)
                {
                    tabbedGroup = new TabbedGroup() { Name = "tabbedGroup" };
                    layoutGroup.Add(tabbedGroup);
                }

                if (layoutPanel == null)
                    layoutPanel = new LayoutPanel()
                    {
                        Name = nameLayoutPanel,
                        Padding = new Thickness(0),
                        Caption = new Dictionary<string, object>() 
                        {
                            {"Cor", new SolidColorBrush((Color)_seriesPoint.Tag)},
                            {"Titulo", RetornaNomeCampo(2).Replace(":", "")},
                        },
                        Content = this.Clone(),
                        CaptionTemplate = this.FindResource("CaptionTemplateCaption") as DataTemplate
                    };
                else
                    dockLayoutManager.ClosedPanels.Remove(layoutPanel);

                tabbedGroup.Items.Add(layoutPanel);
            }
        }

        private void ChartControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseClicouNaSerie = e.Timestamp;
        }

        private void UserControlGrafico_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.comboBoxTipoGrafico.SelectedIndex.Equals(-1))
                this.comboBoxTipoGrafico.SelectedIndex = 0;
        }

        private void spinFiltrar_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.brStckdSeries3DBarra.DataFilters.Clear();
                this.brStckdSeries3DPiza.DataFilters.Clear();

                try
                {
                    DataFilter dataFilter = new DataFilter()
                    {
                        ColumnName = "ArgumentDataMember",
                        Condition = DataFilterCondition.GreaterThanOrEqual,
                        Value = (this.brStckdSeries3DPiza.DataSource as IEnumerable<Grafico>)
                                    .OrderByDescending(o => o.ArgumentDataMember)
                                    .Where((w, index) => index + 1 <= this.spinFiltrar.Value)
                                    .LastOrDefault()
                                    .ArgumentDataMember
                    };

                    this.brStckdSeries3DBarra.DataFilters.Add(dataFilter);
                    this.brStckdSeries3DPiza.DataFilters.Add(dataFilter);
                }
                catch (Exception)
                {
                }
            }
        }

        private void ChartControl_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.SeriesPoint.Tag = e.DrawOptions.Color;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Retorna dados da lista
        /// </summary>
        /// <param name="listaGrafico">Lista com as informaçõe</param>
        /// <param name="nomePropriedade">Nome da propriedade</param>
        /// <returns>Objeto do tipo Dictionary com 3 registros</returns>
        private Dictionary<string, object> RetornaDados(IEnumerable<DadosGrafico> listaGrafico, string nomePropriedade)
        {
            try
            {
                DadosGrafico itemListaGrafico = listaGrafico.FirstOrDefault();

                string nomePropriedadePeso = nomePropriedade == null || nomePropriedade.IndexOf("DescOcor").Equals(-1)
                    ? "TotalPeso"
                    : nomePropriedade.Replace("DescOcor", "PesoOcor");

                return new Dictionary<string, object>
                {
                    {"Descricao", ( String.IsNullOrEmpty(nomePropriedade) 
                                            ?  "Total geral"
                                            : RetornaValorPropriedade<string>(itemListaGrafico, nomePropriedade) ) },                                           

                    {"Peso", listaGrafico.Sum(sm => RetornaValorPropriedade<int>(sm,nomePropriedadePeso))},
                    {"Qtde", listaGrafico.Count()},
                    {"PesoQtde", Math.Round(listaGrafico.Average(a => RetornaValorPropriedade<int>(a,nomePropriedadePeso)),2)},
                };
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// Retorna o valor da propriedade informada
        /// </summary>
        /// <typeparam name="T">Tipo generico para o retorno</typeparam>
        /// <param name="objeto">Objeto para consulta</param>
        /// <param name="nomepropriedade">Nome da propriedade</param>
        /// <returns>Retorna objeto do tipo generico</returns>
        private T RetornaValorPropriedade<T>(object objeto, string nomepropriedade = null)
        {
            object valorObjeto = objeto.GetType().GetProperty(nomepropriedade).GetValue(objeto, null);

            return (T)Convert.ChangeType(valorObjeto, typeof(T));
        }

        /// <summary>
        /// Retorna o nome da propriedade conforme o ripo de garafico
        /// </summary>
        /// <param name="parametro">Índice de qual campo</param>
        /// <param name="argument">Quando for uma ocorrencia</param>
        /// <returns>string</returns>
        private string RetornaNomeCampo(double parametro, string argument = null)
        {
            if (String.IsNullOrEmpty(argument))
                return ((string[])(_parametros[this.comboBoxTipoGrafico.SelectedIndex]))[Convert.ToInt16(parametro)];
            else
                return _parametrosOcorrencia
                    .Where(w => w.ArgumentDataMember.Equals(argument) &&
                                w.ValueDataMember.Equals(parametro))
                    .FirstOrDefault()
                    .NomeArgument;
        }

        /// <summary>
        /// Copia objeto com base no atual
        /// </summary>
        /// <returns>UsrCtrlGraficoGerencial</returns>
        private UsrCtrlGraficoGerencial Clone()
        {
            UsrCtrlGraficoGerencial usrCtrlGraficoGerencial = new UsrCtrlGraficoGerencial()
            {
                CopiaGraficoGerencial = this.MemberwiseClone() as UsrCtrlGraficoGerencial,
                ListaGrafico = _listaGraficoSerie,
                ParametrosOcorrencia = _parametrosOcorrencia,
                ChartControl = _chartControl,
                SeriesPoint = _seriesPoint,
                NomeArgument = _nomeArgument,
            };

            usrCtrlGraficoGerencial.tglBtnImprimir.IsChecked = this.tglBtnImprimir.IsChecked;
            usrCtrlGraficoGerencial.tglBtnMostarTexto.IsChecked = this.tglBtnMostarTexto.IsChecked;
            usrCtrlGraficoGerencial.tglBtnGraficoBarra.IsChecked = this.tglBtnGraficoBarra.IsChecked;
            usrCtrlGraficoGerencial.tglBtnInicial.IsChecked = false;
            usrCtrlGraficoGerencial.labelTitulo.Content =
                String.Concat((this.tglBtnInicial.IsChecked ?? true
                                ? ""
                                : String.Concat(this.labelTitulo.Content, @"\ ")),
                              RetornaNomeCampo(2),
                              _seriesPoint.Argument);

            return usrCtrlGraficoGerencial;
        }

        /// <summary>
        /// Verifica se foi clicado a serie do gráfico
        /// </summary>
        /// <param name="mouseUpClique">Valor do Timestamp</param>
        /// <returns>bool</returns>
        private bool ClicouNaSerie(int mouseUpClique)
        {
            return mouseUpClique - _mouseClicouNaSerie < CLICOU_NA_SERIE;
        }

        #endregion
    }
}
