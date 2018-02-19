using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SnippyLib;
using System.Xml;
using System.IO;

namespace Snippy
{

public class MainForm : Form
{
	// Fields
	private App _app;
	private bool _dirtySnippet;
	private bool _dirtySnippetFile;
	private Button btnLiteralRemove;
	private Button btnLiteralsAdd;
	private Button btnLiteralsEdit;
	private ComboBox cbxActiveSnippet;
	private CheckedListBox clbSnippetTypes;
	private DataGridViewTextBoxColumn Column1;
	private ComboBox comboBoxKind;
	private IContainer components;
	private DataGridView dataGridView1;
	private GroupBox groupBox1;
	private GroupBox groupBox2;
	private GroupBox groupBox3;
	private GroupBox groupBox4;
	private GroupBox groupBox5;
	private GroupBox groupBox6;
	private Label label1;
	private Label label2;
	private Label label3;
	private Label label4;
	private Label label5;
	private Label label6;
	private Label label7;
	private ComboBox languageComboBox;
	private ColumnHeader lviLiteralsDefaultValue;
	private ColumnHeader lviLiteralsEditable;
	private ColumnHeader lviLiteralsFunction;
	private ColumnHeader lviLiteralsID;
	private ColumnHeader lviLiteralsToolTip;
	private ListView lvwLiterals;
	private MainMenu mainMenu1;
	private MenuItem menuItem1;
	private MenuItem menuItem2;
	private MenuItem menuItem3;
	private MenuItem menuItem4;
	private MenuItem menuItem5;
	private MenuItem mnuAbout;
	private MenuItem mnuExit;
	private MenuItem mnuNew;
	private MenuItem mnuNewSnippet;
	private MenuItem mnuOpen;
	private MenuItem mnuSave;
	private MenuItem mnuSaveAs;
	private RichTextBox rtbCode;
	private StatusBarPanel spBuffer;
	private StatusBarPanel spFile;
	private StatusBar statusBar1;
	private TextBox txtAuthor;
	private TextBox txtDescription;
	private TextBox txtHackForTheming;
	private TextBox txtShortcut;
	private TextBox txtTitle;

	// Methods
	public MainForm()
	{
		this.InitializeComponent();
		this._app = new App();
		this.languageComboBox.SelectedIndex = 0;
		this.setFileDirty(false);
	}

	private void btnLiteralRemove_Click(object sender, EventArgs e)
	{
		this.removeLiteral();
	}

	private void btnLiteralsAdd_Click(object sender, EventArgs e)
	{
		LiteralForm form = new LiteralForm();
		if (form.ShowDialog() == DialogResult.OK)
		{
			this.lvwLiterals.Items.Add(new ListViewItem(new string[] { form.ID, form.Tooltip, form.DefaultValue, form.Function, form.Editable.ToString() })).Tag = form.IsObject;
			this.OnDataChanged(sender, e);
		}
	}

	private void btnLiteralsEdit_Click(object sender, EventArgs e)
	{
		this.editLiteral();
	}

	private void cbxActiveSnippet_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.updateInMemorySnippet();
		this._app.SetCurrentSnippet(this.cbxActiveSnippet.SelectedIndex);
		this.refreshForm(false);
	}

	private void clbSnippetTypes_ItemCheck(object sender, ItemCheckEventArgs e)
	{
		this.OnDataChanged(sender, e);
	}

