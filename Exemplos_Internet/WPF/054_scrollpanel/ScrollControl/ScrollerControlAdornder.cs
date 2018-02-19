using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScrollControl
{
    /// <summary>
    /// Hosts an opaque Viewport3D in the adorner layer above a Panel3D.
    /// </summary>
    public class ScrollerControlAdorner : Adorner
    {
        #region Data

        private enum ScrollDirection { Left, Right, None };
        private ScrollDirection currentDirection = ScrollDirection.None;
        private ArrayList logicalChildren;
        private readonly Canvas host = new Canvas();
        private ScrollerControl sc;
        private DispatcherTimer timer = new DispatcherTimer();
        private int spacer = 5;

        #endregion // Data

        #region Constructor

        public ScrollerControlAdorner(ScrollerControl sc)
            : base(sc)
        {

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.IsEnabled = false;
            timer.Tick += new EventHandler(timer_Tick);
            this.sc = sc;

            host.Width = (double)this.AdornedElement.GetValue(ActualWidthProperty);
            host.Height = (double)this.AdornedElement.GetValue(ActualHeightProperty);
            host.VerticalAlignment = VerticalAlignment.Center;
            host.Margin = new Thickness(0);



            Button btnLeft = new Button();
            Style styleLeft = sc.TryFindResource("leftButtonStyle") as Style;
            if (styleLeft != null)
                btnLeft.Style = styleLeft;
            btnLeft.MouseEnter += new System.Windows.Input.MouseEventHandler(btnLeft_MouseEnter);
            btnLeft.MouseLeave += new System.Windows.Input.MouseEventHandler(btnLeft_MouseLeave);
            btnLeft.SetValue(Canvas.TopProperty, (host.Height / 2) - (42 / 2));
            btnLeft.SetValue(Canvas.LeftProperty, (double)spacer);

            Button btnRight = new Button();
            Style styleRight = sc.TryFindResource("rightButtonStyle") as Style;
            if (styleRight != null)
                btnRight.Style = styleRight;
            btnRight.MouseEnter += new System.Windows.Input.MouseEventHandler(btnRight_MouseEnter);
            btnRight.MouseLeave += new System.Windows.Input.MouseEventHandler(btnRight_MouseLeave);
            btnRight.SetValue(Canvas.TopProperty, (host.Height / 2) - (42 / 2));
            btnRight.SetValue(Canvas.LeftProperty, (double)(host.Width - (42 + spacer)));




            host.Children.Add(btnLeft);
            host.Children.Add(btnRight);
            base.AddLogicalChild(host);
            base.AddVisualChild(host);
        }

        #endregion // Constructor

        void timer_Tick(object sender, EventArgs e)
        {
            switch (currentDirection)
            {
                case ScrollDirection.Left:
                    sc.Scroll(new Point(10, 0));
                    break;
                case ScrollDirection.Right:
                    sc.Scroll(new Point(-10, 0));
                    break;
            }
        }

        void btnLeft_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            currentDirection = ScrollDirection.None;
            timer.IsEnabled = false;
        }

        void btnLeft_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            currentDirection = ScrollDirection.Left;
            timer.IsEnabled = true;

        }


        void btnRight_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            currentDirection = ScrollDirection.None;
            timer.IsEnabled = false;
        }

        void btnRight_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            currentDirection = ScrollDirection.Right;
            timer.IsEnabled = true;

        }




        #region Measure/Arrange

        /// <summary>
        /// Allows the control to determine how big it wants to be.
        /// </summary>
        /// <param name="constraint">A limiting size for the control.</param>
        protected override Size MeasureOverride(Size constraint)
        {
            return constraint;
        }

        /// <summary>
        /// Positions and sizes the control.
        /// </summary>
        /// <param name="finalSize">The actual size of the control.</param>		
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect rect = new Rect(new Point(), finalSize);

            host.Arrange(rect);
            return finalSize;
        }

        #endregion // Measure/Arrange

        #region Visual Children

        /// <summary>
        /// Required for the element to be rendered.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Required for the element to be rendered.
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            return host;
        }

        #endregion // Visual Children

        #region Logical Children

        /// <summary>
        /// Required for the displayed element to inherit property values
        /// from the logical tree, such as FontSize.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (logicalChildren == null)
                {
                    logicalChildren = new ArrayList();
                    logicalChildren.Add(host);
                }

                return logicalChildren.GetEnumerator();
            }
        }

        #endregion // Logical Children
    }
}