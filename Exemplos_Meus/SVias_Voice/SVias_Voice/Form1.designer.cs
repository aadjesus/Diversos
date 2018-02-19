namespace SVias_Voice
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
            this.textEditHostSVias = new System.Windows.Forms.TextBox();
            this.textEditPortaSVias = new System.Windows.Forms.TextBox();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.textEditNumeroRastreador = new System.Windows.Forms.TextBox();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.textEditTelefoneSVias = new System.Windows.Forms.TextBox();
            this.groupControl1 = new System.Windows.Forms.GroupBox();
            this.lblCtrlRetornoSVias = new System.Windows.Forms.Label();
            this.smplBtnLigarSVias = new System.Windows.Forms.Button();
            this.groupControl2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCtrlRetornoVoice = new System.Windows.Forms.Label();
            this.labelControl7 = new System.Windows.Forms.Label();
            this.textEditRamalVoice = new System.Windows.Forms.TextBox();
            this.labelControl5 = new System.Windows.Forms.Label();
            this.labelControl6 = new System.Windows.Forms.Label();
            this.textEditHostVoice = new System.Windows.Forms.TextBox();
            this.textEditTelefoneVoice = new System.Windows.Forms.TextBox();
            this.textEditPortaVoice = new System.Windows.Forms.TextBox();
            this.labelControl8 = new System.Windows.Forms.Label();
            this.smplBtnLigarVoice = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.groupControl1.SuspendLayout();
            this.groupControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditHostSVias
            // 
            this.textEditHostSVias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditHostSVias.Location = new System.Drawing.Point(5, 44);
            this.textEditHostSVias.Name = "textEditHostSVias";
            this.textEditHostSVias.Size = new System.Drawing.Size(425, 20);
            this.textEditHostSVias.TabIndex = 0;
            this.textEditHostSVias.Text = "190.120.238.16";
            // 
            // textEditPortaSVias
            // 
            this.textEditPortaSVias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditPortaSVias.Location = new System.Drawing.Point(5, 89);
            this.textEditPortaSVias.Name = "textEditPortaSVias";
            this.textEditPortaSVias.Size = new System.Drawing.Size(425, 20);
            this.textEditPortaSVias.TabIndex = 1;
            this.textEditPortaSVias.Text = "5127";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Host";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(158, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Porta";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 115);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(215, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Rastreador";
            // 
            // textEditNumeroRastreador
            // 
            this.textEditNumeroRastreador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditNumeroRastreador.Location = new System.Drawing.Point(5, 134);
            this.textEditNumeroRastreador.Name = "textEditNumeroRastreador";
            this.textEditNumeroRastreador.Size = new System.Drawing.Size(425, 20);
            this.textEditNumeroRastreador.TabIndex = 5;
            this.textEditNumeroRastreador.Text = "21114040";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 160);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Telefone origem";
            // 
            // textEditTelefoneSVias
            // 
            this.textEditTelefoneSVias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditTelefoneSVias.Location = new System.Drawing.Point(5, 179);
            this.textEditTelefoneSVias.Name = "textEditTelefoneSVias";
            this.textEditTelefoneSVias.Size = new System.Drawing.Size(425, 20);
            this.textEditTelefoneSVias.TabIndex = 7;
            this.textEditTelefoneSVias.Text = "1381515435";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkedComboBoxEdit1);
            this.groupControl1.Controls.Add(this.lblCtrlRetornoSVias);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.smplBtnLigarSVias);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.textEditHostSVias);
            this.groupControl1.Controls.Add(this.textEditTelefoneSVias);
            this.groupControl1.Controls.Add(this.textEditPortaSVias);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.textEditNumeroRastreador);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(439, 249);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.TabStop = false;
            this.groupControl1.Text = "SVias";
            // 
            // lblCtrlRetornoSVias
            // 
            this.lblCtrlRetornoSVias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCtrlRetornoSVias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblCtrlRetornoSVias.Location = new System.Drawing.Point(6, 202);
            this.lblCtrlRetornoSVias.Name = "lblCtrlRetornoSVias";
            this.lblCtrlRetornoSVias.Size = new System.Drawing.Size(424, 13);
            this.lblCtrlRetornoSVias.TabIndex = 12;
            // 
            // smplBtnLigarSVias
            // 
            this.smplBtnLigarSVias.Location = new System.Drawing.Point(6, 218);
            this.smplBtnLigarSVias.Name = "smplBtnLigarSVias";
            this.smplBtnLigarSVias.Size = new System.Drawing.Size(173, 23);
            this.smplBtnLigarSVias.TabIndex = 11;
            this.smplBtnLigarSVias.Text = "Abrir canal para comunicação";
            this.smplBtnLigarSVias.Click += new System.EventHandler(this.smplBtnLigarSVias_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.button1);
            this.groupControl2.Controls.Add(this.lblCtrlRetornoVoice);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.textEditRamalVoice);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.textEditHostVoice);
            this.groupControl2.Controls.Add(this.textEditTelefoneVoice);
            this.groupControl2.Controls.Add(this.textEditPortaVoice);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.smplBtnLigarVoice);
            this.groupControl2.Location = new System.Drawing.Point(12, 267);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(439, 242);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.TabStop = false;
            this.groupControl2.Text = "Voice";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "Ligar";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCtrlRetornoVoice
            // 
            this.lblCtrlRetornoVoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblCtrlRetornoVoice.Location = new System.Drawing.Point(5, 191);
            this.lblCtrlRetornoVoice.Name = "lblCtrlRetornoVoice";
            this.lblCtrlRetornoVoice.Size = new System.Drawing.Size(433, 13);
            this.lblCtrlRetornoVoice.TabIndex = 32;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 149);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(64, 13);
            this.labelControl7.TabIndex = 31;
            this.labelControl7.Text = "Ramal origem";
            // 
            // textEditRamalVoice
            // 
            this.textEditRamalVoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditRamalVoice.Location = new System.Drawing.Point(5, 168);
            this.textEditRamalVoice.Name = "textEditRamalVoice";
            this.textEditRamalVoice.Size = new System.Drawing.Size(425, 20);
            this.textEditRamalVoice.TabIndex = 30;
            this.textEditRamalVoice.Text = "2123";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 16);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(229, 13);
            this.labelControl5.TabIndex = 26;
            this.labelControl5.Text = "Host";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 106);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(80, 13);
            this.labelControl6.TabIndex = 29;
            this.labelControl6.Text = "Telefone destino";
            // 
            // textEditHostVoice
            // 
            this.textEditHostVoice.Location = new System.Drawing.Point(5, 35);
            this.textEditHostVoice.Name = "textEditHostVoice";
            this.textEditHostVoice.Size = new System.Drawing.Size(425, 20);
            this.textEditHostVoice.TabIndex = 24;
            this.textEditHostVoice.Text = "200.162.18.130";
            // 
            // textEditTelefoneVoice
            // 
            this.textEditTelefoneVoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditTelefoneVoice.Location = new System.Drawing.Point(5, 125);
            this.textEditTelefoneVoice.Name = "textEditTelefoneVoice";
            this.textEditTelefoneVoice.Size = new System.Drawing.Size(425, 20);
            this.textEditTelefoneVoice.TabIndex = 28;
            // 
            // textEditPortaVoice
            // 
            this.textEditPortaVoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditPortaVoice.Location = new System.Drawing.Point(5, 80);
            this.textEditPortaVoice.Name = "textEditPortaVoice";
            this.textEditPortaVoice.Size = new System.Drawing.Size(425, 20);
            this.textEditPortaVoice.TabIndex = 25;
            this.textEditPortaVoice.Text = "2555";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 61);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(292, 13);
            this.labelControl8.TabIndex = 27;
            this.labelControl8.Text = "Porta";
            // 
            // smplBtnLigarVoice
            // 
            this.smplBtnLigarVoice.Location = new System.Drawing.Point(6, 207);
            this.smplBtnLigarVoice.Name = "smplBtnLigarVoice";
            this.smplBtnLigarVoice.Size = new System.Drawing.Size(173, 23);
            this.smplBtnLigarVoice.TabIndex = 12;
            this.smplBtnLigarVoice.Text = "Conectar";
            this.smplBtnLigarVoice.Click += new System.EventHandler(this.smplBtnLigarVoice_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupControl2);
            this.panel1.Controls.Add(this.groupControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 520);
            this.panel1.TabIndex = 14;
            // 
            // checkedComboBoxEdit1
            // 
            this.checkedComboBoxEdit1.EditValue = "";
            this.checkedComboBoxEdit1.Location = new System.Drawing.Point(233, 224);
            this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
            this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "asdas"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "asdas")});
            this.checkedComboBoxEdit1.Size = new System.Drawing.Size(197, 20);
            this.checkedComboBoxEdit1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 520);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Teste (SVias \\ Voice)";
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelControl1;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl3;
        private System.Windows.Forms.Label labelControl4;
        private System.Windows.Forms.Label lblCtrlRetornoSVias;
        
        

        private System.Windows.Forms.GroupBox groupControl1;
        private System.Windows.Forms.GroupBox groupControl2;

        private System.Windows.Forms.TextBox textEditHostSVias;
        private System.Windows.Forms.TextBox textEditPortaSVias;
        private System.Windows.Forms.TextBox textEditNumeroRastreador;
        private System.Windows.Forms.TextBox textEditTelefoneSVias;
        private System.Windows.Forms.Button smplBtnLigarSVias;
        private System.Windows.Forms.Button smplBtnLigarVoice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCtrlRetornoVoice;
        private System.Windows.Forms.Label labelControl7;
        private System.Windows.Forms.TextBox textEditRamalVoice;
        private System.Windows.Forms.Label labelControl5;
        private System.Windows.Forms.Label labelControl6;
        private System.Windows.Forms.TextBox textEditHostVoice;
        private System.Windows.Forms.TextBox textEditTelefoneVoice;
        private System.Windows.Forms.TextBox textEditPortaVoice;
        private System.Windows.Forms.Label labelControl8;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;

    }
}

