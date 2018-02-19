//=============================================================================
// System  : Sandcastle Help File Builder
// File    : MainFormImports.cs
// Author  : Eric Woodruff
// Updated : 12/31/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code for the NDoc/Visual Studio import options.
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
// 1.0.0.0  08/12/2006  EFW  Created the code
// 1.1.0.0  08/25/2006  EFW  Added support for Visual Studio projects
// 1.2.0.0  09/06/2006  EFW  Added support for new SHFB properties in the
//                           NDoc import.
// 1.3.1.0  09/26/2006  EFW  Added support for the NDoc ShowMissing* options
// 1.3.1.0  10/03/2006  EFW  Added support for the NDoc Document* options
//=============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;

using SandcastleBuilder.Utils;
using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder
{
    partial class MainForm
    {
        /// <summary>
        /// This is called to import settings from an NDoc project into
        /// a new Sandcastle Help File Builder project.
        /// </summary>
        /// <param name="ndocProject">The NDoc project file from which to
        /// import the settings.</param>
        private void ImportFromNDoc(string ndocProject)
        {
            StreamReader sr = null;
            XmlDocument sourceFile = new XmlDocument();
            XmlNodeList elements;
            XmlNode node;
            DocumentAssembly da;
            ContentItem ci, rootPage = null;
            DependencyItem di;
            NamespaceSummaryItem nsi;
            string folderName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                sr = new StreamReader(ndocProject);
                sourceFile.Load(sr);

                // Add the assemblies
                elements = sourceFile.SelectNodes("//assemblies/*");

                foreach(XmlNode assembly in elements)
                {
                    da = new DocumentAssembly();
                    da.AssemblyPath = new FilePath(
                        assembly.Attributes["location"].Value);
                    da.XmlCommentsPath = new FilePath(
                        assembly.Attributes["documentation"].Value);
                    project.Assemblies.Add(da);
                }

                // Add the namespace summaries
                elements = sourceFile.SelectNodes("//namespaces/*");

                foreach(XmlNode ns in elements)
                {
                    nsi = new NamespaceSummaryItem(ns.Attributes["name"].Value);
                    nsi.Summary = ns.InnerXml;
                    project.NamespaceSummaries.Add(nsi);
                }
                
                // Add one for the global namespace if it isn't there
                if(!project.NamespaceSummaries.Contains(String.Empty))
                    project.NamespaceSummaries.Add(new NamespaceSummaryItem());

                project.NamespaceSummaries.Sort();

                // Add reference paths
                elements = sourceFile.SelectNodes("//referencePaths/*");

                foreach(XmlNode reference in elements)
                {
                    di = new DependencyItem();

                    di.DependencyPath = new FileFolderGacPath(
                        reference.Attributes["path"].Value + "\\*.dll");

                    project.Dependencies.Add(di);
                }

                // Add options from the MSDN documenter
                node = sourceFile.SelectSingleNode("//documenters/documenter" +
                    "[@name=\"MSDN\" or @name=\"MSDN-CHM\"]");

                if(node != null)
                    foreach(XmlNode child in node.ChildNodes)
                        switch(child.Attributes["name"].Value)
                        {
                            case "AdditionalContentResourceDirectory":
                                ci = new ContentItem();
                                ci.SourcePath = new FilePath(
                                    child.Attributes["value"].Value);
                                folderName = Path.GetDirectoryName(ci.SourcePath);
                                ci.DestinationPath = folderName.Substring(
                                    folderName.LastIndexOf('\\') + 1);
                                project.AdditionalContent.Add(ci);
                                break;

                            case "CopyrightHref":
                                project.CopyrightHref =
                                    child.Attributes["value"].Value;
                                break;

                            case "CopyrightText":
                                project.CopyrightText =
                                    child.Attributes["value"].Value;
                                break;

                            case "DocumentAttributes":
                                project.DocumentAttributes = Convert.ToBoolean(
                                    child.Attributes["value"].Value,
                                    CultureInfo.InvariantCulture);
                                break;

                            case "DocumentExplicitInterfaceImplementations":
                                project.DocumentExplicitInterfaceImplementations =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "DocumentInheritedMembers":
                                project.DocumentInheritedMembers =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "DocumentInheritedFrameworkMembers":
                                project.DocumentInheritedFrameworkMembers =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "DocumentInternals":
                                project.DocumentInternals = Convert.ToBoolean(
                                    child.Attributes["value"].Value,
                                    CultureInfo.InvariantCulture);
                                break;

                            case "DocumentPrivates":
                                project.DocumentPrivates = Convert.ToBoolean(
                                    child.Attributes["value"].Value,
                                    CultureInfo.InvariantCulture);
                                break;

                            case "DocumentProtected":
                                project.DocumentProtected = Convert.ToBoolean(
                                    child.Attributes["value"].Value,
                                    CultureInfo.InvariantCulture);
                                break;

                            case "DocumentProtectedInternalAsProtected":
                                project.DocumentProtectedInternalAsProtected =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "DocumentSealedProtected":
                                project.DocumentSealedProtected =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "FeedbackEmailAddress":
                                project.FeedbackEMailAddress =
                                    child.Attributes["value"].Value;
                                break;

                            case "HtmlHelpName":
                                project.HtmlHelpName =
                                    child.Attributes["value"].Value;
                                break;

                            case "OutputDirectory":
                                project.OutputPath = new FolderPath(
                                    child.Attributes["value"].Value);
                                break;

                            case "Title":
                                project.HelpTitle =
                                    child.Attributes["value"].Value;
                                break;

                            case "RootPageContainsNamespaces":
                                project.RootNamespaceContainer =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "RootPageFileName":
                                if(rootPage == null)
                                {
                                    rootPage = new ContentItem();
                                    rootPage.SourcePath = new FilePath(
                                        child.Attributes["value"].Value);
                                    project.AdditionalContent.Insert(0, rootPage);
                                }
                                else
                                    rootPage.SourcePath = new FilePath(
                                        child.Attributes["value"].Value);
                                break;

                            case "RootPageTOCName":
                                if(rootPage == null)
                                {
                                    rootPage = new ContentItem();
                                    rootPage.SourcePath = new FilePath(
                                        @".\Unknown");
                                    project.AdditionalContent.Insert(0, rootPage);
                                }
                                break;

                            case "FilesToInclude":
                                foreach(string filename in child.Attributes[
                                  "value"].Value.Split(new char[] { '|' }))
                                {
                                    ci = new ContentItem();
                                    ci.SourcePath = new FilePath(filename);
                                    project.AdditionalContent.Add(ci);
                                }
                                break;

                            case "AutoDocumentConstructors":
                                project.AutoDocumentConstructors =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "ShowMissingSummaries":
                                project.ShowMissingSummaries =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "ShowMissingRemarks":
                                project.ShowMissingRemarks =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "ShowMissingParams":
                                project.ShowMissingParams =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "ShowMissingReturns":
                                project.ShowMissingReturns =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            case "ShowMissingValues":
                                project.ShowMissingValues =
                                    Convert.ToBoolean(
                                        child.Attributes["value"].Value,
                                        CultureInfo.InvariantCulture);
                                break;

                            default:
                                break;
                        }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                MessageBox.Show(ex.Message, Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sr != null)
                    sr.Close();

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// This is called to import settings from a Visual Studio solution or
        /// project file into a new Sandcastle Help File Builder project.
        /// </summary>
        /// <param name="visualStudioFile">The Visual Studio solution or
        /// project file from which to import the assemblies.</param>
        private void ImportFromVisualStudio(string visualStudioFile)
        {
            List<string> projectFiles = new List<string>();
            List<ProjectConfigItem> projectConfigs = new List<ProjectConfigItem>();
            DocumentAssembly da;
            string solutionContent, folder;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                folder = Path.GetDirectoryName(visualStudioFile) + "\\";

                // Get a list of projects from a solution file?
                if(visualStudioFile.EndsWith(".sln"))
                {
                    using(StreamReader sr = new StreamReader(visualStudioFile))
                    {
                        solutionContent = sr.ReadToEnd();
                        sr.Close();
                    }

                    // Only add projects that are likely to contain assemblies
                    MatchCollection projects = Regex.Matches(solutionContent,
                        "^Project\\(\"\\{(" +
                        "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC|" +   // C#
                        "F184B08F-C81C-45F6-A57F-5ABD9991F28F|" +   // VB.NET
                        "E6FDF86B-F3D1-11D4-8576-0002A516ECE8" +    // J#
                        ")\\}\"\\) = \".*?\", \"(?!http)" +
                        "(?<Path>.*?proj)\", \".*?\"", RegexOptions.Multiline);

                    foreach(Match m in projects)
                        projectFiles.Add(folder + m.Groups["Path"].Value);
                }
                else
                    if(visualStudioFile.EndsWith("proj"))
                        projectFiles.Add(visualStudioFile);

                foreach(string project in projectFiles)
                    this.ImportFromVSProject(project, projectConfigs);

                if(projectConfigs.Count != 0)
                {
                    using(SelectConfigDlg dlg = new SelectConfigDlg(projectConfigs))
                    {
                        if(dlg.ShowDialog() == DialogResult.OK)
                            foreach(ProjectConfigItem cfg in projectConfigs)
                                if(cfg.ConfigurationName == dlg.ConfigurationName)
                                {
                                    da = new DocumentAssembly();
                                    da.AssemblyPath = new FilePath(
                                        cfg.AssemblyFile);
                                    da.XmlCommentsPath = new FilePath(
                                        cfg.XmlCommentsFile);
                                    project.Assemblies.Add(da);
                                }
                    }
                }
                else
                    MessageBox.Show(visualStudioFile + " does not " +
                        "appear to contain any projects that can be imported.",
                        Constants.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                MessageBox.Show(ex.Message, Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Import the assemblies information for the specified Visual
        /// Studio project file.
        /// </summary>
        /// <param name="projectFile">The project file from which to
        /// load the assembly information.</param>
        /// <param name="projectConfigs">The project list</param>
        private void ImportFromVSProject(string projectFile,
            List<ProjectConfigItem> projectConfigs)
        {
            StreamReader sr = null;
            XmlNamespaceManager nsm;
            XmlDocument sourceFile = new XmlDocument();
            XmlNodeList builds;
            XmlNode assemblyNode, outputTypeNode, pathNode, docNode,
                    settingsNode;
            XmlAttribute assemblyAttr, outputTypeAttr, pathAttr, docAttr;
            string assembly, folder;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                sr = new StreamReader(projectFile);
                sourceFile.Load(sr);

                folder = Path.GetDirectoryName(projectFile) + "\\";

                // Visual Studio 2003 or 2005?
                if(sourceFile.ChildNodes[0].Name == "VisualStudioProject")
                {
                    // VS 2003 so use .NET 1.1
                    project.FrameworkVersion =
                        FrameworkVersionTypeConverter.LatestMatching("1.1");

                    settingsNode = sourceFile.SelectSingleNode(
                        "//VisualStudioProject/*/Build/Settings");
                    builds = sourceFile.SelectNodes(
                        "//VisualStudioProject/*/Build/Settings/*");

                    if(settingsNode != null)
                    {
                        assemblyAttr = settingsNode.Attributes["AssemblyName"];
                        outputTypeAttr = settingsNode.Attributes["OutputType"];
                    }
                    else
                        assemblyAttr = outputTypeAttr = null;

                    // Ignore the project if these aren't found
                    if(assemblyAttr == null || outputTypeAttr == null ||
                      builds.Count == 0)
                        return;

                    if(outputTypeAttr.Value == "Library")
                        assembly = assemblyAttr.Value + ".dll";
                    else
                        assembly = assemblyAttr.Value + ".exe";

                    // Get the configuration, assembly, and XML comment info
                    foreach(XmlNode build in builds)
                    {
                        pathAttr = build.Attributes["OutputPath"];
                        docAttr = build.Attributes["DocumentationFile"];

                        if(pathAttr != null && docAttr != null &&
                          docAttr.Value.Length != 0)
                            projectConfigs.Add(new ProjectConfigItem(
                                build.Attributes["Name"].Value,
                                folder + pathAttr.Value + assembly,
                                folder + docAttr.Value));
                    }
                }
                else
                {
                    // VS 2005 so use .NET 2.0
                    project.FrameworkVersion =
                        FrameworkVersionTypeConverter.LatestMatching("2.0");

                    nsm = new XmlNamespaceManager(sourceFile.NameTable);
                    nsm.AddNamespace("prj",
                        sourceFile.DocumentElement.NamespaceURI);

                    assemblyNode = sourceFile.SelectSingleNode(
                        "//prj:Project/prj:PropertyGroup/prj:AssemblyName",
                        nsm);
                    outputTypeNode = sourceFile.SelectSingleNode(
                        "//prj:Project/prj:PropertyGroup/prj:OutputType",
                        nsm);
                    builds = sourceFile.SelectNodes(
                        "//prj:Project/prj:PropertyGroup[@Condition != '']",
                        nsm);

                    // Ignore the project if these aren't found
                    if(assemblyNode == null || outputTypeNode == null ||
                      builds.Count == 0)
                        return;

                    if(outputTypeNode.InnerText == "Library")
                        assembly = assemblyNode.InnerText + ".dll";
                    else
                        assembly = assemblyNode.InnerText + ".exe";

                    // Get the configuration, assembly, and XML comment info
                    foreach(XmlNode build in builds)
                    {
                        pathNode = build.SelectSingleNode("prj:OutputPath",
                            nsm);
                        docNode = build.SelectSingleNode(
                            "prj:DocumentationFile", nsm);

                        if(pathNode != null && docNode != null &&
                          docNode.InnerText.Length != 0)
                            projectConfigs.Add(new ProjectConfigItem(
                                build.Attributes["Condition"].Value,
                                folder + pathNode.InnerText + assembly,
                                folder + docNode.InnerText));
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                MessageBox.Show(ex.Message, Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sr != null)
                    sr.Close();

                this.Cursor = Cursors.Default;
            }
        }
    }
}
