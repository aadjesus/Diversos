namespace RGB2RGBA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.buttonLoadMask = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxOpacityThreshold = new System.Windows.Forms.TextBox();
            this.labelOpacityThreshold = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBoxAllowPartialOpacity = new System.Windows.Forms.CheckBox();
            this.checkBoxInvertMask = new System.Windows.Forms.CheckBox();
            this.checkBoxLoadedImageAsMask = new System.Windows.Forms.CheckBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonSaveMask = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ButtonSave, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(338, 438);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.buttonLoadImage, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonLoadMask, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSaveMask, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(338, 30);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLoadImage.Location = new System.Drawing.Point(3, 3);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(163, 24);
            this.buttonLoadImage.TabIndex = 0;
            this.buttonLoadImage.Text = "Load Image";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.buttonLoadImage_Click);
            // 
            // buttonLoadMask
            // 
            this.buttonLoadMask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLoadMask.Enabled = false;
            this.buttonLoadMask.Location = new System.Drawing.Point(172, 3);
            this.buttonLoadMask.Name = "buttonLoadMask";
            this.buttonLoadMask.Size = new System.Drawing.Size(78, 24);
            this.buttonLoadMask.TabIndex = 1;
            this.buttonLoadMask.Text = "Load Mask";
            this.buttonLoadMask.UseVisualStyleBackColor = true;
            this.buttonLoadMask.Click += new System.EventHandler(this.buttonLoadMask_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxOpacityThreshold);
            this.groupBox1.Controls.Add(this.labelOpacityThreshold);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.checkBoxAllowPartialOpacity);
            this.groupBox1.Controls.Add(this.checkBoxInvertMask);
            this.groupBox1.Controls.Add(this.checkBoxLoadedImageAsMask);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 74);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // textBoxOpacityThreshold
            // 
            this.textBoxOpacityThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxOpacityThreshold.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxOpacityThreshold.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxOpacityThreshold.Location = new System.Drawing.Point(163, 51);
            this.textBoxOpacityThreshold.Name = "textBoxOpacityThreshold";
            this.textBoxOpacityThreshold.ReadOnly = true;
            this.textBoxOpacityThreshold.Size = new System.Drawing.Size(100, 13);
            this.textBoxOpacityThreshold.TabIndex = 5;
            this.textBoxOpacityThreshold.Text = "Opacity Threshold";
            // 
            // labelOpacityThreshold
            // 
            this.labelOpacityThreshold.AutoSize = true;
            this.labelOpacityThreshold.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelOpacityThreshold.Location = new System.Drawing.Point(160, 55);
            this.labelOpacityThreshold.Name = "labelOpacityThreshold";
            this.labelOpacityThreshold.Size = new System.Drawing.Size(0, 13);
            this.labelOpacityThreshold.TabIndex = 4;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 32;
            this.trackBar1.Location = new System.Drawing.Point(131, 28);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(149, 42);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickFrequency = 32;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 128;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // checkBoxAllowPartialOpacity
            // 
            this.checkBoxAllowPartialOpacity.AutoSize = true;
            this.checkBoxAllowPartialOpacity.Location = new System.Drawing.Point(6, 28);
            this.checkBoxAllowPartialOpacity.Name = "checkBoxAllowPartialOpacity";
            this.checkBoxAllowPartialOpacity.Size = new System.Drawing.Size(122, 17);
            this.checkBoxAllowPartialOpacity.TabIndex = 1;
            this.checkBoxAllowPartialOpacity.Text = "Allow Partial Opacity";
            this.checkBoxAllowPartialOpacity.UseVisualStyleBackColor = true;
            this.checkBoxAllowPartialOpacity.CheckedChanged += new System.EventHandler(this.checkBoxPartialOpacity_CheckedChanged);
            // 
            // checkBoxInvertMask
            // 
            this.checkBoxInvertMask.AutoSize = true;
            this.checkBoxInvertMask.Checked = true;
            this.checkBoxInvertMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInvertMask.Location = new System.Drawing.Point(6, 51);
            this.checkBoxInvertMask.Name = "checkBoxInvertMask";
            this.checkBoxInvertMask.Size = new System.Drawing.Size(82, 17);
            this.checkBoxInvertMask.TabIndex = 3;
            this.checkBoxInvertMask.Text = "Invert Mask";
            this.checkBoxInvertMask.UseVisualStyleBackColor = true;
            this.checkBoxInvertMask.CheckedChanged += new System.EventHandler(this.checkBoxInvertMask_CheckedChanged);
            // 
            // checkBoxLoadedImageAsMask
            // 
            this.checkBoxLoadedImageAsMask.AutoSize = true;
            this.checkBoxLoadedImageAsMask.Checked = true;
            this.checkBoxLoadedImageAsMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLoadedImageAsMask.Location = new System.Drawing.Point(6, 9);
            this.checkBoxLoadedImageAsMask.Name = "checkBoxLoadedImageAsMask";
            this.checkBoxLoadedImageAsMask.Size = new System.Drawing.Size(157, 17);
            this.checkBoxLoadedImageAsMask.TabIndex = 0;
            this.checkBoxLoadedImageAsMask.Text = "Use Loaded image as mask";
            this.checkBoxLoadedImageAsMask.UseVisualStyleBackColor = true;
            this.checkBoxLoadedImageAsMask.CheckedChanged += new System.EventHandler(this.checkBoxUseLoadedImageAsMask_CheckedChanged);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSave.Location = new System.Drawing.Point(3, 411);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(332, 24);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Save Image (with Alpha Layer)";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(3, 262);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(332, 143);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(332, 143);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(169, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(160, 137);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 137);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // buttonSaveMask
            // 
            this.buttonSaveMask.Location = new System.Drawing.Point(256, 3);
            this.buttonSaveMask.Name = "buttonSaveMask";
            this.buttonSaveMask.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveMask.TabIndex = 2;
            this.buttonSaveMask.Text = "Save Mask";
            this.buttonSaveMask.UseVisualStyleBackColor = true;
            this.buttonSaveMask.Click += new System.EventHandler(this.buttonSaveMask_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 438);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Alpha Mask Editor 1.0";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.Button buttonLoadMask;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox checkBoxLoadedImageAsMask;
        private System.Windows.Forms.CheckBox checkBoxInvertMask;
        private System.Windows.Forms.CheckBox checkBoxAllowPartialOpacity;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelOpacityThreshold;
        private System.Windows.Forms.TextBox textBoxOpacityThreshold;
        private System.Windows.Forms.Button buttonSaveMask;
    }
}

