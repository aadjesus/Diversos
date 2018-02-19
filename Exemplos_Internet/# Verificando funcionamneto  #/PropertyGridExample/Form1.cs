using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace PropertyGridExample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblTitle;
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
			propertyGrid1.Focus();

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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblTitle = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(56, 40);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(224, 272);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(112, 320);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(104, 32);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.Location = new System.Drawing.Point(40, 8);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(248, 16);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Edit New Customer:";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 358);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "Form1";
			this.Text = "Checking out the Property Grid";
			this.Load += new System.EventHandler(this.Form1_Load);
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

		private void btnOK_Click(object sender, System.EventArgs e)
		{
		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			Customer bill = new Customer();
			bill.Age = 50;
			bill.Address = "114 Maple Drive";
			bill.DateOfBirth = Convert.ToDateTime("9/14/78");
			bill.SSN = "123-345-3566";
			bill.Email = "bill@aol.com";
			bill.Name = "Bill Smith";

			propertyGrid1.SelectedObject = bill;
		}
	}
}
