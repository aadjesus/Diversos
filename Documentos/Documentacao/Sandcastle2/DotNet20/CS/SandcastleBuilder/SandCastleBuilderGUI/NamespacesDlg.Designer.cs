namespace SandcastleBuilder
{
    partial class NamespacesDlg
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
            this.lbNamespaces = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.SuspendLayout();
            // 
            // lbNamespaces
            // 
            this.lbNamespaces.Location = new System.Drawing.Point(12, 35);
            this.lbNamespaces.Name = "lbNamespaces";
            this.lbNamespaces.Size = new System.Drawing.Size(858, 225);
            this.statusBarTextProvider1.SetStatusBarText(this.lbNamespaces, "Namespaces: Check namespaces to include them in the help file.  Uncheck them to e" +
                    "xclude them.");
            this.lbNamespaces.TabIndex = 1;
            this.lbNamespaces.SelectedIndexChanged += new System.EventHandler(this.lbNamespaces_SelectedIndexChanged);
            this.lbNamespaces.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbNamespaces_ItemCheck);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(858, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Checked namespaces will appear in the help file.  Unchecked namespaces will not.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(326, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Edit the summary for the selected namespace.";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(12, 307);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(858, 188);
            this.statusBarTextProvider1.SetStatusBarText(this.txtSummary, "Summary: Edit the summary information for the namespace");
            this.txtSummary.TabIndex = 3;
            this.txtSummary.Enter += new System.EventHandler(this.txtSummary_Enter);
            this.txtSummary.Leave += new System.EventHandler(this.txtSummary_Leave);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(782, 501);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnClose, "Close: Close this form");
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.toolTip1.SetToolTip(this.btnClose, "Close this form");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(12, 501);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnDelete, "Delete: Delete an old namespace that is no longer present");
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete";
            this.toolTip1.SetToolTip(this.btnDelete, "Delete old namespace");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // NamespacesDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(882, 545);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNamespaces);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NamespacesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Namespace Summaries";
            this.Activated += new System.EventHandler(this.NamespacesDlg_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lbNamespaces;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Button btnClose;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDelete;
    }
}