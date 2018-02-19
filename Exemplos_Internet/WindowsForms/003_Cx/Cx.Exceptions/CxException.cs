using System;
using System.Collections.Generic;

namespace Cx.Exceptions
{
	public class CxException : ApplicationException
	{
		public CxException(string msg)
			: base(msg)
		{
		}

		public CxException(List<Exception> exceptions, string msg)
			: base(msg)
		{
		}
	}
}
