namespace Globus5.WPF.Comum.Cadastros
{
    partial class MasterFormCadastro1
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
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlBotoesMFCadastro)).BeginInit();
            this.pnlCtrlBotoesMFCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTelaMFCadastro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barMngrMF)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCtrlTelaMFCadastro
            // 
            this.pnlCtrlTelaMFCadastro.Location = new System.Drawing.Point(0, 24);
            this.pnlCtrlTelaMFCadastro.Size = new System.Drawing.Size(494, 101);
            // 
            // smpBtnBGMExcluirMFCadastro
            // 
            this.smpBtnBGMExcluirMFCadastro.Location = new System.Drawing.Point(346, 6);
            this.smpBtnBGMExcluirMFCadastro.Click += new System.EventHandler(this.smpBtnBGMExcluirMFCadastro_Click);
            // 
            // smpBtnBGMLimparMFCadastro
            // 
            this.smpBtnBGMLimparMFCadastro.Location = new System.Drawing.Point(198, 6);
            this.smpBtnBGMLimparMFCadastro.Click += new System.EventHandler(this.smpBtnBGMLimparMFCadastro_Click);
            // 
            // smpBtnBGMGravarMFCadastro
            // 
            this.smpBtnBGMGravarMFCadastro.Location = new System.Drawing.Point(50, 6);
            this.smpBtnBGMGravarMFCadastro.Click += new System.EventHandler(this.smpBtnBGMGravarMFCadastro_Click);
            // 
            // barMenuMF
            // 
            this.barMenuMF.OptionsBar.AllowQuickCustomization = false;
            this.barMenuMF.OptionsBar.DrawDragBorder = false;
            this.barMenuMF.OptionsBar.RotateWhenVertical = false;
            this.barMenuMF.OptionsBar.UseWholeRow = true;
            // 
            // MasterFormCadastro1
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(447, 144);
            this.Name = "MasterFormCadastro1";
            this.Text = "MasterFormCadastro1";
            this.HabilitarBotoes += new FGlobus.Componentes.WinForms.MasterFormEventHandler(this.MasterFormCadastro1_HabilitarBotoes);

            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlBotoesMFCadastro)).EndInit();
            this.pnlCtrlBotoesMFCadastro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTelaMFCadastro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barMngrMF)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
