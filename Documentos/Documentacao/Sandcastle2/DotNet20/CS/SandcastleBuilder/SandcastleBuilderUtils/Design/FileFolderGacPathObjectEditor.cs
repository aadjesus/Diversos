//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : FileFolderGacPathObjectEditor.cs
// Author  : Eric Woodruff
// Updated : 01/01/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type editor that can display a file browser dialog
// to allow selection of a file path at design time.  This can be used in
// conjunction with the FileDialogAttribute to specify the file browser
// dialog's properties.
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
// 1.3.4.0  01/01/2007  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This is a type editor that can display a file browser dialog to
    /// allow selection of a folder path at design time.
    /// </summary>
    /// <remarks>This can be used in conjunction with the
    /// <see cref="FileDialogAttribute" /> to specify the file browser
    /// dialog's properties.</remarks>
    [PermissionSet(SecurityAction.LinkDemand, Unrestricted = true),
      PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public class FileFolderGacPathObjectEditor : FilePathStringEditor
    {
        /// <summary>
        /// This is overridden to edit the value using a file browser dialog.
        /// </summary>
        /// <param name="context">The descriptor context</param>
        /// <param name="provider">The provider</param>
        /// <param name="value">The folder path as an object</param>
        /// <returns>The selected folder path as an object</returns>
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context,
          IServiceProvider provider, object value)
        {
            FileFolderGacPath FileFolderGacPath = value as FileFolderGacPath;

            if(FileFolderGacPath == null)
                return base.EditValue(context, provider, value);

            string path = (string)base.EditValue(context, provider,
                FileFolderGacPath.ToString());

            if(path != FileFolderGacPath.Path)
                value = new FileFolderGacPath(path,
                    FileFolderGacPath.IsFixedPath);

            return value;
        }
    }
}
