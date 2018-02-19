using System.Windows.Controls;
using Galador.Applications;
using MEFedMVVMDemo.ViewModels;

namespace MEFedMVVMDemo.Views
{
	/// <summary>
	/// Interaction logic for UsersScreen.xaml
	/// </summary>
	[DataView(typeof(TestViewModel))]
	public partial class UsersScreen : UserControl
	{
		public UsersScreen()
		{
			InitializeComponent();
			DataContext = Composition.GetInstance<TestViewModel>();
		}
	}
}
