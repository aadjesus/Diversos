using System;
using CustomControls.BaseClasses;
using CustomControls.HelperClasses;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CustomControls.Win32Controls
{
	
	public class DropDownColorPicker:DropDownListBox
	{

		private Color _Value=Color.White;
		public event EventHandler ValueChanged;
		private ColorListBox CList= new ColorListBox();


		public override ListBox List
		{
			get{return CList;}
		}

		[DefaultValue(typeof(Color),"White")]
		public Color Value
		{
			get{return _Value;}
			set
			{
				if(value !=_Value)
				{
					_Value= value;
					Text=value.Name;
					OnValueChanged(new EventArgs());
					Invalidate(); 
				}
			}
		}


		public DropDownColorPicker()
		{
			this.DropDownStyle=ComboBoxStyle.DropDownList;
			this.List.SelectedIndexChanged+=new EventHandler(List_SelectedIndexChanged);
		}


		protected virtual void OnValueChanged(System.EventArgs e)
		{			
			if(ValueChanged!= null)	{ValueChanged(this,e);}
		}

		protected override void DrawText(System.Drawing.Graphics g, string text, System.Drawing.Font font, System.Drawing.Rectangle bounds, System.Drawing.StringFormat strformat)
		{
			using ( SolidBrush colBrush= new SolidBrush(Value))
			{			
				
				Rectangle colRect=GetColorRect(bounds);
				
				g.FillRectangle(colBrush,colRect);
				g.DrawRectangle( Enabled? Pens.Black:Pens.DarkGray,colRect);
				
				base.DrawText(g,text,font,GetTextRect(colRect, bounds),strformat);
				
			}
		}
		
		private Rectangle GetColorRect(Rectangle bounds)
		{
			return new Rectangle(bounds.X + 2, Math.Max(0,Math.Min(3,bounds.Height-3)),Math.Min(bounds.Width-5,22) ,Math.Max(0,bounds.Height-5));
		}
		
		private Rectangle GetTextRect(Rectangle ColRect,Rectangle bounds)
		{
			return new Rectangle(ColRect.Width+3,ColRect.Top,bounds.Width-ColRect.Width-3, ColRect.Height);
		}
		

		private void List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Value=Color.FromName(List.SelectedItem.ToString());
		}
		

	}
}
