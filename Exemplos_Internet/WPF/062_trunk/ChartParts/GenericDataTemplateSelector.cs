// <copyright file="GenericDataTemplateSelector.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Nick Zhebrun.
// </copyright>
// <author>Nick Zhebrun</author>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-01-21</date>
// <summary>OpenWPFChart library. Modified GenericDataTemplateSelector by Nick Zhebrun 
// (http://zhebrun.blogspot.com/2008/09/are-you-tired-to-create.html).
// </summary>
// <revision>$Id: GenericDataTemplateSelector.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Generic DataTemplateSelector based on the value of the property of the Item templated.
	/// </summary>
	/// <remarks>
	/// <para>Modified version of GenericDataTemplateSelector proposed by Nick Zhebrun 
	/// (<see href="http://zhebrun.blogspot.com/2008/09/are-you-tired-to-create.html">see http://zhebrun.blogspot.com/2008/09/are-you-tired-to-create.html</see>).
	/// </para>
	/// <para>Changes:
	/// <list type="number">
	/// <item>ContentPropertyAttribute added to the class.</item>
	/// <item>Property DataTemplateSelectorItems renamed to SelectorItems and made read only.</item>
	/// <item>Type of SelectorItems changed to Collection&lt;&gt;.</item>
	/// <item>SelectorItems collection is created on construction.</item>
	/// <item>Item type is checked in SelectTemplate override to decide if the template 
	/// could be applied to it.</item>
	/// <item>External Reflector type replaced with TypeDescriptor.</item>
	/// </list>
	/// </para>
	/// </remarks>
	/// <seealso cref="T:OpenWPFChart.GenericDataTemplateSelectorItem"/>
	[ContentProperty("SelectorItems")]
	public class GenericDataTemplateSelector : DataTemplateSelector
	{
		Collection<GenericDataTemplateSelectorItem> selectorItems = new Collection<GenericDataTemplateSelectorItem>();
		/// <summary>
		/// Gets the collection of selector items.
		/// </summary>
		/// <value>The selector items collection.</value>
		public Collection<GenericDataTemplateSelectorItem> SelectorItems 
		{ 
			get { return selectorItems;  }
		}

		/// <summary>
		/// When overridden in a derived class, returns a 
		/// <see cref="T:System.Windows.DataTemplate"/> based on custom logic.
		/// </summary>
		/// <param name="item">The data object for which to select the template.</param>
		/// <param name="container">The data-bound object.</param>
		/// <returns>
		/// Returns a <see cref="T:System.Windows.DataTemplate"/> or null. The default value is null.
		/// </returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item != null)
			{
				foreach (GenericDataTemplateSelectorItem selectorItem in SelectorItems)
				{
					// If the TemplatedType is specified we check the item has that type.
					if (selectorItem.TemplatedType != null && item.GetType() != selectorItem.TemplatedType)
						continue;

					// If the property exists on item and its value matches with the value provided
					// then select that template.
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(item)[selectorItem.PropertyName];
					if (propertyDescriptor != null && selectorItem.Value.Equals(propertyDescriptor.GetValue(item)))
						return selectorItem.Template;
				}
			}
			return null;
		}
	}

	/// <summary>
	/// Associates the named property value with DataTemplate.
	/// </summary>
	/// <remarks>
	/// <para>Modified GenericDataTemplateSelector by Nick Zhebrun 
	/// (<see href="http://zhebrun.blogspot.com/2008/09/are-you-tired-to-create.html">see http://zhebrun.blogspot.com/2008/09/are-you-tired-to-create.html</see>).
	/// <para>Changes:
	/// <list type="number">
	/// <item>GenericDataTemplateSelectorItem type changed to struct.</item>
	/// <item>Value property type changed to object to be more generic.</item>
	/// <item>TemplatedType property added to allow to chech the templated item has that type 
	/// or is derived from it.</item>
	/// <item>Description property added. It could be used in UI to allow the user to select one or 
	/// other template.</item>
	/// </list>
	/// </para></para>
	/// </remarks>
	/// <seealso cref="T:OpenWPFChart.GenericDataTemplateSelector"/>
	public struct GenericDataTemplateSelectorItem
	{
		/// <summary>
		/// Gets or sets the name of the property of the data item which 
		/// is used as a template selector.
		/// </summary>
		/// <value>The name of the property.</value>
		public string PropertyName { get; set; }
		/// <summary>
		/// Gets or sets the value of the property that triggers the DataTemplate 
		/// association.
		/// </summary>
		/// <value>The value.</value>
		public object Value { get; set; }
		/// <summary>
		/// Gets or sets the DataTemplate.
		/// </summary>
		/// <value>The DataTemplate.</value>
		public DataTemplate Template { get; set; }
		/// <summary>
		/// Gets or sets the type of the templated data item to which template could be applied.
		/// The templated item must have the TemplatedType or be derived from it.
		/// </summary>
		/// <value>The type of the templated item or null.</value>
		public Type TemplatedType { get; set; }
		/// <summary>
		/// Gets or sets the user-readable description.
		/// It could be used in UI to allow the user to select one or other template.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
	}
}
