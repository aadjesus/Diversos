using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;	
using CustomControls.HelperClasses;


namespace CustomControls.Win32Controls
{
	public class ToggleButton:PushButton 
	{
		private bool _Pushed=false;
		private bool _AutoToggle=true;
		

		[Category("Behavior")]
		[DefaultValue(typeof(bool),"False")]
		public bool Pushed
		{
			get{return _Pushed;}
			set
			{
				if (value !=_Pushed)
				{
					_Pushed= value;
					Invalidate();
				}
			}
		}


		[Category("Behavior")]
		[DefaultValue(typeof(bool),"True")]
		[Description("If True, the control automatically toggles Pushed state on MouseClick.")]
		public bool AutoToggle
		{
			get{return _AutoToggle;}
			set{_AutoToggle=value;}
		}


		

		public ToggleButton():base()
		{
			
		}

		protected override void PaintBackground(Graphics g, Rectangle bounds)
		{
			Color BackColor=(this.Parent!=null)?Parent.BackColor:CustomControls.BaseClasses.AppColors.ControlColor;
			Color BorderColor=CustomControls.BaseClasses.AppColors.HighlightColor;
			
			if(Enabled)
			{
				switch (State)
				{
					case Enumerations.ButtonState.Normal:
					{
						if (Pushed)
						{
							if (Focused)
							{
								BackColor=CustomControls.BaseClasses.AppColors.HighlightColor;
							}
							else
							{
								BackColor=CustomControls.BaseClasses.AppColors.HighlightColorLight;
							}
							BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;
						}
						else
						{
							if (Focused)
							{
								BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;
							}
						}
						break;
					}
					case Enumerations.ButtonState.Hot:
					{
				
						if (Pushed)
						{
							BackColor=CustomControls.BaseClasses.AppColors.HighlightColorDark;
						}
						else
						{
							BackColor=CustomControls.BaseClasses.AppColors.HighlightColor;
						}

						BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;
						break;
					}
					case Enumerations.ButtonState.Pushed:
					{
						BackColor=CustomControls.BaseClasses.AppColors.HighlightColorDark;
						BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;
						break;
					}
				}
			
			
			}
			else
			{
				BackColor=SystemColors.Control;
				BorderColor=CustomControls.BaseClasses.AppColors.ToolbarBackColor;
			}

			using( SolidBrush BackBrush= new SolidBrush(Color.White))
			{
						
				if (!(State==Enumerations.ButtonState.Normal && !Pushed))g.FillRectangle(BackBrush,bounds);
				BackBrush.Color=BackColor ;
				g.FillRectangle(BackBrush,bounds);
		
			}

			using(Pen BorderPen = new Pen(BorderColor))
			{
				g.DrawRectangle(BorderPen,new Rectangle(bounds.X,bounds.Y,bounds.Width-1,bounds.Height-1));
			}

		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			if (AutoToggle){Pushed=!Pushed;}
		}

	


	}
}
