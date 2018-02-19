using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Phone_Number_Control
{
	/// <summary>
	/// Summary description for Formater.
	/// </summary>
	public class Formater : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DisplayStyle style = new DisplayStyle();

		public DisplayStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if(value != null)
				{
                   this.style = value;
				}
			}
		}

		public Formater()
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

//		[STAThread]
//					static void Main() 
//					{
//						Application.Run(new Form1());
//					}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formater));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Items.AddRange(new object[] {
            "Default",
            "Sample1",
            "Sample2"});
            this.listBox1.Location = new System.Drawing.Point(24, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(80, 95);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Formats";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(136, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Preview";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(136, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(296, 184);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(32, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ok";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(128, 272);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Formater
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(456, 310);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Formater";
            this.Text = "Formater";
            this.Load += new System.EventHandler(this.Formater_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(listBox1.SelectedIndex)
			{
				
				case 0:
					pictureBox1.Image = new Bitmap(@"..\Default.gif");
					//MessageBox.Show("First item selected");
					break;
				case 1:
					pictureBox1.Image =(System.Drawing.Image) (new Bitmap(@"..\Sample1.gif"));
					//MessageBox.Show("Second item selected");	
				    break;
				case 2:
					pictureBox1.Image = new Bitmap(@"..\Sample2.gif");
					//MessageBox.Show("Last item selected");
					break;
			

			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			setbackground();
		    this.Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		public void setbackground()
		{

			switch(listBox1.SelectedIndex)
			{
				
				case 0:
					this.Style.BackGroudColor = Color.FloralWhite;
					this.Style.ForeStyle = FontStyle.Regular;
					break;
				case 1:
					this.Style.BackGroudColor = Color.White;
					this.Style.ForeStyle = FontStyle.Bold;
					break;
				case 2:
					this.Style.BackGroudColor = Color.LightYellow;
					this.Style.ForeStyle = FontStyle.Italic;
					break;
			
			}
		}

		private void Formater_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
