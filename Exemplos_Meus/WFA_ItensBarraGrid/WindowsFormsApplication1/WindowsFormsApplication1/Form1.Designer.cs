namespace WindowsFormsApplication1
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemBarraItens2 = new FGlobus.Componentes.WinForms.ItemBarraItens();
            this.itemBarraItens1 = new FGlobus.Componentes.WinForms.ItemBarraItens();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.userControl11 = new ClassLibrary1.UserControl1();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.itemBarraItens2.AreaItem.SuspendLayout();
            this.itemBarraItens2.AreaItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            this.userControl11.AreaItem.SuspendLayout();
            this.userControl11.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Location = new System.Drawing.Point(10, 1);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Código";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(10, 20);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(93, 20);
            this.buttonEdit1.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(109, 1);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Descrição";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(109, 20);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(241, 20);
            this.textEdit1.TabIndex = 4;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(585, 217);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Código";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descrição";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // itemBarraItens2
            // 
            // 
            // itemBarraItens2.AreaItem
            // 
            this.itemBarraItens2.AreaItem.Controls.Add(this.labelControl1);
            this.itemBarraItens2.AreaItem.Controls.Add(this.buttonEdit1);
            this.itemBarraItens2.AreaItem.Controls.Add(this.textEdit1);
            this.itemBarraItens2.AreaItem.Controls.Add(this.labelControl2);
            this.itemBarraItens2.AreaItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.itemBarraItens2.AreaItem.Size = new System.Drawing.Size(585, 51);
            // 
            // itemBarraItens2.AreaItens
            // 
            this.itemBarraItens2.AreaItens.Controls.Add(this.gridControl1);
            this.itemBarraItens2.AreaItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemBarraItens2.AreaItens.Size = new System.Drawing.Size(585, 217);
            this.itemBarraItens2.Location = new System.Drawing.Point(24, 264);
            this.itemBarraItens2.MinimumSize = new System.Drawing.Size(260, 150);
            this.itemBarraItens2.Name = "itemBarraItens2";
            this.itemBarraItens2.Size = new System.Drawing.Size(589, 291);
            this.itemBarraItens2.TabIndex = 1;
            // 
            // itemBarraItens1
            // 
            // 
            // itemBarraItens1.AreaItem
            // 
            this.itemBarraItens1.AreaItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.itemBarraItens1.AreaItem.Size = new System.Drawing.Size(256, 51);
            // 
            // itemBarraItens1.AreaItens
            // 
            this.itemBarraItens1.AreaItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemBarraItens1.AreaItens.Size = new System.Drawing.Size(256, 76);
            this.itemBarraItens1.Location = new System.Drawing.Point(24, 12);
            this.itemBarraItens1.MinimumSize = new System.Drawing.Size(260, 150);
            this.itemBarraItens1.Name = "itemBarraItens1";
            this.itemBarraItens1.Size = new System.Drawing.Size(260, 150);
            this.itemBarraItens1.TabIndex = 2;
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(378, 213);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(100, 20);
            this.spinEdit1.TabIndex = 4;
            this.spinEdit1.EditValueChanged += new System.EventHandler(this.spinEdit1_EditValueChanged);
            // 
            // userControl11
            // 
            // 
            // userControl11.AreaItem
            // 
            this.userControl11.AreaItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl11.AreaItem.Horizontal = false;
            this.userControl11.AreaItem.Location = new System.Drawing.Point(0, 0);
            this.userControl11.AreaItem.Name = "AreaItem";
            this.userControl11.AreaItem.Panel1.Text = "Panel1";
            this.userControl11.AreaItem.Panel2.Text = "Panel2";
            this.userControl11.AreaItem.Size = new System.Drawing.Size(150, 150);
            this.userControl11.AreaItem.SplitterPosition = 105;
            this.userControl11.AreaItem.TabIndex = 0;
            this.userControl11.AreaItem.Text = "splitContainerControl1";
            this.userControl11.Location = new System.Drawing.Point(444, 12);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(150, 150);
            this.userControl11.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 604);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.itemBarraItens1);
            this.Controls.Add(this.itemBarraItens2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.itemBarraItens2.AreaItem.ResumeLayout(false);
            this.itemBarraItens2.AreaItem.PerformLayout();
            this.itemBarraItens2.AreaItens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            this.userControl11.AreaItem.ResumeLayout(false);
            this.userControl11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private FGlobus.Componentes.WinForms.ItemBarraItens itemBarraItens2;
        private FGlobus.Componentes.WinForms.ItemBarraItens itemBarraItens1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private ClassLibrary1.UserControl1 userControl11;


















    }
}

