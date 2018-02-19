//=============================================================================
// System  : Sandcastle Help File Builder
// File    : ProjectSummaryDlg.cs
// Author  : Eric Woodruff
// Updated : 09/05/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to edit the project summary comments.
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
//=============================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder
{
    /// <summary>
    /// This form is used to edit the project summary comments.
    /// </summary>
    public partial class ProjectSummaryDlg : Form
    {
        //=====================================================================

        /// <summary>
        /// Get or set the project summary comments
        /// </summary>
        public string Summary
        {
            get { return txtSummary.Text; }
            set { txtSummary.Text = value; }
        }

        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectSummaryDlg()
        {
            InitializeComponent();
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
    }
}
