using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScrollControl
{
    /// <summary>
    /// Provides a scrollable ScrollViewer which
    /// allows user to apply friction, which in turn
    /// animates the ScrollViewer position, giving it
    /// the appearance of sliding into position
    /// </summary>
    public class FrictionScrollViewer : ScrollViewer
    {
        #region Data

        // Used when manually scrolling.
        private DispatcherTimer animationTimer = new DispatcherTimer();
        private Point previousPoint;
        private Point scrollStartOffset;
        private Point scrollStartPoint;
        private Point scrollTarget;
        private Vector velocity;

        #endregion

        #region Ctor
        /// <summary>
        /// Overrides metadata
        /// </summary>
        static FrictionScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FrictionScrollViewer),
            new FrameworkPropertyMetadata(typeof(FrictionScrollViewer)));
        }

        /// <summary>
        /// Initialises all friction related variables
        /// </summary>
        public FrictionScrollViewer()
        {
            Friction = 0.95;
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += HandleWorldTimerTick;
            animationTimer.Start();
        }
        #endregion

        #region DPs
        /// <summary>
        /// The ammount of friction to use. Use the Friction property to set a 
        /// value between 0 and 1, 0 being no friction 1 is full friction 
        /// meaning the panel won’t "auto-scroll".
        /// </summary>
        public double Friction
        {
            get { return (double)GetValue(FrictionProperty); }
            set { SetValue(FrictionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Friction.  
        public static readonly DependencyProperty FrictionProperty =
            DependencyProperty.Register("Friction", typeof(double), 
            typeof(FrictionScrollViewer), new UIPropertyMetadata(0.0));
        #endregion

        #region overrides 
        /// <summary>
        /// Get position and CaptureMouse
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (IsMouseOver)
            {
                // Save starting point, used later when determining how much to scroll.
                scrollStartPoint = e.GetPosition(this);
                scrollStartOffset.X = HorizontalOffset;
                scrollStartOffset.Y = VerticalOffset;
                // Update the cursor if can scroll or not. 
                Cursor = (ExtentWidth > ViewportWidth) || 
                    (ExtentHeight > ViewportHeight) ? 
                    Cursors.ScrollAll : Cursors.Arrow;
                CaptureMouse();
            }
            base.OnPreviewMouseDown(e);
        }


        /// <summary>
        /// If IsMouseCaptured scroll to correct position. 
        /// Where position is updated by animation timer
        /// </summary>
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(this);
                // Determine the new amount to scroll.
                Point delta = new Point(scrollStartPoint.X - 
                    currentPoint.X, scrollStartPoint.Y - currentPoint.Y);
                scrollTarget.X = scrollStartOffset.X + delta.X;
                scrollTarget.Y = scrollStartOffset.Y + delta.Y;
                // Scroll to the new position.
                ScrollToHorizontalOffset(scrollTarget.X);
                ScrollToVerticalOffset(scrollTarget.Y);
            }
            base.OnPreviewMouseMove(e);
        }


        /// <summary>
        /// Release MouseCapture if its captured
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Cursor = Cursors.Arrow;
                ReleaseMouseCapture();
            }
            base.OnPreviewMouseUp(e);
        }
        #endregion

        #region Animation timer Tick
        /// <summary>
        /// Animation timer tick, used to move the scrollviewer incrementally
        /// to the desired position. This also uses the friction setting
        /// when determining how much to move the scrollviewer
        /// </summary>
        private void HandleWorldTimerTick(object sender, EventArgs e)
        {
            if (IsMouseCaptured)
            {
                Point currentPoint = Mouse.GetPosition(this);
                velocity = previousPoint - currentPoint;
                previousPoint = currentPoint;
            }
            else
            {
                if (velocity.Length > 1)
                {
                    ScrollToHorizontalOffset(scrollTarget.X);
                    ScrollToVerticalOffset(scrollTarget.Y);
                    scrollTarget.X += velocity.X;
                    scrollTarget.Y += velocity.Y;
                    velocity *= Friction;
                }
            }
        }
        #endregion
    }
}
