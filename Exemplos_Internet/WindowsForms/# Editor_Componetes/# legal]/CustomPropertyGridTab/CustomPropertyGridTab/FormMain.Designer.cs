namespace CustomPropertyGridTab
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lvwItems = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.propertySettings = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 2);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(254, 293);
			this.propertyGrid.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lvwItems);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
			this.splitContainer1.Panel2.Controls.Add(this.splitter1);
			this.splitContainer1.Panel2.Controls.Add(this.propertySettings);
			this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 2, 2, 2);
			this.splitContainer1.Size = new System.Drawing.Size(526, 446);
			this.splitContainer1.SplitterDistance = 266;
			this.splitContainer1.TabIndex = 1;
			// 
			// lvwItems
			// 
			this.lvwItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwItems.FormattingEnabled = true;
			this.lvwItems.Location = new System.Drawing.Point(2, 2);
			this.lvwItems.Name = "lvwItems";
			this.lvwItems.Size = new System.Drawing.Size(264, 407);
			this.lvwItems.TabIndex = 0;
			this.lvwItems.SelectedIndexChanged += new System.EventHandler(this.lvwItems_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(2, 412);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 32);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select an object to show its properties which has OzcanPropertyAttribute";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// propertySettings
			// 
			this.propertySettings.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.propertySettings.Location = new System.Drawing.Point(0, 298);
			this.propertySettings.Name = "propertySettings";
			this.propertySettings.Size = new System.Drawing.Size(254, 146);
			this.propertySettings.TabIndex = 2;
			this.propertySettings.ToolbarVisible = false;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 295);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(254, 3);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 446);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "FormMain";
			this.Text = "Custom Property Grid Tab";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox lvwItems;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PropertyGrid propertySettings;
		private System.Windows.Forms.Splitter splitter1;
	}
}

