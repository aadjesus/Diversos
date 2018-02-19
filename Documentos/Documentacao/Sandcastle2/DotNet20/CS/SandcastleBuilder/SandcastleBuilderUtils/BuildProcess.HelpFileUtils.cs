//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.HelpFileUtils.cs
// Author  : Eric Woodruff
// Updated : 03/07/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code used to modify the help file project files to
// create a better table of content and find the default help file page
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
// 1.0.0.0  08/07/2006  EFW  Created the code
// 1.2.0.0  09/06/2006  EFW  Added support for TOC content placement
// 1.3.0.0  09/09/2006  EFW  Added support for website output
// 1.3.1.0  10/02/2006  EFW  Added support for the September CTP
// 1.3.2.0  11/04/2006  EFW  Added support for the NamingMethod property
//=============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web;

namespace SandcastleBuilder.Utils
{
    partial class BuildProcess
    {
        #region Private data members
        //=====================================================================
        // Private data members

        // Regular expressions used for parsing and fix-up
        private static Regex reExtractDefTopic = new Regex(
            "param name=\"Local\" value=\"(?<Filename>.*)\"",
            RegexOptions.IgnoreCase);

        private static Regex re1xRootEntry = new Regex(
            @"<LI>.*?</LI>.*?<UL>", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);

        private static Regex re2xRootEntry = new Regex(
            @"\<HelpTOCNode .*?\>", RegexOptions.IgnoreCase);

        private static Regex reParamFixUp = new Regex("<param(.*)>",
            RegexOptions.IgnoreCase);

        private static Regex reEncodeLeft = new Regex("(\".*)(<)(.*\")");
        private static Regex reEncodeRight = new Regex("(\".*)(>)(.*\")");

        private static Regex reInvalidChars = new Regex("[:.`#]");
        #endregion

        /// <summary>
        /// This is called to determine the default topic for the help file
        /// and insert any additional table of content entries for the
        /// additional content files.
        /// </summary>
        /// <param name="format">The format of the table of content
        /// (HtmlHelp1x, HtmlHelp2x, or Website).</param>
        /// <remarks>In the absence of an additional content item with a
        /// default topic indicator, the default page is determined by
        /// extracting the first entry from the generated table of contents
        /// file.  If an additional content item with a default topic indicator
        /// has been specified, it will be the used instead.  The default
        /// topic is not used by HTML Help 2.x files.</remarks>
        /// <exception cref="ArgumentException">This is thrown if the
        /// format is not <b>HtmlHelp1x</b>, <b>HtmlHelp2x</b>, or
        /// <b>Website</b>.</exception>
        protected void UpdateTableOfContent(HelpFileFormat format)
        {
            Encoding enc = Encoding.Default;

            string tocFile, content;
            bool tocChanged = false;
            int endTagPos;

            if(format != HelpFileFormat.HtmlHelp1x &&
              format != HelpFileFormat.HtmlHelp2x &&
              format != HelpFileFormat.Website)
                throw new ArgumentException("The format specified must be " +
                    "a single format, not a combination", "format");

            // HTML 2.x or HTML 1.x/website
            if(format != HelpFileFormat.HtmlHelp2x)
                tocFile = workingFolder + project.HtmlHelpName + ".hhc";
            else
                tocFile = workingFolder + project.HtmlHelpName + ".hxt";

            // When reading the file, use the default encoding but detect the
            // encoding if byte order marks are present.
            content = BuildProcess.ReadWithEncoding(tocFile, ref enc);

            // We only need the default page for HTML Help 1.x files and
            // websites.
            if(format != HelpFileFormat.HtmlHelp2x)
            {
                // Don't bother if an explicit default has been specified
                if(defaultTopic == null)
                {
                    Match m = reExtractDefTopic.Match(content);

                    if(!m.Success)
                        throw new BuilderException("Unable to determine " +
                            "default page in " + tocFile);

                    defaultTopic = m.Groups["Filename"].Value;
                }

                // Remove the root namespace container if not wanted and it
                // is there.
                if(!project.RootNamespaceContainer &&
                  re1xRootEntry.IsMatch(content))
                {
                    tocChanged = true;
                    content = re1xRootEntry.Replace(content, String.Empty, 1);
                    endTagPos = content.LastIndexOf("</UL>");

                    if(endTagPos != -1)
                        content = content.Remove(endTagPos, 5);
                }

                // Reset the encoding as HTML 1.x does not appear to
                // support UTF-8 encoding in the table of content.
                enc = Encoding.Default;
                tocChanged = true;
            }
            else
            {
                // Remove the root namespace container if not wanted and
                // it is there.
                if(!project.RootNamespaceContainer &&
                  content.LastIndexOf("</HelpTOCNode>") != -1)
                {
                    tocChanged = true;
                    content = re2xRootEntry.Replace(content, String.Empty, 1);
                    endTagPos = content.LastIndexOf("</HelpTOCNode>");
                    content = content.Remove(endTagPos, 14);
                }
            }

            // Update and save table of content with additional items
            if(tocChanged || (toc != null && toc.Count != 0))
            {
                // The additional entries can go in ahead of the namespace
                // documentation entries or after them.
                if(toc != null && toc.Count != 0)
                    if(format != HelpFileFormat.HtmlHelp2x)
                    {
                        if(project.ContentPlacement == ContentPlacement.AboveNamespaces)
                            content = content.Insert(content.IndexOf(
                                "<UL>") + 6, toc.ToString());
                        else
                            content = content.Insert(content.LastIndexOf(
                                "</UL>"), toc.ToString());
                    }
                    else
                    {
                        if(project.ContentPlacement == ContentPlacement.AboveNamespaces)
                            content = content.Insert(content.IndexOf(
                                "\"1.0\">") + 8,
                                toc.ToString(HelpFileFormat.HtmlHelp2x));
                        else
                            content = content.Insert(content.IndexOf(
                                "</HelpTOC>"),
                                toc.ToString(HelpFileFormat.HtmlHelp2x));
                    }

                // We'll parse the HTML TOC as an XML file later on to
                // generate the tree view TOC for the website.  This
                // requires some fix-ups to the <param> tags and the
                // removal of the DOCTYPE tag.
                if(format == HelpFileFormat.Website)
                {
                    content = reParamFixUp.Replace(content, "<param$1/>");
                    content = reEncodeLeft.Replace(content, "$1&lt;$3");
                    content = reEncodeRight.Replace(content, "$1&gt;$3");
                    content = content.Remove(0, content.IndexOf("<HTML>"));
                }

                // Write the file back out with the appropriate encoding
                using(StreamWriter sw = new StreamWriter(tocFile, false, enc))
                {
                    sw.Write(content);
                }
            }
        }

