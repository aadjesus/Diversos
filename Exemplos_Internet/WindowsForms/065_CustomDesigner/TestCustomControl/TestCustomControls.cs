using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestCustomControl
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label CustomRectangelControl;
		private Phone_Number_Control.PhoneNumControl phoneNumControl1;        

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
            this.CustomRectangelControl = new System.Windows.Forms.Label();
            this.phoneNumControl1 = new Phone_Number_Control.PhoneNumControl();
            this.SuspendLayout();
            // 
            // CustomRectangelControl
            // 
            this.CustomRectangelControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomRectangelControl.ForeColor = System.Drawing.Color.Firebrick;
            this.CustomRectangelControl.Location = new System.Drawing.Point(109, 81);
            this.CustomRectangelControl.Name = "CustomRectangelControl";
            this.CustomRectangelControl.Size = new System.Drawing.Size(160, 23);
            this.CustomRectangelControl.TabIndex = 2;
            this.CustomRectangelControl.Text = "PhoneNumberControl";
            // 
            // phoneNumControl1
            // 
            this.phoneNumControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.phoneNumControl1.ForeColor = System.Drawing.Color.Crimson;
            this.phoneNumControl1.Location = new System.Drawing.Point(75, 107);
            this.phoneNumControl1.Name = "phoneNumControl1";
            this.phoneNumControl1.PhoneData.AreaCode = "";
            this.phoneNumControl1.PhoneData.CountryCode = null;
            this.phoneNumControl1.PhoneData.Name = null;
            this.phoneNumControl1.PhoneData.PhoneNum = null;
            this.phoneNumControl1.Size = new System.Drawing.Size(278, 188);
            this.phoneNumControl1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(728, 558);
            this.Controls.Add(this.phoneNumControl1);
            this.Controls.Add(this.CustomRectangelControl);
            this.Name = "Form1";
            this.Text = "TestCustomControls";
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void rectangleControl1_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
