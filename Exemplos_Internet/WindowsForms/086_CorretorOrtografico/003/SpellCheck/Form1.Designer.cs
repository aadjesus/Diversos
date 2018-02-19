namespace SpellCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspSave = new System.Windows.Forms.ToolStripButton();
            this.tspOpen = new System.Windows.Forms.ToolStripButton();
            this.tspSpellCheck = new System.Windows.Forms.ToolStripButton();
            this.tspExit = new System.Windows.Forms.ToolStripButton();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.spellCheckTextControl1 = new SpellCheckControl.SpellCheckTextControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSave,
            this.tspOpen,
            this.tspSpellCheck,
            this.tspExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspSave
            // 
            this.tspSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspSave.Image = ((System.Drawing.Image)(resources.GetObject("tspSave.Image")));
            this.tspSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSave.Name = "tspSave";
            this.tspSave.Size = new System.Drawing.Size(23, 22);
            this.tspSave.Text = "Save File";
            this.tspSave.Click += new System.EventHandler(this.tspSave_Click);
            // 
            // tspOpen
            // 
            this.tspOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspOpen.Image = ((System.Drawing.Image)(resources.GetObject("tspOpen.Image")));
            this.tspOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspOpen.Name = "tspOpen";
            this.tspOpen.Size = new System.Drawing.Size(23, 22);
            this.tspOpen.Text = "Open File";
            this.tspOpen.Click += new System.EventHandler(this.tspOpen_Click);
            // 
            // tspSpellCheck
            // 
            this.tspSpellCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspSpellCheck.Image = ((System.Drawing.Image)(resources.GetObject("tspSpellCheck.Image")));
            this.tspSpellCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSpellCheck.Name = "tspSpellCheck";
            this.tspSpellCheck.Size = new System.Drawing.Size(23, 22);
            this.tspSpellCheck.Text = "Check Spelling";
            this.tspSpellCheck.Click += new System.EventHandler(this.tspSpellCheck_Click);
            // 
            // tspExit
            // 
            this.tspExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspExit.Image = ((System.Drawing.Image)(resources.GetObject("tspExit.Image")));
            this.tspExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExit.Name = "tspExit";
            this.tspExit.Size = new System.Drawing.Size(23, 22);
            this.tspExit.Text = "Exit Application";
            this.tspExit.Click += new System.EventHandler(this.tspExit_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "openFileDialog1";
            // 
            // spellCheckTextControl1
            // 
            this.spellCheckTextControl1.AlwaysSuggest = true;
            this.spellCheckTextControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellCheckTextControl1.IgnoreUpperCase = true;
            this.spellCheckTextControl1.Location = new System.Drawing.Point(0, 25);
            this.spellCheckTextControl1.Name = "spellCheckTextControl1";
            this.spellCheckTextControl1.Size = new System.Drawing.Size(460, 350);
            this.spellCheckTextControl1.TabIndex = 2;
            this.spellCheckTextControl1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 375);
            this.Controls.Add(this.spellCheckTextControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Test Spell Check Control";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspSpellCheck;
        private System.Windows.Forms.ToolStripButton tspExit;
        private SpellCheckControl.SpellCheckTextControl spellCheckTextControl1;
        private System.Windows.Forms.ToolStripButton tspSave;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.ToolStripButton tspOpen;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
    }
}

