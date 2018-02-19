using System;
using System.ComponentModel.Composition;

namespace Galador.Applications
{
	[Flags]
	public enum ServiceContext
	{
		None = 0,
		Runtime = 1 << 0,
		DesignTime = 1 << 1,
		TestTime = 1 << 2,

		All = Runtime | DesignTime | TestTime,
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class ExportService : ExportAttribute
	{
		internal const string ContextProperty = "Context";

		public ServiceContext Context { get; private set; }

		public ExportService(ServiceContext ctxt, Type contractType)
			: base(contractType)
		{
			Context = ctxt;
		}
	}
}
