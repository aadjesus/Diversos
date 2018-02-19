using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestBed
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		#region Instance_Variables
		private System.Windows.Forms.CheckBox chkDate;
		private System.Windows.Forms.CheckBox chkTime;
		private System.Windows.Forms.CheckBox chkSmPrg;
		private System.Windows.Forms.CheckBox chkPtnPrg;
		private System.Windows.Forms.GroupBox groupBox1;
		private CustomControls.StatusBarPanelEx statusBarPanelExDate;
		private CustomControls.StatusBarPanelEx statusBarPanelExTime;
		private CustomControls.StatusBarPanelEx statusBarPanelExSm;
		private CustomControls.StatusBarPanelEx statusBarPanelExPtn;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Panel pnlPrgOptions;
		private System.Windows.Forms.GroupBox grpPatterns;
		private System.Windows.Forms.ComboBox cmbPatternList;
		private System.Windows.Forms.GroupBox grpColors;
		private System.Windows.Forms.Button btnColor;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private CustomControls.StatusBarPanelEx statusBarPanelEx1;
		private CustomControls.StatusBarEx statusBarEx;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbRegExType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox1;
		private CustomControls.TextBoxEx txtMaskValidator;
		private CustomControls.TextBoxEx txtRegExValidator;
		private System.Windows.Forms.TextBox txtMask;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btnNewMask;
		private Array hatchStyles;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.Button btnValidateAll;
		private System.Windows.Forms.CheckBox checkBox2;
		#endregion Instance_Variables

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			components = new Container();
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

				if (timer1!=null)
				{
					timer1.Enabled=false;
					timer1.Dispose();
					timer1=null;
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pnlPrgOptions = new System.Windows.Forms.Panel();
			this.grpPatterns = new System.Windows.Forms.GroupBox();
			this.cmbPatternList = new System.Windows.Forms.ComboBox();
			this.grpColors = new System.Windows.Forms.GroupBox();
			this.btnColor = new System.Windows.Forms.Button();
			this.chkPtnPrg = new System.Windows.Forms.CheckBox();
			this.chkSmPrg = new System.Windows.Forms.CheckBox();
			this.chkTime = new System.Windows.Forms.CheckBox();
			this.chkDate = new System.Windows.Forms.CheckBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.statusBarEx = new CustomControls.StatusBarEx();
			this.statusBarPanelEx1 = new CustomControls.StatusBarPanelEx();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnValidateAll = new System.Windows.Forms.Button();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.btnNewMask = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbRegExType = new System.Windows.Forms.ComboBox();
			this.txtMask = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtRegExValidator = new CustomControls.TextBoxEx();
			this.txtMaskValidator = new CustomControls.TextBoxEx();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			this.pnlPrgOptions.SuspendLayout();
			this.grpPatterns.SuspendLayout();
			this.grpColors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelEx1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.pnlPrgOptions,
																					this.chkPtnPrg,
																					this.chkSmPrg,
																					this.chkTime,
																					this.chkDate});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 113);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(494, 136);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "StatusBarEx options";
			// 
			// pnlPrgOptions
			// 
			this.pnlPrgOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.grpPatterns,
																						this.grpColors});
			this.pnlPrgOptions.Location = new System.Drawing.Point(240, 16);
			this.pnlPrgOptions.Name = "pnlPrgOptions";
			this.pnlPrgOptions.Size = new System.Drawing.Size(224, 112);
			this.pnlPrgOptions.TabIndex = 10;
			// 
			// grpPatterns
			// 
			this.grpPatterns.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.cmbPatternList});
			this.grpPatterns.Location = new System.Drawing.Point(8, 56);
			this.grpPatterns.Name = "grpPatterns";
			this.grpPatterns.Size = new System.Drawing.Size(208, 48);
			this.grpPatterns.TabIndex = 11;
			this.grpPatterns.TabStop = false;
			this.grpPatterns.Text = "Set pattern for Pattern ProgressBar";
			// 
			// cmbPatternList
			// 
			this.cmbPatternList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPatternList.Enabled = false;
			this.cmbPatternList.Location = new System.Drawing.Point(8, 16);
			this.cmbPatternList.Name = "cmbPatternList";
			this.cmbPatternList.Size = new System.Drawing.Size(192, 21);
			this.cmbPatternList.TabIndex = 5;
			this.cmbPatternList.SelectedIndexChanged += new System.EventHandler(this.cmbPatternList_SelectedIndexChanged);
			// 
			// grpColors
			// 
			this.grpColors.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.btnColor});
			this.grpColors.Location = new System.Drawing.Point(8, 8);
			this.grpColors.Name = "grpColors";
			this.grpColors.Size = new System.Drawing.Size(208, 48);
			this.grpColors.TabIndex = 10;
			this.grpColors.TabStop = false;
			this.grpColors.Text = "Select Color";
			// 
			// btnColor
			// 
			this.btnColor.BackColor = System.Drawing.Color.Red;
			this.btnColor.CausesValidation = false;
			this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnColor.ForeColor = System.Drawing.Color.Red;
			this.btnColor.Location = new System.Drawing.Point(8, 16);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(192, 23);
			this.btnColor.TabIndex = 0;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// chkPtnPrg
			// 
			this.chkPtnPrg.Location = new System.Drawing.Point(8, 103);
			this.chkPtnPrg.Name = "chkPtnPrg";
			this.chkPtnPrg.Size = new System.Drawing.Size(160, 24);
			this.chkPtnPrg.TabIndex = 3;
			this.chkPtnPrg.Text = "Show Pattern ProgressBar";
			this.chkPtnPrg.CheckStateChanged += new System.EventHandler(this.CheckStateChanged);
			// 
			// chkSmPrg
			// 
			this.chkSmPrg.Location = new System.Drawing.Point(8, 74);
			this.chkSmPrg.Name = "chkSmPrg";
			this.chkSmPrg.Size = new System.Drawing.Size(160, 24);
			this.chkSmPrg.TabIndex = 2;
			this.chkSmPrg.Text = "Show Smooth ProgressBar";
			this.chkSmPrg.CheckStateChanged += new System.EventHandler(this.CheckStateChanged);
			// 
			// chkTime
			// 
			this.chkTime.Location = new System.Drawing.Point(8, 45);
			this.chkTime.Name = "chkTime";
			this.chkTime.Size = new System.Drawing.Size(112, 24);
			this.chkTime.TabIndex = 1;
			this.chkTime.Text = "Show Time Panel";
			this.chkTime.CheckStateChanged += new System.EventHandler(this.CheckStateChanged);
			// 
			// chkDate
			// 
			this.chkDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkDate.Location = new System.Drawing.Point(8, 16);
			this.chkDate.Name = "chkDate";
			this.chkDate.Size = new System.Drawing.Size(112, 24);
			this.chkDate.TabIndex = 0;
			this.chkDate.Text = "Show Date Panel";
			this.chkDate.CheckStateChanged += new System.EventHandler(this.CheckStateChanged);
			// 
			// colorDialog1
			// 
			this.colorDialog1.Color = System.Drawing.Color.Red;
			this.colorDialog1.SolidColorOnly = true;
			// 
			// timer1
			// 
			this.timer1.Interval = 500;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// statusBarEx
			// 
			this.statusBarEx.Location = new System.Drawing.Point(0, 249);
			this.statusBarEx.Name = "statusBarEx";
			this.statusBarEx.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						   this.statusBarPanelEx1});
			this.statusBarEx.ShowPanels = true;
			this.statusBarEx.Size = new System.Drawing.Size(494, 22);
			this.statusBarEx.SizingGrip = false;
			this.statusBarEx.TabIndex = 2;
			this.statusBarEx.Text = "statusBarEx1";
			// 
			// statusBarPanelEx1
			// 
			this.statusBarPanelEx1.ForeColor = System.Drawing.Color.Empty;
			this.statusBarPanelEx1.HatchedProgressBarStyle = System.Drawing.Drawing2D.HatchStyle.Horizontal;
			this.statusBarPanelEx1.Maximum = 100;
			this.statusBarPanelEx1.Minimum = 1;
			this.statusBarPanelEx1.Style = CustomControls.StatusBarPanelStyleEx.Text;
			this.statusBarPanelEx1.Text = "Default Text";
			this.statusBarPanelEx1.Value = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.btnValidateAll,
																					this.checkBox2,
																					this.checkBox3,
																					this.btnNewMask,
																					this.checkBox1,
																					this.label2,
																					this.cmbRegExType,
																					this.txtMask,
																					this.label1,
																					this.txtRegExValidator,
																					this.txtMaskValidator});
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(494, 113);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "TextBoxEx Options";
			// 
			// btnValidateAll
			// 
			this.btnValidateAll.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.btnValidateAll.Location = new System.Drawing.Point(368, 80);
			this.btnValidateAll.Name = "btnValidateAll";
			this.btnValidateAll.Size = new System.Drawing.Size(104, 23);
			this.btnValidateAll.TabIndex = 9;
			this.btnValidateAll.Text = "Validate All";
			this.btnValidateAll.Click += new System.EventHandler(this.btnValidateAll_Click);
			// 
			// checkBox2
			// 
			this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(8, 80);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(128, 24);
			this.checkBox2.TabIndex = 10;
			this.checkBox2.Text = "Use ENTER as TAB";
			this.toolTip1.SetToolTip(this.checkBox2, "This will set the focus to next control in the form if you press [Enter]");
			this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(344, 16);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(16, 24);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "checkBox3";
			this.toolTip1.SetToolTip(this.checkBox3, "If checked a value is required for this field");
			this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
			// 
			// btnNewMask
			// 
			this.btnNewMask.Location = new System.Drawing.Point(312, 16);
			this.btnNewMask.Name = "btnNewMask";
			this.btnNewMask.Size = new System.Drawing.Size(24, 23);
			this.btnNewMask.TabIndex = 1;
			this.btnNewMask.Text = "->";
			this.toolTip1.SetToolTip(this.btnNewMask, "Use my mask");
			this.btnNewMask.Click += new System.EventHandler(this.btnNewMask_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(344, 48);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(16, 24);
			this.checkBox1.TabIndex = 5;
			this.toolTip1.SetToolTip(this.checkBox1, "If checked a value is required for this field");
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(179, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Test Regular Expression validation";
			// 
			// cmbRegExType
			// 
			this.cmbRegExType.Items.AddRange(new object[] {
															  "Email",
															  "IP",
															  "Date",
															  "URL",
															  "ZIP"});
			this.cmbRegExType.Location = new System.Drawing.Point(200, 48);
			this.cmbRegExType.Name = "cmbRegExType";
			this.cmbRegExType.Size = new System.Drawing.Size(136, 21);
			this.cmbRegExType.TabIndex = 4;
			this.cmbRegExType.Text = "Select or type pattern)";
			this.cmbRegExType.TextChanged += new System.EventHandler(this.cmbRegExType_TextChanged);
			this.cmbRegExType.SelectedIndexChanged += new System.EventHandler(this.cmbRegExType_SelectedIndexChanged);
			// 
			// txtMask
			// 
			this.txtMask.Location = new System.Drawing.Point(200, 16);
			this.txtMask.MaxLength = 18;
			this.txtMask.Name = "txtMask";
			this.txtMask.Size = new System.Drawing.Size(104, 20);
			this.txtMask.TabIndex = 0;
			this.txtMask.Text = "(0##)-AaaA-$$$-&&&";
			this.toolTip1.SetToolTip(this.txtMask, "MASK format : # = Numbers, A = Capital letters, a = Simple letters, $ = Capital/S" +
				"imple letters/Numbers, $ = Capital/Simple letters");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Test MASK validation";
			// 
			// txtRegExValidator
			// 
			this.txtRegExValidator.ErrorMessage = "Input data is not in a valid format";
			this.txtRegExValidator.IsRequired = false;
			this.txtRegExValidator.Location = new System.Drawing.Point(368, 48);
			this.txtRegExValidator.Name = "txtRegExValidator";
			this.txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Custom;
			this.txtRegExValidator.ShowErrorIcon = true;
			this.txtRegExValidator.Size = new System.Drawing.Size(104, 20);
			this.txtRegExValidator.TabIndex = 6;
			this.txtRegExValidator.Text = "";
			this.txtRegExValidator.TextInputMode = CustomControls.InputMode.Insert;
			this.txtRegExValidator.UseEnterAsTab = true;
			this.txtRegExValidator.ValidationMode = CustomControls.ValidationModes.RegularExpression;
			this.txtRegExValidator.ValidationText = "";
			// 
			// txtMaskValidator
			// 
			this.txtMaskValidator.ErrorMessage = "";
			this.txtMaskValidator.IsRequired = false;
			this.txtMaskValidator.Location = new System.Drawing.Point(368, 16);
			this.txtMaskValidator.Name = "txtMaskValidator";
			this.txtMaskValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Custom;
			this.txtMaskValidator.ShowErrorIcon = true;
			this.txtMaskValidator.Size = new System.Drawing.Size(104, 20);
			this.txtMaskValidator.TabIndex = 3;
			this.txtMaskValidator.Text = "(0__)-____-___-___";
			this.txtMaskValidator.TextInputMode = CustomControls.InputMode.Insert;
			this.toolTip1.SetToolTip(this.txtMaskValidator, "TextBoxEx in MASK mode always required full data or none at all");
			this.txtMaskValidator.UseEnterAsTab = true;
			this.txtMaskValidator.ValidationMode = CustomControls.ValidationModes.MaskEdit;
			this.txtMaskValidator.ValidationText = "(0##)-AaaA-$$$-&&&";
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 10000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(494, 271);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.groupBox1,
																		  this.statusBarEx});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CustomControls Test Window";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.pnlPrgOptions.ResumeLayout(false);
			this.grpPatterns.ResumeLayout(false);
			this.grpColors.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelEx1)).EndInit();
			this.groupBox2.ResumeLayout(false);
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


		#region Private_Methods
		private CustomControls.StatusBarPanelEx PrepareDatePanel()
		{
			statusBarPanelExDate = new CustomControls.StatusBarPanelEx();
			statusBarPanelExDate.ForeColor = System.Drawing.Color.Empty;
			statusBarPanelExDate.HatchedProgressBarStyle = System.Drawing.Drawing2D.HatchStyle.Horizontal;
			statusBarPanelExDate.Maximum = 100;
			statusBarPanelExDate.Minimum = 1;
			statusBarPanelExDate.Style = CustomControls.StatusBarPanelStyleEx.Date;
			statusBarPanelExDate.Value = 0;
			return statusBarPanelExDate;
		}

		private CustomControls.StatusBarPanelEx PrepareTimePanel()
		{
			statusBarPanelExTime = new CustomControls.StatusBarPanelEx();
			statusBarPanelExTime.ForeColor = System.Drawing.Color.Empty;
			statusBarPanelExTime.HatchedProgressBarStyle = System.Drawing.Drawing2D.HatchStyle.Horizontal;
			statusBarPanelExTime.Maximum = 100;
			statusBarPanelExTime.Minimum = 1;
			statusBarPanelExTime.Style = CustomControls.StatusBarPanelStyleEx.Time;
			statusBarPanelExTime.Value = 0;
			return statusBarPanelExTime;
		}

		private CustomControls.StatusBarPanelEx PrepareSmoothPBarPanel()
		{
			statusBarPanelExSm = new CustomControls.StatusBarPanelEx();
			statusBarPanelExSm.ForeColor = System.Drawing.SystemColors.HotTrack;
			statusBarPanelExSm.HatchedProgressBarStyle = System.Drawing.Drawing2D.HatchStyle.Horizontal;
			statusBarPanelExSm.Maximum = 100;
			statusBarPanelExSm.Minimum = 1;
			statusBarPanelExSm.Style = CustomControls.StatusBarPanelStyleEx.SmoothProgressBar;
			statusBarPanelExSm.Value = 0;
			return statusBarPanelExSm;
		}

		private CustomControls.StatusBarPanelEx PreparePatternPBarPanel()
		{
			statusBarPanelExPtn = new CustomControls.StatusBarPanelEx();
			statusBarPanelExPtn.ForeColor = System.Drawing.Color.Red;
			statusBarPanelExPtn.HatchedProgressBarStyle = System.Drawing.Drawing2D.HatchStyle.Horizontal;
			statusBarPanelExPtn.Maximum = 100;
			statusBarPanelExPtn.Minimum = 1;
			statusBarPanelExPtn.Style = CustomControls.StatusBarPanelStyleEx.HatchedProgressBar;
			statusBarPanelExPtn.Value = 0;
			return statusBarPanelExPtn;
		}

		private void UpdateOptionsPanel()
		{
			pnlPrgOptions.Enabled = ((chkSmPrg.Checked) || (chkPtnPrg.Checked))?true:false;
			grpPatterns.Enabled = (chkPtnPrg.Checked)? true:false;
		}
		private void LoadPBarStyles()
		{
			cmbPatternList.Items.AddRange(Enum.GetNames(typeof(HatchStyle)));
			cmbPatternList.SelectedIndex=0;
			hatchStyles = Enum.GetValues(typeof(HatchStyle));
		}
		private void UpdatePBarSettings()
		{
			//now update the statusbar colors
			if (chkSmPrg.Checked)
				statusBarPanelExSm.ForeColor=btnColor.BackColor;

			if (chkPtnPrg.Checked)
				statusBarPanelExPtn.ForeColor=btnColor.BackColor;
		}
		private bool RecursiveControlValidation(Control ctl)
		{
			bool isValid=true;

			if (ctl.HasChildren)
			{
				foreach(Control c in ctl.Controls)
				{
					isValid &=RecursiveControlValidation(c);
				}
			}
			if(ctl.GetType()==typeof(CustomControls.TextBoxEx))
			{
				if ( !((CustomControls.TextBoxEx)ctl).Validate() )
				{
					isValid =false;
				}
			}
			return isValid;
		}

		#endregion Private_Methods

		#region Event_Handlers
		private void CheckStateChanged(object sender, System.EventArgs e)
		{
			try
			{
				CheckBox temp = (CheckBox)sender;
				switch(temp.Name)
				{
					case "chkDate": 
						if (chkDate.Checked)
							statusBarEx.Panels.Add(PrepareDatePanel());
						else
						{
							statusBarPanelExDate.Dispose();
							statusBarPanelExDate=null;
						}
						break;

					case "chkTime":
						if (chkTime.Checked)
							statusBarEx.Panels.Add(PrepareTimePanel());
						else
						{
							statusBarPanelExTime.Dispose();
							statusBarPanelExTime=null;
						}
						break;

					case "chkSmPrg":
						if (chkSmPrg.Checked)
						{
							UpdateOptionsPanel();
							statusBarEx.Panels.Add(PrepareSmoothPBarPanel());
							UpdatePBarSettings();
							timer1.Enabled=true;
						}
						else
						{
							statusBarPanelExSm.Dispose();
							statusBarPanelExSm=null;
							timer1.Enabled= chkPtnPrg.Checked; //keep timer if pattern progress bar is working
						}
						break;

					case "chkPtnPrg":
						if (chkPtnPrg.Checked)
						{
							UpdateOptionsPanel();
							statusBarEx.Panels.Add(PreparePatternPBarPanel());
							UpdatePBarSettings();
							cmbPatternList.Enabled=true;
							timer1.Enabled=true;
						}
						else
						{
							statusBarPanelExPtn.Dispose();
							statusBarPanelExPtn=null;
							cmbPatternList.Enabled=false;
							timer1.Enabled=chkSmPrg.Checked; //keep timer if smooth pbar is working
						}
						break;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(this,"Eh Oh!, check the source for : " + ex.Message);
			}
		}

		private void btnValidateAll_Click(object sender, System.EventArgs e)
		{
			bool allValid=true;

			allValid = RecursiveControlValidation(this);

			if (allValid)
				MessageBox.Show(this,"All controls contain valid data");
			else
				MessageBox.Show(this,"Indicated controls contain invalid/incomplete data");
		}

		private void cmbRegExType_TextChanged(object sender, System.EventArgs e)
		{
			txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Custom;
			txtRegExValidator.ValidationText = cmbRegExType.Text;//customer pattern
		}

		private void btnNewMask_Click(object sender, System.EventArgs e)
		{
			txtMaskValidator.ValidationText = txtMask.Text;
		}

		private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBox3.Checked)
				txtMaskValidator.IsRequired=true;
			else
				txtMaskValidator.IsRequired=false;
		}
		
		private void Form1_Load(object sender, System.EventArgs e)
		{
			LoadPBarStyles();
			UpdatePBarSettings();
		}

		private void btnColor_Click(object sender, System.EventArgs e)
		{
			colorDialog1.Color = btnColor.BackColor;
			colorDialog1.ShowDialog(this);
			btnColor.BackColor =  colorDialog1.Color;
			btnColor.ForeColor = btnColor.BackColor;
			UpdatePBarSettings();
		}
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			//update all progress bars.
			//Additionaly you can use overloaded methods to specify
			//what progressbar to update with what value
			if ( (statusBarPanelExSm!=null) && (statusBarPanelExSm.Value==statusBarPanelExSm.Maximum) )
				statusBarPanelExSm.Value=0;

			if( (statusBarPanelExPtn!=null) && (statusBarPanelExPtn.Value==statusBarPanelExPtn.Maximum) )
				statusBarPanelExPtn.Value=0;

			statusBarEx.UpdateValue();
		}

		private void cmbPatternList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//update the pattern progressbar value
			if (statusBarPanelExPtn!=null)
			{
				statusBarPanelExPtn.HatchedProgressBarStyle=(HatchStyle)hatchStyles.GetValue(cmbPatternList.SelectedIndex); //hatchStyles.GetValue(cmbPatternList.SelectedIndex)
			}
		}

		private void cmbRegExType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ComboBox temp = (ComboBox)sender;
			switch (temp.SelectedIndex)
			{
				case 0: txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Email;break;
				case 1: txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.IP;break;
				case 2: txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Dates;break;
				case 3: txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Url;break;
				case 4: txtRegExValidator.RegExPatternMode = CustomControls.RegularExpressionModes.Zip;break;
			}
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			txtRegExValidator.IsRequired = (checkBox1.Checked)?true:false;
		}

		#endregion Event_Handlers

		private void checkBox2_CheckStateChanged(object sender, System.EventArgs e)
		{
			if (checkBox2.Checked)
			{
				txtMaskValidator.UseEnterAsTab=true;
				txtRegExValidator.UseEnterAsTab=true;
			}
			else
			{
				txtMaskValidator.UseEnterAsTab=false;
				txtRegExValidator.UseEnterAsTab=false;
			}
		}

	}
}
