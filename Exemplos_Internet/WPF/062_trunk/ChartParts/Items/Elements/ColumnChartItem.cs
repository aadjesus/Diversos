// <copyright file="ColumnChartItem.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-10</date>
// <summary>OpenWPFChart library Column Chart Item element.</summary>
// <revision>$Id: ColumnChartItem.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Data;
using System.ComponentModel;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// ColumnChartItem element class.
	/// </summary>
	/// <remarks>
	/// Displays the set of points as columns. 
	/// <para>
	/// Columns can be accompanied with the curve and point markers. The curve and point markers visibility 
	/// is controlled by <see cref="ColumnChartItemDataView.IsCurveVisible"/> property. The curve
	/// interpolator is defined by <see cref="ItemDataView.VisualCue"/> property.
	/// </para>
	/// </remarks>
	public class ColumnChartItem : Item
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnChartItem"/> class.
		/// </summary>
		/// <remarks>
		/// Adds <see cref="ColumnChartItemVisual"/>.
		/// </remarks>
		public ColumnChartItem()
		{
			ColumnChartItemVisual columnVisual = new ColumnChartItemVisual();
			BindingOperations.SetBinding(columnVisual, ItemDataViewProperty
				, new Binding("ItemDataView") { Source = this });
			visuals.Add(columnVisual);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. This method
		/// is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set
		/// to true internally.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> 
		/// that contains the event data.</param>
		/// <remarks>
		/// Adds one of Curve Visuals if <see cref="ColumnChartItemDataView.IsCurveVisible"/> is <c>true</c>.
		/// </remarks>
		protected override void OnInitialized(System.EventArgs e)
		{
			AddCurveVisual();

			base.OnInitialized(e);
		}

		/// <summary>
		/// Adds the curve visual.
		/// </summary>
		/// <remarks>
		/// Adds one of Curve Visuals if <see cref="ColumnChartItemDataView.IsCurveVisible"/>
		/// is <c>true</c>.
		/// </remarks>
		void AddCurveVisual()
		{
			ColumnChartItemDataView itemDataView = ItemDataView as ColumnChartItemDataView;
			if (itemDataView == null || !itemDataView.IsCurveVisible)
				return;

			Debug.Assert(visuals.Count == 1, "visuals.Count == 1");
			if (ItemDataView.VisualCue == typeof(PolylineSampledCurve))
			{
				PolylineSampledCurveVisual curveVisual = new PolylineSampledCurveVisual();
				BindingOperations.SetBinding(curveVisual, ItemDataViewProperty
					, new Binding("ItemDataView") { Source = this });
				visuals.Add(curveVisual);
			}
			else if (ItemDataView.VisualCue == typeof(BezierSampledCurve))
			{
				BezierSampledCurveVisual curveVisual = new BezierSampledCurveVisual();
				BindingOperations.SetBinding(curveVisual, ItemDataViewProperty
					, new Binding("ItemDataView") { Source = this });
				visuals.Add(curveVisual);
			}
			else if (ItemDataView.VisualCue == typeof(SplineSampledCurve))
			{
				SplineSampledCurveVisual curveVisual = new SplineSampledCurveVisual();
				BindingOperations.SetBinding(curveVisual, ItemDataViewProperty
					, new Binding("ItemDataView") { Source = this });
				visuals.Add(curveVisual);
			}
		}

		/// <summary>
		/// Removes the curve visual.
		/// </summary>
		void RemoveCurveVisual()
		{
			Debug.Assert(visuals.Count == 2, "visuals.Count == 2");
			BindingOperations.ClearBinding(visuals[1], ItemDataViewProperty);
			visuals.RemoveAt(1);
		}

		/// <summary>
		/// Notifies derived classes on ItemDataView Change.
		/// </summary>
		/// <remarks>
		/// Manages ItemDataView CurveVisible and VisualCue properties changes.
		/// </remarks>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		protected override void OnItemDataViewChanged(PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsCurveVisible")
			{ // Add/remove Curve Visual.
				ColumnChartItemDataView itemDataView = ItemDataView as ColumnChartItemDataView;
				Debug.Assert(itemDataView != null, "itemDataView != null");
				if (itemDataView.IsCurveVisible)
					AddCurveVisual();
				else
					RemoveCurveVisual();
			}
			else if (e.PropertyName == "VisualCue")
			{ // Replace Curve Visual if any.
				if (visuals.Count == 2)
				{
					RemoveCurveVisual();
					AddCurveVisual();
				}
			}
		}
	}
}
