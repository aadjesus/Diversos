using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UserControlAsDataTemplateDemo
{
    /// <summary>
    /// Animation class
    /// </summary>
    public static class Animations
    {
        static Animations()
        {
        }

        /// <summary>
        /// Fade Animation
        /// </summary>
        /// <param name="control">The UIElement to apply a fade animation too</param>
        /// <param name="sourceOpacity">The Source Opacity value (0.0 - 1.0)</param>
        /// <param name="targetOpactity">The Target Opacity (0.0 - 1.0)</param>
        /// <param name="milliseconds">The duration of the animation in milliseconds</param>
        public static void Fade(this UIElement control, double sourceOpacity, double targetOpactity, int milliseconds)
        {
            control.BeginAnimation(UIElement.OpacityProperty, 
                                   new DoubleAnimation(sourceOpacity, targetOpactity, 
                                   new Duration(TimeSpan.FromMilliseconds(milliseconds))));
        }
    }
}