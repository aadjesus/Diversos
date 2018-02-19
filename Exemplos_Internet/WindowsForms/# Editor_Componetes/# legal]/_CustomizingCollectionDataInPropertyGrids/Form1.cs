using System;
using System.Reflection;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace PropertyGridSample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		Organization organization;

		internal System.Windows.Forms.PropertyGrid PropertyGrid1;
		private System.Windows.Forms.Label label1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			organization = new Organization();
			PropertyGrid1.SelectedObject = organization;

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PropertyGrid1
            // 
            this.PropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.PropertyGrid1.Location = new System.Drawing.Point(0, 28);
            this.PropertyGrid1.Name = "PropertyGrid1";
            this.PropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.PropertyGrid1.Size = new System.Drawing.Size(408, 230);
            this.PropertyGrid1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Employee data of company ABC";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(408, 254);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PropertyGrid1);
            this.Name = "Form1";
            this.Text = "Customizing Collections in Property Grid Demo";
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
