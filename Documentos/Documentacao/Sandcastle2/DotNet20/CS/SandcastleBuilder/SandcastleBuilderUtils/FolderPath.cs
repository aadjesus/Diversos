//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : FolderPath.cs
// Author  : Eric Woodruff
// Updated : 02/21/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class used to represent a folder path.  Support is
// included for treating the path as fixed or relative and for expanding
// environment variables in the path name.
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

using IOPath = System.IO.Path;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This class is used to represent a folder path.  Support is included for
    /// treating the path as fixed or relative and for expanding environment
    /// variables in the path name.
    /// </summary>
    [Serializable, Editor(typeof(FolderPathObjectEditor), typeof(UITypeEditor)),
      TypeConverter(typeof(FolderPathTypeConverter))]
    public class FolderPath : FilePath
    {
        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to get or set the path.
        /// </summary>
        /// <value>When set, if the path is not rooted (a relative path),
        /// <see cref="FilePath.IsFixedPath"/> is set to false.  If rooted (an
        /// absolute path), it is not changed.  This property always returns a
        /// fully qualified path but without any environment variable
        /// expansions and terminated with a trailing backslash if needed.
        /// <p/>If set to a null or empty string, the folder path is cleared
        /// and is considered to be undefined.</value>
        /// <example>
        /// <code lang="cs">
        /// FolderPath path = new FolderPath();
        /// 
        /// // Set it to a relative path
        /// path.Path = @"..\..\ProjectFolder";
        /// 
        /// // Set it to an absolute path
        /// path.Path = @"C:\My Documents\ProjectDocs\";
        /// 
        /// // Set it to a path based on an environment variable
        /// path.Path = @"%HOMEDRIVE%%HOMEPATH%\Favorites\";
        /// </code>
        /// <code lang="vbnet">
        /// Dim path As New FilePath()
        /// 
        /// ' Set it to a relative path
        /// path.Path = "..\..\ProjectFolder"
        /// 
        /// ' Set it to an absolute path
        /// path.Path = "C:\My Documents\ProjectDocs\"
        /// 
        /// ' Set it to a path based on an environment variable
        /// path.Path = "%HOMEDRIVE%%HOMEPATH%\Favorites\"
        /// </code>
        /// </example>
        [RefreshProperties(RefreshProperties.Repaint)]
        public override string Path
        {
            get { return base.Path; }
            set
            {
                base.Path = value;

                if(base.Path.Length != 0 &&
                  !FolderPath.IsPathTerminated(base.Path))
                    base.Path += IOPath.DirectorySeparatorChar.ToString();
            }
        }

        /// <summary>
        /// This read-only property can be used to determine whether or not
        /// the folder path exists.
        /// </summary>
        [Description("This indicates whether or not the folder path exists")]
        public override bool Exists
        {
            get
            {
                return Directory.Exists(this.ToString());
            }
        }
        #endregion

        #region Static helper methods
        //=====================================================================
        // Static helper methods

        /// <summary>
        /// This can be used to find out if a path is terminated with a
        /// trailing backslash.
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>Returns true if it is, false if it is not.</returns>
        public static bool IsPathTerminated(string path)
        {
            path = Environment.ExpandEnvironmentVariables(path);

            return (path.EndsWith(IOPath.DirectorySeparatorChar.ToString()) ||
                path.EndsWith(IOPath.AltDirectorySeparatorChar.ToString()));
        }

        /// <summary>
        /// This can be used to ensure that a path is terminated with a
        /// trailing backslash.
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>The path with a trailing backslash added if necessary.</returns>
        public static string TerminatePath(string path)
        {
            string expandedPath = Environment.ExpandEnvironmentVariables(path);

            if(!expandedPath.EndsWith(IOPath.DirectorySeparatorChar.ToString()) &&
              !expandedPath.EndsWith(IOPath.AltDirectorySeparatorChar.ToString()))
                path += IOPath.DirectorySeparatorChar.ToString();

            return path;
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
        public FolderPath() : base()
        {
        }

        /// <summary>
        /// Constructor.  Assign the specified path.
        /// </summary>
        /// <param name="path">A relative or absolute path.</param>
        /// <remarks>Unless <see cref="FilePath.IsFixedPath"/> is set to true,
        /// the path is always treated as a relative path.</remarks>
        public FolderPath(string path) : base(path)
        {
        }

        /// <summary>
        /// Constructor.  Assign the specified path and fixed setting.
        /// </summary>
        /// <param name="path">A relative or absolute path.</param>
        /// <param name="isFixed">True to treat the path as fixed, false
        /// to treat it as a relative path.</param>
        public FolderPath(string path, bool isFixed) : base(path, isFixed)
        {
        }
    }
}
