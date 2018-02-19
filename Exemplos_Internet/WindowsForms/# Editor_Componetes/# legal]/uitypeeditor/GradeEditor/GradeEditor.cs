using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace GradeTypeSample
{
	public class GradeEditor : UITypeEditor
	{
		public override bool GetPaintValueSupported(ITypeDescriptorContext context) 
		{
			return true;
		}

		public override void PaintValue(PaintValueEventArgs pe) 
		{
			string bmpName = null;
			Grade g = (Grade)pe.Value;
			if (g.Value > 80) 
			{
				bmpName = "best.bmp";
			}
			else if (g.Value > 60) 
			{
				bmpName = "ok.bmp";
			}
			else 
			{
				bmpName = "bad.bmp";
			}

			Bitmap b = new Bitmap(typeof(GradeEditor), bmpName);

			pe.Graphics.DrawImage(b, pe.Bounds);

			b.Dispose();			
		}
	}
}
