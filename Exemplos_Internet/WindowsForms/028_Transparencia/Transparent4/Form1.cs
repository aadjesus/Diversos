using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using PerPixelAlphaBlend;

namespace PerPixelAlphaBlendTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

		private MyPerPixelAlphaForm TestForm;
		private Bitmap bitmap;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		public Form1() 
		{
			InitializeComponent();
		}

		///<para>Constructs and initializes all child controls of this dialog box.</para>
		private void InitializeComponent() 
		{
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(197, 175);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}

		
		///<para>Frees our bitmap.</para>
		protected override void Dispose(bool disposing) 
		{
			this.TestForm.Dispose();

			try 
			{
				if (disposing && bitmap != null) 
				{
					bitmap.Dispose();
					bitmap = null;
				}
			} 
			finally 
			{
				base.Dispose(disposing);
			}
		}


		private void Form1_Load(object sender, System.EventArgs e)
		{
			// TestForm will contain the per-pixel-alpha dib
			TestForm = new MyPerPixelAlphaForm();
			TestForm.Width = 192;
			TestForm.Height = 396;
			TestForm.Show();
			Bitmap btm = (Bitmap)Bitmap.FromFile(@"c:\Users\alessandro.augusto\Documents\Visual Studio 2008\Exemplos_Internet\# teste #\Transparent4\AquaMala.png");
			TestForm.SetBitmap(btm,255);
		
		}


	}
}


