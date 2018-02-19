using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1x : System.Windows.Forms.Form
	{
		protected SpottyDogSoftware.Controls.PaneCaption paneCaption1;
		protected SpottyDogSoftware.Controls.PaneCaption paneCaption2;
		protected System.Windows.Forms.Button button1;
		protected System.Windows.Forms.Button button2;
		protected SpottyDogSoftware.Controls.ActivityBar activityBar1;
		protected SpottyDogSoftware.Controls.XPGroupBoxContainer xpGroupBoxContainer1;
		protected System.Windows.Forms.LinkLabel linkLabel1;
		protected SpottyDogSoftware.Controls.XPGroupBox xpGroupBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1x()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1x));
			this.paneCaption1 = new SpottyDogSoftware.Controls.PaneCaption();
			this.paneCaption2 = new SpottyDogSoftware.Controls.PaneCaption();
			this.activityBar1 = new SpottyDogSoftware.Controls.ActivityBar();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.xpGroupBoxContainer1 = new SpottyDogSoftware.Controls.XPGroupBoxContainer();
			this.xpGroupBox2 = new SpottyDogSoftware.Controls.XPGroupBox();
			this.paneCaption2.SuspendLayout();
			this.xpGroupBoxContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// paneCaption1
			// 
			this.paneCaption1.AllowActive = false;
			this.paneCaption1.AntiAlias = false;
			this.paneCaption1.BackColor = System.Drawing.Color.Transparent;
			this.paneCaption1.Caption = "My UserPane";
			this.paneCaption1.Dock = System.Windows.Forms.DockStyle.Top;
			this.paneCaption1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.paneCaption1.Location = new System.Drawing.Point(0, 0);
			this.paneCaption1.Name = "paneCaption1";
			this.paneCaption1.Size = new System.Drawing.Size(292, 24);
			this.paneCaption1.TabIndex = 0;
			// 
			// paneCaption2
			// 
			this.paneCaption2.AllowActive = false;
			this.paneCaption2.AntiAlias = false;
			this.paneCaption2.BackColor = System.Drawing.Color.Transparent;
			this.paneCaption2.Controls.Add(this.activityBar1);
			this.paneCaption2.Controls.Add(this.linkLabel1);
			this.paneCaption2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.paneCaption2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.paneCaption2.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte)(135)), ((System.Byte)(195)), ((System.Byte)(255)));
			this.paneCaption2.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte)(90)), ((System.Byte)(135)), ((System.Byte)(215)));
			this.paneCaption2.Location = new System.Drawing.Point(0, 24);
			this.paneCaption2.Name = "paneCaption2";
			this.paneCaption2.Size = new System.Drawing.Size(292, 242);
			this.paneCaption2.TabIndex = 1;
			// 
			// activityBar1
			// 
			this.activityBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.activityBar1.BackColor = System.Drawing.Color.Transparent;
			this.activityBar1.BarColor = System.Drawing.SystemColors.InactiveCaption;
			this.activityBar1.BorderColor = System.Drawing.SystemColors.Desktop;
			this.activityBar1.Enabled = false;
			this.activityBar1.HighlightColor = System.Drawing.SystemColors.Menu;
			this.activityBar1.Location = new System.Drawing.Point(0, 203);
			this.activityBar1.Name = "activityBar1";
			this.activityBar1.Size = new System.Drawing.Size(288, 8);
			this.activityBar1.TabIndex = 5;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(184, 72);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel1";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(136, 240);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "Start";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.BackColor = System.Drawing.Color.Transparent;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(216, 240);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "Stop";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// xpGroupBoxContainer1
			// 
			this.xpGroupBoxContainer1.AutoScroll = true;
			this.xpGroupBoxContainer1.Controls.Add(this.xpGroupBox2);
			this.xpGroupBoxContainer1.Location = new System.Drawing.Point(0, 24);
			this.xpGroupBoxContainer1.Name = "xpGroupBoxContainer1";
			this.xpGroupBoxContainer1.PaneBottomRightColor = System.Drawing.SystemColors.ActiveCaption;
			this.xpGroupBoxContainer1.PaneOutlineColor = System.Drawing.SystemColors.ControlDarkDark;
			this.xpGroupBoxContainer1.PaneTopLeftColor = System.Drawing.SystemColors.InactiveCaption;
			this.xpGroupBoxContainer1.Size = new System.Drawing.Size(160, 200);
			this.xpGroupBoxContainer1.TabIndex = 6;
			// 
			// xpGroupBox2
			// 
			this.xpGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.xpGroupBox2.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
			this.xpGroupBox2.CaptionFontColor = System.Drawing.Color.Black;
			this.xpGroupBox2.CaptionFontHighLightColor = System.Drawing.Color.Red;
			this.xpGroupBox2.CaptionIcon = ((System.Drawing.Icon)(resources.GetObject("xpGroupBox2.CaptionIcon")));
			this.xpGroupBox2.CaptionLeftColor = System.Drawing.Color.White;
			this.xpGroupBox2.CaptionRightColor = System.Drawing.Color.FromArgb(((System.Byte)(198)), ((System.Byte)(210)), ((System.Byte)(248)));
			this.xpGroupBox2.CaptionText = "My Caption";
			this.xpGroupBox2.Location = new System.Drawing.Point(10, 10);
			this.xpGroupBox2.Name = "xpGroupBox2";
			this.xpGroupBox2.PaneBottomRightColor = System.Drawing.Color.FromArgb(((System.Byte)(214)), ((System.Byte)(223)), ((System.Byte)(247)));
			this.xpGroupBox2.PaneOutlineColor = System.Drawing.Color.White;
			this.xpGroupBox2.PaneTopLeftColor = System.Drawing.Color.White;
			this.xpGroupBox2.Size = new System.Drawing.Size(140, 121);
			this.xpGroupBox2.TabIndex = 1;
			this.xpGroupBox2.XPGroupBoxItemsList.AddRange(new SpottyDogSoftware.Controls.XPGroupBoxItem[] {
																											  new SpottyDogSoftware.Controls.XPGroupBoxItem(((System.Drawing.Icon)(resources.GetObject("resource"))), "item0", "My text", false),
																											  new SpottyDogSoftware.Controls.XPGroupBoxItem(((System.Drawing.Icon)(resources.GetObject("resource1"))), "item2", "More text still", true),
																											  new SpottyDogSoftware.Controls.XPGroupBoxItem(((System.Drawing.Icon)(resources.GetObject("resource2"))), "item1", "My Text 2", true),
																											  new SpottyDogSoftware.Controls.XPGroupBoxItem(null, "item3", "bob", true)});
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.xpGroupBoxContainer1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.paneCaption2);
			this.Controls.Add(this.paneCaption1);
			this.Name = "Form1";
			this.Text = "Control TestRig";
			this.paneCaption2.ResumeLayout(false);
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
			Application.EnableVisualStyles();
			Application.DoEvents();
			Application.Run(new Form1x());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			activityBar1.Enabled = true;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.Default;
			activityBar1.Enabled = false;
		}
	}
}
