//-------------------------------------------
// PageTurner.cs (c) 2007 by Charles Petzold
//-------------------------------------------
using System;
using System.Windows;
using System.Windows.Media.Media3D;
using Petzold.Media3D;

namespace Petzold.BookViewer3D
{
    public class PageTurner : AlgorithmicTransform
    {
        // Radius dependency property and property.
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius",
                typeof(double),
                typeof(PageTurner),
                new PropertyMetadata(0.0));

        public double Radius
        {
            set { SetValue(RadiusProperty, value); }
            get { return (double)GetValue(RadiusProperty); }
        }

        // Angle dependency property and property.
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle",
                typeof(double),
                typeof(PageTurner),
                new PropertyMetadata(0.0));

        public double Angle
        {
            set { SetValue(AngleProperty, value); }
            get { return (double)GetValue(AngleProperty); }
        }

        // Random dependency properties and properties.
        public static readonly DependencyProperty Random1Property =
            DependencyProperty.Register("Random1",
                typeof(double),
                typeof(PageTurner),
                new PropertyMetadata(0.0), ValidateRandom);

        public double Random1
        {
            set { SetValue(Random1Property, value); }
            get { return (double)GetValue(Random1Property); }
        }

        public static readonly DependencyProperty Random2Property =
            DependencyProperty.Register("Random2",
                typeof(double),
                typeof(PageTurner),
                new PropertyMetadata(0.0), ValidateRandom);

        public double Random2
        {
            set { SetValue(Random2Property, value); }
            get { return (double)GetValue(Random2Property); }
        }

        static bool ValidateRandom(object obj)
        {
            double doub = (double)obj;
            return doub <= 1 && doub >= -1;
        }

        // Transform property required for AlgorithmicTransform.
        public override void Transform(Point3DCollection points)
        {
            if (Angle == 0)
                return;

            double factor = Radius * 90 / Angle;
            double cutoff = Math.PI * Radius / 2;
            double x = 0, y = 0, z = 0;
            double a = (Random1 + Random2) / 5.5 / 5.5;
            double b = (Random1 - Random2) / 5.5;

            for (int i = 0; i < points.Count; i++)
            {
                y = points[i].Y;
                double random = a * y * y + b * y;

                // This is the part of the page that curves.
                if (points[i].Z < cutoff)
                {
                    double radians = points[i].Z / factor;
                    x = factor * (1 - Math.Cos(radians));
                    z = factor * Math.Sin(radians);
                }
                // This is the part of the page that stays straight.
                else
                {
                    double radians = Math.PI * Angle / 180;
                    x = factor * (1 - Math.Cos(radians)) + 
                                    (points[i].Z - cutoff) * Math.Sin(radians);
                    z = factor * Math.Sin(radians) + 
                                    (points[i].Z - cutoff) * Math.Cos(radians);

                    x += random * points[i].Z * (90 - Math.Abs(Angle)) / 1000;
                }
                // Set the recalculated point.
                points[i] = new Point3D(x, y, z);
            }
        }

        // CreateInstanceCore required when deriving from Freezable.
        protected override Freezable CreateInstanceCore()
        {
            return new PageTurner();
        }
    }
}

