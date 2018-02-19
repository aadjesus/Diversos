using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace tabtest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private tabtest.FunkyButton FunkyButton1;
        private tabtest.FunkyButton funkyButton2;
        private FunkyButton funkyButton3;
        private FunkyButton funkyButton4;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.funkyButton3 = new tabtest.FunkyButton();
            this.funkyButton4 = new tabtest.FunkyButton();
            ((System.ComponentModel.ISupportInitialize)(this.funkyButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.funkyButton4)).BeginInit();
            this.SuspendLayout();
            // 
            // funkyButton3
            // 
            this.funkyButton3.BackColor = System.Drawing.Color.Blue;
            this.funkyButton3.Location = new System.Drawing.Point(49, 50);
            this.funkyButton3.Name = "funkyButton3";
            this.funkyButton3.Points.AddRange(new System.Drawing.Point[] {
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(0, 0)});
            this.funkyButton3.Size = new System.Drawing.Size(150, 150);
            this.funkyButton3.TabIndex = 0;
            // 
            // funkyButton4
            // 
            this.funkyButton4.BackColor = System.Drawing.Color.Blue;
            this.funkyButton4.Location = new System.Drawing.Point(406, 48);
            this.funkyButton4.Name = "funkyButton4";
            this.funkyButton4.Points.AddRange(new System.Drawing.Point[] {
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(150, 0),
            new System.Drawing.Point(150, 150),
            new System.Drawing.Point(0, 150)});
            this.funkyButton4.Size = new System.Drawing.Size(150, 150);
            this.funkyButton4.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(632, 309);
            this.Controls.Add(this.funkyButton4);
            this.Controls.Add(this.funkyButton3);
            this.Name = "Form1";
            this.Text = "Control Test";
            ((System.ComponentModel.ISupportInitialize)(this.funkyButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.funkyButton4)).EndInit();
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
