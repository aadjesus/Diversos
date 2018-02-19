//
// RichTextBoxExtended
// Written to support a word processing style toolbar by Richard Parsons
//		(http://www.codeproject.com/script/profile/whos_who.asp?id=419005)
// Extended to support form designer persistance and data binding by Declan Brennan
//		(http://www.codeproject.com/script/profile/whos_who.asp?id=495690)
//

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Extended.Windows.Forms
{
	/// <summary>
	/// Summary description for RichTextBoxContentEditor.
	/// </summary>
	public class RichTextBoxExtendedRtfEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
        private Extended.Windows.Forms.RichTextBoxExtended richTextBoxExtended1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RichTextBoxExtendedRtfEditorForm()
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.richTextBoxExtended1 = new Extended.Windows.Forms.RichTextBoxExtended();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 271);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(440, 41);
            this.panel1.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.OKButton.Location = new System.Drawing.Point(368, 10);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(62, 21);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.cancelButton.Location = new System.Drawing.Point(10, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(63, 21);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            // 
            // richTextBoxExtended1
            // 
            this.richTextBoxExtended1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxExtended1.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxExtended1.Name = "richTextBoxExtended1";
            this.richTextBoxExtended1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
    "ans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs16\\par\r\n}\r\n";
            this.richTextBoxExtended1.ShowOpen = false;
            this.richTextBoxExtended1.ShowSave = false;
            this.richTextBoxExtended1.Size = new System.Drawing.Size(440, 271);
            this.richTextBoxExtended1.TabIndex = 2;
            // 
            // RichTextBoxExtendedRtfEditorForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(440, 312);
            this.Controls.Add(this.richTextBoxExtended1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(292, 191);
            this.Name = "RichTextBoxExtendedRtfEditorForm";
            this.Text = "Rich Text";
            this.Load += new System.EventHandler(this.RichTextBoxExtendedRtfEditorForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void RichTextBoxExtendedRtfEditorForm_Load(object sender, System.EventArgs e)
		{
			this.richTextBoxExtended1.Rtf= mValue;
		}

		string mValue;

		public string Value
		{
			get
			{	return this.richTextBoxExtended1.Rtf;
			}
			set
			{	// Delay the setting of Rtf until the form is fully loaded
				// Otherwise the RichTextBox looses its formatting.
				mValue= value;
			}
		}
	}
}
