using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;

namespace Cx.ComponentLoader
{
	public class CxComponentLoader : ICxBusinessComponentClass
	{
		[CxConsumer]
		public void LoadComponents(object sender, CxEventArgs<string> args)
		{
		}
	}
}
