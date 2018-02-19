namespace PickersDemo
{
    partial class HorizontalAlignmentUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HorizontalAlignmentUI));
            this.toolStripAlignments = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripAlignments.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripAlignments
            // 
            this.toolStripAlignments.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.toolStripAlignments.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripAlignments.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripAlignments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLeft,
            this.toolStripSeparator1,
            this.toolStripButtonCenter,
            this.toolStripSeparator2,
            this.toolStripButtonRight});
            this.toolStripAlignments.Location = new System.Drawing.Point(0, 0);
            this.toolStripAlignments.Name = "toolStripAlignments";
            this.toolStripAlignments.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripAlignments.Size = new System.Drawing.Size(207, 25);
            this.toolStripAlignments.TabIndex = 0;
            // 
            // toolStripButtonLeft
            // 
            this.toolStripButtonLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLeft.Image")));
            this.toolStripButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLeft.Name = "toolStripButtonLeft";
            this.toolStripButtonLeft.Size = new System.Drawing.Size(46, 22);
            this.toolStripButtonLeft.Text = "Left";
            this.toolStripButtonLeft.Click += new System.EventHandler(this.toolStripButtonLeft_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCenter
            // 
            this.toolStripButtonCenter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCenter.Image")));
            this.toolStripButtonCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCenter.Name = "toolStripButtonCenter";
            this.toolStripButtonCenter.Size = new System.Drawing.Size(63, 22);
            this.toolStripButtonCenter.Text = " Center";
            this.toolStripButtonCenter.Click += new System.EventHandler(this.toolStripButtonCenter_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRight
            // 
            this.toolStripButtonRight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRight.Image")));
            this.toolStripButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRight.Name = "toolStripButtonRight";
            this.toolStripButtonRight.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonRight.Text = "Right";
            this.toolStripButtonRight.Click += new System.EventHandler(this.toolStripButtonRight_Click);
            // 
            // HorizontalAlignmentUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripAlignments);
            this.Name = "HorizontalAlignmentUI";
            this.Size = new System.Drawing.Size(175, 25);
            this.toolStripAlignments.ResumeLayout(false);
            this.toolStripAlignments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripAlignments;
        private System.Windows.Forms.ToolStripButton toolStripButtonLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonRight;

    }
}
