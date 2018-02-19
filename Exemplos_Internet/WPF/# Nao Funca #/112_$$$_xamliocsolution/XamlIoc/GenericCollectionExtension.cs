using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Markup;
using System.Collections;

namespace XamlIoc
{
	/// <summary>Extension creating a generic collection type and allowing its population.</summary>
	[ContentProperty("Items")]
	public class GenericCollectionExtension : GenericExtension
	{
		private readonly List<object> items = new List<object>();

		/// <summary>Collection of items being populated in the list.</summary>
		public List<object> Items
		{
			get { return items; }
		}

		/// <summary>Sets some properties of the object.</summary>
		/// <remarks>Populates the <see cref="Items"/> in the generic collection.</remarks>
		/// <param name="obj"></param>
		protected override void PopulateObject(object obj)
		{
			if (Items.Count > 0)
			{
				IList list = (IList)obj;

				foreach (object item in Items)
				{
					list.Add(item);
				}
			}
		}
	}
}