namespace GenericForm
{
    partial class DemoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.crtObject = new System.Windows.Forms.Button();
            this.edtObject = new System.Windows.Forms.Button();
            this.crtArrayObject = new System.Windows.Forms.Button();
            this.edtArrayObject = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Assembly Path";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(390, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(505, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(20, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(479, 290);
            this.listBox1.TabIndex = 3;
            // 
            // crtObject
            // 
            this.crtObject.Location = new System.Drawing.Point(505, 50);
            this.crtObject.Name = "crtObject";
            this.crtObject.Size = new System.Drawing.Size(74, 26);
            this.crtObject.TabIndex = 4;
            this.crtObject.Text = "Create";
            this.crtObject.UseVisualStyleBackColor = true;
            this.crtObject.Click += new System.EventHandler(this.crtObject_Click);
            // 
            // edtObject
            // 
            this.edtObject.Location = new System.Drawing.Point(505, 86);
            this.edtObject.Name = "edtObject";
            this.edtObject.Size = new System.Drawing.Size(74, 26);
            this.edtObject.TabIndex = 5;
            this.edtObject.Text = "Edit";
            this.edtObject.UseVisualStyleBackColor = true;
            this.edtObject.Click += new System.EventHandler(this.edtObject_Click);
            // 
            // crtArrayObject
            // 
            this.crtArrayObject.Location = new System.Drawing.Point(505, 122);
            this.crtArrayObject.Name = "crtArrayObject";
            this.crtArrayObject.Size = new System.Drawing.Size(74, 26);
            this.crtArrayObject.TabIndex = 6;
            this.crtArrayObject.Text = "Create Array";
            this.crtArrayObject.UseVisualStyleBackColor = true;
            this.crtArrayObject.Click += new System.EventHandler(this.crtArrayObject_Click);
            // 
            // edtArrayObject
            // 
            this.edtArrayObject.Location = new System.Drawing.Point(505, 158);
            this.edtArrayObject.Name = "edtArrayObject";
            this.edtArrayObject.Size = new System.Drawing.Size(74, 26);
            this.edtArrayObject.TabIndex = 7;
            this.edtArrayObject.Text = "Edit Array";
            this.edtArrayObject.UseVisualStyleBackColor = true;
            this.edtArrayObject.Click += new System.EventHandler(this.edtArrayObject_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(505, 201);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 27);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(588, 351);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.edtArrayObject);
            this.Controls.Add(this.crtArrayObject);
            this.Controls.Add(this.edtObject);
            this.Controls.Add(this.crtObject);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "DemoForm";
            this.Text = "Demo Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button crtObject;
        private System.Windows.Forms.Button edtObject;
        private System.Windows.Forms.Button crtArrayObject;
        private System.Windows.Forms.Button edtArrayObject;
        private System.Windows.Forms.Button btnClose;

    }
}