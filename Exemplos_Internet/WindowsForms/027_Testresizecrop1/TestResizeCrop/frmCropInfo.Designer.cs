namespace TestResizeCrop
{
  partial class frmCropInfo
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
      this.lblX = new System.Windows.Forms.Label();
      this.lblY = new System.Windows.Forms.Label();
      this.lblHeight = new System.Windows.Forms.Label();
      this.lblWidth = new System.Windows.Forms.Label();
      this.tbX = new System.Windows.Forms.TextBox();
      this.tbY = new System.Windows.Forms.TextBox();
      this.tbHeight = new System.Windows.Forms.TextBox();
      this.tbWidth = new System.Windows.Forms.TextBox();
      this.butCrop = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblX
      // 
      this.lblX.Location = new System.Drawing.Point(12, 9);
      this.lblX.Name = "lblX";
      this.lblX.Size = new System.Drawing.Size(50, 20);
      this.lblX.TabIndex = 0;
      this.lblX.Text = "Start X";
      // 
      // lblY
      // 
      this.lblY.Location = new System.Drawing.Point(12, 35);
      this.lblY.Name = "lblY";
      this.lblY.Size = new System.Drawing.Size(50, 20);
      this.lblY.TabIndex = 0;
      this.lblY.Text = "Start Y";
      // 
      // lblHeight
      // 
      this.lblHeight.Location = new System.Drawing.Point(12, 61);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(50, 20);
      this.lblHeight.TabIndex = 0;
      this.lblHeight.Text = "Height";
      // 
      // lblWidth
      // 
      this.lblWidth.Location = new System.Drawing.Point(12, 87);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(50, 20);
      this.lblWidth.TabIndex = 0;
      this.lblWidth.Text = "Width";
      // 
      // tbX
      // 
      this.tbX.Location = new System.Drawing.Point(68, 6);
      this.tbX.Name = "tbX";
      this.tbX.Size = new System.Drawing.Size(100, 20);
      this.tbX.TabIndex = 0;
      // 
      // tbY
      // 
      this.tbY.Location = new System.Drawing.Point(68, 32);
      this.tbY.Name = "tbY";
      this.tbY.Size = new System.Drawing.Size(100, 20);
      this.tbY.TabIndex = 1;
      // 
      // tbHeight
      // 
      this.tbHeight.Location = new System.Drawing.Point(68, 58);
      this.tbHeight.Name = "tbHeight";
      this.tbHeight.Size = new System.Drawing.Size(100, 20);
      this.tbHeight.TabIndex = 2;
      // 
      // tbWidth
      // 
      this.tbWidth.Location = new System.Drawing.Point(68, 84);
      this.tbWidth.Name = "tbWidth";
      this.tbWidth.Size = new System.Drawing.Size(100, 20);
      this.tbWidth.TabIndex = 3;
      // 
      // butCrop
      // 
      this.butCrop.Location = new System.Drawing.Point(173, 110);
      this.butCrop.Name = "butCrop";
      this.butCrop.Size = new System.Drawing.Size(75, 23);
      this.butCrop.TabIndex = 4;
      this.butCrop.Text = "Crop";
      this.butCrop.UseVisualStyleBackColor = true;
      this.butCrop.Click += new System.EventHandler(this.butCrop_Click);
      // 
      // frmCropInfo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(260, 145);
      this.Controls.Add(this.butCrop);
      this.Controls.Add(this.tbWidth);
      this.Controls.Add(this.tbHeight);
      this.Controls.Add(this.tbY);
      this.Controls.Add(this.tbX);
      this.Controls.Add(this.lblWidth);
      this.Controls.Add(this.lblHeight);
      this.Controls.Add(this.lblY);
      this.Controls.Add(this.lblX);
      this.Name = "frmCropInfo";
      this.Text = "Enter Crop Info";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblX;
    private System.Windows.Forms.Label lblY;
    private System.Windows.Forms.Label lblHeight;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.TextBox tbX;
    private System.Windows.Forms.TextBox tbY;
    private System.Windows.Forms.TextBox tbHeight;
    private System.Windows.Forms.TextBox tbWidth;
    private System.Windows.Forms.Button butCrop;
  }
}