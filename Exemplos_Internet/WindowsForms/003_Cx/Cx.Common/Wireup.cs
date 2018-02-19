using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Common
{
	public class Wireup
	{
		public string Producer { get; set; }
		public string Consumer { get; set; }

		public Wireup()
		{
		}

		public Wireup(string producer, string consumer)
		{
			Producer = producer;
			Consumer = consumer;
		}
	}
}
