//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : DependencyItemEditor.cs
// Author  : Eric Woodruff
// Updated : 01/01/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type editor that displays a dialog box used to edit
// the dependency items for a project.
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
//=============================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This is a type editor that displays the
    /// <see cref="DependencyItemEditorDlg"/> to edit the dependency items
    /// for a project.
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Unrestricted=true),
      PermissionSet(SecurityAction.InheritanceDemand, Unrestricted=true)]
    public class DependencyItemEditor : System.Drawing.Design.UITypeEditor
    {
        /// <summary>
        /// This is overridden to edit the value using the
        /// <see cref="DependencyItemEditorDlg"/> dialog.
        /// </summary>
        /// <param name="context">The descriptor context</param>
        /// <param name="provider">The provider</param>
        /// <param name="value">The dependency item collection as an object</param>
        /// <returns>The edited dependency item collection as an object</returns>
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
          IServiceProvider provider, object value)
        {
            // Get the dependency item collection
            DependencyItemCollection items = value as DependencyItemCollection;

            if(context == null || provider == null || context.Instance == null ||
              value == null)
                return base.EditValue(context, provider, value);

            using(DependencyItemEditorDlg dlg = new DependencyItemEditorDlg(items))
            {
                dlg.ShowDialog();
            }

            return value;
        }

        /// <summary>
        /// This is overridden to specify the editor's edit style
        /// </summary>
        /// <param name="context">The descriptor context</param>
        /// <returns>Always returns <b>Modal</b> as long as there is a context
        /// and an instance.  Otherwise, it returns <b>None</b>.</returns>
        public override UITypeEditorEditStyle GetEditStyle(
          System.ComponentModel.ITypeDescriptorContext context)
        {
            if(context != null && context.Instance != null)
                return UITypeEditorEditStyle.Modal;

            return UITypeEditorEditStyle.None;
        }
    }
}
