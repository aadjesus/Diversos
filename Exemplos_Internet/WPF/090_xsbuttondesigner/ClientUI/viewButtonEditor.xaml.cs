//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows;
using System.Windows.Controls;


namespace ClientUI
{
    public partial class viewButtonEditor : UserControl
    {
        private PropertyValuesSnapshot _cacheButtonProperties = new PropertyValuesSnapshot();
        private PropertyValuesSnapshot _cacheDemoAreaProperties = new PropertyValuesSnapshot();


        public viewButtonEditor()
        {
            InitializeComponent();

            _cacheButtonProperties.CopyValues( btnTarget );
            _cacheDemoAreaProperties.CopyValues( demoArea );
        }


        private void OnResetToDefaults( object sender, RoutedEventArgs e )
        {
            _cacheButtonProperties.RestoreValues( btnTarget );
            _cacheDemoAreaProperties.RestoreValues( demoArea );
        }
    }
}
