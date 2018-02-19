using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Input;

namespace BarberBornander.UI.Physics
{
    public delegate void ParticleReleasedEventHandler(object sender, ParticleEventArgs e);

    /// <summary>
    /// CustomEventArgs : a custom event argument class
    /// which simply holds an int value representing 
    /// how many times the associated event has been fired
    /// </summary>
    public class ParticleEventArgs : RoutedEventArgs
    {
        #region Instance Fields
        public Particle Particle { get; private set; }
        #endregion

        #region Ctor
        /// <summary>
        /// Constructs a new ParticleEventArgs object
        /// using the parameters provided
        /// </summary>
        /// <param name="Particle">the value for the events args</param>
        public ParticleEventArgs(RoutedEvent routedEvent, Particle particle) : base(routedEvent)
        {
            this.Particle = particle;
        }
        #endregion
    }

    /// <summary>
    /// The Physics container where all the <see cref="Particle">Particles</see>
    /// and <see cref="Spring">Springs</see> are connected and them movements
    /// simulated, using the contained <see cref="ParticleSystem">ParticleSystem</see>
    /// </summary>
    public class ParticleCanvas : DashedOutlineCanvas
    {
        #region  Instance Fields
        private DispatcherTimer worldTimer = new DispatcherTimer();
        private Window ownerWindow;
        private Point ownerWindowPosition;
        private Point previousAbsoluteMousePosition;
        private Particle selectedParticle = null;
        private double selectedParticleMass = 0.0f;
        private int zIndex = 0;
        public static bool Has2ndLevelExpanded = false;
        #endregion

        #region Ctor
        public ParticleCanvas ()
	    {
            worldTimer.Interval = new TimeSpan(50);
            worldTimer.Tick += new EventHandler(HandleWorldTimerTick);
            ParticleSystem = new ParticleSystem();
        }

        #endregion

        #region Layout
        /// <summary>
        /// Any custom Panel must override ArrangeOverride and MeasureOverride
        /// </summary>

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement element in base.InternalChildren)
            {
                double x;
                double y;
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                x = double.IsNaN(left) ? 0 : left;
                y = double.IsNaN(top) ? 0 : top;

                element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
            }
            return arrangeSize;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(size);
            }
            return new Size();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            ParticleSystem.Render(dc);
        }
        #endregion

        #region Routed Events

        //The actual event routing
        public static readonly RoutedEvent ParticleReleasedEvent =
            EventManager.RegisterRoutedEvent(
            "ParticleReleased", RoutingStrategy.Bubble,
            typeof(ParticleReleasedEventHandler),
            typeof(ParticleCanvas));

        //add remove handlers
        public event ParticleReleasedEventHandler ParticleReleased
        {
            add { AddHandler(ParticleReleasedEvent, value); }
            remove { RemoveHandler(ParticleReleasedEvent, value); }
        }
        #endregion 

        #region Public properties
        public ParticleSystem ParticleSystem { get; set; }
        public bool RenderParticleSystem { get; set; }

        /// <summary>
        /// The owner form must be set to get the particles to
        /// be affected by form movement.
        /// </summary>
        public Window OwnerWindow
        {
            get { return ownerWindow; }
            set
            {
                if (value != null)
                {
                    ownerWindow = value;
                    ownerWindowPosition = new Point(0, 0);
                    ownerWindow.LocationChanged += new EventHandler(HandleOwnerWindowMove);
                }
            }
        }

        /// <summary>
        /// If a particle is being dragged this event stops that and fires a 
        /// ParticleReleasedEvent to signal this.
        /// </summary>
        public void ParticleCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                if (selectedParticle != null)
                {
                    selectedParticle.Mass = selectedParticleMass;
                    FireParticleReleasedEvent(selectedParticle);
                    selectedParticle = null;
                }
            }
        }

        /// <summary>
        /// This updates a Particles position when a Control is being dragged.
        /// Note that it is not required to move the Control as this is being 
        /// fixed in HandleWorldTimerTick when Controls snap to Particles
        /// positions.
        /// </summary>
        public void ParticleCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (selectedParticle != null)
                {
                    Point absolutePosition = Mouse.GetPosition(this);
                    Rect constaintsRectancle = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
                    selectedParticle.SetPosition(
                        new Vector(
                            selectedParticle.Position.X +
                            (absolutePosition.X - previousAbsoluteMousePosition.X),
                            selectedParticle.Position.Y +
                            (absolutePosition.Y - previousAbsoluteMousePosition.Y)),
                            constaintsRectancle
                            );

                    previousAbsoluteMousePosition = absolutePosition;
                }
            }
        }

        /// <summary>
        /// When the mouse is clicked on a control in the Simulation panel all
        /// Particles are searched to find the Particle that has the Control
        /// as associated control. Then that Particle is made immovable by setting
        /// its mass to infinity and are thus no longer effected by the simulation.
        /// Search for control has to go up through the tree as only the top level
        /// control is associated with the Particle if a particles associated 
        /// Control is a Panel containing more controls.
        /// </summary>
        public void ParticleCanvas_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && ownerWindow != null)
            {
                previousAbsoluteMousePosition = Mouse.GetPosition(this);

                Vector mousePosition = previousAbsoluteMousePosition.ToVector();

                var particleWhere = from particle in ParticleSystem.Particles
                                    where particle.Control == sender
                                    select particle;

                if (particleWhere.Count() > 0)
                {
                    Particle particle = particleWhere.First();
                    if (selectedParticle != null)
                        selectedParticle.Mass = selectedParticleMass;
                    selectedParticleMass = particle.Mass;
                    selectedParticle = particle;
                    selectedParticle.Mass = Single.PositiveInfinity;
                    selectedParticle.Velocity = new Vector();
                    selectedParticle.Control.SetValue(Canvas.ZIndexProperty, zIndex++);
                    return;
                }
            }
        }

        /// <summary>
        /// Sets the Zorder of the sender control to be highest element in Container
        /// </summary>
        public void ParticleCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Control).SetValue(Canvas.ZIndexProperty, zIndex++);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method gets called whenever the parent form is moved and 
        /// moves the particles accordingly.
        /// </summary>
        public void HandleOwnerWindowMove(object sender, EventArgs e)
        {
            Vector deltaMovement = new Vector(ownerWindowPosition.X - ownerWindow.Left, ownerWindowPosition.Y - ownerWindow.Top);
            ownerWindowPosition = new Point(ownerWindow.Left, ownerWindow.Top);
            foreach (Particle particle in ParticleSystem.Particles)
            {
                particle.MovePosition(deltaMovement);
            }
        }

        public void StartSimulation()
        {
            worldTimer.IsEnabled = true;
        }

        public void StopSimulation()
        {
            worldTimer.IsEnabled = false;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// This method is hooked to a timer event and is responsible for calling
        /// the methods that updates the world state.
        /// </summary>
        private void HandleWorldTimerTick(object sender, EventArgs e)
        {
            Rect constaintsRectancle = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
            lock (ParticleSystem.Particles)
            {
                // TODO: Make sure the actual timestep is configurable, 
                // or better yet, is the actual time elapsed between updates.
                ParticleSystem.DoEulerStep(0.005f, constaintsRectancle);
                foreach (Particle particle in ParticleSystem.Particles)
                {
                    // Make sure the Controls are located center on their particles.
                    particle.SnapControl();
                }
            }
            this.InvalidateVisual();
        }

        private void FireParticleReleasedEvent(Particle particle)
        {
            ParticleEventArgs args = new ParticleEventArgs(ParticleReleasedEvent, particle);
            RaiseEvent(args);  
        }

        #endregion
    }
}