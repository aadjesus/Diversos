using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using GG.GameAttackCombos.Logic;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : BaseWindow {

		// The currently loaded button sets.
		private List<ButtonSet> ButtonSets;

		// The currently viewed combo definitions.
		private ComboDefinitions Definitions;

		// The timer set for auto-saving any current completion state.
		private DispatcherTimer AutoSaveTimer;


		/// <summary>
		/// Gets the path to the executing application.
		/// </summary>
		private string ApplicationPath {
			get {
				Assembly ApplicationAssembly = Assembly.GetExecutingAssembly();
				return Path.GetDirectoryName(ApplicationAssembly.Location);
			}
		}

		/// <summary>
		/// Initializes an instance of MainWindow.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
		}

		/// <summary>
		/// Sets up the UI after the form is loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Loaded(object sender, RoutedEventArgs e) {
			// Load the button sets and update the UI.
			LoadButtonSets();
			UpdateDisplayStatus();

			// Setup a timer for auto-saving.
			AutoSaveTimer = new DispatcherTimer();
			AutoSaveTimer.Interval = TimeSpan.FromMinutes(1.0);
			AutoSaveTimer.Tick += new EventHandler(AutoSaveTimer_Tick);

			// * This line may be uncommented to load a package already downloaded when the 
			// application launches.
			//OpenComboPackageFile(@"PrinceOfPersia2008.gcp");
		}

		/// <summary>
		/// Loads the button sets from the definition file and sets a grouped view of them
		/// to the button set combo box's ItemsSource.
		/// </summary>
		private void LoadButtonSets() {
			// Open the button sets definition file.
			string ButtonSetFilePath = Path.Combine(ApplicationPath, "ButtonSets.xml");
			if (File.Exists(ButtonSetFilePath)) {
				XmlDocument ButtonSetsFile = new XmlDocument();
				ButtonSetsFile.Load(ButtonSetFilePath);

				try {
					// Load the button sets.
					ButtonSets = ButtonSet.LoadButtonSets(ButtonSetsFile);

					// Update the UI with a grouped view of the button sets.
					ICollectionView View = CollectionViewSource.GetDefaultView(ButtonSets);
					View.GroupDescriptions.Add(new PropertyGroupDescription("Platform"));
					cmbButtonSets.ItemsSource = ButtonSets;

					// Pre-select the first set.
					if (ButtonSets.Count > 0) {
						cmbButtonSets.SelectedIndex = 0;
					}
				} catch {
					MessageBox.Show("There was an unexpected error loading the buttons sets.", "Error Loading Button Sets");
				}
			} else {
				MessageBox.Show("The ButtonSets.xml file was not found.", "Missing Button Sets");
			}
		}

		/// <summary>
		/// Loads the combo definitions from the specified XML document and sets the flattened combos 
		/// list to the combo list box's ItemSource.
		/// </summary>
		private void LoadDefinitions(XmlDocument comboDefinitionDocument) {
			// Load the definitions into a new combo definitions object and load any completion state.
			Definitions = new ComboDefinitions(comboDefinitionDocument);
			LoadCompletionState();

			// Update the UI with the flattened combos.
			lblComboList.Content = string.Format("Combo List ({0:n0})", Definitions.FlattenedCombos.Count);
			lbCombos.ItemsSource = Definitions.FlattenedCombos;

			// Update the display status, mapped buttons, and filter list.
			UpdateDisplayStatus();
			UpdateMappedButtons();
			UpdateFilterList();
		}

		/// <summary>
		/// Loads the completion state of the FlattenedCombos bound to the combo list.
		/// </summary>
		private void LoadCompletionState() {
			if (Definitions != null) {
				// Get the currently opened combo package file name.
				string FileName = App.Current.CurrentComboPackageFileName;
				if (!string.IsNullOrEmpty(FileName)) {
					// Calculate the completion save file name.
					FileName = Path.GetFileNameWithoutExtension(FileName) + ComboDefinitions.CompletionSaveFileExtension;

					// Open a stream to the completion save file.
					using (Stream SaveStream = ComboPackageHelper.OpenPackageStream(FileName)) {
						Definitions.LoadCompletedCombos(SaveStream);
					}
				}
			}
		}

		/// <summary>
		/// Opens the specified combo package file into the UI.
		/// </summary>
		/// <param name="path">The path the combo package file to open.</param>
		private void OpenComboPackageFile(string fileName) {
			// Prepare for a memory copy of the skin stream.
			Stream SkinStream = null;

			try {
				// Open a stream to the specified package file.
				using (Stream PackageStream = ComboPackageHelper.OpenPackageStream(fileName)) {
					// Open a ComboPackage for the specified file via the opened stream.
					using (ComboPackage Package = new ComboPackage(PackageStream)) {
						// Set the current combo package path to the one being opened.
						App.Current.CurrentComboPackageFileName = fileName;

						// Load the combo definitions from the package.
						LoadDefinitions(Package.OpenComboDefinitionDocument());

						// Load the skin from the package.
						SkinStream = Package.OpenSkinStream(true);
					}
				}

				// Load the skin after closing the package.
				App.Current.LoadSkin(SkinStream);

				// Set the initial sort to natural.
				cmbSort.SelectedIndex = 0;

				// Start the auto-save timer.
				AutoSaveTimer.Start();
			} catch {
				MessageBox.Show("There was a problem opening the combo package or one of its contents.", "Combo Package Error");
			} finally {
				// Dispose of any stream for the skin.
				if (SkinStream != null) {
					SkinStream.Dispose();
				}
			}
		}

		/// <summary>
		/// Saves the completion state of the FlattenedCombos bound to the combo list.
		/// </summary>
		internal void SaveCompletionState() {
			if (Definitions != null) {
				// Get the currently opened combo package file name.
				string FileName = App.Current.CurrentComboPackageFileName;
				if (!string.IsNullOrEmpty(FileName)) {
					// Calculate the completion save file name.
					FileName = Path.GetFileNameWithoutExtension(FileName) + ComboDefinitions.CompletionSaveFileExtension;

					// Open a stream to the completion save file.
					using (Stream SaveStream = ComboPackageHelper.OpenPackageStream(FileName, FileMode.Create, FileAccess.Write)) {
						Definitions.SaveCompletedCombos(SaveStream);
					}
				}
			}
		}

		/// <summary>
		/// Updates the data view of the flattened combos based on user input.
		/// </summary>
		private void UpdateDataView() {
			if (Definitions != null) {
				// Get the default view for the flattened combos.
				ListCollectionView View = CollectionViewSource.GetDefaultView(Definitions.FlattenedCombos) as ListCollectionView;

				// Filter the view if indicated.
				if (cmbFilter.SelectedIndex > 0) {
					string Filter = cmbFilter.SelectedItem as string;
					View.Filter = (Combo => ((FlattenedCombo)Combo).Aspects.Contains(Filter));
				} else {
					View.Filter = null;
				}

				// Sort the view if indicated.
				if (cmbSort.SelectedIndex == 1) {
					View.SortDescriptions.Add(new SortDescription("DisplaySequence", ListSortDirection.Ascending));
				} else {
					View.SortDescriptions.Clear();
				}

				// Update the count display.
				if (View.Filter == null) {
					lblComboList.Content = string.Format("Combo List ({0:n0})", Definitions.FlattenedCombos.Count);
				} else {
					lblComboList.Content = string.Format("Combo List ({0:n0} of {1:n0})", View.Count, Definitions.FlattenedCombos.Count);
				}
			}
		}

		/// <summary>
		/// Updates the UI status of the combo display controls based on available data.
		/// </summary>
		private void UpdateDisplayStatus() {
			// The combo display elements are disabled if there are no definitions.
			grdMain.IsEnabled = (Definitions != null && Definitions.FlattenedCombos.Count > 0);
			miClearChecklist.IsEnabled = grdMain.IsEnabled;
		}

		/// <summary>
		/// Updates the filter list with any assigned aspects to the flattened combos.
		/// </summary>
		private void UpdateFilterList() {
			// Create a list of filters from the assigned aspects of the definitions and bind it
			// to the appropriate control.
			List<string> Filters = new List<string>(Definitions.AssignedAspects);
			Filters.Insert(0, "[None]");

			if (cmbFilter.ItemsSource == null) {
				cmbFilter.Items.Clear();
			}
			cmbFilter.ItemsSource = Filters;
			cmbFilter.SelectedIndex = 0;
		}

		/// <summary>
		/// Updates the mapped button for each available command based on the platform of the
		/// currently selected button set.
		/// </summary>
		private void UpdateMappedButtons() {
			if (Definitions != null) {
				// Get the ButtonSet selected.
				ButtonSet SelectedButtonSet = cmbButtonSets.SelectedItem as ButtonSet;
				if (SelectedButtonSet != null) {
					// Update each available command's mapped button.
					foreach (Command AvailableCommand in Definitions.AvailableCommands.Values) {
						// Try to find the button mapping for the selected platform and this command.
						AvailableCommand.MappedButton = null;
						string MappedButtonId = Definitions.GetMappedButtonId(SelectedButtonSet.Platform, AvailableCommand.Id);
						if (!string.IsNullOrEmpty(MappedButtonId)) {
							if (SelectedButtonSet.Buttons.ContainsKey(MappedButtonId)) {
								AvailableCommand.MappedButton = SelectedButtonSet.Buttons[MappedButtonId];
							}
						}
					}
				}
			}
		}

		private void AutoSaveTimer_Tick(object sender, EventArgs e) {
			SaveCompletionState();
		}

		private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			// The window can only close after it is initialized.
			if (IsInitialized) {
				e.CanExecute = true;
				e.Handled = true;
			}
		}

		private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
			Close();
			e.Handled = true;
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
			SaveCompletionState();

			// Show a OpenPackageWindow to allow the user to select/download a combo package.
			OpenPackageWindow OpenWindow = new OpenPackageWindow();
			OpenWindow.Owner = this;
			if (OpenWindow.ShowDialog() == true && OpenWindow.SelectedPackage != null) {
				// Open the combo package selected.
				OpenComboPackageFile(OpenWindow.SelectedPackage.FileName);
			}
			e.Handled = true;
		}

		private void miClearChecklist_Click(object sender, RoutedEventArgs e) {
			// Prompt to clear the checklist.
			if (MessageBox.Show("Are you sure you want to clear the entire checklist?", "Clear Checklist", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
				Definitions.ClearCompletionStatus();
			}
		}

		private void miAbout_Click(object sender, RoutedEventArgs e) {
			// Show the AboutWindow.
			AboutWindow AboutWindow = new AboutWindow();
			AboutWindow.Owner = this;
			AboutWindow.ShowDialog();
			e.Handled = true;
		}

		private void cmbButtonSets_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			// Update the mapped buttons for the available commands.
			UpdateMappedButtons();
		}

		private void lbCombos_KeyUp(object sender, KeyEventArgs e) {
			// Toggle the checkbox in the combo list when the space bar is released.
			if (e.Key == Key.Space) {
				// Get the selected item from the list.
				ListBoxItem Item = lbCombos.ItemContainerGenerator.ContainerFromIndex(lbCombos.SelectedIndex) as ListBoxItem;
				
				// Find the template child.
				CheckBox chkCompleted = Common.FindTemplateChild<CheckBox>("chkCompleted", Item);
				chkCompleted.IsChecked = !chkCompleted.IsChecked;
			}
		}

		private void winMain_Closing(object sender, CancelEventArgs e) {
			try {
				SaveCompletionState();
			} catch {
				e.Cancel = true;
				throw;
			}
		}

		private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			UpdateDataView();
		}

		private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			UpdateDataView();
		}
		
	}

}