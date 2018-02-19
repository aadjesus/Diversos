namespace FGlobus.Componentes.WinForms
{ 
    partial class SelecionaBrowseDePesquisaBGM 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelecionaBrowseDePesquisaBGM));
            this.trVwPesquisas = new System.Windows.Forms.TreeView();
            this.imgLstTrVw = new System.Windows.Forms.ImageList(this.components);
            this.btnOkPesquisa = new System.Windows.Forms.Button();
            this.btnCancelarPesquisa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trVwPesquisas
            // 
            this.trVwPesquisas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trVwPesquisas.ImageIndex = 0;
            this.trVwPesquisas.ImageList = this.imgLstTrVw;
            this.trVwPesquisas.Location = new System.Drawing.Point(12, 12);
            this.trVwPesquisas.Name = "trVwPesquisas";
            this.trVwPesquisas.SelectedImageIndex = 0;
            this.trVwPesquisas.Size = new System.Drawing.Size(212, 376);
            this.trVwPesquisas.TabIndex = 0;
            // 
            // imgLstTrVw
            // 
            this.imgLstTrVw.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstTrVw.ImageStream")));
            this.imgLstTrVw.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstTrVw.Images.SetKeyName(0, "SelecionaBrowseDePesquisaBGMN.png");
            this.imgLstTrVw.Images.SetKeyName(1, "SelecionaBrowseDePesquisaBGMP.png");
            this.imgLstTrVw.Images.SetKeyName(2, "SelecionaBrowseDePesquisaBGMB.png");
            // 
            // btnOkPesquisa
            // 
            this.btnOkPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkPesquisa.Location = new System.Drawing.Point(69, 394);
            this.btnOkPesquisa.Name = "btnOkPesquisa";
            this.btnOkPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnOkPesquisa.TabIndex = 3;
            this.btnOkPesquisa.Text = "Ok";
            this.btnOkPesquisa.UseVisualStyleBackColor = true;
            this.btnOkPesquisa.Click += new System.EventHandler(this.btnOkPesquisa_Click);
            // 
            // btnCancelarPesquisa
            // 
            this.btnCancelarPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarPesquisa.Location = new System.Drawing.Point(150, 394);
            this.btnCancelarPesquisa.Name = "btnCancelarPesquisa";
            this.btnCancelarPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarPesquisa.TabIndex = 4;
            this.btnCancelarPesquisa.Text = "Cancelar";
            this.btnCancelarPesquisa.UseVisualStyleBackColor = true;
            this.btnCancelarPesquisa.Click += new System.EventHandler(this.btnCancelarPesquisa_Click);
            // 
            // SelecionaBrowseDePesquisaBGM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 429);
            this.Controls.Add(this.btnCancelarPesquisa);
            this.Controls.Add(this.btnOkPesquisa);
            this.Controls.Add(this.trVwPesquisas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelecionaBrowseDePesquisaBGM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione a pesquisa.";
            this.Load += new System.EventHandler(this.FrmSelecionaPesquisa_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trVwPesquisas;
        private System.Windows.Forms.Button btnOkPesquisa;
        private System.Windows.Forms.Button btnCancelarPesquisa;
        private System.Windows.Forms.ImageList imgLstTrVw;
    }
}