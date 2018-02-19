using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using GlacialComponents.Controls;
using System.Diagnostics;

namespace TestGlacialList
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private GlacialComponents.Controls.GlacialList glacialList1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLItem glItem1 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem1 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem2 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem3 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem4 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem2 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem5 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem6 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem7 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem8 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem3 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem9 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem10 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem11 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem12 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem4 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem13 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem14 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem15 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem16 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLItem glItem5 = new GlacialComponents.Controls.GLItem();
            GlacialComponents.Controls.GLSubItem glSubItem17 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem18 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem19 = new GlacialComponents.Controls.GLSubItem();
            GlacialComponents.Controls.GLSubItem glSubItem20 = new GlacialComponents.Controls.GLSubItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.glacialList1 = new GlacialComponents.Controls.GlacialList();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "test";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "test2";
            // 
            // glacialList1
            // 
            this.glacialList1.AllowColumnResize = true;
            this.glacialList1.AllowMultiselect = false;
            this.glacialList1.AlternateBackground = System.Drawing.Color.ForestGreen;
            this.glacialList1.AlternatingColors = true;
            this.glacialList1.AutoHeight = true;
            this.glacialList1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glacialList1.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
            glColumn1.CheckBoxes = true;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "Column1";
            glColumn1.NumericSort = false;
            glColumn1.Text = "Name";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 130;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.DateTimePicker;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "Column2";
            glColumn2.NumericSort = false;
            glColumn2.Text = "Released";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 140;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "Column3";
            glColumn3.NumericSort = false;
            glColumn3.Text = "Download";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 100;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "Column4";
            glColumn4.NumericSort = false;
            glColumn4.Text = "Rating";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn4.Width = 100;
            this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4});
            this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.XP;
            this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glacialList1.FullRowSelect = true;
            this.glacialList1.GridColor = System.Drawing.Color.LightGray;
            this.glacialList1.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.glacialList1.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.glacialList1.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.glacialList1.HeaderHeight = 22;
            this.glacialList1.HeaderVisible = true;
            this.glacialList1.HeaderWordWrap = false;
            this.glacialList1.HotColumnTracking = true;
            this.glacialList1.HotItemTracking = true;
            this.glacialList1.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glacialList1.HoverEvents = false;
            this.glacialList1.HoverTime = 1;
            this.glacialList1.ImageList = this.imageList1;
            this.glacialList1.ItemHeight = 20;
            glItem1.BackColor = System.Drawing.Color.White;
            glItem1.ForeColor = System.Drawing.Color.Black;
            glItem1.RowBorderColor = System.Drawing.Color.Black;
            glItem1.RowBorderSize = 0;
            glSubItem1.BackColor = System.Drawing.Color.Empty;
            glSubItem1.Checked = false;
            glSubItem1.ForceText = false;
            glSubItem1.ForeColor = System.Drawing.Color.Black;
            glSubItem1.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem1.ImageIndex = -1;
            glSubItem1.Text = "Glacial List 1.3";
            glSubItem2.BackColor = System.Drawing.Color.Empty;
            glSubItem2.Checked = false;
            glSubItem2.ForceText = false;
            glSubItem2.ForeColor = System.Drawing.Color.Black;
            glSubItem2.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem2.ImageIndex = -1;
            glSubItem2.Text = "1/7/2004";
            glSubItem3.BackColor = System.Drawing.Color.Empty;
            glSubItem3.Checked = false;
            glSubItem3.ForceText = false;
            glSubItem3.ForeColor = System.Drawing.Color.Black;
            glSubItem3.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem3.ImageIndex = -1;
            glSubItem3.Text = "40";
            glSubItem4.BackColor = System.Drawing.Color.Empty;
            glSubItem4.Checked = false;
            glSubItem4.ForceText = false;
            glSubItem4.ForeColor = System.Drawing.Color.Black;
            glSubItem4.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem4.ImageIndex = 2;
            glSubItem4.Text = "";
            glItem1.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem1,
            glSubItem2,
            glSubItem3,
            glSubItem4});
            glItem1.Text = "Glacial List 1.3";
            glItem2.BackColor = System.Drawing.Color.White;
            glItem2.ForeColor = System.Drawing.Color.Black;
            glItem2.RowBorderColor = System.Drawing.Color.Black;
            glItem2.RowBorderSize = 0;
            glSubItem5.BackColor = System.Drawing.Color.Empty;
            glSubItem5.Checked = false;
            glSubItem5.ForceText = false;
            glSubItem5.ForeColor = System.Drawing.Color.Black;
            glSubItem5.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem5.ImageIndex = -1;
            glSubItem5.Text = "Glacial List 1.28";
            glSubItem6.BackColor = System.Drawing.Color.Empty;
            glSubItem6.Checked = false;
            glSubItem6.ForceText = false;
            glSubItem6.ForeColor = System.Drawing.Color.Black;
            glSubItem6.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem6.ImageIndex = -1;
            glSubItem6.Text = "12/30/2003";
            glSubItem7.BackColor = System.Drawing.Color.Empty;
            glSubItem7.Checked = false;
            glSubItem7.ForceText = false;
            glSubItem7.ForeColor = System.Drawing.Color.Black;
            glSubItem7.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem7.ImageIndex = -1;
            glSubItem7.Text = "90";
            glSubItem8.BackColor = System.Drawing.Color.Empty;
            glSubItem8.Checked = false;
            glSubItem8.ForceText = false;
            glSubItem8.ForeColor = System.Drawing.Color.Black;
            glSubItem8.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem8.ImageIndex = 2;
            glSubItem8.Text = "";
            glItem2.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem5,
            glSubItem6,
            glSubItem7,
            glSubItem8});
            glItem2.Text = "Glacial List 1.28";
            glItem3.BackColor = System.Drawing.Color.White;
            glItem3.ForeColor = System.Drawing.Color.Black;
            glItem3.RowBorderColor = System.Drawing.Color.Black;
            glItem3.RowBorderSize = 0;
            glSubItem9.BackColor = System.Drawing.Color.Empty;
            glSubItem9.Checked = false;
            glSubItem9.ForceText = false;
            glSubItem9.ForeColor = System.Drawing.Color.Black;
            glSubItem9.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem9.ImageIndex = -1;
            glSubItem9.Text = "Glacial List 1.26";
            glSubItem10.BackColor = System.Drawing.Color.Empty;
            glSubItem10.Checked = false;
            glSubItem10.ForceText = false;
            glSubItem10.ForeColor = System.Drawing.Color.Black;
            glSubItem10.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem10.ImageIndex = -1;
            glSubItem10.Text = "12/22/2003";
            glSubItem11.BackColor = System.Drawing.Color.Empty;
            glSubItem11.Checked = false;
            glSubItem11.ForceText = false;
            glSubItem11.ForeColor = System.Drawing.Color.Black;
            glSubItem11.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem11.ImageIndex = -1;
            glSubItem11.Text = "80";
            glSubItem12.BackColor = System.Drawing.Color.Empty;
            glSubItem12.Checked = false;
            glSubItem12.ForceText = false;
            glSubItem12.ForeColor = System.Drawing.Color.Black;
            glSubItem12.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem12.ImageIndex = 1;
            glSubItem12.Text = "";
            glItem3.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem9,
            glSubItem10,
            glSubItem11,
            glSubItem12});
            glItem3.Text = "Glacial List 1.26";
            glItem4.BackColor = System.Drawing.Color.White;
            glItem4.ForeColor = System.Drawing.Color.Black;
            glItem4.RowBorderColor = System.Drawing.Color.Black;
            glItem4.RowBorderSize = 0;
            glSubItem13.BackColor = System.Drawing.Color.Empty;
            glSubItem13.Checked = false;
            glSubItem13.ForceText = false;
            glSubItem13.ForeColor = System.Drawing.Color.Black;
            glSubItem13.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem13.ImageIndex = -1;
            glSubItem13.Text = "Glacial List 1.1";
            glSubItem14.BackColor = System.Drawing.Color.Empty;
            glSubItem14.Checked = false;
            glSubItem14.ForceText = false;
            glSubItem14.ForeColor = System.Drawing.Color.Black;
            glSubItem14.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem14.ImageIndex = -1;
            glSubItem14.Text = "7/9/2003";
            glSubItem15.BackColor = System.Drawing.Color.Empty;
            glSubItem15.Checked = false;
            glSubItem15.ForceText = false;
            glSubItem15.ForeColor = System.Drawing.Color.Black;
            glSubItem15.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem15.ImageIndex = -1;
            glSubItem15.Text = "Done";
            glSubItem16.BackColor = System.Drawing.Color.Empty;
            glSubItem16.Checked = false;
            glSubItem16.ForceText = false;
            glSubItem16.ForeColor = System.Drawing.Color.Black;
            glSubItem16.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem16.ImageIndex = 1;
            glSubItem16.Text = "";
            glItem4.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem13,
            glSubItem14,
            glSubItem15,
            glSubItem16});
            glItem4.Text = "Glacial List 1.1";
            glItem5.BackColor = System.Drawing.Color.White;
            glItem5.ForeColor = System.Drawing.Color.Black;
            glItem5.RowBorderColor = System.Drawing.Color.Black;
            glItem5.RowBorderSize = 0;
            glSubItem17.BackColor = System.Drawing.Color.Empty;
            glSubItem17.Checked = false;
            glSubItem17.ForceText = false;
            glSubItem17.ForeColor = System.Drawing.Color.Black;
            glSubItem17.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem17.ImageIndex = -1;
            glSubItem17.Text = "Glacial List 1.0";
            glSubItem18.BackColor = System.Drawing.Color.Empty;
            glSubItem18.Checked = false;
            glSubItem18.ForceText = false;
            glSubItem18.ForeColor = System.Drawing.Color.Black;
            glSubItem18.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem18.ImageIndex = -1;
            glSubItem18.Text = "6/13/2003";
            glSubItem19.BackColor = System.Drawing.Color.Empty;
            glSubItem19.Checked = false;
            glSubItem19.ForceText = false;
            glSubItem19.ForeColor = System.Drawing.Color.Black;
            glSubItem19.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem19.ImageIndex = -1;
            glSubItem19.Text = "23";
            glSubItem20.BackColor = System.Drawing.Color.Empty;
            glSubItem20.Checked = false;
            glSubItem20.ForceText = false;
            glSubItem20.ForeColor = System.Drawing.Color.Black;
            glSubItem20.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            glSubItem20.ImageIndex = 0;
            glSubItem20.Text = "";
            glItem5.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] {
            glSubItem17,
            glSubItem18,
            glSubItem19,
            glSubItem20});
            glItem5.Text = "Glacial List 1.0";
            this.glacialList1.Items.AddRange(new GlacialComponents.Controls.GLItem[] {
            glItem1,
            glItem2,
            glItem3,
            glItem4,
            glItem5});
            this.glacialList1.ItemWordWrap = false;
            this.glacialList1.Location = new System.Drawing.Point(0, 0);
            this.glacialList1.Name = "glacialList1";
            this.glacialList1.Selectable = true;
            this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
            this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glacialList1.ShowBorder = true;
            this.glacialList1.ShowFocusRect = false;
            this.glacialList1.Size = new System.Drawing.Size(496, 132);
            this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.MergeSort;
            this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glacialList1.TabIndex = 0;
            this.glacialList1.Text = "glacialList1";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(496, 132);
            this.Controls.Add(this.glacialList1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
			Application.Run(new Form1());
		}

		private void glacialList1_HoverEvent(object source, GlacialComponents.Controls.HoverEventArgs e)
		{
			if ( e.HoverType == HoverTypes.HoverStart )
			{
				// do hover start stuff
				if ( e.Region == GLListRegion.client )
				{
					// its in the client area
				}
			}
			if ( e.HoverType == HoverTypes.HoverEnd )
			{
				// do hover end stuff
			}
		}

		private void glacialList1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//GLColumn column = this.glacialList2.Columns.Add( "First column", 100 );
			//column.ActivatedEmbeddedType = GLActivatedEmbeddedTypes.TextBox;


			ProgressBar pb;

			pb = new ProgressBar();
			pb.Value = 60;
			pb.Maximum = 100;
			this.glacialList1.Items[0].SubItems[2].Control = pb;

			pb = new ProgressBar();
			pb.Value = 20;
			pb.Maximum = 100;
			this.glacialList1.Items[1].SubItems[2].Control = pb;

			pb = new ProgressBar();
			pb.Value = 100;
			pb.Maximum = 100;
			this.glacialList1.Items[2].SubItems[2].Control = pb;

			pb = new ProgressBar();
			pb.Value = 45;
			pb.Maximum = 100;
			this.glacialList1.Items[4].SubItems[2].Control = pb;


			this.glacialList1.Items.Add( "timma" );

		}

		private void glacialList2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Debug.WriteLine( "Test" );
		}



	}
}
