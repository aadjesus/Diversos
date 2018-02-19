using System;
using System.Windows.Forms;
using System.Drawing;

namespace CustomControls.BaseClasses
{
	
	public  class AppColors
	{
		private static Color defaultHighlightColor=SystemColors.Highlight;
		private static Color defaultControlColor=SystemColors.Control;
		private static Color defaultControlDarkColor=SystemColors.ControlDark;
		private static Color defaultTextColor=SystemColors.WindowText;
		private static Color defaultWindowColor= SystemColors.Window;
		
		private static Color defaultHeaderBackColor=System.Drawing.Color.SteelBlue;
		private static Color defaultHeaderForeColor= System.Drawing.Color.GhostWhite;
		
		private static Color defaultGridBackColor= System.Drawing.Color.Lavender;
		private static Color defaultGridAltBackColor=System.Drawing.Color.LightSteelBlue;
		private static Color defaultGridForeColor=System.Drawing.Color.Black;
		private static Color defaultGridSelectionColor= System.Drawing.Color.FromArgb(((System.Byte)(48)), ((System.Byte)(100)), ((System.Byte)(155)));
		private static Color defaultGridSelectionForeColor= System.Drawing.Color.GhostWhite;
		private static Color defaultGridLineColor=System.Drawing.Color.SlateGray;

		

		public AppColors()
		{
			
		}

		public static void SetDefaultColors( Color Highlight,Color Control, Color ControlDark,Color Text, Color Window)
		{
			defaultHighlightColor=Highlight;
			defaultControlColor=Control;
			defaultControlDarkColor=ControlDark;
			defaultTextColor=Text;
			defaultWindowColor=Window;
		}

		public static void SetDefaultColors( Color Highlight)
		{
			defaultHighlightColor=Highlight;
		}

        
		public static Color HeaderBackColor
		{
			get{return defaultHeaderBackColor;}
		}

		public static Color HeaderForeColor
		{
			get{return defaultHeaderForeColor;}
		}

		public static Color GridBackColor
		{
			get{return defaultGridBackColor;}
		}

		public static Color GridForeColor
		{
			get{return defaultGridForeColor;}
		}
		public static Color GridAltBackColor
		{
			get{return defaultGridAltBackColor;}
		}

		public static Color GridSelectionColor
		{
			get{return defaultGridSelectionColor;}
		}

		public static Color GridSelectionForeColor
		{
			get{return defaultGridSelectionForeColor;}
		}

		public static Color GridLineColor
		{
			get{return defaultGridLineColor;}
		}

		public static Color HighlightColorDarkDark
		{
			get { return  defaultHighlightColor; }
		}

		public static Color HighlightColorDark
		{
			get { return Color.FromArgb(130, HighlightColorDarkDark); }
		}

		public static Color HighlightColorLight
		{
			get { return Color.FromArgb(50, HighlightColorDarkDark); }
		}

		public static Color HighlightColor
		{
			get { return Color.FromArgb(80,HighlightColorDarkDark); }
		}

		public static Color ControlColor
		{
			get { return  defaultControlColor; }
		}
		
		
		public static Color ControlColorDark
		{
			get { return  defaultControlDarkColor; }
		}

		public static Color ToolbarColorLight
		{
			get{ return ControlPaint.LightLight(ControlColor);}
		}

		public static Color ToolbarBackColor
		{
			get{ return ControlPaint.Light(ControlColor);}
		}

		public static Color ToolbarColorDark
		{
			get{ return ControlPaint.Dark(ControlColor);}
		}
		
		public static Color TextColor
		{
			get{ return defaultTextColor;}
		}
		public static Color WindowColor
		{
			get{ return defaultWindowColor;}
		}

	}
}
