//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : SelectGacEntriesDlg.cs
// Author  : Eric Woodruff
// Updated : 09/02/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to select GAC entries that should be
// project dependencies.
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
// 1.2.0.0  09/02/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using SandcastleBuilder.Utils.Gac;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This form is used to let the user select the GAC entries to add as
    /// dependencies for the project.
    /// </summary>
    internal partial class SelectGacEntriesDlg : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SelectGacEntriesDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is used to return the list of selected dependencies
        /// </summary>
        public List<string> SelectedEntries
        {
            get
            {
                ListBox.SelectedObjectCollection selectedItems =
                    lbGACEntries.SelectedItems;

                List<string> items = new List<string>();

                for(int idx = 0; idx < selectedItems.Count; idx++)
                    items.Add((string)selectedItems[idx]);

                return items;
            }
        }

        /// <summary>
        /// Load the GAC list when activated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void SelectGacEntriesDlg_Activated(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                lbGACEntries.DataSource = AssemblyLoader.GacList;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}