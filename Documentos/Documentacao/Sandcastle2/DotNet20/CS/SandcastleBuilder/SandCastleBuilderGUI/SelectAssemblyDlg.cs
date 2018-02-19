//=============================================================================
// System  : Sandcastle Help File Builder
// File    : SelectAssemblyDlg.cs
// Author  : Eric Woodruff
// Updated : 01/18/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the form used to select assemblies for use in building
// the help file.
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
// 1.0.0.0  08/02/2006  EFW  Created the code
// 1.3.2.0  11/10/2006  EFW  Added support for CommentsOnly
// 1.3.4.0  12/31/2006  EFW  Converted path properties to FilePath objects
//=============================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder
{
    /// <summary>
    /// This form is used to select assemblies for use in building the
    /// help file.
    /// </summary>
    /// <remarks>If <see cref="AssemblyInfo" /> is not set, the form assumes
    /// a new assembly is being added.  If set, it assumes the specified
    /// assembly information is being edited.</remarks>
    public partial class SelectAssemblyDlg : Form
    {
        private DocumentAssembly docAsm;

        //=====================================================================

        /// <summary>
        /// Get or set the document assembly information
        /// </summary>
        public DocumentAssembly AssemblyInfo
        {
            get { return docAsm; }
            set
            {
                this.Text = "Edit a Documentation Assembly";

                if(value != null)
                {
                    docAsm.AssemblyPath = new FilePath(value.AssemblyPath.Path,
                        value.AssemblyPath.IsFixedPath);
                    docAsm.XmlCommentsPath = new FilePath(
                        value.XmlCommentsPath.Path,
                        value.XmlCommentsPath.IsFixedPath);
                    docAsm.CommentsOnly = value.CommentsOnly;
                }

                pgProps.Refresh();
            }
        }

        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public SelectAssemblyDlg()
        {
            InitializeComponent();

            docAsm = new DocumentAssembly();
            pgProps.SelectedObject = docAsm;
        }

        /// <summary>
        /// This is used to validate that an assembly and an XML file have
        /// been selected before allowing OK to close the form.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event argument</param>
        private void SelectAssemblyDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            int asmLength = docAsm.AssemblyPath.Path.Length,
                xmlLength = docAsm.XmlCommentsPath.Path.Length;

            if(this.DialogResult == DialogResult.OK)
            {
                if(!docAsm.CommentsOnly && (asmLength == 0 || xmlLength == 0))
                {
                    MessageBox.Show("An assembly and a comments file " +
                        "are required unless CommentsOnly is true",
                        Constants.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
                else
                    if(docAsm.CommentsOnly && xmlLength == 0)
                    {
                        MessageBox.Show("A comments file is required",
                            Constants.AppName, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                    }

                if(e.Cancel == true)
                    this.DialogResult = DialogResult.None;
            }
        }
    }
}
