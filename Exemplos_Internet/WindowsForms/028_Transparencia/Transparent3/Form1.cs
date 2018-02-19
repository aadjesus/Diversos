using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Opacity
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonAgain;
		private System.Windows.Forms.Button buttonQuit;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			SetStyle(ControlStyles.AllPaintingInWmPaint | 
				ControlStyles.UserPaint | 
				ControlStyles.DoubleBuffer,
				true);

			buttonAgain.Hide();
			buttonQuit.Hide();

			this.Opacity = 0;
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
			this.components = new System.ComponentModel.Container();
			this.buttonAgain = new System.Windows.Forms.Button();
			this.buttonQuit = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// buttonAgain
			// 
			this.buttonAgain.BackColor = System.Drawing.Color.Transparent;
			this.buttonAgain.ForeColor = System.Drawing.Color.White;
			this.buttonAgain.Location = new System.Drawing.Point(75, 144);
			this.buttonAgain.Name = "buttonAgain";
			this.buttonAgain.Size = new System.Drawing.Size(48, 18);
			this.buttonAgain.TabIndex = 0;
			this.buttonAgain.Text = "Again?";
			this.buttonAgain.Visible = false;
			this.buttonAgain.Click += new System.EventHandler(this.buttonAgain_Click);
			// 
			// buttonQuit
			// 
			this.buttonQuit.BackColor = System.Drawing.Color.Transparent;
			this.buttonQuit.ForeColor = System.Drawing.Color.White;
			this.buttonQuit.Location = new System.Drawing.Point(129, 144);
			this.buttonQuit.Name = "buttonQuit";
			this.buttonQuit.Size = new System.Drawing.Size(46, 18);
			this.buttonQuit.TabIndex = 1;
			this.buttonQuit.Text = "Quit?";
			this.buttonQuit.Visible = false;
			this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 25;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Blue;
			this.ClientSize = new System.Drawing.Size(250, 194);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonQuit,
																		  this.buttonAgain});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.TransparencyKey = System.Drawing.Color.Blue;
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

		private float opacity = 0;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			opacity += 0.03f;
			this.Opacity = opacity;
			if(opacity > 1.0)
			{
				timer1.Enabled = false;
				buttonAgain.Show();
				buttonQuit.Show();
			}
			Invalidate();

		}

		private void buttonAgain_Click(object sender, System.EventArgs e)
		{
			opacity = 0;
			buttonAgain.Hide();
			buttonQuit.Hide();
			timer1.Enabled = true;	
		}

		private void buttonQuit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private Bitmap btmBurl = new Bitmap("BurlWoodTech.jpg");
		private Rectangle rect = new Rectangle(0,0,250,194);
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{		
			e.Graphics.DrawImage(btmBurl,
				rect,
				0,0, btmBurl.Width, btmBurl.Height,
				GraphicsUnit.Pixel);	
		}
		// Drag it around the screen
		private const int WM_NCHITTEST = 0x84;
		private const int HTCAPTION = 0x2;
		protected override void WndProc(ref Message m)
		{
			if(m.Msg == WM_NCHITTEST)
				m.Result = new IntPtr(HTCAPTION);
			else
				base.WndProc(ref m);
		}

	}
}
