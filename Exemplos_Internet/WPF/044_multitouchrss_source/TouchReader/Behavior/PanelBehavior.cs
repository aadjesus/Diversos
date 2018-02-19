using System;
using System.Windows;
using System.Windows.Controls;
using VirtualDreams.TouchReader.Factory;

namespace VirtualDreams.TouchReader.Behavior
{
    public class PanelBehavior
    {
        #region ViewFactory

        /// <summary>
        /// ViewFactory Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ViewFactoryProperty =
            DependencyProperty.RegisterAttached("ViewFactory", typeof(IPanelViewFactory), typeof(PanelBehavior),
                new UIPropertyMetadata(null,OnViewFactoryChanged));

        /// <summary>
        /// Gets the ViewFactory property.  This dependency property 
        /// indicates which factory is used to create this panel's views.
        /// </summary>
        public static IPanelViewFactory GetViewFactory(DependencyObject d)
        {
            return (IPanelViewFactory)d.GetValue(ViewFactoryProperty);
        }

        /// <summary>
        /// Sets the ViewFactory property.  This dependency property 
        /// indicates which factory is used to create this panel's views.
        /// </summary>
        public static void SetViewFactory(DependencyObject d, IPanelViewFactory value)
        {
            d.SetValue(ViewFactoryProperty, value);
        }

        /// <summary>
        /// Handles changes to the ViewFactory property.
        /// </summary>
        private static void OnViewFactoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var targetPanel = d as Panel;
            if (targetPanel == null)
                throw new InvalidOperationException(
                  "The ViewFactory attached property can only be applied to Panel controls.");
            if (e.NewValue != null)
                ((IPanelViewFactory) e.NewValue).Panel = targetPanel;
        }

        #endregion
    }
}
