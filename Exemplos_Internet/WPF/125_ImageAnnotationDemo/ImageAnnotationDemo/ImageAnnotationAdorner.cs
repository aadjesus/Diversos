using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageAnnotationDemo
{
    /// <summary>
    /// Renders an ImageAnnotationControl in an Image's adorner layer.
    /// </summary>
    public class ImageAnnotationAdorner : Adorner
    {
        #region Data

        private ImageAnnotationControl _control;
        private Point _location;
        private ArrayList _logicalChildren;

        #endregion // Data

        #region Constructor

        public ImageAnnotationAdorner(
            ImageAnnotation imageAnnotation,
            Image adornedImage,
            Style annotationStyle,
            Style annotationEditorStyle,
            Point location)
            : base(adornedImage)
        {
            _location = location;

            _control = new ImageAnnotationControl(imageAnnotation, annotationStyle, annotationEditorStyle);

            base.AddLogicalChild(_control);
            base.AddVisualChild(_control);
        }

        #endregion // Constructor

        #region UpdateTextLocation

        public void UpdateTextLocation(Point newLocation)
        {
            _location = newLocation;
            _control.InvalidateArrange();
        }

        #endregion // UpdateTextLocation

        #region Measure/Arrange

        /// <summary>
        /// Allows the control to determine how big it wants to be.
        /// </summary>
        /// <param name="constraint">A limiting size for the control.</param>
        protected override Size MeasureOverride(Size constraint)
        {
            _control.Measure(constraint);
            return _control.DesiredSize;
        }

        /// <summary>
        /// Positions and sizes the control.
        /// </summary>
        /// <param name="finalSize">The actual size of the control.</param>		
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect rect = new Rect(_location, finalSize);

            _control.Arrange(rect);

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

            return _control;
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
                if (_logicalChildren == null)
                {
                    _logicalChildren = new ArrayList();
                    _logicalChildren.Add(_control);
                }

                return _logicalChildren.GetEnumerator();
            }
        }

        #endregion // Logical Children
    }
}