using System;
using System.Windows;
using System.Windows.Media;

namespace BarberBornander.UI.Physics
{
    /// <summary>
    /// A representation of a spring.
    /// </summary>
    public class Spring
    {
        #region Ctor
        public Spring(Particle from, Particle to, double restLength, double springConstant, double dampingConstant)
        {
            this.From = from;
            this.To = to;
            this.RestLength = restLength;
            this.SpringConstant = springConstant;
            this.DampingConstant = dampingConstant;
            this.SpringPen = new Pen(Brushes.White, 4.0f);
            this.Renderer = null;
        }
        #endregion

        #region Public properties
        public ISpringRenderer Renderer { get; set; }
        public Particle From { get; set; }
        public Particle To { get; set; }
        public double RestLength { get; set; }
        public double SpringConstant { get; set; }
        public double DampingConstant { get; set; }
        public Pen SpringPen { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method "applies" the springs forces by calculating the force
        /// and applying it to it's two <code>Particle</code>s.
        /// </summary>
        public void Apply()
        {
            Vector deltaX = From.Position - To.Position;
            Vector deltaV = From.Velocity - To.Velocity;

            double term1 = SpringConstant * (deltaX.Length - RestLength);
            double term2 = DampingConstant * Vector.AngleBetween(deltaV, deltaX) / deltaX.Length;

            double leftMultiplicant = -(term1 + term2);
            Vector force = deltaX;

            // FIXME: Should do something about zero-length springs here as the
            // simulation will brake on zero-length springs...
            force *= 1.0f / deltaX.Length;
            force *= leftMultiplicant;

            From.Force += force;
            To.Force -= force;
        }

        public void Render(System.Windows.Media.DrawingContext dc)
        {
            if (Renderer != null)
                Renderer.Render(this, dc);
        }
        #endregion
    }
}
