//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows.Input;


namespace CustomControls
{
    public class CustomWindowCommands
    {
        public static readonly ICommand MinimizeWindow = new RoutedCommand( "MinimizeWindowCommand", typeof( CustomWindowCommands ) );
        public static readonly ICommand MaximizeWindow = new RoutedCommand( "MaximizeWindowCommand", typeof( CustomWindowCommands ) );

        public static readonly ICommand DisplayOfficeButtonOptions = new RoutedCommand( "DisplayOfficeButtonOptions", typeof( CustomWindowCommands ) );
    }
}
