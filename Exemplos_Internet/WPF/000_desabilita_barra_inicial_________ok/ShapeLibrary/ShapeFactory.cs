using System;
using System.Windows;
using System.Windows.Media;

namespace ShapeShow.Library
{
    /// <summary>
    /// A factory class to create new shapes.
    /// </summary>
    public class ShapeFactory
    {
        #region Private Members

        static Int32 _shapeCount = 0;
        static Int32 ShapeCount
        {
            get
            {
                if (_shapeCount == 0)
                {
                    _shapeCount = Enum.GetNames(typeof(ShapeType)).Length;
                }

                return _shapeCount;
            }
        }

        static Int32 _colorCount = 0;
        static Int32 ColorCount
        {
            get
            {
                if (_colorCount == 0)
                {
                    _colorCount = Enum.GetNames(typeof(ShapeColor)).Length;
                }

                return _colorCount;
            }
        }

        static Double MinHeight = 50;
        static Double MinWidth = 50;

        static Random RandomNumber = new Random();

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new shape randomly, bound by the screen size.
        /// </summary>
        /// <returns>A new shape</returns>
        public static IShape CreateShape()
        {
            return CreateShape((ShapeType)RandomNumber.Next(0, ShapeCount));
        }

        /// <summary>
        /// Creates a new shape of the specified type, bound by the screen size.
        /// </summary>
        /// <param name="shapeType">The type of shape to create</param>
        /// <returns>A new shape</returns>
        public static IShape CreateShape(ShapeType shapeType)
        {
            return CreateShape(shapeType, (ShapeColor)RandomNumber.Next(0, ColorCount));
        }

        /// <summary>
        /// Creates a new shape of the specified type and color, bound by the screen size.
        /// </summary>
        /// <param name="shapeType">The type of shape to create</param>
        /// <param name="shapeColor">The color of the shape</param>
        /// <returns>A new shape</returns>
        public static IShape CreateShape(ShapeType shapeType, ShapeColor shapeColor)
        {
            IShape shape = CreateShapeFromType(shapeType);

            shape.Height = RandomNumber.NextDouble() * (SystemParameters.VirtualScreenHeight / 2) + MinHeight;
            shape.Width = RandomNumber.NextDouble() * (SystemParameters.VirtualScreenWidth / 2) + MinWidth;
            shape.Top = RandomNumber.NextDouble() * (SystemParameters.VirtualScreenHeight - shape.Height);
            shape.Left = RandomNumber.NextDouble() * (SystemParameters.VirtualScreenWidth - shape.Width);
            shape.FillBrush = CreateBrushFromColor(shapeColor);

            return shape;
        }

        #endregion

        #region Private Methods

        private static IShape CreateShapeFromType(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Circle:
                    return new Circle();

                case ShapeType.Triangle:
                    return new Triangle();

                case ShapeType.Square:
                    return new Square();

                default:
                    return null;
            }
        }

        private static Brush CreateBrushFromColor(ShapeColor shapeColor)
        {
            switch (shapeColor)
            {
                case ShapeColor.Red:
                    return Brushes.Red;

                case ShapeColor.Green:
                    return Brushes.Green;

                case ShapeColor.Blue:
                    return Brushes.Blue;

                case ShapeColor.Yellow:
                    return Brushes.Yellow;

                case ShapeColor.Pink:
                    return Brushes.Pink;

                case ShapeColor.LightBlue:
                    return Brushes.LightBlue;

                case ShapeColor.LightGreen:
                    return Brushes.LightGreen;

                default:
                    return null;
            }
        }

        #endregion
    }
}
