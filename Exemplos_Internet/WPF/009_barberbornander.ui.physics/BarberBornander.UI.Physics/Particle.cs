using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BarberBornander.UI.Physics
{
    public delegate void ParticleMovedEventHandler(Particle particle);
   
    /// <summary>
    /// Represents a single Particle in the Physics simulation
    /// which can have a control which is owned by the Particle
    /// </summary>
    public class Particle
    {
        #region Instance Fields
        public event ParticleMovedEventHandler Move;

        private double mass;
        #endregion

        #region Ctor

        public Particle(double mass, Vector position, bool constrainedToCanvas)
        {
            this.Mass = mass;
            this.Position = position;
            this.Velocity = new Vector();
            this.Force = new Vector();
            this.ConstrainedToCanvas = constrainedToCanvas;
        }
        #endregion

        #region Public properties
        public double OneOverMass { get; private set; }
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Force { get; set; }
        public ParticleState State { get; set; }
        public Control Control { get; set; }
        public bool ConstrainedToCanvas { get; set; }

        public double Mass 
        {
            get { return mass; }
            set
            {
                if (value > 0)
                {
                    mass = value;
                    OneOverMass = 1.0 / mass;
                }
                else
                {
                    throw new ArgumentException("particle mass can not be zero or less", "value");
                }
            }
        }
        
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a force to the <code>Particle</code>s accumulated forces.
        /// </summary>
        public void AddForce(Vector newForce)
        {
            Force += newForce;
        }

        public void ResetForce()
        {
            Force = new Vector();
        }

        public void SnapControl()
        {
            // If a Control is associated with this Particle then snap 
            // the Controls location so that it centers around the Particle
            if (Control != null)
            {
                Control.SetValue(Canvas.LeftProperty, (double)Position.X - Control.ActualWidth / 2.0);
                Control.SetValue(Canvas.TopProperty, (double)Position.Y - Control.ActualHeight / 2.0);
                Control.Arrange(new Rect(Position.ToPoint(), Control.DesiredSize));
            }
        }

        /// <summary>
        /// This method is used to move movable Particles
        /// when the <code>Form</code> is moved.
        /// </summary>
        public void MovePosition(Vector delta)
        {
            if (!Double.IsInfinity(mass))
                Position += delta;
        }

        public void SetPosition(Vector position, Rect constraintsRectangle)
        {
            // If a Control is attached to this particle, snap the position so that it
            // can not be dragged outside the constraints (canvas).
            if (Control != null) 
            {
                position.X = Math.Min(constraintsRectangle.Right, Math.Max(constraintsRectangle.Left, position.X));
                position.Y = Math.Min(constraintsRectangle.Bottom, Math.Max(constraintsRectangle.Top, position.Y));
            }
            this.Position = position;
            FireMoveEvent();
        }
        #endregion

        #region Private Helpers
        private void FireMoveEvent()
        {
            if (Move != null)
                Move(this);
        }
        #endregion
    }
}
