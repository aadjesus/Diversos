using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;


namespace BarberBornander.UI.Physics
{
    /// <summary>
    /// This class is responsible for all the Springs and Particles.
    /// </summary>
    public class ParticleSystem
    {
        #region Private members

        private double dragFactor = 0.75;
        private double wallFriction = 0.1;

        #endregion

        #region Ctor
        public ParticleSystem()
        {
            Particles = new List<Particle>();
            Springs = new List<Spring>();
            this.Gravity = new Vector(0.0, 20.0);
        }
        #endregion

        #region Public properties
        public Vector Gravity { get; set; }
        public List<Particle> Particles { get; private set; }
        public List<Spring> Springs { get; private set; }

        public double DragFactor 
        { 
            get { return dragFactor; } 
            set
            {
                dragFactor = Math.Min(1, Math.Max(0, value));
            }
        }

        /// <summary>
        /// Wall friction is the energy loss a particle that is constrained
        /// to the canvas suffers when hitting a wall. 
        /// </summary>
        public double WallFriction
        {
            get { return wallFriction; } 
            set
            {
                wallFriction = Math.Min(1, Math.Max(0, value));
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Calculates the derivative for the all entities in the simulation.
        /// </summary>
        public void CalculateDerivative()
        {
            foreach (Particle particle in Particles)
            {
                // Clear all existing forces acting on the particle
                particle.ResetForce();

                // Add a gravity force
                particle.AddForce(Gravity);

                // Add world drag
                Vector drag = particle.Velocity * -dragFactor;
                particle.AddForce(drag);
            }

            foreach (Spring spring in Springs)
            {
                // Apply what ever forces this spring holds
                spring.Apply();
            }

            foreach (Particle particle in Particles)
            {
                particle.State = new ParticleState(particle.Velocity, particle.Force * (particle.OneOverMass));
            }
        }

        /// <summary>
        /// This method is called once per "frame" and is responsible for 
        /// calculating the next state of the simulation. That is the 
        /// velocities and positions for all the particles.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void DoEulerStep(double deltaTime, Rect constaintsRectancle)
        {
            CalculateDerivative();

            foreach (Particle particle in Particles)
            {
                particle.State.Position *= deltaTime;
                particle.State.Velocity *= deltaTime;

                particle.Velocity = particle.Velocity + particle.State.Velocity;

                Vector newPosition = particle.Position + particle.State.Position;
                
                // If the particle is supposed to be constrained to the canvas "visible" area
                // do collision detection and figure out new position and velocity
                if (particle.ConstrainedToCanvas && !constaintsRectancle.Contains(newPosition.ToPoint()))
                {
                    double x = particle.Velocity.X;
                    double y = particle.Velocity.Y;

                    // If particle is moving left and is to the left of the left canvas boundry
                    // clamp position and reverse velocity and damp velocity by wall friction
                    // This is repeated for each of the four borders.
                    if (particle.Velocity.X < 0 && newPosition.X < constaintsRectancle.Left)
                    {
                        newPosition.X = constaintsRectancle.Left;
                        x *= -(1.0 - wallFriction);
                    }

                    if (particle.Velocity.X > 0 && newPosition.X > constaintsRectancle.Right)
                    {
                        newPosition.X = constaintsRectancle.Right;
                        x *= -(1.0 - wallFriction);
                    }

                    if (particle.Velocity.Y < 0 && newPosition.Y < constaintsRectancle.Top)
                    {
                        newPosition.Y = constaintsRectancle.Top;
                        y *= -(1.0 - wallFriction);
                    }

                    if (particle.Velocity.Y > 0 && newPosition.Y > constaintsRectancle.Bottom)
                    {
                        newPosition.Y = constaintsRectancle.Bottom;
                        y *= -(1.0 - wallFriction);
                    }
                    particle.Velocity = new Vector(x, y);
                }

                particle.Position = newPosition;
            }
        }

        public void Render(System.Windows.Media.DrawingContext dc)
        {
            lock(Springs)
            {
                foreach (Spring spring in Springs)
                {
                    spring.Render(dc);
                }
            }
        }
        #endregion
    }
}
