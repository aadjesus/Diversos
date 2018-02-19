using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ClockControlLibrary
{

	[ToolboxBitmap(typeof(ClockControl), "ClockControl.bmp")]
	[DefaultEvent("SoundTheAlarm")]
	[DefaultProperty("ShowDigitalTime")]
	[ProvideProperty("TimeZoneModifier", typeof(Control))]
	[Designer(typeof(ClockControlDesigner))]
	public class ClockControl : Control, ISupportInitialize, IExtenderProvider
	{

		[Category("Banana")]
		[Description("Primary alarm for late risers.")]
		public void EditClockHands() { }

		private bool _initializing = false;
		private Timer _timer = new Timer();

		private bool _isItTimeForABreak = true;
		private DateTime _primaryAlarm = DateTime.Now;
		private DateTime _backupAlarm = DateTime.Now.AddMinutes(10);
		private ClockFace _face = ClockFace.Both;
		private Hand _hourHand = new Hand(Color.Black, 1);
		private Hand _minuteHand = new Hand(Color.Black, 1);
		private Hand _secondHand = new Hand(Color.Red, 1);
		private string _digitalTimeFormat = "dd/MM/yyyy hh:mm:ss tt";

		// Mapping of components to numeric timezone offsets
		Hashtable _timeZoneModifiers = new Hashtable();
		int _timeZoneModifier = 0;

		public ClockControl()
		{
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		#region ISupportInitialize Members

		void ISupportInitialize.BeginInit()
		{
			_initializing = true;
		}

		void ISupportInitialize.EndInit()
		{
			if (!this.DesignMode)
			{
				// Check alarm values
				if (_primaryAlarm >= _backupAlarm)
					throw new ArgumentOutOfRangeException("Primary alarm must be before Backup alarm");
				//Initialize _timer
				_timer.Interval = 1000;
				_timer.Tick += this.timer_Tick;
				_timer.Enabled = true;
			}
			_initializing = false;
		}

		#endregion

		#region IExtenderProvider Members

		bool IExtenderProvider.CanExtend(object extendee)
		{
			// Don't extend self
			if (extendee == this) return false;

			// Extend suitable controls
			return ((extendee is PictureBox) ||
					(extendee is Panel));
		}

		#endregion

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsItTimeForABreak
		{
			get { return _isItTimeForABreak; }
			set { _isItTimeForABreak = value; }
		}

		[Category("Behavior")]
		[Description("Primary alarm for late risers.")]
		public DateTime PrimaryAlarm
		{
			get { return _primaryAlarm; }
			set
			{

				if (!_initializing)
				{
					if (value >= _backupAlarm)
					{
						throw new ArgumentOutOfRangeException("Primary alarm must be before Backup alarm");
					}
				}

				_primaryAlarm = value;
				this.Invalidate();
			}
		}

		[Category("Behavior")]
		[Description("Backup alarm for very very late risers.")]
		public DateTime BackupAlarm
		{
			get { return _backupAlarm; }
			set
			{
				if (!_initializing)
				{
					if (value < _primaryAlarm)
					{
						throw new ArgumentOutOfRangeException("Backup alarm must be after Primary alarm");
					}
				}

				_backupAlarm = value;
				this.Invalidate();
			}
		}

		[Category("Appearance")]
		[Description("Determines the clock face type to display.")]
		[DefaultValue(ClockFace.Both)]
		[Editor(typeof(FaceEditor), typeof(UITypeEditor))]
		public ClockFace Face
		{
			get { return _face; }
			set
			{
				_face = value;
				this.Invalidate();
			}
		}

		[Category("Appearance")]
		[Description("Sets the color and width of the Hour Hand.")]
		public Hand HourHand
		{
			get { return _hourHand; }
			set
			{
				_hourHand = value;
				this.Invalidate();
			}
		}
		private bool ShouldSerializeHourHand()
		{
			// Only serialize non-default values
			return ((_hourHand.Color != Color.Black) || (_hourHand.Width != 1));
		}
		private void ResetHourHand()
		{
			_hourHand = new Hand(Color.Black, 1);
		}

		[Category("Appearance")]
		[Description("Sets the color and width of the Minute Hand.")]
		public Hand MinuteHand
		{
			get { return _minuteHand; }
			set
			{
				_minuteHand = value;
				this.Invalidate();
			}
		}
		private bool ShouldSerializeMinuteHand()
		{
			// Only serialize non-default values
			return ((_minuteHand.Color != Color.Black) || (_minuteHand.Width != 1));
		}
		private void ResetMinuteHand()
		{
			_minuteHand = new Hand(Color.Black, 1);
		}

		[Category("Appearance")]
		[Description("Sets the color and width of the Second Hand.")]
		public Hand SecondHand
		{
			get { return _secondHand; }
			set
			{
				_secondHand = value;
				this.Invalidate();
			}
		}
		private bool ShouldSerializeSecondHand()
		{
			// Only serialize non-default values
			return ((_secondHand.Color != Color.Red) || (_secondHand.Width != 1));
		}
		private void ResetSecondHand()
		{
			_secondHand = new Hand(Color.Red, 1);
		}

		[Category("Appearance")]
		[Description("The digital time format, constructed from .NET format specifiers.")]
		[DefaultValue("dd/MM/yyyy hh:mm:ss tt")]
		[Editor(typeof(DigitalTimeFormatEditor), typeof(UITypeEditor))]
		public string DigitalTimeFormat
		{
			get { return _digitalTimeFormat; }
			set
			{
				_digitalTimeFormat = value;
				this.Invalidate();
			}
		}

		[Category("Behavior")]
		[Description("Sets the timezone difference from the current time.")]
		[DefaultValue(0)]
		[DisplayName("TimeZoneModifier")]
		public int GetTimeZoneModifier(Control extendee)
		{
			// Return component's timezone offset
			return Convert.ToInt32(_timeZoneModifiers[extendee]);
		}
		public void SetTimeZoneModifier(Control extendee, object value)
		{
			// If property isn't provided
			if (value == null)
			{
				// Remove it
				_timeZoneModifiers.Remove(extendee);
				if (!this.DesignMode)
				{
					extendee.Click -= extendee_Click;
				}
			}
			else
			{
				// Add the offset as an integer
				_timeZoneModifiers[extendee] = Convert.ToInt32(value);
				if (!this.DesignMode)
				{
					extendee.Click += extendee_Click;
				}
			}
		}

		[Category("Notification")]
		[Description("Fired when the Alarm goes off.")]
		public event SoundTheAlarmEventHandler SoundTheAlarm;

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;

			// Get specified date/time if control in design-time, 
			// or current date/time if control is in run-time
			DateTime now;
			if (this.DesignMode)
			{
				// Get pretty date/time for design-time
				now = new DateTime(2002, 12, 31, 15, 00, 20, 0);
			}
			else
			{
				// Get current date/time and apply the time zone modifier
				now = DateTime.Now.AddHours(_timeZoneModifier);
			}

			// Calculate required dimensions
			Size _faceSize = this.DisplayRectangle.Size;
			int xRadius = _faceSize.Width / 2;
			int yRadius = _faceSize.Height / 2;
			double degrees;
			int x;
			int y;

			// Make things pretty
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// Paint either Digital or Analog _face
			using (Pen _facePen = new Pen(Color.Black, 2))
			{
				if (_face != ClockFace.Digital)
				{
					g.DrawEllipse(_facePen, _facePen.Width, _facePen.Width, _faceSize.Width - _facePen.Width * 2, _faceSize.Height - _facePen.Width * 2);
					using (SolidBrush _faceBrush = new SolidBrush(Color.White))
					{
						g.FillEllipse(_faceBrush, _facePen.Width, _facePen.Width, _faceSize.Width - _facePen.Width * 2, _faceSize.Height - _facePen.Width * 2);
					}
				}
				else
				{
					g.DrawRectangle(_facePen, 1, 1, _faceSize.Width + 1 - _facePen.Width * 2, _faceSize.Height + 1 - _facePen.Width * 2);
					using (SolidBrush _faceBrush = new SolidBrush(Color.White))
					{
						g.FillRectangle(_faceBrush, 1, 1, _faceSize.Width + 1 - _facePen.Width * 2, _faceSize.Height + 1 - _facePen.Width * 2);
					}
				}
			}

			// Paint analog clock?
			if (_face != ClockFace.Digital)
			{
				// Hour hand
				using (Pen _hourHandPen = new Pen(_hourHand.Color, _hourHand.Width))
				{
					degrees = (90.0 - ((now.Hour / 3.0) + (now.Minute / 180.0)) * 90.0) * (Math.PI / 180.0);
					x = (int)Math.Round((xRadius / 3.0) * Math.Cos(degrees));
					y = (int)-(Math.Round((yRadius / 3.0) * Math.Sin(degrees)));
					g.DrawLine(_hourHandPen, xRadius, yRadius, x + xRadius, y + yRadius);
				}

				// Minute hand
				using (Pen _minuteHandPen = new Pen(_minuteHand.Color, _minuteHand.Width))
				{
					degrees = (90.0 - (now.Minute / 15.0) * 90.0) * (Math.PI / 180.0);
					x = (int)Math.Round((xRadius / 2.0) * Math.Cos(degrees));
					y = (int)-(Math.Round((yRadius / 2.0) * Math.Sin(degrees)));
					g.DrawLine(_minuteHandPen, xRadius, yRadius, x + xRadius, y + yRadius);
				}

				// Second hand
				using (Pen _secondHandPen = new Pen(_secondHand.Color, _secondHand.Width))
				{
					degrees = (90.0 - (now.Second / 15.0) * 90.0) * (Math.PI / 180.0);
					x = (int)Math.Round((2.0 * xRadius / 3.0) * Math.Cos(degrees));
					y = (int)-(Math.Round((2.0 * yRadius / 3.0) * Math.Sin(degrees)));
					g.DrawLine(_secondHandPen, xRadius, yRadius, x + xRadius, y + yRadius);
				}
			}

			// Paint digital clock?
			if (_face != ClockFace.Analog)
			{
				StringFormat format = new StringFormat();
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Center;
				string nowFormatted = now.ToString(_digitalTimeFormat);
				using (Brush brush = new SolidBrush(this.ForeColor))
				{
					if (_face == ClockFace.Both)
					{
						g.DrawString(nowFormatted, this.Font, brush, xRadius, yRadius * 1.5F, format);
					}
					else
					{
						RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
						g.DrawString(nowFormatted, this.Font, brush, rect, format);
					}
				}
			}

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}

		private void timer_Tick(object sender, EventArgs e)
		{

			// Check to see whether we're within 1 second of the primary alarm
			double primarySeconds = (DateTime.Now - _primaryAlarm).TotalSeconds;
			if ((primarySeconds >= 0) && (primarySeconds <= 1))
			{
				DateTime alarm = _primaryAlarm;
				_primaryAlarm = DateTime.Now; // Reset alarm
				if (SoundTheAlarm != null)
				{
					// Sound alarm asynch so clock can keep ticking
					SoundTheAlarm.BeginInvoke(this, new SoundTheAlarmEventArgs(_primaryAlarm, AlarmType.Primary), null, null);
				}
			}

			// Check to see whether we're within 1 second of the backup alarm
			double backupSeconds = (DateTime.Now - _backupAlarm).TotalSeconds;
			if ((backupSeconds >= 0) && (backupSeconds <= 1))
			{
				DateTime alarm = _backupAlarm;
				_backupAlarm = DateTime.Now; // Reset alarm
				if (SoundTheAlarm != null)
				{
					// Sound alarm asynch so clock can keep ticking
					SoundTheAlarm.BeginInvoke(this, new SoundTheAlarmEventArgs(_backupAlarm, AlarmType.Backup), null, null);
				}
			}

			// Refresh clock face
			this.Invalidate();
		}

		private void extendee_Click(object sender, System.EventArgs e)
		{
			// Update the time-zone
			_timeZoneModifier = (int)_timeZoneModifiers[sender];
		}
	}
}
