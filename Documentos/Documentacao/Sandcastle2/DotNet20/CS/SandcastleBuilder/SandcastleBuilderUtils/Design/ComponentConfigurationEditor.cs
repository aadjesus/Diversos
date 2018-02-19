//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : ComponentConfigurationEditor.cs
// Author  : Eric Woodruff
// Updated : 11/24/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type editor that displays a dialog box used to edit
// the configuration build components.
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
// 1.3.3.0  11/24/2006  EFW  Created the code
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
    /// <see cref="ComponentConfigurationEditorDlg"/> to edit the build
    /// component configurations.
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Unrestricted = true),
      PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public class ComponentConfigurationEditor : System.Drawing.Design.UITypeEditor
    {
        /// <summary>
        /// This is overridden to edit the value using the
        /// <see cref="ComponentConfigurationEditorDlg"/> dialog.
        /// </summary>
        /// <param name="context">The descriptor context</param>
        /// <param name="provider">The provider</param>
        /// <param name="value">The component configuration dictionary as an
        /// object</param>
        /// <returns>The edited component configuration dictionary as an
        /// object</returns>
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
          IServiceProvider provider, object value)
        {
            // Get the component configuration dictionary
            ComponentConfigurationDictionary items = value as
                ComponentConfigurationDictionary;

            if(context == null || provider == null || context.Instance == null ||
              value == null)
                return base.EditValue(context, provider, value);

            using(ComponentConfigurationEditorDlg dlg =
              new ComponentConfigurationEditorDlg(items))
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
