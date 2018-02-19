namespace WindowsFormsApplication2
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
            this.autorizaGridBGM1 = new ClassLibrary1.AutorizaGridBGMx2();
            this.colunaAutorizaGridBGM1 = new ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM();
            this.colunaAutorizaGridBGM2 = new ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM();
            this.SuspendLayout();
            // 
            // autorizaGridBGM1
            // 
            this.autorizaGridBGM1.Colunas.Add(this.colunaAutorizaGridBGM1);
            this.autorizaGridBGM1.Colunas.Add(this.colunaAutorizaGridBGM2);
            this.autorizaGridBGM1.Location = new System.Drawing.Point(43, 27);
            this.autorizaGridBGM1.Margin = new System.Windows.Forms.Padding(0);
            this.autorizaGridBGM1.MinimumSize = new System.Drawing.Size(350, 200);
            this.autorizaGridBGM1.Name = "autorizaGridBGM1";
            this.autorizaGridBGM1.Size = new System.Drawing.Size(766, 420);
            this.autorizaGridBGM1.TabIndex = 0;
            this.autorizaGridBGM1.Titulo = "[AutorizaGridBGM]";
            // 
            // colunaAutorizaGridBGM1
            // 
            this.colunaAutorizaGridBGM1.Campo = "Codigo";
            this.colunaAutorizaGridBGM1.CampoAssociado = "Codigo";
            this.colunaAutorizaGridBGM1.Titulo = "Código";
            // 
            // colunaAutorizaGridBGM2
            // 
            this.colunaAutorizaGridBGM2.Campo = "Descricao";
            this.colunaAutorizaGridBGM2.CampoAssociado = "Descricao";
            this.colunaAutorizaGridBGM2.Chave = false;
            this.colunaAutorizaGridBGM2.Titulo = "Descrição";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 524);
            this.Controls.Add(this.autorizaGridBGM1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ClassLibrary1.AutorizaGridBGMx2 autorizaGridBGM1;
        private ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM colunaAutorizaGridBGM1;
        private ClassLibrary1.AutorizaGridBGMx2.ColunaAutorizaGridBGM colunaAutorizaGridBGM2;




























    }
}