namespace TreeTestApp
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
            System.Windows.Forms.TabPage tabPage1;
            System.Windows.Forms.TabPage tabPage2;
            this.testTreeForm1 = new TreeTestApp.TestTreeForm();
            this.folderView1 = new TreeTestApp.FolderView();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeTestAutoSize1 = new TreeTestApp.TreeTestAutoSize();
            this.colorListBox1 = new CommonTools.ColorListBox();
            this.colorPickerCombobox1 = new CommonTools.ColorPickerCombobox();
            this.colorPickerCtrl1 = new CommonTools.ColorPickerCtrl();
            this.colorSlider1 = new CommonTools.ColorSlider();
            this.colorTable1 = new CommonTools.ColorTable();
            this.colorWheel1 = new CommonTools.ColorWheel();
            this.colorWheelCtrl1 = new CommonTools.ColorWheelCtrl();
            this.eyedropColorPicker1 = new CommonTools.EyedropColorPicker();
            this.floatComboBox1 = new CommonTools.FloatComboBox();
            this.floatEditor1 = new CommonTools.FloatEditor();
            this.hslColorSlider1 = new CommonTools.HSLColorSlider();
            this.hslColorSlider2 = new CommonTools.HSLColorSlider();
            this.intEditor1 = new CommonTools.IntEditor();
            this.labelRotate1 = new CommonTools.LabelRotate();
            this.myBindingSource1 = new CommonTools.MyBindingSource();
            this.myRadioButton1 = new CommonTools.MyRadioButton();
            this.systemColorListBox1 = new CommonTools.SystemColorListBox();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(this.testTreeForm1);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(730, 585);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Tree Validation";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // testTreeForm1
            // 
            this.testTreeForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testTreeForm1.Location = new System.Drawing.Point(3, 3);
            this.testTreeForm1.Name = "testTreeForm1";
            this.testTreeForm1.Size = new System.Drawing.Size(724, 579);
            this.testTreeForm1.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(this.folderView1);
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(730, 585);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Folder View";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // folderView1
            // 
            this.folderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderView1.Location = new System.Drawing.Point(3, 3);
            this.folderView1.Name = "folderView1";
            this.folderView1.Size = new System.Drawing.Size(724, 579);
            this.folderView1.TabIndex = 0;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControl.Controls.Add(tabPage1);
            this.m_tabControl.Controls.Add(tabPage2);
            this.m_tabControl.Controls.Add(this.tabPage3);
            this.m_tabControl.Location = new System.Drawing.Point(4, 4);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(738, 611);
            this.m_tabControl.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.treeTestAutoSize1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(730, 585);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "AutoSize";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // treeTestAutoSize1
            // 
            this.treeTestAutoSize1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTestAutoSize1.Location = new System.Drawing.Point(0, 0);
            this.treeTestAutoSize1.Name = "treeTestAutoSize1";
            this.treeTestAutoSize1.Size = new System.Drawing.Size(730, 585);
            this.treeTestAutoSize1.TabIndex = 0;
            // 
            // colorListBox1
            // 
            this.colorListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorListBox1.FormattingEnabled = true;
            this.colorListBox1.Location = new System.Drawing.Point(6, 6);
            this.colorListBox1.Name = "colorListBox1";
            this.colorListBox1.SelectedColor = System.Drawing.Color.AliceBlue;
            this.colorListBox1.Size = new System.Drawing.Size(120, 95);
            this.colorListBox1.TabIndex = 0;
            // 
            // colorPickerCombobox1
            // 
            this.colorPickerCombobox1.Location = new System.Drawing.Point(132, 6);
            this.colorPickerCombobox1.Name = "colorPickerCombobox1";
            this.colorPickerCombobox1.SelectedItem = System.Drawing.Color.Wheat;
            this.colorPickerCombobox1.Size = new System.Drawing.Size(75, 23);
            this.colorPickerCombobox1.TabIndex = 1;
            this.colorPickerCombobox1.Text = "colorPickerCombobox1";
            // 
            // colorPickerCtrl1
            // 
            this.colorPickerCtrl1.BackColor = System.Drawing.Color.Transparent;
            this.colorPickerCtrl1.Location = new System.Drawing.Point(6, 107);
            this.colorPickerCtrl1.Name = "colorPickerCtrl1";
            this.colorPickerCtrl1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.colorPickerCtrl1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.colorPickerCtrl1.Size = new System.Drawing.Size(507, 250);
            this.colorPickerCtrl1.TabIndex = 2;
            // 
            // colorSlider1
            // 
            this.colorSlider1.BarPadding = new System.Windows.Forms.Padding(12, 5, 24, 10);
            this.colorSlider1.Color1 = System.Drawing.Color.Black;
            this.colorSlider1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.colorSlider1.Color3 = System.Drawing.Color.White;
            this.colorSlider1.Location = new System.Drawing.Point(306, 6);
            this.colorSlider1.Name = "colorSlider1";
            this.colorSlider1.NumberOfColors = CommonTools.ColorSlider.eNumberOfColors.Use3Colors;
            this.colorSlider1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider1.Percent = 0F;
            this.colorSlider1.RotatePointAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorSlider1.Size = new System.Drawing.Size(133, 74);
            this.colorSlider1.TabIndex = 3;
            this.colorSlider1.Text = "colorSlider1";
            this.colorSlider1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.colorSlider1.TextAngle = 0F;
            this.colorSlider1.ValueOrientation = CommonTools.ColorSlider.eValueOrientation.MinToMax;
            // 
            // colorTable1
            // 
            this.colorTable1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.DarkSlateGray,
        System.Drawing.Color.Navy,
        System.Drawing.Color.Teal,
        System.Drawing.Color.Olive,
        System.Drawing.Color.Maroon,
        System.Drawing.Color.Purple,
        System.Drawing.Color.Green,
        System.Drawing.Color.Indigo,
        System.Drawing.Color.MidnightBlue,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.DarkMagenta,
        System.Drawing.Color.DarkBlue,
        System.Drawing.Color.DarkRed,
        System.Drawing.Color.DarkOliveGreen,
        System.Drawing.Color.SaddleBrown,
        System.Drawing.Color.ForestGreen,
        System.Drawing.Color.OliveDrab,
        System.Drawing.Color.SeaGreen,
        System.Drawing.Color.DarkGoldenrod,
        System.Drawing.Color.DarkSlateBlue,
        System.Drawing.Color.MediumBlue,
        System.Drawing.Color.Sienna,
        System.Drawing.Color.Brown,
        System.Drawing.Color.DarkTurquoise,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.LightSeaGreen,
        System.Drawing.Color.DarkViolet,
        System.Drawing.Color.Firebrick,
        System.Drawing.Color.MediumVioletRed,
        System.Drawing.Color.MediumSeaGreen,
        System.Drawing.Color.Crimson,
        System.Drawing.Color.Chocolate,
        System.Drawing.Color.Goldenrod,
        System.Drawing.Color.MediumSpringGreen,
        System.Drawing.Color.SteelBlue,
        System.Drawing.Color.LawnGreen,
        System.Drawing.Color.DarkOrchid,
        System.Drawing.Color.Gold,
        System.Drawing.Color.Red,
        System.Drawing.Color.LimeGreen,
        System.Drawing.Color.Orange,
        System.Drawing.Color.Lime,
        System.Drawing.Color.SpringGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.Magenta,
        System.Drawing.Color.Cyan,
        System.Drawing.Color.DarkOrange,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.CadetBlue,
        System.Drawing.Color.Chartreuse,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepSkyBlue,
        System.Drawing.Color.Aqua,
        System.Drawing.Color.YellowGreen,
        System.Drawing.Color.Fuchsia,
        System.Drawing.Color.Gray,
        System.Drawing.Color.SlateGray,
        System.Drawing.Color.Peru,
        System.Drawing.Color.BlueViolet,
        System.Drawing.Color.LightSlateGray,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.MediumTurquoise,
        System.Drawing.Color.DodgerBlue,
        System.Drawing.Color.Turquoise,
        System.Drawing.Color.RoyalBlue,
        System.Drawing.Color.SlateBlue,
        System.Drawing.Color.MediumOrchid,
        System.Drawing.Color.DarkKhaki,
        System.Drawing.Color.IndianRed,
        System.Drawing.Color.GreenYellow,
        System.Drawing.Color.MediumAquamarine,
        System.Drawing.Color.Tomato,
        System.Drawing.Color.DarkSeaGreen,
        System.Drawing.Color.Orchid,
        System.Drawing.Color.PaleVioletRed,
        System.Drawing.Color.MediumPurple,
        System.Drawing.Color.RosyBrown,
        System.Drawing.Color.Coral,
        System.Drawing.Color.CornflowerBlue,
        System.Drawing.Color.DarkGray,
        System.Drawing.Color.SandyBrown,
        System.Drawing.Color.MediumSlateBlue,
        System.Drawing.Color.Tan,
        System.Drawing.Color.DarkSalmon,
        System.Drawing.Color.BurlyWood,
        System.Drawing.Color.HotPink,
        System.Drawing.Color.Salmon,
        System.Drawing.Color.LightCoral,
        System.Drawing.Color.Violet,
        System.Drawing.Color.SkyBlue,
        System.Drawing.Color.LightSalmon,
        System.Drawing.Color.Khaki,
        System.Drawing.Color.Plum,
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Aquamarine,
        System.Drawing.Color.Silver,
        System.Drawing.Color.LightSkyBlue,
        System.Drawing.Color.LightSteelBlue,
        System.Drawing.Color.LightBlue,
        System.Drawing.Color.PaleGreen,
        System.Drawing.Color.PowderBlue,
        System.Drawing.Color.Thistle,
        System.Drawing.Color.PaleGoldenrod,
        System.Drawing.Color.PaleTurquoise,
        System.Drawing.Color.LightGray,
        System.Drawing.Color.Wheat,
        System.Drawing.Color.NavajoWhite,
        System.Drawing.Color.Moccasin,
        System.Drawing.Color.LightPink,
        System.Drawing.Color.PeachPuff,
        System.Drawing.Color.Gainsboro,
        System.Drawing.Color.Pink,
        System.Drawing.Color.Bisque,
        System.Drawing.Color.LightGoldenrodYellow,
        System.Drawing.Color.LemonChiffon,
        System.Drawing.Color.BlanchedAlmond,
        System.Drawing.Color.Beige,
        System.Drawing.Color.AntiqueWhite,
        System.Drawing.Color.PapayaWhip,
        System.Drawing.Color.Cornsilk,
        System.Drawing.Color.LightYellow,
        System.Drawing.Color.LightCyan,
        System.Drawing.Color.Lavender,
        System.Drawing.Color.MistyRose,
        System.Drawing.Color.Linen,
        System.Drawing.Color.OldLace,
        System.Drawing.Color.WhiteSmoke,
        System.Drawing.Color.SeaShell,
        System.Drawing.Color.Azure,
        System.Drawing.Color.AliceBlue,
        System.Drawing.Color.Ivory,
        System.Drawing.Color.Honeydew,
        System.Drawing.Color.FloralWhite,
        System.Drawing.Color.LavenderBlush,
        System.Drawing.Color.MintCream,
        System.Drawing.Color.GhostWhite,
        System.Drawing.Color.Snow,
        System.Drawing.Color.White};
            this.colorTable1.Cols = 16;
            this.colorTable1.FieldSize = new System.Drawing.Size(12, 12);
            this.colorTable1.Location = new System.Drawing.Point(204, 363);
            this.colorTable1.Name = "colorTable1";
            this.colorTable1.RotatePointAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorTable1.SelectedItem = System.Drawing.Color.Black;
            this.colorTable1.Size = new System.Drawing.Size(153, 92);
            this.colorTable1.TabIndex = 4;
            this.colorTable1.Text = "colorTable1";
            this.colorTable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.colorTable1.TextAngle = 0F;
            // 
            // colorWheel1
            // 
            this.colorWheel1.Location = new System.Drawing.Point(213, 6);
            this.colorWheel1.Name = "colorWheel1";
            this.colorWheel1.Size = new System.Drawing.Size(75, 23);
            this.colorWheel1.TabIndex = 5;
            this.colorWheel1.Text = "colorWheel1";
            // 
            // colorWheelCtrl1
            // 
            this.colorWheelCtrl1.BackColor = System.Drawing.Color.Transparent;
            this.colorWheelCtrl1.Location = new System.Drawing.Point(405, 363);
            this.colorWheelCtrl1.Name = "colorWheelCtrl1";
            this.colorWheelCtrl1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(221)))), ((int)(((byte)(179)))));
            this.colorWheelCtrl1.Size = new System.Drawing.Size(296, 206);
            this.colorWheelCtrl1.TabIndex = 6;
            // 
            // eyedropColorPicker1
            // 
            this.eyedropColorPicker1.Location = new System.Drawing.Point(132, 47);
            this.eyedropColorPicker1.Name = "eyedropColorPicker1";
            this.eyedropColorPicker1.SelectedColor = System.Drawing.Color.Empty;
            this.eyedropColorPicker1.Size = new System.Drawing.Size(75, 23);
            this.eyedropColorPicker1.TabIndex = 7;
            this.eyedropColorPicker1.Text = "eyedropColorPicker1";
            this.eyedropColorPicker1.Zoom = 4;
            // 
            // floatComboBox1
            // 
            this.floatComboBox1.DisplayMember = "Name";
            this.floatComboBox1.FormattingEnabled = true;
            this.floatComboBox1.Location = new System.Drawing.Point(456, 8);
            this.floatComboBox1.Name = "floatComboBox1";
            this.floatComboBox1.SelectedItem = null;
            this.floatComboBox1.Size = new System.Drawing.Size(121, 21);
            this.floatComboBox1.TabIndex = 8;
            this.floatComboBox1.ValueMember = "Object";
            // 
            // floatEditor1
            // 
            this.floatEditor1.Location = new System.Drawing.Point(456, 35);
            this.floatEditor1.Name = "floatEditor1";
            this.floatEditor1.Size = new System.Drawing.Size(100, 20);
            this.floatEditor1.TabIndex = 9;
            this.floatEditor1.Text = "0";
            this.floatEditor1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.floatEditor1.Value = 0F;
            // 
            // hslColorSlider1
            // 
            this.hslColorSlider1.BarPadding = new System.Windows.Forms.Padding(12, 5, 24, 10);
            this.hslColorSlider1.Color1 = System.Drawing.Color.Black;
            this.hslColorSlider1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.hslColorSlider1.Color3 = System.Drawing.Color.White;
            this.hslColorSlider1.Location = new System.Drawing.Point(566, 61);
            this.hslColorSlider1.Name = "hslColorSlider1";
            this.hslColorSlider1.NumberOfColors = CommonTools.ColorSlider.eNumberOfColors.Use3Colors;
            this.hslColorSlider1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.hslColorSlider1.Percent = 0F;
            this.hslColorSlider1.RotatePointAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.hslColorSlider1.Size = new System.Drawing.Size(122, 40);
            this.hslColorSlider1.TabIndex = 10;
            this.hslColorSlider1.Text = "hslColorSlider1";
            this.hslColorSlider1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hslColorSlider1.TextAngle = 0F;
            this.hslColorSlider1.ValueOrientation = CommonTools.ColorSlider.eValueOrientation.MinToMax;
            // 
            // hslColorSlider2
            // 
            this.hslColorSlider2.BarPadding = new System.Windows.Forms.Padding(12, 5, 24, 10);
            this.hslColorSlider2.Color1 = System.Drawing.Color.Black;
            this.hslColorSlider2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.hslColorSlider2.Color3 = System.Drawing.Color.White;
            this.hslColorSlider2.Location = new System.Drawing.Point(4, 363);
            this.hslColorSlider2.Name = "hslColorSlider2";
            this.hslColorSlider2.NumberOfColors = CommonTools.ColorSlider.eNumberOfColors.Use3Colors;
            this.hslColorSlider2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.hslColorSlider2.Percent = 0F;
            this.hslColorSlider2.RotatePointAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.hslColorSlider2.Size = new System.Drawing.Size(184, 82);
            this.hslColorSlider2.TabIndex = 11;
            this.hslColorSlider2.Text = "hslColorSlider2";
            this.hslColorSlider2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hslColorSlider2.TextAngle = 0F;
            this.hslColorSlider2.ValueOrientation = CommonTools.ColorSlider.eValueOrientation.MinToMax;
            // 
            // intEditor1
            // 
            this.intEditor1.Location = new System.Drawing.Point(539, 159);
            this.intEditor1.Name = "intEditor1";
            this.intEditor1.Size = new System.Drawing.Size(100, 20);
            this.intEditor1.TabIndex = 12;
            this.intEditor1.Text = "0";
            this.intEditor1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.intEditor1.Value = 0;
            // 
            // labelRotate1
            // 
            this.labelRotate1.Location = new System.Drawing.Point(24, 468);
            this.labelRotate1.Name = "labelRotate1";
            this.labelRotate1.RotatePointAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRotate1.Size = new System.Drawing.Size(75, 23);
            this.labelRotate1.TabIndex = 13;
            this.labelRotate1.Text = "labelRotate1";
            this.labelRotate1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelRotate1.TextAngle = 0F;
            // 
            // myRadioButton1
            // 
            this.myRadioButton1.AutoSize = true;
            this.myRadioButton1.CheckedValue = null;
            this.myRadioButton1.Location = new System.Drawing.Point(122, 468);
            this.myRadioButton1.Name = "myRadioButton1";
            this.myRadioButton1.Size = new System.Drawing.Size(103, 17);
            this.myRadioButton1.TabIndex = 14;
            this.myRadioButton1.TabStop = true;
            this.myRadioButton1.Text = "myRadioButton1";
            this.myRadioButton1.UseVisualStyleBackColor = true;
            // 
            // systemColorListBox1
            // 
            this.systemColorListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.systemColorListBox1.FormattingEnabled = true;
            this.systemColorListBox1.Location = new System.Drawing.Point(255, 474);
            this.systemColorListBox1.Name = "systemColorListBox1";
            this.systemColorListBox1.SelectedColor = System.Drawing.SystemColors.ActiveBorder;
            this.systemColorListBox1.Size = new System.Drawing.Size(120, 95);
            this.systemColorListBox1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 620);
            this.Controls.Add(this.m_tabControl);
            this.Name = "Form1";
            this.Text = "Custom Tree (by Jesper Kristiansen)";
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myBindingSource1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private TestTreeForm testTreeForm1;
		private System.Windows.Forms.TabControl m_tabControl;
		private FolderView folderView1;
		private System.Windows.Forms.TabPage tabPage3;
		private TreeTestAutoSize treeTestAutoSize1;
        private System.Windows.Forms.TabPage tabPage4;
        private CommonTools.SystemColorListBox systemColorListBox1;
        private CommonTools.MyRadioButton myRadioButton1;
        private CommonTools.LabelRotate labelRotate1;
        private CommonTools.IntEditor intEditor1;
        private CommonTools.HSLColorSlider hslColorSlider2;
        private CommonTools.HSLColorSlider hslColorSlider1;
        private CommonTools.FloatEditor floatEditor1;
        private CommonTools.FloatComboBox floatComboBox1;
        private CommonTools.EyedropColorPicker eyedropColorPicker1;
        private CommonTools.ColorWheelCtrl colorWheelCtrl1;
        private CommonTools.ColorWheel colorWheel1;
        private CommonTools.ColorTable colorTable1;
        private CommonTools.ColorSlider colorSlider1;
        private CommonTools.ColorPickerCtrl colorPickerCtrl1;
        private CommonTools.ColorPickerCombobox colorPickerCombobox1;
        private CommonTools.ColorListBox colorListBox1;
        private CommonTools.MyBindingSource myBindingSource1;
	}
}

