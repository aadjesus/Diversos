using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Design;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.Utils;
using FGlobus.Util;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors.Repository;
using System.Xml;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Data.Filtering;
using DevExpress.XtraReports.UI;
using FGlobus.Relatorios;
using FGlobus.Componentes.WinForms;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Componente para customizar GridControl associado.
    /// </summary>
    [ToolboxBitmap(typeof(CustomizarGridControlX), "CustomizarGridControl.ico")]
    public partial class CustomizarGridControlX : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public CustomizarGridControlX()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.Height = 200;
                this.Width = 30;
            }
        }

        #endregion

        #region Atributos

        private string _estruturaXmlPadrao;
        private string _tituloImpressao;
        private bool _inicioFormatacaoCondicional = false;
        private bool _telaCheia = false;
        private bool _atualizaFormatacaoCondicional = false;
        private bool _associoGridControl;
        private bool _populouControle;
        private GridControl _gridControl;
        private eLayout _layoutGridControl;
        private MasterFormGlobus _masterFormGlobus = null;

        private ColumnView _colunasDoGrid;
        private GridView _gridView;
        private BandedGridView _bandedGridView;
        private AdvBandedGridView _advBandedGridView;
        private CardView _cardView;
        private LayoutView _layoutView;
        private CustomizarGridControlVisaoGrafico _customizarGridControlPivotGridGrafico;

        #endregion

        #region Delegates

        private Func<PopupContainerEdit, PropertyGridControl, CheckEdit, string> DelegatePupulaTipoDeLinha =
            delegate(PopupContainerEdit param1, PropertyGridControl param2, CheckEdit param3)
            {
                AppearanceObject appearanceObject = null;
                param1.Enabled = param3.Checked;

                if (param3.Checked)
                    appearanceObject = (AppearanceObject)param2.SelectedObject;
                else
                    param2.SelectedObject = null;

                param1.Text = param3.Checked
                    ? String.Concat(appearanceObject.BackColor.Name, ",",
                                    appearanceObject.BackColor2.Name, ",",
                                    appearanceObject.Font.Name)
                    : "";

                return param1.Text;
            };

        private Func<GridColumn, string> FNomeColuna = s => String.IsNullOrEmpty(s.Caption)
            ? (String.IsNullOrEmpty(s.FieldName)
                ? s.Name
                : s.FieldName)
            : s.Caption;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Get/Set) Informa ou retorna GridControl.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("(Get/Set) Informa ou retorna GridControl que vai ser personalizado.")]
        [DisplayName("Grid Control")]
        [DefaultValue(null)]
        public GridControl GridControl
        {
            get
            {
                if (!_associoGridControl && _gridControl == null && this.Parent != null)
                {
                    _gridControl = this.Parent.Controls
                        .OfType<GridControl>()
                        .FirstOrDefault();
                    _associoGridControl = _gridControl != null;
                }

                return _gridControl;
            }
            set
            {
                _gridControl = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// (Get/Set) Informa ou retorna o titulo para impressão
        /// </summary>		
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("(Get/Set) Informa ou retorna o titulo para impressão.")]
        [DisplayName("Titulo impressão")]
        [DefaultValue(null)]
        public string TituloImpressao
        {
            get { return _tituloImpressao; }
            set { _tituloImpressao = value; }
        }

        /// <summary>
        /// (Get) Retorna as colunas do GridControl associado no controle
        /// </summary>		
        internal ColumnView ColunasDoGrid
        {
            get { return _colunasDoGrid; }
        }

        /// <summary>
        /// (Get) Retorna a expressão atual
        /// </summary>
        private StyleFormatCondition ExpressaoAtual
        {
            get
            {
                if (this.lstBxCtrlExpressoes.SelectedItem != null)
                    return ((ItemExpressao)this.lstBxCtrlExpressoes.SelectedItem).Condicao;

                return null;
            }
        }

        /// <summary>
        /// (Get) Retorna o tipo de layout selecionado
        /// </summary>
        private eLayout LayoutSelecionado
        {
            get
            {
                return (eLayout)this.imgCbBxEdtLayout.Properties.Items[this.imgCbBxEdtLayout.SelectedIndex].Value;
            }
        }

        #endregion

        #region Metodos dos controles

        private void smplBtnTelaCheia_Click(object sender, EventArgs e)
        {
            _telaCheia = true;
            MasterFormGlobus masterFormGlobus = new MasterFormGlobus();
            masterFormGlobus.ShowInTaskbar = true;
            masterFormGlobus.MaximizeBox = false;
            masterFormGlobus.MinimizeBox = false;
            masterFormGlobus.Size = FGlobus.Util.Util.RetornarTamanhoDoForm(10);
            masterFormGlobus.MinimumSize = masterFormGlobus.Size;

            Control control = new Control();
            control = _gridControl.Parent;

            SimpleButton simpleButton = new SimpleButton();
            simpleButton.Dock = DockStyle.Fill;
            simpleButton.BorderStyle = BorderStyles.NoBorder;
            simpleButton.Text = "Formatando Grid...";
            simpleButton.Enabled = false;
            simpleButton.Appearance.Font = new System.Drawing.Font(
                simpleButton.Appearance.Font.FontFamily, 30);

            control.Controls.Add(simpleButton);

            masterFormGlobus.Controls.Add(this);
            masterFormGlobus.Controls.Add(_gridControl);
            if (_customizarGridControlPivotGridGrafico != null && _customizarGridControlPivotGridGrafico.Visible)
            {
                masterFormGlobus.Controls.Add(_customizarGridControlPivotGridGrafico);
                _customizarGridControlPivotGridGrafico.BringToFront();
                this.nvBrCtrlCustonGrid.Enabled = false;
            }

            masterFormGlobus.Controls
                .OfType<GridControl>()
                .FirstOrDefault()
                .BringToFront();

            masterFormGlobus.FormClosing += delegate(object senderFormClosing, FormClosingEventArgs eFormClosing)
            {
                if (!_gridControl.Visible)
                    smplBtnFecharVisaoGrafico_Click(_gridControl, null);

                control.Controls.RemoveAt(0);

                GridControl gridControl = masterFormGlobus.Controls
                    .OfType<GridControl>()
                    .FirstOrDefault();

                masterFormGlobus.Controls.Remove(gridControl);

                if (_customizarGridControlPivotGridGrafico != null && _customizarGridControlPivotGridGrafico.Visible)
                    masterFormGlobus.Controls.Add(_customizarGridControlPivotGridGrafico);

                control.Controls.Add(gridControl);

                CustomizarGridControlX customizarGridControl = masterFormGlobus.Controls
                    .OfType<CustomizarGridControlX>()
                    .FirstOrDefault();

                customizarGridControl.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
                _telaCheia = false;

                masterFormGlobus.Controls.Remove(customizarGridControl);
                control.Controls.Add(customizarGridControl);
            };


            masterFormGlobus.Text = (_masterFormGlobus == null ? "" : _masterFormGlobus.Text + @"\") + simpleButton.Text;

            try
            {
                masterFormGlobus.ShowDialog(_masterFormGlobus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void smplBtnImprimir_Click(object sender, EventArgs e)
        {
            FGlobus.Relatorios.MasterRelatorio masterRelatorio = new Relatorios.MasterRelatorio();
            if (_masterFormGlobus != null)
                masterRelatorio.NomeRelatorio = String.IsNullOrEmpty(_tituloImpressao)
                    ? _masterFormGlobus.Text
                    : _tituloImpressao;

            masterRelatorio.ModoGerencial = true;
            masterRelatorio.Parametros.Add(_colunasDoGrid.FilterPanelText.Replace("[", "")
                    .Replace("]", "")
                    .Replace("%", ""));

            WinControlContainer winControlContainerGrid = new WinControlContainer();

            winControlContainerGrid.WinControl = sender.Equals(smplBtnImprimir)
                ? _gridControl
                : sender as Control;

            masterRelatorio.Bands.GetBandByType(typeof(DetailBand)).Controls
                .Add(winControlContainerGrid);
            masterRelatorio.ShowRibbonPreviewDialog();
            //masterRelatorio.ShowRibbonDesignerDialog();
        }

        private void smplBtnRetaurarPadrao_Click(object sender, EventArgs e)
        {
            this.imgCbBxEdtLayout.SelectedIndex = (int)_layoutGridControl;
            this.chckEdtMostraRodapeDeValores.Checked = true;
            this.chckEdLinhasImpares.Checked = false;
            this.chckEdLinhasPares.Checked = false;
            this.spnEdtDistancia.Value = 0;
            this.spnEdtQtdeColunas.Value = 1;
            this.spnEdtQtdeLinhas.Value = 1;
            this.cmbBxEdtTitulo.SelectedIndex = -1;
            this.imgCmbBxEdtColunas.SelectedIndex = 0;
            this.chckEdtImprimirLinhasHorizontais.Checked = true;
            this.chckEdtImprimirLinhasVerticais.Checked = true;
            this.lstBxCtrlExpressoes.Items.Clear();

            CheckEdit_Changed_Generico(this.chckEdLinhasImpares, null);
            CheckEdit_Changed_Generico(this.chckEdLinhasPares, null);

            _colunasDoGrid.FormatConditions.Clear();
            _colunasDoGrid.ActiveFilter.Clear();

            if (e != null)
                _gridControl.MainView.RestoreLayoutFromStream(FGlobus.Util.Util.ConverteStringEmStream(_estruturaXmlPadrao));
        }

        private void lstBxCtrlExpressoes_DoubleClick(object sender, EventArgs e)
        {
            AbrirEditor();
        }

        private void FormatItemList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AbrirEditor();
        }

        private void chckBtnVisualizarPivEGra_Click(object sender, EventArgs e)
        {
            _gridControl.Visible = false;

            this.smplBtnFecharVisaoGrafico.Visible = true;
            this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            this.nvBrCtrlCustonGrid.Enabled = false;

            if (_customizarGridControlPivotGridGrafico == null)
            {
                _customizarGridControlPivotGridGrafico = new CustomizarGridControlVisaoGrafico(this);
                _gridControl.Parent.Controls.Add(_customizarGridControlPivotGridGrafico);
            }
            _customizarGridControlPivotGridGrafico.Visible = true;
            _customizarGridControlPivotGridGrafico.BringToFront();
        }

        private void CustomizarGridControl_Load(object sender, EventArgs e)
        {
            PopularControleConformeGridControl();
        }

        public void PopularControleConformeGridControl()
        {
            try
            {
                this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
                this.nvBrCtrlCustonGrid.ActiveGroup = this.nvBrGrpGeral;
                this.nvBrCtrlGuardarControles.Visible = false;

                if (!_populouControle && !DesignMode && _gridControl != null)
                {
                    _gridView = _gridControl.MainView as GridView;

                    if (_gridControl.MainView.GetType() == typeof(GridView))
                    {
                        _layoutGridControl = eLayout.GridView;
                    }
                    else if (_gridControl.MainView.GetType() == typeof(CardView))
                    {
                        _cardView = (CardView)_gridControl.MainView;
                        _layoutGridControl = eLayout.CardView;
                    }
                    else if (_gridControl.MainView.GetType() == typeof(BandedGridView))
                    {
                        _bandedGridView = (BandedGridView)_gridControl.MainView;
                        _layoutGridControl = eLayout.BandedGridView;
                    }
                    else if (_gridControl.MainView.GetType() == typeof(AdvBandedGridView))
                    {
                        _advBandedGridView = (AdvBandedGridView)_gridControl.MainView;
                        _layoutGridControl = eLayout.AdvancedBandedGridView;
                    }
                    else if (_gridControl.MainView.GetType() == typeof(LayoutView))
                    {
                        _layoutView = (LayoutView)_gridControl.MainView;
                        _layoutGridControl = eLayout.LayoutView;
                    }

                    _colunasDoGrid = (_gridControl.MainView as ColumnView);
                    var listaColunas = _colunasDoGrid.Columns
                        .Cast<GridColumn>()
                        .Where(w => w.Visible)
                        .Select(s => new ImageComboBoxItem(FNomeColuna(s), s))
                        .ToArray();

                    this.imgCmbBxEdtColunas.Properties.Items.Clear();
                    this.imgCmbBxEdtColunas.Properties.Items.Add(new ImageComboBoxItem("[Nenhuma]"));
                    this.imgCmbBxEdtColunas.Properties.Items.AddRange(listaColunas);
                    this.imgCmbBxEdtColunas.SelectedIndex = 0;

                    this.cmbBxEdtTitulo.Properties.Items.Clear();
                    this.cmbBxEdtTitulo.Properties.Items.Add(new ImageComboBoxItem("Registro"));
                    this.cmbBxEdtTitulo.Properties.Items.AddRange(listaColunas);
                    this.cmbBxEdtTitulo.SelectedIndex = -1;

                    _estruturaXmlPadrao = RetornarXMLLayOut();

                    #region pppCtrlCtnrLinhasImpares //  Define os parametros impares e pares

                    this.pppCtrlCtnrLinhasImpares.Dock = DockStyle.None;
                    this.pppCtrlCtnrLinhasPares.Dock = this.pppCtrlCtnrLinhasImpares.Dock;
                    this.pppCtrlCtnrLinhasImpares.Size = new Size(300, 300);
                    this.pppCtrlCtnrLinhasPares.Size = this.pppCtrlCtnrLinhasImpares.Size;

                    #endregion

                    #region imgCbBxEdtLayout // Limpa o controle Layout e adicionar as images e os itens

                    this.imgCbBxEdtLayout.Properties.Items.Clear();
                    this.imgCbBxEdtLayout.Properties.LargeImages = Enum.GetValues(typeof(eLayout))
                         .Cast<eLayout>()
                         .Aggregate(new DevExpress.Utils.ImageCollection(), (a, b) =>
                         {
                             a.AddImage((Bitmap)Properties.Resources.ResourceManager.GetObject("CustomizarGridControl_" + b.ToString()));
                             return a;
                         });
                    this.imgCbBxEdtLayout.Properties.Items.AddRange(Enum.GetValues(typeof(eLayout))
                         .Cast<eLayout>()
                         .Select(s => new ImageComboBoxItem(FGlobus.Util.Util.GetDescricaoEnum(s), s, (int)s))
                         .ToArray());

                    this.imgCbBxEdtLayout.SelectedIndex = (int)_layoutGridControl;

                    #endregion

                    this.Disposed += new EventHandler(CustomizarGridControl_Disposed);

                    smplBtnRetaurarPadrao_Click(null, null);

                    this.smplBtnVisualizarVisaoEGrafico.Enabled = _colunasDoGrid.Columns.Count > 2;

                    if (_gridView != null)
                        this.zmTrckBrCtrlAltOuDimFont.Properties.Maximum = Convert.ToInt16(_gridView.Appearance.Row.Font.Size * 2) + 1;

                    this.zmTrckBrCtrlAltOuDimFont.Value = (this.zmTrckBrCtrlAltOuDimFont.Properties.Maximum / 2) + 1;

                    this.nvBrGrpCtrlGeral.AutoScroll = true;
                    this.nvBrGrpCtrlGeral.AutoScrollMinSize = new System.Drawing.Size(300, 416);
                    this.nvBrGrpCtrlFormatacao.AutoScroll = this.nvBrGrpCtrlGeral.AutoScroll;
                    this.nvBrGrpCtrlFormatacao.AutoScrollMinSize = this.nvBrGrpCtrlGeral.AutoScrollMinSize;

                    _masterFormGlobus = base.FindForm() as MasterFormGlobus;

                    LayOutVisualisacaoGrid();

                    _populouControle = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }

        }

        private void CustomizarGridControl_Disposed(object sender, EventArgs e)
        {

        }

        private void nvBrCtrlCustonGrid_NavPaneStateChanged(object sender, EventArgs e)
        {
            TamanhoWidth();
        }

        private void CheckEdit_Changed_Generico(object sender, EventArgs e)
        {
            if (sender == this.chckEdtMostraRodapeDeValores && _gridControl.MainView is GridView)
                ((GridView)_gridControl.MainView).OptionsView.ShowFooter = this.chckEdtMostraRodapeDeValores.Checked;
            else
                if (sender == this.chckEdLinhasImpares)
                    DelegatePupulaTipoDeLinha(this.pppCtnrEdtLinhasImpares, this.prtyGrdCtrlLinhasImpares, this.chckEdLinhasImpares);
                else
                    if (sender == this.chckEdLinhasPares)
                        DelegatePupulaTipoDeLinha(this.pppCtnrEdtLinhasPares, this.prtyGrdCtrlLinhasPares, this.chckEdLinhasPares);

            this.zmTrckBrCtrlAltOuDimFont.Enabled = !this.chckEdLinhasImpares.Checked && !this.chckEdLinhasPares.Checked;
            DefinePropriedadeDefaultGridView();
        }

        private void SpinEdit_Changed_Generico(object sender, EventArgs e)
        {
            int valor = (int)((SpinEdit)sender).Value;
            if (sender == this.spnEdtDistancia)
                _advBandedGridView.RowSeparatorHeight = valor;
            if (sender == this.spnEdtQtdeColunas)
                _cardView.MaximumCardColumns = valor;
            if (sender == this.spnEdtQtdeLinhas)
                _cardView.MaximumCardRows = valor;
        }

        private void CustomizarGridControl_Resize(object sender, EventArgs e)
        {
            TamanhoWidth();
        }

        private void lstBxCtrlExpressoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                brBtnItmIncluir_ItemClick(this.brBtnItmExcluir, null);

            if ((Keys.Control | Keys.I) == e.KeyData)
                brBtnItmIncluir_ItemClick(this.brBtnItmIncluir, null);
        }

        private void nvBrCtrlFormatar_ItemChanged(object sender, EventArgs e)
        {
            if (_gridControl != null && !_gridControl.Visible)
                this.smplBtnVisualizarVisaoEGrafico.PerformClick();
        }

        private void zmTrckBrCtrlAltOuDimFont_EditValueChanged(object sender, EventArgs e)
        {
            Font font = new Font(
                _gridView.Appearance.Row.Font.FontFamily,
                (float)this.zmTrckBrCtrlAltOuDimFont.Value,
                _gridView.Appearance.Row.Font.Style,
                _gridView.Appearance.Row.Font.Unit);

            switch (this.LayoutSelecionado)
            {
                case eLayout.GridView:
                    _gridView.Appearance.Row.Font = font;
                    break;
                case eLayout.BandedGridView:
                    _bandedGridView.Appearance.Row.Font = font;
                    break;
                case eLayout.AdvancedBandedGridView:
                    _advBandedGridView.Appearance.Row.Font = font;
                    break;
                case eLayout.CardView:
                    _cardView.Appearance.FieldValue.Font = font;
                    _cardView.Appearance.FieldCaption.Font = font;
                    break;
                case eLayout.LayoutView:
                    _layoutView.Appearance.FieldValue.Font = font;
                    _layoutView.Appearance.FieldCaption.Font = font;
                    break;
            }



            _gridView.RowHeight = this.zmTrckBrCtrlAltOuDimFont.Value;
        }

        private void smplBtnFecharVisaoGrafico_Click(object sender, EventArgs e)
        {
            this.smplBtnFecharVisaoGrafico.Visible = false;
            this.nvBrCtrlCustonGrid.Enabled = true;
            this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            _gridControl.Visible = true;
            _customizarGridControlPivotGridGrafico.Visible = false;
        }

        private void brBtnItmIncluir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Equals(this.brBtnItmIncluir))
            {
                StyleFormatCondition condition = new StyleFormatCondition();
                condition.Condition = FormatConditionEnum.Expression;

                _colunasDoGrid.FormatConditions.Add(condition);
                int index = this.lstBxCtrlExpressoes.Items.Count;
                PopulaCondicoes();
                this.lstBxCtrlExpressoes.SelectedIndex = index;
                AbrirEditor();

                imgCmbBxEdtColunas_SelectedIndexChanged(this.imgCmbBxEdtColunas, null);

                if (this.lstBxCtrlExpressoes.Text.IndexOf("[ Expressão:") != -1)
                    brBtnItmIncluir_ItemClick(this.brBtnItmExcluir, e);

                //SelectObjectUpdate();
            }
            else if (e.Item.Equals(this.brBtnItmAlterar))
                AbrirEditor();
            else if (e.Item.Equals(this.brBtnItmExcluir))
            {
                _atualizaFormatacaoCondicional = true;
                if (this.ExpressaoAtual == null)
                    return;

                _colunasDoGrid.FormatConditions.Remove(this.ExpressaoAtual);

                this.lstBxCtrlExpressoes.Items.RemoveAt(this.lstBxCtrlExpressoes.SelectedIndex);
                _atualizaFormatacaoCondicional = false;
                AtualizaGridControlConformeFormatacaoCodicional();
            }
        }

        private void imgCmbBxEdtColunas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ExpressaoAtual == null || _inicioFormatacaoCondicional)
                return;

            this.ExpressaoAtual.ApplyToRow = this.imgCmbBxEdtColunas.Text.Equals("[Nenhuma]");
            if (!this.ExpressaoAtual.ApplyToRow)
            {
                GridColumn col = this.imgCmbBxEdtColunas.EditValue as GridColumn;
                this.ExpressaoAtual.Column = col;
            }
        }

        private void imgCbBxEdtLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayOutVisualisacaoGrid();
        }

        private void lstBxCtrlExpressoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaGridControlConformeFormatacaoCodicional();
        }

        private void cmbBxEdtTitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridColumn gridColumn = (this.cmbBxEdtTitulo.EditValue as GridColumn);
            string cardCaptionFormat = gridColumn == null
                ? String.Concat("Registro: {0}", (this.LayoutSelecionado.Equals(eLayout.CardView) ? "" : " / {1}"))
                : String.Concat(FNomeColuna(gridColumn), ": ", "{", gridColumn.AbsoluteIndex + (this.LayoutSelecionado.Equals(eLayout.CardView) ? 1 : 2), "}");

            switch (this.LayoutSelecionado)
            {
                case eLayout.CardView:
                    _cardView.CardCaptionFormat = cardCaptionFormat;
                    break;
                case eLayout.LayoutView:
                    _layoutView.CardCaptionFormat = cardCaptionFormat;
                    break;
            }
        }

        private void pppCtnrEdtLinhasImpares_QueryResultValue(object sender, QueryResultValueEventArgs e)
        {
            if (sender == this.pppCtnrEdtLinhasImpares)
                e.Value = DelegatePupulaTipoDeLinha((PopupContainerEdit)sender, this.prtyGrdCtrlLinhasImpares, this.chckEdLinhasImpares);
            else
                e.Value = DelegatePupulaTipoDeLinha((PopupContainerEdit)sender, this.prtyGrdCtrlLinhasPares, this.chckEdLinhasPares);

            DefinePropriedadeDefaultGridView();
        }

        #endregion

        #region Metodos

        private void LayOutVisualisacaoGrid()
        {
            if (_gridControl != null && _gridControl.MainView != null)
            {
                try
                {
                    switch (this.LayoutSelecionado)
                    {
                        case eLayout.GridView:
                            if (_gridView == null)
                                _gridView = new GridView(_gridControl);
                            _gridControl.MainView = _gridView;
                            break;
                        case eLayout.BandedGridView:
                            if (_bandedGridView == null)
                            {
                                _bandedGridView = new BandedGridView(_gridControl);
                                _bandedGridView.Bands.Add(new GridBand() { Caption = "Faixa" });
                            }
                            _gridControl.MainView = _bandedGridView;
                            break;
                        case eLayout.AdvancedBandedGridView:
                            if (_advBandedGridView == null)
                            {
                                _advBandedGridView = new AdvBandedGridView(_gridControl);
                                _advBandedGridView.RowSeparatorHeight = (int)this.spnEdtDistancia.Value;
                                this._advBandedGridView.Bands.Add(new GridBand() { Caption = "Faixa" });
                            }
                            _gridControl.MainView = _advBandedGridView;
                            break;
                        case eLayout.CardView:
                            if (_cardView == null)
                                _cardView = new CardView(_gridControl);
                            _cardView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
                            _gridControl.MainView = _cardView;
                            break;
                        case eLayout.LayoutView:
                            if (_layoutView == null)
                            {
                                _layoutView = new LayoutView(_gridControl);
                                _layoutView.OptionsView.ViewMode = LayoutViewMode.MultiRow;
                            }
                            _gridControl.MainView = _layoutView;
                            break;
                    }

                    this.chckEdtMostraRodapeDeValores.Enabled = (_gridControl.MainView is GridView);
                    this.pnlCtrlDadosLinhasImpares.Enabled = this.chckEdtMostraRodapeDeValores.Enabled;
                    this.pnlCtrlDadosLinhasPares.Enabled = this.chckEdtMostraRodapeDeValores.Enabled;
                    this.chckEdtImprimirLinhasHorizontais.Enabled = this.chckEdtMostraRodapeDeValores.Enabled;
                    this.chckEdtImprimirLinhasVerticais.Enabled = this.chckEdtMostraRodapeDeValores.Enabled;

                    this.pnlCtrlDistanciaEntreLinhas.Enabled = this.LayoutSelecionado.Equals(eLayout.AdvancedBandedGridView);
                    this.pnlCtrlQtdeLinhaColuna.Enabled = this.LayoutSelecionado.Equals(eLayout.CardView);
                    this.pnlCtrlTitulo.Enabled = !this.chckEdtMostraRodapeDeValores.Enabled;
                    if (this.pnlCtrlTitulo.Enabled)
                    {
                        this.cmbBxEdtTitulo.SelectedIndex = this.cmbBxEdtTitulo.SelectedIndex.Equals(-1) ? 0 : this.cmbBxEdtTitulo.SelectedIndex;
                        cmbBxEdtTitulo_SelectedIndexChanged(this.cmbBxEdtTitulo, null);
                    }

                    DefinePropriedadeDefaultGridView();

                    // Atualiza as colunas do grid atual conforme o as colunas do grid default 
                    ColumnView columnView = (_gridControl.MainView as ColumnView);
                    if (_colunasDoGrid != null)
                    {
                        columnView.Columns
                            .OfType<GridColumn>()
                            .Update(u =>
                            {
                                GridColumn gridColumn = _colunasDoGrid.Columns[u.FieldName];
                                if (gridColumn.ColumnEdit is RepositoryItemPictureEdit && this.LayoutSelecionado.Equals(eLayout.LayoutView))
                                    ((RepositoryItemPictureEdit)gridColumn.ColumnEdit).CustomHeight = 50;

                                if ((_gridControl.MainView is GridView))
                                    u.GroupIndex = gridColumn.GroupIndex;
                                u.SummaryItem.Assign(gridColumn.SummaryItem);
                                u.Caption = gridColumn.Caption;
                                u.Visible = gridColumn.Visible;
                                u.ColumnEdit = gridColumn.ColumnEdit;
                                u.FilterInfo = gridColumn.FilterInfo;
                            });

                        columnView.FormatConditions.Clear();
                        columnView.FormatConditions
                            .AddRange(_colunasDoGrid.FormatConditions.OfType<StyleFormatCondition>().ToArray());
                    }

                    _colunasDoGrid = columnView;

                    PopulaCondicoes();
                    if (this.lstBxCtrlExpressoes.Items.Count > 0)
                        this.lstBxCtrlExpressoes.SelectedIndex = 0;

                    CheckEdit_Changed_Generico(this.chckEdtMostraRodapeDeValores, null);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }

        /// <summary>
        /// Define o Width do controle conforme o NavPaneState
        /// <remarks>
        ///     NavPaneState.Collapsed = 40
        ///     NavPaneState.Expanded = 320
        /// </remarks>
        /// </summary>
        private void TamanhoWidth()
        {
            this.Width = this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState.Equals(NavPaneState.Collapsed)
                ? 40
                : 320;

            if (_gridControl != null && this.Parent != null)
                this.nvBrCtrlCustonGrid.Enabled =
                    //!this.smplBtnFecharVisaoGrafico.Visible &&
                    this.Parent.Size.Width > 320 + 200 &&
                    this.Parent.Size.Height > 200;

            this.smplBtnTelaCheia.Visible = !_telaCheia &&
                !this.nvBrCtrlCustonGrid.Enabled &&
                this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState.Equals(NavPaneState.Collapsed);
        }

        private void PopulaCondicoes()
        {
            if (_colunasDoGrid != null)
            {
                this.lstBxCtrlExpressoes.BeginUpdate();
                try
                {
                    this.lstBxCtrlExpressoes.Items.Clear();
                    foreach (StyleFormatCondition condicao in _colunasDoGrid.FormatConditions)
                    {
                        if (condicao.Column != null)
                        {
                            this.imgCmbBxEdtColunas.SelectedItem = this.imgCmbBxEdtColunas.Properties.Items[this.imgCmbBxEdtColunas.SelectedIndex];
                            this.CheckEdit_Changed_Generico(this.imgCmbBxEdtColunas, null);
                        }

                        ItemExpressao itemExpressao = new ItemExpressao(condicao);
                        if (itemExpressao.ExpressaoValida)
                            this.lstBxCtrlExpressoes.Items.Add(itemExpressao);
                    }
                }
                finally
                {
                    this.lstBxCtrlExpressoes.EndUpdate();
                }
            }
        }

        private void AbrirEditor(StyleFormatCondition styleFormatCondition)
        {
            using (ExpressionEditorForm expressionEditorForm = new ConditionExpressionEditorForm(styleFormatCondition, null))
            {
                expressionEditorForm.Text = "Editor de expressão";
                expressionEditorForm.StartPosition = FormStartPosition.CenterScreen;
                //form.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Icone_Globus_2008.ico", UriKind.RelativeOrAbsolute));
                if (expressionEditorForm.ShowDialog(this) == DialogResult.OK)
                    styleFormatCondition.Expression = expressionEditorForm.Expression;
            }
        }

        private void AbrirEditor()
        {
            if (this.ExpressaoAtual != null)
            {
                AbrirEditor(this.ExpressaoAtual);
                this.lstBxCtrlExpressoes.Refresh();
            }
        }

        private void AtualizaGridControlConformeFormatacaoCodicional()
        {
            if (_atualizaFormatacaoCondicional)
                return;

            _inicioFormatacaoCondicional = true;
            if (this.ExpressaoAtual == null)
            {
                this.prprtyGrdAparencia.Enabled = false;
                this.prprtyGrdAparencia.SelectedObject = null;
                this.pnlCtrlColunas.Visible = false;
            }
            else
            {
                this.prprtyGrdAparencia.Enabled = true;
                this.prprtyGrdAparencia.SelectedObject = this.ExpressaoAtual.Appearance;
                this.pnlCtrlColunas.Visible = true;
                if (this.ExpressaoAtual.Column != null)
                    this.imgCmbBxEdtColunas.EditValue = this.ExpressaoAtual.Column;
            }
            _inicioFormatacaoCondicional = false;
        }

        private void DefinePropriedadeDefaultGridView()
        {
            GridView gridView = _gridControl.MainView as GridView;
            if (gridView != null)
            {
                gridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                gridView.OptionsView.ShowIndicator = false;
                gridView.OptionsView.ShowAutoFilterRow = true;
                gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView.OptionsView.ColumnAutoWidth = false;
                gridView.OptionsView.EnableAppearanceOddRow = this.chckEdLinhasImpares.Checked;
                gridView.OptionsView.EnableAppearanceEvenRow = this.chckEdLinhasPares.Checked;

                gridView.OptionsPrint.PrintHorzLines = this.chckEdtImprimirLinhasHorizontais.Checked;
                gridView.OptionsPrint.PrintVertLines = this.chckEdtImprimirLinhasVerticais.Checked;

                Func<CheckEdit, PropertyGridControl, AppearanceObject, Color, AppearanceObject> DelegateAppearanceObject =
                    delegate(CheckEdit param1, PropertyGridControl param2, AppearanceObject param3, Color param4)
                    {
                        AppearanceObject appearanceObject = null;
                        if (param2.SelectedObject == null)
                            param2.SelectedObject = new AppearanceObject(new AppearanceDefault(
                                Color.Empty,
                                param4,
                                Color.Empty,
                                Color.Empty,
                                new Font("Tahoma", 8.25F)));

                        if (param1.Checked)
                            appearanceObject = (AppearanceObject)param2.SelectedObject;

                        param3.Reset();
                        param3.Assign(appearanceObject);
                        return appearanceObject;
                    };

                DelegateAppearanceObject(this.chckEdLinhasImpares, this.prtyGrdCtrlLinhasImpares, gridView.Appearance.OddRow, Color.OldLace);
                DelegateAppearanceObject(this.chckEdLinhasPares, this.prtyGrdCtrlLinhasPares, gridView.Appearance.EvenRow, Color.LightGray);
            }
        }

        private string RetornarXMLLayOut()
        {
            string estruturaXml = "";
            try
            {
                System.IO.Stream stream = new System.IO.MemoryStream();
                _gridControl.DefaultView.SaveLayoutToStream(stream, new OptionsLayoutGrid()
                {
                    LayoutVersion = Application.ProductVersion,
                    StoreAllOptions = true
                });

                stream.Seek(0, System.IO.SeekOrigin.Begin);

                XmlDataDocument xmlDtDoct = new XmlDataDocument();
                xmlDtDoct.Load(stream);

                estruturaXml = xmlDtDoct.InnerXml;
            }
            catch (Exception)
            {
            }
            return estruturaXml;
        }

        #endregion

        #region Class

        /// <summary>
        /// Classe para controlar expressões incluídas
        /// </summary>
        private class ItemExpressao
        {
            #region Construtor

            /// <summary>
            /// Construtor
            /// </summary>
            /// <param name="condicao">Condição.</param>
            public ItemExpressao(StyleFormatCondition condicao)
            {
                this._condicao = condicao;
            }

            #endregion

            #region Atributos

            private StyleFormatCondition _condicao;

            #endregion

            #region Metodos

            /// <summary>
            /// Verifica se a expressão é valida
            /// </summary>
            public bool ExpressaoValida
            {
                get
                {
                    return _condicao.Condition == FormatConditionEnum.Expression;
                }
            }

            /// <summary>
            /// Sobreposição do método para trazer o texto "[ Expressão: 1 ]"
            /// </summary>
            /// <returns>string</returns>
            public override string ToString()
            {
                if (_condicao.Expression == string.Empty)
                    return string.Format("[ Expressão: {0} ]", Index + 1);

                return _condicao.Expression;
            }

            #endregion

            #region Propriedades

            /// <summary>
            /// (Get) Retornar o index da coleção de condições
            /// </summary>
            public int Index
            {
                get
                {
                    return _condicao.Collection.IndexOf(_condicao);
                }
            }

            /// <summary>
            /// (Get) Retorna o StyleFormatCondition
            /// </summary>
            public StyleFormatCondition Condicao
            {
                get
                {
                    return _condicao;
                }
            }

            #endregion
        }

        //private partial class Aparencia : StyleFormatCondition
        //{
        //    #region Construtor

        //    /// <summary>
        //    /// Construtor defalt
        //    /// </summary>
        //    public Aparencia()
        //    {
        //        _fonte = new Font("Microsoft Sans Serif", 9F);
        //        _imagem = null;
        //        _alinhamento = eAlinhamento.Padrão;
        //        _degrade = eDegrade.Horizontal;
        //        _primeiraCorLinha = Color.Red;
        //        _segundaCorLinha = Color.Transparent;
        //        _cor = Color.Black;
        //    }

        //    #endregion

        //    #region Atributos

        //    private Font _fonte;
        //    private eAlinhamento _alinhamento;
        //    private Color _cor;

        //    private eDegrade _degrade;
        //    private Image _imagem;
        //    private Color _primeiraCorLinha;
        //    private Color _segundaCorLinha;

        //    /// <summary>
        //    /// Enum para o alinhamento da font no GridControl
        //    /// </summary>
        //    public enum eAlinhamento
        //    {
        //        Padrão = 0,
        //        Direita = 1,
        //        Cetro = 2,
        //        Esquerda = 3,
        //    }

        //    /// <summary>
        //    /// Enum para a cor do GridControl
        //    /// </summary>
        //    public enum eDegrade
        //    {
        //        [Description("Especifica um gradiente da esquerda para a direita.")]
        //        Horizontal = 0,

        //        [Description("Especifica um gradiente de cima para baixo.")]
        //        Vertical = 1,

        //        [Description("Especifica um gradiente da esquerda superior para inferior direito.")]
        //        EsquerdaSuperiorInferiorDireito = 2,

        //        [Description("Especifica um gradiente da parte superior direita para baixo à esquerda.")]
        //        SuperiorDireitaBaixoEsquerda = 3,
        //    }

        //    #endregion

        //    #region Propriedades

        //    [Browsable(false)]
        //    public override AppearanceObjectEx Appearance
        //    {
        //        get { return base.Appearance; }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa o tipo de fonte.
        //    /// </summary>
        //    [Category("Fonte")]
        //    [DefaultValue(typeof(System.Drawing.Font), "Microsoft Sans Serif, 9pt")]
        //    public Font Fonte
        //    {
        //        get { return _fonte; }
        //        set
        //        {
        //            if (value != null)
        //            {
        //                _fonte = value;
        //                base.Appearance.Font = _fonte;
        //            }
        //        }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa a cor da fonte.
        //    /// </summary>
        //    [Category("Fonte")]
        //    [DefaultValue(typeof(System.Drawing.Color), "Black")]
        //    public Color Cor
        //    {
        //        get { return _cor; }
        //        set
        //        {
        //            _cor = value;
        //            base.Appearance.ForeColor = _cor;
        //        }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa o alinhamento da fonte.
        //    /// </summary>
        //    [Category("Fonte")]
        //    [DefaultValue(eAlinhamento.Padrão)]
        //    public eAlinhamento Alinhamento
        //    {
        //        get { return _alinhamento; }
        //        set
        //        {
        //            _alinhamento = value;
        //            //base.Appearance.ForeColor = _cor;
        //        }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa o degrade da linha.
        //    /// </summary>
        //    [Category("Linha")]
        //    [DefaultValue(eDegrade.Horizontal)]
        //    public eDegrade Degrade
        //    {
        //        get { return _degrade; }
        //        set
        //        {
        //            _degrade = value;
        //            //base.Appearance.ForeColor = _cor;
        //        }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa a 1º cor da linha.
        //    /// </summary>
        //    [Category("Linha")]
        //    [DisplayName("1º cor")]
        //    [DefaultValue(typeof(System.Drawing.Color), "Red")]
        //    [TypeConverter(typeof(ColorConverter))]
        //    public Color PrimeiraCorLinha
        //    {
        //        get { return _primeiraCorLinha; }
        //        set
        //        {
        //            _primeiraCorLinha = value;
        //            base.Appearance.BackColor = _primeiraCorLinha;
        //        }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa a 2º cor da linha.
        //    /// </summary>
        //    [Category("Linha")]
        //    [DisplayName("2º cor")]
        //    [DefaultValue(typeof(System.Drawing.Color), "Transparent")]
        //    public Color SegundaCorLinha
        //    {
        //        get { return _segundaCorLinha; }
        //        set
        //        {
        //            //((CardView)_baseView).Appearance.Row.BackColor2 = value;
        //            _segundaCorLinha = value;
        //            base.Appearance.BackColor2 = _segundaCorLinha;
        //        }
        //    }

        //    /// <summary>
        //    /// (Get/Set) Retorna ou informa a imagem linha.
        //    /// </summary>
        //    [Category("Linha")]
        //    [DefaultValue(null)]
        //    public Image Imagem
        //    {
        //        get { return _imagem; }
        //        set
        //        {
        //            _imagem = value;
        //            base.Appearance.Image = _imagem;
        //        }
        //    }

        //    #endregion

        //    #region ICloneable Members

        //    public object Clone()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    #endregion
        //}       

        /// <summary>
        /// Enumerable com os layouts dos grids
        /// </summary>
        public enum eLayout
        {
            /// <summary>
            /// Grid
            /// </summary>
            [Description("Grid")]
            GridView = 0,

            /// <summary>
            /// Grid com faixa
            /// </summary>
            [Description("Grid com faixa")]
            BandedGridView = 1,

            /// <summary>
            /// Grid avançado
            /// </summary>
            [Description("Grid avançado")]
            AdvancedBandedGridView = 2,

            /// <summary>
            /// Cartão
            /// </summary>
            [Description("Cartão")]
            CardView = 3,

            /// <summary>
            /// Layout
            /// </summary>
            [Description("Layout")]
            LayoutView = 4
        }

        #endregion
    }
}