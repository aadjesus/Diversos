//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : ComponentConfigurationCollection.cs
// Author  : Eric Woodruff
// Updated : 11/25/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the configurations for
// third party build components such as the Code Block Component.
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
// 1.3.3.0  11/24/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Design;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This collection class is used to hold the additional content items
    /// for a project.
    /// </summary>
    [Editor(typeof(ComponentConfigurationEditor), typeof(UITypeEditor)),
      Serializable]
    public class ComponentConfigurationDictionary : Dictionary<string, string>
    {
        #region Events
        /// <summary>
        /// This event is raised when the dictionary is modified
        /// </summary>
        public event ListChangedEventHandler DictionaryChanged;

        /// <summary>
        /// This raises the <see cref="DictionaryChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>The dictionary doesn't raise events automatically so
        /// this is raised manually as needed.</remarks>
        internal void OnDictionaryChanged(ListChangedEventArgs e)
        {
            if(DictionaryChanged != null)
                DictionaryChanged(this, e);
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ComponentConfigurationDictionary()
        {
        }

        /// <summary>
        /// This is used to save the component configuration list to the
        /// project file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            if(base.Count > 0)
            {
                xw.WriteStartElement("componentConfigurations");

                foreach(string s in this.Keys)
                {
                    xw.WriteStartElement("component");
                    xw.WriteAttributeString("id", s);
                    xw.WriteAttributeString("configuration", this[s]);
                    xw.WriteEndElement();
                }

                xw.WriteEndElement();
            }
        }

        /// <summary>
        /// This is used to load the component configuration list from the
        /// project file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            string id, config;

            while(!xr.EOF && xr.NodeType != XmlNodeType.EndElement)
            {
                if(xr.NodeType == XmlNodeType.Element &&
                  xr.Name == "component")
                {
                    id = xr.GetAttribute("id");
                    config = xr.GetAttribute("configuration");
                    this.Add(id, config);
                }

                xr.Read();
            }
        }
    }
}
