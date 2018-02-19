using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using CustomControls.Enumerations;

namespace CustomControls.Win32Controls
{

	public class CTextBox:TextBox
	{
		#region "Variables"

		CustomControls.Enumerations.State _State=CustomControls.Enumerations.State.Normal;

		#endregion

		#region "Properties"

		protected State State
		{
			get{return _State;}
			set
			{
				if(value!=_State)
				{
					_State= value;
					Invalidate();
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new  BorderStyle BorderStyle
		{
			get{return BorderStyle.FixedSingle;}
		}

		#endregion

		#region "Constructor"
		
		public CTextBox()
		{
			base.BorderStyle=BorderStyle.FixedSingle;
			//keep it like that!
		}

		#endregion

		#region "Override"

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State = State.Hot;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if ( !ContainsFocus )
			{
				State = State.Normal;
			}
		}
      
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			State = State.Hot;
		}
        
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			State=State.Normal;
		}

		protected override  void WndProc(ref Message m)
		{
			
			base.WndProc(ref m);	
	
			if(m.Msg==(int)Msgs.WM_PAINT)
			{
				PaintBorder(this.State);
			}			
		}
	

		#endregion

		#region"Implementation"

		private void PaintBorder(State state)
		{
			using (Graphics g=this.CreateGraphics())
			{
				Color borderColor;
				if(Enabled)
				{					
					if (State==State.Normal)
					{
						if(ReadOnly){borderColor=CustomControls.BaseClasses.AppColors.ToolbarColorLight;}
						else {borderColor=(this.Parent!=null)?Parent.BackColor:CustomControls.BaseClasses.AppColors.ControlColor;}
					}
					else{borderColor=  CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;}
				}
				else{borderColor=CustomControls.BaseClasses.AppColors.ToolbarBackColor;}
				
				using (Pen pen= new Pen(borderColor))
				{
					g.DrawRectangle(pen,new Rectangle(0,0,this.Width-1, this.Height-1));
				}
				
			}
		}

		#endregion

	}
}
