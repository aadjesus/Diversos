using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using CustomControls.HelperClasses;
using CustomControls.Enumerations;

namespace CustomControls.BaseClasses
{
	public class DropDownListBase:Control,ISupportInitialize,IMessageFilter
	{

		#region "Variable Declaration"

		public event EventHandler BeginInitialization;
		public event EventHandler EndInitialization;
	

		protected DropdownFormBase dropDownForm;
		private int buttonWidth=14;
		protected Control _HostedControl;
		protected EditControl editControl;
		private Form hostForm;
		private StringFormat strFormat;
		private HorizontalAlignment _TextAlign=HorizontalAlignment.Left;


		private Enumerations.CustomBorderStyle _CustomBorderStyle=Enumerations.CustomBorderStyle.Hot;
		private bool _DroppedDown=false;
		private int _ListWidth=120;
		private int _ListHeight=20;
		private Enumerations.State _State=State.Normal;
		private Color _BackColor=SystemColors.Window;
		private ComboBoxStyle _DropDownStyle=ComboBoxStyle.DropDown;
		private bool _Initialized=false;


	

		#endregion

		#region "Properties"
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Initialized
		{
			get{return _Initialized;}
		}
	
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool DroppedDown
		{
			get{return _DroppedDown;}
			set
			{
				if (value!=_DroppedDown)
				{
					_DroppedDown=value;
					OnDropDownChanged(new EventArgs());
				}
			}
		}


	

		[Category("Appearance")]
		[DefaultValue(typeof(ComboBoxStyle),"DropDown")]
		public virtual ComboBoxStyle DropDownStyle
		{
			get{return _DropDownStyle;}
			set
			{
				if (value!=_DropDownStyle)
				{
					_DropDownStyle=value;
					OnDropDownStyleChanged(new EventArgs());
				}
			}
		}

		protected virtual Control HostedControl
		{
			get{return _HostedControl;}
		}

		public override string Text
		{
			get
			{
				return editControl.Text;
			}
			set
			{
				if(value !=editControl.Text)
				{
					editControl.Text = value;
					//OnTextChanged(new EventArgs());
					Invalidate();
				}
			}
		}

		[DefaultValue(typeof(Enumerations.CustomBorderStyle),"Hot")]
		[Category("Appearance")]
		public virtual Enumerations.CustomBorderStyle CustomBorderStyle
		{
			get{return _CustomBorderStyle;}
			set
			{
				if (value!=_CustomBorderStyle)
				{
					_CustomBorderStyle=value;
					RefreshEditRectangle();
					this.Invalidate();
				}
			}
		}

	
	
		protected int Listwidth
		{
			get{return _ListWidth;}
			set
			{
				if (value !=_ListWidth)
				{
					_ListWidth=value;
				}
			}
		}
	
	
		protected int ListHeight
		{
			get{return _ListHeight;}
			set
			{
				if (value !=_ListHeight)
				{
					_ListHeight=value;
				}
			}
		}


		public override Color BackColor
		{
			get
			{
				return _BackColor;
			}
			set
			{
				if (value !=_BackColor)
				{
					_BackColor = value;
					Invalidate();
				}
			}
		}


		protected Enumerations.State State
		{
			get{return _State;}
			set
			{
				if (value!=_State)
				{
					_State=value;
					OnStateChanged(new EventArgs());
				}
			}
		}


		protected override Size DefaultSize
		{
			get
			{
				return new Size(120,20);
			}
		}

	
		protected new Rectangle ClientRectangle
		{
			get
			{
				if (CustomBorderStyle==Enumerations.CustomBorderStyle.None)
				{
					return base.ClientRectangle;
				}
				else
				{
					return new Rectangle(1,1,base.ClientRectangle.Width-2,base.ClientRectangle.Height-2);
				}
			}
		}
	
