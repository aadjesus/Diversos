namespace ClassLibrary1
{
    partial class ListaImagens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaImagens));
            this.btnCancelarTela = new System.Windows.Forms.Button();
            this.btnOkTela = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.usrCtrlTreeViewImagesns = new ClassLibrary1.UsrCtrlTreeViewImagesns();
            this.SuspendLayout();
            // 
            // btnCancelarTela
            // 
            this.btnCancelarTela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarTela.Location = new System.Drawing.Point(397, 527);
            this.btnCancelarTela.Name = "btnCancelarTela";
            this.btnCancelarTela.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarTela.TabIndex = 10;
            this.btnCancelarTela.Text = "Cancelar";
            this.btnCancelarTela.UseVisualStyleBackColor = true;
            this.btnCancelarTela.Click += new System.EventHandler(this.btnOkTela_Click);
            // 
            // btnOkTela
            // 
            this.btnOkTela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkTela.Location = new System.Drawing.Point(316, 527);
            this.btnOkTela.Name = "btnOkTela";
            this.btnOkTela.Size = new System.Drawing.Size(75, 23);
            this.btnOkTela.TabIndex = 9;
            this.btnOkTela.Text = "Ok";
            this.btnOkTela.UseVisualStyleBackColor = true;
            this.btnOkTela.Click += new System.EventHandler(this.btnOkTela_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Location = new System.Drawing.Point(12, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(460, 509);
            this.elementHost1.TabIndex = 11;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.usrCtrlTreeViewImagesns;
            // 
            // ListaImagens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.btnCancelarTela);
            this.Controls.Add(this.btnOkTela);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 600);
            this.Name = "ListaImagens";
            this.Text = "Imagens";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListaImagens_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelarTela;
        private System.Windows.Forms.Button btnOkTela;
        internal System.Windows.Forms.Integration.ElementHost elementHost1;
        internal UsrCtrlTreeViewImagesns usrCtrlTreeViewImagesns;

    }
}