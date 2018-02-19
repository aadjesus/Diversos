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
using System.Windows.Shapes;

namespace CrypTool
{
    /// <summary>
    /// Interaction logic for DlgHowTo.xaml
    /// </summary>

    public partial class DlgHowTo : System.Windows.Window
    {

        public DlgHowTo()
        {
            InitializeComponent();
        }
        private void ButtonClose_OnClick(object sender, RoutedEventArgs arg)
        {
            checkShowAgain();
            Close();
        }
        private void checkShowAgain()
        {
            if ((bool)this.checkBoxShowAgain.IsChecked)
            {
                CrypTool.AppLogic.StartOptions startOptions = new CrypTool.AppLogic.StartOptions();
                startOptions.setShowHowToStartDialog(false);
                startOptions.saveOptions();
            }
        }
    }
}