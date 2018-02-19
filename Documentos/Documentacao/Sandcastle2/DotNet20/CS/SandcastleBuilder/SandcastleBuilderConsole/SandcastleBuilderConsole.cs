//=============================================================================
// System  : Sandcastle Help File Builder
// File    : SandcastleBuilderConsole.cs
// Author  : Eric Woodruff
// Updated : 02/23/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the console mode version of the Sandcastle Help File
// Builder.
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
// 1.0.0.0  08/02/2006  EFW  Created the code
// 1.3.1.2  10/18/2006  EFW  Moved file logging to the BuildProcess class
// 1.3.4.0  12/27/2006  EFW  Added support for setting project properties
//                           using command line parameters.
// 1.4.0.0  02/23/2007  EFW  Added support for three value parameters
//=============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

using SandcastleBuilder.Utils;

namespace SandcastleBuilderConsole
{
    /// <summary>
    /// This is the console mode version of the Sandcastle Help File Builder.
    /// </summary>
    class SandcastleBuilderConsole
    {
        //=====================================================================
        // Private data members

        private static bool verbose;
        private static BuildStep lastBuildStep;

        private static SandcastleProject currentProject;
        private static BuildProcess buildProcess;

        //=====================================================================
        // Methods

        /// <summary>
        /// Main program entry point
        /// </summary>
        /// <param name="args">The command line arguments.  This should be a
        /// list of projects to build and an optional "/v" option to provide
        /// verbose output.</param>
        /// <returns>Zero on success, non-zero on failure</returns>
        static int Main(string[] args)
        {
            DocumentAssembly docAsm;
            ContentItem ci;
            DependencyItem di;
            OptionInfo lastOption = null;

            SandcastleProject newProject = new SandcastleProject();
            List<OptionInfo> options = new List<OptionInfo>();
            List<SandcastleProject> projects = new List<SandcastleProject>();
            bool success = false;
            char[] wildcards = new char[] { '*', '?' };
            string currentFolder = Directory.GetCurrentDirectory();

            Assembly asm = Assembly.GetEntryAssembly();

            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            Console.WriteLine("{0}, version {1}\r\n{2}\r\nE-Mail: " +
                "Eric@EWoodruff.us\r\n", fvi.ProductName, fvi.ProductVersion,
                fvi.LegalCopyright);

            try
            {
                // Get a list of options and expand any response files
                foreach(string option in args)
                    if(option[0] == '@')
                        LoadResponseFile(option.Substring(1), options);
                    else
                        options.Add(new OptionInfo(option));

                if(options.Count == 0)
                {
                    ShowHelp();
                    return 1;
                }

                // Build a list of projects and apply any properties to them
                // as needed.
                foreach(OptionInfo option in options)
                {
                    lastOption = option;

                    switch(option.Name)
                    {
                        case "help":
                        case "?":
                            ShowHelp();
                            break;

                        case "verbose":
                        case "v":
                            verbose = true;
                            break;

                        case "new":
                        case "project":
                            if(newProject.IsDirty || newProject.Filename !=
                              SandcastleProject.DefaultName)
                                projects.Add(newProject);

                            newProject = new SandcastleProject();

                            if(option.Name == "project")
                                newProject.LoadProject(option.Value);
                            break;

                        case "assembly":
                            if(option.Value.IndexOfAny(wildcards) == -1)
                            {
                                // Single assembly
                                docAsm = new DocumentAssembly();
                                docAsm.AssemblyPath = new FilePath(option.Value);

                                if(!String.IsNullOrEmpty(option.SecondValue))
                                    docAsm.XmlCommentsPath =
                                        new FilePath(option.SecondValue);

                                newProject.Assemblies.Add(docAsm);
                            }
                            else
                                AddAssemblyWildCard(option.Value, newProject,
                                    false);
                            break;

                        case "comments":
                            if(option.Value.IndexOfAny(wildcards) == -1)
                            {
                                docAsm = new DocumentAssembly();
                                docAsm.XmlCommentsPath = new FilePath(option.Value);
                                docAsm.CommentsOnly = true;

                                newProject.Assemblies.Add(docAsm);
                            }
                            else
                                AddAssemblyWildCard(option.Value, newProject,
                                    true);
                            break;

                        case "addcontent":
                            ci = new ContentItem();
                            ci.SourcePath = new FilePath(option.Value);

                            if(!String.IsNullOrEmpty(option.SecondValue))
                                ci.DestinationPath = option.SecondValue;

                            newProject.AdditionalContent.Add(ci);

                            if(!String.IsNullOrEmpty(option.ThirdValue))
                                ci.ExcludeItems = Convert.ToBoolean(
                                    option.ThirdValue, CultureInfo.InvariantCulture);
                            break;

                        case "dependency":
                            di = new DependencyItem();
                            di.DependencyPath = new FileFolderGacPath(
                                option.Value);

                            newProject.Dependencies.Add(di);
                            break;

                        case "component":
                            newProject.ComponentConfigurations.Add(
                                option.Value, option.SecondValue);
                            break;

                        default:
                            // Set a simple project property
                            newProject.SetProperty(option.Name, option.Value);
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                if(lastOption != null)
                    Console.WriteLine("Fatal error applying command line " +
                        "option '{0}' in project '{1}': {2}",
                        lastOption.OptionText, newProject.Filename,
                        ex.ToString());
                else
                    Console.WriteLine("Unexpected error while parsing " +
                        "command line options: {0}", ex.ToString());

                return 2;
            }

            // Add the final project if necessary
            if(newProject.IsDirty || newProject.Filename !=
              SandcastleProject.DefaultName)
                projects.Add(newProject);

            // Build each of the projects
            foreach(SandcastleProject project in projects)
            {
                success = BuildProject(project);

                if(!success)
                    break;

                // Switch back to the original folder in case we are
                // building a default project next.
                Directory.SetCurrentDirectory(currentFolder);
            }

            return (success) ? 0 : 3;
        }

        /// <summary>
        /// Load settings from a response file.
        /// </summary>
        /// <param name="filename">The response file to load.</param>
        /// <param name="options">The option list to which the content is
        /// added.</param>
        /// <remarks>Nested response files are supported.  Blank lines and
        /// lines starting with '#' (comments) are ignored.</remarks>
        private static void LoadResponseFile(string filename,
          List<OptionInfo> options)
        {
            string content, option;
            string[] lines;

            using(StreamReader sr = new StreamReader(filename))
            {
                content = sr.ReadToEnd();

                lines = content.Replace("\r\n", "\n").Split(new char[] { '\n' });

                foreach(string entry in lines)
                {
                    option = entry.Trim();

                    if(option.Length != 0)
                        if(option[0] == '@')
                            LoadResponseFile(option.Substring(1), options);
                        else
                            if(option[0] != '#')
                                options.Add(new OptionInfo(option));
                }
            }
        }

        /// <summary>
        /// This is called to add multiple assemblies and/or comments files to
        /// a project for documenting.
        /// </summary>
        /// <param name="wildcard">The wildcard to add.</param>
        /// <param name="project">The project to which they are added.</param>
        /// <param name="commentsOnly">True if adding comments only, false if
        /// adding assembly/comments file pairs.</param>
        private static void AddAssemblyWildCard(string wildcard,
          SandcastleProject project, bool commentsOnly)
        {
            DocumentAssembly newItem;
            string dirName, fileSpec;
            string[] files;
            int idx;

            idx = wildcard.LastIndexOf('\\');
            if(idx == -1)
            {
                dirName = Directory.GetCurrentDirectory();
                fileSpec = wildcard;
            }
            else
            {
                dirName = Path.GetFullPath(wildcard.Substring(0, idx));
                fileSpec = wildcard.Substring(idx + 1);
            }

            files = Directory.GetFiles(dirName, fileSpec);

            foreach(string file in files)
            {
                newItem = new DocumentAssembly();

                if(file.ToLower(CultureInfo.InvariantCulture).EndsWith(".xml"))
                {
                    newItem.XmlCommentsPath = new FilePath(file);
                    if(newItem.AssemblyPath.Path.Length == 0)
                        if(commentsOnly)
                            newItem.CommentsOnly = true;
                        else
                            newItem.AssemblyPath = new FilePath("Unknown.dll");
                }
                else
                {
                    newItem.AssemblyPath = new FilePath(file);
                    if(newItem.XmlCommentsPath.Path.Length == 0)
                        newItem.XmlCommentsPath = new FilePath("Unknown.xml");
                }

                if(!project.Assemblies.Contains(newItem))
                    project.Assemblies.Add(newItem);
            }
        }

        /// <summary>
        /// This is used to show the command line syntax
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine(@"Syntax:
SandcastleBuilderConsole [@responseFile] [projectFile] [-options] ...

Prefix options with '-' or '/'.  Names and values are case-insensitive.  All
property options are applied to the last specified project or a default project
if one is not specified.  Property values should be enclosed in double quotes
if they contain spaces, commas, or other special characters.

@responseFile       A response file containing a list of projects and options
                    to use (one project filename or option per line).  Nested
                    response files are supported.  Blank lines and lines
                    starting with '#' (comments) are ignored.  

projectFile         One or more project files to compile.  Project options are
                    reset to those of the specified project.  If not specified,
                    a default blank project is used and all paths are relative
                    to the current folder at runtime.

-help or -?         Show this help.

-verbose or -v      Enable verbose logging.

-new                Start another new project with default settings.

-property=value     Set the named project property (non-collection) to the
                    specified value.  This can be used to override settings in
                    a loaded project file.

-assembly=assembly,comments
                    Add an assembly and comments file to document.  Comments
                    file is optional if named after the assembly.  Assembly
                    can be a wildcard to add multiple assemblies with matching
                    comments files.  Comments file is ignored if a wildcard
                    is used for the assembly.

-comments=commentsFile
                    Add a 'Comments only' or project/namespace comments file
                    to document.  Wildcards are supported to add multiple files
                    at once.

-addcontent=source,dest,exclude
                    Add additional content (source filespec and relative
                    destination path).  If the destination path is omitted,
                    the content is copied to the root of the output folder.
                    The 'exclude' option is optional.  If specified it should
                    be 'true' or 'false'.  If true, it indicates that the
                    source path is used to specify a list of files that should
                    be excluded from the additional content.  In such cases,
                    the destination path is ignored.

-dependency=filespec
                    A wildcard, single file, or GAC dependency.

-component=id,config
                    Add a component configuration override.");
        }

        /// <summary>
        /// This is called to build a project
        /// </summary>
        /// <param name="project">The project to build</param>
        /// <returns>Returns true if successful, false if not</returns>
        private static bool BuildProject(SandcastleProject project)
        {
            lastBuildStep = BuildStep.None;
            currentProject = project;

            Console.WriteLine("\nBuilding {0}", project.Filename);

            try
            {
                buildProcess = new BuildProcess(currentProject);
                buildProcess.BuildStepChanged +=
                    new EventHandler<BuildProgressEventArgs>(
                        buildProcess_BuildStepChanged);
                buildProcess.BuildProgress +=
                    new EventHandler<BuildProgressEventArgs>(
                        buildProcess_BuildProgress);

                // Since this is a console app, we'll run it directly rather
                // than in a background thread.
                buildProcess.Build();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Fatal error, unable to compile " +
                    "project '{0}': {1}", project.Filename, ex.ToString());
            }

            return (lastBuildStep == BuildStep.Completed);
        }

        /// <summary>
        /// This is called by the build process thread to update the
        /// application with the current build step.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private static void buildProcess_BuildStepChanged(object sender,
          BuildProgressEventArgs e)
        {
            string outputPath;

            if(!verbose)
                Console.WriteLine(e.BuildStep.ToString());

            if(e.HasCompleted)
            {
                // If successful, report the location of the help file/website
                if(e.BuildStep == BuildStep.Completed)
                {
                    outputPath = buildProcess.OutputFolder +
                        currentProject.HtmlHelpName;

                    if(currentProject.HelpFileFormat ==
                      HelpFileFormat.HtmlHelp1x)
                        outputPath += ".chm";
                    else
                        if(currentProject.HelpFileFormat ==
                          HelpFileFormat.HtmlHelp2x)
                            outputPath += ".hxs";

                    // Report single file or multi-format output location
                    if(File.Exists(outputPath))
                        Console.WriteLine("The help file is located at: {0}",
                            outputPath);
                    else
                        Console.WriteLine("The help output is located at: {0}",
                            buildProcess.OutputFolder);
                }

                if(File.Exists(buildProcess.LogFilename))
                    Console.WriteLine("Build details can be found in {0}",
                        buildProcess.LogFilename);
            }

            lastBuildStep = e.BuildStep;
        }

        /// <summary>
        /// This is called by the build process thread to update the
        /// application with information about progress.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private static void buildProcess_BuildProgress(object sender,
          BuildProgressEventArgs e)
        {
            if(verbose || e.BuildStep == BuildStep.Failed)
                Console.WriteLine(e.Message);
        }
    }
}
