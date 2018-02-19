namespace Tela_Generica
{
    partial class Form3
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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.itemBarraItensBGM1 = new FGlobus.Componentes.WinForms.ItemBarraItensBGM();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 23);
            this.treeView1.Name = "treeView1";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(268, 484);
            this.treeView1.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeList1.Location = new System.Drawing.Point(286, 23);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.EnableFiltering = true;
            this.treeList1.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.treeList1.OptionsView.ShowAutoFilterRow = true;
            this.treeList1.Size = new System.Drawing.Size(323, 484);
            this.treeList1.TabIndex = 1;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "treeListColumn1";
            this.treeListColumn1.FieldName = "treeListColumn1";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn2";
            this.treeListColumn2.FieldName = "treeListColumn2";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // itemBarraItensBGM1
            // 
            // 
            // itemBarraItensBGM1.AreaItem
            // 
            this.itemBarraItensBGM1.AreaItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.itemBarraItensBGM1.AreaItem.Size = new System.Drawing.Size(292, 51);
            // 
            // itemBarraItensBGM1.AreaItens
            // 
            this.itemBarraItensBGM1.AreaItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemBarraItensBGM1.AreaItens.Size = new System.Drawing.Size(292, 410);
            this.itemBarraItensBGM1.Location = new System.Drawing.Point(615, 23);
            this.itemBarraItensBGM1.MinimumSize = new System.Drawing.Size(260, 150);
            this.itemBarraItensBGM1.Name = "itemBarraItensBGM1";
            this.itemBarraItensBGM1.PrimeiroControle = null;
            this.itemBarraItensBGM1.SentidoNavegacao = FGlobus.Componentes.WinForms.MasterPanel.eSentidoNavegacao.None;
            this.itemBarraItensBGM1.Size = new System.Drawing.Size(296, 484);
            this.itemBarraItensBGM1.TabIndex = 2;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 519);
            this.Controls.Add(this.itemBarraItensBGM1);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.treeView1);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private FGlobus.Componentes.WinForms.ItemBarraItensBGM itemBarraItensBGM1;

    }
}