		[Category("Appearance")]
		[DefaultValue(typeof(HorizontalAlignment),"Left")]
		public HorizontalAlignment TextAlign
		{
			get{return _TextAlign;}
			set
			{
				if (value!=_TextAlign)
				{
					_TextAlign=value;
					editControl.TextAlign=value;
					Invalidate();
				}
			}
		}
		protected Rectangle EditRectangle
		{
			get
			{
				if (DropDownStyle!=ComboBoxStyle.Simple)
				{
					Rectangle cr=ClientRectangle;
					return new Rectangle(cr.X,cr.Y,cr.Width-buttonWidth-1,cr.Height);
				}
				else{return ClientRectangle;}
			}
		}

	
		protected Rectangle ButtonRectangle
		{
			get
			{
				if (DropDownStyle!=ComboBoxStyle.Simple)
				{
					Rectangle cr=ClientRectangle;
					return new Rectangle(cr.Right-buttonWidth,cr.Top,buttonWidth,cr.Height);
				}
				else
				{
					return Rectangle.Empty;
				}
				
			}
		}


		#endregion
		
		#region "Constructor"
	
		public DropDownListBase()
		{

				
			SetStyle(ControlStyles.AllPaintingInWmPaint,true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.FixedHeight,true);
			SetStyle(ControlStyles.FixedWidth, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, false);

			editControl= new EditControl();
			editControl.AutoSize=false;
			RefreshEditRectangle();
			editControl.MouseEnter+= new EventHandler(TopMouseEnter);
			editControl.MouseLeave+=new EventHandler(TopMouseLeave);
			editControl.GotFocus+= new EventHandler(TopGotFocus);
			editControl.LostFocus+= new EventHandler(TopLostFocus);
			editControl.MouseDown+=new MouseEventHandler(TopMouseDown);
			editControl.TextChanged+= new EventHandler(editControl_TextChanged);
			
	
			
			this.Controls.Add(editControl);

			dropDownForm = new DropdownFormBase();
			dropDownForm.FormBorderStyle = FormBorderStyle.None;
			dropDownForm.StartPosition = FormStartPosition.Manual;
			dropDownForm.TopMost = true;
			dropDownForm.ShowInTaskbar = false;
			dropDownForm.BackColor = SystemColors.Window;
			dropDownForm.TabStop=false;
			dropDownForm.Size=GetDefaultSize();
			dropDownForm.LostFocus+= new EventHandler(dropDownForm_LostFocus);
			

			if (HostedControl!=null)
			{
				HostedControl.Location=new Point(2,2);
				HostedControl.Size=new Size(dropDownForm.Width-4,dropDownForm.Height-4);
				HostedControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right |AnchorStyles.Top;
				HostedControl.Parent=dropDownForm;
				HostedControl.LostFocus+=new EventHandler(dropDownForm_LostFocus);
			}


			strFormat= new StringFormat(StringFormatFlags.NoWrap);
			
			Application.AddMessageFilter(this);
		
		}

		#endregion

		#region "Overrides"

		protected override void OnPaint(PaintEventArgs e)
		{
			DrawButton(e.Graphics);
			DrawBorder(e.Graphics);
			DrawButtonLLVLine(e.Graphics);

			if(DropDownStyle==ComboBoxStyle.DropDownList)
			{
				strFormat.Alignment= HAlignToStrAlign(TextAlign);
				DrawText(e.Graphics,Text,Font,EditRectangle,strFormat);
			}
			
		}


