using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartSolutions.Controls.Test
{
	using SmartSolutions.Controls;

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private SmartSolutions.Controls.TriStateTreeView triStateTreeView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ConfigureTreeView();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.triStateTreeView1 = new SmartSolutions.Controls.TriStateTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // triStateTreeView1
            // 
            this.triStateTreeView1.CheckBoxes = true;
            this.triStateTreeView1.CheckedImageIndex = 3;
            this.triStateTreeView1.ImageIndex = 0;
            this.triStateTreeView1.ImageList = this.imageList1;
            this.triStateTreeView1.IndeterminateImageIndex = 4;
            this.triStateTreeView1.Location = new System.Drawing.Point(10, 11);
            this.triStateTreeView1.Name = "triStateTreeView1";
            this.triStateTreeView1.SelectedImageIndex = 1;
            this.triStateTreeView1.Size = new System.Drawing.Size(250, 298);
            this.triStateTreeView1.TabIndex = 0;
            this.triStateTreeView1.UncheckedImageIndex = 5;
            this.triStateTreeView1.UseCustomImages = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(269, 320);
            this.Controls.Add(this.triStateTreeView1);
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

		private void ConfigureTreeView()
		{
			TriStateTreeNode rootNode = new TriStateTreeNode( "Home - \"CheckboxVisible = false\"." );
			rootNode.CheckboxVisible = false;
			rootNode.IsContainer = true;

			for( int i = 0; i < 4; i++ )
			{
				TriStateTreeNode folderNode = new TriStateTreeNode( string.Format( "Foldernode {0}, can show 3 states, as shown here.", i), 0, 1 );
				folderNode.IsContainer = true;
				rootNode.Nodes.Add( folderNode );
			}

			TriStateTreeNode firstFolder = rootNode.FirstNode as TriStateTreeNode;
			for(int i = 0; i < 2; i++)
			{
				TriStateTreeNode itemNode = new TriStateTreeNode( string.Format( "Item node {0}", i ), 2, 2 );
				firstFolder.Nodes.Add( itemNode );
			}

			TriStateTreeNode secondFolder = firstFolder.NextNode as TriStateTreeNode;
			for(int i = 0; i < 2; i++)
			{
				TriStateTreeNode itemNode = new TriStateTreeNode( string.Format( "Item node {0}", i ), 2, 2);
				secondFolder.Nodes.Add( itemNode );
			}

			TriStateTreeNode thirdFolder = secondFolder.NextNode as TriStateTreeNode;
			for(int i = 0; i < 2; i++)
			{
				TriStateTreeNode itemNode = new TriStateTreeNode( string.Format( "Item node {0}", i ), 2, 2 );
				thirdFolder.Nodes.Add( itemNode );
			}

			TriStateTreeNode fourthFolder = thirdFolder.NextNode as TriStateTreeNode;
			fourthFolder.CheckboxVisible = false;
			for(int i = 0; i < 2; i++)
			{
				TriStateTreeNode itemNode = new TriStateTreeNode( string.Format( "Item node {0}", i ), 2, 2 );
				itemNode.CheckboxVisible = false;
				fourthFolder.Nodes.Add( itemNode );
			}

			this.triStateTreeView1.SuspendLayout();
			this.triStateTreeView1.Nodes.Add( rootNode );
			this.triStateTreeView1.ResumeLayout();

			secondFolder.FirstNode.Checked = true;
			thirdFolder.Checked = true;
		}
	}
}
