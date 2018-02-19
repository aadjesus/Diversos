using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Snippy
{
	public class AboutForm : Form
	{
		// Fields
		private Button btnClose;
		private Container components = null;
		private GroupBox groupBox1;
		private Label label1;
		private Label label3;
		private Label lblVersion;
		private LinkLabel lnkBlog;

		// Methods
		public AboutForm()
		{
			this.InitializeComponent();
			this.lblVersion.Text = "Version 0.2.0.0";
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lnkBlog = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(280, 120);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "&Close";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(328, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Snippy - Visual Studio Code Snippet Editor";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(12, 36);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(328, 20);
			this.lblVersion.TabIndex = 2;
			this.lblVersion.Text = "Version";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(328, 20);
			this.label3.TabIndex = 3;
			// 
			// lnkBlog
			// 
			this.lnkBlog.Location = new System.Drawing.Point(24, 92);
			this.lnkBlog.Name = "lnkBlog";
			this.lnkBlog.Size = new System.Drawing.Size(260, 23);
			this.lnkBlog.TabIndex = 4;
			this.lnkBlog.TabStop = true;
			this.lnkBlog.Text = "Bug reports, latest versions, and blog";
			this.lnkBlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBlog_LinkClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lblVersion);
			this.groupBox1.Location = new System.Drawing.Point(8, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 60);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// AboutForm
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(364, 151);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lnkBlog);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnClose);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About Snippy";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private void lnkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://blogs.msdn.com/vseditor");
		}
	}

 

}
