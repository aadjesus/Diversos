using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using SpottyDogSoftware.Controls;

namespace ControlDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private SpottyDogSoftware.Controls.XPGroupBoxContainer xpGroupBoxContainer1;
		private SpottyDogSoftware.Controls.XPGroupBox xpGroupBox1;
		private SpottyDogSoftware.Controls.XPGroupBox xpGroupBox2;
		private SpottyDogSoftware.Controls.XPGroupBox xpGroupBox3;
		private SpottyDogSoftware.Controls.ActivityBar activityBar1;
        private Button button1;
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

			// I am handling all the item click events in a single handler
			xpGroupBox1.ItemClicked += new SpottyDogSoftware.Controls.ItemClickEventHandler(GeneralItemClickedHandler);
			xpGroupBox2.ItemClicked += new SpottyDogSoftware.Controls.ItemClickEventHandler(GeneralItemClickedHandler);
			xpGroupBox3.ItemClicked += new SpottyDogSoftware.Controls.ItemClickEventHandler(GeneralItemClickedHandler);

			// Initial handler for XPGroupBox click event to ensure there is only one open group box
			xpGroupBox1.Click += new EventHandler(AllXPGroupBox_Click);
			xpGroupBox2.Click += new EventHandler(AllXPGroupBox_Click);
			xpGroupBox3.Click += new EventHandler(AllXPGroupBox_Click);
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
            this.button1 = new System.Windows.Forms.Button();
            this.activityBar1 = new SpottyDogSoftware.Controls.ActivityBar();
            this.xpGroupBoxContainer1 = new SpottyDogSoftware.Controls.XPGroupBoxContainer();
            this.xpGroupBox3 = new SpottyDogSoftware.Controls.XPGroupBox();
            this.xpGroupBox2 = new SpottyDogSoftware.Controls.XPGroupBox();
            this.xpGroupBox1 = new SpottyDogSoftware.Controls.XPGroupBox();
            this.xpGroupBoxContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(313, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // activityBar1
            // 
            this.activityBar1.BackColor = System.Drawing.Color.Transparent;
            this.activityBar1.BarColor = System.Drawing.Color.Orange;
            this.activityBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.activityBar1.Enabled = false;
            this.activityBar1.HighlightColor = System.Drawing.Color.White;
            this.activityBar1.Location = new System.Drawing.Point(224, 216);
            this.activityBar1.Name = "activityBar1";
            this.activityBar1.Size = new System.Drawing.Size(336, 8);
            this.activityBar1.TabIndex = 1;
            // 
            // xpGroupBoxContainer1
            // 
            this.xpGroupBoxContainer1.AutoScroll = true;
            this.xpGroupBoxContainer1.Controls.Add(this.xpGroupBox3);
            this.xpGroupBoxContainer1.Controls.Add(this.xpGroupBox2);
            this.xpGroupBoxContainer1.Controls.Add(this.xpGroupBox1);
            this.xpGroupBoxContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xpGroupBoxContainer1.Location = new System.Drawing.Point(0, 0);
            this.xpGroupBoxContainer1.Name = "xpGroupBoxContainer1";
            this.xpGroupBoxContainer1.PaneBottomRightColor = System.Drawing.SystemColors.ActiveCaption;
            this.xpGroupBoxContainer1.PaneOutlineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.xpGroupBoxContainer1.PaneTopLeftColor = System.Drawing.SystemColors.InactiveCaption;
            this.xpGroupBoxContainer1.Size = new System.Drawing.Size(208, 475);
            this.xpGroupBoxContainer1.TabIndex = 0;
            // 
            // xpGroupBox3
            // 
            this.xpGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.xpGroupBox3.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpGroupBox3.CaptionFontColor = System.Drawing.Color.Black;
            this.xpGroupBox3.CaptionFontHighLightColor = System.Drawing.Color.Red;
            this.xpGroupBox3.CaptionIcon = null;
            this.xpGroupBox3.CaptionLeftColor = System.Drawing.Color.White;
            this.xpGroupBox3.CaptionRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(210)))), ((int)(((byte)(248)))));
            this.xpGroupBox3.CaptionText = "A Group Box";
            this.xpGroupBox3.Location = new System.Drawing.Point(10, 10);
            this.xpGroupBox3.Name = "xpGroupBox3";
            this.xpGroupBox3.PaneBottomRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpGroupBox3.PaneOutlineColor = System.Drawing.Color.White;
            this.xpGroupBox3.PaneTopLeftColor = System.Drawing.Color.White;
            this.xpGroupBox3.Size = new System.Drawing.Size(188, 49);
            this.xpGroupBox3.TabIndex = 2;
            // 
            // xpGroupBox2
            // 
            this.xpGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.xpGroupBox2.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpGroupBox2.CaptionFontColor = System.Drawing.Color.Black;
            this.xpGroupBox2.CaptionFontHighLightColor = System.Drawing.Color.Red;
            this.xpGroupBox2.CaptionIcon = null;
            this.xpGroupBox2.CaptionLeftColor = System.Drawing.Color.White;
            this.xpGroupBox2.CaptionRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(210)))), ((int)(((byte)(248)))));
            this.xpGroupBox2.CaptionText = "Another Group Box";
            this.xpGroupBox2.GroupExpanded = false;
            this.xpGroupBox2.Location = new System.Drawing.Point(10, 105);
            this.xpGroupBox2.Name = "xpGroupBox2";
            this.xpGroupBox2.PaneBottomRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpGroupBox2.PaneOutlineColor = System.Drawing.Color.White;
            this.xpGroupBox2.PaneTopLeftColor = System.Drawing.Color.White;
            this.xpGroupBox2.Size = new System.Drawing.Size(188, 121);
            this.xpGroupBox2.TabIndex = 1;
            // 
            // xpGroupBox1
            // 
            this.xpGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.xpGroupBox1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpGroupBox1.CaptionFontColor = System.Drawing.Color.Black;
            this.xpGroupBox1.CaptionFontHighLightColor = System.Drawing.Color.Red;
            this.xpGroupBox1.CaptionIcon = null;
            this.xpGroupBox1.CaptionLeftColor = System.Drawing.Color.White;
            this.xpGroupBox1.CaptionRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(210)))), ((int)(((byte)(248)))));
            this.xpGroupBox1.CaptionText = "Final Group Box";
            this.xpGroupBox1.GroupExpanded = false;
            this.xpGroupBox1.Location = new System.Drawing.Point(10, 236);
            this.xpGroupBox1.Name = "xpGroupBox1";
            this.xpGroupBox1.PaneBottomRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpGroupBox1.PaneOutlineColor = System.Drawing.Color.White;
            this.xpGroupBox1.PaneTopLeftColor = System.Drawing.Color.White;
            this.xpGroupBox1.Size = new System.Drawing.Size(188, 85);
            this.xpGroupBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 475);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.activityBar1);
            this.Controls.Add(this.xpGroupBoxContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.xpGroupBoxContainer1.ResumeLayout(false);
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

		private void GeneralItemClickedHandler(object sender, SpottyDogSoftware.Controls.ItemClickEventArgs e)
		{
			switch(e.Item.Name)
			{
				case "item3":
					activityBar1.Enabled = true;
					break;

				case "item4":
					activityBar1.Enabled = false;
					break;

				default:
					MessageBox.Show(string.Format("{0} clicked", e.Item.Name));
					break;
			}
		}

		private void AllXPGroupBox_Click(object sender, EventArgs e)
		{
			xpGroupBoxContainer1.ExpandOnlyOneXPGroupBox((XPGroupBox) sender);
		}

        private void button1_Click(object sender, EventArgs e)
        {
            new SpottyDogSoftware.Controls.Form1().Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            activityBar1.Enabled = true;
        }
	}
}
