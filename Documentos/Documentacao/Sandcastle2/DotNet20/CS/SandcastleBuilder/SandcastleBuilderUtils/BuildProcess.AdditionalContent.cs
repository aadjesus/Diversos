//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.AdditionalContent.cs
// Author  : Eric Woodruff
// Updated : 03/09/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code used to merge the additional content into the
// working folder and build the table of content entries for it.
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
// 1.3.0.0  08/07/2006  EFW  Created the code
// 1.3.3.1  12/08/2006  EFW  Added support for colorizing <pre> tags in
//                           additional content files.
// 1.3.3.2  12/20/2006  EFW  Added support for project property and shared
//                           content substitution.
// 1.4.0.0  02/23/2007  EFW  Added support for Exclude content items and
//                           support for <code source="file"/> tags.
//=============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml;

using ColorizerLibrary;

namespace SandcastleBuilder.Utils
{
    partial class BuildProcess
    {
        #region Private data members
        //=====================================================================
        // Private data members

        // The exclusions list
        private Dictionary<string, string> exclusionList;

        // The table of content entries for the additional content
        private TocEntryCollection toc;

        // Regular expressions used to match table of content options and to
        // resolve namespace references in additional content files.
        private static Regex reTocExclude = new Regex(
            @"<!--\s*@TOCExclude\s*-->", RegexOptions.IgnoreCase);

        internal static Regex reIsDefaultTopic = new Regex(
            @"<!--\s*@DefaultTopic\s*-->", RegexOptions.IgnoreCase);

        internal static Regex reSortOrder = new Regex(@"<!--\s*@SortOrder\s*" +
            @"(?<SortOrder>\d{1,5})\s*-->",
            RegexOptions.IgnoreCase);

        private static Regex rePageTitle = new Regex(
            @"<title>(?<Title>.*)</title>", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);

        private static Regex reResolveLinks = new Regex(
            "(<see\\s+.*?cref\\s*=\\s*\"(?<Link>.+?)\"\\s*)(/>|" +
            "(>(?<Text>.*?)</see>))", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);

        private static Regex reColorizeCheck = new Regex(
            @"<pre\s+.*?lang\s*=", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);

        private static Regex reProjectTags = new Regex(
            @"<\s*@(?<Field>\w*?)\s*/?>");

        private static Regex reSharedContent = new Regex(
            "<\\s*include\\s*item\\s*=\\s*\"(?<Item>.*?)\"\\s*/\\s*>",
            RegexOptions.IgnoreCase);

