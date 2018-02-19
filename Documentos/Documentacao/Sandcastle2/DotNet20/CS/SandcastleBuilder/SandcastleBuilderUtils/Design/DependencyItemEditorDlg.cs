//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : DependencyItemEditorDlg.cs
// Author  : Eric Woodruff
// Updated : 03/07/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to edit the dependency items.
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
// 1.1.0.0  08/23/2006  EFW  Created the code
// 1.3.4.0  01/01/2007  EFW  Reworked for use with FileFolderGacPath object
//=============================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This form is used to edit the dependency item collection
    /// </summary>
    internal partial class DependencyItemEditorDlg : Form
    {
        //=====================================================================
        // Private data members

        private DependencyItemCollection items;
        private bool wasModified;

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dependencyItems">The item collection to edit</param>
        public DependencyItemEditorDlg(DependencyItemCollection dependencyItems)
        {
            InitializeComponent();
            items = dependencyItems;

            lbDependencyItems.DisplayMember = lbDependencyItems.ValueMember =
                "DependencyPath";

            if(items.Count == 0)
                pgProps.Enabled = btnDelete.Enabled = false;
            else
            {
                // Binding the collection to the list box caused some
                // odd problems with the property grid so we'll add the
                // items to the list box directly.
                foreach(DependencyItem di in items)
                    lbDependencyItems.Items.Add(di);

                lbDependencyItems.SelectedIndex = 0;
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

                for(int idx = 0; idx < lbDependencyItems.Items.Count; idx++)
                    items.Add((DependencyItem)lbDependencyItems.Items[idx]);
            }

            this.Close();
        }

        /// <summary>
        /// Add a new folder dependency item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder containing the dependencies";
                dlg.SelectedPath = Directory.GetCurrentDirectory();

                // If selected, set the new filename
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    DependencyItem newItem = new DependencyItem();
                    newItem.DependencyPath = new FileFolderGacPath(
                        dlg.SelectedPath + @"\*.*");

                    lbDependencyItems.Items.Add(newItem);
                    btnDelete.Enabled = true;
                    lbDependencyItems.SelectedIndex = lbDependencyItems.Items.Count - 1;
                    wasModified = true;
                }
            }
        }

        /// <summary>
        /// Add a new single file dependency item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            DependencyItem newItem;

            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Title = "Select the dependency file(s)";
                dlg.Filter = "Library and Executable Files (*.dll, *.exe)|" +
                    "*.dll;*.exe|Library Files (*.dll)|*.dll|" +
                    "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                dlg.InitialDirectory = Directory.GetCurrentDirectory();

                // If selected, add the file(s)
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach(string file in dlg.FileNames)
                    {
                        newItem = new DependencyItem();
                        newItem.DependencyPath = new FileFolderGacPath(file);

                        lbDependencyItems.Items.Add(newItem);
                    }

                    btnDelete.Enabled = true;
                    lbDependencyItems.SelectedIndex = lbDependencyItems.Items.Count - 1;
                    wasModified = true;
                }
            }
        }

        /// <summary>
        /// Add one or more dependencies from the GAC
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddFromGAC_Click(object sender, EventArgs e)
        {
            DependencyItem newItem;

            using(SelectGacEntriesDlg dlg = new SelectGacEntriesDlg())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach(string gacEntry in dlg.SelectedEntries)
                    {
                        newItem = new DependencyItem();
                        newItem.DependencyPath = new FileFolderGacPath(
                            "GAC:" + gacEntry);

                        lbDependencyItems.Items.Add(newItem);
                    }

                    if(lbDependencyItems.Items.Count > 0)
                    {
                        btnDelete.Enabled = true;
                        lbDependencyItems.SelectedIndex =
                            lbDependencyItems.Items.Count - 1;
                        wasModified = true;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a dependency item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idx = lbDependencyItems.SelectedIndex;

            if(idx == -1)
                lbDependencyItems.SelectedIndex = 0;
            else
            {
                lbDependencyItems.Items.RemoveAt(idx);
                wasModified = true;

                if(lbDependencyItems.Items.Count == 0)
                    pgProps.Enabled = btnDelete.Enabled = false;
                else
                    if(idx < lbDependencyItems.Items.Count)
                        lbDependencyItems.SelectedIndex = idx;
                    else
                        lbDependencyItems.SelectedIndex =
                            lbDependencyItems.Items.Count - 1;
            }
        }

        /// <summary>
        /// Update the dependency path text box with the selected item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lbDependencyItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbDependencyItems.SelectedItem != null)
            {
                DependencyItem di = (DependencyItem)lbDependencyItems.SelectedItem;
                pgProps.SelectedObject = di;

                // Can't edit GAC paths
                pgProps.Enabled = !(di.DependencyPath.Path.StartsWith("GAC:"));
            }
            else
            {
                pgProps.SelectedObject = null;
                pgProps.Enabled = false;
            }

            pgProps.Refresh();
        }

        /// <summary>
        /// Refresh the list box item when a property changes
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            lbDependencyItems.Refresh(lbDependencyItems.SelectedIndex);
        }
    }
}
