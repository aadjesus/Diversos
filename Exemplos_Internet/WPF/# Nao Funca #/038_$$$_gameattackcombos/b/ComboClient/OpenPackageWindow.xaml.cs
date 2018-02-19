using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GG.GameAttackCombos.Logic;

namespace GG.GameAttackCombos.Client {
	
	/// <summary>
	/// Interaction logic for OpenPackageWindow.xaml.
	/// </summary>
	public partial class OpenPackageWindow : BaseWindow {

		/// <summary>
		/// Gets information for the currently selected package.
		/// </summary>
		public ComboPackageInfo SelectedPackage { get; private set; }


		/// <summary>
		/// Initializes an instance of OpenPackageWindow.
		/// </summary>
		public OpenPackageWindow() {
			InitializeComponent();
		}

		/// <summary>
		/// Displays all downloaded packages in the list.
		/// </summary>
		private void DisplayDownloadedPackages() {
			// Get a list of all the combo packages already downloaded.
			IEnumerable<ComboPackageInfo> ComboPackageInfos = ComboPackageHelper.GetDownloadedPackageInfoList(true);
			if (ComboPackageInfos != null) {
				// Set the list's item source to the informations and default the selection.
				lbExistingGames.ItemsSource = ComboPackageInfos;
				rbExistingGames.IsChecked = true;
				EvaluateOpenButtonStatus();
			}
#if !Standalone
			else {
				// Default to a new game and disable appropriate UI, if none already exist.
				rbNewGame.IsChecked = true;
				rbExistingGames.IsEnabled = false;
				lbExistingGames.IsEnabled = false;
			}
#else
			// Disable the new game controls for a standalone application.
			rbNewGame.IsEnabled = false;
			txtNewGameCode.IsEnabled = false;
#endif
		}

		/// <summary>
		/// Enables/disables the open button based on the state of the UI.
		/// </summary>
		private void EvaluateOpenButtonStatus() {
			btnOpen.IsEnabled = (
				rbExistingGames.IsChecked == true && lbExistingGames.SelectedIndex > -1
				||
				rbNewGame.IsChecked == true && GameCode.ValidateGameCode(txtNewGameCode.Text.Trim())
			);
		}

		/// <summary>
		/// Inidicates the user's option to open an existing game or download a new one.
		/// </summary>
		/// <param name="indicateExistingGames">A flag that determines which option the user has indicated.</param>
		private void IndicateOption(bool indicateExistingGames) {
			if (IsLoaded) {
				// Check the appropriate radio button.
				RadioButton rbOption = (indicateExistingGames ? rbExistingGames : rbNewGame);
				if (rbOption.IsChecked != true) {
					rbOption.IsChecked = true;
				}

				// Focus the appropriate related control.
				Control RelatedControl = (indicateExistingGames ? (Control)lbExistingGames : (Control)txtNewGameCode);
				if (!RelatedControl.IsKeyboardFocusWithin) {
					RelatedControl.Focus();
				}

				// Select the appropriate package and update the open button.
				SelectedPackage = (indicateExistingGames ? lbExistingGames.SelectedItem as ComboPackageInfo : null);
				EvaluateOpenButtonStatus();
			}
		}

		/// <summary>
		/// Selects a package based on the UI state. If an existing package is selected or
		/// a new package is successfully downloaded, the window is closed.
		/// </summary>
		private void SelectPackage() {
#if Standalone
			// If the currently opened package is selected, set the selection to null.
			if (SelectedPackage.FileName.Equals(App.Current.CurrentComboPackageFileName)) {
				SelectedPackage = null;
			}

			// Close the window.
			DialogResult = true;
#else
			tbStatus.Text = string.Empty;
			ShowDownloadProgress(true);

			if (rbNewGame.IsChecked == true) {
				// Download a new package, if indicated.
				// * Create a delegate for the method call and invoke it on a separate thread.
				tbStatus.Text = "Downloading game package...";
				Func<string, ComboPackageInfo> DownloadNewPackageCall = new Func<string, ComboPackageInfo>(ComboPackageHelper.DownloadNewPackage);
				DownloadNewPackageCall.BeginInvoke(txtNewGameCode.Text.Trim(), new AsyncCallback(DownloadNewPackageCompleted), DownloadNewPackageCall);
			} else if (SelectedPackage != null) {
				// Just close the window if the currently opened package is selected.
				if (SelectedPackage.FileName.Equals(App.Current.CurrentComboPackageFileName)) {
					SelectedPackage = null;
					DialogResult = true;
				} else {
					// Check for an update to the selected existing package.
					// * Create a delegate for the method call and invoke it on a separate thread.
					tbStatus.Text = "Updating game package...";
					Func<ComboPackageInfo, bool> DownloadPackageUpdateCall = new Func<ComboPackageInfo, bool>(ComboPackageHelper.DownloadPackageUpdate);
					DownloadPackageUpdateCall.BeginInvoke(SelectedPackage, new AsyncCallback(DownloadPackageUpdateCompleted), DownloadPackageUpdateCall);
				}
			}
#endif
		}

