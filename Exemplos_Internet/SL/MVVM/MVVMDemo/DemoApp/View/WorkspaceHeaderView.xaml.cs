using System.Windows.Controls;
using Galador.Applications;

namespace DemoApp.View
{
	/// <summary>
	/// Interaction logic for CustomerHeaderView.xaml
	/// </summary>
	[DataView(typeof(WorkspaceViewModel), "header")]
	public partial class CustomerHeaderView : UserControl
	{
		public CustomerHeaderView()
		{
			InitializeComponent();
		}
	}
}
