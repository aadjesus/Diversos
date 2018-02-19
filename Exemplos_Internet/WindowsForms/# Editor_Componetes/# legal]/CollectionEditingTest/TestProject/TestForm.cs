using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestProject
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TestForm : System.Windows.Forms.Form
	{
		private Test.TestControl tc;
		private System.Windows.Forms.ImageList il_Images;
		private CustomControls.Win32Controls.PushButton btn_Controls;	
		private System.ComponentModel.IContainer components;

		public TestForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.tc = new Test.TestControl();
            this.il_Images = new System.Windows.Forms.ImageList(this.components);
            this.btn_Controls = new CustomControls.Win32Controls.PushButton();
            this.SuspendLayout();
            // 
            // tc
            // 
            this.tc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc.Location = new System.Drawing.Point(8, 8);
            this.tc.Name = "tc";
            this.tc.SimpleItem.Name = "SimpleItem";
            this.tc.Size = new System.Drawing.Size(120, 97);
            this.tc.TabIndex = 0;
            this.tc.Text = "Test Control";
            this.tc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tc.Click += new System.EventHandler(this.tc_Click);
            // 
            // il_Images
            // 
            this.il_Images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il_Images.ImageStream")));
            this.il_Images.TransparentColor = System.Drawing.Color.Transparent;
            this.il_Images.Images.SetKeyName(0, "");
            this.il_Images.Images.SetKeyName(1, "");
            this.il_Images.Images.SetKeyName(2, "");
            this.il_Images.Images.SetKeyName(3, "");
            this.il_Images.Images.SetKeyName(4, "");
            this.il_Images.Images.SetKeyName(5, "");
            this.il_Images.Images.SetKeyName(6, "");
            // 
            // btn_Controls
            // 
            this.btn_Controls.Image = ((System.Drawing.Image)(resources.GetObject("btn_Controls.Image")));
            this.btn_Controls.Location = new System.Drawing.Point(141, 36);
            this.btn_Controls.Name = "btn_Controls";
            this.btn_Controls.Size = new System.Drawing.Size(181, 68);
            this.btn_Controls.TabIndex = 1;
            this.btn_Controls.Text = "Custom Controls";
            this.btn_Controls.Click += new System.EventHandler(this.btn_Controls_Click);
            // 
            // TestForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(387, 113);
            this.Controls.Add(this.btn_Controls);
            this.Controls.Add(this.tc);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Name = "TestForm";
            this.Text = "Test Form";
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Application.Run(new CustomControlsForm());
			Application.Run(new TestForm());
		}


		
	
		private void tc_Click(object sender, System.EventArgs e)
		{
			Test.CollectionEditors.CustomCollectionEditorDialog cced= new Test.CollectionEditors.CustomCollectionEditorDialog();
			cced.Text="Test Control's Custom Items";
			cced.Collection=tc.CustomItems;
			cced.ImageList=this.il_Images;
			cced.ShowDialog();
		}

		private void btn_Controls_Click(object sender, System.EventArgs e)
		{
			CustomControlsForm cForm= new CustomControlsForm();
			cForm.ShowDialog();
		}

	
	}
}


