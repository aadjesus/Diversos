using System;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.HelperClasses;
namespace CustomControls.BaseClasses
{

	public class DropdownFormBase:Form
	{
		public DropdownFormBase():base()
		{
			
			
		}
		protected override Size DefaultSize
		{
			get{return new Size(20,20);}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground (pevent);

			pevent.Graphics.DrawRectangle(new Pen(CustomControls.BaseClasses.AppColors.ToolbarColorDark),0,0,Width-1,Height-1);
		}



	}
}
