namespace Globus5.WPF.Comum
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.rbnMFMenuPrincipal = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.appMnMenuPrincipal = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.brBtnItmSaida = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnItmUsuario = new DevExpress.XtraBars.BarButtonItem();
            this.brStcItmVersao = new DevExpress.XtraBars.BarStaticItem();
            this.brEdtItmMensagemBarra = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.brBtnItmCalculadora = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnItmGlobusReport = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnItmAgenda = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnItmFavoritos = new DevExpress.XtraBars.BarButtonItem();
            this.brSbItmUsuarioMFMnPrncpl = new DevExpress.XtraBars.BarSubItem();
            this.brBtnItmTrocarUsuario = new DevExpress.XtraBars.BarButtonItem();
            this.brBtnItmCadastrarNovaSenha = new DevExpress.XtraBars.BarButtonItem();
            this.brBttnItmBgmRodotec = new DevExpress.XtraBars.BarButtonItem();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.brStcItmAjudaMFMenuPrincipal = new DevExpress.XtraBars.BarSubItem();
            this.brBttnItmAjuda = new DevExpress.XtraBars.BarButtonItem();
            this.brBttnItmSobre = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageCategory1 = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.rbnFavoritos = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rbPgGrpFavoritos = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbnStsBrMFMenuPrincipal = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dfltLkFlMFMenuPrincipal = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.rbnPgFavoritosMasterForm = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rbnMFMenuPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMnMenuPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // rbnMFMenuPrincipal
            // 
            this.rbnMFMenuPrincipal.ApplicationButtonDropDownControl = this.appMnMenuPrincipal;
            resources.ApplyResources(this.rbnMFMenuPrincipal, "rbnMFMenuPrincipal");
            this.rbnMFMenuPrincipal.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.brBtnItmUsuario,
            this.brStcItmVersao,
            this.brEdtItmMensagemBarra,
            this.brBtnItmCalculadora,
            this.brBtnItmGlobusReport,
            this.brBtnItmAgenda,
            this.brBtnItmFavoritos,
            this.brSbItmUsuarioMFMnPrncpl,
            this.brBtnItmCadastrarNovaSenha,
            this.brBtnItmTrocarUsuario,
            this.brBtnItmSaida,
            this.brBttnItmBgmRodotec,
            this.barLinkContainerItem1,
            this.barMdiChildrenListItem1,
            this.brStcItmAjudaMFMenuPrincipal,
            this.brBttnItmAjuda,
            this.brBttnItmSobre});
            this.rbnMFMenuPrincipal.MaxItemId = 117;
            this.rbnMFMenuPrincipal.Name = "rbnMFMenuPrincipal";
            this.rbnMFMenuPrincipal.PageCategories.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageCategory[] {
            this.ribbonPageCategory1});
            this.rbnMFMenuPrincipal.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Right;
            this.rbnMFMenuPrincipal.PageHeaderItemLinks.Add(this.brBttnItmBgmRodotec);
            this.rbnMFMenuPrincipal.PageHeaderItemLinks.Add(this.brStcItmAjudaMFMenuPrincipal);
            this.rbnMFMenuPrincipal.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.rbnMFMenuPrincipal.SelectedPage = this.rbnFavoritos;
            this.rbnMFMenuPrincipal.ShowToolbarCustomizeItem = false;
            this.rbnMFMenuPrincipal.StatusBar = this.rbnStsBrMFMenuPrincipal;
            this.rbnMFMenuPrincipal.Toolbar.ItemLinks.Add(this.brBtnItmCalculadora);
            this.rbnMFMenuPrincipal.Toolbar.ItemLinks.Add(this.brBtnItmGlobusReport);
            this.rbnMFMenuPrincipal.Toolbar.ItemLinks.Add(this.brBtnItmAgenda);
            this.rbnMFMenuPrincipal.Toolbar.ShowCustomizeItem = false;
            // 
            // appMnMenuPrincipal
            // 
            this.appMnMenuPrincipal.BottomPaneControlContainer = null;
            this.appMnMenuPrincipal.ItemLinks.Add(this.brBtnItmSaida, true);
            this.appMnMenuPrincipal.Name = "appMnMenuPrincipal";
            this.appMnMenuPrincipal.Ribbon = this.rbnMFMenuPrincipal;
            this.appMnMenuPrincipal.RightPaneControlContainer = null;
            // 
            // brBtnItmSaida
            // 
            resources.ApplyResources(this.brBtnItmSaida, "brBtnItmSaida");
            this.brBtnItmSaida.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmSaida.Glyph")));
            this.brBtnItmSaida.Id = 53;
            this.brBtnItmSaida.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.brBtnItmSaida.Name = "brBtnItmSaida";
            this.brBtnItmSaida.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnItmSaida_ItemClick);
            // 
            // brBtnItmUsuario
            // 
            resources.ApplyResources(this.brBtnItmUsuario, "brBtnItmUsuario");
            this.brBtnItmUsuario.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmUsuario.Glyph")));
            this.brBtnItmUsuario.Id = 4;
            this.brBtnItmUsuario.Name = "brBtnItmUsuario";
            // 
            // brStcItmVersao
            // 
            this.brStcItmVersao.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            resources.ApplyResources(this.brStcItmVersao, "brStcItmVersao");
            this.brStcItmVersao.Id = 7;
            this.brStcItmVersao.Name = "brStcItmVersao";
            this.brStcItmVersao.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // brEdtItmMensagemBarra
            // 
            this.brEdtItmMensagemBarra.Edit = this.repositoryItemTextEdit1;
            this.brEdtItmMensagemBarra.EditValue = "";
            this.brEdtItmMensagemBarra.Enabled = false;
            this.brEdtItmMensagemBarra.Id = 9;
            this.brEdtItmMensagemBarra.Name = "brEdtItmMensagemBarra";
            resources.ApplyResources(this.brEdtItmMensagemBarra, "brEdtItmMensagemBarra");
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.repositoryItemTextEdit1.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.repositoryItemTextEdit1.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.repositoryItemTextEdit1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.repositoryItemTextEdit1.Appearance.Options.UseBackColor = true;
            this.repositoryItemTextEdit1.Appearance.Options.UseBorderColor = true;
            this.repositoryItemTextEdit1.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit1.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.repositoryItemTextEdit1.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.repositoryItemTextEdit1.AppearanceDisabled.Options.UseBackColor = true;
            this.repositoryItemTextEdit1.AppearanceDisabled.Options.UseForeColor = true;
            resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
            this.repositoryItemTextEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // brBtnItmCalculadora
            // 
            resources.ApplyResources(this.brBtnItmCalculadora, "brBtnItmCalculadora");
            this.brBtnItmCalculadora.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmCalculadora.Glyph")));
            this.brBtnItmCalculadora.Id = 10;
            this.brBtnItmCalculadora.Name = "brBtnItmCalculadora";
            this.brBtnItmCalculadora.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnItmCalculadora_ItemClick);
            // 
            // brBtnItmGlobusReport
            // 
            resources.ApplyResources(this.brBtnItmGlobusReport, "brBtnItmGlobusReport");
            this.brBtnItmGlobusReport.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmGlobusReport.Glyph")));
            this.brBtnItmGlobusReport.Id = 11;
            this.brBtnItmGlobusReport.Name = "brBtnItmGlobusReport";
            this.brBtnItmGlobusReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnItmGlobusReport_ItemClick);
            // 
            // brBtnItmAgenda
            // 
            resources.ApplyResources(this.brBtnItmAgenda, "brBtnItmAgenda");
            this.brBtnItmAgenda.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmAgenda.Glyph")));
            this.brBtnItmAgenda.Id = 12;
            this.brBtnItmAgenda.Name = "brBtnItmAgenda";
            this.brBtnItmAgenda.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnItmAgenda_ItemClick);
            // 
            // brBtnItmFavoritos
            // 
            resources.ApplyResources(this.brBtnItmFavoritos, "brBtnItmFavoritos");
            this.brBtnItmFavoritos.DropDownEnabled = false;
            this.brBtnItmFavoritos.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmFavoritos.Glyph")));
            this.brBtnItmFavoritos.Id = 14;
            this.brBtnItmFavoritos.Name = "brBtnItmFavoritos";
            this.brBtnItmFavoritos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // brSbItmUsuarioMFMnPrncpl
            // 
            resources.ApplyResources(this.brSbItmUsuarioMFMnPrncpl, "brSbItmUsuarioMFMnPrncpl");
            this.brSbItmUsuarioMFMnPrncpl.Glyph = ((System.Drawing.Image)(resources.GetObject("brSbItmUsuarioMFMnPrncpl.Glyph")));
            this.brSbItmUsuarioMFMnPrncpl.Id = 15;
            this.brSbItmUsuarioMFMnPrncpl.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brBtnItmTrocarUsuario),
            new DevExpress.XtraBars.LinkPersistInfo(this.brBtnItmCadastrarNovaSenha)});
            this.brSbItmUsuarioMFMnPrncpl.Name = "brSbItmUsuarioMFMnPrncpl";
            // 
            // brBtnItmTrocarUsuario
            // 
            resources.ApplyResources(this.brBtnItmTrocarUsuario, "brBtnItmTrocarUsuario");
            this.brBtnItmTrocarUsuario.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmTrocarUsuario.Glyph")));
            this.brBtnItmTrocarUsuario.Id = 17;
            this.brBtnItmTrocarUsuario.Name = "brBtnItmTrocarUsuario";
            this.brBtnItmTrocarUsuario.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnItmTrocarUsuario_ItemClick);
            // 
            // brBtnItmCadastrarNovaSenha
            // 
            resources.ApplyResources(this.brBtnItmCadastrarNovaSenha, "brBtnItmCadastrarNovaSenha");
            this.brBtnItmCadastrarNovaSenha.Glyph = ((System.Drawing.Image)(resources.GetObject("brBtnItmCadastrarNovaSenha.Glyph")));
            this.brBtnItmCadastrarNovaSenha.Id = 16;
            this.brBtnItmCadastrarNovaSenha.Name = "brBtnItmCadastrarNovaSenha";
            this.brBtnItmCadastrarNovaSenha.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBtnItmCadastrarNovaSenha_ItemClick);
            // 
            // brBttnItmBgmRodotec
            // 
            resources.ApplyResources(this.brBttnItmBgmRodotec, "brBttnItmBgmRodotec");
            this.brBttnItmBgmRodotec.Glyph = ((System.Drawing.Image)(resources.GetObject("brBttnItmBgmRodotec.Glyph")));
            this.brBttnItmBgmRodotec.Id = 54;
            this.brBttnItmBgmRodotec.Name = "brBttnItmBgmRodotec";
            this.brBttnItmBgmRodotec.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBttnItmBgmRodotec_ItemClick);
            // 
            // barLinkContainerItem1
            // 
            resources.ApplyResources(this.barLinkContainerItem1, "barLinkContainerItem1");
            this.barLinkContainerItem1.Id = 56;
            this.barLinkContainerItem1.Name = "barLinkContainerItem1";
            // 
            // barMdiChildrenListItem1
            // 
            resources.ApplyResources(this.barMdiChildrenListItem1, "barMdiChildrenListItem1");
            this.barMdiChildrenListItem1.Id = 59;
            this.barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            // 
            // brStcItmAjudaMFMenuPrincipal
            // 
            resources.ApplyResources(this.brStcItmAjudaMFMenuPrincipal, "brStcItmAjudaMFMenuPrincipal");
            this.brStcItmAjudaMFMenuPrincipal.Glyph = ((System.Drawing.Image)(resources.GetObject("brStcItmAjudaMFMenuPrincipal.Glyph")));
            this.brStcItmAjudaMFMenuPrincipal.Id = 61;
            this.brStcItmAjudaMFMenuPrincipal.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.brBttnItmAjuda),
            new DevExpress.XtraBars.LinkPersistInfo(this.brBttnItmSobre)});
            this.brStcItmAjudaMFMenuPrincipal.Name = "brStcItmAjudaMFMenuPrincipal";
            // 
            // brBttnItmAjuda
            // 
            resources.ApplyResources(this.brBttnItmAjuda, "brBttnItmAjuda");
            this.brBttnItmAjuda.Id = 62;
            this.brBttnItmAjuda.Name = "brBttnItmAjuda";
            this.brBttnItmAjuda.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBttnItmAjuda_ItemClick);
            // 
            // brBttnItmSobre
            // 
            resources.ApplyResources(this.brBttnItmSobre, "brBttnItmSobre");
            this.brBttnItmSobre.Id = 63;
            this.brBttnItmSobre.Name = "brBttnItmSobre";
            this.brBttnItmSobre.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.brBttnItmSobre_ItemClick);
            // 
            // ribbonPageCategory1
            // 
            resources.ApplyResources(this.ribbonPageCategory1, "ribbonPageCategory1");
            this.ribbonPageCategory1.Name = "ribbonPageCategory1";
            this.ribbonPageCategory1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rbnFavoritos});
            // 
            // rbnFavoritos
            // 
            this.rbnFavoritos.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rbPgGrpFavoritos});
            this.rbnFavoritos.Image = ((System.Drawing.Image)(resources.GetObject("rbnFavoritos.Image")));
            this.rbnFavoritos.Name = "rbnFavoritos";
            resources.ApplyResources(this.rbnFavoritos, "rbnFavoritos");
            // 
            // rbPgGrpFavoritos
            // 
            this.rbPgGrpFavoritos.ItemLinks.Add(this.brBtnItmFavoritos);
            this.rbPgGrpFavoritos.Name = "rbPgGrpFavoritos";
            // 
            // rbnStsBrMFMenuPrincipal
            // 
            this.rbnStsBrMFMenuPrincipal.ItemLinks.Add(this.brSbItmUsuarioMFMnPrncpl);
            this.rbnStsBrMFMenuPrincipal.ItemLinks.Add(this.brStcItmVersao, true);
            this.rbnStsBrMFMenuPrincipal.ItemLinks.Add(this.brEdtItmMensagemBarra);
            resources.ApplyResources(this.rbnStsBrMFMenuPrincipal, "rbnStsBrMFMenuPrincipal");
            this.rbnStsBrMFMenuPrincipal.Name = "rbnStsBrMFMenuPrincipal";
            this.rbnStsBrMFMenuPrincipal.Ribbon = this.rbnMFMenuPrincipal;
            this.rbnStsBrMFMenuPrincipal.ShowSizeGrip = false;
            // 
            // dfltLkFlMFMenuPrincipal
            // 
            this.dfltLkFlMFMenuPrincipal.LookAndFeel.SkinName = "Globus5Black";
            // 
            // rbnPgFavoritosMasterForm
            // 
            this.rbnPgFavoritosMasterForm.Name = "rbnPgFavoritosMasterForm";
            resources.ApplyResources(this.rbnPgFavoritosMasterForm, "rbnPgFavoritosMasterForm");
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            // 
            // MenuPrincipal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbnStsBrMFMenuPrincipal);
            this.Controls.Add(this.rbnMFMenuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "MenuPrincipal";
            this.Ribbon = this.rbnMFMenuPrincipal;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StatusBar = this.rbnStsBrMFMenuPrincipal;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuPrincipal_FormClosing);
            this.Shown += new System.EventHandler(this.MenuPrincipal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.rbnMFMenuPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMnMenuPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar rbnStsBrMFMenuPrincipal;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private DevExpress.XtraBars.Ribbon.RibbonControl rbnMFMenuPrincipal;
        private DevExpress.LookAndFeel.DefaultLookAndFeel dfltLkFlMFMenuPrincipal;
        private DevExpress.XtraBars.BarButtonItem brBtnItmUsuario;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem brBtnItmCalculadora;
        private DevExpress.XtraBars.BarButtonItem brBtnItmGlobusReport;
        private DevExpress.XtraBars.BarButtonItem brBtnItmAgenda;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbnPgFavoritosMasterForm;
        private DevExpress.XtraBars.BarButtonItem brBtnItmCadastrarNovaSenha;
        private DevExpress.XtraBars.BarButtonItem brBtnItmTrocarUsuario;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu appMnMenuPrincipal;
        private DevExpress.XtraBars.BarButtonItem brBtnItmSaida;
        private DevExpress.XtraBars.BarButtonItem brBttnItmBgmRodotec;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem1;
        private DevExpress.XtraBars.BarMdiChildrenListItem barMdiChildrenListItem1;
        private DevExpress.XtraBars.BarSubItem brStcItmAjudaMFMenuPrincipal;
        private DevExpress.XtraBars.BarButtonItem brBttnItmAjuda;
        private DevExpress.XtraBars.BarButtonItem brBttnItmSobre;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory ribbonPageCategory1;
        private DevExpress.XtraBars.BarSubItem brSbItmUsuarioMFMnPrncpl;
        private DevExpress.XtraBars.BarStaticItem brStcItmVersao;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbnFavoritos;
        private DevExpress.XtraBars.BarEditItem brEdtItmMensagemBarra;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbPgGrpFavoritos;
        private DevExpress.XtraBars.BarButtonItem brBtnItmFavoritos;
    }
}





