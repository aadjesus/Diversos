using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Common;
using Cx.Interfaces;

namespace Cx.Common
{
	public interface ICxDataService
	{
		DiagnosticDictionary<string, ICxComponent> Components { get; }
		List<Wireup> Wireups { get; }
		DiagnosticDictionary<string, List<PropertyValue>> PropertyValues { get; }

		void LoadComponents(string uri);
	}
}
																   