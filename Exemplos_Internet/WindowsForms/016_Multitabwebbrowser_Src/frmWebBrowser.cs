using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SHDocVw;

//Version 2.00

//Include the following interface declaration just before your application's namespace
//declaration to add a reference to the Microsoft HTML (MSHTML) IOleCommandTarget interface
[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
public struct OLECMDTEXT
{
	public uint cmdtextf;
	public uint cwActual;
	public uint cwBuf;
	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=100)]public char rgwz;
}

[StructLayout(LayoutKind.Sequential)]
public struct OLECMD
{
	public uint cmdID;
	public uint cmdf;
}

// Interop definition for IOleCommandTarget.
[ComImport,
Guid("b722bccb-4e68-101b-a2bc-00aa00404770"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleCommandTarget
{
	//IMPORTANT: The order of the methods is critical here. You
	//perform early binding in most cases, so the order of the methods
	//here MUST match the order of their vtable layout (which is determined
	//by their layout in IDL). The interop calls key off the vtable ordering,
	//not the symbolic names. Therefore, if you switched these method declarations
	//and tried to call the Exec method on an IOleCommandTarget interface from your
	//application, it would translate into a call to the QueryStatus method instead.
	void QueryStatus(ref Guid pguidCmdGroup, UInt32 cCmds,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] OLECMD[] prgCmds, ref OLECMDTEXT CmdText);
	void Exec(ref Guid pguidCmdGroup, uint nCmdId, uint nCmdExecOpt, ref object pvaIn, ref object pvaOut);
}


namespace WindowsApplication1
{

	public class frmWebBrowser : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton tlbBack;
		private System.Windows.Forms.Panel pnlAddress;
		private System.Windows.Forms.Panel pnlGo;
		private System.Windows.Forms.ToolBarButton tlbForward;
		private System.Windows.Forms.ToolBarButton tlbStop;
		private System.Windows.Forms.ToolBarButton tlbRefresh;
		private System.Windows.Forms.ToolBarButton tlbHome;
		private System.Windows.Forms.Button btnGo;
		public System.Windows.Forms.ComboBox cmbAddress;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mniFile;
		private System.Windows.Forms.MenuItem mniEdit;
		private System.Windows.Forms.MenuItem mniView;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton tlbFind;
		private System.Windows.Forms.ToolBarButton tlbPrint;
		private System.Windows.Forms.ToolBarButton tlbSearch;
		private System.Windows.Forms.MenuItem mniViewSource;
		private System.Windows.Forms.MenuItem mniSaveAs;
		private System.Windows.Forms.MenuItem mniPrint;
		private System.Windows.Forms.MenuItem mniCopy;
		private System.Windows.Forms.MenuItem mniSelectAll;
		private System.Windows.Forms.MenuItem mniFind;
		private System.Windows.Forms.MenuItem mniGoBack;
		private System.Windows.Forms.MenuItem mniGoForward;
		private System.Windows.Forms.MenuItem mniStop;
		private System.Windows.Forms.MenuItem mniRefresh;
		private System.Windows.Forms.MenuItem mniProperties;
		private System.Windows.Forms.MenuItem mniPrintPreview;
		private System.Windows.Forms.MenuItem mniSearch;
		private System.Windows.Forms.MenuItem mniCut;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem mniPaste;
		private System.Windows.Forms.MenuItem mniOptions;
		private System.Windows.Forms.Panel pnlWebBrowser;
		private System.Windows.Forms.MenuItem mniOpen;
		private System.Windows.Forms.ToolBarButton tlbOpen;
		private System.Windows.Forms.ToolBarButton tlbSave;
		private System.Windows.Forms.ToolBarButton tlbSeparator1;
		private System.Windows.Forms.ToolBarButton tlbSeparator2;
		public System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		public System.Windows.Forms.TextBox txbURLLocation;
		private System.Windows.Forms.Panel panel3;
		public System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private AxSHDocVw.AxWebBrowser axWebBrowser1;
		public System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.ToolBarButton tblCloseTab;
		private System.Windows.Forms.ToolBarButton tlbSeparator;
		private System.Windows.Forms.MenuItem mniCloseTab;
		private System.Windows.Forms.MenuItem mniNewTab;

		#region private variables

		//Assign the interface the GUID of the appropriate COM interface.
		private Guid cmdGuid = new Guid("ED016940-BD5B-11CF-BA4E-00C04FD70816");
		private bool _FirstShown;
		private System.Windows.Forms.MenuItem mniPageSetup;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.ToolBarButton tblNewTab;
		private AxSHDocVw.AxWebBrowser _ActiveWebBrowser;

		#endregion


		public frmWebBrowser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			axWebBrowser1.Tag = new HE_WebBrowserTag();
			SetActiveWebBrowser();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWebBrowser));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tblNewTab = new System.Windows.Forms.ToolBarButton();
            this.tblCloseTab = new System.Windows.Forms.ToolBarButton();
            this.tlbSeparator = new System.Windows.Forms.ToolBarButton();
            this.tlbOpen = new System.Windows.Forms.ToolBarButton();
            this.tlbSave = new System.Windows.Forms.ToolBarButton();
            this.tlbSeparator1 = new System.Windows.Forms.ToolBarButton();
            this.tlbPrint = new System.Windows.Forms.ToolBarButton();
            this.tlbFind = new System.Windows.Forms.ToolBarButton();
            this.tlbSeparator2 = new System.Windows.Forms.ToolBarButton();
            this.tlbBack = new System.Windows.Forms.ToolBarButton();
            this.tlbForward = new System.Windows.Forms.ToolBarButton();
            this.tlbStop = new System.Windows.Forms.ToolBarButton();
            this.tlbRefresh = new System.Windows.Forms.ToolBarButton();
            this.tlbHome = new System.Windows.Forms.ToolBarButton();
            this.tlbSearch = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.pnlGo = new System.Windows.Forms.Panel();
            this.btnGo = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mniFile = new System.Windows.Forms.MenuItem();
            this.mniNewTab = new System.Windows.Forms.MenuItem();
            this.mniCloseTab = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mniOpen = new System.Windows.Forms.MenuItem();
            this.mniSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mniPageSetup = new System.Windows.Forms.MenuItem();
            this.mniPrint = new System.Windows.Forms.MenuItem();
            this.mniPrintPreview = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mniProperties = new System.Windows.Forms.MenuItem();
            this.mniOptions = new System.Windows.Forms.MenuItem();
            this.mniEdit = new System.Windows.Forms.MenuItem();
            this.mniCut = new System.Windows.Forms.MenuItem();
            this.mniCopy = new System.Windows.Forms.MenuItem();
            this.mniPaste = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mniSelectAll = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mniFind = new System.Windows.Forms.MenuItem();
            this.mniView = new System.Windows.Forms.MenuItem();
            this.mniGoBack = new System.Windows.Forms.MenuItem();
            this.mniGoForward = new System.Windows.Forms.MenuItem();
            this.mniStop = new System.Windows.Forms.MenuItem();
            this.mniRefresh = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mniSearch = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mniViewSource = new System.Windows.Forms.MenuItem();
            this.pnlWebBrowser = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbURLLocation = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.pnlAddress.SuspendLayout();
            this.pnlGo.SuspendLayout();
            this.pnlWebBrowser.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tblNewTab,
            this.tblCloseTab,
            this.tlbSeparator,
            this.tlbOpen,
            this.tlbSave,
            this.tlbSeparator1,
            this.tlbPrint,
            this.tlbFind,
            this.tlbSeparator2,
            this.tlbBack,
            this.tlbForward,
            this.tlbStop,
            this.tlbRefresh,
            this.tlbHome,
            this.tlbSearch});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(512, 28);
            this.toolBar1.TabIndex = 2;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tblNewTab
            // 
            this.tblNewTab.ImageIndex = 10;
            this.tblNewTab.Name = "tblNewTab";
            this.tblNewTab.ToolTipText = "New tab";
            // 
            // tblCloseTab
            // 
            this.tblCloseTab.Enabled = false;
            this.tblCloseTab.ImageIndex = 11;
            this.tblCloseTab.Name = "tblCloseTab";
            this.tblCloseTab.ToolTipText = "Close current tab";
            // 
            // tlbSeparator
            // 
            this.tlbSeparator.Name = "tlbSeparator";
            this.tlbSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbOpen
            // 
            this.tlbOpen.ImageIndex = 8;
            this.tlbOpen.Name = "tlbOpen";
            this.tlbOpen.ToolTipText = "Open file";
            // 
            // tlbSave
            // 
            this.tlbSave.ImageIndex = 9;
            this.tlbSave.Name = "tlbSave";
            this.tlbSave.ToolTipText = "Save to file";
            // 
            // tlbSeparator1
            // 
            this.tlbSeparator1.Name = "tlbSeparator1";
            this.tlbSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbPrint
            // 
            this.tlbPrint.Enabled = false;
            this.tlbPrint.ImageIndex = 6;
            this.tlbPrint.Name = "tlbPrint";
            this.tlbPrint.ToolTipText = "Print page";
            // 
            // tlbFind
            // 
            this.tlbFind.Enabled = false;
            this.tlbFind.ImageIndex = 5;
            this.tlbFind.Name = "tlbFind";
            this.tlbFind.ToolTipText = "Find text";
            // 
            // tlbSeparator2
            // 
            this.tlbSeparator2.Name = "tlbSeparator2";
            this.tlbSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbBack
            // 
            this.tlbBack.Enabled = false;
            this.tlbBack.ImageIndex = 0;
            this.tlbBack.Name = "tlbBack";
            this.tlbBack.ToolTipText = "Go back";
            // 
            // tlbForward
            // 
            this.tlbForward.Enabled = false;
            this.tlbForward.ImageIndex = 1;
            this.tlbForward.Name = "tlbForward";
            this.tlbForward.ToolTipText = "Go forward";
            // 
            // tlbStop
            // 
            this.tlbStop.Enabled = false;
            this.tlbStop.ImageIndex = 2;
            this.tlbStop.Name = "tlbStop";
            this.tlbStop.ToolTipText = "Stop";
            // 
            // tlbRefresh
            // 
            this.tlbRefresh.Enabled = false;
            this.tlbRefresh.ImageIndex = 3;
            this.tlbRefresh.Name = "tlbRefresh";
            this.tlbRefresh.ToolTipText = "Refresh";
            // 
            // tlbHome
            // 
            this.tlbHome.ImageIndex = 4;
            this.tlbHome.Name = "tlbHome";
            this.tlbHome.ToolTipText = "Go home";
            // 
            // tlbSearch
            // 
            this.tlbSearch.ImageIndex = 7;
            this.tlbSearch.Name = "tlbSearch";
            this.tlbSearch.ToolTipText = "Search the web";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Teal;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            // 
            // pnlAddress
            // 
            this.pnlAddress.Controls.Add(this.cmbAddress);
            this.pnlAddress.Controls.Add(this.pnlGo);
            this.pnlAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddress.Location = new System.Drawing.Point(0, 28);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(512, 22);
            this.pnlAddress.TabIndex = 4;
            // 
            // cmbAddress
            // 
            this.cmbAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAddress.Location = new System.Drawing.Point(0, 0);
            this.cmbAddress.Name = "cmbAddress";
            this.cmbAddress.Size = new System.Drawing.Size(464, 21);
            this.cmbAddress.TabIndex = 0;
            this.cmbAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAddress_KeyDown);
            // 
            // pnlGo
            // 
            this.pnlGo.Controls.Add(this.btnGo);
            this.pnlGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlGo.Location = new System.Drawing.Point(464, 0);
            this.pnlGo.Name = "pnlGo";
            this.pnlGo.Size = new System.Drawing.Size(48, 22);
            this.pnlGo.TabIndex = 7;
            // 
            // btnGo
            // 
            this.btnGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnGo.Location = new System.Drawing.Point(3, 0);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(45, 22);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "Go";
            this.btnGo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniFile,
            this.mniEdit,
            this.mniView});
            // 
            // mniFile
            // 
            this.mniFile.Index = 0;
            this.mniFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniNewTab,
            this.mniCloseTab,
            this.menuItem7,
            this.mniOpen,
            this.mniSaveAs,
            this.menuItem5,
            this.mniPageSetup,
            this.mniPrint,
            this.mniPrintPreview,
            this.menuItem6,
            this.mniProperties,
            this.mniOptions});
            this.mniFile.Text = "File";
            // 
            // mniNewTab
            // 
            this.mniNewTab.Index = 0;
            this.mniNewTab.Text = "New Tab";
            this.mniNewTab.Click += new System.EventHandler(this.mniNewTab_Click);
            // 
            // mniCloseTab
            // 
            this.mniCloseTab.Enabled = false;
            this.mniCloseTab.Index = 1;
            this.mniCloseTab.Text = "Close Current Tab";
            this.mniCloseTab.Click += new System.EventHandler(this.mniCloseTab_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.Text = "-";
            // 
            // mniOpen
            // 
            this.mniOpen.Index = 3;
            this.mniOpen.Text = "Open...";
            this.mniOpen.Click += new System.EventHandler(this.mniOpen_Click);
            // 
            // mniSaveAs
            // 
            this.mniSaveAs.Index = 4;
            this.mniSaveAs.Text = "Save As...";
            this.mniSaveAs.Click += new System.EventHandler(this.mniSaveAs_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 5;
            this.menuItem5.Text = "-";
            // 
            // mniPageSetup
            // 
            this.mniPageSetup.Index = 6;
            this.mniPageSetup.Text = "Page Setup...";
            this.mniPageSetup.Click += new System.EventHandler(this.mniPageSetup_Click);
            // 
            // mniPrint
            // 
            this.mniPrint.Index = 7;
            this.mniPrint.Text = "Print...";
            this.mniPrint.Click += new System.EventHandler(this.mniPrint_Click);
            // 
            // mniPrintPreview
            // 
            this.mniPrintPreview.Index = 8;
            this.mniPrintPreview.Text = "Print Preview...";
            this.mniPrintPreview.Click += new System.EventHandler(this.mniPrintPreview_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 9;
            this.menuItem6.Text = "-";
            // 
            // mniProperties
            // 
            this.mniProperties.Index = 10;
            this.mniProperties.Text = "Properties...";
            this.mniProperties.Click += new System.EventHandler(this.mniProperties_Click);
            // 
            // mniOptions
            // 
            this.mniOptions.Index = 11;
            this.mniOptions.Text = "Options...";
            this.mniOptions.Click += new System.EventHandler(this.mniOptions_Click);
            // 
            // mniEdit
            // 
            this.mniEdit.Index = 1;
            this.mniEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniCut,
            this.mniCopy,
            this.mniPaste,
            this.menuItem2,
            this.mniSelectAll,
            this.menuItem3,
            this.mniFind});
            this.mniEdit.Text = "Edit";
            // 
            // mniCut
            // 
            this.mniCut.Index = 0;
            this.mniCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.mniCut.Text = "Cut";
            this.mniCut.Click += new System.EventHandler(this.mniCut_Click);
            // 
            // mniCopy
            // 
            this.mniCopy.Index = 1;
            this.mniCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mniCopy.Text = "Copy";
            this.mniCopy.Click += new System.EventHandler(this.mniCopy_Click);
            // 
            // mniPaste
            // 
            this.mniPaste.Index = 2;
            this.mniPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.mniPaste.Text = "Paste";
            this.mniPaste.Click += new System.EventHandler(this.mniPaste_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "-";
            // 
            // mniSelectAll
            // 
            this.mniSelectAll.Index = 4;
            this.mniSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.mniSelectAll.Text = "Select All";
            this.mniSelectAll.Click += new System.EventHandler(this.mniSelectAll_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.Text = "-";
            // 
            // mniFind
            // 
            this.mniFind.Index = 6;
            this.mniFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.mniFind.Text = "Find...";
            this.mniFind.Click += new System.EventHandler(this.mniFind_Click);
            // 
            // mniView
            // 
            this.mniView.Index = 2;
            this.mniView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniGoBack,
            this.mniGoForward,
            this.mniStop,
            this.mniRefresh,
            this.menuItem1,
            this.mniSearch,
            this.menuItem4,
            this.mniViewSource});
            this.mniView.Text = "View";
            // 
            // mniGoBack
            // 
            this.mniGoBack.Enabled = false;
            this.mniGoBack.Index = 0;
            this.mniGoBack.Text = "Go Back";
            // 
            // mniGoForward
            // 
            this.mniGoForward.Enabled = false;
            this.mniGoForward.Index = 1;
            this.mniGoForward.Text = "Go Forward";
            // 
            // mniStop
            // 
            this.mniStop.Index = 2;
            this.mniStop.Text = "Stop";
            // 
            // mniRefresh
            // 
            this.mniRefresh.Index = 3;
            this.mniRefresh.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.mniRefresh.Text = "Refresh";
            this.mniRefresh.Click += new System.EventHandler(this.mniRefresh_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // mniSearch
            // 
            this.mniSearch.Index = 5;
            this.mniSearch.Text = "Search";
            this.mniSearch.Click += new System.EventHandler(this.mniSearch_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 6;
            this.menuItem4.Text = "-";
            // 
            // mniViewSource
            // 
            this.mniViewSource.Index = 7;
            this.mniViewSource.Text = "Source";
            this.mniViewSource.Click += new System.EventHandler(this.mniViewSource_Click);
            // 
            // pnlWebBrowser
            // 
            this.pnlWebBrowser.Controls.Add(this.panel3);
            this.pnlWebBrowser.Controls.Add(this.panel1);
            this.pnlWebBrowser.Controls.Add(this.statusBar1);
            this.pnlWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebBrowser.Location = new System.Drawing.Point(0, 50);
            this.pnlWebBrowser.Name = "pnlWebBrowser";
            this.pnlWebBrowser.Size = new System.Drawing.Size(512, 412);
            this.pnlWebBrowser.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(512, 372);
            this.panel3.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 372);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axWebBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(504, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Blank";
            // 
            // axWebBrowser1
            // 
            this.axWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWebBrowser1.Enabled = true;
            this.axWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.axWebBrowser1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser1.OcxState")));
            this.axWebBrowser1.Size = new System.Drawing.Size(504, 346);
            this.axWebBrowser1.TabIndex = 11;
            this.axWebBrowser1.CommandStateChange += new AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEventHandler(this.axWebBrowser1_CommandStateChange);
            this.axWebBrowser1.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.axWebBrowser1_BeforeNavigate2);
            this.axWebBrowser1.ProgressChange += new AxSHDocVw.DWebBrowserEvents2_ProgressChangeEventHandler(this.axWebBrowser1_ProgressChange);
            this.axWebBrowser1.StatusTextChange += new AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEventHandler(this.axWebBrowser1_StatusTextChange);
            this.axWebBrowser1.NavigateError += new AxSHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(this.axWebBrowser1_NavigateError);
            this.axWebBrowser1.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(this.axWebBrowser1_NavigateComplete2);
            this.axWebBrowser1.TitleChange += new AxSHDocVw.DWebBrowserEvents2_TitleChangeEventHandler(this.axWebBrowser1_TitleChange);
            this.axWebBrowser1.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.axWebBrowser1_DocumentComplete);
            this.axWebBrowser1.NewWindow2 += new AxSHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(this.axWebBrowser1_NewWindow2);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 372);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 18);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txbURLLocation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 18);
            this.panel2.TabIndex = 7;
            // 
            // txbURLLocation
            // 
            this.txbURLLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbURLLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbURLLocation.Location = new System.Drawing.Point(0, 0);
            this.txbURLLocation.Name = "txbURLLocation";
            this.txbURLLocation.ReadOnly = true;
            this.txbURLLocation.Size = new System.Drawing.Size(340, 13);
            this.txbURLLocation.TabIndex = 8;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.progressBar1.Location = new System.Drawing.Point(344, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(168, 18);
            this.progressBar1.TabIndex = 6;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 390);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(512, 22);
            this.statusBar1.TabIndex = 9;
            // 
            // frmWebBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 462);
            this.Controls.Add(this.pnlWebBrowser);
            this.Controls.Add(this.pnlAddress);
            this.Controls.Add(this.toolBar1);
            this.Menu = this.mainMenu1;
            this.Name = "frmWebBrowser";
            this.ShowInTaskbar = false;
            this.Text = "Web Browser";
            this.Closed += new System.EventHandler(this.frmWebBrowser_Closed);
            this.Activated += new System.EventHandler(this.frmWebBrowser_Activated);
            this.pnlAddress.ResumeLayout(false);
            this.pnlGo.ResumeLayout(false);
            this.pnlWebBrowser.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void WebBrowserGo()
		{

			txbURLLocation.Text = "";
			// The user has clicked on the GO button next to the Address combo box,
			//so open the URL address in this page
			string url = cmbAddress.Text;

			// Return if nowhere to go
			if (url == "") return;   
			try 
			{
				Cursor.Current = Cursors.WaitCursor;
				Object o = null;
				// Get the URL from the control, and send the WebBrowser to fetch and display it	
				_ActiveWebBrowser.Navigate(url, ref o, ref o, ref o, ref o);
			} 
			finally 
			{
				int i = cmbAddress.Items.IndexOf(url);
				if (i == -1)
					cmbAddress.Items.Add(url);

				Cursor.Current = Cursors.Default;
			}			
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			// Evaluate the Button property to determine which button was clicked.
			try 
			{
				//Go Home
				if ( e.Button == tlbHome )
				{
					_ActiveWebBrowser.GoHome();
				}
				//Refresh
				else if ( e.Button == tlbRefresh )
				{
					mniRefresh_Click(null, null);	
				}
				//Go Back
				else if ( e.Button == tlbBack )
				{
					_ActiveWebBrowser.GoBack();
				}
				//Go Forward
				else if ( e.Button == tlbForward )
				{
					_ActiveWebBrowser.GoForward();
				}
				//Stop
				else if ( e.Button == tlbStop )
				{
					_ActiveWebBrowser.Stop();
					statusBar1.Text = "Stopped";
					progressBar1.Value = 0;
				}
				//Find
				else if ( e.Button == tlbFind )
				{
					WebBrowserFind();
				}
				//Search
				else if ( e.Button == tlbSearch )
				{
					_ActiveWebBrowser.GoSearch();
				}
				//Print
				else if ( e.Button == tlbPrint )
				{
					PrintPage();
				}
				//Open File
				else if ( e.Button == tlbOpen)
				{
					OpenFile();
				}
				//Save file
				else if ( e.Button == tlbSave)
				{
					mniSaveAs_Click(null, null);
				}
				//New tab
				else if ( e.Button == tblNewTab)
				{
					mniNewTab_Click(null, null);
				}
				//Close tab
				else if ( e.Button == tblCloseTab)
				{
					mniCloseTab_Click(null, null);
				}

			}
			finally 
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnGo_Click(object sender, System.EventArgs e)
		{
			WebBrowserGo();
		}
	
		private void axWebBrowser1_ProgressChange(object sender, AxSHDocVw.DWebBrowserEvents2_ProgressChangeEvent e)
		{
			AxSHDocVw.AxWebBrowser axWebBrowser1 = (AxSHDocVw.AxWebBrowser)sender;
			HE_WebBrowserTag _HE_WebBrowserTag = (HE_WebBrowserTag)axWebBrowser1.Tag;

			//If current tab page does not match current webbrowser then leave it.
			if (_HE_WebBrowserTag._TabIndex != tabControl1.SelectedIndex) {return;}

			progressBar1.Visible = true;
			if ((e.progress > 0) && (e.progressMax > 0) )
			{
				progressBar1.Maximum = e.progressMax;
				progressBar1.Step = e.progress;
				progressBar1.PerformStep();
			}
			else if (axWebBrowser1.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
			{
				progressBar1.Value = 0;
				progressBar1.Visible = false;
			}

		}

		private void axWebBrowser1_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{
			HE_WebBrowserTag _HE_WebBrowserTag = (HE_WebBrowserTag)_ActiveWebBrowser.Tag;
			if (_HE_WebBrowserTag._TabIndex != tabControl1.SelectedIndex) {return;}

			tlbStop.Enabled = true;
			txbURLLocation.Text = "";
			statusBar1.Text = "Loading...";
		}

		private void axWebBrowser1_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
	
			if (_ActiveWebBrowser.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE) 
			{
				//if the selected tab matches the current webbrowser then update status text with the
				//current URL location.
				HE_WebBrowserTag _HE_WebBrowserTag = (HE_WebBrowserTag)_ActiveWebBrowser.Tag;
				if (_HE_WebBrowserTag._TabIndex == tabControl1.SelectedIndex)
						{txbURLLocation.Text = _ActiveWebBrowser.LocationURL;}

	
				progressBar1.Value = 0;
				progressBar1.Visible = false;
				
				tlbPrint.Enabled = IsPrinterEnabled();
				
			}	
		}

		private void axWebBrowser1_StatusTextChange(object sender, AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEvent e)
		{
			AxSHDocVw.AxWebBrowser _AxWebBrowser = (AxSHDocVw.AxWebBrowser)sender;
			HE_WebBrowserTag _HE_WebBrowserTag = (HE_WebBrowserTag)_AxWebBrowser.Tag;
			if (_HE_WebBrowserTag._TabIndex != tabControl1.SelectedIndex)
			{
				statusBar1.Text = "";
				return;
			}
			else
				statusBar1.Text = e.text;
			
		}

		private void cmbAddress_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				WebBrowserGo();
			}
		}

		private enum MiscCommandTarget
		{Find = 1, ViewSource, Options}
		
		private mshtml.HTMLDocument GetDocument()
		{
			try
			{
				mshtml.HTMLDocument htm = (mshtml.HTMLDocument)_ActiveWebBrowser.Document;
				return htm;
			}
			catch
			{
				throw (new Exception("Cannot retrieve the document from the WebBrowser control"));
			}
		}

		//Activate Find dialog box
		public void WebBrowserFind()
		{
			IOleCommandTarget cmdt;
			Object o = new object();
			try
			{
				cmdt = (IOleCommandTarget)GetDocument();
				cmdt.Exec(ref cmdGuid, (uint)MiscCommandTarget.Find,
					(uint)SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref o, ref o);
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}

		//Activate HTML source
		public void WebBrowserViewSource()
		{
			IOleCommandTarget cmdt;
			Object o = new object();
			try
			{
				cmdt = (IOleCommandTarget)GetDocument();
				cmdt.Exec(ref cmdGuid, (uint)MiscCommandTarget.ViewSource,
					(uint)SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref o, ref o);
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}

		//Activate Internet options dialog
		public void WebBrowserInternetOptions()
		{
			IOleCommandTarget cmdt;
			Object o = new object();
			try
			{
				cmdt = (IOleCommandTarget)GetDocument();
				cmdt.Exec(ref cmdGuid, (uint)MiscCommandTarget.Options,
					(uint)SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref o, ref o);
			}
			catch
			{
				// NOTE: Because of the way that this CMDID is handled in Internet Explorer,
				// this catch block will always fire, even though the dialog box
				// and its operations completed successfully. You can suppress this
				// error without causing any damage to your host.
			}
		}


		private void mniViewSource_Click(object sender, System.EventArgs e)
		{
			WebBrowserViewSource();
		}

		private void ExecCommandID(SHDocVw.OLECMDID _OLECMDID)
		{
			int response = (int) _ActiveWebBrowser.QueryStatusWB(_OLECMDID);
			bool IsOK = (response & (int) SHDocVw.OLECMDF.OLECMDF_ENABLED) != 0 ? true : false;
			if (IsOK == false) {return;}

			Object o = null;
			_ActiveWebBrowser.ExecWB(_OLECMDID, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref o, ref o);
		}

		private void mniSaveAs_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_SAVEAS);
		}

		private void mniCopy_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_COPY);
		}

		private void mniSelectAll_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_SELECTALL);
		}

		private void mniFind_Click(object sender, System.EventArgs e)
		{
			WebBrowserFind();
		}

		private void mniProperties_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_PROPERTIES);
		}

		private bool IsPrinterEnabled()
		{
			int response = (int) _ActiveWebBrowser.QueryStatusWB(SHDocVw.OLECMDID.OLECMDID_PRINT);
			return (response & (int) SHDocVw.OLECMDF.OLECMDF_ENABLED) != 0 ? true : false;
		}

		private void PrintPage()
		{
			object o = "";
			// constants useful when printing
			SHDocVw.OLECMDID Print = SHDocVw.OLECMDID.OLECMDID_PRINT;
			// use this value to print without prompting
			// SHDocVw.OLECMDEXECOPT PromptUser = SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER;
			SHDocVw.OLECMDEXECOPT DontPromptUser = SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER;
  
			if (!IsPrinterEnabled() ) return;
			// print without prompting user
			_ActiveWebBrowser.ExecWB(Print, DontPromptUser, ref o, ref o);
			// to prompt the user with printer settings
			// axWebBrowser1.ExecWB(Print, PromptUser, ref o, ref o);
		}

		private void mniPrint_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_PRINT);
		}

		private void mniPrintPreview_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_PRINTPREVIEW);
		}

		private void mniSearch_Click(object sender, System.EventArgs e)
		{
			_ActiveWebBrowser.GoSearch();
		}

		private void mniCut_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_CUT);
		}

		private void mniPaste_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_PASTE);
		}

		private void frmWebBrowser_Activated(object sender, System.EventArgs e)
		{
			if (_FirstShown == false)
			{
				cmbAddress.Focus();
				cmbAddress.Text = "about:blank";
				WebBrowserGo();
				_FirstShown = true;
			}

		}

		private void axWebBrowser1_NavigateError(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent e)
		{
			HE_WebBrowserTag _HE_WebBrowserTag = (HE_WebBrowserTag)_ActiveWebBrowser.Tag;
			if (_HE_WebBrowserTag._TabIndex != tabControl1.SelectedIndex) {return;}

			Cursor.Current = Cursors.Default;
			tlbStop.Enabled = false;
			tlbHome.Enabled = true;
			tlbSearch.Enabled = true;
			tlbRefresh.Enabled = true;
		}

		private void axWebBrowser1_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
		{
			HE_WebBrowserTag _HE_WebBrowserTag = (HE_WebBrowserTag)_ActiveWebBrowser.Tag;
			if (_HE_WebBrowserTag._TabIndex != tabControl1.SelectedIndex) {return;}

			Cursor.Current = Cursors.Default;
			tlbStop.Enabled = false;
			tlbHome.Enabled = true;
			tlbSearch.Enabled = true;
			tlbRefresh.Enabled = true;
			tlbFind.Enabled = true;
			progressBar1.Value = 0;
			statusBar1.Text = "";
		}

		private void mniOptions_Click(object sender, System.EventArgs e)
		{
			WebBrowserInternetOptions();
		}

		private void OpenFile()
		{
			OpenFileDialog _openFileDialog = new OpenFileDialog();
			_openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
			_openFileDialog.FilterIndex = 2 ;
			_openFileDialog.RestoreDirectory = true ;

			if(_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				cmbAddress.Text = _openFileDialog.FileName;
				WebBrowserGo();
			}
		}

		private void mniOpen_Click(object sender, System.EventArgs e)
		{
			OpenFile();
		}


		private  AxSHDocVw.AxWebBrowser CreateNewWebBrowser()
		{
			AxSHDocVw.AxWebBrowser _axWebBrowser = new AxSHDocVw.AxWebBrowser();
			_axWebBrowser.Tag = new HE_WebBrowserTag();
			TabPage _TabPage = new TabPage();
			_TabPage.Controls.Add(_axWebBrowser);

			_axWebBrowser.Dock = DockStyle.Fill;
			_axWebBrowser.BeforeNavigate2		+= new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.axWebBrowser1_BeforeNavigate2);
			_axWebBrowser.DocumentComplete	+= new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.axWebBrowser1_DocumentComplete); 
			_axWebBrowser.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(this.axWebBrowser1_NavigateComplete2);
			_axWebBrowser.NavigateError			+= new AxSHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(this.axWebBrowser1_NavigateError);
			_axWebBrowser.NewWindow2				+= new AxSHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(this.axWebBrowser1_NewWindow2);
			_axWebBrowser.ProgressChange		+= new AxSHDocVw.DWebBrowserEvents2_ProgressChangeEventHandler(this.axWebBrowser1_ProgressChange);
			_axWebBrowser.StatusTextChange	+= new AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEventHandler(this.axWebBrowser1_StatusTextChange);
			_axWebBrowser.TitleChange				+= new AxSHDocVw.DWebBrowserEvents2_TitleChangeEventHandler(this.axWebBrowser1_TitleChange);
			_axWebBrowser.CommandStateChange += new AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEventHandler(this.axWebBrowser1_CommandStateChange);
				
			tabControl1.TabPages.Add(_TabPage);
			tabControl1.SelectedTab = _TabPage;
			mniCloseTab.Enabled = true;
			tblCloseTab.Enabled = true;
			return _axWebBrowser;
		}

		//On this event we dinamically create a new tab and webbrowser.
		private void axWebBrowser1_NewWindow2(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e)
		{
			AxSHDocVw.AxWebBrowser _axWebBrowser = CreateNewWebBrowser();
			e.ppDisp = _axWebBrowser.Application;
			_axWebBrowser.RegisterAsBrowser = true;
		}

		//We change the active web browser when a tab page is changed. 
		//Then we change form controls properties according to what is indicated in the
		//active web browser and in the active web browser tag object.
		private void SetActiveWebBrowser()
		{
			TabPage _TabPage;
			_TabPage = tabControl1.SelectedTab;
			for (int i = 0; i < _TabPage.Controls.Count; i++)
			{
				if (_TabPage.Controls[i].GetType().Name == "AxWebBrowser")
				{
					_ActiveWebBrowser = (AxSHDocVw.AxWebBrowser)_TabPage.Controls[i];
					HE_WebBrowserTag _HE_WebBrowserTag;

					_HE_WebBrowserTag						= (HE_WebBrowserTag)_ActiveWebBrowser.Tag;
					_HE_WebBrowserTag._TabIndex = tabControl1.SelectedIndex;
					txbURLLocation.Text					= _ActiveWebBrowser.LocationURL;
					tlbBack.Enabled							= _HE_WebBrowserTag._CanBack;
					tlbForward.Enabled					= _HE_WebBrowserTag._CanForward;
					break;
				}
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetActiveWebBrowser();
		}

		private void mniCloseTab_Click(object sender, System.EventArgs e)
		{
			tabControl1.SelectedTab.Dispose();
			mniCloseTab.Enabled = (tabControl1.TabCount > 1);
			tblCloseTab.Enabled = mniCloseTab.Enabled;
		}

		private void axWebBrowser1_CommandStateChange(object sender, AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent e)
		{
		
			AxSHDocVw.AxWebBrowser _AxWebBrowser = (AxSHDocVw.AxWebBrowser)sender;
			HE_WebBrowserTag _HE_WebBrowserTag;
			_HE_WebBrowserTag = (HE_WebBrowserTag)_AxWebBrowser.Tag;

			//Set enabled property for the Forward/Backward functions.
			switch(e.command)
			{
				case ((int)CommandStateChangeConstants.CSC_NAVIGATEFORWARD):
					_HE_WebBrowserTag._CanForward = e.enable;
					break;

				case ((int)CommandStateChangeConstants.CSC_NAVIGATEBACK):
					_HE_WebBrowserTag._CanBack = e.enable;
					break;

				default:
					break;
			}
			_AxWebBrowser.Tag = _HE_WebBrowserTag;

			//If the active tab page matches the current webbrowser then
			//set controls according to its tag properties.
			if (_HE_WebBrowserTag._TabIndex == tabControl1.SelectedIndex)
			{
				tlbForward.Enabled = _HE_WebBrowserTag._CanForward;
				mniGoForward.Enabled = _HE_WebBrowserTag._CanForward;
				tlbBack.Enabled = _HE_WebBrowserTag._CanBack;
				mniGoBack.Enabled = _HE_WebBrowserTag._CanBack;
			}
		}

		private void axWebBrowser1_TitleChange(object sender, AxSHDocVw.DWebBrowserEvents2_TitleChangeEvent e)
		{
			//Set corresponding tab text with the corresponding location name
			AxSHDocVw.AxWebBrowser _AxWebBrowser = (AxSHDocVw.AxWebBrowser)sender;
			HE_WebBrowserTag _HE_WebBrowserTag;
			_HE_WebBrowserTag = (HE_WebBrowserTag)_AxWebBrowser.Tag;
			tabControl1.TabPages[_HE_WebBrowserTag._TabIndex].Text = e.text;
		}

		private void mniNewTab_Click(object sender, System.EventArgs e)
		{
			mniCloseTab.Enabled = true;
			tblCloseTab.Enabled = true;
			CreateNewWebBrowser();
			cmbAddress.Focus();
			cmbAddress.Text = "about:blank";
			WebBrowserGo();
		}

	
		private void frmWebBrowser_Closed(object sender, System.EventArgs e)
		{
			_ActiveWebBrowser.Stop();
		}

		private void mniPageSetup_Click(object sender, System.EventArgs e)
		{
			ExecCommandID(SHDocVw.OLECMDID.OLECMDID_PAGESETUP);		
		}

		private void mniRefresh_Click(object sender, System.EventArgs e)
		{
			object REFRESH_COMPLETELY = 3;
			_ActiveWebBrowser.Refresh2(ref REFRESH_COMPLETELY); 
		}

	}

//Each instance of a web browser will have in its tag an HE_WebBrowserTag object
//that holds some details on that web browser object
	public class HE_WebBrowserTag
	{
		public int _TabIndex = 0;
		public bool _CanBack = false;
		public bool _CanForward = false;
	}
}
