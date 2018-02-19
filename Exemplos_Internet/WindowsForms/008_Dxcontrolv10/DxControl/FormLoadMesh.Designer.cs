namespace DxControl
{
	partial class FormLoadMesh
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
        this.buttonAddNormals = new System.Windows.Forms.Button();
        this.labelLighting = new System.Windows.Forms.Label();
        this.labelFormat = new System.Windows.Forms.Label();
        this.buttonCancel = new System.Windows.Forms.Button();
        this.buttonAccept = new System.Windows.Forms.Button();
        this.labelPictureSize = new System.Windows.Forms.Label();
        this.labelPictureDirectory = new System.Windows.Forms.Label();
        this.labelPictureName = new System.Windows.Forms.Label();
        this.d3dModel = new Gosub.Direct3d();
        this.SuspendLayout();
        // 
        // buttonAddNormals
        // 
        this.buttonAddNormals.Location = new System.Drawing.Point(9, 473);
        this.buttonAddNormals.Name = "buttonAddNormals";
        this.buttonAddNormals.Size = new System.Drawing.Size(75, 23);
        this.buttonAddNormals.TabIndex = 32;
        this.buttonAddNormals.Text = "Add Normals";
        this.buttonAddNormals.Click += new System.EventHandler(this.buttonAddNormals_Click);
        // 
        // labelLighting
        // 
        this.labelLighting.AutoSize = true;
        this.labelLighting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelLighting.Location = new System.Drawing.Point(88, 473);
        this.labelLighting.Name = "labelLighting";
        this.labelLighting.Size = new System.Drawing.Size(74, 20);
        this.labelLighting.TabIndex = 31;
        this.labelLighting.Text = "Lighting?";
        // 
        // labelFormat
        // 
        this.labelFormat.AutoSize = true;
        this.labelFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelFormat.Location = new System.Drawing.Point(8, 64);
        this.labelFormat.Name = "labelFormat";
        this.labelFormat.Size = new System.Drawing.Size(60, 20);
        this.labelFormat.TabIndex = 30;
        this.labelFormat.Text = "Format";
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new System.Drawing.Point(456, 8);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(59, 23);
        this.buttonCancel.TabIndex = 29;
        this.buttonCancel.Text = "&Cancel";
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // buttonAccept
        // 
        this.buttonAccept.Location = new System.Drawing.Point(392, 8);
        this.buttonAccept.Name = "buttonAccept";
        this.buttonAccept.Size = new System.Drawing.Size(59, 23);
        this.buttonAccept.TabIndex = 28;
        this.buttonAccept.Text = "&Accept";
        this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
        // 
        // labelPictureSize
        // 
        this.labelPictureSize.AutoSize = true;
        this.labelPictureSize.Location = new System.Drawing.Point(8, 48);
        this.labelPictureSize.Name = "labelPictureSize";
        this.labelPictureSize.Size = new System.Drawing.Size(63, 13);
        this.labelPictureSize.TabIndex = 25;
        this.labelPictureSize.Text = "Picture Size";
        // 
        // labelPictureDirectory
        // 
        this.labelPictureDirectory.AutoSize = true;
        this.labelPictureDirectory.Location = new System.Drawing.Point(8, 32);
        this.labelPictureDirectory.Name = "labelPictureDirectory";
        this.labelPictureDirectory.Size = new System.Drawing.Size(85, 13);
        this.labelPictureDirectory.TabIndex = 24;
        this.labelPictureDirectory.Text = "Picture Directory";
        // 
        // labelPictureName
        // 
        this.labelPictureName.AutoSize = true;
        this.labelPictureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelPictureName.Location = new System.Drawing.Point(8, 8);
        this.labelPictureName.Name = "labelPictureName";
        this.labelPictureName.Size = new System.Drawing.Size(104, 20);
        this.labelPictureName.TabIndex = 23;
        this.labelPictureName.Text = "Picture Name";
        // 
        // d3dModel
        // 
        this.d3dModel.BackColor = System.Drawing.Color.Black;
        this.d3dModel.DxAutoResize = false;
        this.d3dModel.DxFullScreen = false;
        this.d3dModel.Location = new System.Drawing.Point(8, 88);
        this.d3dModel.Name = "d3dModel";
        this.d3dModel.Size = new System.Drawing.Size(512, 384);
        this.d3dModel.TabIndex = 33;
        this.d3dModel.DxRender3d += new Gosub.Direct3d.DxDirect3dDelegate(this.d3dModel_DxRender3d);
        this.d3dModel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.d3dModel_MouseUp);
        this.d3dModel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.d3dModel_MouseMove);
        this.d3dModel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.d3dModel_MouseDown);
        // 
        // FormLoadMesh
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(525, 497);
        this.Controls.Add(this.d3dModel);
        this.Controls.Add(this.buttonAddNormals);
        this.Controls.Add(this.labelLighting);
        this.Controls.Add(this.labelFormat);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonAccept);
        this.Controls.Add(this.labelPictureSize);
        this.Controls.Add(this.labelPictureDirectory);
        this.Controls.Add(this.labelPictureName);
        this.KeyPreview = true;
        this.Name = "FormLoadMesh";
        this.Text = "Mesh viewer";
        this.Shown += new System.EventHandler(this.FormViewMesh_Shown);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLoadMesh_KeyDown);
        this.ResumeLayout(false);
        this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonAddNormals;
		private System.Windows.Forms.Label labelLighting;
		private System.Windows.Forms.Label labelFormat;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonAccept;
		private System.Windows.Forms.Label labelPictureSize;
		private System.Windows.Forms.Label labelPictureDirectory;
		private System.Windows.Forms.Label labelPictureName;
		private Gosub.Direct3d d3dModel;
	}
}