//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : XmlCommentsFile.cs
// Author  : Eric Woodruff
// Updated : 01/20/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing an XML comment file and is used
// when searching for and adding missing documentation tag information.
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
// 1.3.3.1  12/07/2006  EFW  Added C++ comments fixup
//=============================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This represents an XML comment file and is used when searching for and
    /// adding missing documentation tag information.
    /// </summary>
    public class XmlCommentsFile
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private static Regex reFixupComments1 = new Regex(
            "(member name=\".*?System\\.Collections\\.Generic.*?)(`[0-9]+)");

        private static Regex reFixupComments2 = new Regex(
            "(member name=\".*?System\\.Collections\\.Generic.*?)(\\^)");

        private string sourcePath;
        private Encoding enc;
        private XmlDocument comments;
        private XmlNode members;
        private bool wasModified;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This read-only property is used to get the source path of the file
        /// </summary>
        public string SourcePath
        {
            get { return sourcePath; }
        }

        /// <summary>
        /// This is used to load the comments file on first use
        /// </summary>
        public XmlDocument Comments
        {
            get
            {
                if(comments == null)
                {
                    // Although Visual Studio doesn't add an encoding
                    // attribute, the files are UTF-8 encoded.
                    enc = Encoding.UTF8;
                    comments = new XmlDocument();

                    // Read it with the appropriate encoding
                    string content = BuildProcess.ReadWithEncoding(
                        sourcePath, ref enc);
                    comments.LoadXml(content);

                    comments.NodeChanged += new XmlNodeChangedEventHandler(
                        comments_NodeChanged);
                    comments.NodeInserted += new XmlNodeChangedEventHandler(
                        comments_NodeChanged);
                }

                return comments;
            }
        }

        /// <summary>
        /// Mark the file as modified if a node is changed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        void comments_NodeChanged(object sender, XmlNodeChangedEventArgs e)
        {
            wasModified = true;
        }

        /// <summary>
        /// This read-only property is used to get the root members node
        /// </summary>
        public XmlNode Members
        {
            get
            {
                if(members == null)
                    members = this.Comments.SelectSingleNode("//members");

                return members;
            }
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">The XML comments filename</param>
        /// <exception cref="ArgumentNullException">This is thrown if the
        /// filename is null or an empty string.</exception>
        public XmlCommentsFile(string filename)
        {
            if(String.IsNullOrEmpty(filename))
                throw new ArgumentException("filename cannot be null",
                    "filename");

            sourcePath = filename;
        }

        /// <summary>
        /// Save the comments file if it was modified
        /// </summary>
        public void Save()
        {
            // Write the file back out with the appropriate encoding if it
            // was modified
            if(wasModified)
            {
                using(StreamWriter sw = new StreamWriter(sourcePath, false, enc))
                {
                    comments.Save(sw);
                }

                wasModified = false;
            }
        }

        /// <summary>
        /// This is called to fixup the comments for C++ compiler generated
        /// XML comments files.
        /// </summary>
        /// <remarks>The C++ compiler generates method signatures that differ
        /// from the other .NET compilers for methods that take generics as
        /// parameters.  These methods fail to get documented as they do not
        /// match the output of <b>MRefBuilder</b>.  The C# and VB.NET
        /// compilers generate names that do match it and this option is not
        /// needed for comments files generated by them.</remarks>
        public void FixupComments()
        {
            this.Save();
            comments = null;
            members = null;
            enc = Encoding.UTF8;

            // Read it with the appropriate encoding
            string content = BuildProcess.ReadWithEncoding(
                sourcePath, ref enc);

            // Strip out "`" followed by digits and "^" in member names
            content = reFixupComments1.Replace(content, "$1");
            content = reFixupComments2.Replace(content, "$1");

            // Write the file back out using its original encoding
            using(StreamWriter sw = new StreamWriter(sourcePath,
                false, enc))
            {
                sw.Write(content);
            }
        }
    }
}
