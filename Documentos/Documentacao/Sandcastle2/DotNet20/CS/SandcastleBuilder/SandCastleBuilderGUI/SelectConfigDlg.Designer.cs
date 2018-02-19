namespace SandcastleBuilder
{
    partial class SelectConfigDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbConfigs = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the configuration from which to load assembly information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbConfigs
            // 
            this.lbConfigs.FormattingEnabled = true;
            this.lbConfigs.ItemHeight = 16;
            this.lbConfigs.Location = new System.Drawing.Point(12, 35);
            this.lbConfigs.Name = "lbConfigs";
            this.lbConfigs.Size = new System.Drawing.Size(534, 180);
            this.statusBarTextProvider1.SetStatusBarText(this.lbConfigs, "Configuration: Select the configuration from which to load the assembly informati" +
                    "on");
            this.lbConfigs.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.Location = new System.Drawing.Point(12, 225);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnSelect, "Select: Use the selected configuration for the assembly information");
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "&Select";
            this.toolTip1.SetToolTip(this.btnSelect, "Use the selected configuration");
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(458, 225);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnCancel, "Cancel: Close without making a selection");
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Close without a selection");
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SelectConfigDlg
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(558, 269);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbConfigs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectConfigDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Configuration";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbConfigs;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}