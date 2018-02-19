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
            this.multiPaneControl1 = new Kerido.Controls.MultiPaneControl();
            this.multiPanePage1 = new Kerido.Controls.MultiPanePage();
            this.button2 = new System.Windows.Forms.Button();
            this.multiPanePage2 = new Kerido.Controls.MultiPanePage();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.multiPanePage3 = new Kerido.Controls.MultiPanePage();
            this.multiPaneControl1.SuspendLayout();
            this.multiPanePage1.SuspendLayout();
            this.multiPanePage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // multiPaneControl1
            // 
            this.multiPaneControl1.BackColor = System.Drawing.Color.Transparent;
            this.multiPaneControl1.Controls.Add(this.multiPanePage1);
            this.multiPaneControl1.Controls.Add(this.multiPanePage2);
            this.multiPaneControl1.Controls.Add(this.multiPanePage3);
            this.multiPaneControl1.Location = new System.Drawing.Point(74, 29);
            this.multiPaneControl1.Name = "multiPaneControl1";
            this.multiPaneControl1.SelectedPage = this.multiPanePage1;
            this.multiPaneControl1.Size = new System.Drawing.Size(200, 100);
            this.multiPaneControl1.TabIndex = 0;
            this.multiPaneControl1.Text = "multiPaneControl1";
            // 
            // multiPanePage1
            // 
            this.multiPanePage1.Controls.Add(this.button2);
            this.multiPanePage1.Name = "multiPanePage1";
            this.multiPanePage1.Size = new System.Drawing.Size(200, 100);
            this.multiPanePage1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // multiPanePage2
            // 
            this.multiPanePage2.Controls.Add(this.checkBox2);
            this.multiPanePage2.Controls.Add(this.checkBox1);
            this.multiPanePage2.Controls.Add(this.button1);
            this.multiPanePage2.Name = "multiPanePage2";
            this.multiPanePage2.Size = new System.Drawing.Size(200, 100);
            this.multiPanePage2.TabIndex = 1;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(97, 25);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // multiPanePage3
            // 
            this.multiPanePage3.Name = "multiPanePage3";
            this.multiPanePage3.Size = new System.Drawing.Size(200, 100);
            this.multiPanePage3.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 310);
            this.Controls.Add(this.multiPaneControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.multiPaneControl1.ResumeLayout(false);
            this.multiPanePage1.ResumeLayout(false);
            this.multiPanePage2.ResumeLayout(false);
            this.multiPanePage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Kerido.Controls.MultiPaneControl multiPaneControl1;
        private Kerido.Controls.MultiPanePage multiPanePage1;
        private System.Windows.Forms.Button button2;
        private Kerido.Controls.MultiPanePage multiPanePage2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private Kerido.Controls.MultiPanePage multiPanePage3;
    }
}

