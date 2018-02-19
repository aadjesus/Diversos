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
	/// <summary>
	/// Summary description for EditEx.
	/// </summary>
	/// 

	public class EditEx : System.Windows.Forms.TextBox
	{
		#region Variable
		private DrawState   drawState = DrawState.Normal;
		private Color       borderColor = Color.FromName("HotTrack");
		#endregion

		#region Constructure
		public EditEx()
		{
			// Make sure we have the 3D look setting which is the one
			// we are going to paint over
			BorderStyle = BorderStyle.Fixed3D;
		}
		#endregion

		#region Property
		public new BorderStyle BorderStyle 
		{
			// Don't let the user change this property
			// because we are counting on the extra pixels
			// than the 3D look adds to the edit control size
			// to do our painting
			get { return base.BorderStyle; } 
			set 
			{
				if ( value != BorderStyle.Fixed3D )
				{
					// Throw an exception to tell the user
					// that this property needs to be Fixe3D if 
					// he is to use this class
					string message = "BorderStyle can only be Fixed3D for this class";
					ArgumentException argumentException = new ArgumentException("BorderStyle", message);
					throw(argumentException);
				}
				else 
					base.BorderStyle = value;
			}
		}
		
		public Color BoardColor
		{
			get{ return borderColor;}    
			set
			{
				if(borderColor != value)
				{
					borderColor = value;
					Invalidate();
				}
			}
		}
		#endregion

		#region Override
		protected override void OnMouseEnter(EventArgs e)
		{
			// Set state to hot
			base.OnMouseEnter(e);
			drawState = DrawState.Hot;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			// Set state to Normal
			base.OnMouseLeave(e);
			if ( !ContainsFocus )
			{
				drawState = DrawState.Normal;
				Invalidate();
			}
		}
      
		protected override void OnGotFocus(EventArgs e)
		{
			// Set state to Hot
			base.OnGotFocus(e);
			drawState = DrawState.Hot;
			Invalidate();
		}
        
		protected override void OnLostFocus(EventArgs e)
		{
			// Set state to Normal
			base.OnLostFocus(e);
			drawState = DrawState.Normal;
			Invalidate();
		}

		#endregion

		#region Helper
		void DrawTextBoxState(DrawState drawState)
		{
			// Get window area
			Win32.RECT rc = new Win32.RECT();
			WindowsAPI.GetWindowRect(Handle, ref rc);
			// Convert to Rectangle
			Rectangle rect = new Rectangle(0, 0, rc.right - rc.left, rc.bottom - rc.top);

			// Create DC for the whole edit window instead of just for the client area
			IntPtr hDC = WindowsAPI.GetWindowDC(Handle);
			
			using (Graphics g = Graphics.FromHdc(hDC))
			{
				// This rectangle is always drawn for any state
				using ( Pen windowPen = new Pen(SystemBrushes.Window) )
				{
					g.DrawRectangle(windowPen, rect.Left+1, rect.Top+1, rect.Width-3, rect.Height-3);
				}
				
				if ( drawState == DrawState.Normal )
				{
					// draw SystemColos.Window rectangle
					using ( Pen windowPen = new Pen(borderColor/*SystemBrushes.Window*/) )
					{
						g.DrawRectangle(windowPen, rect.Left, rect.Top, rect.Width-1, rect.Height-1);
					}
				}
				else if ( drawState == DrawState.Hot )
				{
					// draw highlighted rectangle
					g.DrawRectangle(SystemPens.Highlight, rect.Left, rect.Top, rect.Width-1, rect.Height-1);
				}
				else if ( drawState == DrawState.Disable )
				{
					// draw highlighted rectangle
					g.FillRectangle(SystemBrushes.Window, rect);
					Size textSize = TextUtil.GetTextSize(g, Text, Font);
					Point point = new Point(rect.Left+1, rect.Top + (rect.Height - textSize.Height)/2);
					g.DrawString(Text, Font, SystemBrushes.ControlDark, point); 
				}
			}

			// Release DC
			WindowsAPI.ReleaseDC(Handle, hDC);
            
		}

		#endregion

		#region WndProc
		protected override  void WndProc(ref Message m)
		{
			bool callBase = true;
						
			switch(m.Msg)
			{
				case ((int)Msg.WM_PAINT):
				{
					// Let the edit control do its painting
					base.WndProc(ref m);
					// Now do our custom painting
					DrawTextBoxState(Enabled?drawState:DrawState.Disable);
					callBase = false;
				}
					break;
				default:
					break;
			}

			if ( callBase )
				base.WndProc(ref m);
		
		}
		#endregion
	}

}
