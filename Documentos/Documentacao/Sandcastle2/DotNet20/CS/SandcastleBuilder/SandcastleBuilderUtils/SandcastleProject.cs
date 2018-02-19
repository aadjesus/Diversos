//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : SandcastleProject.cs
// Author  : Eric Woodruff
// Updated : 02/23/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the project class.
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
// 1.0.0.0  08/03/2006  EFW  Created the code
// 1.1.0.0  08/28/2006  EFW  Added various new options for the August CTP
// 1.2.0.0  09/04/2006  EFW  Added new properties to support namespace
//                           summaries and project summary comments.
// 1.3.1.0  09/26/2006  EFW  Added the ShowMissing* properties.
// 1.3.1.0  10/02/2006  EFW  Added support for the September CTP and the
//                           the Document* properties.
// 1.3.2.0  11/03/2006  EFW  Added the NamingMethod property
// 1.3.3.1  11/24/2006  EFW  Added the SyntaxFilters property, support for
//                           build component confurations, and other stuff.
// 1.3.4.0  12/24/2006  EFW  Added WorkingPath project property.  Reworked the
//                           load and save code to use reflection for most
//                           properties to simplify the code and support
//                           setting them via the command line from the
//                           console mode builder.  Converted folder properties
//                           to FolderPath objects.
// 1.4.0.0  02/02/2007  EFW  Converted PresentationStyle to a string with a
//                           type converter listing the presentation folders.
//                           Added FooterText property.
//=============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

