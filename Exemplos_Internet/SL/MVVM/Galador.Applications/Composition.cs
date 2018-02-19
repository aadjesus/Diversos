using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.ComponentModel;
using System.Text;

namespace Galador.Applications
{
	/// <summary>
	/// This is a centralized class to register assembly for the MEF catalogs 
	/// and View for model, using <seealso cref="ExportAttribute"/> and <see cref="DataViewAttribute"/>
	/// </summary>
	public static class Composition
	{
		#region Compose()

		/// <summary>
		/// Call upon MEF to fill all <see cref="ImportAttribute"/> of an object.
		/// </summary>
		/// <param name="o"></param>
		public static void Compose(object o)
		{
			Compose(null, o);
		}
		public static void Compose(CompositionContainer cc, object o)
		{
			if (o == null)
				return;
			if (cc == null)
				cc = Container;

			var batch = new CompositionBatch();
			batch.AddPart(o);
			cc.Compose(batch);
		}

		#endregion

		#region GetInstance()

		/// <summary>
		/// Ask MEF for a given exported object. It's a unique shared object.
		/// </summary>
		public static T GetInstance<T>(CompositionContainer cc = null)
		{
			if (cc == null)
				cc = Container;
			var result = cc.GetExportedValue<T>();
			if (result == null)
				Debug.WriteLine(string.Format("Composition.GetInstance<{0}>()", typeof(T).FullName), "Composition Failure");
			return result;
		}

		#endregion

		#region GetView()

		/// <summary>
		/// Get the view for an object. A UI element would be returned verbatim.
		/// For other, the API try to find the appropriate view tagged with the type 
		/// of the data as its <see cref="DataViewAttribute.DataType"/>.
		/// </summary>
		/// <remarks>It will se the data as the <see cref="FrameworkElement.DataContext"/>
		/// and <see cref="Compose(object)"/> the view</remarks>.
		public static object GetView(object data, object location = null)
		{
			return GetView(null, data, location);
		}

		public static object GetView(this CompositionContainer cc, object data, object location = null)
		{
			if (data == null)
				return null;

			if (data is UIElement)
				return (UIElement)data;

			var tt = data.GetType();
			foreach (var item in ContextCatalog.Views)
			{
				if (!item.Item3.Candidate(tt, location))
					continue;
				object result = item.Item1.CreatePart().GetExportedValue(item.Item2);
				var ui = result as FrameworkElement;
				if (ui != null)
				{
					ui.DataContext = data;
					Compose(cc, ui);
					return ui;
				}
			}

#if DEBUG
			var sb = new StringBuilder();
			sb.AppendFormat("Composition.GetView({0}, {1})", data.GetType().FullName, location);
			var possibles = (
				from item in ContextCatalog.Views
				where item.Item3.DataType.IsAssignableFrom(tt)
				select item.Item3.Location
				).ToList();
			if (possibles.Count > 0)
			{
				sb.Append(" returns null, locations found (");
				for (int i = 0; i < possibles.Count; i++)
				{
					if (i > 0)
						sb.Append(", ");
					sb.Append(possibles[i]);
				}
				sb.Append(")");
			}
			Debug.WriteLine(sb.ToString(), "Composition Failure");
#endif
			return data;
		}

		#endregion

		#region AP: DesignerDataContext

		public static Type GetDesignerDataContext(DependencyObject obj)
		{
			return (Type)obj.GetValue(DesignerDataContextProperty);
		}

		public static void SetDesignerDataContext(DependencyObject obj, Type value)
		{
			obj.SetValue(DesignerDataContextProperty, value);
		}

		public static readonly DependencyProperty DesignerDataContextProperty = DependencyProperty.RegisterAttached(
			"DesignerDataContext",
			typeof(Type),
			typeof(Composition),
			new PropertyMetadata(DesignerDataContextChangedCallback));

		private static void DesignerDataContextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!DesignerProperties.GetIsInDesignMode(d))
				return;
			CurrentContext = ServiceContext.DesignTime;

			var fe = d as FrameworkElement;
			if (fe == null)
				return;

			DesignRegister(d.GetType());

			var t = (Type)e.NewValue;
			DesignRegister(t);

			var get = typeof(Composition).GetMethod("GetInstance", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(t);
			var dc = get.Invoke(null, new object[] { Container });
			if (dc != null)
			{
				if (dc is IDesignTimeAware)
					((IDesignTimeAware)dc).DesignTimeInitialization();
				// if(fe.IsLoaded) is not supported on Silverlight
				fe.DataContext = dc;
				fe.Loaded += (sender, arg) => fe.DataContext = dc;
			}
		}

		private static void DesignRegister(Type t)
		{
			if (t == null)
				return;
			var ass = t.Assembly;
			var isThere = (
				from c in Catalog.Catalogs
				let ac = c as AssemblyCatalog
				where ac.Assembly == ass
				select ac
			).FirstOrDefault() != null;
			if (!isThere)
				Catalog.Catalogs.Add(new AssemblyCatalog(ass));
		}

		#endregion

		#region Catalog, Container, CurrentContext, Register(), Reset()

		/// <summary>
		/// Used to identify which <see cref="ExportService"/> should be loaded.
		/// It is <see cref="ServiceContext.Runtime"/> by default, automatically set
		/// set <see cref="ServiceContext.DesignTime"/> by <see cref="DesignerDataContext"/> property.
		/// <see cref="ServiceContext.Test"/> could be set by code if needs be.
		/// </summary>
		public static ServiceContext CurrentContext
		{
			get { return ContextCatalog.CurrentContext; }
			set { ContextCatalog.CurrentContext = value; }
		}

		static object locker = new object();

		/// <summary>
		/// Register an assembly to be in the MEF catalog as well as one to look for
		/// view for model (<seealso cref="DataViewAttribute"/>)
		/// </summary>
		public static void Register(params Assembly[] assemblies)
		{
			if (assemblies == null)
				return;
			foreach (var a in assemblies)
				if (a != null)
					Catalog.Catalogs.Add(new AssemblyCatalog(a));
		}

		public static void Register(params Type[] types)
		{
			if (types == null || types.Length == 0)
				return;
			Catalog.Catalogs.Add(new TypeCatalog(types));
		}

		/// <summary>
		/// Empty the catalog and container, in effect all recomposition further will use new objects!
		/// </summary>
		public static void Reset()
		{
			lock (locker)
			{
				catalog = null;
				container = null;
				contextCatalog = null;
			}
		}

		public static AggregateCatalog Catalog
		{
			get
			{
				if (catalog == null)
					lock (locker)
						if (catalog == null)
							catalog = new AggregateCatalog();
				return catalog;
			}
		}
		static AggregateCatalog catalog;

		static ContextCatalog ContextCatalog
		{
			get
			{
				if (contextCatalog == null)
					lock (locker)
						if (contextCatalog == null)
							contextCatalog = new ContextCatalog(Catalog);
				return contextCatalog;
			}
		}
		static ContextCatalog contextCatalog;

		public static CompositionContainer Container
		{
			get
			{
				if (container == null)
					lock (locker)
						if (container == null)
							container = new CompositionContainer(ContextCatalog, true);
				return container;
			}
		}
		static CompositionContainer container;

		#endregion
	}
}
