//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : NamespaceSummaryItem.cs
// Author  : Eric Woodruff
// Updated : 02/23/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing a namespace summary item that can
// be used to add comments to a namespace in the help file or exclude it
// completely from the help file.
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
// 1.2.0.0  08/02/2006  EFW  Created the code
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
    /// This represents a a namespace summary item that can be used to add
    /// comments to a namespace in the help file or exclude it completely
    /// from the help file.
    /// </summary>
    [DefaultProperty("Summary")]
    public class NamespaceSummaryItem
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private bool isDocumented;
        private string name, summary;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to get or set whether or not the namespace is included
        /// in the help file.
        /// </summary>
        [Category("Summary"), Description("If true, the namespace and its " +
          "contents will appear in the help file.  If false, it is excluded."),
          DefaultValue(true)]
        public bool IsDocumented
        {
            get { return isDocumented; }
            set { isDocumented = value; }
        }

        /// <summary>
        /// This is used to get or set the namespace summary comments
        /// </summary>
        [Category("Summary"), Description("The summary comments for the " +
          "namespace."), DefaultValue("")]
        public string Summary
        {
            get { return summary; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    summary = String.Empty;
                else
                    summary = value.Trim();
            }
        }

        /// <summary>
        /// This read-only property is used to get the namespace name
        /// </summary>
        [Category("Summary"), Description("The namespace's name."),
          DefaultValue("(global)")]
        public string Name
        {
            get { return (name.Length == 0) ? "(global)" : name; }
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public NamespaceSummaryItem() : this("")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemName">The namespace's name</param>
        public NamespaceSummaryItem(string itemName)
        {
            name = itemName;
            summary = String.Empty;

            // The global namespace is not documented by default
            isDocumented = (itemName != null && itemName.Length != 0);
        }

        /// <summary>
        /// This is used to save the namespace summary item information to the
        /// project file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            xw.WriteStartElement("namespaceSummaryItem");
            xw.WriteAttributeString("name", name);
            xw.WriteAttributeString("isDocumented", isDocumented.ToString());
            xw.WriteString(summary);
            xw.WriteEndElement();
        }

        /// <summary>
        /// This is used to load the namespace summary item information from
        /// the project file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            name = xr.GetAttribute("name");
            this.IsDocumented = Convert.ToBoolean(xr.GetAttribute(
                "isDocumented"), CultureInfo.InvariantCulture);
            this.Summary = xr.ReadString();
        }

        /// <summary>
        /// Return the namespace name as the string representation of the
        /// object.
        /// </summary>
        /// <returns>The namespace name</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
