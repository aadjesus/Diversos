// <copyright file="CurveDataView.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-19</date>
// <summary>OpenWPFChart library. CurveDataView is the DataView with a Pen.</summary>
// <revision>$Id: CurveDataView.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows.Media;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// CurveDataView is the ItemDataView with a Pen.
	/// </summary>
	public abstract class CurveDataView : ItemDataView
	{
		#region Pen
		Pen pen = new Pen(Brushes.Black, 1);
		/// <summary>
		/// Gets or sets the Pen property.
		/// </summary>
		/// <value>any Pen</value>
		public Pen Pen
		{
			get { return pen; }
			set
			{
				if (pen != value)
				{
					pen = value;
					NotifyPropertyChanged("Pen");
				}
			}
		}
		#endregion Pen
	}
}
