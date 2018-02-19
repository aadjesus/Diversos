using System;
using System.Collections.Generic;
using System.Linq;
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
using PopupLib;

namespace WpfPopup
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}

		private void btnShow_Click(object sender, RoutedEventArgs e)
		{
			//Pop up a 150-pixel wide and 300-pixel high window on the right lower corner of the screen
			PopupWinHelper.ShowPopUp(150, 300, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "bg.png")), txtMsg.Text, new Thickness(20));
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			//Exit program
			Application.Current.Shutdown();
		}

	}
}
