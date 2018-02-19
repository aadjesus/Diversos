using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestProject
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class CustomControlsForm : System.Windows.Forms.Form
	{
		private CustomControls.Win32Controls.CTextBox cTextBox;
		private CustomControls.Win32Controls.ToggleButton toggleButton1;
		private CustomControls.Win32Controls.PushButton pushButton1;
		private CustomControls.Win32Controls.DropDownColorPicker dropDownColorPicker1;
		private CustomControls.Win32Controls.DropDownCalendar dropDownCalendar1;
		private CustomControls.Win32Controls.DropDownBool dropDownBool1;
		private CustomControls.Win32Controls.DropDownListBox dropDownListBox1;
		private CustomControls.Win32Controls.DropDownListBoxButton dropDownListBoxButton1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomControlsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.dropDownListBox1.List.Items.AddRange(new string[] {"Highlight", "Black", "Green","FireBrick"});
			this.dropDownListBox1.SelectedIndex=0;
			this.dropDownListBoxButton1.List.Items.AddRange(new string[] {"First", "Second", "Third"});
		

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CustomControlsForm));
			this.cTextBox = new CustomControls.Win32Controls.CTextBox();
			this.toggleButton1 = new CustomControls.Win32Controls.ToggleButton();
			this.pushButton1 = new CustomControls.Win32Controls.PushButton();
			this.dropDownColorPicker1 = new CustomControls.Win32Controls.DropDownColorPicker();
			this.dropDownCalendar1 = new CustomControls.Win32Controls.DropDownCalendar();
			this.dropDownBool1 = new CustomControls.Win32Controls.DropDownBool();
			this.dropDownListBox1 = new CustomControls.Win32Controls.DropDownListBox();
			this.dropDownListBoxButton1 = new CustomControls.Win32Controls.DropDownListBoxButton();
			((System.ComponentModel.ISupportInitialize)(this.dropDownColorPicker1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownCalendar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownBool1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownListBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownListBoxButton1)).BeginInit();
			this.SuspendLayout();
			// 
			// cTextBox
			// 
			this.cTextBox.Location = new System.Drawing.Point(16, 160);
			this.cTextBox.Name = "cTextBox";
			this.cTextBox.Size = new System.Drawing.Size(120, 20);
			this.cTextBox.TabIndex = 4;
			this.cTextBox.Text = "Custom Controls";
			// 
			// toggleButton1
			// 
			this.toggleButton1.Image = ((System.Drawing.Image)(resources.GetObject("toggleButton1.Image")));
			this.toggleButton1.Location = new System.Drawing.Point(200, 64);
			this.toggleButton1.Name = "toggleButton1";
			this.toggleButton1.TabIndex = 6;
			this.toggleButton1.Text = "Toggle Button";
			this.toggleButton1.Click += new System.EventHandler(this.toggleButton1_Click);
			// 
			// pushButton1
			// 
			this.pushButton1.Image = ((System.Drawing.Image)(resources.GetObject("pushButton1.Image")));
			this.pushButton1.Location = new System.Drawing.Point(200, 32);
			this.pushButton1.Name = "pushButton1";
			this.pushButton1.TabIndex = 5;
			this.pushButton1.Text = "Push Button";
			this.pushButton1.Click += new System.EventHandler(this.pushButton1_Click);
			// 
			// dropDownColorPicker1
			// 
			this.dropDownColorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dropDownColorPicker1.Location = new System.Drawing.Point(16, 64);
			this.dropDownColorPicker1.Name = "dropDownColorPicker1";
			this.dropDownColorPicker1.TabIndex = 1;
			this.dropDownColorPicker1.Text = "White";
			this.dropDownColorPicker1.ValueChanged += new System.EventHandler(this.dropDownColorPicker1_ValueChanged);
			// 
			// dropDownCalendar1
			// 
			this.dropDownCalendar1.Location = new System.Drawing.Point(16, 32);
			this.dropDownCalendar1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
			this.dropDownCalendar1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this.dropDownCalendar1.Name = "dropDownCalendar1";
			this.dropDownCalendar1.TabIndex = 0;
			this.dropDownCalendar1.Text = "12-10-2003";
			this.dropDownCalendar1.Value = new System.DateTime(2003, 10, 12, 0, 0, 0, 0);
			this.dropDownCalendar1.DateChanged += new System.EventHandler(this.dropDownCalendar1_DateChanged);
			// 
			// dropDownBool1
			// 
			this.dropDownBool1.FalseValueString = "Disabled";
			this.dropDownBool1.Location = new System.Drawing.Point(16, 96);
			this.dropDownBool1.Name = "dropDownBool1";
			this.dropDownBool1.SelectedIndex = 0;
			this.dropDownBool1.TabIndex = 2;
			this.dropDownBool1.Text = "Enabled";
			this.dropDownBool1.TrueValueString = "Enabled";
			this.dropDownBool1.ValueChanged += new System.EventHandler(this.dropDownBool1_ValueChanged);
			// 
			// dropDownListBox1
			// 
			this.dropDownListBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dropDownListBox1.Location = new System.Drawing.Point(16, 128);
			this.dropDownListBox1.Name = "dropDownListBox1";
			this.dropDownListBox1.TabIndex = 3;
			this.dropDownListBox1.ItemSelected += new CustomControls.Win32Controls.DropDownListBox.ItemSelectedEventHandler(this.dropDownListBox1_ItemSelected);
			// 
			// dropDownListBoxButton1
			// 
			this.dropDownListBoxButton1.Image = ((System.Drawing.Image)(resources.GetObject("dropDownListBoxButton1.Image")));
			this.dropDownListBoxButton1.Location = new System.Drawing.Point(200, 96);
			this.dropDownListBoxButton1.Name = "dropDownListBoxButton1";
			this.dropDownListBoxButton1.TabIndex = 7;
			this.dropDownListBoxButton1.Text = "DD Button";
			this.dropDownListBoxButton1.ItemSelected += new CustomControls.Win32Controls.DropDownListBoxButton.ItemSelectedEventHandler(this.dropDownListBoxButton1_ItemSelected);
			// 
			// CustomControlsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(320, 197);
			this.Controls.Add(this.cTextBox);
			this.Controls.Add(this.dropDownListBoxButton1);
			this.Controls.Add(this.dropDownListBox1);
			this.Controls.Add(this.toggleButton1);
			this.Controls.Add(this.pushButton1);
			this.Controls.Add(this.dropDownColorPicker1);
			this.Controls.Add(this.dropDownCalendar1);
			this.Controls.Add(this.dropDownBool1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "CustomControlsForm";
			this.Text = "CustomControls";
			((System.ComponentModel.ISupportInitialize)(this.dropDownColorPicker1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownCalendar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownBool1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownListBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dropDownListBoxButton1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void dropDownCalendar1_DateChanged(object sender, System.EventArgs e)
		{
			this.Text=this.dropDownCalendar1.Value.ToShortDateString();
		}

		private void dropDownColorPicker1_ValueChanged(object sender, System.EventArgs e)
		{
			this.Text=this.dropDownColorPicker1.Value.Name;
			CustomControls.BaseClasses.AppColors.SetDefaultColors(this.dropDownColorPicker1.Value);
			this.Refresh();
		}

		private void dropDownBool1_ValueChanged(object sender, System.EventArgs e)
		{
			this.Text=this.dropDownBool1.Value.ToString();
			foreach(Control c in this.Controls )
			{
				if(c!=sender)
				{
					c.Enabled=this.dropDownBool1.Value;
				}
			}
		}

		private void pushButton1_Click(object sender, System.EventArgs e)
		{
			this.Text="Push Button Click";
		}

		private void toggleButton1_Click(object sender, System.EventArgs e)
		{
			this.Text="ToggleButton is pushed=  " + (!this.toggleButton1.Pushed).ToString();
		}
		

		private void dropDownListBoxButton1_ItemSelected(object sender, CustomControls.HelperClasses.ItemSelectedEventArgs e)
		{
			if(e.SelectedItem!=null)
			{
				this.Text=e.SelectedItem.ToString();
			}
		}

		private void dropDownListBox1_ItemSelected(object sender, CustomControls.HelperClasses.ItemSelectedEventArgs e)
		{
			if(e.SelectedItem!=null)
			{
				this.Text=e.SelectedItem.ToString();
				CustomControls.BaseClasses.AppColors.SetDefaultColors(Color.FromName(e.SelectedItem.ToString()));
				this.Refresh();
			}
		}

		
	}
}
