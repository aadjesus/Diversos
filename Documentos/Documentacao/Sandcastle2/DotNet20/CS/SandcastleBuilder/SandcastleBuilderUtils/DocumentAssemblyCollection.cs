//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : DocumentAssemblyCollection.cs
// Author  : Eric Woodruff
// Updated : 08/03/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the documentation
// assembly information.
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
// 1.0.0.0  08/03/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This collection class is used to hold the documentation assembly
    /// information.
    /// </summary>
    public class DocumentAssemblyCollection : BindingList<DocumentAssembly>
    {
        /// <summary>
        /// This is used to save the document assembly list to the project
        /// file.
        /// </summary>
        /// <param name="xw">The XML text writer to which the information
        /// is written.</param>
        internal void WriteXml(XmlTextWriter xw)
        {
            if(base.Count > 0)
            {
                xw.WriteStartElement("assemblies");

                foreach(DocumentAssembly da in this)
                    da.WriteXml(xw);

                xw.WriteEndElement();
            }
        }

        /// <summary>
        /// This is used to load the document assembly list from the project
        /// file.
        /// </summary>
        /// <param name="xr">The XML text reader from which the information
        /// is loaded.</param>
        /// <param name="schemaVersion">The schema version</param>
        internal void ReadXml(XmlTextReader xr, Version schemaVersion)
        {
            DocumentAssembly da;

            while(!xr.EOF && xr.NodeType != XmlNodeType.EndElement)
            {
                if(xr.NodeType == XmlNodeType.Element && xr.Name == "assembly")
                {
                    da = new DocumentAssembly();
                    da.ReadXml(xr, schemaVersion);
                    this.Add(da);
                }

                xr.Read();
            }
        }

        /// <summary>
        /// Returns a list of the comment file paths in a format suitable
        /// for inserting into a Sandcastle Configuration file.
        /// </summary>
        /// <param name="workingFolder">The working folder path</param>
        /// <returns>The comment file list XML tags</returns>
        internal string CommentFileList(string workingFolder)
        {
            StringBuilder sb = new StringBuilder(2048);

            foreach(DocumentAssembly da in this)
                sb.AppendFormat("            <data files=\"{0}{1}\" />\r\n",
                    workingFolder, Path.GetFileName(da.XmlCommentsPath));

            return sb.ToString();
        }
    }
}
