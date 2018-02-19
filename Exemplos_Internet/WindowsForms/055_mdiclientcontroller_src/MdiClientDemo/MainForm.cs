using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace MdiClientDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private Slusser.Components.MdiClientController mdiClientController;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuAddForm;
		private System.Windows.Forms.MenuItem menuHelp;
		private System.Windows.Forms.MenuItem menuAbout;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Splitter splitter1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


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
			this.mdiClientController = new Slusser.Components.MdiClientController();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuAddForm = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuHelp = new System.Windows.Forms.MenuItem();
			this.menuAbout = new System.Windows.Forms.MenuItem();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.SuspendLayout();
			// 
			// mdiClientController
			// 
			this.mdiClientController.ParentForm = this;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFile,
																					 this.menuHelp});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuAddForm,
																					 this.menuExit});
			this.menuFile.Text = "&File";
			// 
			// menuAddForm
			// 
			this.menuAddForm.Index = 0;
			this.menuAddForm.Text = "&Add Form";
			this.menuAddForm.Click += new System.EventHandler(this.menuAddForm_Click);
			// 
			// menuExit
			// 
			this.menuExit.Index = 1;
			this.menuExit.Text = "E&xit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.Index = 1;
			this.menuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuAbout});
			this.menuHelp.Text = "&Help";
			// 
			// menuAbout
			// 
			this.menuAbout.Index = 0;
			this.menuAbout.Text = "&About";
			this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
			// 
			// propertyGrid
			// 
			this.propertyGrid.CommandsVisibleIfAvailable = true;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
			this.propertyGrid.LargeButtons = false;
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid.Location = new System.Drawing.Point(432, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(208, 345);
			this.propertyGrid.TabIndex = 1;
			this.propertyGrid.Text = "Property Grid";
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(429, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 345);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 345);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.propertyGrid);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "<title>";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}


		#region Constructor

		public MainForm()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

			// Set the PropertyGrid to the MdiClientController component.
			propertyGrid.SelectedObject = mdiClientController;

			// Set the application specific information from the assembly.
			// Make use of the AssemblyInfo helper class.
			AssemblyInfo aInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
			Text = aInfo.Product;
			menuAbout.Text = "&About " + aInfo.Product + "...";
		}

		#endregion // Constructor


		#region File Menu

		private void menuExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuAddForm_Click(object sender, System.EventArgs e)
		{
			Form form = new Form();
			form.MdiParent = this;
			form.Show();
			form.Activate();
		}

		#endregion // File Menu


		#region Help Menu

		private void menuAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);
		}

		#endregion // Help Menu
	}
}
