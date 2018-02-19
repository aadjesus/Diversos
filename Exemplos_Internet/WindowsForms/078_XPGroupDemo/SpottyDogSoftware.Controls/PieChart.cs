using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Delegate invoked when a Segment is clicked
	/// </summary>
	public delegate void SegmentClickEventHandler(object sender, SegmentClickEventArgs e);

	/// <summary>
	/// Pie Chart Control
	/// </summary>
	public class PieChart : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;

		/// <summary>
		/// Raised when a Segment is clicked on the control
		/// </summary>
		[DescriptionAttribute("Event fired when a Segment is clicked on the control.")]
		public event SegmentClickEventHandler SegmentClick;

		private PieSegmentCollection _segmentList = new PieSegmentCollection();

		/// <summary>
		/// Collection containing all the PieSegments for the control
		/// </summary>
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
		[CategoryAttribute("Data")]
		[Browsable(true)]
		[DescriptionAttribute("Collection containing all the PieSegments for the control.")]
		public PieSegmentCollection SegmentList
		{
			get
			{
				return _segmentList;
			}
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public PieChart()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			UpdateStyles();
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
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			// 
			// PieChart
			// 
			this.Name = "PieChart";
			this.toolTip1.SetToolTip(this, "No Data Loaded!");
			this.MouseHover += new System.EventHandler(this.PieChart_MouseHover);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PieChart_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PieChart_MouseDown);

		}
		#endregion

		/// <summary>
		/// Implementation of a PieChart paint
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
		
			Random rand = new Random();
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			GraphicsPath gp = new GraphicsPath();
			int diameter = (ClientSize.Width < ClientSize.Height) ? ClientSize.Width -4: ClientSize.Height -4;
			int cx = (ClientSize.Width  - diameter)/2;
			int cy = (ClientSize.Height - diameter)/2;
			gp.AddEllipse(cx, cy, diameter-2, diameter-2);
			Region region = new Region(gp);
			Brush brush = new SolidBrush( Color.FromArgb(3, 55, 145));
			Pen p = new Pen(brush);
			this.Region = region;
			gp.Dispose();
			float totalSize = 0.0f;
			if (_segmentList != null)
			{
				foreach( PieSegment ps in _segmentList)
				{
					totalSize += (float)ps.Size;
				}
			}
			float sizePerDegree = totalSize/360.0f;

			if (_segmentList != null)
			{
				float angle = 0f;
				foreach( PieSegment ps in _segmentList)
				{
					float sweepAngle = 0f;
					try
					{
						sweepAngle =  ((float)ps.Size)/sizePerDegree;
					}
					catch {}

					Brush b = new SolidBrush(ps.SegmentColor);

					GraphicsPath gpPieSegment = new GraphicsPath();
					gpPieSegment.AddPie(cx-2, cy-2, diameter+5, diameter+5, angle, sweepAngle);
					ps.Region = new Region(gpPieSegment);
					e.Graphics.FillPie(b, cx-2, cy-2, diameter+5, diameter+5, angle, sweepAngle);
					angle += sweepAngle;
				}
			}

			p.Dispose();
			brush.Dispose();
		}

		private void PieChart_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			foreach (PieSegment ps in  _segmentList)
			{
				if (ps.Region.IsVisible(p))
				{
					this.toolTip1.SetToolTip(this, string.Format("{0} = {1} Bytes", ps.Path, ps.Size.ToString("N0", CultureInfo.CurrentUICulture)));
					break;
				}
			}
		}

		private void PieChart_MouseHover(object sender, System.EventArgs e)
		{
			this.toolTip1.SetToolTip(this, "No data loaded!");
		}

		/// <summary>
		/// Handles the OnClick event and determines if the 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSegmentClick(SegmentClickEventArgs e)
		{
			if (SegmentClick != null) 
			{
				// Invokes the delegates. 
				SegmentClick(this, e);
			}
		}

		private void PieChart_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point p = new Point(e.X, e.Y);
				foreach (PieSegment ps in  _segmentList)
				{
					if (ps.Region.IsVisible(p))
					{
						SegmentClickEventArgs se = new SegmentClickEventArgs(ps.Path);
						OnSegmentClick(se);
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// Pie Chart Click Event Argument class
	/// </summary>
	public class SegmentClickEventArgs : EventArgs
	{
		private string _segmentName;
		/// <summary>
		/// Segment name clicked
		/// </summary>
		public string SegmentName
		{
			get { return _segmentName; }
		}

		/// <summary>
		/// Create an instance of the SegmentClickEventArgs class
		/// </summary>
		/// <param name="segmentName">Segment name clicked</param>
		public SegmentClickEventArgs(string segmentName)
		{
			_segmentName = segmentName;
		}
	}

}
