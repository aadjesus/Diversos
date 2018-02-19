//****************************************************************
// This class is used to create a custom control. This custom control
// draw a reactagle. The width and height properties of this control can be
// modified using a custom property editor.
//****************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;

namespace CustomEditor
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class RectangleControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int width = 10;
		private int height = 10;
		/// <summary>
		/// This property is used to set the width of the rectangle.
		/// </summary>
		/// 
		[Description("Width of the rectangle"),Editor(typeof(WidthEditor),typeof(UITypeEditor))]
		public int RectWidth
		{
			get
			{
				return this.width;
			}
			set
			{
				if( value > 0)
				{
					this.width = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// This property is used to set the height of the rectangle.
		/// </summary>
		///
		[Description("Height of the rectangle"),Editor(typeof(WidthEditor),typeof(UITypeEditor))]
		public int RectHeight
		{
			get
			{
				return this.height;
			}
			set
			{
				if( value > 0)
				{
					this.height = value;
					this.Invalidate();
				}
			}
		}

		public RectangleControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call

		}

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
			// 
			// RectangleControl
			// 
			this.Name = "RectangleControl";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.RectangleControl_Paint);

		}
		#endregion

		private void RectangleControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Brush br = new SolidBrush(Color.Blue);
			e.Graphics.FillRectangle(br, 0,0,this.RectWidth,this.RectHeight);
		}
	}
}
