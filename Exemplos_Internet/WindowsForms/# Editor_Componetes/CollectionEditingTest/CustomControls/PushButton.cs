using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using CustomControls.HelperClasses;
using System.Drawing.Design;

namespace CustomControls.Win32Controls
{

	public class PushButton:Button
	{


		#region "Variables"
	
		private Enumerations.ButtonState _State=Enumerations.ButtonState.Normal;
		private Image _Image= null;
		private ContentAlignment _TextAlign=ContentAlignment.MiddleLeft;
		private ContentAlignment _ImageAlign=ContentAlignment.MiddleRight;
		private StringFormat strFormat;
	

	
		#endregion

		#region "Properties" 

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color ForeColor
		{
			get
			{
				return CustomControls.BaseClasses.AppColors.TextColor;
			}
			set
			{
				
			}
		}


		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color BackColor
		{
			get
			{
				return CustomControls.BaseClasses.AppColors.ControlColor;
			}
			set
			{
				
			}
		}


		override protected   System.Drawing.Size DefaultSize
		{
			get{return new System.Drawing.Size(100, 20);}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(System.Drawing.Image),"null")]
		public new Image Image
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
					Invalidate();
				}
			}
		}
	

		[Category("Appearance")]
		[DefaultValue(typeof(ContentAlignment),"MiddleLeft")]
		public override ContentAlignment TextAlign
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
		public new ContentAlignment ImageAlign
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

		
	

		#endregion

		#region "Constructor"
	
		public PushButton()
		{
			SetStyle(ControlStyles.UserPaint, true); 
			SetStyle(ControlStyles.AllPaintingInWmPaint, true); 
			SetStyle(ControlStyles.DoubleBuffer, true);

			strFormat= new StringFormat(StringFormatFlags.NoWrap);
			strFormat.Alignment=StringAlignment.Center;
		}


		#endregion

		#region "Overrides"

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			PaintBackground(e.Graphics,ClientRectangle);

			if (Image!=null)
			{
				DrawImage(e.Graphics,GetImageRectangle(e.Graphics));
			}
			if (Text!=string.Empty)
			{
				DrawText(e.Graphics,GetTextRect(e.Graphics));
			}
		
		}

	

		
		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State= Enumerations.ButtonState.Hot;
		
		}

		
		protected override void OnMouseLeave(EventArgs e)
		{
	
			base.OnMouseLeave(e);
			State= Enumerations.ButtonState.Normal;
		
			
		}
      
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
			State= Enumerations.ButtonState.Pushed;
		
		}

		
		protected override void OnMouseUp(MouseEventArgs e)
		{
			State= Enumerations.ButtonState.Hot;
			base.OnMouseUp(e);
		}

		
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);	
			Invalidate();
		}
        
		
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			State= Enumerations.ButtonState.Normal;
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
				base.OnClick(new EventArgs());
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{			

			if(e.KeyData==Keys.Space)
			{
				if (RectangleToScreen(DisplayRectangle).Contains(Cursor.Position))
				{State=Enumerations.ButtonState.Hot;	}
				else
				{State=Enumerations.ButtonState.Normal;}
			
				base.OnClick(new EventArgs());
			}
			else{base.OnKeyUp(e);}
		}

	


		#endregion

		#region "Implementation"

		protected virtual void PaintBackground(Graphics g, Rectangle bounds)
		{
			Color BackColor=(this.Parent!=null)?Parent.BackColor:CustomControls.BaseClasses.AppColors.ControlColor;
			Color BorderColor=CustomControls.BaseClasses.AppColors.HighlightColor;
			
			if(Enabled)
			{
				switch (State)
				{
					case Enumerations.ButtonState.Normal:
					{
						if (Focused)
						{
							BorderColor=CustomControls.BaseClasses.AppColors.HighlightColorDarkDark;
						}
						break;
					}
					case Enumerations.ButtonState.Hot:
					{
				
						BackColor=CustomControls.BaseClasses.AppColors.HighlightColor;
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
				BackColor=CustomControls.BaseClasses.AppColors.ControlColor;
				BorderColor=CustomControls.BaseClasses.AppColors.ToolbarBackColor;
			}

			using( SolidBrush BackBrush= new SolidBrush(Color.White))
			{
				if(State==Enumerations.ButtonState.Hot || State==Enumerations.ButtonState.Pushed ){g.FillRectangle(BackBrush,bounds);}
				BackBrush.Color=BackColor ;
				g.FillRectangle(BackBrush,bounds);
		
			}

			using(Pen BorderPen = new Pen(BorderColor))
			{
				g.DrawRectangle(BorderPen,new Rectangle(bounds.X,bounds.Y,bounds.Width-1,bounds.Height-1));
			}


		

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
			return PositionRect(new Rectangle(3,3,Width-6,Height-6),new Rectangle(0,0,(int)textSize.Width+3,(int)textSize.Height+3),TextAlign);
		}


		private Rectangle GetImageRectangle(Graphics g)
		{
			return PositionRect(new Rectangle(3,3,Width-6,Height-6),new Rectangle(0,0,Image.Width,Image.Height),ImageAlign);
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

		


		#endregion
	}
}

