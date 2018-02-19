using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using EnvDTE;

namespace AddItemsToVSToolbar
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class AddRemoveItems : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.OpenFileDialog fdSelectComponent;
		private System.Windows.Forms.Button Browse;
		private System.Windows.Forms.TextBox txtComponentPath;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.TextBox txtTabName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;

		private ToolBoxTabs tlbTabs;
		private ToolBoxTab tlbTab;
		private Window ToolBoxWnd;
		private EnvDTE.DTE dteObject;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddRemoveItems()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AddRemoveItems));
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.fdSelectComponent = new System.Windows.Forms.OpenFileDialog();
			this.txtComponentPath = new System.Windows.Forms.TextBox();
			this.Browse = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.txtTabName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(278, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select Custom Control/Component to Add to ToolBox :";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.PeachPuff;
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.txtTabName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btnRemove);
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Controls.Add(this.Browse);
			this.groupBox1.Controls.Add(this.txtComponentPath);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 176);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Add/Remove Items";
			// 
			// fdSelectComponent
			// 
			this.fdSelectComponent.DefaultExt = "dll";
			this.fdSelectComponent.Filter = ".Net Framework Components|*.dll";
			this.fdSelectComponent.Title = "Select a .Net Component/Control to Load";
			// 
			// txtComponentPath
			// 
			this.txtComponentPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtComponentPath.Location = new System.Drawing.Point(16, 88);
			this.txtComponentPath.Name = "txtComponentPath";
			this.txtComponentPath.ReadOnly = true;
			this.txtComponentPath.Size = new System.Drawing.Size(264, 20);
			this.txtComponentPath.TabIndex = 1;
			this.txtComponentPath.Text = "";
			// 
			// Browse
			// 
			this.Browse.BackColor = System.Drawing.Color.SandyBrown;
			this.Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Browse.Location = new System.Drawing.Point(288, 88);
			this.Browse.Name = "Browse";
			this.Browse.Size = new System.Drawing.Size(64, 20);
			this.Browse.TabIndex = 2;
			this.Browse.Text = "Browse...";
			this.Browse.Click += new System.EventHandler(this.Browse_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.BackColor = System.Drawing.Color.SandyBrown;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAdd.Location = new System.Drawing.Point(160, 136);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(88, 24);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "Add Item...";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.BackColor = System.Drawing.Color.SandyBrown;
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemove.Location = new System.Drawing.Point(264, 136);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(88, 24);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "Remove Item...";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// txtTabName
			// 
			this.txtTabName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTabName.Location = new System.Drawing.Point(136, 30);
			this.txtTabName.MaxLength = 50;
			this.txtTabName.Name = "txtTabName";
			this.txtTabName.Size = new System.Drawing.Size(216, 20);
			this.txtTabName.TabIndex = 6;
			this.txtTabName.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Enter New Tab Name";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.SandyBrown;
			this.panel1.Location = new System.Drawing.Point(0, 120);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(368, 4);
			this.panel1.TabIndex = 7;
			// 
			// AddRemoveItems
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.PeachPuff;
			this.ClientSize = new System.Drawing.Size(402, 208);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "AddRemoveItems";
			this.Text = "Add/Remove Items....";
			this.Load += new System.EventHandler(this.AddRemoveItems_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new AddRemoveItems());
		}

		private void AddRemoveItems_Load(object sender, System.EventArgs e)
		{
			//EnvDTE class cannot be created using the new keyword, it will throw an error
			//if an instance is created directly.
			//We need to use Reflection to get an instance of the DTE Object.
			System.Type t = System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0");
			object obj = System.Activator.CreateInstance(t, true);
			dteObject = (EnvDTE.DTE)obj;
		}

		private void Browse_Click(object sender, System.EventArgs e)
		{
			fdSelectComponent.ShowDialog();
			txtComponentPath.Text	=	fdSelectComponent.FileName;
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(txtComponentPath.Text	==	"" || txtComponentPath.Text	==	string.Empty)
			{
				MessageBox.Show("Select Component To Add to ToolBox","Add/Remove", MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			else
			{
				//Get the window object corresponding to the Visual Studio ToolBox
				ToolBoxWnd = dteObject.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
				tlbTabs = ((ToolBox)ToolBoxWnd.Object).ToolBoxTabs;
				tlbTab = null;			
				//Check if ToolBarTab Already Exists, if toolbar already exists, delete it,
				//Else recreate it.
				//This section can be changed to the user needs.
				int tabIndex;
				bool tabExists = false;
				for(tabIndex = 1; tabIndex <= tlbTabs.Count; tabIndex++)
				{
					tlbTab = tlbTabs.Item(tabIndex);
					if(tlbTab.Name.Equals(txtTabName.Text))
					{
						MessageBox.Show("Tab " + txtTabName.Text + " Already Exists","Add/Remove", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						tabExists = true;
						break;
					}
				}
				
				if(!tabExists)
				{
					tlbTab = tlbTabs.Add(txtTabName.Text);
					ToolBoxWnd.Visible	=	true;
					ToolBoxWnd.DTE.ExecuteCommand("View.PropertiesWindow","");
					tlbTab.Activate();
					tlbTab.ToolBoxItems.Item(1).Select();
					tlbTab.ToolBoxItems.Add(txtTabName.Text,txtComponentPath.Text,vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
					MessageBox.Show(txtTabName.Text + " Succesfully added to ToolBox.","Add/Remove", MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			int tabIndex;
			bool tabExists = true;
			//Get the window object corresponding to the Visual Studio ToolBox
			ToolBoxWnd = dteObject.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
			tlbTabs = ((ToolBox)ToolBoxWnd.Object).ToolBoxTabs;
			tlbTab = null;	

			//loop through all the tabs and match the tab name entered by the user.
			for(tabIndex = 1; tabIndex <= tlbTabs.Count; tabIndex++)
			{
				tlbTab = tlbTabs.Item(tabIndex);
				if(tlbTab.Name.Equals(txtTabName.Text))
				{
					tlbTab.Delete();
					tabExists = false;
					MessageBox.Show(txtTabName.Text + " Succesfully deleted from ToolBox.","Add/Remove", MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				}
			}
			if(tabExists)
				MessageBox.Show(txtTabName.Text + " Does not Exist in the ToolBox","Add/Remove", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
		}
	}
}
