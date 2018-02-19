using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Real;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSampleGridContextMenu.
	/// </summary>
	public class frmSample10 : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid1;
		private System.Windows.Forms.RadioButton rdDefaultContextMenu;
		private System.Windows.Forms.RadioButton rdCustomContextMenu;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample10()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.grid1 = new SourceGrid2.Grid();
			this.rdDefaultContextMenu = new System.Windows.Forms.RadioButton();
			this.rdCustomContextMenu = new System.Windows.Forms.RadioButton();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grid1
			// 
			this.grid1.AutoSizeMinHeight = 10;
			this.grid1.AutoSizeMinWidth = 10;
			this.grid1.AutoStretchColumnsToFitWidth = false;
			this.grid1.AutoStretchRowsToFitHeight = false;
			this.grid1.BackColor = System.Drawing.Color.White;
			this.grid1.ContextMenuStyle = SourceGrid2.ContextMenuStyle.CellContextMenu;
			this.grid1.CustomSort = false;
			this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid1.GridToolTipActive = true;
			this.grid1.Location = new System.Drawing.Point(0, 0);
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(554, 98);
			this.grid1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid1.TabIndex = 0;
			// 
			// rdDefaultContextMenu
			// 
			this.rdDefaultContextMenu.Checked = true;
			this.rdDefaultContextMenu.Location = new System.Drawing.Point(12, 8);
			this.rdDefaultContextMenu.Name = "rdDefaultContextMenu";
			this.rdDefaultContextMenu.Size = new System.Drawing.Size(160, 24);
			this.rdDefaultContextMenu.TabIndex = 1;
			this.rdDefaultContextMenu.TabStop = true;
			this.rdDefaultContextMenu.Text = "Grid Context Menu";
			this.rdDefaultContextMenu.CheckedChanged += new System.EventHandler(this.rdDefaultContextMenu_CheckedChanged);
			// 
			// rdCustomContextMenu
			// 
			this.rdCustomContextMenu.Location = new System.Drawing.Point(12, 32);
			this.rdCustomContextMenu.Name = "rdCustomContextMenu";
			this.rdCustomContextMenu.Size = new System.Drawing.Size(160, 24);
			this.rdCustomContextMenu.TabIndex = 2;
			this.rdCustomContextMenu.Text = "Custom Context Menu";
			this.rdCustomContextMenu.CheckedChanged += new System.EventHandler(this.rdCustomContextMenu_CheckedChanged);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Custom Context Menu 1";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Custom Context Menu 2";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.grid1);
			this.panel1.Location = new System.Drawing.Point(4, 72);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(556, 100);
			this.panel1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(180, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(376, 60);
			this.label1.TabIndex = 5;
			this.label1.Text = "You can customize the grid ContextMenu using grid.Selection.ContextMenuItems or C" +
				"ell.ContextMenuItems. Use SourceLibrary.Windows.Forms.MenuItemImage to insert im" +
				"age in a menu item. Right Click on the grid below to try the ContextMenu.";
			// 
			// frmSample10
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(564, 175);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.rdCustomContextMenu);
			this.Controls.Add(this.rdDefaultContextMenu);
			this.Name = "frmSample10";
			this.Text = "ContextMenu";
			this.Load += new System.EventHandler(this.frmSampleGridContextMenu_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void rdDefaultContextMenu_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshContextMenu();
		}

		private void rdCustomContextMenu_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshContextMenu();
		}

		private void rdMixedMode_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshContextMenu();
		}

		private void RefreshContextMenu()
		{
			if (rdDefaultContextMenu.Checked)
			{
				grid1.ContextMenuStyle = (SourceGrid2.ContextMenuStyle)grid1[0,1].Value;
				grid1[1,1].Value = "Righ-Click On Me";
			}
			else if (rdCustomContextMenu.Checked)
			{
				grid1.ContextMenuStyle = 0;
				grid1.ContextMenu = contextMenu1;
				grid1[1,1].Value = "Righ-Click On Me (Disabled)";
			}
			else //mixed mode
			{
				grid1[1,1].Value = "Righ-Click On Me";
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(this,"Custom Context Menu");
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(this,"Custom Context Menu");
		}

		private void frmSampleGridContextMenu_Load(object sender, System.EventArgs e)
		{
			grid1.Redim(2,2);
			grid1.AutoStretchColumnsToFitWidth = true;

			Cells.Cell l_Cell1 = new Cells.Cell("ContextMenuStyle:");
			grid1[0,0] = l_Cell1;
			l_Cell1.BackColor = Color.LightGray;
			
			Cells.Cell l_Cell2 = new Cells.Cell(grid1.ContextMenuStyle, typeof(SourceGrid2.ContextMenuStyle));
			grid1[0,1] = l_Cell2;
			l_Cell2.Behaviors.Add(new SourceGrid2.BehaviorModels.BindProperty(typeof(SourceGrid2.Grid).GetProperty("ContextMenuStyle"), grid1));

			Cells.Cell l_Cell3 = new Cells.Cell("Cell ContextMenu:");
			grid1[1,0] = l_Cell3;
			l_Cell3.BackColor = Color.LightGray;
			Cells.Cell l_Cell4 = new Cells.Cell("Right-Click on Me");
			grid1[1,1] = l_Cell4;
			l_Cell4.ContextMenuItems.Add(new System.Windows.Forms.MenuItem("Cell[1,1] Context Menu 1"));
			l_Cell4.ContextMenuItems.Add(new System.Windows.Forms.MenuItem("Cell[1,1] Context Menu 2"));

			grid1.Selection.ContextMenuItems = new SourceGrid2.MenuCollection();
			grid1.Selection.ContextMenuItems.Add(new System.Windows.Forms.MenuItem("Selection Context Menu 1") );
			grid1.Selection.ContextMenuItems.Add(new System.Windows.Forms.MenuItem("Selection Context Menu 2") );

			grid1.AutoSizeAll();
		}
	}
}
