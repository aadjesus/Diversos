using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace mdisample
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemCFile;
		private System.Windows.Forms.MenuItem menuItemCopen;
		private System.Windows.Forms.MenuItem menuItemCSave;
		private System.Windows.Forms.OpenFileDialog oFileDlg;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.SaveFileDialog sFileDlg;
		private System.Windows.Forms.MenuItem menuItemWM;

		public string fileloc;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
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
				if(components != null)
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemCFile = new System.Windows.Forms.MenuItem();
			this.menuItemCopen = new System.Windows.Forms.MenuItem();
			this.menuItemWM = new System.Windows.Forms.MenuItem();
			this.menuItemCSave = new System.Windows.Forms.MenuItem();
			this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.sFileDlg = new System.Windows.Forms.SaveFileDialog();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemCFile});
			// 
			// menuItemCFile
			// 
			this.menuItemCFile.Index = 0;
			this.menuItemCFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.menuItemCopen,
																						  this.menuItemWM,
																						  this.menuItemCSave});
			this.menuItemCFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItemCFile.Text = "&File";
			// 
			// menuItemCopen
			// 
			this.menuItemCopen.Index = 0;
			this.menuItemCopen.MergeOrder = 1;
			this.menuItemCopen.Text = "&Open";
			this.menuItemCopen.Click += new System.EventHandler(this.menuItemCopen_Click);
			// 
			// menuItemWM
			// 
			this.menuItemWM.Index = 1;
			this.menuItemWM.MergeOrder = 2;
			this.menuItemWM.Text = "&Watermark";
			this.menuItemWM.Click += new System.EventHandler(this.menuItemWM_Click);
			// 
			// menuItemCSave
			// 
			this.menuItemCSave.Index = 2;
			this.menuItemCSave.MergeOrder = 3;
			this.menuItemCSave.Text = "&Save";
			this.menuItemCSave.Click += new System.EventHandler(this.menuItemCSave_Click);
			// 
			// oFileDlg
			// 
			this.oFileDlg.AddExtension = false;
			this.oFileDlg.Title = "MDI Sample";
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.pictureBox1});
			this.panel1.Location = new System.Drawing.Point(8, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(280, 264);
			this.panel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(16, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(248, 224);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// sFileDlg
			// 
			this.sFileDlg.FileName = "doc1";
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Menu = this.mainMenu1;
			this.Name = "Form2";
			this.Text = "Child";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.Form2_Resize);
			this.Load += new System.EventHandler(this.Form2_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItemCopen_Click(object sender, System.EventArgs e)
		{
			oFileDlg.CheckFileExists=true;
			oFileDlg.CheckPathExists=true;
			oFileDlg.Title="Open File - MDI Sample";
			oFileDlg.ValidateNames=true;
			oFileDlg.Filter = "jpg files (*.jpg)|*.jpg";

			if (oFileDlg.ShowDialog() == DialogResult.OK)
			{	
				try
				{
					string fileloc=oFileDlg.FileName;	
					//load image to picturebox
					pictureBox1.Image = Image.FromFile(fileloc);		
				}
				catch
				{
					MessageBox.Show("Error opening image","MDI Sample",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
			
		}

		private void menuItemCSave_Click(object sender, System.EventArgs e)
		{
			if (pictureBox1.Image == null)
				return;

			sFileDlg.CheckPathExists=true;
			sFileDlg.ValidateNames = true;
			sFileDlg.Filter= "jpg files (*.jpg)|*.jpg";
			sFileDlg.Title="Save File - MDI Sample";
			if (sFileDlg.ShowDialog() == DialogResult.OK)
			{	
				try
				{
					//Create Bitmap
					Bitmap bimg= (Bitmap) this.pictureBox1.Image;
					//Save Bitmap to file
					bimg.Save(sFileDlg.FileName);						
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message,"MDI Sample", MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}

		private void Form2_Resize(object sender, System.EventArgs e)
		{
			//Resize the panel to fit in the form 
			//when the form is maximised or minimised
			panel1.Width=this.Width-20;
			panel1.Height=this.Height-40;
		}

		private void menuItemWM_Click(object sender, System.EventArgs e)
		{
			if (pictureBox1.Image != null)
			{
				// Create image.
				Image tmp = pictureBox1.Image;
				// Create graphics object for alteration.
				Graphics g = Graphics.FromImage(tmp);

				// Create string to draw.
				String wmString = "Code Project";
				// Create font and brush.
				Font wmFont = new Font("Trebuchet MS", 10);
				SolidBrush wmBrush = new SolidBrush(Color.Black);
				// Create point for upper-left corner of drawing.
				PointF wmPoint = new PointF(10.0F, 10.0F);
				// Draw string to image.
				g.DrawString(wmString, wmFont, wmBrush, wmPoint);	
				//Load the new image to picturebox		
				pictureBox1.Image= tmp;
				// Release graphics object.
				g.Dispose();				
			}
		}

		private void Form2_Load(object sender, System.EventArgs e)
		{
			try
			{				
				//load image to picturebox
				pictureBox1.Image = Image.FromFile(fileloc);		
			}
			catch
			{
				MessageBox.Show("Error opening image","MDI Sample",MessageBoxButtons.OK,MessageBoxIcon.Error);				
			}			
		}
	}
}
