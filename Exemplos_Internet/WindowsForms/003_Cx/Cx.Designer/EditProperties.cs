using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Attributes;
using Cx.Common;
using Cx.EventArgs;
using Cx.Interfaces;

using Cx.Designer.Common;

namespace Cx.Designer
{
	[CxComponentName("EditProperties")]
	[CxExplicitEvent("GetPropertyList")]
	[CxExplicitEvent("UpdatePropertyValues")]
	[CxExplicitEvent("Save")]
	[CxExplicitEvent("RequestPropertyListUpdate")]
	[CxExplicitEvent("CloseDialog")]
	[CxExplicitEvent("UpdateItemList")]
	public class EditProperties : ICxBusinessComponentClass
	{
		protected List<PropertyValue> propVals;
		protected EventHelper updatePropertyValues;
		protected EventHelper updateItemList;
		protected EventHelper save;

		public EditProperties()
		{
			// gets the property list from the underlying model
			EventHelpers.CreateCommand(this, "GetPropertyList");
			EventHelpers.CreateCommand(this, "CloseDialog");
			// requests an update from the UI, since we aren't doing data binding.
			EventHelpers.CreateCommand(this, "RequestPropertyListUpdate");
			updatePropertyValues = EventHelpers.CreateEvent<IEnumerable>(this, "UpdatePropertyValues");
			updateItemList = EventHelpers.CreateEvent<IEnumerable>(this, "UpdateItemList");
			save = EventHelpers.CreateEvent<IEnumerable>(this, "Save");
		}

		[CxConsumer]
		public void OnInitialize(object sender, System.EventArgs args)
		{
			// gets the property list from the master model.
			EventHelpers.FireCommand(this, "GetPropertyList");
		}

		[CxConsumer]
		public void OnPropertyList(object sender, CxEventArgs<IEnumerable> args)
		{
			propVals = new List<PropertyValue>((IEnumerable<PropertyValue>)args.Data);
			updatePropertyValues.Fire(new List<PropertyValue>(propVals));
		}

		[CxConsumer]
		public void OnPropertySelected(object sender, CxEventArgs<object> args)
		{
			PropertyValue pv = (PropertyValue)args.Data;
			updateItemList.Fire(new List<ItemValue>(pv.ItemValues));
		}

		[CxConsumer]
		public void OnSave(object sender, System.EventArgs args)
		{
			EventHelpers.FireCommand(this, "RequestPropertyListUpdate");
			save.Fire(propVals);
			EventHelpers.FireCommand(this, "CloseDialog");
		}
	}
}							  