using SandcastleBuilder.Utils.Design;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This class represents all of the properties that make up a Sandcastle
    /// Help File Builder project.
    /// </summary>
    public class SandcastleProject
    {
        #region Constants
        //=====================================================================
        // Constants

        /// <summary>The default filename for a new project</summary>
        public const string DefaultName = "Unnamed.shfb";

        // The schema version used in the saved project files
        private static Version SchemaVersion = new Version(1, 4, 0, 0);

        // These are used to decode hex values in the copyright text
        private static Regex reDecode = new Regex(@"\\x[0-9a-f]{2,4}",
            RegexOptions.IgnoreCase);

        private MatchEvaluator characterMatchEvaluator;
        #endregion

        #region Private data members
        //=====================================================================
        // Private data members

        private PropertyInfo[] propertyCache;

        private string filename;    // The name of the project

        private bool isDirty;       // Project dirty state

        // The list of assemblies to use when building the help file
        private DocumentAssemblyCollection assemblies;

        // List of namespace summary items
        private NamespaceSummaryItemCollection namespaceSummaries;

        // List of additional content for the help file
        private ContentItemCollection additionalContent;
        private ContentPlacement contentPlacement;

        // List of dependency items for the documentation assemblies
        private DependencyItemCollection dependencies;

        // Build component configurations
        private ComponentConfigurationDictionary componentConfigs;

        // Path and build-related properties
        private FolderPath hhcPath, hxcompPath, sandcastlePath, workingPath;
        private string outputPath, frameworkVersion;
        private bool cleanIntermediates, purgeDupTopics, cppCommentsFixup;
        private HelpFileFormat helpFileFormat;

        // Help file properties
        private bool binaryTOC, includeFavorites, preliminary, rootNSContainer;
        private string helpTitle, htmlHelpName, copyrightHref, copyrightText,
            feedbackEMailAddress, headerText, footerText, projectSummary,
            rootNSTitle, presentationStyle;
        private CultureInfo language;
        private ProjectLinkType projectLinks;
        private SdkLinkType sdkLinks;
        private NamingMethod namingMethod;
        private SyntaxFilters syntaxFilters;

        // Show Missing Tags options
        private MissingTags missingTags;

        // Visibility options
        private VisibleItems visibleItems;
        #endregion

        #region Non-browsable Properties
        //=====================================================================
        // Non-browsable properties

        /// <summary>
        /// This is used to set or get the filename for the project
        /// </summary>
        /// <remarks>If set to null or an empty string, it defaults to
        /// <b>Unnamed.shfb</b>.</remarks>
        [Browsable(false), XmlIgnore]
        public string Filename
        {
            get { return filename; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    filename = SandcastleProject.DefaultName;
                else
                    filename = value;

                FilePath.BasePath = Path.GetDirectoryName(filename);
            }
        }

        /// <summary>
        /// This is used to set or get the dirty state of the project
        /// </summary>
        /// <remarks>If dirty, the project needs to be saved</remarks>
        /// <event cref="DirtyChanged">This event is raised to let interested
        /// parties know that the project's dirty state has been changed.</event>
        [Browsable(false), XmlIgnore]
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                if(isDirty != value)
                {
                    isDirty = value;
                    OnDirtyChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// This is used to set or get the project summary comments
        /// </summary>
        /// <remarks>These notes will appear in the root namespaces page if
        /// entered.</remarks>
        [Browsable(false)]
        public string ProjectSummary
        {
            get { return projectSummary; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    projectSummary = String.Empty;
                else
                    projectSummary = value;
            }
        }

        /// <summary>
        /// Returns the list of assemblies to document
        /// </summary>
        [Browsable(false)]
        public DocumentAssemblyCollection Assemblies
        {
            get { return assemblies; }
        }

        /// <summary>
        /// Returns the list of namespace summaries
        /// </summary>
        [Browsable(false)]
        public NamespaceSummaryItemCollection NamespaceSummaries
        {
            get { return namespaceSummaries; }
        }

        /// <summary>
        /// This is used to get the copyright notice that appears in the footer
        /// of each page with any hex value place holders replaced with their
        /// actual character.
        /// </summary>
        [Browsable(false)]
        public string DecodedCopyrightText
        {
            get
            {
                return reDecode.Replace(copyrightText,
                    characterMatchEvaluator);
            }
        }

        /// <summary>
        /// This read-only helper property returns the flags to use when
        /// looking for missing tags.
        /// </summary>
        [Browsable(false)]
        public MissingTags MissingTags
        {
            get { return missingTags; }
            private set { missingTags = value; }
        }

        /// <summary>
        /// This read-only helper property returns the flags used to indicate
        /// which optional items to document.
        /// </summary>
        [Browsable(false)]
        public VisibleItems VisibleItems
        {
            get { return visibleItems; }
            private set { visibleItems = value; }
        }
        #endregion

        #region Path-related properties
        //=====================================================================
        // Path-related properties

        /// <summary>
        /// This is used to set or get the path to the HTML 1.x help compiler
        /// (HHC.EXE).
        /// </summary>
        /// <value>You only need to set this if the builder cannot determine
        /// the path for itself.</value>
        [Category("Paths"), Description("The path to the HTML 1.x help " +
          "compiler (HHC.EXE).  This only needs to be set if the builder " +
          "cannot determine the path for itself."),
          DefaultValue(typeof(FolderPath), ""),
          Editor(typeof(FolderPathObjectEditor), typeof(UITypeEditor)),
          FolderDialog("Select the HTML 1.x help compiler installation location",
            Environment.SpecialFolder.ProgramFiles)]
        public FolderPath HtmlHelp1xCompilerPath
        {
            get { return hhcPath; }
            set
            {
                if(value == null)
                    hhcPath = new FolderPath();
                else
                    hhcPath = value;
            }
        }

        /// <summary>
        /// This is used to set or get the path to the HTML 2.x help compiler
        /// (HXCOMP.EXE).
        /// </summary>
        /// <value>You only need to set this if the builder cannot determine
        /// the path for itself.</value>
        [Category("Paths"), Description("The path to the HTML 2.x help " +
          "compiler (HXCOMP.EXE).  This only needs to be set if the builder " +
          "cannot determine the path for itself."),
          DefaultValue(typeof(FolderPath), ""),
          Editor(typeof(FolderPathObjectEditor), typeof(UITypeEditor)),
          FolderDialog("Select the HTML 2.x help compiler installation location",
            Environment.SpecialFolder.ProgramFiles)]
        public FolderPath HtmlHelp2xCompilerPath
        {
            get { return hxcompPath; }
            set
            {
                if(value == null)
                    hxcompPath= new FolderPath();
                else
                    hxcompPath = value;
            }
        }

        /// <summary>
        /// This is used to set or get the path to which the help files
        /// will be generated.
        /// </summary>
        /// <remarks>The default is to create it in a folder called
        /// <b>Help</b> in the same folder as the project file.
        /// <p/><b>Warning:</b> If building a web site, the output folder's
        /// prior content will be erased without warning prior to copying
        /// the new web site content to it!</remarks>
        [Category("Paths"), Description("The path to which the help " +
          "files will be generated.  The default is to save it to " +
              "the .\\Help folder relative to the project file's folder.  " +
              "WARNING: When building a web site, the prior content of " +
              "the output folder will be erased without warning before " +
              "copying the new content to it!"), DefaultValue(@".\Help\"),
          Editor(typeof(FolderPathStringEditor), typeof(UITypeEditor)),
          FolderDialog("Select the output location for the help file")]
        public string OutputPath
        {
            get { return outputPath; }
            set
            {
                if(value == null)
                    outputPath = @".\Help\";
                else
                    if(value.Trim().Length == 0)
                        outputPath = @".\";
                    else
                        outputPath = FolderPath.TerminatePath(value);
            }
        }

        /// <summary>
        /// This is used to set or get the path to the Sandcastle components
        /// </summary>
        /// <value>You only need to set this if the builder cannot determine
        /// the path for itself.</value>
        [Category("Paths"), Description("The path to the Sandcastle " +
          "components.  This only needs to be set if the builder " +
          "cannot determine the path for itself."),
          DefaultValue(typeof(FolderPath), ""),
          Editor(typeof(FolderPathObjectEditor), typeof(UITypeEditor)),
          FolderDialog("Select the Sandcastle installation location",
            Environment.SpecialFolder.ProgramFiles)]
        public FolderPath SandcastlePath
        {
            get { return sandcastlePath; }
            set
            {
                if(value == null)
                    sandcastlePath = new FolderPath();
                else
                    sandcastlePath = value;
            }
        }

        /// <summary>
        /// This is used to set or get the path to the working folder used
        /// during the build process to store the intermediate files.
        /// </summary>
        /// <value>This can be used to perform the build in a different
        /// location with a shorter path if you encounter errors due to long
        /// file path names.  If not specified, it defaults to a folder
        /// called <b>.\Working</b> under the folder specified by the
        /// <see cref="OutputPath"/> property.</value>
        [Category("Paths"), Description("An alternate location to use for " +
          "the intermediate build files.  If not set, it defaults to " +
          ".\\Working under the OutputPath folder."),
          DefaultValue(typeof(FolderPath), ""),
          Editor(typeof(FolderPathObjectEditor), typeof(UITypeEditor)),
          FolderDialog("Select the working files location",
            Environment.SpecialFolder.ProgramFiles)]
        public FolderPath WorkingPath
        {
            get { return workingPath; }
            set
            {
                if(value == null)
                    workingPath = new FolderPath();
                else
                    workingPath = value;
            }
        }
        #endregion

        #region Build-related properties
        //=====================================================================
        // Build-related properties

        /// <summary>
        /// This is used to set or get whether intermediate files are
        /// deleted after a successful build.
        /// </summary>
        /// <value>The default value is true.</value>
        [Category("Build"), Description("If set to true, intermediate files " +
          "are deleted after a successful build"), DefaultValue(true)]
        public bool CleanIntermediates
        {
            get { return cleanIntermediates; }
            set { cleanIntermediates = value; }
        }

        /// <summary>
        /// This is used to set or get the help file format generated by the
        /// build process.
        /// </summary>
        /// <value>The default is to produce an HTML Help 1.x format file
        /// built using HHC.exe.</value>
        /// <remarks>If building a web site, the output folder will be cleared
        /// before the new content is copied to it.</remarks>
        [Category("Build"), Description("Specify the type of help produced " +
          "(HTML 1.x built with HHC.EXE, HTML 2.x built with HXCOMP.EXE, " +
          "and/or a web site.  WARNING: When building a web site, the prior " +
          "content of the output folder will be erased without warning " +
          "before copying the new content to it!"),
          DefaultValue(HelpFileFormat.HtmlHelp1x)]
        public HelpFileFormat HelpFileFormat
        {
            get { return helpFileFormat; }
            set { helpFileFormat = value; }
        }

        /// <summary>
        /// This is used to set or get whether duplicate topics are purged from
        /// the reflection information file generated by <b>MRefBuilder</b>.
        /// </summary>
        /// <value>The default value is true.</value>
        /// <remarks>When generating documentation into one help file for
        /// multiple assemblies that contain one or more common classes
        /// compiled into each of them, duplicate entries occur in the table
        /// of content for the common classes.  Duplicate entries also appear
        /// in the namespace help page for each namespace associated with the
        /// common classes.
        /// <p/>To fix this problem for now, the builder will remove any
        /// duplicate topics it finds in the reflection information file when
        /// this property is set to true.  Set it to false if you think it may
        /// be causing problems.  Otherwise, leave it set to true.</remarks>
        [Category("Build"), Description("Leave this set to true to work " +
            "around a bug in MRefBuilder that causes duplicate topics " +
            "when building help for multiple assemblies that share " +
            "common code"), DefaultValue(true)]
        public bool PurgeDuplicateTopics
        {
            get { return purgeDupTopics; }
            set { purgeDupTopics = value; }
        }

        /// <summary>
        /// This is used to set or get whether to fix-up the XML comments files
        /// to work around an issue with those generated by the C++ compiler.
        /// </summary>
        /// <value>The default value is false.</value>
        /// <remarks>The C++ compiler generates method signatures that differ
        /// from the other .NET compilers for methods that take generics as
        /// parameters.  These methods fail to get documented as they do not
        /// match the output of <b>MRefBuilder</b>.  The C# and VB.NET
        /// compilers generate names that do match it and this option is not
        /// needed for comments files generated by them.  Set this to true if
        /// the project contains C++ compiler generated XML comments files and
        /// your project contains methods that take generic types for
        /// parameters.</remarks>
        [Category("Build"), Description("Set this to true to work around a " +
          "C++ compiler generated XML comments file issue."),
          DefaultValue(false)]
        public bool CppCommentsFixup
        {
            get { return cppCommentsFixup; }
            set { cppCommentsFixup = value; }
        }

        /// <summary>
        /// This is used to get a collection of dependencies (individual files
        /// or wildcards) for MRefBuilder if needed.
        /// </summary>
        /// <remarks>If you have several dependencies, you may want to copy them
        /// to a folder and specify the reference to it with a wildcard to get
        /// all of the files in the folder (i.e. .\Deps\*.dll).</remarks>
        [Category("Build"), Description("A collection of dependencies (" +
          "individual files or wildcards) for MRefBuilder if needed")]
        public DependencyItemCollection Dependencies
        {
            get { return dependencies; }
        }

        /// <summary>
        /// This is used to get a dictionary of build component configurations.
        /// </summary>
        /// <remarks>This allows you to configure the settings for third
        /// party build components if they support it.</remarks>
        [Category("Build"), Description("Configuration options for third " +
          "party build components such as the Code Block Colorizer")]
        public ComponentConfigurationDictionary ComponentConfigurations
        {
            get { return componentConfigs; }
        }

        /// <summary>
        /// This is used to set or get the .NET Framework version to which the
        /// documentation links for system types should point.
        /// </summary>
        /// <remarks>If not found, it will default to the most recent version
        /// of the framework installed.</remarks>
        [Category("Build"), Description("The .NET Framework version to " +
          "which the documentation links for system types should point."),
          TypeConverter(typeof(FrameworkVersionTypeConverter))]
        public string FrameworkVersion
        {
            get { return frameworkVersion; }
            set
            {
                if(value == null || !FrameworkVersionTypeConverter.IsPresent(value))
                    frameworkVersion = FrameworkVersionTypeConverter.LatestMatching("2.0");
                else
                    frameworkVersion = value;
            }
        }
        #endregion

        #region Help file properties
        //=====================================================================
        // Build-related properties

        /// <summary>
        /// This is used to set or get the path to the Sandcastle components
        /// </summary>
        /// <remarks>This can significantly reduce the amount of time
        /// required to load a very large help document.</remarks>
        [Category("Help File"), Description("Create a binary table of " +
            "contents file.  This can significantly reduce the amount " +
            "of time required to load a very large help document."),
          DefaultValue(true)]
        public bool BinaryTOC
        {
            get { return binaryTOC; }
            set { binaryTOC = value; }
        }

        /// <summary>
        /// This is used to set or get whether or not a Favorites tab will
        /// appear in the help file.
        /// </summary>
        [Category("Help File"), Description("Set to true to include a " +
            "Favorites tab in the compiled help file"), DefaultValue(false)]
        public bool IncludeFavorites
        {
            get { return includeFavorites; }
            set { includeFavorites = value; }
        }

        /// <summary>
        /// This is used to set or get whether or not all pages should be
        /// marked with a "preliminary documentation" warning in the page
        /// header.
        /// </summary>
        [Category("Help File"), Description("If true, all pages will contain " +
            "a 'preliminary documentation' warning in the page header"),
            DefaultValue(false)]
        public bool Preliminary
        {
            get { return preliminary; }
            set { preliminary = value; }
        }

        /// <summary>
        /// This is used to set or get whether or not a root namespace entry
        /// is added to the table of content to act as a container for the
        /// namespaces from the documented assemblies.
        /// </summary>
        /// <value>If true, a root <b>Namespaces</b> table of content entry
        /// will be created as the container of the namespaces in the
        /// documented assemblies.  If false, the default, the namespaces are
        /// listed in the table of content as root entries.</value>
        [Category("Help File"), Description("If true, a root \"Namespaces\" " +
          "table of content entry will be created as the container of the " +
          "namespaces in the documented assemblies.  If false, the default, " +
          "the namespaces are listed in the table of content as root entries."),
          DefaultValue(false)]
        public bool RootNamespaceContainer
        {
            get { return rootNSContainer; }
            set { rootNSContainer = value; }
        }

        /// <summary>
        /// This is used to set or get an alternate title for the root
        /// namespaces page and the root table of content container that
        /// appears when <see cref="RootNamespaceContainer"/> is set to true.
        /// </summary>
        /// <value>If left blank (the default), the localized version of the
        /// text "Namespaces" will be used.</value>
        [Category("Help File"), Description("An alternate title for the root " +
          "namespaces page and the root table of content container."),
          DefaultValue("")]
        public string RootNamespaceTitle
        {
            get { return rootNSTitle; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    rootNSTitle = String.Empty;
                else
                    rootNSTitle = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get the help file's title
        /// </summary>
        [Category("Help File"), Description("The title for the help file"),
          DefaultValue("A Sandcastle Documented Class Library")]
        public string HelpTitle
        {
            get { return helpTitle; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    helpTitle = "A Sandcastle Documented Class Library";
                else
                    helpTitle = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get the name of the compiled help file.
        /// Do not include a path or the extension.
        /// </summary>
        [Category("Help File"), Description("The name of the compiled help " +
          "file.  Do not include a path or the extension."),
          DefaultValue("Documentation")]
        public string HtmlHelpName
        {
            get { return htmlHelpName; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    htmlHelpName = "Documentation";
                else
                    htmlHelpName = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get the language option for the help file
        /// and to determine which set of presentation resource files to use.
        /// </summary>
        /// <value>If a matching set of presentation resources cannot be
        /// found for the specified language, the US English set will be
        /// used.</value>
        [Category("Help File"), Description("The language for the help file"),
            DefaultValue(typeof(CultureInfo), "en-US"),
            TypeConverter(typeof(LanguageResourceConverter))]
        public CultureInfo Language
        {
            get { return language; }
            set
            {
                if(value == null || value == CultureInfo.InvariantCulture)
                    language = new CultureInfo("en-US");
                else
                    language = value;
            }
        }

        /// <summary>
        /// This is used to set or get the URL to use as the link for the
        /// copyright notice.
        /// </summary>
        /// <value>If not set, the see cref="CopyrighText"/> (if any) is not
        /// turned into a clickable link.</value>
        [Category("Help File"), Description("The URL reference for the copyright notice"),
            DefaultValue("")]
        public string CopyrightHref
        {
            get { return copyrightHref; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    copyrightHref = String.Empty;
                else
                    copyrightHref = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get the copyright notice that appears in
        /// the footer of each page.
        /// </summary>
        /// <remarks>If not set, no copyright note will appear.  If a
        /// <see cref="CopyrightHref" /> is specified without copyright text,
        /// the URL appears instead.</remarks>
        [Category("Help File"), Description("The copyright notice for the page footer"),
            DefaultValue("")]
        public string CopyrightText
        {
            get { return copyrightText; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    copyrightText = String.Empty;
                else
                    copyrightText = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get the feedback e-mail address that
        /// appears in the footer of each page.
        /// </summary>
        /// <remarks>If not set, no feedback link will appear.</remarks>
        [Category("Help File"), Description("The feedback e-mail address that " +
          "will appear in the footer of each page"), DefaultValue("")]
        public string FeedbackEMailAddress
        {
            get { return feedbackEMailAddress; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    feedbackEMailAddress = String.Empty;
                else
                    feedbackEMailAddress = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get additional text that should appear
        /// in the header of every page.
        /// </summary>
        [Category("Help File"), Description("Additional text for the header " +
          "in every page"), DefaultValue("")]
        public string HeaderText
        {
            get { return headerText; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    headerText = String.Empty;
                else
                    headerText = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get additional text that should appear
        /// in the footer of every page.
        /// </summary>
        [Category("Help File"), Description("Additional text for the footer " +
          "in every page"), DefaultValue("")]
        public string FooterText
        {
            get { return footerText; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    footerText = String.Empty;
                else
                    footerText = value.Trim();
            }
        }

        /// <summary>
        /// This is used to set or get the type of links used to reference
        /// other help topics referring to items within the documented
        /// assemblies.
        /// </summary>
        /// <value>The default is to produce local links.</value>
        [Category("Help File"), Description("Specify which type of links to " +
          "create when referencing other help topics related to items " +
          "within the documented assemblies.  None = No links, Local = " +
          "Links using anchor tags, Index = HTML 2.x style MS-Help links."),
          DefaultValue(ProjectLinkType.Local)]
        public ProjectLinkType ProjectLinkType
        {
            get { return projectLinks; }
            set { projectLinks = value; }
        }

        /// <summary>
        /// This is used to set or get the type of links used to reference
        /// other help topics referring to framework (SDK) help topics.
        /// </summary>
        /// <value>The default is to produce MSDN links.</value>
        [Category("Help File"), Description("Specify which type of links to " +
          "create when referencing other help topics related to framework " +
          "(SDK) topics.  None = No links, Index = HTML 2.x style MS-Help " +
          "links, MSDN = Online links to MSDN help topics."),
          DefaultValue(SdkLinkType.Msdn)]
        public SdkLinkType SdkLinkType
        {
            get { return sdkLinks; }
            set { sdkLinks = value; }
        }

        /// <summary>
        /// This is used to set or get the presentation style for the help
        /// topic pages.
        /// </summary>
        /// <value>The default is to use the Prototype style.</value>
        [Category("Help File"), Description("Select which presentation " +
          "style to use for the generated help topic pages"),
          TypeConverter(typeof(PresentationStyleTypeConverter))]
        public string PresentationStyle
        {
            get { return presentationStyle; }
            set
            {
                if(value == null || !PresentationStyleTypeConverter.IsPresent(value))
                    presentationStyle = PresentationStyleTypeConverter.FirstMatching(value);
                else
                    presentationStyle = value;
            }
        }

        /// <summary>
        /// This is used to set or get the naming method used to generate the
        /// help topic filenames.
        /// </summary>
        /// <value>The default is to use GUID values as the filenames.</value>
        [Category("Help File"), Description("Specify the naming method to " +
          "use for the help topic filenames"), DefaultValue(NamingMethod.Guid)]
        public NamingMethod NamingMethod
        {
            get { return namingMethod; }
            set { namingMethod = value; }
        }

        /// <summary>
        /// This is used to set or get the language filters which determines
        /// which languages appear in the <b>Syntax</b> section of the help
        /// topics.
        /// </summary>
        /// <value>The default is <b>Standard</b> (C#, VB.NET, and C++).</value>
        [Category("Help File"), Description("Select which languages will " +
          "appear in the Syntax section of each help topic.  Select a value " +
          "from the dropdown or enter a comma-separated list of values"),
          DefaultValue(SyntaxFilters.Standard)]
        public SyntaxFilters SyntaxFilters
        {
            get { return syntaxFilters; }
            set { syntaxFilters = value; }
        }
        #endregion

        #region Additional content properties
        //=====================================================================
        // Additional content properties

        /// <summary>
        /// This is used to get the additional content collection.
        /// </summary>
        [Category("Additional Content"), Description("Additional content " +
          "that should appear in the help file")]
        public ContentItemCollection AdditionalContent
        {
            get { return additionalContent; }
        }

        /// <summary>
        /// This is used to set or get the placement of any additional
        /// content items in the table of content.
        /// </summary>
        /// <value>The default is to place additional content items above
        /// the namespaces.</value>
        [Category("Additional Content"), Description("Specify whether the " +
          "additional content items appear above or below the namespaces in " +
          "the table of content."),
          DefaultValue(ContentPlacement.AboveNamespaces)]
        public ContentPlacement ContentPlacement
        {
            get { return contentPlacement; }
            set { contentPlacement = value; }
        }
        #endregion

        #region Show Missing Tags properties
        //=====================================================================
        // Show Missing Tags properties

        /// <summary>
        /// This is used to get or set whether or not missing &lt;summary&gt;
        /// tags are indicated in the help file.
        /// </summary>
        /// <value>Set this to true to add a message to the help topic
        /// to indicate that the &lt;summary&gt; tag is missing.  A message
        /// is also written to the log file.</value>
        [Category("Show Missing Tags"), Description("If set to true, a " +
          "message is added to the help topic if the <summary> tag is " +
          "missing.  If set to false, it is not."), DefaultValue(true),
          XmlIgnore]
        public bool ShowMissingSummaries
        {
            get { return ((missingTags & MissingTags.Summary) != 0); }
            set
            {
                if(value)
                    missingTags |= MissingTags.Summary;
                else
                    missingTags &= ~MissingTags.Summary;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not missing &lt;param&gt;
        /// tags are indicated in the help file.
        /// </summary>
        /// <value>Set this to true to add a message to the help topic
        /// to indicate that a &lt;param&gt; tag is missing.  A message
        /// is also written to the log file.</value>
        [Category("Show Missing Tags"), Description("If set to true, a " +
          "message is added to the help topic if a <param> tag is " +
          "missing.  If set to false, it is not."), DefaultValue(true),
          XmlIgnore]
        public bool ShowMissingParams
        {
            get { return ((missingTags & MissingTags.Parameter) != 0); }
            set
            {
                if(value)
                    missingTags |= MissingTags.Parameter;
                else
                    missingTags &= ~MissingTags.Parameter;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not missing &lt;returns&gt;
        /// tags are indicated in the help file.
        /// </summary>
        /// <value>Set this to true to add a message to the help topic
        /// to indicate that the &lt;returns&gt; tag is missing.  A message
        /// is also written to the log file.</value>
        [Category("Show Missing Tags"), Description("If set to true, a " +
          "message is added to the help topic if the <returns> tag is " +
          "missing.  If set to false, it is not."), DefaultValue(true),
          XmlIgnore]
        public bool ShowMissingReturns
        {
            get { return ((missingTags & MissingTags.Returns) != 0); }
            set
            {
                if(value)
                    missingTags |= MissingTags.Returns;
                else
                    missingTags &= ~MissingTags.Returns;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not missing &lt;value&gt;
        /// tags are indicated in the help file.
        /// </summary>
        /// <value>Set this to true to add a message to the help topic
        /// to indicate that the &lt;value&gt; tag is missing.  A message
        /// is also written to the log file.</value>
        [Category("Show Missing Tags"), Description("If set to true, a " +
          "message is added to the help topic if the <value> tag is " +
          "missing.  If set to false, it is not."), DefaultValue(false),
          XmlIgnore]
        public bool ShowMissingValues
        {
            get { return ((missingTags & MissingTags.Value) != 0); }
            set
            {
                if(value)
                    missingTags |= MissingTags.Value;
                else
                    missingTags &= ~MissingTags.Value;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not missing &lt;remarks&gt;
        /// tags are indicated in the help file.
        /// </summary>
        /// <value>Set this to true to add a message to the help topic
        /// to indicate that the &lt;remarks&gt; tag is missing.  A message
        /// is also written to the log file.</value>
        [Category("Show Missing Tags"), Description("If set to true, a " +
          "message is added to the help topic if the <remarks> tag is " +
          "missing.  If set to false, it is not."), DefaultValue(false),
          XmlIgnore]
        public bool ShowMissingRemarks
        {
            get { return ((missingTags & MissingTags.Remarks) != 0); }
            set
            {
                if(value)
                    missingTags |= MissingTags.Remarks;
                else
                    missingTags &= ~MissingTags.Remarks;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not constructors are
        /// automatically documented if they are missing the &lt;summary&gt;
        /// tag and for classes with compiler generated constructors.
        /// </summary>
        /// <value>Set this to true to automatically add default text for the
        /// &lt;summary&gt; tag on constructors that are missing it and for
        /// classes with a compiler generated constructor.  If set to false
        /// and <see cref="ShowMissingSummaries"/> is true, a "missing summary"
        /// warning will appear instead.  A message is also written to the log
        /// file.</value>
        [Category("Show Missing Tags"), Description("If set to true, a " +
          "default message is added to constructors that are missing their " +
          "<summary> tag.  If set to false, it is not."), DefaultValue(true),
          XmlIgnore]
        public bool AutoDocumentConstructors
        {
            get { return ((missingTags & MissingTags.AutoDocumentCtors) != 0); }
            set
            {
                if(value)
                    missingTags |= MissingTags.AutoDocumentCtors;
                else
                    missingTags &= ~MissingTags.AutoDocumentCtors;
            }
        }
        #endregion

        #region Visibility properties
        //=====================================================================
        // Visibility properties

        /// <summary>
        /// This is used to get or set whether or not attributes on types and
        /// members are documented in the syntax portion of the help file.
        /// </summary>
        /// <value>Set to true to document attributes or false to hide them</value>
        [Category("Visibility"), Description("If set to true, attributes on " +
          "types and members are documented in the syntax portion of the " +
          "help pages.  If set to false, they are not."), DefaultValue(false),
          XmlIgnore]
        public bool DocumentAttributes
        {
            get { return ((visibleItems & VisibleItems.Attributes) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.Attributes;
                else
                    visibleItems &= ~VisibleItems.Attributes;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not explicit interface
        /// implementations are documented.
        /// </summary>
        /// <value>Set to true to document explicit interface implementations
        /// or false to hide them.</value>
        [Category("Visibility"), Description("If set to true, explicit " +
          "interface implementations are documented.  If set to false, " +
          "they are not."), DefaultValue(false), XmlIgnore]
        public bool DocumentExplicitInterfaceImplementations
        {
            get { return ((visibleItems & VisibleItems.ExplicitInterfaceImplementations) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.ExplicitInterfaceImplementations;
                else
                    visibleItems &= ~VisibleItems.ExplicitInterfaceImplementations;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not inherited members
        /// are documented.
        /// </summary>
        /// <value>Set to true to document inherited members or false to
        /// hide them.</value>
        [Category("Visibility"), Description("If set to true, inherited " +
          "members are documented.  If set to false, they are not."),
          DefaultValue(true), RefreshProperties(RefreshProperties.All),
          XmlIgnore]
        public bool DocumentInheritedMembers
        {
            get { return ((visibleItems & VisibleItems.InheritedMembers) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.InheritedMembers;
                else
                    visibleItems &= ~(VisibleItems.InheritedMembers |
                        VisibleItems.InheritedFrameworkMembers);
            }
        }

        /// <summary>
        /// This is used to get or set whether or not inherited framework
        /// members are documented.
        /// </summary>
        /// <value>Set to true to document inherited framework members or
        /// false to hide them.  For this to work,
        /// <see cref="DocumentInheritedMembers"/> must also be enabled.</value>
        [Category("Visibility"), Description("If set to true, inherited " +
          "framework members are documented.  If set to false, they are " +
          "not.  NOTE: To work, DocumentInheritedMembers must also be set " +
          "to True."), DefaultValue(true), XmlIgnore,
          RefreshProperties(RefreshProperties.All)]
        public bool DocumentInheritedFrameworkMembers
        {
            get { return ((visibleItems & VisibleItems.InheritedFrameworkMembers) != 0); }
            set
            {
                if(value)
                    visibleItems |= (VisibleItems.InheritedMembers |
                        VisibleItems.InheritedFrameworkMembers);
                else
                    visibleItems &= ~VisibleItems.InheritedFrameworkMembers;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not internal members are
        /// documented in the help file.
        /// </summary>
        /// <value>Set to true to document internal members or false to hide
        /// them</value>
        [Category("Visibility"), Description("If set to true, internal " +
          "members are documented in the help file.  If set to false, " +
          "they are not."), DefaultValue(false), XmlIgnore]
        public bool DocumentInternals
        {
            get { return ((visibleItems & VisibleItems.Internals) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.Internals;
                else
                    visibleItems &= ~VisibleItems.Internals;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not private members are
        /// documented in the help file.
        /// </summary>
        /// <value>Set to true to document private members or false to hide
        /// them</value>
        [Category("Visibility"), Description("If set to true, private " +
          "members are documented in the help file.  If set to false, " +
          "they are not."), DefaultValue(false), XmlIgnore,
          RefreshProperties(RefreshProperties.All)]
        public bool DocumentPrivates
        {
            get { return ((visibleItems & VisibleItems.Privates) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.Privates;
                else
                    visibleItems &= ~(VisibleItems.Privates |
                        VisibleItems.PrivateFields);
            }
        }

        /// <summary>
        /// This is used to get or set whether or not private fields are
        /// documented in the help file.
        /// </summary>
        /// <value>Set to true to document private fields or false to hide
        /// them.  For this to work, <see cref="DocumentPrivates"/> must
        /// also be enabled.</value>
        /// <remarks>Private fields are most often used to back properties
        /// and do not have documentation.  With this set to false, they
        /// are omitted from the help file to reduce unnecessary clutter.</remarks>
        [Category("Visibility"), Description("If set to true, private " +
          "fields are documented in the help file.  If set to false, " +
          "they are not.  NOTE: To work, DocumentPrivates must also be set " +
          "to True."), DefaultValue(false), XmlIgnore]
        public bool DocumentPrivateFields
        {
            get { return ((visibleItems & VisibleItems.PrivateFields) != 0); }
            set
            {
                if(value)
                    visibleItems |= (VisibleItems.Privates |
                        VisibleItems.PrivateFields);
                else
                    visibleItems &= ~VisibleItems.PrivateFields;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not protected members are
        /// documented in the help file.
        /// </summary>
        /// <value>Set to true to document protected members or false to hide
        /// them</value>
        [Category("Visibility"), Description("If set to true, protected " +
          "members are documented in the help file.  If set to false, " +
          "they are not."), DefaultValue(true), XmlIgnore,
          RefreshProperties(RefreshProperties.All)]
        public bool DocumentProtected
        {
            get { return ((visibleItems & VisibleItems.Protected) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.Protected;

                else
                    visibleItems &= ~(VisibleItems.Protected |
                        VisibleItems.SealedProtected);
            }
        }

        /// <summary>
        /// This is used to get or set whether or not "protected internal"
        /// members are documented as "protected" only in the help file.
        /// </summary>
        /// <value>Set to true to document "protected internal" members
        /// as "protected" only or false to document them normally.  This
        /// option is ignored if <see cref="DocumentProtected"/> is false.</value>
        [Category("Visibility"), Description("If set to true, \"protected " +
          "internal\" members are documented as \"protected\" only.  If " +
          "set to false, they documented normally.  NOTE: This option is " +
          "ignored if DocumentProtected is false"), DefaultValue(false),
          XmlIgnore]
        public bool DocumentProtectedInternalAsProtected
        {
            get { return ((visibleItems & VisibleItems.ProtectedInternalAsProtected) != 0); }
            set
            {
                if(value)
                    visibleItems |= VisibleItems.ProtectedInternalAsProtected;
                else
                    visibleItems &= ~VisibleItems.ProtectedInternalAsProtected;
            }
        }

        /// <summary>
        /// This is used to get or set whether or not protected members of
        /// sealed classes are documented in the help file.
        /// </summary>
        /// <value>Set to true to document protected members of sealed classes
        /// or false to hide them. For this to work,
        /// <see cref="DocumentProtected"/> must also be enabled.</value>
        [Category("Visibility"), Description("If set to true, protected " +
          "members of sealed classes are documented in the help file.  If " +
          "set to false, they are not.  NOTE: To work, DocumentProtected " +
          "must also be enabled."), RefreshProperties(RefreshProperties.All),
          DefaultValue(true), XmlIgnore]
        public bool DocumentSealedProtected
        {
            get { return ((visibleItems & VisibleItems.SealedProtected) != 0); }
            set
            {
                if(value)
                    visibleItems |= (VisibleItems.SealedProtected |
                        VisibleItems.Protected);
                else
                    visibleItems &= ~VisibleItems.SealedProtected;
            }
        }
        #endregion

        #region Events
        //=====================================================================
        // Events

        /// <summary>
        /// This event is raised when the dirty property changes
        /// </summary>
        public event EventHandler DirtyChanged;

        /// <summary>
        /// This raises the <see cref="DirtyChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected void OnDirtyChanged(EventArgs e)
        {
            if(DirtyChanged != null)
                DirtyChanged(this, e);
        }

        /// <summary>
        /// This event is raised when the assembly list is modified
        /// </summary>
        public event EventHandler AssembliesChanged;

        /// <summary>
        /// This raises the <see cref="AssembliesChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected void OnAssembliesChanged(EventArgs e)
        {
            if(AssembliesChanged != null)
                AssembliesChanged(this, e);
        }
        #endregion

        #region Designer methods
        //=====================================================================
        // Designer methods

        /// <summary>
        /// This is used to see if the <see cref="FrameworkVersion"/> property
        /// should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.</returns>
        private bool ShouldSerializeFrameworkVersion()
        {
            return (this.FrameworkVersion !=
                FrameworkVersionTypeConverter.LatestMatching("2.0"));
        }

        /// <summary>
        /// This is used to reset the <see cref="FrameworkVersion"/> property
        /// to its default value.
        /// </summary>
        private void ResetFrameworkVersion()
        {
            this.FrameworkVersion = FrameworkVersionTypeConverter.LatestMatching("2.0");
        }

        /// <summary>
        /// This is used to see if the <see cref="PresentationStyle"/> property
        /// should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.</returns>
        private bool ShouldSerializePresentationStyle()
        {
            return (this.PresentationStyle !=
                PresentationStyleTypeConverter.DefaultStyle);
        }

        /// <summary>
        /// This is used to reset the <see cref="FrameworkVersion"/> property
        /// to its default value.
        /// </summary>
        private void ResetPresentationStyle()
        {
            this.PresentationStyle = PresentationStyleTypeConverter.DefaultStyle;
        }

        /// <summary>
        /// This is used to see if the <see cref="AdditionalContent"/> property
        /// should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.</returns>
        /// <remarks>We do not allow resetting this property as it is a
        /// collection and we don't want to lose all items accidentally.</remarks>
        private bool ShouldSerializeAdditionalContent()
        {
            return (this.AdditionalContent.Count != 0);
        }

        /// <summary>
        /// This is used to see if the <see cref="Dependencies"/> property
        /// should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.</returns>
        /// <remarks>We do not allow resetting this property as it is a
        /// collection and we don't want to lose all items accidentally.</remarks>
        private bool ShouldSerializeDependencies()
        {
            return (this.Dependencies.Count != 0);
        }

        /// <summary>
        /// This is used to see if the <see cref="ComponentConfigurations"/>
        /// property should be serialized.
        /// </summary>
        /// <returns>True to serialize it, false if it matches the default
        /// and should not be serialized.</returns>
        /// <remarks>We do not allow resetting this property as it is a
        /// dictionary and we don't want to lose all items accidentally.</remarks>
        private bool ShouldSerializeComponentConfigurations()
        {
            return (this.ComponentConfigurations.Count != 0);
        }
        #endregion

        #region Private class methods
        //=====================================================================
        // Private class methods

        /// <summary>
        /// This is handled to mark the project as dirty when the list of
        /// assemblies changes.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void assemblies_ListChanged(object sender,
          ListChangedEventArgs e)
        {
            this.IsDirty = true;
            OnAssembliesChanged(EventArgs.Empty);
        }

        /// <summary>
        /// This is handled to mark the project as dirty when the list of
        /// additional content or dependencies changes.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void ItemList_ListChanged(object sender,
          ListChangedEventArgs e)
        {
            this.IsDirty = true;
        }

        /// <summary>
        /// Replace a \xNN value in the copyright text with its actual
        /// character.
        /// </summary>
        /// <param name="match">The match that was found</param>
        /// <returns>The string to use as the replacement</returns>
        private string OnCharacterMatch(Match match)
        {
            // Ignore it if it is escaped
            if(match.Index != 0 && copyrightText[match.Index - 1] == '\\')
                return match.Value;

            int value = Convert.ToInt32(match.Value.Substring(2), 16);
            char ch = (char)value;

            return new String(ch, 1);
        }

        /// <summary>
        /// This is called to write a property to the project file if it
        /// is persistable.
        /// </summary>
        /// <param name="xw">The XML text writer to which it is written</param>
        /// <param name="property">The property to persist</param>
        private void WriteProperty(XmlTextWriter xw, PropertyInfo property)
        {
            if(!property.CanWrite || property.IsDefined(
              typeof(XmlIgnoreAttribute), true))
                return;

            // Write out the persistable path and fixed path properties for
            // file and folder path objects.
            if(property.PropertyType == typeof(FolderPath))
            {
                FolderPath filePath = (FolderPath)property.GetValue(this, null);

                xw.WriteStartElement(property.Name);
                xw.WriteAttributeString("path", filePath.PersistablePath);
                xw.WriteEndElement();
            }
            else
                if(property.PropertyType == typeof(FilePath))
                {
                    FilePath filePath = (FilePath)property.GetValue(this, null);

                    xw.WriteStartElement(property.Name);
                    xw.WriteAttributeString("path", filePath.PersistablePath);
                    xw.WriteEndElement();
                }
                else
                    xw.WriteElementString(property.Name,
                        property.GetValue(this, null).ToString());
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        public SandcastleProject()
        {
            characterMatchEvaluator = new MatchEvaluator(
                this.OnCharacterMatch);

            assemblies = new DocumentAssemblyCollection();
            assemblies.ListChanged += new ListChangedEventHandler(
                assemblies_ListChanged);

            namespaceSummaries = new NamespaceSummaryItemCollection();
            namespaceSummaries.ListChanged += new ListChangedEventHandler(
                ItemList_ListChanged);

            additionalContent = new ContentItemCollection();
            additionalContent.ListChanged += new ListChangedEventHandler(
                ItemList_ListChanged);

            dependencies = new DependencyItemCollection();
            dependencies.ListChanged += new ListChangedEventHandler(
                ItemList_ListChanged);

            componentConfigs = new ComponentConfigurationDictionary();
            componentConfigs.DictionaryChanged += new ListChangedEventHandler(
                ItemList_ListChanged);

            this.ResetProject();
        }

        /// <summary>
        /// This is used to reset the project to its default state
        /// </summary>
        public void ResetProject()
        {
            assemblies.Clear();
            namespaceSummaries.Clear();
            additionalContent.Clear();
            dependencies.Clear();
            componentConfigs.Clear();

            contentPlacement = ContentPlacement.AboveNamespaces;
            cleanIntermediates = purgeDupTopics = binaryTOC = true;

            includeFavorites = preliminary = rootNSContainer =
                cppCommentsFixup = false;

            missingTags = MissingTags.Summary | MissingTags.Parameter |
                MissingTags.Returns | MissingTags.AutoDocumentCtors;

            visibleItems = VisibleItems.InheritedFrameworkMembers |
                VisibleItems.InheritedMembers | VisibleItems.Protected |
                VisibleItems.SealedProtected;

            helpFileFormat = HelpFileFormat.HtmlHelp1x;
            projectLinks = ProjectLinkType.Local;
            sdkLinks = SdkLinkType.Msdn;
            presentationStyle = PresentationStyleTypeConverter.DefaultStyle;
            namingMethod = NamingMethod.Guid;
            syntaxFilters = SyntaxFilters.Standard;

            // Reset the filename before the paths so that they end up with
            // an appropriate base path.
            this.Filename = null;
            this.OutputPath = null;
            this.HtmlHelp1xCompilerPath = this.HtmlHelp2xCompilerPath =
                this.SandcastlePath = this.WorkingPath = null;

            this.HelpTitle = this.HtmlHelpName = this.CopyrightHref =
                this.CopyrightText = this.FeedbackEMailAddress =
                this.HeaderText = this.FooterText = this.ProjectSummary =
                this.RootNamespaceTitle = null;

            language = new CultureInfo("en-US");
            frameworkVersion = FrameworkVersionTypeConverter.LatestMatching("2.0");

            // This fires an event so set it last
            this.IsDirty = false;
        }

        /// <summary>
        /// This is used to save the project file
        /// </summary>
        public void SaveProject()
        {
            this.Filename = Path.GetFullPath(filename);

            // Write it to memory first so that we don't lose any existing
            // version on disk if something goes wrong while writing it out.
            MemoryStream ms = new MemoryStream(10240);
            XmlTextWriter xw = null;

            try
            {
                // Get all public instance properties for this class
                if(propertyCache == null)
                    propertyCache = this.GetType().GetProperties(
                        BindingFlags.Public | BindingFlags.Instance);

                xw = new XmlTextWriter(ms, new UTF8Encoding(true));
                xw.Formatting = Formatting.Indented;
                xw.Indentation = 4;

                xw.WriteStartElement("project");
                xw.WriteAttributeString("schemaVersion",
                    SandcastleProject.SchemaVersion.ToString(4));

                // Store the assembly information
                assemblies.WriteXml(xw);

                // Store the namespace summary information
                namespaceSummaries.WriteXml(xw);

                // Store the additional content information
                additionalContent.WriteXml(xw);

                // Store the dependency information
                dependencies.WriteXml(xw);

                // Store the component configurations
                componentConfigs.WriteXml(xw);

                // Store the project properties
                foreach(PropertyInfo property in propertyCache)
                    this.WriteProperty(xw, property);

                xw.WriteEndElement();
                xw.Flush();

                // Save the file to disk
                using(FileStream fs = File.Create(filename))
                {
                    ms.WriteTo(fs);
                }
            }
            catch(Exception ex)
            {
                throw new BuilderException(String.Format(
                    CultureInfo.CurrentCulture, "Error saving project " +
                    "to '{0}':\r\n{1}", filename, ex.Message), ex);
            }
            finally
            {
                if(xw != null)
                    xw.Close();

                ms.Dispose();
            }

            this.IsDirty = false;
        }

        /// <summary>
        /// This is used to load a project file
        /// </summary>
        /// <param name="projectFile">The project file to load</param>
        public void LoadProject(string projectFile)
        {
            FilePath filePath;
            DependencyItem di;
            XmlTextReader xr = null;
            Version schemaVersion;
            Dictionary<string, string> renameProps =
                new Dictionary<string, string>();

            object value;
            string version, deps, propName, path;
            string[] depList;

            this.ResetProject();
            this.Filename = Path.GetFullPath(projectFile);
            this.IsDirty = true;

            // Create a list of property names that need renaming due to
            // changes from version to version.
            renameProps.Add("showAttributes",     // v1.0.0.0 name
                "DocumentAttributes");
            renameProps.Add("hhcPath",            // v1.3.3.1 and prior
                "HtmlHelp1xCompilerPath");
            renameProps.Add("hxcompPath",         // v1.3.3.1 and prior
                "HtmlHelp2xCompilerPath");
            renameProps.Add("projectLinks",       // v1.3.3.1 and prior
                "ProjectLinkType");
            renameProps.Add("sdkLinks",           // v1.3.3.1 and prior
                "SdkLinkType");
            renameProps.Add("rootNSContainer",    // v1.3.3.1 and prior
                "RootNamespaceContainer");

            try
            {
                xr = new XmlTextReader(new StreamReader(filename));
                xr.MoveToContent();

                if(xr.EOF)
                    return;

                version = xr.GetAttribute("schemaVersion");

                if(String.IsNullOrEmpty(version))
                    throw new BuilderException(
                        "Invalid or missing schema version");

                schemaVersion = new Version(version);

                if(schemaVersion > SandcastleProject.SchemaVersion)
                    throw new BuilderException("The selected file is for a " +
                        "more recent version of the help file builder.  " +
                        "Please upgrade your copy to load the file.");

                while(!xr.EOF)
                {
                    if(xr.NodeType == XmlNodeType.Element)
                        switch(xr.Name)
                        {
                            case "project":     // Ignore the main project node
                                break;

                            case "additionalContent":
                                additionalContent.ReadXml(xr, schemaVersion);
                                break;

                            case "assemblies":
                                assemblies.ReadXml(xr, schemaVersion);
                                break;

                            case "componentConfigurations":
                                componentConfigs.ReadXml(xr, schemaVersion);
                                break;

                            case "namespaceSummaries":
                                namespaceSummaries.ReadXml(xr, schemaVersion);
                                break;

                            case "dependencies":
                                // The first version used a comma-separated
                                // string of dependencies.
                                if(schemaVersion.Major == 1 &&
                                  schemaVersion.Minor == 0)
                                {
                                    deps = xr.ReadString();

                                    if(deps.Length != 0)
                                    {
                                        depList = deps.Split(new char[] { ',' });

                                        foreach(string s in depList)
                                        {
                                            di = new DependencyItem();
                                            di.DependencyPath =
                                                new FileFolderGacPath(s.Trim());
                                            dependencies.Add(di);
                                        }
                                    }
                                }
                                else
                                    dependencies.ReadXml(xr, schemaVersion);
                                break;

                            default:
                                propName = xr.Name;

                                if(renameProps.ContainsKey(propName))
                                    propName = renameProps[propName];

                                value = this.SetProperty(propName,
                                    xr.ReadString());

                                filePath = value as FilePath;

                                // For file and folder paths, set the value
                                // from the attribute if present.
                                path = xr.GetAttribute("path");

                                if(filePath != null && path != null)
                                {
                                    filePath.Path = path;
                                    filePath.IsFixedPath = Path.IsPathRooted(
                                        path);
                                }
                                break;
                        }

                    xr.Read();
                }

                // The default for SealedProtected changed to true in v1.3.1.1
                Version changeVer = new Version(1, 3, 1, 1);

                if(schemaVersion < changeVer)
                    visibleItems |= VisibleItems.SealedProtected;
            }
            catch(Exception ex)
            {
                this.ResetProject();
                throw new BuilderException(String.Format(
                    CultureInfo.CurrentCulture, "Error reading project " +
                    "from '{0}':\r\n{1}", projectFile, ex.Message), ex);
            }
            finally
            {
                if(xr != null)
                    xr.Close();
            }

            this.IsDirty = false;
        }

        /// <summary>
        /// This is used to set the named property to the specified value
        /// using Reflection.
        /// </summary>
        /// <param name="name">The name of the property to set</param>
        /// <param name="value">The value to which it is set</param>
        /// <remarks>Property name matching is case insensitive as are the
        /// values themselves.  This is used to allow setting of simple project
        /// properties (non-collection) using command line parameters in the
        /// console mode builder.</remarks>
        /// <exception cref="ArgumentNullException">This is thrown if the
        /// name parameter is null or an empty string.</exception>
        /// <exception cref="BuilderException">This is thrown if an error
        /// occurs while trying to set the named property.</exception>
        /// <returns>The parsed object value to which the property was set.</returns>
        public object SetProperty(string name, string value)
        {
            object parsedValue;

            if(String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            // Get all public instance properties for this class
            if(propertyCache == null)
                propertyCache = this.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);

            foreach(PropertyInfo property in propertyCache)
                if(String.Compare(name, property.Name, true,
                  CultureInfo.InvariantCulture) == 0 && property.CanWrite)
                {
                    try
                    {
                        if(property.PropertyType.IsEnum)
                            parsedValue = Enum.Parse(property.PropertyType,
                                value, true);
                        else
                        {
                            TypeConverter tc = TypeDescriptor.GetConverter(
                                property.PropertyType);
                            parsedValue = tc.ConvertFromString(value);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new BuilderException("Unable to parse value '" +
                            value + "' for property '" + name + "'", ex);
                    }

                    property.SetValue(this, parsedValue, null);
                    return parsedValue;
                }

            throw new BuilderException("An attempt was made to set an " +
                "unknown or read-only property: " + name + "   Value: " +
                value);
        }
    }
}
