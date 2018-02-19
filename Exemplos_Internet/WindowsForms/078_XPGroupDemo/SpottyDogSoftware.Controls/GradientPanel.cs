using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Summary description for GradientPanel.
	/// </summary>
	[Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof(System.ComponentModel.Design.IDesigner))]
	public class GradientPanel : System.Windows.Forms.UserControl
	{
		#region Members
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Color paneTopLeftColor = Color.FromKnownColor(KnownColor.InactiveCaption);
		private Color paneBottomRightColor = Color.FromKnownColor(KnownColor.ActiveCaption);
		private Color paneOutlineColor = Color.FromKnownColor(KnownColor.ControlDarkDark);

		private const int groupBoxSpacing = 10;

		#endregion

		#region Properties
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
		#endregion

		#region Constructors
		public GradientPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			AutoScroll = true;
		}
		#endregion

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			// 
			// GradientPanel
			// 
			this.Name = "GradientPanel";
		}
		#endregion

		#region Methods

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			Rectangle rect = new Rectangle(0, AutoScrollPosition.Y, this.Width, 
				this.Height); 

			LinearGradientBrush b = new LinearGradientBrush(this.DisplayRectangle, paneTopLeftColor, paneBottomRightColor, 
				LinearGradientMode.Vertical);

			pevent.Graphics.FillRectangle(b, this.DisplayRectangle);
		}
		#endregion
	}
}
