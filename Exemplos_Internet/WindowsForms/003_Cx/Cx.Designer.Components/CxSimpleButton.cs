using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;
using Cx.WinForm;

namespace Cx.Designer.Components
{
	[CxComponentName("CxSimpleButton")]
	[CxExplicitEvent("ButtonClick")]
	public class CxSimpleButton : Button, ICxVisualComponentClass
	{
		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);

			// We transform the Click event to the ButtonClick event so that we can
			// add a logger to the event.  If we wire-up directly to the button's Click
			// event, the EventHelpers.Fire mechanism doesn't see the event and the logger
			// doesn't work.  

			// However, if the wireup routine were smart enough to see that we're wiring
			// up an event directly to a third party class, then it could add a logger event
			// as well, but this makes things even more complicated, though it's a question
			// as to whether every possible event an app might be interested in needs to be
			// wired up this way.
			EventHelpers.Transform(this, this, "Click").To("ButtonClick");
		}
	}
}
														