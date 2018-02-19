using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

  

namespace AutomobileFades
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.Timer timer1;

		private System.Windows.Forms.Button button1;

        private Bitmap btmInner = new Bitmap(@"c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Internet\WindowsForms\028_Transparencia\Transparent1\Inner.png");
        private Bitmap btmmiddle = new Bitmap(@"c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Internet\WindowsForms\028_Transparencia\Transparent1\middle.png");
        private Bitmap btmouter = new Bitmap(@"c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Internet\WindowsForms\028_Transparencia\Transparent1\outer.png");

		private bool removemiddle = false;
		private bool removeouter = false;

		private static float masterTransparency = 1.0f;
		private static float middleTransparency = 1.0f;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private static float outerTransparency = 1.0f;



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
			this.button1 = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(440, 224);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(144, 24);
			this.button1.TabIndex = 5;
			this.button1.Text = "Remove outer Layer";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// timer1
			// 
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(384, 256);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 40);
			this.label1.TabIndex = 6;
			this.label1.Text = "All images are copyrighted by Kevin Hulsey who generously allowed them to be used" +
				" for this demo.";
			// 
			// linkLabel1
			// 
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 22);
			this.linkLabel1.Location = new System.Drawing.Point(456, 280);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(128, 16);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.khulsey.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(602, 302);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.linkLabel1,
																		  this.label1,
																		  this.button1});
			this.Name = "Form1";
			this.Text = " Automobile Fades";
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


		private int Next = 0;
		private void button1_Click(object sender, System.EventArgs e)
		{
			switch(Next)       
			{         
				case 0: 
					removeouter = true;
					middleTransparency = 1.0f;
					removemiddle = false;
					timer1.Enabled = true;
					button1.Text = "Remove middle Layer";
					break;
				case 1:
					removeouter = false;
					removemiddle = true;
					outerTransparency = 0.0f;	
					timer1.Enabled = true;
					button1.Text = "Restore Layers";
					break;           
				case 2: 
					removeouter = false;
					removemiddle = false;
					outerTransparency = 1.0f;	
					middleTransparency = 1.0f;
					button1.Text = "Remove outer Layer";
					Refresh();
					break;                  
				default:  
					timer1.Enabled = false;
					break;      
			}

			if(Next < 2)Next++;
			else Next = 0;
		
		}

		private float Count = 1.0f;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			masterTransparency = Count;
			if(Count > 0.05) Count -= 0.05f;
			else
			{
				Count = 1.0f;
				timer1.Enabled = false;
				masterTransparency = 0.0f;
			}
	
			if(removeouter)
			{
				outerTransparency = masterTransparency;	
			}			
			
			if(removemiddle)
			{
				middleTransparency = masterTransparency;
			}

			Invalidate();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// Change the color of the link text by setting LinkVisited 
			// to True.
			linkLabel1.LinkVisited = true;
   
			// Call the Process.Start method to open the default browser 
			// with a URL:
			System.Diagnostics.Process.Start("http://www.khulsey.com");
		
		}

		// Create a rectangle for drawing the image
		private Rectangle rect = new Rectangle(0,0,600,300);
		// Create the ImageAttributes objects
		ImageAttributes middleImageAttributes = new ImageAttributes();
		ImageAttributes outerImageAttributes = new ImageAttributes();
		// Create a ColorMatrix objects
		ColorMatrix middleColorMatrix = new ColorMatrix();
		ColorMatrix outerColorMatrix = new ColorMatrix();
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			// Draw the base image
			e.Graphics.DrawImage(btmInner,
				rect,
				0,0, 600, 300,
				GraphicsUnit.Pixel);

			//Set alpha in the ColorMatrix
			middleColorMatrix.Matrix33 = middleTransparency;

			// Set color matrix of imageAttributes
			middleImageAttributes.SetColorMatrix(middleColorMatrix,
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

			// Draw the middle image
			e.Graphics.DrawImage(btmmiddle,
				rect,
				0,0, 600, 300,
				GraphicsUnit.Pixel, middleImageAttributes);


			//Set alpha in the ColorMatrix
			outerColorMatrix.Matrix33 = outerTransparency;

			// Set color matrix of imageAttributes
			outerImageAttributes.SetColorMatrix(outerColorMatrix,
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

			// Draw the outer image
			e.Graphics.DrawImage(btmouter,
				rect,
				0,0, 600, 300,
				GraphicsUnit.Pixel, outerImageAttributes);

		}

	}
}
