namespace Globus5.WPF.Comum.Pesquisas
{
    partial class MasterPanelBrowseDePesquisaTelaGenerica
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
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica = new FGlobus.Componentes.WinForms.BrowseDePesquisaBGM(this.components);
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.objVwMFBrowseDePesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCtrlTelaMP)).BeginInit();
            this.grpCtrlTelaMP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVwMasterPanelBrowseDePesquisaTelaGenerica)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCtrlTelaMP
            // 
            this.grpCtrlTelaMP.Controls.Add(this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica);
            this.grpCtrlTelaMP.Size = new System.Drawing.Size(720, 450);
            // 
            // browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica
            // 
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.DataSource = this.objVwMFBrowseDePesquisa;
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.FormSize = new System.Drawing.Size(720, 450);
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.Location = new System.Drawing.Point(0, 0);
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.MainView = this.gridVwMasterPanelBrowseDePesquisaTelaGenerica;
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.MasterPanelBrowseDePesquisa = this;
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.Name = "browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica";
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.Size = new System.Drawing.Size(720, 450);
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.TabIndex = 0;
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.TamanhoForm = FGlobus.Componentes.WinForms.BrowseDePesquisaBGM.eTamanhoForm.Pequeno;
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica});
            this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica.Pesquisar += new FGlobus.Componentes.WinForms.BrowseDePesquisaEventHandler(this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica_Pesquisar);
            // 
            // gridVwMasterPanelBrowseDePesquisaTelaGenerica
            // 
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.GridControl = this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.GroupPanelText = "Arraste o cabeçalho da coluna para visualizá-la agrupada.";
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.Name = "gridVwMasterPanelBrowseDePesquisaTelaGenerica";
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.OptionsBehavior.Editable = false;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.OptionsView.ColumnAutoWidth = false;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.OptionsView.ShowAutoFilterRow = true;
            this.gridVwMasterPanelBrowseDePesquisaTelaGenerica.OptionsView.ShowIndicator = false;
            // 
            // MasterPanelBrowseDePesquisaTelaGenerica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MasterPanelBrowseDePesquisaTelaGenerica";
            this.Size = new System.Drawing.Size(720, 450);
            ((System.ComponentModel.ISupportInitialize)(this.objVwMFBrowseDePesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCtrlTelaMP)).EndInit();
            this.grpCtrlTelaMP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVwMasterPanelBrowseDePesquisaTelaGenerica)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FGlobus.Componentes.WinForms.BrowseDePesquisaBGM browseDePesquisaBGMMasterPanelBrowseDePesquisaTelaGenerica;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVwMasterPanelBrowseDePesquisaTelaGenerica;
    }
}
