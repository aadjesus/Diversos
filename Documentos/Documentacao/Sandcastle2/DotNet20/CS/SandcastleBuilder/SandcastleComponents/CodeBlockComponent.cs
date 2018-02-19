//=============================================================================
// System  : Sandcastle Help File Builder Components
// File    : CodeBlockComponent.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 03/09/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a build component that is used to search for <code> XML
// comment tags and colorize the code within them.  It can also include code
// from an external file or region within a file first.  Note that this
// component must be paired with the PostTransformComponent.
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
// 1.3.3.0  11/21/2006  EFW  Created the code
// 1.3.4.0  01/03/2007  EFW  Added support for VB.NET style #region blocks
// 1.4.0.0  02/02/2007  EFW  Made changes to support custom presentation styles
//                           and new colorizer options.
//=============================================================================

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.XPath;

using Microsoft.Ddue.Tools;

using ColorizerLibrary;

namespace SandcastleBuilder.Components
{
    /// <summary>
    /// This build component is used to search for &lt;code&gt; XML comment
    /// tags and colorize the code within them.  It can also include code
    /// from an external file or a region within a file first.
    /// </summary>
    /// <remarks>Note that this component must be paired with the
    /// <see cref="PostTransformComponent"/>.</remarks>
    /// <example>
    /// <code lang="xml" title="Example configuration">
    /// &lt;!-- Code block component configuration.  This must appear before
    ///      the TransformComponent.  See also: PostTransformComponent. --&gt;
    /// &lt;component type="SandcastleBuilder.Components.CodeBlockComponent"
    ///   assembly="C:\SandcastleBuilder\SandcastleBuilder.Components.dll"&gt;
    ///     &lt;!-- Base path for relative filenames in source
    ///          attributes (optional). --&gt;
    ///     &lt;basePath value="..\SandcastleComponents" /&gt;
    ///
    ///     &lt;!-- Connect to language filter (optional).  If omitted,
    ///          language filtering is enabled by default. --&gt;
    ///     &lt;languageFilter value="true" /&gt;
    ///
    ///     &lt;!-- Code colorizer options (required).
    ///       Attributes:
    ///         Language syntax configuration file (required)
    ///         XSLT style file (required)
    ///         "Copy" image file URL (required)
    ///         Default language (optional)
    ///         Enable line numbering (optional)
    ///         Enable outlining (optional)
    ///         Tab size override (optional, 0 = Use syntax file setting)
    ///         Use language name as default title (optional) --&gt;
    ///     &lt;colorizer syntaxFile="highlight.xml" styleFile="highlight.xsl"
    ///       copyImageUrl="CopyCode.gif" language="cs" numberLines="false"
    ///       outlining="false" tabSize="0" defaultTitle="true" /&gt;
    /// &lt;/component&gt;
    /// </code>
    ///
    /// <code lang="xml" title="Examples as used in XML comments.">
    /// &lt;example&gt;
    /// A basic code block that uses the configuration defaults:
    /// &lt;code&gt;
    /// /// Code to colorize
    /// &lt;/code&gt;
    ///
    /// Override all options with block-specific options:
    /// &lt;code lang="xml" numberLines="true" outlining="false" tabSize="4" &gt;
    ///     &amp;lt;XmlTags/&amp;gt;
    /// &lt;/code&gt;
    ///
    /// An entire external file or a delimited region from it can be
    /// included.  This allows you to compile your example code externally
    /// to ensure that it is still valid and saves you from maintaining it
    /// in two places.
    ///
    /// Retrieve all code from an external file.  Use VB.NET syntax.
    /// &lt;code source="..\Examples\WholeDemo.vb" lang="vbnet"/&gt;
    ///
    /// Retrieve a specific #region from an external file.
    /// &lt;code source="..\Examples\SeveralExamples.vb"
    ///     region="Example 1" lang="vbnet"/&gt;
    ///
    /// &lt;example&gt;
    /// </code>
    /// </example>
    public class CodeBlockComponent : BuildComponent
    {
        #region Private data members
        //=====================================================================
        // Private data members

        // Code colorized flag and image location used by PostTransformComponent
        private static bool needsHighlightFiles;
        private static string copyImageLocation, copyText;

        private CodeColorizer colorizer;    // The code colorizer

        // Line numbering and outlining flags
        private bool numberLines, outliningEnabled;

