using System;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MdiClientDemo
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelCopyright;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


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
			this.buttonOk = new System.Windows.Forms.Button();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(113, 96);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "OK";
			// 
			// labelVersion
			// 
			this.labelVersion.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelVersion.Location = new System.Drawing.Point(0, 0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(306, 32);
			this.labelVersion.TabIndex = 1;
			this.labelVersion.Text = "<version>";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelCopyright
			// 
			this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelCopyright.Location = new System.Drawing.Point(0, 32);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(306, 56);
			this.labelCopyright.TabIndex = 2;
			this.labelCopyright.Text = "<copyright>";
			this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AboutForm
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(306, 128);
			this.Controls.Add(this.labelCopyright);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.buttonOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "<about>";
			this.ResumeLayout(false);

		}
		#endregion


		#region Constructor

		public AboutForm()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

			// Make use of the AssemblyInfo helper class.
			AssemblyInfo aInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
			Text = "About " + aInfo.Product;			

			aInfo = new AssemblyInfo(Assembly.GetAssembly(typeof(Slusser.Components.MdiClientController)));
			labelVersion.Text = aInfo.Product + " v" + aInfo.Version;
			labelCopyright.Text = aInfo.Copyright;
		}

		#endregion // Constructor
	}
}
