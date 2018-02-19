using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DragDropNish
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			listBox1.Items.Add("Nish [BusterBoy]");
			listBox1.Items.Add("Colin Davies");
			listBox1.Items.Add("Paul Watson");
			
			listBox1.Items.Add("David Wulff");
			listBox1.Items.Add("Christian Graus");
			listBox1.Items.Add("Chris Maunder");		

			listBox1.Items.Add("Tweety");
			listBox1.Items.Add("Qomi");		
			listBox1.Items.Add("Lauren");		

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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listBox1.ItemHeight = 24;
			this.listBox1.Location = new System.Drawing.Point(32, 16);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(176, 196);
			this.listBox1.TabIndex = 0;
			this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
			// 
			// listBox2
			// 
			this.listBox2.AllowDrop = true;
			this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listBox2.ItemHeight = 24;
			this.listBox2.Location = new System.Drawing.Point(344, 16);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(176, 196);
			this.listBox2.TabIndex = 1;
			this.listBox2.DragOver += new System.Windows.Forms.DragEventHandler(this.listBox2_DragOver);
			this.listBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(88, 224);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(328, 40);
			this.label1.TabIndex = 2;
			this.label1.Text = "Drag and drop names from the list box on the left to the list box on the right";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 278);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.listBox2,
																		  this.listBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Playing with drag and drop";
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

		
		private void listBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{	
			if(listBox1.Items.Count==0)
				return;
			string s = listBox1.Items[listBox1.IndexFromPoint(e.X,e.Y)].ToString();			
			DragDropEffects dde1=DoDragDrop(s,
				DragDropEffects.All);
			
			if(dde1 == DragDropEffects.All )
			{
				listBox1.Items.RemoveAt(listBox1.IndexFromPoint(e.X,e.Y));				
			}		
			
		}

		private void listBox2_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent(DataFormats.StringFormat))
			{				
				string str= (string)e.Data.GetData(DataFormats.StringFormat);
				listBox2.Items.Add(str);				
			}
			
		}

		private void listBox2_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect=DragDropEffects.All;
		}
	}
}
