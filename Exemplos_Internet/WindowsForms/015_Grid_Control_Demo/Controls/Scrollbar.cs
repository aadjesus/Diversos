using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LJ.Controls.Win32;
using LJ.Controls.General;  

namespace LJ.Controls
{
	#region DrawState
	public enum DrawState
	{
		Normal,
		Hot,
		Pressed,
		Disable
	}
	#endregion

	#region ScrollBarEvent
	public enum ScrollBarEvent
	{
		LineUp,
		LineDown,
		PageUp,
		PageDown,
		ThumbUp,
		ThumbDown
	}
	#endregion

	#region ScrollBarHit
	public enum ScrollBarHit
	{
		UpArrow,
		DownArrow,
		PageUp,
		PageDown,
		Thumb,
		LeftArrow,
		RightArrow,
		PageLeft,
		PageRight,
		None
	}
	#endregion

	#region Enumerations
	public enum ThumbDraggedFireFrequency
	{
		MouseMove,
		MouseUp
	}

	#endregion

	#region Delegates
	public delegate void ThumbHandler(object sender, int delta);
	#endregion


	[ToolboxItem(false)]
	public class ScrollBarEx : System.Windows.Forms.Control
	{
		#region Events
		public event EventHandler StartingAutomaticScrolling = null;
		public event EventHandler StoppingAutomaticScrolling = null;
		public event EventHandler ThumbReleased = null;
		#endregion

		#region Class Variables
        
		// Constants
		protected const int GRIPPER_WIDTH = 8;
		protected const int GRIPPER_HEIGHT = 8;
		protected const int MINIMUM_THUMB_WITH_GRIPPER_SIZE = 12;
		protected const int TIMER_INTERVAL = 200;
        		
		// Parent control for the scrollbar
		protected Control parentWindow;

		// property backing variables
		int hThumb = -1;
		int vThumb = -1;
		int borderGap = 0;
		protected int min = 0;
		protected int max = 100;
		protected double pos = 0;
		protected double previousPos = 0;
		protected int smallChange = 10;
		protected int largeChange = 20;
		protected ThumbDraggedFireFrequency dragFrequency = ThumbDraggedFireFrequency.MouseMove;

		// Standard scrollbars properties
		protected Color backgroundColor = Color.Empty;
		protected Color foregroundColor = Color.Empty;
		protected Color borderColor = Color.Empty;
		protected Color arrowColor = Color.Empty;
		protected Color gripperLightColor = Color.Empty;
		protected Color gripperDarkColor = Color.Empty;
		protected Color hoverColor = Color.Empty;
		protected Color pressedColor = Color.Empty;
		protected Color gradientStartBackgroundColor = Color.Empty;
		protected Color gradientEndBackgroundColor = Color.Empty;
		protected Color gradientStartForegroundColor = Color.Empty;
		protected Color gradientEndForegroundColor = Color.Empty;

		// Skin support
		protected ImageList thumbImageList = null;
		protected Image scrollShaftImage = null;
		protected ImageList upArrowImageList = null;
		protected ImageList downArrowImageList = null;
                				
		// Keep track of the UI element state
		protected DrawState thumbDrawState = DrawState.Normal;

		// Other helper variables
		protected bool stopAutomaticScrolling = false;
		
		// Keeps track of the ScrollBar objects constructed
		// so that we can transparently--to the user--check if
		// an horizontal and vertical bars are both being used
		// on the same parent window to be able to leave the 
		// lower right corner empty space that avoids the two
		// scrollbars overlapping on the arrow button
		static ArrayList scrollBarList = new ArrayList();
		protected bool usingBothScrollBars = false;

		// Miscellaneous
		protected bool drawGripper = true;
                
		#endregion
		
		#region Constructors
		public ScrollBarEx()
		{

		}

		public ScrollBarEx(Control parentControl)
		{
			// We are going to do all of the painting so better 
			// setup the control to use double buffering
			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.ResizeRedraw|
				ControlStyles.Opaque|ControlStyles.UserPaint|ControlStyles.DoubleBuffer, true);
			TabStop = false;

			hThumb = WindowsAPI.GetSystemMetrics(SystemMetricsCodes.SM_CXHTHUMB);
			vThumb = WindowsAPI.GetSystemMetrics(SystemMetricsCodes.SM_CYVTHUMB);
		
			parentWindow = parentControl;
			parentWindow.SizeChanged += new EventHandler(ParentSizeChanged);

			// Need to check setting of flag
			CheckForUsingBothScrollBarsFlag(this);
			scrollBarList.Add(this);
		}
		#endregion

