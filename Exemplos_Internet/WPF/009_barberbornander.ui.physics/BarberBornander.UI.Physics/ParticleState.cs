using System;
using System.Windows;

namespace BarberBornander.UI.Physics
{
    /// <summary>
    /// Data holder for the temporary result of the integration.
    /// </summary>
    public class ParticleState
    {
        #region Ctor
        /// <summary>
        /// Constructs a new Particle State using the input parameters
        /// provided
        /// </summary>
        public ParticleState(Vector position, Vector velocity)
        {
            this.Position = position;
            this.Velocity = velocity;
        }
        #endregion

        #region Public properties
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        #endregion
    }
}