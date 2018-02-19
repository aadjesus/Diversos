using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
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
using FGlobus.Componentes.WinForms.ws.controle;
using FGlobus.Util;
using DevExpress.XtraNavBar;
using System.Security.Permissions;
using System.ComponentModel.Design;
using DevExpress.XtraEditors.Repository;
using System.Xml;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Data;
using System.Text;
using System.Globalization;

namespace FGlobus.Componentes.WinForms
{
    /// <summary>
    /// Componente para customizar GridControl associado.
    /// </summary>
    //[Designer(typeof(myControlDesigner))]
    //[ToolboxItem("xxxxxxxxxxxxxxxxxx")]
    //[ToolboxBitmap(typeof(CustomizarGridControl), "Resources.CustomizarGridControl.ico")]
    //[DesignerAttribute(typeof(OcultarPropriedadesEEventos))]
    //[PropertyTab(typeof(BotaoBrowserPropriedades), PropertyTabScope.Document)]


    [ToolboxItem(true)]
    [DevExpress.Utils.ToolboxTabName("xxxxxxxx")]
    [Description("Overrides Controllers from the SystemModule and supplies additional basic Controllers that are specific for ASP.NET applications.")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [ToolboxBitmap(typeof(CustomizarGridControl), "Resources.CustomizarGridControl.ico")]
    [ToolboxItemFilter("Xaf.Platform.Web")]
    public partial class CustomizarGridControl : UserControl
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public CustomizarGridControl()
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

        private GridControl _gridControl;
        private eLayout _layoutGridControl;
        private string _estruturaXmlPadrao;
        private bool _inicioFormatacaoCondicional = false;
        private bool _atualizaFormatacaoCondicional = false;
        private bool _habilitarImpressao = true;
        private bool _associoGridControl { get; set; }

        private PrintingSystem _printingSystem;
        private PrintableComponentLink _printableComponentLink;
        private CompositeLink _compositeLink;
        private ColumnView _columnView = null;
        private GridView _gridView;
        private BandedGridView _bandedGridView;
        private AdvBandedGridView _advBandedGridView;
        private CardView _cardView;
        private LayoutView _layoutView;

        #endregion

        #region Delegates

        Func<PopupContainerEdit, PropertyGridControl, CheckEdit, string> DelegatePupulaTipoDeLinha =
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

        Func<GridColumn, string> FNomeColuna = s => String.IsNullOrEmpty(s.Caption)
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
                if (!_associoGridControl && _gridControl == null)
                {
                    try
                    {
                        GridControl = this.Parent.Controls
                            .OfType<GridControl>()
                            .First();
                        _associoGridControl = true;
                    }
                    catch (Exception)
                    {
                    }
                }

                return _gridControl;
            }
            set
            {
                _gridControl = value;
                this.nvBrCtrlCustonGrid.Enabled = _gridControl != null;
                this.Refresh();
            }
        }

        /// <summary>
        /// (Get/Set) Informa ou retorna se habilitar impressão.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("(Get/Set) Informa ou retorna se habilitar impressão.")]
        [DisplayName("Habilitar impressão")]
        [DefaultValue(true)]
        public bool HabilitarImpressao
        {
            get { return _habilitarImpressao; }
            set { _habilitarImpressao = value; }
        }

        /// <summary>
        /// Informa ou retorna o layout defalt do GridControl associado.
        /// </summary>
        //[Category(FGlobus.Util.Constantes.CATEGORIA)]
        //[Description("(Get/Set) Informa ou retorna o layout defalt do GridControl associado.")]
        //[DisplayName("Layout")]
        //[DefaultValue(eLayout.GridView)]
        //[Editor(typeof(LayoutUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        //public eLayout LayoutGridControl
        //{
        //    get { return _layoutGridControl; }
        //    set
        //    {
        //        if (_gridControl != null)
        //        {
        //            _layoutGridControl = value;

        //            //this.imgCbBxEdtLayout.SelectedIndex = (int)_layoutGridControl;
        //            //LayOutVisualisacao();
        //        }
        //    }
        //}

        private StyleFormatCondition ExpressaoAtual
        {
            get
            {
                if (this.lstBxCtrlExpressoes.SelectedItem != null)
                    return ((ItemExpressao)((ListBoxItem)(this.lstBxCtrlExpressoes.SelectedItem)).Value).Condicao;

                return null;
            }
        }

        private eLayout LayoutSelecionado
        {
            get
            {
                return (eLayout)this.imgCbBxEdtLayout.Properties.Items[this.imgCbBxEdtLayout.SelectedIndex].Value;
            }
        }

        #endregion

        #region Metodos dos controles

        #region Click

        private void smplBtnImprimir_Click(object sender, EventArgs e)
        {
            if (_printingSystem == null)
            {
                _printableComponentLink = new PrintableComponentLink();
                _compositeLink = new CompositeLink();

                _printingSystem = new PrintingSystem();
                _printingSystem.Links.AddRange(new object[] { _printableComponentLink, _compositeLink });

                _printableComponentLink.PrintingSystem = _printingSystem;
                _printableComponentLink.PrintingSystemBase = _printingSystem;

                _compositeLink.Links.AddRange(new object[] { _printableComponentLink });
                _compositeLink.PrintingSystem = _printingSystem;
                _compositeLink.PrintingSystemBase = _printingSystem;

                _compositeLink.CreateInnerPageHeaderArea += new CreateAreaEventHandler(_compositeLink_CreateInnerPageHeaderArea);
                _compositeLink.CreateInnerPageFooterArea += new CreateAreaEventHandler(_compositeLink_CreateInnerPageFooterArea);
            }

            _compositeLink.SkipArea = BrickModifier.None;
            _compositeLink.Landscape = false;
            _printableComponentLink.Component = _gridControl;
            _compositeLink.CreateDocument();
            _compositeLink.ShowPreview();
        }

        private void smplBtnRetaurarPadrao_Click(object sender, EventArgs e)
        {
            this.imgCbBxEdtLayout.SelectedIndex = (int)_layoutGridControl;
            this.chckEdtMostraRodapeDeValores.Checked = true;
            this.chckEdtLinhasImpares.Checked = false;
            this.chckEdtLinhasPares.Checked = false;
            this.spnEdtDistancia.Value = 0;
            this.spnEdtQtdeColunas.Value = 1;
            this.spnEdtQtdeLinhas.Value = 1;
            this.cmbBxEdtTitulo.SelectedIndex = -1;
            this.imgCmbBxEdtColunas.SelectedIndex = 0;
            this.chckEdtImprimirLinhasHorizontais.Checked = true;
            this.chckEdtImprimirLinhasVerticais.Checked = true;
            this.chckEdtImprimirFiltros.Checked = true;
            this.lstBxCtrlExpressoes.Items.Clear();

            CheckEdit_Changed_Generico(this.chckEdtLinhasImpares, null);
            CheckEdit_Changed_Generico(this.chckEdtLinhasPares, null);

            _columnView.FormatConditions.Clear();
            _columnView.ActiveFilter.Clear();

            if (e != null)
                _gridControl.MainView.RestoreLayoutFromStream(FGlobus.Util.Util.ConverteStringEmStream(_estruturaXmlPadrao));
        }

        private void FormatItemList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AbrirEditor();
        }

        private void ToolStripMenuItem_Click_Generico(object sender, EventArgs e)
        {
            if (sender == this.tlStrpMnItmIncluir)
            {
                StyleFormatCondition condition = new StyleFormatCondition();
                condition.Condition = FormatConditionEnum.Expression;

                _columnView.FormatConditions.Add(condition);
                int index = this.lstBxCtrlExpressoes.Items.Count;
                PopulaCondicoes();
                this.lstBxCtrlExpressoes.SelectedIndex = index;
                lstBxCtrlExpressoes_ItemCheck(this.lstBxCtrlExpressoes, null);
                AbrirEditor();

                SelectedIndexChanged_Generico(this.imgCmbBxEdtColunas, null);


                if (this.lstBxCtrlExpressoes.Text.IndexOf("[ Expressão:") != -1)
                    ToolStripMenuItem_Click_Generico(this.tlStrpMnItmExcluir, e);
            }
            else if (sender == this.tlStrpMnItmExcluir)
            {
                _atualizaFormatacaoCondicional = true;
                if (ExpressaoAtual == null)
                    return;

                _columnView.FormatConditions.Remove(ExpressaoAtual);

                this.lstBxCtrlExpressoes.Items.RemoveAt(this.lstBxCtrlExpressoes.SelectedIndex);
                _atualizaFormatacaoCondicional = false;
                SelectObjectUpdate();

            }
            else if (sender == this.tlStrpMnItmAlterar)
            {
                AbrirEditor();
            }
        }

        #endregion

        private void CustomizarGridControl_Load(object sender, EventArgs e)
        {
            // Devine valores da barra de navegação
            this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.nvBrCtrlCustonGrid_NavPaneStateChanged(this.nvBrCtrlCustonGrid, e);
            this.nvBrCtrlCustonGrid.ActiveGroup = this.nvBrGrpGeral;

            this.nvBrCtrlCustonGrid.Enabled = _gridControl != null;

            if (!DesignMode && _gridControl != null)
            {
                if (_gridControl.MainView.GetType() == typeof(GridView))
                {
                    _gridView = (GridView)_gridControl.MainView;
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

                _columnView = (_gridControl.MainView as ColumnView);
                var listaColunas = _columnView.Columns
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

                this.grpCtrlImpressao.Visible = _habilitarImpressao;

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
                         a.AddImage((Bitmap)CustomizarGridControl_Resource.ResourceManager.GetObject("CustomizarGridControl_" + b.ToString()));
                         return a;
                     });
                this.imgCbBxEdtLayout.Properties.Items.AddRange(Enum.GetValues(typeof(eLayout))
                     .Cast<eLayout>()
                     .Select(s => new ImageComboBoxItem(FGlobus.Util.Util.GetDescricaoEnum(s), s, (int)s))
                     .ToArray());

                this.imgCbBxEdtLayout.SelectedIndex = (int)_layoutGridControl;

                #endregion

                this.lstBxCtrlExpressoes.KeyDown += new KeyEventHandler(lstBxCtrlExpressoes_KeyDown);
                this.lstBxCtrlExpressoes.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(lstBxCtrlExpressoes_ItemCheck);

                this.chckEdtMostraRodapeDeValores.CheckedChanged += new EventHandler(CheckEdit_Changed_Generico);
                this.chckEdtLinhasImpares.CheckedChanged += new EventHandler(CheckEdit_Changed_Generico);
                this.chckEdtLinhasPares.CheckedChanged += new EventHandler(CheckEdit_Changed_Generico);
                this.chckEdtImprimirLinhasHorizontais.CheckedChanged += new EventHandler(CheckEdit_Changed_Generico);
                this.chckEdtImprimirLinhasVerticais.CheckedChanged += new EventHandler(CheckEdit_Changed_Generico);
                this.chckEdtImprimirFiltros.CheckedChanged += new EventHandler(CheckEdit_Changed_Generico);

                this.Disposed += new EventHandler(CustomizarGridControl_Disposed);

                this.spnEdtDistancia.ValueChanged += new EventHandler(SpinEdit_Changed_Generico);
                this.spnEdtQtdeColunas.ValueChanged += new EventHandler(SpinEdit_Changed_Generico);
                this.spnEdtQtdeLinhas.ValueChanged += new EventHandler(SpinEdit_Changed_Generico);

                this.imgCbBxEdtLayout.SelectedIndexChanged += new EventHandler(SelectedIndexChanged_Generico);
                this.lstBxCtrlExpressoes.SelectedIndexChanged += new EventHandler(SelectedIndexChanged_Generico);
                this.cmbBxEdtTitulo.SelectedIndexChanged += new EventHandler(SelectedIndexChanged_Generico);
                this.imgCmbBxEdtColunas.SelectedIndexChanged += new EventHandler(SelectedIndexChanged_Generico);

                this.pppCtnrEdtLinhasImpares.QueryResultValue += new QueryResultValueEventHandler(PopupContainerEdit_QueryResultValue_Generico);
                this.pppCtnrEdtLinhasPares.QueryResultValue += new QueryResultValueEventHandler(PopupContainerEdit_QueryResultValue_Generico);

                this.btnEdtNomeCriarColuna.Properties.ButtonClick += new ButtonPressedEventHandler(Properties_ButtonClick);
                this.nvBrCtrlCustonGrid.NavPaneStateChanged += new EventHandler(nvBrCtrlCustonGrid_NavPaneStateChanged);

                this.smplBtnRetaurarPadrao.Click += new EventHandler(smplBtnRetaurarPadrao_Click);
                this.smplBtnImprimir.Click += new EventHandler(smplBtnImprimir_Click);

                lstBxCtrlExpressoes_ItemCheck(sender, null);
                smplBtnRetaurarPadrao_Click(sender, null);
                LayOutVisualisacao();
            }
        }

        private void Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridColumn novaColuna = new GridColumn()
            {
                FieldName = this.btnEdtNomeCriarColuna.Text.RemoverAcentuacao().RemoverCaracterEspecial(),
                Caption = this.btnEdtNomeCriarColuna.Text,
                UnboundType = UnboundColumnType.String,
                UnboundExpression = ExpressaoAtual.Expression,
                ShowUnboundExpressionMenu = true,
                Visible = true,
                VisibleIndex = _gridView.Columns.Count
            };

            //CheckNullValue ExpressaoAtual.Expression

            switch (LayoutSelecionado)
            {
                case eLayout.GridView:

                    switch (e.Button.Index)
                    {
                        case 0:
                            _gridView.Columns.Add(novaColuna);
                            break;
                        case 1:
                            _gridView.Columns.Remove(novaColuna);
                            break;
                    }

                    break;
                case eLayout.BandedGridView:
                    break;
                case eLayout.AdvancedBandedGridView:
                    break;
                case eLayout.CardView:
                    break;
                case eLayout.LayoutView:
                    break;
            }

        }

        private void CustomizarGridControl_Disposed(object sender, EventArgs e)
        {

        }

        private void _compositeLink_CreateInnerPageFooterArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Far);
            e.Graph.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            e.Graph.DrawPageInfo(
                PageInfo.NumberOfTotal,
                "Página {0}/{1}",
                Color.DimGray,
                new RectangleF(0, 2, e.Graph.ClientPageSize.Width - 1, 15),
                DevExpress.XtraPrinting.BorderSide.None);
        }

        private void _compositeLink_CreateInnerPageHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string caption = "ssssss";// FGlobus.Util.Util.RetornaFormDoControle(this).Text;
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Far);
            e.Graph.Font = new Font("Nina", 12, FontStyle.Bold);
            e.Graph.DrawString(caption, Color.DimGray, new RectangleF(00, 0, e.Graph.ClientPageSize.Width - 1, 20), DevExpress.XtraPrinting.BorderSide.None);

            LineBrick lineBrick = new LineBrick();
            lineBrick = e.Graph.DrawLine(new PointF(0, 21), new PointF(e.Graph.ClientPageSize.Width - 1, 21), Color.DimGray, 1);

            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Arial", 8, FontStyle.Regular);
            e.Graph.DrawString((this.chckEdtImprimirFiltros.Checked
                ? _columnView.FilterPanelText.Replace("[", "")
                    .Replace("]", "")
                    .Replace("%", "")
                : ""),
                Color.DimGray,
                new RectangleF(00, 23, e.Graph.ClientPageSize.Width - 1, 30),
                DevExpress.XtraPrinting.BorderSide.None);
        }

        private void nvBrCtrlCustonGrid_NavPaneStateChanged(object sender, EventArgs e)
        {
            TamanhoWidth();
        }

        private void SelectedIndexChanged_Generico(object sender, EventArgs e)
        {
            if (sender == this.imgCbBxEdtLayout)
                LayOutVisualisacao();
            else if (sender == this.lstBxCtrlExpressoes)
                SelectObjectUpdate();
            else if (sender == this.cmbBxEdtTitulo)
            {
                GridColumn gridColumn = (this.cmbBxEdtTitulo.EditValue as GridColumn);
                string cardCaptionFormat = gridColumn == null
                    ? String.Concat("Registro: {0}", (LayoutSelecionado.Equals(eLayout.CardView) ? "" : " / {1}"))
                    : String.Concat(FNomeColuna(gridColumn), ": ", "{", gridColumn.AbsoluteIndex + (LayoutSelecionado.Equals(eLayout.CardView) ? 1 : 2), "}");

                switch (LayoutSelecionado)
                {
                    case eLayout.CardView:
                        _cardView.CardCaptionFormat = cardCaptionFormat;
                        break;
                    case eLayout.LayoutView:
                        _layoutView.CardCaptionFormat = cardCaptionFormat;
                        break;
                }
            }
            else if (sender == this.imgCmbBxEdtColunas)
            {
                if (ExpressaoAtual == null || _inicioFormatacaoCondicional)
                    return;

                ExpressaoAtual.ApplyToRow = this.imgCmbBxEdtColunas.Text.Equals("[Nenhuma]");
                if (!ExpressaoAtual.ApplyToRow)
                {
                    GridColumn col = this.imgCmbBxEdtColunas.EditValue as GridColumn;
                    ExpressaoAtual.Column = col;
                }
            }
        }

        private void CheckEdit_Changed_Generico(object sender, EventArgs e)
        {
            if (sender == this.chckEdtMostraRodapeDeValores && _gridControl.MainView is GridView)
                ((GridView)_gridControl.MainView).OptionsView.ShowFooter = this.chckEdtMostraRodapeDeValores.Checked;
            else if (sender == this.chckEdtLinhasImpares)
                DelegatePupulaTipoDeLinha(this.pppCtnrEdtLinhasImpares, this.prtyGrdCtrlLinhasImpares, this.chckEdtLinhasImpares);
            else if (sender == this.chckEdtLinhasPares)
                DelegatePupulaTipoDeLinha(this.pppCtnrEdtLinhasPares, this.prtyGrdCtrlLinhasPares, this.chckEdtLinhasPares);

            DefineGrid();
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

        private void PopupContainerEdit_QueryResultValue_Generico(object sender, QueryResultValueEventArgs e)
        {
            if (sender == this.pppCtnrEdtLinhasImpares)
                e.Value = DelegatePupulaTipoDeLinha((PopupContainerEdit)sender, this.prtyGrdCtrlLinhasImpares, this.chckEdtLinhasImpares);
            else
                e.Value = DelegatePupulaTipoDeLinha((PopupContainerEdit)sender, this.prtyGrdCtrlLinhasPares, this.chckEdtLinhasPares);

            DefineGrid();
        }

        private void CustomizarGridControl_Resize(object sender, EventArgs e)
        {
            TamanhoWidth();
        }

        private void lstBxCtrlExpressoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                ToolStripMenuItem_Click_Generico(this.tlStrpMnItmExcluir, null);

            if ((Keys.Control | Keys.I) == e.KeyData)
                ToolStripMenuItem_Click_Generico(this.tlStrpMnItmIncluir, null);

            //            this.btnEdtNomeCriarColuna.Enabled = this.lstBxCtrlExpressoes.Items[this.lstBxCtrlExpressoes.SelectedIndex].CheckState.Equals(CheckState.Checked);
        }

        #endregion

        #region Metodos

        private Stream ConverteStringEmStream(string estruturaXml)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(estruturaXml);
            return new MemoryStream(byteArray);
        }

        private void LayOutVisualisacao()
        {
            if (_gridControl != null && _gridControl.MainView != null)
            {
                try
                {
                    switch (LayoutSelecionado)
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

                    this.pnlCtrlDistanciaEntreLinhas.Enabled = LayoutSelecionado.Equals(eLayout.AdvancedBandedGridView);
                    this.pnlCtrlQtdeLinhaColuna.Enabled = LayoutSelecionado.Equals(eLayout.CardView);
                    this.pnlCtrlTitulo.Enabled = !this.chckEdtMostraRodapeDeValores.Enabled;
                    if (this.pnlCtrlTitulo.Enabled)
                    {
                        this.cmbBxEdtTitulo.SelectedIndex = this.cmbBxEdtTitulo.SelectedIndex.Equals(-1) ? 0 : this.cmbBxEdtTitulo.SelectedIndex;
                        SelectedIndexChanged_Generico(this.cmbBxEdtTitulo, null);
                    }

                    DefineGrid();

                    // Atualiza as colunas do grid atual conforme o as colunas do grid default 
                    ColumnView columnView = (_gridControl.MainView as ColumnView);
                    if (_columnView != null)
                    {
                        columnView.Columns
                            .OfType<GridColumn>()
                            .Update(u =>
                            {
                                GridColumn gridColumn = _columnView.Columns[u.FieldName];
                                if (gridColumn.ColumnEdit is RepositoryItemPictureEdit && LayoutSelecionado.Equals(eLayout.LayoutView))
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
                            .AddRange(_columnView.FormatConditions.OfType<StyleFormatCondition>().ToArray());
                    }

                    _columnView = columnView;

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

        private void TamanhoWidth()
        {
            this.Width = this.nvBrCtrlCustonGrid.OptionsNavPane.NavPaneState.Equals(NavPaneState.Collapsed)
                ? 40
                : 300;
        }

        private void PopulaCondicoes()
        {
            if (_columnView != null)
            {
                this.lstBxCtrlExpressoes.BeginUpdate();
                try
                {
                    this.lstBxCtrlExpressoes.Items.Clear();
                    foreach (StyleFormatCondition condicao in _columnView.FormatConditions)
                    {
                        ItemExpressao itemExpressao = new ItemExpressao(condicao);

                        if (condicao.Column != null)
                        {
                            //this.imgCmbBxEdtColunas.SelectedItem = this.imgCmbBxEdtColunas.Properties.Items.GetItem(new ImageComboBoxItem(condicao.Column.GetTextCaption(), condicao.Column, -1));
                            this.imgCmbBxEdtColunas.SelectedItem = this.imgCmbBxEdtColunas.Properties.Items[this.imgCmbBxEdtColunas.SelectedIndex];//.GetItem(new ImageComboBoxItem(condicao.Column.GetTextCaption(), condicao.Column, -1));
                            this.CheckEdit_Changed_Generico(this.imgCmbBxEdtColunas, null);
                        }

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
            if (ExpressaoAtual == null)
                return;

            AbrirEditor(ExpressaoAtual);

            this.lstBxCtrlExpressoes.Refresh();
        }

        private void SelectObjectUpdate()
        {
            if (_atualizaFormatacaoCondicional)
                return;

            _inicioFormatacaoCondicional = true;
            if (ExpressaoAtual == null)
            {
                this.prprtyGrdAparencia.Enabled = false;
                this.prprtyGrdAparencia.SelectedObject = null;
                this.pnlCtrlColunas.Visible = false;
            }
            else
            {
                this.prprtyGrdAparencia.Enabled = true;
                this.prprtyGrdAparencia.SelectedObject = ExpressaoAtual.Appearance;
                this.pnlCtrlColunas.Visible = true;
                if (ExpressaoAtual.Column != null)
                    this.imgCmbBxEdtColunas.EditValue = ExpressaoAtual.Column;
            }
            _inicioFormatacaoCondicional = false;
        }

        private void DefineGrid()
        {
            if (_gridControl.MainView is GridView)
            {
                ((GridView)_gridControl.MainView).FocusRectStyle = DrawFocusRectStyle.RowFocus;
                ((GridView)_gridControl.MainView).OptionsView.ShowIndicator = false;
                ((GridView)_gridControl.MainView).OptionsView.ShowAutoFilterRow = true;
                ((GridView)_gridControl.MainView).OptionsSelection.EnableAppearanceFocusedCell = false;
                ((GridView)_gridControl.MainView).OptionsView.ColumnAutoWidth = false;
                ((GridView)_gridControl.MainView).OptionsView.EnableAppearanceOddRow = this.chckEdtLinhasImpares.Checked;
                ((GridView)_gridControl.MainView).OptionsView.EnableAppearanceEvenRow = this.chckEdtLinhasPares.Checked;

                ((GridView)_gridControl.MainView).OptionsPrint.PrintHorzLines = this.chckEdtImprimirLinhasHorizontais.Checked;
                ((GridView)_gridControl.MainView).OptionsPrint.PrintVertLines = this.chckEdtImprimirLinhasVerticais.Checked;

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

                DelegateAppearanceObject(this.chckEdtLinhasImpares, this.prtyGrdCtrlLinhasImpares, ((GridView)_gridControl.MainView).Appearance.OddRow, Color.OldLace);
                DelegateAppearanceObject(this.chckEdtLinhasPares, this.prtyGrdCtrlLinhasPares, ((GridView)_gridControl.MainView).Appearance.EvenRow, Color.LightGray);
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

            public bool ExpressaoValida
            {
                get
                {
                    return _condicao.Condition == FormatConditionEnum.Expression;
                }
            }

            public override string ToString()
            {
                if (_condicao.Expression == string.Empty)
                    return string.Format("[ Expressão: {0} ]", Index + 1);
                return _condicao.Expression;
            }

            #endregion

            #region Propriedades

            public int Index
            {
                get
                {
                    return _condicao.Collection.IndexOf(_condicao);
                }
            }

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
            [Description("Grid")]
            GridView = 0,
            [Description("Grid com faixa")]
            BandedGridView = 1,
            [Description("Grid avançado")]
            AdvancedBandedGridView = 2,
            [Description("Cartão")]
            CardView = 3,
            [Description("Layout")]
            LayoutView = 4
        }
        private class LayoutUITypeEditor : UITypeEditor
        {
            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                try
                {
                    Bitmap newImage = (Bitmap)CustomizarGridControl_Resource.ResourceManager.GetObject("CustomizarGridControl_" + e.Value.ToString());
                    newImage.MakeTransparent();
                    e.Graphics.DrawImage(newImage, e.Bounds);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion

        private void lstBxCtrlExpressoes_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            this.btnEdtNomeCriarColuna.Enabled = this.lstBxCtrlExpressoes.SelectedIndex != -1 && this.lstBxCtrlExpressoes.Items[this.lstBxCtrlExpressoes.SelectedIndex].CheckState.Equals(CheckState.Checked);
        }

    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class myControlDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new myControlActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    public class myControlActionList : System.ComponentModel.Design.DesignerActionList
    {
        private CustomizarGridControl colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        public myControlActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as CustomizarGridControl;

            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        public GridControl GridControl
        {
            get
            {
                return colUserControl.GridControl;
            }
            set
            {
                GetPropertyByName("GridControl").SetValue(colUserControl, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();


            items.Add(new DesignerActionHeaderItem(FGlobus.Util.Constantes.CATEGORIA));

            items.Add(new DesignerActionPropertyItem("GridControl", "Grid Control", FGlobus.Util.Constantes.CATEGORIA, "Informa ou retorna GridControl que vai ser personalizado."));
            items.Add(new DesignerActionPropertyItem("SalvarLayoutGrid", "Salvar layout", FGlobus.Util.Constantes.CATEGORIA, "Informa ou retorna se salva o layout do GridControl associado."));

            return items;
        }
    }
}