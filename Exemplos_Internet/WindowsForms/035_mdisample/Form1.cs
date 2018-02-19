using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace mdisample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;

		int count=0;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItemAcs;
		private System.Windows.Forms.MenuItem menuItemHoriz;
		private System.Windows.Forms.MenuItem menuItemVert;
		private System.Windows.Forms.MenuItem menuItemAI;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemNew;
		private System.Windows.Forms.MenuItem menuItemClose;
		private System.Windows.Forms.MenuItem menuItemWindow;
		private System.Windows.Forms.MenuItem menuItemMax;
		private System.Windows.Forms.MenuItem menuItemMin;
		private System.Windows.Forms.OpenFileDialog oFileDlg;
		private System.Windows.Forms.MenuItem menuItem1;

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemNew = new System.Windows.Forms.MenuItem();
			this.menuItemClose = new System.Windows.Forms.MenuItem();
			this.menuItemWindow = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemAI = new System.Windows.Forms.MenuItem();
			this.menuItemAcs = new System.Windows.Forms.MenuItem();
			this.menuItemHoriz = new System.Windows.Forms.MenuItem();
			this.menuItemVert = new System.Windows.Forms.MenuItem();
			this.menuItemMax = new System.Windows.Forms.MenuItem();
			this.menuItemMin = new System.Windows.Forms.MenuItem();
			this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemFile,
																					  this.menuItemWindow,
																					  this.menuItem5});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItemNew,
																						 this.menuItem1,
																						 this.menuItemClose});
			this.menuItemFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItemFile.Text = "&File";
			// 
			// menuItemNew
			// 
			this.menuItemNew.Index = 0;
			this.menuItemNew.Text = "&New";
			this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
			// 
			// menuItemClose
			// 
			this.menuItemClose.Index = 2;
			this.menuItemClose.MergeOrder = 5;
			this.menuItemClose.Text = "Cl&ose";
			this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
			// 
			// menuItemWindow
			// 
			this.menuItemWindow.Index = 1;
			this.menuItemWindow.MdiList = true;
			this.menuItemWindow.Text = "&Window";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemAI,
																					  this.menuItemAcs,
																					  this.menuItemHoriz,
																					  this.menuItemVert,
																					  this.menuItemMax,
																					  this.menuItemMin});
			this.menuItem5.Text = "&Layout";
			// 
			// menuItemAI
			// 
			this.menuItemAI.Index = 0;
			this.menuItemAI.Text = "Arrange&Icons";
			this.menuItemAI.Click += new System.EventHandler(this.menuItemAI_Click);
			// 
			// menuItemAcs
			// 
			this.menuItemAcs.Index = 1;
			this.menuItemAcs.Text = "&Cascade";
			this.menuItemAcs.Click += new System.EventHandler(this.menuItemCas_Click);
			// 
			// menuItemHoriz
			// 
			this.menuItemHoriz.Index = 2;
			this.menuItemHoriz.Text = "Arrange &Horizontal";
			this.menuItemHoriz.Click += new System.EventHandler(this.menuItemHoriz_Click);
			// 
			// menuItemVert
			// 
			this.menuItemVert.Index = 3;
			this.menuItemVert.Text = "Arrange &Vertical";
			this.menuItemVert.Click += new System.EventHandler(this.menuItemVert_Click);
			// 
			// menuItemMax
			// 
			this.menuItemMax.Index = 4;
			this.menuItemMax.Text = "Ma&ximize all";
			this.menuItemMax.Click += new System.EventHandler(this.menuItemMax_Click);
			// 
			// menuItemMin
			// 
			this.menuItemMin.Index = 5;
			this.menuItemMin.Text = "Mi&nimize all";
			this.menuItemMin.Click += new System.EventHandler(this.menuItemMin_Click);
			// 
			// oFileDlg
			// 
			this.oFileDlg.AddExtension = false;
			this.oFileDlg.Title = "MDI Sample";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MergeOrder = 4;
			this.menuItem1.Text = "Close All";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 401);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MDI Sample";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            
            Application.Run(form12);
		}
        static Form1 form12 = new Form1();
		private void menuItemNew_Click(object sender, System.EventArgs e)
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
					//Create a new instance of the MDI child template form
					Form2 chForm = new Form2();
					//set parent form for the child window
                    chForm.MdiParent = form12;
			
					//increment the child form count
					count ++;
					//set the title of the child window.
					chForm.Text= "Child - " + count.ToString();
					
					chForm.fileloc=oFileDlg.FileName;	
					
					//display the child window
					chForm.Show();					
				}
				catch
				{
					MessageBox.Show("Error opening image","MDI Sample",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}

		private void menuItemClose_Click(object sender, System.EventArgs e)
		{
			//Gets the currently active MDI child window.
			Form a = this.ActiveMdiChild;
			//Close the MDI child window
			a.Close();
		}		

		private void menuItemAI_Click(object sender, System.EventArgs e)
		{
			//Arrange MDI child icons within the client region of the MDI parent form.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
		}

		private void menuItemCas_Click(object sender, System.EventArgs e)
		{
			//Cascade all child forms.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
		}

		private void menuItemHoriz_Click(object sender, System.EventArgs e)
		{
			//Tile all child forms horizontally.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
		}

		private void menuItemVert_Click(object sender, System.EventArgs e)
		{			
			//Tile all child forms vertically.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
		}

		private void menuItemMax_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Maximized
			foreach (Form chform in charr)
				chform.WindowState=FormWindowState.Maximized;
		}

		private void menuItemMin_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Minimized
			foreach (Form chform in charr)
				chform.WindowState=FormWindowState.Minimized;
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Minimized
			foreach (Form chform in charr)
				chform.Close();
		}

			
	}
}
