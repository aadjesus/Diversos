namespace Mensagem
{
    partial class GroupControlBGM
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupControlBGM));
            this.btnEdtMostrarOuOcultar = new DevExpress.XtraEditors.ButtonEdit();
            this.pnlCtrlF8IncluirSair = new DevExpress.XtraEditors.PanelControl();
            this.lblCtrlF8IncluirSair = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdtMostrarOuOcultar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlF8IncluirSair)).BeginInit();
            this.pnlCtrlF8IncluirSair.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEdtMostrarOuOcultar
            // 
            resources.ApplyResources(this.btnEdtMostrarOuOcultar, "btnEdtMostrarOuOcultar");
            this.btnEdtMostrarOuOcultar.Name = "btnEdtMostrarOuOcultar";
            this.btnEdtMostrarOuOcultar.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnEdtMostrarOuOcultar.Properties.Appearance.Options.UseBackColor = true;
            this.btnEdtMostrarOuOcultar.Properties.AutoHeight = ((bool)(resources.GetObject("btnEdtMostrarOuOcultar.Properties.AutoHeight")));
            this.btnEdtMostrarOuOcultar.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnEdtMostrarOuOcultar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("btnEdtMostrarOuOcultar.Properties.Buttons"))))});
            this.btnEdtMostrarOuOcultar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnEdtMostrarOuOcultar.TabStop = false;
            this.btnEdtMostrarOuOcultar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdtMostrarOuOcultar_ButtonClick);
            // 
            // pnlCtrlF8IncluirSair
            // 
            resources.ApplyResources(this.pnlCtrlF8IncluirSair, "pnlCtrlF8IncluirSair");
            this.pnlCtrlF8IncluirSair.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCtrlF8IncluirSair.Controls.Add(this.btnEdtMostrarOuOcultar);
            this.pnlCtrlF8IncluirSair.Controls.Add(this.lblCtrlF8IncluirSair);
            this.pnlCtrlF8IncluirSair.Name = "pnlCtrlF8IncluirSair";
            // 
            // lblCtrlF8IncluirSair
            // 
            this.lblCtrlF8IncluirSair.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblCtrlF8IncluirSair.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblCtrlF8IncluirSair.Appearance.Options.UseBackColor = true;
            this.lblCtrlF8IncluirSair.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblCtrlF8IncluirSair, "lblCtrlF8IncluirSair");
            this.lblCtrlF8IncluirSair.Name = "lblCtrlF8IncluirSair";
            // 
            // GroupControlBGM
            // 
            this.Controls.Add(this.pnlCtrlF8IncluirSair);
            this.MinimumSize = new System.Drawing.Size(300, 100);
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.btnEdtMostrarOuOcultar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlF8IncluirSair)).EndInit();
            this.pnlCtrlF8IncluirSair.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit btnEdtMostrarOuOcultar;
        private DevExpress.XtraEditors.PanelControl pnlCtrlF8IncluirSair;
        private DevExpress.XtraEditors.LabelControl lblCtrlF8IncluirSair;

    }
}
