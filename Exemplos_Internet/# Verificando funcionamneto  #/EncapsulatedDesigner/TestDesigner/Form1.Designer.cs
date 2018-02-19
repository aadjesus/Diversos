namespace TestDesigner
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.demoContextStrip1 = new EncapsulatedDesigner.DemoContextStrip();
            this.oneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoTreeView1 = new EncapsulatedDesigner.DemoTreeView();
            this.demoContextStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // demoContextStrip1
            // 
            this.demoContextStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneToolStripMenuItem,
            this.twoToolStripMenuItem,
            this.threeToolStripMenuItem});
            this.demoContextStrip1.Name = "demoContextStrip1";
            this.demoContextStrip1.Size = new System.Drawing.Size(120, 70);
            // 
            // oneToolStripMenuItem
            // 
            this.oneToolStripMenuItem.Name = "oneToolStripMenuItem";
            this.oneToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.oneToolStripMenuItem.Text = "one";
            // 
            // twoToolStripMenuItem
            // 
            this.twoToolStripMenuItem.Name = "twoToolStripMenuItem";
            this.twoToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.twoToolStripMenuItem.Text = "two";
            // 
            // threeToolStripMenuItem
            // 
            this.threeToolStripMenuItem.Name = "threeToolStripMenuItem";
            this.threeToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.threeToolStripMenuItem.Text = "three";
            // 
            // demoTreeView1
            // 
            this.demoTreeView1.Location = new System.Drawing.Point(69, 69);
            this.demoTreeView1.Name = "demoTreeView1";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Node2";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Node0";
            this.demoTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.demoTreeView1.Size = new System.Drawing.Size(151, 99);
            this.demoTreeView1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 262);
            this.Controls.Add(this.demoTreeView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.demoContextStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem oneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threeToolStripMenuItem;
        protected EncapsulatedDesigner.DemoContextStrip demoContextStrip1;
        protected EncapsulatedDesigner.DemoTreeView demoTreeView1;
    }
}