		#region Overrides
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		
			// Set the parent for the scrollBar
			WindowsAPI.SetParent(Handle, parentWindow.Handle);
			SizeScrollBar();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			Graphics g = pe.Graphics;
			DrawScrollBar(g);
		}

		protected override  void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			switch (message.Msg)
			{	
				case (int)Msg.WM_LBUTTONUP:
					stopAutomaticScrolling = true;
					break;
				default:
					break;
			}
		}

		#endregion

		#region Virtuals
		// Need to be implemented by a derived class
		// This shoud have been abstract methods but 
		// because of the comments at the class declaration
		// above, this could not be
		protected virtual void SizeScrollBar()
		{      			
		}
		protected virtual void DrawScrollBar(Graphics g)
		{
		}
		protected virtual Rectangle GetThumbRect()
		{
			return Rectangle.Empty;
		}
		protected virtual void DrawBackground(Graphics g)
		{
			Rectangle rc = ClientRectangle;
			Color backColor = SystemColors.ControlLight;
			if ( backgroundColor != Color.Empty )
				backColor = BackgroundColor;

			if ( scrollShaftImage != null )
			{
				// Draw background bitmap
				g.DrawImage(scrollShaftImage, rc);

			}
			else if ( gradientStartBackgroundColor != Color.Empty && gradientEndBackgroundColor != Color.Empty )
			{

				LinearGradientMode mode = LinearGradientMode.Horizontal;
				HScrollBarEx hs = this as HScrollBarEx;
				if ( hs != null )
					mode = LinearGradientMode.Vertical;
				
				using ( LinearGradientBrush b = new LinearGradientBrush( rc, gradientStartBackgroundColor, 
							gradientEndBackgroundColor, mode) )
				{
					g.FillRectangle(b, rc);
				}
			}
			else
			{
				using ( Brush b = new SolidBrush(backColor) )
				{
					// Fill background;
					g.FillRectangle(b, rc);
				}
			}
		}

		protected virtual void DrawFlatArrowButton(Graphics g, Rectangle rc, ArrowGlyph arrowGlyph, DrawState drawState)
		{
			// Make rectangle 1 pixel smaller
			// makes it look nicer
			rc.Inflate(-1, -1);
		
			Color border = Color.Empty;
			Color background = Color.Empty;

			if ( drawState == DrawState.Normal )
			{
				border = ColorUtil.VSNetBorderColor;
				background = ColorUtil.VSNetControlColor;
				if ( backgroundColor != Color.Empty )
					background = foregroundColor;
			}
			else if ( drawState == DrawState.Hot )
			{
				border = ColorUtil.VSNetBorderColor;
				background = ColorUtil.VSNetSelectionColor;
				if ( hoverColor != Color.Empty )
					background = hoverColor;
			}
			else if ( drawState == DrawState.Pressed )
			{
				border = ColorUtil.VSNetBorderColor;
				background = ColorUtil.VSNetPressedColor;
				if ( pressedColor != Color.Empty )
					background = pressedColor;
			}

			// Which arrow gyph we need to draw
			bool upArrow = (arrowGlyph == ArrowGlyph.Up || arrowGlyph == ArrowGlyph.Left);
			bool paintBorder = true;
            
			if ( (upArrow && upArrowImageList != null && upArrowImageList.Images.Count > (int)drawState)
				|| (!upArrow && downArrowImageList != null && downArrowImageList.Images.Count > (int)drawState) )
			{
				if ( upArrow )
					g.DrawImage(upArrowImageList.Images[(int)drawState], rc);
				else
					g.DrawImage(downArrowImageList.Images[(int)drawState], rc);
				paintBorder = false;
			}
			else if ( gradientStartForegroundColor != Color.Empty && gradientEndForegroundColor != Color.Empty 
				&& drawState == DrawState.Normal )
			{
				using ( LinearGradientBrush b = new LinearGradientBrush( rc, gradientStartForegroundColor, 
							gradientEndForegroundColor, LinearGradientMode.Horizontal) )
				{
					g.FillRectangle(b, rc);
				}
			}
			else
			{
				using ( Brush b = new SolidBrush(background) )
				{
					// Fill background;
					g.FillRectangle(b, rc);
				}
			}
			
			// Check if the user set custom colors
			if ( borderColor != Color.Empty )
				border = borderColor;
			using ( Pen p = new Pen(border) )
			{
				if ( paintBorder )
					g.DrawRectangle(p, rc.Left, rc.Top, rc.Width-1, rc.Height-1);
				Color arrow = Color.Black;
				if ( arrowColor != Color.Empty )
					arrow = arrowColor;
				using ( Brush b = new SolidBrush(arrow) ) 
				{
					GDIUtil.DrawArrowGlyph(g, rc, 7, 4, arrowGlyph, b);
				}
			}
		}

		protected virtual void DrawThumb(Graphics g, DrawState drawState)
		{
			Rectangle rc = GetThumbRect();
			Color border = Color.Empty;
			Color background = Color.Empty;

			if ( drawState == DrawState.Normal )
			{
				border = ColorUtil.VSNetBorderColor;
				background = ColorUtil.VSNetControlColor;
				if ( backgroundColor != Color.Empty )
					background = foregroundColor;
			}
			else if ( drawState == DrawState.Hot )
			{
				border = ColorUtil.VSNetBorderColor;
				background = ColorUtil.VSNetSelectionColor;
				if ( hoverColor != Color.Empty )
					background = hoverColor;
			}
			else if ( drawState == DrawState.Pressed )
			{
				border = ColorUtil.VSNetBorderColor;
				background = ColorUtil.VSNetPressedColor;
				if ( pressedColor != Color.Empty )
					background = pressedColor;
			}

			// Paint border by default
			bool paintBorder = true;
            
			if ( thumbImageList != null && thumbImageList.Images.Count > (int)drawState )
			{
				
				VScrollBarEx vScrollBarEx = this as VScrollBarEx;
				bool verticalScrollBar = ( vScrollBarEx != null);
				
				// We need to draw the thumb in chunks to avoid distorting the thumb image
				// We are assuming that the corners of the image require at most 10 pixel
				// on both size, the rest will be the area we strech
				if ( !verticalScrollBar )
				{
					// Draw start corner
					Image image = thumbImageList.Images[(int)drawState];
					g.DrawImage(image, new Rectangle(rc.Left, rc.Top, 10, rc.Height), 0, 0, 10, rc.Height, GraphicsUnit.Pixel);
					// Draw middle part
					g.DrawImage(image, new Rectangle(rc.Left+10, rc.Top, rc.Width-20, rc.Height), 10, 0, image.Width-20, rc.Height, GraphicsUnit.Pixel);
					// Draw end corner
					g.DrawImage(image, new Rectangle(rc.Right-10, rc.Top, 10, rc.Height), image.Width-10, 0, 10, rc.Height, GraphicsUnit.Pixel);
				}
				else
				{
					// Draw start corner
					Image image = thumbImageList.Images[(int)drawState];
					g.DrawImage(image, new Rectangle(rc.Left, rc.Top, rc.Width, 10), 0, 0, rc.Width, 10, GraphicsUnit.Pixel);
					// Draw middle part
					g.DrawImage(image, new Rectangle(rc.Left, rc.Top+10, rc.Width, rc.Height-20), 0, 10, rc.Width, image.Height - 20, GraphicsUnit.Pixel);
					// Draw end corner
					g.DrawImage(image, new Rectangle(rc.Left, rc.Bottom-10, rc.Width, 10), 0, image.Height-10, rc.Width, 10, GraphicsUnit.Pixel);

				}
				paintBorder = false;
			}
			else if ( gradientStartForegroundColor != Color.Empty && gradientEndForegroundColor != Color.Empty )
			{
				Color startColor = gradientStartForegroundColor;
				if ( drawState == DrawState.Hot || drawState == DrawState.Pressed )
					startColor = background;
				
				LinearGradientMode mode = LinearGradientMode.Horizontal;
				HScrollBarEx hs = this as HScrollBarEx;
				if ( hs != null )
					mode = LinearGradientMode.Vertical;
				
				using ( LinearGradientBrush b = new LinearGradientBrush( rc, startColor, 
							gradientEndForegroundColor, mode) )
				{
					g.FillRectangle(b, rc);
				}
			}
			else
			{
				using ( Brush b = new SolidBrush(background) )
				{
					// Fill background;
					g.FillRectangle(b, rc);
				}
			}
			
			if ( paintBorder )
			{
				// If border color was set by the user
				if ( borderColor != Color.Empty )
					border = borderColor;
				using ( Pen p = new Pen(border) )
				{
					g.DrawRectangle(p, rc.Left, rc.Top, rc.Width-1, rc.Height-1);
				}
			}
		}

		#endregion

		#region Properties 
		public int Position
		{
			set
			{
				if ( pos != value )
				{
					previousPos = pos;
					int newValue = value;

					if ( newValue >= max-largeChange ) 
					{
						// Position cannot be larger than
						// max-largeChange
						pos = max-largeChange;
					}
					else if ( newValue < 0 )
					{
						// Negative values don't make sense
						pos = 0;
					}
					else
						pos = newValue;
					
					if ( previousPos != pos )
						Invalidate();
				}
				else
					previousPos = pos;
			}
			get 
			{
				return (int)pos;
			}
		}

		public int VThumb
		{
			get { return vThumb;}
		}

		public int HThumb
		{
			get { return hThumb;}
		}
		
		public int BorderGap
		{
			set { borderGap = value;}
			get { return borderGap; }
		}

		public int SmallChange
		{
			set 
			{
				if ( value < min || value > max)
					smallChange = 1;
				smallChange = value;				
			}
			get { return smallChange;}
		}

		public int LargeChange
		{
			set 
			{
				if ( value < min || value > max )
					largeChange = 1;
				largeChange = value;				
			}
			get { return largeChange;}
		}

		public int Minimum
		{
			set { min = value;}
			get { return min; }
		}

		public int Maximum
		{
			set { max = value;}
			get { return max; }
		}

		public int PreviousPosition
		{
			get { return (int)previousPos; }
		}

		public ThumbDraggedFireFrequency ThumbDraggedFireFrequency
		{
			set { dragFrequency = value; }
			get { return dragFrequency; }
		}

		public Color BackgroundColor
		{
			set { backgroundColor = value;}
			get { return backgroundColor; }
		}

		public Color ForegroundColor
		{
			set { foregroundColor = value;}
			get { return foregroundColor; }
		}

		public Color BorderColor
		{
			set { borderColor = value;}
			get { return borderColor; }
		}

		public Color PressedColor
		{
			set { pressedColor = value;}
			get { return pressedColor; }
		}

		public Color HoverColor
		{
			set { hoverColor = value;}
			get { return hoverColor; }
		}

		public Color ArrowColor
		{
			set { arrowColor = value;}
			get { return arrowColor; }
		}

		public Color GripperLightColor
		{
			set { gripperLightColor = value;}
			get { return gripperLightColor; }
		}

		public Color GripperDarkColor
		{
			set { gripperDarkColor = value;}
			get { return gripperDarkColor; }
		}
                
		public bool UsingBothScrollBars
		{
			set { usingBothScrollBars = value; }
			get { return usingBothScrollBars; }
		}

		public bool DrawGripper
		{
			set 
			{ 
				drawGripper = value;
				Invalidate();
			}
			get { return drawGripper; }
		}

		public Color GradientStartBackgroundColor
		{
			set { gradientStartBackgroundColor = value;}
			get { return gradientStartBackgroundColor; }
		}

		public Color GradientEndBackgroundColor
		{
			set { gradientEndBackgroundColor = value;}
			get { return gradientEndBackgroundColor; }
		}

		public Color GradientStartForegroundColor
		{
			set { gradientStartForegroundColor = value;}
			get { return gradientStartForegroundColor; }
		}

		public Color GradientEndForegroundColor
		{
			set { gradientEndForegroundColor = value;}
			get { return gradientEndForegroundColor; }
		}

		public ImageList ThumbImageList
		{
			set { thumbImageList = value; }
			get { return thumbImageList; }
		}

		public Image ScrollShaftImage
		{
			set { scrollShaftImage = value; }
			get { return scrollShaftImage; }
		}

		#endregion
		
		#region Implementation
		void CheckForUsingBothScrollBarsFlag(ScrollBarEx scrollBar)
		{
			for ( int i = 0; i < scrollBarList.Count; i++ )
			{
				ScrollBarEx currentScrollBar = (ScrollBarEx)scrollBarList[i];
				Debug.Assert(currentScrollBar != null);
				if ( currentScrollBar.parentWindow == scrollBar.parentWindow )
				{
					if ( currentScrollBar.GetType() != scrollBar.GetType() )
					{
						currentScrollBar.usingBothScrollBars = true;
						scrollBar.usingBothScrollBars = true;
					}
				}
			}
		}
		
		void ParentSizeChanged(object sender, EventArgs e)
		{
			SizeScrollBar();
		}

		protected void FireStartingAutomaticScrolling()
		{
			if ( StartingAutomaticScrolling != null )
				StartingAutomaticScrolling(this, EventArgs.Empty);
		}

		protected void FireStoppingAutomaticScrolling()
		{
			if ( StoppingAutomaticScrolling != null )
				StoppingAutomaticScrolling(this, EventArgs.Empty);
		}

		protected void FireThumbRelease()
		{
			if ( ThumbReleased != null )
				ThumbReleased(this, EventArgs.Empty);
		}

		#endregion
	}


	[ToolboxItem(false)]
	public class HScrollBarEx : ScrollBarEx
	{
		
		#region Events
		public event ThumbHandler ThumbLeft = null;
		public event ThumbHandler ThumbRight = null;
		public event EventHandler LineLeft = null;
		public event EventHandler LineRight = null;
		public event EventHandler PageLeft = null;
		public event EventHandler PageRight = null;
		#endregion
		
		#region Class variables
		DrawState leftArrowDrawState = DrawState.Normal;
		DrawState rightArrowDrawState = DrawState.Normal;
		bool ignoreMouseMove = false;
		bool draggingThumb = false;
		int oldMouseX = 0;
		ScrollBarHit currentTarget = ScrollBarHit.None;
		bool firstTick = false;
		double thumbPixelPos = 0;
		bool processingAutomaticScrolling = false;
		#endregion
				
		#region Constructors		
		public HScrollBarEx()
		{

		}

		public HScrollBarEx(Control parent) : base(parent)
		{

		}
		
		#endregion

		#region Properties
		public ImageList LeftArrowImageList
		{
			set {  upArrowImageList = value; }
			get { return upArrowImageList; }
		}

		public ImageList RightArrowImageList
		{
			set {  downArrowImageList = value; }
			get { return downArrowImageList; }
		}

		#endregion

		#region Overrides
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ( e.Button != MouseButtons.Left )
				return;

			ScrollBarHit hit = HitTest(new Point(e.X, e.Y));
			if ( hit == ScrollBarHit.LeftArrow )
			{
				leftArrowDrawState = DrawState.Hot;
				Position -= smallChange;
				FireLineLeft();
			}
			else if ( hit == ScrollBarHit.RightArrow )
			{
				rightArrowDrawState = DrawState.Hot;
				Position += smallChange;
				FireLineRight();
			}
			else if ( hit == ScrollBarHit.PageLeft )
			{
				Position -= largeChange;
				FirePageLeft();
			}
			else if ( hit == ScrollBarHit.PageRight )
			{
				Position += largeChange;
				FirePageRight();
			}
			else if ( hit == ScrollBarHit.Thumb )
			{
				Capture = true;
				draggingThumb = true;
				thumbDrawState = DrawState.Pressed;
				oldMouseX = e.X;
				thumbPixelPos = GetThumbPixelPosition(pos);
				Invalidate();
			}

			if ( !processingAutomaticScrolling )
			{
				if ( hit != ScrollBarHit.Thumb && hit != ScrollBarHit.None )
					ProcessScrolling(hit);
			}

		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			
			if ( ignoreMouseMove) 
			{
				ignoreMouseMove = false;
				return;
			}
			
			// Reset every thing to normal state
			leftArrowDrawState = DrawState.Normal;
			rightArrowDrawState = DrawState.Normal;
			thumbDrawState = DrawState.Normal;

			ScrollBarHit hit = HitTest(new Point(e.X, e.Y));
			if ( hit == ScrollBarHit.LeftArrow )
			{
				leftArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.RightArrow )
			{
				rightArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.Thumb || draggingThumb )
			{
				if ( draggingThumb )
				{
					thumbDrawState = DrawState.Pressed;
					UpdatePosition(e.X);
					oldMouseX = e.X;
					if ( dragFrequency == ThumbDraggedFireFrequency.MouseMove )
					{
						if ( pos > previousPos )
							FireThumbRight((int)pos-(int)previousPos);
						else
							FireThumbLeft((int)previousPos-(int)pos);
					}
				}
				else
				{
					thumbDrawState = DrawState.Hot;
				}
			}
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if ( e.Button != MouseButtons.Left )
				return;

			// Reset drawing to normal state
			leftArrowDrawState = DrawState.Normal;
			rightArrowDrawState = DrawState.Normal;
			thumbDrawState = DrawState.Normal;
			ignoreMouseMove = true;

			if ( draggingThumb )
			{
				Capture = false;
				thumbDrawState = DrawState.Normal;
				UpdatePosition(e.X);
				draggingThumb = false;
                
				if ( pos > previousPos )
				{
					FireThumbRight((int)pos-(int)previousPos);
				}
				else
					FireThumbLeft((int)previousPos-(int)pos);

				// For users who that want to know when the
				// Thumb is released
				FireThumbRelease();

			}
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			// Set state to hot
			base.OnMouseEnter(e);
			Point pos = Control.MousePosition;
			pos = PointToClient(pos);

			ScrollBarHit hit = HitTest(pos);
			if ( hit == ScrollBarHit.LeftArrow )
			{
				leftArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.RightArrow )
			{
				rightArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.Thumb )
			{
				thumbDrawState = DrawState.Hot;
			}
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			// Set state to Normal
			base.OnMouseLeave(e);
			leftArrowDrawState = DrawState.Normal;
			rightArrowDrawState = DrawState.Normal;
			thumbDrawState = DrawState.Normal;
			Invalidate();
		}

		protected override void SizeScrollBar()
		{
			// Resize scrollbar
			// Size scrollbar to have the standard dimensions
			// of an operating system created scrollbar
			Rectangle rcParent = parentWindow.ClientRectangle;
			if ( usingBothScrollBars )
			{
				Bounds =  new Rectangle(rcParent.Left+BorderGap, rcParent.Bottom - BorderGap*2 - HThumb,  
					rcParent.Width - BorderGap*2 - VThumb, HThumb);
			}
			else
			{
				Bounds =  new Rectangle(rcParent.Left+BorderGap, rcParent.Bottom - BorderGap*2 - HThumb,  
					rcParent.Width - BorderGap*2, HThumb);
			}
		}

		protected override void DrawScrollBar(Graphics g)
		{
			// Draw background
			DrawBackground(g);
						
			// Draw Button up arrow
			if ( Enabled )
			{
				// Up and Down buttons
				DrawArrowButtons(g);

				// Draw Thumb
				DrawThumb(g, thumbDrawState);

				// Draw Gripper
				if ( drawGripper )
					DrawThumbGripper(g, thumbDrawState);
			}
		}

		#endregion

		#region Implementation
		void FireThumbLeft(int delta)
		{
			if ( ThumbLeft != null && previousPos != pos )
				ThumbLeft(this, delta);
		}

		void FireThumbRight(int delta)
		{
			if ( ThumbRight != null && previousPos != pos )
				ThumbRight(this, delta);
		}

		void FireLineLeft()
		{
			if ( LineLeft != null && previousPos != pos )
				LineLeft(this, EventArgs.Empty);
		}

		void FireLineRight()
		{
			if ( LineRight != null && previousPos != pos )
				LineRight(this, EventArgs.Empty);
		}
        
		void FirePageLeft()
		{
			if ( PageLeft != null && previousPos != pos )
				PageLeft(this, EventArgs.Empty);
		}

		void FirePageRight()
		{
			if ( PageRight != null && previousPos != pos )
				PageRight(this, EventArgs.Empty);
		}

		void ProcessScrolling(ScrollBarHit hit)
		{
			// Capture the mouse
			stopAutomaticScrolling = false;
			Capture = true;
			firstTick = true;
					
			// Start timer
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Tick += new EventHandler(ScrollingTick);
			timer.Interval = TIMER_INTERVAL;
			timer.Start();
			currentTarget = hit;
                         
			while ( stopAutomaticScrolling == false ) 
			{
				// Check messages until we find a condition to break out of the loop
				MSG msg = new MSG();
				WindowsAPI.GetMessage(ref msg, 0, 0, 0);
				Point point = new Point(0,0);
				if ( msg.message == (int)Msg.WM_MOUSEMOVE 
					|| msg.message == (int)Msg.WM_LBUTTONUP || msg.message == (int)Msg.WM_LBUTTONDBLCLK )
				{
					point = WindowsAPI.GetPointFromLPARAM((int)msg.lParam);
				}

				Msg thisMessage = (Msg)msg.message;
				switch(msg.message)
				{
					case (int)Msg.WM_MOUSEMOVE:
					{
						ScrollBarHit current = HitTest(point);
						ProcessMouseMoveScrolling(current);
						Invalidate();
						break;
					}
					case (int)Msg.WM_LBUTTONUP:
					case (int)Msg.WM_LBUTTONDBLCLK:
					{
						stopAutomaticScrolling = true;
						WindowsAPI.DispatchMessage(ref msg);
						break;
					}
					case (int)Msg.WM_KEYDOWN:
					{
						if ( (int)msg.wParam == (int)VirtualKeys.VK_ESCAPE) 
							stopAutomaticScrolling = true;
						break;
					}
					default:
						try
						{
							WindowsAPI.DispatchMessage(ref msg);
						}
						catch(Exception e)
						{
							MessageBox.Show(e.Message);
						}
						break;
				}
			}

			// Stop timer
			timer.Stop();
			timer.Dispose();
			Invalidate();
			// Release the capture
			Capture = false;
			if ( processingAutomaticScrolling )
			{
				processingAutomaticScrolling = false;
				FireStoppingAutomaticScrolling();
			}
		}

		void ProcessMouseMoveScrolling(ScrollBarHit hit)
		{
			leftArrowDrawState = DrawState.Normal;
			rightArrowDrawState = DrawState.Normal;
			if ( hit == ScrollBarHit.LeftArrow )
				leftArrowDrawState = DrawState.Hot;
			else if ( hit == ScrollBarHit.RightArrow )
				rightArrowDrawState = DrawState.Hot;
            
		}

		void ScrollingTick(Object timeObject, EventArgs eventArgs) 
		{
			
			processingAutomaticScrolling = true;
			FireStartingAutomaticScrolling();
			
			// Ignore the first tick since sometimes the user
			// is just clicking and the first tick happens
			// so fast that produces the effect of scrolling twice
			if ( firstTick )
			{
				firstTick = false;
				return;
			}
								
			// Get mouse coordinates
			Point point = Control.MousePosition;
			point = PointToClient(point);
			Rectangle rc = Rectangle.Empty;
			
			if ( currentTarget == ScrollBarHit.LeftArrow )
			{
				rc = GetArrowButtonRectangle(true);
				if ( rc.Contains(point) )
				{
					Position -= smallChange;
					FireLineLeft();
				}
			}
			else if ( currentTarget == ScrollBarHit.RightArrow )
			{
				rc = GetArrowButtonRectangle(false);
				if ( rc.Contains(point) )
				{
					Position += smallChange;
					FireLineRight();
				}
			}
			else if ( currentTarget == ScrollBarHit.PageLeft )
			{
				rc = GetPageRect(true);
				if ( rc.Contains(point) )
				{
					Position -= largeChange;
					FirePageLeft();
				}
			}
			else if ( currentTarget == ScrollBarHit.PageRight )
			{
				rc = GetPageRect(false);
				if ( rc.Contains(point) )
				{
					Position += largeChange;
					FirePageRight();
				}
			}
		}

		void SetPosition(double fpos)
		{
			if ( pos != fpos )
			{
				previousPos = pos;
				double newValue = fpos;

				if ( newValue >= max-largeChange ) 
				{
					// Position cannot be larger than
					// max-largeChange
					pos = max-largeChange;
				}
				else if ( newValue < 0 )
				{
					// Negative values don't make sense
					pos = 0;
				}
				else
					pos = newValue;
				
				if ( previousPos != pos )
					Invalidate();
			}
			else
				previousPos = pos;
		}

		void UpdatePosition(int xPos)
		{
			int increment = 0;
			if ( xPos >= oldMouseX )
				increment = xPos - oldMouseX;
			else
				increment = (oldMouseX - xPos)*(-1);
		
			thumbPixelPos += increment; 
			double fPos = GetThumbLogicalPosition(thumbPixelPos);
			SetPosition(fPos);
		}

		protected virtual void DrawArrowButtons(Graphics g)
		{
			Rectangle leftRect = GetArrowButtonRectangle(true); 
			Rectangle rightRect = GetArrowButtonRectangle(false);
			DrawFlatArrowButton(g, leftRect, ArrowGlyph.Left, leftArrowDrawState);
			DrawFlatArrowButton(g, rightRect, ArrowGlyph.Right, rightArrowDrawState);
		}


		protected virtual void DrawThumbGripper(Graphics g, DrawState drawState)
		{
			Rectangle rc = GetThumbRect();

			// Don't draw it if it won't fit
			if ( rc.Width < MINIMUM_THUMB_WITH_GRIPPER_SIZE )
				return;

			int xMiddle = rc.Left + rc.Width/2;
			int yPos = rc.Top + (rc.Height - GRIPPER_HEIGHT)/2;
            
			Color lightColor = ColorUtil.VSNetSelectionColor;
			Color darkColor = ColorUtil.VSNetPressedColor;

			// Check if the user set custom colors for the gripper
			if ( gripperLightColor != Color.Empty )
				lightColor = gripperLightColor;
			if ( gripperDarkColor != Color.Empty )
				darkColor = gripperDarkColor;

			Pen lightPen = new Pen(lightColor);
			Pen darkPen = new Pen(darkColor);

			Point pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2, yPos);
			Point pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2, yPos+GRIPPER_WIDTH);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+1, yPos+1);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+1, yPos+GRIPPER_WIDTH+1);
			g.DrawLine(darkPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+2, yPos);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+2, yPos+GRIPPER_WIDTH);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+3, yPos+1);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+3, yPos+GRIPPER_WIDTH+1);
			g.DrawLine(darkPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+4, yPos);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+4, yPos+GRIPPER_WIDTH);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+5, yPos+1);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+5, yPos+GRIPPER_WIDTH+1);
			g.DrawLine(darkPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+6, yPos);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+6, yPos+GRIPPER_WIDTH);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xMiddle-GRIPPER_HEIGHT/2+7, yPos+1);
			pt2 = new Point(xMiddle-GRIPPER_HEIGHT/2+7, yPos+GRIPPER_WIDTH+1);
			g.DrawLine(darkPen, pt1, pt2);

			lightPen.Dispose();
			darkPen.Dispose();
			
		}

		Rectangle GetArrowButtonRectangle( bool leftButton)
		{
			Rectangle rc = ClientRectangle;
			if ( leftButton )
			{
				return new Rectangle(rc.Left, 0, HThumb, HThumb);
			}
			else
			{
				return new Rectangle(rc.Right-HThumb, 0, HThumb, HThumb);
			}
		}

		protected override Rectangle GetThumbRect()
		{
			double thumbWidth = GetThumbPixelSize();
			int drawPos = 0;

			if ( draggingThumb  ) 
			{
				// If dragging the thumb don't use
				// a position based on the scaled calculation
				// but the actual one from the OnMouseMove event
				// to avoid jerkiness from rounding off errors
				drawPos = GetSafeThumbPixelPos((int)thumbWidth);
			}
			else
			{
				drawPos = (int)GetThumbPixelPosition(pos);
			}

			if(drawPos+thumbWidth > Width - HThumb)
				drawPos=Width - HThumb - (int)thumbWidth;

			// To avoid rounding off errors
			if ( pos == max-largeChange )
				drawPos = ClientRectangle.Width - HThumb - (int)thumbWidth;
			Rectangle rc = Rectangle.Empty;
			
			// Smaller than the total width of the scrollbar
			// to make it look nicer
			rc = new Rectangle(drawPos, 0, (int)thumbWidth, HThumb);
			rc.Inflate(0, -1);

			return rc;
		}

		int GetSafeThumbPixelPos(int thumbWidth)
		{
			if ( thumbPixelPos > (ClientRectangle.Width - HThumb)- thumbWidth ) 
			{
				// Position cannot be larger than
				// max-largeChange
				return (ClientRectangle.Width - HThumb)-thumbWidth;
			}
			else if ( thumbPixelPos <= HThumb )
			{
				// Negative values don't make sense
				return HThumb;
			}
			else
				return (int)thumbPixelPos;
		}

		Rectangle GetPageRect(bool left)
		{
			Rectangle rcClient = ClientRectangle;
			Rectangle rcThumb = GetThumbRect();
			Rectangle pageRect;
			if ( left )
			{
				pageRect = new Rectangle(rcClient.Left+HThumb+1, 
					rcClient.Top, rcThumb.Left-HThumb-2, rcClient.Height);
			}
			else
			{
				pageRect = new Rectangle(rcThumb.Right+1, 
					rcClient.Top, rcClient.Right-HThumb-1, rcClient.Height);
			}
			return pageRect;
		}

		double GetThumbPixelSize()
		{
			Rectangle rc = ClientRectangle;
			int width = rc.Width - HThumb*2;
			if ( largeChange == 0 || (max-min) == 0)
				return 0;
			float numOfPages = (float)(max-min)/(float)largeChange;
			double result=width/numOfPages;                         
			if(result < 10)
				result=10;
			return result;
		}

		double GetThumbPixelPosition(double logicalPos)
		{
			double fWidth = ClientRectangle.Width - HThumb*2;
			double fRange = (max-min);
			double fLogicalPos = logicalPos;
			return (fLogicalPos*fWidth)/fRange + HThumb;
		}

		double GetThumbLogicalPosition(double pixelPos)
		{
			double fWidth = ClientRectangle.Width - HThumb*2;
			double fRange = (max-min);
			double fpixelPos = pixelPos;
			return (fRange*(fpixelPos-HThumb)/fWidth);
		}

		ScrollBarHit HitTest(Point point)
		{
			Rectangle leftArrow = GetArrowButtonRectangle(true);
			if ( leftArrow.Contains(point) )
				return ScrollBarHit.LeftArrow;
			
			Rectangle rightArrow = GetArrowButtonRectangle(false);
			if ( rightArrow.Contains(point) )
				return ScrollBarHit.RightArrow;

			Rectangle leftPageRect = GetPageRect(true);
			if ( leftPageRect.Contains(point) )
				return ScrollBarHit.PageLeft;

			Rectangle rightPageRect = GetPageRect(false);
			if ( rightPageRect.Contains(point) )
				return ScrollBarHit.PageRight;

			Rectangle thumbRect = GetThumbRect();
			if ( thumbRect.Contains(point) )
				return ScrollBarHit.Thumb;
            
			return ScrollBarHit.None;
		}
		#endregion
	}


	[ToolboxItem(false)]
	public class VScrollBarEx : ScrollBarEx
	{
		#region Events
		public event ThumbHandler ThumbUp = null;
		public event ThumbHandler ThumbDown = null;
		public event EventHandler LineUp = null;
		public event EventHandler LineDown = null;
		public event EventHandler PageUp = null;
		public event EventHandler PageDown = null;
		#endregion
		
		#region Class variables
		DrawState upArrowDrawState = DrawState.Normal;
		DrawState downArrowDrawState = DrawState.Normal;
		bool ignoreMouseMove = false;
		bool draggingThumb = false;
		int oldMouseY = 0;
		ScrollBarHit currentTarget = ScrollBarHit.None;
		bool firstTick = false;
		double thumbPixelPos = 0;
		bool processingAutomaticScrolling = false;
		#endregion
				
		#region Constructors		
		public VScrollBarEx()
		{

		}

		public VScrollBarEx(Control parent) : base(parent)
		{

		}
		
		#endregion

		#region Properties
		public ImageList UpArrowImageList
		{
			set {  upArrowImageList = value; }
			get { return upArrowImageList; }
		}

		public ImageList DownArrowImageList
		{
			set {  downArrowImageList = value; }
			get { return downArrowImageList; }
		}

		#endregion

		#region Overrides
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ( e.Button != MouseButtons.Left )
				return;

			ScrollBarHit hit = HitTest(new Point(e.X, e.Y));
			if ( hit == ScrollBarHit.UpArrow )
			{
				upArrowDrawState = DrawState.Hot;
				Position -= smallChange;
				FireLineUp();
			}
			else if ( hit == ScrollBarHit.DownArrow )
			{
				downArrowDrawState = DrawState.Hot;
				Position += smallChange;
				FireLineDown();
			}
			else if ( hit == ScrollBarHit.PageUp )
			{
				Position -= largeChange;
				FirePageUp();
			}
			else if ( hit == ScrollBarHit.PageDown )
			{
				Position += largeChange;
				FirePageDown();
			}
			else if ( hit == ScrollBarHit.Thumb )
			{
				Capture = true;
				draggingThumb = true;
				thumbDrawState = DrawState.Pressed;
				oldMouseY = e.Y;
				thumbPixelPos = GetThumbPixelPosition(pos);
				Invalidate();
			}

			// Don't create reentry problems
			if ( !processingAutomaticScrolling )
			{
				if ( hit != ScrollBarHit.Thumb && hit != ScrollBarHit.None )
					ProcessScrolling(hit);
			}

		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			
			if ( ignoreMouseMove) 
			{
				ignoreMouseMove = false;
				return;
			}
			
			// Reset every thing to normal state
			upArrowDrawState = DrawState.Normal;
			downArrowDrawState = DrawState.Normal;
			thumbDrawState = DrawState.Normal;

			ScrollBarHit hit = HitTest(new Point(e.X, e.Y));
			if ( hit == ScrollBarHit.UpArrow )
			{
				upArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.DownArrow )
			{
				downArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.Thumb || draggingThumb )
			{
				if ( draggingThumb )
				{
					thumbDrawState = DrawState.Pressed;
					UpdatePosition(e.Y);
					oldMouseY = e.Y;
					if ( dragFrequency == ThumbDraggedFireFrequency.MouseMove )
					{
						if ( pos > previousPos )
						{
							FireThumbDown((int)pos-(int)previousPos);
						}
						else
						{
							FireThumbUp((int)previousPos-(int)pos);
						}
					}
				}
				else
				{
					thumbDrawState = DrawState.Hot;
				}
			}
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if ( e.Button != MouseButtons.Left )
				return;

			// Reset drawing to normal state
			upArrowDrawState = DrawState.Normal;
			downArrowDrawState = DrawState.Normal;
			thumbDrawState = DrawState.Normal;
			ignoreMouseMove = true;

			if ( draggingThumb )
			{
				Capture = false;
				thumbDrawState = DrawState.Normal;
				UpdatePosition(e.Y);
				draggingThumb = false;
                
				if ( pos > previousPos )
				{
					FireThumbDown((int)pos-(int)previousPos);
				}
				else
					FireThumbUp((int)previousPos-(int)pos);

				// For users who that want to know when the
				// Thumb is released
				FireThumbRelease();

			}
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			// Set state to hot
			base.OnMouseEnter(e);
			Point pos = Control.MousePosition;
			pos = PointToClient(pos);

			ScrollBarHit hit = HitTest(pos);
			if ( hit == ScrollBarHit.UpArrow )
			{
				upArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.DownArrow )
			{
				downArrowDrawState = DrawState.Hot;
			}
			else if ( hit == ScrollBarHit.Thumb )
			{
				thumbDrawState = DrawState.Hot;
			}
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			// Set state to Normal
			base.OnMouseLeave(e);
			upArrowDrawState = DrawState.Normal;
			downArrowDrawState = DrawState.Normal;
			thumbDrawState = DrawState.Normal;
			Invalidate();
		}

		protected override void SizeScrollBar()
		{
			// Resize scrollbar
			// Size scrollbar to have the standard dimensions
			// of an operating system created scrollbar
			Rectangle rcParent = parentWindow.ClientRectangle;
			// If both scrollbar are being used
			if ( usingBothScrollBars )
			{
				Bounds =  new Rectangle(rcParent.Right-VThumb-BorderGap, 
					rcParent.Top+BorderGap, VThumb, rcParent.Bottom - BorderGap*2 - HThumb);
			}
			else
			{
				Bounds =  new Rectangle(rcParent.Right-VThumb-BorderGap, 
					rcParent.Top+BorderGap, VThumb, rcParent.Bottom - BorderGap*2);
			}

		}

		protected override void DrawScrollBar(Graphics g)
		{
			// Draw background
			DrawBackground(g);
						
			// Draw Button up arrow
			if ( Enabled )
			{
				// Up and Down buttons
				DrawArrowButtons(g);

				// Draw Thumb
				DrawThumb(g, thumbDrawState);

				// Draw Gripper
				if ( drawGripper )
					DrawThumbGripper(g, thumbDrawState);
			}
		}
				
		#endregion

		#region Implementation
		void FireThumbUp(int delta)
		{
			if ( ThumbUp != null && previousPos != pos )
				ThumbUp(this, delta);
		}

		void FireThumbDown(int delta)
		{
			if ( ThumbDown != null && previousPos != pos )
				ThumbDown(this, delta);
		}

		void FireLineUp()
		{
			if ( LineUp != null && previousPos != pos )
				LineUp(this, EventArgs.Empty);
		}

		void FireLineDown()
		{
			if ( LineDown != null && previousPos != pos )
				LineDown(this, EventArgs.Empty);
		}
        
		void FirePageUp()
		{
			if ( PageUp != null && previousPos != pos )
				PageUp(this, EventArgs.Empty);
		}

		void FirePageDown()
		{
			if ( PageDown != null && previousPos != pos )
				PageDown(this, EventArgs.Empty);
		}

		void ProcessScrolling(ScrollBarHit hit)
		{
			// Capture the mouse
			stopAutomaticScrolling = false;
			Capture = true;
			firstTick = true;
					
			// Start timer
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Tick += new EventHandler(ScrollingTick);
			timer.Interval = TIMER_INTERVAL;
			timer.Start();
			currentTarget = hit;
                         
			while ( stopAutomaticScrolling == false ) 
			{
				// Check messages until we find a condition to break out of the loop
				MSG msg = new MSG();
				WindowsAPI.GetMessage(ref msg, 0, 0, 0);
				Point point = new Point(0,0);
				if ( msg.message == (int)Msg.WM_MOUSEMOVE 
					|| msg.message == (int)Msg.WM_LBUTTONUP || msg.message == (int)Msg.WM_LBUTTONDBLCLK )
				{
					point = WindowsAPI.GetPointFromLPARAM((int)msg.lParam);
				}

				Msg thisMessage = (Msg)msg.message;
				switch(msg.message)
				{
					case (int)Msg.WM_MOUSEMOVE:
					{
						ScrollBarHit current = HitTest(point);
						ProcessMouseMoveScrolling(current);
						Invalidate();
						break;
					}
					case (int)Msg.WM_LBUTTONUP:
					case (int)Msg.WM_LBUTTONDBLCLK:
					{
						stopAutomaticScrolling = true;
						WindowsAPI.DispatchMessage(ref msg);
						break;
					}
					case (int)Msg.WM_KEYDOWN:
					{
						if ( (int)msg.wParam == (int)VirtualKeys.VK_ESCAPE) 
							stopAutomaticScrolling = true;
						break;
					}
					default:
						WindowsAPI.DispatchMessage(ref msg);
						break;
				}
			}

			// Stop timer
			timer.Stop();
			timer.Dispose();
			Invalidate();
			// Release the capture
			Capture = false;
			if ( processingAutomaticScrolling )
			{
				processingAutomaticScrolling = false;
				FireStoppingAutomaticScrolling();
			}
		}

		void ProcessMouseMoveScrolling(ScrollBarHit hit)
		{
			upArrowDrawState = DrawState.Normal;
			downArrowDrawState = DrawState.Normal;
			if ( hit == ScrollBarHit.UpArrow )
				upArrowDrawState = DrawState.Hot;
			else if ( hit == ScrollBarHit.DownArrow )
				downArrowDrawState = DrawState.Hot;
            
		}

		void ScrollingTick(Object timeObject, EventArgs eventArgs) 
		{
			
			processingAutomaticScrolling = true;
			FireStartingAutomaticScrolling();
			
			// Ignore the first tick since sometimes the user
			// is just clicking and the first tick happens
			// so fast that produces the effect of scrolling twice
			if ( firstTick )
			{
				firstTick = false;
				return;
			}
								
			// Get mouse coordinates
			Point point = Control.MousePosition;
			point = PointToClient(point);
			Rectangle rc = Rectangle.Empty;
			
			if ( currentTarget == ScrollBarHit.UpArrow )
			{
				rc = GetArrowButtonRectangle(true);
				if ( rc.Contains(point) )
				{
					Position -= smallChange;
					FireLineUp();
				}
			}
			else if ( currentTarget == ScrollBarHit.DownArrow )
			{
				rc = GetArrowButtonRectangle(false);
				if ( rc.Contains(point) )
				{
					Position += smallChange;
					FireLineDown();
				}
			}
			else if ( currentTarget == ScrollBarHit.PageUp )
			{
				rc = GetPageRect(true);
				if ( rc.Contains(point) )
				{
					Position -= largeChange;
					FirePageUp();
				}
			}
			else if ( currentTarget == ScrollBarHit.PageDown )
			{
				rc = GetPageRect(false);
				if ( rc.Contains(point) )
				{
					Position += largeChange;
					FirePageDown();
				}
			}
		}

		void SetPosition(double fpos)
		{
			if ( pos != fpos )
			{
				previousPos = pos;
				double newValue = fpos;

				if ( newValue >= max-largeChange ) 
				{
					// Position cannot be larger than
					// max-largeChange
					pos = max-largeChange;
				}
				else if ( newValue < 0 )
				{
					// Negative values don't make sense
					pos = 0;
				}
				else
					pos = newValue;
				
				if ( previousPos != pos )
					Invalidate();
			}
			else
				previousPos = pos;
		}

		void UpdatePosition(int yPos)
		{
			int increment = 0;
			if ( yPos >= oldMouseY )
				increment = yPos - oldMouseY;
			else
				increment = (oldMouseY - yPos)*(-1);
		
			thumbPixelPos += increment; 
			double fPos = GetThumbLogicalPosition(thumbPixelPos);
			SetPosition(fPos);
		}

		protected virtual void DrawArrowButtons(Graphics g)
		{
			Rectangle upRect = GetArrowButtonRectangle(true); 
			Rectangle downRect = GetArrowButtonRectangle(false);
			DrawFlatArrowButton(g, upRect, ArrowGlyph.Up, upArrowDrawState);
			DrawFlatArrowButton(g, downRect, ArrowGlyph.Down, downArrowDrawState);
		}

		protected virtual void DrawThumbGripper(Graphics g, DrawState drawState)
		{
			Rectangle rc = GetThumbRect();

			// Don't draw it if it won't fit
			if ( rc.Height < MINIMUM_THUMB_WITH_GRIPPER_SIZE )
				return;

			int yMiddle = rc.Top + rc.Height/2;
			int xPos = rc.Left + (rc.Width - GRIPPER_WIDTH)/2;
            
			Color lightColor = ColorUtil.VSNetSelectionColor;
			Color darkColor = ColorUtil.VSNetPressedColor;

			// Check if the user set custom colors for the gripper
			if ( gripperLightColor != Color.Empty )
				lightColor = gripperLightColor;
			if ( gripperDarkColor != Color.Empty )
				darkColor = gripperDarkColor;
			
			Pen lightPen = new Pen(lightColor);
			Pen darkPen = new Pen(darkColor);

			Point pt1 = new Point(xPos, yMiddle-GRIPPER_HEIGHT/2);
			Point pt2 = new Point(xPos + GRIPPER_WIDTH, yMiddle-GRIPPER_HEIGHT/2);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xPos+1, yMiddle-GRIPPER_HEIGHT/2+1);
			pt2 = new Point(xPos + GRIPPER_WIDTH + 1, yMiddle-GRIPPER_HEIGHT/2+1);
			g.DrawLine(darkPen, pt1, pt2);

			pt1 = new Point(xPos, yMiddle-GRIPPER_HEIGHT/2+2);
			pt2 = new Point(xPos + GRIPPER_WIDTH, yMiddle-GRIPPER_HEIGHT/2+2);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xPos+1, yMiddle-GRIPPER_HEIGHT/2+3);
			pt2 = new Point(xPos + GRIPPER_WIDTH + 1, yMiddle-GRIPPER_HEIGHT/2+3);
			g.DrawLine(darkPen, pt1, pt2);

			pt1 = new Point(xPos, yMiddle-GRIPPER_HEIGHT/2+4);
			pt2 = new Point(xPos + GRIPPER_WIDTH, yMiddle-GRIPPER_HEIGHT/2+4);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xPos+1, yMiddle-GRIPPER_HEIGHT/2+5);
			pt2 = new Point(xPos + GRIPPER_WIDTH+1, yMiddle-GRIPPER_HEIGHT/2+5);
			g.DrawLine(darkPen, pt1, pt2);

			pt1 = new Point(xPos, yMiddle-GRIPPER_HEIGHT/2+6);
			pt2 = new Point(xPos + GRIPPER_WIDTH, yMiddle-GRIPPER_HEIGHT/2+6);
			g.DrawLine(lightPen, pt1, pt2);

			pt1 = new Point(xPos+1, yMiddle-GRIPPER_HEIGHT/2+7);
			pt2 = new Point(xPos + GRIPPER_WIDTH + 1, yMiddle-GRIPPER_HEIGHT/2+7);
			g.DrawLine(darkPen, pt1, pt2);

			// Cleanup 
			lightPen.Dispose();
			darkPen.Dispose();
			
		}

		Rectangle GetArrowButtonRectangle( bool upButton)
		{
			Rectangle rc = ClientRectangle;
			if ( upButton )
			{
				return new Rectangle(0, rc.Top,
					VThumb, VThumb);
			}
			else
			{
				return new Rectangle(0, rc.Bottom-VThumb,
					VThumb, VThumb);
			}
		}

		protected override Rectangle GetThumbRect()
		{
			double thumbHeight = GetThumbPixelSize();
			int drawPos = 0;

			if ( draggingThumb  ) 
			{
				// If dragging the thumb don't use
				// a position based on the scaled calculation
				// but the actual one from the OnMouseMove event
				// to avoid jerkiness from rounding off errors
				drawPos = GetSafeThumbPixelPos((int)thumbHeight);
			}
			else
			{
				drawPos = (int)GetThumbPixelPosition(pos);
			}

            if(drawPos+thumbHeight > Height - VThumb)
				drawPos=Height - VThumb - (int)thumbHeight;

			// To avoid rounding off errors
			if ( pos == max-largeChange )
			{
				drawPos = ClientRectangle.Height - VThumb - (int)thumbHeight;
			}
			Rectangle rc = Rectangle.Empty;
			
			// Smaller than the total width of the scrollbar
			// to make it look nicer
			rc = new Rectangle(0, drawPos, VThumb, (int)thumbHeight);
			rc.Inflate(-1, 0);
			return rc;
		}

		int GetSafeThumbPixelPos(int thumbHeight)
		{
			if ( thumbPixelPos > (ClientRectangle.Height - VThumb)-thumbHeight) 
			{
				// Position cannot be larger than
				// max-largeChange
				return (ClientRectangle.Height - VThumb)-thumbHeight;
			}
			else if ( thumbPixelPos <= VThumb )
			{
				// Negative values don't make sense
				return VThumb;
			}
			else
				return (int)thumbPixelPos;
		}

		Rectangle GetPageRect(bool up)
		{
			Rectangle rcClient = ClientRectangle;
			Rectangle rcThumb = GetThumbRect();
			Rectangle pageRect;
			if ( up )
			{
				pageRect = new Rectangle(rcClient.Left, 
					rcClient.Top+VThumb+1, rcClient.Width, rcThumb.Top-VThumb-2);
			}
			else
			{
				pageRect = new Rectangle(rcClient.Left, 
					rcThumb.Bottom+1, rcClient.Width, rcClient.Bottom-VThumb-1);
			}
			return pageRect;
		}

		double GetThumbPixelSize()
		{
			Rectangle rc =ClientRectangle;
			int height = rc.Height - VThumb*2;
			if ( largeChange == 0 || (max-min) == 0)
				return 0;
			float numOfPages = (float)(max-min)/(float)largeChange;
			double result= height/numOfPages;                         
			if(result <10)
			{
				result=10;
			}
			return result;
		}

		double GetThumbPixelPosition(double logicalPos)
		{
			double fHeight = ClientRectangle.Height - VThumb*2;
			double fRange = (max-min);
			double fLogicalPos = logicalPos;
			return (fLogicalPos*fHeight)/fRange + VThumb;
		}

		double GetThumbLogicalPosition(double pixelPos)
		{
			double fHeight = ClientRectangle.Height - VThumb*2;
			double fRange = (max-min);
			double fpixelPos = pixelPos;
			return (fRange*(fpixelPos-VThumb)/fHeight);
		}

		ScrollBarHit HitTest(Point point)
		{
			Rectangle upArrow = GetArrowButtonRectangle(true);
			if ( upArrow.Contains(point) )
				return ScrollBarHit.UpArrow;
			
			Rectangle downArrow = GetArrowButtonRectangle(false);
			if ( downArrow.Contains(point) )
				return ScrollBarHit.DownArrow;

			Rectangle upPageRect = GetPageRect(true);
			if ( upPageRect.Contains(point) )
				return ScrollBarHit.PageUp;

			Rectangle downPageRect = GetPageRect(false);
			if ( downPageRect.Contains(point) )
				return ScrollBarHit.PageDown;

			Rectangle thumbRect = GetThumbRect();
			if ( thumbRect.Contains(point) )
				return ScrollBarHit.Thumb;
            
			return ScrollBarHit.None;
		}
		#endregion

	}
}
