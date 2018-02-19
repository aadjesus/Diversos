//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : NamespaceSummaryItemCollection.cs
// Author  : Eric Woodruff
// Updated : 09/29/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the namespace summary
// item information.
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
// 1.2.0.0  09/04/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This collection class is used to hold the namespace summary items
    /// for a project.
    /// </summary>
    /// <remarks>Namespaces that appear in the assemblies but not in this
    /// list are documented by default and will appear without a namespace
    /// summary.</remarks>
    public class NamespaceSummaryItemCollection : BindingList<NamespaceSummaryItem>
    {
        #region Internal methods
        //=====================================================================
        // Internal methods

        /// <summary>
        /// This is used to save the namespace summary item list to the project
        /// file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            if(base.Count > 0)
            {
                xw.WriteStartElement("namespaceSummaries");

                foreach(NamespaceSummaryItem nsi in this)
                    nsi.WriteXml(xw);

                xw.WriteEndElement();
            }
        }

        /// <summary>
        /// This is used to load the namespace summary item list from the
        /// project file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            NamespaceSummaryItem nsi;

            while(!xr.EOF && xr.NodeType != XmlNodeType.EndElement)
            {
                if(xr.NodeType == XmlNodeType.Element &&
                  xr.Name == "namespaceSummaryItem")
                {
                    nsi = new NamespaceSummaryItem();
                    nsi.ReadXml(xr, schemaVersion);
                    this.Add(nsi);
                }

                xr.Read();
            }
        }
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This read-only property returns true if the collection has
        /// summary items with summary text or items marked as not to be
        /// documented.
        /// </summary>
        public bool HasDefinitions
        {
            get
            {
                foreach(NamespaceSummaryItem nsi in this)
                    if(!nsi.IsDocumented || nsi.Summary.Length != 0)
                        return true;

                return false;
            }
        }

        /// <summary>
        /// Indexer.  This can be used to retrieve the summary information
        /// for the specified namespace.
        /// </summary>
        /// <param name="name">The namespace for which to search</param>
        /// <returns>The namespace summary information if found or null if
        /// not found.</returns>
        public NamespaceSummaryItem this[string name]
        {
            get
            {
                if(name == null || name.Length == 0)
                    name = "(global)";

                foreach(NamespaceSummaryItem nsi in this)
                    if(nsi.Name == name)
                        return nsi;

                return null;
            }
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// This is used to see if the collection contains an entry for the
        /// specified namespace.
        /// </summary>
        /// <param name="name">The namespace for which to search</param>
        /// <returns>True if found, false if not</returns>
        public bool Contains(string name)
        {
            if(name == null || name.Length == 0)
                name = "(global)";

            foreach(NamespaceSummaryItem nsi in this)
                if(nsi.Name == name)
                    return true;

            return false;
        }

        /// <summary>
        /// This is used to see if the collection contains an entry for the
        /// specified namespace and returns whether or not it is documented.
        /// </summary>
        /// <param name="name">The namespace for which to search</param>
        /// <returns>True if found and it is to be documented or if not found,
        /// false if found and it is not to be documented.</returns>
        public bool IsDocumented(string name)
        {
            return (this[name] != null);
        }

        /// <summary>
        /// This is used to sort the namespace items by name
        /// </summary>
        public void Sort()
        {
            ((List<NamespaceSummaryItem>)this.Items).Sort(
                delegate(NamespaceSummaryItem x, NamespaceSummaryItem y)
                {
                    return Comparer<string>.Default.Compare(x.Name, y.Name);
                });
        }
    }
}
