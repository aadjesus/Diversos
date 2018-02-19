namespace Sessions_WebService
{
    partial class MasterFormMensagem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterFormMensagem));
            this.imgClctn = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlCtrlMensagem = new DevExpress.XtraEditors.PanelControl();
            this.pnlCtrlTextoMensagem = new DevExpress.XtraEditors.PanelControl();
            this.lblCtrlTextoMensagem = new DevExpress.XtraEditors.MemoEdit();
            this.pnlCtrlBotoes = new DevExpress.XtraEditors.PanelControl();
            this.smplBtnNao = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.smplBtnSim = new DevExpress.XtraEditors.SimpleButton();
            this.smplBtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtEdtCaixaEntrada = new DevExpress.XtraEditors.TextEdit();
            this.pnlCtrlTitulo = new DevExpress.XtraEditors.PanelControl();
            this.lblCtrlTitulo = new DevExpress.XtraEditors.LabelControl();
            this.pnlCtrlImagem = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.pctrEdtImagem = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClctn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlMensagem)).BeginInit();
            this.pnlCtrlMensagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTextoMensagem)).BeginInit();
            this.pnlCtrlTextoMensagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblCtrlTextoMensagem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlBotoes)).BeginInit();
            this.pnlCtrlBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdtCaixaEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTitulo)).BeginInit();
            this.pnlCtrlTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlImagem)).BeginInit();
            this.pnlCtrlImagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctrEdtImagem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imgClctn
            // 
            this.imgClctn.ImageSize = new System.Drawing.Size(50, 50);
            this.imgClctn.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgClctn.ImageStream")));
            this.imgClctn.Images.SetKeyName(0, "MensagemErro.png");
            this.imgClctn.Images.SetKeyName(1, "MensagemInformacao.png");
            this.imgClctn.Images.SetKeyName(2, "MensagemPergunta.png");
            // 
            // pnlCtrlMensagem
            // 
            this.pnlCtrlMensagem.Controls.Add(this.pnlCtrlTextoMensagem);
            this.pnlCtrlMensagem.Controls.Add(this.pnlCtrlImagem);
            this.pnlCtrlMensagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCtrlMensagem.Location = new System.Drawing.Point(0, 0);
            this.pnlCtrlMensagem.Name = "pnlCtrlMensagem";
            this.pnlCtrlMensagem.Size = new System.Drawing.Size(617, 124);
            this.pnlCtrlMensagem.TabIndex = 3;
            // 
            // pnlCtrlTextoMensagem
            // 
            this.pnlCtrlTextoMensagem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCtrlTextoMensagem.Controls.Add(this.lblCtrlTextoMensagem);
            this.pnlCtrlTextoMensagem.Controls.Add(this.pnlCtrlBotoes);
            this.pnlCtrlTextoMensagem.Controls.Add(this.txtEdtCaixaEntrada);
            this.pnlCtrlTextoMensagem.Controls.Add(this.pnlCtrlTitulo);
            this.pnlCtrlTextoMensagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCtrlTextoMensagem.Location = new System.Drawing.Point(67, 2);
            this.pnlCtrlTextoMensagem.Name = "pnlCtrlTextoMensagem";
            this.pnlCtrlTextoMensagem.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCtrlTextoMensagem.Size = new System.Drawing.Size(548, 120);
            this.pnlCtrlTextoMensagem.TabIndex = 8;
            this.pnlCtrlTextoMensagem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseDown);
            this.pnlCtrlTextoMensagem.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseMove);
            this.pnlCtrlTextoMensagem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseUp);
            // 
            // lblCtrlTextoMensagem
            // 
            this.lblCtrlTextoMensagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCtrlTextoMensagem.EditValue = "123456789012345678901234567890";
            this.lblCtrlTextoMensagem.Location = new System.Drawing.Point(5, 57);
            this.lblCtrlTextoMensagem.Name = "lblCtrlTextoMensagem";
            this.lblCtrlTextoMensagem.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCtrlTextoMensagem.Properties.ReadOnly = true;
            this.lblCtrlTextoMensagem.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.lblCtrlTextoMensagem.Properties.WordWrap = false;
            this.lblCtrlTextoMensagem.Size = new System.Drawing.Size(538, 27);
            this.lblCtrlTextoMensagem.TabIndex = 9;
            this.lblCtrlTextoMensagem.TabStop = false;
            // 
            // pnlCtrlBotoes
            // 
            this.pnlCtrlBotoes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCtrlBotoes.Controls.Add(this.smplBtnNao);
            this.pnlCtrlBotoes.Controls.Add(this.panelControl4);
            this.pnlCtrlBotoes.Controls.Add(this.smplBtnSim);
            this.pnlCtrlBotoes.Controls.Add(this.smplBtnOk);
            this.pnlCtrlBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCtrlBotoes.Location = new System.Drawing.Point(5, 84);
            this.pnlCtrlBotoes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlCtrlBotoes.Name = "pnlCtrlBotoes";
            this.pnlCtrlBotoes.Size = new System.Drawing.Size(538, 31);
            this.pnlCtrlBotoes.TabIndex = 10;
            this.pnlCtrlBotoes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseDown);
            this.pnlCtrlBotoes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseMove);
            this.pnlCtrlBotoes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseUp);
            // 
            // smplBtnNao
            // 
            this.smplBtnNao.Dock = System.Windows.Forms.DockStyle.Right;
            this.smplBtnNao.Image = ((System.Drawing.Image)(resources.GetObject("smplBtnNao.Image")));
            this.smplBtnNao.Location = new System.Drawing.Point(307, 0);
            this.smplBtnNao.Name = "smplBtnNao";
            this.smplBtnNao.Size = new System.Drawing.Size(75, 31);
            this.smplBtnNao.TabIndex = 11;
            this.smplBtnNao.TabStop = false;
            this.smplBtnNao.Text = "Não";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(382, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(10, 31);
            this.panelControl4.TabIndex = 10;
            // 
            // smplBtnSim
            // 
            this.smplBtnSim.Dock = System.Windows.Forms.DockStyle.Right;
            this.smplBtnSim.Image = ((System.Drawing.Image)(resources.GetObject("smplBtnSim.Image")));
            this.smplBtnSim.Location = new System.Drawing.Point(392, 0);
            this.smplBtnSim.Name = "smplBtnSim";
            this.smplBtnSim.Size = new System.Drawing.Size(73, 31);
            this.smplBtnSim.TabIndex = 9;
            this.smplBtnSim.TabStop = false;
            this.smplBtnSim.Text = "Sim";
            // 
            // smplBtnOk
            // 
            this.smplBtnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.smplBtnOk.Image = ((System.Drawing.Image)(resources.GetObject("smplBtnOk.Image")));
            this.smplBtnOk.Location = new System.Drawing.Point(465, 0);
            this.smplBtnOk.Name = "smplBtnOk";
            this.smplBtnOk.Size = new System.Drawing.Size(73, 31);
            this.smplBtnOk.TabIndex = 6;
            this.smplBtnOk.TabStop = false;
            this.smplBtnOk.Text = "Ok";
            // 
            // txtEdtCaixaEntrada
            // 
            this.txtEdtCaixaEntrada.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEdtCaixaEntrada.Location = new System.Drawing.Point(5, 37);
            this.txtEdtCaixaEntrada.Name = "txtEdtCaixaEntrada";
            this.txtEdtCaixaEntrada.Size = new System.Drawing.Size(538, 20);
            this.txtEdtCaixaEntrada.TabIndex = 11;
            this.txtEdtCaixaEntrada.Visible = false;
            // 
            // pnlCtrlTitulo
            // 
            this.pnlCtrlTitulo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCtrlTitulo.Controls.Add(this.lblCtrlTitulo);
            this.pnlCtrlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCtrlTitulo.Location = new System.Drawing.Point(5, 5);
            this.pnlCtrlTitulo.Name = "pnlCtrlTitulo";
            this.pnlCtrlTitulo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlCtrlTitulo.Size = new System.Drawing.Size(538, 32);
            this.pnlCtrlTitulo.TabIndex = 10;
            this.pnlCtrlTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseDown);
            this.pnlCtrlTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseMove);
            this.pnlCtrlTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseUp);
            // 
            // lblCtrlTitulo
            // 
            this.lblCtrlTitulo.Appearance.BackColor = System.Drawing.Color.Silver;
            this.lblCtrlTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtrlTitulo.Appearance.Options.UseBackColor = true;
            this.lblCtrlTitulo.Appearance.Options.UseFont = true;
            this.lblCtrlTitulo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCtrlTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCtrlTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblCtrlTitulo.Name = "lblCtrlTitulo";
            this.lblCtrlTitulo.Padding = new System.Windows.Forms.Padding(5);
            this.lblCtrlTitulo.Size = new System.Drawing.Size(538, 22);
            this.lblCtrlTitulo.TabIndex = 10;
            this.lblCtrlTitulo.Text = "lblCtrlTitulo";
            this.lblCtrlTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseDown);
            this.lblCtrlTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseMove);
            this.lblCtrlTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseUp);
            // 
            // pnlCtrlImagem
            // 
            this.pnlCtrlImagem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCtrlImagem.Controls.Add(this.labelControl3);
            this.pnlCtrlImagem.Controls.Add(this.pctrEdtImagem);
            this.pnlCtrlImagem.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCtrlImagem.Location = new System.Drawing.Point(2, 2);
            this.pnlCtrlImagem.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlCtrlImagem.Name = "pnlCtrlImagem";
            this.pnlCtrlImagem.Size = new System.Drawing.Size(65, 120);
            this.pnlCtrlImagem.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Lucida Console", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(0, 0);
            this.labelControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelControl3.Size = new System.Drawing.Size(65, 120);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "?";
            // 
            // pctrEdtImagem
            // 
            this.pctrEdtImagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctrEdtImagem.EditValue = ((object)(resources.GetObject("pctrEdtImagem.EditValue")));
            this.pctrEdtImagem.Location = new System.Drawing.Point(0, 0);
            this.pctrEdtImagem.Name = "pctrEdtImagem";
            this.pctrEdtImagem.Properties.AllowFocused = false;
            this.pctrEdtImagem.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pctrEdtImagem.Properties.Appearance.Options.UseBackColor = true;
            this.pctrEdtImagem.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pctrEdtImagem.Properties.ShowMenu = false;
            this.pctrEdtImagem.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pctrEdtImagem.Size = new System.Drawing.Size(65, 120);
            this.pctrEdtImagem.TabIndex = 0;
            this.pctrEdtImagem.Visible = false;
            this.pctrEdtImagem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseDown);
            this.pctrEdtImagem.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseMove);
            this.pctrEdtImagem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseUp);
            // 
            // MasterFormMensagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 124);
            this.ControlBox = false;
            this.Controls.Add(this.pnlCtrlMensagem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterFormMensagem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MensagemBGM_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MensagemBGM_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.imgClctn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlMensagem)).EndInit();
            this.pnlCtrlMensagem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTextoMensagem)).EndInit();
            this.pnlCtrlTextoMensagem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblCtrlTextoMensagem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlBotoes)).EndInit();
            this.pnlCtrlBotoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdtCaixaEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlTitulo)).EndInit();
            this.pnlCtrlTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCtrlImagem)).EndInit();
            this.pnlCtrlImagem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctrEdtImagem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.Utils.ImageCollection imgClctn;
        internal DevExpress.XtraEditors.PanelControl pnlCtrlTextoMensagem;
        internal DevExpress.XtraEditors.MemoEdit lblCtrlTextoMensagem;
        internal DevExpress.XtraEditors.PanelControl pnlCtrlBotoes;
        internal DevExpress.XtraEditors.SimpleButton smplBtnOk;
        private DevExpress.XtraEditors.PanelControl pnlCtrlTitulo;
        private DevExpress.XtraEditors.LabelControl lblCtrlTitulo;
        internal DevExpress.XtraEditors.TextEdit txtEdtCaixaEntrada;
        public DevExpress.XtraEditors.PanelControl pnlCtrlMensagem;
        internal DevExpress.XtraEditors.SimpleButton smplBtnNao;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        internal DevExpress.XtraEditors.SimpleButton smplBtnSim;
        public DevExpress.XtraEditors.PictureEdit pctrEdtImagem;
        public DevExpress.XtraEditors.PanelControl pnlCtrlImagem;
        private DevExpress.XtraEditors.LabelControl labelControl3;





    }
}

