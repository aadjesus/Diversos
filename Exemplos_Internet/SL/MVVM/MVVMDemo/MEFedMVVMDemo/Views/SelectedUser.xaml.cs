using System.Windows.Controls;
using Galador.Applications;
using MEFedMVVMDemo.ViewModels;

namespace MEFedMVVMDemo.Views
{
    /// <summary>
    /// Interaction logic for SelectedUser.xaml
    /// </summary>
	[DataView(typeof(SelectedUserViewModel))]
    public partial class SelectedUser : UserControl
    {
        public SelectedUser()
        {
            InitializeComponent();

			DataContext = new SelectedUserViewModel();
        }
    }
}
