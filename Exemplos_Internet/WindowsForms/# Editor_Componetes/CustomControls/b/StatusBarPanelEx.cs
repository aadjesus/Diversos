using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

namespace CustomControls
{
	internal interface IStatusBarPanelExRefresh
	{
		void Refresh();
	}

	public enum StatusBarPanelStyleEx
	{
		OwnerDraw=0,
		Text,
		Date,
		Time,
		SmoothProgressBar,
		HatchedProgressBar
	}

	/// <summary>
	/// Author	: Moditha Kumara
	/// Date	: 27/5/2004
	/// 
	/// Extended Statusbar panel. This has build in support for:
	/// A smooth progressbar. 
	/// A Pattern progressbar. 
	/// Date
	/// Time
	/// 
	/// Microsoft Knowledge Base Article 323116 have been usefull when implementing
	/// the smooth progressbar.
	/// 
	/// If you do any updates please send me a copy.
	/// </summary>
	[DesignTimeVisible(false),ToolboxItem(false)]
	[DesignerCategory("Component")]
	public class StatusBarPanelEx : System.Windows.Forms.StatusBarPanel,CustomControls.IStatusBarPanelExRefresh
	{
		#region Instance_Variables
		private int m_Minimum = 1;
		private int m_Maximum = 100;
		private int m_Value = 0;
		private Color m_Color;
		private HatchStyle hatchStyle;
		private StatusBarPanelStyleEx style;
		#endregion Instance_Variables
	
		#region Properties
		public StatusBarPanelEx()
		{
			this.Style = StatusBarPanelStyleEx.Text;
		}

		[Category("ProgressBar Panel")]
		[Description("Minimum value the progress bar can have. (If this panel acts as a progress bar)")]
		public int Minimum
		{
			get { return m_Minimum; }
			set
			{ 
				// Prevent a negative value.
				if (value < 0)
				{
					m_Minimum = 0;
				}
		
				// Make sure that the minimum value is never set higher than the maximum value.
				if (value > m_Minimum)
				{
					m_Minimum = value;
					m_Minimum = value;
				}
		
				// Ensure value is still in range
				if (m_Value < m_Minimum)
				{
					m_Value = m_Minimum;
				}
			}
		}


		[Category("ProgressBar Panel")]		
		[Description("Maximum value the progress bar can have. (If this panel acts as a progress bar)")]
		public int Maximum
		{
			get { return m_Maximum; }
			set
			{ 
				// Make sure that the maximum value is never set lower than the minimum value.
				if (value < m_Minimum)
				{
					m_Minimum = value;
				}

				m_Maximum = value;

				// Make sure that value is still in range.
				if (m_Value > m_Maximum)
				{
					m_Value = m_Maximum;
				}
			}
		}
        	

		[Category("ProgressBar Panel")]
		[Description("Value of the progress bar. (If this panel acts as a progress bar)")]
		public int Value
		{
			get { return m_Value; }
			set { m_Value = value; }
		}


		[Category("ProgressBar Panel")]
		[Description("Progress bar color. (If this panel acts as a progress bar)")]
		public Color ForeColor
		{
			get { return m_Color; }
			set { m_Color = value; }
		}


		[Category("ProgressBar Style")]
		[Description("Style of the Hatched progress bar. Drawing2D.HatchStyles are available.")]
		public HatchStyle HatchedProgressBarStyle
		{
			get { return hatchStyle; }
			set { hatchStyle = value; }
		}


		[Category("Appearance")]
		public new StatusBarPanelStyleEx Style
		{
			get{return style;}
			set
			{
				style=value;
				//set the base style
				if ( 
					(style==StatusBarPanelStyleEx.OwnerDraw) || 
					(style == StatusBarPanelStyleEx.SmoothProgressBar) ||
					(style == StatusBarPanelStyleEx.HatchedProgressBar)
					)
				{
					base.Style=StatusBarPanelStyle.OwnerDraw;
				}
				else
				{
					base.Style=StatusBarPanelStyle.Text;
				}

			}
		}

		#endregion Properties

		public void Refresh()
		{
			DateTime dt=System.DateTime.Now;

			if (style==StatusBarPanelStyleEx.Date)
			{
				this.Text = dt.ToString("d", System.Threading.Thread.CurrentThread.CurrentUICulture);
			}
			if (style==StatusBarPanelStyleEx.Time)
			{
				this.Text = dt.ToString("T", System.Threading.Thread.CurrentThread.CurrentUICulture);
			}
		}
	}	
}
