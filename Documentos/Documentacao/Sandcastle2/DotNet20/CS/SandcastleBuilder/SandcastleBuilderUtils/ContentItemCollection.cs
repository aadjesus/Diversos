//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : ContentItemCollection.cs
// Author  : Eric Woodruff
// Updated : 09/19/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the additional content
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
// 1.0.0.0  08/08/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This collection class is used to hold the additional content items
    /// for a project.
    /// </summary>
    [Editor(typeof(ContentItemEditor), typeof(UITypeEditor))]
    public class ContentItemCollection : BindingList<ContentItem>
    {
        /// <summary>
        /// This is used to save the content item list to the project
        /// file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            if(base.Count > 0)
            {
                xw.WriteStartElement("additionalContent");

                foreach(ContentItem ci in this)
                    ci.WriteXml(xw);

                xw.WriteEndElement();
            }
        }

        /// <summary>
        /// This is used to load the content item list from the project
        /// file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            ContentItem ci;

            while(!xr.EOF && xr.NodeType != XmlNodeType.EndElement)
            {
                if(xr.NodeType == XmlNodeType.Element &&
                  xr.Name == "contentItem")
                {
                    ci = new ContentItem();
                    ci.ReadXml(xr, schemaVersion);
                    this.Add(ci);
                }

                xr.Read();
            }
        }
    }
}
