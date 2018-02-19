//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : FilePath.cs
// Author  : Eric Woodruff
// Updated : 02/21/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class used to represent a file path.  Support is
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
using System.Globalization;
using System.IO;

using IOPath = System.IO.Path;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This class is used to represent a file path.  Support is included for
    /// treating the path as fixed or relative and for expanding environment
    /// variables in the path name.
    /// </summary>
    [Serializable, DefaultProperty("Path"),
      Editor(typeof(FilePathObjectEditor), typeof(UITypeEditor)),
      TypeConverter(typeof(FilePathTypeConverter))]
    public class FilePath
    {
        #region Private data members
        //=====================================================================

        private static string basePath;     // Static base path

        private string filePath;            // Instance path
        private bool isFixedPath;
        #endregion

        #region Properties
        /// <summary>
        /// This is used to get or set the path to use.
        /// </summary>
        /// <value>When set, if the path is not rooted (a relative path),
        /// <see cref="IsFixedPath"/> is set to false.  If rooted (an absolute
        /// path), it is not changed.  This property always returns a fully
        /// qualified path but without any environment variable expansions.
        /// <p/>If set to a null or empty string, the file path is cleared and
        /// is considered to be undefined.</value>
        /// <example>
        /// <code lang="cs">
        /// FilePath path = new FilePath();
        ///
        /// // Set it to a relative path
        /// path.Path = @"..\..\Test.txt";
        ///
        /// // Set it to an absolute path
        /// path.Path = @"C:\My Documents\Info.doc";
        ///
        /// // Set it to a path based on an environment variable
        /// path.Path = @"%HOMEDRIVE%%HOMEPATH%\Favorites\*.*";
        /// </code>
        /// <code lang="vbnet">
        /// Dim path As New FilePath()
        ///
        /// ' Set it to a relative path
        /// path.Path = "..\..\Test.txt"
        ///
        /// ' Set it to an absolute path
        /// path.Path = "C:\My Documents\Info.doc"
        ///
        /// ' Set it to a path based on an environment variable
        /// path.Path = "%HOMEDRIVE%%HOMEPATH%\Favorites\*.*"
        /// </code>
        /// </example>
        [Description("The fully qualified path but without environment " +
          "variable expansions"), RefreshProperties(RefreshProperties.Repaint)]
        public virtual string Path
        {
            get { return filePath; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                {
                    filePath = String.Empty;    // Undefined
                    isFixedPath = false;
                }
                else
                {
                    filePath = value.Trim();

                    string tempPath = Environment.ExpandEnvironmentVariables(
                        filePath).Trim();

                    if(!IOPath.IsPathRooted(tempPath))
                    {
                        filePath = FilePath.RelativeToAbsolutePath(
                            basePath, filePath);
                        isFixedPath = false;
                    }
                }
            }
        }

        /// <summary>
        /// This is used to retrieve the file path in a format suitable for
        /// persisting to storage based on the current settings.
        /// </summary>
        /// <remarks>If <see cref="IsFixedPath"/> is true, an absolute path
        /// is always returned.  If false, the path is returned in a form that
        /// is relative to the path stored in the <see cref="BasePath"/>
        /// property.</remarks>
        [Browsable(false), DefaultValue(""), Description("The file path as " +
          "it should be persisted for storage based on the current settings")]
        public virtual string PersistablePath
        {
            get
            {
                // If fixed, blank, or starts with an environment variable,
                // return it as-is.
                if(isFixedPath || filePath.Length == 0 || filePath[0] == '%')
                    return filePath;

                return FilePath.AbsoluteToRelativePath(basePath, filePath);
            }
            set { this.Path = value; }
        }

        /// <summary>
        /// This read-only property can be used to determine whether or not
        /// the file path exists.
        /// </summary>
        [Description("This indicates whether or not the file path exists")]
        public virtual bool Exists
        {
            get
            {
                return File.Exists(this.ToString());
            }
        }

        /// <summary>
        /// This read-only property is used to display the fully qualified
        /// path with environment variable expansions in the designer.
        /// </summary>
        [Description("The fully qualified path with environment variables " +
            "expanded")]
        public string ExpandedPath
        {
            get { return this.ToString(); }
        }

        /// <summary>
        /// This is used to indicate whether or not the path will be treated
        /// as a relative or fixed path when converted retrieved via the
        /// <see cref="PersistablePath"/> property.
        /// </summary>
        /// <value>If true, the path is returned as a fixed path when
        /// retrieved.  If false, it is returned as a path relative to the
        /// current value of the <see cref="BasePath"/> property.</value>
        [DefaultValue(false), Description("If true, the path is returned " +
          "as an absolute path by the PersistablePath property.  If false, " +
          "it is returned as a path relative to the value of the static " +
          "BasePath property."), RefreshProperties(RefreshProperties.Repaint)]
        public bool IsFixedPath
        {
            get { return isFixedPath; }
            set { isFixedPath = value; }
        }
        #endregion

        #region Static properties and methods
        //=====================================================================
        // Static properties and methods

        /// <summary>
        /// This is used to set or get the base path to use as a reference
        /// when converting file paths between their fixed and relative form
        /// and vice versa.
        /// </summary>
        /// <value>If not set, the current working folder is used.  Any
        /// environment variables in the value are expanded.</value>
        /// <example>
        /// <code lang="cs">
        /// // Set the base path to a folder
        /// FilePath.BasePath = @"C:\ProjectFolder\My Project";
        ///
        /// // Set the base path to a folder under a folder defined in an
        /// // environment variable.
        /// FilePath.BasePath = @"%CommonProgramFiles%\Company\Product\";
        /// </code>
        /// <code lang="vbnet">
        /// ' Set the base path to a folder
        /// FilePath.BasePath = "C:\ProjectFolder\My Project"
        ///
        /// ' Set the base path to a folder under a folder defined in an
        /// ' environment variable.
        /// FilePath.BasePath = "%CommonProgramFiles%\Company\Product\"
        /// </code>
        /// </example>
        public static string BasePath
        {
            get
            {
                if(String.IsNullOrEmpty(basePath))
                    return Directory.GetCurrentDirectory();

                return basePath;
            }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    basePath = null;
                else
                {
                    basePath = Environment.ExpandEnvironmentVariables(
                        value).Trim();

                    // Make sure it is absolute
                    if(!IOPath.IsPathRooted(basePath))
                        basePath = IOPath.GetFullPath(basePath);
                }
            }
        }

        /// <summary>
        /// This is used to handle an implicit conversion from a
        /// <see cref="FilePath"/> object to a string.
        /// </summary>
        /// <param name="filePath">The <see cref="FilePath"/> to convert.</param>
        /// <returns>The file path as a relative or absolute path string
        /// based on its current settings.</returns>
        /// <example>
        /// <code lang="cs">
        /// FilePath filePath = new FilePath(@"%APPDATA%\TestApp\App.config");
        /// 
        /// // The FilePath object is automatically converted to a string
        /// // representing the expanded, fully qualified path.
        /// string pathString = filePath;
        /// </code>
        /// <code lang="vbnet">
        /// Dim filePath As New FilePath("%APPDATA%\TestApp\App.config")
        ///
        /// ' The FilePath object is automatically converted to a string
        /// ' representing the expanded, fully qualified path.
        /// Dim pathString As String = filePath
        /// </code>
        /// </example>
        public static implicit operator String(FilePath filePath)
        {
            if(filePath == null)
                return null;

            return filePath.ToString();
        }

        /// <summary>
        /// Overload for equal operator.
        /// </summary>
        /// <param name="firstPath">The first object to compare</param>
        /// <param name="secondPath">The second object to compare</param>
        /// <returns>True if equal, false if not.</returns>
        public static bool operator == (FilePath firstPath, FilePath secondPath)
        {
            if((object)firstPath == null && (object)secondPath == null)
                return true;

            if((object)firstPath == null)
                return false;

            return firstPath.Equals(secondPath);
        }

        /// <summary>
        /// Overload for not equal operator.
        /// </summary>
        /// <param name="firstPath">The first object to compare</param>
        /// <param name="secondPath">The second object to compare</param>
        /// <returns>True if not equal, false if they are.</returns>
        public static bool operator != (FilePath firstPath, FilePath secondPath)
        {
            if((object)firstPath == null && (object)secondPath == null)
                return false;

            if(firstPath == null)
                return true;

            return !firstPath.Equals(secondPath);
        }

        /// <summary>
        /// This returns the fully qualified path for the specified path.
        /// This version allows wildcards in the filename part if present.
        /// </summary>
        /// <param name="path">The path to expand</param>
        /// <returns>The fully qualified path name</returns>
        /// <remarks>The <b>System.IO.Path</b> version of
        /// <see cref="System.IO.Path.GetFullPath"/> will throw an exception
        /// if the path contains wildcard characters.  This version does not.
        /// </remarks>
        public static string GetFullPath(string path)
        {
            if(path == null)
                return null;

            if(!path.Contains("*") && !path.Contains("?"))
                return IOPath.GetFullPath(path);

            string fullPath = IOPath.GetFullPath(IOPath.GetDirectoryName(path));

            return fullPath + IOPath.DirectorySeparatorChar +
                IOPath.GetFileName(path);
        }

        /// <summary>
        /// This helper method can be used to convert an absolute path to one
        /// that is relative to the given base path.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <param name="absolutePath">An absolute path</param>
        /// <returns>A path to the given absolute path that is relative to the
        /// given base path.</returns>
        /// <remarks>If the base path is null or empty, the current working
        /// folder is used.</remarks>
        /// <example>
        /// <code lang="cs">
        /// string basePath = @"E:\DotNet20\CS\TestProject\Source";
        /// string absolutePath = @"E:\DotNet20\CS\TestProject\Doc\Help.html";
        /// 
        /// string relativePath = FilePath.AbsoluteToRelativePath(basePath,
        ///     absolutePath);
        /// 
        /// Console.WriteLine(relativePath);
        /// 
        /// // Results in: ..\Doc\Help.html
        /// </code>
        /// <code lang="vbnet">
        /// Dim basePath As String = "E:\DotNet20\CS\TestProject\Source"
        /// Dim absolutePath As String = "E:\DotNet20\CS\TestProject\Doc\Help.html"
        /// 
        /// Dim relativePath As String = _
        ///     FilePath.AbsoluteToRelativePath(basePath, absolutePath);
        /// 
        /// Console.WriteLine(relativePath)
        /// 
        /// ' Results in: ..\Doc\Help.html
        /// </code>
        /// </example>
        public static string AbsoluteToRelativePath(string basePath,
          string absolutePath)
        {
            bool hasBackslash = false;
            string relPath;
            int minLength, idx;

            // If not specified, use the current folder as the base path
            if(basePath == null || basePath.Trim().Length == 0)
                basePath = Directory.GetCurrentDirectory();
            else
                basePath = IOPath.GetFullPath(basePath);

            if(absolutePath == null)
                absolutePath = String.Empty;

            // Just in case, make sure the path is absolute
            if(!IOPath.IsPathRooted(absolutePath))
                absolutePath = FilePath.GetFullPath(absolutePath);

            // Remove trailing backslashes for comparison
            if(FolderPath.IsPathTerminated(basePath))
                basePath = basePath.Substring(0, basePath.Length - 1);

            if(FolderPath.IsPathTerminated(absolutePath))
            {
                absolutePath = absolutePath.Substring(0, absolutePath.Length - 1);
                hasBackslash = true;
            }

            // Split the paths into their component parts
            char[] separators = { IOPath.DirectorySeparatorChar,
                IOPath.AltDirectorySeparatorChar, IOPath.VolumeSeparatorChar };
            string[] baseParts = basePath.Split(separators);
            string[] absParts = absolutePath.Split(separators);

            // Find the common base path
            minLength = Math.Min(baseParts.Length, absParts.Length);

            for(idx = 0; idx < minLength; idx++)
                if(String.Compare(baseParts[idx], absParts[idx], true,
                  CultureInfo.InvariantCulture) != 0)
                    break;

            // Use the absolute path if there's nothing in common (i.e. they
            // are on different drives or network shares.
            if(idx == 0)
                relPath = absolutePath;
            else
            {
                // If equal to the base path, it doesn't have to go anywhere.
                // Otherwise, work up from the base path to the common root.
                if(idx == baseParts.Length)
                    relPath = "." + IOPath.DirectorySeparatorChar;
                else
                    relPath = new String(' ', baseParts.Length - idx).Replace(
                        " ", ".." + IOPath.DirectorySeparatorChar);

                // And finally, add the path from the common root to the absolute
                // path.
                relPath += String.Join(IOPath.DirectorySeparatorChar.ToString(),
                    absParts, idx, absParts.Length - idx);
            }

            return (hasBackslash) ? FolderPath.TerminatePath(relPath) : relPath;
        }

        /// <summary>
        /// This helper method can be used to convert a relative path to an
        /// absolute path based on the given base path.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <param name="relativePath">A relative path</param>
        /// <returns>An absolute path</returns>
        /// <remarks>If the base path is null or empty, the current working
        /// folder is used.</remarks>
        /// <example>
        /// <code lang="cs">
        /// string basePath = @"E:\DotNet20\CS\TestProject\Source";
        /// string relativePath = @"..\Doc\Help.html";
        /// 
        /// string absolutePath = FilePath.RelativeToAbsolutePath(basePath,
        ///     relativePath);
        /// 
        /// Console.WriteLine(absolutePath);
        /// 
        /// // Results in: E:\DotNet20\CS\TestProject\Doc\Help.html
        /// </code>
        /// <code lang="vbnet">
        /// Dim basePath As String = "E:\DotNet20\CS\TestProject\Source"
        /// Dim relativePath As String = "..\Doc\Help.html"
        /// 
        /// Dim absolutePath As String = _
        ///     FilePath.RelativeToAbsolutePath(basePath, relativePath);
        /// 
        /// Console.WriteLine(absolutePath)
        /// 
        /// ' Results in: E:\DotNet20\CS\TestProject\Doc\Help.html
        /// </code>
        /// </example>
        public static string RelativeToAbsolutePath(string basePath,
          string relativePath)
        {
            int idx;

            // If blank return the base path
            if(String.IsNullOrEmpty(relativePath))
                return basePath;

            // Don't bother if already absolute
            if(IOPath.IsPathRooted(relativePath))
                return relativePath;

            // If not specified, use the current folder as the base path
            if(basePath == null || basePath.Trim().Length == 0)
                basePath = Directory.GetCurrentDirectory();
            else
                basePath = IOPath.GetFullPath(basePath);

            if(relativePath == ".")
                relativePath = String.Empty;

            // Remove ".\" or "./" if it's there
            if(relativePath.Length > 1 && relativePath[0] == '.' &&
              (relativePath[1] == IOPath.DirectorySeparatorChar ||
              relativePath[1] == IOPath.AltDirectorySeparatorChar))
                relativePath = relativePath.Substring(2);

            // Split the paths into their component parts
            string[] baseParts = basePath.Split(IOPath.DirectorySeparatorChar);
            string[] relParts = relativePath.Split(
                IOPath.DirectorySeparatorChar);

            // Figure out how far to move up from the relative path
            for(idx = 0; idx < relParts.Length; ++idx)
                if(relParts[idx] != "..")
                    break;

            // If it's below the base path, just add it to the base path
            if(idx == 0)
                return FilePath.GetFullPath(basePath +
                    IOPath.DirectorySeparatorChar + relativePath);

            string absPath = String.Join(
                IOPath.DirectorySeparatorChar.ToString(), baseParts, 0,
                Math.Max(0, baseParts.Length - idx));

            absPath += IOPath.DirectorySeparatorChar + String.Join(
                IOPath.DirectorySeparatorChar.ToString(), relParts, idx,
                relParts.Length - idx);

            return FilePath.GetFullPath(absPath);
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
        /// the expanded path if needed.  The <see cref="PersistablePath"/>
        /// property is used as the display value in the designer.</remarks>
        private bool ShouldSerializePath()
        {
            return false;
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Default constructor.  The file path is undefined.
        /// </summary>
        /// <overloads>There are three overloads for the constructor.</overloads>
        public FilePath()
        {
            filePath = String.Empty;
        }

        /// <summary>
        /// Constructor.  Assign the specified path.
        /// </summary>
        /// <param name="path">A relative or absolute path.</param>
        /// <remarks>Unless <see cref="IsFixedPath"/> is set to true,
        /// the path is always treated as a relative path.</remarks>
        public FilePath(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Constructor.  Assign the specified path and fixed setting.
        /// </summary>
        /// <param name="path">A relative or absolute path.</param>
        /// <param name="isFixed">True to treat the path as fixed, false
        /// to treat it as a relative path.</param>
        public FilePath(string path, bool isFixed)
        {
            this.Path = path;
            this.IsFixedPath = isFixed;
        }

        /// <summary>
        /// Convert the file path to a string
        /// </summary>
        /// <returns>A fixed or relative path based on the current
        /// settings.</returns>
        public override string ToString()
        {
            return Environment.ExpandEnvironmentVariables(filePath);
        }

        /// <summary>
        /// Get a hash code for the file path object
        /// </summary>
        /// <returns>Returns the hash code for the file path object.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// This is overridden to allow proper comparison of file path objects.
        /// </summary>
        /// <param name="obj">The object to which this instance is
        /// compared.</param>
        /// <returns>Returns true if the object equals this instance, false
        /// if it does not.</returns>
        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != this.GetType())
                return false;

            FilePath otherPath = obj as FilePath;

            return (this.Path == otherPath.Path &&
              this.IsFixedPath == otherPath.IsFixedPath);
        }
    }
}
