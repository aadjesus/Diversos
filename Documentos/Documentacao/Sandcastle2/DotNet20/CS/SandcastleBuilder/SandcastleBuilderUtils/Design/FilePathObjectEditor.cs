//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : FilePathObjectEditor.cs
// Author  : Eric Woodruff
// Updated : 12/29/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type editor that can display a file dialog to allow
// selection of a file path at design time.  It is used in conjunction with
// the FileDialogAttribute to specify the file dialog title, filter, and type.
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
// 1.3.4.0  12/29/2006  EFW  Created the code
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
    /// This is a type editor that can display a file dialog to allow
    /// selection of a file path at design time for a <see cref="FilePath"/>
    /// object.
    /// </summary>
    /// <remarks>It is used in conjunction with the
    /// <see cref="FileDialogAttribute" /> to specify the file dialog title,
    /// filter, and type</remarks>
    public class FilePathObjectEditor : FilePathStringEditor
    {
        /// <summary>
        /// This is overridden to edit the value using a file dialog.
        /// </summary>
        /// <param name="context">The descriptor context</param>
        /// <param name="provider">The provider</param>
        /// <param name="value">The file path as an object</param>
        /// <returns>The selected file path as an object</returns>
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
          IServiceProvider provider, object value)
        {
            FilePath filePath = value as FilePath;

            if(filePath == null)
                return base.EditValue(context, provider, value);

            string path = (string)base.EditValue(context, provider,
                filePath.ToString());

            if(path != filePath.Path)
                value = new FilePath(path, filePath.IsFixedPath);

            return value;
        }
    }
}
