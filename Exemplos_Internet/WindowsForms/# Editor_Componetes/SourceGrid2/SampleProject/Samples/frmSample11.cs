using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample11.
	/// </summary>
	public class frmSample11 : System.Windows.Forms.Form
	{
		private SampleProject.Extensions.WorkSheet workSheet1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample11()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.workSheet1 = new SampleProject.Extensions.WorkSheet();
			this.SuspendLayout();
			// 
			// workSheet1
			// 
			this.workSheet1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.workSheet1.Location = new System.Drawing.Point(0, 0);
			this.workSheet1.Name = "workSheet1";
			this.workSheet1.Size = new System.Drawing.Size(496, 407);
			this.workSheet1.TabIndex = 0;
			// 
			// frmSample11
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 407);
			this.Controls.Add(this.workSheet1);
			this.Name = "frmSample11";
			this.Text = "Worksheet";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
