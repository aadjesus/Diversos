using System ;
using System.Drawing ;
using System.Drawing.Imaging ;
using System.Collections ;
using System.ComponentModel ;
using System.Windows.Forms ;
using System.Data ;
using UIComponents ;

namespace XPPanelTest {
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form {
		private System.Windows.Forms.CheckBox checkBox1 ;
		private System.Windows.Forms.CheckBox checkBox2 ;
		private UIComponents.XPPanelGroup xpPanelGroup1 ;
		private UIComponents.XPPanel xpPanel1 ;
		private UIComponents.XPPanel xpPanel2 ;
		private UIComponents.XPPanel xpPanel3 ;
		private UIComponents.XPPanelGroup xpPanelGroup2 ;
		private UIComponents.XPPanel xpPanel4 ;
		private UIComponents.XPPanel xpPanel5 ;
		private UIComponents.XPPanel xpPanel6 ;
		private System.Windows.Forms.TreeView treeView1 ;
		private System.Windows.Forms.LinkLabel linkLabel1 ;
		private System.Windows.Forms.LinkLabel linkLabel2 ;
		private System.Windows.Forms.LinkLabel linkLabel3 ;
		private System.Windows.Forms.LinkLabel linkLabel4 ;
		private System.Windows.Forms.LinkLabel linkLabel5 ;
		private System.Windows.Forms.LinkLabel linkLabel6 ;
		private System.Windows.Forms.CheckBox checkBox3 ;
		private UIComponents.ImageSet MediaItemImageSet ;
		private UIComponents.ImageSet purpleGlyphsImageSet ;
		private System.Windows.Forms.CheckBox checkBox4 ;
		private UIComponents.ItemLayoutPanel itemLayoutPanel1 ;
		private System.Windows.Forms.GroupBox groupBox1 ;
		private System.Windows.Forms.CheckBox checkBox5 ;
		private System.Windows.Forms.CheckBox checkBox6 ;
		private System.Windows.Forms.TextBox textBox1 ;
		private System.Windows.Forms.Button button1 ;
		private System.Windows.Forms.Label label1 ;
		private System.Windows.Forms.Button button2 ;
		private System.Windows.Forms.GroupBox groupBox2 ;
		private System.Windows.Forms.RadioButton radioButton1 ;
		private System.Windows.Forms.RadioButton radioButton2 ;
		private System.Windows.Forms.RadioButton radioButton3 ;
		private System.Windows.Forms.ImageList imageList1 ;
		private UIComponents.ItemLayoutPanel itemLayoutPanel2 ;
		private System.Windows.Forms.LinkLabel linkLabel7 ;
		private System.Windows.Forms.LinkLabel linkLabel8 ;
		private System.Windows.Forms.LinkLabel linkLabel9 ;
		private System.Windows.Forms.LinkLabel linkLabel10 ;
		private System.Windows.Forms.LinkLabel linkLabel11 ;
		private System.Windows.Forms.LinkLabel linkLabel12 ;
		private System.Windows.Forms.ImageList imageList2 ;
		private UIComponents.TextLayoutPanel textLayoutPanel1 ;
		private UIComponents.TextStyle HeaderStyle ;
		private UIComponents.TextStyle TypeStyle ;
		private UIComponents.TextStyle SectionHeaderStyle ;
        private Button button3;
		private System.ComponentModel.IContainer components ;

		public Form1() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent() ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose() ;
				}
			}
			base.Dispose(disposing) ;
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("My Media");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Media Manager");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Program Files", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Windows");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("C:\\", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode8,
            treeNode9});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            UIComponents.TextElement textElement11 = new UIComponents.TextElement("", "TestApp.exe", null);
            UIComponents.TextElement textElement12 = new UIComponents.TextElement("", "Application", null);
            UIComponents.TextElement textElement13 = new UIComponents.TextElement("Date Modified", "Friday, June 25, 2004, 5:16 PM", null);
            UIComponents.TextElement textElement14 = new UIComponents.TextElement("Size", "128.65 KB", null);
            UIComponents.TextElement textElement15 = new UIComponents.TextElement("", "Audio", null);
            UIComponents.TextElement textElement16 = new UIComponents.TextElement("Bit Rate", "320 kbs", null);
            UIComponents.TextElement textElement17 = new UIComponents.TextElement("Sample Rate", "44.1 KHz", null);
            UIComponents.TextElement textElement18 = new UIComponents.TextElement("", "Video", null);
            UIComponents.TextElement textElement19 = new UIComponents.TextElement("Bit Rate", "1.5 MB", null);
            UIComponents.TextElement textElement20 = new UIComponents.TextElement("Dimensions", "640 x 480 x 24", null);
            this.HeaderStyle = new UIComponents.TextStyle();
            this.TypeStyle = new UIComponents.TextStyle();
            this.SectionHeaderStyle = new UIComponents.TextStyle();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.xpPanelGroup1 = new UIComponents.XPPanelGroup();
            this.xpPanel3 = new UIComponents.XPPanel(160);
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.MediaItemImageSet = new UIComponents.ImageSet();
            this.xpPanel2 = new UIComponents.XPPanel(216);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.xpPanel1 = new UIComponents.XPPanel(176);
            this.purpleGlyphsImageSet = new UIComponents.ImageSet();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.xpPanelGroup2 = new UIComponents.XPPanelGroup();
            this.xpPanel6 = new UIComponents.XPPanel(160);
            this.textLayoutPanel1 = new UIComponents.TextLayoutPanel();
            this.xpPanel5 = new UIComponents.XPPanel(224);
            this.itemLayoutPanel1 = new UIComponents.ItemLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.xpPanel4 = new UIComponents.XPPanel(232);
            this.itemLayoutPanel2 = new UIComponents.ItemLayoutPanel();
            this.linkLabel12 = new System.Windows.Forms.LinkLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.linkLabel11 = new System.Windows.Forms.LinkLabel();
            this.linkLabel10 = new System.Windows.Forms.LinkLabel();
            this.linkLabel9 = new System.Windows.Forms.LinkLabel();
            this.linkLabel8 = new System.Windows.Forms.LinkLabel();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup1)).BeginInit();
            this.xpPanelGroup1.SuspendLayout();
            this.xpPanel3.SuspendLayout();
            this.xpPanel2.SuspendLayout();
            this.xpPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup2)).BeginInit();
            this.xpPanelGroup2.SuspendLayout();
            this.xpPanel6.SuspendLayout();
            this.xpPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemLayoutPanel1)).BeginInit();
            this.itemLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.xpPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemLayoutPanel2)).BeginInit();
            this.itemLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderStyle
            // 
            this.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
            this.HeaderStyle.Description = "";
            this.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderStyle.HorzAlign = System.Drawing.StringAlignment.Near;
            this.HeaderStyle.ImageSet = null;
            this.HeaderStyle.Indent = 0;
            this.HeaderStyle.SpacingAdjustment = new System.Drawing.Size(0, 0);
            this.HeaderStyle.TextColor = System.Drawing.Color.Empty;
            this.HeaderStyle.VertAlign = System.Drawing.StringAlignment.Center;
            // 
            // TypeStyle
            // 
            this.TypeStyle.BackColor = System.Drawing.Color.Transparent;
            this.TypeStyle.Description = "";
            this.TypeStyle.Font = null;
            this.TypeStyle.HorzAlign = System.Drawing.StringAlignment.Near;
            this.TypeStyle.ImageSet = null;
            this.TypeStyle.Indent = 0;
            this.TypeStyle.SpacingAdjustment = new System.Drawing.Size(-4, 0);
            this.TypeStyle.TextColor = System.Drawing.Color.Empty;
            this.TypeStyle.VertAlign = System.Drawing.StringAlignment.Center;
            // 
            // SectionHeaderStyle
            // 
            this.SectionHeaderStyle.BackColor = System.Drawing.Color.Transparent;
            this.SectionHeaderStyle.Description = "";
            this.SectionHeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionHeaderStyle.HorzAlign = System.Drawing.StringAlignment.Near;
            this.SectionHeaderStyle.ImageSet = null;
            this.SectionHeaderStyle.Indent = 0;
            this.SectionHeaderStyle.SpacingAdjustment = new System.Drawing.Size(2, 0);
            this.SectionHeaderStyle.TextColor = System.Drawing.Color.White;
            this.SectionHeaderStyle.VertAlign = System.Drawing.StringAlignment.Center;
            // 
            // checkBox1
            // 
            this.checkBox1.CausesValidation = false;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(216, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(152, 24);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Enable Media File Tasks";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(216, 48);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(144, 24);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "Enable Media Browser";
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // xpPanelGroup1
            // 
            this.xpPanelGroup1.AutoScroll = true;
            this.xpPanelGroup1.BackColor = System.Drawing.Color.Transparent;
            this.xpPanelGroup1.Controls.Add(this.xpPanel3);
            this.xpPanelGroup1.Controls.Add(this.xpPanel2);
            this.xpPanelGroup1.Controls.Add(this.xpPanel1);
            this.xpPanelGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xpPanelGroup1.Location = new System.Drawing.Point(0, 0);
            this.xpPanelGroup1.Name = "xpPanelGroup1";
            this.xpPanelGroup1.PanelGradient = ((UIComponents.GradientColor)(resources.GetObject("xpPanelGroup1.PanelGradient")));
            this.xpPanelGroup1.Size = new System.Drawing.Size(200, 606);
            this.xpPanelGroup1.TabIndex = 4;
            // 
            // xpPanel3
            // 
            this.xpPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpPanel3.BackColor = System.Drawing.Color.Transparent;
            this.xpPanel3.Caption = "Media Network";
            this.xpPanel3.CaptionCornerType = ((UIComponents.CornerType)((UIComponents.CornerType.TopLeft | UIComponents.CornerType.TopRight)));
            this.xpPanel3.CaptionGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.xpPanel3.CaptionGradient.Start = System.Drawing.Color.White;
            this.xpPanel3.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel3.CaptionUnderline = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xpPanel3.CollapsedGlyphs.ImageSet = null;
            this.xpPanel3.Controls.Add(this.linkLabel6);
            this.xpPanel3.Controls.Add(this.linkLabel2);
            this.xpPanel3.Controls.Add(this.linkLabel1);
            this.xpPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpPanel3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xpPanel3.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.xpPanel3.ImageItems.Highlight = 0;
            this.xpPanel3.ImageItems.ImageSet = this.MediaItemImageSet;
            this.xpPanel3.ImageItems.Normal = 0;
            this.xpPanel3.ImageItems.Pressed = 0;
            this.xpPanel3.Location = new System.Drawing.Point(8, 416);
            this.xpPanel3.Name = "xpPanel3";
            this.xpPanel3.PanelGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpPanel3.PanelGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpPanel3.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel3.Size = new System.Drawing.Size(184, 160);
            this.xpPanel3.TabIndex = 2;
            this.xpPanel3.TextColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.xpPanel3.TextHighlightColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.xpPanel3.VertAlignment = System.Drawing.StringAlignment.Center;
            this.xpPanel3.XPPanelStyle = UIComponents.XPPanelStyle.WindowsXP;
            // 
            // linkLabel6
            // 
            this.linkLabel6.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.linkLabel6.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel6.Image")));
            this.linkLabel6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel6.Location = new System.Drawing.Point(8, 128);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(128, 23);
            this.linkLabel6.TabIndex = 2;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Help with Media";
            this.linkLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.linkLabel2.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel2.Image")));
            this.linkLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel2.Location = new System.Drawing.Point(8, 96);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(120, 23);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Transfer Media";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.linkLabel1.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel1.Image")));
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.Location = new System.Drawing.Point(8, 64);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(128, 23);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Search for Media";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MediaItemImageSet
            // 
            this.MediaItemImageSet.Images.AddRange(new System.Drawing.Image[] {
            ((System.Drawing.Image)(resources.GetObject("MediaItemImageSet.Images"))),
            ((System.Drawing.Image)(resources.GetObject("MediaItemImageSet.Images1"))),
            ((System.Drawing.Image)(resources.GetObject("MediaItemImageSet.Images2"))),
            ((System.Drawing.Image)(resources.GetObject("MediaItemImageSet.Images3"))),
            ((System.Drawing.Image)(resources.GetObject("MediaItemImageSet.Images4")))});
            this.MediaItemImageSet.Size = new System.Drawing.Size(48, 48);
            this.MediaItemImageSet.TransparentColor = System.Drawing.Color.Empty;
            // 
            // xpPanel2
            // 
            this.xpPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpPanel2.BackColor = System.Drawing.Color.Transparent;
            this.xpPanel2.Caption = "Media Browser";
            this.xpPanel2.CaptionCornerType = ((UIComponents.CornerType)((UIComponents.CornerType.TopLeft | UIComponents.CornerType.TopRight)));
            this.xpPanel2.CaptionGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.xpPanel2.CaptionGradient.Start = System.Drawing.Color.White;
            this.xpPanel2.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel2.CaptionUnderline = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xpPanel2.CollapsedGlyphs.Disabled = 9;
            this.xpPanel2.CollapsedGlyphs.Highlight = 10;
            this.xpPanel2.CollapsedGlyphs.ImageSet = null;
            this.xpPanel2.CollapsedGlyphs.Normal = 8;
            this.xpPanel2.CollapsedGlyphs.Pressed = 10;
            this.xpPanel2.Controls.Add(this.treeView1);
            this.xpPanel2.CurveRadius = 12;
            this.xpPanel2.ExpandedGlyphs.Highlight = 5;
            this.xpPanel2.ExpandedGlyphs.ImageSet = null;
            this.xpPanel2.ExpandedGlyphs.Normal = 4;
            this.xpPanel2.ExpandedGlyphs.Pressed = 6;
            this.xpPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpPanel2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xpPanel2.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.xpPanel2.ImageItems.Highlight = 1;
            this.xpPanel2.ImageItems.ImageSet = this.MediaItemImageSet;
            this.xpPanel2.ImageItems.Normal = 2;
            this.xpPanel2.ImageItems.Pressed = 1;
            this.xpPanel2.Location = new System.Drawing.Point(8, 192);
            this.xpPanel2.Name = "xpPanel2";
            this.xpPanel2.PanelGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpPanel2.PanelGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpPanel2.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel2.Size = new System.Drawing.Size(184, 216);
            this.xpPanel2.TabIndex = 1;
            this.xpPanel2.TextColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.xpPanel2.TextHighlightColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.xpPanel2.VertAlignment = System.Drawing.StringAlignment.Center;
            this.xpPanel2.XPPanelStyle = UIComponents.XPPanelStyle.WindowsXP;
            this.xpPanel2.SizeChanged += new System.EventHandler(this.xpPanel2_SizeChanged);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Location = new System.Drawing.Point(8, 64);
            this.treeView1.Name = "treeView1";
            treeNode6.Name = "";
            treeNode6.Text = "My Media";
            treeNode7.Name = "";
            treeNode7.Text = "Media Manager";
            treeNode8.Name = "";
            treeNode8.Text = "Program Files";
            treeNode9.Name = "";
            treeNode9.Text = "Windows";
            treeNode10.Name = "";
            treeNode10.Text = "C:\\";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView1.Size = new System.Drawing.Size(168, 144);
            this.treeView1.TabIndex = 0;
            // 
            // xpPanel1
            // 
            this.xpPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpPanel1.BackColor = System.Drawing.Color.Transparent;
            this.xpPanel1.Caption = "Media File Tasks";
            this.xpPanel1.CaptionCornerType = ((UIComponents.CornerType)((UIComponents.CornerType.TopLeft | UIComponents.CornerType.TopRight)));
            this.xpPanel1.CaptionGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.xpPanel1.CaptionGradient.Start = System.Drawing.Color.White;
            this.xpPanel1.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel1.CaptionUnderline = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xpPanel1.CollapsedGlyphs.Highlight = 1;
            this.xpPanel1.CollapsedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel1.CollapsedGlyphs.Normal = 0;
            this.xpPanel1.CollapsedGlyphs.Pressed = 1;
            this.xpPanel1.Controls.Add(this.linkLabel5);
            this.xpPanel1.Controls.Add(this.linkLabel4);
            this.xpPanel1.Controls.Add(this.linkLabel3);
            this.xpPanel1.ExpandedGlyphs.Highlight = 3;
            this.xpPanel1.ExpandedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel1.ExpandedGlyphs.Normal = 2;
            this.xpPanel1.ExpandedGlyphs.Pressed = 3;
            this.xpPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpPanel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xpPanel1.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.xpPanel1.ImageItems.Highlight = 3;
            this.xpPanel1.ImageItems.ImageSet = this.MediaItemImageSet;
            this.xpPanel1.ImageItems.Normal = 4;
            this.xpPanel1.ImageItems.Pressed = 3;
            this.xpPanel1.Location = new System.Drawing.Point(8, 8);
            this.xpPanel1.Name = "xpPanel1";
            this.xpPanel1.PanelGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpPanel1.PanelGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.xpPanel1.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel1.Size = new System.Drawing.Size(184, 176);
            this.xpPanel1.TabIndex = 0;
            this.xpPanel1.TextColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.xpPanel1.TextHighlightColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.xpPanel1.VertAlignment = System.Drawing.StringAlignment.Center;
            this.xpPanel1.XPPanelStyle = UIComponents.XPPanelStyle.WindowsXP;
            // 
            // purpleGlyphsImageSet
            // 
            this.purpleGlyphsImageSet.Images.AddRange(new System.Drawing.Image[] {
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images"))),
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images1"))),
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images2"))),
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images3")))});
            this.purpleGlyphsImageSet.Size = new System.Drawing.Size(21, 21);
            this.purpleGlyphsImageSet.TransparentColor = System.Drawing.Color.Empty;
            // 
            // linkLabel5
            // 
            this.linkLabel5.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.linkLabel5.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel5.Image")));
            this.linkLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel5.Location = new System.Drawing.Point(8, 144);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(152, 23);
            this.linkLabel5.TabIndex = 2;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Remove Media";
            this.linkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel4
            // 
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.linkLabel4.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel4.Image")));
            this.linkLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel4.Location = new System.Drawing.Point(8, 100);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(128, 23);
            this.linkLabel4.TabIndex = 1;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Play Media";
            this.linkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.linkLabel3.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel3.Image")));
            this.linkLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel3.Location = new System.Drawing.Point(8, 56);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(144, 23);
            this.linkLabel3.TabIndex = 0;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Create Media Image";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xpPanelGroup2
            // 
            this.xpPanelGroup2.AutoScroll = true;
            this.xpPanelGroup2.BackColor = System.Drawing.Color.Transparent;
            this.xpPanelGroup2.Controls.Add(this.xpPanel6);
            this.xpPanelGroup2.Controls.Add(this.xpPanel5);
            this.xpPanelGroup2.Controls.Add(this.xpPanel4);
            this.xpPanelGroup2.Dock = System.Windows.Forms.DockStyle.Right;
            this.xpPanelGroup2.Location = new System.Drawing.Point(568, 0);
            this.xpPanelGroup2.Name = "xpPanelGroup2";
            this.xpPanelGroup2.PanelGradient = ((UIComponents.GradientColor)(resources.GetObject("xpPanelGroup2.PanelGradient")));
            this.xpPanelGroup2.Size = new System.Drawing.Size(224, 606);
            this.xpPanelGroup2.TabIndex = 5;
            // 
            // xpPanel6
            // 
            this.xpPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpPanel6.BackColor = System.Drawing.Color.Transparent;
            this.xpPanel6.Caption = "Details";
            this.xpPanel6.CaptionCornerType = ((UIComponents.CornerType)((((UIComponents.CornerType.TopLeft | UIComponents.CornerType.TopRight) 
            | UIComponents.CornerType.BottomLeft) 
            | UIComponents.CornerType.BottomRight)));
            this.xpPanel6.CaptionGradient.End = System.Drawing.Color.White;
            this.xpPanel6.CaptionGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(137)))), ((int)(((byte)(250)))));
            this.xpPanel6.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.xpPanel6.CaptionUnderline = System.Drawing.Color.Empty;
            this.xpPanel6.CollapsedGlyphs.Highlight = 3;
            this.xpPanel6.CollapsedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel6.CollapsedGlyphs.Normal = 2;
            this.xpPanel6.CollapsedGlyphs.Pressed = 3;
            this.xpPanel6.Controls.Add(this.textLayoutPanel1);
            this.xpPanel6.CurveRadius = 10;
            this.xpPanel6.ExpandedGlyphs.Highlight = 1;
            this.xpPanel6.ExpandedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel6.ExpandedGlyphs.Normal = 0;
            this.xpPanel6.ExpandedGlyphs.Pressed = 1;
            this.xpPanel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpPanel6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xpPanel6.GradientOffset = -0.6D;
            this.xpPanel6.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.xpPanel6.ImageItems.ImageSet = null;
            this.xpPanel6.Location = new System.Drawing.Point(8, 480);
            this.xpPanel6.Name = "xpPanel6";
            this.xpPanel6.OutlineColor = System.Drawing.Color.Empty;
            this.xpPanel6.PanelGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(88)))), ((int)(((byte)(248)))));
            this.xpPanel6.PanelGradient.Start = System.Drawing.Color.Transparent;
            this.xpPanel6.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.xpPanel6.Size = new System.Drawing.Size(191, 160);
            this.xpPanel6.TabIndex = 2;
            this.xpPanel6.TextColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xpPanel6.TextHighlightColors.Foreground = System.Drawing.Color.Violet;
            this.xpPanel6.VertAlignment = System.Drawing.StringAlignment.Center;
            // 
            // textLayoutPanel1
            // 
            this.textLayoutPanel1.AutoSize = true;
            this.textLayoutPanel1.BackgroundStyle = UIComponents.BackgroundStyle.Transparent;
            this.textLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            textElement11.TextStyle = this.HeaderStyle;
            textElement12.TextStyle = this.TypeStyle;
            textElement13.TextStyle = null;
            textElement14.TextStyle = null;
            textElement15.TextStyle = this.SectionHeaderStyle;
            textElement15.Visible = false;
            textElement16.TextStyle = null;
            textElement16.Visible = false;
            textElement17.TextStyle = null;
            textElement17.Visible = false;
            textElement18.TextStyle = this.SectionHeaderStyle;
            textElement18.Visible = false;
            textElement19.TextStyle = null;
            textElement19.Visible = false;
            textElement20.TextStyle = null;
            textElement20.Visible = false;
            this.textLayoutPanel1.Elements.AddRange(new UIComponents.ITextElement[] {
            textElement11,
            textElement12,
            textElement13,
            textElement14,
            textElement15,
            textElement16,
            textElement17,
            textElement18,
            textElement19,
            textElement20});
            this.textLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.textLayoutPanel1.ImageSize = new System.Drawing.Size(0, 0);
            this.textLayoutPanel1.Location = new System.Drawing.Point(1, 33);
            this.textLayoutPanel1.Name = "textLayoutPanel1";
            this.textLayoutPanel1.PanelGradient = ((UIComponents.GradientColor)(resources.GetObject("textLayoutPanel1.PanelGradient")));
            this.textLayoutPanel1.Size = new System.Drawing.Size(189, 126);
            this.textLayoutPanel1.TabIndex = 0;
            // 
            // xpPanel5
            // 
            this.xpPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpPanel5.BackColor = System.Drawing.Color.Transparent;
            this.xpPanel5.Caption = "Item Layout Panel";
            this.xpPanel5.CaptionCornerType = ((UIComponents.CornerType)((((UIComponents.CornerType.TopLeft | UIComponents.CornerType.TopRight) 
            | UIComponents.CornerType.BottomLeft) 
            | UIComponents.CornerType.BottomRight)));
            this.xpPanel5.CaptionGradient.End = System.Drawing.Color.White;
            this.xpPanel5.CaptionGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(137)))), ((int)(((byte)(250)))));
            this.xpPanel5.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.xpPanel5.CaptionUnderline = System.Drawing.Color.Empty;
            this.xpPanel5.CollapsedGlyphs.Highlight = 3;
            this.xpPanel5.CollapsedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel5.CollapsedGlyphs.Normal = 2;
            this.xpPanel5.CollapsedGlyphs.Pressed = 3;
            this.xpPanel5.Controls.Add(this.itemLayoutPanel1);
            this.xpPanel5.CurveRadius = 10;
            this.xpPanel5.ExpandedGlyphs.Highlight = 1;
            this.xpPanel5.ExpandedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel5.ExpandedGlyphs.Normal = 0;
            this.xpPanel5.ExpandedGlyphs.Pressed = 1;
            this.xpPanel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpPanel5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xpPanel5.GradientOffset = -0.6D;
            this.xpPanel5.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.xpPanel5.ImageItems.ImageSet = null;
            this.xpPanel5.Location = new System.Drawing.Point(8, 248);
            this.xpPanel5.Name = "xpPanel5";
            this.xpPanel5.OutlineColor = System.Drawing.Color.Empty;
            this.xpPanel5.PanelGradient.End = System.Drawing.Color.Transparent;
            this.xpPanel5.PanelGradient.Start = System.Drawing.Color.Transparent;
            this.xpPanel5.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xpPanel5.Size = new System.Drawing.Size(191, 224);
            this.xpPanel5.TabIndex = 1;
            this.xpPanel5.TextColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xpPanel5.TextHighlightColors.Foreground = System.Drawing.Color.Violet;
            this.xpPanel5.VertAlignment = System.Drawing.StringAlignment.Center;
            // 
            // itemLayoutPanel1
            // 
            this.itemLayoutPanel1.AutoSize = true;
            this.itemLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.itemLayoutPanel1.BackgroundStyle = UIComponents.BackgroundStyle.Transparent;
            this.itemLayoutPanel1.Controls.Add(this.button3);
            this.itemLayoutPanel1.Controls.Add(this.label1);
            this.itemLayoutPanel1.Controls.Add(this.textBox1);
            this.itemLayoutPanel1.Controls.Add(this.button1);
            this.itemLayoutPanel1.Controls.Add(this.groupBox1);
            this.itemLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemLayoutPanel1.Location = new System.Drawing.Point(1, 33);
            this.itemLayoutPanel1.Name = "itemLayoutPanel1";
            this.itemLayoutPanel1.PanelGradient = ((UIComponents.GradientColor)(resources.GetObject("itemLayoutPanel1.PanelGradient")));
            this.itemLayoutPanel1.Size = new System.Drawing.Size(189, 202);
            this.itemLayoutPanel1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 171);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "This is just some text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "textBox1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox6);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // checkBox6
            // 
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.Location = new System.Drawing.Point(16, 40);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(104, 24);
            this.checkBox6.TabIndex = 1;
            this.checkBox6.Text = "Show Button";
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(16, 16);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(104, 24);
            this.checkBox5.TabIndex = 0;
            this.checkBox5.Text = "Show Textbox";
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // xpPanel4
            // 
            this.xpPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpPanel4.BackColor = System.Drawing.Color.Transparent;
            this.xpPanel4.Caption = "Tasks";
            this.xpPanel4.CaptionCornerType = ((UIComponents.CornerType)((((UIComponents.CornerType.TopLeft | UIComponents.CornerType.TopRight) 
            | UIComponents.CornerType.BottomLeft) 
            | UIComponents.CornerType.BottomRight)));
            this.xpPanel4.CaptionGradient.End = System.Drawing.Color.White;
            this.xpPanel4.CaptionGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(137)))), ((int)(((byte)(250)))));
            this.xpPanel4.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.xpPanel4.CaptionUnderline = System.Drawing.Color.Empty;
            this.xpPanel4.CollapsedGlyphs.Highlight = 3;
            this.xpPanel4.CollapsedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel4.CollapsedGlyphs.Normal = 2;
            this.xpPanel4.CollapsedGlyphs.Pressed = 3;
            this.xpPanel4.Controls.Add(this.itemLayoutPanel2);
            this.xpPanel4.CurveRadius = 10;
            this.xpPanel4.ExpandedGlyphs.Highlight = 1;
            this.xpPanel4.ExpandedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.xpPanel4.ExpandedGlyphs.Normal = 0;
            this.xpPanel4.ExpandedGlyphs.Pressed = 1;
            this.xpPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xpPanel4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xpPanel4.GradientOffset = -0.6D;
            this.xpPanel4.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.xpPanel4.ImageItems.ImageSet = null;
            this.xpPanel4.Location = new System.Drawing.Point(8, 8);
            this.xpPanel4.Name = "xpPanel4";
            this.xpPanel4.OutlineColor = System.Drawing.Color.Empty;
            this.xpPanel4.PanelGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(88)))), ((int)(((byte)(248)))));
            this.xpPanel4.PanelGradient.Start = System.Drawing.Color.Transparent;
            this.xpPanel4.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.xpPanel4.Size = new System.Drawing.Size(191, 232);
            this.xpPanel4.TabIndex = 0;
            this.xpPanel4.TextColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xpPanel4.TextHighlightColors.Foreground = System.Drawing.Color.Violet;
            this.xpPanel4.VertAlignment = System.Drawing.StringAlignment.Center;
            // 
            // itemLayoutPanel2
            // 
            this.itemLayoutPanel2.AutoSize = true;
            this.itemLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.itemLayoutPanel2.BackgroundStyle = UIComponents.BackgroundStyle.Transparent;
            this.itemLayoutPanel2.BorderMargin = new System.Drawing.Size(4, 4);
            this.itemLayoutPanel2.Controls.Add(this.linkLabel12);
            this.itemLayoutPanel2.Controls.Add(this.linkLabel11);
            this.itemLayoutPanel2.Controls.Add(this.linkLabel10);
            this.itemLayoutPanel2.Controls.Add(this.linkLabel9);
            this.itemLayoutPanel2.Controls.Add(this.linkLabel8);
            this.itemLayoutPanel2.Controls.Add(this.linkLabel7);
            this.itemLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemLayoutPanel2.Location = new System.Drawing.Point(1, 33);
            this.itemLayoutPanel2.Name = "itemLayoutPanel2";
            this.itemLayoutPanel2.PanelGradient = ((UIComponents.GradientColor)(resources.GetObject("itemLayoutPanel2.PanelGradient")));
            this.itemLayoutPanel2.Size = new System.Drawing.Size(189, 186);
            this.itemLayoutPanel2.TabIndex = 0;
            // 
            // linkLabel12
            // 
            this.linkLabel12.ForeColor = System.Drawing.SystemColors.WindowText;
            this.linkLabel12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel12.ImageIndex = 5;
            this.linkLabel12.ImageList = this.imageList1;
            this.linkLabel12.LinkColor = System.Drawing.Color.WhiteSmoke;
            this.linkLabel12.Location = new System.Drawing.Point(4, 159);
            this.linkLabel12.Name = "linkLabel12";
            this.linkLabel12.Size = new System.Drawing.Size(140, 23);
            this.linkLabel12.TabIndex = 17;
            this.linkLabel12.TabStop = true;
            this.linkLabel12.Text = "Properties";
            this.linkLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel12.Visible = false;
            this.linkLabel12.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentSet2_LinkClicked);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            // 
            // linkLabel11
            // 
            this.linkLabel11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.linkLabel11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel11.ImageIndex = 4;
            this.linkLabel11.ImageList = this.imageList1;
            this.linkLabel11.LinkColor = System.Drawing.Color.WhiteSmoke;
            this.linkLabel11.Location = new System.Drawing.Point(4, 128);
            this.linkLabel11.Name = "linkLabel11";
            this.linkLabel11.Size = new System.Drawing.Size(140, 23);
            this.linkLabel11.TabIndex = 16;
            this.linkLabel11.TabStop = true;
            this.linkLabel11.Text = "Search Document";
            this.linkLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel11.Visible = false;
            this.linkLabel11.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentSet2_LinkClicked);
            // 
            // linkLabel10
            // 
            this.linkLabel10.ForeColor = System.Drawing.SystemColors.WindowText;
            this.linkLabel10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel10.ImageIndex = 3;
            this.linkLabel10.ImageList = this.imageList1;
            this.linkLabel10.LinkColor = System.Drawing.Color.WhiteSmoke;
            this.linkLabel10.Location = new System.Drawing.Point(4, 97);
            this.linkLabel10.Name = "linkLabel10";
            this.linkLabel10.Size = new System.Drawing.Size(140, 23);
            this.linkLabel10.TabIndex = 15;
            this.linkLabel10.TabStop = true;
            this.linkLabel10.Text = "Save Document";
            this.linkLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel10.Visible = false;
            this.linkLabel10.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentSet2_LinkClicked);
            // 
            // linkLabel9
            // 
            this.linkLabel9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.linkLabel9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel9.ImageIndex = 2;
            this.linkLabel9.ImageList = this.imageList1;
            this.linkLabel9.LinkColor = System.Drawing.Color.WhiteSmoke;
            this.linkLabel9.Location = new System.Drawing.Point(4, 66);
            this.linkLabel9.Name = "linkLabel9";
            this.linkLabel9.Size = new System.Drawing.Size(140, 23);
            this.linkLabel9.TabIndex = 14;
            this.linkLabel9.TabStop = true;
            this.linkLabel9.Text = "Find Document";
            this.linkLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel9.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentSet1_LinkClicked);
            // 
            // linkLabel8
            // 
            this.linkLabel8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.linkLabel8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel8.ImageIndex = 1;
            this.linkLabel8.ImageList = this.imageList1;
            this.linkLabel8.LinkColor = System.Drawing.Color.WhiteSmoke;
            this.linkLabel8.Location = new System.Drawing.Point(4, 35);
            this.linkLabel8.Name = "linkLabel8";
            this.linkLabel8.Size = new System.Drawing.Size(140, 23);
            this.linkLabel8.TabIndex = 13;
            this.linkLabel8.TabStop = true;
            this.linkLabel8.Text = "Open Document";
            this.linkLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentSet1_LinkClicked);
            // 
            // linkLabel7
            // 
            this.linkLabel7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.linkLabel7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel7.ImageIndex = 0;
            this.linkLabel7.ImageList = this.imageList1;
            this.linkLabel7.LinkColor = System.Drawing.Color.WhiteSmoke;
            this.linkLabel7.Location = new System.Drawing.Point(4, 4);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(140, 23);
            this.linkLabel7.TabIndex = 12;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "New Document";
            this.linkLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.documentSet1_LinkClicked);
            // 
            // checkBox3
            // 
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(216, 72);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(144, 24);
            this.checkBox3.TabIndex = 6;
            this.checkBox3.Text = "Enable Media Network";
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(216, 104);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(144, 24);
            this.checkBox4.TabIndex = 7;
            this.checkBox4.Text = "Show Media Browser";
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(216, 144);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Rotate Panels";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(338, 383);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 120);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Show File Details";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(16, 88);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(104, 24);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Video File";
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(16, 56);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(104, 24);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Audio File";
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(104, 24);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Application ";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(122)))), ((int)(((byte)(215)))));
            this.ClientSize = new System.Drawing.Size(792, 606);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.xpPanelGroup2);
            this.Controls.Add(this.xpPanelGroup1);
            this.Name = "Form1";
            this.Text = "XPPanel Demo Application";
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup1)).EndInit();
            this.xpPanelGroup1.ResumeLayout(false);
            this.xpPanel3.ResumeLayout(false);
            this.xpPanel2.ResumeLayout(false);
            this.xpPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup2)).EndInit();
            this.xpPanelGroup2.ResumeLayout(false);
            this.xpPanel6.ResumeLayout(false);
            this.xpPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemLayoutPanel1)).EndInit();
            this.itemLayoutPanel1.ResumeLayout(false);
            this.itemLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.xpPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemLayoutPanel2)).EndInit();
            this.itemLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1()) ;
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e) {
			xpPanel1.Enabled = checkBox1.CheckState == CheckState.Checked ;
		}

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e) {
			xpPanel2.Enabled = checkBox2.CheckState == CheckState.Checked ;
		}


		private void checkBox3_CheckedChanged(object sender, System.EventArgs e) {
			xpPanel3.Enabled = checkBox3.CheckState == CheckState.Checked ;

		}

		private void xpPanel2_SizeChanged(object sender, System.EventArgs e) {
			treeView1.Width = xpPanel2.Width - (treeView1.Left << 1) ;
		}

		private void checkBox4_CheckedChanged(object sender, System.EventArgs e) {
			xpPanel2.Visible = checkBox4.CheckState == CheckState.Checked ;
		}

		private void checkBox5_CheckedChanged(object sender, System.EventArgs e) {
			textBox1.Visible = checkBox5.Checked ;
		}

		private void checkBox6_CheckedChanged(object sender, System.EventArgs e) {
			button1.Visible = checkBox6.Checked ;
		}

		// rotate panels by moving the last panel to the 1st
		private void button2_Click(object sender, System.EventArgs e) {
			xpPanelGroup1.MovePanel(2, 0) ;
		}

		// Application file
		private void radioButton1_CheckedChanged(object sender, System.EventArgs e) {
			if (radioButton1.Checked == false) {
				textLayoutPanel1.Elements.HideAll() ;
			} else {
				try {
					textLayoutPanel1.DisableRedraw() ;
					textLayoutPanel1.Elements[0].Text = "TestApp.exe" ;
					textLayoutPanel1.Elements[0].Image = null ;
					textLayoutPanel1.Elements[1].Text = "Application" ;
					textLayoutPanel1.Elements[2].Text = "Friday, June 25, 2004, 5:16 PM" ;
					textLayoutPanel1.Elements[3].Text = "135.78 KB" ;

					for(int i = 0; i < 4; i++) {
						textLayoutPanel1.Elements[i].Visible = true ;
					}
				} finally {
					textLayoutPanel1.EnableRedraw() ;
				}
			}
		}

		// Audio file
		private void radioButton2_CheckedChanged(object sender, System.EventArgs e) {
			if (radioButton2.Checked == false) {
				textLayoutPanel1.Elements.HideAll() ;
			} else {
				try {
					textLayoutPanel1.DisableRedraw() ;
					textLayoutPanel1.Elements[0].Text = "New Orleans Is Sinking.mp3" ;
					textLayoutPanel1.Elements[0].Image = imageList2.Images[0] ;
					textLayoutPanel1.Elements[1].Text = "MPEG Layer 3 Audio" ;
					textLayoutPanel1.Elements[2].Text = "Monday, July 20, 2004, 8:38 AM" ;
					textLayoutPanel1.Elements[3].Text = "1.33 MB" ;

					for(int i = 0; i < 7; i++) {
						textLayoutPanel1.Elements[i].Visible = true ;
					}
				} finally {
					textLayoutPanel1.EnableRedraw() ;
				}
			}
		}

		// video file
		private void radioButton3_CheckedChanged(object sender, System.EventArgs e) {
			if (radioButton3.Checked == false) {
				textLayoutPanel1.Elements.HideAll() ;
			} else {
				try {
					textLayoutPanel1.DisableRedraw() ;
					textLayoutPanel1.Elements[0].Text = "Ghostbusters.wmv" ;
					textLayoutPanel1.Elements[0].Image = imageList2.Images[1] ;
					textLayoutPanel1.Elements[1].Text = "Windows Movie" ;
					textLayoutPanel1.Elements[2].Text = "Friday, October 3, 2003, 12:25 PM" ;
					textLayoutPanel1.Elements[3].Text = "234.91 MB" ;

					for(int i = 0; i < 10; i++) {
						textLayoutPanel1.Elements[i].Visible = true ;
					}
				} finally {
					textLayoutPanel1.EnableRedraw() ;
				}
			}
		}

		// Tasks - Initial set: New, Open, Find
		private void documentSet1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			xpPanel4.TogglePanelState() ;

			do {
				Application.DoEvents() ;
			} while(xpPanel4.PanelState == XPPanelState.Expanded) ;

			try {
				itemLayoutPanel2.SuspendLayout() ;
				itemLayoutPanel2.HideAll() ;

				for(int i = 3; i < 6; i++) {
					((Control) itemLayoutPanel2.Items[i]).Visible = true ;
				}
			} finally {
				itemLayoutPanel2.ResumeLayout() ;
				xpPanel4.TogglePanelState() ;
			}
		}

		// Tasks - 2nd set: Save, Search, Properties
		private void documentSet2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			xpPanel4.TogglePanelState() ;
			do {
				Application.DoEvents() ;
			} while(xpPanel4.PanelState == XPPanelState.Expanded) ;

			try {
				itemLayoutPanel2.SuspendLayout() ;
				itemLayoutPanel2.HideAll() ;

				for(int i = 0; i < 3; i++) {
					((Control) itemLayoutPanel2.Items[i]).Visible = true ;
				}
			} finally {
				itemLayoutPanel2.ResumeLayout() ;
				xpPanel4.TogglePanelState() ;
			}
		}

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
	}
}