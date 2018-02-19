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
    /// Hosts an opaque Canvas with 2 scroll buttons (left and right) which
    /// scrolls the adornerElement either left or right
    /// </summary>
    public class ItemHolderAdorner : Adorner
    {
        #region Data


        private ArrayList logicalChildren;
        private readonly Grid host = new Grid();

        #endregion // Data

        #region Constructor

        public ItemHolderAdorner(ItemHolder adornedElement, Point position)
            : base(adornedElement)
        {

            host.Width = adornedElement.Width;
            host.Height = adornedElement.Height;
            host.HorizontalAlignment = HorizontalAlignment.Left;
            host.VerticalAlignment = VerticalAlignment.Top;
            host.IsHitTestVisible = false;
            this.IsHitTestVisible = false;


            Border outerBorder = new Border();
            outerBorder.Background = Brushes.White;
            outerBorder.Margin = new Thickness(0);
            outerBorder.CornerRadius = new CornerRadius(5);
            outerBorder.Width = adornedElement.Width;
            outerBorder.Height = adornedElement.Height;
            
            Border innerBorder = new Border();
            innerBorder.Background = Brushes.CornflowerBlue;
            innerBorder.Margin = new Thickness(1);
            innerBorder.CornerRadius = new CornerRadius(5);


            outerBorder.Child = innerBorder;
            innerBorder.Child = new Grid
                {
                    Background = new VisualBrush(adornedElement as Visual),
                    Margin = new Thickness(1)
                };

            double scale = 1.5;

            host.RenderTransform = new ScaleTransform(scale, scale, -0.5, -0.5);
            double hostWidth = host.Width * scale;
            double diff = (double)(adornedElement.Width / 4);
            Thickness margin = new Thickness();
            margin.Top = diff * -1;
            margin.Left = diff * -1;
            host.Margin = margin;

            host.Children.Add(outerBorder);
            base.AddLogicalChild(host);
            base.AddVisualChild(host);
        }



        #endregion // Constructor

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