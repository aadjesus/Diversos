namespace WCF_Client_SvcUtil
{
    partial class FOptions
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
            this.lblSvcUtilFolder = new System.Windows.Forms.Label();
            this.btnBrowseForSvcUtilFolder = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblReset = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cbGlass = new System.Windows.Forms.CheckBox();
            this.txtSvcUtilFolder = new System.Windows.Forms.TextBox();
            this.glassProvider = new VistaStyle.GlassProvider(this.components);
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSvcUtilFolder
            // 
            this.lblSvcUtilFolder.AutoSize = true;
            this.lblSvcUtilFolder.Location = new System.Drawing.Point(12, 19);
            this.lblSvcUtilFolder.Name = "lblSvcUtilFolder";
            this.lblSvcUtilFolder.Size = new System.Drawing.Size(81, 13);
            this.lblSvcUtilFolder.TabIndex = 2;
            this.lblSvcUtilFolder.Text = "Path of SvcUtil:";
            // 
            // btnBrowseForSvcUtilFolder
            // 
            this.btnBrowseForSvcUtilFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseForSvcUtilFolder.Location = new System.Drawing.Point(385, 10);
            this.btnBrowseForSvcUtilFolder.Name = "btnBrowseForSvcUtilFolder";
            this.btnBrowseForSvcUtilFolder.Size = new System.Drawing.Size(50, 23);
            this.btnBrowseForSvcUtilFolder.TabIndex = 4;
            this.btnBrowseForSvcUtilFolder.Text = "...";
            this.btnBrowseForSvcUtilFolder.UseVisualStyleBackColor = true;
            this.btnBrowseForSvcUtilFolder.Click += new System.EventHandler(this.btnBrowseForSvcUtilFolder_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(229, 99);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(335, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select the folder of SvcUtil...";
            this.folderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(335, 38);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Here";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblReset
            // 
            this.lblReset.AutoSize = true;
            this.lblReset.Location = new System.Drawing.Point(12, 43);
            this.lblReset.Name = "lblReset";
            this.lblReset.Size = new System.Drawing.Size(193, 13);
            this.lblReset.TabIndex = 2;
            this.lblReset.Text = "To reset the application to default press";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.cbGlass);
            this.pnlMain.Controls.Add(this.lblReset);
            this.pnlMain.Controls.Add(this.lblSvcUtilFolder);
            this.pnlMain.Controls.Add(this.btnBrowseForSvcUtilFolder);
            this.pnlMain.Controls.Add(this.btnReset);
            this.pnlMain.Controls.Add(this.txtSvcUtilFolder);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(443, 93);
            this.pnlMain.TabIndex = 2;
            // 
            // cbGlass
            // 
            this.cbGlass.AutoSize = true;
            this.cbGlass.Checked = global::WCF_Client_SvcUtil.Properties.Settings.Default.StartWithAereoGlass;
            this.cbGlass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGlass.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::WCF_Client_SvcUtil.Properties.Settings.Default, "StartWithAereoGlass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbGlass.Location = new System.Drawing.Point(12, 68);
            this.cbGlass.Name = "cbGlass";
            this.cbGlass.Size = new System.Drawing.Size(113, 17);
            this.cbGlass.TabIndex = 6;
            this.cbGlass.Text = "Enable Aero Glass";
            this.cbGlass.UseVisualStyleBackColor = true;
            // 
            // txtSvcUtilFolder
            // 
            this.txtSvcUtilFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WCF_Client_SvcUtil.Properties.Settings.Default, "SvcUtilPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSvcUtilFolder.Location = new System.Drawing.Point(99, 12);
            this.txtSvcUtilFolder.Name = "txtSvcUtilFolder";
            this.txtSvcUtilFolder.Size = new System.Drawing.Size(280, 20);
            this.txtSvcUtilFolder.TabIndex = 3;
            this.txtSvcUtilFolder.Text = global::WCF_Client_SvcUtil.Properties.Settings.Default.SvcUtilPath;
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
            this.glassProvider.OmittedPanel = this.pnlMain;
            // 
            // FOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(443, 129);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FOptions_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSvcUtilFolder;
        private System.Windows.Forms.TextBox txtSvcUtilFolder;
        private System.Windows.Forms.Button btnBrowseForSvcUtilFolder;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblReset;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.CheckBox cbGlass;
        private VistaStyle.GlassProvider glassProvider;
    }
}