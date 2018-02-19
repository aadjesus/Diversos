/*
 * Criado por SharpDevelop.
 * Usuário: f5361091
 * Data: 05/08/2009
 * Hora: 12:55
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
namespace exportaDB
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.label2 = new System.Windows.Forms.Label();
            this.txtBD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTabela = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNomeArquivoXLS = new System.Windows.Forms.TextBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cboSGBD = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlSGBD = new System.Windows.Forms.Panel();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAbrirXLS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlSGBD.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Banco de dados";
            // 
            // txtBD
            // 
            this.txtBD.Location = new System.Drawing.Point(108, 89);
            this.txtBD.Name = "txtBD";
            this.txtBD.Size = new System.Drawing.Size(207, 20);
            this.txtBD.TabIndex = 3;
            this.txtBD.Text = "Northwind";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tabela";
            // 
            // txtTabela
            // 
            this.txtTabela.Location = new System.Drawing.Point(108, 115);
            this.txtTabela.Name = "txtTabela";
            this.txtTabela.Size = new System.Drawing.Size(207, 20);
            this.txtTabela.TabIndex = 5;
            this.txtTabela.Text = "Products";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Arquivo Excel";
            // 
            // txtNomeArquivoXLS
            // 
            this.txtNomeArquivoXLS.Location = new System.Drawing.Point(108, 141);
            this.txtNomeArquivoXLS.Name = "txtNomeArquivoXLS";
            this.txtNomeArquivoXLS.Size = new System.Drawing.Size(207, 20);
            this.txtNomeArquivoXLS.TabIndex = 7;
            this.txtNomeArquivoXLS.Text = "c:\\dados\\Produtos.xls";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(108, 180);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(93, 53);
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Text = "Gerar arquivo para o Excel";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.BtnExportarClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 53);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // cboSGBD
            // 
            this.cboSGBD.FormattingEnabled = true;
            this.cboSGBD.Items.AddRange(new object[] {
            "MS Access",
            "SQL Server Express"});
            this.cboSGBD.Location = new System.Drawing.Point(109, 22);
            this.cboSGBD.Name = "cboSGBD";
            this.cboSGBD.Size = new System.Drawing.Size(206, 21);
            this.cboSGBD.TabIndex = 10;
            this.cboSGBD.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "SGBD";
            // 
            // pnlSGBD
            // 
            this.pnlSGBD.Controls.Add(this.txtServidor);
            this.pnlSGBD.Controls.Add(this.label1);
            this.pnlSGBD.Location = new System.Drawing.Point(15, 47);
            this.pnlSGBD.Name = "pnlSGBD";
            this.pnlSGBD.Size = new System.Drawing.Size(345, 36);
            this.pnlSGBD.TabIndex = 12;
            this.pnlSGBD.Visible = false;
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(94, 7);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(207, 20);
            this.txtServidor.TabIndex = 3;
            this.txtServidor.Text = "MAC\\SQLEXPRESS";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Servidor";
            // 
            // btnAbrirXLS
            // 
            this.btnAbrirXLS.Location = new System.Drawing.Point(222, 180);
            this.btnAbrirXLS.Name = "btnAbrirXLS";
            this.btnAbrirXLS.Size = new System.Drawing.Size(93, 53);
            this.btnAbrirXLS.TabIndex = 13;
            this.btnAbrirXLS.Text = "Abrir Arquivo Excel Gerado";
            this.btnAbrirXLS.UseVisualStyleBackColor = true;
            this.btnAbrirXLS.Click += new System.EventHandler(this.btnAbrirXLS_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 261);
            this.Controls.Add(this.btnAbrirXLS);
            this.Controls.Add(this.pnlSGBD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboSGBD);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.txtNomeArquivoXLS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTabela);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBD);
            this.Controls.Add(this.label2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar tabela para o Excel";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlSGBD.ResumeLayout(false);
            this.pnlSGBD.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button btnAbrirXLS;
		private System.Windows.Forms.ComboBox cboSGBD;
		private System.Windows.Forms.Panel pnlSGBD;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnExportar;
		private System.Windows.Forms.TextBox txtNomeArquivoXLS;
		private System.Windows.Forms.TextBox txtTabela;
		private System.Windows.Forms.TextBox txtBD;
		private System.Windows.Forms.TextBox txtServidor;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
