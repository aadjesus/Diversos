namespace WindowsFormsApplication1.FiltroRelatorio
{
    partial class FormFiltroRelatorio
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
            this.filtroRelatorioBGM1 = new FGlobus.Componentes.WinForms.FiltroRelatorioBGM();
            this.SuspendLayout();
            // 
            // filtroRelatorioBGM1
            // 
            this.filtroRelatorioBGM1.BrowserDePesquisa = null;
            this.filtroRelatorioBGM1.ClassMascaras.Codigo = 0;
            this.filtroRelatorioBGM1.ClassMascaras.Controles = null;
            this.filtroRelatorioBGM1.ClassMascaras.DescricaoTipo = "";
            this.filtroRelatorioBGM1.ClassMascaras.EditMask = "";
            this.filtroRelatorioBGM1.ClassMascaras.Exemplo = "";
            this.filtroRelatorioBGM1.ClassMascaras.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.filtroRelatorioBGM1.ClassMascaras.PasswordChar = ' ';
            this.filtroRelatorioBGM1.ClassMascaras.PlaceHolder = '\0';
            this.filtroRelatorioBGM1.ClassMascaras.Tipo = "";
            this.filtroRelatorioBGM1.ColunaRetorno = null;
            this.filtroRelatorioBGM1.Location = new System.Drawing.Point(34, 40);
            this.filtroRelatorioBGM1.MinimumSize = new System.Drawing.Size(234, 70);
            this.filtroRelatorioBGM1.Name = "filtroRelatorioBGM1";
            this.filtroRelatorioBGM1.Size = new System.Drawing.Size(234, 70);
            this.filtroRelatorioBGM1.TabIndex = 0;
            this.filtroRelatorioBGM1.Tipo = FGlobus.Componentes.WinForms.FiltroRelatorioBGM.eTipo.Data;
            this.filtroRelatorioBGM1.Titulo = "filtroRelatorioBGM1";
            this.filtroRelatorioBGM1.ValorFinal = null;
            this.filtroRelatorioBGM1.ValorInicial = null;
            // 
            // FormFiltroRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 440);
            this.Controls.Add(this.filtroRelatorioBGM1);
            this.Name = "FormFiltroRelatorio";
            this.Text = "FormFiltroRelatorio";
            this.ResumeLayout(false);

        }

        #endregion

        private FGlobus.Componentes.WinForms.FiltroRelatorioBGM filtroRelatorioBGM1;










    }
}