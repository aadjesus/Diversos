using System;
using System.Windows;

namespace CustomWindow
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        private void show_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show(
                this,
                this._message.Text, 
                this._caption.Text,
                (MessageBoxButton)this._buttons.SelectedItem,
                (MessageBoxImage)Enum.Parse(typeof(MessageBoxImage), (string)this._image.SelectedItem));
        }
	}
}