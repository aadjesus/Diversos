//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : FileFolderGacPath.cs
// Author  : Eric Woodruff
// Updated : 01/20/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class used to represent a file path, a folder path, or
// a GAC reference.  Support is included for treating the path as fixed or
// relative and for expanding environment variables in the path name.
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
// 1.3.4.0  12/31/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;

using IOPath = System.IO.Path;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This class is used to represent a folder path.  Support is included for
    /// treating the path as fixed or relative and for expanding environment
    /// variables in the path name.
    /// </summary>
    [Serializable, Editor(typeof(FilePathObjectEditor), typeof(UITypeEditor)),
      TypeConverter(typeof(FileFolderGacPathTypeConverter))]
    public class FileFolderGacPath : FilePath
    {
        private string gacPath;

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to get or set the file, folder, or GAC path.
        /// </summary>
        /// <value>When set, if the path is not rooted (a relative path),
        /// <see cref="FilePath.IsFixedPath"/> is set to false.  If rooted (an
        /// absolute path), it is not changed.  This property always returns a
        /// fully qualified path but without any environment variable
        /// expansions and terminated with a trailing backslash if needed.
        /// <p/>If set to a null or empty string, the folder path is cleared
        /// and is considered to be undefined.</value>
        [RefreshProperties(RefreshProperties.Repaint)]
        public override string Path
        {
            get
            {
                if(!String.IsNullOrEmpty(gacPath))
                    return gacPath;

                return base.Path;
            }
            set
            {
                // GAC paths can't be resolved like file and folder paths so
                // we handle them separately.
                if(value != null && value.StartsWith("GAC:"))
                    gacPath = value;
                else
                {
                    gacPath = null;
                    base.Path = value;
                }
            }
        }

        /// <summary>
        /// This is used to retrieve the file path in a format suitable for
        /// persisting to storage based on the current settings.
        /// </summary>
        [Browsable(false), Description("The file path as it should be " +
          "persisted for storage based on the current settings")]
        public override string PersistablePath
        {
            get
            {
                if(!String.IsNullOrEmpty(gacPath))
                    return gacPath;

                return base.PersistablePath;
            }
            set { this.Path = value; }
        }

        /// <summary>
        /// This read-only property can be used to determine whether or not
        /// the folder path exists.
        /// </summary>
        /// <value>This has no meaning for a GAC path.</value>
        [Description("This indicates whether or not the folder path " +
          "exists.  This has no meaning for a GAC item.")]
        public override bool Exists
        {
            get
            {
                // Can't say for sure on GAC paths without looking it up
                // and I don't want to do that here.
                if(!String.IsNullOrEmpty(gacPath))
                    return false;

                return Directory.Exists(this.ToString());
            }
        }
        #endregion

        #region Private designer methods
        //=====================================================================
        // Private designer methods

        /// <summary>
        /// This is used to prevent the Path property from showing as modified
        /// in the designer.
        /// </summary>
        /// <returns>Always returns false</returns>
        /// <remarks>The <see cref="Path"/> property is mainly for display
        /// purposes in the designer but can be used for making changes to
        /// the expanded path if needed.  The <see cref="FilePath.PersistablePath"/>
        /// property is used as the display value in the designer.</remarks>
        private bool ShouldSerializePath()
        {
            return false;
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Default constructor.  The folder path is undefined.
        /// </summary>
        /// <overloads>There are three overloads for the constructor.</overloads>
        public FileFolderGacPath() : base()
        {
        }

        /// <summary>
        /// Constructor.  Assign the specified path.
        /// </summary>
        /// <param name="path">A relative or absolute path.</param>
        /// <remarks>Unless <see cref="FilePath.IsFixedPath"/> is set to true,
        /// the path is always treated as a relative path.</remarks>
        public FileFolderGacPath(string path) : base(path)
        {
        }

        /// <summary>
        /// Constructor.  Assign the specified path and fixed setting.
        /// </summary>
        /// <param name="path">A relative or absolute path.</param>
        /// <param name="isFixed">True to treat the path as fixed, false
        /// to treat it as a relative path.</param>
        public FileFolderGacPath(string path, bool isFixed) : base(path, isFixed)
        {
        }

        /// <summary>
        /// Convert the file path to a string
        /// </summary>
        /// <returns>A fixed, relative, or GAC path based on the current
        /// settings.</returns>
        public override string ToString()
        {
            if(!String.IsNullOrEmpty(gacPath))
                return gacPath;

            return base.ToString();
        }
    }
}
