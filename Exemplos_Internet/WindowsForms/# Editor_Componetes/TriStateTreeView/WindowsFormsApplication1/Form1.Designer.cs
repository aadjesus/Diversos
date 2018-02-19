namespace WindowsFormsApplication1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node3");
            this.triStateTreeView1 = new SIL.FieldWorks.Common.Controls.TriStateTreeView();
            this.triStateTreeView2 = new SIL.FieldWorks.Common.Controls.TriStateTreeView();
            this.SuspendLayout();
            // 
            // triStateTreeView1
            // 
            this.triStateTreeView1.ImageIndex = 1;
            this.triStateTreeView1.Location = new System.Drawing.Point(54, 26);
            this.triStateTreeView1.Name = "triStateTreeView1";
            treeNode1.Name = "Node4";
            treeNode1.Text = "Node4";
            treeNode2.Name = "Node5";
            treeNode2.Text = "Node5";
            treeNode3.Name = "Node6";
            treeNode3.Text = "Node6";
            treeNode4.Name = "Node7";
            treeNode4.Text = "Node7";
            treeNode5.Name = "Node8";
            treeNode5.Text = "Node8";
            this.triStateTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.triStateTreeView1.SelectedImageIndex = 1;
            this.triStateTreeView1.Size = new System.Drawing.Size(173, 180);
            this.triStateTreeView1.TabIndex = 0;
            // 
            // triStateTreeView2
            // 
            this.triStateTreeView2.ImageIndex = 1;
            this.triStateTreeView2.Location = new System.Drawing.Point(8, 37);
            this.triStateTreeView2.Name = "triStateTreeView2";
            treeNode6.Name = "Node0";
            treeNode6.Text = "Node0";
            treeNode7.Name = "Node1";
            treeNode7.Text = "Node1";
            treeNode8.Name = "Node2";
            treeNode8.Text = "Node2";
            treeNode9.Name = "Node3";
            treeNode9.Text = "Node3";
            this.triStateTreeView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            this.triStateTreeView2.SelectedImageIndex = 1;
            this.triStateTreeView2.Size = new System.Drawing.Size(121, 97);
            this.triStateTreeView2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.triStateTreeView2);
            this.Controls.Add(this.triStateTreeView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SIL.FieldWorks.Common.Controls.TriStateTreeView triStateTreeView1;
        private SIL.FieldWorks.Common.Controls.TriStateTreeView triStateTreeView2;
    }
}

