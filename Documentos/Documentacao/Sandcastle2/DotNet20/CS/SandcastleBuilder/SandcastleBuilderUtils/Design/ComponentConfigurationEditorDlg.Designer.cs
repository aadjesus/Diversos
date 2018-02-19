namespace SandcastleBuilder.Utils.Design
{
    partial class ComponentConfigurationEditorDlg
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
            if(disposing)
            {
                if(components != null)
                    components.Dispose();

                if(boldFont != null)
                    boldFont.Dispose();
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
            this.lbComponents = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConfigure = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.statusBarTextProvider1 = new SandcastleBuilder.Utils.Controls.StatusBarTextProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbComponents
            // 
            this.lbComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbComponents.FormattingEnabled = true;
            this.lbComponents.IntegralHeight = false;
            this.lbComponents.ItemHeight = 16;
            this.lbComponents.Location = new System.Drawing.Point(12, 35);
            this.lbComponents.Name = "lbComponents";
            this.lbComponents.Size = new System.Drawing.Size(354, 259);
            this.statusBarTextProvider1.SetStatusBarText(this.lbComponents, "Select the component to configure");
            this.lbComponents.TabIndex = 1;
            this.lbComponents.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbComponents_DrawItem);
            this.lbComponents.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbComponents_MeasureItem);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(278, 300);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnClose, "Close: Close this form");
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConfigure
            // 
            this.btnConfigure.Location = new System.Drawing.Point(12, 300);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnConfigure, "Configure: Configure the selected component");
            this.btnConfigure.TabIndex = 4;
            this.btnConfigure.Text = "C&onfigure";
            this.btnConfigure.UseVisualStyleBackColor = true;
            this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(106, 300);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 32);
            this.statusBarTextProvider1.SetStatusBarText(this.btnReset, "Reset: Reset the configuration of the selected component to the defaults");
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bold items have custom settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComponentConfigurationEditorDlg
            // 
            this.AcceptButton = this.btnConfigure;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(378, 344);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnConfigure);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbComponents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComponentConfigurationEditorDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurable Build Components";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbComponents;
        private SandcastleBuilder.Utils.Controls.StatusBarTextProvider statusBarTextProvider1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnConfigure;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label1;
    }
}