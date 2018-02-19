namespace UsersList
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
            this.btn_dp = new System.Windows.Forms.Button();
            this.list_projects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.list_users = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btn_dp
            // 
            this.btn_dp.Location = new System.Drawing.Point(22, 13);
            this.btn_dp.Name = "btn_dp";
            this.btn_dp.Size = new System.Drawing.Size(121, 23);
            this.btn_dp.TabIndex = 0;
            this.btn_dp.Text = "Domain Picker";
            this.btn_dp.UseVisualStyleBackColor = true;
            this.btn_dp.Click += new System.EventHandler(this.btn_dp_Click);
            // 
            // list_projects
            // 
            this.list_projects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_projects.FormattingEnabled = true;
            this.list_projects.Location = new System.Drawing.Point(22, 62);
            this.list_projects.Name = "list_projects";
            this.list_projects.Size = new System.Drawing.Size(496, 21);
            this.list_projects.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project List:";
            // 
            // list_users
            // 
            this.list_users.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_users.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.list_users.FullRowSelect = true;
            this.list_users.GridLines = true;
            this.list_users.Location = new System.Drawing.Point(22, 89);
            this.list_users.Name = "list_users";
            this.list_users.Size = new System.Drawing.Size(496, 239);
            this.list_users.TabIndex = 4;
            this.list_users.UseCompatibleStateImageBehavior = false;
            this.list_users.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Display Name";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Account Name";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Email";
            this.columnHeader3.Width = 119;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Sid";
            this.columnHeader4.Width = 179;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 340);
            this.Controls.Add(this.list_users);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.list_projects);
            this.Controls.Add(this.btn_dp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_dp;
        private System.Windows.Forms.ComboBox list_projects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView list_users;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

