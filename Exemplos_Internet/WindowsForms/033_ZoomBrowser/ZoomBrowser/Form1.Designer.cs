namespace ZoomBrowser
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ZoomValue = new System.Windows.Forms.ToolStripComboBox();
            this.myBrowser1 = new ZoomBrowser.MyBrowser();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ZoomValue});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(920, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Zoom";
            // 
            // ZoomValue
            // 
            this.ZoomValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ZoomValue.Items.AddRange(new object[] {
            "50",
            "75",
            "100",
            "125",
            "150",
            "175",
            "200"});
            this.ZoomValue.Name = "ZoomValue";
            this.ZoomValue.Size = new System.Drawing.Size(121, 25);
            this.ZoomValue.SelectedIndexChanged += new System.EventHandler(this.ZoomValue_SelectedIndexChanged);
            // 
            // myBrowser1
            // 
            this.myBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myBrowser1.Location = new System.Drawing.Point(0, 25);
            this.myBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.myBrowser1.Name = "myBrowser1";
            this.myBrowser1.Size = new System.Drawing.Size(920, 344);
            this.myBrowser1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 369);
            this.Controls.Add(this.myBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Zoom Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private MyBrowser myBrowser1;
        private System.Windows.Forms.ToolStripComboBox ZoomValue;
    }
}

