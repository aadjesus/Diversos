//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : TocEntry.cs
// Author  : Eric Woodruff
// Updated : 03/09/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing a table of content entry.  This is
// used to build the table of content entries for additional content items.
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
// 1.3.0.0  09/17/2006  EFW  Created the code
// 1.3.3.1  12/08/2006  EFW  Added NeedsColorizing property
// 1.4.0.0  03/09/2007  EFW  Added support for <code source="file" /> tags
//=============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This represents a table of content entry.  This is used to build the
    /// table of content entries for additional content items.
    /// </summary>
    public class TocEntry : IComparable<TocEntry>
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private TocEntryCollection children;
        private string sourceFile, destFile, title;
        private bool isDefaultTopic, includePage, hasLinks, needsColorizing,
            hasProjectTags, hasCodeBlocks;
        private int sortOrder;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This returns the child table of content collection for this entry
        /// </summary>
        /// <value>If empty, this is a single item in the table of content.
        /// If it has children, they are listed below this one.  A file may
        /// or may not be associated with this entry if it is a root node.</value>
        public TocEntryCollection Children
        {
            get { return children; }
        }

        /// <summary>
        /// This is used to get or set the entry's source file path.
        /// </summary>
        public string SourceFile
        {
            get { return sourceFile; }
            set { sourceFile = value; }
        }

        /// <summary>
        /// This is used to get or set the entry's destination file path.
        /// </summary>
        public string DestinationFile
        {
            get { return destFile; }
            set { destFile = value; }
        }

        /// <summary>
        /// This is used to get or set the entry's title in the table of
        /// content.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// This is used to get or set whether or not the page will appear in
        /// the table of content.
        /// </summary>
        /// <remarks>For root entries that have children, the node will appear
        /// in the table of content but will have no page associated with it.
        /// The other options such as <see cref="SortOrder" /> will still have
        /// an effect.</remarks>
        public bool IncludePage
        {
            get { return includePage; }
            set { includePage = value; }
        }

        /// <summary>
        /// This is used to get or set whether or not the item is the default
        /// topic for the help file.
        /// </summary>
        public bool IsDefaultTopic
        {
            get { return isDefaultTopic; }
            set { isDefaultTopic = value; }
        }

        /// <summary>
        /// This is used to get or set whether or not the topic has links that
        /// need to be resolved when the file is copied.
        /// </summary>
        public bool HasLinks
        {
            get { return hasLinks; }
            set { hasLinks = value; }
        }

        /// <summary>
        /// This is used to get or set whether or not the topic has
        /// <c>&lt;pre&gt;</c> blocks that have a <c>lang</c>
        /// attribute to indicate that they should be colorized.
        /// </summary>
        public bool NeedsColorizing
        {
            get { return needsColorizing; }
            set { needsColorizing = value; }
        }

        /// <summary>
        /// This is used to get or set whether or not the topic has
        /// <c>&lt;code /&gt;</c> blocks that need expanding.
        /// </summary>
        public bool HasCodeBlocks
        {
            get { return hasCodeBlocks; }
            set { hasCodeBlocks = value; }
        }

        /// <summary>
        /// This is used to get or set whether or not the topic has tags
        /// that should be resolved to project options.
        /// </summary>
        public bool HasProjectTags
        {
            get { return hasProjectTags; }
            set { hasProjectTags = value; }
        }

        /// <summary>
        /// This is used to get or set the sort order for the entry within
        /// its group.
        /// </summary>
        /// <value>Entries with identical sort order values will sort by
        /// title as well.  Items with not specific sort order will sort
        /// below those with a defined sort order.</value>
        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
        #endregion

        #region IComparable<TocEntry> Members
        /// <summary>
        /// Compares this instance to another instance and returns an
        /// indication of their relative values.
        /// </summary>
        /// <param name="other">A TocEntry object to compare</param>
        /// <returns>Returns -1 if this instance is less than the
        /// value, 0 if they are equal, or 1 if this instance is
        /// greater than the value or the value is null.</returns>
        /// <remarks>The <see cref="SortOrder"/> property is compared first.
        /// If equal, the <see cref="Title"/> property is used.</remarks>
        public int CompareTo(TocEntry other)
        {
            if(other == null)
                return 1;

            if(this.SortOrder < other.SortOrder)
                return -1;

            if(this.SortOrder > other.SortOrder)
                return 1;

            return String.Compare(this.Title, other.Title, false,
                CultureInfo.CurrentCulture);
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public TocEntry()
        {
            includePage = true;
            sortOrder = Int32.MaxValue;
            children = new TocEntryCollection();
        }

        /// <summary>
        /// Convert the table of content entry and its children to a string
        /// </summary>
        /// <returns>The entries in HTML 1.x help format</returns>
        public override string ToString()
        {
            return this.ToString(HelpFileFormat.HtmlHelp1x);
        }

        /// <summary>
        /// Convert the table of content entry and its children to a string
        /// in the specified help file format.
        /// </summary>
        /// <param name="format">The help file format to use</param>
        /// <returns>The entries in specified help format</returns>
        /// <exception cref="ArgumentException">This is thrown if the
        /// format is not <b>HtmlHelp1x</b> or <b>HtmlHelp2x</b>.</exception>
        public string ToString(HelpFileFormat format)
        {
            if(format != HelpFileFormat.HtmlHelp1x &&
              format != HelpFileFormat.HtmlHelp2x)
                throw new ArgumentException("The format specified must be " +
                    "HtmlHelp1x or HtmlHelp2x only", "format");

            StringBuilder sb = new StringBuilder(1024);
            this.ConvertToString(format, sb);
            return sb.ToString();
        }

        /// <summary>
        /// This is used to convert the collection to a string and append it
        /// to the specified string builder.
        /// </summary>
        /// <param name="format">The help file format to use</param>
        /// <param name="sb">The string builder to which the information is
        /// appended.</param>
        internal void ConvertToString(HelpFileFormat format, StringBuilder sb)
        {
            if(format == HelpFileFormat.HtmlHelp1x)
            {
                if(children.Count == 0)
                    sb.AppendFormat("<LI><OBJECT type=\"text/sitemap\">" +
                        "\r\n<param name=\"Name\" value=\"{0}\">\r\n" +
                        "<param name=\"Local\" value=\"{1}\">\r\n" +
                        "</OBJECT></LI>\r\n", HttpUtility.HtmlEncode(title),
                        destFile);
                else
                {
                    if(String.IsNullOrEmpty(destFile))
                        sb.AppendFormat("<LI><OBJECT type=\"text/sitemap\">" +
                            "\r\n<param name=\"Name\" value=\"{0}\">\r\n" +
                            "</OBJECT></LI>\r\n",
                            HttpUtility.HtmlEncode(title));
                    else
                        sb.AppendFormat("<LI><OBJECT type=\"text/sitemap\">" +
                            "\r\n<param name=\"Name\" value=\"{0}\">\r\n" +
                            "<param name=\"Local\" value=\"{1}\">\r\n" +
                            "</OBJECT></LI>\r\n",
                            HttpUtility.HtmlEncode(title), destFile);

                    sb.Append("<UL>\r\n");
                    children.ConvertToString(format, sb);
                    sb.Append("</UL>\r\n");
                }
            }
            else
            {
                // Use a GUID to uniquely identify the entries
                string guid = Guid.NewGuid().ToString();

                if(children.Count == 0)
                    sb.AppendFormat("<HelpTOCNode Id=\"{0}\" Url=\"{1}\" />\r\n",
                        guid, destFile);
                else
                {
                    // If there is no file for the root node, define the title
                    // property instead.
                    if(String.IsNullOrEmpty(destFile))
                        sb.AppendFormat("<HelpTOCNode Id=\"{0}\" Title=\"{1}\">\r\n",
                            guid, HttpUtility.HtmlEncode(title));
                    else
                        sb.AppendFormat("<HelpTOCNode Id=\"{0}\" Url=\"{1}\">\r\n",
                            guid, destFile);

                    children.ConvertToString(format, sb);
                    sb.Append("</HelpTOCNode>\r\n");
                }
            }
        }
    }
}
