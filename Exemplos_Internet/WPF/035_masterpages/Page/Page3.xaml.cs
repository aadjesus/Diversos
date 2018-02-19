using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterPages.Page
{
	/// <summary>
	/// Interaction logic for Page3.xaml
	/// </summary>

	public partial class Page3 : System.Windows.Controls.Page
	{
		public Page3()
		{
			InitializeComponent();
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			btnShowMessage.Click += new RoutedEventHandler(BtnShowMessage_Click);
		}

		private void BtnShowMessage_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("You clicked the button.");
		}

	}
}