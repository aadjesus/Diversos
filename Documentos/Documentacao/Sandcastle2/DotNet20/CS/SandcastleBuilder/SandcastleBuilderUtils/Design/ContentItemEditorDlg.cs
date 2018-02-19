//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : ContentItemEditorDlg.cs
// Author  : Eric Woodruff
// Updated : 01/07/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to edit the additional content items.
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
// 1.3.3.0  12/01/2006  EFW  Project path is now maintained automatically
//=============================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This form is used to edit the additional content item collection
    /// </summary>
    public partial class ContentItemEditorDlg : Form
    {
        //=====================================================================
        // Private data members

        private static string projectPath;  // Project path for preview

        private ContentItemCollection items;
        private bool wasModified;

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// This is used to set or get the current project folder to use with
        /// the preview form so that it can resolve relative paths on
        /// additional content items.
        /// </summary>
        public static string ProjectPath
        {
            get { return projectPath; }
            set
            {
                if(value == null)
                    projectPath = String.Empty;
                else
                    projectPath = value;
            }
        }

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentItems">The item collection to edit</param>
        internal ContentItemEditorDlg(ContentItemCollection contentItems)
        {
            InitializeComponent();
            items = contentItems;

            lbContentItems.DisplayMember = lbContentItems.ValueMember =
                "ListDescription";

            if(items.Count == 0)
                btnDelete.Enabled = btnPreviewTOC.Enabled =
                    pgProps.Enabled = false;
            else
            {
                // Binding the collection to the list box caused some
                // odd problems with the property grid so we'll add the
                // items to the list box directly.
                foreach(ContentItem ci in items)
                    lbContentItems.Items.Add(ci);

                lbContentItems.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Refresh the collection and close the form
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if(wasModified)
            {
                items.Clear();

                for(int idx = 0; idx < lbContentItems.Items.Count; idx++)
                    items.Add((ContentItem)lbContentItems.Items[idx]);
            }

            this.Close();
        }

        /// <summary>
        /// Set the current item as the property grid's object when the
        /// selection changes.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lbContentItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = lbContentItems.SelectedIndex;

            if(idx != -1)
            {
                pgProps.SelectedObject = lbContentItems.SelectedItem;
            }
            else
                pgProps.SelectedObject = null;
        }

        /// <summary>
        /// Refresh the list box item when a property changes
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s,
          PropertyValueChangedEventArgs e)
        {
            lbContentItems.Refresh(lbContentItems.SelectedIndex);
            wasModified = true;
        }

        /// <summary>
        /// Add a new folder content item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder to add all of its content";
                dlg.SelectedPath = Directory.GetCurrentDirectory();

                // If selected, add the new folder item
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    ContentItem newItem = new ContentItem();
                    newItem.SourcePath = new FilePath(
                        dlg.SelectedPath + @"\*.*");
                    newItem.DestinationPath = dlg.SelectedPath.Substring(
                        dlg.SelectedPath.LastIndexOf('\\') + 1);

                    lbContentItems.Items.Add(newItem);
                    btnDelete.Enabled = btnPreviewTOC.Enabled =
                        pgProps.Enabled = true;
                    lbContentItems.SelectedIndex =
                        lbContentItems.Items.Count - 1;
                    wasModified = true;
                }
            }
        }

        /// <summary>
        /// Add a new single file content item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            ContentItem newItem;

            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Title = "Select the additional content file(s)";
                dlg.Filter = "HTML files (*.htm, *.html, *.css)|" +
                    "*.htm;*.html;*.css|Image files (*.gif, *.jpg, " +
                    "*.jpe*, *.png, *.bmp)|*.gif;*.jpg;*.jpe*;*.png;*.bmp|" +
                    "All Files (*.*)|*.*";
                dlg.InitialDirectory = Directory.GetCurrentDirectory();

                // If selected, add the new file(s)
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach(string file in dlg.FileNames)
                    {
                        newItem = new ContentItem();
                        newItem.SourcePath = new FilePath(file);
                        newItem.DestinationPath = null;
                        lbContentItems.Items.Add(newItem);
                    }

                    btnDelete.Enabled = btnPreviewTOC.Enabled =
                        pgProps.Enabled = true;
                    lbContentItems.SelectedIndex =
                        lbContentItems.Items.Count - 1;
                    wasModified = true;
                }
            }
        }

        /// <summary>
        /// Delete a content item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idx = lbContentItems.SelectedIndex;

            if(idx == -1)
                lbContentItems.SelectedIndex = 0;
            else
            {
                lbContentItems.Items.RemoveAt(idx);
                wasModified = true;

                if(lbContentItems.Items.Count == 0)
                    btnDelete.Enabled = btnPreviewTOC.Enabled =
                        pgProps.Enabled = false;
                else
                    if(idx < lbContentItems.Items.Count)
                        lbContentItems.SelectedIndex = idx;
                    else
                        lbContentItems.SelectedIndex =
                            lbContentItems.Items.Count - 1;
            }
        }

        /// <summary>
        /// Preview the table of content based on the current settings
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnPreviewTOC_Click(object sender, EventArgs e)
        {
            SandcastleProject project = new SandcastleProject();
            project.Filename = projectPath + @"\Test.shfb";

            for(int idx = 0; idx < lbContentItems.Items.Count; idx++)
                project.AdditionalContent.Add(
                    (ContentItem)lbContentItems.Items[idx]);

            using(PreviewTocDlg previewDlg = new PreviewTocDlg(project))
            {
                previewDlg.ShowDialog();
            }
        }
    }
}
