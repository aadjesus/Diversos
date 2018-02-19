
using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;	
using CustomControls.Enumerations;
using CustomControls.HelperClasses;

namespace CustomControls.BaseClasses
{

	public class DropDownButtonBase:Control,ISupportInitialize,IMessageFilter
	{

		#region "Variables"
		public event EventHandler DropDown;
		public event EventHandler BeginInitialization;
		public event EventHandler EndInitialization;
	
		private Enumerations.ButtonState _State=Enumerations.ButtonState.Normal;
		private Image _Image= null;
		private ContentAlignment _TextAlign=ContentAlignment.MiddleLeft;
		private ContentAlignment _ImageAlign=ContentAlignment.MiddleRight;
		private StringFormat strFormat;
		private  int _DropButtonWidth=10;
		protected DropdownFormBase dropDownForm;
		protected Control _HostedControl;
		private Form hostForm;
		private int _ListWidth=120;
		private int _ListHeight=20;
		private bool _CanDropDown=true;
		private bool selecting=false;

	
		#endregion

		#region "Properties" 
	


		override protected   System.Drawing.Size DefaultSize
		{
			get{return new System.Drawing.Size(100, 20);}
		}

		protected int DropDownButtonWidth
		{
			get
			{
				if(CanDropDown){return _DropButtonWidth;}
				else{return 0;}
			}
			set
			{
				if(value!=_DropButtonWidth)
				{
					_DropButtonWidth=value;
					Invalidate();
				}
			}
		}


