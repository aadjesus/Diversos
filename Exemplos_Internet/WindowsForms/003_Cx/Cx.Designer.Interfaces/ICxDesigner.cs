using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Interfaces;

namespace Cx.Designer.Interfaces
{
	public interface ICxDesigner : ICxBusinessComponentClass
	{
		void LoadComponents(string filename);
	}
}
