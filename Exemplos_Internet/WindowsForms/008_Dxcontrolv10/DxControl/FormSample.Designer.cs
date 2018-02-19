namespace DxControl
{
	partial class SampleForm
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
			this.buttonAutoSize = new System.Windows.Forms.Button();
			this.buttonScreenParams = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioBlackIsClear = new System.Windows.Forms.RadioButton();
			this.radioFade = new System.Windows.Forms.RadioButton();
			this.radioDarkIsClear = new System.Windows.Forms.RadioButton();
			this.radioSolid = new System.Windows.Forms.RadioButton();
			this.pictureWhite = new System.Windows.Forms.PictureBox();
			this.pictureBlue = new System.Windows.Forms.PictureBox();
			this.loadMesh = new System.Windows.Forms.Button();
			this.mD3d = new Gosub.Direct3d();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureWhite)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBlue)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 323);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Press F1 to toggle full screen";
			// 
			// buttonAutoSize
			// 
			this.buttonAutoSize.Location = new System.Drawing.Point(256, 320);
			this.buttonAutoSize.Name = "buttonAutoSize";
			this.buttonAutoSize.Size = new System.Drawing.Size(64, 23);
			this.buttonAutoSize.TabIndex = 2;
			this.buttonAutoSize.Text = "Auto Size";
			this.buttonAutoSize.Click += new System.EventHandler(this.buttonAutoSize_Click);
			// 
			// buttonScreenParams
			// 
			this.buttonScreenParams.Location = new System.Drawing.Point(320, 320);
			this.buttonScreenParams.Name = "buttonScreenParams";
			this.buttonScreenParams.Size = new System.Drawing.Size(56, 23);
			this.buttonScreenParams.TabIndex = 3;
			this.buttonScreenParams.Text = "Screen";
			this.buttonScreenParams.Click += new System.EventHandler(this.buttonScreenParams_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioBlackIsClear);
			this.groupBox1.Controls.Add(this.radioFade);
			this.groupBox1.Controls.Add(this.radioDarkIsClear);
			this.groupBox1.Controls.Add(this.radioSolid);
			this.groupBox1.Location = new System.Drawing.Point(14, 352);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(346, 43);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Texutre Alpha";
			// 
			// radioBlackIsClear
			// 
			this.radioBlackIsClear.AutoSize = true;
			this.radioBlackIsClear.Location = new System.Drawing.Point(56, 19);
			this.radioBlackIsClear.Name = "radioBlackIsClear";
			this.radioBlackIsClear.Size = new System.Drawing.Size(84, 17);
			this.radioBlackIsClear.TabIndex = 9;
			this.radioBlackIsClear.TabStop = false;
			this.radioBlackIsClear.Text = "Black is clear";
			this.radioBlackIsClear.CheckedChanged += new System.EventHandler(this.radioBlackIsClear_CheckedChanged);
			// 
			// radioFade
			// 
			this.radioFade.AutoSize = true;
			this.radioFade.Location = new System.Drawing.Point(268, 19);
			this.radioFade.Name = "radioFade";
			this.radioFade.Size = new System.Drawing.Size(71, 17);
			this.radioFade.TabIndex = 8;
			this.radioFade.TabStop = false;
			this.radioFade.Text = "Circle fade";
			this.radioFade.CheckedChanged += new System.EventHandler(this.radioFade_CheckedChanged);
			// 
			// radioDarkIsClear
			// 
			this.radioDarkIsClear.AutoSize = true;
			this.radioDarkIsClear.Checked = true;
			this.radioDarkIsClear.Location = new System.Drawing.Point(151, 19);
			this.radioDarkIsClear.Name = "radioDarkIsClear";
			this.radioDarkIsClear.Size = new System.Drawing.Size(111, 17);
			this.radioDarkIsClear.TabIndex = 7;
			this.radioDarkIsClear.TabStop = false;
			this.radioDarkIsClear.Text = "Dark fades to clear";
			this.radioDarkIsClear.CheckedChanged += new System.EventHandler(this.radioDarkFadesToClear_CheckedChanged);
			// 
			// radioSolid
			// 
			this.radioSolid.AutoSize = true;
			this.radioSolid.Location = new System.Drawing.Point(6, 19);
			this.radioSolid.Name = "radioSolid";
			this.radioSolid.Size = new System.Drawing.Size(44, 17);
			this.radioSolid.TabIndex = 6;
			this.radioSolid.TabStop = false;
			this.radioSolid.Text = "Solid";
			this.radioSolid.CheckedChanged += new System.EventHandler(this.radioSolid_CheckedChanged);
			// 
			// pictureWhite
			// 
			this.pictureWhite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.pictureWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureWhite.Location = new System.Drawing.Point(366, 352);
			this.pictureWhite.Name = "pictureWhite";
			this.pictureWhite.Size = new System.Drawing.Size(60, 19);
			this.pictureWhite.TabIndex = 5;
			this.pictureWhite.TabStop = false;
			this.pictureWhite.Click += new System.EventHandler(this.pictureWhite_Click);
			// 
			// pictureBlue
			// 
			this.pictureBlue.BackColor = System.Drawing.Color.Purple;
			this.pictureBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBlue.Location = new System.Drawing.Point(366, 375);
			this.pictureBlue.Name = "pictureBlue";
			this.pictureBlue.Size = new System.Drawing.Size(60, 20);
			this.pictureBlue.TabIndex = 6;
			this.pictureBlue.TabStop = false;
			this.pictureBlue.Click += new System.EventHandler(this.pictureBlue_Click);
			// 
			// loadMesh
			// 
			this.loadMesh.Location = new System.Drawing.Point(376, 320);
			this.loadMesh.Name = "loadMesh";
			this.loadMesh.Size = new System.Drawing.Size(56, 23);
			this.loadMesh.TabIndex = 7;
			this.loadMesh.Text = "Load...";
			this.loadMesh.Click += new System.EventHandler(this.loadMesh_Click);
			// 
			// mD3d
			// 
			this.mD3d.BackColor = System.Drawing.Color.Black;
			this.mD3d.DxAutoResize = false;
			this.mD3d.DxFullScreen = false;
			this.mD3d.Location = new System.Drawing.Point(8, 8);
			this.mD3d.Name = "mD3d";
			this.mD3d.Size = new System.Drawing.Size(424, 308);
			this.mD3d.TabIndex = 0;
			this.mD3d.DxLoaded += new Gosub.Direct3d.DxDirect3dDelegate(this.mD3d_DxLoaded);
			this.mD3d.DxRender3d += new Gosub.Direct3d.DxDirect3dDelegate(this.mD3d_DxRender3d);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// SampleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(441, 410);
			this.Controls.Add(this.loadMesh);
			this.Controls.Add(this.pictureBlue);
			this.Controls.Add(this.pictureWhite);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonScreenParams);
			this.Controls.Add(this.buttonAutoSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.mD3d);
			this.KeyPreview = true;
			this.Name = "SampleForm";
			this.Text = "Direct X Control Sample";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SampleForm_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureWhite)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBlue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAutoSize;
		private System.Windows.Forms.Button buttonScreenParams;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioFade;
		private System.Windows.Forms.RadioButton radioDarkIsClear;
		private System.Windows.Forms.RadioButton radioSolid;
		private System.Windows.Forms.RadioButton radioBlackIsClear;
		private System.Windows.Forms.PictureBox pictureWhite;
		private System.Windows.Forms.PictureBox pictureBlue;
		private Gosub.Direct3d mD3d;
		private System.Windows.Forms.Button loadMesh;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;


	}
}

