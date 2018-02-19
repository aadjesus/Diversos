namespace SandcastleBuilder
{
    partial class ProjectSummaryDlg
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 233);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnOK, "OK: Save changes to the comments");
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.toolTip1.SetToolTip(this.btnOK, "Save changes to the comments");
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(658, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnCancel, "Cancel: Exit without saving changes");
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Exit without saving changes");
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(12, 39);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(732, 188);
            this.statusBarTextProvider1.SetStatusBarText(this.txtSummary, "Summary: Edit the summary information for the project");
            this.txtSummary.TabIndex = 1;
            this.txtSummary.Enter += new System.EventHandler(this.txtSummary_Enter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(422, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "These comments will appear in the root namespaces page.";
            // 
            // ProjectSummaryDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(758, 279);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectSummaryDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Project Summary Comments";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCancel;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label label2;
    }
}