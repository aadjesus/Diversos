using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeShow.Library
{
    /// <summary>
    /// Represents a circle or ellipse.
    /// </summary>
    class Circle : IShape
    {
        #region Private Members

        Ellipse _primitive;
        TranslateTransform _translateTransform;

        #endregion

        #region IShape Members

        public PointCollection Points
        {
            get { return null; }
        }

        public Double Height
        {
            get { return _primitive.Height; }
            set { _primitive.Height = value; }
        }

        public Double Width
        {
            get { return _primitive.Width; }
            set { _primitive.Width = value; }
        }

        public Double Top
        {
            get { return _translateTransform.Y; }
            set { _translateTransform.Y = value; }
        }

        public Double Left
        {
            get { return _translateTransform.X; }
            set { _translateTransform.X = value; }
        }

        public Brush FillBrush
        {
            get { return _primitive.Fill; }
            set { _primitive.Fill = value; }
        }

        public Double OutlineWidth
        {
            get { return _primitive.StrokeThickness; }
            set { _primitive.StrokeThickness = value; }
        }

        public Brush OutlineBrush
        {

            get { return _primitive.Stroke; }
            set { _primitive.Stroke = value; }
        }

        public UIElement UIElement
        {
            get { return _primitive; }
        }

        #endregion

        #region Constructors

        public Circle()
        {
            _translateTransform = new TranslateTransform(0, 0);

            _primitive = new Ellipse();
            _primitive.Height = 1;
            _primitive.Width = 1;
            _primitive.RenderTransform = _translateTransform;
            _primitive.Stretch = Stretch.Fill;
            _primitive.Stroke = Brushes.Black;
            _primitive.StrokeThickness = 2;
        }

        public Circle(Double height, Double width) : this()
        {
            Height = height;
            Width = width;
        }

        public Circle(Double height, Double width, Double top, Double left) : this(height, width)
        {
            Top = top;
            Left = left;
        }

        #endregion
    }
}
