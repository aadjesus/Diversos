namespace testpaintcontrol
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
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemListEditor1 = new testpaintcontrol.RepositoryItemListEditor();
            this.listEditor1 = new testpaintcontrol.ListEditor();
            this.listEditor2 = new testpaintcontrol.ListEditor();
            this.pedeEmpresaBGM1 = new FGlobus.Componentes.WinForms.PedeEmpresaBGM();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemListEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listEditor1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listEditor2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedeEmpresaBGM1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedeEmpresaBGM1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.labelControl1);
            this.popupContainerControl1.Location = new System.Drawing.Point(12, 64);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(249, 113);
            this.popupContainerControl1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(19, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "test";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 183);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemListEditor1});
            this.gridControl1.Size = new System.Drawing.Size(249, 272);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.ColumnEdit = this.repositoryItemListEditor1;
            this.gridColumn1.FieldName = "test";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // repositoryItemListEditor1
            // 
            this.repositoryItemListEditor1.AutoHeight = false;
            this.repositoryItemListEditor1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemListEditor1.Name = "repositoryItemListEditor1";
            // 
            // listEditor1
            // 
            this.listEditor1.Location = new System.Drawing.Point(12, 12);
            this.listEditor1.Name = "listEditor1";
            this.listEditor1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listEditor1.Properties.PopupControl = this.popupContainerControl1;
            this.listEditor1.Size = new System.Drawing.Size(249, 20);
            this.listEditor1.TabIndex = 8;
            // 
            // listEditor2
            // 
            this.listEditor2.Location = new System.Drawing.Point(12, 38);
            this.listEditor2.Name = "listEditor2";
            this.listEditor2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listEditor2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.listEditor2.Size = new System.Drawing.Size(249, 20);
            this.listEditor2.TabIndex = 9;
            // 
            // pedeEmpresaBGM1
            // 
            this.pedeEmpresaBGM1.EmpresasAutorizadas = null;
            this.pedeEmpresaBGM1.EmpresaSelecionada = null;
            this.pedeEmpresaBGM1.Location = new System.Drawing.Point(304, 99);
            this.pedeEmpresaBGM1.Name = "pedeEmpresaBGM1";
            this.pedeEmpresaBGM1.NenhumaEmpresaEncontrada = null;
            this.pedeEmpresaBGM1.PopularAutomatico = false;
            this.pedeEmpresaBGM1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.pedeEmpresaBGM1.SelectedIndex = 0;
            this.pedeEmpresaBGM1.Size = new System.Drawing.Size(100, 20);
            this.pedeEmpresaBGM1.TabIndex = 10;
            this.pedeEmpresaBGM1.ValidarUsuario = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 467);
            this.Controls.Add(this.pedeEmpresaBGM1);
            this.Controls.Add(this.listEditor2);
            this.Controls.Add(this.listEditor1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.popupContainerControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.popupContainerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemListEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listEditor1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listEditor2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedeEmpresaBGM1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedeEmpresaBGM1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private ListEditor listEditor1;
        private RepositoryItemListEditor repositoryItemListEditor1;
        private ListEditor listEditor2;
        private FGlobus.Componentes.WinForms.PedeEmpresaBGM pedeEmpresaBGM1;

    }
}

