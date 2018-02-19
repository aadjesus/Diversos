namespace SandcastleBuilder.Utils.Design
{
    partial class ContentItemEditorDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentItemEditorDlg));
            this.lbContentItems = new SandcastleBuilder.Utils.Design.RefreshableItemListBox();
            this.pgProps = new SandcastleBuilder.Utils.Controls.CustomPropertyGrid();
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.ilButton = new System.Windows.Forms.ImageList(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnPreviewTOC = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbContentItems
            // 
            this.lbContentItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbContentItems.FormattingEnabled = true;
            this.lbContentItems.IntegralHeight = false;
            this.lbContentItems.ItemHeight = 16;
            this.lbContentItems.Location = new System.Drawing.Point(0, 0);
            this.lbContentItems.Name = "lbContentItems";
            this.lbContentItems.Size = new System.Drawing.Size(245, 320);
            this.statusBarTextProvider1.SetStatusBarText(this.lbContentItems, "Select a content item to edit its properties");
            this.lbContentItems.TabIndex = 0;
            this.lbContentItems.SelectedIndexChanged += new System.EventHandler(this.lbContentItems_SelectedIndexChanged);
            // 
            // pgProps
            // 
            this.pgProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProps.Location = new System.Drawing.Point(0, 0);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(488, 320);
            this.statusBarTextProvider1.SetStatusBarText(this.pgProps, "Properties for the selected content item");
            this.pgProps.TabIndex = 0;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFolder.ImageIndex = 0;
            this.btnAddFolder.ImageList = this.ilButton;
            this.btnAddFolder.Location = new System.Drawing.Point(12, 398);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnAddFolder, "Add Folder: Add a new folder content item");
            this.btnAddFolder.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnAddFolder, "Add a folder content item");
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // ilButton
            // 
            this.ilButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButton.ImageStream")));
            this.ilButton.TransparentColor = System.Drawing.Color.Magenta;
            this.ilButton.Images.SetKeyName(0, "AddFolder.bmp");
            this.ilButton.Images.SetKeyName(1, "AddItem.bmp");
            this.ilButton.Images.SetKeyName(2, "Delete.bmp");
            this.ilButton.Images.SetKeyName(3, "PreviewTOC.bmp");
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.ImageIndex = 2;
            this.btnDelete.ImageList = this.ilButton;
            this.btnDelete.Location = new System.Drawing.Point(116, 398);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnDelete, "Delete: Delete the selected content item");
            this.btnDelete.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnDelete, "Delete the selected content item");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(665, 398);
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
            this.btnAddFile.Location = new System.Drawing.Point(50, 398);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnAddFile, "Add File(s): Add one or more new single file content items");
            this.btnAddFile.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAddFile, "Add one or more single file content items");
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnPreviewTOC
            // 
            this.btnPreviewTOC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreviewTOC.ImageIndex = 3;
            this.btnPreviewTOC.ImageList = this.ilButton;
            this.btnPreviewTOC.Location = new System.Drawing.Point(225, 398);
            this.btnPreviewTOC.Name = "btnPreviewTOC";
            this.btnPreviewTOC.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnPreviewTOC, "Preview TOC: Preview and arrange the table of content using the additional conten" +
                    "t");
            this.btnPreviewTOC.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnPreviewTOC, "Preview and arrange table of content");
            this.btnPreviewTOC.UseVisualStyleBackColor = true;
            this.btnPreviewTOC.Click += new System.EventHandler(this.btnPreviewTOC_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(737, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 72);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbContentItems);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pgProps);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(741, 320);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // ContentItemEditorDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(765, 447);
            this.Controls.Add(this.btnPreviewTOC);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(675, 475);
            this.Name = "ContentItemEditorDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Additional Content Items";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SandcastleBuilder.Utils.Design.RefreshableItemListBox lbContentItems;
        private SandcastleBuilder.Utils.Controls.CustomPropertyGrid pgProps;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList ilButton;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnPreviewTOC;
    }
}