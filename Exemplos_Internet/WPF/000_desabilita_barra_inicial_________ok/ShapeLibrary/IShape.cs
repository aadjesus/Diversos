using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeShow.Library
{
    public interface IShape
    {
        /// <summary>
        /// A collection of points representing the shape
        /// </summary>
        PointCollection Points { get; }

        /// <summary>
        /// The height of the shape
        /// </summary>
        Double Height { get; set; }

        /// <summary>
        /// The width of the shape
        /// </summary>
        Double Width { get; set; }

        /// <summary>
        /// The topmost y-coordinate of the shape
        /// </summary>
        Double Top { get; set; }

        /// <summary>
        /// The leftmost x-coordinate of the shape
        /// </summary>
        Double Left { get; set; }

        /// <summary>
        /// The color of the inside of the shape
        /// </summary>
        Brush FillBrush { get; set; }

        /// <summary>
        /// The width of the shape stroke
        /// </summary>
        Double OutlineWidth { get; set; }

        /// <summary>
        /// The color of the shape stroke
        /// </summary>
        Brush OutlineBrush { get; set; }

        /// <summary>
        /// The UI element of the shape
        /// </summary>
        UIElement UIElement { get; }

    }
}
