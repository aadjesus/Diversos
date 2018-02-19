// <copyright file="ChartTypes.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Misc Chart types.</summary>
// <revision>$Id: ChartTypes.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Chart Item and Grid orientation.
	/// </summary>
	public enum Orientation
	{
		/// <summary>
		/// Item or Grid lines are dislayed vertically so <see cref="P:ItemVisual.HorizontalScale"/> 
		/// and <see cref="P:ItemVisual.VerticalScale"/> look swapped.
		/// </summary>
		Vertical,
		/// <summary>
		/// Item or Grid lines are dislayed according to their <see cref="P:ItemVisual.HorizontalScale"/> 
		/// and <see cref="P:ItemVisual.VerticalScale"/> properties.
		/// </summary>
		Horizontal
	}

	/// <summary>
	/// Defines which Coordinate Grid lines are drawn.
	/// </summary>
	public enum GridVisibility
	{
		/// <summary>
		/// All Grid lines visible.
		/// </summary>
		AllTicks,
		/// <summary>
		/// Only Long Tick Grid lines visible.
		/// </summary>
		LongTicks,
		/// <summary>
		/// Grid is hidden.
		/// </summary>
		Hidden
	}

	/// <summary>
	/// Defines where axis ticks and labels appear relative to the axis line.
	/// </summary>
	/// <remarks>
	/// AtRightOrAbove and AtLeftOrBelow are mutually exclusive.
	/// </remarks>
	[Flags]
	public enum AxisContentLayout
	{
		/// <summary>
		/// Axis tics and labels are drawn at left or below the axis line.
		/// <para>AtRightOrAbove and AtLeftOrBelow are mutually exclusive.</para>
		/// </summary>
		AtLeftOrBelow = 0x1,
		/// <summary>
		/// Axis tics and labels are drawn at right or above the axis line.
		/// <para>AtRightOrAbove and AtLeftOrBelow are mutually exclusive.</para>
		/// </summary>
		AtRightOrAbove = 0x2,
		/// <summary>
		/// Axis tics are centered at the axis line
		/// </summary>
		TicksCentered = 0x4,
	}

	/// <summary>
	/// Visible Logarithmic Scale Ticks. Applied to the small ticks only.
	/// </summary>
	[Flags]
	public enum LogarithmicScaleTicks
	{
		/// <summary>
		/// Tick at base value 2 is visible
		/// </summary>
		Two = 0x1,
		/// <summary>
		/// Tick at base value 3 is visible
		/// </summary>
		Three = 0x2,
		/// <summary>
		/// Tick at base value 4 is visible
		/// </summary>
		Four = 0x4,
		/// <summary>
		/// Tick at base value 5 is visible
		/// </summary>
		Five = 0x8,
		/// <summary>
		/// Tick at base value 6 is visible
		/// </summary>
		Six = 0x10,
		/// <summary>
		/// Tick at base value 7 is visible
		/// </summary>
		Seven = 0x20,
		/// <summary>
		/// Tick at base value 8 is visible
		/// </summary>
		Eight = 0x40,
		/// <summary>
		/// Tick at base value 9 is visible
		/// </summary>
		Nine = 0x80,
		/// <summary>
		/// Ticks at base values 2 and 5 are visible
		/// </summary>
		Main = Two | Five,
		/// <summary>
		/// All Ticks visible
		/// </summary>
		All = Two | Three | Four | Five | Six | Seven | Eight | Nine,
	}

	/// <summary>
	/// DateTime Scale ticks measurement units.
	/// </summary>
	public enum DateTimeTickUnits
	{
		/// <summary>
		/// DateTime Scale is measured in <see cref="DateTime"/> Ticks.
		/// </summary>
		Ticks,
		/// <summary>
		/// DateTime Scale is measured in Milliseconds.
		/// </summary>
		Milliseconds,
		/// <summary>
		/// DateTime Scale is measured in Seconds.
		/// </summary>
		Seconds,
		/// <summary>
		/// DateTime Scale is measured in Minutes.
		/// </summary>
		Minutes,
		/// <summary>
		/// DateTime Scale is measured in Hours.
		/// </summary>
		Hours,
		/// <summary>
		/// DateTime Scale is measured in Days.
		/// </summary>
		Days,
		/// <summary>
		/// DateTime Scale is measured in Weeks.
		/// </summary>
		Weeks,
		/// <summary>
		/// DateTime Scale is measured in Months.
		/// </summary>
		Months,
		/// <summary>
		/// DateTime Scale is measured in Years.
		/// </summary>
		Years
	}
}