		/// <summary>
		/// Finishes the asynchronous call that downloads a new package via the service and
		/// schedules an update of the UI.
		/// </summary>
		/// <param name="result">The result of the asynchronous call.</param>
		private void DownloadNewPackageCompleted(IAsyncResult result) {
			if (result.IsCompleted) {
				// End the delegate that was invoked.
				Func<string, ComboPackageInfo> InvokedDelegate = result.AsyncState as Func<string, ComboPackageInfo>;
				if (InvokedDelegate != null) {
					ComboPackageInfo Info = InvokedDelegate.EndInvoke(result);

					// Process the results of the invoked delegate on the UI thread.
					Dispatcher.BeginInvoke(new Action<ComboPackageInfo>(ProcessNewPackage), Info);
				}
			}
		}

		/// <summary>
		/// Finishes the asynchronous call that downloads any update to a package via the service 
		/// and schedules an update of the UI.
		/// </summary>
		/// <param name="result">The result of the asynchronous call.</param>
		private void DownloadPackageUpdateCompleted(IAsyncResult result) {
			if (result.IsCompleted) {
				// End the delegate that was invoked.
				Func<ComboPackageInfo, bool> InvokedDelegate = result.AsyncState as Func<ComboPackageInfo, bool>;
				if (InvokedDelegate != null) {
					bool WasUpdated = InvokedDelegate.EndInvoke(result);

					// Process the results of the invoked delegate on the UI thread.
					Dispatcher.BeginInvoke(new Action<bool>(ProcessPackageUpdate), WasUpdated);
				}
			}
		}

		/// <summary>
		/// Processes the results of the download of a new package and updates the UI accordingly.
		/// </summary>
		/// <param name="downloadedPackageInfo">
		/// The combo package information acquired from the download.
		/// </param>
		private void ProcessNewPackage(ComboPackageInfo downloadedPackageInfo) {
			// Hide the progress.
			ShowDownloadProgress(false);

			if (downloadedPackageInfo == null) {
				// Display a download error and do not close the window.
				MessageBox.Show("An error occured downloading the new game combo package.", "Error Downloading");
			} else if (downloadedPackageInfo.IsEmpty) {
				// Display an invalid error and do not close the window.
				MessageBox.Show("No game data was downloaded. Most likely, this means the game code entered was invalid.\n\nCheck the code and try again.", "No Data");
			} else {
				// Update the selected package reference to the newly downloaded one and close the window.
				SelectedPackage = downloadedPackageInfo;
				DialogResult = true;
			}
		}

		/// <summary>
		/// Processes the results of any downloaded package update and updates the UI accordingly.
		/// </summary>
		/// <param name="wasUpdated">A flag indicating if the package was updated or not.</param>
		private void ProcessPackageUpdate(bool wasUpdated) {
			// Hide the progress and close the window.
			ShowDownloadProgress(false);
			DialogResult = true;
		}

		/// <summary>
		/// Shows/hides the download progress indicator.
		/// </summary>
		/// <param name="show">A flag indicating whether to show or hide the download progress.</param>
		private void ShowDownloadProgress(bool show) {
			Cursor = (show ? Cursors.Wait : null);
			grdMain.IsEnabled = !show;
			pbDownload.Visibility = (show ? Visibility.Visible : Visibility.Hidden);
			tbStatus.Visibility = pbDownload.Visibility;
		}


		private void winOpenPackage_Loaded(object sender, RoutedEventArgs e) {
			DisplayDownloadedPackages();
		}

		private void rbExistingGames_Checked(object sender, RoutedEventArgs e) {
			IndicateOption(true);
		}

		private void rbNewGame_Checked(object sender, RoutedEventArgs e) {
			IndicateOption(false);
		}

		private void btnOpen_Click(object sender, RoutedEventArgs e) {
			SelectPackage();
		}

		private void lbExistingGames_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			// Double-clicking an item is a shortcut for selecting the item and pressing the default button.
			if (lbExistingGames.SelectedItem != null) {
				SelectPackage();
			}
		}

		private void lbExistingGames_GotFocus(object sender, RoutedEventArgs e) {
			IndicateOption(true);
		}

		private void lbExistingGames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			IndicateOption(true);
		}

		private void txtNewGameCode_GotFocus(object sender, RoutedEventArgs e) {
			IndicateOption(false);
		}

		private void txtNewGameCode_TextChanged(object sender, TextChangedEventArgs e) {
			IndicateOption(false);
		}
		
	}

}