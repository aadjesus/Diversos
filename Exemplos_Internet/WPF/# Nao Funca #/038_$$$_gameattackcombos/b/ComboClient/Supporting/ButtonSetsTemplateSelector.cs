using System.Windows;
using System.Windows.Controls;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// A DataTemplateSelector for the combo box used to display the available button sets.
	/// </summary>
	/// <remarks>
	/// This template selector will use a standard template that displays the button set's 
	/// Name property, unless a ContentPresenter is present inside a ComboBox; this indicates
	/// that the text in the selection box area is being displayed. We want to show both the
	/// platform and the name of the button set in this case.
	/// </remarks>
	public class ButtonSetsTemplateSelector : DataTemplateSelector {

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			string ResourceKey = "ButtonSetByName";

			// Get the main window.
			Window Window = Application.Current.MainWindow;

			// Test if the container is a ContentPresenter.
			ContentPresenter Presenter = container as ContentPresenter;
			if (Presenter != null) {
				ComboBox Combo = Presenter.TemplatedParent as ComboBox;
				if (Combo != null) {
					ResourceKey = "ButtonSetByPlatformAndName";
				}
			}

			return Window.FindResource(ResourceKey) as DataTemplate;
		}

	}

}