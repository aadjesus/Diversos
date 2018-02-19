using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Designer.Common
{
	public class ComponentPacket
	{
		public string Name { get; set; }
		public string ComponentName { get; set; }
		public string AssemblyName { get; set; }
		public string Location { get; set; }
		public string Size { get; set; }
		public Type ComponentType { get; set; }

		public ComponentPacket(string name, string compName, string assyName, Type componentType, string location, string size)
		{
			Name = name;
			ComponentName = compName;
			AssemblyName = assyName;
			ComponentType = componentType;
			Location = location;
			Size = size;
		}
	}
}
