//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : DependencyItem.cs
// Author  : Eric Woodruff
// Updated : 01/01/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing a dependency item that can be used
// by MRefBuilder to locate assembly dependencies for the assemblies being
// documented.
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
// 1.3.4.0  01/01/2007  EFW  Converted path to a FileFolderGacPath object
//=============================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This represents a dependency item that can be used by <b>MRefBuilder</b>
    /// to locate assembly dependencies for the assemblies being documented.
    /// </summary>
    public class DependencyItem
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private FileFolderGacPath depPath;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to set or path to the dependency
        /// </summary>
        /// <value>This can be a reference to a single file, a wildcard to
        /// include multiple files, or a GAC full name prefixed with "GAC:".
        /// GAC entries are resolved to an actual assembly at build-time.
        /// </value>
        [Category("Dependency"), Description("The path to the dependency " +
          "item.  This can be a single file, a wildcard to include " +
          "multiple files, or a GAC reference."),
          Editor(typeof(FileFolderGacPathObjectEditor), typeof(UITypeEditor)),
          RefreshProperties(RefreshProperties.All),
          FileDialog("Select the dependency item",
            "Library and Executable Files (*.dll, *.exe)|*.dll;*.exe|" +
            "Library Files (*.dll)|*.dll|Executable Files (*.exe)|*.exe|" +
            "All Files (*.*)|*.*", FileDialogType.FileOpen)]
        public FileFolderGacPath DependencyPath
        {
            get { return depPath; }
            set
            {
                if(value == null || value.Path.Trim().Length == 0)
                    throw new BuilderException(
                        "The dependency path cannot be blank");

                depPath = value;

                if(!depPath.Path.StartsWith("GAC:") &&
                  depPath.Path.EndsWith(@"\"))
                    depPath.Path += "*.*";
            }
        }
        #endregion

        #region Designer methods
        //=====================================================================
        // Designer methods

        /// <summary>
        /// This is used to see if the <see cref="DependencyPath"/> property
        /// should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.  This property cannot be reset
        /// as it should always have a value.</returns>
        private bool ShouldSerializeDependencyPath()
        {
            return (this.DependencyPath.Path.Length != 0);
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public DependencyItem()
        {
            // Just an example by default
            depPath = new FileFolderGacPath(@"dll\*.*");
        }

        /// <summary>
        /// This is used to save the dependency item information to the
        /// project file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            xw.WriteStartElement("dependencyItem");
            xw.WriteAttributeString("depPath", depPath.PersistablePath);
            xw.WriteEndElement();
        }

        /// <summary>
        /// This is used to load the dependency item information from the
        /// project file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            string dep = xr.GetAttribute("depPath");

            this.DependencyPath = new FileFolderGacPath(dep,
                (!dep.StartsWith("GAC:") && Path.IsPathRooted(dep)));
        }
    }
}
