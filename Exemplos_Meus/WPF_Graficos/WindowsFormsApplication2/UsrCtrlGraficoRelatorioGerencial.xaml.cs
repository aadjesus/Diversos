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
using DevExpress.Xpf.Docking;
using System.Data;
using DevExpress.Xpf.Charts;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Editors;
using Globus5.WPF.Comum.ws.gestaoDeFrotaOnline;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

namespace Globus5.WPF.Sistemas.GestaoDeFrotaOnline
{
    /// <summary>
    /// Interaction logic for UsrCtrlGraficoRelatorioGerencial.xaml
    /// </summary>
    public partial class UsrCtrlGraficoRelatorioGerencial : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor Default
        /// </summary>
        public UsrCtrlGraficoRelatorioGerencial()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.graficoDeBarra.Points.Clear();
                this.graficoDePizza.Points.Clear();
            }
        }

        #endregion

        #region Atributos

        private List<DadosGrafico> _listaGrafico = new List<DadosGrafico>();
        private IEnumerable<DadosGrafico> _listaGraficoSerie;
        private List<AgrupamentoGrafico> _parametrosOcorrencia = new List<AgrupamentoGrafico>();
        private ChartControl _chartControl;
        private SeriesPoint _seriesPoint;
        private string _nomeArgument = "";
        private int _mouseClicouNaSerie;
        private const int CLICOU_NA_SERIE = 200;
        private bool _liberaNavegacao;
        private List<object> _parametros = new List<object>() 
        {
            new string[] {"CodigoLinha","NomeLinha",     "Linha: ",     "ValueDataMember"    },
            new string[] {"Dia",        "Dia",           "Dia: ",       "ArgumentDataMember" },
            new string[] {"Hora",       "Hora",          "Hora: ",      "ArgumentDataMember" },
            new string[] {"Localidade", "DescLocalidade","Local: ",     "ValueDataMember"    },
            new string[] {"PrefixoVeic","PlacaAtualVeic","Veículo: ",   "ValueDataMember"    },
            new string[] {"",           "",              "Ocorrência: ","ValueDataMember"    },
        };

        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna o UsrCtrlGraficoRelatorioGerencial
        /// </summary>
        protected UsrCtrlGraficoRelatorioGerencial CopiaGraficoGerencial;

        /// <summary>
        /// (Set/Get) Informa ou retorna a lista com os dados do grafico
        /// </summary>
        internal List<DadosGrafico> ListaGrafico
        {
            get
            {
                return _listaGrafico;
            }
            set
            {
                _listaGrafico = value;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna lista com os parametros de identificacao das ocorrencias
        /// </summary>
        internal List<AgrupamentoGrafico> ParametrosOcorrencia
        {
            get { return _parametrosOcorrencia; }
            set { _parametrosOcorrencia = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna "ChartControl" selecionado
        /// </summary>
        internal ChartControl ChartControl
        {
            get { return _chartControl; }
            set { _chartControl = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna "SeriesPoint" selecionada
        /// </summary>
        protected SeriesPoint SeriesPoint
        {
            get { return _seriesPoint; }
            set { _seriesPoint = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o nome de argumento da "SeriesPoint" selecionada
        /// </summary>
        protected string NomeArgument
        {
            get { return _nomeArgument; }
            set { _nomeArgument = value; }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna libera a tela para navegação
        /// </summary>
        public bool LiberaNavegacao
        {
            get
            {
                _liberaNavegacao = (bool)GetValue(LiberaNavegacaoProperty);
                return _liberaNavegacao;
            }
            set
            {
                _liberaNavegacao = value;
                SetValue(LiberaNavegacaoProperty, value);
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna libera a tela para navegação
        /// </summary>
        public static readonly DependencyProperty LiberaNavegacaoProperty =
            DependencyProperty.Register(
            "LiberaNavegacao",
            typeof(bool),
            typeof(UsrCtrlGraficoRelatorioGerencial),
            new FrameworkPropertyMetadata(false));

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
                                .AsParallel()
                                .Where(w => RetornaValorPropriedade<string>(w, nomePropriedade) != null &&
                                            RetornaValorPropriedade<string>(w, nomePropriedade).Equals(_nomeArgument));

                        this.contentControlTotalSerie.DataContext = RetornaDados(_listaGraficoSerie, descPropriedade);

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

        public void comboBoxTipoGrafico_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            // So se ñ estiver e execução e o index ñ for igual a -1
            if (!DesignerProperties.GetIsInDesignMode(this) &&
                this.comboBoxTipoGrafico.SelectedIndex != -1)
            {
                #region Delegate de agrupamento

                Func<string, string, IEnumerable<AgrupamentoGrafico>> delegateGrupo = delegate(string nomePropriedadeGrupo, string nomePropriedadePeso)
                {
                    try
                    {
                        return _listaGrafico
                            .AsParallel()
                            .Where(w => !String.IsNullOrEmpty(RetornaValorPropriedade<string>(w, nomePropriedadeGrupo)))
                            .GroupBy(g => RetornaValorPropriedade<string>(g, nomePropriedadeGrupo), (grupo, lista) => new
                            {
                                ArgumentDataMember = grupo.ToString(),
                                ValueDataMember = Math.Round(lista
                                    .Average(a => RetornaValorPropriedade<int>(a, nomePropriedadePeso)), 2)
                            })
                            .Select(s => new AgrupamentoGrafico()
                            {
                                NomeArgument = nomePropriedadeGrupo,
                                ArgumentDataMember = s.ArgumentDataMember,
                                ValueDataMember = s.ValueDataMember
                            })
                            .Where(w => w.ValueDataMember > 0);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                };

                #endregion

                IEnumerable<AgrupamentoGrafico> listaGrupo = null;

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
                        .Select(s => new AgrupamentoGrafico()
                        {
                            NomeArgument = s.NomeArgument,
                            ArgumentDataMember = s.ArgumentDataMember,
                            ValueDataMember = s.ValueDataMember
                        })
                        .ToList();
                }
                else
                    listaGrupo = delegateGrupo(RetornaNomeCampo(0), "TotalPeso");

                if (listaGrupo != null && listaGrupo.Count() > 0)
                {
                    listaGrupo = listaGrupo
                        .OrderBy(o => RetornaValorPropriedade<int>(o, RetornaNomeCampo(3)))
                        .ToArray();

                    this.graficoDeBarra.DataFilters.Clear();
                    this.graficoDePizza.DataFilters.Clear();

                    this.graficoDeBarra.DataSource = listaGrupo;
                    this.graficoDePizza.DataSource = listaGrupo.ToArray();

                    this.graficoDeBarra.UpdateLayout();
                    this.graficoDePizza.UpdateLayout();

                    this.contentControlTotalGeral.DataContext = RetornaDados(_listaGrafico, null);
                    this.labelQtdeSerie.Content = listaGrupo.Count();
                    this.spinFiltrar.MaxValue = listaGrupo.Count();
                    this.spinFiltrar.Value = this.spinFiltrar.MaxValue ?? 0;
                }

                _chartControl = this.tglBtnGraficoBarra.IsChecked ?? true
                    ? this.chartControlBarra
                    : this.chartControlPizza;
            }
        }

        private void ChartControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_liberaNavegacao && ClicouNaSerie(e.Timestamp) && _seriesPoint != null)
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

                bool adicionaPanel = false;
                if (layoutPanel == null)
                {
                    UsrCtrlGraficoRelatorioGerencial usrCtrlGraficoRelatorioGerencial = this.Clone();
                    usrCtrlGraficoRelatorioGerencial.comboBoxTipoGrafico.SelectedIndex = -1;
                    usrCtrlGraficoRelatorioGerencial.comboBoxTipoGrafico.SelectedIndex = 0;
                    adicionaPanel = true;
                    layoutPanel = new LayoutPanel()
                    {
                        Name = nameLayoutPanel,
                        Padding = new Thickness(0),
                        Caption = new Dictionary<string, object>() 
                        {
                            {"Cor", new SolidColorBrush((Color)_seriesPoint.Tag)},
                            {"Titulo", RetornaNomeCampo(2).Replace(":", "")},
                        },
                        Content = usrCtrlGraficoRelatorioGerencial,
                        CaptionTemplate = this.FindResource("CaptionTemplateCaption") as DataTemplate
                    };
                }
                else if (layoutPanel.IsClosed)
                {
                    adicionaPanel = true;
                    dockLayoutManager.ClosedPanels.Remove(layoutPanel);
                }

                if (adicionaPanel)
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
            if (_liberaNavegacao)
            {
                this.graficoDeBarra.DataFilters.Clear();
                this.graficoDePizza.DataFilters.Clear();
                try
                {
                    DataFilter dataFilter = new DataFilter()
                    {
                        ColumnName = "ValueDataMember",
                        Condition = DataFilterCondition.GreaterThanOrEqual,
                        Value = (this.graficoDePizza.DataSource as IEnumerable<AgrupamentoGrafico>)
                                    .AsParallel()
                                    .OrderByDescending(o => o.ValueDataMember)
                                    .Where((w, index) => index + 1 <= Convert.ToInt16(e.NewValue))
                                    .LastOrDefault()
                                    .ValueDataMember
                    };

                    this.graficoDeBarra.DataFilters.Add(dataFilter);
                    this.graficoDePizza.DataFilters.Add(dataFilter);
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

        private void tglBtnImprimir_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            (((this.Parent as DockLayoutManager).Parent as Grid).Parent as UsrCtrlRelatorioGerencial).HabilitarBotaoDeImpressao();
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
        internal static T RetornaValorPropriedade<T>(object objeto, string nomepropriedade = null)
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
                    .AsParallel()
                    .Where(w => w.ArgumentDataMember.Equals(argument) &&
                                w.ValueDataMember.Equals(parametro))
                    .FirstOrDefault()
                    .NomeArgument;
        }

        /// <summary>
        /// Copia objeto com base no atual
        /// </summary>
        /// <returns>UsrCtrlGraficoGerencial</returns>
        internal UsrCtrlGraficoRelatorioGerencial Clone()
        {
            UsrCtrlGraficoRelatorioGerencial usrCtrlGraficoGerencial = new UsrCtrlGraficoRelatorioGerencial()
            {
                CopiaGraficoGerencial = this.MemberwiseClone() as UsrCtrlGraficoRelatorioGerencial,
                ListaGrafico = _listaGraficoSerie == null
                    ? _listaGrafico
                    : _listaGraficoSerie.ToList(),
                ParametrosOcorrencia = _parametrosOcorrencia,
                ChartControl = _chartControl,
                SeriesPoint = _seriesPoint,
                NomeArgument = _nomeArgument,
                LiberaNavegacao = true
            };

            usrCtrlGraficoGerencial.tglBtnSeta.IsChecked = this.tglBtnSeta.IsChecked;
            usrCtrlGraficoGerencial.tglBtnImprimir.IsChecked = false;
            usrCtrlGraficoGerencial.tglBtnMostarTexto.IsChecked = this.tglBtnMostarTexto.IsChecked;
            usrCtrlGraficoGerencial.tglBtnGraficoBarra.IsChecked = this.tglBtnGraficoBarra.IsChecked;
            usrCtrlGraficoGerencial.tglBtnInicial.IsChecked = false;
            usrCtrlGraficoGerencial.labelTituloRodapeBarra.Content =
                String.Concat((this.tglBtnInicial.IsChecked ?? true
                                ? ""
                                : String.Concat(this.labelTituloRodapeBarra.Content, @"\ ")),
                              RetornaNomeCampo(2),
                              _seriesPoint.Argument);
            usrCtrlGraficoGerencial.labelTituloRodapePizza.Content = usrCtrlGraficoGerencial.labelTituloRodapeBarra.Content;

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

        /// <summary>
        /// Limpa os controles
        /// </summary>
        internal void LimpaControles()
        {
            this.graficoDeBarra.DataSource = null;
            this.graficoDePizza.DataSource = null;

            this.graficoDeBarra.DataFilters.Clear();
            this.graficoDePizza.DataFilters.Clear();

            this.graficoDeBarra.UpdateLayout();
            this.graficoDePizza.UpdateLayout();

            this.comboBoxTipoGrafico.SelectedIndex = 0;
            this.LiberaNavegacao = false;
        }

        #endregion

   }

    /// <summary>
    /// Estrutura para os graficos
    /// </summary>
    public struct DadosGrafico
    {
        /// <summary>
        /// Codigo da linha
        /// </summary>
        public string CodigoLinha { get; set; }
        /// <summary>
        /// Nome da linha
        /// </summary>
        public string NomeLinha { get; set; }
        /// <summary>
        /// Dia
        /// </summary>
        public string Dia { get; set; }
        /// <summary>
        /// Hora
        /// </summary>
        public string Hora { get; set; }
        /// <summary>
        /// Codigo da localidade
        /// </summary>
        public string Localidade { get; set; }
        /// <summary>
        /// Descrição da localidade
        /// </summary>
        public string DescLocalidade { get; set; }
        /// <summary>
        /// Prefixo do veiculo
        /// </summary>
        public string PrefixoVeic { get; set; }
        /// <summary>
        /// Placa do veiculo
        /// </summary>
        public string PlacaAtualVeic { get; set; }
        /// <summary>
        /// Peso da ocorrencia horario
        /// </summary>
        public int PesoOcorHorario { get; set; }
        /// <summary>
        /// Peso da ocorrencia comboio
        /// </summary>
        public int PesoOcorComboio { get; set; }
        /// <summary>
        /// Peso da ocorrencia mensagem
        /// </summary>
        public int PesoOcorMensagem { get; set; }
        /// <summary>
        /// Peso da ocorrencia programado e nao realizado
        /// </summary>
        public int PesoOcorProgNaoRea { get; set; }
        /// <summary>
        /// Peso da ocorrencia realizado e nao programado
        /// </summary>
        public int PesoOcorReaNaoProg { get; set; }
        /// <summary>
        /// Total de peso
        /// </summary>
        public int TotalPeso { get; set; }
        /// <summary>
        /// Descricao da ocorrencia horario
        /// </summary>
        public string DescOcorHorario { get; set; }
        /// <summary>
        /// Descricao da ocorrencia comboio
        /// </summary>
        public string DescOcorComboio { get; set; }
        /// <summary>
        /// Descricao da ocorrencia mensagem
        /// </summary>
        public string DescOcorMensagem { get; set; }
        /// <summary>
        /// Descricao da ocorrencia programado e nao realizado
        /// </summary>
        public string DescOcorProgNaoRea { get; set; }
        /// <summary>
        /// Descricao da ocorrencia realizado e nao programado
        /// </summary>
        public string DescOcorReaNaoProg { get; set; }
    }

    /// <summary>
    /// Classe com o agrupamento do grafico
    /// </summary>
    public class AgrupamentoGrafico
    {
        /// <summary>
        /// Nome da coluna do argumento 
        /// </summary>
        public string NomeArgument { get; set; }

        /// <summary>
        /// Nome do argumento
        /// </summary>
        public string ArgumentDataMember { get; set; }

        /// <summary>
        /// Valor do argumento
        /// </summary>
        public double ValueDataMember { get; set; }
    }

}

