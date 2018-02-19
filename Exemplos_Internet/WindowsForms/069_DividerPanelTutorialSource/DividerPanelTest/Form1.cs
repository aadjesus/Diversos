using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DividerPanelTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button button1;
		private DividerPanel.DividerPanel dividerPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
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
			this.dividerPanel1 = new DividerPanel.DividerPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.dividerPanel2 = new DividerPanel.DividerPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.dividerPanel1.SuspendLayout();
			this.dividerPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// dividerPanel1
			// 
			this.dividerPanel1.AllowDrop = true;
			this.dividerPanel1.BackColor = System.Drawing.SystemColors.Control;
			this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
			this.dividerPanel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.button1});
			this.dividerPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dividerPanel1.Location = new System.Drawing.Point(0, 134);
			this.dividerPanel1.Name = "dividerPanel1";
			this.dividerPanel1.Size = new System.Drawing.Size(272, 40);
			this.dividerPanel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.button1.Location = new System.Drawing.Point(100, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			// 
			// dividerPanel2
			// 
			this.dividerPanel2.AllowDrop = true;
			this.dividerPanel2.BackColor = System.Drawing.SystemColors.Window;
			this.dividerPanel2.BorderSide = System.Windows.Forms.Border3DSide.Bottom;
			this.dividerPanel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.label2,
																						this.label1});
			this.dividerPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.dividerPanel2.Name = "dividerPanel2";
			this.dividerPanel2.Size = new System.Drawing.Size(272, 56);
			this.dividerPanel2.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "A custom control tutorial from start to ToolBox";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Divider Panel";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 174);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.dividerPanel2,
																		  this.dividerPanel1});
			this.Name = "Form1";
			this.Text = "DividerPanel Test";
			this.dividerPanel1.ResumeLayout(false);
			this.dividerPanel2.ResumeLayout(false);
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
	}
}
