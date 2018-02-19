using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cx.Interfaces
{
	public interface ICxApp
	{
		IWireupInfo GetWireupInfo(object producer, object consumer, MethodInfo mi);
	}
}
