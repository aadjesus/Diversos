using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Reflection;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Data;

namespace Galador.Wpf
{
	public static class WpfUtils
	{
		public static IEnumerable<ResourceDictionary> EnumerateResources(DependencyObject item)
		{
			while (item != null)
			{
				var fe = item as FrameworkElement;
				if (fe != null)
				{
					var res = fe.Resources;
					foreach (var res2 in EnumerateResources(res))
						yield return res2;
				}
				item = VisualTreeHelper.GetParent(item);
			}
			{
				var res = Application.Current.Resources;
				foreach (var res2 in EnumerateResources(res))
					yield return res2;
			}
		}

		public static IEnumerable<ResourceDictionary> EnumerateResources(ResourceDictionary dict)
		{
			if (dict == null)
				yield break;
			yield return dict;
			foreach (var md in dict.MergedDictionaries)
				foreach (var md2 in EnumerateResources(md))
					yield return md2;
		}

		public static object FindResource(this FrameworkElement e, object key)
		{
			if (e == null || key == null)
				return null;
			foreach (var res in EnumerateResources(e))
				if (res.Contains(key))
					return res[key];
			return null;
		}

		public static DependencyObject GetContainerFromItem(this ItemsControl ctrl, object item, bool create)
		{
			var result = ctrl.ItemContainerGenerator.ContainerFromItem(item);
			if (result == null && create)
			{
				var index = ctrl.Items.IndexOf(item);
				if (index < 0)
					return null;
				var ig = (IItemContainerGenerator)ctrl.ItemContainerGenerator;
				var gp = ig.GeneratorPositionFromIndex(index);
				bool newRealized;
				using (ig.StartAt(gp, GeneratorDirection.Forward, false))
					ig.GenerateNext(out newRealized);
				result = ctrl.ItemContainerGenerator.ContainerFromItem(item);
			}
			return result;
		}

		public static DependencyObject FindTopVisualAncestor(DependencyObject depo)
		{
			while (depo != null)
			{
				var p = VisualTreeHelper.GetParent(depo);
				if (p == null)
					return depo;
				depo = p;
			}
			return depo;
		}

		public static T FindVisualAncestor<T>(DependencyObject depo)
			where T : DependencyObject
		{
			while (depo != null && !(depo is T))
			{
				depo = VisualTreeHelper.GetParent(depo);
			}
			return (T)depo;
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depo)
		{
			int nChild = VisualTreeHelper.GetChildrenCount(depo);
			for (int i = 0; i < nChild; i++)
			{
				var c = VisualTreeHelper.GetChild(depo, i);
				if (c is T)
					yield return (T)(object)c;

				foreach (var item in FindVisualChildren<T>(c))
					yield return item;
			}
		}

		public static BitmapImage GetImageSource(string url, UriKind uriKind = UriKind.Relative)
		{
			if (string.IsNullOrEmpty(url))
				return null;
			switch (uriKind)
			{
				case UriKind.Absolute:
				case UriKind.RelativeOrAbsolute:
					if (!UriParser.IsKnownScheme("pack"))
						UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), "pack", -1);
					break;
			}
			var uri = new Uri(url, uriKind);
			return new BitmapImage { UriSource = uri };
		}

		public static void RegisterForNotification(this FrameworkElement element, string propertyName, PropertyChangedCallback callback)
		{
			Binding b = new Binding(propertyName) { Source = element };
			var prop = System.Windows.DependencyProperty.RegisterAttached(
				"ListenAttached" + propertyName,
				typeof(object),
				typeof(UserControl),
				new System.Windows.PropertyMetadata(callback));
			element.SetBinding(prop, b);
		}
	}
}
