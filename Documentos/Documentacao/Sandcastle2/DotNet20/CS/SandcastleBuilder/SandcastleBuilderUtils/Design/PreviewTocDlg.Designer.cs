namespace SandcastleBuilder.Utils.Design
{
    partial class PreviewTocDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewTocDlg));
            this.btnClose = new System.Windows.Forms.Button();
            this.tvTOC = new System.Windows.Forms.TreeView();
            this.ilImages = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDefaultTopic = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(389, 455);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnClose, "Close: Close this form");
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.toolTip1.SetToolTip(this.btnClose, "Close this form");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tvTOC
            // 
            this.tvTOC.HideSelection = false;
            this.tvTOC.ImageIndex = 0;
            this.tvTOC.ImageList = this.ilImages;
            this.tvTOC.Location = new System.Drawing.Point(12, 12);
            this.tvTOC.Name = "tvTOC";
            this.tvTOC.SelectedImageIndex = 0;
            this.tvTOC.ShowNodeToolTips = true;
            this.tvTOC.Size = new System.Drawing.Size(427, 437);
            this.statusBarTextProvider1.SetStatusBarText(this.tvTOC, "Table of Content: This is how the table of content for additional content current" +
                    "ly appears");
            this.tvTOC.TabIndex = 0;
            this.tvTOC.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTOC_AfterSelect);
            // 
            // ilImages
            // 
            this.ilImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages.ImageStream")));
            this.ilImages.TransparentColor = System.Drawing.Color.Magenta;
            this.ilImages.Images.SetKeyName(0, "NormalTopic.bmp");
            this.ilImages.Images.SetKeyName(1, "DefaultTopic.bmp");
            this.ilImages.Images.SetKeyName(2, "MoveUp.bmp");
            this.ilImages.Images.SetKeyName(3, "MoveDown.bmp");
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 455);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnSave, "Save: Save all changes to the order and default topic");
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.toolTip1.SetToolTip(this.btnSave, "Save changes");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDefaultTopic
            // 
            this.btnDefaultTopic.ImageIndex = 1;
            this.btnDefaultTopic.ImageList = this.ilImages;
            this.btnDefaultTopic.Location = new System.Drawing.Point(445, 12);
            this.btnDefaultTopic.Name = "btnDefaultTopic";
            this.btnDefaultTopic.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnDefaultTopic, "Default Topic: Toggle the default topic");
            this.btnDefaultTopic.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnDefaultTopic, "Toggle the default topic");
            this.btnDefaultTopic.UseVisualStyleBackColor = true;
            this.btnDefaultTopic.Click += new System.EventHandler(this.btnDefaultTopic_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.ImageIndex = 2;
            this.btnMoveUp.ImageList = this.ilImages;
            this.btnMoveUp.Location = new System.Drawing.Point(445, 63);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnMoveUp, "Move Up: Move the selected topic up within its group");
            this.btnMoveUp.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnMoveUp, "Move topic up within group");
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveNode_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.ImageIndex = 3;
            this.btnMoveDown.ImageList = this.ilImages;
            this.btnMoveDown.Location = new System.Drawing.Point(445, 101);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(32, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnMoveDown, "Move Down: Move the selected topic down within its group");
            this.btnMoveDown.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnMoveDown, "Move topic down within group");
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveNode_Click);
            // 
            // PreviewTocDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(489, 499);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnDefaultTopic);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tvTOC);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewTocDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Arrange Table of Content";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreviewTocDlg_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TreeView tvTOC;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.ImageList ilImages;
        private System.Windows.Forms.Button btnDefaultTopic;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
    }
}