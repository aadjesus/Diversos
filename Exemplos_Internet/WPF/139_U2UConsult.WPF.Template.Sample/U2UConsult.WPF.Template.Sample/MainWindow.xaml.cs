namespace U2UConsult.WPF.Template.Sample
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Provide some data to the listboxes.
            this.UnstyledBox.ItemsSource = FlemishRockband.Top3();
            this.ItemTemplateBox.ItemsSource = FlemishRockband.Top3();
            this.FullyStyledBox.ItemsSource = FlemishRockband.Top3();
        }

        /// <summary>
        /// Populates the bottom listbox.
        /// </summary>
        private void PopulateButton_Click(object sender, RoutedEventArgs e)
        {
            this.EmptyTemplateBox.ItemsSource = FlemishRockband.Top3();
        }

        /// <summary>
        /// Clears the bottom list box, reveiling the empty data template.
        /// </summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.EmptyTemplateBox.ItemsSource = null;
        }
    }
}
