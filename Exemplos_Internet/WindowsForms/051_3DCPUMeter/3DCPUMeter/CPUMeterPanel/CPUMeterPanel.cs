using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
 
namespace Uchukamen.CPUMeter
{
	/// <summary>
	/// CPUMeter の概要の説明です。
	/// </summary>
	public class CPUMeterPanel: System.Windows.Forms.PictureBox
	{
		private System.ComponentModel.IContainer components;

		public CPUMeterPanel()
		{
			// 
			//
			InitializeComponent();

			// Initialize Brush and Image
			handBrush = new SolidBrush(handColor);
			LoadImage();
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
			this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
			// 
			// performanceCounter1
			// 
			this.performanceCounter1.CategoryName = "Processor";
			this.performanceCounter1.CounterName = "% Processor Time";
			this.performanceCounter1.InstanceName = "_Total";
			// 
			// CPUMeterPanel
			// 
			this.Resize += new System.EventHandler(this.CPUMeterPanel_Resize);
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();

		}
		#endregion

		#region プロパティ
		private Color handColor = Color.White;
		[Category( "Meter" )]
		[Description( "Hand Color" )]
		[DefaultValue( typeof(Color), "White" )]
		public Color HandColor
		{
			get {return this.handColor;}
			set 
			{
				handColor = value;
				handBrush = new SolidBrush(value);
				Refresh();
			}
		}

		private double startAngle = -125.0;
		[Category( "Meter" )]
		[Description( "Start Angle" )]
		[DefaultValue( typeof(double), "-125.0" )]
		public double StartAngle
		{
			get {return startAngle;}
			set 
			{
				startAngle = value;
				Refresh();
			}
		}

		private double endAngle = 125.0F;
		[Category( "Meter" )]
		[Description( "End Angle" )]
		[DefaultValue( typeof(double), "125.0" )]
		public double EndAngle
		{
			get {return endAngle;}
			set 
			{
				endAngle = value;
				Refresh();
			}
		}

		private Bitmap originalBitmap = null;
		private System.Diagnostics.PerformanceCounter performanceCounter1;
	
		[Category( "Meter" )]
		[Description( "Background Bitmap" )]
		[DefaultValue( typeof(Bitmap), "" )]
		public Bitmap OriginalBitmap
		{
			get {return originalBitmap;}
			set 
			{
				originalBitmap = value;
				LoadImage();
			}
		}

		#endregion

 
		private const double pai = Math.PI;
		private SolidBrush handBrush;

		/// <summary>
		/// 図形関係
		/// </summary>
		private int r;     // Meter Radius

		private GraphicsPath handPath;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

		// Current Meter Value
		static float meterRead = 0.0F;
		/// <summary>
		/// Draw Hand
		/// </summary>
		/// <param name="g"></param>
		/// <param name="currentSpeed"></param>
		public void drawHand(Graphics g, float currentSpeed)
		{			
			double totalAngle = endAngle - startAngle;

			// Delta = Current Value - Current Meter Read
			// Get the current meter closer to the current value with delta/10 step.
			float delta = currentSpeed - meterRead;
			float step = delta/10F;
			meterRead += step;
			GraphicsState gs = g.Save();
			// Rotate and draw the meter hand.
			g.RotateTransform((float)(startAngle + meterRead * totalAngle/100F));
			if (handPath != null)
				g.FillPath(handBrush, handPath);
			g.Restore(gs);
		}
		
		/// <summary>
		/// Re calculate size related values
		/// </summary>
		public void Recalculate(int width, int height)
		{			
			// Radius
			r = (int)(0.6 * Math.Min(width/2, height/2));

			// Graphics Path of the meter hand
			int handWidth = (int)(Math.Max(1.0F, r*0.02F));
			handPath = new GraphicsPath();
			Point [] p = new Point[5];
			p[0] = new Point((int)(-r*0.1F), 0);
			p[1] = new Point(0, -handWidth);
			p[2] = new Point(r, 0);
			p[3] = new Point(0, handWidth);
			p[4] = new Point((int)(-r*0.1F), 0);
			handPath.AddLines(p);
			handPath.CloseAllFigures();
		}

		public void LoadImage()
		{
			if (OriginalBitmap == null)
				return;
			if (Width <= 0 || Height <= 0)
				return;
			if (BackgroundImage != null)
				BackgroundImage.Dispose();
			BackgroundImage = new Bitmap(OriginalBitmap, Width, Height);
		}
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			float val = performanceCounter1.NextValue();
			Graphics g = e.Graphics;
			g.TranslateTransform(Width/2, Height/2);
			g.RotateTransform(-90.0F);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			drawHand(g, val);
		}
		
		private void CPUMeterPanel_Resize(object sender, System.EventArgs e)
		{	
			Recalculate(Width, Height);
			LoadImage();
		}
	}
}