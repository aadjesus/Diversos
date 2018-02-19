//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : FileDialogAttribute.cs
// Author  : Eric Woodruff
// Updated : 08/10/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains an attribute class that is used to associate file dialog
// parameters with a class property for use in editing it in a property grid.
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
//=============================================================================

using System;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// The type of file dialog to display
    /// </summary>
    public enum FileDialogType
    {
        /// <summary>Display a File Open dialog box</summary>
        FileOpen,
        /// <summary>Display a File Save dialog box</summary>
        FileSave
    }

    /// <summary>
    /// This attribute is used to associate file dialog parameters with a
    /// class property for use in editing it in a property grid.
    /// </summary>
    /// <seealso cref="FilePathStringEditor" />
    /// <seealso cref="FilePathObjectEditor" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FileDialogAttribute : System.Attribute
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private string dlgTitle, dlgFilter;
        private FileDialogType dlgType;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to get the file dialog title
        /// </summary>
        public string Title
        {
            get { return dlgTitle; }
        }

        /// <summary>
        /// This is used to get the file dialog filter
        /// </summary>
        public string Filter
        {
            get { return dlgFilter; }
        }

        /// <summary>
        /// This is used to get the file dialog type
        /// </summary>
        public FileDialogType DialogType
        {
            get { return dlgType; }
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">The title for the file dialog</param>
        /// <param name="filter">The filter to use for the file dialog</param>
        /// <param name="dialogType">The type of file dialog to display</param>
        /// <overloads>There are two overloads for the constructor.</overloads>
        public FileDialogAttribute(string title, string filter,
          FileDialogType dialogType)
        {
            dlgTitle = title;
            dlgFilter = filter;
            dlgType = dialogType;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">The title for the file dialog</param>
        /// <remarks>The filter defaults to "All Files (*.*)|*.*" and
        /// the dialog type defaults to <b>FileOpen</b></remarks>
        public FileDialogAttribute(string title)
        {
            dlgTitle = title;
            dlgFilter = "All Files (*.*)|*.*";
        }
    }
}
