//=============================================================================
// System  : Sandcastle Help File Builder Components
// File    : ShowMissingComponent.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 03/13/2007
// Note    : Copyright 2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a build component that is used to add "missing" notes
// for missing summary, parameter, returns, value, and remarks tags.  It can
// also add default summary documentation for constructors.
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
// 1.4.0.0  02/16/2007  EFW  Created the code
//=============================================================================

using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.XPath;

using Microsoft.Ddue.Tools;

namespace SandcastleBuilder.Components
{
    /// <summary>
    /// This build component is is used to add "missing" notes for missing
    /// summary, parameter, returns, value, and remarks tags.  It can also
    /// add default summary documentation for constructors.
    /// </summary>
    /// <example>
    /// <code lang="xml" title="Example configuration">
    /// &lt;!-- Show missing documentation component configuration.  This must
    ///      appear before the TransformComponent. --&gt;
    /// &lt;component type="SandcastleBuilder.Components.ShowMissingComponent"
    ///   assembly="C:\SandcastleBuilder\SandcastleBuilder.Components.dll"&gt;
    ///     &lt;!-- All elements are optional. --&gt;
    ///
    ///     &lt;!-- Auto-document constructors (true by default) --&gt;
    ///     &lt;AutoDocumentConstructors value="true" /&gt;
    ///
    ///     &lt;!-- Show missing param tags (true by default) --&gt;
    ///     &lt;ShowMissingParams value="true" /&gt;
    ///
    ///     &lt;!-- Show missing remarks tags (false by default) --&gt;
    ///     &lt;ShowMissingRemarks value="false" /&gt;
    ///
    ///     &lt;!-- Show missing returns tags (true by default) --&gt;
    ///     &lt;ShowMissingReturns value="true" /&gt;
    ///
    ///     &lt;!-- Show missing summary tags (true by default) --&gt;
    ///     &lt;ShowMissingSummaries value="true" /&gt;
    ///
    ///     &lt;!-- Show missing value tags (false by default) --&gt;
    ///     &lt;ShowMissingValues value="false" /&gt;
    /// &lt;/component&gt;
    /// </code>
    /// </example>
    public class ShowMissingComponent : BuildComponent
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private static Regex reStripWhitespace = new Regex(@"\s");

        private bool autoDocConstructors, showMissingParams,
            showMissingRemarks, showMissingReturns, showMissingSummaries,
            showMissingValues, isEnabled;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assembler">A reference to the build assembler.</param>
        /// <param name="configuration">The configuration information</param>
        /// <remarks>See the <see cref="ShowMissingComponent"/> class topic
        /// for an example of the configuration</remarks>
        /// <exception cref="ConfigurationErrorsException">This is thrown if
        /// an error is detected in the configuration.</exception>
        public ShowMissingComponent(BuildAssembler assembler,
          XPathNavigator configuration) : base(assembler, configuration)
        {
            XPathNavigator nav;
            string value = null;

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);

            base.WriteMessage(MessageLevel.Info, String.Format(
                CultureInfo.InvariantCulture,
                "\r\n    [{0}, version {1}]\r\n    Show Missing " +
                "Documentation Component. {2}\r\n    " +
                "http://www.codeplex.com/SHFB", fvi.ProductName,
                fvi.ProductVersion, fvi.LegalCopyright));

            // All elements are optional.  If omitted, all properties are
            // true except for showMissingRemarks and showMissingValues;
            autoDocConstructors = showMissingParams = showMissingReturns =
                showMissingSummaries = true;

