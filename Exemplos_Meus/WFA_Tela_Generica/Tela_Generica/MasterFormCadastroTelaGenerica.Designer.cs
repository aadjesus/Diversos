namespace FGlobus.Componentes.WinForms
{
    partial class MasterFormCadastroTelaGenerica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterFormCadastroTelaGenerica));
            this.objVwTelaGenerica = new FGlobus.Componentes.WinForms.ObjectView();
            this.tblLytPnlTelaGenerica = new System.Windows.Forms.TableLayoutPanel();
            this.objVwTelaGenericaGeral = new FGlobus.Componentes.WinForms.ObjectView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlBotoesMFCadastro)).BeginInit();
            this.pnlCtrlBotoesMFCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTelaMFCadastro)).BeginInit();
            this.pnlCtrlTelaMFCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objVwTelaGenerica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objVwTelaGenericaGeral)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCtrlBotoesMFCadastro
            // 
            this.pnlCtrlBotoesMFCadastro.Location = new System.Drawing.Point(0, 104);
            this.pnlCtrlBotoesMFCadastro.Size = new System.Drawing.Size(494, 43);
            // 
            // pnlCtrlTelaMFCadastro
            // 
            this.pnlCtrlTelaMFCadastro.Controls.Add(this.tblLytPnlTelaGenerica);
            this.pnlCtrlTelaMFCadastro.Size = new System.Drawing.Size(494, 73);
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
            // objVwTelaGenerica
            // 
            this.objVwTelaGenerica.AutoGenerateColumns = true;
            this.objVwTelaGenerica.DataSource = ((System.Collections.ICollection)(resources.GetObject("objVwTelaGenerica.DataSource")));
            // 
            // tblLytPnlTelaGenerica
            // 
            this.tblLytPnlTelaGenerica.ColumnCount = 1;
            this.tblLytPnlTelaGenerica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytPnlTelaGenerica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytPnlTelaGenerica.Location = new System.Drawing.Point(2, 2);
            this.tblLytPnlTelaGenerica.Name = "tblLytPnlTelaGenerica";
            this.tblLytPnlTelaGenerica.RowCount = 1;
            this.tblLytPnlTelaGenerica.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLytPnlTelaGenerica.Size = new System.Drawing.Size(490, 69);
            this.tblLytPnlTelaGenerica.TabIndex = 0;
            // 
            // objVwTelaGenericaGeral
            // 
            this.objVwTelaGenericaGeral.AutoGenerateColumns = true;
            this.objVwTelaGenericaGeral.DataSource = ((System.Collections.ICollection)(resources.GetObject("objVwTelaGenericaGeral.DataSource")));
            // 
            // MasterFormCadastroTelaGenerica
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(494, 147);
            this.Name = "MasterFormCadastroTelaGenerica";
            this.Text = "TelaGenerica";
            this.HabilitarBotoes += new FGlobus.Componentes.WinForms.MasterFormEventHandler(this.TelaGenerica_HabilitarBotoes);
            this.Load += new System.EventHandler(this.MasterFormCadastroTelaGenerica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlBotoesMFCadastro)).EndInit();
            this.pnlCtrlBotoesMFCadastro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTelaMFCadastro)).EndInit();
            this.pnlCtrlTelaMFCadastro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objVwTelaGenerica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objVwTelaGenericaGeral)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FGlobus.Componentes.WinForms.ObjectView objVwTelaGenerica;
        private System.Windows.Forms.TableLayoutPanel tblLytPnlTelaGenerica;
        private ObjectView objVwTelaGenericaGeral;
    }
}
