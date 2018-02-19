namespace CrazyCombos
{
    partial class frmCrazy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrazy));
            this.label1 = new System.Windows.Forms.Label();
            this.ilCrazy = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cboColourCrazy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCenterCrazy = new System.Windows.Forms.ComboBox();
            this.btnRightText = new System.Windows.Forms.Button();
            this.btnLeftAlignScroll = new System.Windows.Forms.Button();
            this.btnLeftAlignButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboFontsCrazy = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.imgCboCrazy = new System.Windows.Forms.ImgCbo();
            this.lineCboCrazy = new System.Windows.Forms.LineCbo();
            this.cboAlignAllCrazy = new System.Windows.Forms.ComboAlignSettings();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Image ComboBox";
            // 
            // ilCrazy
            // 
            this.ilCrazy.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCrazy.ImageStream")));
            this.ilCrazy.TransparentColor = System.Drawing.Color.Transparent;
            this.ilCrazy.Images.SetKeyName(0, "Blue hills.jpg");
            this.ilCrazy.Images.SetKeyName(1, "Sunset.jpg");
            this.ilCrazy.Images.SetKeyName(2, "Water lilies.jpg");
            this.ilCrazy.Images.SetKeyName(3, "Winter.jpg");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "System Colour ComboBox";
            // 
            // cboColourCrazy
            // 
            this.cboColourCrazy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboColourCrazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColourCrazy.FormattingEnabled = true;
            this.cboColourCrazy.Location = new System.Drawing.Point(6, 34);
            this.cboColourCrazy.Name = "cboColourCrazy";
            this.cboColourCrazy.Size = new System.Drawing.Size(195, 21);
            this.cboColourCrazy.TabIndex = 3;
            this.cboColourCrazy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboColourCrazy_DrawItem);
            this.cboColourCrazy.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboColourCrazy_MeasureItem);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Center Align Normal ComboBox Text";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Custom Alignment ComboBox";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCenterCrazy
            // 
            this.cboCenterCrazy.FormattingEnabled = true;
            this.cboCenterCrazy.Location = new System.Drawing.Point(6, 35);
            this.cboCenterCrazy.Name = "cboCenterCrazy";
            this.cboCenterCrazy.Size = new System.Drawing.Size(203, 21);
            this.cboCenterCrazy.TabIndex = 8;
            this.cboCenterCrazy.DropDown += new System.EventHandler(this.cboCenterCrazy_DropDown);
            // 
            // btnRightText
            // 
            this.btnRightText.Location = new System.Drawing.Point(6, 108);
            this.btnRightText.Name = "btnRightText";
            this.btnRightText.Size = new System.Drawing.Size(203, 23);
            this.btnRightText.TabIndex = 10;
            this.btnRightText.Text = "Right Align Text Custom ComboBox";
            this.btnRightText.UseVisualStyleBackColor = true;
            this.btnRightText.Click += new System.EventHandler(this.btnRightText_Click);
            // 
            // btnLeftAlignScroll
            // 
            this.btnLeftAlignScroll.Location = new System.Drawing.Point(6, 137);
            this.btnLeftAlignScroll.Name = "btnLeftAlignScroll";
            this.btnLeftAlignScroll.Size = new System.Drawing.Size(203, 23);
            this.btnLeftAlignScroll.TabIndex = 11;
            this.btnLeftAlignScroll.Text = "Left Align ScrollBar Custom ComboBox";
            this.btnLeftAlignScroll.UseVisualStyleBackColor = true;
            this.btnLeftAlignScroll.Click += new System.EventHandler(this.btnLeftAlignScroll_Click);
            // 
            // btnLeftAlignButton
            // 
            this.btnLeftAlignButton.Location = new System.Drawing.Point(6, 166);
            this.btnLeftAlignButton.Name = "btnLeftAlignButton";
            this.btnLeftAlignButton.Size = new System.Drawing.Size(203, 23);
            this.btnLeftAlignButton.TabIndex = 12;
            this.btnLeftAlignButton.Text = "Left Align Button Custom ComboBox";
            this.btnLeftAlignButton.UseVisualStyleBackColor = true;
            this.btnLeftAlignButton.Click += new System.EventHandler(this.btnLeftAlignButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Line ComboBox";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Fonts ComboBox";
            // 
            // cboFontsCrazy
            // 
            this.cboFontsCrazy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFontsCrazy.FormattingEnabled = true;
            this.cboFontsCrazy.Location = new System.Drawing.Point(6, 78);
            this.cboFontsCrazy.Name = "cboFontsCrazy";
            this.cboFontsCrazy.Size = new System.Drawing.Size(195, 21);
            this.cboFontsCrazy.TabIndex = 16;
            this.cboFontsCrazy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboFontsCrazy_DrawItem);
            this.cboFontsCrazy.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboFontsCrazy_MeasureItem);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboCenterCrazy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboAlignAllCrazy);
            this.groupBox1.Controls.Add(this.btnRightText);
            this.groupBox1.Controls.Add(this.btnLeftAlignScroll);
            this.groupBox1.Controls.Add(this.btnLeftAlignButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 205);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ComboBox Alignment Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cboColourCrazy);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cboFontsCrazy);
            this.groupBox2.Controls.Add(this.imgCboCrazy);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lineCboCrazy);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(249, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 205);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom ComboBox Item Options";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 221);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(453, 23);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // imgCboCrazy
            // 
            this.imgCboCrazy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.imgCboCrazy.FormattingEnabled = true;
            this.imgCboCrazy.ICImageList = this.ilCrazy;
            this.imgCboCrazy.Location = new System.Drawing.Point(6, 166);
            this.imgCboCrazy.Name = "imgCboCrazy";
            this.imgCboCrazy.Size = new System.Drawing.Size(195, 21);
            this.imgCboCrazy.TabIndex = 0;
            // 
            // lineCboCrazy
            // 
            this.lineCboCrazy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lineCboCrazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineCboCrazy.FormattingEnabled = true;
            this.lineCboCrazy.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom",
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot",
            "Custom"});
            this.lineCboCrazy.Location = new System.Drawing.Point(6, 122);
            this.lineCboCrazy.Name = "lineCboCrazy";
            this.lineCboCrazy.Size = new System.Drawing.Size(195, 21);
            this.lineCboCrazy.TabIndex = 13;
            // 
            // cboAlignAllCrazy
            // 
            this.cboAlignAllCrazy.CASDropButtonAlignment = System.Windows.Forms.ComboAlignSettings.CASAlignment.CASRight;
            this.cboAlignAllCrazy.CASDropListAlignment = System.Windows.Forms.ComboAlignSettings.CASAlignment.CASLeft;
            this.cboAlignAllCrazy.CASScrollAlignment = System.Windows.Forms.ComboAlignSettings.CASAlignment.CASRight;
            this.cboAlignAllCrazy.FormattingEnabled = true;
            this.cboAlignAllCrazy.Location = new System.Drawing.Point(6, 81);
            this.cboAlignAllCrazy.Name = "cboAlignAllCrazy";
            this.cboAlignAllCrazy.Size = new System.Drawing.Size(203, 21);
            this.cboAlignAllCrazy.TabIndex = 9;
            // 
            // frmCrazy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 256);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCrazy";
            this.Text = "C# Crazy Combos";
            this.Load += new System.EventHandler(this.frmCrazy_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImgCbo imgCboCrazy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList ilCrazy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboColourCrazy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCenterCrazy;
        private System.Windows.Forms.ComboAlignSettings cboAlignAllCrazy;
        private System.Windows.Forms.Button btnRightText;
        private System.Windows.Forms.Button btnLeftAlignScroll;
        private System.Windows.Forms.Button btnLeftAlignButton;
        private System.Windows.Forms.LineCbo lineCboCrazy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboFontsCrazy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExit;
    }
}

