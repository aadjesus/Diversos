namespace ShellControlsExample
{
  partial class SampleForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            this.tabControl = new System.Windows.Forms.TabControl();
            this.defaultTabPage = new System.Windows.Forms.TabPage();
            this.defaultSplitContainer = new System.Windows.Forms.SplitContainer();
            this.defaultTreeView = new System.Windows.Forms.TreeView();
            this.defaultListView = new System.Windows.Forms.ListView();
            this.shellTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.shellTreeView = new ShellControlsExample.TreeView();
            this.shellListView = new ShellControlsExample.ListView();
            this.closeButton = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new ShellControlsExample.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeView1 = new ShellControlsExample.TreeView();
            this.tabControl.SuspendLayout();
            this.defaultTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultSplitContainer)).BeginInit();
            this.defaultSplitContainer.Panel1.SuspendLayout();
            this.defaultSplitContainer.Panel2.SuspendLayout();
            this.defaultSplitContainer.SuspendLayout();
            this.shellTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.defaultTabPage);
            this.tabControl.Controls.Add(this.shellTabPage);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(720, 313);
            this.tabControl.TabIndex = 0;
            // 
            // defaultTabPage
            // 
            this.defaultTabPage.Controls.Add(this.defaultSplitContainer);
            this.defaultTabPage.Location = new System.Drawing.Point(4, 22);
            this.defaultTabPage.Name = "defaultTabPage";
            this.defaultTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.defaultTabPage.Size = new System.Drawing.Size(712, 287);
            this.defaultTabPage.TabIndex = 0;
            this.defaultTabPage.Text = "Default";
            this.defaultTabPage.UseVisualStyleBackColor = true;
            // 
            // defaultSplitContainer
            // 
            this.defaultSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.defaultSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.defaultSplitContainer.Name = "defaultSplitContainer";
            // 
            // defaultSplitContainer.Panel1
            // 
            this.defaultSplitContainer.Panel1.Controls.Add(this.defaultTreeView);
            // 
            // defaultSplitContainer.Panel2
            // 
            this.defaultSplitContainer.Panel2.Controls.Add(this.defaultListView);
            this.defaultSplitContainer.Size = new System.Drawing.Size(706, 281);
            this.defaultSplitContainer.SplitterDistance = 250;
            this.defaultSplitContainer.TabIndex = 1;
            // 
            // defaultTreeView
            // 
            this.defaultTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultTreeView.Location = new System.Drawing.Point(0, 0);
            this.defaultTreeView.Name = "defaultTreeView";
            this.defaultTreeView.Size = new System.Drawing.Size(250, 281);
            this.defaultTreeView.TabIndex = 0;
            // 
            // defaultListView
            // 
            this.defaultListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultListView.Location = new System.Drawing.Point(0, 0);
            this.defaultListView.Name = "defaultListView";
            this.defaultListView.Size = new System.Drawing.Size(452, 281);
            this.defaultListView.TabIndex = 0;
            this.defaultListView.UseCompatibleStateImageBehavior = false;
            // 
            // shellTabPage
            // 
            this.shellTabPage.Controls.Add(this.splitContainer1);
            this.shellTabPage.Location = new System.Drawing.Point(4, 22);
            this.shellTabPage.Name = "shellTabPage";
            this.shellTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.shellTabPage.Size = new System.Drawing.Size(712, 287);
            this.shellTabPage.TabIndex = 1;
            this.shellTabPage.Text = "Using Visual Styles";
            this.shellTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.shellTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.shellListView);
            this.splitContainer1.Size = new System.Drawing.Size(706, 281);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // shellTreeView
            // 
            this.shellTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellTreeView.Location = new System.Drawing.Point(0, 0);
            this.shellTreeView.Name = "shellTreeView";
            this.shellTreeView.Size = new System.Drawing.Size(250, 281);
            this.shellTreeView.TabIndex = 0;
            // 
            // shellListView
            // 
            this.shellListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellListView.Location = new System.Drawing.Point(0, 0);
            this.shellListView.Name = "shellListView";
            this.shellListView.Size = new System.Drawing.Size(452, 281);
            this.shellListView.TabIndex = 0;
            this.shellListView.UseCompatibleStateImageBehavior = false;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(657, 331);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(712, 287);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView1.Location = new System.Drawing.Point(34, 38);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(202, 151);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(278, 36);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.Size = new System.Drawing.Size(121, 97);
            this.treeView1.TabIndex = 1;
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(744, 366);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabControl);
            this.Name = "SampleForm";
            this.Text = "cyotek.com Shell Controls Example";
            this.Load += new System.EventHandler(this.SampleForm_Load);
            this.tabControl.ResumeLayout(false);
            this.defaultTabPage.ResumeLayout(false);
            this.defaultSplitContainer.Panel1.ResumeLayout(false);
            this.defaultSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultSplitContainer)).EndInit();
            this.defaultSplitContainer.ResumeLayout(false);
            this.shellTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage defaultTabPage;
    private System.Windows.Forms.TabPage shellTabPage;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private TreeView shellTreeView;
    private ListView shellListView;
    private System.Windows.Forms.SplitContainer defaultSplitContainer;
    private System.Windows.Forms.TreeView defaultTreeView;
    private System.Windows.Forms.ListView defaultListView;
    private System.Windows.Forms.TabPage tabPage1;
    private TreeView treeView1;
    private ListView listView1;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
  }
}

