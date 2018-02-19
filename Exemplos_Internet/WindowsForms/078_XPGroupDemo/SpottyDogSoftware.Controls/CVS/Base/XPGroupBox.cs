using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Delegate invoked when an Item is clicked
	/// </summary>
	public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

	/// <summary>
	/// Delegate Invoked when the Size changes
	/// </summary>
	public delegate void SizeChangingHandler(Object sender, EventArgs e);
	/// <summary>
	/// Summary description for XPGroupBox.
	/// </summary>
	[Designer("SpottyDogSoftware.Controls.XPGroupBoxDesigner, SpottyDogSoftware.Controls", typeof(System.ComponentModel.Design.IDesigner))]
	public class XPGroupBox : System.Windows.Forms.UserControl
	{
		#region Constants
		/// <summary>
		/// Encapsulates the constants within the XPGroupBox control
		/// </summary>
		class Consts
		{
			/// <summary>
			/// The caption height.
			/// </summary>
			public const int CaptionHeight = 25;
			/// <summary>
			/// Indent for the caption text.
			/// </summary>
			public const int CaptionIndent = 37;
			/// <summary>
			/// Downward displacement for the caption to allow icon overlap.
			/// </summary>
			public const int CaptionOffset = 8;
			/// <summary>
			/// Dimensions of the Item Icons
			/// </summary>
			public const int ItemIconSize = 16;
			/// <summary>
			/// Item offset work area top and bottom
			/// </summary>
			public const int ItemOffset = 8;
			/// <summary>
			/// Y position of the first item in the list
			/// </summary>
			public const int FirstItemOffset = CaptionHeight + CaptionOffset + ItemOffset;
			/// <summary>
			///Item Icon Offset
			/// </summary>
			public const int ItemIconOffset = 12;
		}
		#endregion

		#region Members
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int itemHeight						= -1;
		private int controlHeight					= -1;
		private int transitionSizeDelta			= 0;
		private int transitionAlphaChannel	= 0;
		private bool captionHighlighted		= false;

		private bool expanded				= true; // current control  expanded state
		private bool controlExpanded	= true; // used to determine if control expanded at runtime

		private int captionCurveRadius = 7;

		private Color captionLeftColor					= Color.White;
		private Color captionRightColor				= Color.FromArgb(198, 210, 248);
		private Color paneTopLeftColor				= Color.White;
		private Color paneBottomRightColor		= Color.FromArgb(214, 223, 247);
		private Color paneOutlineColor				= Color.White;
		private Color captionFontColor				= Color.Black;
		private Color captionFontHighLightColor = Color.Red;

		private Font captionFont = new Font("Microsoft Verdana", 8, FontStyle.Bold);
		private string captionText = "My Caption";
		private System.Timers.Timer timer1;
		private XPGroupBoxItem activeItem;

		/// <summary>
		/// The state of an XPGroupBox
		/// </summary>
		private enum GroupState{ 
			/// <summary>
			/// The Group control is neither expanding nor collapsing
			/// </summary>
			Static, 
			/// <summary>
			/// The Group control is expanding 
			/// </summary>
			Expanding, 
			/// <summary>
			/// The Group control is collapsing
			/// </summary>
			Collapsing };

		private GroupState groupState = GroupState.Static;
		private Icon _captionIcon;

		#endregion

		#region Events
		/// <summary>
		/// Event raised when the XPGroupBox size is changing
		/// </summary>
		public event SizeChangingHandler SizeChanging;

		/// <summary>
		/// Raisie the SizeChange event
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSizeChanging(EventArgs e)
		{
			// Only fires event if something is handling the event
			if (SizeChanging != null)
			{
				SizeChanging(this, e);
			}
		}

		/// <summary>
		/// Event raised when an Item is clicked
		/// </summary>
		public event ItemClickEventHandler ItemClicked;
		/// <summary>
		/// Raisie the SizeChange event
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnItemClicked(ItemClickEventArgs e)
		{
			// Only fires event if something is handling the event
			if (ItemClicked != null)
			{
				ItemClicked(this, e);
			}
		}
		#endregion

		#region Properties

		/// <summary>
		/// The Icon displayed in the caption.
		/// </summary>
		[Description("The Icon displayed in the caption."), 
		Category("Appearance")]
		public Icon CaptionIcon
		{
			get { return _captionIcon; }
			set { _captionIcon = value; }
		}

		/// <summary>
		/// Determines if the group is expanded by default at runtime.
		/// </summary>
		[Description("Determines if the group is expanded by default at runtime."), 
		DefaultValue(true),
		Category("Appearance")]
		public bool GroupExpanded
		{
			get { return controlExpanded; }
			set 
			{ 
				controlExpanded = value; 
				if (!this.DesignMode)
				{
					CalcHeight();
					this.Refresh();
				}
			}
		}

		/// <summary>
		/// Determines the radius of the curves at the top-left and top-right of the control caption.
		/// </summary>
		[Description("Determines the radius of the curves at the top-left and top-right of the control caption."), 
		DefaultValue(7),
		Category("Appearance")]
		public int CaptionCurveRadius
		{
			get { return captionCurveRadius; }
			set { captionCurveRadius = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the starting (light) color of the caption gradient fill.
		/// </summary>
		[Description("Determines the starting (light) color of the caption gradient fill."), 
		Category("Appearance")]
		public Color CaptionLeftColor
		{
			get { return captionLeftColor; }
			set { captionLeftColor = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the ending (dark) color of the caption gradient fill.
		/// </summary>
		[Description("Determines the ending (dark) color of the caption gradient fill."), 
		Category("Appearance")]
		public Color CaptionRightColor
		{
			get { return captionRightColor; }
			set { captionRightColor = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the starting (light) color of the pane gradient fill.
		/// </summary>
		[Description("Determines the starting (light) color of the pane gradient fill."), 
		Category("Appearance")]
		public Color PaneTopLeftColor
		{
			get { return paneTopLeftColor; }
			set { paneTopLeftColor = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the ending (dark) color of the pane gradient fill.
		/// </summary>
		[Description("Determines the ending (dark) color of the pane gradient fill."), 
		Category("Appearance")]
		public Color PaneBottomRightColor
		{
			get { return paneBottomRightColor; }
			set { paneBottomRightColor = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the color of the pane outline.
		/// </summary>
		[Description("Determines the color of the pane outline."), 
		Category("Appearance")]
		public Color PaneOutlineColor
		{
			get { return paneOutlineColor; }
			set { paneOutlineColor = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the font and style of the caption text.
		/// </summary>
		[Description("Determines the font and style of the caption text."), 
		Category("Appearance")]
		public Font CaptionFont
		{
			get { return captionFont; }
			set { captionFont = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the color of the caption font.
		/// </summary>
		[Description("Determines the color of the caption font."), 
		Category("Appearance")]
		public Color CaptionFontColor
		{
			get { return captionFontColor; }
			set { captionFontColor = value; Invalidate(); }
		}

		/// <summary>
		/// Determines the highlight color of the caption font.
		/// </summary>
		[Description("Determines the highlight color of the caption font."), 
		Category("Appearance")]
		public Color CaptionFontHighLightColor
		{
			get { return captionFontHighLightColor; }
			set { captionFontHighLightColor = value; Invalidate(); }
		}

		/// <summary>
		/// The text contained in the caption.
		/// </summary>
		[Description("The text contained in the caption."), 
		Category("Appearance")]
		public string CaptionText
		{
			get { return captionText; }
			set { captionText = value; Invalidate(); }
		}

		private XPGroupBoxItemCollection _xpItems = new XPGroupBoxItemCollection();
		/// <summary>
		/// Collection containing all the XPGroupBoxItems for the control
		/// </summary>
		[
		RefreshProperties(RefreshProperties.Repaint),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Category("Data"),
		Browsable(true),
		Description("Collection containing all the XPGroupBoxItems for the control."),
		Editor("SpottyDogSoftware.Controls.XPGroupBoxItemCollectionEditor", "System.Drawing.Design.UITypeEditor")
		]
		public XPGroupBoxItemCollection XPGroupBoxItemsList
		{
			get
			{
				return _xpItems;
			}
		}

		#endregion

		#region Constructors
		public XPGroupBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.ContainerControl, true);

			this.BackColor = Color.Transparent;			
	
			// Determine what the item height should be
			// by adding 30% padding after measuring
			// the letter A with the selected font.
			Graphics g = this.CreateGraphics();
			itemHeight = (int)(g.MeasureString("A", this.Font).Height * 1.3);
			if (itemHeight < Consts.ItemIconSize + 2)
				itemHeight = Consts.ItemIconSize + 2;
			g.Dispose();
			_xpItems.CollectionUpdated += new CollectionUpdatedEventHandler(_xpItems_CollectionUpdated);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.timer1 = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			// 
			// timer1
			// 
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// XPGroupBox
			// 
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.Name = "XPGroupBox";
			this.Load += new System.EventHandler(this.XPGroupBox_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.XPGroupBox_MouseUp);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.XPGroupBox_MouseMove);
			this.MouseLeave += new System.EventHandler(this.XPGroupBox_MouseLeave);
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			if (DesignMode)
			{
				CalcHeight();
			}
			Rectangle rc = new Rectangle(0, 0, this.Width, Consts.CaptionHeight);
			LinearGradientBrush b = new LinearGradientBrush(rc, captionLeftColor, captionRightColor, 
				LinearGradientMode.Horizontal);
			Size size = e.Graphics.MeasureString(captionText, captionFont).ToSize();

			// Now draw the caption areas with the rounded corners at the top
			GraphicsPath path = new GraphicsPath();
			path.AddLine(captionCurveRadius, Consts.CaptionOffset, this.Width - captionCurveRadius - 1, Consts.CaptionOffset);
			path.AddArc(this.Width - captionCurveRadius*2 - 1, Consts.CaptionOffset, captionCurveRadius*2, captionCurveRadius*2, 270, 90);
			path.AddLine(this.Width-1, Consts.CaptionOffset + captionCurveRadius, this.Width -1 , Consts.CaptionOffset + Consts.CaptionHeight );
			path.AddLine(this.Width -1 , Consts.CaptionOffset + Consts.CaptionHeight - 1,  0 , Consts.CaptionOffset + Consts.CaptionHeight  - 1);
			path.AddLine(0 , Consts.CaptionOffset + Consts.CaptionHeight, 0, Consts.CaptionOffset + captionCurveRadius);
			path.AddArc(0 , Consts.CaptionOffset, captionCurveRadius*2, captionCurveRadius*2, 180, 90);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.FillPath(b, path);

			// Draw the outline around the work area
			Point start = new Point(0, Consts.CaptionHeight + Consts.CaptionOffset);
		    Size workAreaSize = new Size(this.Width -1, this.Height-1 - start.Y);

			Rectangle workAreaRect = new Rectangle(start, workAreaSize);

			if ( this.Height > Consts.CaptionHeight + Consts.CaptionOffset)
			{
				e.Graphics.DrawRectangle(new Pen(paneOutlineColor), workAreaRect);
			}
			// Draw the pseudo button indicating caption state
			int psuedoButtonDiameter = 14;
			Point psuedoButtonorigin = new Point(this.Width - psuedoButtonDiameter - 5, 5 + Consts.CaptionOffset);
			Size psuedoButtonSize = new Size(psuedoButtonDiameter, psuedoButtonDiameter);
			Rectangle psuedoButtonRect = new Rectangle(psuedoButtonorigin, psuedoButtonSize);
			e.Graphics.FillEllipse(new SolidBrush(captionLeftColor), psuedoButtonRect);
			e.Graphics.DrawEllipse(new Pen(CaptionFontColor), psuedoButtonRect);

			DrawChevrons(e.Graphics, psuedoButtonRect.X, psuedoButtonRect.Y+1, psuedoButtonRect.Width/4);

			// Draw the caption text
			if (captionHighlighted)
			{
				e.Graphics.DrawString(captionText , captionFont, new SolidBrush(captionFontHighLightColor),
					Consts.CaptionIndent, (Consts.CaptionHeight  - 2 - size.Height) / 2 + Consts.CaptionOffset);
			}
			else
			{
				e.Graphics.DrawString(captionText , captionFont, new SolidBrush(captionFontColor),
					Consts.CaptionIndent, (Consts.CaptionHeight  - 2 - size.Height) / 2 + Consts.CaptionOffset);
			}

			// Draw the caption icon
			if (_captionIcon != null)
			{
				Rectangle rect = new Rectangle(1, 1, 32, 32);
				e.Graphics.DrawIcon(_captionIcon, rect);
			}

			// Draw the items
			int offset = 0;
			Brush brush = new SolidBrush(this.ForeColor);
			Brush highLightBrush = new SolidBrush(this.captionFontHighLightColor);
			Brush disabledBrush = new SolidBrush(SystemColors.GrayText);
			e.Graphics.SetClip(workAreaRect);
			foreach (XPGroupBoxItem xpItem in _xpItems)
			{
				RectangleF rectf = new RectangleF(
					Consts.CaptionIndent, 
					Consts.FirstItemOffset + (offset * this.itemHeight) - (controlHeight - this.Height),
					this.Width - Consts.CaptionOffset - 2, 
					this.itemHeight);

				if (xpItem.Enabled)
				{
					xpItem.Region = new Region(new Rectangle(
						Consts.ItemIconOffset, 
						Consts.FirstItemOffset + (offset * this.itemHeight) - (controlHeight - this.Height),
						this.Width - Consts.ItemIconOffset - 2,
						this.itemHeight));
					if (xpItem.Equals(activeItem))
					{
						e.Graphics.DrawString(xpItem.Text, this.Font, highLightBrush, rectf);
					}
					else
					{
						e.Graphics.DrawString(xpItem.Text, this.Font, brush, rectf);
					}
				}
				else
				{
					e.Graphics.DrawString(xpItem.Text, this.Font, disabledBrush, rectf);
				}

				if (xpItem.Icon != null)
				{
					Rectangle iconRect = new Rectangle(
						Consts.ItemIconOffset, 
						Consts.FirstItemOffset + (offset * this.itemHeight) - (controlHeight - this.Height),
						16 ,
						16);
					if (xpItem.Enabled)
					{
						e.Graphics.DrawImage(xpItem.Icon.ToBitmap(), iconRect);
					}
					else
					{
						Icon i = new Icon(xpItem.Icon, 16, 16);
						ControlPaint.DrawImageDisabled(e.Graphics, i.ToBitmap(), Consts.ItemIconOffset,
							Consts.FirstItemOffset + (offset * this.itemHeight) - (controlHeight - this.Height),
							this.PaneTopLeftColor);
					}
				}
				offset++;
			}
			b.Dispose();
			brush.Dispose();
			disabledBrush.Dispose();
			highLightBrush.Dispose();
		}

		private void DrawChevrons(Graphics g, int x, int y, int offset)
		{
			// Determine the orientation of the pseudo-button
			if (!expanded)
			{
				DrawChevron(g, x + offset + 1, y + 1*offset, -offset);
				DrawChevron(g, x + offset + 1, y + 2*offset, -offset);
			}
			else
			{
				DrawChevron(g, x + offset + 1, y + 2*offset, offset);
				DrawChevron(g, x + offset + 1, y + 3*offset, offset);
			}   
		}

		private void DrawChevron(Graphics g, int x, int y, int offset)
		{
			Pen p;
			if (captionHighlighted)
			{
				p = new Pen(captionFontHighLightColor);
			}
			else
			{
				p = new Pen(CaptionFontColor);
			}
			Point[] points = { new Point(x, y),
								 new Point(x+Math.Abs(offset), y-offset),
								 new Point(x+2*Math.Abs(offset), y)
							 };
			g.DrawLines(p, points);
			p.Dispose();
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
			if (this.Height > Consts.CaptionHeight)
			{
				Rectangle rect = new Rectangle(0, Consts.CaptionHeight + Consts.CaptionOffset, this.Width, this.Height - Consts.CaptionHeight); 

				LinearGradientBrush b = new LinearGradientBrush(rect, paneTopLeftColor, paneBottomRightColor, 
					LinearGradientMode.ForwardDiagonal);

				pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				pevent.Graphics.FillRectangle(b, rect);
				b.Dispose();
			}
		}

		private void XPGroupBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Y < Consts.CaptionHeight + Consts.CaptionOffset)
			{
				// Mouse click on caption
				captionHighlighted = true;
				Cursor.Current = Cursors.Hand;
			}
			else
			{
				// Mouse click elsewhere on control
				captionHighlighted = false;
				Cursor.Current = Cursors.Default;
				// Now iterate through list items
				activeItem = null;
				foreach (XPGroupBoxItem item in _xpItems)
				{
					if (item.Region != null && item.Region.IsVisible(e.X, e.Y))
					{
						activeItem = item;
						Cursor.Current = Cursors.Hand;
						break;
					}
				}
			}
			this.Invalidate();
		}

		private void XPGroupBox_MouseLeave(object sender, System.EventArgs e)
		{
			// Ensure caption returned to non-highlighted condition
			captionHighlighted = false;
			Cursor.Current = Cursors.Default;
			this.Invalidate();
		}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			// Initializes the transition delta
			if (transitionSizeDelta == 0)
			{
				transitionSizeDelta = 1;
			}

			// Reduces the interval between timer events - this gives the visual effect of the 
			// control slowly starting to collapse/expand then accelertaing
			if (timer1.Interval > 20)
			{
				timer1.Interval -= 20;
			}
			else
			{
				transitionSizeDelta+=2;
			}

			// Initialises the control transaprency
			if (transitionAlphaChannel == 0)
			{
				transitionAlphaChannel = 10;
			}
			else
			{
				if ( transitionAlphaChannel + 10 < 255)
				{
					// Increase control transparency as it collapses
					transitionAlphaChannel += 10;
				}
			}
	
			switch (groupState)
			{
				case GroupState.Expanding:
					if ((this.Height + transitionSizeDelta)< controlHeight )
					{
						SetControlsOpacity(transitionAlphaChannel);
						paneBottomRightColor = Color.FromArgb(transitionAlphaChannel, paneBottomRightColor);
						paneTopLeftColor = Color.FromArgb(transitionAlphaChannel, paneTopLeftColor);
						paneOutlineColor = Color.FromArgb(transitionAlphaChannel, paneOutlineColor);
						this.Height += transitionSizeDelta;
						SetControlsVisible();
					}
					else
					{
						SetControlsOpacity(255);
						paneBottomRightColor = Color.FromArgb(255 , paneBottomRightColor);
						paneTopLeftColor = Color.FromArgb(255 , paneTopLeftColor);
						paneOutlineColor = Color.FromArgb(255 , paneOutlineColor);
						transitionAlphaChannel = 0;
						this.Height = controlHeight;
						expanded = true;
						controlExpanded = true;
						groupState = GroupState.Static;
						SetControlsVisible();
					}
					break;
				
				case GroupState.Collapsing:
					if ((this.Height - transitionSizeDelta ) > Consts.CaptionHeight + Consts.CaptionOffset)
					{
						SetControlsOpacity(transitionAlphaChannel);
						this.Height -= transitionSizeDelta;
						paneBottomRightColor = Color.FromArgb(255 - transitionAlphaChannel, paneBottomRightColor);
						paneTopLeftColor = Color.FromArgb(255 - transitionAlphaChannel, paneTopLeftColor);
						paneOutlineColor = Color.FromArgb(255 - transitionAlphaChannel, paneOutlineColor);
						SetControlsVisible();
					}
					else
					{
						transitionAlphaChannel = 0;
						SetControlsOpacity(0);
						paneBottomRightColor = Color.FromArgb(0, paneBottomRightColor);
						paneTopLeftColor = Color.FromArgb(0, paneTopLeftColor);
						paneOutlineColor = Color.FromArgb(0, paneOutlineColor);
						this.Height = Consts.CaptionHeight + Consts.CaptionOffset;
						expanded = false;
						controlExpanded = false;
						groupState = GroupState.Static;
						SetControlsVisible();
					}
					break;
				
				case GroupState.Static:
					timer1.Enabled = false;
					transitionSizeDelta = 0;
					break;
			
				default:
					throw new InvalidExpressionException("groupState variable set to incorrect value");
			}

			Invalidate();	
			OnSizeChanging(new EventArgs());
		}
			
		private void SetControlsVisible()
		{
//			foreach (Control c in this.Controls)
//			{
//				if (c.Top < Consts.CaptionHeight)
//				{
//					c.Visible = false;
//				}
//				else
//				{
//					c.Visible = true;
//				}
//			}
		}

		private void SetControlsOpacity(int opacity)
		{
			foreach (Control c in this.Controls)
			{
				if (!(c is TextBox || c is ComboBox) )
				{
					switch (groupState)
					{
						case GroupState.Expanding:
							if (c.BackColor != Color.Transparent)
							{
								c.BackColor = Color.FromArgb(opacity, c.BackColor);
							}
							c.ForeColor = Color.FromArgb(opacity, c.ForeColor);
							break;
				
						case GroupState.Collapsing:
							if (c.BackColor != Color.Transparent)
							{
								c.BackColor = Color.FromArgb(255-opacity, c.BackColor);
							}
							c.ForeColor = Color.FromArgb(255-opacity, c.ForeColor);
							break;
				
						case GroupState.Static:
							break;
			
						default:
							throw new InvalidExpressionException("groupState variable set to incorrect value");
					}
				}
			}
		}

		private void XPGroupBox_Load(object sender, System.EventArgs e)
		{
			controlHeight = this.Height;

			if (!this.DesignMode && !controlExpanded)
			{
				this.Height = Consts.CaptionHeight + Consts.CaptionOffset;
				expanded = false;
			}
			else if (controlExpanded && !this.DesignMode)
			{
				this.Height = Consts.FirstItemOffset +
					(_xpItems.Count * this.itemHeight);
				controlHeight = this.Height;
			}
			SetControlsVisible();
			this.Refresh();
		}

		private void CalcHeight()
		{
			controlHeight = this.Height;

			if (!this.DesignMode && !controlExpanded)
			{
				this.Height = Consts.CaptionHeight + Consts.CaptionOffset;
				expanded = false;
			}
			else if (controlExpanded)
			{
				this.Height = Consts.FirstItemOffset +
					(_xpItems.Count * this.itemHeight) + Consts.ItemOffset;
				controlHeight = this.Height;
			}
		}

		
		private void _xpItems_CollectionUpdated(object sender, EventArgs e)
		{
			Debug.WriteLine(string.Format("Collection Updated called @ {0}", DateTime.Now.ToLocalTime()));
			CalcHeight();
			this.Invalidate();;
		}


		private void XPGroupBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Y > Consts.CaptionHeight || groupState != GroupState.Static)
			{
				foreach (XPGroupBoxItem item in _xpItems)
				{
					if (item.Region != null && item.Region.IsVisible(e.X, e.Y))
					{
						OnItemClicked(new ItemClickEventArgs(item));
						break;
					}
				}
				return;
			}
			ChangeGBSTate();
		}

		private void ChangeGBSTate()
		{
			if (expanded)
			{
				Collapse();
			}
			else
			{
				Expand();
			}
		}

		/// <summary>
		/// Collapse the XPGroupBox. Does nothing if the XPGroupBox is collapsed
		/// </summary>
		public void Collapse()
		{
			if (expanded)
			{
				groupState = GroupState.Collapsing;
				timer1.Interval = 100;
				timer1.Enabled = true;
			}
		}

		/// <summary>
		/// Expand the XPGroupBox. Does nothing if the XPGroupBox is expanded
		/// </summary>
		public void Expand()
		{
			if (!expanded)
			{
				groupState = GroupState.Expanding;
				timer1.Interval = 100;
				timer1.Enabled = true;
			}
		}

	}



	/// <summary>
	/// Item Click Event Argument class
	/// </summary>
	public class ItemClickEventArgs : EventArgs
	{
		private XPGroupBoxItem _item;
		/// <summary>
		/// XPGroupBoxItem clicked
		/// </summary>
		public XPGroupBoxItem Item
		{
			get { return _item; }
		}

		/// <summary>
		/// Create an instance of the ItemClickEventArgs class
		/// </summary>
		/// <param name="item">XPGroupBoxItem clicked</param>
		public ItemClickEventArgs(XPGroupBoxItem item)
		{
			_item = item;
		}
		#endregion
	}

}
