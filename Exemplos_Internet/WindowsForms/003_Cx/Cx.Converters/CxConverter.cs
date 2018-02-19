using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Attributes;
using Cx.Interfaces;
using Cx.EventArgs;

namespace Cx.Converters
{						  
	[CxComponentName("Cx.Converters.CxBindingConverter")]
	[CxExplicitEvent("Converted")]
	public class CxBindingConverter<T> : ICxBusinessComponentClass
	{
		protected EventHelper converted;

		public CxBindingConverter()
		{
			converted=EventHelpers.CreateEvent<IEnumerable>(this, "Converted");
		}

		[CxConsumer]
		public void OnConvertToSortedBindingList(object sender, CxEventArgs<IEnumerable> args)
		{
			SortableBindingList<T> sbl = new SortableBindingList<T>((List<T>)args.Data);
			converted.Fire(sbl);
		}
	}
}