        // The base path to use for file references with relative paths,
        // the syntax and style filenames, and the default language.
        private string basePath, syntaxFile, styleFile, copyImage,
            defaultLanguage;

        // Connect code blocks to the language filter if true
        private bool languageFilter;

        private int defaultTabSize;     // Default tab size

        // Uh, yeah.  Don't ask me to explain this.  Just accept that it
        // works (I hope :)).  It uses balancing groups to extract #region
        // to #endregion accounting for any nested regions within it.  If
        // you want to know all of the mind-bending details, Google for
        // the terms: regex "balancing group".
        private static Regex reMatchRegion = new Regex(
            @"\#region\s+(.*?(((?<Open>\#region\s+).*?)+" +
            @"((?<Close-Open>\#end\s?region).*?)+)*(?(Open)(?!)))" +
            @"\#end\s?region", RegexOptions.IgnoreCase |
            RegexOptions.Singleline);
        #endregion

        #region Properties
        /// <summary>
        /// This is used by the <see cref="PostTransformComponent"/> to see
        /// whether or not it should include the stylesheet and script files
        /// in the current topic.
        /// </summary>
        public static bool NeedsHighlightFiles
        {
            get { return needsHighlightFiles; }
        }

        /// <summary>
        /// This is used by the <see cref="PostTransformComponent"/> to get
        /// the destination location and filename of the "Copy" image.
        /// </summary>
        public static string CopyImageLocation
        {
            get { return copyImageLocation; }
        }

        /// <summary>
        /// This is used by the <see cref="PostTransformComponent"/> to get
        /// the "Copy" text so that it can be replaced with an include item.
        /// </summary>
        /// <remarks>We can't do it here as the TransformComponent strips the
        /// &lt;include&gt; tag as it isn't one it recognizes.</remarks>
        public static string CopyText
        {
            get { return copyText; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assembler">A reference to the build assembler.</param>
        /// <param name="configuration">The configuration information</param>
        /// <remarks><b>NOTE:</b>  This component must be paired with the
        /// <see cref="PostTransformComponent"/>.  See the
        /// <see cref="CodeBlockComponent"/> class topic for an example of the
        /// configuration and usage.</remarks>
        /// <exception cref="ConfigurationErrorsException">This is thrown if
        /// an error is detected in the configuration.</exception>
        public CodeBlockComponent(BuildAssembler assembler,
          XPathNavigator configuration) : base(assembler, configuration)
        {
            XPathNavigator nav;
            string value = null;
            bool useDefaultTitle = false;

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);

            base.WriteMessage(MessageLevel.Info, String.Format(
                CultureInfo.InvariantCulture,
                "\r\n    [{0}, version {1}]\r\n    Code Block Component. " +
                "{2}.\r\n    Portions copyright (c) 2003, Jonathan de " +
                "Halleux, All rights reserved.\r\n" +
                "    http://www.codeplex.com/SHFB", fvi.ProductName,
                fvi.ProductVersion, fvi.LegalCopyright));

            // The <basePath> element is optional.  If not set, it will assume
            // the current folder as the base path for source references with
            // relative paths.
            nav = configuration.SelectSingleNode("basePath");
            if(nav != null)
                basePath = nav.GetAttribute("value", String.Empty);

            if(String.IsNullOrEmpty(basePath))
                basePath = Directory.GetCurrentDirectory();

            if(!basePath.EndsWith(@"\"))
                basePath += @"\";

            // The <languageFilter> element is optional.  If not set, language
            // filtering is enabled by default.
            nav = configuration.SelectSingleNode("languageFilter");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out languageFilter))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <languageFilter> 'value' " +
                        "attribute.");
            }
            else
                languageFilter = true;

            // The <colorizer> element is required and defines the defaults
            // for the code colorizer.
            nav = configuration.SelectSingleNode("colorizer");
            if(nav == null)
                throw new ConfigurationErrorsException("You must specify " +
                    "a <colorizer> element to define the code colorizer " +
                    "options.");

            // The syntax configuration file and XSLT style file are required
            syntaxFile = nav.GetAttribute("syntaxFile", String.Empty);
            styleFile = nav.GetAttribute("styleFile", String.Empty);
            copyImage = nav.GetAttribute("copyImageUrl", String.Empty);

