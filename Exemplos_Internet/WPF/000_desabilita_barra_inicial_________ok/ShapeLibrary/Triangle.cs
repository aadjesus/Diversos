using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeShow.Library
{
    /// <summary>
    /// Represents a triangle.
    /// </summary>
    class Triangle : IShape
    {
        #region Private Members

        PointCollection _points;
        Polygon _primitive;
        TranslateTransform _translateTransform;

        #endregion

        #region IShape Members

        public PointCollection Points
        {
            get { return _points; }
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

        public Triangle()
        {
            _points = new PointCollection(3);
            _points.Add(new Point(0, 2));
            _points.Add(new Point(1, 0));
            _points.Add(new Point(2, 2));

            _translateTransform = new TranslateTransform(0, 0);

            _primitive = new Polygon();
            _primitive.Points = _points;
            _primitive.RenderTransform = _translateTransform;
            _primitive.Stretch = Stretch.Fill;
            _primitive.Stroke = Brushes.Black;
            _primitive.StrokeThickness = 2;
        }

        public Triangle(Double height, Double width) : this()
        {
            Height = height;
            Width = width;
        }

        public Triangle(Double height, Double width, Double top, Double left) : this(height, width)
        {
            Top = top;
            Left = left;
        }

        #endregion
    }
}
