namespace WFA_CodigoDeBarras
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
            this.pctrBxImagem = new System.Windows.Forms.PictureBox();
            this.panelControl1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblArquivo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxValor = new System.Windows.Forms.TextBox();
            this.btnLerArquivo = new System.Windows.Forms.Button();
            this.btnArquivo = new System.Windows.Forms.Button();
            this.cmbBxTpCodBarra = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxImagem)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctrBxImagem
            // 
            this.pctrBxImagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctrBxImagem.Location = new System.Drawing.Point(0, 75);
            this.pctrBxImagem.Name = "pctrBxImagem";
            this.pctrBxImagem.Size = new System.Drawing.Size(786, 327);
            this.pctrBxImagem.TabIndex = 7;
            this.pctrBxImagem.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.lblArquivo);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.txtBxValor);
            this.panelControl1.Controls.Add(this.btnLerArquivo);
            this.panelControl1.Controls.Add(this.btnArquivo);
            this.panelControl1.Controls.Add(this.cmbBxTpCodBarra);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(786, 75);
            this.panelControl1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Tipo código de barra";
            // 
            // lblArquivo
            // 
            this.lblArquivo.AutoSize = true;
            this.lblArquivo.Location = new System.Drawing.Point(5, 54);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(43, 13);
            this.lblArquivo.TabIndex = 23;
            this.lblArquivo.Text = "Arquivo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Valor";
            // 
            // txtBxValor
            // 
            this.txtBxValor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxValor.Location = new System.Drawing.Point(408, 31);
            this.txtBxValor.Name = "txtBxValor";
            this.txtBxValor.Size = new System.Drawing.Size(373, 20);
            this.txtBxValor.TabIndex = 21;
            // 
            // btnLerArquivo
            // 
            this.btnLerArquivo.Location = new System.Drawing.Point(274, 28);
            this.btnLerArquivo.Name = "btnLerArquivo";
            this.btnLerArquivo.Size = new System.Drawing.Size(128, 23);
            this.btnLerArquivo.TabIndex = 20;
            this.btnLerArquivo.Text = "Ler imagem";
            this.btnLerArquivo.UseVisualStyleBackColor = true;
            this.btnLerArquivo.Click += new System.EventHandler(this.btnLerArquivo_Click);
            // 
            // btnArquivo
            // 
            this.btnArquivo.Location = new System.Drawing.Point(5, 28);
            this.btnArquivo.Name = "btnArquivo";
            this.btnArquivo.Size = new System.Drawing.Size(136, 23);
            this.btnArquivo.TabIndex = 19;
            this.btnArquivo.Text = "Arquivo";
            this.btnArquivo.UseVisualStyleBackColor = true;
            this.btnArquivo.Click += new System.EventHandler(this.btnArquivo_Click);
            // 
            // cmbBxTpCodBarra
            // 
            this.cmbBxTpCodBarra.FormattingEnabled = true;
            this.cmbBxTpCodBarra.Location = new System.Drawing.Point(147, 30);
            this.cmbBxTpCodBarra.Name = "cmbBxTpCodBarra";
            this.cmbBxTpCodBarra.Size = new System.Drawing.Size(121, 21);
            this.cmbBxTpCodBarra.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 402);
            this.Controls.Add(this.pctrBxImagem);
            this.Controls.Add(this.panelControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxImagem)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctrBxImagem;
        private System.Windows.Forms.Panel panelControl1;
        private System.Windows.Forms.ComboBox cmbBxTpCodBarra;
        private System.Windows.Forms.Button btnArquivo;
        private System.Windows.Forms.Button btnLerArquivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxValor;
    }
}

