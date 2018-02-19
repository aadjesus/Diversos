using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ImageButtons
{
    public sealed class ImageStates : DependencyObject
    {
        public static ImageSource GetNormalImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(NormalImageProperty);
        }

        public static void SetNormalImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(NormalImageProperty, value);
        }

        public static readonly DependencyProperty NormalImageProperty =
             DependencyProperty.RegisterAttached("NormalImage", typeof(ImageSource), typeof(ImageStates), new UIPropertyMetadata(null));
        public static ImageSource GetDisabledImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(DisabledImageProperty);
        }

        public static void SetDisabledImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(DisabledImageProperty, value);
        }
        public static readonly DependencyProperty DisabledImageProperty =
             DependencyProperty.RegisterAttached("DisabledImage", typeof(ImageSource), typeof(ImageStates), new UIPropertyMetadata(null));
        public static ImageSource GetFocusedImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(FocusedImageProperty);
        }

        public static void SetFocusedImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(FocusedImageProperty, value);
        }
        public static readonly DependencyProperty FocusedImageProperty =
             DependencyProperty.RegisterAttached("FocusedImage", typeof(ImageSource), typeof(ImageStates), new UIPropertyMetadata(null));
    }
}
