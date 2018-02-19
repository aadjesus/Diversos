namespace PropGirdExTest
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            this.propertyGridEx1 = new href.Controls.PropGridEx.PropertyGridEx();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView1 = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // propertyGridEx1
            // 
            this.propertyGridEx1.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.propertyGridEx1.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridEx1.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.propertyGridEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridEx1.DrawFlat = true;
            this.propertyGridEx1.Location = new System.Drawing.Point(310, 0);
            this.propertyGridEx1.Name = "propertyGridEx1";
            this.propertyGridEx1.Size = new System.Drawing.Size(262, 368);
            this.propertyGridEx1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(307, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 368);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(307, 368);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Type";
            columnHeader1.Width = 118;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Description";
            columnHeader2.Width = 180;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 368);
            this.Controls.Add(this.propertyGridEx1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "PropertyGridEx Test Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private href.Controls.PropGridEx.PropertyGridEx propertyGridEx1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView listView1;
    }
}

