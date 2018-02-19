using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Phone_Number_Control
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class PhoneNumControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtAreaCode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPhnNumber;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCntryCode;

		#region User Defined Variables
		private PhoneNumber phonedata = new PhoneNumber();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PhoneNumber PhoneData
		{
			get
			{
				return this.phonedata;
			}
			set
			{
				if(value!=null)
				{
					this.phonedata = value;
					DisplayData();
				}
			}

		}
		#endregion
		public PhoneNumControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call

			// subscribe for the event
			this.phonedata.PropertyChanged +=new PropertyChangedEventHandler(this.PhoneNumber_Changed);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtCntryCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAreaCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPhnNumber = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Location = new System.Drawing.Point(32, 64);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "CountryCode";
			// 
			// txtCntryCode
			// 
			this.txtCntryCode.Location = new System.Drawing.Point(152, 64);
			this.txtCntryCode.MaxLength = 2;
			this.txtCntryCode.Name = "txtCntryCode";
			this.txtCntryCode.TabIndex = 1;
			this.txtCntryCode.Text = "";
			this.txtCntryCode.TextChanged += new System.EventHandler(this.txtCntryCode_TextChanged);
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label2.Location = new System.Drawing.Point(32, 104);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "AreaCode";
			// 
			// txtAreaCode
			// 
			this.txtAreaCode.Location = new System.Drawing.Point(152, 104);
			this.txtAreaCode.MaxLength = 3;
			this.txtAreaCode.Name = "txtAreaCode";
			this.txtAreaCode.TabIndex = 3;
			this.txtAreaCode.Text = "";
			this.txtAreaCode.TextChanged += new System.EventHandler(this.txtAreaCode_TextChanged);
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label3.Location = new System.Drawing.Point(32, 24);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(152, 24);
			this.txtName.Name = "txtName";
			this.txtName.TabIndex = 5;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label4.Location = new System.Drawing.Point(32, 144);
			this.label4.Name = "label4";
			this.label4.TabIndex = 6;
			this.label4.Text = "Phone Number";
			// 
			// txtPhnNumber
			// 
			this.txtPhnNumber.Location = new System.Drawing.Point(152, 144);
			this.txtPhnNumber.Name = "txtPhnNumber";
			this.txtPhnNumber.TabIndex = 7;
			this.txtPhnNumber.Text = "";
			this.txtPhnNumber.TextChanged += new System.EventHandler(this.txtPhnNumber_TextChanged);
			// 
			// PhoneNumControl
			// 
			this.Controls.Add(this.txtPhnNumber);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtAreaCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtCntryCode);
			this.Controls.Add(this.label1);
			this.Name = "PhoneNumControl";
			this.Size = new System.Drawing.Size(320, 224);
			this.ResumeLayout(false);

		}
		#endregion

		private void txtPhnNumber_TextChanged(object sender, System.EventArgs e)
		{
		    phonedata.PhoneNum = txtPhnNumber.Text;
		}

		private void PhoneNumber_Changed(string name)
		{
			DisplayData();
		}

		private void DisplayData()
		{
			txtName.Text = phonedata.Name;
			txtCntryCode.Text = phonedata.CountryCode;
			txtAreaCode.Text = phonedata.AreaCode;
			txtPhnNumber.Text = phonedata.PhoneNum;
		}

		private void txtName_TextChanged(object sender, System.EventArgs e)
		{
			 phonedata.Name = txtName.Text;
		}

		private void txtCntryCode_TextChanged(object sender, System.EventArgs e)
		{
			phonedata.CountryCode = txtCntryCode.Text;
		}

		private void txtAreaCode_TextChanged(object sender, System.EventArgs e)
		{
			phonedata.AreaCode = txtAreaCode.Text;
		}
	}
}
