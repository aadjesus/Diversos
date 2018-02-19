//=============================================================================
// System  : Sandcastle Help File Builder Components
// File    : PostTransformComponent.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 03/09/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a build component that is a companion to the
// CodeBlockComponent.  It is used to add the stylesheet and JavaScript
// links to the rendered HTML if the topic contains colorized code.  In
// addition, it can insert a logo image at the top of each help topic and,
// for the Prototype presentation style, hook up the code blocks to the
// language filter and hide the language combo box if only one language
// appears in the Syntax section.  With a modification to the Sandcastle
// reference content files, it will also add version information to each topic.
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
// 1.3.3.0  11/23/2006  EFW  Created the code
// 1.4.0.0  01/31/2007  EFW  Added placement options for logo.  Made changes
//                           to support custom presentation styles.  Reworked
//                           version info code to improve performance when used
//                           with very large documentation builds.
//=============================================================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

using Microsoft.Ddue.Tools;

namespace SandcastleBuilder.Components
{
    /// <summary>
    /// This build component is is a companion to the
    /// <see cref="CodeBlockComponent"/>.  It is used to add the
    /// stylesheet and JavaScript links to the rendered HTML if the topic
    /// contains colorized code.  In addition, it can insert a logo image at
    /// the top of each help topic and, for the Prototype presentation style,
    /// hook up the code blocks to the language filter and hide the language
    /// combo box if only one language appears in the Syntax section.   With a
    /// modification to the Sandcastle reference content files, it will also
    /// add version information to each topic.
    /// </summary>
    /// <remarks>The colorizer files are only copied once and only if code is
    /// actually colorized.  If the files already exist (i.e. additional
    /// content has replaced them), they are not copied either.  That way, you
    /// can customize the color stylesheet as you see fit without modifying the
    /// default stylesheet.
    ///
    /// <p/>By adding "Version: {2}" to the <b>locationInformation</b> entry
    /// and the <b>requirementsAssemblyLayout</b> entry in the
    /// <b>reference_content.xml</b> file for both the Prototype and VS2005
    /// style content files, you can add version information to each topic.
    /// The help file builder uses a composite file with this fix already in
    /// place.</remarks>
    /// <example>
    /// <code lang="xml" title="Example configuration">
    /// &lt;!-- Post-transform component configuration.  This must
    ///      appear after the TransformComponent.  See also:
    ///      CodeBlockComponent. --&gt;
    /// &lt;component type="SandcastleBuilder.Components.PostTransformComponent"
    ///   assembly="C:\SandcastleComponents\SandcastleBuilder.Components.dll" &gt;
    ///     &lt;!-- Code colorizer files (required).
    ///          Attributes:
    ///             Stylesheet file (required)
    ///             Script file (required)
    ///             "Copy" image file (required) --&gt;
    ///     &lt;colorizer stylesheet="highlight.css" scriptFile="highlight.js"
    ///        copyImage="CopyCode.gif" /&gt;
    ///
    ///     &lt;!-- Output path for the files (required).  This should match
    ///          the output path of the HTML files (see SaveComponent). --&gt;
    ///     &lt;outputPath value="Output\html" /&gt;
    ///
    ///     &lt;!-- Logo image file (optional).  Filename is required.  The
    ///          height, width, altText, placement, and alignment attributes
    ///          are optional. --&gt;
    ///     &lt;logoFile filename="Logo.jpg" height="64" width="64"
    ///        altText="Test Logo" placement="left" alignment="left" /&gt;
    /// &lt;/component&gt;
    /// </code>
    /// </example>
    public class PostTransformComponent : BuildComponent
    {
        #region Logo placement enumerations
        /// <summary>
        /// This enumeration defines the logo placement options
        /// </summary>
        public enum LogoPlacement
        {
            /// <summary>Place the logo to the left of the header text
            /// (the default).</summary>
            Left,
            /// <summary>Place the logo to the right of the header text.</summary>
            Right,
            /// <summary>Place the logo above the header text.</summary>
            Above
        }

