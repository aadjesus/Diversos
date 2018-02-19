using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Sample
{
	public class MainForm : System.Windows.Forms.Form
	{
		private SampleControl sampleControl1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private Sample.SampleControl sampleControl2;
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.sampleControl2 = new Sample.SampleControl();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(312, 24);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.SelectedObject = this.sampleControl2;
			this.propertyGrid1.Size = new System.Drawing.Size(256, 408);
			this.propertyGrid1.TabIndex = 1;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// sampleControl2
			// 
			this.sampleControl2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(82)), ((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(255)));
			this.sampleControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.sampleControl2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(180)), ((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.sampleControl2.Location = new System.Drawing.Point(32, 40);
			this.sampleControl2.Name = "sampleControl2";
			this.sampleControl2.Size = new System.Drawing.Size(264, 168);
			this.sampleControl2.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(576, 445);
			this.Controls.Add(this.sampleControl2);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}
	}
}
