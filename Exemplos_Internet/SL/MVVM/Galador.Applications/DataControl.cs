using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel.Composition.Hosting;

namespace Galador.Applications
{
	/// <summary>
	/// Like a DataTemplate driven by <see cref="DataViewAttribute"/>
	/// </summary>
	public class DataControl : ContentControl
	{
		public object Data
		{
			get { return (object)GetValue(DataProperty); }
			set { SetValue(DataProperty, value); }
		}

		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(
				"Data",
				typeof(object),
				typeof(DataControl),
				new PropertyMetadata((d, e) => ((DataControl)d).UpdateContent()));

		public object Location
		{
			get { return (object)GetValue(LocationProperty); }
			set { SetValue(LocationProperty, value); }
		}

		public static readonly DependencyProperty LocationProperty =
			DependencyProperty.Register(
				"Location",
				typeof(object),
				typeof(DataControl),
				new PropertyMetadata((d, e) => ((DataControl)d).UpdateContent()));

		public CompositionContainer Container
		{
			get { return (CompositionContainer)GetValue(ContainerProperty); }
			set { SetValue(ContainerProperty, value); }
		}

		public static readonly DependencyProperty ContainerProperty =
			DependencyProperty.Register(
				"Container",
				typeof(CompositionContainer),
				typeof(DataControl),
				new PropertyMetadata((d, e) => ((DataControl)d).UpdateContent()));

		private void UpdateContent()
		{
			var view = Composition.GetView(Container, Data, Location);
			if (view is ImageSource)
				Content = new Image { Source = (ImageSource)view };
			else
				Content = view;
		}
	}
}
