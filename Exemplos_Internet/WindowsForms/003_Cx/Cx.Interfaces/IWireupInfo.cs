using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Interfaces
{
	public interface IWireupInfo
	{
		string Producer { get; }
		string Consumer { get; }
	}
}
