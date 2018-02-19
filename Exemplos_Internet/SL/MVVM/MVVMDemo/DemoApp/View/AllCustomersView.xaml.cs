using Galador.Applications;
using DemoApp.ViewModel;
namespace DemoApp.View
{
	[DataView(typeof(AllCustomersViewModel))]
	public partial class AllCustomersView : System.Windows.Controls.UserControl
	{
		public AllCustomersView()
		{
			InitializeComponent();
		}
	}
}