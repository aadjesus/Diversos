//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.cs
// Author  : Eric Woodruff
// Updated : 03/09/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the thread class that handles all aspects of the build
// process.
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
// 1.0.0.0  08/04/2006  EFW  Created the code
// 1.3.0.0  09/09/2006  EFW  Added support for website output
// 1.3.1.0  09/29/2006  EFW  Added support for the ShowMissing* properties
// 1.3.1.0  10/02/2006  EFW  Added support for the September CTP and the
//                           the Document* properties.
// 1.3.2.0  11/10/2006  EFW  Added support for DocumentAssembly.CommentsOnly
// 1.3.4.0  12/24/2006  EFW  Various additions and updates
// 1.4.0.0  03/07/2007  EFW  Added support for the March 2007 CTP
//=============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

using SandcastleBuilder.Utils.Gac;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This class is used to handle all aspects of the build process in a
    /// separate thread.
    /// </summary>
    public partial class BuildProcess
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private SandcastleProject project;      // The project to build

        // The log file stream
        private StreamWriter swLog;

        // Progress event arguments
        private BuildProgressEventArgs progressArgs;
        private DateTime buildStart, stepStart;

        // Various paths and other strings
        private string shfbFolder, templateFolder, projectFolder, outputFolder,
            workingFolder, sandcastleFolder, hhcFolder, hxcompFolder,
            languageFolder, webFolder, presentationFolder, defaultTopic;

        // Process information for the tools and scripts
        private Process currentProcess;
        private Thread stdOutThread, stdErrThread;
        private bool errorDetected;

        private CultureInfo language;   // The project language

        // The current reflection information file used in various steps
        private XmlDocument reflectionInfo;

        // Regular expressions used for error handling
        private static Regex reErrorCheck = new Regex(
            @"^(Error|UnrecognizedOption|Unhandled Exception|Fatal Error|" +
            @"HHC\d+: Error|(Fatal )?Error HXC\d+|Process is terminated):",
            RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static Regex reKillProcess = new Regex(
            "hhc|hxcomp|BuildAssembler|XslTransform|MRefBuilder",
            RegexOptions.IgnoreCase);
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This returns the output folder where the log file and help file
        /// can be found after the build process has finished.
        /// </summary>
        public string OutputFolder
        {
            get { return outputFolder; }
        }

        /// <summary>
        /// This returns the name of the log file used for saving the
        /// build progress messages.
        /// </summary>
        public string LogFilename
        {
            get { return outputFolder + "LastBuild.log"; }
        }
        #endregion

        #region Events
        //=====================================================================
        // Events

        /// <summary>
        /// This event is raised to report a change in the build step
        /// </summary>
        public event EventHandler<BuildProgressEventArgs> BuildStepChanged;

        /// <summary>
        /// This raises the <see cref="BuildStepChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnBuildStepChanged(BuildProgressEventArgs e)
        {
            if(BuildStepChanged != null)
                BuildStepChanged(this, e);
        }

        /// <summary>
        /// This event is raised to report progress information throughout
        /// each build step.
        /// </summary>
        public event EventHandler<BuildProgressEventArgs> BuildProgress;

        /// <summary>
        /// This raises the <see cref="BuildProgress"/> event.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnBuildProgress(BuildProgressEventArgs e)
        {
            if(BuildProgress != null)
                BuildProgress(this, e);
        }
        #endregion

        //=====================================================================
        // Methods, etc.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buildProject">The project to build</param>
        public BuildProcess(SandcastleProject buildProject)
        {
            project = buildProject;
            progressArgs = new BuildProgressEventArgs();

            fieldMatchEval = new MatchEvaluator(OnFieldMatch);
            contentMatchEval = new MatchEvaluator(OnContentMatch);
            linkMatchEval = new MatchEvaluator(OnLinkMatch);
            codeBlockMatchEval = new MatchEvaluator(OnCodeBlockMatch);
            excludeElementEval = new MatchEvaluator(OnExcludeElement);
        }

        /// <summary>
        /// Call this method to perform the build on the project.
        /// </summary>
        /// <event cref="BuildStepChanged">This event fires when the
        /// current build step changes.</event>
        /// <event cref="BuildProgress">This event fires to report progress
        /// information.</event>
        public void Build()
        {
            string reflectionFile, helpFile, presentationParam;
            int waitCount;

            System.Diagnostics.Debug.WriteLine("Build process starting\r\n");

            try
            {
                Assembly asm = Assembly.GetEntryAssembly();

                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                this.ReportProgress(BuildStep.Initializing,
                    "[{0}, version {1}]", fvi.ProductName, fvi.ProductVersion);

                buildStart = stepStart = DateTime.Now;

                // Base folder for SHFB
                shfbFolder = Path.GetDirectoryName(asm.Location) + @"\";

                // Get the location of the template files
                templateFolder = shfbFolder + @"Templates\";

                // Get the location of the web files
                webFolder = shfbFolder + @"\Web\";

                // Make sure we start out in the project's output folder
                // in case the output folder is relative to it.
                projectFolder = Path.GetDirectoryName(project.Filename);
                if(projectFolder.Length == 0)
                    projectFolder = Directory.GetCurrentDirectory();

                projectFolder += @"\";

                Directory.SetCurrentDirectory(projectFolder);

                this.ReportProgress("Creating output and working folders...");

                outputFolder = project.OutputPath;
                if(String.IsNullOrEmpty(outputFolder))
                    outputFolder = Directory.GetCurrentDirectory();
                else
                    outputFolder = Path.GetFullPath(outputFolder);

                if(!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                if(!outputFolder.EndsWith(@"\"))
                    outputFolder += @"\";

                // Create the log file
                swLog = new StreamWriter(this.LogFilename);
                swLog.WriteLine("[{0}, version {1}]", fvi.ProductName,
                    fvi.ProductVersion);

                if(project.WorkingPath.Path.Length == 0)
                    workingFolder = outputFolder + @"Working\";
                else
                    workingFolder = project.WorkingPath;

                if(Directory.Exists(workingFolder))
                {
                    // Clear any data from a prior run
                    this.ReportProgress(BuildStep.ClearWorkFolder,
                        "Clearing working folder...");
                    Directory.Delete(workingFolder, true);
                }

                // If the help file is open, it will fail to build so try
                // to get rid of it now before we get too far into it.
                helpFile = outputFolder + project.HtmlHelpName + ".chm";

                if((project.HelpFileFormat & HelpFileFormat.HtmlHelp1x) != 0 &&
                  File.Exists(helpFile))
                    File.Delete(helpFile);

                helpFile = Path.ChangeExtension(helpFile, ".hxs");

                if((project.HelpFileFormat & HelpFileFormat.HtmlHelp2x) != 0 &&
                  File.Exists(helpFile))
                    File.Delete(helpFile);

                Directory.CreateDirectory(workingFolder);

                // Make sure we can find the tools
                this.ReportProgress(BuildStep.FindingTools, "Finding tools...");
                this.FindTools();

                this.GarbageCollect();

                // Get the location of the language resources.  If not found,
                // default to US English
                languageFolder = shfbFolder + @"\LanguageResources\";

                if(Directory.Exists(languageFolder + project.Language.Name))
                {
                    language = project.Language;
                    languageFolder += language.Name;
                }
                else
                {
                    // Warn the user about the default being used
                    this.ReportProgress("\r\nWarning: Language resources " +
                        "for '{0}' could not be found.  Using en-US defaults.\r\n",
                        project.Language.Name);
                    language = new CultureInfo("en-US");
                    languageFolder += "en-US";
                }

                // Validate the assembly information and copy them
                // to the working folder
                this.ReportProgress(BuildStep.ValidatingAssemblies,
                    "Validating documentation assembly information...");
                this.ValidateAssemblies();

                // Create the Sandcastle configuration file
                this.ReportProgress(BuildStep.CreateConfig,
                    "-------------------------------\r\n" +
                    "Creating Sandcastle configuration file...");
                this.TransformTemplate("sandcastle.config", templateFolder,
                    workingFolder);

                // Merge the build component custom configurations
                if(project.ComponentConfigurations.Count != 0)
                    this.MergeComponentConfigurations();

                // Transform the language resource files
                this.ReportProgress(BuildStep.GenerateSharedContent,
                    "-------------------------------\r\n" +
                    "Generating shared content files ({0})...",
                    language.Name);
                foreach(string file in Directory.GetFiles(languageFolder))
                    this.TransformTemplate(Path.GetFileName(file),
                        languageFolder, workingFolder);

                // Copy any dependencies to the working folder
                if(project.Dependencies.Count != 0)
                {
                    this.ReportProgress(BuildStep.GenerateSharedContent,
                        "-------------------------------\r\n" +
                        "Copying document assembly dependencies...");
                    this.CopyDependencies();
                }

                // Generate the reflection information
                this.ReportProgress(BuildStep.GenerateReflectionInfo,
                    "-------------------------------\r\n" +
                    "Generating reflection information...");
                this.TransformTemplate("MRefBuilder.config",
                    templateFolder, workingFolder);
                this.RunProcess(this.TransformTemplate("GenerateRefInfo.bat",
                    templateFolder, workingFolder), null);

                reflectionFile = workingFolder + "reflection.org";
                reflectionInfo = new XmlDocument();
                reflectionInfo.Load(reflectionFile);

                // Purge duplicate topics if requested
                if(project.PurgeDuplicateTopics)
                    this.PurgeDuplicateTopics();

                // Generate namespace summary information and/or filter out
                // unwanted namespaces.
                this.GenerateNamespaceSummaries();

                // Remove unwanted items from the reflection information file
                this.ApplyVisibilityProperties();

                // Backup the original for reference and save the changed file
                File.Copy(reflectionFile, Path.ChangeExtension(reflectionFile,
                    ".bak"), true);
                reflectionInfo.Save(reflectionFile);
                reflectionInfo = null;

                this.GarbageCollect();

                // Transform the reflection output.  First we need to figure
                // out which style is in effect.  Base it on whether the
                // presentation style folder contains "v2005" or "prototype".
                this.ReportProgress(BuildStep.TransformReflectionInfo,
                    "-------------------------------\r\n" +
                    "Transforming reflection output...");

                presentationParam = project.PresentationStyle.ToLower(
                    CultureInfo.InvariantCulture);

                if(presentationParam.IndexOf("vs2005") != -1)
                    presentationParam = "vs2005";
                else
                {
                    presentationParam = "prototype";

                    if(presentationParam.IndexOf("prototype") == -1)
                        this.ReportProgress("Warning: Unable to determine " +
                            "presentation style from folder '{0}'.  Assuming " +
                            "Prototype style.", project.PresentationStyle);
                }

                this.RunProcess(this.TransformTemplate("TransformManifest.bat",
                    templateFolder, workingFolder), presentationParam);

                // Load the transformed file
                reflectionFile = reflectionFile.Replace(".org", ".xml");
                reflectionInfo = new XmlDocument();
                reflectionInfo.Load(reflectionFile);

                // Alter the help topic filenames?
                if(project.NamingMethod != NamingMethod.Guid)
                    this.ModifyHelpTopicFilenames();

                // Build the help topics
                this.ReportProgress(BuildStep.CopyStandardContent,
                    "-------------------------------\r\n" +
                    "Copying standard help content...");
                this.CopyStandardHelpContent();

                if(project.AdditionalContent.Count != 0)
                {
                    this.ReportProgress(BuildStep.CopyAdditionalContent,
                        "\r\nCopying additional content files...");
                    this.CopyAdditionalContent(false);
                }

                // Backup the original for reference and save the changed file
                File.Copy(reflectionFile, Path.ChangeExtension(reflectionFile,
                    ".bak"), true);

                reflectionInfo.Save(reflectionFile);
                commentsFiles.Save();

                reflectionInfo = null;
                commentsFiles = null;

                this.GarbageCollect();

                this.ReportProgress(BuildStep.BuildHelpTopics,
                    "\r\nBuilding help topics...");
                this.RunProcess(this.TransformTemplate("BuildHelpTopics.bat",
                    templateFolder, workingFolder), null);

                // The last part differs based on the help file format
                if((project.HelpFileFormat & HelpFileFormat.Website) != 0)
                {
                    // Generate the table of content and set the
                    // default topic.  We'll re-use the CHM table
                    // of content and convert it to an HTML tree.
                    this.ReportProgress(BuildStep.GenerateTableOfContent,
                        "-------------------------------\r\n" +
                        "Generating table of content for website...");
                    this.RunProcess(this.TransformTemplate(
                        "Generate1xTOC.bat", templateFolder,
                        workingFolder), presentationParam);

                    this.ReportProgress(BuildStep.UpdateTableOfContent,
                        "Updating table of content with additional " +
                        "content items and determining default topic...");
                    this.UpdateTableOfContent(HelpFileFormat.Website);

                    this.GenerateWebsite();
                }

                if((project.HelpFileFormat & HelpFileFormat.HtmlHelp1x) != 0)
                {
                    // Generate the table of content and set the
                    // default topic.
                    this.ReportProgress(BuildStep.GenerateTableOfContent,
                        "-------------------------------\r\n" +
                        "Generating table of content for HTML Help 1.x file...");
                    this.RunProcess(this.TransformTemplate(
                        "Generate1xTOC.bat", templateFolder,
                        workingFolder), presentationParam);

                    this.ReportProgress(BuildStep.UpdateTableOfContent,
                        "Updating table of content with additional " +
                        "content items and determining default topic ...");
                    this.UpdateTableOfContent(HelpFileFormat.HtmlHelp1x);

                    // Generate the help file index
                    this.ReportProgress(BuildStep.GenerateHelpFileIndex,
                        "-------------------------------\r\n" +
                        "Generating help file index...");
                    this.RunProcess(this.TransformTemplate(
                        "Generate1xIndex.bat", templateFolder,
                        workingFolder), null);

                    // Generate help project file
                    this.ReportProgress(BuildStep.GenerateHelpProject,
                        "-------------------------------\r\n" +
                        "Generating help project file...");
                    this.TransformTemplate("Help1x.hhp",
                        templateFolder, workingFolder);

                    // Build the HTML 1.x help file
                    this.ReportProgress(BuildStep.CompilingHelpFile,
                        "-------------------------------\r\n" +
                        "Compiling the help file...");
                    this.RunProcess(this.TransformTemplate(
                        "Build1xHelpFile.bat", templateFolder,
                        workingFolder), null);
                }

                if((project.HelpFileFormat & HelpFileFormat.HtmlHelp2x) != 0)
                {
                    // Generate the table of content and set the
                    // default topic.
                    this.ReportProgress(BuildStep.GenerateTableOfContent,
                        "-------------------------------\r\n" +
                        "Generating table of content for HTML Help 2.x file...");
                    this.RunProcess(this.TransformTemplate(
                        "Generate2xTOC.bat", templateFolder,
                        workingFolder), presentationParam);

                    this.ReportProgress(BuildStep.UpdateTableOfContent,
                        "Updating table of content with additional " +
                        "content items...");
                    this.UpdateTableOfContent(HelpFileFormat.HtmlHelp2x);

                    // Generate help project file
                    this.ReportProgress(BuildStep.GenerateHelpProject,
                        "-------------------------------\r\n" +
                        "Generating help project files...");
                    this.TransformTemplate("Help2x.hxc", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x.hxf", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x_a.hxk", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x_b.hxk", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x_f.hxk", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x_k.hxk", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x_n.hxk", templateFolder,
                        workingFolder);
                    this.TransformTemplate("Help2x_s.hxk", templateFolder,
                        workingFolder);

                    // Build the HTML 2.x help file
                    this.ReportProgress(BuildStep.CompilingHelpFile,
                        "-------------------------------\r\n" +
                        "Compiling the help file...");
                    this.RunProcess(this.TransformTemplate(
                        "Build2xHelpFile.bat", templateFolder,
                        workingFolder), null);
                }

                // All done
                if(project.CleanIntermediates)
                {
                    this.ReportProgress(BuildStep.CleanIntermediates,
                        "Removing intermediate files...");
                    Directory.Delete(workingFolder, true);
                }

                progressArgs.HasCompleted = true;

                TimeSpan runtime = DateTime.Now - buildStart;

                this.ReportProgress(BuildStep.Completed,
                    "\r\n\r\nBuild completed successfully.  Total time: " +
                    "{0:00}:{1:00}:{2:00.0000}\r\n",
                    Math.Floor(runtime.TotalSeconds / 3600),
                    Math.Floor((runtime.TotalSeconds % 3600) / 60),
                    (runtime.TotalSeconds % 60));
                System.Diagnostics.Debug.WriteLine(
                    "Build process finished successfully\r\n");
            }
            catch(ThreadAbortException )
            {
                // Kill off the process, known child processes and the STDOUT
                // and STDERR threads too if necessary.
                if(currentProcess != null && !currentProcess.HasExited)
                {
                    currentProcess.Kill();

                    foreach(Process p in Process.GetProcesses())
                        if(reKillProcess.IsMatch(p.ProcessName))
                        {
                            System.Diagnostics.Debug.WriteLine("Killing " +
                                p.ProcessName);
                            p.Kill();
                        }
                }

                if(stdOutThread != null && stdOutThread.IsAlive)
                {
                    stdOutThread.Abort();
                    waitCount = 0;

                    while(waitCount < 5 && !stdOutThread.Join(1000))
                        waitCount++;
                }

                if(stdErrThread != null && stdErrThread.IsAlive)
                {
                    stdErrThread.Abort();
                    waitCount = 0;

                    while(waitCount < 5 && !stdErrThread.Join(1000))
                        waitCount++;
                }

                progressArgs.HasCompleted = true;
                this.ReportProgress(BuildStep.Canceled,
                    "\r\n\r\nBUILD CANCELLED BY USER\r\n");
                System.Diagnostics.Debug.WriteLine("Build process aborted\r\n");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                progressArgs.HasCompleted = true;
                this.ReportProgress(BuildStep.Failed,
                    "\r\n\r\nBUILD FAILED: {0}\r\n", ex.Message);
                System.Diagnostics.Debug.WriteLine("Build process failed\r\n");
            }
            finally
            {
                if(currentProcess != null)
                    currentProcess.Dispose();

                this.GarbageCollect();

                if(swLog != null)
                {
                    swLog.Close();
                    swLog = null;
                }
            }
        }

        /// <summary>
        /// This is used to report progress during the build process
        /// within the current step.
        /// </summary>
        /// <param name="message">The message to report</param>
        /// <param name="args">A list of arguments to format into the
        /// message text</param>
        /// <overloads>This method has two overloads.</overloads>
        protected void ReportProgress(string message, params object[] args)
        {
            this.ReportProgress(progressArgs.BuildStep, message, args);
        }

        /// <summary>
        /// This is used to report progress during the build process
        /// and possible update the current step.
        /// </summary>
        /// <param name="step">The current build step</param>
        /// <param name="message">The message to report</param>
        /// <param name="args">A list of arguments to format into the
        /// message text</param>
        /// <event cref="BuildStepChanged">This event fires when the
        /// current build step changes.</event>
        /// <event cref="BuildProgress">This event fires to report progress
        /// information.</event>
        protected void ReportProgress(BuildStep step, string message,
          params object[] args)
        {
            TimeSpan runtime;

            bool stepChanged = (progressArgs.BuildStep != step);

            if(stepChanged)
            {
                // Don't bother for the initialization steps
                if(step > BuildStep.CreateConfig)
                {
                    runtime = DateTime.Now - stepStart;
                    progressArgs.Message = String.Format(
                        CultureInfo.InvariantCulture, "    Last step " +
                        "completed in {0:00}:{1:00}:{2:00.0000}",
                        Math.Floor(runtime.TotalSeconds / 3600),
                        Math.Floor((runtime.TotalSeconds % 3600) / 60),
                        (runtime.TotalSeconds % 60));

                    if(swLog != null)
                        swLog.WriteLine(progressArgs.Message);

                    this.OnBuildProgress(progressArgs);
                }

                stepStart = DateTime.Now;
                progressArgs.BuildStep = step;
            }

            progressArgs.Message = String.Format(CultureInfo.CurrentCulture,
                message, args);

            // Save the message to the log file
            if(swLog != null)
                swLog.WriteLine(progressArgs.Message);

            // Report progress first and then the step change so that any
            // final information gets saved to the log file.
            this.OnBuildProgress(progressArgs);

            if(stepChanged)
                OnBuildStepChanged(progressArgs);
        }

        /// <summary>
        /// Find the Sandcastle tools and the HTML help compiler
        /// </summary>
        /// <exception cref="BuilderException">This is thrown if any of
        /// the tools cannot be found.</exception>
        protected void FindTools()
        {
            sandcastleFolder = project.SandcastlePath;

            // Try to find it based on the DXROOT environment variable
            if(sandcastleFolder.Length == 0)
            {
                sandcastleFolder = Environment.GetEnvironmentVariable("DXROOT");
                if(String.IsNullOrEmpty(sandcastleFolder) ||
                  !sandcastleFolder.Contains(@"\Sandcastle"))
                    sandcastleFolder = String.Empty;
            }

            // Try to find Sandcastle based on the path if not specified
            // in the project or DXROOT.
            if(sandcastleFolder.Length == 0)
            {
                Match m = Regex.Match(Environment.GetEnvironmentVariable("PATH"),
                    @"[A-Z]:\\.[^;]+\\Sandcastle(?=\\Prod)",
                    RegexOptions.IgnoreCase);

                // If not found in the path, search all fixed drives
                if(m.Success)
                    sandcastleFolder = m.Value;
                else
                {
                    sandcastleFolder = BuildProcess.FindOnFixedDrives(
                        @"\Sandcastle");

                    // If not found there, try the VS 2005 SDK folders
                    if(sandcastleFolder.Length == 0)
                    {
                        sandcastleFolder = BuildProcess.FindSdkExecutable(
                            "MRefBuilder.exe");

                        if(sandcastleFolder.Length != 0)
                            sandcastleFolder = sandcastleFolder.Substring(0,
                                sandcastleFolder.LastIndexOf('\\'));
                    }
                }
            }
            else
                sandcastleFolder = Path.GetFullPath(sandcastleFolder);

            if(!sandcastleFolder.EndsWith(@"\"))
                sandcastleFolder += @"\";

            if(sandcastleFolder.Length == 0 ||
              !Directory.Exists(sandcastleFolder) || !File.Exists(
              sandcastleFolder + @"ProductionTools\MRefBuilder.exe"))
                throw new BuilderException("Could not find path to Sandcastle tools");

            this.ReportProgress("Found Sandcastle in '{0}'", sandcastleFolder);

            // Set the presentation folder too
            presentationFolder = String.Format(CultureInfo.InvariantCulture,
                @"{0}Presentation\{1}\", sandcastleFolder,
                project.PresentationStyle);

            // Make sure we've got a version we can use
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(
                sandcastleFolder + @"ProductionTools\MRefBuilder.exe");

            Version fileVersion = new Version(fvi.FileMajorPart,
                    fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart);
            Version expectedVersion = new Version("2.2.63000.2");

            if(fileVersion < expectedVersion)
                throw new BuilderException("Your version of Sandcastle is " +
                    "out of date.  Please download the latest version.");

            // Warn if the version is different.  The user may need to
            // upgrade the help file builder.
            if(fileVersion > expectedVersion)
            {
                // I tend to forget, so this is a clue to me that I
                // need to update the version number used above.
                if(System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

                this.ReportProgress("\r\nWarning: MRefBuilder has a version " +
                    "of '{0}' but version '{1}' was expected.  You may need " +
                    "to update your copy of the help file builder.\r\n",
                    fileVersion, expectedVersion);
            }

            // Find the help compilers by looking on all fixed drives.
            // We don't need them if the result is only a website.
            if((project.HelpFileFormat & ~HelpFileFormat.Website) != 0)
            {
                if((project.HelpFileFormat & HelpFileFormat.HtmlHelp1x) != 0)
                {
                    hhcFolder = project.HtmlHelp1xCompilerPath;

                    if(hhcFolder.Length == 0)
                        hhcFolder = BuildProcess.FindOnFixedDrives(
                            @"\HTML Help Workshop");

                    if(hhcFolder.Length == 0 || !Directory.Exists(hhcFolder))
                        throw new BuilderException("Could not find path to " +
                            "HTML 1.x help compiler");

                    if(!hhcFolder.EndsWith(@"\"))
                        hhcFolder += @"\";

                    this.ReportProgress("Found HTML 1.x help compiler in '{0}'",
                        hhcFolder);
                }

                if((project.HelpFileFormat & HelpFileFormat.HtmlHelp2x) != 0)
                {
                    hxcompFolder = project.HtmlHelp2xCompilerPath;

                    if(hxcompFolder.Length == 0)
                    {
                        // Search the VS 2005 SDK folders first as it usually
                        // has a more recent version.
                        hxcompFolder = BuildProcess.FindSdkExecutable(
                                "hxcomp.exe");

                        // If not found there, try the default installation
                        // folder.
                        if(hxcompFolder.Length == 0)
                            hxcompFolder = BuildProcess.FindOnFixedDrives(
                                @"\Microsoft Help 2.0 SDK");
                    }

                    if(hxcompFolder.Length == 0 ||
                      !Directory.Exists(hxcompFolder))
                        throw new BuilderException("Could not find path to " +
                            "HTML 2.x help compiler");

                    if(!hxcompFolder.EndsWith(@"\"))
                        hxcompFolder += @"\";

                    this.ReportProgress("Found HTML 2.x help compiler in '{0}'",
                        hxcompFolder);
                }
            }
        }

        /// <summary>
        /// Find a folder by searching the Program Files folders on all fixed
        /// drives.
        /// </summary>
        /// <param name="path">The path for which to search</param>
        /// <returns>The path if found or an empty string if not found</returns>
        protected internal static string FindOnFixedDrives(string path)
        {
            StringBuilder sb = new StringBuilder(Environment.GetFolderPath(
                Environment.SpecialFolder.ProgramFiles));

            // Check for 64-bit Windows.  The tools will be in the x86 folder.
            if(Directory.Exists(sb.ToString() + " (x86)"))
                sb.Append(" (x86)");

            sb.Append(path);

            foreach(DriveInfo di in DriveInfo.GetDrives())
                if(di.DriveType == DriveType.Fixed)
                {
                    sb[0] = di.Name[0];
                    if(Directory.Exists(sb.ToString()))
                        return sb.ToString();
                }

            return String.Empty;
        }

        /// <summary>
        /// This is used to find the named executable in one of the Visual
        /// Studio SDK installation folders.
        /// </summary>
        /// <param name="exeName">The name of the executable to find</param>
        /// <returns>The path if found or an empty string if not found</returns>
        /// <remarks>The search looks in all "Visual*" folders under the
        /// Program Files special folder on all fixed drives.</remarks>
        protected internal static string FindSdkExecutable(string exeName)
        {
            StringBuilder sb = new StringBuilder(Environment.GetFolderPath(
                Environment.SpecialFolder.ProgramFiles));
            string[] dirs, files;
            string folder;

            // Check for 64-bit Windows.  The tools will be in the x86 folder.
            if(Directory.Exists(sb.ToString() + " (x86)"))
                sb.Append(" (x86)");

            foreach(DriveInfo di in DriveInfo.GetDrives())
                if(di.DriveType == DriveType.Fixed)
                {
                    sb[0] = di.Name[0];
                    folder = sb.ToString();

                    if(!Directory.Exists(folder))
                        continue;

                    dirs = Directory.GetDirectories(folder, "Visual*");

                    foreach(string dir in dirs)
                    {
                        files = Directory.GetFiles(dir, exeName,
                            SearchOption.AllDirectories);

                        if(files.Length != 0)
                        {
                            // If more than one, sort them and take the
                            // last one as it should be the most recent.
                            if(files.Length > 1)
                                Array.Sort(files);

                            return Path.GetDirectoryName(files[files.Length - 1]);
                        }
                    }
                }

            return String.Empty;
        }

        /// <summary>
        /// Validate the documentation assembly information and copy the
        /// files to the working folder.
        /// </summary>
        /// <exception cref="BuilderException">This is thrown if any of
        /// the information is invalid.</exception>
        protected void ValidateAssemblies()
        {
            XmlCommentsFile comments;
            string workingPath;

            if(project.Assemblies.Count == 0)
                throw new BuilderException("You must specify at least one " +
                    "assembly to document");

            commentsFiles = new XmlCommentsFileCollection();

            foreach(DocumentAssembly da in project.Assemblies)
            {
                if(!da.CommentsOnly)
                {
                    if(!File.Exists(da.AssemblyPath))
                        throw new BuilderException("Unable to find assembly: " +
                            da.AssemblyPath);

                    // All attributes are turned off so that we can delete
                    // it later.
                    workingPath = workingFolder + Path.GetFileName(
                        da.AssemblyPath);
                    File.Copy(da.AssemblyPath, workingPath, true);
                    File.SetAttributes(workingPath, FileAttributes.Normal);
                }

                if(!File.Exists(da.XmlCommentsPath))
                    throw new BuilderException(
                        "Unable to find XML comments file: " +
                        da.XmlCommentsPath);

                workingPath = workingFolder + Path.GetFileName(
                    da.XmlCommentsPath);
                File.Copy(da.XmlCommentsPath, workingPath, true);
                File.SetAttributes(workingPath, FileAttributes.Normal);

                // Add the file to the XML comments file collection ready
                // for adding missing tag information.
                comments = new XmlCommentsFile(workingPath);

                // Fixup comments for CPP comments files?
                if(project.CppCommentsFixup)
                    comments.FixupComments();

                commentsFiles.Add(comments);
            }
        }

        /// <summary>
        /// This is called to create the DLL dependencies folder in the
        /// working folder and to copy the project's dependency items into it.
        /// </summary>
        /// <remarks>By copying the dependencies ourself, we do not have to
        /// make the user do it.  They can simply specify the locations of
        /// the dependencies.  It also allows resolving references to GAC
        /// assemblies and we also can reference a single location in one
        /// command line parameter and there is no risk of exceeding any
        /// command line length limitations with a large number of
        /// dependencies.</remarks>
        protected void CopyDependencies()
        {
            AssemblyLoader loader = null;
            string[] files;
            string sourcePath, dirName, filename, fileSpec,
                   depFolder = workingFolder + @"DLL\";
            int idx;

            Directory.CreateDirectory(depFolder);

            try
            {
                foreach(DependencyItem di in project.Dependencies)
                {
                    sourcePath = di.DependencyPath;

                    if(sourcePath.StartsWith("GAC:"))
                    {
                        // Resolve GAC reference
                        if(loader == null)
                            loader = AssemblyLoader.CreateAssemblyLoader(project);

                        sourcePath = loader.GetAssemblyLocation(
                            sourcePath.Substring(4));
                    }

                    if(sourcePath.IndexOfAny(new char[] { '*', '?' }) == -1)
                    {
                        filename = depFolder + Path.GetFileName(sourcePath);

                        // All attributes are turned off so that we can delete
                        // it later.
                        File.Copy(sourcePath, filename, true);
                        File.SetAttributes(filename, FileAttributes.Normal);

                        this.ReportProgress("{0} -> {1}", sourcePath, filename);
                    }
                    else
                    {
                        idx = sourcePath.LastIndexOf('\\');
                        dirName = sourcePath.Substring(0, idx);
                        fileSpec = sourcePath.Substring(idx + 1);

                        files = Directory.GetFiles(dirName, fileSpec);

                        foreach(string name in files)
                            if(name.EndsWith(".dll") || name.EndsWith(".exe"))
                            {
                                filename = depFolder + Path.GetFileName(name);

                                File.Copy(name, filename, true);
                                File.SetAttributes(filename, FileAttributes.Normal);

                                this.ReportProgress("{0} -> {1}", name, filename);
                            }
                    }
                }
            }
            finally
            {
                if(loader != null)
                    AssemblyLoader.ReleaseAssemblyLoader();
            }
        }

        /// <summary>
        /// This is used to run a step in the build process
        /// </summary>
        /// <param name="fileToRun">The file to execute.  This will be one of
        /// the template batch files with all the necessary values for the
        /// paths and options plugged into it.</param>
        /// <param name="args">The arguments to pass to the file if any.</param>
        protected void RunProcess(string fileToRun, string args)
        {
            int waitCount;

            if(fileToRun == null)
                throw new ArgumentNullException("fileToRun");

            currentProcess = new Process();
            errorDetected = false;

            ProcessStartInfo psi = currentProcess.StartInfo;

            // Set CreateNoWindow to true to suppress the window rather
            // than setting  WindowStyle to hidden as WindowStyle has no
            // effect on command prompt windows and they always appear.
            psi.CreateNoWindow = true;
            psi.FileName = fileToRun;
            psi.Arguments = args;
            psi.WorkingDirectory = workingFolder;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = psi.RedirectStandardError = true;

            this.ReportProgress(String.Format(CultureInfo.CurrentCulture,
                "[{0}]", fileToRun));
            currentProcess.Start();

            // Spawn two threads so that we can capture both STDOUT
            // and STDERR without the risk of a deadlock.
            stdOutThread = new Thread(ReadStdOut);
            stdOutThread.Start();

            stdErrThread = new Thread(ReadStdErr);
            stdErrThread.Start();

            currentProcess.WaitForExit();
            waitCount = 0;

            // Give the output threads a chance to finish
            while(waitCount < 5 && stdOutThread.IsAlive &&
              !stdOutThread.Join(1000))
                waitCount++;

            waitCount = 0;
            while(waitCount < 5 && stdErrThread.IsAlive &&
              !stdErrThread.Join(1000))
                waitCount++;

            currentProcess.Dispose();
            currentProcess = null;
            stdOutThread = stdErrThread = null;

            // Stop if an error was detected
            if(errorDetected)
                throw new BuilderException("Unexpected error in last " +
                    "build step.  See output above for details.");
        }

        /// <summary>
        /// This is the thread procedure used to capture standard ouput text
        /// </summary>
        private void ReadStdOut()
        {
            string line;

            try
            {
                do
                {
                    line = currentProcess.StandardOutput.ReadLine();

                    if(line != null)
                        this.ReportToolOutput(line);

                } while(line != null);
            }
            catch(ThreadAbortException)
            {
                System.Diagnostics.Debug.WriteLine("ReadStdOut thread aborted\r\n");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                System.Diagnostics.Debug.WriteLine("ReadStdOut thread failed\r\n");
            }

            System.Diagnostics.Debug.WriteLine("ReadStdOut thread stopped\r\n");
        }

        /// <summary>
        /// This is the thread procedure used to capture standard error text
        /// </summary>
        private void ReadStdErr()
        {
            string line;

            try
            {
                do
                {
                    line = currentProcess.StandardError.ReadLine();

                    if(line != null)
                        this.ReportToolOutput(line);

                } while(line != null);
            }
            catch(ThreadAbortException)
            {
                System.Diagnostics.Debug.WriteLine("ReadStdErr thread aborted\r\n");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                System.Diagnostics.Debug.WriteLine("ReadStdErr thread failed\r\n");
            }

            System.Diagnostics.Debug.WriteLine("ReadStdErr thread stopped\r\n");
        }

        /// <summary>
        /// Report the output from the currently running tool
        /// </summary>
        /// <param name="line">The line to report</param>
        private void ReportToolOutput(string line)
        {
            // The ReportProgress method uses String.Format so double
            // any braces in the output.
            if(line.IndexOf('{') != -1)
                line = line.Replace("{", "{{");

            if(line.IndexOf('}') != -1)
                line = line.Replace("}", "}}");

            // Check for errors
            if(reErrorCheck.IsMatch(line))
                errorDetected = true;

            this.ReportProgress(line);
        }

        /// <summary>
        /// Force garbage collection to reduce memory usage.
        /// </summary>
        /// <remarks>The reflection information file and XML comments files
        /// can be quite large.  To reduce memory usage, we force a garbage
        /// collection to get rid of all the discarded objects.</remarks>
        private void GarbageCollect()
        {
#if DEBUG
            this.ReportProgress("\r\n  GC: Memory used before: {0:0,0}",
                GC.GetTotalMemory(false));
#endif
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            GC.Collect(2);
#if DEBUG
            this.ReportProgress("  GC: Memory used after: {0:0,0}\r\n",
                GC.GetTotalMemory(false));
#endif
        }
    }
}
