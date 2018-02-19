namespace CarApplication
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode( "Car" );
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode( "Wheels" );
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode( "Person" );
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode( "Person Collection" );
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.propertyGridControl1 = new CustomControls.PropertyGridControl();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add( this.treeView1 );
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add( this.propertyGridControl1 );
			this.splitContainer1.Size = new System.Drawing.Size( 774, 432 );
			this.splitContainer1.SplitterDistance = 258;
			this.splitContainer1.TabIndex = 0;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point( 0, 0 );
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "treeNode1";
			treeNode1.Text = "Car";
			treeNode2.Name = "treeNode2";
			treeNode2.Text = "Wheels";
			treeNode3.Name = "treeNode3";
			treeNode3.Text = "Person";
			treeNode4.Name = "treeNode4";
			treeNode4.Text = "Person Collection";
			this.treeView1.Nodes.AddRange( new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4} );
			this.treeView1.Size = new System.Drawing.Size( 258, 432 );
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.treeView1_AfterSelect );
			// 
			// propertyGridControl1
			// 
			this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridControl1.Location = new System.Drawing.Point( 0, 0 );
			this.propertyGridControl1.Name = "propertyGridControl1";
			this.propertyGridControl1.Size = new System.Drawing.Size( 512, 432 );
			this.propertyGridControl1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 774, 432 );
			this.Controls.Add( this.splitContainer1 );
			this.Name = "Form1";
			this.splitContainer1.Panel1.ResumeLayout( false );
			this.splitContainer1.Panel2.ResumeLayout( false );
			this.splitContainer1.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private CustomControls.PropertyGridControl propertyGridControl1;
		private System.Windows.Forms.TreeView treeView1;
	}
}

