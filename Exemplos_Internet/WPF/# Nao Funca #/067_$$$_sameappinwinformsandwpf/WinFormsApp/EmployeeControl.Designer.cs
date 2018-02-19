namespace WinFormsApp
{
    partial class EmployeeControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.employeePicture = new System.Windows.Forms.PictureBox();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.startDateLabel = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fullNameLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.employeePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(115, 44);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(71, 13);
            label1.TabIndex = 0;
            label1.Text = "First Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(115, 74);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(71, 13);
            label2.TabIndex = 2;
            label2.Text = "Last Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(117, 103);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(69, 13);
            label3.TabIndex = 4;
            label3.Text = "Start Date:";
            // 
            // employeePicture
            // 
            this.employeePicture.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.employeeBindingSource, "PictureFile", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.employeePicture.Location = new System.Drawing.Point(3, 3);
            this.employeePicture.Name = "employeePicture";
            this.employeePicture.Size = new System.Drawing.Size(96, 125);
            this.employeePicture.TabIndex = 0;
            this.employeePicture.TabStop = false;
            // 
            // employeeBindingSource
            // 
            this.employeeBindingSource.DataSource = typeof(BusinessObjects.Employee);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.employeeBindingSource, "StartDate", true));
            this.startDateLabel.Location = new System.Drawing.Point(192, 103);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(53, 13);
            this.startDateLabel.TabIndex = 5;
            this.startDateLabel.Text = "1/1/0001";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.employeeBindingSource, "FirstName", true));
            this.firstNameTextBox.Location = new System.Drawing.Point(195, 41);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(149, 20);
            this.firstNameTextBox.TabIndex = 1;
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.employeeBindingSource, "LastName", true));
            this.lastNameTextBox.Location = new System.Drawing.Point(195, 71);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(149, 20);
            this.lastNameTextBox.TabIndex = 3;
            // 
            // fullNameLabel
            // 
            this.fullNameLabel.AutoSize = true;
            this.fullNameLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.employeeBindingSource, "FullName", true));
            this.fullNameLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.fullNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullNameLabel.Location = new System.Drawing.Point(261, 0);
            this.fullNameLabel.Name = "fullNameLabel";
            this.fullNameLabel.Size = new System.Drawing.Size(91, 20);
            this.fullNameLabel.TabIndex = 6;
            this.fullNameLabel.Text = "Doe, John";
            // 
            // EmployeeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.fullNameLabel);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.employeePicture);
            this.Name = "EmployeeControl";
            this.Size = new System.Drawing.Size(352, 131);
            ((System.ComponentModel.ISupportInitialize)(this.employeePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox employeePicture;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.BindingSource employeeBindingSource;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label fullNameLabel;
    }
}
