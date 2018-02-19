using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace Galador.Applications
{
	/// <summary>
	/// This class is the <see cref="Composition.Container"/> root catalog, and it filters
	/// the exports so that, if they are <see cref="ExportService"/>, their current
	/// <see cref="ServiceContext"/> match <see cref="Composition.CurrentContext"/>.
	/// </summary>
	class ContextCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		readonly ComposablePartCatalog inner;

		public ContextCatalog(AggregateCatalog inner)
		{
			this.inner = inner;
			inner.Changed += (o, e) =>
			{
				Update();
				if (Changed != null)
					Changed(this, e);
			};
			inner.Changing += (o, e) =>
			{
				if (Changing != null)
					Changing(this, e);
			};
			Update();
		}

		public ServiceContext CurrentContext
		{
			get { return context; }
			set
			{
				switch (value)
				{
					default:
						throw new ArgumentOutOfRangeException();
					case ServiceContext.Runtime:
					case ServiceContext.DesignTime:
					case ServiceContext.TestTime:
						break;
				}
				if (value == context)
					return;
				context = value;
				Update();
			}
		}
		ServiceContext context = ServiceContext.Runtime;

		void Update()
		{
			this.parts = (
				from ip in inner.Parts
				let fp = new FilteredPartDefinition(ip, CurrentContext)
				where fp.ExportDefinitions.Count() > 0
				select (ComposablePartDefinition)fp
			).ToList();

			this.views = (
				from ip in parts
				let ed = ip.ExportDefinitions.First()
				where ed.ContractName == DataViewAttribute.DataViewContract
				from dv in MatchDataView(ed.Metadata)
				select Tuple.Create(ip, ed, dv)
			).ToList();
		}
		static bool MatchContext(IDictionary<string, object> meta, ServiceContext current)
		{
			ServiceContext sc = ServiceContext.All;
			if (meta.ContainsKey(ExportService.ContextProperty) && meta[ExportService.ContextProperty] is ServiceContext[])
			{
				var scs = (ServiceContext[])meta[ExportService.ContextProperty];
				sc = scs.Aggregate(ServiceContext.None, (aSC1, aSC2) => aSC1 | aSC2);
			}
			return (sc & current) != 0;
		}
		static IEnumerable<DataViewAttribute> MatchDataView(IDictionary<string, object> meta)
		{
			if (!meta.ContainsKey(DataViewAttribute.DataTypeProperty)
				|| !(meta[DataViewAttribute.DataTypeProperty] is Type[])
				|| !meta.ContainsKey(DataViewAttribute.LocationProperty)
				|| !(meta[DataViewAttribute.LocationProperty] is object[]))
				yield break;

			var types = (Type[])meta[DataViewAttribute.DataTypeProperty];
			var locations = (object[])meta[DataViewAttribute.LocationProperty];
			if (types.Length != locations.Length)
				yield break;

			for (int i = 0; i < types.Length; i++)
				yield return new DataViewAttribute(types[i], locations[i]);
		}

#if WINDOWS_PHONE
		public override IEnumerable<ComposablePartDefinition> Parts { get { return parts; } }
#else
		public override IQueryable<ComposablePartDefinition> Parts { get { return parts.AsQueryable<ComposablePartDefinition>(); } }
#endif
		public IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition, DataViewAttribute>> Views { get { return views; } }

		List<ComposablePartDefinition> parts;
		List<Tuple<ComposablePartDefinition, ExportDefinition, DataViewAttribute>> views;

		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		class FilteredPartDefinition : ComposablePartDefinition
		{
			ComposablePartDefinition inner;
			List<ExportDefinition> filteredExports;

			public FilteredPartDefinition(ComposablePartDefinition inner, ServiceContext current)
			{
				this.inner = inner;
				filteredExports = (
					from e in inner.ExportDefinitions
					where MatchContext(e.Metadata, current)
					select e
				).ToList();
			}

			public override ComposablePart CreatePart() { return inner.CreatePart(); }
			public override IEnumerable<ExportDefinition> ExportDefinitions { get { return filteredExports; } }
			public override IEnumerable<ImportDefinition> ImportDefinitions { get { return inner.ImportDefinitions; } }
			public override IDictionary<string, object> Metadata { get { return inner.Metadata; } }
		}
	}

}