        /// <summary>
        /// This is called to create the help project output folder and copy
        /// the standard content files (art, media, scripts, and styles) to the
        /// help project folder.
        /// </summary>
        /// <remarks>This creates the folders <b>Output\</b> and
        /// <b>Output\html</b> under the working folder and copies the stock
        /// art, icon, media, script, and style sheet files from the
        /// <b>{@PresentationPath}\art</b>, <b>{@PresentationPath}\icons</b>,
        /// <b>{@PresentationPath}\media</b>,
        /// <b>{@PresentationPath}\scripts</b>, and
        /// <b>{@PresentationPath}\styles</b> folders which are located in the
        /// Sandcastle installation folder.  The art, icons, and media folders
        /// may or may not exist based on the style.</remarks>
        protected void CopyStandardHelpContent()
        {
            Directory.CreateDirectory(workingFolder + "Output");
            Directory.CreateDirectory(workingFolder + @"Output\html");

            if(Directory.Exists(presentationFolder + "art"))
                this.RecursiveCopy(presentationFolder + @"art\*.*",
                    workingFolder + @"Output\art\");

            if(Directory.Exists(presentationFolder + "icons"))
                this.RecursiveCopy(presentationFolder + @"icons\*.*",
                    workingFolder + @"Output\icons\");

            if(Directory.Exists(presentationFolder + "media"))
                this.RecursiveCopy(presentationFolder + @"media\*.*",
                    workingFolder + @"Output\media\");

            this.RecursiveCopy(presentationFolder + @"scripts\*.*",
                workingFolder + @"Output\scripts\");
            this.RecursiveCopy(presentationFolder + @"styles\*.*",
                workingFolder + @"Output\styles\");
        }

        /// <summary>
        /// This copies files from the specified source folder to the specified
        /// destination folder.  If any subfolders are found below the source
        /// folder and the wildcard is "*.*", the subfolders are also copied
        /// recursively.
        /// </summary>
        /// <param name="sourcePath">The source path from which to copy</param>
        /// <param name="destPath">The destination path to which to copy</param>
        protected void RecursiveCopy(string sourcePath, string destPath)
        {
            if(sourcePath == null)
                throw new ArgumentNullException("sourcePath");

            if(destPath == null)
                throw new ArgumentNullException("destPath");

            int idx = sourcePath.LastIndexOf('\\');

            string dirName = sourcePath.Substring(0, idx),
                   fileSpec = sourcePath.Substring(idx + 1),
                   filename;

            string[] files = Directory.GetFiles(dirName, fileSpec);

            foreach(string name in files)
            {
                filename = destPath + Path.GetFileName(name);

                if(!Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);

                // All attributes are turned off so that we can delete
                // it later.
                File.Copy(name, filename, true);
                File.SetAttributes(filename, FileAttributes.Normal);

                this.ReportProgress("{0} -> {1}", name, filename);
            }

            // For "*.*", copy subfolders too
            if(fileSpec == "*.*")
            {
                string[] subFolders = Directory.GetDirectories(dirName);

                foreach(string folder in subFolders)
                    this.RecursiveCopy(folder + @"\*.*", destPath +
                        folder.Substring(dirName.Length + 1) + @"\");
            }
        }

        /// <summary>
        /// This returns a complete list of files for inclusion in the
        /// compiled help file.
        /// </summary>
        /// <param name="folder">The folder to expand</param>
        /// <param name="format">The HTML help file format</param>
        /// <returns>The full list of all files for the help project</returns>
        /// <remarks>The help file list is expanded to ensure that we get
        /// all additional content including all nested sub-folders.  The
        /// <b>format</b> parameter determines the format of the returned
        /// file list.  For HTML 1.x, it returns a list of the filenames.
        /// For HTML 2.x, it returns the list formatted with the necessary
        /// XML markup.</remarks>
        protected static string HelpProjectFileList(string folder,
          HelpFileFormat format)
        {
            StringBuilder sb = new StringBuilder(10240);
            string itemFormat;

            if(folder == null)
                throw new ArgumentNullException("folder");

            string[] files = Directory.GetFiles(folder, "*.*",
                SearchOption.AllDirectories);

            if(!folder.EndsWith(@"\"))
                folder += @"\";

            if((format & HelpFileFormat.HtmlHelp1x) != 0)
                itemFormat = "{0}\r\n";
            else
                itemFormat = "	<File Url=\"{0}\" />\r\n";

            foreach(string name in files)
                sb.AppendFormat(itemFormat, name.Replace(folder, String.Empty));

            return sb.ToString();
        }

        /// <summary>
        /// This is used to generate the website helper files and copy the
        /// output to the project output folder ready for use as a website.
        /// </summary>
        protected void GenerateWebsite()
        {
            string destFile;

            this.ReportProgress(BuildStep.CopyingWebsiteFiles,
                "-------------------------------\r\n" +
                "Copying website files to output folder...\r\n");

            // Purge all files and folders (except the working folder and
            // build log) from the output path.
            foreach(string file in Directory.GetFiles(outputFolder))
                if(!file.EndsWith("LastBuild.log"))
                    File.Delete(file);

            foreach(string folder in Directory.GetDirectories(outputFolder))
                if(!folder.EndsWith("Working"))
                    Directory.Delete(folder, true);

            // Copy the index page, TOC, and tree view stuff
            foreach(string file in Directory.GetFiles(webFolder))
                if(file.EndsWith("html"))
                    this.TransformTemplate(Path.GetFileName(file),
                        webFolder, outputFolder);
                else
                {
                    destFile = outputFolder + Path.GetFileName(file);
                    File.Copy(file, destFile, true);
                    File.SetAttributes(destFile, FileAttributes.Normal);
                }

            // Copy the help pages and related content
            this.RecursiveCopy(workingFolder + @"Output\*.*", outputFolder);
        }

        /// <summary>
        /// This is called to generate the HTML table of content when
        /// creating the website output.
        /// </summary>
        /// <returns>The HTML to insert for the table of content.</returns>
        protected string GenerateHtmlToc()
        {
            XmlDocument toc;
            Encoding enc = Encoding.Default;
            StringBuilder sb = new StringBuilder(2048);

            string content;

            // When reading the file, use the default encoding but detect the
            // encoding if byte order marks are present.
            content = BuildProcess.ReadWithEncoding(workingFolder +
              project.HtmlHelpName + ".hhc", ref enc);

            toc = new XmlDocument();
            toc.LoadXml(content);

            // Get the TOC entries from the first UL tag in the body
            XmlNodeList entries = toc.ChildNodes[0].ChildNodes[0].
                ChildNodes[0].ChildNodes;

            this.AppendTocEntry(entries, sb);

            return sb.ToString();
        }

        /// <summary>
        /// This is called to recursively append the child nodes to the
        /// HTML table of content in the specified string builder.
        /// </summary>
        /// <param name="entries">The list over which to iterate
        /// recursively.</param>
        /// <param name="sb">The string builder to which the entries are
        /// appended.</param>
        private void AppendTocEntry(XmlNodeList entries, StringBuilder sb)
        {
            XmlNodeList parameters;
            string url, target, title;
            int idx;

            for(idx = 0; idx < entries.Count; idx++)
                if(idx < entries.Count - 1 && entries[idx + 1].Name == "UL" &&
                  entries[idx + 1].ChildNodes.Count != 0)
                {
                    // Write out a root node with or without a content page
                    parameters = entries[idx].ChildNodes[0].ChildNodes;

                    if(parameters.Count == 1)
                    {
                        title = parameters[0].Attributes["value"].Value;
                        url = "#";
                        target = String.Empty;
                    }
                    else
                    {
                        title = parameters[0].Attributes["value"].Value;
                        url = parameters[1].Attributes["value"].Value.Replace(
                            '\\', '/');
                        target = " target=\"Content\"";
                    }

                    sb.AppendFormat("<div class=\"TreeNode\">\r\n" +
                        "<img class=\"TreeNodeImg\" " +
                        "onclick=\"javascript: Toggle(this);\" " +
                        "src=\"Collapsed.gif\"/><a class=\"UnselectedNode\" " +
                        "onclick=\"javascript: return Expand(this);\" " +
                        "href=\"{0}\"{1}>{2}</a>\r\n" +
                        "<div class=\"Hidden\">\r\n", url, target,
                        HttpUtility.HtmlEncode(title));

                    // Append child nodes
                    idx++;
                    this.AppendTocEntry(entries[idx].ChildNodes, sb);

                    // Write out the closing tags for the root node
                    sb.Append("</div>\r\n</div>\r\n");
                }
                else  // Ignore empty UL containers
                    if(entries[idx].Name == "LI")
                    {
                        parameters = entries[idx].ChildNodes[0].ChildNodes;
                        title = parameters[0].Attributes["value"].Value;
                        url = parameters[1].Attributes["value"].Value.Replace(
                            '\\', '/');

                        // Write out a TOC entry
                        sb.AppendFormat("<div class=\"TreeItem\">\r\n" +
                            "<img src=\"Item.gif\"/>" +
                            "<a class=\"UnselectedNode\" " +
                            "onclick=\"javascript: return SelectNode(this);\" " +
                            "href=\"{0}\" target=\"Content\">{1}</a>\r\n" +
                            "</div>\r\n", url, HttpUtility.HtmlEncode(title));
                    }
        }

        /// <summary>
        /// This is used to change the filenames assigned to each member
        /// in the reflection information file.
        /// </summary>
        protected void ModifyHelpTopicFilenames()
        {
            XmlNodeList elements;
            string originalName, memberName, newName;
            bool duplicate;
            int idx;

            this.ReportProgress(BuildStep.ModifyHelpTopicFilenames,
                "-------------------------------\r\n" +
                "Modifying help topic filenames in reflection information file");

            // The reflection file can contain tens of thousands of entries
            // for large assemblies.  Dictionary<K, T> is much faster at
            // lookups than List<T>.
            Dictionary<string, string> filenames = new Dictionary<string, string>();

            try
            {
                // Find the API node list
                elements = reflectionInfo.SelectNodes("//apis/api/file");

                foreach(XmlNode file in elements)
                {
                    originalName = memberName = file.ParentNode.Attributes[
                        "id"].Value;

                    // Remove parameters
                    idx = memberName.IndexOf('(');
                    if(idx != -1)
                        memberName = memberName.Substring(0, idx);

                    // Replace invalid filename characters with an underscore
                    // if member names are used as the filenames.
                    if(project.NamingMethod == NamingMethod.MemberName)
                        newName = memberName = reInvalidChars.Replace(
                            memberName, "_");
                    else
                        newName = memberName;

                    idx = 0;

                    do
                    {
                        // Hash codes can be used to shorten extremely long
                        // type and member names.
                        if(project.NamingMethod == NamingMethod.HashedMemberName)
                            newName = String.Format(
                                CultureInfo.InvariantCulture, "{0:X}",
                                newName.GetHashCode());

                        // Check for a duplicate (i.e. an overloaded member).
                        // These will be made unique by adding a counter to
                        // the end of the name.
                        duplicate = filenames.ContainsKey(newName);

                        if(duplicate)
                        {
                            idx++;
                            newName = String.Format(
                                CultureInfo.InvariantCulture, "{0}_{1}",
                                memberName, idx);
                        }

                    } while(duplicate);

                    // Log duplicates that had unique names created
                    if(idx != 0)
                        this.ReportProgress("    Unique name {0} generated " +
                            "for {1}", newName, originalName);

                    file.Attributes["name"].Value = newName;
                    filenames.Add(newName, null);
                }
            }
            catch(Exception ex)
            {
                throw new BuilderException("Error modifying help topic " +
                    "filenames: " + ex.Message, ex);
            }
        }
    }
}
