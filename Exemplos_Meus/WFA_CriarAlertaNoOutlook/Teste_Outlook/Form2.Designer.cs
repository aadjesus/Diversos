namespace Teste_Outlook
{
    partial class Form2
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
            this.dateEditData = new DevExpress.XtraEditors.DateEdit();
            this.textEditLocal = new DevExpress.XtraEditors.TextEdit();
            this.textEditAssunto = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditTexto = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.imageComboBoxEdit2 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.elementHostComboBox = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditData.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLocal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAssunto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditTexto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEditData
            // 
            this.dateEditData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditData.EditValue = null;
            this.dateEditData.Location = new System.Drawing.Point(82, 61);
            this.dateEditData.Name = "dateEditData";
            this.dateEditData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditData.Properties.DisplayFormat.FormatString = "g";
            this.dateEditData.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditData.Properties.EditFormat.FormatString = "g";
            this.dateEditData.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditData.Properties.Mask.EditMask = "g";
            this.dateEditData.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditData.Size = new System.Drawing.Size(197, 20);
            this.dateEditData.TabIndex = 8;
            // 
            // textEditLocal
            // 
            this.textEditLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditLocal.EditValue = "Local";
            this.textEditLocal.Location = new System.Drawing.Point(82, 35);
            this.textEditLocal.Name = "textEditLocal";
            this.textEditLocal.Size = new System.Drawing.Size(197, 20);
            this.textEditLocal.TabIndex = 7;
            // 
            // textEditAssunto
            // 
            this.textEditAssunto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditAssunto.EditValue = "Assunto";
            this.textEditAssunto.Location = new System.Drawing.Point(82, 9);
            this.textEditAssunto.Name = "textEditAssunto";
            this.textEditAssunto.Size = new System.Drawing.Size(197, 20);
            this.textEditAssunto.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(64, 311);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Data";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Local";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "Assunto";
            // 
            // memoEditTexto
            // 
            this.memoEditTexto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditTexto.EditValue = "Texto";
            this.memoEditTexto.Location = new System.Drawing.Point(82, 165);
            this.memoEditTexto.Name = "memoEditTexto";
            this.memoEditTexto.Size = new System.Drawing.Size(197, 140);
            this.memoEditTexto.TabIndex = 12;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 168);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Descrição";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 90);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(47, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Categoria";
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(82, 113);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(197, 20);
            this.imageComboBoxEdit1.TabIndex = 16;
            // 
            // imageComboBoxEdit2
            // 
            this.imageComboBoxEdit2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageComboBoxEdit2.Location = new System.Drawing.Point(82, 139);
            this.imageComboBoxEdit2.Name = "imageComboBoxEdit2";
            this.imageComboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit2.Size = new System.Drawing.Size(197, 20);
            this.imageComboBoxEdit2.TabIndex = 17;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 116);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Prioridade";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(11, 142);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(65, 13);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Mostrar como";
            // 
            // elementHostComboBox
            // 
            this.elementHostComboBox.Location = new System.Drawing.Point(82, 87);
            this.elementHostComboBox.Name = "elementHostComboBox";
            this.elementHostComboBox.Size = new System.Drawing.Size(197, 20);
            this.elementHostComboBox.TabIndex = 21;
            this.elementHostComboBox.Text = "elementHost1";
            this.elementHostComboBox.Child = null;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 342);
            this.Controls.Add(this.elementHostComboBox);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.imageComboBoxEdit2);
            this.Controls.Add(this.imageComboBoxEdit1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.memoEditTexto);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dateEditData);
            this.Controls.Add(this.textEditLocal);
            this.Controls.Add(this.textEditAssunto);
            this.Controls.Add(this.simpleButton1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dateEditData.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLocal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAssunto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditTexto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateEditData;
        private DevExpress.XtraEditors.TextEdit textEditLocal;
        private DevExpress.XtraEditors.TextEdit textEditAssunto;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit memoEditTexto;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.Integration.ElementHost elementHostComboBox;
    }
}