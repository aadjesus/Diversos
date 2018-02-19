using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// An XP style Activity Bar.
	/// </summary>
	public class ActivityBar : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ColorBlend cb;
		private Timer t;
		private int tickCount;

		private Color _barColor = Color.Orange;
		/// <summary>
		/// Determines the main color for the activity bar
		/// </summary>
		[Description("Determines the main color for the activity bar."), 
		Category("Appearance")]
		public Color BarColor
		{
			get { return _barColor;}
			set 
			{
				_barColor = value; 
				ColorPropertyChanged();
			}
		}

		private Color _borderColor= SystemColors.ActiveBorder;
		/// <summary>
		/// Determines the color for the activity bar border
		/// </summary>
		[Description("Determines the color for the activity bar border."), 
		Category("Appearance")]
		public Color BorderColor
		{
			get { return _borderColor;}
			set 
			{
				_borderColor = value; 
				ColorPropertyChanged();
			}
		}

		private Color _highlightColor = Color.White;
		/// <summary>
		/// Determines the highlight color for the activity bar
		/// </summary>
		[Description("Determines the highlight color for the activity bar."), 
		Category("Appearance")]
		public Color HighlightColor
		{
			get { return _highlightColor;}
			set 
			{ 
				_highlightColor = value; 
				ColorPropertyChanged();
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ActivityBar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// set double buffer styles
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.ContainerControl, true);

			this.BackColor = Color.Transparent;				
			UpdateStyles();
			
			BuildColorBlend();

			t = new Timer();
			t.Interval = 10;
			t.Tick += new EventHandler(t_Tick);
			this.EnabledChanged += new EventHandler(ActivityBar_EnabledChanged);
		}

		private void BuildColorBlend()
		{
			cb = new ColorBlend(7);
			cb.Colors  = new Color[] {
										 _barColor, 
										 _barColor,
										 _barColor,
										 _highlightColor, 
										 _highlightColor, 
										 _barColor, 
										 _barColor};

			cb.Positions = new float[] {
										   0.0f, 
										   0.01f,
										   0.2f, 
										   0.4f, 
										   0.6f, 
										   0.8f, 
										   1.0f};
		}

		private void ColorPropertyChanged()
		{
			BuildColorBlend();
			this.Refresh();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ActivityBar
			// 
			this.Name = "ActivityBar";
			this.Size = new System.Drawing.Size(440, 8);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics ; 
			LinearGradientBrush lBrush = new LinearGradientBrush(this.ClientRectangle, Color.White,  Color.Orange, 
				LinearGradientMode.Horizontal); 
			lBrush.InterpolationColors = cb;
			g.FillRectangle(lBrush, 1,1, this.Width-2, this.Height-2); 	
			Brush b = new SolidBrush(_borderColor);
			Pen p = new Pen(b, 1);
			g.DrawRectangle(p, 0,0, this.Width-1, this.Height-1);
			p.Dispose();
			b.Dispose();
			lBrush.Dispose();
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			// Make sure the background isn't erased, to reduce flicker

			//base.OnPaintBackground (pevent);
		}

		/// <summary>
		/// Performs the color rotation in the activity bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void t_Tick(object sender, EventArgs e)
		{
			// Each color segment gets "scrolled" 20 positions to the right
			// then the colors get moved to the right in the colorblend array
			// and the deltas reset
			if (++tickCount >= 20)
			{
				tickCount = 0;
				Color last = cb.Colors[6];
				for (int i = 6; i>0; i--)
				{
					cb.Colors[i] = cb.Colors[i-1];
				}
				cb.Colors[0] = last;
			}

			// get color advance delta
			float f = tickCount / 100.0f;

			// advance colors
			cb.Positions[1] = 0.01f + f;
			cb.Positions[2] = 0.2f + f;
			cb.Positions[3] = 0.4f + f;
			cb.Positions[4] = 0.6f + f;
			cb.Positions[5] = 0.8f + f;
			//redraw controls
			this.Refresh();   
		}

		private void ActivityBar_EnabledChanged(object sender, EventArgs e)
		{
			t.Enabled = this.Enabled;
		}
	}
}
