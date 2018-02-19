using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace windowregion.cs
{
	public enum SizeAction
	{
		None,
		SizeTop,
		SizeLeft,
		SizeRight,
		SizeBottom,
		SizeTopLeft,
		SizeBottomLeft,
		SizeTopRight,
		SizeBottomRight,
		Move
	}

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label1;

		Bitmap closeBitmap; //holds the bitmap for the close button
		GraphicsPath OuterPath; //the border path
		Region borderRegion; //the border region
		bool opening=false; //when true, the close button is sliding out
		Timer closer; //a two second delay before hiding the close button
		bool sizing=false; //when true, the window is being sized
		bool moving=false; //When true the window is being moved
		int closerPos=0; //the position 0-45 of the closer tab
		bool inborder=false; //when true, the mouse is in the border
		SizeAction sizeAction=SizeAction.None; // the type of size operation to undertake
		Point currentPos=new Point(0,0); //the mouse position
		Point cursorOffset=new Point(0,0); //the offset of the mouse from the origin. 


		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.MinimumSize=new Size(90,90);

			this.closer=new Timer();
			this.closer.Tick+=new EventHandler(CloserHandler);

			this.SetStyle(ControlStyles.ResizeRedraw,true);
			this.SetStyle(ControlStyles.DoubleBuffer,true);
			this.SetStyle(ControlStyles.UserPaint,true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);

			closeBitmap=(Bitmap)Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("windowregion.cs.Bitmap1.bmp"));
			closeBitmap.MakeTransparent(Color.Magenta);

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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(56, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(480, 160);
			this.label1.TabIndex = 0;
			this.label1.Text = "This application has a completely non-standard UI. Try dragging all the corners t" +
				"o resize it, look out for the pop-up close button!";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.ClientSize = new System.Drawing.Size(600, 300);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
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


		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(OuterPath==null)
				return;
			Pen p=new Pen(Color.DarkGray,4);
			p.Alignment=PenAlignment.Inset;
			e.Graphics.DrawPath(p,OuterPath);

			p.Color=Color.LightGray;
			p.Width=2;

			e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;
			e.Graphics.DrawPath(p,OuterPath);

			if(closerPos!=0)
			{
				GraphicsPath gp=new GraphicsPath();
				gp.AddArc(this.Size.Width-90,0,90,90,270,90);
				gp.AddLine(this.Size.Width,45,this.Size.Width,0);
				gp.CloseFigure();

				e.Graphics.Clip=new Region(gp);

				e.Graphics.DrawImage(closeBitmap,new Rectangle(this.Size.Width-45,45-closerPos,closerPos,closerPos),0,0,closeBitmap.Width,closeBitmap.Height,GraphicsUnit.Pixel);
			}

			p.Dispose();
		}

		private void CloserHandler(object sender, EventArgs e)
		{
			this.closer.Enabled=false;
			this.opening=false;
			this.timer1.Interval=50;
			this.timer1.Enabled=true;
		}
 
		private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Graphics g=CreateGraphics();
			
			inborder = borderRegion.IsVisible(new Point(e.X,e.Y),g);

			g.Dispose();

			if(!moving & !sizing & !inborder)
			{
				if(closerPos==45)
				{
					opening=false;
					this.closer.Interval=2000;
					this.closer.Enabled=true;
				}

				inborder=false;
				Cursor=Cursors.Arrow;

				//Do non-border processing here...
				return;
			}


			if(!sizing)
			{
				
				if((e.Y<45 & e.X<45))
				{
					this.Cursor=Cursors.SizeNWSE;
					sizeAction=SizeAction.SizeTopLeft;
				}
				else if(e.Y>this.Height-45 & e.X>this.Width-45)
				{
					this.Cursor=Cursors.SizeNWSE;
					sizeAction=SizeAction.SizeBottomRight;
				}
				else if (e.Y<45 & e.X>this.Width-45)
				{
					if(closerPos!=45)
					{
						opening=true;
						this.timer1.Interval=10;
						this.timer1.Enabled=true;
					}
					this.Cursor=Cursors.SizeNESW;
					sizeAction=SizeAction.SizeTopRight;
				}
				else if(e.Y>this.Height-45 & e.X<45)
				{
					this.Cursor=Cursors.SizeNESW;
					sizeAction=SizeAction.SizeBottomLeft;
				}
				else if(e.X<45)
				{
					this.Cursor=Cursors.SizeWE;
					sizeAction=SizeAction.SizeLeft;
				}
				else if(e.X>this.Width-45)
				{
					this.Cursor=Cursors.SizeWE;
					sizeAction=SizeAction.SizeRight;
				}
				else if(e.Y>this.Height-45)
				{
					this.Cursor=Cursors.SizeNS;
					sizeAction=SizeAction.SizeBottom;
				}
				else if (e.Y<45)
				{
					this.Cursor=Cursors.SizeNS;
					sizeAction=SizeAction.SizeTop;
				}
			}
			
			if (sizing)
			{
				Point t,s,oldLoc;
				switch(sizeAction)
				{
					case SizeAction.SizeTop:
						this.Location=new Point(this.Location.X,this.Location.Y+e.Y);
						this.Size=new Size(this.Size.Width,this.Size.Height-e.Y);
						break;
					case SizeAction.SizeLeft:
						this.Location=new Point(this.Location.X+e.X,this.Location.Y);
						this.Size=new Size(this.Size.Width-e.X ,this.Size.Height);
						break;
					case SizeAction.SizeRight:
						this.Size=new Size(e.X,this.Size.Height);
						break;
					case SizeAction.SizeBottom:
						this.Size=new Size(this.Size.Width,e.Y);
						break;
					case SizeAction.SizeTopLeft:
						t=new Point(e.X-cursorOffset.X,e.Y-cursorOffset.Y);
						Location=this.PointToScreen(t);
						this.Size=new Size(this.Size.Width-t.X ,this.Size.Height-t.Y);
						break;
					case SizeAction.SizeBottomLeft:
						oldLoc=Location;
						t=new Point(e.X-cursorOffset.X,e.Y-cursorOffset.Y);
						s=this.PointToScreen(t);
						this.Location=new Point(s.X,Location.Y);
						this.Size=new Size(this.Size.Width-(Location.X-oldLoc.X) ,e.Y+(this.Height-cursorOffset.Y) );
						this.cursorOffset.Y=e.Y;
						break;
					case SizeAction.SizeTopRight:
						oldLoc=Location;
						t=new Point(e.X-cursorOffset.X,e.Y-cursorOffset.Y);
						s=this.PointToScreen(t);
						this.Location=new Point(Location.X,s.Y);
						this.Size=new Size(e.X+(this.Size.Width-cursorOffset.X),this.Size.Height-(Location.Y-oldLoc.Y));
						this.cursorOffset=new Point(e.X,cursorOffset.Y);
						break;
					case SizeAction.SizeBottomRight:
						this.Size=new Size(e.X+(this.Size.Width-cursorOffset.X),e.Y+(this.Size.Height-this.cursorOffset.Y));
						cursorOffset=new Point(e.X,e.Y);
						break;
				}
			}

			if (moving)
			{
				this.Cursor=Cursors.SizeAll;
				this.Location=PointToScreen(new Point(e.X-cursorOffset.X,e.Y-cursorOffset.Y));
			}
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			cursorOffset=new Point(e.X,e.Y);
			currentPos=new Point(e.X,e.Y);
			if(inborder)
			{
				sizing=true;
				this.Capture=true;
			}
			else
			{
				moving=true;
				this.Cursor=Cursors.SizeAll;
			}
		}

		private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{

			if(!sizing & closerPos==45 & e.X>this.Size.Width-23 & e.Y<23)
					Application.Exit();

			if(sizing | moving)
			{
				sizing=moving=false;
				this.Capture=false;
			}
			//do non-border mouseup actions here		
		}

		private void CreateWindowRegion()
		{
			GraphicsPath gp=new GraphicsPath(FillMode.Winding);
			gp.AddArc(0,0,90,90,180,90);
			gp.AddLine(45,0,this.Size.Width-45,0);
			gp.AddArc(this.Size.Width-90,0,90,90,270,90);
			gp.AddLine(this.Size.Width,45,this.Size.Width,this.Size.Height-45);
			gp.AddArc(this.Size.Width-90,this.Size.Height-90,90,90,0,90);
			gp.AddLine(this.Size.Width-45,this.Size.Height,45,this.Size.Height);
			gp.AddArc(0,this.Size.Height-90,90,90,90,90);
			gp.AddLine(0,this.Size.Height-45,0,45);
			gp.CloseFigure();

			OuterPath=gp;

			GraphicsPath border=(GraphicsPath)gp.Clone();

			border.Widen(new Pen(Color.Black,4));

			borderRegion=new Region(border);

			System.Drawing.Region r = new Region(gp);
			
			r.Union(new Rectangle(this.Width-(45),45-closerPos,closerPos,closerPos));

			this.Region=r;		
		}

		private void Form1_SizeChanged(object sender, System.EventArgs e)
		{
			CreateWindowRegion();
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if(opening & closerPos!=45)
				closerPos++;
			if(!opening & closerPos!=0)
				closerPos--;

			if(closerPos==0 | closerPos==45)
				this.timer1.Enabled=false;

			CreateWindowRegion();

			Invalidate();

		}
	}
}