        private static Regex reCodeBlock = new Regex(
            "<code([^>]+?)source\\s*=\\s*\"(.*?)\"(.*?)/>",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private static Regex reCodeRegion = new Regex(
            "region\\s*=\\s*\"(.*?)\"", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);

        // NOTE: This same expression is used in the CodeBlockComponent.
        // See it for details on what it is and what it does.
        private static Regex reMatchRegion = new Regex(
            @"\#region\s+(.*?(((?<Open>\#region\s+).*?)+" +
            @"((?<Close-Open>\#end\s?region).*?)+)*(?(Open)(?!)))" +
            @"\#end\s?region", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);

        private MatchEvaluator linkMatchEval, contentMatchEval,
            codeBlockMatchEval;

        // The XML document used to resolve shared content
        private XmlDocument sharedContent;

        private string pathToRoot;      // Path to root for resolved links

        private CodeColorizer codeColorizer;    // The code colorizer
        #endregion

        /// <summary>
        /// This is used to generate a table of content for previewing
        /// </summary>
        internal TocEntryCollection TableOfContent
        {
            get
            {
                try
                {
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(
                        project.Filename));
                    this.CopyAdditionalContent(true);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());

                    MessageBox.Show(
                        "Unable to fully generate table of content:" +
                        ex.Message, "Sandcastle Help File Builder",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return toc;
            }
        }

        /// <summary>
        /// This is called to copy the additional content files and build a
        /// list of them for the help file project.
        /// </summary>
        /// <param name="previewing">Pass true to generate the table of content
        /// collection for previewing without actually copying anything.</param>
        /// <remarks>Note that for wilcard content items, the folders are
        /// copied recursively.</remarks>
        protected void CopyAdditionalContent(bool previewing)
        {
            TocEntry tocEntry;
            string source, filename, dirName, srcLower,
                rootPath = workingFolder + @"Output\";
            string[] fileList;
            int idx;

            toc = new TocEntryCollection();
            exclusionList = new Dictionary<string, string>();

            // Get a list of the exclusions first
            foreach(ContentItem ci in project.AdditionalContent)
            {
                if(!ci.ExcludeItems)
                    continue;

                source = ci.SourcePath;

                if(source.IndexOfAny(new char[] { '*', '?' }) == -1)
                    exclusionList.Add(source, null);
                else
                {
                    idx = source.LastIndexOf('\\');
                    dirName = source.Substring(0, idx);
                    filename = source.Substring(idx + 1);

                    fileList = Directory.GetFiles(dirName, filename,
                        SearchOption.AllDirectories);

                    foreach(string matchFile in fileList)
                        exclusionList.Add(matchFile, null);
                }
            }

            // Now copy the additional content less the excluded files
            foreach(ContentItem ci in project.AdditionalContent)
            {
                if(ci.ExcludeItems)
                    continue;

                source = ci.SourcePath;
                dirName = workingFolder + @"Output\" + ci.DestinationPath;
                filename = dirName + Path.GetFileName(source);

                if(source.IndexOfAny(new char[] { '*', '?' }) == -1)
                {
                    if(exclusionList.ContainsKey(source))
                        continue;

                    srcLower = source.ToLower(CultureInfo.InvariantCulture);

                    if(srcLower.EndsWith(".htm") || srcLower.EndsWith(".html"))
                    {
                        tocEntry = BuildProcess.GetTocInfo(source);

                        // If not to be included in the TOC, don't add it
                        if(tocEntry.IncludePage)
                        {
                            tocEntry.SourceFile = source;
                            tocEntry.DestinationFile = filename.Remove(0,
                                rootPath.Length);
                            toc.Add(tocEntry);
                        }

                        if(tocEntry.IsDefaultTopic)
                            defaultTopic = tocEntry.DestinationFile;
                    }
                    else
                        tocEntry = null;

                    if(!previewing && !Directory.Exists(dirName))
                        Directory.CreateDirectory(dirName);

                    // If the file contains links that need to be resolved,
                    // it is handled separately.
                    if(!previewing && tocEntry != null &&
                      (tocEntry.HasLinks || tocEntry.HasCodeBlocks ||
                      tocEntry.NeedsColorizing || tocEntry.HasProjectTags))
                    {
                        // Figure out the path to the root if needed
                        string[] parts = ci.DestinationPath.Split('\\');
                        pathToRoot = String.Empty;

                        for(int part = 0; part < parts.Length - 1; part++)
                            pathToRoot += "../";

                        this.ResolveLinksAndCopy(source, filename, tocEntry);
                    }
                    else
                    {
                        this.ReportProgress("{0} -> {1}", source, filename);

                        // All attributes are turned off so that we can delete
                        // it later.
                        if(!previewing)
                        {
                            File.Copy(source, filename, true);
                            File.SetAttributes(filename, FileAttributes.Normal);
                        }
                    }
                }
                else
                {
                    tocEntry = this.RecursiveContentCopy(previewing, source,
                        dirName);

                    if(tocEntry != null)
                        if(ci.DestinationPath.Length != 0)
                            toc.Add(tocEntry);
                        else
                        {
                            // If the content is copied to the root, just
                            // add the children to the TOC.
                            foreach(TocEntry te in tocEntry.Children)
                                toc.Add(te);
                        }
                }
            }

            // Sort the additional content entries into the proper order
            toc.Sort();

            codeColorizer = null;
            sharedContent = null;
            exclusionList = null;
        }

        /// <summary>
        /// This copies additional content files from the specified source
        /// folder to the specified destination folder and builds table of
        /// content entries for them.  If any subfolders are found below the
        /// source folder and the wildcard is "*.*", the subfolders are also
        /// copied recursively.
        /// </summary>
        /// <param name="previewing">Pass true to generate the table of content
        /// collection for previewing without actually copying anything.</param>
        /// <param name="sourcePath">The source path from which to copy.</param>
        /// <param name="destPath">The destination path to which to copy.</param>
        /// <returns>The table of content entry for the folder and all
        /// of its children or null if there is nothing to show in the table
        /// of content for this item.</returns>
        protected TocEntry RecursiveContentCopy(bool previewing,
          string sourcePath, string destPath)
        {
            TocEntry tocFolder, tocEntry;
            string[] files;
            string filename, nameLower, rootPath = workingFolder + @"Output\";
            bool hasContent = false;

            if(sourcePath == null)
                throw new ArgumentNullException("sourcePath");

            if(destPath == null)
                throw new ArgumentNullException("destPath");

            if(toc == null)
                throw new ArgumentNullException("toc");

            int idx = sourcePath.LastIndexOf('\\');

            string dirName = sourcePath.Substring(0, idx),
                   fileSpec = sourcePath.Substring(idx + 1);

            tocFolder = new TocEntry();
            tocFolder.SourceFile = dirName + @"\";
            tocFolder.Title = dirName.Substring(dirName.LastIndexOf("\\") + 1);

            // Copy all the files in the folder
            files = Directory.GetFiles(dirName, fileSpec);

            foreach(string name in files)
            {
                if(exclusionList.ContainsKey(name))
                    continue;

                filename = destPath + Path.GetFileName(name);
                nameLower = name.ToLower(CultureInfo.InvariantCulture);

                if(nameLower.EndsWith(".htm") || nameLower.EndsWith(".html"))
                    tocEntry = BuildProcess.GetTocInfo(name);
                else
                    tocEntry = null;

                // If the filename matches the folder name and it's an HTML
                // file, extract the TOC info from it for the root node.
                // If there is no info or its excluded, the folder entry won't
                // have an associated page.  Even if excluded, it will still use
                // the title, default topic, and sort order properties though.
                if(tocEntry != null)
                {
                    if(Path.GetFileNameWithoutExtension(name) == tocFolder.Title)
                    {
                        tocFolder.Title = tocEntry.Title;
                        tocFolder.IsDefaultTopic = tocEntry.IsDefaultTopic;
                        tocFolder.SortOrder = tocEntry.SortOrder;
                        tocFolder.SourceFile = name;
                        hasContent = true;  // In case it's this page only

                        if(tocEntry.IncludePage)
                        {
                            tocFolder.DestinationFile = filename.Remove(0,
                                rootPath.Length);

                            if(tocFolder.IsDefaultTopic)
                                defaultTopic = tocFolder.DestinationFile;
                        }
                        else
                            tocFolder.DestinationFile = null;
                    }
                    else
                    {
                        if(tocEntry.IncludePage)
                        {
                            tocEntry.SourceFile = name;
                            tocEntry.DestinationFile = filename.Remove(0,
                                rootPath.Length);
                            tocFolder.Children.Add(tocEntry);
                        }

                        if(tocEntry.IsDefaultTopic)
                            defaultTopic = tocEntry.DestinationFile;
                    }
                }

                if(!previewing && !Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);

                // If the file contains links that need to be resolved,
                // it is handled separately.
                if(!previewing && tocEntry != null &&
                  (tocEntry.HasLinks || tocEntry.HasCodeBlocks ||
                  tocEntry.NeedsColorizing || tocEntry.HasProjectTags))
                {
                    // Figure out the path to the root if needed
                    string[] parts = destPath.Remove(0, rootPath.Length).Split('\\');
                    pathToRoot = String.Empty;

                    for(int part = 0; part < parts.Length - 1; part++)
                        pathToRoot += "../";

                    this.ResolveLinksAndCopy(name, filename, tocEntry);
                }
                else
                {
                    this.ReportProgress("{0} -> {1}", name, filename);

                    // All attributes are turned off so that we can delete
                    // it later.
                    if(!previewing)
                    {
                        File.Copy(name, filename, true);
                        File.SetAttributes(filename, FileAttributes.Normal);
                    }
                }
            }

            // For "*.*", copy subfolders too
            if(fileSpec == "*.*")
            {
                string[] subFolders = Directory.GetDirectories(dirName);

                foreach(string folder in subFolders)
                {
                    tocEntry = this.RecursiveContentCopy(previewing,
                        folder + @"\*.*", destPath +
                        folder.Substring(dirName.Length + 1) + @"\");

                    if(tocEntry != null)
                        tocFolder.Children.Add(tocEntry);
                }
            }

            return (hasContent || tocFolder.Children.Count != 0) ?
                tocFolder : null;
        }

        /// <summary>
        /// This is used to extract table of content information from a file
        /// that will appear in the help file's table of content.
        /// </summary>
        /// <param name="filename">The file from which to extract the 
        /// information</param>
        /// <returns>The table of content entry</returns>
        private static TocEntry GetTocInfo(string filename)
        {
            TocEntry tocEntry;
            Encoding enc = Encoding.Default;
            string content;

            content = BuildProcess.ReadWithEncoding(filename, ref enc);

            tocEntry = new TocEntry();
            tocEntry.IncludePage = !reTocExclude.IsMatch(content);
            tocEntry.IsDefaultTopic = reIsDefaultTopic.IsMatch(content);

            Match m = reSortOrder.Match(content);
            if(m.Success)
                tocEntry.SortOrder = Convert.ToInt32(
                    m.Groups["SortOrder"].Value,
                    CultureInfo.InvariantCulture);

            // Get the page title if possible.  If not found, use the filename
            // without the path or extension as the page title.
            m = rePageTitle.Match(content);
            if(!m.Success)
                tocEntry.Title = Path.GetFileNameWithoutExtension(filename);
            else
                tocEntry.Title = HttpUtility.HtmlDecode(
                    m.Groups["Title"].Value).Replace("\r", "").Replace("\n", "");

            // Since we've got the file loaded, see if there are links
            // that need to be resolved when the file is copied, if it
            // contains <pre> blocks that should be colorized, or if it
            // contains tags or shared content items that need replacing.
            tocEntry.HasLinks = reResolveLinks.IsMatch(content);
            tocEntry.HasCodeBlocks = reCodeBlock.IsMatch(content);
            tocEntry.NeedsColorizing = reColorizeCheck.IsMatch(content);
            tocEntry.HasProjectTags = (reProjectTags.IsMatch(content) ||
                reSharedContent.IsMatch(content));

            return tocEntry;
        }

        /// <summary>
        /// This is called to load an additional content file, resolve links
        /// to namespace content and copy it to the output folder.
        /// </summary>
        /// <param name="sourceFile">The source filename to copy</param>
        /// <param name="destFile">The destination filename</param>
        /// <param name="entry">The entry being resolved.</param>
        private void ResolveLinksAndCopy(string sourceFile, string destFile,
          TocEntry entry)
        {
            Encoding enc = Encoding.Default;
            string content, script, syntaxFile;
            int pos;

            this.ReportProgress("{0} -> {1}", sourceFile, destFile);

            // When reading the file, use the default encoding but detect the
            // encoding if byte order marks are present.
            content = BuildProcess.ReadWithEncoding(sourceFile, ref enc);

            // Use a regular expression to find and replace all <see>
            // tags with link to the help file content.
            if(entry.HasLinks)
                content = reResolveLinks.Replace(content, linkMatchEval);

            // Expand <code> tags if necessary
            if(entry.HasCodeBlocks)
                content = reCodeBlock.Replace(content, codeBlockMatchEval);

            // Colorize <pre> tags if necessary
            if(entry.NeedsColorizing || entry.HasCodeBlocks)
            {
                // Initialize code colorizer on first use
                if(codeColorizer == null)
                    codeColorizer = new CodeColorizer(
                        shfbFolder + @"Colorizer\highlight.xml",
                        shfbFolder + @"Colorizer\highlight.xsl");

                // Set the path the "Copy" image
                codeColorizer.CopyImageUrl = pathToRoot + "icons/CopyCode.gif";

                // Colorize it and replace the "Copy" literal text with the
                // shared content include item so that it gets localized.
                content = codeColorizer.ProcessAndHighlightText(content);
                content = content.Replace(codeColorizer.CopyText + "</span",
                    "<include item=\"copyCode\"/></span");
                entry.HasProjectTags = true;

                // Add the links to the colorizer stylesheet and script files
                script = String.Format(CultureInfo.InvariantCulture,
                    "<link type='text/css' rel='stylesheet' " +
                    "href='{0}html/highlight.css' />" +
                    "<script type='text/javascript' " +
                    "src='{0}html/highlight.js'></script>", pathToRoot);

                pos = content.IndexOf("</head>");

                // Create a <head> section if one doesn't exist
                if(pos == -1)
                {
                    script = "<head>" + script + "</head>";
                    pos = content.IndexOf("<html>");

                    if(pos != -1)
                        pos += 6;
                    else
                        pos = 0;
                }

                content = content.Insert(pos, script);

                // Copy the colorizer files if not already there
                if(!File.Exists(workingFolder + @"Output\html\highlight.css"))
                {
                    syntaxFile = shfbFolder + @"Colorizer\highlight.css";
                    File.Copy(syntaxFile, workingFolder +
                        @"Output\html\highlight.css");
                    File.SetAttributes(syntaxFile, FileAttributes.Normal);

                    syntaxFile = shfbFolder + @"Colorizer\highlight.js";
                    File.Copy(syntaxFile, workingFolder +
                        @"Output\html\highlight.js");
                    File.SetAttributes(syntaxFile, FileAttributes.Normal);

                    // This one may exist as the default presentation styles
                    // contain an image by this name.
                    syntaxFile = shfbFolder + @"Colorizer\CopyCode.gif";
                    File.Copy(syntaxFile, workingFolder +
                        @"Output\icons\CopyCode.gif", true);
                    File.SetAttributes(syntaxFile, FileAttributes.Normal);
                }
            }

            // Replace project option tags with project option values
            if(entry.HasProjectTags)
            {
                content = reProjectTags.Replace(content, fieldMatchEval);

                // Shared content items can be nested
                while(reSharedContent.IsMatch(content))
                    content = reSharedContent.Replace(content, contentMatchEval);
            }

            // Write the file back out with the appropriate encoding
            using(StreamWriter sw = new StreamWriter(destFile, false, enc))
            {
                sw.Write(content);
            }
        }

        /// <summary>
        /// Replace a link to a namespace item with a link to the HTML page
        /// for it.
        /// </summary>
        /// <param name="match">The match that was found</param>
        /// <returns>The string to use as the replacement</returns>
        private string OnLinkMatch(Match match)
        {
            XmlNodeList elements;
            string link, text;

            link = match.Groups["Link"].Value;
            text = match.Groups["Text"].Value;

            // Use the link as the text if no text is specified
            if(String.IsNullOrEmpty(text))
                text = link;

            // If it looks like we've got a prefix, try for a "starts with"
            // match.  If not, try for a substring match after the prefix.
            // This should give the best results before going more general.
            if(link.IndexOf(':') != -1)
                elements = reflectionInfo.SelectNodes(
                    "//apis/api[starts-with(@id,'" + link + "')]/file");
            else
                elements = reflectionInfo.SelectNodes(
                    "//apis/api[substring-after(@id,':') = '" + link + "']/file");

            // Find all nodes containing the text if the above did't find
            // anything and use the first one found.
            if(elements.Count == 0)
                elements = reflectionInfo.SelectNodes(
                    "//apis/api[contains(@id,'" + link + "')]/file");

            // If no matches are found, return the text bolded
            if(elements.Count == 0)
            {
                this.ReportProgress("\tResolve Links: No matches found " +
                    "for '{0}'", link);
                return String.Format(CultureInfo.InvariantCulture,
                    "<b>{0}</b>", text);
            }

            // If one match is found, return it formatted as a link
            if(elements.Count == 1)
                this.ReportProgress("\tResolve Links: Matched '{0}' to '{1}'",
                    link, elements[0].ParentNode.Attributes["id"].Value);
            else
            {
                // If multiple matches are found, issue a warning, dump all
                // matches, and then use first match as the link.
                this.ReportProgress("\tResolve Links: Multiple matches " +
                    "found for '{0}':", link);

                foreach(XmlNode n in elements)
                    this.ReportProgress("\t\t{0}",
                        n.ParentNode.Attributes["id"].Value);

                this.ReportProgress("\t\tUsing '{0}' for link",
                    elements[0].ParentNode.Attributes["id"].Value);
            }

            return String.Format(CultureInfo.InvariantCulture,
                "<a href=\"{0}html/{1}.htm\">{2}</a>", pathToRoot,
                elements[0].Attributes["name"].Value, text);
        }

        /// <summary>
        /// Replace a shared content item with it's value.  Note that these
        /// may be nested.
        /// </summary>
        /// <param name="match">The match that was found</param>
        /// <returns>The string to use as the replacement</returns>
        private string OnContentMatch(Match match)
        {
            string content = String.Empty;

            // Load the shared content file on first use
            if(sharedContent == null)
            {
                sharedContent = new XmlDocument();
                sharedContent.Load(workingFolder + "shared_content.xml");
            }

            XmlNode item = sharedContent.SelectSingleNode("//item[@id='" +
                match.Groups["Item"] + "']");

            if(item != null)
                content = item.InnerXml;

            return content;
        }

        /// <summary>
        /// This is used to load a code block from an external file.
        /// </summary>
        /// <returns>The HTML encoded block extracted from the file and
        /// wrapped in a &lt;pre&gt; tag ready for colorizing.</returns>
        /// <remarks>If a region attribute is found, only the named region
        /// is returned.  If n region attribute is found, the whole file is
        /// returned.  Relative paths are assumed to be relative to the
        /// project folder.</remarks>
        private string OnCodeBlockMatch(Match match)
        {
            Regex reFindRegion;
            Match find, m;

            string sourceFile = null, region = null, codeBlock = null,
                options = match.Groups[1].Value + match.Groups[3].Value;

            sourceFile = match.Groups[2].Value;

            try
            {
                if(!Path.IsPathRooted(sourceFile))
                    sourceFile = Path.GetFullPath(projectFolder + sourceFile);

                using(StreamReader sr = new StreamReader(sourceFile))
                {
                    codeBlock = sr.ReadToEnd();
                }
            }
            catch(ArgumentException argEx)
            {
                throw new BuilderException(String.Format(
                    CultureInfo.InvariantCulture, "Possible invalid path " +
                    "'{0}{1}'.", projectFolder, sourceFile), argEx);
            }
            catch(IOException ioEx)
            {
                throw new BuilderException(String.Format(
                    CultureInfo.InvariantCulture, "Unable to load source " +
                    "file '{0}'.", sourceFile), ioEx);
            }

            // If no region is specified, the whole file is included
            m = reCodeRegion.Match(options);

            if(m.Success)
            {
                region = m.Groups[1].Value;
                options = options.Remove(m.Index, m.Length);

                // Find the start of the region.  This gives us an immediate
                // starting match on the second search and we can look for the
                // matching #endregion without caring about the region name.
                // Otherwise, nested regions get in the way and complicate
                // things.
                reFindRegion = new Regex("#region\\s+\"?" +
                    Regex.Escape(region), RegexOptions.IgnoreCase);

                find = reFindRegion.Match(codeBlock);

                if(!find.Success)
                    throw new BuilderException(String.Format(
                        CultureInfo.InvariantCulture, "Unable to locate " +
                        "start of region '{0}' in source file {1}", region,
                        sourceFile));

                // Find the end of the region taking into account any
                // nested regions.
                m = reMatchRegion.Match(codeBlock, find.Index);

                if(!m.Success)
                    throw new BuilderException(String.Format(
                        CultureInfo.InvariantCulture, "Unable to extract " +
                        "region '{0}' in source file {1}{2} (missing " +
                        "#endregion?)", region, projectFolder, sourceFile));

                // Extract just the specified region starting after the
                // description.
                codeBlock = m.Groups[1].Value.Substring(
                    m.Groups[1].Value.IndexOf('\n') + 1);

                // Strip off the trailing single quote if present (VB.NET,
                // commented #End Region statement within a method body).
                if(codeBlock.EndsWith("'"))
                    codeBlock = codeBlock.Substring(0, codeBlock.Length - 1);
            }

            // Return the HTML encoded block
            return "<pre " + options + ">" + HttpUtility.HtmlEncode(
                codeBlock) + "</pre>";
        }
    }
}
