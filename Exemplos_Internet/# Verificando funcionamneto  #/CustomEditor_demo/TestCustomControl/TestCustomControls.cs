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
		private CustomEditor.RectangleControl rectangleControl1;
		private Phone_Number_Control.PhoneNumControl phoneNumControl1;
		private System.Windows.Forms.Label CustomRectangelControl;
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
			this.rectangleControl1 = new CustomEditor.RectangleControl();
			this.phoneNumControl1 = new Phone_Number_Control.PhoneNumControl();
			this.CustomRectangelControl = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// rectangleControl1
			// 
			this.rectangleControl1.BackColor = System.Drawing.SystemColors.Info;
			this.rectangleControl1.Location = new System.Drawing.Point(32, 72);
			this.rectangleControl1.Name = "rectangleControl1";
			this.rectangleControl1.RectHeight = 122;
			this.rectangleControl1.RectWidth = 191;
			this.rectangleControl1.Size = new System.Drawing.Size(312, 192);
			this.rectangleControl1.TabIndex = 0;
			this.rectangleControl1.Load += new System.EventHandler(this.rectangleControl1_Load);
			// 
			// phoneNumControl1
			// 
			this.phoneNumControl1.BackColor = System.Drawing.SystemColors.Info;
			this.phoneNumControl1.Location = new System.Drawing.Point(32, 320);
			this.phoneNumControl1.Name = "phoneNumControl1";
			this.phoneNumControl1.PhoneData.AreaCode = "123";
			this.phoneNumControl1.PhoneData.CountryCode = "11";
			this.phoneNumControl1.PhoneData.Name = "Pani";
			this.phoneNumControl1.PhoneData.PhoneNum = "12345";
			this.phoneNumControl1.Size = new System.Drawing.Size(320, 216);
			this.phoneNumControl1.TabIndex = 1;
			// 
			// CustomRectangelControl
			// 
			this.CustomRectangelControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CustomRectangelControl.ForeColor = System.Drawing.Color.Firebrick;
			this.CustomRectangelControl.Location = new System.Drawing.Point(32, 288);
			this.CustomRectangelControl.Name = "CustomRectangelControl";
			this.CustomRectangelControl.Size = new System.Drawing.Size(160, 23);
			this.CustomRectangelControl.TabIndex = 2;
			this.CustomRectangelControl.Text = "PhoneNumberControl";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Firebrick;
			this.label1.Location = new System.Drawing.Point(40, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "RectangleControl";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(600, 558);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CustomRectangelControl);
			this.Controls.Add(this.phoneNumControl1);
			this.Controls.Add(this.rectangleControl1);
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
