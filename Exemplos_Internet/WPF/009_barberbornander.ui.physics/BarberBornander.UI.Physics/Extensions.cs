using System;
using System.Windows;

namespace BarberBornander.UI.Physics
{
    /// <summary>
    /// Contains several Extension methods
    /// </summary>
    public static class Extensions
    {
        #region Extension Methods

        /// <summary>
        /// Point to Vector
        /// </summary>
        public static Vector ToVector(this Point point)
        {
            return new Vector(point.X, point.Y);
        }

        public static Point ToPoint(this Vector vector)
        {
            return new Point(vector.X, vector.Y);
        }
        #endregion
    }
}
