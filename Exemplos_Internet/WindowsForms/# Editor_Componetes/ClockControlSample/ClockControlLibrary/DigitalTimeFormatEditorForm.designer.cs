namespace ClockControlLibrary {
  partial class DigitalTimeFormatEditorForm {
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
      this.components = new System.ComponentModel.Container();
      this.okButton = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.example2Label = new System.Windows.Forms.Label();
      this.addFormatSpecifierButton = new System.Windows.Forms.Button();
      this.example1Label = new System.Windows.Forms.Label();
      this.formatText = new System.Windows.Forms.TextBox();
      this.exampleLabel = new System.Windows.Forms.Label();
      this.formatLabel = new System.Windows.Forms.Label();
      this.lstFormatSpecifiers = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(250, 71);
      this.okButton.Name = "okButton";
      this.okButton.TabIndex = 2;
      this.okButton.Text = "OK";
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(331, 71);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      // 
      // example2Label
      // 
      this.example2Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.example2Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
      this.example2Label.Location = new System.Drawing.Point(70, 51);
      this.example2Label.Name = "example2Label";
      this.example2Label.Size = new System.Drawing.Size(330, 14);
      this.example2Label.TabIndex = 13;
      this.example2Label.Text = "1/1/02 1:1:1 AM";
      // 
      // addFormatSpecifierButton
      // 
      this.addFormatSpecifierButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
      this.addFormatSpecifierButton.Location = new System.Drawing.Point(224, 9);
      this.addFormatSpecifierButton.Name = "addFormatSpecifierButton";
      this.addFormatSpecifierButton.Size = new System.Drawing.Size(29, 19);
      this.addFormatSpecifierButton.TabIndex = 9;
      this.addFormatSpecifierButton.Text = "<<";
      this.addFormatSpecifierButton.Click += new System.EventHandler(this.addFormatSpecifierButton_Click);
      // 
      // example1Label
      // 
      this.example1Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.example1Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
      this.example1Label.Location = new System.Drawing.Point(70, 38);
      this.example1Label.Name = "example1Label";
      this.example1Label.Size = new System.Drawing.Size(330, 14);
      this.example1Label.TabIndex = 12;
      this.example1Label.Text = "12/12/2002 12:12:12 AM";
      // 
      // formatText
      // 
      this.formatText.Location = new System.Drawing.Point(73, 8);
      this.formatText.Name = "formatText";
      this.formatText.Size = new System.Drawing.Size(145, 20);
      this.formatText.TabIndex = 8;
      this.formatText.TextChanged += new System.EventHandler(this.formatText_TextChanged);
      // 
      // exampleLabel
      // 
      this.exampleLabel.Location = new System.Drawing.Point(8, 38);
      this.exampleLabel.Name = "exampleLabel";
      this.exampleLabel.Size = new System.Drawing.Size(60, 23);
      this.exampleLabel.TabIndex = 11;
      this.exampleLabel.Text = "Examples:";
      // 
      // formatLabel
      // 
      this.formatLabel.Location = new System.Drawing.Point(7, 11);
      this.formatLabel.Name = "formatLabel";
      this.formatLabel.Size = new System.Drawing.Size(60, 23);
      this.formatLabel.TabIndex = 7;
      this.formatLabel.Text = "Format:";
      // 
      // lstFormatSpecifiers
      // 
      this.lstFormatSpecifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.lstFormatSpecifiers.FormattingEnabled = true;
      this.lstFormatSpecifiers.Location = new System.Drawing.Point(261, 8);
      this.lstFormatSpecifiers.Name = "lstFormatSpecifiers";
      this.lstFormatSpecifiers.Size = new System.Drawing.Size(145, 21);
      this.lstFormatSpecifiers.TabIndex = 10;
      // 
      // DigitalTimeFormatEditorForm
      // 
      this.AcceptButton = this.okButton;
      this.ClientSize = new System.Drawing.Size(412, 100);
      this.Controls.Add(this.example2Label);
      this.Controls.Add(this.addFormatSpecifierButton);
      this.Controls.Add(this.example1Label);
      this.Controls.Add(this.formatText);
      this.Controls.Add(this.exampleLabel);
      this.Controls.Add(this.formatLabel);
      this.Controls.Add(this.lstFormatSpecifiers);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.okButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DigitalTimeFormatEditorForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Digital Time Format Editor";
      this.Load += new System.EventHandler(this.DigitalTimeFormatEditorForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label example2Label;
    private System.Windows.Forms.Button addFormatSpecifierButton;
    private System.Windows.Forms.Label example1Label;
    private System.Windows.Forms.TextBox formatText;
    private System.Windows.Forms.Label exampleLabel;
    private System.Windows.Forms.Label formatLabel;
    private System.Windows.Forms.ComboBox lstFormatSpecifiers;
  }
}