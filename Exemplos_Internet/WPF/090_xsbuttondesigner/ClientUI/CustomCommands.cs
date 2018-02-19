//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows.Input;


namespace ClientUI
{
    public class CustomCommands
    {
        public static readonly ICommand ToggleSecondView = new RoutedCommand( "ToggleSecondViewCommand", typeof( CustomCommands ) );
    }
}
