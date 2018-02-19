using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace WellFormed
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TrackBar trackBar1;

		private RoundRectControl panel=new RoundRectControl();

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			panel.Location=new Point(100,100);
			panel.Size=new Size(200,200);
			this.Controls.Add(panel);

			panel.Paint+=new PaintEventHandler(panel_Paint);

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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(340, 262);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(456, 334);
            this.Controls.Add(this.trackBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			this.panel.BorderRadius=this.trackBar1.Value;
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			//e.Graphics.Clear(Color.Blue);
		}
	}

	public class RoundRectControl : Control
	{
		private int _borderRadius=1;
  
		public int BorderRadius
		{
			get
			{
				return _borderRadius;
			}
			set
			{
				if ((_borderRadius != value) && value>0)
				{
					_borderRadius = value;
					OnBorderRadiusChanged(new EventArgs());
				}
			}
		}
  
		protected virtual void OnBorderRadiusChanged(EventArgs e)
		{
			RecreateHandle();
			Invalidate();
		}

		[DllImport("User32.dll")]
		protected static extern int ReleaseDC(IntPtr hWnd, IntPtr dc);

		[DllImport("User32.dll")]
		protected static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		protected static extern int GetWindowRect(IntPtr hWnd, ref RECT rect);
		
		public RoundRectControl()
		{
		}

		protected override void WndProc(ref Message m)
		{
			//calculate the maximum size of the non-client border area
			//3 is the minimum because the border is 3 pizels wide
			int bmax=(int)Math.Max(3,_borderRadius);
			//0.41 is a fudge factor. A square of approximately 0.82*diameter will fit neatly into a circle
			//with diameter of 1 so 0.41 is half of the diameter
			int adustedbmax=(int)Math.Max(3,0.40*bmax);
			switch(m.Msg)
			{
				case (int)WmDefs.WM_NCCALCSIZE:
					if ((int)m.WParam == 0)
					{
						//Shrink the window rect to create the client rect
						RECT rc=(RECT)Marshal.PtrToStructure(m.LParam,typeof(RECT));
						rc.left+=adustedbmax;
						rc.top+=adustedbmax ;
						rc.right-=adustedbmax ;
						rc.bottom-=adustedbmax ;
						Marshal.StructureToPtr(rc,m.LParam,false);
						m.Result=IntPtr.Zero;
					}
					break;
				case (int)WmDefs.WM_NCPAINT:
				{
					//get the window DC
					IntPtr dc=GetWindowDC(m.HWnd);
					//and the window rectangle in screen coordinates
					RECT rc=new RECT();
					GetWindowRect(m.HWnd,ref rc);
					//convert to a GDI+ rectangle and translate to client coordinates
					Rectangle r=new Rectangle(rc.left,rc.top,rc.right-rc.left,rc.bottom-rc.top);
					r=this.RectangleToClient(r);
					//because the rectangle origin is negative with respect to the client area
					//we offset the rectangle so that it fits into the WindowDC correctly
					r.Offset(adustedbmax,adustedbmax);

					//Create a rectangle that is the right size for the border. 3 pixels smalller than the window DC
					Rectangle rcBorder=r;
					rcBorder.Width-=3;
					rcBorder.Height-=3;

					//Get a graphics for the WindowDC
					Graphics g=Graphics.FromHdc(dc);
					
					//the client rectangle and the window rectangle are used to make a region 
					//that includes the border but excludes the client area to reduce potential flicker
					Rectangle r2=this.ClientRectangle;
					r2.Offset(adustedbmax,adustedbmax);
					Region rg=new Region(r);
					rg.Exclude(r2);
					//the clipping region is set to include only the non-client area
					g.SetClip(rg,CombineMode.Replace);

					g.Clear(BackColor);

					//Now a 3 pixel 3D shaded border is drawn using 
					//3 overlapping round-rectangles
					Pen p=new Pen(Color.White,1);
					g.SmoothingMode=SmoothingMode.AntiAlias;
					//#1
					RoundRect(g, p, rcBorder, bmax);

					p.Color=Color.Gray;
					rcBorder.Offset(2,2);
					//#2
					RoundRect(g, p, rcBorder, bmax);

					p.Color=Color.Black;
					rcBorder.Offset(-1,-1);
					//#3
					RoundRect(g, p, rcBorder, bmax);

					//clean up.
					p.Dispose();
					g.Dispose();
					ReleaseDC(m.HWnd,dc);

					//signal success
					m.Result=IntPtr.Zero;
				}
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		//The good old roundrect routine
		private void RoundRect(Graphics g, Pen p, Rectangle r, int radius)
		{
			int r2=radius*2;
			g.DrawLine(p, r.X + radius, r.Y, r.X + r.Width - radius, r.Y); //top

			g.DrawArc(p, r.X + r.Width - r2, r.Y, radius*2, radius*2, 270, 90); //top right

			g.DrawLine(p, r.X + r.Width, r.Y + radius, r.X + r.Width, r.Y + r.Height - radius); //right

			g.DrawArc(p, r.X + r.Width - r2, r.Y + r.Height - r2, radius*2, radius*2,0,90); //bottom right

			g.DrawLine(p, r.X + r.Width - radius, r.Y + r.Height, r.X + radius, r.Y + r.Height); //bottom

			g.DrawArc(p, r.X, r.Y + r.Height - r2, r2, r2, 90, 90);

			g.DrawLine(p, r.X, r.Y + r.Height - radius, r.X, r.Y + radius);

			g.DrawArc(p, r.X, r.Y, r2, r2, 180, 90);
		}

		//The Win32 RECT structure rebuilt for interop
		[StructLayout(LayoutKind.Sequential)]
			public struct RECT
		{
			public Int32 left;
			public Int32 top;
			public Int32 right;
			public Int32 bottom;

			public override string ToString()
			{
				return string.Format("({0},{1},{2},{3})  ",left,top,right,bottom);
			}

		}


		//For the sakeof interest, the NCCREATESIZEPARAMS rebuilt for interop.
		//Can be used when the wParam is true during an NCCALCSIZE message. 
		//in practice this is never seen with simple controls.
		[StructLayout(LayoutKind.Sequential)]
			public struct NCCREATESIZEPARAMS
		{
			public RECT rgrc0, rgrc1, rgrc2;
			public IntPtr wp; 
		}
	}
}
