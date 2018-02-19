// <copyright file="ChartScale.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-23</date>
// <summary>OpenWPFChart library. Scale Tick data structure.</summary>
// <revision>$Id: ScaleTick.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// The data describing a <see cref="ChartScale"/> Tick.
	/// </summary>
	/// <remarks>
	/// Contains the Tick value and flag whether it's long or regular.
	/// </remarks>
	public struct ScaleTick
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScaleTick"/> struct.
		/// </summary>
		/// <param name="value">Tick value in "external" units.</param>
		/// <param name="isLong">if set to <c>true</c> than the tick is long; otherwise it's regular.</param>
		public ScaleTick(object value, bool isLong)
		{
			this.value = value;
			this.isLong = isLong;
		}

		private readonly object value;
		/// <summary>
		/// Gets the <see cref="ScaleTick"/> value.
		/// </summary>
		/// <remarks>
		/// Concrete type of Value property is either Numeric or DateTime or object. The latter is
		/// used when scale is presented by the collection of value (often of the string type).
		/// </remarks>
		/// <value><see cref="ScaleTick"/> value in "external" measurement units.</value>
		public object Value { get { return value; } }

		private readonly bool isLong;
		/// <summary>
		/// Gets a value indicating whether the <see cref="ScaleTick"/> is long.
		/// </summary>
		/// <value><c>true</c> if this <see cref="ScaleTick"/> is long; otherwise, <c>false</c>.</value>
		public bool IsLong { get { return isLong; } }
	}
}
