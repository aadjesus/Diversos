using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TextFormatterTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private WellFormed.TextPanel textPanel1;
        private WellFormed.AControl aControl1;
        private WellFormed.TextPanel textPanel2;
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
            this.textPanel1 = new WellFormed.TextPanel();
            this.aControl1 = new WellFormed.AControl();
            this.textPanel2 = new WellFormed.TextPanel();
            this.SuspendLayout();
            // 
            // textPanel1
            // 
            this.textPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPanel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPanel1.JustificationStyle = WellFormed.JustificationStyles.Justified;
            this.textPanel1.Location = new System.Drawing.Point(207, 16);
            this.textPanel1.Name = "textPanel1";
            this.textPanel1.ShowWhiteSpace = true;
            this.textPanel1.Size = new System.Drawing.Size(409, 134);
            this.textPanel1.StandardTabs = 48F;
            this.textPanel1.TabIndex = 0;
            this.textPanel1.Tabs = new float[] {
        0F,
        48F,
        96F,
        144F,
        192F,
        240F,
        288F,
        336F,
        384F};
            this.textPanel1.Text = "aaaa aaaa aaa";
            // 
            // aControl1
            // 
            this.aControl1.EnableTimeout = false;
            this.aControl1.Location = new System.Drawing.Point(41, 39);
            this.aControl1.Name = "aControl1";
            this.aControl1.Size = new System.Drawing.Size(75, 23);
            this.aControl1.TabIndex = 1;
            this.aControl1.Text = "aControl1";
            this.aControl1.UsableTime = System.TimeSpan.Parse("00:00:00");
            // 
            // textPanel2
            // 
            this.textPanel2.JustificationStyle = WellFormed.JustificationStyles.Left;
            this.textPanel2.Location = new System.Drawing.Point(41, 127);
            this.textPanel2.Name = "textPanel2";
            this.textPanel2.ShowWhiteSpace = false;
            this.textPanel2.Size = new System.Drawing.Size(98, 53);
            this.textPanel2.StandardTabs = 48F;
            this.textPanel2.TabIndex = 2;
            this.textPanel2.Tabs = new float[] {
        0F,
        48F,
        96F};
            this.textPanel2.Text = "textPanel2";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(640, 294);
            this.Controls.Add(this.textPanel2);
            this.Controls.Add(this.aControl1);
            this.Controls.Add(this.textPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
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
