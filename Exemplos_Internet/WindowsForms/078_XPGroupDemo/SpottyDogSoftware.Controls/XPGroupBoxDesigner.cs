using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Summary description for XPGroupBoxDesigner.
	/// </summary>
	//public class XPGroupBoxDesigner : System.Windows.Forms.Design.ParentControlDesigner
	public class XPGroupBoxDesigner : System.Windows.Forms.Design.ControlDesigner
		{
		Pen	borderPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
		XPGroupBox control;

		public XPGroupBoxDesigner()
		{
			borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		}

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize (component);
			control = (XPGroupBox)this.Control;
		}

		protected override void OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
		{
			base.OnPaintAdornments (pe);
			pe.Graphics.DrawRectangle(borderPen, 0, 0, control.Width -2, control.Height -2);
		}

	}
}
