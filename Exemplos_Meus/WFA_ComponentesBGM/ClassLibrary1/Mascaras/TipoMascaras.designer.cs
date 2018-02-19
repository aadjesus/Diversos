namespace FGlobus.Componentes.WinForms.ControleDeComponente
{
    partial class TipoMascaras
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
            this.grdCtrlTipoMascaras = new DevExpress.XtraGrid.GridControl();
            this.grdVwTipoMascaras = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdClmnTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdClmnExemplo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpstryItmChckEdtAtivo = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rpstryItmChckEdtRecEmailAtu = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rpstryItmRdGrpSexo = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpstryItmTxtEdtCelular = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpstryItmTxtEdtRg = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpstryItmTxtEdtCpf = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrlTipoMascaras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwTipoMascaras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmChckEdtAtivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmChckEdtRecEmailAtu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmRdGrpSexo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmTxtEdtCelular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmTxtEdtRg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmTxtEdtCpf)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCtrlTipoMascaras
            // 
            this.grdCtrlTipoMascaras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCtrlTipoMascaras.Location = new System.Drawing.Point(0, 0);
            this.grdCtrlTipoMascaras.MainView = this.grdVwTipoMascaras;
            this.grdCtrlTipoMascaras.Name = "grdCtrlTipoMascaras";
            this.grdCtrlTipoMascaras.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpstryItmChckEdtAtivo,
            this.rpstryItmChckEdtRecEmailAtu,
            this.rpstryItmRdGrpSexo,
            this.rpstryItmTxtEdtCelular,
            this.rpstryItmTxtEdtRg,
            this.rpstryItmTxtEdtCpf});
            this.grdCtrlTipoMascaras.ShowOnlyPredefinedDetails = true;
            this.grdCtrlTipoMascaras.Size = new System.Drawing.Size(475, 173);
            this.grdCtrlTipoMascaras.TabIndex = 27;
            this.grdCtrlTipoMascaras.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdVwTipoMascaras});
            // 
            // grdVwTipoMascaras
            // 
            this.grdVwTipoMascaras.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grdVwTipoMascaras.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grdClmnTipo,
            this.grdClmnExemplo});
            this.grdVwTipoMascaras.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grdVwTipoMascaras.GridControl = this.grdCtrlTipoMascaras;
            this.grdVwTipoMascaras.GroupPanelText = "Arraste o cabeçalho da coluna para visualizá-la agrupada.";
            this.grdVwTipoMascaras.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Horas", null, "( Horas: {0}", "3"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Total", null, "Total: {0:c} )", "4")});
            this.grdVwTipoMascaras.Name = "grdVwTipoMascaras";
            this.grdVwTipoMascaras.OptionsBehavior.AutoExpandAllGroups = true;
            this.grdVwTipoMascaras.OptionsCustomization.AllowFilter = false;
            this.grdVwTipoMascaras.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grdVwTipoMascaras.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grdVwTipoMascaras.OptionsView.ShowGroupPanel = false;
            this.grdVwTipoMascaras.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.grdVwTipoMascaras_RowClick);
            this.grdVwTipoMascaras.DoubleClick += new System.EventHandler(this.grdVwTipoMascaras_DoubleClick);
            // 
            // grdClmnTipo
            // 
            this.grdClmnTipo.Caption = "Tipo";
            this.grdClmnTipo.FieldName = "DescricaoTipo";
            this.grdClmnTipo.Name = "grdClmnTipo";
            this.grdClmnTipo.OptionsColumn.AllowEdit = false;
            this.grdClmnTipo.OptionsColumn.AllowFocus = false;
            this.grdClmnTipo.OptionsColumn.TabStop = false;
            this.grdClmnTipo.Visible = true;
            this.grdClmnTipo.VisibleIndex = 0;
            this.grdClmnTipo.Width = 200;
            // 
            // grdClmnExemplo
            // 
            this.grdClmnExemplo.Caption = "Exemplo";
            this.grdClmnExemplo.FieldName = "Exemplo";
            this.grdClmnExemplo.Name = "grdClmnExemplo";
            this.grdClmnExemplo.OptionsColumn.AllowEdit = false;
            this.grdClmnExemplo.OptionsColumn.AllowFocus = false;
            this.grdClmnExemplo.OptionsColumn.TabStop = false;
            this.grdClmnExemplo.Visible = true;
            this.grdClmnExemplo.VisibleIndex = 1;
            this.grdClmnExemplo.Width = 258;
            // 
            // rpstryItmChckEdtAtivo
            // 
            this.rpstryItmChckEdtAtivo.AutoHeight = false;
            this.rpstryItmChckEdtAtivo.Name = "rpstryItmChckEdtAtivo";
            this.rpstryItmChckEdtAtivo.ValueChecked = "A";
            this.rpstryItmChckEdtAtivo.ValueUnchecked = "I";
            // 
            // rpstryItmChckEdtRecEmailAtu
            // 
            this.rpstryItmChckEdtRecEmailAtu.AutoHeight = false;
            this.rpstryItmChckEdtRecEmailAtu.Name = "rpstryItmChckEdtRecEmailAtu";
            this.rpstryItmChckEdtRecEmailAtu.ValueChecked = "S";
            this.rpstryItmChckEdtRecEmailAtu.ValueUnchecked = "N";
            // 
            // rpstryItmRdGrpSexo
            // 
            this.rpstryItmRdGrpSexo.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("M", "Masculino     "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("F", "Feminino     ")});
            this.rpstryItmRdGrpSexo.Name = "rpstryItmRdGrpSexo";
            // 
            // rpstryItmTxtEdtCelular
            // 
            this.rpstryItmTxtEdtCelular.AutoHeight = false;
            this.rpstryItmTxtEdtCelular.Mask.EditMask = "(999) 0000-0000";
            this.rpstryItmTxtEdtCelular.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.rpstryItmTxtEdtCelular.Mask.UseMaskAsDisplayFormat = true;
            this.rpstryItmTxtEdtCelular.Name = "rpstryItmTxtEdtCelular";
            // 
            // rpstryItmTxtEdtRg
            // 
            this.rpstryItmTxtEdtRg.AutoHeight = false;
            this.rpstryItmTxtEdtRg.Mask.EditMask = "00.000.000-a";
            this.rpstryItmTxtEdtRg.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.rpstryItmTxtEdtRg.Mask.UseMaskAsDisplayFormat = true;
            this.rpstryItmTxtEdtRg.Name = "rpstryItmTxtEdtRg";
            // 
            // rpstryItmTxtEdtCpf
            // 
            this.rpstryItmTxtEdtCpf.AutoHeight = false;
            this.rpstryItmTxtEdtCpf.Mask.EditMask = "000.000.000-00";
            this.rpstryItmTxtEdtCpf.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.rpstryItmTxtEdtCpf.Mask.UseMaskAsDisplayFormat = true;
            this.rpstryItmTxtEdtCpf.Name = "rpstryItmTxtEdtCpf";
            // 
            // TipoMascaras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdCtrlTipoMascaras);
            this.Name = "TipoMascaras";
            this.Size = new System.Drawing.Size(475, 173);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrlTipoMascaras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwTipoMascaras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmChckEdtAtivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmChckEdtRecEmailAtu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmRdGrpSexo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmTxtEdtCelular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmTxtEdtRg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpstryItmTxtEdtCpf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl grdCtrlTipoMascaras;
        public DevExpress.XtraGrid.Views.Grid.GridView grdVwTipoMascaras;
        private DevExpress.XtraGrid.Columns.GridColumn grdClmnTipo;
        private DevExpress.XtraGrid.Columns.GridColumn grdClmnExemplo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpstryItmChckEdtAtivo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpstryItmChckEdtRecEmailAtu;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpstryItmRdGrpSexo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpstryItmTxtEdtCelular;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpstryItmTxtEdtRg;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpstryItmTxtEdtCpf;

    }
}
