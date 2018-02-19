using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;

namespace Cx.Designer
{
	[CxComponentName("OpenMetadata")]
	[CxExplicitEvent("CloseDialog")]
	[CxExplicitEvent("OpenMetadata")]
	public class OpenMetadata : ICxBusinessComponentClass
	{
		protected EventHelper openMetadata;
		protected EventHelper closeDialog;
		protected string filename;

		public OpenMetadata()
		{
			openMetadata = EventHelpers.CreateEvent<string>(this, "OpenMetadata");
			closeDialog = EventHelpers.CreateEvent(this, "CloseDialog");
		}

		[CxConsumer]
		public void OnMetadataFilename(object sender, CxEventArgs<string> args)
		{
			filename = args.Data;
		}

		[CxConsumer]
		public void OnOpenMetadata(object sender, System.EventArgs args)
		{
			openMetadata.Fire(filename);
			closeDialog.Fire();
		}
	}
}
