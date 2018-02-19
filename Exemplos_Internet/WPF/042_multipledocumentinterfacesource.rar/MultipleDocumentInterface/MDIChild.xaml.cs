using System.Windows;
using System.Windows.Controls;

namespace MultipleDocumentInterface
{
    /// <summary>
    /// Interaction logic for MDIChild.xaml
    /// </summary>
    public partial class MDIChild : UserControl
    {
        private FrameworkElement control;

        /// <summary>
        /// Gets or sets the main visual control.
        /// </summary>
        /// <value>The control.</value>
        public FrameworkElement Control
        {
            get { return control; }
            set
            {
                ContentGrid.Children.Clear();
                ContentGrid.Children.Add(control = value);

                if (ContentGrid.Width != double.NaN)
                    Width = ContentGrid.Width;

                if (ContentGrid.Height != double.NaN)
                    Height = ContentGrid.Height + 27;
            }
        }

        /// <summary>
        /// Gets or sets the MDI parent.
        /// </summary>
        /// <value>The MDI parent.</value>
        public MDIParent MDIParent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the MDI child is [in front].
        /// </summary>
        /// <value><c>true</c> if [in front]; otherwise, <c>false</c>.</value>
        public bool InFront { get; set; }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return (string)TitleValue.Content; }
            set { TitleValue.Content = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDIChild"/> class.
        /// </summary>
        public MDIChild()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MDIParent.RemoveChild(this);
        }
    }
}