using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
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
	[ToolboxBitmap(typeof(StatusBarEx))] 
	public class StatusBarEx : System.Windows.Forms.StatusBar
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private StatusBarPanelExCollection panels=null;
		private Timer timer=null;
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TextBoxEx
			// 
			this.Name = "StatusBarEx";
		}
		#endregion

		public StatusBarEx()
		{
			this.components = new Container();
			this.panels=new StatusBarPanelExCollection(this);
			this.SizingGrip = false;
			this.ShowPanels = true;

			//Internal timer used to update date/time panel(s)
			timer=new Timer(components);
			timer.Interval=1000;//1 second
			timer.Tick += new EventHandler(TimerEventProcessor);
			timer.Enabled=true;
		}
        	

		[Description("Update any progress bar panel(s) within this statusbar.")]
		public void UpdateValue()
		{
			IEnumerator col = this.Panels.GetEnumerator();

			while(col.MoveNext())
			{
				StatusBarPanelEx c = (StatusBarPanelEx)col.Current;
				if (c.Style.ToString().EndsWith("ProgressBar"))
				{
					c.Value++;
					this.Invalidate(true);
				}
			}
		}

		[Description("Update given progress bar panels value to the new value.")]
		public void UpdateValue(StatusBarPanelEx panel, int NewValue)
		{
			panel.Value = NewValue;
			this.Invalidate(true);
		}

		[Description("Update given progress bar panels value by one.")]
		public void UpdateValue(StatusBarPanelEx panel)
		{
			panel.Value++;
			this.Invalidate(true);
		}


//		/// <summary>
//		/// Collection of StatusBarPanelEx panels
//		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Editor(typeof(StatusBarPanelExCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		public new StatusBarPanelExCollection Panels
		{
			get 
			{
				return this.panels;
			}
		}

		protected override void OnDrawItem(StatusBarDrawItemEventArgs e)
		{
			if(e.Panel.GetType().ToString().EndsWith("StatusBarPanelEx"))
			{
				StatusBarPanelEx ProgressPanel = (StatusBarPanelEx)e.Panel;

				//if this panel style!=ProgressBar? dont draw
				if (!(ProgressPanel.Style.ToString().EndsWith("ProgressBar")))
				{
					return;
				}

				//draw if progress bar
				if (ProgressPanel.Value > ProgressPanel.Minimum)
				{
					int NewWidth =
						(int)(((double)ProgressPanel.Value / (double)ProgressPanel.Maximum) *
						(double)ProgressPanel.Width);
					Rectangle NewBounds = e.Bounds;

					//select brush type
					Brush PaintBrush;
					if (ProgressPanel.Style == StatusBarPanelStyleEx.SmoothProgressBar)
					{
						PaintBrush = new SolidBrush(ProgressPanel.ForeColor);
					}
					else
					{
						PaintBrush = new HatchBrush(ProgressPanel.HatchedProgressBarStyle,ProgressPanel.ForeColor,this.Parent.BackColor);
					}

					NewBounds.Width = NewWidth;
	
					e.Graphics.FillRegion(PaintBrush, new Region(NewBounds));
					PaintBrush.Dispose();
				}
				else
				{
					base.OnDrawItem(e);
				}
			}
			else
			{
				base.OnDrawItem(e);
			}
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

		private void TimerEventProcessor(Object myObject,EventArgs myEventArgs)
		{
			IEnumerator col = this.Panels.GetEnumerator();

			while(col.MoveNext())
			{
				((CustomControls.IStatusBarPanelExRefresh)col.Current).Refresh();
			}
		}


		/// <summary>
		/// StatusBarPanelEx Collection.
		/// </summary>
		public class StatusBarPanelExCollection: StatusBar.StatusBarPanelCollection, IEnumerable
		{
			public StatusBarPanelExCollection(StatusBarEx owner):base(owner)
			{
			}
	
			public new StatusBarPanelEx this[int index]
			{
				get{return (StatusBarPanelEx)base[index];}
				set{base[index]=value;}
			}
		}
	}
}
