using System.Windows;
using Example.Controls;
using MultipleDocumentInterface;

namespace Example
{
    /// <summary>
    /// Interaction logic for ExampleWindow.xaml
    /// </summary>
    public partial class ExampleWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleWindow"/> class.
        /// </summary>
        public ExampleWindow()
        {
            InitializeComponent();

            MDIHost.AddChild(new MDIChild()
            {
                Title = "Example Without Content"
            });

            MDIHost.AddChild(new MDIChild()
            {
                Title = "Example With Content",
                Control = new ContentOne()
            });
        }

        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MDI Example App\nCreated by xadet\nwww.xadet.net", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}