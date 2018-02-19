using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RtfInsert
{
	/// <summary>
	/// Summary description for PlainTextDialog.
	/// </summary>
	public class PlainTextDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox tbox_Plain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_OK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string PlainText {
			get {return tbox_Plain.Text;}
		}

		public PlainTextDialog()
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
			this.tbox_Plain = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_OK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbox_Plain
			// 
			this.tbox_Plain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbox_Plain.Location = new System.Drawing.Point(16, 40);
			this.tbox_Plain.Multiline = true;
			this.tbox_Plain.Name = "tbox_Plain";
			this.tbox_Plain.Size = new System.Drawing.Size(384, 176);
			this.tbox_Plain.TabIndex = 0;
			this.tbox_Plain.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Type or Paste Text Here";
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Cancel.Location = new System.Drawing.Point(208, 232);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.TabIndex = 2;
			this.btn_Cancel.Text = "Cancel";
			// 
			// btn_OK
			// 
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_OK.Location = new System.Drawing.Point(120, 232);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.TabIndex = 3;
			this.btn_OK.Text = "Insert";
			// 
			// PlainTextDialog
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(418, 263);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btn_OK,
																		  this.btn_Cancel,
																		  this.label1,
																		  this.tbox_Plain});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PlainTextDialog";
			this.Text = "Plain Text Dialog";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
