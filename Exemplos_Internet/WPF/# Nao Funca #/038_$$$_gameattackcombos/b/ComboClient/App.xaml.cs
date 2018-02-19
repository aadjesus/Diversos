using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using GG.GameAttackCombos.Logic;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// Interaction logic for App.xaml.
	/// </summary>
	public partial class App : Application {

		/// <summary>
		/// Gets the current Application as an App type.
		/// </summary>
		public new static App Current {
			get { return Application.Current as App; }
		}

		/// <summary>
		/// Gets or sets the isolated storage file name of the current combo package being 
		/// viewed in the application.
		/// </summary>
		public string CurrentComboPackageFileName {
			get {
				string Value = null;
				if (Properties.Contains("CurrentComboPackageFileName")) {
					Value = Properties["CurrentComboPackageFileName"] as string;
				}
				return Value;
			}
			set {
				Properties["CurrentComboPackageFileName"] = value;
			}
		}


		/// <summary>
		/// Load the skin from the specified stream and replace the application resources accordingly.
		/// </summary>
		/// <param name="skinStream">The Stream for the new skin resource.</param>
		public void LoadSkin(Stream skinStream) {
			// Create a new resource dictionary for the skin from the specified stream via an XAML reader.
			ResourceDictionary NewSkin = XamlReader.Load(skinStream) as ResourceDictionary;

			// Replace the last dictionary with the new skin.
			Resources.MergedDictionaries.RemoveAt(Resources.MergedDictionaries.Count - 1);
			Resources.MergedDictionaries.Add(NewSkin);
		}

		/// <summary>
		/// Opens any current combo package from disk.
		/// </summary>
		/// <returns></returns>
		public ComboPackage OpenCurrentComboPackage() {
			ComboPackage Package = null;

			// Get the current combo package's path from the application.
			string CurrentComboPackageFileName = App.Current.CurrentComboPackageFileName;
			if (!string.IsNullOrEmpty(CurrentComboPackageFileName)) {
				try {
					// Open a stream to the package file.
					Stream PackageStream = ComboPackageHelper.OpenPackageStream(CurrentComboPackageFileName);

					// Attempt to open a ComboPackage for the stream.
					Package = new ComboPackage(PackageStream);
				} catch {
					throw new ApplicationException("There was a problem opening the combo package.");
				}
			}

			return Package;
		}

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
			// Make sure to save any completion state on the main window.
			if (MainWindow != null && MainWindow is MainWindow) {
				((MainWindow)MainWindow).SaveCompletionState();
			}
		}

	}

}