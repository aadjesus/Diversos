using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using FGlobus.Util;
using DevExpress.Data.Filtering;
using System.Drawing;
using System.Collections;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.Utils;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// UserControl para visualizar PivotGrid e Grafico conforme os dados do GridControl
    /// </summary>
    [ToolboxItem(false)]
    internal partial class CustomizarGridControlVisaoGrafico : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public CustomizarGridControlVisaoGrafico()
        {
            InitializeComponent();
            //AppDomain.
            //DevExpress.Utils.Drawing.Helpers.NativeMethods.            
        }

        /// <summary>
        /// Construtor default
        /// </summary>
        /// <param name="customizarGridControl">Objeto do tipo customizarGridControl</param>
        public CustomizarGridControlVisaoGrafico(CustomizarGridControlX customizarGridControl)
        {
            InitializeComponent();
            _customizarGridControl = customizarGridControl;

            this.Dock = DockStyle.Fill;
        }

        #endregion

        #region Atributos

        private CustomizarGridControlX _customizarGridControl;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get) Retornar o objeto XYDiagram do grafico
        /// </summary>
        private XYDiagram XyDiagram
        {
            get
            {

                //foreach (Series series in ChartControl.Series)
                //    if (series.Label != null)
                //        series.Label.Visible = checkEditShowLabels.Checked;


                //RadarDiagram

                return this.chtCtrlVisualizacoes.Diagram as XYDiagram;
            }
        }

        /// <summary>
        /// (Get) Retornar o objeto Diagram3D do grafico
        /// </summary>
        private Diagram3D Diagram3D
        {
            get
            {
                return this.chtCtrlVisualizacoes.Diagram as Diagram3D;
            }
        }

        #endregion

        #region Metodos tela

        private void pvtGrdVisualizacoes_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GuardaPosicaoCursor(this.pvtGrdVisualizacoes.CalcHitInfo(new Point(e.X, e.Y)));
            }
            catch (Exception)
            {
            }
        }

        private void pvtGrdVisualizacoes_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                GuardaPosicaoCursor(this.pvtGrdVisualizacoes.CalcHitInfo(new Point(-1, -1)));
            }
            catch (Exception)
            {
            }
        }

        private void pvtGrdVisualizacoes_FieldAreaChanged(object sender, PivotFieldEventArgs e)
        {
            try
            {
                Func<string, string> delegateTextoAreasSelecionada =
                    delegate(string tipo)
                    {
                        try
                        {
                            PivotArea pivotArea = tipo.Equals("RowArea")
                                ? PivotArea.RowArea
                                : PivotArea.ColumnArea;

                            return string.Concat(this.pvtGrdVisualizacoes.GetFieldsByArea(pivotArea)
                                .Select((s, index) => (index.Equals(0) ? "" : " | ") + (String.IsNullOrWhiteSpace(s.Caption) ? s.ToString() : s.Caption))
                                .ToArray());
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    };

                if (this.XyDiagram != null)
                {
                    this.XyDiagram.AxisY.Title.Text = delegateTextoAreasSelecionada("RowArea");
                    this.XyDiagram.AxisX.Title.Text = delegateTextoAreasSelecionada("ColumnArea");
                }
            }
            catch (Exception)
            {
            }
        }

        private void CustomizarGridControlPivotGridGrafico_Load(object sender, EventArgs e)
        {
            PopularDadosPivotGrid();

            PopularDadosGrafico();

            pvtGrdVisualizacoes_FieldAreaChanged(null, null);

            this.spltCtrlCustonGrid.SplitterPosition = _customizarGridControl.Height / 2;
        }

        private void barCheckItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Func<BarCheckItem, bool, bool> delegateBarCheckItem =
                delegate(BarCheckItem barCheckItemInformado, bool marcado)
                {
                    BarCheckItem barCheckItem = e.Item as BarCheckItem;
                    return marcado
                        ? barCheckItem.Checked
                        : barCheckItem.Equals(barCheckItemInformado);
                };

            if (delegateBarCheckItem(this.brChckItmLegenda, false))
                this.chtCtrlVisualizacoes.Legend.Visible = delegateBarCheckItem(this.brChckItmLegenda, true);

            if (delegateBarCheckItem(this.brChckItmAlterarLinhasPorColunas, false))
                this.pvtGrdVisualizacoes.OptionsChartDataSource.ProvideDataByColumns = delegateBarCheckItem(this.brChckItmAlterarLinhasPorColunas, true);

            if (delegateBarCheckItem(this.brChckItmSomenteOSelecionado, false))
                this.pvtGrdVisualizacoes.OptionsChartDataSource.SelectionOnly = delegateBarCheckItem(this.brChckItmSomenteOSelecionado, true);

            if (delegateBarCheckItem(this.brChckItmMostrarTotalColuna, false))
                this.pvtGrdVisualizacoes.OptionsChartDataSource.ProvideColumnGrandTotals = delegateBarCheckItem(this.brChckItmMostrarTotalColuna, true);

            if (delegateBarCheckItem(this.brChckItmMostrarTotalLinha, false))
                this.pvtGrdVisualizacoes.OptionsChartDataSource.ProvideRowGrandTotals = delegateBarCheckItem(this.brChckItmMostrarTotalLinha, true);

            if (this.chtCtrlVisualizacoes.SeriesTemplate.Label != null && delegateBarCheckItem(this.brChckItmValor, false))
                this.chtCtrlVisualizacoes.SeriesTemplate.Label.Visible = delegateBarCheckItem(this.brChckItmValor, true);

            if (this.XyDiagram != null)
            {
                if (delegateBarCheckItem(this.brChckItmEixoY, false))
                    this.XyDiagram.AxisY.Visible = delegateBarCheckItem(this.brChckItmEixoY, true);

                if (delegateBarCheckItem(this.brChckItmEixoX, false))
                    this.XyDiagram.AxisX.Visible = delegateBarCheckItem(this.brChckItmEixoX, true);

                if (delegateBarCheckItem(this.brChckItmVirar, false))
                    this.XyDiagram.Rotated = delegateBarCheckItem(this.brChckItmVirar, true);
            }
        }

        private void spltCtrlCustonGrid_DoubleClick(object sender, EventArgs e)
        {
            if (this.spltCtrlCustonGrid.Panel1.Height.Equals(0) || this.spltCtrlCustonGrid.Panel2.Height.Equals(0))
                this.spltCtrlCustonGrid.SplitterPosition = _customizarGridControl.Height / 2;
            else
                if (this.spltCtrlCustonGrid.Panel1.Height > this.spltCtrlCustonGrid.Panel2.Height)
                    this.spltCtrlCustonGrid.SplitterPosition = _customizarGridControl.Height;
                else
                    if (this.spltCtrlCustonGrid.Panel1.Height < this.spltCtrlCustonGrid.Panel2.Height)
                        this.spltCtrlCustonGrid.SplitterPosition = 0;
        }

        #endregion

        #region Metodos

        private void PopularDadosPivotGrid()
        {
            #region Adiciona as colunas

            this.pvtGrdVisualizacoes.Fields.Clear();
            this.pvtGrdVisualizacoes.Fields.AddRange(_customizarGridControl.ColunasDoGrid.Columns
                .OfType<GridColumn>()
                .Aggregate(new List<PivotGridField>(), (a, b) =>
                {
                    PivotGridField coluna = new PivotGridField();
                    coluna.Caption = b.Caption;
                    coluna.FieldName = b.FieldName;
                    coluna.UnboundFieldName = "field" + b.FieldName;

                    coluna.Area = PivotArea.FilterArea;
                    coluna.SummaryType = PivotSummaryType.Count;
                    coluna.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.True;

                    coluna.ToolTips.HeaderText = b.ToolTip;
                    coluna.CellFormat.FormatString = b.DisplayFormat.FormatString;
                    coluna.CellFormat.FormatType = b.DisplayFormat.FormatType;

                    if (b.ColumnEdit != null)
                    {
                        RepositoryItem repositoryItem = new RepositoryItem();
                        repositoryItem = b.ColumnEdit;
                        coluna.FieldEdit = repositoryItem;
                    }
                    a.Add(coluna);
                    return a;
                })
                .ToArray());

            #endregion

            #region Adicionas os itens do context menu

            new PivotSummaryType[] 
            { 
                PivotSummaryType.Count, 
                PivotSummaryType.Max, 
                PivotSummaryType.Min, 
                PivotSummaryType.Sum 
            }
                .ToList()
                .ForEach(f =>
                {
                    this.ctxtMnStrpVisao.Items.Add(f.ToString());
                    this.ctxtMnStrpVisao.Items[this.ctxtMnStrpVisao.Items.Count - 1].Click +=
                        delegate(object senderClick, EventArgs eClick)
                        {
                            try
                            {
                                PivotGridField field = this.ctxtMnStrpVisao.Tag as PivotGridField;
                                if (field != null)
                                    field.SummaryType = Enum.GetValues(typeof(PivotSummaryType))
                                        .Cast<PivotSummaryType>()
                                        .Where(w => w.ToString().Equals(senderClick.ToString()))
                                        .SingleOrDefault();
                            }
                            catch (Exception)
                            {
                            }
                        };
                });

            #endregion

            // Adicopna os filtros do grid caso exista
            this.pvtGrdVisualizacoes.Prefilter.Criteria = CriteriaOperator.And(
                _customizarGridControl.ColunasDoGrid.Columns
                .OfType<GridColumn>()
                .Where(w => w.FilterInfo != null)
                .Select(s => CriteriaOperator.Parse(s.FilterInfo.FilterString))
                .ToArray());

            this.pvtGrdVisualizacoes.DataSource = _customizarGridControl.GridControl.DataSource;
            this.pvtGrdVisualizacoes.RefreshData();
        }

        private void PopularDadosGrafico()
        {
            ViewType[] ignorarOsTipos = new ViewType[] 
            {
                ViewType.PolarArea, 
                ViewType.PolarLine, 
                ViewType.PolarPoint, 
                //ViewType.SideBySideGantt, 
                ViewType.SideBySideRangeBar,                 
                ViewType.RangeBar, 
                //ViewType.Gantt, 
                ViewType.Stock, 
                ViewType.SwiftPlot,                
                ViewType.CandleStick,
                //ViewType.SideBySideFullStackedBar, 
                //ViewType.SideBySideFullStackedBar3D, 
                //ViewType.SideBySideStackedBar, 
                //ViewType.SideBySideStackedBar3D 
            };

            #region Layout

            Enum.GetValues(typeof(ViewType))
                .OfType<ViewType>()
                .Where(w => !ignorarOsTipos.Contains(w))
                .ToList()
                .ForEach(f =>
                {
                    BarCheckItem barCheckItem = new BarCheckItem();
                    barCheckItem.Caption = f.ToString();
                    barCheckItem.Tag = f;

                    barCheckItem.ItemClick += delegate(object senderItemClick, ItemClickEventArgs eItemClick)
                    {
                        if ((eItemClick.Item as BarCheckItem).Checked)
                        {
                            // Procura e desmarca o item selecionado
                            this.brMngrCustomGrid.Items
                                .OfType<BarCheckItem>()
                                .Where(w => w.Tag != null && w.Tag is ViewType && eItemClick.Item != w && w.Checked)
                                .Update(u => u.Checked = false);

                            this.chtCtrlVisualizacoes.SeriesTemplate.ChangeView((ViewType)eItemClick.Item.Tag);

                            this.chtCtrlVisualizacoes.Legend.Visible = this.brChckItmLegenda.Checked;
                            this.pvtGrdVisualizacoes.OptionsChartDataSource.ProvideDataByColumns = this.brChckItmAlterarLinhasPorColunas.Checked;
                            this.pvtGrdVisualizacoes.OptionsChartDataSource.SelectionOnly = this.brChckItmSomenteOSelecionado.Checked;
                            this.pvtGrdVisualizacoes.OptionsChartDataSource.ProvideColumnGrandTotals = this.brChckItmMostrarTotalColuna.Checked;
                            this.pvtGrdVisualizacoes.OptionsChartDataSource.ProvideRowGrandTotals = this.brChckItmMostrarTotalLinha.Checked;
                            this.chtCtrlVisualizacoes.Legend.Visible = this.brChckItmLegenda.Checked;
                            if (this.chtCtrlVisualizacoes.SeriesTemplate.Label != null)
                                this.chtCtrlVisualizacoes.SeriesTemplate.Label.Visible = this.brChckItmValor.Checked;

                            if (this.Diagram3D != null)
                            {
                                this.Diagram3D.RuntimeRotation = true;
                                this.Diagram3D.RuntimeZooming = true;
                                this.Diagram3D.RuntimeScrolling = true;
                            }
                            else
                                if (this.XyDiagram != null)
                                {
                                    this.XyDiagram.AxisX.Visible = this.brChckItmEixoX.Checked;
                                    this.XyDiagram.AxisX.Title.Visible = true;
                                    this.XyDiagram.AxisX.Range.MaxValue = 20;
                                    this.XyDiagram.AxisX.Range.Auto = true;
                                    this.XyDiagram.AxisX.Range.SideMarginsEnabled = true;

                                    this.XyDiagram.AxisY.Visible = this.brChckItmEixoY.Checked;
                                    this.XyDiagram.AxisY.Title.Visible = true;
                                    this.XyDiagram.AxisY.Range.MaxValue = 20;
                                    this.XyDiagram.AxisY.Range.Auto = true;
                                    this.XyDiagram.AxisY.Range.SideMarginsEnabled = true;

                                    this.XyDiagram.Rotated = this.brChckItmVirar.Checked;
                                    this.XyDiagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;
                                }


                            this.chtCtrlVisualizacoes.Series
                                .OfType<Series>()
                                .ToList()
                                .ForEach(f1 =>
                                {
                                    ISupportTransparency supportTransparency = f1.View as ISupportTransparency;
                                    if (supportTransparency != null &&
                                        (f1.View is AreaSeriesView ||
                                         f1.View is Area3DSeriesView ||
                                         f1.View is RadarAreaSeriesView ||
                                         f1.View is Bar3DSeriesView))
                                        supportTransparency.Transparency = 135;
                                });

                            Series series = this.chtCtrlVisualizacoes.Series
                                .OfType<Series>()
                                .FirstOrDefault();

                            if (series != null && series.View != null)
                            {
                                this.brSbItmTipo.Enabled = series.View is PointSeriesView;
                                this.brMngrCustomGrid.Items
                                    .OfType<BarCheckItem>()
                                    .Where(w => w.Checked)
                                    .FirstOrDefault()
                                    .PerformClick();
                            }
                        }
                        else
                            (eItemClick.Item as BarCheckItem).Checked = true;

                    };

                    if (f.ToString().ToUpper().IndexOf("RADAR") != -1)
                        this.brSbItmRadar.AddItem(barCheckItem);
                    else
                        if (f.ToString().ToUpper().IndexOf("BAR") != -1)
                            this.brSbItmBarra.AddItem(barCheckItem);
                        else
                            if (f.ToString().ToUpper().IndexOf("LINE") != -1)
                                this.brSbItmLinha.AddItem(barCheckItem);
                            else
                                if (f.ToString().ToUpper().IndexOf("AREA") != -1)
                                    this.brSbItmArea.AddItem(barCheckItem);
                                else
                                    if (f.ToString().ToUpper().IndexOf("PIE") != -1 || f.ToString().ToUpper().IndexOf("DOUGHNUT") != -1)
                                        this.brSbItmPizza.AddItem(barCheckItem);
                                    else
                                        this.brSbItmOutros.AddItem(barCheckItem);
                });

            #endregion

            #region Tipo

            Enum.GetValues(typeof(MarkerKind))
                .OfType<MarkerKind>()
                .ToList()
                .ForEach(u =>
                {
                    BarCheckItem barCheckItem = new BarCheckItem();
                    barCheckItem.Caption = u.ToString();
                    barCheckItem.Tag = u;

                    barCheckItem.ItemClick += delegate(object senderItemClick, ItemClickEventArgs eItemClick)
                    {
                        if ((eItemClick.Item as BarCheckItem).Checked)
                        {
                            // Procura e desmarca o item selecionado
                            this.brSbItmTipo.LinksPersistInfo
                                .OfType<LinkPersistInfo>()
                                .Where(w => eItemClick.Item != w.Item && (w.Item as BarCheckItem).Checked)
                                .Update(u1 => (u1.Item as BarCheckItem).Checked = false);

                            // Altera o Kind de todas series
                            MarkerKind markerKind = (MarkerKind)eItemClick.Item.Tag;
                            this.chtCtrlVisualizacoes.Series
                                .OfType<Series>()
                                .ToList()
                                .ForEach(f1 =>
                                {
                                    PointSeriesView pointSeriesView = f1.View as PointSeriesView;
                                    if (pointSeriesView != null)
                                    {
                                        pointSeriesView.PointMarkerOptions.Kind = markerKind;
                                        pointSeriesView.PointMarkerOptions.BorderVisible = false;
                                    }
                                });
                        }
                        else
                            (eItemClick.Item as BarCheckItem).Checked = true;
                    };
                    this.brSbItmTipo.AddItem(barCheckItem);
                });

            #endregion

            this.chtCtrlVisualizacoes.DataSource = this.pvtGrdVisualizacoes;
            this.chtCtrlVisualizacoes.RefreshData();

            this.brMngrCustomGrid.Items
                .OfType<BarCheckItem>()
                .Where(w => w.Tag != null && w.Tag.Equals(ViewType.Line))
                .FirstOrDefault()
                .PerformClick();

            this.brMngrCustomGrid.Items
                .OfType<BarCheckItem>()
                .Where(w => w.Tag != null && w.Tag.Equals(MarkerKind.Circle))
                .FirstOrDefault()
                .PerformClick();
        }

        private void GuardaPosicaoCursor(PivotGridHitInfo info)
        {
            try
            {
                this.pvtGrdVisualizacoes.ContextMenuStrip = null;
                if (info != null && info.CellInfo != null)
                {
                    this.pvtGrdVisualizacoes.ContextMenuStrip = this.ctxtMnStrpVisao;
                    this.ctxtMnStrpVisao.Tag = info.CellInfo.DataField;
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

    }
}