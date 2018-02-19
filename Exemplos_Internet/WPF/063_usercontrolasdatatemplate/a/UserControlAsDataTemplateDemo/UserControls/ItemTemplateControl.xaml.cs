#region Using

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

#endregion

namespace UserControlAsDataTemplateDemo.UserControls
{
    /// <summary>
    /// Interaction logic for ItemTemplateControl.xaml
    /// </summary>
    public partial class ItemTemplateControl : UserControl
    {

        #region Constructor

        /// <summary>
        /// The ItemTemplateControl constructor
        /// </summary>
        public ItemTemplateControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Loaded Event Handler

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(this.AgeTextBox.Text) > 40)
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.DarkGray;
                MainBorder.Background = brush;
            }

            if (this.ProgressReporter.PercentToShow > 80.0)
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.Lavender;
                MainBorder.Background = brush;
            }

            // An extension method to create and start an animation
            Animations.Fade(this, 0.0, (ProgressReporter.PercentToShow / 100), 1000);
        }

        #endregion

    }
}