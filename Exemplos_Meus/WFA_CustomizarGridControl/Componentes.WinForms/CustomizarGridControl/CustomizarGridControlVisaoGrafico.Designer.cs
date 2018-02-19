namespace FGlobus.Componentes.WinForms
{
    partial class CustomizarGridControlVisaoGrafico
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.StackedBarSeriesLabel stackedBarSeriesLabel1 = new DevExpress.XtraCharts.StackedBarSeriesLabel();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.spltCtrlCustonGrid = new DevExpress.XtraEditors.SplitContainerControl();
            this.pvtGrdVisualizacoes = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.ctxtMnStrpVisao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chtCtrlVisualizacoes = new DevExpress.XtraCharts.ChartControl();
            this.brMngrCustomGrid = new DevExpress.XtraBars.BarManager(this.components);
            this.ppupMnParametrosGrafico = new DevExpress.XtraBars.PopupMenu(this.components);
            this.brSbItmLayout = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmBarra = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmLinha = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmArea = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmPizza = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmRadar = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmOutros = new DevExpress.XtraBars.BarSubItem();
            this.brSbItmTipo = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.brChckItmEixoX = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmEixoY = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmLegenda = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmValor = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmVirar = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmSomenteOSelecionado = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmMostrarTotalColuna = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmMostrarTotalLinha = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmAlterarLinhasPorColunas = new DevExpress.XtraBars.BarCheckItem();
            this.brChckItmMarcarParaImpressao = new DevExpress.XtraBars.BarCheckItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem7 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.spltCtrlCustonGrid)).BeginInit();
            this.spltCtrlCustonGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvtGrdVisualizacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtCtrlVisualizacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brMngrCustomGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppupMnParametrosGrafico)).BeginInit();
            this.SuspendLayout();
            // 
            // spltCtrlCustonGrid
            // 
            this.spltCtrlCustonGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltCtrlCustonGrid.Horizontal = false;
            this.spltCtrlCustonGrid.Location = new System.Drawing.Point(0, 0);
            this.spltCtrlCustonGrid.Name = "spltCtrlCustonGrid";
            this.spltCtrlCustonGrid.Panel1.CaptionLocation = DevExpress.Utils.Locations.Right;
            this.spltCtrlCustonGrid.Panel1.Controls.Add(this.pvtGrdVisualizacoes);
            this.spltCtrlCustonGrid.Panel1.Text = "Panel1";
            this.spltCtrlCustonGrid.Panel2.Controls.Add(this.chtCtrlVisualizacoes);
            this.spltCtrlCustonGrid.Panel2.Text = "Panel2";
            this.spltCtrlCustonGrid.Size = new System.Drawing.Size(567, 534);
            this.spltCtrlCustonGrid.SplitterPosition = 262;
            this.spltCtrlCustonGrid.TabIndex = 0;
            this.spltCtrlCustonGrid.DoubleClick += new System.EventHandler(this.spltCtrlCustonGrid_DoubleClick);
            // 
            // pvtGrdVisualizacoes
            // 
            this.pvtGrdVisualizacoes.ContextMenuStrip = this.ctxtMnStrpVisao;
            this.pvtGrdVisualizacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvtGrdVisualizacoes.Location = new System.Drawing.Point(0, 0);
            this.pvtGrdVisualizacoes.Name = "pvtGrdVisualizacoes";
            this.pvtGrdVisualizacoes.Size = new System.Drawing.Size(567, 262);
            this.pvtGrdVisualizacoes.TabIndex = 0;
            this.pvtGrdVisualizacoes.FieldAreaChanged += new DevExpress.XtraPivotGrid.PivotFieldEventHandler(this.pvtGrdVisualizacoes_FieldAreaChanged);
            this.pvtGrdVisualizacoes.MouseLeave += new System.EventHandler(this.pvtGrdVisualizacoes_MouseLeave);
            this.pvtGrdVisualizacoes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pvtGrdVisualizacoes_MouseMove);
            // 
            // ctxtMnStrpVisao
            // 
            this.ctxtMnStrpVisao.Name = "contextMenuStrip1";
            this.ctxtMnStrpVisao.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ctxtMnStrpVisao.ShowImageMargin = false;
            this.ctxtMnStrpVisao.Size = new System.Drawing.Size(36, 4);
            // 
            // chtCtrlVisualizacoes
            // 
            this.chtCtrlVisualizacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtCtrlVisualizacoes.Location = new System.Drawing.Point(0, 0);
            this.chtCtrlVisualizacoes.Name = "chtCtrlVisualizacoes";
            this.chtCtrlVisualizacoes.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Stretch;
            this.brMngrCustomGrid.SetPopupContextMenu(this.chtCtrlVisualizacoes, this.ppupMnParametrosGrafico);
            this.chtCtrlVisualizacoes.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chtCtrlVisualizacoes.SeriesTemplate.Label = stackedBarSeriesLabel1;
            this.chtCtrlVisualizacoes.SeriesTemplate.View = stackedBarSeriesView1;
            this.chtCtrlVisualizacoes.Size = new System.Drawing.Size(567, 267);
            this.chtCtrlVisualizacoes.TabIndex = 5;
            chartTitle1.Alignment = System.Drawing.StringAlignment.Far;
            chartTitle1.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Bottom;
            chartTitle1.Font = new System.Drawing.Font("Tahoma", 6F);
            chartTitle1.Text = "www.bgmrodotec.com.br";
            this.chtCtrlVisualizacoes.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // brMngrCustomGrid
            // 
            this.brMngrCustomGrid.DockControls.Add(this.barDockControlTop);
            this.brMngrCustomGrid.DockControls.Add(this.barDockControlBottom);
            this.brMngrCustomGrid.DockControls.Add(this.barDockControlLeft);
            this.brMngrCustomGrid.DockControls.Add(this.barDockControlRight);
            this.brMngrCustomGrid.Form = this;
            this.brMngrCustomGrid.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barCheckItem1,
            this.barSubItem1,
            this.barCheckItem2,
            this.barButtonItem2,
            this.brSbItmLayout,
            this.brSbItmTipo,
            this.barSubItem4,
            this.brChckItmEixoX,
            this.brChckItmEixoY,
            this.brChckItmLegenda,
            this.brChckItmValor,
            this.barCheckItem7,
            this.brChckItmSomenteOSelecionado,
            this.brChckItmVirar,
            this.barButtonItem3,
            this.brChckItmMostrarTotalColuna,
            this.brChckItmMostrarTotalLinha,
            this.brChckItmAlterarLinhasPorColunas,
            this.brSbItmBarra,
            this.brSbItmLinha,
            this.brSbItmArea,
            this.brSbItmPizza,
            this.brSbItmOutros,
            this.brSbItmRadar,
            this.barButtonItem4,
            this.brChckItmMarcarParaImpressao});
            this.brMngrCustomGrid.MaxItemId = 27;
            // 
            // ppupMnParametrosGrafico
            // 
            this.ppupMnParametrosGrafico.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmTipo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmLegenda),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmValor),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmVirar),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmSomenteOSelecionado),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmMostrarTotalColuna),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmMostrarTotalLinha),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmAlterarLinhasPorColunas),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmMarcarParaImpressao)});
            this.ppupMnParametrosGrafico.Manager = this.brMngrCustomGrid;
            this.ppupMnParametrosGrafico.MenuCaption = "Parâmetros";
            this.ppupMnParametrosGrafico.Name = "ppupMnParametrosGrafico";
            // 
            // brSbItmLayout
            // 
            this.brSbItmLayout.Caption = "Layout";
            this.brSbItmLayout.Id = 5;
            this.brSbItmLayout.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmBarra),
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmLinha),
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmArea),
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmPizza),
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmRadar),
            new DevExpress.XtraBars.LinkPersistInfo(this.brSbItmOutros)});
            this.brSbItmLayout.Name = "brSbItmLayout";
            // 
            // brSbItmBarra
            // 
            this.brSbItmBarra.Caption = "Barra";
            this.brSbItmBarra.Id = 19;
            this.brSbItmBarra.Name = "brSbItmBarra";
            // 
            // brSbItmLinha
            // 
            this.brSbItmLinha.Caption = "Linha";
            this.brSbItmLinha.Id = 20;
            this.brSbItmLinha.Name = "brSbItmLinha";
            // 
            // brSbItmArea
            // 
            this.brSbItmArea.Caption = "Área";
            this.brSbItmArea.Id = 21;
            this.brSbItmArea.Name = "brSbItmArea";
            // 
            // brSbItmPizza
            // 
            this.brSbItmPizza.Caption = "Pizza";
            this.brSbItmPizza.Id = 22;
            this.brSbItmPizza.Name = "brSbItmPizza";
            // 
            // brSbItmRadar
            // 
            this.brSbItmRadar.Caption = "Radar";
            this.brSbItmRadar.Id = 24;
            this.brSbItmRadar.Name = "brSbItmRadar";
            // 
            // brSbItmOutros
            // 
            this.brSbItmOutros.Caption = "Outros";
            this.brSbItmOutros.Id = 23;
            this.brSbItmOutros.Name = "brSbItmOutros";
            // 
            // brSbItmTipo
            // 
            this.brSbItmTipo.Caption = "Tipo";
            this.brSbItmTipo.Id = 6;
            this.brSbItmTipo.Name = "brSbItmTipo";
            // 
            // barSubItem4
            // 
            this.barSubItem4.Caption = "Eixos";
            this.barSubItem4.Id = 7;
            this.barSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmEixoX),
            new DevExpress.XtraBars.LinkPersistInfo(this.brChckItmEixoY)});
            this.barSubItem4.Name = "barSubItem4";
            // 
            // brChckItmEixoX
            // 
            this.brChckItmEixoX.Caption = "Linha";
            this.brChckItmEixoX.Checked = true;
            this.brChckItmEixoX.Id = 8;
            this.brChckItmEixoX.Name = "brChckItmEixoX";
            this.brChckItmEixoX.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmEixoY
            // 
            this.brChckItmEixoY.Caption = "Coluna";
            this.brChckItmEixoY.Checked = true;
            this.brChckItmEixoY.Id = 9;
            this.brChckItmEixoY.Name = "brChckItmEixoY";
            this.brChckItmEixoY.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmLegenda
            // 
            this.brChckItmLegenda.Caption = "Legenda";
            this.brChckItmLegenda.Checked = true;
            this.brChckItmLegenda.Id = 10;
            this.brChckItmLegenda.Name = "brChckItmLegenda";
            this.brChckItmLegenda.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmValor
            // 
            this.brChckItmValor.Caption = "Valor";
            this.brChckItmValor.Checked = true;
            this.brChckItmValor.Id = 11;
            this.brChckItmValor.Name = "brChckItmValor";
            this.brChckItmValor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmVirar
            // 
            this.brChckItmVirar.Caption = "Virar";
            this.brChckItmVirar.Checked = true;
            this.brChckItmVirar.Id = 14;
            this.brChckItmVirar.Name = "brChckItmVirar";
            this.brChckItmVirar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmSomenteOSelecionado
            // 
            this.brChckItmSomenteOSelecionado.Caption = "Somente o que está selecionado";
            this.brChckItmSomenteOSelecionado.Id = 13;
            this.brChckItmSomenteOSelecionado.Name = "brChckItmSomenteOSelecionado";
            this.brChckItmSomenteOSelecionado.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmMostrarTotalColuna
            // 
            this.brChckItmMostrarTotalColuna.Caption = "Mostrar total do coluna";
            this.brChckItmMostrarTotalColuna.Id = 16;
            this.brChckItmMostrarTotalColuna.Name = "brChckItmMostrarTotalColuna";
            this.brChckItmMostrarTotalColuna.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmMostrarTotalLinha
            // 
            this.brChckItmMostrarTotalLinha.Caption = "Mostrar total da linha";
            this.brChckItmMostrarTotalLinha.Id = 17;
            this.brChckItmMostrarTotalLinha.Name = "brChckItmMostrarTotalLinha";
            this.brChckItmMostrarTotalLinha.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmAlterarLinhasPorColunas
            // 
            this.brChckItmAlterarLinhasPorColunas.Caption = "Alterar linhas por colunas";
            this.brChckItmAlterarLinhasPorColunas.Checked = true;
            this.brChckItmAlterarLinhasPorColunas.Id = 18;
            this.brChckItmAlterarLinhasPorColunas.Name = "brChckItmAlterarLinhasPorColunas";
            this.brChckItmAlterarLinhasPorColunas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_ItemClick);
            // 
            // brChckItmMarcarParaImpressao
            // 
            this.brChckItmMarcarParaImpressao.Caption = "Marcar para impressão";
            this.brChckItmMarcarParaImpressao.Id = 26;
            this.brChckItmMarcarParaImpressao.Name = "brChckItmMarcarParaImpressao";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(567, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 534);
            this.barDockControlBottom.Size = new System.Drawing.Size(567, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 534);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(567, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 534);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 1;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 2;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Caption = "barCheckItem2";
            this.barCheckItem2.Id = 3;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 4;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barCheckItem7
            // 
            this.barCheckItem7.Caption = "ssssssssssss";
            this.barCheckItem7.Id = 12;
            this.barCheckItem7.Name = "barCheckItem7";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Mostrar total da linha";
            this.barButtonItem3.Id = 15;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Imprimir";
            this.barButtonItem4.Id = 25;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // CustomizarGridControlVisaoGrafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spltCtrlCustonGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomizarGridControlVisaoGrafico";
            this.Size = new System.Drawing.Size(567, 534);
            this.Load += new System.EventHandler(this.CustomizarGridControlPivotGridGrafico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spltCtrlCustonGrid)).EndInit();
            this.spltCtrlCustonGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pvtGrdVisualizacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtCtrlVisualizacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brMngrCustomGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppupMnParametrosGrafico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.SplitContainerControl spltCtrlCustonGrid;
        internal DevExpress.XtraPivotGrid.PivotGridControl pvtGrdVisualizacoes;
        private System.Windows.Forms.ContextMenuStrip ctxtMnStrpVisao;
        public DevExpress.XtraCharts.ChartControl chtCtrlVisualizacoes;
        private DevExpress.XtraBars.BarManager brMngrCustomGrid;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.PopupMenu ppupMnParametrosGrafico;
        private DevExpress.XtraBars.BarSubItem brSbItmLayout;
        private DevExpress.XtraBars.BarSubItem brSbItmTipo;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarCheckItem brChckItmEixoX;
        private DevExpress.XtraBars.BarCheckItem brChckItmEixoY;
        private DevExpress.XtraBars.BarCheckItem brChckItmLegenda;
        private DevExpress.XtraBars.BarCheckItem brChckItmValor;
        private DevExpress.XtraBars.BarCheckItem barCheckItem7;
        private DevExpress.XtraBars.BarCheckItem brChckItmSomenteOSelecionado;
        private DevExpress.XtraBars.BarCheckItem brChckItmVirar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarCheckItem brChckItmMostrarTotalColuna;
        private DevExpress.XtraBars.BarCheckItem brChckItmMostrarTotalLinha;
        private DevExpress.XtraBars.BarCheckItem brChckItmAlterarLinhasPorColunas;
        private DevExpress.XtraBars.BarSubItem brSbItmBarra;
        private DevExpress.XtraBars.BarSubItem brSbItmLinha;
        private DevExpress.XtraBars.BarSubItem brSbItmArea;
        private DevExpress.XtraBars.BarSubItem brSbItmPizza;
        private DevExpress.XtraBars.BarSubItem brSbItmOutros;
        private DevExpress.XtraBars.BarSubItem brSbItmRadar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        internal DevExpress.XtraBars.BarCheckItem brChckItmMarcarParaImpressao;
    }
}
