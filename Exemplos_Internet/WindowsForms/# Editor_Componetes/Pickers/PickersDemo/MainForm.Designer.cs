namespace PickersDemo
{
    partial class MainForm
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
            this.groupBoxColorPicker = new System.Windows.Forms.GroupBox();
            this.appearanceChanger1 = new PickersDemo.AppearanceChanger();
            this.colorPicker1 = new Pickers.ColorPicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dockStylePicker1 = new Pickers.DockStylePicker();
            this.appearanceChanger2 = new PickersDemo.AppearanceChanger();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.contentAlignmentPicker1 = new Pickers.ContentAlignmentPicker();
            this.appearanceChanger3 = new PickersDemo.AppearanceChanger();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.horizontalAlignmentPicker1 = new PickersDemo.HorizontalAlignmentPicker();
            this.appearanceChanger4 = new PickersDemo.AppearanceChanger();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.basicTabPage = new System.Windows.Forms.TabPage();
            this.dgvTabPage = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gridTabPage = new System.Windows.Forms.TabPage();
            this.groupBoxColorPicker.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.basicTabPage.SuspendLayout();
            this.dgvTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxColorPicker
            // 
            this.groupBoxColorPicker.Controls.Add(this.appearanceChanger1);
            this.groupBoxColorPicker.Controls.Add(this.colorPicker1);
            this.groupBoxColorPicker.Location = new System.Drawing.Point(6, 6);
            this.groupBoxColorPicker.Name = "groupBoxColorPicker";
            this.groupBoxColorPicker.Size = new System.Drawing.Size(200, 152);
            this.groupBoxColorPicker.TabIndex = 0;
            this.groupBoxColorPicker.TabStop = false;
            this.groupBoxColorPicker.Text = "ColorPicker";
            // 
            // appearanceChanger1
            // 
            this.appearanceChanger1.BuddyPicker = this.colorPicker1;
            this.appearanceChanger1.Location = new System.Drawing.Point(7, 49);
            this.appearanceChanger1.Name = "appearanceChanger1";
            this.appearanceChanger1.Size = new System.Drawing.Size(130, 97);
            this.appearanceChanger1.TabIndex = 1;
            // 
            // colorPicker1
            // 
            this.colorPicker1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPicker1.Location = new System.Drawing.Point(7, 21);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(187, 21);
            this.colorPicker1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dockStylePicker1);
            this.groupBox1.Controls.Add(this.appearanceChanger2);
            this.groupBox1.Location = new System.Drawing.Point(212, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 152);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DockStylePicker";
            // 
            // dockStylePicker1
            // 
            this.dockStylePicker1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dockStylePicker1.Location = new System.Drawing.Point(7, 22);
            this.dockStylePicker1.Name = "dockStylePicker1";
            this.dockStylePicker1.Size = new System.Drawing.Size(187, 21);
            this.dockStylePicker1.TabIndex = 0;
            // 
            // appearanceChanger2
            // 
            this.appearanceChanger2.BuddyPicker = this.dockStylePicker1;
            this.appearanceChanger2.Location = new System.Drawing.Point(7, 49);
            this.appearanceChanger2.Name = "appearanceChanger2";
            this.appearanceChanger2.Size = new System.Drawing.Size(130, 97);
            this.appearanceChanger2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.contentAlignmentPicker1);
            this.groupBox2.Controls.Add(this.appearanceChanger3);
            this.groupBox2.Location = new System.Drawing.Point(6, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 152);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ContentAlignmentPicker";
            // 
            // contentAlignmentPicker1
            // 
            this.contentAlignmentPicker1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.contentAlignmentPicker1.Location = new System.Drawing.Point(7, 21);
            this.contentAlignmentPicker1.Name = "contentAlignmentPicker1";
            this.contentAlignmentPicker1.Size = new System.Drawing.Size(187, 21);
            this.contentAlignmentPicker1.TabIndex = 0;
            // 
            // appearanceChanger3
            // 
            this.appearanceChanger3.BuddyPicker = this.contentAlignmentPicker1;
            this.appearanceChanger3.Location = new System.Drawing.Point(7, 49);
            this.appearanceChanger3.Name = "appearanceChanger3";
            this.appearanceChanger3.Size = new System.Drawing.Size(130, 97);
            this.appearanceChanger3.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.horizontalAlignmentPicker1);
            this.groupBox3.Controls.Add(this.appearanceChanger4);
            this.groupBox3.Location = new System.Drawing.Point(212, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 152);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HorizontalAlignmentPicker (custom)";
            // 
            // horizontalAlignmentPicker1
            // 
            this.horizontalAlignmentPicker1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.horizontalAlignmentPicker1.Location = new System.Drawing.Point(7, 21);
            this.horizontalAlignmentPicker1.Name = "horizontalAlignmentPicker1";
            this.horizontalAlignmentPicker1.Size = new System.Drawing.Size(187, 21);
            this.horizontalAlignmentPicker1.TabIndex = 0;
            // 
            // appearanceChanger4
            // 
            this.appearanceChanger4.BuddyPicker = this.horizontalAlignmentPicker1;
            this.appearanceChanger4.Location = new System.Drawing.Point(7, 49);
            this.appearanceChanger4.Name = "appearanceChanger4";
            this.appearanceChanger4.Size = new System.Drawing.Size(130, 97);
            this.appearanceChanger4.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.basicTabPage);
            this.tabControl1.Controls.Add(this.dgvTabPage);
            this.tabControl1.Controls.Add(this.gridTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 349);
            this.tabControl1.TabIndex = 0;
            // 
            // basicTabPage
            // 
            this.basicTabPage.Controls.Add(this.groupBoxColorPicker);
            this.basicTabPage.Controls.Add(this.groupBox3);
            this.basicTabPage.Controls.Add(this.groupBox1);
            this.basicTabPage.Controls.Add(this.groupBox2);
            this.basicTabPage.Location = new System.Drawing.Point(4, 22);
            this.basicTabPage.Name = "basicTabPage";
            this.basicTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.basicTabPage.Size = new System.Drawing.Size(421, 323);
            this.basicTabPage.TabIndex = 0;
            this.basicTabPage.Text = "Basic Demo";
            this.basicTabPage.UseVisualStyleBackColor = true;
            // 
            // dgvTabPage
            // 
            this.dgvTabPage.Controls.Add(this.dataGridView1);
            this.dgvTabPage.Location = new System.Drawing.Point(4, 22);
            this.dgvTabPage.Name = "dgvTabPage";
            this.dgvTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dgvTabPage.Size = new System.Drawing.Size(421, 323);
            this.dgvTabPage.TabIndex = 1;
            this.dgvTabPage.Text = "DataGridView Demo";
            this.dgvTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(408, 310);
            this.dataGridView1.TabIndex = 0;
            // 
            // gridTabPage
            // 
            this.gridTabPage.Location = new System.Drawing.Point(4, 22);
            this.gridTabPage.Name = "gridTabPage";
            this.gridTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.gridTabPage.Size = new System.Drawing.Size(421, 323);
            this.gridTabPage.TabIndex = 2;
            this.gridTabPage.Text = "SourceGrid Demo";
            this.gridTabPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(453, 373);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Pickers Library Demo";
            this.groupBoxColorPicker.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.basicTabPage.ResumeLayout(false);
            this.dgvTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxColorPicker;
        private Pickers.ColorPicker colorPicker1;
        private AppearanceChanger appearanceChanger1;
        private System.Windows.Forms.GroupBox groupBox1;
        private AppearanceChanger appearanceChanger2;
        private Pickers.DockStylePicker dockStylePicker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Pickers.ContentAlignmentPicker contentAlignmentPicker1;
        private AppearanceChanger appearanceChanger3;
        private System.Windows.Forms.GroupBox groupBox3;
        private AppearanceChanger appearanceChanger4;
        private HorizontalAlignmentPicker horizontalAlignmentPicker1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage basicTabPage;
        private System.Windows.Forms.TabPage dgvTabPage;
        private System.Windows.Forms.TabPage gridTabPage;
        private System.Windows.Forms.DataGridView dataGridView1;


    }
}

