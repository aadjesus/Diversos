//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : ContentItem.cs
// Author  : Eric Woodruff
// Updated : 02/23/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing a content item that can be added
// to the help file that is not part of the XML comments (i.e. additional
// HTML pages, images, replacement stylesheets, etc).
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
// 1.2.0.0  09/06/2006  EFW  Added IsDefaultPage property
// 1.3.0.0  09/19/2006  EFW  Removed IsDefaultPage and ContentDescription
// 1.3.4.0  01/01/2007  EFW  Converted source path property to FilePath object
// 1.4.0.0  02/23/2007  EFW  Added Exclude option
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
    /// This represents a content item that can be added to the help file that
    /// is not part of the XML comments (i.e. additional HTML pages, images,
    /// replacement stylesheets, etc).
    /// </summary>
    [DefaultProperty("SourcePath")]
    public class ContentItem
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private FilePath sourcePath;
        private string destPath;
        private bool excludeItems;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to get or set the source path to the additional
        /// content.
        /// </summary>
        /// <value>This can be a reference to a single file or a wildcard
        /// to include multiple files.</value>
        [Category("Content"), Description("The path to the additional " +
          "content.  This can be a single file or a wildcard to include " +
          "multiple files."),
          Editor(typeof(FilePathObjectEditor), typeof(UITypeEditor)),
          RefreshProperties(RefreshProperties.All),
          FileDialog("Select the additional content",
            "HTML files (*.htm, *.html, *.css)|*.htm;*.html;*.css|" +
            "Image files (*.gif, *.jpg, *.jpe*, *.png, *.bmp)|*.gif;*.jpg;*.jpe*;*.png;*.bmp|" +
            "All Files (*.*)|*.*", FileDialogType.FileOpen)]
        public FilePath SourcePath
        {
            get { return sourcePath; }
            set
            {
                if(value == null || value.Path.Length == 0)
                    throw new BuilderException("The source path cannot be blank");

                sourcePath = value;

                if(sourcePath.Path.EndsWith(@"\"))
                    sourcePath.Path += "*.*";
            }
        }

        /// <summary>
        /// This is used to get or set the destination path for the content
        /// files (their relative location within the help file).
        /// </summary>
        /// <value>This should always be a relative path.  If not specified,
        /// the content will be placed in the root of the help project build
        /// folder.  If specified, the folder will be relative to the help
        /// project build folder.  Also, if set to a non-empty path,
        /// <see cref="ExcludeItems"/> is automatically set to false.</value>
        [Category("Content"), Description("The destination path for the " +
          "content files (their relative location within the help file). " +
          "If not set, the content is placed in the root of the help " +
          "project's build folder.  If specified, the folder will be " +
          "relative to the help project's build folder."),
          RefreshProperties(RefreshProperties.All), DefaultValue("")]
        public string DestinationPath
        {
            get { return destPath; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    destPath = String.Empty;
                else
                {
                    destPath = value.Trim();
                    excludeItems = false;

                    // Make it relative to the output folder
                    if(destPath[1] == ':')
                        destPath = destPath.Substring(2);

                    while(destPath[0] == '.' || destPath[0] == '\\')
                        destPath = destPath.Substring(1);

                    if(!destPath.EndsWith(@"\"))
                        destPath += @"\";
                }
            }
        }

        /// <summary>
        /// This is used to get or set whether or not to exclude rather than
        /// include items in the specified source path.
        /// </summary>
        /// <value>If false (the default), files will be copied to the
        /// destination folder.  If set to true, files and folders matching
        /// the <see cref="SourcePath"/> wildcard will be ignored and are not
        /// copied.  The <see cref="DestinationPath"/> property is ignored.
        /// When set to true, <see cref="DestinationPath"/> is automatically
        /// cleared.</value>
        [Category("Content"), Description("If set to true, all files and " +
          "folders matching the source path wildcard will be ignored and " +
          "will not be copied."), RefreshProperties(RefreshProperties.All),
          DefaultValue(false)]
        public bool ExcludeItems
        {
            get { return excludeItems; }
            set
            {
                excludeItems = value;

                if(excludeItems)
                    this.DestinationPath = null;
            }
        }

        /// <summary>
        /// This returns a description of the entry suitable for display in a
        /// bound list control.
        /// </summary>
        [Category("Info"), Description("Description and/or source/dest")]
        public string ListDescription
        {
            get
            {
                if(excludeItems)
                    return String.Format(CultureInfo.InvariantCulture,
                        "Exclude {0}", sourcePath.PersistablePath);

                return String.Format(CultureInfo.CurrentCulture,
                    "{0} -> {1}", sourcePath.PersistablePath,
                    (destPath.Length == 0) ? @".\" : destPath);
            }
        }
        #endregion

        #region Designer methods
        //=====================================================================
        // Designer methods

        /// <summary>
        /// This is used to see if the <see cref="SourcePath"/> property
        /// should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.  This property cannot be reset
        /// as it should always have a value.</returns>
        private bool ShouldSerializeSourcePath()
        {
            return (this.SourcePath.Path.Length != 0);
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentItem()
        {
            // Just an example by default
            sourcePath = new FilePath(@"Images\*.*");
            destPath = String.Empty;
        }

        /// <summary>
        /// This is used to save the content item information to the
        /// project file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            xw.WriteStartElement("contentItem");
            xw.WriteAttributeString("sourcePath", sourcePath.PersistablePath);
            xw.WriteAttributeString("destPath", destPath);
            xw.WriteAttributeString("excludeItems", excludeItems.ToString());
            xw.WriteEndElement();
        }

        /// <summary>
        /// This is used to load the content item information from the
        /// project file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            string src = xr.GetAttribute("sourcePath");

            this.SourcePath = new FilePath(src, Path.IsPathRooted(src));
            this.DestinationPath = xr.GetAttribute("destPath");
            this.ExcludeItems = Convert.ToBoolean(xr.GetAttribute(
                "excludeItems"), CultureInfo.InvariantCulture);
        }
    }
}