		private StringAlignment HAlignToStrAlign(HorizontalAlignment hAlign)
		{
			switch (hAlign)
			{
				case HorizontalAlignment.Center:
				{
					return StringAlignment.Center;
				}
				case HorizontalAlignment.Left:
				{
					return StringAlignment.Near;
				}
				case HorizontalAlignment.Right:
				{
					return StringAlignment.Far;
				}
				default :
				{
					return StringAlignment.Near;
				}
			}
		}

	
	

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			using (SolidBrush backBrush= new SolidBrush(AppColors.WindowColor))
			{
				if (!Enabled)
				{
					backBrush.Color=AppColors.ControlColor;
				}

				pevent.Graphics.FillRectangle(backBrush,ClientRectangle);
			}
		}


		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			RefreshEditRectangle();
			Invalidate();
		}

	
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			TopMouseEnter(this,e);			
		}

		private void TopMouseEnter(object sender, EventArgs e)
		{
			if (!DroppedDown  )
			{
				this.State=State.Hot;
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			TopMouseLeave(this,e);			
		}
	
		private void TopMouseLeave(object sender, EventArgs e)
		{
			if (!DroppedDown && !ContainsFocus && !editControl.Capture)
			{
				this.State=State.Normal;
			}
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			TopLostFocus(this,e);
		}

		private void TopLostFocus(object sender, EventArgs e)
		{
		
			if(!this.ContainsFocus && !dropDownForm.ContainsFocus )
			{
				DroppedDown=false;
				this.State=State.Normal;
			}
		}

		protected override void OnGotFocus(EventArgs e)
		{
			//base.OnGotFocus(e);
			TopGotFocus(this,e);			
		}

		private void TopGotFocus(object sender, EventArgs e)
		{
			base.OnGotFocus(e);
			editControl.Focus();
		
			if(!DroppedDown){this.State=State.Hot;}
		
		}
		
		
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			TopMouseDown(this,e);
		}

		private void TopMouseDown(object sender, MouseEventArgs e)
		{
			Focus();
			if (DropDownStyle==ComboBoxStyle.DropDown)
			{
				if (ButtonRectangle.Contains(new Point(e.X,e.Y)))
				{
					OnButtonClick(new EventArgs());
				}
			}
			else
			{
				if(ClientRectangle.Contains(new Point(e.X,e.Y)) )
				{
					OnButtonClick(new EventArgs());
				}
			}
			
			if(DroppedDown && !this.DisplayRectangle.Contains(new Point(e.X,e.Y)))
			{
				DroppedDown=false;
			}


		
		}
		
		

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel (e);
			if (e.Delta<0){	OnNextItem();}
			else{OnPrevItem();}
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			OnItemSelected(DroppedDown);
		}
		
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged (e);
			if (Enabled){editControl.BackColor=AppColors.WindowColor;}
			else{editControl.BackColor=AppColors.ControlColor;}
		}


		#endregion

		#region "Virtual"

		
		protected virtual void OnPrevItem()
		{}

		protected virtual void OnNextItem()
		{}
	
		protected virtual void OnScrollUp()
		{}

		protected virtual void OnScrollDown()
		{}

		protected virtual void OnPgUp()
		{}

		protected virtual void OnPgDown()
		{}
		
		protected virtual void OnBeginInit(EventArgs e)
		{
			if(BeginInitialization!=null)
			{
				BeginInitialization(this,e);
			}
		}

		protected virtual void OnEndInit(EventArgs e)
		{
			if (EndInitialization!=null)
			{
				EndInitialization(this,e);
			}
		}

		protected virtual void DrawText(Graphics g,string text,Font font,Rectangle bounds,StringFormat strformat)
		{
			using (SolidBrush textBrush= new SolidBrush(BaseClasses.AppColors.TextColor))
			{
				if(!Enabled){textBrush.Color=AppColors.ControlColorDark;}
				g.DrawString(text,font,textBrush,GetTextRectangle(font.Height,bounds),strFormat);
			}
		}

	
		protected virtual void OnDropDownChanged(EventArgs e)
		{
			if (DroppedDown){ShowDropDownForm();}
			else
			{
				if (ContainsFocus)
				{State=State.Hot;}
				else
				{State= State.Normal;}
				HideDropDownForm();
			}
			
		}
	
		
		protected virtual void OnShowButtonChanged(EventArgs e)
		{
			RefreshEditRectangle();
			Invalidate();
		}

		
		protected virtual void OnStateChanged(EventArgs e)
		{
			Invalidate();
		}


		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			
			if(DropDownStyle==ComboBoxStyle.DropDownList)
			{
				editControl.Visible=false;
				SetStyle(ControlStyles.Selectable,true);
			}
			else
			{
				editControl.Visible=true;
				SetStyle(ControlStyles.Selectable,false);
			}
			RefreshEditRectangle();
			Invalidate();
		}


		protected virtual void OnButtonClick(EventArgs e)
		{
			DroppedDown=!_DroppedDown;
		
			if (DroppedDown)
			{
				State=State.DropDown;
			}
			else
			{
				State=State.Hot;
			}
			

		}
		

		protected virtual void OnItemSelected(bool Dropped)
		{}


		protected virtual void OnDropDown(System.EventArgs e)
		{
			dropDownForm.Size=new Size(this.Listwidth,this.ListHeight);
		}

		#endregion

		#region "Implementation"

		public bool PreFilterMessage(ref Message msg)
		{
			if(Enabled && (this.ContainsFocus || dropDownForm.ContainsFocus) )
			{
				int code = (int)msg.WParam;
				
				switch (msg.Msg)
				{
					case (int)CustomControls.Enumerations.Msgs.WM_MOUSEWHEEL:
					{
						if((int)msg.WParam>0)
						{
							if(DroppedDown){OnScrollUp();}
							else{OnPrevItem();}
						}
						else
						{
							if(DroppedDown){OnScrollDown();}
							else{OnNextItem();}
						}
						return true;
					}
					case (int)CustomControls.Enumerations.Msgs.WM_LBUTTONDOWN:
					case(int)CustomControls.Enumerations.Msgs.WM_RBUTTONDOWN:
					{
						if (DroppedDown && (!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position) && !dropDownForm.RectangleToScreen(dropDownForm.DisplayRectangle).Contains(Cursor.Position)))
						{
							DroppedDown=false;
							
						}
						return false;
					}
						
					case (int)CustomControls.Enumerations.Msgs.WM_SYSKEYUP:
					{		
					
						if(ModifierKeys==Keys.Alt)
						{											
							if (code==(int)Keys.Down ||code==(int)Keys.Up )
							{
								DroppedDown=!DroppedDown;
								return true;
							}
							
						}
						
						break;
					}
					case(int)CustomControls.Enumerations.Msgs.WM_KEYDOWN:
					{
						if(DropDownStyle!=ComboBoxStyle.DropDownList){editControl.Focus();}
						if(code==(int)Keys.F4)
						{
							DroppedDown=!DroppedDown;
							return true;
						}

						else if(code ==(int)Keys.Escape && DroppedDown)
						{
							DroppedDown=false;
							return true;
						}
						else if(code ==(int)Keys.Enter )
						{
							bool flag=false;
							if (DroppedDown)
							{
								flag=true;
								DroppedDown=false;
							}
							OnItemSelected(flag);
							return true;
						}
						else if(this.ContainsFocus ||dropDownForm.ContainsFocus)
						{ 
							if(code==(int)Keys.Down)
							{
								OnNextItem();
								return true;
							}
							else if(code==(int)Keys.Up)
							{
								OnPrevItem();
								return true;
							}
							else if(code==(int)Keys.PageUp)
							{
								OnPgUp();
								return true;
							}
							else if(code==(int)Keys.PageDown)
							{
								OnPgDown();
								return true;
							}
						
						}
						break;
					}
				}
			}
			
			

			return false;
		}

		public void BeginInit()
		{
			_Initialized=false;
			OnBeginInit(new EventArgs());
		}
		public  void EndInit()
		{
			hostForm=this.FindForm();
			if (hostForm!=null)
			{
				hostForm.Move+=new EventHandler(hostForm_MoveResize);
				hostForm.Resize+=new EventHandler(hostForm_MoveResize);
				dropDownForm.Owner=hostForm;
				_Initialized=true;
			}
			
			OnEndInit(new EventArgs());
		}
	
		

		private void hostForm_MoveResize(object sender, EventArgs e)
		{
			DroppedDown=false;
		}

		private void dropDownForm_LostFocus(object sender, EventArgs e)
		{
			if ( !this.ContainsFocus && !this.dropDownForm.ContainsFocus &&!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position))
			{
				DroppedDown=false;
			}
			
		}

		protected virtual void DrawBorder(Graphics g)
		{
			switch(CustomBorderStyle)
			{
				case Enumerations.CustomBorderStyle.None:
				{
					break;
				}
				case Enumerations.CustomBorderStyle.FixedSingle:
				{
				{g.DrawRectangle(Pens.Black,new Rectangle(0,0, this.DisplayRectangle.Width-1,this.DisplayRectangle.Height-1));}
					break;
				}
				case Enumerations.CustomBorderStyle.Hot:
				{
					if (!Enabled  )
					{
					{
						g.DrawRectangle(new Pen(CustomControls.BaseClasses.AppColors.ToolbarBackColor),new Rectangle(0,0, this.DisplayRectangle.Width-1,this.DisplayRectangle.Height-1));}
					}
					else
					{
						switch(State)
						{
							case State.Normal:
							{
							{
								Color borderColor=(this.Parent!=null)?Parent.BackColor:CustomControls.BaseClasses.AppColors.ControlColor;
								g.DrawRectangle(new Pen(borderColor),new Rectangle(0,0, this.DisplayRectangle.Width-1,this.DisplayRectangle.Height-1));}
								break;
							}
							case State.Hot:
							case State.DropDown:
							{
							{g.DrawRectangle(new Pen(CustomControls.BaseClasses.AppColors.HighlightColorDarkDark),new Rectangle(0,0, this.Width-1,this.Height-1));}
								break;
							}
						}
					}
					break;
				}
			}
		}
		

		protected virtual void DrawButton(Graphics g )
		{

			if (DropDownStyle!=ComboBoxStyle.Simple)
			{

				Color backColor=(this.Parent!=null)?Parent.BackColor:CustomControls.BaseClasses.AppColors.ControlColor;
				Color arrowColor=CustomControls.BaseClasses.AppColors.TextColor;
				Rectangle backRect=ButtonRectangle;

				if (Enabled)
				{
					switch (State)
					{
						case State.Normal:
						{
							if (CustomBorderStyle!=Enumerations.CustomBorderStyle.FixedSingle)
					
							{
								backRect=new Rectangle(backRect.Left+1,backRect.Top+1,backRect.Width-2,backRect.Height-2);
							}
							break;
						}
						case State.Hot:
						{
							backColor=CustomControls.BaseClasses.AppColors.HighlightColor;
							break;
						}
						case State.DropDown:
						{
							backColor=CustomControls.BaseClasses.AppColors.HighlightColorDark;
							arrowColor=CustomControls.BaseClasses.AppColors.WindowColor;
							break;
						}
					}
				}
				else
				{
					if (CustomBorderStyle!=Enumerations.CustomBorderStyle.FixedSingle)
					{
						backRect=new Rectangle(backRect.Left+1,backRect.Top+1,backRect.Width-2,backRect.Height-2);
					}
					arrowColor=CustomControls.BaseClasses.AppColors.ControlColorDark;
				}
				g.FillRectangle(new SolidBrush(backColor), backRect);
				DrawArrow(g,backRect,arrowColor);
		
			}
		}


		private  Point[] GetArrowPoligon(Rectangle ButtonRectangle)
		{
			Point[] pts = new Point[3];
			pts[0] = new Point(ButtonRectangle.Left + ButtonRectangle.Width/2-2 , ButtonRectangle.Top + ButtonRectangle.Height/2-1);
			pts[1] = new Point(ButtonRectangle.Left + ButtonRectangle.Width/2 +3,  ButtonRectangle.Top + ButtonRectangle.Height/2-1);
			pts[2] = new Point(ButtonRectangle.Left + ButtonRectangle.Width/2, (ButtonRectangle.Top + ButtonRectangle.Height/2-1) + 3);

			return pts;
		}
	
	
		private  void DrawArrow(Graphics g,Rectangle bounds,Color  ArrowColor)
		{
			g.FillPolygon(new SolidBrush(ArrowColor),GetArrowPoligon(bounds));
		}

	
		private void DrawButtonLLVLine(Graphics g)
		{
			if(DropDownStyle!=ComboBoxStyle.Simple )
			{
				bool drawLine=false;
				Color lineColor=SystemColors.ControlText;

				switch (CustomBorderStyle)
				{
					case Enumerations.CustomBorderStyle.FixedSingle:
					{
						drawLine=true;
						break;
					}
					case Enumerations.CustomBorderStyle.Hot:
					{
						
						if (Enabled )
						{
							if(State!=State.Normal) 
							{
								drawLine=true;
								lineColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;
							}
						}
						else
						{
							drawLine=true;
							lineColor=CustomControls.BaseClasses.AppColors.ToolbarBackColor;
						}

						break;
					}
				}

				if (drawLine)
				{
					Rectangle cr=ClientRectangle;
					Point pt= new Point(cr.Right-buttonWidth-1,cr.Top);
					Point pb= new Point(cr.Right-buttonWidth-1, cr.Bottom);
					g.DrawLine(new Pen(lineColor),pt, pb);
				}

			}
		}

	

		protected virtual void ShowDropDownForm()
		{
			if(!Initialized){EndInit();}
			dropDownForm.Location=GetBestDisplayPoint( PointToScreen(new Point(0,Height)),dropDownForm.Size,this.Height);
			OnDropDown(new System.EventArgs());
			dropDownForm.Show();
			if(DropDownStyle!=ComboBoxStyle.DropDownList){editControl.Focus();}
			else{this.Focus();}
		}


		protected virtual void HideDropDownForm()
		{
			dropDownForm.Hide();
		}

		

		private  Point GetBestDisplayPoint(Point OriginPoint, Size WindowSize, int yJump)
		{
			int tmpX=OriginPoint.X;
			int tmpY=OriginPoint.Y;
			int ScreenHeight=Screen.PrimaryScreen.WorkingArea.Height;
			int ScreenWidth=Screen.PrimaryScreen.WorkingArea.Width;
			
	
			if(ScreenHeight- tmpY-WindowSize.Height<0)
			{
				tmpY-=WindowSize.Height+yJump;
			}

			tmpX=Math.Min( Math.Max(0,tmpX),ScreenWidth-WindowSize.Width);
			

			return new Point(tmpX,tmpY);
		}



		private void RefreshEditRectangle()
		{
			editControl.Size= new Size(EditRectangle.Width,EditRectangle.Height);
			editControl.Location= new Point(ClientRectangle.Left, ClientRectangle.Top);
		}


		private void editControl_TextChanged(object sender,EventArgs e)
		{
			OnTextChanged(e);
		}

		private Size GetDefaultSize()
		{
			return new Size(this.Width,20);
			
		}

		protected  RectangleF GetTextRectangle(int TextHeight,Rectangle bounds)
		{
			return new RectangleF(bounds.X+2 , bounds.Y + Math.Max(0, (bounds.Height - TextHeight) / 2), Math.Max(bounds.Width-4, 1), bounds.Height - Math.Max(0, (bounds.Height - TextHeight) / 2));
		}

		#endregion

	}

}

namespace CustomControls.Enumerations
{
	#region "Enumerations"

	public enum CustomBorderStyle
	{
		None=0,
		FixedSingle=1,
		Hot=2
	}
	public enum State
	{
		Normal=0,
		Hot=1,
		DropDown=2
	}


	public enum DisplayPosition
	{
		TopLeft=0,
		TopRight=1,
		BottonLeft=2,
		BottomRight=3
	}

	public enum ButtonState
	{
		Normal=0,
		Hot=1,
		Pushed=2,
		DropDown=3
	}
	#endregion
}