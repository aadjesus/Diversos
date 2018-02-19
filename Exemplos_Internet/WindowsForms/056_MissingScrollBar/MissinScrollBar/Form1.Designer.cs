namespace MissinScrollBar {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
			this.ScrollableBottom = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			this.xtraScrollableControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraScrollableControl1
			// 
			this.xtraScrollableControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.xtraScrollableControl1.Controls.Add(this.pictureEdit1);
			this.xtraScrollableControl1.Location = new System.Drawing.Point(12, 12);
			this.xtraScrollableControl1.Name = "xtraScrollableControl1";
			this.xtraScrollableControl1.Size = new System.Drawing.Size(568, 128);
			this.xtraScrollableControl1.TabIndex = 0;
			// 
			// ScrollableBottom
			// 
			this.ScrollableBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ScrollableBottom.Location = new System.Drawing.Point(12, 146);
			this.ScrollableBottom.Name = "ScrollableBottom";
			this.ScrollableBottom.Size = new System.Drawing.Size(568, 218);
			this.ScrollableBottom.TabIndex = 1;
			this.ScrollableBottom.VisibleChanged += new System.EventHandler(this.OnVisableChanged);
			this.ScrollableBottom.SizeChanged += new System.EventHandler(this.OnSizeChanged);
			// 
			// pictureEdit1
			// 
			this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
			this.pictureEdit1.Location = new System.Drawing.Point(3, 3);
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			this.pictureEdit1.Size = new System.Drawing.Size(100, 80);
			this.pictureEdit1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 376);
			this.Controls.Add(this.ScrollableBottom);
			this.Controls.Add(this.xtraScrollableControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.xtraScrollableControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
		private DevExpress.XtraEditors.XtraScrollableControl ScrollableBottom;
		private DevExpress.XtraEditors.PictureEdit pictureEdit1;
	}
}

