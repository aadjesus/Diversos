using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GalaSoftLb.Wpf.SynchronizedAnimations.Xbap
{
    public class DataItem : DependencyObject
    {
        // Dependency property responsible for starting/stopping the animation.
        public bool IsBlink
        {
            get { return (bool) GetValue(IsBlinkProperty); }
            set { SetValue(IsBlinkProperty, value); }
        }

        public static readonly DependencyProperty IsBlinkProperty =
            DependencyProperty.Register("IsBlink",
                typeof(bool),
                typeof(DataItem),
                new UIPropertyMetadata(false));

        // Just a description for the item.
        public string Description
        {
            get { return (string) GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description",
                typeof(string),
                typeof(DataItem),
                new UIPropertyMetadata("-none-"));
    }
}
