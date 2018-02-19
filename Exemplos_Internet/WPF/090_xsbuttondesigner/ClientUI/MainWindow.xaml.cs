//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows;
using System.Windows.Input;


namespace ClientUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Toggle visibility of right-side panel on or off
            CommandBindings.Add( new CommandBinding( CustomCommands.ToggleSecondView, ( sender, args ) =>
                {
                    panelRight.Visibility = panelRight.IsVisible ? Visibility.Collapsed : Visibility.Visible;
                }
            ) );
        }
    }
}
