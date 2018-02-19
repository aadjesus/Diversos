/****************************************************************************
 *                                                                          *
 *      Created By: Ernie Booth                                             *
 *      Contact: ebooth@microsoft.com - http://blogs.msdn.com/ebooth        *
 *      Last Modified: 6/23/2006                                            *
 *                                                                          *
 * **************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ink_A_Mator
{
    /// <summary>
    /// Interaction logic for DuplicateDialog.xaml
    /// </summary>

    public partial class DuplicateDialog : Window
    {

        public DuplicateDialog()
        {
            InitializeComponent();            
        }

        void OnSubmit(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (b.Tag.ToString() == "Insert")
            {
                DialogResult = false;
            }
            else if (b.Tag.ToString() == "Replace")
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = null;
            }
        }
    }
}