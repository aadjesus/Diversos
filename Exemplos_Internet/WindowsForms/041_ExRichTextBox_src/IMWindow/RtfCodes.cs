using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace IMWindow {

	/// <summary>
	/// Summary description for RtfCodes.
	/// </summary>
	public class RtfCodes : System.Windows.Forms.Form {

		private System.Windows.Forms.TextBox tbox_RtfCodes;
		private int line;
		StringBuilder appendText;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RtfCodes() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			tbox_RtfCodes.Font = new Font(FontFamily.GenericMonospace, 8.2f);
			tbox_RtfCodes.ScrollBars = ScrollBars.Both;
			tbox_RtfCodes.WordWrap = false;
			tbox_RtfCodes.AcceptsReturn = true;
			line = 0;	
		}

		public RtfCodes(Form _owner) : this() {
			this.Owner = _owner;
			this.Width = _owner.Width;
			this.Height = _owner.Height / 2;
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(this.Owner.Location.X, this.Owner.Location.Y - this.Height - SystemInformation.CaptionHeight);
		}

		public void AppendText(string _text) {
			++line;
			appendText = new StringBuilder();
			appendText.Append("<");
			appendText.Append("Line ");
			appendText.Append(line);
			appendText.Append(">");
			appendText.Append(" ");
			appendText.Append(_text);
			tbox_RtfCodes.Text += appendText.ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
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
		private void InitializeComponent() {
			this.tbox_RtfCodes = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tbox_RtfCodes
			// 
			this.tbox_RtfCodes.BackColor = System.Drawing.SystemColors.Window;
			this.tbox_RtfCodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbox_RtfCodes.Multiline = true;
			this.tbox_RtfCodes.Name = "tbox_RtfCodes";
			this.tbox_RtfCodes.ReadOnly = true;
			this.tbox_RtfCodes.Size = new System.Drawing.Size(504, 149);
			this.tbox_RtfCodes.TabIndex = 0;
			this.tbox_RtfCodes.Text = "";
			// 
			// RtfCodes
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 149);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tbox_RtfCodes});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RtfCodes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Rtf Codes";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
