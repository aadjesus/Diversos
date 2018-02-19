namespace WCF_Client_SvcUtil
{
    partial class FMain
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlClientArea = new System.Windows.Forms.Panel();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.lblProxyFilename = new System.Windows.Forms.Label();
            this.lblConfigFilename = new System.Windows.Forms.Label();
            this.txtProxyFilename = new System.Windows.Forms.TextBox();
            this.txtConfigFilename = new System.Windows.Forms.TextBox();
            this.txtServiceAddress = new System.Windows.Forms.TextBox();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.lblServiceAddress = new System.Windows.Forms.Label();
            this.btnOutputDirectory = new System.Windows.Forms.Button();
            this.gbParameters = new System.Windows.Forms.GroupBox();
            this.cbSerializable = new System.Windows.Forms.CheckBox();
            this.cbMergeConfig = new System.Windows.Forms.CheckBox();
            this.cbAsync = new System.Windows.Forms.CheckBox();
            this.lblLanguageDescription = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbAppIcon = new System.Windows.Forms.PictureBox();
            this.pnlProcOutput = new System.Windows.Forms.Panel();
            this.txtProcOutput = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblProcOutput = new System.Windows.Forms.Label();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.generationWorker = new System.ComponentModel.BackgroundWorker();
            this.glassProvider = new VistaStyle.GlassProvider(this.components);
            this.waitControl = new WCF_Client_SvcUtil.Controls.WaitControl();
            this.pnlClientArea.SuspendLayout();
            this.gbParameters.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppIcon)).BeginInit();
            this.pnlProcOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Black;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblTitle.Image")));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10);
            this.lblTitle.Size = new System.Drawing.Size(487, 53);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "WCF Client SvcUtil";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlClientArea
            // 
            this.pnlClientArea.Controls.Add(this.lblOutputDirectory);
            this.pnlClientArea.Controls.Add(this.lblProxyFilename);
            this.pnlClientArea.Controls.Add(this.lblConfigFilename);
            this.pnlClientArea.Controls.Add(this.txtProxyFilename);
            this.pnlClientArea.Controls.Add(this.txtConfigFilename);
            this.pnlClientArea.Controls.Add(this.txtServiceAddress);
            this.pnlClientArea.Controls.Add(this.txtOutputDirectory);
            this.pnlClientArea.Controls.Add(this.lblServiceAddress);
            this.pnlClientArea.Controls.Add(this.btnOutputDirectory);
            this.pnlClientArea.Controls.Add(this.gbParameters);
            this.pnlClientArea.Controls.Add(this.lblLanguageDescription);
            this.pnlClientArea.Controls.Add(this.menuStrip);
            this.pnlClientArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClientArea.Location = new System.Drawing.Point(0, 53);
            this.pnlClientArea.Margin = new System.Windows.Forms.Padding(0);
            this.pnlClientArea.Name = "pnlClientArea";
            this.pnlClientArea.Size = new System.Drawing.Size(487, 197);
            this.pnlClientArea.TabIndex = 1;
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Location = new System.Drawing.Point(7, 38);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(85, 13);
            this.lblOutputDirectory.TabIndex = 13;
            this.lblOutputDirectory.Text = "Output directory:";
            // 
            // lblProxyFilename
            // 
            this.lblProxyFilename.AutoSize = true;
            this.lblProxyFilename.Location = new System.Drawing.Point(7, 64);
            this.lblProxyFilename.Name = "lblProxyFilename";
            this.lblProxyFilename.Size = new System.Drawing.Size(78, 13);
            this.lblProxyFilename.TabIndex = 14;
            this.lblProxyFilename.Text = "Proxy filename:";
            // 
            // lblConfigFilename
            // 
            this.lblConfigFilename.AutoSize = true;
            this.lblConfigFilename.Location = new System.Drawing.Point(7, 90);
            this.lblConfigFilename.Name = "lblConfigFilename";
            this.lblConfigFilename.Size = new System.Drawing.Size(79, 13);
            this.lblConfigFilename.TabIndex = 11;
            this.lblConfigFilename.Text = "Config filename";
            // 
            // txtProxyFilename
            // 
            this.txtProxyFilename.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WCF_Client_SvcUtil.Properties.Settings.Default, "TxtProxyFilename_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtProxyFilename.Location = new System.Drawing.Point(105, 62);
            this.txtProxyFilename.Name = "txtProxyFilename";
            this.txtProxyFilename.Size = new System.Drawing.Size(168, 20);
            this.txtProxyFilename.TabIndex = 17;
            this.txtProxyFilename.Text = global::WCF_Client_SvcUtil.Properties.Settings.Default.TxtProxyFilename_Text;
            // 
            // txtConfigFilename
            // 
            this.txtConfigFilename.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WCF_Client_SvcUtil.Properties.Settings.Default, "TxtConfigFilename_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtConfigFilename.Location = new System.Drawing.Point(105, 88);
            this.txtConfigFilename.Name = "txtConfigFilename";
            this.txtConfigFilename.Size = new System.Drawing.Size(168, 20);
            this.txtConfigFilename.TabIndex = 18;
            this.txtConfigFilename.Text = global::WCF_Client_SvcUtil.Properties.Settings.Default.TxtConfigFilename_Text;
            // 
            // txtServiceAddress
            // 
            this.txtServiceAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtServiceAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.txtServiceAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WCF_Client_SvcUtil.Properties.Settings.Default, "TxtServiceAddress_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtServiceAddress.Location = new System.Drawing.Point(105, 114);
            this.txtServiceAddress.Name = "txtServiceAddress";
            this.txtServiceAddress.Size = new System.Drawing.Size(370, 20);
            this.txtServiceAddress.TabIndex = 20;
            this.txtServiceAddress.Text = global::WCF_Client_SvcUtil.Properties.Settings.Default.TxtServiceAddress_Text;
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WCF_Client_SvcUtil.Properties.Settings.Default, "TxtOutputDirectory_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtOutputDirectory.Location = new System.Drawing.Point(105, 36);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(314, 20);
            this.txtOutputDirectory.TabIndex = 15;
            this.txtOutputDirectory.Text = global::WCF_Client_SvcUtil.Properties.Settings.Default.TxtOutputDirectory_Text;
            // 
            // lblServiceAddress
            // 
            this.lblServiceAddress.AutoSize = true;
            this.lblServiceAddress.Location = new System.Drawing.Point(7, 116);
            this.lblServiceAddress.Name = "lblServiceAddress";
            this.lblServiceAddress.Size = new System.Drawing.Size(86, 13);
            this.lblServiceAddress.TabIndex = 12;
            this.lblServiceAddress.Text = "Service address:";
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputDirectory.Location = new System.Drawing.Point(425, 31);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(50, 25);
            this.btnOutputDirectory.TabIndex = 16;
            this.btnOutputDirectory.Text = "...";
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // gbParameters
            // 
            this.gbParameters.Controls.Add(this.cbSerializable);
            this.gbParameters.Controls.Add(this.cbMergeConfig);
            this.gbParameters.Controls.Add(this.cbAsync);
            this.gbParameters.Location = new System.Drawing.Point(10, 140);
            this.gbParameters.Name = "gbParameters";
            this.gbParameters.Size = new System.Drawing.Size(465, 49);
            this.gbParameters.TabIndex = 21;
            this.gbParameters.TabStop = false;
            this.gbParameters.Text = "Parameters";
            // 
            // cbSerializable
            // 
            this.cbSerializable.AutoSize = true;
            this.cbSerializable.Checked = global::WCF_Client_SvcUtil.Properties.Settings.Default.ParamSerializable;
            this.cbSerializable.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::WCF_Client_SvcUtil.Properties.Settings.Default, "ParamSerializable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSerializable.Location = new System.Drawing.Point(377, 19);
            this.cbSerializable.Name = "cbSerializable";
            this.cbSerializable.Size = new System.Drawing.Size(82, 17);
            this.cbSerializable.TabIndex = 13;
            this.cbSerializable.Text = "/serializable";
            this.cbSerializable.UseVisualStyleBackColor = true;
            // 
            // cbMergeConfig
            // 
            this.cbMergeConfig.AutoSize = true;
            this.cbMergeConfig.Checked = global::WCF_Client_SvcUtil.Properties.Settings.Default.ParamMergeConfig;
            this.cbMergeConfig.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::WCF_Client_SvcUtil.Properties.Settings.Default, "ParamMergeConfig", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbMergeConfig.Location = new System.Drawing.Point(173, 19);
            this.cbMergeConfig.Name = "cbMergeConfig";
            this.cbMergeConfig.Size = new System.Drawing.Size(90, 17);
            this.cbMergeConfig.TabIndex = 12;
            this.cbMergeConfig.Text = "/mergeConfig";
            this.cbMergeConfig.UseVisualStyleBackColor = true;
            // 
            // cbAsync
            // 
            this.cbAsync.AutoSize = true;
            this.cbAsync.Checked = global::WCF_Client_SvcUtil.Properties.Settings.Default.ParamAsync;
            this.cbAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAsync.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::WCF_Client_SvcUtil.Properties.Settings.Default, "ParamAsync", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbAsync.Location = new System.Drawing.Point(6, 19);
            this.cbAsync.Name = "cbAsync";
            this.cbAsync.Size = new System.Drawing.Size(59, 17);
            this.cbAsync.TabIndex = 11;
            this.cbAsync.Text = "/async";
            this.cbAsync.UseVisualStyleBackColor = true;
            // 
            // lblLanguageDescription
            // 
            this.lblLanguageDescription.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblLanguageDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLanguageDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguageDescription.ForeColor = System.Drawing.Color.Black;
            this.lblLanguageDescription.Location = new System.Drawing.Point(295, 62);
            this.lblLanguageDescription.Name = "lblLanguageDescription";
            this.lblLanguageDescription.Size = new System.Drawing.Size(180, 46);
            this.lblLanguageDescription.TabIndex = 19;
            this.lblLanguageDescription.Text = "Use the suffix .cs to indicate that C#-code is to be generated. Use .vb for VBasi" +
                "c-code otherwise.";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.extrasToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(487, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generate_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.extrasToolStripMenuItem.Text = "Extras";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // pbAppIcon
            // 
            this.pbAppIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbAppIcon.Image")));
            this.pbAppIcon.Location = new System.Drawing.Point(575, 12);
            this.pbAppIcon.Name = "pbAppIcon";
            this.pbAppIcon.Size = new System.Drawing.Size(32, 32);
            this.pbAppIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbAppIcon.TabIndex = 2;
            this.pbAppIcon.TabStop = false;
            // 
            // pnlProcOutput
            // 
            this.pnlProcOutput.Controls.Add(this.txtProcOutput);
            this.pnlProcOutput.Controls.Add(this.waitControl);
            this.pnlProcOutput.Controls.Add(this.btnGenerate);
            this.pnlProcOutput.Controls.Add(this.lblProcOutput);
            this.pnlProcOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProcOutput.Location = new System.Drawing.Point(0, 250);
            this.pnlProcOutput.Name = "pnlProcOutput";
            this.pnlProcOutput.Size = new System.Drawing.Size(487, 217);
            this.pnlProcOutput.TabIndex = 3;
            // 
            // txtProcOutput
            // 
            this.txtProcOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtProcOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProcOutput.ForeColor = System.Drawing.Color.White;
            this.txtProcOutput.Location = new System.Drawing.Point(10, 34);
            this.txtProcOutput.Multiline = true;
            this.txtProcOutput.Name = "txtProcOutput";
            this.txtProcOutput.ReadOnly = true;
            this.txtProcOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProcOutput.Size = new System.Drawing.Size(465, 168);
            this.txtProcOutput.TabIndex = 3;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.Black;
            this.btnGenerate.Location = new System.Drawing.Point(375, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 25);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate files";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.generate_Click);
            // 
            // lblProcOutput
            // 
            this.lblProcOutput.AutoSize = true;
            this.lblProcOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcOutput.Location = new System.Drawing.Point(13, 9);
            this.lblProcOutput.Name = "lblProcOutput";
            this.lblProcOutput.Size = new System.Drawing.Size(105, 13);
            this.lblProcOutput.TabIndex = 0;
            this.lblProcOutput.Text = "Output of SvcUtil";
            // 
            // dlgFolderBrowser
            // 
            this.dlgFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // generationWorker
            // 
            this.generationWorker.WorkerReportsProgress = true;
            this.generationWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.generationWorker_DoWork);
            this.generationWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.generationWorker_RunWorkerCompleted);
            this.generationWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.generationWorker_ProgressChanged);
            // 
            // glassProvider
            // 
            this.glassProvider.ClientBackColorOnGlass = System.Drawing.Color.Black;
            this.glassProvider.ClientForeColorOnGlass = System.Drawing.Color.White;
            this.glassProvider.GlassClientForm = this;
            this.glassProvider.GlassOmisson = VistaStyle.GlassProvider.GlassOmissionEnum.OmitPanel;
            this.glassProvider.MarginFromBottom = 0;
            this.glassProvider.MarginFromLeft = 0;
            this.glassProvider.MarginFromRight = 0;
            this.glassProvider.MarginFromTop = 0;
            this.glassProvider.OmittedPanel = this.pnlClientArea;
            // 
            // waitControl
            // 
            this.waitControl.AutoSize = true;
            this.waitControl.Location = new System.Drawing.Point(183, 9);
            this.waitControl.Name = "waitControl";
            this.waitControl.Size = new System.Drawing.Size(161, 19);
            this.waitControl.TabIndex = 2;
            this.waitControl.Text = "Wait while generating...";
            this.waitControl.Visible = false;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 467);
            this.Controls.Add(this.pnlProcOutput);
            this.Controls.Add(this.pbAppIcon);
            this.Controls.Add(this.pnlClientArea);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WCF Client SvcUtil";
            this.Load += new System.EventHandler(this.FMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.pnlClientArea.ResumeLayout(false);
            this.pnlClientArea.PerformLayout();
            this.gbParameters.ResumeLayout(false);
            this.gbParameters.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppIcon)).EndInit();
            this.pnlProcOutput.ResumeLayout(false);
            this.pnlProcOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlClientArea;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbAppIcon;
        private System.Windows.Forms.Label lblOutputDirectory;
        private System.Windows.Forms.Label lblProxyFilename;
        private System.Windows.Forms.Label lblConfigFilename;
        private System.Windows.Forms.TextBox txtProxyFilename;
        private System.Windows.Forms.TextBox txtConfigFilename;
        private System.Windows.Forms.TextBox txtServiceAddress;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Label lblServiceAddress;
        private System.Windows.Forms.Button btnOutputDirectory;
        private System.Windows.Forms.GroupBox gbParameters;
        private System.Windows.Forms.CheckBox cbSerializable;
        private System.Windows.Forms.CheckBox cbMergeConfig;
        private System.Windows.Forms.CheckBox cbAsync;
        private System.Windows.Forms.Label lblLanguageDescription;
        private System.Windows.Forms.Panel pnlProcOutput;
        private System.Windows.Forms.Label lblProcOutput;
        private System.Windows.Forms.Button btnGenerate;
        private WCF_Client_SvcUtil.Controls.WaitControl waitControl;
        private System.Windows.Forms.TextBox txtProcOutput;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.ComponentModel.BackgroundWorker generationWorker;
        private VistaStyle.GlassProvider glassProvider;
    }
}

