using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using System.Data;
using nBASS;

namespace CDTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private CD cd;
		private int track = 1; 

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			cd = new CD( CDSetupFlags.LeaveVolume );
			this.propertyGrid1.SelectedObject = cd;
			cd.Progress += new BASSProgessHandler( Progress);
		}

		private void Progress(ChannelBase cdchannel)
		{
			CD cd2 = (CD) cdchannel;
			this.propertyGrid1.Refresh();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			cd.Stop(); //the known problem
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Open";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(8, 40);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "Close";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Location = new System.Drawing.Point(88, 40);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "Previous";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Location = new System.Drawing.Point(168, 40);
			this.button4.Name = "button4";
			this.button4.TabIndex = 3;
			this.button4.Text = "Play";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button5.Location = new System.Drawing.Point(248, 40);
			this.button5.Name = "button5";
			this.button5.TabIndex = 4;
			this.button5.Text = "Next";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.Location = new System.Drawing.Point(248, 8);
			this.button6.Name = "button6";
			this.button6.TabIndex = 5;
			this.button6.Text = "Resume";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button7.Location = new System.Drawing.Point(168, 8);
			this.button7.Name = "button7";
			this.button7.TabIndex = 6;
			this.button7.Text = "Pause";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button8.Location = new System.Drawing.Point(88, 8);
			this.button8.Name = "button8";
			this.button8.TabIndex = 7;
			this.button8.Text = "Stop";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(8, 72);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(312, 256);
			this.propertyGrid1.TabIndex = 8;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ToolbarVisible = false;
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(328, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.propertyGrid1,
																							 this.button8,
																							 this.button7,
																							 this.button6,
																							 this.button5,
																							 this.button4,
																							 this.button3,
																							 this.button2,
																							 this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "CD Test";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			cd.Door(true);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			cd.Door(false);
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			cd.Stop();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (track <= 1) track = cd.Tracks + 1;
			cd.Play( --track, false, false);
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if (track >= cd.Tracks) track = 0;
			cd.Play( ++track, false, false);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			cd.Play( track, false, true);
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			cd.Pause();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			cd.Resume();
		}
	}
}