		[Category("Behavior")]
		[DefaultValue(typeof(Boolean),"True")]
		public bool CanDropDown
		{
			get{return _CanDropDown;}
			set
			{
				if(value!=_CanDropDown)
				{
					_CanDropDown= value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(System.Drawing.Image),"null")]
		public Image Image
		{
			get{return _Image;}
			set
			{
				if (value !=_Image)
				{
					_Image= value;
					Invalidate();
				}
			}
		}

	
		protected virtual Enumerations.ButtonState State
		{
			get{return _State;}
			set
			{
				if(value!=_State)
				{
					_State= value;
					OnStateChanged(new EventArgs());
					Invalidate();
				}
			}
		}
	

		[Category("Appearance")]
		[DefaultValue(typeof(ContentAlignment),"MiddleLeft")]
		public ContentAlignment TextAlign
		{
			get{return _TextAlign;}
			set
			{
				if (value!=_TextAlign)
				{
					_TextAlign= value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(ContentAlignment),"MiddleRight")]
		public ContentAlignment ImageAlign
		{
			get{return _ImageAlign;}
			set
			{
				if (value!=_ImageAlign)
				{
					_ImageAlign= value;
					Invalidate();
				}
			}
		}

		public override string Text
		{
			get{return base.Text;}
			set
			{
				if (value!=base.Text)
				{
					base.Text= value;
					Invalidate();
					base.OnTextChanged(new EventArgs());
				}
			}
		}

		protected Rectangle PushRectangle
		{
			get{return new Rectangle(1,1,Width-2-DropDownButtonWidth,Height-2);}
		}
	
		protected Rectangle DropRectangle
		{
			get
			{
				if(CanDropDown){return new Rectangle(Width-DropDownButtonWidth-1,1,DropDownButtonWidth,Height-2);}
				else{return Rectangle.Empty;}
			}
		}


		protected virtual Control HostedControl
		{
			get{return _HostedControl;}
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


		#endregion

		#region "Contructors"

		public DropDownButtonBase()
		{
			SetStyle(ControlStyles.UserPaint, true); 
			SetStyle(ControlStyles.AllPaintingInWmPaint, true); 
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.FixedHeight,true);
			SetStyle(ControlStyles.FixedWidth, true);
	

			strFormat= new StringFormat(StringFormatFlags.NoWrap);
			strFormat.Alignment=StringAlignment.Center;

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


	
			
			Application.AddMessageFilter(this);
		}

		#endregion

		#region "Overrides"

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			
			if (Image!=null)
			{
				DrawImage(e.Graphics,GetImageRectangle(e.Graphics));
			}
			if (Text!=string.Empty)
			{
				DrawText(e.Graphics,GetTextRect(e.Graphics));
			}
		
			if (CanDropDown){ DrawArrow(e.Graphics,DropRectangle);}

		}

		
		protected override  void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
		{
			PaintBackground(e.Graphics,ClientRectangle);
		
		}

		
		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		
		protected override void OnMouseEnter(EventArgs e)
		{
			
			base.OnMouseEnter(e);
			if (State!=Enumerations.ButtonState.DropDown) {State= Enumerations.ButtonState.Hot;}
		
		}

		
		protected override void OnMouseLeave(EventArgs e)
		{
	
			base.OnMouseLeave(e);
			if (State!=Enumerations.ButtonState.DropDown){ State= Enumerations.ButtonState.Normal;}
		
			
		}
      

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel (e);
			if (e.Delta<0){	OnNextItem();}
			else{OnPrevItem();}
		}
	
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if(e.Button==MouseButtons.Left)
			{
				Focus();

			
				if(e.X<Width-DropDownButtonWidth)
				{
					State= Enumerations.ButtonState.Pushed;
					selecting=true;

				}
				else if(CanDropDown)			
				{
					if (State==Enumerations.ButtonState.DropDown)
					{
						State= Enumerations.ButtonState.Hot;
					}
					else{State= Enumerations.ButtonState.DropDown;}
				}
				else{State= Enumerations.ButtonState.Pushed;}

			}
			
		}

		
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (State!=Enumerations.ButtonState.DropDown)
			{
				State= Enumerations.ButtonState.Hot;
			}
			if(e.X<Width-DropDownButtonWidth && e.X>0 && selecting)
			{
				selecting=false;
				OnSelectItem(true);
			}

		}

		
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);	
			Invalidate();
		
		}
        
		
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
		
			if(!this.ContainsFocus && !dropDownForm.ContainsFocus )
			{
				State= Enumerations.ButtonState.Normal;
			}
			Invalidate();
		}


		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown (e);

			if(e.KeyData==Keys.Space)
			{
				State=Enumerations.ButtonState.Pushed;
			}
			else if (e.KeyData== Keys.Enter)
			{
				OnClick(new EventArgs());
				OnSelectItem(true);

			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp (e);

			if(e.KeyData==Keys.Space)
			{
				if (RectangleToScreen(DisplayRectangle).Contains(Cursor.Position))
				{State=Enumerations.ButtonState.Hot;	}
				else
				{State=Enumerations.ButtonState.Normal;}
			
				OnClick(new EventArgs());
				OnSelectItem(true);

			}
		}

		


	
		#endregion


		#region "virtual"
			

		protected virtual void OnPrevItem()
		{}

	
		protected virtual void OnNextItem()
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
		
		protected virtual void OnStateChanged(EventArgs e)
		{
			if (State==Enumerations.ButtonState.DropDown)
			{
				ShowDropDownForm();
			}
			else
			{
			
				HideDropDownForm();
			}
		}

		
		protected virtual void ShowDropDownForm()
		{
			
			dropDownForm.Location=GetBestDisplayPoint( PointToScreen(new Point(0,Height)),dropDownForm.Size,this.Height);
			OnDropDown(new EventArgs());
			dropDownForm.Show();
			this.Focus();
		}


		protected virtual void HideDropDownForm()
		{
			dropDownForm.Hide();
		}

		

			protected virtual void OnDropDown(EventArgs e)
			{
				if(DropDown!=null)
				{
					DropDown(this,e);
				}
			
			}



	
		protected virtual void PaintBackground(Graphics g, Rectangle bounds)
		{
		
			Color BorderColor=CustomControls.BaseClasses.AppColors.HighlightColor;
			SolidBrush BackBrush= new SolidBrush(this.Parent.BackColor);
			Pen BorderPen = new Pen(BorderColor);
			
			if(Enabled)
			{
				switch (State)
				{
					case Enumerations.ButtonState.Normal:
					{
						if (Focused){BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;}
					
						g.FillRectangle(BackBrush,bounds);
						break;
					}
					case Enumerations.ButtonState.Hot:
					{
				
						BackBrush.Color=CustomControls.BaseClasses.AppColors.HighlightColor;
						BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;

						g.FillRectangle(Brushes.White,bounds);
						g.FillRectangle(BackBrush,bounds);

						break;
					}
					case Enumerations.ButtonState.Pushed:
					{
						BackBrush.Color=CustomControls.BaseClasses.AppColors.HighlightColorDark;
						BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;

						g.FillRectangle(Brushes.White,bounds);
						g.FillRectangle(BackBrush,PushRectangle);

						if(CanDropDown)
						{
							BackBrush.Color=CustomControls.BaseClasses.AppColors.HighlightColor;
							g.FillRectangle(BackBrush,DropRectangle);
						}
						break;
					}
					case Enumerations.ButtonState.DropDown:
					{
						BackBrush.Color=CustomControls.BaseClasses.AppColors.HighlightColorDark;
						BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;

						g.FillRectangle(Brushes.White,bounds);
						g.FillRectangle(BackBrush,DropRectangle);

						BackBrush.Color=CustomControls.BaseClasses.AppColors.HighlightColorLight;
						g.FillRectangle(BackBrush,PushRectangle);
						break;
					}
				}
			
			
			}
			else
			{			
				BorderColor=CustomControls.BaseClasses.AppColors.ToolbarBackColor;
				BackBrush.Color=CustomControls.BaseClasses.AppColors.ControlColor ;
				g.FillRectangle(BackBrush,bounds);
			}

				
			
			BorderPen.Color=BorderColor;
			g.DrawRectangle(BorderPen,new Rectangle(bounds.X,bounds.Y,bounds.Width-1,bounds.Height-1));
			
		{
			if(CanDropDown){g.DrawLine(BorderPen,Width-DropDownButtonWidth-2,0,Width-DropDownButtonWidth -2,Height);}
		}
		
			BackBrush.Dispose();
			BorderPen.Dispose();

		}
			

		protected virtual void DrawText(Graphics g,Rectangle bounds)
		{


			using (SolidBrush brush= new SolidBrush(SystemColors.ControlText))
			{
				StringFormat StrFormat= new StringFormat(StringFormatFlags.NoWrap);
				StrFormat.Alignment=StringAlignment.Center;
				

				if (Enabled)
				{
					switch (State)
					{
						case Enumerations.ButtonState.Normal:
						case Enumerations.ButtonState.DropDown:		
						case Enumerations.ButtonState.Hot:
						{
							g.DrawString(Text,Font,brush,bounds,StrFormat );
							break;
						}
						case Enumerations.ButtonState.Pushed:
						{
							brush.Color=SystemColors.HighlightText;
							g.DrawString(Text,Font,brush,bounds,StrFormat );
							break;
						}
					}
				}
				else
				{
					brush.Color=SystemColors.GrayText;
					g.DrawString(Text,Font,brush,bounds,StrFormat);
				}
			}
		}


		protected virtual void DrawImage(Graphics g,Rectangle bounds )
		{
			if (Enabled)
			{
				
				switch (State)
				{
					case Enumerations.ButtonState.Normal:
					case Enumerations.ButtonState.Pushed:
					case Enumerations.ButtonState.DropDown:
					{
							
						g.DrawImage(Image,bounds);
						break;	
					}
					case Enumerations.ButtonState.Hot:
					{
							
						DrawImageDisabled(g,new Rectangle(bounds.X+1,bounds.Y+1,bounds.Width,bounds.Height),Image);
						g.DrawImage(Image,new Rectangle(bounds.X-1,bounds.Y-1,bounds.Width,bounds.Height));
						break;
					}

				}
				
			}
			else
			{
				DrawImageDisabled(g,bounds,Image);
			}
		}


		protected virtual void OnSelectItem(bool DefaultValue)
		{}

		#endregion

		#region "Implementation"

		protected void DrawImageDisabled(Graphics g,Rectangle bounds, Image image)
		{
			Bitmap DisabledImage = new Bitmap((Bitmap) image);
			Color shadowColor = SystemColors.ControlDark;
			Color transparent1 = Color.FromArgb(0, 0, 0, 0);
			

			for(int pixelX = 0; pixelX < image.Width; pixelX++)
			{
				for(int pixelY = 0; pixelY < image.Height; pixelY++)
				{
					Color pixel=DisabledImage.GetPixel(pixelX, pixelY);
					if ( pixel!= transparent1 )
						DisabledImage.SetPixel(pixelX, pixelY, shadowColor);
				}
			}
		
			g.DrawImage(DisabledImage, bounds);

		}

	
		private Rectangle GetTextRect(Graphics g)
		{
			SizeF textSize=g.MeasureString(Text,Font,Width,strFormat);
			return PositionRect(new Rectangle(3,3,Width-6-DropDownButtonWidth-1,Height-6),new Rectangle(0,0,(int)textSize.Width+3,(int)textSize.Height+3),TextAlign);
		}


		private Rectangle GetImageRectangle(Graphics g)
		{
			Rectangle pRect= new Rectangle(3,3,Width-6-DropDownButtonWidth-1,Height-6);
			return PositionRect(pRect,new Rectangle(0,0,Image.Width,Image.Height),ImageAlign);
		}


		private Rectangle PositionRect(Rectangle parentRect, Rectangle childRect, ContentAlignment position)
		{
			int cWidth= Math.Min(parentRect.Width,childRect.Width);
			int cHeight=Math.Min(parentRect.Height,childRect.Height);
			switch(position)
			{
				case ContentAlignment.TopLeft:
				{
					return new Rectangle(parentRect.X,parentRect.Y,cWidth,cHeight);
				}
				case ContentAlignment.MiddleLeft:
				{
					return new Rectangle(parentRect.X,parentRect.Y+ CenterSegment(parentRect.Height,cHeight),cWidth,cHeight);
				}
				case ContentAlignment.BottomLeft:
				{
					return new Rectangle(parentRect.X,parentRect.Y+parentRect.Height-cHeight,cWidth,cHeight);
				}


				case ContentAlignment.TopCenter:
				{
					return new Rectangle(parentRect.X + CenterSegment(parentRect.Width,cWidth),parentRect.Y,cWidth,cHeight);
				}
				case ContentAlignment.MiddleCenter:
				{
					return new Rectangle(parentRect.X + CenterSegment(parentRect.Width,cWidth),parentRect.Y+ CenterSegment(parentRect.Height,cHeight),cWidth,cHeight);
				}
				case ContentAlignment.BottomCenter:
				{
					return new Rectangle(parentRect.X + CenterSegment(parentRect.Width,cWidth),parentRect.Y+parentRect.Height-cHeight,cWidth,cHeight);
				}


				case ContentAlignment.TopRight:
				{
					return new Rectangle(parentRect.X +parentRect.Width-cWidth,parentRect.Y,cWidth,cHeight);
				}
				case ContentAlignment.MiddleRight:
				{
					return new Rectangle(parentRect.X +parentRect.Width-cWidth,parentRect.Y+ CenterSegment(parentRect.Height,cHeight),cWidth,cHeight);
				}
				case ContentAlignment.BottomRight:
				{
					return new Rectangle(parentRect.X +parentRect.Width-cWidth,parentRect.Y+parentRect.Height-cHeight,cWidth,cHeight);
				}


				default:{return parentRect;}

			}
		}


		private int CenterSegment(int parentSegment, int childSegment)
		{
			return Math.Max(0,(parentSegment-childSegment)/2);
		}


		private  Point[] GetArrowPoligon(Rectangle ButtonRectangle)
		{
			Point[] pts = new Point[3];
			pts[0] = new Point(ButtonRectangle.Left + ButtonRectangle.Width/2-2 , ButtonRectangle.Top + ButtonRectangle.Height/2-1);
			pts[1] = new Point(ButtonRectangle.Left + ButtonRectangle.Width/2 +3,  ButtonRectangle.Top + ButtonRectangle.Height/2-1);
			pts[2] = new Point(ButtonRectangle.Left + ButtonRectangle.Width/2, (ButtonRectangle.Top + ButtonRectangle.Height/2-1) + 3);

			return pts;
		}
	
	
		private  void DrawArrow(Graphics g,Rectangle bounds)
		{
			Color  ArrowColor=SystemColors.ControlText;
			if (!Enabled){ArrowColor=AppColors.ControlColorDark;}
			else if (State==Enumerations.ButtonState.DropDown){ArrowColor=AppColors.WindowColor; }
			g.FillPolygon(new SolidBrush(ArrowColor),GetArrowPoligon(bounds));
		}


		private Size GetDefaultSize()
		{
			return new Size(this.Width,20);
		}


		private void dropDownForm_LostFocus(object sender, EventArgs e)
		{
			if ( !this.ContainsFocus && !this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position))
			{
				State=Enumerations.ButtonState.Normal;
			}
			
		}

	
		public void BeginInit()
		{
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
			}

			OnEndInit(new EventArgs());
		}
	
	
		private void hostForm_MoveResize(object sender, EventArgs e)
		{
			State=Enumerations.ButtonState.Normal;
		}


		public bool PreFilterMessage(ref Message msg)
		{
			if(Enabled && (this.ContainsFocus || dropDownForm.ContainsFocus) )
			{
				int code = (int)msg.WParam;
				
				switch (msg.Msg)
				{
					
					case (int)CustomControls.Enumerations.Msgs.WM_LBUTTONDOWN:
					case(int)CustomControls.Enumerations.Msgs.WM_RBUTTONDOWN:
					{
						if (State==Enumerations.ButtonState.DropDown && (!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position) && !dropDownForm.RectangleToScreen(dropDownForm.DisplayRectangle).Contains(Cursor.Position)))
						{
							State=Enumerations.ButtonState.Normal;
							
						}
						return false;
					}
						
					case (int)CustomControls.Enumerations.Msgs.WM_SYSKEYUP:
					{		
					
						if(ModifierKeys==Keys.Alt)
						{											
							if (code==(int)Keys.Down ||code==(int)Keys.Up )
							{
								ToggleDropDown();
								return true;
							}
							
						}
						
						break;
					}
					case(int)CustomControls.Enumerations.Msgs.WM_KEYDOWN:
					{
				
						if(code==(int)Keys.F4)
						{
							ToggleDropDown();
							return true;
						}

						else if(code ==(int)Keys.Escape && State==Enumerations.ButtonState.DropDown)
						{
							ToggleDropDown();
							return true;
						}
						else if(code ==(int)Keys.Enter && State==Enumerations.ButtonState.DropDown)
						{
							ToggleDropDown();
							OnSelectItem(false);
							return true;
						}
						else if(this.ContainsFocus )
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

	
		private void ToggleDropDown()
		{
			if (State==Enumerations.ButtonState.Normal || State==Enumerations.ButtonState.Hot )	{State=Enumerations.ButtonState.DropDown;	}
			else if(State==Enumerations.ButtonState.DropDown)
			{
				if(this.RectangleToScreen(DisplayRectangle).Contains(Cursor.Position))
				{
					State=Enumerations.ButtonState.Hot;	
				}
				else
				{
					State=Enumerations.ButtonState.Normal;	
				}
			}
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


		#endregion
	}

}

namespace CustomControls.HelperClasses
{
	public struct ItemSelectedEventArgs
	{
	
		object _SelectedItem;
		int _SelectedIndex;

		public ItemSelectedEventArgs(object SelectedItem, int SelectedIndex )
		{
			this._SelectedItem=SelectedItem;
			this._SelectedIndex =SelectedIndex ;
		}
		

		public int SelectedIndex
		{
			get{return _SelectedIndex;}
		}

		public object SelectedItem
		{
			get{return _SelectedItem;}
		}
	}
}



