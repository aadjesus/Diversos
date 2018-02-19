namespace ClockControlLibrary {
  partial class HandsEditorForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if( disposing && (components != null) ) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.hhColorPanel = new System.Windows.Forms.Panel();
      this.hhNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.hhColorButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.mhColorPanel = new System.Windows.Forms.Panel();
      this.mhColorButton = new System.Windows.Forms.Button();
      this.mhNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.shColorButton = new System.Windows.Forms.Button();
      this.shColorPanel = new System.Windows.Forms.Panel();
      this.shNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.colorDialog = new System.Windows.Forms.ColorDialog();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.hhNumericUpDown)).BeginInit();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.mhNumericUpDown)).BeginInit();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.shNumericUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.Location = new System.Drawing.Point(76, 242);
      this.okButton.Name = "okButton";
      this.okButton.TabIndex = 3;
      this.okButton.Text = "OK";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(157, 242);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.TabIndex = 4;
      this.cancelButton.Text = "Cancel";
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.hhColorPanel);
      this.groupBox1.Controls.Add(this.hhNumericUpDown);
      this.groupBox1.Controls.Add(this.hhColorButton);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(220, 68);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Hour Hand";
      // 
      // hhColorPanel
      // 
      this.hhColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.hhColorPanel.Location = new System.Drawing.Point(63, 13);
      this.hhColorPanel.Name = "hhColorPanel";
      this.hhColorPanel.Size = new System.Drawing.Size(124, 20);
      this.hhColorPanel.TabIndex = 1;
      // 
      // hhNumericUpDown
      // 
      this.hhNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.hhNumericUpDown.Location = new System.Drawing.Point(63, 37);
      this.hhNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.hhNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.hhNumericUpDown.Name = "hhNumericUpDown";
      this.hhNumericUpDown.Size = new System.Drawing.Size(151, 20);
      this.hhNumericUpDown.TabIndex = 4;
      this.hhNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // hhColorButton
      // 
      this.hhColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.hhColorButton.Location = new System.Drawing.Point(194, 13);
      this.hhColorButton.Name = "hhColorButton";
      this.hhColorButton.Size = new System.Drawing.Size(20, 20);
      this.hhColorButton.TabIndex = 2;
      this.hhColorButton.Text = "...";
      this.hhColorButton.Click += new System.EventHandler(this.hhColorButton_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 39);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(36, 14);
      this.label2.TabIndex = 3;
      this.label2.Text = "Width:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 14);
      this.label1.TabIndex = 0;
      this.label1.Text = "Color:";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.mhColorPanel);
      this.groupBox2.Controls.Add(this.mhColorButton);
      this.groupBox2.Controls.Add(this.mhNumericUpDown);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Location = new System.Drawing.Point(12, 86);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(220, 68);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Minute Hand";
      // 
      // mhColorPanel
      // 
      this.mhColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mhColorPanel.Location = new System.Drawing.Point(63, 13);
      this.mhColorPanel.Name = "mhColorPanel";
      this.mhColorPanel.Size = new System.Drawing.Size(124, 20);
      this.mhColorPanel.TabIndex = 1;
      // 
      // mhColorButton
      // 
      this.mhColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.mhColorButton.Location = new System.Drawing.Point(194, 13);
      this.mhColorButton.Name = "mhColorButton";
      this.mhColorButton.Size = new System.Drawing.Size(20, 20);
      this.mhColorButton.TabIndex = 2;
      this.mhColorButton.Text = "...";
      this.mhColorButton.Click += new System.EventHandler(this.mhColorButton_Click);
      // 
      // mhNumericUpDown
      // 
      this.mhNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mhNumericUpDown.Location = new System.Drawing.Point(63, 37);
      this.mhNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.mhNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.mhNumericUpDown.Name = "mhNumericUpDown";
      this.mhNumericUpDown.Size = new System.Drawing.Size(151, 20);
      this.mhNumericUpDown.TabIndex = 4;
      this.mhNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 39);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(36, 14);
      this.label3.TabIndex = 3;
      this.label3.Text = "Width:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 19);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(34, 14);
      this.label4.TabIndex = 0;
      this.label4.Text = "Color:";
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.shColorButton);
      this.groupBox3.Controls.Add(this.shColorPanel);
      this.groupBox3.Controls.Add(this.shNumericUpDown);
      this.groupBox3.Controls.Add(this.label5);
      this.groupBox3.Controls.Add(this.label6);
      this.groupBox3.Location = new System.Drawing.Point(12, 160);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(220, 68);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Second Hand";
      // 
      // shColorButton
      // 
      this.shColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.shColorButton.Location = new System.Drawing.Point(194, 13);
      this.shColorButton.Name = "shColorButton";
      this.shColorButton.Size = new System.Drawing.Size(20, 20);
      this.shColorButton.TabIndex = 2;
      this.shColorButton.Text = "...";
      this.shColorButton.Click += new System.EventHandler(this.shColorButton_Click);
      // 
      // shColorPanel
      // 
      this.shColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.shColorPanel.Location = new System.Drawing.Point(63, 13);
      this.shColorPanel.Name = "shColorPanel";
      this.shColorPanel.Size = new System.Drawing.Size(124, 20);
      this.shColorPanel.TabIndex = 1;
      // 
      // shNumericUpDown
      // 
      this.shNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.shNumericUpDown.Location = new System.Drawing.Point(63, 37);
      this.shNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.shNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.shNumericUpDown.Name = "shNumericUpDown";
      this.shNumericUpDown.Size = new System.Drawing.Size(151, 20);
      this.shNumericUpDown.TabIndex = 4;
      this.shNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 39);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(36, 14);
      this.label5.TabIndex = 3;
      this.label5.Text = "Width:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 19);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(34, 14);
      this.label6.TabIndex = 0;
      this.label6.Text = "Color:";
      // 
      // HandsEditorForm
      // 
      this.AcceptButton = this.okButton;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(244, 277);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.MinimumSize = new System.Drawing.Size(252, 311);
      this.Name = "HandsEditorForm";
      this.ShowInTaskbar = false;
      this.Text = "Edit Clock Hands";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.hhNumericUpDown)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.mhNumericUpDown)).EndInit();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.shNumericUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Panel hhColorPanel;
    private System.Windows.Forms.NumericUpDown hhNumericUpDown;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Panel mhColorPanel;
    private System.Windows.Forms.NumericUpDown mhNumericUpDown;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Panel shColorPanel;
    private System.Windows.Forms.NumericUpDown shNumericUpDown;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button hhColorButton;
    private System.Windows.Forms.Button mhColorButton;
    private System.Windows.Forms.Button shColorButton;
    private System.Windows.Forms.ColorDialog colorDialog;
  }
}