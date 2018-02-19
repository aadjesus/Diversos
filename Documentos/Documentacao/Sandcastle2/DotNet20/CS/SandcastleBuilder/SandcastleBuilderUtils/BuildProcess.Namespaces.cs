//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.Namespaces.cs
// Author  : Eric Woodruff
// Updated : 03/08/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code used to generate the namespace summary file and
// to purge the unwanted namespaces from the reflection information file.
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
// 1.3.1.0  09/29/2006  EFW  Reworked to insert "missing summary" note for all
//                           namespaces without a summary.
// 1.3.2.0  11/10/2006  EFW  Reworked to support external XML comments files
//=============================================================================

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace SandcastleBuilder.Utils
{
    partial class BuildProcess
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private XmlCommentsFileCollection commentsFiles;

        private static Regex reStripWhitespace = new Regex(@"\s");
        #endregion

        /// <summary>
        /// This is called to generate the namespace summary file and to purge
        /// the unwanted namespaces from the reflection information file.
        /// </summary>
        protected void GenerateNamespaceSummaries()
        {
            XmlNodeList nsElements, nsItems;
            XmlNode item, member, tag;
            NamespaceSummaryItem nsi;
            string nsName, summaryText;
            bool isDocumented;

            this.ReportProgress(BuildStep.GenerateNamespaceSummaries,
                "-------------------------------\r\n" +
                "Generating namespace summary information...");

            // XML comments do not support summaries on namespace elements
            // by default.  However, if Sandcastle finds them, it will add
            // them to the help file.  The same holds true for project
            // comments on the root namespaces page (R:Project).  We can
            // accomplish this by adding elements to the first comments
            // file or by supplying them in an external XML comments file.
            try
            {
                // Add out the project comments if specified
                if(project.ProjectSummary.Length != 0)
                {
                    member = commentsFiles.FindMember("R:Project");
                    this.AddNamespaceComments(member, project.ProjectSummary);
                }

                // Get all the namespace nodes
                nsElements = reflectionInfo.SelectNodes(
                    "//apis/api[@id='R:Project']/elements/*");

                // Add the namespace summaries and remove unwanted
                // namespaces from the reflection information file.
                foreach(XmlNode n in nsElements)
                {
                    nsName = n.Attributes["api"].Value;
                    nsi = project.NamespaceSummaries[nsName.Substring(2)];

                    if(nsi != null)
                    {
                        isDocumented = nsi.IsDocumented;
                        summaryText = nsi.Summary;
                    }
                    else
                    {
                        isDocumented = true;
                        summaryText = String.Empty;
                    }

                    member = commentsFiles.FindMember(nsName);

                    // Is it excluded in an external XML comments file?
                    tag = member.SelectSingleNode("exclude");

                    if(tag != null)
                        isDocumented = false;

                    // If documented, add the summary text
                    if(isDocumented)
                        this.AddNamespaceComments(member, summaryText);
                    else
                    {
                        this.ReportProgress("    Removed namespace {0}",
                            nsName);

                        // Remove the namespace
                        n.ParentNode.RemoveChild(n);

                        // Remove the namespace container
                        item = reflectionInfo.SelectSingleNode(
                            String.Format(CultureInfo.InvariantCulture,
                            "//apis/api[@id='{0}']", nsName));

                        item.ParentNode.RemoveChild(item);

                        // Remove all of the namespace items
                        nsItems = reflectionInfo.SelectNodes(String.Format(
                            CultureInfo.InvariantCulture,
                            "//apis/api/containers/namespace[@api='{0}']/../..",
                            nsName));

                        foreach(XmlNode xn in nsItems)
                            xn.ParentNode.RemoveChild(xn);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new BuilderException("Error generating namespace " +
                    "summaries: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Add project or namespace comments
        /// </summary>
        /// <param name="member">The member node to modify.</param>
        /// <param name="summaryText">The summary text to add.</param>
        private void AddNamespaceComments(XmlNode member, string summaryText)
        {
            string text;
            XmlNode tag = member.SelectSingleNode("summary");

            if(tag == null)
            {
                tag = member.OwnerDocument.CreateNode(XmlNodeType.Element,
                    "summary", null);
                member.AppendChild(tag);
            }

            text = reStripWhitespace.Replace(tag.InnerText, String.Empty);

            // NOTE: The text is not HTML encoded as it can contain HTML
            //       tags.  As such, entities such as "&" should be entered
            //       in encoded form in the text (i.e. &amp;).
            if(!String.IsNullOrEmpty(text))
                tag.InnerXml += "<p/>" + summaryText;
            else
                if(!String.IsNullOrEmpty(summaryText))
                    tag.InnerXml = summaryText;
                else
                {
                    text = member.Attributes["name"].Value;

                    this.ReportProgress("    Warning: Missing namespace " +
                        "summary documentation for {0}", text);
                    tag.InnerXml = String.Format(CultureInfo.InvariantCulture,
                        "<p style=\"color: #dc143c; font-size: 8.5pt; " +
                        "font-weight: bold;\">[Missing namespace summary " +
                        "documentation for {0}]</p>",
                        HttpUtility.HtmlEncode(text));
                }
        }
    }
}
