namespace TestMultiPanel
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
            this.multiPanel1 = new Liron.Windows.Forms.MultiPanel();
            this.pageCheckboxes = new Liron.Windows.Forms.MultiPanelPage();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pageComboBoxes = new Liron.Windows.Forms.MultiPanelPage();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pageButtons = new Liron.Windows.Forms.MultiPanelPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.multiPanel1.SuspendLayout();
            this.pageCheckboxes.SuspendLayout();
            this.pageComboBoxes.SuspendLayout();
            this.pageButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // multiPanel1
            // 
            this.multiPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.multiPanel1.Controls.Add(this.pageCheckboxes);
            this.multiPanel1.Controls.Add(this.pageComboBoxes);
            this.multiPanel1.Controls.Add(this.pageButtons);
            this.multiPanel1.Location = new System.Drawing.Point(25, 12);
            this.multiPanel1.Name = "multiPanel1";
            this.multiPanel1.SelectedPage = this.pageButtons;
            this.multiPanel1.Size = new System.Drawing.Size(267, 127);
            this.multiPanel1.TabIndex = 0;
            // 
            // pageCheckboxes
            // 
            this.pageCheckboxes.Controls.Add(this.checkBox2);
            this.pageCheckboxes.Controls.Add(this.checkBox1);
            this.pageCheckboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageCheckboxes.Location = new System.Drawing.Point(0, 0);
            this.pageCheckboxes.Name = "pageCheckboxes";
            this.pageCheckboxes.Size = new System.Drawing.Size(265, 125);
            this.pageCheckboxes.TabIndex = 0;
            this.pageCheckboxes.Text = "checkboxes";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(139, 59);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(33, 41);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pageComboBoxes
            // 
            this.pageComboBoxes.Controls.Add(this.comboBox2);
            this.pageComboBoxes.Controls.Add(this.comboBox1);
            this.pageComboBoxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageComboBoxes.Location = new System.Drawing.Point(0, 0);
            this.pageComboBoxes.Name = "pageComboBoxes";
            this.pageComboBoxes.Size = new System.Drawing.Size(265, 125);
            this.pageComboBoxes.TabIndex = 1;
            this.pageComboBoxes.Text = "combo boxes";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(19, 75);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(19, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // pageButtons
            // 
            this.pageButtons.Controls.Add(this.button2);
            this.pageButtons.Controls.Add(this.button1);
            this.pageButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageButtons.Location = new System.Drawing.Point(0, 0);
            this.pageButtons.Name = "pageButtons";
            this.pageButtons.Size = new System.Drawing.Size(265, 125);
            this.pageButtons.TabIndex = 2;
            this.pageButtons.Text = "buttons";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Buttons",
            "Checkboxes",
            "Combo boxes"});
            this.comboBox3.Location = new System.Drawing.Point(111, 161);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(180, 21);
            this.comboBox3.TabIndex = 1;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose view:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 204);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.multiPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.multiPanel1.ResumeLayout(false);
            this.pageCheckboxes.ResumeLayout(false);
            this.pageCheckboxes.PerformLayout();
            this.pageComboBoxes.ResumeLayout(false);
            this.pageButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Liron.Windows.Forms.MultiPanel multiPanel1;
        private Liron.Windows.Forms.MultiPanelPage pageCheckboxes;
        private Liron.Windows.Forms.MultiPanelPage pageComboBoxes;
        private Liron.Windows.Forms.MultiPanelPage pageButtons;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label1;
    }
}

