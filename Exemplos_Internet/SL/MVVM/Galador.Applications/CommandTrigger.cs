using System.Windows;
using System.Windows.Input;

namespace Galador.Applications
{
	/// <summary>
	/// Automatically execute a given command when a given trigger value changes.
	/// </summary>
	public static class CommandTrigger
	{
		public static ICommand GetCommand(DependencyObject obj)
		{
			return (ICommand)obj.GetValue(CommandProperty);
		}

		public static void SetCommand(DependencyObject obj, ICommand value)
		{
			obj.SetValue(CommandProperty, value);
		}

		public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandTrigger), new PropertyMetadata(null));

		public static object GetTriggerValue(DependencyObject obj)
		{
			return (object)obj.GetValue(TriggerValueProperty);
		}

		public static void SetTriggerValue(DependencyObject obj, object value)
		{
			obj.SetValue(TriggerValueProperty, value);
		}

		/// <summary>
		/// When this value change the property is triggered!
		/// </summary>
		public static readonly DependencyProperty TriggerValueProperty =
			DependencyProperty.RegisterAttached("TriggerValue", typeof(object), typeof(CommandTrigger), new PropertyMetadata(TriggerValueChangedCallback));

		private static void TriggerValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var cmd = GetCommand(d);
			if (cmd != null)
				cmd.Execute(e.NewValue);
		}

	}
}
