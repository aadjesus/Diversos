// <copyright file="ChartSeriesScaleDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-18</date>
// <summary>OpenWPFChart library. ItemPropertiesDialog ChartSeriesScale Data View class.</summary>
// <revision>$Id: ChartSeriesScaleDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Collections;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Helpers
{
	/// <summary>
	/// ItemPropertiesDialog ChartSeriesScale Data View class.
	/// </summary>
	public class ChartSeriesScaleDataView : ChartScaleDataView
	{
		public ChartSeriesScaleDataView(ChartSeriesScale scale) : base(scale.Scale)
		{
			series = scale.Series;
			start = scale.Start;
			stop = scale.Stop;
		}

		public ChartScaleVerieties ScaleVeriety
		{
			get { return ChartScaleVerieties.Series; }
		}
		public override ChartScaleVerieties GetVeriety() { return ChartScaleVerieties.Series; }

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

		object start;
		public object Start
		{
			get { return start; }
			set 
			{
				if (start != value)
				{
					start = value;
					Notify("Start");
				}
			}
		}
		public override object GetStart() { return Start; }

		object stop;
		public object Stop
		{
			get { return stop; }
			set
			{
				if (stop != value)
				{
					stop = value;
					Notify("Stop");
				}
			}
		}
		public override object GetStop() { return Stop; }

		protected override string GetDataErrorInfo(string columnName)
		{
			switch(columnName)
			{
				case "Start":
				case "Stop":
					if (Start == Stop)
						return "Start and Stop must not be equal";
					break;
			}
			return base.GetDataErrorInfo(columnName);
		}
	}
}
