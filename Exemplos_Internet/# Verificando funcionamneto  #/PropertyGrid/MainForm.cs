using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SreeSharp
{

	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Label label4;

		CustomClass myProperties = new CustomClass();
		private System.Windows.Forms.TextBox txtNameToRemove;

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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.chkReadOnly = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtNameToRemove = new System.Windows.Forms.TextBox();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(8, 8);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(224, 272);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtValue);
			this.groupBox1.Controls.Add(this.txtName);
			this.groupBox1.Controls.Add(this.cmdAdd);
			this.groupBox1.Controls.Add(this.chkReadOnly);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(240, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(208, 160);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Add Property";
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(64, 56);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(136, 20);
			this.txtValue.TabIndex = 5;
			this.txtValue.Text = "Value";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(64, 24);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(136, 20);
			this.txtName.TabIndex = 4;
			this.txtName.Text = "Name";
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(64, 128);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(96, 24);
			this.cmdAdd.TabIndex = 3;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// chkReadOnly
			// 
			this.chkReadOnly.Location = new System.Drawing.Point(8, 96);
			this.chkReadOnly.Name = "chkReadOnly";
			this.chkReadOnly.Size = new System.Drawing.Size(120, 16);
			this.chkReadOnly.TabIndex = 2;
			this.chkReadOnly.Text = "ReadOnly";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Value";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtNameToRemove);
			this.groupBox2.Controls.Add(this.cmdRemove);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(240, 184);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(208, 96);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Remove Property";
			// 
			// txtNameToRemove
			// 
			this.txtNameToRemove.Location = new System.Drawing.Point(64, 24);
			this.txtNameToRemove.Name = "txtNameToRemove";
			this.txtNameToRemove.Size = new System.Drawing.Size(136, 20);
			this.txtNameToRemove.TabIndex = 4;
			this.txtNameToRemove.Text = "";
			// 
			// cmdRemove
			// 
			this.cmdRemove.Location = new System.Drawing.Point(64, 64);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(96, 24);
			this.cmdRemove.TabIndex = 3;
			this.cmdRemove.Text = "Remove";
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Name";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 285);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "Form1";
			this.Text = "Add/Remove Items to/from Property Grid at Runtime";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
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

		

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			CustomProperty myProp = new CustomProperty(txtName.Text,txtValue.Text,chkReadOnly.Checked,true);
			myProperties.Add(myProp); 
			propertyGrid1.Refresh();
		}

		

		private void cmdRemove_Click(object sender, System.EventArgs e)
		{
			myProperties.Remove(txtNameToRemove.Text); 
			propertyGrid1.Refresh(); 
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			propertyGrid1.SelectedObject = myProperties;
		}
	}
}
