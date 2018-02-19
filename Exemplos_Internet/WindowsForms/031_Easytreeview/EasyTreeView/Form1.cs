using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyTreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // start off by adding a base treeview node
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "mainNode";
            mainNode.Text = "Main";
            this.treeView1.Nodes.Add(mainNode);
        }


        
#region Add and Remove Nodes

        /// <summary>
        /// Add a Treeview node using a dialog box
        /// forcing the user to set the name and text properties
        /// of the node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuAddNode_Click(object sender, EventArgs e)
        {
            NewNode n = new NewNode();
            n.ShowDialog();
            TreeNode nod = new TreeNode();
            nod.Name = n.NewNodeName.ToString();
            nod.Text = n.NewNodeText.ToString();
            nod.Tag = n.NewNodeTag.ToString();
            n.Close();

            treeView1.SelectedNode.Nodes.Add(nod);
            treeView1.SelectedNode.ExpandAll();
        }



        /// <summary>
        /// Remove the selected node and it children
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuRemoveNode_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.Remove();
        }

        
#endregion


       
#region Treeview Event Handlers

        /// <summary>
        /// Display information about the selected node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                txtName.Text = "";
                txtParentName.Text = "";
                txtText.Text = "";
                txtTag.Text = "";

                txtName.Text = treeView1.SelectedNode.Name.ToString();
                txtText.Text = treeView1.SelectedNode.Text.ToString();
                txtTag.Text = treeView1.SelectedNode.Tag.ToString();
                txtParentName.Text = treeView1.SelectedNode.Parent.Text.ToString();
            }
            catch { }
        }


        /// <summary>
        /// Clear nodes marked by the find functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_Click(object sender, EventArgs e)
        {
            ClearBackColor();
        }

#endregion


        
#region Find By Name

        /// <summary>
        /// Use the treeview's built-in find function
        /// to search for a node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindNode_Click(object sender, EventArgs e)
        {
            ClearBackColor();

            try
            {
                TreeNode[] tn = treeView1.Nodes[0].Nodes.Find(txtNodeSearch.Text, true);
                for (int i = 0; i < tn.Length; i++)
                {
                    treeView1.SelectedNode = tn[i];
                    treeView1.SelectedNode.BackColor = Color.Yellow;
                }
            }
            catch { }
        }

#endregion


        
#region Remove BackColor

        // recursively move through the treeview nodes
        // and reset backcolors to white
        private void ClearBackColor()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                ClearRecursive(n);
            }
        }

        // called by ClearBackColor function
        private void ClearRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.BackColor = Color.White;
                ClearRecursive(tn);
            }
        }
        
#endregion


        
#region Find By Text

        /// <summary>
        /// Searching for nodes by text requires a special function
        /// this function recursively scans the treeview and
        /// marks matching items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNodeTextSearch_Click(object sender, EventArgs e)
        {
            ClearBackColor();
            FindByText();
        }


        private void FindByText()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n);
            }
        }


        private void FindRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // if the text properties match, color the item
                if (tn.Text == this.txtNodeTextSearch.Text)
                    tn.BackColor = Color.Yellow;

                FindRecursive(tn);
            }
        }
        
#endregion



#region Find By Tag

        /// <summary>
        /// Searching for nodes by tag requires a special function
        /// this function recursively scans the treeview and
        /// marks matching items.  Tags can be object; in this
        /// case they are just used to contain strings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNodeTagSearch_Click(object sender, EventArgs e)
        {
            ClearBackColor();
            FindByTag();
        }


        private void FindByTag()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursiveTag(n);
            }
        }


        private void FindRecursiveTag(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // if the text properties match, color the item
                if (tn.Tag.ToString() == this.txtTagSearch.Text)
                    tn.BackColor = Color.Yellow;

                FindRecursiveTag(tn);
            }
        }

        
#endregion



    }
}