//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows;
using System.Windows.Input;

using CustomControlsClientUIClientUI;


namespace ClientUI
{
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            comboLayout.SelectedValue = Layout;
        }


        private void OnSelectLayout( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {
            if( e.AddedItems[0] != null )
            {
                Layout = (Layout)e.AddedItems[0];
            }
        }

        private void OnOfficeButtonClick( object sender, ExecutedRoutedEventArgs e )
        {
            MessageBox.Show( "OfficeButton notification received in derived Window class!" );
        }


        private void OnHelp( object sender, ExecutedRoutedEventArgs e )
        {
            MessageBox.Show( "Display Help file!" );
        }
    }
}
