using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace LunaticsInc
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		private Bitmap btmLunaticsInc;
		
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            btmLunaticsInc = new Bitmap(@"c:\Users\alessandro.augusto\Documents\Visual Studio 2008\Exemplos_Internet\# teste #\Transparent2\LunaticsInc.png");

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.DoubleBuffer,
				true);

			button1.Hide();
			button2.Hide();

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
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Silver;
			this.button1.Location = new System.Drawing.Point(64, 368);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Do it again";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Silver;
			this.button2.Location = new System.Drawing.Point(176, 368);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 24);
			this.button2.TabIndex = 1;
			this.button2.Text = "Quit";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Blue;
			this.ClientSize = new System.Drawing.Size(328, 400);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button2,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TransparencyKey = System.Drawing.Color.Blue;
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
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

		private float size = 20.0f;
		private Rectangle rect = new Rectangle(0,0,326,349);
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
				e.Graphics.DrawImage(btmLunaticsInc,
					rect,
					this.Left - this.Width/2 - (size*btmLunaticsInc.Width)/2, this.Top  - (size*btmLunaticsInc.Height)/2, size*btmLunaticsInc.Width, size*btmLunaticsInc.Height,
					GraphicsUnit.Pixel);		
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{	
			size -= 0.1f;
			if(size <= 1.15f)
			{
				timer1.Enabled = false;
				button1.Show();
				button2.Show();
			}
			Invalidate();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			size = 20.0f;
			timer1.Enabled = true;
			button1.Hide();
			button2.Hide();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Application.Exit();		
		}

		// Starting minimized is hacking crap done to get rid of the initial
		// flashing of garbage when the program first starts. We still see a
		// bit of a title bar zipping around, odd since this form border style is none.
		// ANYBODY KNOW HOW TO DO THIS CORRECTLY?
		private void Form1_Load(object sender, System.EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
		}




	}
}
