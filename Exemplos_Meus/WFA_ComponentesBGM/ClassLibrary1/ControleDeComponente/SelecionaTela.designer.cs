namespace FGlobus.Componentes.WinForms
{
    partial class SelecionaTela 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelecionaTela));
            this.trVwTela = new System.Windows.Forms.TreeView();
            this.imgLstTrVw = new System.Windows.Forms.ImageList(this.components);
            this.btnOkTela = new System.Windows.Forms.Button();
            this.btnCancelarTela = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trVwTela
            // 
            this.trVwTela.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trVwTela.ImageIndex = 0;
            this.trVwTela.ImageList = this.imgLstTrVw;
            this.trVwTela.Location = new System.Drawing.Point(12, 12);
            this.trVwTela.Name = "trVwTela";
            this.trVwTela.SelectedImageIndex = 0;
            this.trVwTela.ShowNodeToolTips = true;
            this.trVwTela.Size = new System.Drawing.Size(281, 374);
            this.trVwTela.TabIndex = 0;
            this.trVwTela.DoubleClick += new System.EventHandler(this.btnOkTela_Click);
            // 
            // imgLstTrVw
            // 
            this.imgLstTrVw.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstTrVw.ImageStream")));
            this.imgLstTrVw.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstTrVw.Images.SetKeyName(0, "SelecionaTelaPesq.png");
            this.imgLstTrVw.Images.SetKeyName(1, "SelecionaTelaCad.png");
            this.imgLstTrVw.Images.SetKeyName(2, "SelecionaTelaNone.png");
            this.imgLstTrVw.Images.SetKeyName(3, "SelecionaTelaClasse.png");
            this.imgLstTrVw.Images.SetKeyName(4, "SelecionaTelaPesqCad.png");
            // 
            // btnOkTela
            // 
            this.btnOkTela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkTela.Location = new System.Drawing.Point(138, 392);
            this.btnOkTela.Name = "btnOkTela";
            this.btnOkTela.Size = new System.Drawing.Size(75, 23);
            this.btnOkTela.TabIndex = 3;
            this.btnOkTela.Text = "Ok";
            this.btnOkTela.UseVisualStyleBackColor = true;
            this.btnOkTela.Click += new System.EventHandler(this.btnOkTela_Click);
            // 
            // btnCancelarTela
            // 
            this.btnCancelarTela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarTela.Location = new System.Drawing.Point(219, 392);
            this.btnCancelarTela.Name = "btnCancelarTela";
            this.btnCancelarTela.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarTela.TabIndex = 4;
            this.btnCancelarTela.Text = "Cancelar";
            this.btnCancelarTela.UseVisualStyleBackColor = true;
            this.btnCancelarTela.Click += new System.EventHandler(this.btnCancelarTela_Click);
            // 
            // SelecionaTela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 427);
            this.Controls.Add(this.btnCancelarTela);
            this.Controls.Add(this.btnOkTela);
            this.Controls.Add(this.trVwTela);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelecionaTela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione a tela de";
            this.Load += new System.EventHandler(this.SelecionaTela_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trVwTela;
        private System.Windows.Forms.Button btnOkTela;
        private System.Windows.Forms.Button btnCancelarTela;
        private System.Windows.Forms.ImageList imgLstTrVw;
    }
}