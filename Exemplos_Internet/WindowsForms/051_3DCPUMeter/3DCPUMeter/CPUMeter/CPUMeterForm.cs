using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Uchukamen.CPUMeter
{
	/// <summary>
	/// 3D like CPU Meter Main form.
	/// </summary>
	public class CPUMeterForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timer1;
		private Uchukamen.CPUMeter.CPUMeterPanel cpuMeterPanel1;
		private System.ComponentModel.IContainer components;

		public CPUMeterForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CPUMeterForm));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.cpuMeterPanel1 = new Uchukamen.CPUMeter.CPUMeterPanel();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// cpuMeterPanel1
			// 
			this.cpuMeterPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cpuMeterPanel1.BackgroundImage")));
			this.cpuMeterPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cpuMeterPanel1.Location = new System.Drawing.Point(0, 0);
			this.cpuMeterPanel1.Name = "cpuMeterPanel1";
			this.cpuMeterPanel1.OriginalBitmap = ((System.Drawing.Bitmap)(resources.GetObject("cpuMeterPanel1.OriginalBitmap")));
			this.cpuMeterPanel1.Size = new System.Drawing.Size(240, 209);
			this.cpuMeterPanel1.TabIndex = 0;
			this.cpuMeterPanel1.TabStop = false;
			// 
			// CPUMeterForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(240, 209);
			this.Controls.Add(this.cpuMeterPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(300, 300);
			this.MinimumSize = new System.Drawing.Size(100, 100);
			this.Name = "CPUMeterForm";
			this.Text = "CPU Meter";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new CPUMeterForm());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			this.timer1.Enabled = true;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.Refresh();
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			if(this.WindowState != FormWindowState.Minimized) 
				this.Refresh();
		}
	}
}