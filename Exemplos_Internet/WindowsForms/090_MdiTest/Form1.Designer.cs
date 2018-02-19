namespace MdiTest
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.form1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.opToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.form1ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(774, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // form1ToolStripMenuItem
            // 
            this.form1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opToolStripMenuItem1,
            this.opToolStripMenuItem2});
            this.form1ToolStripMenuItem.Name = "form1ToolStripMenuItem";
            this.form1ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.form1ToolStripMenuItem.Text = "Menu";
            // 
            // opToolStripMenuItem1
            // 
            this.opToolStripMenuItem1.Name = "opToolStripMenuItem1";
            this.opToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.opToolStripMenuItem1.Text = "Open Form 1";
            this.opToolStripMenuItem1.Click += new System.EventHandler(this.opToolStripMenuItem1_Click);
            // 
            // opToolStripMenuItem2
            // 
            this.opToolStripMenuItem2.Name = "opToolStripMenuItem2";
            this.opToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.opToolStripMenuItem2.Text = "Open Form 2";
            this.opToolStripMenuItem2.Click += new System.EventHandler(this.opToolStripMenuItem2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 590);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.LookAndFeel.SkinName = "Blue";
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem form1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem opToolStripMenuItem2;
    }
}

