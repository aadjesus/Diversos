namespace Sessions_WebService
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
            this.components = new System.ComponentModel.Container();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.colorEdit1 = new DevExpress.XtraEditors.ColorEdit();
            this.colorEdit2 = new DevExpress.XtraEditors.ColorEdit();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton1.Location = new System.Drawing.Point(502, 166);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(72, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.TabStop = false;
            this.simpleButton1.Text = "Mostrar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Globus5Black";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEdit1.EditValue = "12345678901234567890";
            this.memoEdit1.Location = new System.Drawing.Point(12, 12);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.WordWrap = false;
            this.memoEdit1.Size = new System.Drawing.Size(628, 148);
            this.memoEdit1.TabIndex = 4;
            // 
            // colorEdit1
            // 
            this.colorEdit1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.colorEdit1.EditValue = System.Drawing.Color.Empty;
            this.colorEdit1.Location = new System.Drawing.Point(12, 166);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit1.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
            this.colorEdit1.Size = new System.Drawing.Size(42, 20);
            this.colorEdit1.TabIndex = 5;
            // 
            // colorEdit2
            // 
            this.colorEdit2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.colorEdit2.EditValue = System.Drawing.Color.Empty;
            this.colorEdit2.Location = new System.Drawing.Point(12, 201);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit2.Size = new System.Drawing.Size(42, 20);
            this.colorEdit2.TabIndex = 6;
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(149, 201);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(181, 20);
            this.imageComboBoxEdit1.TabIndex = 7;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.radioGroup1.Location = new System.Drawing.Point(336, 200);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Erro"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Informação"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Pergunta")});
            this.radioGroup1.Size = new System.Drawing.Size(304, 21);
            this.radioGroup1.TabIndex = 8;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // radioGroup2
            // 
            this.radioGroup2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.radioGroup2.Location = new System.Drawing.Point(60, 166);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Columns = 1;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Com borda"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Sem borda")});
            this.radioGroup2.Size = new System.Drawing.Size(83, 55);
            this.radioGroup2.TabIndex = 9;
            this.radioGroup2.SelectedIndexChanged += new System.EventHandler(this.radioGroup2_SelectedIndexChanged);
            // 
            // checkedComboBoxEdit1
            // 
            this.checkedComboBoxEdit1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkedComboBoxEdit1.EditValue = ", , , ";
            this.checkedComboBoxEdit1.Location = new System.Drawing.Point(149, 166);
            this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
            this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Esquerda", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Topo", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Direita", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Rodape", System.Windows.Forms.CheckState.Checked)});
            this.checkedComboBoxEdit1.Size = new System.Drawing.Size(181, 20);
            this.checkedComboBoxEdit1.TabIndex = 10;
            // 
            // spinEdit1
            // 
            this.spinEdit1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.spinEdit1.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(336, 166);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(160, 20);
            this.spinEdit1.TabIndex = 11;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton2.Location = new System.Drawing.Point(580, 166);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(60, 23);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.TabStop = false;
            this.simpleButton2.Text = "Exemplo";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 224);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.checkedComboBoxEdit1);
            this.Controls.Add(this.radioGroup2);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.imageComboBoxEdit1);
            this.Controls.Add(this.colorEdit2);
            this.Controls.Add(this.colorEdit1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.ColorEdit colorEdit1;
        private DevExpress.XtraEditors.ColorEdit colorEdit2;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Button button1;
    }
}