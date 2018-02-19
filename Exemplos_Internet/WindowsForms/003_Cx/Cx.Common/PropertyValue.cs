using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Common
{
	public class ItemValue
	{
		public string Text { get; set; }

		public ItemValue()
		{
			Text = String.Empty;
		}

		public ItemValue(string text)
		{
			Text = text;
		}
	}

	public class PropertyValue
	{
		public string ComponentName { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public List<ItemValue> ItemValues { get; set; }

		public PropertyValue()
		{
			ItemValues = new List<ItemValue>();
		}
	}
}
