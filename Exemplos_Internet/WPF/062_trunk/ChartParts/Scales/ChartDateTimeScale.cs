// <copyright file="ChartDateTimeScale.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Chart DateTime Scale and its decorations layout.</summary>
// <revision>$Id: ChartDateTimeScale.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Linear Chart Scale of the <see langword="DateTime"/> base type and its decorations layout.
	/// </summary>
	/// <remarks>
	/// <see cref="ChartDateTimeScale"/> requires the <see cref="ChartScale.Start"/> and <see cref="ChartScale.Stop"/>
	/// properties values are convertible to the <see langword="DateTime"/> type.
	/// <para>This class Scale property means pixel count per one 
	/// <see cref="P:OpenWPFChart.ChartDateTimeScale.TickUnit"/>.</para>
	/// </remarks>
	public class ChartDateTimeScale : ChartScale
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChartDateTimeScale"/> class.
		/// </summary>
		/// <overloads>
		/// <summary><see cref="ChartDateTimeScale"/> constructors.</summary>
		/// <remarks>
		/// Parametrized constructors arrange the Scale properties so that it fills itself 
		/// confortable in the screen extent propvided.
		/// </remarks>
		/// </overloads>
		public ChartDateTimeScale() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartDateTimeScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartDateTimeScale(DateTime start, DateTime stop, double extent)
		{
			autoArrage(start, stop, extent);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartDateTimeScale"/> class.
		/// </summary>
		/// <remarks>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </remarks>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		public ChartDateTimeScale(object start, object stop, double extent)
		{
			try
			{
				DateTime dblStart = Convert.ToDateTime(start), dblStop = Convert.ToDateTime(stop);
				autoArrage(dblStart, dblStop, extent);
			}
			catch (InvalidCastException)
			{
			}
		}

		/// <summary>
		/// Arranges the Scale so that it fills itself confortable in the screen extent propvided.
		/// </summary>
		/// <param name="start">Scale start.</param>
		/// <param name="stop">Scale stop.</param>
		/// <param name="extent">Scale visual extent in pixels.</param>
		void autoArrage(DateTime start, DateTime stop, double extent)
		{
			if (start == stop)
				return;

			Start = start;
			Stop = stop;
			TimeSpan ts;
			if (start < stop)
				ts = stop - start;
			else
				ts = start - stop;

			if (ts.TotalDays > 3650) // Measure in Years
				TickUnits = DateTimeTickUnits.Years;
			else if (ts.TotalDays > 300) // Measure in Months
				TickUnits = DateTimeTickUnits.Months;
			else if (ts.TotalDays > 70) // Measure in Weeks
				TickUnits = DateTimeTickUnits.Weeks;
			else if (ts.TotalDays > 10) // Measure in Days
				TickUnits = DateTimeTickUnits.Days;
			else if (ts.TotalHours > 10) // Measure in Hours
				TickUnits = DateTimeTickUnits.Hours;
			else if (ts.TotalMinutes > 10) // Measure in Minutes
				TickUnits = DateTimeTickUnits.Minutes;
			else if (ts.TotalSeconds > 10) // Measure in Seconds
				TickUnits = DateTimeTickUnits.Seconds;
			else if (ts.TotalMilliseconds > 10) // Measure in Milliseconds
				TickUnits = DateTimeTickUnits.Milliseconds;
			else // Measure in Ticks
				TickUnits = DateTimeTickUnits.Ticks;

			Scale = extent / ((double)ts.Ticks / (double)GetTickUnitsTickCount(TickUnits));
			TickStep = 1;
			LongTickAnchor = start;
			LongTickRate = 5;
		}
		#endregion Constructors

		#region TickUnits
		DateTimeTickUnits tickUnits = DateTimeTickUnits.Ticks;
		/// <summary>
		/// Gets or sets the <see cref="TickUnits"/> property.
		/// </summary>
		/// <remarks>
		/// Tick values are the integer number of DataTime in TickUnits (e.g. "Days").
		/// </remarks>
		/// <value>The <see cref="DateTimeTickUnits"/> value.</value>
		public DateTimeTickUnits TickUnits
		{
			get { return tickUnits; }
			set
			{
				if (tickUnits != value)
				{
					if (!ValidateTickUnitsValue((DateTimeTickUnits)value))
						throw new ArgumentException("Invalid TickUnits flags combination", "value");
					tickUnits = value;
					NotifyPropertyChanged("TickUnits");
				}
			}
		}
		/// <summary>
		/// Validates suggested value.
		/// </summary>
		/// <param name="value">DateTimeTickUnits value.</param>
		/// <returns/>
		private static bool ValidateTickUnitsValue(DateTimeTickUnits value)
		{
			DateTimeTickUnits x = (DateTimeTickUnits)value;
			foreach (DateTimeTickUnits item in Enum.GetValues(typeof(DateTimeTickUnits)))
			{
				if (item == x)
					return true;
			}
			return false;
		}
		#endregion TickUnits

		#region TickStep
		long tickStep = 1L;
		/// <summary>
		/// Gets or sets the <see cref="TickStep"/> property.
		/// </summary>
		/// <remarks>
		/// The space between two regular (not long) ticks in <see cref="DateTimeTickUnits"/>.
		/// It should be positive regardeless of <see cref="ChartScale.Start"/> 
		/// and <see cref="ChartScale.Stop"/> values relationship.
		/// </remarks>
		/// <value>The space between two regular ticks.</value>
		public long TickStep
		{
			get { return tickStep; }
			set
			{
				if (tickStep != value)
				{
					if (value <= 0)
						throw new ArgumentException("TickStep must be positive", "value");
					tickStep = value;
					NotifyPropertyChanged("TickStep");
				}
			}
		}
		#endregion TickStep

		#region LongTickAnchor
		DateTime longTickAnchor = new DateTime(2000, 1, 1);
		/// <summary>
		/// Gets or sets the <see cref="LongTickAnchor"/> property.
		/// </summary>
		/// <remarks>
		/// <para>Represents the position of some long tick.</para>
		/// <para>Returned value is coersed according to current TickUnits value.</para>
		/// </remarks>
		/// <value>Whole DateTime value in <see cref="DateTimeTickUnits"/> representing the largest 
		/// <see cref="DateTime"/> that is less than or equal to this property backing value</value>
		public DateTime LongTickAnchor
		{
			get { return Floor(longTickAnchor, TickUnits); }
			set
			{
				if (longTickAnchor != value)
				{
					longTickAnchor = value;
					NotifyPropertyChanged("LongTickAnchor");
				}
			}
		}
		#endregion LongTickAnchor

		#region LongTickRate
		int longTickRate = 10;
		/// <summary>
		/// Gets or sets the <see cref="LongTickRate"/> property.
		/// </summary>
		/// <remarks>
		/// <para>Represents long ticks rate; e.g. 
		/// <list>
		/// if <see cref="LongTickRate"/> == 1 then all ticks will be long.
		/// if <see cref="LongTickRate"/> == 2 then will be one short tick between two long ticks.
		/// if <see cref="LongTickRate"/> == 10 then every tenth tick will be long.
		/// </list>
		/// </para>
		/// <para>Must be positive.</para>
		/// </remarks>
		/// <value>Long ticks rate.</value>
		public int LongTickRate
		{
			get { return longTickRate; }
			set
			{
				if (longTickRate != value)
				{
					if (value <= 0)
						throw new ArgumentException("LongTickRate must be positive", "value");
					longTickRate = value;
					NotifyPropertyChanged("LongTickRate");
				}
			}
		}
		#endregion LongTickRate

		/// <inheritdoc />
		public override bool IsConsistent
		{
			get
			{
				if (!base.IsConsistent)
					return false;
				try
				{
					DateTime start = Convert.ToDateTime(Start);
					DateTime stop = Convert.ToDateTime(Stop);
					if (start != stop)
						return true;
					return false;
				}
				catch (InvalidCastException)
				{
					return false;
				}
			}
		}

		/// <inheritdoc />
		public override double ToPixels(object value)
		{
			if (!IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");
			
			DateTime dateTimeValue = Convert.ToDateTime(value), start = Convert.ToDateTime(Start)
				, stop = Convert.ToDateTime(Stop);

			if (start < stop)
				return (double)((dateTimeValue - start).Ticks) * Scale / (double)GetTickUnitsTickCount(TickUnits);
			else // (start > stop)
				return (double)((start - dateTimeValue).Ticks) * Scale / (double)GetTickUnitsTickCount(TickUnits);
		}

		/// <inheritdoc />
		public override object FromPixels(double value)
		{
			if (!IsConsistent)
				throw new InvalidOperationException("Object isn't properly initialized");

			DateTime start = Convert.ToDateTime(Start), stop = Convert.ToDateTime(Stop);

			if (start < stop)
				return new DateTime((long)(value * GetTickUnitsTickCount(TickUnits) / Scale) + start.Ticks);
			else // (start > stop)
				return new DateTime(start.Ticks - (long)(value * GetTickUnitsTickCount(TickUnits) / Scale));
		}

		#region Ticks iterator
		/// <inheritdoc />
		public override IEnumerable<ScaleTick> Ticks()
		{
			if (!IsConsistent)
				yield break;

			DateTime start = Convert.ToDateTime(Start), stop = Convert.ToDateTime(Stop);
			if (start == stop)
				yield break;

			int longTickRate = LongTickRate;
			long tickStep = TickStep;
			DateTime tick = nearestLongTick();
			int ticksProcessed = 0;
			if (start < stop)
			{
				while (tick <= stop)
				{
					if (tick >= start)
						yield return new ScaleTick(tick, ticksProcessed % longTickRate == 0);
					tick = NextTick(tick, tickStep, TickUnits);
					ticksProcessed++;
				}
			}
			else // start > stop
			{
				while (tick >= stop)
				{
					if (tick <= start)
						yield return new ScaleTick(tick, ticksProcessed % longTickRate == 0);
					tick = PrevTick(tick, tickStep, TickUnits);
					ticksProcessed++;
				}
			}
		}

		/// <summary>
		/// Returns next tick value according to units and tickStep given.
		/// </summary>
		/// <param name="tick">The tick. Supposed the tick is aligned to units bound.</param>
		/// <param name="tickStep">The tick step.</param>
		/// <param name="units">The units.</param>
		/// <returns></returns>
		private static DateTime NextTick(DateTime tick, long tickStep, DateTimeTickUnits units)
		{
			switch (units)
			{
				case DateTimeTickUnits.Milliseconds:
					return tick.AddMilliseconds(tickStep);
				case DateTimeTickUnits.Seconds:
					return tick.AddSeconds(tickStep);
				case DateTimeTickUnits.Minutes:
					return tick.AddMinutes(tickStep);
				case DateTimeTickUnits.Hours:
					return tick.AddHours(tickStep);
				case DateTimeTickUnits.Days:
					return tick.AddDays(tickStep);
				case DateTimeTickUnits.Weeks:
					return tick.AddDays(7 * tickStep);
				case DateTimeTickUnits.Months:
					return tick.AddMonths((int)tickStep);
				case DateTimeTickUnits.Years:
					return tick.AddYears((int)tickStep);
				default: // DateTimeTickUnits.Ticks
					return new DateTime(tick.Ticks + tickStep);
			}
		}

		/// <summary>
		/// Returns previous tick value according to units and tickStep given.
		/// </summary>
		/// <param name="tick">The tick. Supposed the tick is aligned to units bound.</param>
		/// <param name="tickStep">The tick step.</param>
		/// <param name="units">The units.</param>
		/// <returns></returns>
		private static DateTime PrevTick(DateTime tick, long tickStep, DateTimeTickUnits units)
		{
			switch (units)
			{
				case DateTimeTickUnits.Milliseconds:
					return tick.Subtract(new TimeSpan(0, 0, 0, (int)tickStep));
				case DateTimeTickUnits.Seconds:
					return tick.Subtract(new TimeSpan(0, 0, (int)tickStep));
				case DateTimeTickUnits.Minutes:
					return tick.Subtract(new TimeSpan(0, (int)tickStep, 0));
				case DateTimeTickUnits.Hours:
					return tick.Subtract(new TimeSpan((int)tickStep, 0, 0));
				case DateTimeTickUnits.Days:
					return tick.Subtract(new TimeSpan((int)tickStep, 0, 0, 0));
				case DateTimeTickUnits.Weeks:
					return tick.Subtract(new TimeSpan(7 * (int)tickStep, 0, 0, 0));
				case DateTimeTickUnits.Months:
					int year = tick.Year - (int)tickStep / 12;
					int month = tick.Month - (int)tickStep % 12;
					if (month <= 0)
					{
						month += 12;
						year -= 1;
					}
					return new DateTime(year, month, 1);
				case DateTimeTickUnits.Years:
					return new DateTime(tick.Year - (int)tickStep, 1, 1);
				default: // DateTimeTickUnits.Ticks
					return new DateTime(tick.Ticks - tickStep);
			}
		}

		/// <summary>
		/// Find nearest Long Tick position. It is:
		/// <list type="bullet">
		/// <item>at the left of the scale range if Start &lt; Stop</item>.
		/// <item>at the right of the scale range if Start &gt; Stop</item>.
		/// <item>not defined if Start == Stop</item>.
		/// </list>
		/// </summary>
		/// <returns>Nearest Long Tick position</returns>
		private DateTime nearestLongTick()
		{
			DateTime start = Convert.ToDateTime(Start), stop = Convert.ToDateTime(Stop);
			Debug.Assert(start != stop, "start != stop");

			long longTickStep = TickStep * LongTickRate;// Distance between two adjacent long Ticks in TickUnits.
			DateTimeTickUnits tickUnits = TickUnits;
			long longTickStepInTicks = longTickStep * GetTickUnitsTickCount(tickUnits);
			DateTime anchor = LongTickAnchor;
			if (start < stop)
			{ // LongTickAnchor should be at the left of the scale range.
				if (start > anchor)
				{
					if (tickUnits == DateTimeTickUnits.Months || tickUnits == DateTimeTickUnits.Years)
					{
						while (true)
						{
							DateTime next = NextTick(anchor, longTickStep, tickUnits);
							if (next == start)
							{
								anchor = next;
								break;
							}
							else if (next < start)
								anchor = next;
							else
								break;
						}
					}
					else // other DateTimeTickUnits
					{
						long n = (start.Ticks - anchor.Ticks) / longTickStepInTicks;
						anchor = new DateTime(anchor.Ticks + n * longTickStepInTicks);
					}
				}
				else if (start < anchor)
				{
					if (tickUnits == DateTimeTickUnits.Months || tickUnits == DateTimeTickUnits.Years)
					{
						while (true)
						{
							anchor = PrevTick(anchor, longTickStep, tickUnits);
							if (anchor <= start)
								break;
						}
					}
					else // other DateTimeTickUnits
					{
						long n = (anchor.Ticks - start.Ticks) / longTickStepInTicks;
						anchor = new DateTime(anchor.Ticks - (n + 1) * longTickStepInTicks);
						if (anchor + new TimeSpan(longTickStepInTicks) == start)
							anchor = start;
					}
				}

				Debug.Assert(anchor <= start, "anchor <= start");
				Debug.Assert(NextTick(anchor, longTickStep, tickUnits) > start
					, "NextTick(anchor, longTickStep, tickUnits) > start");
			}
			else // start > stop
			{ // LongTickAnchor should be at the right of the scale range.
				if (start > anchor)
				{
					if (tickUnits == DateTimeTickUnits.Months || tickUnits == DateTimeTickUnits.Years)
					{
						while (true)
						{
							DateTime next = NextTick(anchor, longTickStep, tickUnits);
							if (next == start)
							{
								anchor = next;
								break;
							}
							else if (next < start)
								anchor = next;
							else
								break;
						}
					}
					else // other DateTimeTickUnits
					{
						long n = (start.Ticks - anchor.Ticks) / longTickStepInTicks;
						anchor = new DateTime(anchor.Ticks + (n + 1) * longTickStepInTicks);
						if (anchor - new TimeSpan(longTickStepInTicks) == start)
							anchor = start;
					}
				}
				else if (start < anchor)
				{
					if (tickUnits == DateTimeTickUnits.Months || tickUnits == DateTimeTickUnits.Years)
					{
						while (true)
						{
							DateTime prev = PrevTick(anchor, longTickStep, tickUnits);
							if (prev == start)
							{
								anchor = prev;
								break;
							}
							else if (prev > start)
								anchor = prev;
							else
								break;
						}
					}
					else // other DateTimeTickUnits
					{
						long n = (anchor.Ticks - start.Ticks) / longTickStepInTicks;
						anchor = new DateTime(anchor.Ticks - n * longTickStepInTicks);
					}
				}

				Debug.Assert(anchor >= start, "anchor >= start");
				Debug.Assert(PrevTick(anchor, longTickStep, tickUnits) < start
					, "PrevTick(anchor, longTickStep, tickUnits) < start");
			}

			return anchor;
		}
		#endregion Ticks iterator

		/// <summary>
		/// Gets the tick count per one TickUnit (i.e. how many DataTime.Ticks are in one Tick Unit).
		/// </summary>
		/// <param name="tickUnits">The TickUnit.</param>
		/// <returns>Number of TimeSpan.Ticks per one TickUnit.</returns>
		private static long GetTickUnitsTickCount(DateTimeTickUnits tickUnits)
		{
			TimeSpan ts;
			switch (tickUnits)
			{
				case DateTimeTickUnits.Milliseconds:
					ts = new TimeSpan(0, 0, 0, 0, 1);
					return ts.Ticks;
				case DateTimeTickUnits.Seconds:
					ts = new TimeSpan(0, 0, 1);
					return ts.Ticks;
				case DateTimeTickUnits.Minutes:
					ts = new TimeSpan(0, 1, 0);
					return ts.Ticks;
				case DateTimeTickUnits.Hours:
					ts = new TimeSpan(1, 0, 0);
					return ts.Ticks;
				case DateTimeTickUnits.Days:
					ts = new TimeSpan(1, 0, 0, 0);
					return ts.Ticks;
				case DateTimeTickUnits.Weeks:
					ts = new TimeSpan(7, 0, 0, 0);
					return ts.Ticks;
				case DateTimeTickUnits.Months:
					ts = new TimeSpan(30, 0, 0, 0);
					return ts.Ticks;
				case DateTimeTickUnits.Years:
					ts = new TimeSpan(365, 0, 0, 0);
					return ts.Ticks;
				default: // Ticks
					return 1;
			}
		}

		/// <summary>
		/// Returns a whole DateTime value in DateTimeTickUnits representing the largest DateTime 
		/// that is less than or equal to input value.
		/// </summary>
		/// <param name="dt">The input value.</param>
		/// <param name="units">DateTimeTickUnits.</param>
		/// <returns>whole DateTime value in DateTimeTickUnits</returns>
		private static DateTime Floor(DateTime dt, DateTimeTickUnits units)
		{
			switch (units)
			{
				case DateTimeTickUnits.Milliseconds:
					return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
				case DateTimeTickUnits.Seconds:
					return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
				case DateTimeTickUnits.Minutes:
					return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
				case DateTimeTickUnits.Hours:
					return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
				case DateTimeTickUnits.Days:
					return new DateTime(dt.Year, dt.Month, dt.Day);
				case DateTimeTickUnits.Months:
					return new DateTime(dt.Year, dt.Month, 1);
				case DateTimeTickUnits.Years:
					return new DateTime(dt.Year, 1, 1);
				default: // DateTimeTickUnits.Ticks
					return dt;
			}
		}
	}
}
