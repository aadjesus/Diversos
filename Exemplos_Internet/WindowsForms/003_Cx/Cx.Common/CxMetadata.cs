using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Interfaces;

namespace Cx.Common
{
	public class CxMetadata
	{
		public string Uri { get; set; }
		public List<ICxComponent> ComponentList { get; set; }
		public List<Wireup> Wireups { get; protected set; }
		public DiagnosticDictionary<string, List<PropertyValue>> PropertyValues { get; protected set; }
		public DiagnosticDictionary<string, List<DeclaredEvent>> DeclaredEvents { get; protected set; }

		public CxMetadata(string uri, 
			List<ICxComponent> componentList, 
			List<Wireup> wireups,
			DiagnosticDictionary<string, List<PropertyValue>> propertyValues,
			DiagnosticDictionary<string, List<DeclaredEvent>> declaredEvents)
		{
			Uri = uri;
			ComponentList = componentList;
			Wireups = wireups;
			PropertyValues = propertyValues;
			DeclaredEvents = declaredEvents;
		}
	}
}
