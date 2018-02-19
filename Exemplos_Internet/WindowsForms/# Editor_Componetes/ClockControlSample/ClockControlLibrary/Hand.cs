#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;

#endregion

namespace ClockControlLibrary
{

	[TypeConverter(typeof(HandConverter))]
	//[Editor(typeof(HandEditor), typeof(UITypeEditor))]
	public class Hand
	{
		private Color _color = Color.Black;
		private int _width = 1;
		public Hand() { }
		public Hand(Color color, int width)
		{
			_color = color;
			_width = width;
		}

		[Description("Sets the color of the clock Hand.")]
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		[Description("Sets the width of the clock Hand.")]
		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}
	}
}
