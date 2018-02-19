namespace ColorDialogExample
{
    partial class Main
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
            FGlobus.Componentes.WinForms.SelecionaPesquisaBGMPropriedade selecionaPesquisaBGMPropriedade1 = new FGlobus.Componentes.WinForms.SelecionaPesquisaBGMPropriedade();
            this.pgMain = new System.Windows.Forms.PropertyGrid();
            this.buttonEditBGM1 = new FGlobus.Componentes.WinForms.ButtonEditBGM(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBGM1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pgMain
            // 
            this.pgMain.Location = new System.Drawing.Point(0, 0);
            this.pgMain.Name = "pgMain";
            this.pgMain.Size = new System.Drawing.Size(292, 266);
            this.pgMain.TabIndex = 0;
            // 
            // buttonEditBGM1
            // 
            this.buttonEditBGM1.ExecutarPesquisa = true;
            this.buttonEditBGM1.Location = new System.Drawing.Point(137, 310);
            this.buttonEditBGM1.Name = "buttonEditBGM1";
            selecionaPesquisaBGMPropriedade1.NomePesquisa = "XtraForm1";
            this.buttonEditBGM1.Pesquisa = selecionaPesquisaBGMPropriedade1;
            this.buttonEditBGM1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditBGM1.Size = new System.Drawing.Size(100, 20);
            this.buttonEditBGM1.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 414);
            this.Controls.Add(this.buttonEditBGM1);
            this.Controls.Add(this.pgMain);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBGM1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgMain;
        private FGlobus.Componentes.WinForms.ButtonEditBGM buttonEditBGM1;
    }
}

