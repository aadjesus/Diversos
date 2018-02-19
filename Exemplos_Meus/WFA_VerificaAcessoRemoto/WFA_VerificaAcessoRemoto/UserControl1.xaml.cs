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

namespace WFA_VerificaAcessoRemoto
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //if (System.Windows.Forms.SystemInformation.TerminalServerSession)
            //{
            //    this.imgLogoGlobusMais.Visibility = System.Windows.Visibility.Collapsed;
            //    this.imgFundo.Visibility = System.Windows.Visibility.Collapsed;
            //}
        }
    }
}
