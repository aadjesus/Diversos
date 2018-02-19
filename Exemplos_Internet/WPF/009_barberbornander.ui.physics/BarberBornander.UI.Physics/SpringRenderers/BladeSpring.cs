using System;
using System.Windows.Media;
using System.Windows;

namespace BarberBornander.UI.Physics.SpringRenderers
{
    /// <summary>
    /// Creates a Blade Spring renderer
    /// Basically it a zig-zag line
    /// </summary>
    public class BladeSpring : ISpringRenderer
    {
        #region Public Properties
        public Pen Pen { get; set; }
        public double Width {get; set; }
        public uint Turns{ get; set; }
        #endregion

        #region Ctor
        public BladeSpring(Pen pen, double width, uint turns)
        {
            Pen = pen;
            Width = width;
            Turns = turns;
        }
        #endregion

        #region ISpringRenderer Members

        public void Render(Spring spring, System.Windows.Media.DrawingContext drawingContext)
        {
            Vector direction = spring.To.Position - spring.From.Position;
            Vector angleVector = new Vector(-direction.Y, direction.X);
            double distance = (direction.Length * 0.9) / Turns;
            double offsetDistance = direction.Length * 0.1;
            direction.Normalize();
            angleVector.Normalize();

            Vector previousPoint = spring.From.Position;
            for (uint i = 0; i < Turns; ++i)
            {
                Vector point = spring.From.Position + (direction * (offsetDistance + distance * i) + (angleVector * (Width * ((i & 1) == 0 ? 1.0 : -1.0))));
                drawingContext.DrawLine(Pen, previousPoint.ToPoint(), point.ToPoint());
                previousPoint = point;
            }
            drawingContext.DrawLine(Pen, previousPoint.ToPoint(), spring.To.Position.ToPoint());


        }

        #endregion
    }
}
