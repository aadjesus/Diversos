using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using DemoApp.ViewModel;
using System;
using Galador.Applications;

namespace DemoApp
{
	public partial class App : Application
	{
		static App()
		{
			// This code is used to test the app when using other cultures.
			//
			//System.Threading.Thread.CurrentThread.CurrentCulture =
			//    System.Threading.Thread.CurrentThread.CurrentUICulture =
			//        new System.Globalization.CultureInfo("it-IT");

			// Ensure the current culture passed into bindings is the OS culture.
			// By default, WPF uses en-US as the culture, regardless of the system settings.
			//
			FrameworkElement.LanguageProperty.OverrideMetadata(
			  typeof(FrameworkElement),
			  new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Composition.Register(GetType().Assembly);

			MainWindow window = new MainWindow();

			var viewModel = Composition.GetInstance<MainWindowViewModel>();
			viewModel.CloseCommand = new DelegateCommand(window.Close);

			window.DataContext = viewModel;
			window.Show();
		}
	}
}