	private bool confirmLoseChanges()
	{
		if (this._dirtySnippetFile)
		{
			switch (MessageBox.Show("The contents of the " + this._app.CurrentFile + " file have changed.\n\nDo you want to save the changes?", "Snippy", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
			{
				case DialogResult.Cancel:
					return false;

				case DialogResult.Yes:
					this.save();
					break;
			}
		}
		return true;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && (this.components != null))
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void editLiteral()
	{
		if (this.lvwLiterals.SelectedIndices.Count == 0)
		{
			MessageBox.Show("Need to select a literal to modify");
		}
		else
		{
			LiteralForm form = new LiteralForm();
			form.ID = this.lvwLiterals.SelectedItems[0].SubItems[0].Text;
			form.Tooltip = this.lvwLiterals.SelectedItems[0].SubItems[1].Text;
			form.DefaultValue = this.lvwLiterals.SelectedItems[0].SubItems[2].Text;
			form.Function = this.lvwLiterals.SelectedItems[0].SubItems[3].Text;
			form.Editable = bool.Parse(this.lvwLiterals.SelectedItems[0].SubItems[4].Text);
			form.IsObject = (bool) this.lvwLiterals.SelectedItems[0].Tag;
			if (form.ShowDialog() == DialogResult.OK)
			{
				this.lvwLiterals.SelectedItems[0].SubItems[0].Text = form.ID;
				this.lvwLiterals.SelectedItems[0].SubItems[1].Text = form.Tooltip;
				this.lvwLiterals.SelectedItems[0].SubItems[2].Text = form.DefaultValue;
				this.lvwLiterals.SelectedItems[0].SubItems[3].Text = form.Function;
				this.lvwLiterals.SelectedItems[0].SubItems[4].Text = form.Editable.ToString();
				this.lvwLiterals.SelectedItems[0].Tag = form.IsObject;
				this.OnDataChanged(this.lvwLiterals, null);
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtAuthor = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.TextBox();
		this.txtShortcut = new System.Windows.Forms.TextBox();
		this.txtTitle = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.comboBoxKind = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.languageComboBox = new System.Windows.Forms.ComboBox();
		this.rtbCode = new System.Windows.Forms.RichTextBox();
		this.txtHackForTheming = new System.Windows.Forms.TextBox();
		this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
		this.menuItem1 = new System.Windows.Forms.MenuItem();
		this.mnuNew = new System.Windows.Forms.MenuItem();
		this.mnuOpen = new System.Windows.Forms.MenuItem();
		this.mnuSave = new System.Windows.Forms.MenuItem();
		this.mnuSaveAs = new System.Windows.Forms.MenuItem();
		this.menuItem4 = new System.Windows.Forms.MenuItem();
		this.mnuExit = new System.Windows.Forms.MenuItem();
		this.menuItem5 = new System.Windows.Forms.MenuItem();
		this.mnuNewSnippet = new System.Windows.Forms.MenuItem();
		this.menuItem3 = new System.Windows.Forms.MenuItem();
		this.menuItem2 = new System.Windows.Forms.MenuItem();
		this.mnuAbout = new System.Windows.Forms.MenuItem();
		this.statusBar1 = new System.Windows.Forms.StatusBar();
		this.spFile = new System.Windows.Forms.StatusBarPanel();
		this.spBuffer = new System.Windows.Forms.StatusBarPanel();
		this.cbxActiveSnippet = new System.Windows.Forms.ComboBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnLiteralRemove = new System.Windows.Forms.Button();
		this.btnLiteralsEdit = new System.Windows.Forms.Button();
		this.btnLiteralsAdd = new System.Windows.Forms.Button();
		this.lvwLiterals = new System.Windows.Forms.ListView();
		this.lviLiteralsID = new System.Windows.Forms.ColumnHeader();
		this.lviLiteralsToolTip = new System.Windows.Forms.ColumnHeader();
		this.lviLiteralsDefaultValue = new System.Windows.Forms.ColumnHeader();
		this.lviLiteralsFunction = new System.Windows.Forms.ColumnHeader();
		this.lviLiteralsEditable = new System.Windows.Forms.ColumnHeader();
		this.clbSnippetTypes = new System.Windows.Forms.CheckedListBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.spFile)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.spBuffer)).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
		this.SuspendLayout();
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.txtAuthor);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtDescription);
		this.groupBox1.Controls.Add(this.txtShortcut);
		this.groupBox1.Controls.Add(this.txtTitle);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.groupBox1.Location = new System.Drawing.Point(4, 45);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(616, 119);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		// 
		// txtAuthor
		// 
		this.txtAuthor.Location = new System.Drawing.Point(96, 37);
		this.txtAuthor.Name = "txtAuthor";
		this.txtAuthor.Size = new System.Drawing.Size(304, 22);
		this.txtAuthor.TabIndex = 7;
		this.txtAuthor.TextChanged += new System.EventHandler(this.OnDataChanged);
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(8, 40);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 14);
		this.label7.TabIndex = 6;
		this.label7.Text = "&Author";
		// 
		// label4
		// 
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label4.Location = new System.Drawing.Point(8, 66);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(84, 19);
		this.label4.TabIndex = 8;
		this.label4.Text = "&Description:";
		// 
		// txtDescription
		// 
		this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtDescription.Location = new System.Drawing.Point(96, 63);
		this.txtDescription.Multiline = true;
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(508, 49);
		this.txtDescription.TabIndex = 9;
		this.txtDescription.TextChanged += new System.EventHandler(this.OnDataChanged);
		// 
		// txtShortcut
		// 
		this.txtShortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtShortcut.Location = new System.Drawing.Point(472, 11);
		this.txtShortcut.Name = "txtShortcut";
		this.txtShortcut.Size = new System.Drawing.Size(132, 21);
		this.txtShortcut.TabIndex = 5;
		this.txtShortcut.TextChanged += new System.EventHandler(this.OnDataChanged);
		// 
		// txtTitle
		// 
		this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtTitle.Location = new System.Drawing.Point(96, 11);
		this.txtTitle.Name = "txtTitle";
		this.txtTitle.Size = new System.Drawing.Size(304, 21);
		this.txtTitle.TabIndex = 3;
		this.txtTitle.TextChanged += new System.EventHandler(this.OnDataChanged);
		// 
		// label3
		// 
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label3.Location = new System.Drawing.Point(412, 15);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(60, 19);
		this.label3.TabIndex = 4;
		this.label3.Text = "S&hortcut:";
		// 
		// label1
		// 
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.Location = new System.Drawing.Point(8, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 19);
		this.label1.TabIndex = 2;
		this.label1.Text = "&Title:";
		// 
		// groupBox2
		// 
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.comboBoxKind);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.languageComboBox);
		this.groupBox2.Controls.Add(this.rtbCode);
		this.groupBox2.Controls.Add(this.txtHackForTheming);
		this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.groupBox2.Location = new System.Drawing.Point(4, 440);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(820, 268);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "&Code";
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(308, 21);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(30, 14);
		this.label6.TabIndex = 1004;
		this.label6.Text = "&Kind";
		// 
		// comboBoxKind
		// 
		this.comboBoxKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBoxKind.FormattingEnabled = true;
		this.comboBoxKind.Items.AddRange(new object[] {
            "method body",
            "method decl",
            "type decl"});
		this.comboBoxKind.Location = new System.Drawing.Point(349, 17);
		this.comboBoxKind.Name = "comboBoxKind";
		this.comboBoxKind.Size = new System.Drawing.Size(137, 22);
		this.comboBoxKind.Sorted = true;
		this.comboBoxKind.TabIndex = 1003;
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(8, 21);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(64, 14);
		this.label5.TabIndex = 1002;
		this.label5.Text = "&Language:";
		// 
		// languageComboBox
		// 
		this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.languageComboBox.FormattingEnabled = true;
		this.languageComboBox.Items.AddRange(new object[] {
            "cpp",
            "csharp",
            "jsharp",
            "vb",
            "xml"});
		this.languageComboBox.Location = new System.Drawing.Point(96, 17);
		this.languageComboBox.Name = "languageComboBox";
		this.languageComboBox.Size = new System.Drawing.Size(137, 22);
		this.languageComboBox.Sorted = true;
		this.languageComboBox.TabIndex = 1001;
		this.languageComboBox.SelectedIndexChanged += new System.EventHandler(this.languageComboBox_SelectedIndexChanged);
		// 
		// rtbCode
		// 
		this.rtbCode.AcceptsTab = true;
		this.rtbCode.AllowDrop = true;
		this.rtbCode.AutoWordSelection = true;
		this.rtbCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.rtbCode.DetectUrls = false;
		this.rtbCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.rtbCode.Location = new System.Drawing.Point(9, 43);
		this.rtbCode.Name = "rtbCode";
		this.rtbCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
		this.rtbCode.ShowSelectionMargin = true;
		this.rtbCode.Size = new System.Drawing.Size(798, 220);
		this.rtbCode.TabIndex = 13;
		this.rtbCode.Text = "";
		this.rtbCode.WordWrap = false;
		this.rtbCode.TextChanged += new System.EventHandler(this.OnDataChanged);
		// 
		// txtHackForTheming
		// 
		this.txtHackForTheming.Enabled = false;
		this.txtHackForTheming.Location = new System.Drawing.Point(8, 42);
		this.txtHackForTheming.Multiline = true;
		this.txtHackForTheming.Name = "txtHackForTheming";
		this.txtHackForTheming.Size = new System.Drawing.Size(800, 222);
		this.txtHackForTheming.TabIndex = 1000;
		this.txtHackForTheming.TabStop = false;
		// 
		// mainMenu1
		// 
		this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem3});
		// 
		// menuItem1
		// 
		this.menuItem1.Index = 0;
		this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuSave,
            this.mnuSaveAs,
            this.menuItem4,
            this.mnuExit});
		this.menuItem1.Text = "&File";
		// 
		// mnuNew
		// 
		this.mnuNew.Index = 0;
		this.mnuNew.Text = "&New";
		this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
		// 
		// mnuOpen
		// 
		this.mnuOpen.Index = 1;
		this.mnuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
		this.mnuOpen.Text = "&Open...";
		this.mnuOpen.Click += new System.EventHandler(this.mnuLoad_Click);
		// 
		// mnuSave
		// 
		this.mnuSave.Index = 2;
		this.mnuSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
		this.mnuSave.Text = "&Save";
		this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
		// 
		// mnuSaveAs
		// 
		this.mnuSaveAs.Index = 3;
		this.mnuSaveAs.Text = "Save &As...";
		this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
		// 
		// menuItem4
		// 
		this.menuItem4.Index = 4;
		this.menuItem4.Text = "-";
		// 
		// mnuExit
		// 
		this.mnuExit.Index = 5;
		this.mnuExit.Text = "E&xit";
		this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
		// 
		// menuItem5
		// 
		this.menuItem5.Index = 1;
		this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNewSnippet});
		this.menuItem5.Text = "&Snippet";
		// 
		// mnuNewSnippet
		// 
		this.mnuNewSnippet.Index = 0;
		this.mnuNewSnippet.Text = "&New ";
		this.mnuNewSnippet.Click += new System.EventHandler(this.mnuNewSnippet_Click);
		// 
		// menuItem3
		// 
		this.menuItem3.Index = 2;
		this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.mnuAbout});
		this.menuItem3.Text = "&Help";
		// 
		// menuItem2
		// 
		this.menuItem2.Index = 0;
		this.menuItem2.Text = "Help on Snippy...";
		this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
		// 
		// mnuAbout
		// 
		this.mnuAbout.Index = 1;
		this.mnuAbout.Text = "&About Snippy...";
		this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
		// 
		// statusBar1
		// 
		this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.statusBar1.Location = new System.Drawing.Point(0, 766);
		this.statusBar1.Name = "statusBar1";
		this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.spFile,
            this.spBuffer});
		this.statusBar1.ShowPanels = true;
		this.statusBar1.Size = new System.Drawing.Size(830, 21);
		this.statusBar1.SizingGrip = false;
		this.statusBar1.TabIndex = 2;
		this.statusBar1.Text = "stbStatus";
		// 
		// spFile
		// 
		this.spFile.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
		this.spFile.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
		this.spFile.Name = "spFile";
		this.spFile.Text = "    Untitled.snippet    ";
		this.spFile.ToolTipText = "The current snippet file you\'re editing/viewing";
		this.spFile.Width = 115;
		// 
		// spBuffer
		// 
		this.spBuffer.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
		this.spBuffer.Name = "spBuffer";
		this.spBuffer.Width = 715;
		// 
		// cbxActiveSnippet
		// 
		this.cbxActiveSnippet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbxActiveSnippet.Location = new System.Drawing.Point(96, 11);
		this.cbxActiveSnippet.Name = "cbxActiveSnippet";
		this.cbxActiveSnippet.Size = new System.Drawing.Size(712, 22);
		this.cbxActiveSnippet.TabIndex = 1;
		this.cbxActiveSnippet.SelectedIndexChanged += new System.EventHandler(this.cbxActiveSnippet_SelectedIndexChanged);
		// 
		// groupBox3
		// 
		this.groupBox3.Controls.Add(this.cbxActiveSnippet);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.groupBox3.Location = new System.Drawing.Point(4, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(820, 41);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		// 
		// label2
		// 
		this.label2.Location = new System.Drawing.Point(8, 15);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(56, 15);
		this.label2.TabIndex = 0;
		this.label2.Text = "Sni&ppet:";
		// 
		// groupBox4
		// 
		this.groupBox4.Controls.Add(this.btnLiteralRemove);
		this.groupBox4.Controls.Add(this.btnLiteralsEdit);
		this.groupBox4.Controls.Add(this.btnLiteralsAdd);
		this.groupBox4.Controls.Add(this.lvwLiterals);
		this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.groupBox4.Location = new System.Drawing.Point(4, 275);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(820, 157);
		this.groupBox4.TabIndex = 6;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Literals && Objects";
		// 
		// btnLiteralRemove
		// 
		this.btnLiteralRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnLiteralRemove.Location = new System.Drawing.Point(8, 71);
		this.btnLiteralRemove.Name = "btnLiteralRemove";
		this.btnLiteralRemove.Size = new System.Drawing.Size(80, 21);
		this.btnLiteralRemove.TabIndex = 11;
		this.btnLiteralRemove.Text = "&Remove";
		this.btnLiteralRemove.Click += new System.EventHandler(this.btnLiteralRemove_Click);
		// 
		// btnLiteralsEdit
		// 
		this.btnLiteralsEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnLiteralsEdit.Location = new System.Drawing.Point(8, 45);
		this.btnLiteralsEdit.Name = "btnLiteralsEdit";
		this.btnLiteralsEdit.Size = new System.Drawing.Size(80, 21);
		this.btnLiteralsEdit.TabIndex = 10;
		this.btnLiteralsEdit.Text = "&Edit...";
		this.btnLiteralsEdit.Click += new System.EventHandler(this.btnLiteralsEdit_Click);
		// 
		// btnLiteralsAdd
		// 
		this.btnLiteralsAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnLiteralsAdd.Location = new System.Drawing.Point(8, 19);
		this.btnLiteralsAdd.Name = "btnLiteralsAdd";
		this.btnLiteralsAdd.Size = new System.Drawing.Size(80, 21);
		this.btnLiteralsAdd.TabIndex = 9;
		this.btnLiteralsAdd.Text = "&Add...";
		this.btnLiteralsAdd.Click += new System.EventHandler(this.btnLiteralsAdd_Click);
		// 
		// lvwLiterals
		// 
		this.lvwLiterals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lviLiteralsID,
            this.lviLiteralsToolTip,
            this.lviLiteralsDefaultValue,
            this.lviLiteralsFunction,
            this.lviLiteralsEditable});
		this.lvwLiterals.FullRowSelect = true;
		this.lvwLiterals.GridLines = true;
		this.lvwLiterals.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.lvwLiterals.HideSelection = false;
		this.lvwLiterals.Location = new System.Drawing.Point(96, 19);
		this.lvwLiterals.MultiSelect = false;
		this.lvwLiterals.Name = "lvwLiterals";
		this.lvwLiterals.Size = new System.Drawing.Size(712, 130);
		this.lvwLiterals.TabIndex = 12;
		this.lvwLiterals.UseCompatibleStateImageBehavior = false;
		this.lvwLiterals.View = System.Windows.Forms.View.Details;
		this.lvwLiterals.DoubleClick += new System.EventHandler(this.lvwLiterals_DoubleClick);
		// 
		// lviLiteralsID
		// 
		this.lviLiteralsID.Name = "lviLiteralsID";
		this.lviLiteralsID.Text = "ID";
		this.lviLiteralsID.Width = 140;
		// 
		// lviLiteralsToolTip
		// 
		this.lviLiteralsToolTip.Name = "lviLiteralsToolTip";
		this.lviLiteralsToolTip.Text = "ToolTip";
		this.lviLiteralsToolTip.Width = 222;
		// 
		// lviLiteralsDefaultValue
		// 
		this.lviLiteralsDefaultValue.Name = "lviLiteralsDefaultValue";
		this.lviLiteralsDefaultValue.Text = "Default Value";
		this.lviLiteralsDefaultValue.Width = 142;
		// 
		// lviLiteralsFunction
		// 
		this.lviLiteralsFunction.Name = "lviLiteralsFunction";
		this.lviLiteralsFunction.Text = "Function";
		this.lviLiteralsFunction.Width = 123;
		// 
		// lviLiteralsEditable
		// 
		this.lviLiteralsEditable.Name = "lviLiteralsEditable";
		this.lviLiteralsEditable.Text = "Editable";
		// 
		// clbSnippetTypes
		// 
		this.clbSnippetTypes.CheckOnClick = true;
		this.clbSnippetTypes.Items.AddRange(new object[] {
            "Expansion",
            "SurroundsWith",
            "Refactoring"});
		this.clbSnippetTypes.Location = new System.Drawing.Point(12, 19);
		this.clbSnippetTypes.Name = "clbSnippetTypes";
		this.clbSnippetTypes.Size = new System.Drawing.Size(176, 38);
		this.clbSnippetTypes.TabIndex = 8;
		this.clbSnippetTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSnippetTypes_ItemCheck);
		// 
		// groupBox5
		// 
		this.groupBox5.Controls.Add(this.clbSnippetTypes);
		this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.groupBox5.Location = new System.Drawing.Point(624, 45);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(200, 119);
		this.groupBox5.TabIndex = 8;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Snippet Types";
		// 
		// groupBox6
		// 
		this.groupBox6.Controls.Add(this.dataGridView1);
		this.groupBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.groupBox6.Location = new System.Drawing.Point(4, 170);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(820, 100);
		this.groupBox6.TabIndex = 9;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Imports";
		// 
		// dataGridView1
		// 
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
		this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
		this.dataGridView1.Location = new System.Drawing.Point(11, 17);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(796, 77);
		this.dataGridView1.TabIndex = 17;
		// 
		// Column1
		// 
		this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.Column1.HeaderText = "Namespace";
		this.Column1.Name = "Column1";
		// 
		// MainForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.ClientSize = new System.Drawing.Size(830, 787);
		this.Controls.Add(this.groupBox6);
		this.Controls.Add(this.groupBox5);
		this.Controls.Add(this.groupBox4);
		this.Controls.Add(this.groupBox3);
		this.Controls.Add(this.statusBar1);
		this.Controls.Add(this.groupBox2);
		this.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		this.Menu = this.mainMenu1;
		this.Name = "MainForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Snippy - Visual Studio Code Snippet Editor";
		this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.spFile)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.spBuffer)).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		this.groupBox6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
		this.ResumeLayout(false);

	}

	private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (this.clbSnippetTypes.Items.Count > 2)
		{
			this.clbSnippetTypes.Items.RemoveAt(2);
			this.clbSnippetTypes.Items.RemoveAt(1);
		}
		if (this.languageComboBox.Text == "vb")
		{
			this.comboBoxKind.Enabled = true;
			this.dataGridView1.Enabled = true;
			this.mnuNewSnippet.Enabled = false;
			foreach (DataGridViewRow row2 in this.dataGridView1.Rows)
			{
				row2.Visible = true;
			}
		}
		else
		{
			if (this.clbSnippetTypes.Items.Count < 3)
			{
				this.clbSnippetTypes.Items.AddRange(new object[] { "SurroundsWith", "Refactoring" });
			}
			this.comboBoxKind.Enabled = false;
			this.dataGridView1.Enabled = false;
			this.mnuNewSnippet.Enabled = true;
			foreach (DataGridViewRow row in this.dataGridView1.Rows)
			{
				try
				{
					row.Visible = false;
					continue;
				}
				catch (Exception)
				{
					continue;
				}
			}
		}
		this.OnDataChanged(sender, e);
	}

	private void lvwLiterals_DoubleClick(object sender, EventArgs e)
	{
		this.editLiteral();
	}

	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new MainForm());
	}

	private void MainForm_Closing(object sender, CancelEventArgs e)
	{
		if (!this.confirmLoseChanges())
		{
			e.Cancel = true;
		}
	}

	private void menuItem2_Click(object sender, EventArgs e)
	{
		new Help().ShowDialog();
	}

	private void mnuAbout_Click(object sender, EventArgs e)
	{
		new AboutForm().ShowDialog();
	}

	private void mnuExit_Click(object sender, EventArgs e)
	{
		if (this.confirmLoseChanges())
		{
			Application.Exit();
		}
	}

	private void mnuLoad_Click(object sender, EventArgs e)
	{
		if (this.confirmLoseChanges())
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "snippet";
			dialog.Filter = "Snippet files (*.snippet)|*.snippet";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					this._app.LoadFile(dialog.FileName);
				}
				catch (XmlException)
				{
					MessageBox.Show("Failed to load " + dialog.FileName + " as a valid XML file.", "Error - Snippy", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				this._app.SetCurrentSnippet(0);
				this.refreshForm(true);
				this._dirtySnippet = false;
				this.setFileDirty(false);
			}
		}
	}

	private void mnuNew_Click(object sender, EventArgs e)
	{
		if (this.confirmLoseChanges())
		{
			this.newFile();
		}
	}

	private void mnuNewSnippet_Click(object sender, EventArgs e)
	{
		this._app.AppendNewSnippet();
		this.refreshForm(true);
		this.txtTitle.Focus();
		this._dirtySnippet = false;
		this._dirtySnippetFile = true;
	}

	private void mnuSave_Click(object sender, EventArgs e)
	{
		this.save();
	}

	private void mnuSaveAs_Click(object sender, EventArgs e)
	{
		this.saveAs();
	}

	private void newFile()
	{
		this._app.CreateNewFile();
		this.refreshForm(true);
		this.txtTitle.Focus();
		this._dirtySnippet = false;
		this._dirtySnippetFile = false;
	}

	private void OnDataChanged(object sender, EventArgs e)
	{
		this._dirtySnippet = true;
		this.setFileDirty(true);
	}

	private void refreshActiveSnippetList()
	{
		this.cbxActiveSnippet.Items.Clear();
		foreach (string str in this._app.GetSnippetTitles())
		{
			this.cbxActiveSnippet.Items.Add(str);
		}
		this.cbxActiveSnippet.SelectedIndex = this._app.CurrentSnippetIndex;
	}

	private void refreshCodeCombos()
	{
		this.languageComboBox.Text = this._app.CurrentSnippet.CodeLanguageAttribute;
		this.comboBoxKind.Text = this._app.CurrentSnippet.CodeKindAttribute;
	}

	private void refreshForm(bool shouldRefreshActiveSnippetList)
	{
		this.txtTitle.Text = this._app.CurrentSnippet.Title;
		this.txtShortcut.Text = this._app.CurrentSnippet.Shortcut;
		this.txtDescription.Text = this._app.CurrentSnippet.Description;
		this.txtAuthor.Text = this._app.CurrentSnippet.Author;
		this.rtbCode.Text = this._app.CurrentSnippet.Code;
		this.refreshLiteralsList();
		this.refreshImports();
		this.refreshSnippetTypes();
		this.refreshStatusBar();
		this.refreshCodeCombos();
		if (shouldRefreshActiveSnippetList)
		{
			this.refreshActiveSnippetList();
		}
	}

	private void refreshImports()
	{
		this.dataGridView1.Rows.Clear();
		foreach (string str in this._app.CurrentSnippet.Imports)
		{
			this.dataGridView1.Rows.Add(new object[] { str });
		}
	}

	private void refreshLiteralsList()
	{
		this.lvwLiterals.Items.Clear();
		foreach (Literal literal in this._app.CurrentSnippet.Literals)
		{
			ListViewItem item = new ListViewItem(new string[] { literal.ID, literal.ToolTip, literal.DefaultValue, literal.Function, literal.Editable.ToString() });
			item.Tag = literal.Object;
			this.lvwLiterals.Items.Add(item);
		}
	}

	private void refreshSnippetTypes()
	{
		for (int i = 0; i < this.clbSnippetTypes.Items.Count; i++)
		{
			this.clbSnippetTypes.SetItemChecked(i, false);
		}
		foreach (SnippetType type in this._app.CurrentSnippet.SnippetTypes)
		{
			string str = type.Value;
			if (str != null)
			{
				if (str != "Expansion")
				{
					if (str == "SurroundsWith")
					{
						goto Label_008D;
					}
					if (str == "Refactoring")
					{
						goto Label_00AF;
					}
				}
				else
				{
					this.clbSnippetTypes.SetItemChecked(0, true);
				}
			}
			continue;
		Label_008D:
			if (this.clbSnippetTypes.Items.Count > 1)
			{
				this.clbSnippetTypes.SetItemChecked(1, true);
			}
			continue;
		Label_00AF:
			if (this.clbSnippetTypes.Items.Count > 2)
			{
				this.clbSnippetTypes.SetItemChecked(2, true);
			}
		}
	}

	private void refreshStatusBar()
	{
		this.spFile.Text = " " + new FileInfo(this._app.CurrentFile).Name;
		if (this._dirtySnippetFile)
		{
			this.spFile.Text = this.spFile.Text + "* ";
		}
		else
		{
			this.spFile.Text = this.spFile.Text + " ";
		}
	}

	private void removeLiteral()
	{
		if (this.lvwLiterals.SelectedIndices.Count == 0)
		{
			MessageBox.Show("Need to select a literal to modify");
		}
		else
		{
			this.lvwLiterals.SelectedItems[0].Remove();
			this.OnDataChanged(this.lvwLiterals, null);
		}
	}

	private void save()
	{
		if (this._app.CurrentFile == "Untitled.snippet")
		{
			this.saveAs();
		}
		else
		{
			this.updateInMemorySnippet();
			this._app.Save();
			this.refreshActiveSnippetList();
			this._dirtySnippet = false;
			this.setFileDirty(false);
		}
	}

	private void saveAs()
	{
		this.updateInMemorySnippet();
		SaveFileDialog dialog = new SaveFileDialog();
		dialog.DefaultExt = "snippet";
		dialog.Filter = "Snippet files (*.snippet)|*.snippet";
		if (dialog.ShowDialog() == DialogResult.OK)
		{
			this._app.SaveAs(dialog.FileName);
			this.refreshActiveSnippetList();
			this._dirtySnippet = false;
			this.setFileDirty(false);
		}
	}

	private void setFileDirty(bool flag)
	{
		this._dirtySnippetFile = flag;
		this.mnuSave.Enabled = this._dirtySnippetFile;
		this.refreshStatusBar();
	}

	private void updateImports()
	{
		this._app.CurrentSnippet.ClearImports();
		if (this.dataGridView1.Enabled)
		{
			foreach (DataGridViewRow row in this.dataGridView1.Rows)
			{
				if (((row.Cells.Count > 0) && (row.Cells[0].Value != null)) && (row.Cells[0].Value.ToString() != string.Empty))
				{
					this._app.CurrentSnippet.AddImport(row.Cells[0].Value.ToString());
				}
			}
		}
	}

	private void updateInMemorySnippet()
	{
		if (this._dirtySnippet)
		{
			this._app.CurrentSnippet.Title = this.txtTitle.Text;
			this._app.CurrentSnippet.Shortcut = this.txtShortcut.Text;
			this._app.CurrentSnippet.Description = this.txtDescription.Text;
			this._app.CurrentSnippet.Author = this.txtAuthor.Text;
			this.updateSnippetTypes();
			this.updateImports();
			this.updateLiterals();
			this._app.CurrentSnippet.Code = this.rtbCode.Text;
			this._app.CurrentSnippet.CodeLanguageAttribute = this.languageComboBox.Text;
			if (this.comboBoxKind.Enabled)
			{
				this._app.CurrentSnippet.CodeKindAttribute = this.comboBoxKind.Text;
			}
			else
			{
				this._app.CurrentSnippet.CodeKindAttribute = null;
			}
			this._dirtySnippet = false;
		}
	}

	private void updateLiterals()
	{
		this._app.CurrentSnippet.ClearLiterals();
		for (int i = 0; i < this.lvwLiterals.Items.Count; i++)
		{
			string text = this.lvwLiterals.Items[i].SubItems[0].Text;
			string toolTip = this.lvwLiterals.Items[i].SubItems[1].Text;
			string defaultVal = this.lvwLiterals.Items[i].SubItems[2].Text;
			string function = this.lvwLiterals.Items[i].SubItems[3].Text;
			bool editable = bool.Parse(this.lvwLiterals.Items[i].SubItems[4].Text);
			this._app.CurrentSnippet.AddLiteral(text, toolTip, defaultVal, function, editable, (bool) this.lvwLiterals.Items[i].Tag);
		}
	}

	private void updateSnippetTypes()
	{
		this._app.CurrentSnippet.ClearSnippetTypes();
		for (int i = 0; i < this.clbSnippetTypes.Items.Count; i++)
		{
			if (this.clbSnippetTypes.GetItemChecked(i))
			{
				switch (i)
				{
					case 0:
						this._app.CurrentSnippet.AddSnippetType("Expansion");
						break;

					case 1:
						this._app.CurrentSnippet.AddSnippetType("SurroundsWith");
						break;

					case 2:
						this._app.CurrentSnippet.AddSnippetType("Refactoring");
						break;
				}
			}
		}
	}
}
	}

 

 
