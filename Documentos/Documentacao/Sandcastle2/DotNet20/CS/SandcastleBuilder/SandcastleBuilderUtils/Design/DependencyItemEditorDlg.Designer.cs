namespace SandcastleBuilder.Utils.Design
{
    partial class DependencyItemEditorDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DependencyItemEditorDlg));
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.ilButton = new System.Windows.Forms.ImageList(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.lbDependencyItems = new SandcastleBuilder.Utils.Design.RefreshableItemListBox();
            this.btnAddFromGAC = new System.Windows.Forms.Button();
            this.pgProps = new SandcastleBuilder.Utils.Controls.CustomPropertyGrid();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFolder.ImageIndex = 0;
            this.btnAddFolder.ImageList = this.ilButton;
            this.btnAddFolder.Location = new System.Drawing.Point(12, 552);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnAddFolder, "Add Folder: Add a new folder as a dependency");
            this.btnAddFolder.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnAddFolder, "Add a folder dependency");
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // ilButton
            // 
            this.ilButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButton.ImageStream")));
            this.ilButton.TransparentColor = System.Drawing.Color.Magenta;
            this.ilButton.Images.SetKeyName(0, "AddFolder.bmp");
            this.ilButton.Images.SetKeyName(1, "AddItem.bmp");
            this.ilButton.Images.SetKeyName(2, "AddGAC.bmp");
            this.ilButton.Images.SetKeyName(3, "Delete.bmp");
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.ImageIndex = 3;
            this.btnDelete.ImageList = this.ilButton;
            this.btnDelete.Location = new System.Drawing.Point(150, 552);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnDelete, "Delete: Delete the selected dependency");
            this.btnDelete.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnDelete, "Delete the selected dependency");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(665, 552);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnClose, "Close: Close this form");
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.toolTip1.SetToolTip(this.btnClose, "Close this form");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFile.ImageIndex = 1;
            this.btnAddFile.ImageList = this.ilButton;
            this.btnAddFile.Location = new System.Drawing.Point(50, 552);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnAddFile, "Add File(s): Add one or more new single file as a dependencies");
            this.btnAddFile.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAddFile, "Add one or more single file dependencies");
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // lbDependencyItems
            // 
            this.lbDependencyItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDependencyItems.FormattingEnabled = true;
            this.lbDependencyItems.IntegralHeight = false;
            this.lbDependencyItems.ItemHeight = 16;
            this.lbDependencyItems.Location = new System.Drawing.Point(12, 12);
            this.lbDependencyItems.Name = "lbDependencyItems";
            this.lbDependencyItems.Size = new System.Drawing.Size(741, 300);
            this.statusBarTextProvider1.SetStatusBarText(this.lbDependencyItems, "Select a dependency item");
            this.lbDependencyItems.TabIndex = 0;
            this.lbDependencyItems.SelectedIndexChanged += new System.EventHandler(this.lbDependencyItems_SelectedIndexChanged);
            // 
            // btnAddFromGAC
            // 
            this.btnAddFromGAC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFromGAC.ImageIndex = 2;
            this.btnAddFromGAC.ImageList = this.ilButton;
            this.btnAddFromGAC.Location = new System.Drawing.Point(88, 552);
            this.btnAddFromGAC.Name = "btnAddFromGAC";
            this.btnAddFromGAC.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnAddFromGAC, "Add from GAC: Add one or more new dependencies from the Global Assembly Cache");
            this.btnAddFromGAC.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnAddFromGAC, "Add GAC dependencies");
            this.btnAddFromGAC.UseVisualStyleBackColor = true;
            this.btnAddFromGAC.Click += new System.EventHandler(this.btnAddFromGAC_Click);
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgProps.Location = new System.Drawing.Point(12, 318);
            this.pgProps.Name = "pgProps";
            this.pgProps.PropertyNamePaneWidth = 150;
            this.pgProps.Size = new System.Drawing.Size(741, 228);
            this.statusBarTextProvider1.SetStatusBarText(this.pgProps, "Edit the properties of the selected dependency");
            this.pgProps.TabIndex = 1;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // DependencyItemEditorDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(765, 601);
            this.Controls.Add(this.pgProps);
            this.Controls.Add(this.btnAddFromGAC);
            this.Controls.Add(this.lbDependencyItems);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(675, 475);
            this.Name = "DependencyItemEditorDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Documentation Assembly Dependencies";
            this.ResumeLayout(false);

        }

        #endregion

        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList ilButton;
        private System.Windows.Forms.Button btnAddFile;
        private RefreshableItemListBox lbDependencyItems;
        private System.Windows.Forms.Button btnAddFromGAC;
        private SandcastleBuilder.Utils.Controls.CustomPropertyGrid pgProps;
    }
}