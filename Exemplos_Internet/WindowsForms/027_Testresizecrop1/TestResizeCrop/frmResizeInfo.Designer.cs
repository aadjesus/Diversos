namespace TestResizeCrop
{
  partial class frmResizeInfo
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
      this.lbHeight = new System.Windows.Forms.Label();
      this.lblWidth = new System.Windows.Forms.Label();
      this.tbHeight = new System.Windows.Forms.TextBox();
      this.tbWidth = new System.Windows.Forms.TextBox();
      this.butOK = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lbHeight
      // 
      this.lbHeight.Location = new System.Drawing.Point(12, 9);
      this.lbHeight.Name = "lbHeight";
      this.lbHeight.Size = new System.Drawing.Size(50, 20);
      this.lbHeight.TabIndex = 0;
      this.lbHeight.Text = "Height";
      // 
      // lblWidth
      // 
      this.lblWidth.Location = new System.Drawing.Point(12, 39);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(50, 20);
      this.lblWidth.TabIndex = 0;
      this.lblWidth.Text = "Width";
      // 
      // tbHeight
      // 
      this.tbHeight.Location = new System.Drawing.Point(68, 6);
      this.tbHeight.Name = "tbHeight";
      this.tbHeight.Size = new System.Drawing.Size(100, 20);
      this.tbHeight.TabIndex = 0;
      // 
      // tbWidth
      // 
      this.tbWidth.Location = new System.Drawing.Point(68, 36);
      this.tbWidth.Name = "tbWidth";
      this.tbWidth.Size = new System.Drawing.Size(100, 20);
      this.tbWidth.TabIndex = 1;
      // 
      // butOK
      // 
      this.butOK.Location = new System.Drawing.Point(152, 62);
      this.butOK.Name = "butOK";
      this.butOK.Size = new System.Drawing.Size(75, 23);
      this.butOK.TabIndex = 2;
      this.butOK.Text = "Resize";
      this.butOK.UseVisualStyleBackColor = true;
      this.butOK.Click += new System.EventHandler(this.butOK_Click);
      // 
      // frmResizeInfo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(239, 97);
      this.Controls.Add(this.butOK);
      this.Controls.Add(this.tbWidth);
      this.Controls.Add(this.tbHeight);
      this.Controls.Add(this.lblWidth);
      this.Controls.Add(this.lbHeight);
      this.Name = "frmResizeInfo";
      this.Text = "Input Resize Info";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbHeight;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.TextBox tbHeight;
    private System.Windows.Forms.TextBox tbWidth;
    private System.Windows.Forms.Button butOK;

  }
}