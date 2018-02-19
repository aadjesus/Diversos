//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : XmlCommentsFileCollection.cs
// Author  : Eric Woodruff
// Updated : 09/29/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class used to hold the XML comments files
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
// 1.3.1.0  09/26/2006  EFW  Created the code
//=============================================================================

using System;
using System.Globalization;
using System.ComponentModel;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This collection class is used to hold the XML comments files
    /// during the build process.
    /// </summary>
    public class XmlCommentsFileCollection : BindingList<XmlCommentsFile>
    {
        /// <summary>
        /// Save the comments files
        /// </summary>
        public void Save()
        {
            foreach(XmlCommentsFile file in this)
                file.Save();
        }

        /// <summary>
        /// Search all comments files for the specified member.  If not found,
        /// add the blank member to the first file.
        /// </summary>
        /// <param name="memberName">The member name for which to search.</param>
        /// <returns>The XML node of the found or added member</returns>
        public XmlNode FindMember(string memberName)
        {
            XmlDocument doc;
            XmlNode member = null;
            XmlAttribute name;

            string xPathQuery = String.Format(CultureInfo.InvariantCulture,
                "//members/member[@name='{0}']", memberName);

            foreach(XmlCommentsFile f in this)
            {
                member = f.Comments.SelectSingleNode(xPathQuery);

                if(member != null)
                    break;
            }

            // If not found at all, add an entry for it to the first file
            if(member == null)
            {
                doc = this[0].Comments;
                member = doc.CreateNode(XmlNodeType.Element, "member", null);

                name = doc.CreateAttribute("name");
                name.Value = memberName;
                member.Attributes.Append(name);

                this[0].Members.AppendChild(member);
            }

            return member;
        }
    }
}
