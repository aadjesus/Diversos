namespace WFA_ListagemPesquisas
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rpstryItmBtnEdtTitulo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.tmControlarThead = new System.Windows.Forms.Timer(this.components);
            this.xtrTbCtrlPesquisas = new DevExpress.XtraTab.XtraTabControl();
            this.xtrTbPgInicial = new DevExpress.XtraTab.XtraTabPage();
            this.tblLytPnlAguarde = new System.Windows.Forms.TableLayoutPanel();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.elmntHstLogoGlobus = new System.Windows.Forms.Integration.ElementHost();
            this.pnlCtrlPesquisas = new DevExpress.XtraEditors.PanelControl();
            this.pnlCtrlNavegacao = new DevExpress.XtraEditors.PanelControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.trLstClmID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trLstClmParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trLstClmTitulo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trLstClmMostrou = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rpstryItmChEdtMostrou = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.dataSet1 = new System.Data.DataSet();
            this.dtTblTblTreeList = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmBtnEdtTitulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtrTbCtrlPesquisas)).BeginInit();
            this.xtrTbCtrlPesquisas.SuspendLayout();
            this.xtrTbPgInicial.SuspendLayout();
            this.tblLytPnlAguarde.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlPesquisas)).BeginInit();
            this.pnlCtrlPesquisas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlNavegacao)).BeginInit();
            this.pnlCtrlNavegacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmChEdtMostrou)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTblTblTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rpstryItmBtnEdtTitulo
            // 
            this.rpstryItmBtnEdtTitulo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rpstryItmBtnEdtTitulo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rpstryItmBtnEdtTitulo.HideSelection = false;
            this.rpstryItmBtnEdtTitulo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.rpstryItmBtnEdtTitulo.Name = "rpstryItmBtnEdtTitulo";
            this.rpstryItmBtnEdtTitulo.ReadOnly = true;
            this.rpstryItmBtnEdtTitulo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rpstryItmBtnEdtTitulo_ButtonClick);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 10;
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 0;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 158;
            this.navBarControl1.Size = new System.Drawing.Size(158, 586);
            this.navBarControl1.TabIndex = 2;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "navBarGroup1";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Appearance.BackColor = System.Drawing.Color.Red;
            this.navBarItem1.Appearance.Options.UseBackColor = true;
            this.navBarItem1.AppearanceDisabled.BackColor = System.Drawing.Color.Red;
            this.navBarItem1.AppearanceDisabled.Options.UseBackColor = true;
            this.navBarItem1.AppearanceHotTracked.BackColor = System.Drawing.Color.Blue;
            this.navBarItem1.AppearanceHotTracked.Options.UseBackColor = true;
            this.navBarItem1.AppearancePressed.BackColor = System.Drawing.Color.Green;
            this.navBarItem1.AppearancePressed.Options.UseBackColor = true;
            this.navBarItem1.Caption = "navBarItem1";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // xtrTbCtrlPesquisas
            // 
            this.xtrTbCtrlPesquisas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xtrTbCtrlPesquisas.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtrTbCtrlPesquisas.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtrTbCtrlPesquisas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtrTbCtrlPesquisas.Location = new System.Drawing.Point(0, 0);
            this.xtrTbCtrlPesquisas.Name = "xtrTbCtrlPesquisas";
            this.xtrTbCtrlPesquisas.SelectedTabPage = this.xtrTbPgInicial;
            this.xtrTbCtrlPesquisas.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtrTbCtrlPesquisas.Size = new System.Drawing.Size(451, 608);
            this.xtrTbCtrlPesquisas.TabIndex = 3;
            this.xtrTbCtrlPesquisas.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtrTbPgInicial});
            this.xtrTbCtrlPesquisas.Visible = false;
            // 
            // xtrTbPgInicial
            // 
            this.xtrTbPgInicial.Controls.Add(this.tblLytPnlAguarde);
            this.xtrTbPgInicial.Name = "xtrTbPgInicial";
            this.xtrTbPgInicial.Size = new System.Drawing.Size(445, 602);
            this.xtrTbPgInicial.Text = "xtrTbPgInicial";
            // 
            // tblLytPnlAguarde
            // 
            this.tblLytPnlAguarde.ColumnCount = 3;
            this.tblLytPnlAguarde.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytPnlAguarde.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLytPnlAguarde.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytPnlAguarde.Controls.Add(this.progressPanel1, 1, 1);
            this.tblLytPnlAguarde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytPnlAguarde.Location = new System.Drawing.Point(0, 0);
            this.tblLytPnlAguarde.Name = "tblLytPnlAguarde";
            this.tblLytPnlAguarde.RowCount = 3;
            this.tblLytPnlAguarde.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytPnlAguarde.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblLytPnlAguarde.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytPnlAguarde.Size = new System.Drawing.Size(445, 602);
            this.tblLytPnlAguarde.TabIndex = 1;
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.Location = new System.Drawing.Point(125, 269);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.Size = new System.Drawing.Size(194, 64);
            this.progressPanel1.TabIndex = 0;
            this.progressPanel1.Text = "progressPanel1";
            // 
            // elmntHstLogoGlobus
            // 
            this.elmntHstLogoGlobus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elmntHstLogoGlobus.Location = new System.Drawing.Point(0, 0);
            this.elmntHstLogoGlobus.Name = "elmntHstLogoGlobus";
            this.elmntHstLogoGlobus.Size = new System.Drawing.Size(451, 608);
            this.elmntHstLogoGlobus.TabIndex = 4;
            this.elmntHstLogoGlobus.Text = "elementHost1";
            this.elmntHstLogoGlobus.Child = null;
            // 
            // pnlCtrlPesquisas
            // 
            this.pnlCtrlPesquisas.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCtrlPesquisas.Controls.Add(this.xtrTbCtrlPesquisas);
            this.pnlCtrlPesquisas.Controls.Add(this.elmntHstLogoGlobus);
            this.pnlCtrlPesquisas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCtrlPesquisas.Location = new System.Drawing.Point(520, 0);
            this.pnlCtrlPesquisas.Name = "pnlCtrlPesquisas";
            this.pnlCtrlPesquisas.Size = new System.Drawing.Size(451, 608);
            this.pnlCtrlPesquisas.TabIndex = 5;
            // 
            // pnlCtrlNavegacao
            // 
            this.pnlCtrlNavegacao.Controls.Add(this.treeList1);
            this.pnlCtrlNavegacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCtrlNavegacao.Location = new System.Drawing.Point(158, 0);
            this.pnlCtrlNavegacao.Name = "pnlCtrlNavegacao";
            this.pnlCtrlNavegacao.Size = new System.Drawing.Size(362, 586);
            this.pnlCtrlNavegacao.TabIndex = 6;
            // 
            // treeList1
            // 
            this.treeList1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.trLstClmID,
            this.trLstClmParentID,
            this.trLstClmTitulo,
            this.trLstClmMostrou});
            this.treeList1.DataMember = "TblTreeList";
            this.treeList1.DataSource = this.dataSet1;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(2, 2);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.EnableFiltering = true;
            this.treeList1.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.OptionsView.ShowAutoFilterRow = true;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.OptionsView.ShowSummaryFooter = true;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpstryItmBtnEdtTitulo,
            this.rpstryItmChEdtMostrou});
            this.treeList1.Size = new System.Drawing.Size(358, 582);
            this.treeList1.TabIndex = 3;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // trLstClmID
            // 
            this.trLstClmID.Caption = "ID";
            this.trLstClmID.FieldName = "ID";
            this.trLstClmID.Name = "trLstClmID";
            // 
            // trLstClmParentID
            // 
            this.trLstClmParentID.Caption = "ParentID";
            this.trLstClmParentID.FieldName = "ParentID";
            this.trLstClmParentID.Name = "trLstClmParentID";
            // 
            // trLstClmTitulo
            // 
            this.trLstClmTitulo.Caption = "Titulo";
            this.trLstClmTitulo.ColumnEdit = this.rpstryItmBtnEdtTitulo;
            this.trLstClmTitulo.FieldName = "Titulo";
            this.trLstClmTitulo.MinWidth = 32;
            this.trLstClmTitulo.Name = "trLstClmTitulo";
            this.trLstClmTitulo.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.trLstClmTitulo.Visible = true;
            this.trLstClmTitulo.VisibleIndex = 0;
            this.trLstClmTitulo.Width = 323;
            // 
            // trLstClmMostrou
            // 
            this.trLstClmMostrou.Caption = "Mostrou";
            this.trLstClmMostrou.ColumnEdit = this.rpstryItmChEdtMostrou;
            this.trLstClmMostrou.FieldName = "Mostrou";
            this.trLstClmMostrou.Name = "trLstClmMostrou";
            this.trLstClmMostrou.OptionsColumn.FixedWidth = true;
            this.trLstClmMostrou.Visible = true;
            this.trLstClmMostrou.VisibleIndex = 1;
            this.trLstClmMostrou.Width = 35;
            // 
            // rpstryItmChEdtMostrou
            // 
            this.rpstryItmChEdtMostrou.AutoHeight = false;
            this.rpstryItmChEdtMostrou.Name = "rpstryItmChEdtMostrou";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dtTblTblTreeList});
            // 
            // dtTblTblTreeList
            // 
            this.dtTblTblTreeList.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5});
            this.dtTblTblTreeList.TableName = "TblTreeList";
            // 
            // dataColumn1
            // 
            this.dataColumn1.AutoIncrement = true;
            this.dataColumn1.ColumnName = "ID";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "ParentID";
            this.dataColumn2.DataType = typeof(int);
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Titulo";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Pesquisa";
            this.dataColumn4.DataType = typeof(object);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Mostrou";
            this.dataColumn5.DataType = typeof(bool);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.navBarControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(158, 586);
            this.panelControl1.TabIndex = 7;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioGroup1.Location = new System.Drawing.Point(0, 0);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "1"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "2")});
            this.radioGroup1.Size = new System.Drawing.Size(520, 22);
            this.radioGroup1.TabIndex = 8;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Controls.Add(this.radioGroup1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(520, 608);
            this.panelControl2.TabIndex = 9;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.pnlCtrlNavegacao);
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 22);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(520, 586);
            this.panelControl3.TabIndex = 9;
            // 
            // Form1
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 608);
            this.Controls.Add(this.pnlCtrlPesquisas);
            this.Controls.Add(this.panelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Tag = "Smart viewer";
            this.Text = "Smart viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmBtnEdtTitulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtrTbCtrlPesquisas)).EndInit();
            this.xtrTbCtrlPesquisas.ResumeLayout(false);
            this.xtrTbPgInicial.ResumeLayout(false);
            this.tblLytPnlAguarde.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlPesquisas)).EndInit();
            this.pnlCtrlPesquisas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlNavegacao)).EndInit();
            this.pnlCtrlNavegacao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmChEdtMostrou)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTblTblTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private System.Windows.Forms.Timer tmControlarThead;
        private DevExpress.XtraTab.XtraTabControl xtrTbCtrlPesquisas;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private System.Windows.Forms.TableLayoutPanel tblLytPnlAguarde;
        private DevExpress.XtraTab.XtraTabPage xtrTbPgInicial;
        private System.Windows.Forms.Integration.ElementHost elmntHstLogoGlobus;
        private DevExpress.XtraEditors.PanelControl pnlCtrlPesquisas;
        private DevExpress.XtraEditors.PanelControl pnlCtrlNavegacao;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rpstryItmBtnEdtTitulo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trLstClmID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trLstClmParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trLstClmTitulo;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dtTblTblTreeList;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trLstClmMostrou;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpstryItmChEdtMostrou;

    }
}

