using System.Windows;
using Ricciolo.PaperBoy.Feeds;
using VirtualDreams.TouchReader.Behavior;
using VirtualDreams.TouchReader.ViewModel;

namespace VirtualDreams.TouchReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Initialize the form by creating the root folder view, using the mainPanel's ViewFactory
            PanelBehavior.GetViewFactory(mainPanel).CreateViewFromDataContext(new FolderViewModel(FeedsManager.Current.RootFolder), new Point(100, 100));
        }
    }
}
