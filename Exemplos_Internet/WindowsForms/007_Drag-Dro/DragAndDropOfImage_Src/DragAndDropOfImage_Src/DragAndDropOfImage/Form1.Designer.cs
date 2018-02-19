namespace DragAndDropOfImage
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.AllowDrop = true;
            this.panel4.BackgroundImage = global::DragAndDropOfImage.Properties.Resources._4;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(336, 275);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(229, 148);
            this.panel4.TabIndex = 1;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel4.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_DragDrop);
            this.panel4.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_DragEnter);
            // 
            // panel3
            // 
            this.panel3.AllowDrop = true;
            this.panel3.BackgroundImage = global::DragAndDropOfImage.Properties.Resources._3;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(51, 275);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(229, 148);
            this.panel3.TabIndex = 1;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel3.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_DragDrop);
            this.panel3.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_DragEnter);
            // 
            // panel2
            // 
            this.panel2.AllowDrop = true;
            this.panel2.BackgroundImage = global::DragAndDropOfImage.Properties.Resources._2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(336, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(229, 148);
            this.panel2.TabIndex = 1;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_DragDrop);
            this.panel2.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_DragEnter);
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackgroundImage = global::DragAndDropOfImage.Properties.Resources._1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(51, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 148);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_DragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 559);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}

