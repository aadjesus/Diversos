using System;
using System.Windows.Forms;
using System.Drawing;
using CustomControls.Enumerations;

namespace CustomControls.HelperClasses
{
	public class EditControl:TextBox
	{
		
		public new  BorderStyle BorderStyle
		{
			get{return BorderStyle.FixedSingle;}
		}


		public EditControl()
		{
			base.BorderStyle=BorderStyle.FixedSingle;
		}



		protected override  void WndProc(ref Message m)
		{			
			base.WndProc(ref m);		
			if(m.Msg==(int)Msgs.WM_PAINT){PaintBorder();}			
		}

		private void PaintBorder()
		{
			using (Graphics g=this.CreateGraphics())
			{
				Color borderColor =this.BackColor;
				
				using (Pen pen= new Pen(borderColor))
				{
					g.DrawRectangle(pen,new Rectangle(0,0,this.Width-1, this.Height-1));
				}
				
			}
		}

	}
}
