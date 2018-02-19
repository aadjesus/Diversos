namespace ClockControlLibrary {
  partial class FaceEditorControl {
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.bothPictureBox = new System.Windows.Forms.PictureBox();
      this.analogPictureBox = new System.Windows.Forms.PictureBox();
      this.digitalPictureBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.bothPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.analogPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.digitalPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // bothPictureBox
      // 
      this.bothPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.bothPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.bothPictureBox.Image = ClockControlLibrary.Properties.Resources.BothClock;
      this.bothPictureBox.Location = new System.Drawing.Point(5, 4);
      this.bothPictureBox.Name = "bothPictureBox";
      this.bothPictureBox.Size = new System.Drawing.Size(63, 63);
      this.bothPictureBox.TabIndex = 0;
      this.bothPictureBox.TabStop = false;
      this.bothPictureBox.Click += new System.EventHandler(this.bothPictureBox_Click);
      // 
      // analogPictureBox
      // 
      this.analogPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.analogPictureBox.Image = ClockControlLibrary.Properties.Resources.AnalogClock;
      this.analogPictureBox.Location = new System.Drawing.Point(74, 4);
      this.analogPictureBox.Name = "analogPictureBox";
      this.analogPictureBox.Size = new System.Drawing.Size(63, 63);
      this.analogPictureBox.TabIndex = 1;
      this.analogPictureBox.TabStop = false;
      this.analogPictureBox.Click += new System.EventHandler(this.analogPictureBox_Click);
      // 
      // digitalPictureBox
      // 
      this.digitalPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.digitalPictureBox.Image = ClockControlLibrary.Properties.Resources.DigitalClock;
      this.digitalPictureBox.Location = new System.Drawing.Point(143, 4);
      this.digitalPictureBox.Name = "digitalPictureBox";
      this.digitalPictureBox.Size = new System.Drawing.Size(63, 63);
      this.digitalPictureBox.TabIndex = 2;
      this.digitalPictureBox.TabStop = false;
      this.digitalPictureBox.Click += new System.EventHandler(this.digitalPictureBox_Click);
      // 
      // FaceEditorControl
      // 
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
      this.Controls.Add(this.digitalPictureBox);
      this.Controls.Add(this.analogPictureBox);
      this.Controls.Add(this.bothPictureBox);
      this.Name = "FaceEditorControl";
      this.Size = new System.Drawing.Size(211, 72);
      ((System.ComponentModel.ISupportInitialize)(this.bothPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.analogPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.digitalPictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox bothPictureBox;
    private System.Windows.Forms.PictureBox analogPictureBox;
    private System.Windows.Forms.PictureBox digitalPictureBox;
  }
}
