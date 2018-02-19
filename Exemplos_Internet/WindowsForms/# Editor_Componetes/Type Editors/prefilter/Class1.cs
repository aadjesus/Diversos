using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

namespace WellFormed
{

	/// <summary>
	/// Summary description for AControl.
	/// </summary>
	[Designer(typeof(AControlDesigner))] //associate the designer with the control
	public class AControl : Control
	{

		private TimeSpan _usableTime;

		/// <summary>
		/// The period of time after which the control will become unusable
		/// </summary>
		public TimeSpan UsableTime
		{
			get
			{
				return _usableTime;
			}
			set
			{
				_usableTime = value;
				EnableTimeout=EnableTimeout;
			}
		}		Timer TimeoutTimer;		bool _enableTimeout;		/// <summary>
		/// The control will time out and become unusable after a period of time
		/// </summary>		public bool EnableTimeout		{
			get 
			{
				return _enableTimeout;
			}
			set 
			{
				_enableTimeout=value;
				if (TimeoutTimer!=null)
					TimeoutTimer.Dispose();
				if(value && this._usableTime>new TimeSpan(0,0,0,0,0))
				{
					TimeoutTimer=new Timer();
					TimeoutTimer.Interval=(int)_usableTime.TotalMilliseconds;
					TimeoutTimer.Tick+=new EventHandler(TimeoutTimer_Tick);
					TimeoutTimer.Enabled=true;
				}
			}
		}

		public AControl()
		{
			//Because the BackColor is always PowderBlue the BackColor property must be removed.
			this.BackColor=Color.PowderBlue;
		}

		private void TimeoutTimer_Tick(object sender, EventArgs e)
		{
			TimeoutTimer.Enabled=false;
			this.Enabled=false;
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.Enabled==false)
			{
				e.Graphics.DrawLine(Pens.Red,0,0,this.Width,this.Height);
				e.Graphics.DrawLine(Pens.Red,this.Width,0,0,this.Height);
			}
			base.OnPaint (e);
		}


	}

	public class AControlDesigner : ControlDesigner
	{
  
		public AControlDesigner()
		{
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			base.PreFilterProperties(properties);
		
			//Remove the BackColor property from the list of available properties.
			properties.Remove("BackColor");
		}

		protected override void PostFilterProperties(System.Collections.IDictionary properties)
		{
			base.PostFilterProperties (properties);

			//The properties that would cause the control to cease functioning
			//in the designer must be replaced with harmless copies.

			//EnableTimeout is replaced with the designers harmless one
			properties["EnableTimeout"]=TypeDescriptor.CreateProperty(
				typeof(AControlDesigner),
				(PropertyDescriptor)properties["EnableTimeout"],
				new Attribute[0]);

			//UsableTime is also replaced
			properties["UsableTime"]=TypeDescriptor.CreateProperty(
				typeof(AControlDesigner),
				(PropertyDescriptor)properties["UsableTime"],
				new Attribute[0]);

		}



		public TimeSpan UsableTime
		{
			get 
			{
				return (TimeSpan)ShadowProperties["UsableTime"];
			}
			set 
			{
				ShadowProperties["UsableTime"]=value;
			}
		}

		public bool EnableTimeout
		{
			get
			{
				return (bool)ShadowProperties["EnableTimeout"];
			}
			set
			{
				 ShadowProperties["EnableTimeout"]= value;
			}
		}
		public override void Initialize(IComponent component)
		{
			base.Initialize (component);

			AControl c = component as AControl;
			if (component==null)
				throw new ArgumentException();

			ShadowProperties["UsableTime"]=c.UsableTime;
			ShadowProperties["EnableTimeout"]=c.EnableTimeout;

			c.EnableTimeout=false;
			c.UsableTime=new TimeSpan(0,0,0,0,0);
		}
	}


}
