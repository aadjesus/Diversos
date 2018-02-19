//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : PreviewTocDlg.cs
// Author  : Eric Woodruff
// Updated : 10/18/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to preview the table of content based on
// the currently defined set of additional content items.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  08/08/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This form is used to preview the table of content based on the
    /// currently defined set of additional content items.
    /// </summary>
    internal partial class PreviewTocDlg : Form
    {
        #region Data Members
        //=====================================================================
        // Data members

        private BuildProcess bp;
        private TreeNode defaultNode;
        private bool modified;

        //=====================================================================
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project">The temporary project from which to get
        /// the additional content items</param>
        public PreviewTocDlg(SandcastleProject project)
        {
            InitializeComponent();

            bp = new BuildProcess(project);

            this.LoadTree();
        }

        #region Helper methods
        /// <summary>
        /// Load the tree view with the table of content nodes
        /// </summary>
        private void LoadTree()
        {
            TreeNode node;
            TocEntryCollection toc = bp.TableOfContent;

            tvTOC.Nodes.Clear();
            defaultNode = null;

            if(toc != null && toc.Count != 0)
            {
                btnDefaultTopic.Enabled = btnMoveUp.Enabled =
                    btnMoveDown.Enabled = btnSave.Enabled = true;

                foreach(TocEntry te in bp.TableOfContent)
                {
                    node = tvTOC.Nodes.Add(te.Title);
                    node.Tag = te;

                    if(te.IsDefaultTopic)
                        defaultNode = node;

                    if(te.Children.Count != 0)
                        this.AddChildren(te.Children, node);
                }

                if(defaultNode != null)
                {
                    defaultNode.ToolTipText = "Default topic";
                    defaultNode.ImageIndex = defaultNode.SelectedImageIndex = 1;
                }
            }
            else
                btnDefaultTopic.Enabled = btnMoveUp.Enabled =
                    btnMoveDown.Enabled = btnSave.Enabled = false;

            if(tvTOC.Nodes.Count != 0)
                tvTOC.ExpandAll();
        }

        /// <summary>
        /// Add a root node and its children to the tree view recursively
        /// </summary>
        /// <param name="children">The collection of entries to add</param>
        /// <param name="root">The root to which they are added</param>
        private void AddChildren(TocEntryCollection children, TreeNode root)
        {
            TreeNode node;

            foreach(TocEntry te in children)
            {
                node = root.Nodes.Add(te.Title);
                node.Tag = te;

                if(te.IsDefaultTopic)
                    defaultNode = node;

                if(te.Children.Count != 0)
                    this.AddChildren(te.Children, node);
            }
        }

        /// <summary>
        /// Update the file related to the specified tree node with its new
        /// sort order and/or default topic indicator.
        /// </summary>
        /// <param name="tn">The tree node containing the TOC entry</param>
        /// <param name="sortOrder">The new sort order</param>
        private void UpdateTocEntry(TreeNode tn, int sortOrder)
        {
            Encoding enc;
            string content;

            TocEntry toc = (TocEntry)tn.Tag;

            if(toc.SortOrder != sortOrder || (toc.IsDefaultTopic &&
                defaultNode != tn) || (!toc.IsDefaultTopic &&
                defaultNode == tn))
            {
                enc = Encoding.Default;

                if(toc.SourceFile.EndsWith(@"\"))
                {
                    toc.SourceFile += toc.Title + ".html";

                    // Exclude the new folder file if we create it
                    content = "<!-- @TOCExclude -->";
                }
                else
                    content = BuildProcess.ReadWithEncoding(
                        toc.SourceFile, ref enc);

                // Add or remove the default topic indicator
                if(defaultNode != tn)
                    content = BuildProcess.reIsDefaultTopic.Replace(content,
                        String.Empty);
                else
                    if(!BuildProcess.reIsDefaultTopic.IsMatch(content))
                        content += "<!-- @DefaultTopic -->";

                // Add or update the sort order
                if(BuildProcess.reSortOrder.IsMatch(content))
                    content = BuildProcess.reSortOrder.Replace(content,
                        String.Format(CultureInfo.InvariantCulture,
                            "<!-- @SortOrder {0} -->", sortOrder));
                else
                    content += String.Format(CultureInfo.InvariantCulture,
                        "<!-- @SortOrder {0} -->", sortOrder);

                using(StreamWriter sw = new StreamWriter(toc.SourceFile, false,
                    enc))
                {
                    sw.Write(content);
                }
            }
        }

        /// <summary>
        /// Update the files related to the children of a tree node
        /// </summary>
        /// <param name="children">The children to update</param>
        private void UpdateChildNodes(TreeNodeCollection children)
        {
            int sortOrder = 1;

            foreach(TreeNode tn in children)
            {
                this.UpdateTocEntry(tn, sortOrder);

                if(tn.Nodes.Count != 0)
                    this.UpdateChildNodes(tn.Nodes);

                sortOrder++;
            }
        }
        #endregion

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Update the state of the move up/down buttons based on the
        /// current selection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvTOC_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = tvTOC.SelectedNode;
            btnMoveUp.Enabled = (current.PrevNode != null);
            btnMoveDown.Enabled = (current.NextNode != null);
        }

        /// <summary>
        /// Set the selected node as the default topic
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The sender of the event</param>
        private void btnDefaultTopic_Click(object sender, EventArgs e)
        {
            TreeNode newDefault = tvTOC.SelectedNode;

            if(defaultNode != newDefault)
            {
                if(defaultNode != null)
                {
                    defaultNode.ToolTipText = null;
                    defaultNode.ImageIndex = defaultNode.SelectedImageIndex = 0;
                }

                newDefault.ToolTipText = "Default topic";
                newDefault.ImageIndex = newDefault.SelectedImageIndex = 1;
                defaultNode = newDefault;
                modified = true;
            }
            else
                if(defaultNode != null)
                {
                    // Turn it off altogether
                    defaultNode.ToolTipText = null;
                    defaultNode.ImageIndex = defaultNode.SelectedImageIndex = 0;
                    defaultNode = null;
                    modified = true;
                }
        }

        /// <summary>
        /// Move the selected node up or down within the group
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The sender of the event</param>
        private void btnMoveNode_Click(object sender, EventArgs e)
        {
            TreeNode moveNode, insertNode;
            TreeNodeCollection tnc;

            moveNode = tvTOC.SelectedNode;

            if(moveNode.Parent == null)
                tnc = tvTOC.Nodes;
            else
                tnc = moveNode.Parent.Nodes;

            if(sender == btnMoveUp)
            {
                insertNode = moveNode.PrevNode;
                tnc.Remove(moveNode);
                tnc.Insert(tnc.IndexOf(insertNode), moveNode);
            }
            else
            {
                insertNode = moveNode.NextNode;
                tnc.Remove(moveNode);
                tnc.Insert(tnc.IndexOf(insertNode) + 1, moveNode);
            }

            tvTOC.SelectedNode = moveNode;
            modified = true;
        }

        /// <summary>
        /// Save changes to the default topic and order by adding or
        /// removing the comment tags in the files.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The sender of the event</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int sortOrder = 1;

            if(!modified)
                return;

            try
            {
                foreach(TreeNode tn in tvTOC.Nodes)
                {
                    this.UpdateTocEntry(tn, sortOrder);

                    if(tn.Nodes.Count != 0)
                        this.UpdateChildNodes(tn.Nodes);

                    sortOrder++;
                }

                modified = false;
                this.LoadTree();

                MessageBox.Show("All changes saved successfully",
                    "Sandcastle Help File Builder", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                MessageBox.Show("Unexpected error updating additional " +
                    "content files: " + ex.Message,
                    "Sandcastle Help File Builder", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ask to save changes if modifications were made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTocDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr;

            if(modified)
            {
                dr = MessageBox.Show("Do you want to save your changes to " +
                    "the layout of the table of content?  Click YES to " +
                    "save the changes, NO to discard them, or CANCEL to " +
                    "stay here and make further changes.",
                    "Sandcastle Help File Builder",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button3);

                switch(dr)
                {
                    case DialogResult.Yes:
                        btnSave_Click(sender, e);

                        // Stay here if it failed
                        e.Cancel = modified;
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
