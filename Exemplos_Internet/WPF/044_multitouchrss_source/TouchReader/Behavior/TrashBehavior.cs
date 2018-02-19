using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Multitouch.Framework.WPF.Input;
using VirtualDreams.TouchReader.ViewModel;

namespace VirtualDreams.TouchReader.Behavior
{
    public class TrashBehavior
    {
        private static readonly Dictionary<DoubleAnimation, FrameworkElement> AnimationViews =
            new Dictionary<DoubleAnimation, FrameworkElement>();

        private static readonly TimeSpan DefaultAnimationDuration = TimeSpan.FromSeconds(0.5);

        private static readonly List<Trash> Trashes = new List<Trash>();

        #region SourcePanel

        /// <summary>
        /// SourcePanel Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SourcePanelProperty =
            DependencyProperty.RegisterAttached("SourcePanel", typeof (Panel), typeof (TrashBehavior),
                                                new FrameworkPropertyMetadata(null,
                                                                              new PropertyChangedCallback(
                                                                                  OnSourcePanelChanged)));

        /// <summary>
        /// Gets the SourcePanel property.  This dependency property 
        /// indicates which panel uses this element as trash.
        /// </summary>
        public static Panel GetSourcePanel(DependencyObject d)
        {
            if (d == null)
                throw new ArgumentNullException("d");
            return (Panel) d.GetValue(SourcePanelProperty);
        }

        /// <summary>
        /// Sets the SourcePanel property.  This dependency property 
        /// indicates which panel uses this element as trash.
        /// </summary>
        public static void SetSourcePanel(DependencyObject d, Panel value)
        {
            if (d == null)
                throw new ArgumentNullException("d");
            d.SetValue(SourcePanelProperty, value);
        }

        /// <summary>
        /// Handles changes to the SourcePanel property.
        /// </summary>
        private static void OnSourcePanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var trash = d as Trash;
            if (trash == null)
                throw new InvalidOperationException(
                    "The SourcePanel attached property can only be applied to Trash controls.");

            var oldPanel = e.OldValue as Panel;
            if (oldPanel != null)
                MultitouchScreen.RemovePreviewContactRemovedHandler(oldPanel, SourcePanel_PreviewContactRemoved);

            var panel = e.NewValue as Panel;
            if (panel != null)
            {
                if (!Trashes.Contains(trash)) Trashes.Add(trash);
                MultitouchScreen.AddPreviewContactRemovedHandler(panel, SourcePanel_PreviewContactRemoved);
            }
            else
            {
                if (Trashes.Contains(trash)) Trashes.Remove(trash);
            }
        }

        #endregion

        private static void SourcePanel_PreviewContactRemoved(object sender, ContactEventArgs e)
        {
            var panel = (Panel) sender;

            var element = e.Contact.Captured as FrameworkElement;
            if (element == null || element is Trash || IsRootFolder(element)) return; // root folder can't be removed

            foreach (Trash trash in Trashes)
            {
                Rect trashRect = GetBoundingBox(trash, panel);
                Rect elementTrashIntersection = Rect.Intersect(GetBoundingBox(element, panel), trashRect);
                var contactRect = new Rect(e.Contact.GetPosition(panel),
                                           new Size(e.Contact.BoundingRect.Width, e.Contact.BoundingRect.Height));
                Rect contactTrashIntersection = Rect.Intersect(contactRect, trashRect);
                if ((elementTrashIntersection.Width > 0 || elementTrashIntersection.Height > 0) &&
                    (contactTrashIntersection.Width > 0 || contactTrashIntersection.Height > 0) &&
                    panel.Children.Contains(element))
                {
                    RemoveView(panel, element, e.Contact.GetPosition(panel));
                    break;
                }
            }
        }

        private static bool IsRootFolder(FrameworkElement element)
        {
            var viewModel = element.DataContext as FolderViewModel;
            return (viewModel != null && viewModel.IsRoot);
        }

        private static Rect GetBoundingBox(FrameworkElement element, Visual visual)
        {
            GeneralTransform transform = element.TransformToVisual(visual);
            Point topLeft = transform.Transform(new Point(0, 0));
            Point bottomRight = transform.Transform(new Point(element.ActualWidth, element.ActualHeight));
            return new Rect(topLeft, bottomRight);
        }

        public static void RemoveView(Panel panel, FrameworkElement view, Point animationDestination)
        {
            RemoveView(panel, view, animationDestination, DefaultAnimationDuration);
        }

        public static void RemoveView(Panel panel, FrameworkElement view, Point animationDestination,
                                      Duration animationDuration)
        {
            if (!panel.Children.Contains(view)) return;

            var zeroAnimation = new DoubleAnimation(0, animationDuration) {AccelerationRatio = 1};

            Rect viewBoundingBox = GetBoundingBox(view, panel);

            var scale = new ScaleTransform(1, 1)
                            {
                                CenterX = view.ActualWidth/2 + (animationDestination.X - viewBoundingBox.Left)/2,
                                CenterY = view.ActualHeight/2 + (animationDestination.Y - viewBoundingBox.Top)/2
                            };

            var group = view.RenderTransform as TransformGroup;
            if (group != null)
                group.Children.Add(scale);
            else
                view.RenderTransform = new TransformGroup {Children = {view.RenderTransform, scale}};

            AnimationViews.Add(zeroAnimation, view);

            view.BeginAnimation(UIElement.OpacityProperty, zeroAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, zeroAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, zeroAnimation);

            zeroAnimation.CurrentStateInvalidated += AnimationCurrentStateInvalidated;
        }

        private static void AnimationCurrentStateInvalidated(object sender, EventArgs e)
        {
            var da = sender as DoubleAnimation;
            if (da != null)
            {
                FrameworkElement view = AnimationViews[da];
                var parent = view.Parent as Panel;
                if (parent != null) parent.Children.Remove(view);
                AnimationViews.Remove(da);
            }
        }
    }
}