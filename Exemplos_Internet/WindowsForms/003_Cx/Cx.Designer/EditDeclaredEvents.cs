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
	[CxComponentName("EditEvents")]
	[CxExplicitEvent("GetEventList")]
	[CxExplicitEvent("UpdateEventList")]
	[CxExplicitEvent("Save")]
	[CxExplicitEvent("RequestEventListUpdate")]
	[CxExplicitEvent("CloseDialog")]
	public class EditDeclaredEvents : ICxBusinessComponentClass
	{
		protected List<DeclaredEvent> events;
		protected EventHelper updateEventList;
		protected EventHelper save;

		public EditDeclaredEvents()
		{
			// gets the event list from the underlying model
			EventHelpers.CreateCommand(this, "GetEventList");
			EventHelpers.CreateCommand(this, "CloseDialog");
			// requests an update from the UI, since we aren't doing data binding.
			EventHelpers.CreateCommand(this, "RequestEventListUpdate");
			updateEventList = EventHelpers.CreateEvent<IEnumerable>(this, "UpdateEventList");
			save = EventHelpers.CreateEvent<IEnumerable>(this, "Save");
		}

		[CxConsumer]
		public void OnInitialize(object sender, System.EventArgs args)
		{
			// gets the property list from the master model.
			EventHelpers.FireCommand(this, "GetEventList");
		}

		[CxConsumer]
		public void OnEventList(object sender, CxEventArgs<IEnumerable> args)
		{
			events = new List<DeclaredEvent>((IEnumerable<DeclaredEvent>)args.Data);
			updateEventList.Fire(new List<DeclaredEvent>(events));
		}

		[CxConsumer]
		public void OnSave(object sender, System.EventArgs args)
		{
			EventHelpers.FireCommand(this, "RequestEventListUpdate");
			save.Fire(events);
			EventHelpers.FireCommand(this, "CloseDialog");
		}
	}
}
