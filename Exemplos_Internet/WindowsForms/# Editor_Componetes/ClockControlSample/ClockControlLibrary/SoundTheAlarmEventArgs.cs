#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace ClockControlLibrary
{
	public class SoundTheAlarmEventArgs : EventArgs
	{
		private DateTime _alarm;
		private AlarmType _alarmType;
		public SoundTheAlarmEventArgs(DateTime alarm, AlarmType alarmType)
		{
			_alarm = alarm;
			_alarmType = alarmType;
		}
		public DateTime Alarm
		{
			get { return _alarm; }
		}
		public AlarmType AlarmType
		{
			get { return _alarmType; }
		}
	}
}
