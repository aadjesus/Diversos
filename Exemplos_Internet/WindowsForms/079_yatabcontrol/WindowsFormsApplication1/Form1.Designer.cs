namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.yaTabControl1 = new GrayIris.Utilities.UI.Controls.YaTabControl();
            this.tabPage1 = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.tabPage2 = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.tabPage3 = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.tabPage4 = new GrayIris.Utilities.UI.Controls.YaTabPage();
            this.ovalTabDrawer1 = new GrayIris.Utilities.UI.Controls.OvalTabDrawer();
            this.vsTabDrawer1 = new GrayIris.Utilities.UI.Controls.VsTabDrawer();
            this.xlTabDrawer1 = new GrayIris.Utilities.UI.Controls.XlTabDrawer();
            this.yaTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // yaTabControl1
            // 
            this.yaTabControl1.ActiveColor = System.Drawing.SystemColors.Control;
            this.yaTabControl1.BackColor = System.Drawing.SystemColors.Control;
            this.yaTabControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.yaTabControl1.Controls.Add(this.tabPage1);
            this.yaTabControl1.Controls.Add(this.tabPage2);
            this.yaTabControl1.Controls.Add(this.tabPage3);
            this.yaTabControl1.Controls.Add(this.tabPage4);
            this.yaTabControl1.ImageIndex = -1;
            this.yaTabControl1.ImageList = null;
            this.yaTabControl1.InactiveColor = System.Drawing.SystemColors.Window;
            this.yaTabControl1.Location = new System.Drawing.Point(14, 48);
            this.yaTabControl1.Name = "yaTabControl1";
            this.yaTabControl1.ScrollButtonStyle = GrayIris.Utilities.UI.Controls.YaScrollButtonStyle.Always;
            this.yaTabControl1.SelectedIndex = 2;
            this.yaTabControl1.SelectedTab = this.tabPage3;
            this.yaTabControl1.Size = new System.Drawing.Size(421, 286);
            this.yaTabControl1.TabDock = System.Windows.Forms.DockStyle.Top;
            this.yaTabControl1.TabDrawer = null;
            this.yaTabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yaTabControl1.TabIndex = 0;
            this.yaTabControl1.Text = "yaTabControl1";
            // 
            // tabPage1
            // 
            this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage1.ImageIndex = -1;
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(413, 252);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage2.ImageIndex = -1;
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(413, 252);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // tabPage3
            // 
            this.tabPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage3.ImageIndex = -1;
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(413, 252);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // tabPage4
            // 
            this.tabPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage4.ImageIndex = -1;
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(413, 252);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 396);
            this.Controls.Add(this.yaTabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.yaTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GrayIris.Utilities.UI.Controls.OvalTabDrawer ovalTabDrawer1;
        private GrayIris.Utilities.UI.Controls.VsTabDrawer vsTabDrawer1;
        private GrayIris.Utilities.UI.Controls.XlTabDrawer xlTabDrawer1;
        private GrayIris.Utilities.UI.Controls.YaTabControl yaTabControl1;
        private GrayIris.Utilities.UI.Controls.YaTabPage tabPage1;
        private GrayIris.Utilities.UI.Controls.YaTabPage tabPage2;
        private GrayIris.Utilities.UI.Controls.YaTabPage tabPage3;
        private GrayIris.Utilities.UI.Controls.YaTabPage tabPage4;
    }
}

