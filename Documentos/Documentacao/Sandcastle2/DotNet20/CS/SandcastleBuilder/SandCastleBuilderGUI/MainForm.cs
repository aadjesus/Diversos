//=============================================================================
// System  : Sandcastle Help File Builder
// File    : MainForm.cs
// Author  : Eric Woodruff
// Updated : 02/17/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the main form for the application.
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
// 1.3.1.2  10/18/2006  EFW  Moved file logging to the BuildProcess class.
//                           Added a Verbose Logging user setting.
//=============================================================================

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using SandcastleBuilder.Utils;
using SandcastleBuilder.Utils.Controls;
using SandcastleBuilder.Utils.Design;

using SandcastleBuilder.Properties;

namespace SandcastleBuilder
{
    /// <summary>
    /// This is the main form for the application
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private SandcastleProject project;
        private Thread buildThread;
        private BuildProcess buildProcess;

        private string lastBuiltHelpFile, lastOutputLog;
        #endregion

        //=====================================================================
        // Methods, etc

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="projectToLoad">A default project file to load.  If
        /// not specified, the most recently used project is loaded if there
        /// is one.</param>
        public MainForm(string projectToLoad)
        {
            InitializeComponent();

            // Skip the rest of the initialization in design mode
            if(this.DesignMode)
                return;

            if(projectToLoad != null)
            {
                if(Settings.Default.MruList == null)
                    Settings.Default.MruList = new StringCollection();

                if(File.Exists(projectToLoad))
                    MainForm.UpdateMruList(projectToLoad);
                else
                    MessageBox.Show("Unable to find project: " + projectToLoad,
                        Constants.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }

            // Set the status label to use for status bar text
            StatusBarTextProvider.ApplicationStatusBar = tsslStatusText;

            // Define the status label and progress bar too.  This allows
            // easy access to those items from anywhere within the
            // application.
            StatusBarTextProvider.StatusLabel = tsslProgressNote;
            StatusBarTextProvider.ProgressBar = tspbProgressBar;

            // Start with a blank project
            project = new SandcastleProject();
            project.AssembliesChanged += new EventHandler(project_Modified);
            project.DirtyChanged += new EventHandler(project_Modified);

            lbAssemblies.DisplayMember = lbAssemblies.ValueMember =
                "AssemblyDescription";
            lbAssemblies.DataSource = project.Assemblies;

            pgProps.SelectedObject = project;

            this.UpdateFilenameInfo();
            txtOutput.Text = String.Format(CultureInfo.CurrentCulture,
                "[{0}, version {1}]", Application.ProductName,
                Application.ProductVersion);
        }

        #region Private helper methods
        //=====================================================================
        // Private helper methods

        /// <summary>
        /// This is used to update the filename information in the title bar
        /// and in the Content Item editor form.
        /// </summary>
        private void UpdateFilenameInfo()
        {
            this.Text = String.Format(CultureInfo.CurrentCulture,
                "{0}{1} - Sandcastle Help File Builder",
                Path.GetFileNameWithoutExtension(project.Filename),
                (project.IsDirty) ? "*" : "");

            if(ContentItemEditorDlg.ProjectPath != Path.GetDirectoryName(
              project.Filename))
                ContentItemEditorDlg.ProjectPath = Path.GetDirectoryName(
                    project.Filename);
        }

        /// <summary>
        /// This is called to ask the user if they want to save their project
        /// </summary>
        /// <returns>Returns true if saved successfully or no save is
        /// wanted.  Returns false on error or if cancelled.</returns>
        private bool AskToSaveProject()
        {
            DialogResult dr;
            bool result = true;

            if(project.IsDirty)
            {
                dr = MessageBox.Show("Do you want to save your changes to " +
                  "this project?  Click YES to save changes, NO to " +
                  "discard them, or CANCEL to stay here and make " +
                  "more changes.", Constants.AppName,
                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if(dr == DialogResult.Cancel)
                    result = false;
                else
                    if(dr == DialogResult.Yes)
                    {
                        miSaveProject_Click(this, EventArgs.Empty);

                        // Stay here if the save fails
                        result = !project.IsDirty;
                    }
            }

            return result;
        }

        /// <summary>
        /// Save the project file
        /// </summary>
        private void SaveProject()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                project.SaveProject();
                MainForm.UpdateMruList(project.Filename);
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
        /// This is used to enable or disable the UI elements for the
        /// build process
        /// </summary>
        /// <param name="enabled">True to enable the UI, false to disable
        /// it</param>
        private void SetUIEnabledState(bool enabled)
        {
            foreach(ToolStripMenuItem item in mnuMain.Items)
                foreach(ToolStripItem subItem in item.DropDownItems)
                    subItem.Enabled = enabled;

            foreach(ToolStripItem item in tsbMain.Items)
                item.Enabled = enabled;

            gbAssemblies.Enabled = pgProps.Enabled = enabled;

            // The Cancel Build options are the inverse of the value
            miCancelBuild.Enabled = tsbCancelBuild.Enabled = !enabled;

            // Exit is always enabled even when building
            miExit.Enabled = true;
        }

        /// <summary>
        /// Update the MRU list with the specified project filename by
        /// adding it to the list or making it the most recently used.
        /// </summary>
        private static void UpdateMruList(string projectFile)
        {
            StringCollection mruList = Settings.Default.MruList;

            projectFile = Path.GetFullPath(projectFile);

            if(mruList.Contains(projectFile))
                mruList.Remove(projectFile);

            mruList.Insert(0, projectFile);

            while(mruList.Count > 10)
                mruList.RemoveAt(9);
        }
        #endregion

        //=====================================================================
        // Event handlers

        /// <summary>
        /// This updates the state of the form when the project is modified
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        void project_Modified(object sender, EventArgs e)
        {
            this.UpdateFilenameInfo();

            btnEditAssembly.Enabled = btnRemoveAssembly.Enabled =
                (project.Assemblies.Count != 0);
        }

        /// <summary>
        /// Load the last used project on startup
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            StringCollection mruList = Settings.Default.MruList;
            int idx = 0;

            // Set the verbose logging option
            miVerboseLogging.Checked = Settings.Default.VerboseLogging;

            // Create the MRU on first use
            if(mruList == null)
                Settings.Default.MruList = new StringCollection();
            else
            {
                // Get rid of projects that no longer exist
                while(idx < mruList.Count)
                    if(!File.Exists(mruList[idx]))
                        mruList.RemoveAt(idx);
                    else
                        idx++;

                if(mruList.Count != 0)
                    project.LoadProject(mruList[0]);
            }
        }

        /// <summary>
        /// Save changes on exit if necessary
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(buildThread != null && buildThread.IsAlive)
            {
                if(MessageBox.Show("A build is currently taking place.  Do " +
                  "you want to abort it and exit?", Constants.AppName,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                  DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                miCancelBuild_Click(sender, e);
            }

            if(project.IsDirty && !this.AskToSaveProject())
                e.Cancel = project.IsDirty;

            Settings.Default.Save();
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Show information about the application and contact info
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miAbout_Click(object sender, EventArgs e)
        {
            using(AboutDlg dlg = new AboutDlg())
            {
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Start a new help project
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miNewProject_Click(object sender, EventArgs e)
        {
            if(this.AskToSaveProject())
            {
                project.ResetProject();
                lastBuiltHelpFile = lastOutputLog = txtOutput.Text = null;
            }

            pgProps.Refresh();
        }

        /// <summary>
        /// Open an existing help project
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miOpenProject_Click(object sender, EventArgs e)
        {
            if(this.AskToSaveProject())
            {
                using(OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Load Help Project";
                    dlg.Filter = "Help Project files (*.shfb)|*.shfb|" +
                        "All files (*.*)|*.*";
                    dlg.DefaultExt = "shfb";
                    dlg.FilterIndex = 1;
                    dlg.InitialDirectory = Directory.GetCurrentDirectory();

                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            lastBuiltHelpFile = lastOutputLog =
                                txtOutput.Text = null;
                            this.Cursor = Cursors.WaitCursor;
                            project.LoadProject(dlg.FileName);
                            MainForm.UpdateMruList(project.Filename);
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
                            pgProps.Refresh();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save all changes to the current help project
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miSaveProject_Click(object sender, EventArgs e)
        {
            // If it's a new project without a name, ask the
            // user to save it now.
            if(project.Filename == SandcastleProject.DefaultName)
                miSaveProjectAs_Click(sender, e);
            else
                this.SaveProject();
        }

        /// <summary>
        /// Save all changes to the current help project under a new name
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miSaveProjectAs_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Help Project As";
                dlg.Filter = "Help Project files (*.shfb)|*.shfb|" +
                    "All files (*.*)|*.*";
                dlg.DefaultExt = "shfb";
                dlg.FilterIndex = 1;
                dlg.InitialDirectory = Directory.GetCurrentDirectory();

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    project.Filename = dlg.FileName;
                    project.IsDirty = true;
                    this.SaveProject();
                    pgProps.Refresh();
                }
            }
        }

        /// <summary>
        /// Build the help file using the current project settings
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miBuildProject_Click(object sender, EventArgs e)
        {
            // If it's a new project without a name, ask the
            // user to save it now.  This is required as we need
            // a base folder for any relative paths in the project.
            if(project.Filename == SandcastleProject.DefaultName)
            {
                miSaveProjectAs_Click(sender, e);

                if(project.Filename == SandcastleProject.DefaultName)
                    return;
            }

            this.SetUIEnabledState(false);
            lastBuiltHelpFile = null;
            txtOutput.Text = String.Empty;

            buildProcess = new BuildProcess(project);
            buildProcess.BuildStepChanged +=
                new EventHandler<BuildProgressEventArgs>(
                    buildProcess_BuildStepChanged);
            buildProcess.BuildProgress +=
                new EventHandler<BuildProgressEventArgs>(
                    buildProcess_BuildProgress);

            StatusBarTextProvider.InitializeProgressBar(0,
                (int)BuildStep.Completed, "Building help file");

            buildThread = new Thread(new ThreadStart(
                buildProcess.Build));
            buildThread.Name = "Help file builder thread";
            buildThread.IsBackground = true;
            buildThread.Start();
        }

        /// <summary>
        /// This is called by the build process thread to update the main
        /// window with the current build step.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void buildProcess_BuildStepChanged(object sender,
          BuildProgressEventArgs e)
        {
            string outputPath;

            if(this.InvokeRequired)
            {
                // Ignore it if we've already shut down or it hasn't
                // completed yet.
                if(!this.IsDisposed)
                    this.Invoke(new EventHandler<BuildProgressEventArgs>(
                        buildProcess_BuildStepChanged),
                        new object[] { sender, e });
            }
            else
            {
                if(!Settings.Default.VerboseLogging)
                {
                    txtOutput.AppendText(e.BuildStep.ToString());
                    txtOutput.AppendText("\r\n");
                }

                if(e.HasCompleted)
                {
                    StatusBarTextProvider.ResetProgressBar();
                    this.SetUIEnabledState(true);

                    // Save the output log filename and help filename on
                    // completion if possible
                    outputPath = buildProcess.OutputFolder;

                    if(Directory.Exists(outputPath))
                        lastOutputLog = buildProcess.LogFilename;
                    else
                        lastOutputLog = null;

                    // Store the help file filename so that it can be viewed
                    // if it exists.
                    if(e.BuildStep == BuildStep.Completed)
                    {
                        switch(project.HelpFileFormat)
                        {
                            case HelpFileFormat.HtmlHelp1x:
                            case HelpFileFormat.Help1xAndHelp2x:
                                outputPath += project.HtmlHelpName + ".chm";
                                break;

                            case HelpFileFormat.HtmlHelp2x:
                                outputPath += project.HtmlHelpName + ".hxs";
                                break;

                            default:
                                outputPath += "Index.html";
                                break;
                        }

                        if(File.Exists(outputPath))
                            lastBuiltHelpFile = outputPath;
                    }

                    buildThread = null;
                    buildProcess = null;
                }
            }
        }

        /// <summary>
        /// This is called by the build process thread to update the main
        /// window with information about its progress.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void buildProcess_BuildProgress(object sender,
          BuildProgressEventArgs e)
        {
            if(this.InvokeRequired)
            {
                // Ignore it if we've already shut down
                if(!this.IsDisposed)
                    this.Invoke(new EventHandler<BuildProgressEventArgs>(
                        buildProcess_BuildProgress),
                        new object[] { sender, e });
            }
            else
            {
                if(e.BuildStep < BuildStep.Completed)
                    StatusBarTextProvider.UpdateProgress((int)e.BuildStep);

                if(Settings.Default.VerboseLogging ||
                  e.BuildStep == BuildStep.Failed)
                {
                    txtOutput.AppendText(e.Message);
                    txtOutput.AppendText("\r\n");
                }
            }
        }

        /// <summary>
        /// Cancel the current build process
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miCancelBuild_Click(object sender, EventArgs e)
        {
            if(buildThread != null && buildThread.IsAlive)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    StatusBarTextProvider.UpdateProgress("Cancelling build...");
                    buildThread.Abort();

                    while(buildThread != null && !buildThread.Join(1000))
                        Application.DoEvents();

                    StatusBarTextProvider.ResetProgressBar();
                    System.Diagnostics.Debug.WriteLine("Thread stopped");
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    buildThread = null;
                    buildProcess = null;
                }
            }

            this.SetUIEnabledState(true);
        }

        /// <summary>
        /// View the help file produced by the last build
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miViewHelpFile_Click(object sender, EventArgs e)
        {
            if(lastBuiltHelpFile == null)
            {
                MessageBox.Show("The help file needs to be built before viewing it",
                    Constants.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if(lastBuiltHelpFile.EndsWith(".hxs"))
            {
                MessageBox.Show("HTML Help 2.0 files must be registered in " +
                    "a collection to be viewed or you can use a standalone " +
                    "viewer", Constants.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(lastBuiltHelpFile);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show(String.Format(CultureInfo.CurrentCulture,
                    "Unable to open help file '{0}'\r\nReason: {1}",
                    lastBuiltHelpFile, ex.Message), Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Add one or more documentation assemblies
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        /// <remarks>This will assume that the matching file (assembly or
        /// comments file) has the same name as the selected file.  If a
        /// match is not found, the missing file's name is set to "Unknown"
        /// and the Edit option can be used to fix it.</remarks>
        private void btnAddAssembly_Click(object sender, EventArgs e)
        {
            DocumentAssembly newItem;

            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Title = "Select the file(s) to document";
                dlg.Filter = "Library, Executable, and Comment Files " +
                    "(*.dll, *.exe, *.xml)|*.dll;*.exe;*.xml|" +
                    "Library Files (*.dll)|*.dll|" +
                    "Executable Files (*.exe)|*.exe|" +
                    "XML Comment Files (*.xml)|*.xml|" +
                    "All Files (*.*)|*.*";
                dlg.InitialDirectory = Directory.GetCurrentDirectory();

                // If selected, add the new file(s)
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach(string file in dlg.FileNames)
                    {
                        newItem = new DocumentAssembly();

                        if(file.ToLower(
                          CultureInfo.InvariantCulture).EndsWith(".xml"))
                        {
                            newItem.XmlCommentsPath = new FilePath(file);
                            if(newItem.AssemblyPath.Path.Length == 0)
                                newItem.AssemblyPath = new FilePath(
                                    "Unknown.dll");
                        }
                        else
                        {
                            newItem.AssemblyPath = new FilePath(file);
                            if(newItem.XmlCommentsPath.Path.Length == 0)
                                newItem.XmlCommentsPath = new FilePath(
                                    "Unknown.xml");
                        }

                        if(!project.Assemblies.Contains(newItem))
                            project.Assemblies.Add(newItem);
                    }
                }
            }
        }

        /// <summary>
        /// Edit the information for the selected documentation assembly
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEditAssembly_Click(object sender, EventArgs e)
        {
            using(SelectAssemblyDlg dlg = new SelectAssemblyDlg())
            {
                if(lbAssemblies.SelectedIndex == -1)
                    lbAssemblies.SelectedIndex = 0;

                dlg.AssemblyInfo = project.Assemblies[lbAssemblies.SelectedIndex];

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    project.Assemblies[lbAssemblies.SelectedIndex] =
                        dlg.AssemblyInfo;

                    lbAssemblies.DataSource = null;
                    lbAssemblies.DisplayMember = lbAssemblies.ValueMember =
                        "AssemblyDescription";
                    lbAssemblies.DataSource = project.Assemblies;
                    project.IsDirty = true;
                }
            }
        }

        /// <summary>
        /// Remove the selected documentation assembly from the project
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnRemoveAssembly_Click(object sender, EventArgs e)
        {
            if(lbAssemblies.SelectedIndex == -1)
                lbAssemblies.SelectedIndex = 0;

            if(MessageBox.Show("Are you sure you want to remove '" +
              project.Assemblies[lbAssemblies.SelectedIndex].AssemblyDescription +
              "' from the project?", Constants.AppName, MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
                project.Assemblies.RemoveAt(lbAssemblies.SelectedIndex);
        }

        /// <summary>
        /// Set the project state to dirty when a property changes.  This
        /// also updates the Sandcastle path in the component configuration
        /// form.
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if(ComponentConfigurationEditorDlg.SandcastlePath !=
              project.SandcastlePath)
                ComponentConfigurationEditorDlg.SandcastlePath =
                    project.SandcastlePath;

            project.IsDirty = true;
        }

        /// <summary>
        /// View the output from the last build in a window so that it's
        /// easier to review.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miViewLog_Click(object sender, EventArgs e)
        {
            if(lastOutputLog == null)
                MessageBox.Show("There is no log file to view.  Please " +
                    "build the project first.", Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                using(ViewLogDlg dlg = new ViewLogDlg(lastOutputLog))
                {
                    dlg.ShowDialog();
                }
        }

        /// <summary>
        /// Build the MRU list when the Recent Projects menu opens
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miRecentProjects_DropDownOpening(object sender, EventArgs e)
        {
            StringCollection mruList = Settings.Default.MruList;
            ToolStripMenuItem miProject;
            int idx = 1;

            miRecentProjects.DropDownItems.Clear();

            if(mruList.Count == 0)
            {
                miProject = new ToolStripMenuItem("(Empty)");
                miProject.Enabled = false;
                miRecentProjects.DropDownItems.Add(miProject);
            }
            else
                foreach(string project in mruList)
                {
                    miProject = new ToolStripMenuItem();
                    miProject.Text = String.Format(CultureInfo.CurrentCulture,
                        "&{0} {1}", idx, project);
                    miProject.Click += new EventHandler(miProject_Click);
                    miRecentProjects.DropDownItems.Add(miProject);
                }
        }

        /// <summary>
        /// This is used to load a project from the MRU List
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        void miProject_Click(object sender, EventArgs e)
        {
            StringCollection mruList = Settings.Default.MruList;

            if(this.AskToSaveProject())
            {
                try
                {
                    lastBuiltHelpFile = lastOutputLog = txtOutput.Text = null;

                    this.Cursor = Cursors.WaitCursor;
                    project.LoadProject(mruList[
                        miRecentProjects.DropDownItems.IndexOf(
                        (ToolStripMenuItem)sender)]);

                    MainForm.UpdateMruList(project.Filename);
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
                    pgProps.Refresh();
                }
            }
        }

        /// <summary>
        /// Create a new Sandcastle Help File Builder Project from an NDoc
        /// project.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miNewFromNDoc_Click(object sender, EventArgs e)
        {
            if(this.AskToSaveProject())
            {
                using(OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Select NDoc Project";
                    dlg.Filter = "NDoc Project files (*.ndoc)|*.ndoc|" +
                        "All files (*.*)|*.*";
                    dlg.DefaultExt = "ndoc";
                    dlg.FilterIndex = 1;
                    dlg.InitialDirectory = Directory.GetCurrentDirectory();

                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            project.ResetProject();
                            lastBuiltHelpFile = lastOutputLog =
                                txtOutput.Text = null;
                            this.ImportFromNDoc(dlg.FileName);
                            this.UpdateFilenameInfo();
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
                            pgProps.Refresh();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create a new project from a Visual Studio solution or project
        /// file.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miNewFromVS_Click(object sender, EventArgs e)
        {
            if(this.AskToSaveProject())
            {
                using(OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Select Visual Studio Solution or Project";
                    dlg.Filter = "Visual Studio solution files (*.sln)|*.sln|" +
                        "Visual Studio project files (*.*proj)|*.*proj|" +
                        "All files (*.*)|*.*";
                    dlg.InitialDirectory = Directory.GetCurrentDirectory();

                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            project.ResetProject();
                            lastBuiltHelpFile = lastOutputLog =
                                txtOutput.Text = null;
                            this.ImportFromVisualStudio(dlg.FileName);
                            this.UpdateFilenameInfo();
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
                            pgProps.Refresh();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Edit the namespace summaries and indicate which ones should appear
        /// in the help file.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnNSSummaries_Click(object sender, EventArgs e)
        {
            // If it's a new project without a name, ask the
            // user to save it now.  This is required as we need
            // a base folder for any relative paths in the project.
            if(project.Filename == SandcastleProject.DefaultName)
            {
                miSaveProjectAs_Click(sender, e);

                if(project.Filename == SandcastleProject.DefaultName)
                    return;
            }

            using(NamespacesDlg dlg = new NamespacesDlg(project))
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                    project.IsDirty = true;
            }
        }

        /// <summary>
        /// Add project summary notes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        /// <remarks>These comments will appear on the root namespaces
        /// page if entered.</remarks>
        private void btnProjectSummary_Click(object sender, EventArgs e)
        {
            using(ProjectSummaryDlg dlg = new ProjectSummaryDlg())
            {
                dlg.Summary = project.ProjectSummary;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    project.ProjectSummary = dlg.Summary;
                    project.IsDirty = true;
                }
            }
        }

        /// <summary>
        /// View the help file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miHelp_Click(object sender, EventArgs e)
        {
            string topic, path = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            try
            {
#if DEBUG
                path += @"\..\..\..\Doc\Help\SandcastleBuilder.chm";
#else
                path += @"\SandcastleBuilder.chm";
#endif

                if(sender == miHelp || sender == tsbAbout)
                    topic = "Welcome.html";
                else
                    topic = "FAQ.html";

                Help.ShowHelp(this, path, HelpNavigator.Topic, topic);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show(String.Format(CultureInfo.CurrentCulture,
                    "Unable to open help file '{0}'.  Reason: {1}",
                    path, ex.Message), Constants.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Clear the last build information from the output window
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miClearOutput_Click(object sender, EventArgs e)
        {
            txtOutput.Text = null;
        }

        /// <summary>
        /// Clean the output and working folders by deleting all files from
        /// them.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miCleanOutput_Click(object sender, EventArgs e)
        {
            // Make sure we start out in the project's output folder
            // in case the output folder is relative to it.
            Directory.SetCurrentDirectory(Path.GetDirectoryName(
                Path.GetFullPath(project.Filename)));

            string outputFolder = Path.GetFullPath(project.OutputPath);

            // If it's a new project without a name, or if the folder does
            // not exist, don't bother.
            if(Directory.Exists(outputFolder) &&
              project.Filename != SandcastleProject.DefaultName &&
              MessageBox.Show(String.Format(CultureInfo.CurrentCulture,
                  "This will delete all files from the output folder '{0}' " +
                  "including any not created by the help file builder.  " +
                  "Do you want to continue?", outputFolder), Constants.AppName,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    lastBuiltHelpFile = lastOutputLog = txtOutput.Text = null;

                    if(project.WorkingPath.Path.Length != 0 &&
                      Directory.Exists(project.WorkingPath))
                        Directory.Delete(project.WorkingPath, true);

                    Directory.Delete(outputFolder, true);
                    Directory.CreateDirectory(outputFolder);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    MessageBox.Show("Unable to clean output folder.  Reason:" +
                        ex.Message, Constants.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Enable or disable verbose logging in the output window
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miVerboseLogging_Click(object sender, EventArgs e)
        {
            miVerboseLogging.Checked = Settings.Default.VerboseLogging =
                !miVerboseLogging.Checked;
        }
    }
}
