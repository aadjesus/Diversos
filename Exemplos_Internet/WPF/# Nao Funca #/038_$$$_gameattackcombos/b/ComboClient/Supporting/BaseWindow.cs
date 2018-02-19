using System.Windows;
using System.Windows.Controls.Primitives;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// The base window class for all windows in the application.
	/// </summary>
	public class BaseWindow : Window {

		/// <summary>
		/// Initializes an instance of BaseWindow with default settings.
		/// </summary>
		public BaseWindow() : base() {
			ResizeMode = ResizeMode.CanMinimize;
			WindowStyle = WindowStyle.None;
			AllowsTransparency = true;
			SetResourceReference(StyleProperty, "Window");
		}

		/// <summary>
		/// Overriden to hook the titlebar parts interactivity.
		/// </summary>
		public override void OnApplyTemplate() {
			base.OnApplyTemplate();
			HookTitlebarParts();
		}

		/// <summary>
		/// Hooks into the necessary events of the titlebar parts.
		/// </summary>
		protected void HookTitlebarParts() {
			// Try to get the titlebar in the template to hook into its MouseLeftButtonDown event.
			UIElement Titlebar = Template.FindName("PART_Titlebar", this) as UIElement;
			if (Titlebar != null) {
				Titlebar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Titlebar_MouseLeftButtonDown);
			}

			// Try to find each titlebar button in the template to hook into its Click event.
			ButtonBase TitlebarButton = Template.FindName("PART_MinimizeButton", this) as ButtonBase;
			if (TitlebarButton != null) {
				TitlebarButton.Click += new RoutedEventHandler(TitlebarMinimizeButton_Click);
			}
			TitlebarButton = Template.FindName("PART_CloseButton", this) as ButtonBase;
			if (TitlebarButton != null) {
				TitlebarButton.Click += new RoutedEventHandler(TitlebarCloseButton_Click);
			}
		}

		void Titlebar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			DragMove();
			e.Handled = true;
		}

		void TitlebarMinimizeButton_Click(object sender, RoutedEventArgs e) {
			WindowState = WindowState.Minimized;
			e.Handled = true;
		}

		void TitlebarCloseButton_Click(object sender, RoutedEventArgs e) {
			Close();
			e.Handled = true;
		}

	}

}