            nav = configuration.SelectSingleNode("AutoDocumentConstructors");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out autoDocConstructors))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <AutoDocumentConstructors> " +
                        "'value' attribute.");
            }

            nav = configuration.SelectSingleNode("ShowMissingParams");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out showMissingParams))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <ShowMissingParams> " +
                        "'value' attribute.");
            }

            nav = configuration.SelectSingleNode("ShowMissingRemarks");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out showMissingRemarks))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <ShowMissingRemarks> " +
                        "'value' attribute.");
            }

            nav = configuration.SelectSingleNode("ShowMissingReturns");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out showMissingReturns))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <ShowMissingReturns> " +
                        "'value' attribute.");
            }

            nav = configuration.SelectSingleNode("ShowMissingSummaries");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out showMissingSummaries))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <ShowMissingSummaries> " +
                        "'value' attribute.");
            }

            nav = configuration.SelectSingleNode("ShowMissingValues");
            if(nav != null)
            {
                value = nav.GetAttribute("value", String.Empty);

                if(!String.IsNullOrEmpty(value) && !Boolean.TryParse(value,
                  out showMissingValues))
                    throw new ConfigurationErrorsException("You must specify " +
                        "a Boolean value for the <ShowMissingValues> " +
                        "'value' attribute.");
            }

            isEnabled = (autoDocConstructors || showMissingParams ||
                showMissingRemarks || showMissingReturns ||
                showMissingSummaries || showMissingValues);

            if(!isEnabled)
                base.WriteMessage(MessageLevel.Info, "  All Show Missing " +
                    "options are disabled.  The component will do nothing.");
        }
        #endregion

        #region Apply the component
        /// <summary>
        /// This is implemented to add the missing documentation tags
        /// </summary>
        /// <param name="document">The XML document with which to work.</param>
        /// <param name="key">The key (member name) of the item being
        /// documented.</param>
        public override void Apply(XmlDocument document, string key)
        {
            XmlNodeList items;
            XmlNode comments, returnsNode;

            // Don't bother if there is nothing to add
            if(!isEnabled || key[0] == 'R' || key[1] != ':')
                return;

            try
            {
                // Add missing tags based on the type of item it represents
                comments = document.SelectSingleNode("//comments");

                // All elements can have a summary
                if(showMissingSummaries || (autoDocConstructors &&
                  (key.Contains("#ctor") || key.Contains("#cctor"))))
                    this.CheckForMissingText(comments, key, "summary", document);

                // All elements can have remarks
                if(showMissingRemarks)
                    this.CheckForMissingText(comments, key, "remarks", null);

                // If it's a property, check for a missing <value> tag
                if(key[0] == 'P' && showMissingValues)
                    this.CheckForMissingText(comments, key, "value", null);
                else
                    if(key[0] == 'M')
                    {
                        // If it's a member, check for missing <returns>
                        // and <param> tags.
                        if(showMissingReturns)
                        {
                            returnsNode = document.SelectSingleNode(
                                "//reference/returns");

                            if(returnsNode != null)
                                this.CheckForMissingText(comments, key,
                                    "returns", null);
                        }

                        if(showMissingParams)
                        {
                            items = document.SelectNodes(
                                "//reference/parameters/parameter");

                            foreach(XmlNode p in items)
                                this.CheckForMissingParameter(comments, key,
                                    p.Attributes["name"].Value);
                        }
                    }
            }
            catch(Exception ex)
            {
                base.WriteMessage(MessageLevel.Error, "Error adding " +
                    "missing documentation tags: " + ex.Message);
            }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Check for missing text in the specified documentation tag and, if
        /// it doesn't exist or the text is blank, add a "missing" message as
        /// the documentation tag's text.
        /// </summary>
        /// <param name="comments">The comments node to check.</param>
        /// <param name="key">The key (name) for the current item</param>
        /// <param name="tagName">The tag type for which to check.</param>
        /// <param name="refInfo">The reflection information used to resolve
        /// the type names for summary tags.</param>
        private void CheckForMissingText(XmlNode comments, string key,
          string tagName, XmlDocument refInfo)
        {
            string text;
            XmlNode tag = comments.SelectSingleNode(tagName);

            if(tag == null)
            {
                tag = comments.OwnerDocument.CreateNode(XmlNodeType.Element,
                    tagName, null);
                comments.AppendChild(tag);
                text = String.Empty;
            }
            else
                text = reStripWhitespace.Replace(tag.InnerText, String.Empty);

            if(text.Length == 0)
            {
                // Auto document constructor?
                if(tagName == "summary" && (key.Contains("#ctor") ||
                  key.Contains("#cctor")) && autoDocConstructors)
                {
                    this.WriteMessage(MessageLevel.Info, "Auto-documenting " +
                        "constructor " + key);

                    if(key.Contains("#cctor"))
                        tag.InnerXml = "Static constructor";
                    else
                        tag.InnerXml = ShowMissingComponent.ResolveTypeLink(
                            key, refInfo);
                }
                else
                {
                    this.WriteMessage(MessageLevel.Warn, String.Format(
                        CultureInfo.InvariantCulture,
                        "Missing <{0}> documentation for {1}", tagName, key));

                    tag.InnerXml = String.Format(CultureInfo.InvariantCulture,
                        "<p style=\"color: #dc143c; font-size: 8.5pt; " +
                        "font-weight: bold;\">[Missing &lt;{0}&gt; " +
                        "documentation for {1}]</p>", tagName,
                        HttpUtility.HtmlEncode(key));
                }
            }
        }

        /// <summary>
        /// This is used to resolve a type name to a link used for auto
        /// documented constructors.
        /// </summary>
        /// <param name="typeName">The type name</param>
        /// <param name="document">The reflection information file</param>
        /// <returns></returns>
        private static string ResolveTypeLink(string typeName,
          XmlDocument document)
        {
            XmlNodeList templates;
            XmlNode filename;
            StringBuilder sb = new StringBuilder(256);
            bool isFirst = true;

            typeName = typeName.Substring(2, typeName.IndexOf(".#ctor") - 2);

            templates = document.SelectNodes("//templates/template");
            filename = document.SelectSingleNode("//reference/file");

            sb.Append("Initializes a new instance of the <a href=\"./");
            sb.Append(filename.Attributes["name"].Value);
            sb.Append(".htm\">");

            // Translate the template place holders
            if(templates.Count != 0)
            {
                typeName = typeName.Substring(typeName.LastIndexOf('.') + 1);
                sb.Append(typeName.Substring(0, typeName.IndexOf('`')));
                sb.Append("&lt;");

                foreach(XmlNode t in templates)
                {
                    if(isFirst)
                        isFirst = false;
                    else
                        sb.Append(",");

                    sb.Append(t.Attributes["name"].Value);
                }

                sb.Append("&gt;");
            }
            else
                sb.Append(typeName.Substring(typeName.LastIndexOf('.') + 1));

            sb.Append("</a> class.");

            return sb.ToString();
        }

        /// <summary>
        /// Check for missing text in the specified &lt;param&gt; tag and, if
        /// it doesn't exist or the text is blank, add a "missing" message as
        /// the tag's text.
        /// </summary>
        /// <param name="comments">The comments node to check.</param>
        /// <param name="key">The key (name) for the current item</param>
        /// <param name="paramName">The parameter name for which to check.</param>
        private void CheckForMissingParameter(XmlNode comments, string key,
          string paramName)
        {
            string text;
            XmlAttribute name;
            XmlNode tag = comments.SelectSingleNode("param[@name='" +
                paramName + "']");

            if(tag == null)
            {
                tag = comments.OwnerDocument.CreateNode(XmlNodeType.Element,
                    "param", null);

                name = comments.OwnerDocument.CreateAttribute("name");
                name.Value = paramName;
                tag.Attributes.Append(name);

                comments.AppendChild(tag);
                text = String.Empty;
            }
            else
                text = reStripWhitespace.Replace(tag.InnerText, String.Empty);

            if(text.Length == 0)
            {
                this.WriteMessage(MessageLevel.Warn, String.Format(
                    CultureInfo.InvariantCulture,
                    "Missing <param name=\"{0}\"> documentation for {1}",
                    paramName, key));

                tag.InnerXml = String.Format(CultureInfo.InvariantCulture,
                    "<p style=\"color: #dc143c; font-size: 8.5pt; " +
                    "font-weight: bold;\">[Missing &lt;param&gt; " +
                    "documentation for \"{0}\"]</p>",
                    HttpUtility.HtmlEncode(paramName));
            }
        }
        #endregion
    }
}
