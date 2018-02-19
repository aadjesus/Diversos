//=============================================================================
// System  : Sandcastle Help File Builder
// File    : NamespacesDlg.cs
// Author  : Eric Woodruff
// Updated : 02/12/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to edit namespace summaries and to indicate
// which namespaces should appear in the help file.
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
// 1.2.0.0  09/04/2006  EFW  Created the code
// 1.4.0.0  02/12/2007  EFW  Added the ability to delete old namespaces
//=============================================================================

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using SandcastleBuilder.Utils;
using SandcastleBuilder.Utils.Gac;

namespace SandcastleBuilder
{
    /// <summary>
    /// This form is used to edit namespace summaries and to indicate which
    /// namespaces should appear in the help file.
    /// </summary>
    public partial class NamespacesDlg : Form
    {
        private SandcastleProject project;
        private bool wasChanged, namespacesLoaded;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="editProject">The project file in which to edit the
        /// namespace summaries</param>
        public NamespacesDlg(SandcastleProject editProject)
        {
            InitializeComponent();
            project = editProject;
        }

        /// <summary>
        /// Scan for new namespaces when loaded
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void NamespacesDlg_Activated(object sender, EventArgs e)
        {
            NamespaceSummaryItemCollection nsColl = project.NamespaceSummaries;
            Collection<string> asmNS, namespaces = new Collection<string>();
            AssemblyLoader loader = null;
            string path = null;

            // Only load them once and not at design time
            if(namespacesLoaded || this.DesignMode)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                namespacesLoaded = true;

                try
                {
                    // Make sure we start in the project's folder so that
                    // relative paths are resolved correctly.
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(
                        project.Filename));

                    loader = AssemblyLoader.CreateAssemblyLoader(project);

                    // Get a list of all unique namespaces in the
                    // documentation assemblies.
                    foreach(DocumentAssembly da in project.Assemblies)
                    {
                        if(da.CommentsOnly)
                            continue;

                        try
                        {
                            path = da.AssemblyPath;
                            asmNS = loader.GetNamespaces(path);

                            foreach(string ns in asmNS)
                                if(!namespaces.Contains(ns))
                                    namespaces.Add(ns);
                        }
                        catch(FileNotFoundException ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex);

                            if(MessageBox.Show("Unable to find the assembly '" +
                              path + "' or one of its dependencies.  You " +
                              "may need to build it or add a dependency " +
                              "to the project dependency list.  Do you " +
                              "want to continue loading the remaining " +
                              "assemblies?", Constants.AppName,
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Error) == DialogResult.No)
                                break;
                        }
                    }
                }
                finally
                {
                    if(loader != null)
                        AssemblyLoader.ReleaseAssemblyLoader();
                }

                // The global namespace (N:) isn't always listed but we'll
                // add it as it does show up in the reflection info anyway.
                if(!namespaces.Contains(String.Empty))
                    namespaces.Add(String.Empty);

                // Add new namespaces to the list
                foreach(string ns in namespaces)
                    if(!nsColl.Contains(ns))
                        nsColl.Add(new NamespaceSummaryItem(ns));

                nsColl.Sort();

                // Load the listbox with the namespace items
                foreach(NamespaceSummaryItem nsi in nsColl)
                    lbNamespaces.Items.Add(nsi, nsi.IsDocumented);

                if(lbNamespaces.Items.Count == 0)
                    btnDelete.Enabled = txtSummary.Enabled = false;
                else
                    lbNamespaces.SelectedIndex = 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Store the changes and close the dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if(wasChanged)
            {
                this.DialogResult = DialogResult.OK;
                project.NamespaceSummaries.Clear();

                for(int idx = 0; idx < lbNamespaces.Items.Count; idx++)
                    project.NamespaceSummaries.Add((NamespaceSummaryItem)
                        lbNamespaces.Items[idx]);
            }
            else
                this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        /// <summary>
        /// When the item changes, show its summary in the text box.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lbNamespaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbNamespaces.SelectedIndex != -1)
            {
                NamespaceSummaryItem nsi =
                    (NamespaceSummaryItem)lbNamespaces.SelectedItem;
                txtSummary.Text = nsi.Summary;
            }
            else
                txtSummary.Text = null;
        }

        /// <summary>
        /// Mark the summary item as documented or not when the check state
        /// changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbNamespaces_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            NamespaceSummaryItem nsi = (NamespaceSummaryItem)lbNamespaces.SelectedItem;

            bool isChecked = (e.NewValue == CheckState.Checked);

            if(nsi != null && nsi.IsDocumented != isChecked)
            {
                nsi.IsDocumented = isChecked;
                wasChanged = true;
            }
        }

        /// <summary>
        /// Clear the selection to prevent accidental deletion of the text
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtSummary_Enter(object sender, EventArgs e)
        {
            txtSummary.Select(0, 0);
            txtSummary.ScrollToCaret();
        }

        /// <summary>
        /// Store changes to the summary when the textbox loses focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtSummary_Leave(object sender, EventArgs e)
        {
            NamespaceSummaryItem nsi = (NamespaceSummaryItem)lbNamespaces.SelectedItem;

            if(nsi != null && nsi.Summary != txtSummary.Text)
            {
                nsi.Summary = txtSummary.Text;
                wasChanged = true;
            }
        }

        /// <summary>
        /// Delete an old namespace entry that is no longer needed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idx = lbNamespaces.SelectedIndex;

            if(idx == -1)
                lbNamespaces.SelectedIndex = 0;
            else
            {
                lbNamespaces.Items.RemoveAt(idx);
                wasChanged = true;

                if(lbNamespaces.Items.Count == 0)
                    btnDelete.Enabled = txtSummary.Enabled = false;
                else
                    if(idx < lbNamespaces.Items.Count)
                        lbNamespaces.SelectedIndex = idx;
                    else
                        lbNamespaces.SelectedIndex =
                            lbNamespaces.Items.Count - 1;
            }
        }
    }
}
