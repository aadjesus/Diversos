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
    /// Interaction logic for ProgressReporter.xaml
    /// </summary>
    public partial class ProgressReporter : UserControl
    {

        #region Custom Dependency Property

        /// <summary>
        /// The dependency property of the ProgressReporter UserControl to display in the GUI
        /// </summary>
        public static DependencyProperty PercentProperty = DependencyProperty.Register("PercentToShow", typeof(double), typeof(ProgressReporter));
        /// <summary>
        /// The PercentToShow value
        /// </summary>
        public double PercentToShow
        {
            get { return (double)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProgressReporter()
        {
            InitializeComponent();
        }

        #endregion

    }
}