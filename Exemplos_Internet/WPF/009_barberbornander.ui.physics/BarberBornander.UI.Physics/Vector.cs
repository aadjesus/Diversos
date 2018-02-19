using System;
using System.Windows;


namespace BarberBornander.UI.Physics
{

    //TODO : Check with Fredrik, there is a System.Windows.Vector class that does this
    //with a few minor changes

    /// <summary>
    /// A representation of a 2D vector.
    /// </summary>
    public class Vector
    {
        #region Data
        public static readonly Vector Empty = new Vector();
        #endregion

        #region Ctor
        public Vector()
        {
            X = 0.0f;
            Y = 0.0f;
        }

        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector(Vector vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
        }
        #endregion

        #region Public properties

        public double Length
        {
            get { return (double)Math.Sqrt(X * X + Y * Y); }
        }

        public double X { get; set; }
        public double Y { get; set; }
        #endregion

        #region Public static methods/operators

        public void Normalize()
        {
            double length = Length;
            if (length > 0.0f)
            {
                X /= length;
                Y /= length;
            }
            else
            {
                X = 0.0f;
                Y = 0.0f;
            }
        }

        public static Vector Normalize(Vector vector)
        {
            double length = vector.Length;
            if (length > 0.0f)
                return new Vector(vector.X / length, vector.Y / length);
            else
                return Vector.Empty;
        }

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vector operator -(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vector operator *(Vector lhs, double rhs)
        {
            return new Vector(lhs.X * rhs, lhs.Y * rhs);
        }

        public static double Dot(Vector lhs, Vector rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        public static double DistanceBetween(Vector lhs, Vector rhs)
        {
            return (lhs - rhs).Length;
        }

        public static explicit operator Vector(System.Windows.Point point)
        {
            return new Vector(point.X, point.Y);
        }

        public static explicit operator System.Windows.Point(Vector vector)
        {
            return new System.Windows.Point((double)vector.X, (double)vector.Y);
        }

        #endregion
    }
}
