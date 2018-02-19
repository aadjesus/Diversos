//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.Transform.cs
// Author  : Eric Woodruff
// Updated : 02/23/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code used to transform and generate the files used
// to define and compile the help file.
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
// 1.3.3.0  11/24/2006  EFW  Added support for component configurations
//=============================================================================

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    partial class BuildProcess
    {
        #region Private data members
        //=====================================================================
        // Private data members

        // Regular expressions used for encoding detection and parsing
        private static Regex reXmlEncoding = new Regex(
            "<\\?xml.*encoding=\"(?<Encoding>.*)\"\\?>",
                RegexOptions.IgnoreCase);

        private static Regex reField = new Regex(@"{@(?<Field>\w*?)}");

        private MatchEvaluator fieldMatchEval;
        #endregion

        /// <summary>
        /// Transform the specified template by inserting the necessary
        /// values into the place holders and saving it to the working folder.
        /// </summary>
        /// <param name="template">The template to transform</param>
        /// <param name="sourceFolder">The folder where the template is
        /// located</param>
        /// <param name="destFolder">The folder in which to save the
        /// transformed file</param>
        /// <returns>The path to the transformed file</returns>
        protected string TransformTemplate(string template,
          string sourceFolder, string destFolder)
        {
            Encoding enc = Encoding.Default;
            string templateText, transformedFile;

            if(template == null)
                throw new ArgumentNullException("template");

            if(sourceFolder == null)
                throw new ArgumentNullException("sourceFolder");

            if(destFolder == null)
                throw new ArgumentNullException("destFolder");

            if(!sourceFolder.EndsWith(@"\"))
                sourceFolder += @"\";

            if(!destFolder.EndsWith(@"\"))
                destFolder += @"\";

            try
            {
                // When reading the file, use the default encoding but
                // detect the encoding if byte order marks are present.
                templateText = BuildProcess.ReadWithEncoding(
                    sourceFolder + template, ref enc);

                // Use a regular expression to find and replace all field
                // tags with a matching value from the project.
                templateText = reField.Replace(templateText, fieldMatchEval);

                transformedFile = destFolder + template;

                // Write the file back out using its original encoding
                using(StreamWriter sw = new StreamWriter(transformedFile,
                    false, enc))
                {
                    sw.Write(templateText);
                }
            }
            catch(Exception ex)
            {
                throw new BuilderException(String.Format(
                    CultureInfo.CurrentCulture,
                    "Unable to transform template '{0}': {1}", template,
                    ex.Message), ex);
            }

            return transformedFile;
        }

        /// <summary>
        /// This is used to read in a template file using an appropriate
        /// encoding method.
        /// </summary>
        /// <param name="filename">The template file to load</param>
        /// <param name="encoding">Pass the default encoding to use.  On
        /// return, it contains the actual encoding for the file.</param>
        /// <returns>The contents of the file.</returns>
        /// <remarks>When reading the file, use the default encoding specified
        /// but detect the encoding if byte order marks are present.  In
        /// addition, if the template is an XML file and it contains an
        /// encoding identifier in the XML tag, the file is read using
        /// that encoding.</remarks>
        public static string ReadWithEncoding(string filename,
          ref Encoding encoding)
        {
            Encoding fileEnc;
            string templateText;

            using(StreamReader sr = new StreamReader(filename, encoding, true))
            {
                templateText = sr.ReadToEnd();

                // Get the actual encoding used
                encoding = sr.CurrentEncoding;
            }

            Match m = reXmlEncoding.Match(templateText);

            // Re-read an XML file using the correct encoding?
            if(m.Success)
            {
                fileEnc = Encoding.GetEncoding(m.Groups["Encoding"].Value);

                if(fileEnc != encoding)
                {
                    encoding = fileEnc;

                    using(StreamReader sr = new StreamReader(filename,
                      encoding, true))
                    {
                        templateText = sr.ReadToEnd();
                    }
                }
            }

            return templateText;
        }

        /// <summary>
        /// Replace a field tag with a value from the project
        /// </summary>
        /// <param name="match">The match that was found</param>
        /// <returns>The string to use as the replacement</returns>
        private string OnFieldMatch(Match match)
        {
            string replaceWith;

            switch(match.Groups["Field"].Value.ToLower(
                CultureInfo.InvariantCulture))
            {
                case "shfbfolder":
                    replaceWith = shfbFolder;
                    break;

                case "projectfolder":
                    replaceWith = Path.GetDirectoryName(project.Filename);
                    if(replaceWith.Length == 0)
                        replaceWith = Directory.GetCurrentDirectory();

                    replaceWith += @"\";
                    break;

                case "outputfolder":
                    replaceWith = outputFolder;
                    break;

                case "workingFolder":
                    replaceWith = workingFolder;
                    break;

                case "sandcastlepath":
                    replaceWith = sandcastleFolder;
                    break;

                case "presentationpath":
                    replaceWith = presentationFolder;
                    break;

                case "presentationstyle":
                    replaceWith = project.PresentationStyle;
                    break;

                case "hhcpath":
                    replaceWith = hhcFolder;
                    break;

                case "hxcomppath":
                    replaceWith = hxcompFolder;
                    break;

                case "dependencies":
                    // If there are any, the dependencies have already been
                    // copied to the .\DLL sub-folder in the working folder.
                    if(project.Dependencies.Count == 0)
                        replaceWith = String.Empty;
                    else
                        replaceWith = @"/dep:DLL\*.*";
                    break;

                case "docinternals":
                    if(project.DocumentInternals || project.DocumentPrivates)
                        replaceWith = "/internal+";
                    else
                        replaceWith = String.Empty;
                    break;

                case "htmlhelpname":
                    replaceWith = project.HtmlHelpName;
                    break;

                case "commentfilelist":
                    replaceWith = project.Assemblies.CommentFileList(
                        workingFolder);
                    break;

                case "helptitle":
                    replaceWith = project.HelpTitle;
                    break;

                case "htmlenchelptitle":
                    replaceWith = HttpUtility.HtmlEncode(project.HelpTitle);
                    break;

                case "urlenchelptitle": // Just replace "&" for now
                    replaceWith = project.HelpTitle.Replace("&", "%26");
                    break;

                case "rootnamespacetitle":
                    replaceWith = project.RootNamespaceTitle;

                    if(replaceWith.Length == 0)
                        replaceWith = "<include item=\"rootTopicTitleLocalized\"/>";
                    break;

                case "binarytoc":
                    replaceWith = project.BinaryTOC ? "Yes" : "No";
                    break;

                case "windowoptions":
                    // Currently, we use a default set of options and only
                    // allow showing or hiding the Favorites tab.
                    replaceWith = (project.IncludeFavorites) ? "0x63520" : "0x62520";
                    break;

                case "langid":
                    replaceWith = language.LCID.ToString(
                        CultureInfo.InvariantCulture);
                    break;

                case "language":
                    replaceWith = String.Format(CultureInfo.InvariantCulture,
                        "0x{0:X} {1}", language.LCID, language.NativeName);
                    break;

                case "locale":
                    replaceWith = language.Name.ToLower(
                        CultureInfo.InvariantCulture);
                    break;

                case "copyright":
                    // Include copyright info if there is a copyright HREF or
                    // copyright text.
                    if(project.CopyrightHref.Length != 0 ||
                      project.CopyrightText.Length != 0)
                        replaceWith = "<include item=\"copyright\"/>";
                    else
                        replaceWith = String.Empty;
                    break;

                case "copyrightinfo":
                    if(project.CopyrightHref.Length == 0 &&
                      project.CopyrightText.Length == 0)
                        replaceWith = String.Empty;
                    else
                        if(project.CopyrightHref.Length == 0)
                            replaceWith = project.DecodedCopyrightText;
                        else
                            if(project.CopyrightText.Length == 0)
                                replaceWith = project.CopyrightHref;
                            else
                                replaceWith = String.Format(
                                    CultureInfo.CurrentCulture, "{0} ({1})",
                                    project.DecodedCopyrightText,
                                    project.CopyrightHref);
                    break;

                case "htmlenccopyrightinfo":
                    if(project.CopyrightHref.Length == 0 &&
                      project.CopyrightText.Length == 0)
                        replaceWith = String.Empty;
                    else
                        if(project.CopyrightHref.Length == 0)
                            replaceWith = "<p/>" + HttpUtility.HtmlEncode(
                                project.DecodedCopyrightText);
                        else
                            if(project.CopyrightText.Length == 0)
                                replaceWith = String.Format(
                                    CultureInfo.CurrentCulture,
                                    "<p/><a href='{0}'>{0}</a>",
                                    HttpUtility.HtmlEncode(
                                        project.CopyrightHref));
                            else
                                replaceWith = String.Format(
                                    CultureInfo.CurrentCulture,
                                    "<p/><a href='{0}'>{1}</a>",
                                    HttpUtility.HtmlEncode(
                                        project.CopyrightHref),
                                    HttpUtility.HtmlEncode(
                                        project.DecodedCopyrightText));
                    break;

                case "copyrighthref":
                    replaceWith = project.CopyrightHref;
                    break;

                case "htmlenccopyrighthref":
                    if(project.CopyrightHref.Length == 0)
                        replaceWith = String.Empty;
                    else
                        replaceWith = String.Format(
                            CultureInfo.CurrentCulture,
                            "<a href='{0}'>{0}</a>", HttpUtility.HtmlEncode(
                            project.CopyrightHref));
                    break;

                case "copyrighttext":
                    if(project.CopyrightText.Length == 0)
                        replaceWith = String.Empty;
                    else
                        replaceWith = project.DecodedCopyrightText;
                    break;

                case "htmlenccopyrighttext":
                    if(project.CopyrightText.Length == 0)
                        replaceWith = String.Empty;
                    else
                        replaceWith = HttpUtility.HtmlEncode(
                            project.DecodedCopyrightText);
                    break;

                case "comments":
                    // Include "send comments" line if feedback e-mail address
                    // is specified.
                    if(project.FeedbackEMailAddress.Length != 0)
                        replaceWith = "<include item=\"comments\"/>";
                    else
                        replaceWith = String.Empty;
                    break;

                case "feedbackemailaddress":
                    replaceWith = project.FeedbackEMailAddress;
                    break;

                case "urlencfeedbackemailaddress":
                    if(project.FeedbackEMailAddress.Length == 0)
                        replaceWith = String.Empty;
                    else
                        replaceWith = HttpUtility.UrlEncode(
                            project.FeedbackEMailAddress);
                    break;

                case "htmlencfeedbackemailaddress":
                    if(project.FeedbackEMailAddress.Length == 0)
                        replaceWith = String.Empty;
                    else
                        replaceWith = HttpUtility.HtmlEncode(
                            project.FeedbackEMailAddress);
                    break;

                case "headertext":
                    replaceWith = project.HeaderText;
                    break;

                case "footertext":
                    replaceWith = project.FooterText;
                    break;

                case "preliminary":
                    // Include the "preliminary" warning in the header text if wanted
                    if(project.Preliminary)
                        replaceWith = "<include item=\"preliminary\"/>";
                    else
                        replaceWith = String.Empty;
                    break;

                case "defaulttopic":
                    replaceWith = defaultTopic;
                    break;

                case "webdefaulttopic":
                    replaceWith = defaultTopic.Replace(@"\", "/");
                    break;

                case "frameworkversion":
                    replaceWith = project.FrameworkVersion;

                    // For .NET 3.0, Microsoft says to use the .NET 2.0
                    // framework files.
                    if(replaceWith[0] == '3')
                        replaceWith = FrameworkVersionTypeConverter.LatestMatching("2.0");
                    break;

                case "frameworkversionshort":
                    replaceWith = project.FrameworkVersion.Substring(0, 3);

                    // For .NET 3.0, Microsoft says to use the .NET 2.0
                    // framework files.
                    if(replaceWith[0] == '3')
                        replaceWith = "2.0";
                    break;

                case "help1xprojectfiles":
                    replaceWith = BuildProcess.HelpProjectFileList(
                        workingFolder + @"Output\", HelpFileFormat.HtmlHelp1x);
                    break;

                case "help2xprojectfiles":
                    replaceWith = BuildProcess.HelpProjectFileList(
                        workingFolder + @"Output\", HelpFileFormat.HtmlHelp2x);
                    break;

                case "projectlinks":
                    replaceWith = project.ProjectLinkType.ToString().ToLower(
                        CultureInfo.InvariantCulture);
                    break;

                case "sdklinks":
                    replaceWith = project.SdkLinkType.ToString().ToLower(
                        CultureInfo.InvariantCulture);
                    break;

                case "htmltoc":
                    replaceWith = this.GenerateHtmlToc();
                    break;

                case "syntaxfilters":
                    replaceWith = String.Empty;

                    if((project.SyntaxFilters & SyntaxFilters.CSharp) != 0)
                        replaceWith += "<generator type=\"Microsoft.Ddue." +
                            "Tools.CSharpDeclarationSyntaxGenerator\" " +
                            "assembly=\"" + sandcastleFolder +
                            "ProductionTools\\SyntaxComponents.dll\" />\r\n";

                    if((project.SyntaxFilters & SyntaxFilters.VisualBasic) != 0)
                        replaceWith += "<generator type=\"Microsoft.Ddue." +
                            "Tools.VisualBasicDeclarationSyntaxGenerator\" " +
                            "assembly=\"" + sandcastleFolder +
                            "ProductionTools\\SyntaxComponents.dll\" />\r\n";

                    if((project.SyntaxFilters & SyntaxFilters.CPlusPlus) != 0)
                        replaceWith += "<generator type=\"Microsoft.Ddue." +
                            "Tools.CPlusPlusDeclarationSyntaxGenerator\" " +
                            "assembly=\"" + sandcastleFolder +
                            "ProductionTools\\SyntaxComponents.dll\" />\r\n";

                    if((project.SyntaxFilters & SyntaxFilters.JSharp) != 0)
                        replaceWith += "<generator type=\"Microsoft.Ddue." +
                            "Tools.JSharpDeclarationSyntaxGenerator\" " +
                            "assembly=\"" + sandcastleFolder +
                            "ProductionTools\\SyntaxComponents.dll\" />\r\n";
                    break;

                case "syntaxfiltersdropdown":
                    replaceWith = String.Empty;

                    // Note that we can't remove the dropdown box if only
                    // a single language is selected as script still
                    // depends on it.
                    if((project.SyntaxFilters & SyntaxFilters.CSharp) != 0)
                        replaceWith += "<language label=\"CSharp\" " +
                            "name=\"CSharp\" style=\"cs\" />\r\n";

                    if((project.SyntaxFilters & SyntaxFilters.VisualBasic) != 0)
                        replaceWith += "<language label=\"VisualBasic\" " +
                            "name=\"VisualBasic\" style=\"vb\" />\r\n";

                    if((project.SyntaxFilters & SyntaxFilters.CPlusPlus) != 0)
                        replaceWith += "<language label=\"ManagedCPlusPlus\" " +
                            "name=\"ManagedCPlusPlus\" style=\"cs\" />\r\n";

                    if((project.SyntaxFilters & SyntaxFilters.JSharp) != 0)
                        replaceWith += "<language label=\"JSharp\" " +
                            "name=\"JSharp\" style=\"cs\" />\r\n";
                    break;

                case "autodocumentconstructors":
                    replaceWith = project.AutoDocumentConstructors.ToString().
                        ToLower(CultureInfo.InvariantCulture);
                    break;

                case "showmissingparams":
                    replaceWith = project.ShowMissingParams.ToString().
                        ToLower(CultureInfo.InvariantCulture);
                    break;

                case "showmissingremarks":
                    replaceWith = project.ShowMissingRemarks.ToString().
                        ToLower(CultureInfo.InvariantCulture);
                    break;

                case "showmissingreturns":
                    replaceWith = project.ShowMissingReturns.ToString().
                        ToLower(CultureInfo.InvariantCulture);
                    break;

                case "showmissingsummaries":
                    replaceWith = project.ShowMissingSummaries.ToString().
                        ToLower(CultureInfo.InvariantCulture);
                    break;

                case "showmissingvalues":
                    replaceWith = project.ShowMissingValues.ToString().
                        ToLower(CultureInfo.InvariantCulture);
                    break;

                default:
                    throw new BuilderException(String.Format(
                        CultureInfo.CurrentCulture,
                        "Unknown field tag: '{0}'",
                        match.Groups["Field"].Value));
            }

            return replaceWith;
        }

        /// <summary>
        /// This is used to merge the component configurations from the
        /// project with the <b>sandcastle.config</b> file.
        /// </summary>
        private void MergeComponentConfigurations()
        {
            XmlNode node, newNode;
            bool wasModified = false;

            if(project.ComponentConfigurations.Count == 0)
                return;

            string configName = workingFolder + "sandcastle.config";

            XmlDocument config = new XmlDocument();
            config.Load(configName);

            foreach(string id in project.ComponentConfigurations.Keys)
            {
                node = config.SelectSingleNode("//component[@id='" +
                    id + "']");

                if(node != null)
                {
                    newNode = config.CreateDocumentFragment();
                    newNode.InnerXml = project.ComponentConfigurations[id];
                    node.InnerXml = reField.Replace(
                        newNode.ChildNodes[0].InnerXml, fieldMatchEval);
                    wasModified = true;
                }
            }

            if(wasModified)
                config.Save(configName);
        }
    }
}
