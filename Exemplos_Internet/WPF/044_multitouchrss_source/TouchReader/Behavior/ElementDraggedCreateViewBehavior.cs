using System;
using System.Windows;
using Multitouch.Framework.WPF.Controls;
using VirtualDreams.TouchReader.Factory;

namespace VirtualDreams.TouchReader.Behavior
{
    public class ElementDraggedCreateViewBehavior
    {
        #region PanelViewFactory

        /// <summary>
        /// PanelViewFactory Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty PanelViewFactoryProperty =
            DependencyProperty.RegisterAttached("PanelViewFactory", typeof(IPanelViewFactory), typeof(ElementDraggedCreateViewBehavior),
                                                new FrameworkPropertyMetadata(null, OnPanelViewFactoryChanged));

        /// <summary>
        /// Gets the PanelViewFactory property.  This dependency property 
        /// indicates which factory will be used to create the views.
        /// </summary>
        public static IPanelViewFactory GetPanelViewFactory(DependencyObject d)
        {
            if (d == null)
                throw new ArgumentNullException("d");
            return (IPanelViewFactory)d.GetValue(PanelViewFactoryProperty);
        }

        /// <summary>
        /// Sets the PanelViewFactory property.  This dependency property 
        /// indicates which factory will be used to create the views.
        /// </summary>
        public static void SetPanelViewFactory(DependencyObject d, IPanelViewFactory value)
        {
            if (d == null)
                throw new ArgumentNullException("d");
            d.SetValue(PanelViewFactoryProperty, value);
        }

        /// <summary>
        /// Handles changes to the PanelViewFactory property.
        /// </summary>
        private static void OnPanelViewFactoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as DraggableScrollViewer;
            if (scrollViewer != null)
            {
                if (e.NewValue != null)
                    scrollViewer.ElementDragged += ControlElementDragged;
                else
                    scrollViewer.ElementDragged -= ControlElementDragged;
            }
        }

        #endregion

        static void ControlElementDragged(object sender, ElementRoutedEventArgs e)
        {
            IPanelViewFactory viewFactory = GetPanelViewFactory((DependencyObject)sender);
            if (viewFactory == null) return;
            var position = e.GetPosition(viewFactory.Panel);
            var offset = e.Offset;
            viewFactory.CreateViewFromDataContext(e.Element.DataContext, new Point(position.X - offset.X, position.Y - offset.Y));
        }
       
    }
}
