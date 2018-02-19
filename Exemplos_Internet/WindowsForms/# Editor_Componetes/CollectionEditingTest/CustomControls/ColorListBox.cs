using System;
using CustomControls.BaseClasses;
using CustomControls.HelperClasses;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CustomControls.HelperClasses
{

	public class ColorListBox:ListBox
	{

		private int LastSelIndex =-1;

		protected override Size DefaultSize
		{
			get{return  new Size(160,160);}
		}

		public ColorListBox()
		{
			this.DrawMode=System.Windows.Forms.DrawMode.OwnerDrawFixed;
			if (!this.DesignMode && this.Items.Count==0){this.Items.AddRange(System.Enum.GetNames(typeof(System.Drawing.KnownColor)));}
			
		}


		protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
		{

			e.Graphics.FillRectangle(new SolidBrush(this.BackColor),e.Bounds);
			
			Rectangle ColorRect= new Rectangle(e.Bounds.X+2,e.Bounds.Y+2,22,e.Bounds.Height-5);
			Color color = Color.FromName(((string)this.Items[e.Index]));
			Color ForeColor=this.ForeColor;
			
			if((e.State & System.Windows.Forms.DrawItemState.Selected)==System.Windows.Forms.DrawItemState.Selected)
			{
				if (this.LastSelIndex>-1)
				{
					System.Windows.Forms.DrawItemEventArgs E =new  System.Windows.Forms.DrawItemEventArgs(e.Graphics,e.Font,e.Bounds,this.LastSelIndex,System.Windows.Forms.DrawItemState.NoFocusRect);
					base.OnDrawItem(E);	

				}
				
				e.Graphics.FillRectangle(new SolidBrush(Color.Navy),e.Bounds);
				ForeColor=Color.White;
				this.LastSelIndex=e.Index;
			}
			e.Graphics.FillRectangle(new SolidBrush(color),ColorRect);
			e.Graphics.DrawRectangle(new Pen(Color.Black),ColorRect);
			e.Graphics.DrawString((string)this.Items[e.Index],Font,new SolidBrush(ForeColor),new Rectangle(e.Bounds.X+30 ,e.Bounds.Y,e.Bounds.Width-26,e.Bounds.Height-1) );
			
			
		}

	}
}