            if(String.IsNullOrEmpty(syntaxFile))
                throw new ConfigurationErrorsException("You must specify " +
                    "a 'syntaxFile' attribute on the <colorizer> element.");

            if(String.IsNullOrEmpty(styleFile))
                throw new ConfigurationErrorsException("You must specify " +
                    "a 'styleFile' attribute on the <colorizer> element.");

            if(String.IsNullOrEmpty(copyImage))
                throw new ConfigurationErrorsException("You must specify " +
                    "a 'copyImage' attribute on the <colorizer> element.");

            // The syntax and style files must also exist.  The "copy" image
            // is just a location and it doesn't have to exist yet.
            if(!File.Exists(syntaxFile))
                throw new ConfigurationErrorsException("The specified " +
                    "syntax file could not be found: " + syntaxFile);

            if(!File.Exists(styleFile))
                throw new ConfigurationErrorsException("The specified " +
                    "style file could not be found: " + styleFile);

            // Optional attributes
            defaultLanguage = nav.GetAttribute("language", String.Empty);
            if(String.IsNullOrEmpty(defaultLanguage))
                defaultLanguage = "none";

            value = nav.GetAttribute("numberLines", String.Empty);
            if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
              out numberLines))
                throw new ConfigurationErrorsException("You must specify " +
                    "a Boolean value for the 'numberLines' attribute.");

            value = nav.GetAttribute("outlining", String.Empty);
            if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
              out outliningEnabled))
                throw new ConfigurationErrorsException("You must specify " +
                    "a Boolean value for the 'outlining' attribute.");

            value = nav.GetAttribute("tabSize", String.Empty);
            if(!String.IsNullOrEmpty(value) && !Int32.TryParse(value,
              out defaultTabSize))
                throw new ConfigurationErrorsException("You must specify " +
                    "an integer value for the 'tabSize' attribute.");

            value = nav.GetAttribute("defaultTitle", String.Empty);
            if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
              out useDefaultTitle))
                throw new ConfigurationErrorsException("You must specify " +
                    "a Boolean value for the 'defaultTitle' attribute.");

            // Initialize the code colorizer
            colorizer = new CodeColorizer(syntaxFile, styleFile);
            colorizer.UseDefaultTitle = useDefaultTitle;
            colorizer.CopyImageUrl = copyImageLocation = copyImage;
            copyText = colorizer.CopyText;
        }
        #endregion

        #region Apply the component
        /// <summary>
        /// This is implemented to perform the code colorization.
        /// </summary>
        /// <param name="document">The XML document with which to work.</param>
        /// <param name="key">The key (member name) of the item being
        /// documented.</param>
        public override void Apply(XmlDocument document, string key)
        {
            XmlNodeList codeList;
            XmlNode titleDiv;

            string language, title, codeBlock;
            bool nbrLines, outline;
            int tabSize, start, end;

            // Reset the flag
            needsHighlightFiles = false;

            // Select all code nodes
            codeList = document.SelectNodes("//code");

            foreach(XmlNode code in codeList)
            {
                // Set the defaults
                language = defaultLanguage;
                nbrLines = numberLines;
                outline = outliningEnabled;
                tabSize = defaultTabSize;
                title = String.Empty;

                // If there's a source attribute, load the code from the file
                if(code.Attributes["source"] != null)
                    codeBlock = this.LoadCodeBlock(code);
                else
                    codeBlock = code.InnerXml;

                // Check for option overrides
                if(code.Attributes["numberLines"] != null)
                    nbrLines = Convert.ToBoolean(
                        code.Attributes["numberLines"].Value,
                        CultureInfo.InvariantCulture);

                if(code.Attributes["outlining"] != null)
                    outline = Convert.ToBoolean(
                        code.Attributes["outlining"].Value,
                        CultureInfo.InvariantCulture);

                if(code.Attributes["tabSize"] != null)
                    tabSize = Convert.ToInt32(
                        code.Attributes["numberLines"].Value,
                        CultureInfo.InvariantCulture);

                // If either language option is set to "none" or an unknown
                // language, it just strips excess leading whitespace and
                // optionally numbers the lines and adds outlining based on
                // the other settings.
                if(code.Attributes["lang"] != null)
                    language = code.Attributes["lang"].Value;

                // Use the title if one is supplied.
                if(code.Attributes["title"] != null)
                    title = HttpUtility.HtmlEncode(
                        code.Attributes["title"].Value);

                // Process the code.  The colorizer is built to highlight
                // <pre> tags in an HTML file so we'll wrap the code in a
                // <pre> tag with the settings.
                codeBlock = colorizer.ProcessAndHighlightText(
                    String.Format(CultureInfo.InvariantCulture,
                    "<pre lang=\"{0}\" numberLines=\"{1}\" " +
                    "outlining=\"{2}\" tabSize=\"{3}\" {4}>{5}</pre>",
                    language, nbrLines, outline, tabSize,
                    (title.Length != 0) ? "title=\"" + title + "\"" : String.Empty,
                    codeBlock));

                // Non-breaking spaces are replaced with a space entity.  If
                // not, they disappear in the rendered HTML.  Seems to be an
                // XML or XSLT thing.
                codeBlock = codeBlock.Replace("&nbsp;", "&#x20;");

                // Move the title above the code block so that it doesn't
                // interfere with the stuff generated by the transformation
                // component.  The post-transform component will perform some
                // clean-up to get rid of similar stuff added by it.
                if(codeBlock.StartsWith("<div"))
                {
                    title = codeBlock.Substring(0, codeBlock.IndexOf(
                        "</div>") + 6);
                    codeBlock = codeBlock.Substring(title.Length);

                    titleDiv = document.CreateDocumentFragment();
                    titleDiv.InnerXml = title;
                    titleDiv = titleDiv.ChildNodes[0];
                }
                else
                    titleDiv = null;

                // Remove the colorizer's <pre> tag and put it back into
                // the document.  The Sandcastle transform puts the code in
                // <div> and <pre> tags itself.
                start = codeBlock.IndexOf(">") + 1;
                end = codeBlock.LastIndexOf("<");

                code.InnerXml = codeBlock.Substring(start, end - start);

                // The <span> tags cannot be self-closing if empty.  The
                // colorizer renders them correctly but when written out as
                // XML, they get converted to self-closing tags which breaks
                // them.  To fix them, store an empty string in each empty
                // span so that it renders as an opening and closing tag.
                XmlNodeList spans = code.SelectNodes("//span");

                // Note that if null, InnerText returns an empty string
                // by default.  As such, this looks redundant but it
                // really isn't (see note above).
                foreach(XmlNode span in spans)
                    if(span.InnerText.Length == 0)
                        span.InnerText = String.Empty;

                // Add language filter stuff if needed or just the title if
                // there is one and we aren't using the language filter.
                if(languageFilter)
                    CodeBlockComponent.AddLanguageFilter(titleDiv, code, language);
                else
                    if(titleDiv != null)
                        code.ParentNode.InsertBefore(titleDiv, code);

                // Set the flag if needed to have the PostTransformComponent
                // include the stylesheet and script files.
                if(!needsHighlightFiles && (outline || language != "none" ||
                  titleDiv != null))
                    needsHighlightFiles = true;
            }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// This is used to load a code block from an external file.
        /// </summary>
        /// <returns>The HTML encoded block extracted from the file.</returns>
        private string LoadCodeBlock(XmlNode code)
        {
            Regex reFindRegion;
            Match find, m;

            string sourceFile = null, region = null, codeBlock = null;

            sourceFile = code.Attributes["source"].Value;

            try
            {
                if(!Path.IsPathRooted(sourceFile))
                    sourceFile = Path.GetFullPath(basePath + sourceFile);

                using(StreamReader sr = new StreamReader(sourceFile))
                {
                    codeBlock = sr.ReadToEnd();
                }
            }
            catch(ArgumentException argEx)
            {
                base.WriteMessage(MessageLevel.Error, String.Format(
                    CultureInfo.InvariantCulture,
                    "Possible invalid path '{0}{1}'.  Error: {2}",
                    basePath, sourceFile, argEx.Message));
                return "!ERROR: See log file!";
            }
            catch(IOException ioEx)
            {
                base.WriteMessage(MessageLevel.Error, String.Format(
                    CultureInfo.InvariantCulture,
                    "Unable to load source file '{0}'.  Error: {1}",
                    sourceFile, ioEx.Message));
                return "!ERROR: See log file!";
            }

            // If no region is specified, the whole file is included
            if(code.Attributes["region"] != null)
            {
                region = code.Attributes["region"].Value;

                // Find the start of the region.  This gives us an immediate
                // starting match on the second search and we can look for the
                // matching #endregion without caring about the region name.
                // Otherwise, nested regions get in the way and complicate
                // things.
                reFindRegion = new Regex("#region\\s+\"?" +
                    Regex.Escape(region), RegexOptions.IgnoreCase);

                find = reFindRegion.Match(codeBlock);

                if(!find.Success)
                {
                    base.WriteMessage(MessageLevel.Error, String.Format(
                        CultureInfo.InvariantCulture,
                        "Unable to locate start of region '{0}' in source " +
                        "file {1}", region, sourceFile));
                    return "!ERROR: See log file!";
                }

                // Find the end of the region taking into account any
                // nested regions.
                m = reMatchRegion.Match(codeBlock, find.Index);

                if(!m.Success)
                {
                    base.WriteMessage(MessageLevel.Error, String.Format(
                        CultureInfo.InvariantCulture,
                        "Unable to extract region '{0}' in source file " +
                        "{1}{2} (missing #endregion?)", region, basePath,
                        sourceFile));
                    return "!ERROR: See log file!";
                }

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
            return HttpUtility.HtmlEncode(codeBlock);
        }

        /// <summary>
        /// This is used to add language filter IDs for the Prototype and
        /// VS2005 styles so that the code block is shown or hidden based on
        /// the language filter selection.
        /// </summary>
        /// <param name="title">The title node if used</param>
        /// <param name="code">The code node</param>
        /// <param name="language">The language to use as the filter</param>
        /// <remarks>The <see cref="PostTransformComponent"/> adds the script
        /// necessary to register the code blocks and set their initial visible
        /// state in the Prototype style.</remarks>
        private static void AddLanguageFilter(XmlNode title, XmlNode code,
          string language)
        {
            XmlDocument document = code.OwnerDocument;
            XmlNode span, parent = code.ParentNode;
            XmlAttribute cbcAttr, langAttr;

            // Create a place holder title if needed to simplify things
            // in the post-transform component.
            if(title == null)
            {
                title = document.CreateNode(XmlNodeType.Element, "div", null);
                title.InnerText = "@CBC_REMOVE";
            }

            // Add the language filter attribute
            span = document.CreateNode(XmlNodeType.Element, "span", null);
            cbcAttr = document.CreateAttribute("cbc-lang");     // Prototype
            langAttr = document.CreateAttribute("codeLanguage");    // VS2005

            switch(language)
            {
                case "cs":
                    cbcAttr.Value = langAttr.Value = "CSharp";
                    break;

                case "vbnet":
                    cbcAttr.Value = langAttr.Value = "VisualBasic";
                    break;

                case "cpp":
                    cbcAttr.Value = langAttr.Value = "ManagedCPlusPlus";
                    break;

                case "jsharp":
                    cbcAttr.Value = langAttr.Value = "JSharp";
                    break;

                default:
                    // Unknown, don't apply the filter.  It'll show up for
                    // all languages.  Just add the title if needed.
                    cbcAttr.Value = String.Empty;
                    langAttr.Value = language;
                    break;
            }

            span.Attributes.Append(langAttr);

            // The post-transform component does all the hard work if it
            // finds this attribute.
            if(cbcAttr.Value.Length != 0)
                title.Attributes.Append(cbcAttr);

            parent.ReplaceChild(span, code);

            if(cbcAttr.Value.Length != 0 || title.InnerText != "@CBC_REMOVE")
                span.AppendChild(title);

            // The Prototype style doesn't need the enclosing span but it 
            // doesn't hurt anything.  With the introduction of custom styles
            // in v1.4.0.0, it needs to be generic so elements are added to
            // cover both styles.
            span.AppendChild(code);
        }
        #endregion

        #region Static configuration method for use with SHFB
        /// <summary>
        /// This static method is used by the Sandcastle Help File Builder to
        /// let the component perform its own configuration.
        /// </summary>
        /// <param name="currentConfig">The current configuration XML fragment</param>
        /// <returns>A string containing the new configuration XML fragment</returns>
        public static string ConfigureComponent(string currentConfig)
        {
            using(CodeBlockConfigDlg dlg = new CodeBlockConfigDlg(currentConfig))
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                    currentConfig = dlg.Configuration;
            }

            return currentConfig;
        }
        #endregion
    }
}
