namespace TestResizeCrop
{
  partial class frmResizeCrop
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResizeCrop));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.lblPM = new System.Windows.Forms.Label();
      this.lblPicMode = new System.Windows.Forms.Label();
      this.pbImage = new System.Windows.Forms.PictureBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnOpenImage = new System.Windows.Forms.ToolStripButton();
      this.butResize = new System.Windows.Forms.ToolStripButton();
      this.butCrop = new System.Windows.Forms.ToolStripButton();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.lblImgSize = new System.Windows.Forms.ToolStripLabel();
      this.lblImgHeight = new System.Windows.Forms.ToolStripLabel();
      this.lblImgHeightAct = new System.Windows.Forms.ToolStripLabel();
      this.lblImgWidth = new System.Windows.Forms.ToolStripLabel();
      this.lblImgWidthAct = new System.Windows.Forms.ToolStripLabel();
      this.butSave = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
      this.toolStrip1.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.lblPM);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.lblPicMode);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.pbImage);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(541, 488);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(541, 513);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
      // 
      // lblPM
      // 
      this.lblPM.AutoSize = true;
      this.lblPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPM.Location = new System.Drawing.Point(12, 10);
      this.lblPM.Name = "lblPM";
      this.lblPM.Size = new System.Drawing.Size(105, 24);
      this.lblPM.TabIndex = 1;
      this.lblPM.Text = "Size Mode:";
      // 
      // lblPicMode
      // 
      this.lblPicMode.AutoSize = true;
      this.lblPicMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPicMode.Location = new System.Drawing.Point(123, 10);
      this.lblPicMode.Name = "lblPicMode";
      this.lblPicMode.Size = new System.Drawing.Size(60, 24);
      this.lblPicMode.TabIndex = 1;
      this.lblPicMode.Text = "Zoom";
      this.lblPicMode.Click += new System.EventHandler(this.lblPicMode_Click);
      // 
      // pbImage
      // 
      this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbImage.Location = new System.Drawing.Point(0, 0);
      this.pbImage.Name = "pbImage";
      this.pbImage.Size = new System.Drawing.Size(541, 488);
      this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pbImage.TabIndex = 0;
      this.pbImage.TabStop = false;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenImage,
            this.butSave,
            this.butResize,
            this.butCrop});
      this.toolStrip1.Location = new System.Drawing.Point(3, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(257, 25);
      this.toolStrip1.TabIndex = 0;
      // 
      // btnOpenImage
      // 
      this.btnOpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenImage.Image")));
      this.btnOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpenImage.Name = "btnOpenImage";
      this.btnOpenImage.Size = new System.Drawing.Size(70, 22);
      this.btnOpenImage.Text = "Open Image";
      this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
      // 
      // butResize
      // 
      this.butResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.butResize.Enabled = false;
      this.butResize.Image = ((System.Drawing.Image)(resources.GetObject("butResize.Image")));
      this.butResize.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.butResize.Name = "butResize";
      this.butResize.Size = new System.Drawing.Size(42, 22);
      this.butResize.Text = "Resize";
      this.butResize.Click += new System.EventHandler(this.butResize_Click);
      // 
      // butCrop
      // 
      this.butCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.butCrop.Enabled = false;
      this.butCrop.Image = ((System.Drawing.Image)(resources.GetObject("butCrop.Image")));
      this.butCrop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.butCrop.Name = "butCrop";
      this.butCrop.Size = new System.Drawing.Size(34, 22);
      this.butCrop.Text = "Crop";
      this.butCrop.Click += new System.EventHandler(this.butCrop_Click);
      // 
      // toolStrip2
      // 
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblImgSize,
            this.lblImgHeight,
            this.lblImgHeightAct,
            this.lblImgWidth,
            this.lblImgWidthAct});
      this.toolStrip2.Location = new System.Drawing.Point(260, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(256, 25);
      this.toolStrip2.TabIndex = 1;
      // 
      // lblImgSize
      // 
      this.lblImgSize.Name = "lblImgSize";
      this.lblImgSize.Size = new System.Drawing.Size(63, 22);
      this.lblImgSize.Text = "Image Size:";
      // 
      // lblImgHeight
      // 
      this.lblImgHeight.Name = "lblImgHeight";
      this.lblImgHeight.Size = new System.Drawing.Size(42, 22);
      this.lblImgHeight.Text = "Height:";
      // 
      // lblImgHeightAct
      // 
      this.lblImgHeightAct.AutoSize = false;
      this.lblImgHeightAct.Name = "lblImgHeightAct";
      this.lblImgHeightAct.Size = new System.Drawing.Size(50, 22);
      // 
      // lblImgWidth
      // 
      this.lblImgWidth.Name = "lblImgWidth";
      this.lblImgWidth.Size = new System.Drawing.Size(39, 22);
      this.lblImgWidth.Text = "Width:";
      // 
      // lblImgWidthAct
      // 
      this.lblImgWidthAct.AutoSize = false;
      this.lblImgWidthAct.Name = "lblImgWidthAct";
      this.lblImgWidthAct.Size = new System.Drawing.Size(50, 22);
      // 
      // butSave
      // 
      this.butSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.butSave.Enabled = false;
      this.butSave.Image = ((System.Drawing.Image)(resources.GetObject("butSave.Image")));
      this.butSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.butSave.Name = "butSave";
      this.butSave.Size = new System.Drawing.Size(68, 22);
      this.butSave.Text = "Save Image";
      this.butSave.Click += new System.EventHandler(this.butSave_Click);
      // 
      // frmResizeCrop
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(541, 513);
      this.Controls.Add(this.toolStripContainer1);
      this.Name = "frmResizeCrop";
      this.Text = "Test Resize & Crop";
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.PerformLayout();
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.PictureBox pbImage;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnOpenImage;
    private System.Windows.Forms.ToolStripButton butResize;
    private System.Windows.Forms.ToolStripButton butCrop;
    private System.Windows.Forms.Label lblPM;
    private System.Windows.Forms.Label lblPicMode;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripLabel lblImgSize;
    private System.Windows.Forms.ToolStripLabel lblImgHeight;
    private System.Windows.Forms.ToolStripLabel lblImgHeightAct;
    private System.Windows.Forms.ToolStripLabel lblImgWidth;
    private System.Windows.Forms.ToolStripLabel lblImgWidthAct;
    private System.Windows.Forms.ToolStripButton butSave;
  }
}

