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
            this.expando1 = new XPExplorerBar.Expando();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.expando1)).BeginInit();
            this.expando1.SuspendLayout();
            this.SuspendLayout();
            // 
            // expando1
            // 
            this.expando1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.expando1.Items.AddRange(new System.Windows.Forms.Control[] {
            this.checkBox1});
            this.expando1.Location = new System.Drawing.Point(53, 51);
            this.expando1.Name = "expando1";
            this.expando1.Size = new System.Drawing.Size(186, 100);
            this.expando1.TabIndex = 0;
            this.expando1.Text = "expando1";
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(245, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.expando1;
            this.propertyGrid1.Size = new System.Drawing.Size(205, 413);
            this.propertyGrid1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 454);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.expando1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.expando1)).EndInit();
            this.expando1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private XPExplorerBar.Expando expando1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;



    }
}

