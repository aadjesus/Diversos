namespace SandcastleBuilder
{
    partial class SelectAssemblyDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAssemblyDlg));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pgProps = new SandcastleBuilder.Utils.Controls.CustomPropertyGrid();
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 423);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnOK, "OK: Save changes to the assembly");
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.toolTip1.SetToolTip(this.btnOK, "Save changes to assembly");
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(656, 423);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnCancel, "Cancel: Exit without saving changes");
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Exit without saving changes");
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgProps.Location = new System.Drawing.Point(12, 61);
            this.pgProps.Name = "pgProps";
            this.pgProps.PropertyNamePaneWidth = 175;
            this.pgProps.Size = new System.Drawing.Size(732, 356);
            this.statusBarTextProvider1.SetStatusBarText(this.pgProps, "Properties: Edit the assembly properties");
            this.pgProps.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(732, 49);
            this.label1.TabIndex = 3;
            this.label1.Text = "NOTE: Only add assemblies that you want documented and the related comments  file" +
                "s.  For dependent assemblies that do not need documenting, add them to the proje" +
                "ct\'s Dependencies property.";
            // 
            // SelectAssemblyDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(756, 467);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgProps);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1600, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(764, 350);
            this.Name = "SelectAssemblyDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a Documentation Assembly";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectAssemblyDlg_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCancel;
        private SandcastleBuilder.Utils.Controls.CustomPropertyGrid pgProps;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.Label label1;
    }
}