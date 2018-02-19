namespace Tray_minimizer
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
            this.components = new System.ComponentModel.Container();
            this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.AppContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Separator = new System.Windows.Forms.ToolStripSeparator();
            this.quick = new System.Windows.Forms.ToolStripMenuItem();
            this.all = new System.Windows.Forms.ToolStripMenuItem();
            this.alltray = new System.Windows.Forms.ToolStripMenuItem();
            this.Exititem = new System.Windows.Forms.ToolStripMenuItem();
            this.Abouttoolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AppContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tray
            // 
            this.Tray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Tray.BalloonTipText = "Right click on this icon to minimize a window to tray.\r\nUse quick commands or hot" +
                "keys to process all windows\r\nDouble-click for options";
            this.Tray.BalloonTipTitle = "Tray minimizer";
            this.Tray.ContextMenuStrip = this.AppContextMenu;
            this.Tray.Icon = Properties.Resources.icon;
            this.Tray.Text = "Tray minimizer\r\nDouble-Click for options";
            this.Tray.Visible = true;
            this.Tray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Tray_MouseDoubleClick);
            // 
            // AppContextMenu
            // 
            this.AppContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Separator,
            this.quick,
            this.Exititem,
            this.Abouttoolstrip});
            this.AppContextMenu.Name = "AppContextMenu";
            this.AppContextMenu.ShowImageMargin = false;
            this.AppContextMenu.Size = new System.Drawing.Size(129, 76);
            this.AppContextMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.AppContextMenu_Closed);
            this.AppContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.AppContextMenu_Opening);
            // 
            // Separator
            // 
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(125, 6);
            // 
            // quick
            // 
            this.quick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.quick.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.all,
            this.alltray});
            this.quick.Name = "quick";
            this.quick.Size = new System.Drawing.Size(128, 22);
            this.quick.Text = "Quick commands";
            // 
            // all
            // 
            this.all.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(121, 22);
            this.all.Text = "Show All";
            this.all.Click += new System.EventHandler(this.all_Click);
            // 
            // alltray
            // 
            this.alltray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.alltray.Name = "alltray";
            this.alltray.Size = new System.Drawing.Size(121, 22);
            this.alltray.Text = "All to tray";
            this.alltray.Click += new System.EventHandler(this.alltray_Click);
            // 
            // Exititem
            // 
            this.Exititem.Name = "Exititem";
            this.Exititem.Size = new System.Drawing.Size(128, 22);
            this.Exititem.Text = "Exit";
            this.Exititem.Click += new System.EventHandler(this.Exititem_Click);
            // 
            // Abouttoolstrip
            // 
            this.Abouttoolstrip.Name = "Abouttoolstrip";
            this.Abouttoolstrip.Size = new System.Drawing.Size(128, 22);
            this.Abouttoolstrip.Text = "About";
            this.Abouttoolstrip.Click += new System.EventHandler(this.Abouttoolstrip_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 210);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.AppContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon Tray;
        private System.Windows.Forms.ContextMenuStrip AppContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Exititem;
        private System.Windows.Forms.ToolStripSeparator Separator;
        private System.Windows.Forms.ToolStripMenuItem quick;
        private System.Windows.Forms.ToolStripMenuItem all;
        private System.Windows.Forms.ToolStripMenuItem alltray;
        private System.Windows.Forms.ToolStripMenuItem Abouttoolstrip;
    }
}