        /// <summary>
        /// This enumeration defines the logo alignment options when placement
        /// is set to <b>Above</b>.
        /// </summary>
        public enum LogoAlignment
        {
            /// <summary>Left-align the logo (the default).</summary>
            Left,
            /// <summary>Right-align the logo.</summary>
            Right,
            /// <summary>Center the logo.</summary>
            Center
        }
        #endregion

        #region Private data members
        //=====================================================================
        // Private data members

        // The stylesheet, script, and image files to include and the output
        // path.
        private string stylesheet, scriptFile, copyImage, outputPath;

        // Logo properties
        private string logoFilename, logoAltText, alignment;
        private int logoHeight, logoWidth;
        private LogoPlacement placement;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assembler">A reference to the build assembler.</param>
        /// <param name="configuration">The configuration information</param>
        /// <exception cref="ConfigurationErrorsException">This is thrown if
        /// an error is detected in the configuration.</exception>
        public PostTransformComponent(BuildAssembler assembler,
          XPathNavigator configuration) : base(assembler, configuration)
        {
            XPathNavigator nav;
            string attr;

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);

            base.WriteMessage(MessageLevel.Info, String.Format(
                CultureInfo.InvariantCulture,
                "\r\n    [{0}, version {1}]\r\n    Post-Transform Component. " +
                "{2}\r\n    http://www.codeplex.com/SHFB", fvi.ProductName,
                fvi.ProductVersion, fvi.LegalCopyright));

            // The <colorizer> element is required and defines the colorizer
            // file locations.
            nav = configuration.SelectSingleNode("colorizer");
            if(nav == null)
                throw new ConfigurationErrorsException("You must specify " +
                    "a <colorizer> element to define the code colorizer " +
                    "files.");

            // All of the attributes are required
            stylesheet = nav.GetAttribute("stylesheet", String.Empty);
            scriptFile = nav.GetAttribute("scriptFile", String.Empty);
            copyImage = nav.GetAttribute("copyImage", String.Empty);

            if(String.IsNullOrEmpty(stylesheet))
                throw new ConfigurationErrorsException("You must specify a " +
                    "'stylesheet' attribute on the <colorizer> element");

            if(String.IsNullOrEmpty(scriptFile))
                throw new ConfigurationErrorsException("You must specify a " +
                    "'scriptFile' attribute on the <colorizer> element");

            if(String.IsNullOrEmpty(copyImage))
                throw new ConfigurationErrorsException("You must specify a " +
                    "'copyImage' attribute on the <colorizer> element");

            // This specifies the output and the XPath expression used
            // to get the filename.  If the base class found them, we will.
            nav = configuration.SelectSingleNode("outputPath");
            if(nav != null)
                outputPath = nav.GetAttribute("value", String.Empty);

            if(String.IsNullOrEmpty(outputPath))
                throw new ConfigurationErrorsException("You must specify a " +
                    "'value' attribute on the <outputPath> element");

            // All present.  Make sure they exist.
            stylesheet = Path.GetFullPath(stylesheet);
            scriptFile = Path.GetFullPath(scriptFile);
            copyImage = Path.GetFullPath(copyImage);

