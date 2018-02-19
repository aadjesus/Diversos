//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : DependencyItemCollection.cs
// Author  : Eric Woodruff
// Updated : 08/23/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the dependency item
// information.
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
    /// This collection class is used to hold the dependency items for a
    /// project.
    /// </summary>
    [Editor(typeof(DependencyItemEditor), typeof(UITypeEditor))]
    public class DependencyItemCollection : BindingList<DependencyItem>
    {
        /// <summary>
        /// This is used to save the dependency item list to the project
        /// file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            if(base.Count > 0)
            {
                xw.WriteStartElement("dependencies");

                foreach(DependencyItem di in this)
                    di.WriteXml(xw);

                xw.WriteEndElement();
            }
        }

        /// <summary>
        /// This is used to load the dependency item list from the project
        /// file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            DependencyItem di;

            while(!xr.EOF && xr.NodeType != XmlNodeType.EndElement)
            {
                if(xr.NodeType == XmlNodeType.Element &&
                  xr.Name == "dependencyItem")
                {
                    di = new DependencyItem();
                    di.ReadXml(xr, schemaVersion);
                    this.Add(di);
                }

                xr.Read();
            }
        }
    }
}
