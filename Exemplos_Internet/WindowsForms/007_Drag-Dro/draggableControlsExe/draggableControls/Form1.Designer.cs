namespace draggableControls
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("I am movable List view");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("I am movable tree");
            this.designMode = new System.Windows.Forms.Button();
            this.controlMode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.dragControl1 = new draggableControls.Resources.DragControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // designMode
            // 
            this.designMode.BackColor = System.Drawing.Color.SpringGreen;
            this.designMode.Location = new System.Drawing.Point(12, 12);
            this.designMode.Name = "designMode";
            this.designMode.Size = new System.Drawing.Size(89, 23);
            this.designMode.TabIndex = 0;
            this.designMode.Text = "Design Mode";
            this.designMode.UseVisualStyleBackColor = false;
            this.designMode.Click += new System.EventHandler(this.designMode_Click);
            // 
            // controlMode
            // 
            this.controlMode.BackColor = System.Drawing.Color.Crimson;
            this.controlMode.Location = new System.Drawing.Point(441, 12);
            this.controlMode.Name = "controlMode";
            this.controlMode.Size = new System.Drawing.Size(89, 23);
            this.controlMode.TabIndex = 1;
            this.controlMode.Text = "Control Mode";
            this.controlMode.UseVisualStyleBackColor = false;
            this.controlMode.Click += new System.EventHandler(this.controlMode_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(12, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "I am movable Text box";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Yellow;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "I am movable Text Box"});
            this.listBox1.Location = new System.Drawing.Point(381, 432);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(134, 69);
            this.listBox1.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.YellowGreen;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(12, 155);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(198, 95);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BackColor = System.Drawing.Color.GreenYellow;
            this.maskedTextBox1.Location = new System.Drawing.Point(332, 396);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(183, 20);
            this.maskedTextBox1.TabIndex = 5;
            this.maskedTextBox1.Text = "I am draggable Masked Text Box";
            this.maskedTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox1_MaskInputRejected);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.numericUpDown1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.numericUpDown1.Location = new System.Drawing.Point(15, 439);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::draggableControls.Properties.Resources.www_gruplar_info__706;
            this.pictureBox1.Location = new System.Drawing.Point(340, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 122);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.HotPink;
            this.progressBar1.Location = new System.Drawing.Point(183, 460);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(144, 32);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.UseWaitCursor = true;
            this.progressBar1.Value = 45;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Cyan;
            this.radioButton1.Location = new System.Drawing.Point(16, 279);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(152, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "I am movable Radio button";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.richTextBox1.Location = new System.Drawing.Point(15, 342);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(153, 49);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "I am movable Rich Text Box control";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.treeView1.Location = new System.Drawing.Point(352, 279);
            this.treeView1.Name = "treeView1";
            treeNode2.Name = "Node0";
            treeNode2.Text = "I am movable tree";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView1.Size = new System.Drawing.Size(166, 97);
            this.treeView1.TabIndex = 12;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "I am movable";
            // 
            // dragControl1
            // 
            this.dragControl1.Location = new System.Drawing.Point(596, 48);
            this.dragControl1.Name = "dragControl1";
            this.dragControl1.Size = new System.Drawing.Size(40, 22);
            this.dragControl1.TabIndex = 14;
            this.dragControl1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 4);
            this.panel1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Cornsilk;
            this.label2.Location = new System.Drawing.Point(122, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Click on Design Mode -> Then Drag and drop any control :)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(542, 513);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dragControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.controlMode);
            this.Controls.Add(this.designMode);
            this.Name = "Form1";
            this.Text = "Controls Dragging Examble";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button designMode;
        private System.Windows.Forms.Button controlMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TreeView treeView1;
        private draggableControls.Resources.DragControl dragControl1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}

