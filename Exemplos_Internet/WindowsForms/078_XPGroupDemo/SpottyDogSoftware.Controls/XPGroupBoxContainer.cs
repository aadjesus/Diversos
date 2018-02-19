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
	/// Summary description for XPGroupBoxContainer.
	/// </summary>
	[Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof(System.ComponentModel.Design.IDesigner))]
	public class XPGroupBoxContainer : GradientPanel
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
		private XPGroupBoxCollection _xpGroupBoxes = new XPGroupBoxCollection();
		/// <summary>
		/// Collection containing all the XPGroupBox for the control
		/// </summary>
		[
		RefreshProperties(RefreshProperties.Repaint),
		Category("Data"),
		Browsable(true),
		Description("Collection containing all the XPGroupBoxs for the control."),
		Editor("SpottyDogSoftware.Controls.XPGroupBoxItemCollectionEditor", "System.Drawing.Design.UITypeEditor")
		]
		public XPGroupBoxCollection XPGroupBoxList
		{
			get
			{
				return _xpGroupBoxes;
			}
		}
		#endregion

		public XPGroupBoxContainer()
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
			// XPGroupBoxContainer
			// 
			this.Name = "XPGroupBoxContainer";
			this.Resize += new System.EventHandler(this.XPGroupBoxContainer_Resize);
			this.Load += new System.EventHandler(this.XPGroupBoxContainer_Load);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded (e);
			XPGroupBox xpg = e.Control as XPGroupBox;
			if (xpg != null)
			{
				if (!_xpGroupBoxes.Contains(xpg))
				{
					_xpGroupBoxes.Add(xpg);
					RepositionControls();
					xpg.SizeChanging += new SizeChangingHandler(XPGroupBoxContainer_SizeChanging);
					xpg.LocationChanged += new EventHandler(XPGroupBoxContainer_LocationChanged);
				}
			}
			else
			{
				throw new InvalidOperationException("Can only add XPGroupBoxControls");
			}
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved (e);
			if (e.Control is XPGroupBox)
			{
				_xpGroupBoxes.Remove((XPGroupBox)e.Control);
				RepositionControls();
			}
		}

		public void RepositionControls()
		{
			bool bVerticalScrollBarVisible = false;
			int lastVerticalPosition = AutoScrollPosition.Y;
			if (this.DisplayRectangle.Size.Height > this.ClientRectangle.Size.Height)
			{
				bVerticalScrollBarVisible = true;
			}
			foreach (Control c  in this.Controls)
			{
				XPGroupBox xpg = c as XPGroupBox;
				if (xpg != null)
				{
					xpg.Left = groupBoxSpacing;
					xpg.Top = lastVerticalPosition + groupBoxSpacing;
					lastVerticalPosition += xpg.Height + groupBoxSpacing ;
					xpg.Width = this.Width - 2 * groupBoxSpacing - 
						(bVerticalScrollBarVisible ? 16 : 0); 
				}
			}
			this.Invalidate();
		}

		private void XPGroupBoxContainer_Load(object sender, System.EventArgs e)
		{
			RepositionControls();
		}

		private void XPGroupBoxContainer_SizeChanging(Object sender, EventArgs e)
		{
			RepositionControls();
		}

		private void XPGroupBoxContainer_Resize(object sender, System.EventArgs e)
		{
			RepositionControls();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			RepositionControls();
		}

		/// <summary>
		/// Expand one XPGroupBox and collapse all others
		/// </summary>
		/// <param name="expandXPGroupBox">XPGroupBox to collapse</param>
		public void ExpandOnlyOneXPGroupBox(XPGroupBox expandXPGroupBox)
		{
			foreach (XPGroupBox xpg in _xpGroupBoxes)
			{
				if (xpg == expandXPGroupBox)
				{
					xpg.Expand();
				}
				else
				{
					xpg.Collapse();
				}
			}
		}

		private void XPGroupBoxContainer_LocationChanged(object sender, EventArgs e)
		{
			//RepositionControls();
		}
	}
}
