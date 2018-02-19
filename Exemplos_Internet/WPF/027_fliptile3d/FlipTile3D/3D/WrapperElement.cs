using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace FlipTile3D
{
    /// <summary>
    /// Source code origin Kevin Moore, Kevin's WPF Bag-o-Tricks
    /// http://j832.com/bagotricks/
    /// 
    /// Modified by Sacha Barber for codeproject article
    /// </summary>
    public abstract class WrapperElement<TElement> : FrameworkElement
        where TElement : UIElement
    {
        #region Data
        private readonly TElement wrappedElement;
        #endregion

        #region Ctor
        protected WrapperElement(TElement element)
        {
            Util.RequireNotNull(element, "element");

            wrappedElement = element;

            AddVisualChild(wrappedElement);
        }
        #endregion

        #region Protected Methods
        protected override Size MeasureOverride(Size availableSize)
        {
            wrappedElement.Measure(availableSize);
            return wrappedElement.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            wrappedElement.Arrange(new Rect(finalSize));
            return wrappedElement.RenderSize;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            Util.RequireArgumentRange(index == 0, "index", "index must be 0");
            return wrappedElement;
        }

        protected TElement WrappedElement { get { return wrappedElement; } }
        #endregion

    }
}
