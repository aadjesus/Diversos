using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

namespace tabtest
{
	/// <summary>
	/// Our implementation of UITypeEditor that allows us to modify
	/// the editor for Point on each of our Control vertices.
	/// </summary>
	public class PointUIEditor : System.Drawing.Design.UITypeEditor
	{

		FunkyButton target;
		pointEditorControlx ui;
		
		public PointUIEditor(FunkyButton target)
		{
			// we use this to draw the paintvalue area.
			this.target = target;
		}

		/// <summary>
		/// This is called when the user clicks the down-arrow
		/// button in the property browser.  Here, we create our control
		/// and ask the IWindowsFormsEditorService to display it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sp"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider sp, object value) 
		{
			// get the editor service.
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)sp.GetService(typeof(IWindowsFormsEditorService));

			if (edSvc == null) 
			{
				// uh oh.
				return value;
			}

			// create our UI
			if (ui == null) 
			{
				ui = new pointEditorControlx();
			}
			
			// initialize the ui with the settings for this vertex
			ui.SelectedPoint = (Point)value;
			ui.EditorService = edSvc;
			ui.Target = (FunkyButton)context.Instance;
			
			// instruct the editor service to display the control as a dropdown.
			edSvc.DropDownControl(ui);
		
			// return the updated value;
			return ui.SelectedPoint;
		}

		/// <summary>
		/// Specify that this editor is the drop down style (instead of the [...] popup style)
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
		}

		/// <summary>
		/// Returns true to specify this editor will draw a value.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPaintValueSupported(ITypeDescriptorContext context) 
		{
			return true;
		}

		/// <summary>
		/// Paint a small representaion of the control and which vertex this value refers to
		/// </summary>
		/// <param name="pe"></param>
		public override void PaintValue(PaintValueEventArgs pe) 
		{
			// get the points out of the target control.
			//
			Point[] targetPoints = new Point[target.Points.Count];
			target.Points.CopyTo(targetPoints, 0);

			// deterimine our scaling factors.
			//
			float yRatio = (float)pe.Bounds.Height / target.Height;
			float xRatio = (float)pe.Bounds.Width / target.Width;

			// run through the points and update the scaling.
			//
			for (int i = 0; i < targetPoints.Length; i++) 
			{
				Point p = targetPoints[i];
				p.X = (int)(p.X * xRatio);
				p.Y = (int)(p.Y * yRatio);
				targetPoints[i] = p;
			}

			// create a brush and a polygon based on the points
			// that we've now scaled down.
			//
			Brush b = new SolidBrush(target.BackColor);
			pe.Graphics.FillPolygon(b, targetPoints);
			Point thisPt = (Point)pe.Value;
			thisPt.X =(int)(thisPt.X *  xRatio);
			thisPt.Y = (int)(thisPt.Y * yRatio);
			// draw a dot representing which vertex we are.
			//
			Brush dotBrush = Brushes.Red;
			// if the back color is red, make the brush green
			// 
			if (target.BackColor == Color.Red) 
			{
				dotBrush = Brushes.Green;
			}
			pe.Graphics.FillEllipse(dotBrush, thisPt.X, thisPt.Y, 4, 4);

			// cleanup the brush we created.
			//
			b.Dispose();
		}


	}
}