            if(!outputPath.EndsWith(@"\"))
                outputPath += @"\";

            if(!File.Exists(stylesheet))
                throw new ConfigurationErrorsException("Could not find " +
                    "stylesheet file: " + stylesheet);

            if(!File.Exists(stylesheet))
                throw new ConfigurationErrorsException("Could not find " +
                    "script file: " + scriptFile);

            if(!File.Exists(copyImage))
                throw new ConfigurationErrorsException("Could not find " +
                    "image file: " + copyImage);

            if(!Directory.Exists(outputPath))
                throw new ConfigurationErrorsException("The output path '" +
                    outputPath + "' must exist");

            // The logo element is optional.  The file must exist if
            // specified.
            nav = configuration.SelectSingleNode("logoFile");
            if(nav != null)
            {
                logoFilename = nav.GetAttribute("filename", String.Empty);

                if(!String.IsNullOrEmpty(logoFilename))
                {
                    if(!File.Exists(logoFilename))
                        throw new ConfigurationErrorsException("The logo " +
                            "file '" + logoFilename + "' must exist");

                    logoAltText = nav.GetAttribute("altText", String.Empty);

                    attr = nav.GetAttribute("height", String.Empty);
                    if(!String.IsNullOrEmpty(attr))
                        if(!Int32.TryParse(attr, out logoHeight))
                            throw new ConfigurationErrorsException("The logo " +
                                "height must be an integer value");

                    attr = nav.GetAttribute("width", String.Empty);
                    if(!String.IsNullOrEmpty(attr))
                        if(!Int32.TryParse(attr, out logoWidth))
                            throw new ConfigurationErrorsException("The logo " +
                                "width must be an integer value");

                    // Ignore them if negative
                    if(logoHeight < 0)
                        logoHeight = 0;

                    if(logoWidth < 0)
                        logoWidth = 0;

                    // Placement and alignment are optional
                    attr = nav.GetAttribute("placement", String.Empty);
                    if(!String.IsNullOrEmpty(attr))
                        placement = (LogoPlacement)Enum.Parse(
                            typeof(LogoPlacement), attr, true);
                    else
                        placement = LogoPlacement.Left;

                    attr = nav.GetAttribute("alignment", String.Empty);
                    if(!String.IsNullOrEmpty(attr))
                        alignment = attr;
                    else
                        alignment = "left";
                }
            }
        }
        #endregion

        #region Apply the component
        /// <summary>
        /// This is implemented to perform the post-transformation tasks.
        /// </summary>
        /// <param name="document">The XML document with which to work.</param>
        /// <param name="key">The key (member name) of the item being
        /// documented.</param>
        public override void Apply(XmlDocument document, string key)
        {
            XmlNode head, node;
            XmlAttribute attr;
            string destStylesheet, destScriptFile, destImageFile;

            // Add the version information if possible
            if(VersionInfoComponent.ItemVersion != null)
                PostTransformComponent.AddVersionInfo(document);

            // For the Prototype style, hide the dropdown if there's only
            // one language.  The VS2005 ignores the language settings and
            // shows everything in the dropdown.  We could fix that to but
            // it will require a bit more work.  Maybe later...
            node = document.SelectSingleNode("//select[@id='languageSelector']");

            if(node != null && node.SelectNodes("//option").Count == 1)
            {
                attr = document.CreateAttribute("style");
                attr.Value = "visibility: hidden;";
                node.Attributes.Append(attr);
            }

            // Add the logo?
            if(!String.IsNullOrEmpty(logoFilename))
                this.AddLogo(document);

            // Don't bother with the rest if the topic contains no code that
            // needs the files.
            if(!CodeBlockComponent.NeedsHighlightFiles)
                return;

            // Only copy the files if needed
            destStylesheet = outputPath + Path.GetFileName(stylesheet);
            destScriptFile = outputPath + Path.GetFileName(scriptFile);
            destImageFile = outputPath +
                CodeBlockComponent.CopyImageLocation.Replace("/", @"\");

            // All attributes are turned off so that we can delete it later
            if(!File.Exists(destStylesheet))
            {
                File.Copy(stylesheet, destStylesheet);
                File.SetAttributes(destStylesheet, FileAttributes.Normal);
            }

            if(!File.Exists(destScriptFile))
            {
                File.Copy(scriptFile, destScriptFile);
                File.SetAttributes(destScriptFile, FileAttributes.Normal);
            }

            // Always copy the image file, it may be different
            File.Copy(copyImage, destImageFile, true);
            File.SetAttributes(destImageFile, FileAttributes.Normal);

            // Find the <head> section
            head = document.SelectSingleNode("//head");

            if(head == null)
            {
                base.WriteMessage(MessageLevel.Error,
                    "<head> section not found!  Could not insert links.");
                return;
            }

            // Add the link to the stylesheet
            node = document.CreateNode(XmlNodeType.Element, "link", null);

            attr = document.CreateAttribute("type");
            attr.Value = "text/css";
            node.Attributes.Append(attr);

            attr = document.CreateAttribute("rel");
            attr.Value = "stylesheet";
            node.Attributes.Append(attr);

            attr = document.CreateAttribute("href");
            attr.Value = Path.GetFileName(stylesheet);
            node.Attributes.Append(attr);

            head.AppendChild(node);

            // Add the link to the script
            node = document.CreateNode(XmlNodeType.Element, "script", null);

            attr = document.CreateAttribute("type");
            attr.Value = "text/javascript";
            node.Attributes.Append(attr);

            attr = document.CreateAttribute("src");
            attr.Value = Path.GetFileName(scriptFile);
            node.Attributes.Append(attr);

            // Script tags cannot be self-closing so set their inner text
            // to an empty string so that they render as an opening and a
            // closing tag.  Note that if null, InnerText returns an empty
            // string.  This looks redundant but it isn't.
            node.InnerText = String.Empty;

            head.AppendChild(node);

            // Strip out the Copy Code header used in the VS2005 style.  It
            // doesn't work with the CodeBlockComponent code blocks.
            XmlNodeList codeTitles = document.SelectNodes(
                "//span[@class='copyCode']");

            foreach(XmlNode copyCode in codeTitles)
            {
                node = copyCode.ParentNode.ParentNode;

                // Remove colspan attribute from next sibling's first child
                attr = node.NextSibling.ChildNodes[0].SelectSingleNode(
                    "//@colspan") as XmlAttribute;

                if(attr != null)
                    node.NextSibling.ChildNodes[0].Attributes.Remove(attr);

                node.ParentNode.RemoveChild(node);
            }

            // Swap the literal "Copy" text with an include item so that it
            // gets localized.
            codeTitles = document.SelectNodes(
                "//span[@class='highlight-copycode']");

            foreach(XmlNode span in codeTitles)
                span.InnerXml = span.InnerXml.Replace(
                    " " + CodeBlockComponent.CopyText,
                    " <include item=\"copyCode\"/>");

            // If there are code blocks associated with the Prototype style,
            // connect them to the language filter.
            codeTitles = document.SelectNodes("//div/@cbc-lang/..");
            if(codeTitles.Count != 0)
                if(!PostTransformComponent.ConnectLanguageFilter(document,
                  codeTitles))
                    base.WriteMessage(MessageLevel.Warn, "A required section " +
                        "was not found and language filtering will not work.");
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// This is used to add version information to the topic
        /// </summary>
        /// <param name="document">The document to modify</param>
        /// <remarks>This requires a modification to the Sandcastle style
        /// file reference_content.xml (Prototype and VS2005).  The help file
        /// builder uses a composite file for both and it includes the fix.
        /// This can go away once version information is supported by
        /// Sandcastle itself.  The request has been made.</remarks>
        private static void AddVersionInfo(XmlDocument document)
        {
            XmlNode locationInfo, parameter, footer;

            // Prototype style...
            locationInfo = document.SelectSingleNode(
                "//include[@item='locationInformation']");

            // ... or VS2005 style?
            if(locationInfo == null)
            {
                locationInfo = document.SelectSingleNode(
                    "//include[@item='requirementsAssemblyLayout']");

                if(locationInfo == null)
                    locationInfo = document.SelectSingleNode(
                        "//include[@item='assemblyNameAndModule']");
            }
            else
            {
                // For prototype, move the version information below
                // the footer.
                footer = document.SelectSingleNode("//include[@item='footer']");

                if(footer != null)
                {
                    footer.ParentNode.RemoveChild(footer);
                    locationInfo.ParentNode.InsertBefore(footer, locationInfo);
                }
            }

            if(locationInfo != null)
            {
                parameter = document.CreateNode(XmlNodeType.Element,
                    "parameter", null);
                parameter.InnerXml = VersionInfoComponent.ItemVersion;

                locationInfo.AppendChild(parameter);
            }
        }

        /// <summary>
        /// This is called to add the logo to the page header area
        /// </summary>
        /// <param name="document">The document to which the logo is added.</param>
        private void AddLogo(XmlDocument document)
        {
            XmlNode div;
            string imgWidth, imgHeight, imgAltText, filename, destFile;

            filename = Path.GetFileName(logoFilename);
            destFile = outputPath + filename;

            // Copy the logo to the output folder if not there already.
            // All attributes are turned off so that we can delete it later.
            if(!File.Exists(destFile))
            {
                File.Copy(logoFilename, destFile);
                File.SetAttributes(destFile, FileAttributes.Normal);
            }

            imgAltText = (String.IsNullOrEmpty(logoAltText)) ? String.Empty :
                " alt='" + logoAltText + "'";
            imgWidth = (logoWidth == 0) ? String.Empty : " width='" +
                logoWidth.ToString(CultureInfo.InvariantCulture) + "'";
            imgHeight = (logoHeight == 0) ? String.Empty : " height='" +
                logoHeight.ToString(CultureInfo.InvariantCulture) + "'";

            div = document.SelectSingleNode("//div[@id='control']");

            // Prototype style?
            if(div != null)
            {
                // Wrap the header <div> in a table with the image based on
                // the placement option.
                switch(placement)
                {
                    case LogoPlacement.Left:
                        div.InnerXml = String.Format(
                            CultureInfo.InvariantCulture,
                            "<table border='0' width='100%' cellpadding='0' " +
                            "cellspacing='0'><tr><td align='center' " +
                            "valign='top' style='padding-right: 10px'>" +
                            "<img src='{0}'{1}{2}{3}/></td><td valign='top' " +
                            "width='100%'>{4}</td></tr></table>", filename,
                            imgAltText, imgWidth, imgHeight, div.InnerXml);
                        break;

                    case LogoPlacement.Right:
                        div.InnerXml = String.Format(
                            CultureInfo.InvariantCulture,
                            "<table border='0' width='100%' cellpadding='0' " +
                            "cellspacing='0'><tr><td valign='top' " +
                            "width='100%'>{0}</td><td align='center' " +
                            "valign='top' style='padding-left: 10px'>" +
                            "<img src='{1}'{2}{3}{4}/></td></tr></table>",
                            div.InnerXml, filename, imgAltText, imgWidth,
                            imgHeight);
                        break;

                    case LogoPlacement.Above:
                        div.InnerXml = String.Format(
                            CultureInfo.InvariantCulture,
                            "<table border='0' width='100%' cellpadding='0' " +
                            "cellspacing='0'><tr><td align='{0}' " +
                            "style='padding-bottom: 5px'><img src='{1}'" +
                            "{2}{3}{4}/></td></tr><tr><td valign='top' " +
                            "width='100%'>{5}</td></tr></table>", alignment,
                            filename, imgAltText, imgWidth, imgHeight,
                            div.InnerXml);
                        break;
                }
            }
            else
            {
                // VS2005 style
                div = document.SelectSingleNode("//table[@id='topTable']");

                if(div == null)
                {
                    base.WriteMessage(MessageLevel.Error, "Unable to locate " +
                        "'control' <div> or 'topTable' <table> to insert logo.");
                    return;
                }

                switch(placement)
                {
                    case LogoPlacement.Left:
                        // Insert a new row with a cell spanning all rows
                        div.InnerXml = String.Format(
                            CultureInfo.InvariantCulture,
                            "<tr><td rowspan='4' align='center' valign='top' " +
                            "style='width: 1px; padding: 0px'>" +
                            "<img src='{0}'{1}{2}{3}/></td></tr>{4}",
                            filename, imgAltText, imgWidth, imgHeight,
                            div.InnerXml);
                        break;

                    case LogoPlacement.Right:
                        // For this, we add a second cell to the first row
                        // that spans all rows.
                        div = div.ChildNodes[0];
                        div.InnerXml += String.Format(
                            CultureInfo.InvariantCulture,
                            "<td rowspan='3' align='center' valign='top' " +
                            "style='width: 1px; padding: 0px'>" +
                            "<img src='{0}'{1}{2}{3}/></td>",
                            filename, imgAltText, imgWidth, imgHeight);
                        break;

                    case LogoPlacement.Above:
                        // Add a new first row
                        div.InnerXml = String.Format(
                            CultureInfo.InvariantCulture,
                            "<tr><td align='{0}'><img src='{1}'{2}{3}{4}/>" +
                            "</td></tr>{5}", alignment, filename, imgAltText,
                            imgWidth, imgHeight, div.InnerXml);
                        break;
                }
            }
        }

        /// <summary>
        /// This is used to connect the code blocks in the Prototype style
        /// to the language filter.
        /// </summary>
        /// <param name="document">The document</param>
        /// <param name="codeTitles">The list of title DIVs associated with
        /// code blocks that need hooking up to the language filter</param>
        /// <returns>Returns true if successful or false on failure</returns>
        private static bool ConnectLanguageFilter(XmlDocument document,
          XmlNodeList codeTitles)
        {
            StringBuilder sb;
            List<string> idList;
            XmlAttribute tempLang, id, lang;
            XmlNode code, script, body;
            int uniqueID = 1;

            script = document.SelectSingleNode("//script[contains(text(), " +
                "'var sb = ')]");

            // If this isn't found, assume it's the VS2005 style.  Just
            // remove any placeholder title DIVs.
            if(script == null)
            {
                foreach(XmlNode title in codeTitles)
                    if(title.InnerText == "@CBC_REMOVE")
                        title.ParentNode.RemoveChild(title);

                return true;
            }

            // Add IDs to each title and code block
            idList = new List<string>();

            foreach(XmlNode title in codeTitles)
            {
                tempLang = title.Attributes["cbc-lang"];
                code = title.NextSibling;

                lang = document.CreateAttribute("x-lang");
                lang.Value = tempLang.Value;

                id = document.CreateAttribute("id");
                id.Value = String.Format(CultureInfo.InvariantCulture,
                    "cb_title_{0}", uniqueID);

                // If the title was a place holder, get rid of it
                if(title.InnerText == "@CBC_REMOVE")
                    title.ParentNode.RemoveChild(title);
                else
                {
                    title.Attributes.Remove(tempLang);
                    title.Attributes.Append(lang);
                    title.Attributes.Append(id);
                    idList.Add(id.Value);
                }

                // Clone the attributes for the code block and change the ID
                lang = (XmlAttribute)lang.Clone();
                id = (XmlAttribute)id.Clone();
                id.Value = String.Format(CultureInfo.InvariantCulture,
                    "cb_code_{0}", uniqueID);

                code.Attributes.Append(lang);
                code.Attributes.Append(id);
                idList.Add(id.Value);

                uniqueID++;
            }

            // Now add the JavaScript to register the IDs and refresh the
            // currently displayed elements.
            sb = new StringBuilder(1024);

            foreach(string idTitle in idList)
                sb.AppendFormat("sb.elements.push(document.getElementById(" +
                    "'{0}'));\r\n", idTitle);

            // Add the code to update the current filter after adding
            // all of our elements.
            string[] lines = script.InnerText.Split('\n');

            for(int idx = lines.Length - 1; idx > 0; idx--)
                if(lines[idx].IndexOf("sb.toggleStyle") != -1)
                {
                    sb.Append(lines[idx]);
                    sb.Append('\n');
                    break;
                }

            script = document.CreateNode(XmlNodeType.Element, "script", null);
            lang = document.CreateAttribute("type");
            lang.Value = "text/javascript";
            script.Attributes.Append(lang);
            script.InnerText = sb.ToString();

            body = document.SelectSingleNode("//body");

            if(body == null)
                return false;

            body.AppendChild(script);
            return true;
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
            using(PostTransformConfigDlg dlg =
              new PostTransformConfigDlg(currentConfig))
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                    currentConfig = dlg.Configuration;
            }

            return currentConfig;
        }
        #endregion
    }
}
