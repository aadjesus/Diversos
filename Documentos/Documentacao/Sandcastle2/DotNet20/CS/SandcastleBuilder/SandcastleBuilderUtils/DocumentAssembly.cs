//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : DocumentAssembly.cs
// Author  : Eric Woodruff
// Updated : 01/01/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing an assembly and an XML comments
// file used to build the help file.
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
// 1.3.2.0  11/10/2006  EFW  Added CommentsOnly property.
// 1.3.4.0  12/31/2006  EFW  Converted path properties to FilePath objects
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
    /// This represents an assembly and an XML file to use for building the
    /// help file.
    /// </summary>
    [DefaultProperty("AssemblyPath")]
    public class DocumentAssembly
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private FilePath assemblyPath, xmlCommentsPath;
        private bool commentsOnly;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to set or get the assembly file path
        /// </summary>
        [Category("File"), Description("The assembly to document"),
          DefaultValue(typeof(FilePath), ""),
          Editor(typeof(FilePathObjectEditor), typeof(UITypeEditor)),
          RefreshProperties(RefreshProperties.All),
          FileDialog("Select the assembly to document",
            "Library and Executable Files (*.dll, *.exe)|*.dll;*.exe|" +
            "Library Files (*.dll)|*.dll|Executable Files (*.exe)|*.exe|" +
            "All Files (*.*)|*.*", FileDialogType.FileOpen)]
        public FilePath AssemblyPath
        {
            get { return assemblyPath; }
            set
            {
                string comments;

                if(value == null)
                    assemblyPath = new FilePath();
                else
                {
                    assemblyPath = value;

                    if(assemblyPath.Path.Length != 0)
                    {
                        commentsOnly = false;

                        // Default the comments file if not yet specified
                        if(xmlCommentsPath.Path.Length == 0)
                        {
                            comments = Path.ChangeExtension(assemblyPath,
                                ".xml");

                            if(File.Exists(comments))
                                xmlCommentsPath = new FilePath(comments,
                                    assemblyPath.IsFixedPath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is used to set or get the XML comments file path
        /// </summary>
        [Category("File"), Description("The XML comments file for the assembly"),
          DefaultValue(typeof(FilePath), ""),
          Editor(typeof(FilePathObjectEditor), typeof(UITypeEditor)),
          RefreshProperties(RefreshProperties.All),
          FileDialog("Select the XML comments file for the assembly",
            "XML Comments Files (*.xml)|*.xml|All Files (*.*)|*.*",
            FileDialogType.FileOpen)]
        public FilePath XmlCommentsPath
        {
            get { return xmlCommentsPath; }
            set
            {
                if(value == null)
                    xmlCommentsPath = new FilePath();
                else
                {
                    xmlCommentsPath = value;

                    // Default the assembly file if not yet specified
                    if(xmlCommentsPath.Path.Length != 0 &&
                      assemblyPath.Path.Length == 0)
                    {
                        string asm = Path.ChangeExtension(xmlCommentsPath,
                            ".dll");

                        if(File.Exists(asm))
                            assemblyPath = new FilePath(asm,
                                xmlCommentsPath.IsFixedPath);
                        else
                        {
                            asm = Path.ChangeExtension(xmlCommentsPath,
                                ".exe");

                            if(File.Exists(asm))
                                assemblyPath = new FilePath(asm,
                                    xmlCommentsPath.IsFixedPath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set or get whether or not this entry represents an XML comments
        /// file without an associated assembly.
        /// </summary>
        /// <value>Set to false if the comments file has an associated
        /// assembly (the default) or true if it only contains comments (i.e
        /// for the project or namespaces).</value>
        [Category("Info"), Description("Comments file only.  Set to true if " +
          "the comments file has no associated assembly (i.e. project or " +
          "namespace comments."), DefaultValue(false)]
        public bool CommentsOnly
        {
            get { return commentsOnly; }
            set
            {
                commentsOnly = value;

                if(commentsOnly)
                    this.AssemblyPath = null;
                else
                    if(this.AssemblyPath.Path.Length == 0)
                        this.AssemblyPath = new FilePath("Unknown.dll");
            }
        }

        /// <summary>
        /// This returns a description of the entry suitable for display in a
        /// bound list control.
        /// </summary>
        [Category("Info"), Description("The assembly and comments file")]
        public string AssemblyDescription
        {
            get
            {
                if(commentsOnly)
                    return Path.GetFileName(xmlCommentsPath) +
                        " (Comments only)";

                return String.Format(CultureInfo.CurrentCulture, "{0}{1}{2}",
                    Path.GetFileName(assemblyPath),
                    (assemblyPath.Path.Length == 0 ||
                        xmlCommentsPath.Path.Length == 0) ? "" : ", ",
                    Path.GetFileName(xmlCommentsPath));
            }
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentAssembly()
        {
            assemblyPath = new FilePath();
            xmlCommentsPath = new FilePath();
        }

        /// <summary>
        /// This is used to save the document assembly information to
        /// the project file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            xw.WriteStartElement("assembly");
            xw.WriteAttributeString("assemblyPath",
                assemblyPath.PersistablePath);
            xw.WriteAttributeString("xmlCommentsPath",
                xmlCommentsPath.PersistablePath);
            xw.WriteAttributeString("commentsOnly", commentsOnly.ToString());
            xw.WriteEndElement();
        }

        /// <summary>
        /// This is used to load the document assembly information from the
        /// project file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            Version noCOVersion = new Version(1, 3, 1, 1);

            string asmPath = xr.GetAttribute("assemblyPath"),
                   xmlPath = xr.GetAttribute("xmlCommentsPath");

            bool asmPathIsFixed = Path.IsPathRooted(asmPath),
                 xmlPathIsFixed = Path.IsPathRooted(xmlPath);

            if(schemaVersion > noCOVersion)
                this.CommentsOnly = Convert.ToBoolean(xr.GetAttribute(
                    "commentsOnly"), CultureInfo.InvariantCulture);

            if(!this.CommentsOnly && asmPath.Trim().Length == 0)
                asmPath = "Unknown.dll";

            if(xmlPath.Trim().Length == 0)
                xmlPath = "Unknown.xml";

            this.AssemblyPath = new FilePath(asmPath, asmPathIsFixed);
            this.XmlCommentsPath = new FilePath(xmlPath, xmlPathIsFixed);
        }

        /// <summary>
        /// See if specified item equals this one
        /// </summary>
        /// <param name="obj">The object to compare to this one</param>
        /// <returns>True if equal, false if not</returns>
        public override bool Equals(object obj)
        {
            DocumentAssembly da = obj as DocumentAssembly;

            if(da == null)
                return false;

            return (this == da || (this.AssemblyPath == da.AssemblyPath &&
                this.XmlCommentsPath == da.XmlCommentsPath));
        }

        /// <summary>
        /// Get a hash code for this item
        /// </summary>
        /// <returns>Returns the hash code for the assembly path and
        /// XML comments path.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Return a string representation of the item
        /// </summary>
        /// <returns>Returns the assembly path and XML comments path separated
        /// by a comma.</returns>
        public override string ToString()
        {
            return this.AssemblyPath + ", " + this.XmlCommentsPath;
        }
    }
}
