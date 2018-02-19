using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imageList1;
		private WinLib.Controls.ImageListAssigner imageListAssigner1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components;

		public Form1()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("", 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("", 1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("", 2);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("", 3);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("", 4);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("", 5);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("", 6);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("", 7);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("", 8);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("", 9);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("", 10);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("", 13);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageListAssigner1 = new WinLib.Controls.ImageListAssigner(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// listView1
			// 
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2,
																					  listViewItem3,
																					  listViewItem4,
																					  listViewItem5,
																					  listViewItem6,
																					  listViewItem7,
																					  listViewItem8,
																					  listViewItem9,
																					  listViewItem10,
																					  listViewItem11,
																					  listViewItem12});
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(416, 128);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			// 
			// imageListAssigner1
			// 
			this.imageListAssigner1.Destination = this.imageList1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(408, 32);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select imageListAssigner1 object, on the Property Editor, double-click on the Sho" +
				"wDesigner property value.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 176);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(408, 48);
			this.label2.TabIndex = 2;
			this.label2.Text = "After ImageListAssigner modify your ImageList, you will not see any changes made " +
				"to ImageList, but any object which using the ImageList will see all its images. " +
				"You should save the form, close and re-open to take effect.";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(432, 222);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listView1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
