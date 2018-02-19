using System.Windows;
using System.ComponentModel;

namespace MEFedMVVMDemo
{
	public static class VisualState
	{
		public static string GetName(DependencyObject obj) { return (string)obj.GetValue(NameProperty); }

		public static void SetName(DependencyObject obj, string value) { obj.SetValue(NameProperty, value); }

		public static readonly DependencyProperty NameProperty = DependencyProperty.RegisterAttached(
			"Name", 
			typeof(string), 
			typeof(VisualState), 
			new PropertyMetadata(NameChangedCallback));

		private static void NameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			string n = e.NewValue as string;
			var fe = d as FrameworkElement;
			if (fe == null || n == null)
				return;

			if (fe.IsLoaded)
				TryGoToState(fe, n);
			else
				fe.Loaded += delegate { TryGoToState(fe, n); };
		}
		static void TryGoToState(FrameworkElement element, string name)
		{
			try { VisualStateManager.GoToState(element, name, true); }
			catch 
			{
				// when designing the control with the VisualState Groups, it will always fail in design mode
				if (!DesignerProperties.GetIsInDesignMode(element))
					throw;
			}
		}
	}
}
