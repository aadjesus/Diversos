using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace tabtest
{
	/// <summary>
	/// The control that will be dropped down when the down arrow on a vertex property is selected.
	/// </summary>
	public class pointEditorControlx : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;

		private const int DotSize = 5;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public Point SelectedPoint = Point.Empty;
		private IWindowsFormsEditorService edSvc;
		private Point[]  targetPoints;
		private System.Windows.Forms.Panel panel1;
		private FunkyButton  target;
		private float xRatio, yRatio;
		private bool dragging;
		private int  targetIndex = -1;

		public pointEditorControlx()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();			
		}

		public IWindowsFormsEditorService EditorService 
		{
			get  
			{
				return edSvc;
			}
			set 
			{
				this.edSvc = value;
			}
		}

		public FunkyButton Target 
		{
			get 
			{
				return target;
			}
			set 
			{
				if (target == value) return;

				// copy and scale the points from the specified
				// button into our private array.
				//
				this.target = value;

				if (target == null) return;

				dragging = false;
				this.targetPoints = new Point[target.Points.Count];
				target.Points.CopyTo(targetPoints, 0);

				// compute scaling factors.
				yRatio = (float)panel1.Height / target.Height;
				xRatio = (float)panel1.Width / target.Width;

				// update array.
				for (int i = 0; i < targetPoints.Length; i++) 
				{
					Point p = targetPoints[i];
					if (p.Equals(SelectedPoint)) 
					{
						targetIndex = i;
					}

					p.X = (int)(p.X * xRatio);
					p.Y = (int)(p.Y * yRatio);
					targetPoints[i] = p;
				}
			}
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "x: 0, y: 0";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 53);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.PointEditorUI_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PointEditorUI_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PointEditorUI_MouseUp);
            // 
            // pointEditorControlx
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "pointEditorControlx";
            this.Size = new System.Drawing.Size(150, 96);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Paint the representation based on our point array.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PointEditorUI_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// draw the shape of the control
			e.Graphics.FillPolygon(SystemBrushes.ControlText, targetPoints);

			// draw the location of the modified vertex.
			Point p = dragging && this.targetIndex != -1 ? targetPoints[targetIndex] : PointToEditor(SelectedPoint);
			e.Graphics.FillEllipse(Brushes.Red, p.X - (DotSize / 2), p.Y - (DotSize /2 ), DotSize, DotSize);
		}

		/// <summary>
		/// When the mouse moves, update the X and Y coordinates of the mouse cursor
		/// </summary>
		private void PointEditorUI_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point movePt = new Point(e.X, e.Y);
			// scale the location back to the coordinates of
			// the actual control.
			//
			Point p = PointToTarget(movePt);
			label1.Text = "x: " + p.X + ", y:" + p.Y;

			// if the left button is down, update the polygon.
			//
			if (e.Button == MouseButtons.Left && this.targetIndex != -1) 
			{
				targetPoints[targetIndex] = movePt; 
				panel1.Invalidate();
				panel1.Update();
			}
		}

		/// <summary>
		/// When the button is released, we've finished.  Update our selected value
		/// and ask the IWindowsFormsEditorService to close the dropdown.
		/// </summary>
		private void PointEditorUI_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dragging = false;
			this.SelectedPoint = PointToTarget(new Point(e.X, e.Y));
			edSvc.CloseDropDown();	

		}

		/// <summary>
		/// Scale a point from the editor coordinates to the actual control coorinates.
		/// </summary>
		private Point PointToTarget(Point p) 
		{
			return new Point((int)(p.X / xRatio), (int)(p.Y / yRatio));
		}

		/// <summary>
		/// Scale a point from control coordinates to editor coordinates.
		/// </summary>
		private Point PointToEditor(Point p) 
		{
			return new Point((int)(p.X * xRatio), (int)(p.Y * yRatio));
		}

		/// <summary>
		/// Set our dragging state if the left button is down.
		/// </summary>
		private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dragging = e.Button == MouseButtons.Left;
		}
	}
}
