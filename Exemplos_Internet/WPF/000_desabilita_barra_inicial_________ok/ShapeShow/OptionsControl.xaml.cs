using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ShapeShow
{
    /// <summary>
    /// Interaction logic for OptionsControl.xaml
    /// </summary>
    public partial class OptionsControl : UserControl
    {
        public OptionsControl()
        {
            InitializeComponent();

            // Provides design-time configuration.
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Opacity = 0.2;
            }
        }
    }
}
