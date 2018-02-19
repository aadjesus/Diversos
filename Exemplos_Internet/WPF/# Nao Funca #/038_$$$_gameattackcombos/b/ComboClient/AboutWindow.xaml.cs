using System;
using System.Windows;
using GG.GameAttackCombos.Logic;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : BaseWindow {

		// Holds a reference to the current package for binding purposes.
		// * This must be disposed of when the window is closed.
		ComboPackage _currentPackage;


		public AboutWindow() {
			InitializeComponent();
		}

		private void winAbout_Loaded(object sender, RoutedEventArgs e) {
			// Set the form's data context to the assembly attributes of the application.
			AssemblyAttributes Attributes = new AssemblyAttributes();
			DataContext = Attributes;

			// Attempt to open any current combo package and set the expander's data context.
			_currentPackage = App.Current.OpenCurrentComboPackage();
			if (_currentPackage != null) {
				grpCurrentSkinInfo.DataContext = _currentPackage;
			} else {
				grpCurrentSkinInfo.Visibility = Visibility.Collapsed;
			}
		}

		private void winAbout_Closed(object sender, EventArgs e) {
			// Dispose of any current package.
			if (_currentPackage != null) {
				_currentPackage.Dispose();
			}
		}

		private void btnOk_Click(object sender, RoutedEventArgs e) {
			Close();
		}
		
	}

}