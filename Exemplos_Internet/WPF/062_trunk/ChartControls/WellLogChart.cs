// <copyright file="WellLogChart.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-08</date>
// <summary>OpenWPFChart library. WellLogChart control.
// Default WellLogChart style contains Depth axis and Value axes for each WellLog in WellLogHeader.
// </summary>
// <revision>$Id: WellLogChart.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Specialized; // For NotifyCollectionChangedEventArgs
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using OpenWPFChart.Parts;

namespace OpenWPFChart.Controls
{
	/// <summary>
	/// Chart control.
	/// Default WellLogChart style contains Depth axis and Value axes for each WellLog in WellLogHeader.
	/// </summary>
	public class WellLogChart : ListBox
	{
		/// <summary>
		/// Initializes the <see cref="WellLogChart"/> class.
		/// </summary>
		static WellLogChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(WellLogChart)
				, new FrameworkPropertyMetadata(typeof(WellLogChart)));
		}

		#region Dependency properties
		#region DepthScale
		/// <summary>
		/// Identifies the DepthScale dependency property.
		/// </summary>
		public static readonly DependencyProperty DepthScaleProperty =
			DependencyProperty.Register("DepthScale", typeof(ChartLinearScale), typeof(WellLogChart)
				, new FrameworkPropertyMetadata(null
					, FrameworkPropertyMetadataOptions.AffectsMeasure 
						| FrameworkPropertyMetadataOptions.AffectsRender
					, DepthScalePropertyChanged)
				);
		/// <summary>
		/// Gets or sets the DepthScale dependency property.
		/// </summary>
		/// <value><see cref="DepthScaleProperty"/>.</value>
		public ChartLinearScale DepthScale
		{
			get { return (ChartLinearScale)GetValue(DepthScaleProperty); }
			set { SetValue(DepthScaleProperty, value); }
		}
		/// <summary>
		/// DepthScale property changed.
		/// </summary>
		/// <remarks>
		/// Assigns the new DepthScale to all WellLogChart items.
		/// </remarks>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> 
		/// instance containing the event data.</param>
		private static void DepthScalePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			WellLogChart ths = sender as WellLogChart;
			if (ths == null)
				return;

			foreach (object obj in ths.Items)
			{
				ItemDataView itemDataView = castToItemDataView(obj);
				if (itemDataView != null)
					itemDataView.HorizontalScale = e.NewValue as ChartLinearScale;
			}
		}
		#endregion DepthScale

		#region ChartWidth
		/// <summary>
		/// Identifies the ChartWidth dependency property.
		/// </summary>
		public static readonly DependencyProperty ChartWidthProperty =
			DependencyProperty.Register("ChartWidth", typeof(double), typeof(WellLogChart)
				, new FrameworkPropertyMetadata(300.0
					, FrameworkPropertyMetadataOptions.AffectsMeasure 
						| FrameworkPropertyMetadataOptions.AffectsRender
					, ChartWidthPropertyChanged)
				, ChartWidthPropertyValidate);
		/// <summary>
		/// Gets or sets the ChartWidth dependency property.
		/// </summary>
		public double ChartWidth
		{
			get { return (double)GetValue(ChartWidthProperty); }
			set { SetValue(ChartWidthProperty, value); }
		}
		/// <summary>
		/// Validates the suggested value.
		/// </summary>
		/// <param name="value">The value suggested.</param>
		/// <returns></returns>
		private static bool ChartWidthPropertyValidate(object value)
		{
			double x = (double)value;
			return x > 0.0;
		}
		/// <summary>
		/// ChartWidth property changed.
		/// </summary>
		/// <remarks>
		/// Applies new ChartWidth to all items.
		/// </remarks>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void ChartWidthPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			WellLogChart ths = sender as WellLogChart;
			if (ths == null)
				return;

			foreach (object obj in ths.Items)
			{
				ItemDataView itemDataView = castToItemDataView(obj);
				if (itemDataView != null)
				{
					ChartScale vScale = itemDataView.VerticalScale;
					Debug.Assert(vScale != null, "vScale != null");
					if (vScale is ChartLinearScale)
						itemDataView.VerticalScale = new ChartLinearScale(Convert.ToDouble(vScale.Start)
							, Convert.ToDouble(vScale.Stop), ths.ChartWidth);
					else if (vScale is ChartLogarithmicScale)
						itemDataView.VerticalScale = new ChartLogarithmicScale(Convert.ToDouble(vScale.Start)
							, Convert.ToDouble(vScale.Stop), ths.ChartWidth);
				}
			}
		}
		#endregion ChartWidth

		#region Grids
		#region HorizontalGridVisibility
		/// <summary>
		/// Identifies the HorizontalGridVisibility dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalGridVisibilityProperty
			= DependencyProperty.Register("HorizontalGridVisibility", typeof(GridVisibility), typeof(WellLogChart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks, FrameworkPropertyMetadataOptions.AffectsRender)
				, Visibility_Validate);
		/// <summary>
		/// Gets or sets the HorizontalGridVisibility property.
		/// </summary>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility HorizontalGridVisibility
		{
			get { return (GridVisibility)GetValue(HorizontalGridVisibilityProperty); }
			set { SetValue(HorizontalGridVisibilityProperty, value); }
		}
		#endregion HorizontalGridVisibility

		#region VerticalGridVisibility
		/// <summary>
		/// Identifies the VerticalGridVisibility dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalGridVisibilityProperty
			= DependencyProperty.Register("VerticalGridVisibility", typeof(GridVisibility), typeof(WellLogChart)
				, new FrameworkPropertyMetadata(GridVisibility.AllTicks, FrameworkPropertyMetadataOptions.AffectsRender)
				, Visibility_Validate);
		/// <summary>
		/// Gets or sets the VerticalGridVisibility property.
		/// </summary>
		/// <value><see cref="GridVisibility"/> value</value>
		public GridVisibility VerticalGridVisibility
		{
			get { return (GridVisibility)GetValue(VerticalGridVisibilityProperty); }
			set { SetValue(VerticalGridVisibilityProperty, value); }
		}
		#endregion VerticalGridVisibility

		/// <summary>
		/// Validates suggested property value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool Visibility_Validate(object value)
		{
			GridVisibility x = (GridVisibility)value;
			return (x == GridVisibility.AllTicks || x == GridVisibility.LongTicks || x == GridVisibility.Hidden);
		}
		#endregion Grids

		#region Depth Axis
		#region DepthAxisTitle
		/// <summary>
		/// Identifies the DepthAxisTitle dependency property.
		/// </summary>
		public static readonly DependencyProperty DepthAxisTitleProperty
			= DependencyProperty.Register("DepthAxisTitle", typeof(string), typeof(WellLogChart)
				, new FrameworkPropertyMetadata("Depth"
						, FrameworkPropertyMetadataOptions.AffectsMeasure
							| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets the DepthAxisTitle property.
		/// </summary>
		/// <value>Depth Axis Title</value>
		public string DepthAxisTitle
		{
			get { return (string)GetValue(DepthAxisTitleProperty); }
			set { SetValue(DepthAxisTitleProperty, value); }
		}
		#endregion DepthAxisTitle
		#endregion Depth Axis
		#endregion Dependency properties

		/// <summary>
		/// Casts the <paramref name="obj"/> to <see cref="T:OpenWPFChart.ItemDataView"/>.
		/// </summary>
		/// <param name="obj">The object to cast.</param>
		/// <returns>ItemDataView object or null.</returns>
		static ItemDataView castToItemDataView(object obj)
		{
			ItemDataView itemDataView = obj as ItemDataView;
			if (itemDataView == null)
			{
				Item item = obj as Item;
				if (item != null)
					itemDataView = item.ItemDataView;
			}
			return itemDataView;
		}

		/// <summary>
		/// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property changes.
		/// </summary>
		/// <param name="e">The event data.</param>
		/// <remarks>
		/// Adds/removes <see cref="T:OpenWPFChart.Parts.ItemDataView"/> PropertyChanged event handlers.
		/// </remarks>
		protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (object obj in e.NewItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
						{
							setCommonDepthScale(itemDataView);

							itemDataView.PropertyChanged += item_PropertyChanged;
						}
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (object obj in e.OldItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
							itemDataView.PropertyChanged -= item_PropertyChanged;
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					foreach (object obj in e.OldItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
							itemDataView.PropertyChanged -= item_PropertyChanged;
					}
					foreach (object obj in e.NewItems)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
						{
							setCommonDepthScale(itemDataView);

							itemDataView.PropertyChanged += item_PropertyChanged;
						}
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					foreach (object obj in Items)
					{
						ItemDataView itemDataView = castToItemDataView(obj);
						if (itemDataView != null)
						{
							setCommonDepthScale(itemDataView);

							itemDataView.PropertyChanged += item_PropertyChanged;
						}
					}
					break;
			}

			if (Items.Count == 0)
				DepthScale = null;
		}

		/// <summary>
		/// Sets Common DepthScale to the WellLogChart control and its Items.
		/// </summary>
		/// <remarks>
		/// When the control Items count is or becomes equal to zero the control depth scale is set 
		/// to <c>null</c> by the <see cref="M:OpenWPFChart.WellLogChart.OnItemsChanged"/> method override.
		/// <para>If the control depth scale has <c>null</c> value it's to the corresponding 
		/// <paramref name="itemDataView"/> horizontal scale value.</para>
		/// <para>If the control depth scale value isn't <c>null</c> then the corresponding 
		/// <paramref name="itemDataView"/> horizontal scale is set to that value.</para>
		/// </remarks>
		/// <param name="itemDataView">The item data view.</param>
		void setCommonDepthScale(ItemDataView itemDataView)
		{
			Debug.Assert(itemDataView != null, "itemDataView != null");

			if (DepthScale == null)
				DepthScale = itemDataView.HorizontalScale as ChartLinearScale;
			else
				itemDataView.HorizontalScale = DepthScale;
		}

		/// <summary>
		/// Handles the PropertyChanged event of the item control.
		/// </summary>
		/// <remarks>
		/// This handler is responsible for coordination this control <see cref="P:OpenWPFChart.WellLogChart.DepthScale"/>
		/// property with hosted items <see cref="P:OpenWPFChart.ItemDataView.HorizontalScale"/> property.
		/// <para>For this control the DepthScale of the control and horizontal ChartScale's of the hosted 
		/// items must be the same. So if the ChartScale of the hosted item is changed by third-partie code
		/// this change should be discarded.</para>
		/// <para>Also this handler resets the ItemTemplateSelector when 
		/// <see cref="P:OpenWPFChart.ItemDataView.VisualCue"/> property changes.</para>
		/// </remarks>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			ItemDataView itemDataView = castToItemDataView(sender);
			if (itemDataView == null)
				return;

			if (e.PropertyName.StartsWith("HorizontalScale"))
			{
				Debug.Assert(DepthScale != null, "DepthScale != null");
				if (!object.ReferenceEquals(itemDataView.HorizontalScale, DepthScale))
					itemDataView.HorizontalScale = DepthScale;
			}

			if (e.PropertyName == "VisualCue")
			{ // Reset the ItemTemplateSelector.
				DataTemplateSelector old = ItemTemplateSelector;
				ItemTemplateSelector = null;
				ItemTemplateSelector = old;
			}
		}
	}
}
