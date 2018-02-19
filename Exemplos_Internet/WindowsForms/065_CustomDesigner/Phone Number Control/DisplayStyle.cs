using System;
using System.Drawing;

namespace Phone_Number_Control
{
	/// <summary>
	/// Summary description for DisplayStyle.
	/// </summary>
	public class DisplayStyle
	{
		public DisplayStyle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private Color backgrdClr;
		private FontStyle  forestyle;

		public Color BackGroudColor
		{
			get
			{
				return this.backgrdClr;
			}
			set
			{
				
					this.backgrdClr = value;
				
			}
		}
		public FontStyle ForeStyle
		{
			get
			{
				return this.forestyle;
			}
			set
			{
				
					this.forestyle = value;
				
			}
		}
	}
}
