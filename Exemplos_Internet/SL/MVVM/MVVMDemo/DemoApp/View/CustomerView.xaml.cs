using Galador.Applications;
using DemoApp.ViewModel;
namespace DemoApp.View
{
	[DataView(typeof(CustomerViewModel))]
	public partial class CustomerView : System.Windows.Controls.UserControl
	{
		public CustomerView()
		{
			InitializeComponent();
		}
	}
}