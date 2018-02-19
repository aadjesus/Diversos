// <copyright file="SeriesAxisDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-07</date>
// <summary>OpenWPFChart library. Series Axis DataView.</summary>
// <revision>$Id: SeriesAxisDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Collections;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// Series Axis DataView.
	/// </summary>
	public class SeriesAxisDataView : AxisDataView
	{
		public SeriesAxisDataView(SeriesAxis axis) : base(axis)
		{
			ChartSeriesScale seriesScale = axis.AxisScale as ChartSeriesScale;
			series = seriesScale.Series;
			longTickAnchor = seriesScale.LongTickAnchor;
			longTickRate = seriesScale.LongTickRate;
		}

		#region Series
		IEnumerable series;
		/// <summary>
		/// Gets or Sets the Series property.
		/// <para>The series must not contain reference to the same object.</para>
		/// </summary>
		/// <value>IEnumerable.</value>
		public IEnumerable Series
		{
			get { return series; }
			set
			{
				if (series != value)
				{
					series = value;
					Notify("Series");
				}
			}
		}
		#endregion Series

		#region LongTickAnchor
		object longTickAnchor;
		/// <summary>
		/// Gets or sets the LongTickAnchor property.
		/// <para>Represents the position of any long tick.</para>
		/// </summary>
		/// <value>Any finite double value.</value>
		public object LongTickAnchor
		{
			get { return longTickAnchor; }
			set
			{
				if (longTickAnchor != value)
				{
					longTickAnchor = value;
					Notify("LongTickAnchor");
				}
			}
		}
		#endregion LongTickAnchor

		#region LongTickRate
		int longTickRate;
		/// <summary>
		/// Gets or sets the LongTickRate property.
		/// <para>Represents long ticks rate; e.g. 
		/// <list>
		/// if LongTickRate == 1 then all ticks will be long.
		/// if LongTickRate == 2 then will be one short tick between two long ticks.
		/// if LongTickRate == 10 then every tenth tick will be long.
		/// </list>
		/// </para>
		/// </summary>
		/// <value>Positive integer value.</value>
		public int LongTickRate
		{
			get { return longTickRate; }
			set
			{
				if (longTickRate != value)
				{
					longTickRate = value;
					Notify("LongTickRate");
				}
			}
		}
		#endregion LongTickRate

		#region IDataErrorInfo Members
		protected override string GetDataErrorInfo(string columnName)
		{
			switch (columnName)
			{
				case "LongTickRate":
					if (LongTickRate <= 0)
						return "LongTickRate must be positive";
					return null;
			}
			return base.GetDataErrorInfo(columnName);
		}
		#endregion IDataErrorInfo Members
	}
}
