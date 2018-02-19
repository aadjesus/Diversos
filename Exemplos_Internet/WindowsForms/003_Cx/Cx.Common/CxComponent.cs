using System;
using System.IO;

using Cx.Interfaces;

namespace Cx.Common
{
	public class CxComponent : ICxComponent
	{
		public string Name { get; set; }
		public string ComponentName { get; set; }
		public string Assembly { get; set; }

		public string AssemblyFilename
		{
			get { return Path.GetFileName(Assembly); }
		}

		public Type Type { get; set; }
		public ICxComponentClass Instance { get; set; }

		public CxComponent()
		{
			Name = String.Empty;
			ComponentName = String.Empty;
			Assembly = String.Empty;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class CxVisualComponent : CxComponent, ICxVisualComponent
	{
		public string Location { get; set; }
		public string Size { get; set; }

		public CxVisualComponent()
		{
			Location = "0, 0";
			Size = "100, 100";
		}
	}

	public class CxBusinessComponent : CxComponent, ICxBusinessComponent
	{
		/// <summary>
		/// The Guid is useful for determining different instances of the same named business component.
		/// </summary>
		public Guid Guid { get; protected set; }

		public CxBusinessComponent()
		{
			Guid = Guid.NewGuid();
		}
	}

}
