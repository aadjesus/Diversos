namespace ClockControlHost {
  partial class MainForm {
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
            this.clockControl1 = new ClockControlLibrary.ClockControl();
            ((System.ComponentModel.ISupportInitialize)(this.clockControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // clockControl1
            // 
            this.clockControl1.BackupAlarm = new System.DateTime(2005, 4, 23, 13, 57, 0, 0);
            this.clockControl1.DigitalTimeFormat = "dd/MM hh:mm:ss tt///////";
            this.clockControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clockControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.clockControl1.Location = new System.Drawing.Point(3, 3);
            this.clockControl1.Name = "clockControl1";
            this.clockControl1.PrimaryAlarm = new System.DateTime(2005, 4, 23, 13, 47, 0, 0);
            this.clockControl1.Size = new System.Drawing.Size(457, 336);
            this.clockControl1.TabIndex = 0;
            this.clockControl1.Text = "clockControl1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 342);
            this.Controls.Add(this.clockControl1);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.clockControl1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private ClockControlLibrary.ClockControl clockControl1;



  }
}

