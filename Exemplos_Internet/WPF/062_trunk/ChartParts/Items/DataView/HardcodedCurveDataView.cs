// <copyright file="HardcodedCurveDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-21</date>
// <summary>OpenWPFChart library. HardcodedCurveDataView associates this DataView with the HardcodedCurve Type.</summary>
// <revision>$Id: HardcodedCurveDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// HardcodedCurveDataView associates this DataView with the HardcodedCurve Type.
	/// </summary>
	public class HardcodedCurveDataView : CurveDataView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HardcodedCurveDataView"/> class.
		/// <para>This object default VisualCue is the <see cref="HardcodedCurve"/> Type.</para>
		/// </summary>
		public HardcodedCurveDataView()
		{
			VisualCue = typeof(HardcodedCurve);
		}
	}
}
