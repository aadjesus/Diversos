using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample16.
	/// </summary>
	public class frmSample16 : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkArrows;
		private System.Windows.Forms.CheckBox chkTab;
		private System.Windows.Forms.CheckBox chkEnter;
		private System.Windows.Forms.CheckBox chkEscape;
		private System.Windows.Forms.CheckBox chkRemoveFocus;
		private System.Windows.Forms.CheckBox chkRemoveSelection;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample16()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkRemoveSelection = new System.Windows.Forms.CheckBox();
			this.chkRemoveFocus = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkEscape = new System.Windows.Forms.CheckBox();
			this.chkEnter = new System.Windows.Forms.CheckBox();
			this.chkTab = new System.Windows.Forms.CheckBox();
			this.chkArrows = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// grid1
			// 
			this.grid1.AutoSizeMinHeight = 10;
			this.grid1.AutoSizeMinWidth = 10;
			this.grid1.AutoStretchColumnsToFitWidth = false;
			this.grid1.AutoStretchRowsToFitHeight = false;
			this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid1.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
			this.grid1.CustomSort = false;
			this.grid1.FocusStyle = SourceGrid2.FocusStyle.None;
			this.grid1.GridToolTipActive = true;
			this.grid1.Location = new System.Drawing.Point(184, 40);
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(280, 192);
			this.grid1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkRemoveSelection);
			this.groupBox1.Controls.Add(this.chkRemoveFocus);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(160, 112);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "FocusStyle";
			// 
			// chkRemoveSelection
			// 
			this.chkRemoveSelection.Location = new System.Drawing.Point(8, 64);
			this.chkRemoveSelection.Name = "chkRemoveSelection";
			this.chkRemoveSelection.Size = new System.Drawing.Size(136, 40);
			this.chkRemoveSelection.TabIndex = 1;
			this.chkRemoveSelection.Text = "Remove Selection on Leave";
			this.chkRemoveSelection.CheckedChanged += new System.EventHandler(this.chkRemoveSelection_CheckedChanged);
			// 
			// chkRemoveFocus
			// 
			this.chkRemoveFocus.Location = new System.Drawing.Point(8, 16);
			this.chkRemoveFocus.Name = "chkRemoveFocus";
			this.chkRemoveFocus.Size = new System.Drawing.Size(136, 48);
			this.chkRemoveFocus.TabIndex = 0;
			this.chkRemoveFocus.Text = "Remove Focus Cell on Leave";
			this.chkRemoveFocus.CheckedChanged += new System.EventHandler(this.chkRemoveFocus_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkEscape);
			this.groupBox2.Controls.Add(this.chkEnter);
			this.groupBox2.Controls.Add(this.chkTab);
			this.groupBox2.Controls.Add(this.chkArrows);
			this.groupBox2.Location = new System.Drawing.Point(8, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(160, 112);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "SpecialKeys";
			// 
			// chkEscape
			// 
			this.chkEscape.Checked = true;
			this.chkEscape.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEscape.Location = new System.Drawing.Point(8, 88);
			this.chkEscape.Name = "chkEscape";
			this.chkEscape.Size = new System.Drawing.Size(136, 16);
			this.chkEscape.TabIndex = 3;
			this.chkEscape.Text = "Escape";
			this.chkEscape.CheckedChanged += new System.EventHandler(this.chkEscape_CheckedChanged);
			// 
			// chkEnter
			// 
			this.chkEnter.Checked = true;
			this.chkEnter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEnter.Location = new System.Drawing.Point(8, 64);
			this.chkEnter.Name = "chkEnter";
			this.chkEnter.Size = new System.Drawing.Size(136, 16);
			this.chkEnter.TabIndex = 2;
			this.chkEnter.Text = "Enter";
			this.chkEnter.CheckedChanged += new System.EventHandler(this.chkEnter_CheckedChanged);
			// 
			// chkTab
			// 
			this.chkTab.Checked = true;
			this.chkTab.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkTab.Location = new System.Drawing.Point(8, 40);
			this.chkTab.Name = "chkTab";
			this.chkTab.Size = new System.Drawing.Size(136, 16);
			this.chkTab.TabIndex = 1;
			this.chkTab.Text = "Tab";
			this.chkTab.CheckedChanged += new System.EventHandler(this.chkTab_CheckedChanged);
			// 
			// chkArrows
			// 
			this.chkArrows.Checked = true;
			this.chkArrows.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkArrows.Location = new System.Drawing.Point(8, 16);
			this.chkArrows.Name = "chkArrows";
			this.chkArrows.Size = new System.Drawing.Size(136, 16);
			this.chkArrows.TabIndex = 0;
			this.chkArrows.Text = "Arrows";
			this.chkArrows.CheckedChanged += new System.EventHandler(this.chkArrows_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(184, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 3;
			this.label1.Text = "Trial TextBox";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(296, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(152, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "CIAO";
			// 
			// frmSample16
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 243);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grid1);
			this.Name = "frmSample16";
			this.Text = "SpecialKeys and FocusStyle";
			this.Load += new System.EventHandler(this.frmSample16_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSample16_Load(object sender, System.EventArgs e)
		{
			grid1.Redim(10,10);
			for (int r = 0; r < grid1.RowsCount; r++)
				for (int c = 0; c < grid1.ColumnsCount; c++)
					grid1[r,c] = new SourceGrid2.Cells.Real.Cell("Ciao", typeof(string));
		}

		private void chkRemoveFocus_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshGridStyles();
		}

		private void chkRemoveSelection_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshGridStyles();
		}

		private void chkArrows_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshGridStyles();
		}

		private void chkTab_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshGridStyles();
		}

		private void chkEnter_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshGridStyles();
		}

		private void chkEscape_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshGridStyles();
		}

		private void RefreshGridStyles()
		{
			SourceGrid2.GridSpecialKeys l_Keys = SourceGrid2.GridSpecialKeys.None;
			if (chkArrows.Checked)
				l_Keys |= SourceGrid2.GridSpecialKeys.Arrows;
			if (chkEnter.Checked)
				l_Keys |= SourceGrid2.GridSpecialKeys.Enter;
			if (chkEscape.Checked)
				l_Keys |= SourceGrid2.GridSpecialKeys.Escape;
			if (chkTab.Checked)
				l_Keys |= SourceGrid2.GridSpecialKeys.Tab;

			SourceGrid2.FocusStyle l_Focus = SourceGrid2.FocusStyle.None;
			if (chkRemoveFocus.Checked)
				l_Focus |= SourceGrid2.FocusStyle.RemoveFocusCellOnLeave;
			if (chkRemoveSelection.Checked)
				l_Focus |= SourceGrid2.FocusStyle.RemoveSelectionOnLeave;

			grid1.FocusStyle = l_Focus;
			grid1.SpecialKeys = l_Keys;
		}
	}
}
