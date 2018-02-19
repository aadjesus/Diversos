using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace tabtest
{
	/// <summary>
	/// This is a sample of a non-rectangular button class.  Setting the
	/// vertices of the shape is done via a custom PropertyTab.
	/// </summary>
	/// 
	[PropertyTab(typeof(PointsPropertyTab), PropertyTabScope.Component),
	 DefaultEvent("Click"),
	 DefaultProperty("BackColor")]
	public class FunkyButton : System.Windows.Forms.UserControl, ISupportInitialize
	{

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private PointsCollection points;

		private bool inInit = false;  // are we initializing?
		private bool drawDown = false; // are we in the "down" button state?

		private const int BumpAmount = 2; // the amount we vary coordinates to determine if an edge is light or dark.

		public FunkyButton()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Our collection of Points that will be the vertices of our control.
		/// Notice this is marked Browsable(false) so it doesn't show
		/// up in the normal property view.
		/// </summary>
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PointsCollection Points 
		{
			get 
			{
				if (points == null) 
				{
					points = new PointsCollection(this, new Point[4]{new Point(0,0), new Point(Width, 0), new Point(Width, Height), new Point(0, Height)});	
                    //points = new PointsCollection();
				}

				return points;
			}
		}
		
		/// <summary>
		/// This function is used to determine if an edge is on the "light" side
		/// of the control -- that is if it should be drawn with a highlight 
		/// or a shadow.  
		/// 
		/// Basically the algorithm is to find the center of the edge, and then vary the point
		/// a few pixels in one direction or the other.  Then, based on if that Point is contained
		/// in the Control's region or not, we can determine whether it should be drawn light or dark.
		/// 
		/// For example, if we're on a positive slope (edge going from lower left to upper right), it should be
		/// drawn light if the control is to the right of the line, and dark if it is to the left.
		/// </summary>
		/// <param name="g">The Graphics object in question</param>
		/// <param name="p1">The start point of the side</param>
		/// <param name="p2">The end point of the side</param>
		/// <returns>true if this edge should be lighted.</returns>
		private bool LightSide(Graphics g, Point p1, Point p2) 
		{

			int xDelta = p2.X - p1.X;
			// flipped because y grows down
			int yDelta = p1.Y - p2.Y;

			// this is how much we will bump our location
			// to 
			int xBump = 0;
			int yBump = 0;

			// we create a point at the center of the edge and will vary this to decide
			// which side is the control and which is not.
			//
			Point checkPoint = new Point(p1.X + (xDelta / 2), p1.Y - (yDelta / 2));
			
			if (xDelta == 0) 
			{
				// we have a vertical line, bump right
				//
				xBump = BumpAmount;
			}
			else if (yDelta == 0) 
			{
				// we have a horizontal line, bump down.
				//
				yBump = BumpAmount;				
			}
			else 
			{
				//  use the tangent to get the angle of the absolute value in degrees.
				//
				double angle = Math.Atan(Math.Abs((double)yDelta / (double)xDelta)) * (180/Math.PI);

				// now that we have the angle (in degrees), we can decide which direction to bump.
				// for flatter edges (<45 degree angle), we bump vertically.  For more sloped edges
				// we bump horizontally.  This way we don't accidentally pick another point on the line itself.
				//
				if (angle >= 45) 
				{
					xBump = BumpAmount;
				}
				else 
				{
					yBump = BumpAmount;
				}			
			}
			
			// now we've decided which direction to move.  Test to see if it's on the line or not by
			// asking the graphics object if this point is visible.
			//
			checkPoint.X += xBump;
			checkPoint.Y += yBump;
			return g.IsVisible(checkPoint); 
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// FunkyButton
			// 
			this.BackColor = System.Drawing.Color.Blue;
			this.Name = "FunkyButton";			
		}
		#endregion

		/// <summary>
		/// Capture mouse down to set our "down state"
		/// </summary>
		/// <param name="me"></param>
		protected override void OnMouseDown(MouseEventArgs me) 
		{
			drawDown = true;
			base.OnMouseDown(me);
			Invalidate();
		}

		/// <summary>
		/// Capture mouse up to clear our "down state"
		/// </summary>
		/// <param name="me"></param>
		protected override void OnMouseUp(MouseEventArgs me) 
		{
			drawDown = false;
			base.OnMouseUp(me);
			Invalidate();
		}
		
		/// <summary>
		/// Do our custom drawing logic.  We walk each side
		/// and paint it light or dark based on its angle and 
		/// edge.
		/// </summary>
		/// <param name="pe"></param>
		protected override void OnPaint(PaintEventArgs pe) 
		{
			// here, we move the origin of the Graphics
			// in the down state so any underlying drawing is
			// done moved over a bit.
			//
			try 
			{
				if (drawDown) 
				{
					pe.Graphics.ResetTransform();
					pe.Graphics.TranslateTransform(2,2);            
				}
				base.OnPaint(pe);
			}
			finally 
			{
				// clear our transform.
				//
				if (drawDown) 
				{
					pe.Graphics.ResetTransform();			
				}
			}


			// save off the old clipping region and push in ours
			// so we can use the Graphics.IsVisible call to test
			// visibility.
			//
			Region clipRegion = pe.Graphics.Clip;
			Region r = this.Region;
			if (r != null) 
			{
				pe.Graphics.Clip = r;
			}

			Pen lightPen = new Pen(SystemColors.ControlLightLight, 3);
			Pen darkPen = new Pen(SystemColors.ControlDark, 3);				

			// now walk the edges and draw them.
			//
			for (int i = 0; i < this.Points.Count; i++) 
			{
				Point p1 = this.Points[i];
				Point p2;

				// wrap to the first vertex if
				// we're on the last one.
				//
				if (i + 1 == Points.Count) 
				{
					p2 = this.Points[0];
				}
				else
				{
					p2 = this.Points[i + 1];
				}

				bool drawLight = LightSide(pe.Graphics, p1, p2);

				// reverse the drawing if we're "down"
				//
				if (this.drawDown) 
				{
					drawLight = !drawLight;
				}

				// finally draw the edge.
				//
				pe.Graphics.DrawLine(drawLight ? lightPen : darkPen, p1, p2);				
			}	
			lightPen.Dispose();
			darkPen.Dispose();

			// and replace the clip region...
			pe.Graphics.Clip = clipRegion;					
		}

		protected override void OnPaintBackground(PaintEventArgs pe) 
		{
			// here, we move the origin of the Graphics
			// in the down state so any underlying drawing is
			// done moved over a bit.
			//
			try 
			{
				if (drawDown) 
				{
					pe.Graphics.ResetTransform();
					pe.Graphics.TranslateTransform(2,2);            
				}
				base.OnPaintBackground(pe);
			}
			finally 
			{
				if (drawDown) 
				{
					pe.Graphics.ResetTransform();			
				}
			}
		}

		/// <summary>
		/// We trap the setbounds core call so we can automatically scale all our vertices
		/// in response to a resizing of the control.
		/// </summary>
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified boundsSpecified) 
		{
			double wRatio = 1.0;
			double hRatio = 1.0;

			// pick up the ratios of our sizing.
			if (!inInit) 
			{
				if (this.Width != 0 && width != 0) 
				{
					wRatio = (double)width / this.Width;
				}				
			}

			if (!inInit) 
			{
				if (this.Height != 0 && width != 0) 
				{
					hRatio = (double)height / this.Height;
				}
			}			
			base.SetBoundsCore(x,y,width,height, boundsSpecified);

			// update the points array and update our region.
			//
			if (wRatio != 1.0 || hRatio != 1.0) 
			{
				this.Points.Scale(wRatio, hRatio);
				this.UpdateRegion();
			}
		}

		/// <summary>
		/// Updates the Region shape for this control based on
		/// the points we've selected.
		/// </summary>
		internal void UpdateRegion() 
		{
			if (points.Count > 2) 
			{
				GraphicsPath gp = new GraphicsPath();
				Point[] regionPoints = new Point[points.Count];
				points.CopyTo(regionPoints, 0);
				gp.AddPolygon(regionPoints);
				this.Region = new Region(gp);
				Graphics g = this.CreateGraphics();
				g.FillPath(SystemBrushes.ActiveBorder, gp);
				gp.Dispose();
			}
			else 
			{
				this.Region = null;
			}
			Invalidate();
		}

		/// <summary>
		/// Called when the Control is being created in InitializeComponent
		/// so we can ignore resizes, etc.
		/// </summary>
		public void BeginInit() 
		{
			Points.ClearOnAdd = true;
			inInit = true;
		}

		/// <summary>
		/// Called when the InitializeComponent code has been completed for this control.
		/// </summary>
		public void EndInit() 
		{
			Points.ClearOnAdd = false;
			inInit = false;
		}
		
	}	
}
