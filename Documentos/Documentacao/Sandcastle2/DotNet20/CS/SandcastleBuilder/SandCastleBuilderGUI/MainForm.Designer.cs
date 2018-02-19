namespace SandcastleBuilder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sbStatusBarText = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.miProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewFromNDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewFromVS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.miSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.miRecentProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miFaq = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.miDocumentation = new System.Windows.Forms.ToolStripMenuItem();
            this.miBuildProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miCancelBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.miViewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.miViewHelpFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.miCleanOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.miVerboseLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbNewProject = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenProject = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveProject = new System.Windows.Forms.ToolStripButton();
            this.tsbBuildProject = new System.Windows.Forms.ToolStripButton();
            this.tsbViewHelpFile = new System.Windows.Forms.ToolStripButton();
            this.lbAssemblies = new System.Windows.Forms.ListBox();
            this.btnAddAssembly = new System.Windows.Forms.Button();
            this.btnEditAssembly = new System.Windows.Forms.Button();
            this.btnRemoveAssembly = new System.Windows.Forms.Button();
            this.pgProps = new SandcastleBuilder.Utils.Controls.CustomPropertyGrid();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.tsbCancelBuild = new System.Windows.Forms.ToolStripButton();
            this.tsbViewLog = new System.Windows.Forms.ToolStripButton();
            this.btnNSSummaries = new System.Windows.Forms.Button();
            this.btnProjectSummary = new System.Windows.Forms.Button();
            this.tsbFaq = new System.Windows.Forms.ToolStripButton();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsbMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslProgressNote = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.gbAssemblies = new SandcastleBuilder.Utils.Controls.DividerGroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbProperties = new SandcastleBuilder.Utils.Controls.DividerGroupBox();
            this.dividerLabel1 = new SandcastleBuilder.Utils.Controls.DividerLabel();
            this.mnuMain.SuspendLayout();
            this.tsbMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbAssemblies.SuspendLayout();
            this.gbProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // miProject
            // 
            this.miProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewProject,
            this.miNewFromNDoc,
            this.miNewFromVS,
            this.toolStripSeparator2,
            this.miOpenProject,
            this.toolStripSeparator7,
            this.miSaveProject,
            this.miSaveProjectAs,
            this.toolStripSeparator6,
            this.miRecentProjects,
            this.toolStripSeparator1,
            this.miExit});
            this.miProject.Name = "miProject";
            this.miProject.Size = new System.Drawing.Size(64, 21);
            this.sbStatusBarText.SetStatusBarText(this.miProject, "File related options");
            this.miProject.Text = "&Project";
            // 
            // miNewProject
            // 
            this.miNewProject.Image = global::SandcastleBuilder.Properties.Resources.NewProject;
            this.miNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miNewProject.Name = "miNewProject";
            this.miNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miNewProject.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miNewProject, "Start a brand new project");
            this.miNewProject.Text = "&New Project";
            this.miNewProject.Click += new System.EventHandler(this.miNewProject_Click);
            // 
            // miNewFromNDoc
            // 
            this.miNewFromNDoc.Name = "miNewFromNDoc";
            this.miNewFromNDoc.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miNewFromNDoc, "Create a new Sandcastle Help File Builder project from an existing NDoc project");
            this.miNewFromNDoc.Text = "New Project from NDo&c Project...";
            this.miNewFromNDoc.Click += new System.EventHandler(this.miNewFromNDoc_Click);
            // 
            // miNewFromVS
            // 
            this.miNewFromVS.Name = "miNewFromVS";
            this.miNewFromVS.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miNewFromVS, "Create a new Sandcastle Help File Builder project from an existing Visual Studio " +
                    "solution or project file");
            this.miNewFromVS.Text = "New Project from &Visual Studio Project...";
            this.miNewFromVS.Click += new System.EventHandler(this.miNewFromVS_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(330, 6);
            // 
            // miOpenProject
            // 
            this.miOpenProject.Image = global::SandcastleBuilder.Properties.Resources.OpenProject;
            this.miOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miOpenProject.Name = "miOpenProject";
            this.miOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpenProject.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miOpenProject, "Open an existing project");
            this.miOpenProject.Text = "&Open Project...";
            this.miOpenProject.Click += new System.EventHandler(this.miOpenProject_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(330, 6);
            // 
            // miSaveProject
            // 
            this.miSaveProject.Image = global::SandcastleBuilder.Properties.Resources.SaveProject;
            this.miSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miSaveProject.Name = "miSaveProject";
            this.miSaveProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSaveProject.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miSaveProject, "Save all changes to the current project");
            this.miSaveProject.Text = "&Save Project";
            this.miSaveProject.Click += new System.EventHandler(this.miSaveProject_Click);
            // 
            // miSaveProjectAs
            // 
            this.miSaveProjectAs.Name = "miSaveProjectAs";
            this.miSaveProjectAs.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miSaveProjectAs, "Save the current project under a new name");
            this.miSaveProjectAs.Text = "Save Project &As...";
            this.miSaveProjectAs.Click += new System.EventHandler(this.miSaveProjectAs_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(330, 6);
            // 
            // miRecentProjects
            // 
            this.miRecentProjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.miRecentProjects.Name = "miRecentProjects";
            this.miRecentProjects.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miRecentProjects, "Select a recent project to load");
            this.miRecentProjects.Text = "&Recent Projects";
            this.miRecentProjects.DropDownOpening += new System.EventHandler(this.miRecentProjects_DropDownOpening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem1.Text = "(Empty)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(330, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(333, 22);
            this.sbStatusBarText.SetStatusBarText(this.miExit, "Exit this application");
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelp,
            this.miFaq,
            this.toolStripSeparator8,
            this.miAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 21);
            this.sbStatusBarText.SetStatusBarText(this.helpToolStripMenuItem, "Help related options");
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // miHelp
            // 
            this.miHelp.Name = "miHelp";
            this.miHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.miHelp.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miHelp, "View the help file");
            this.miHelp.Text = "&Help";
            this.miHelp.Click += new System.EventHandler(this.miHelp_Click);
            // 
            // miFaq
            // 
            this.miFaq.Image = global::SandcastleBuilder.Properties.Resources.FAQ;
            this.miFaq.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miFaq.Name = "miFaq";
            this.miFaq.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miFaq, "View frequently asked questions");
            this.miFaq.Text = "&Frequently Asked Questions";
            this.miFaq.Click += new System.EventHandler(this.miHelp_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(307, 6);
            // 
            // miAbout
            // 
            this.miAbout.Image = global::SandcastleBuilder.Properties.Resources.About;
            this.miAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miAbout, "View application, system, and contact information");
            this.miAbout.Text = "&About Sandcastle Help File Builder...";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.AutoSize = false;
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = global::SandcastleBuilder.Properties.Resources.About;
            this.tsbAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 22);
            this.sbStatusBarText.SetStatusBarText(this.tsbAbout, "View application, system, and contact information");
            this.tsbAbout.ToolTipText = "View application, system, and contact information";
            this.tsbAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miDocumentation
            // 
            this.miDocumentation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBuildProject,
            this.miCancelBuild,
            this.miViewLog,
            this.miClearOutput,
            this.toolStripSeparator5,
            this.miViewHelpFile,
            this.toolStripSeparator9,
            this.miCleanOutput,
            this.toolStripSeparator10,
            this.miVerboseLogging});
            this.miDocumentation.Name = "miDocumentation";
            this.miDocumentation.Size = new System.Drawing.Size(115, 21);
            this.sbStatusBarText.SetStatusBarText(this.miDocumentation, "Documentation related options");
            this.miDocumentation.Text = "&Documentation";
            // 
            // miBuildProject
            // 
            this.miBuildProject.Image = global::SandcastleBuilder.Properties.Resources.BuildProject;
            this.miBuildProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miBuildProject.Name = "miBuildProject";
            this.miBuildProject.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.B)));
            this.miBuildProject.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miBuildProject, "Build the current project to produce a help file");
            this.miBuildProject.Text = "&Build Project";
            this.miBuildProject.Click += new System.EventHandler(this.miBuildProject_Click);
            // 
            // miCancelBuild
            // 
            this.miCancelBuild.Enabled = false;
            this.miCancelBuild.Image = global::SandcastleBuilder.Properties.Resources.CancelBuild;
            this.miCancelBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miCancelBuild.Name = "miCancelBuild";
            this.miCancelBuild.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.miCancelBuild.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miCancelBuild, "Cancel the current build process");
            this.miCancelBuild.Text = "&Cancel Build";
            this.miCancelBuild.Click += new System.EventHandler(this.miCancelBuild_Click);
            // 
            // miViewLog
            // 
            this.miViewLog.Image = global::SandcastleBuilder.Properties.Resources.ViewLog;
            this.miViewLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miViewLog.Name = "miViewLog";
            this.miViewLog.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.W)));
            this.miViewLog.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miViewLog, "View the last build\'s output in a window");
            this.miViewLog.Text = "View &Output in Window";
            this.miViewLog.Click += new System.EventHandler(this.miViewLog_Click);
            // 
            // miClearOutput
            // 
            this.miClearOutput.Name = "miClearOutput";
            this.miClearOutput.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.X)));
            this.miClearOutput.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miClearOutput, "Clear the last build information from the output window");
            this.miClearOutput.Text = "C&lear Output Window";
            this.miClearOutput.Click += new System.EventHandler(this.miClearOutput_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(307, 6);
            // 
            // miViewHelpFile
            // 
            this.miViewHelpFile.Image = global::SandcastleBuilder.Properties.Resources.ViewHelpFile;
            this.miViewHelpFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miViewHelpFile.Name = "miViewHelpFile";
            this.miViewHelpFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.V)));
            this.miViewHelpFile.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miViewHelpFile, "View the help file produced by the last build");
            this.miViewHelpFile.Text = "&View Help File";
            this.miViewHelpFile.Click += new System.EventHandler(this.miViewHelpFile_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(307, 6);
            // 
            // miCleanOutput
            // 
            this.miCleanOutput.Name = "miCleanOutput";
            this.miCleanOutput.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miCleanOutput, "Clean the output folder by deleting all files in it");
            this.miCleanOutput.Text = "Clea&n Output Folder";
            this.miCleanOutput.Click += new System.EventHandler(this.miCleanOutput_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(307, 6);
            // 
            // miVerboseLogging
            // 
            this.miVerboseLogging.Name = "miVerboseLogging";
            this.miVerboseLogging.Size = new System.Drawing.Size(310, 22);
            this.sbStatusBarText.SetStatusBarText(this.miVerboseLogging, "Enable or disable verbose logging in the output window");
            this.miVerboseLogging.Text = "Verbose Logging";
            this.miVerboseLogging.Click += new System.EventHandler(this.miVerboseLogging_Click);
            // 
            // tsbNewProject
            // 
            this.tsbNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewProject.Image = global::SandcastleBuilder.Properties.Resources.NewProject;
            this.tsbNewProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewProject.Name = "tsbNewProject";
            this.tsbNewProject.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbNewProject, "Start a brand new project");
            this.tsbNewProject.ToolTipText = "Start a brand new project";
            this.tsbNewProject.Click += new System.EventHandler(this.miNewProject_Click);
            // 
            // tsbOpenProject
            // 
            this.tsbOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenProject.Image = global::SandcastleBuilder.Properties.Resources.OpenProject;
            this.tsbOpenProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenProject.Name = "tsbOpenProject";
            this.tsbOpenProject.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbOpenProject, "Open an existing project");
            this.tsbOpenProject.ToolTipText = "Open an existing project";
            this.tsbOpenProject.Click += new System.EventHandler(this.miOpenProject_Click);
            // 
            // tsbSaveProject
            // 
            this.tsbSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveProject.Image = global::SandcastleBuilder.Properties.Resources.SaveProject;
            this.tsbSaveProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveProject.Name = "tsbSaveProject";
            this.tsbSaveProject.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbSaveProject, "Save all changes to the current project");
            this.tsbSaveProject.ToolTipText = "Save the current project";
            this.tsbSaveProject.Click += new System.EventHandler(this.miSaveProject_Click);
            // 
            // tsbBuildProject
            // 
            this.tsbBuildProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuildProject.Image = global::SandcastleBuilder.Properties.Resources.BuildProject;
            this.tsbBuildProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbBuildProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuildProject.Name = "tsbBuildProject";
            this.tsbBuildProject.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbBuildProject, "Build the current project to produce a help file");
            this.tsbBuildProject.ToolTipText = "Build the help file";
            this.tsbBuildProject.Click += new System.EventHandler(this.miBuildProject_Click);
            // 
            // tsbViewHelpFile
            // 
            this.tsbViewHelpFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbViewHelpFile.Image = global::SandcastleBuilder.Properties.Resources.ViewHelpFile;
            this.tsbViewHelpFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbViewHelpFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewHelpFile.Name = "tsbViewHelpFile";
            this.tsbViewHelpFile.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbViewHelpFile, "View the help file produced by the last build");
            this.tsbViewHelpFile.ToolTipText = "View help file from last build";
            this.tsbViewHelpFile.Click += new System.EventHandler(this.miViewHelpFile_Click);
            // 
            // lbAssemblies
            // 
            this.lbAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAssemblies.ItemHeight = 16;
            this.lbAssemblies.Location = new System.Drawing.Point(0, 21);
            this.lbAssemblies.Name = "lbAssemblies";
            this.lbAssemblies.Size = new System.Drawing.Size(706, 84);
            this.sbStatusBarText.SetStatusBarText(this.lbAssemblies, "The assemblies to document");
            this.lbAssemblies.TabIndex = 0;
            // 
            // btnAddAssembly
            // 
            this.btnAddAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAssembly.Location = new System.Drawing.Point(712, 21);
            this.btnAddAssembly.Name = "btnAddAssembly";
            this.btnAddAssembly.Size = new System.Drawing.Size(88, 32);
            this.sbStatusBarText.SetStatusBarText(this.btnAddAssembly, "Add: Add one or more assemblies to document");
            this.btnAddAssembly.TabIndex = 1;
            this.btnAddAssembly.Text = "&Add";
            this.toolTip1.SetToolTip(this.btnAddAssembly, "Add one or more assemblies to document");
            this.btnAddAssembly.UseVisualStyleBackColor = true;
            this.btnAddAssembly.Click += new System.EventHandler(this.btnAddAssembly_Click);
            // 
            // btnEditAssembly
            // 
            this.btnEditAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditAssembly.Enabled = false;
            this.btnEditAssembly.Location = new System.Drawing.Point(806, 21);
            this.btnEditAssembly.Name = "btnEditAssembly";
            this.btnEditAssembly.Size = new System.Drawing.Size(88, 32);
            this.sbStatusBarText.SetStatusBarText(this.btnEditAssembly, "Edit: Edit the selected assembly to document");
            this.btnEditAssembly.TabIndex = 2;
            this.btnEditAssembly.Text = "&Edit";
            this.toolTip1.SetToolTip(this.btnEditAssembly, "Edit the selected assembly to document");
            this.btnEditAssembly.UseVisualStyleBackColor = true;
            this.btnEditAssembly.Click += new System.EventHandler(this.btnEditAssembly_Click);
            // 
            // btnRemoveAssembly
            // 
            this.btnRemoveAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAssembly.Enabled = false;
            this.btnRemoveAssembly.Location = new System.Drawing.Point(898, 21);
            this.btnRemoveAssembly.Name = "btnRemoveAssembly";
            this.btnRemoveAssembly.Size = new System.Drawing.Size(88, 32);
            this.sbStatusBarText.SetStatusBarText(this.btnRemoveAssembly, "Remove: Remove the selected assembly to document");
            this.btnRemoveAssembly.TabIndex = 3;
            this.btnRemoveAssembly.Text = "&Remove";
            this.toolTip1.SetToolTip(this.btnRemoveAssembly, "Remove the selected assembly to document");
            this.btnRemoveAssembly.UseVisualStyleBackColor = true;
            this.btnRemoveAssembly.Click += new System.EventHandler(this.btnRemoveAssembly_Click);
            // 
            // pgProps
            // 
            this.pgProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProps.Location = new System.Drawing.Point(3, 18);
            this.pgProps.Name = "pgProps";
            this.pgProps.PropertyNamePaneWidth = 300;
            this.pgProps.Size = new System.Drawing.Size(986, 385);
            this.sbStatusBarText.SetStatusBarText(this.pgProps, "Project properties");
            this.pgProps.TabIndex = 0;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.DetectUrls = false;
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.HideSelection = false;
            this.txtOutput.Location = new System.Drawing.Point(12, 602);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(992, 108);
            this.sbStatusBarText.SetStatusBarText(this.txtOutput, "Project build results");
            this.txtOutput.TabIndex = 5;
            this.txtOutput.Text = "";
            this.txtOutput.WordWrap = false;
            // 
            // tsbCancelBuild
            // 
            this.tsbCancelBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelBuild.Enabled = false;
            this.tsbCancelBuild.Image = global::SandcastleBuilder.Properties.Resources.CancelBuild;
            this.tsbCancelBuild.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCancelBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelBuild.Name = "tsbCancelBuild";
            this.tsbCancelBuild.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbCancelBuild, "Cancel the current build process");
            this.tsbCancelBuild.ToolTipText = "Cancel the current build process";
            this.tsbCancelBuild.Click += new System.EventHandler(this.miCancelBuild_Click);
            // 
            // tsbViewLog
            // 
            this.tsbViewLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbViewLog.Image = global::SandcastleBuilder.Properties.Resources.ViewLog;
            this.tsbViewLog.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbViewLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewLog.Name = "tsbViewLog";
            this.tsbViewLog.Size = new System.Drawing.Size(23, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbViewLog, "View output log in separate window");
            this.tsbViewLog.ToolTipText = "View output log in a separate window";
            this.tsbViewLog.Click += new System.EventHandler(this.miViewLog_Click);
            // 
            // btnNSSummaries
            // 
            this.btnNSSummaries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNSSummaries.Location = new System.Drawing.Point(853, 73);
            this.btnNSSummaries.Name = "btnNSSummaries";
            this.btnNSSummaries.Size = new System.Drawing.Size(133, 32);
            this.sbStatusBarText.SetStatusBarText(this.btnNSSummaries, "Namespaces: Edit namespace summaries");
            this.btnNSSummaries.TabIndex = 5;
            this.btnNSSummaries.Text = "&Namespaces";
            this.toolTip1.SetToolTip(this.btnNSSummaries, "Edit namespace summaries");
            this.btnNSSummaries.UseVisualStyleBackColor = true;
            this.btnNSSummaries.Click += new System.EventHandler(this.btnNSSummaries_Click);
            // 
            // btnProjectSummary
            // 
            this.btnProjectSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProjectSummary.Location = new System.Drawing.Point(712, 73);
            this.btnProjectSummary.Name = "btnProjectSummary";
            this.btnProjectSummary.Size = new System.Drawing.Size(133, 32);
            this.sbStatusBarText.SetStatusBarText(this.btnProjectSummary, "Project Summary: Enter project summary notes that appear on the root namespace pa" +
                    "ge");
            this.btnProjectSummary.TabIndex = 4;
            this.btnProjectSummary.Text = "Prj &Summary";
            this.toolTip1.SetToolTip(this.btnProjectSummary, "Enter project summary");
            this.btnProjectSummary.UseVisualStyleBackColor = true;
            this.btnProjectSummary.Click += new System.EventHandler(this.btnProjectSummary_Click);
            // 
            // tsbFaq
            // 
            this.tsbFaq.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFaq.Image = global::SandcastleBuilder.Properties.Resources.FAQ;
            this.tsbFaq.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFaq.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFaq.Name = "tsbFaq";
            this.tsbFaq.Size = new System.Drawing.Size(24, 29);
            this.sbStatusBarText.SetStatusBarText(this.tsbFaq, "View frequently asked questions");
            this.tsbFaq.ToolTipText = "View frequently asked questions";
            this.tsbFaq.Click += new System.EventHandler(this.miHelp_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miProject,
            this.miDocumentation,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1016, 25);
            this.mnuMain.TabIndex = 0;
            // 
            // tsbMain
            // 
            this.tsbMain.AutoSize = false;
            this.tsbMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.tsbMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewProject,
            this.tsbOpenProject,
            this.tsbSaveProject,
            this.toolStripSeparator3,
            this.tsbBuildProject,
            this.tsbCancelBuild,
            this.tsbViewLog,
            this.tsbViewHelpFile,
            this.toolStripSeparator4,
            this.tsbFaq,
            this.tsbAbout});
            this.tsbMain.Location = new System.Drawing.Point(0, 25);
            this.tsbMain.Name = "tsbMain";
            this.tsbMain.Size = new System.Drawing.Size(1016, 32);
            this.tsbMain.TabIndex = 1;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusText,
            this.tsslProgressNote,
            this.tspbProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 713);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatusText
            // 
            this.tsslStatusText.Margin = new System.Windows.Forms.Padding(0);
            this.tsslStatusText.Name = "tsslStatusText";
            this.tsslStatusText.Size = new System.Drawing.Size(895, 22);
            this.tsslStatusText.Spring = true;
            this.tsslStatusText.Text = "Ready";
            this.tsslStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslProgressNote
            // 
            this.tsslProgressNote.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslProgressNote.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslProgressNote.Margin = new System.Windows.Forms.Padding(0);
            this.tsslProgressNote.Name = "tsslProgressNote";
            this.tsslProgressNote.Size = new System.Drawing.Size(4, 22);
            this.tsslProgressNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tspbProgressBar
            // 
            this.tspbProgressBar.Name = "tspbProgressBar";
            this.tspbProgressBar.Size = new System.Drawing.Size(100, 16);
            this.tspbProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // gbAssemblies
            // 
            this.gbAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAssemblies.Controls.Add(this.btnProjectSummary);
            this.gbAssemblies.Controls.Add(this.btnNSSummaries);
            this.gbAssemblies.Controls.Add(this.btnRemoveAssembly);
            this.gbAssemblies.Controls.Add(this.btnEditAssembly);
            this.gbAssemblies.Controls.Add(this.btnAddAssembly);
            this.gbAssemblies.Controls.Add(this.lbAssemblies);
            this.gbAssemblies.Location = new System.Drawing.Point(12, 57);
            this.gbAssemblies.Name = "gbAssemblies";
            this.gbAssemblies.Size = new System.Drawing.Size(992, 107);
            this.gbAssemblies.TabIndex = 2;
            this.gbAssemblies.TabStop = false;
            this.gbAssemblies.Text = "Assemblies to Document";
            // 
            // gbProperties
            // 
            this.gbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProperties.Controls.Add(this.pgProps);
            this.gbProperties.Location = new System.Drawing.Point(12, 170);
            this.gbProperties.Name = "gbProperties";
            this.gbProperties.Size = new System.Drawing.Size(992, 406);
            this.gbProperties.TabIndex = 3;
            this.gbProperties.TabStop = false;
            this.gbProperties.Text = "Project Properties";
            // 
            // dividerLabel1
            // 
            this.dividerLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dividerLabel1.Location = new System.Drawing.Point(12, 576);
            this.dividerLabel1.Name = "dividerLabel1";
            this.dividerLabel1.Size = new System.Drawing.Size(992, 23);
            this.dividerLabel1.TabIndex = 4;
            this.dividerLabel1.Text = "Output";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1016, 735);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.dividerLabel1);
            this.Controls.Add(this.gbProperties);
            this.Controls.Add(this.gbAssemblies);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsbMain);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Sandcastle Help File Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tsbMain.ResumeLayout(false);
            this.tsbMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbAssemblies.ResumeLayout(false);
            this.gbProperties.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider sbStatusBarText;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem miProject;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tsbMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusText;
        private System.Windows.Forms.ToolStripStatusLabel tsslProgressNote;
        private System.Windows.Forms.ToolStripProgressBar tspbProgressBar;
        private System.Windows.Forms.ToolStripMenuItem miNewProject;
        private System.Windows.Forms.ToolStripMenuItem miOpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miSaveProject;
        private System.Windows.Forms.ToolStripMenuItem miSaveProjectAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miDocumentation;
        private System.Windows.Forms.ToolStripMenuItem miBuildProject;
        private System.Windows.Forms.ToolStripMenuItem miViewHelpFile;
        private System.Windows.Forms.ToolStripButton tsbNewProject;
        private System.Windows.Forms.ToolStripButton tsbOpenProject;
        private System.Windows.Forms.ToolStripButton tsbSaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbBuildProject;
        private System.Windows.Forms.ToolStripButton tsbViewHelpFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private SandcastleBuilder.Utils.Controls.DividerGroupBox gbAssemblies;
        private System.Windows.Forms.ListBox lbAssemblies;
        private System.Windows.Forms.Button btnAddAssembly;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnEditAssembly;
        private System.Windows.Forms.Button btnRemoveAssembly;
        private SandcastleBuilder.Utils.Controls.DividerGroupBox gbProperties;
        private SandcastleBuilder.Utils.Controls.CustomPropertyGrid pgProps;
        private SandcastleBuilder.Utils.Controls.DividerLabel dividerLabel1;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ToolStripButton tsbCancelBuild;
        private System.Windows.Forms.ToolStripMenuItem miCancelBuild;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbViewLog;
        private System.Windows.Forms.ToolStripMenuItem miViewLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem miRecentProjects;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem miNewFromNDoc;
        private System.Windows.Forms.ToolStripMenuItem miNewFromVS;
        private System.Windows.Forms.Button btnNSSummaries;
        private System.Windows.Forms.Button btnProjectSummary;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem miClearOutput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem miCleanOutput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem miVerboseLogging;
        private System.Windows.Forms.ToolStripButton tsbFaq;
        private System.Windows.Forms.ToolStripMenuItem miFaq;
    }
}

