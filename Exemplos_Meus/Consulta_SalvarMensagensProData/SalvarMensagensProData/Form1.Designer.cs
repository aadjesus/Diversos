namespace SalvarMensagensProData
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGrupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDMensagem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDMensagemRetorno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDOperador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrefixo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDEmpresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.sMensagensBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMensagensBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.sMensagensBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(554, 282);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTipo,
            this.colGrupo,
            this.colCodigo,
            this.colIDMensagem,
            this.colIDMensagemRetorno,
            this.colIDOperador,
            this.colPrefixo,
            this.colIDEmpresa,
            this.colLinha});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colTipo
            // 
            this.colTipo.FieldName = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.Visible = true;
            this.colTipo.VisibleIndex = 0;
            // 
            // colGrupo
            // 
            this.colGrupo.FieldName = "Grupo";
            this.colGrupo.Name = "colGrupo";
            this.colGrupo.Visible = true;
            this.colGrupo.VisibleIndex = 1;
            // 
            // colCodigo
            // 
            this.colCodigo.FieldName = "Codigo";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.Visible = true;
            this.colCodigo.VisibleIndex = 2;
            // 
            // colIDMensagem
            // 
            this.colIDMensagem.FieldName = "IDMensagem";
            this.colIDMensagem.Name = "colIDMensagem";
            this.colIDMensagem.Visible = true;
            this.colIDMensagem.VisibleIndex = 3;
            // 
            // colIDMensagemRetorno
            // 
            this.colIDMensagemRetorno.FieldName = "IDMensagemRetorno";
            this.colIDMensagemRetorno.Name = "colIDMensagemRetorno";
            this.colIDMensagemRetorno.Visible = true;
            this.colIDMensagemRetorno.VisibleIndex = 4;
            // 
            // colIDOperador
            // 
            this.colIDOperador.FieldName = "IDOperador";
            this.colIDOperador.Name = "colIDOperador";
            this.colIDOperador.Visible = true;
            this.colIDOperador.VisibleIndex = 5;
            // 
            // colPrefixo
            // 
            this.colPrefixo.FieldName = "Prefixo";
            this.colPrefixo.Name = "colPrefixo";
            this.colPrefixo.Visible = true;
            this.colPrefixo.VisibleIndex = 6;
            // 
            // colIDEmpresa
            // 
            this.colIDEmpresa.FieldName = "IDEmpresa";
            this.colIDEmpresa.Name = "colIDEmpresa";
            this.colIDEmpresa.Visible = true;
            this.colIDEmpresa.VisibleIndex = 7;
            // 
            // colLinha
            // 
            this.colLinha.FieldName = "Linha";
            this.colLinha.Name = "colLinha";
            this.colLinha.Visible = true;
            this.colLinha.VisibleIndex = 8;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(12, 300);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // sMensagensBindingSource
            // 
            this.sMensagensBindingSource.DataSource = typeof(SalvarMensagensProData.integracao.sMensagens);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 331);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMensagensBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn colTipo;
        private DevExpress.XtraGrid.Columns.GridColumn colGrupo;
        private DevExpress.XtraGrid.Columns.GridColumn colCodigo;
        private DevExpress.XtraGrid.Columns.GridColumn colIDMensagem;
        private DevExpress.XtraGrid.Columns.GridColumn colIDMensagemRetorno;
        private DevExpress.XtraGrid.Columns.GridColumn colIDOperador;
        private DevExpress.XtraGrid.Columns.GridColumn colPrefixo;
        private DevExpress.XtraGrid.Columns.GridColumn colIDEmpresa;
        private DevExpress.XtraGrid.Columns.GridColumn colLinha;
        private System.Windows.Forms.BindingSource sMensagensBindingSource;
    }
}

