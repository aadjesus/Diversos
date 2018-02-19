// <copyright file="ChartLegend.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-02-09</date>
// <summary>OpenWPFChart library. ChartLegend control.</summary>
// <revision>$Id: ChartLegend.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System.Windows;
using System.Windows.Controls;

namespace OpenWPFChart.Controls
{
	/// <summary>
	/// ChartLegend control.
	/// </summary>
	/// <remarks>
	/// Helper control to use with Chart controls.
	/// </remarks>
	public class ChartLegend : ListBox
	{
		/// <summary>
		/// Initializes the <see cref="ChartLegend"/> class.
		/// </summary>
		static ChartLegend()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartLegend)
				, new FrameworkPropertyMetadata(typeof(ChartLegend)));
		}

		#region Dependency properties
		#region Title
		/// <summary>
		/// Identifies the Title dependency property.
		/// </summary>
		public static readonly DependencyProperty TitleProperty
			= DependencyProperty.Register("Title", typeof(string), typeof(ChartLegend)
				, new FrameworkPropertyMetadata("Legend"
					, FrameworkPropertyMetadataOptions.AffectsMeasure
						| FrameworkPropertyMetadataOptions.AffectsRender
					)
				);
		/// <summary>
		/// Gets or sets a Legend Title property. This is a dependency property.
		/// </summary>
		/// <value>TitleProperty value</value>
		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		#endregion Title

		#region CornerRadius
		/// <summary>
		/// Identifies the CornerRadiusProperty dependency property.
		/// </summary>
		public static readonly DependencyProperty CornerRadiusProperty
			= Border.CornerRadiusProperty.AddOwner(typeof(ChartLegend));
		/// <summary>
		/// Gets or sets a value that represents the degree to which the corners of a Legend Border 
		/// are rounded. This is a dependency property.
		/// </summary>
		/// <value>CornerRadiusProperty value.</value>
		public CornerRadius CornerRadius
		{
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		#endregion CornerRadius
		#endregion Dependency properties
	}
}
