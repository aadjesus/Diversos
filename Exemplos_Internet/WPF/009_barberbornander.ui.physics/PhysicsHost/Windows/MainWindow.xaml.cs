using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using BarberBornander.UI.Physics;
using BarberBornander.UI.Physics.SpringRenderers;

using PhysicsHost.DataAccess;
using PhysicsHost.ViewModel;

namespace PhysicsHost
{
    /// <summary>
    /// Creates Particles within a 
    /// <see cref="BarberBornander.UI.PhysicsParticleCanvas">
    /// ParticleCanvas</see>, and uses the 2 ModelViews
    /// <list type="bullet">
    /// <item>CustomerViewModel</item>
    /// <item>OrderViewModel</item>
    /// </list>
    /// To provide the models that are used to bind to
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Data
        Particle anchor;
        private CustomerViewModel customerViewModel = new CustomerViewModel();
        private OrderViewModel orderViewModel = new OrderViewModel();
        private Customer currentCustomer;
        private CustomerUserControl currentCustomerUserControl;
        private Particle currentParticleForCustomer;
        private Particle[] particlesLatched;
        private Spring[] anchorSpringsLatched;
        private Spring[] particleSpringsLatched;
        private Spring springLastToFirstLatched;
        private bool ordersCurrentlyShown = false;
        #endregion

        #region Ctor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Command Sinks

        /// <summary>
        /// Only allow the 
        /// <see cref="PhysicsHost.ViewModel.
        /// CustomerViewModel.ShowHideOrdersForCustomerCommand">
        /// ShowHideOrdersForCustomerCommand </see>command to
        /// execute if the current Customer has enough orders
        /// to show
        /// </summary>
        private void ShowHideOrdersForCustomerCommand_CanExecute(
            object sender, CanExecuteRoutedEventArgs e)
        {
            currentCustomerUserControl = 
                (e.OriginalSource as Button).Tag as CustomerUserControl;
            if (currentCustomerUserControl != null)
            {
                currentCustomer = 
                    currentCustomerUserControl.DataContext as Customer;
                e.CanExecute = 
                    customerViewModel.CustomerHasEnoughOrders(
                    currentCustomer.CustomerID);
            }
            else
                e.CanExecute = false;
        }

        /// <summary>
        /// Shows Orders for selected Customer
        /// </summary>
        private void ShowHideOrdersForCustomerCommand_Executed(
            object sender, ExecutedRoutedEventArgs e)
        {
            //hide shown Customer Orders
            RemoveOrdersFromContainer();
            //show Orders for Customer selected
            foreach (Particle particle in 
                particleCanvasSimulation.ParticleSystem.Particles)
            {
                if (particle.Control.Equals(currentCustomerUserControl))
                {
                    currentParticleForCustomer = particle;
                    break;
                }
            }
            //show orders for Customer
            InitialiseOrders(currentCustomer.CustomerID, 
                currentParticleForCustomer);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates the initial Customer particles
        /// within the contained 
        /// <see cref="BarberBornander.UI.PhysicsParticleCanvas">
        /// ParticleCanvas, and setups the ParticleCanvas
        /// </see>
        /// </summary>
        private void InitialiseSystem()
        {
            try
            {
                //create the Customers particles
                InitialiseCustomers();
                //initialise other Canvas objects
                particleCanvasSimulation.OwnerWindow = this;
                this.PreviewMouseDown += new MouseButtonEventHandler(
                    particleCanvasSimulation.ParticleCanvas_PreviewMouseDown);
                this.PreviewMouseMove += new MouseEventHandler(
                    particleCanvasSimulation.ParticleCanvas_PreviewMouseMove);
                this.PreviewMouseUp += new MouseButtonEventHandler(
                    particleCanvasSimulation.ParticleCanvas_PreviewMouseUp);
                particleCanvasSimulation.RenderParticleSystem = true;
                particleCanvasSimulation.StartSimulation();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Fetches Order items for a given Customer
        /// </summary>
        /// <param name="CustomerID">The CustomerID to fetch the
        /// orders for</param>
        /// <param name="customerAnchor">The related Customer
        /// Particle to attach the fetched orders to</param>
        private void InitialiseOrders(string CustomerID, Particle customerAnchor)
        {
            try
            {
                Order[] orders = orderViewModel.GetOrders(CustomerID).ToArray();
                Particle[] particles = new Particle[orders.Count()];
                int startPos = 100;

                //setup Usercontrol particles
                for (int i = 0; i < orders.Count(); i++)
                {
                    if (i == 0)
                    {
                        particles[i] = new Particle(1.0f, 
                            new Vector(startPos, startPos),true);
                    }
                    else
                    {
                        startPos += 100;
                        particles[i] = new Particle(1.0f, 
                            new Vector(startPos, startPos), true);
                    }
                    particles[i].Control = getOrderUserControl(orders[i]);
                    particleCanvasSimulation.ParticleSystem.Particles.Add(particles[i]);
                }

                //now generate the pyhsics for these new Particles
                GeneratePhysicsForParticles(particles, customerAnchor, true, 840.0f,300.0f,90.0f);

                ordersCurrentlyShown = true;
                txtRemoveOrders.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBoxHelper.ShowMessageBox(ex.Message, "An error occurred");
            }
        }

        /// <summary>
        /// Create the Customer Particles
        /// </summary>
        private void InitialiseCustomers()
        {
            try
            {
                Customer[] custs = customerViewModel.GetCustomers().ToArray();
                Particle[] particles = new Particle[custs.Count()];
                int startPos = 100;

                //setup Usercontrol particles
                for (int i = 0; i < custs.Count(); i++)
                {
                    if (i == 0)
                    {
                        particles[i] = new Particle(1.0f, 
                            new Vector(startPos, startPos), true);
                    }
                    else
                    {
                        startPos += 200;
                        particles[i] = new Particle(1.0f, 
                            new Vector(startPos, startPos), true);
                    }
                    particles[i].Control = getCustomerUserControl(custs[i]);
                    particleCanvasSimulation.ParticleSystem.Particles.Add(particles[i]);
                }

                //setup anchor
                anchor = new Particle(float.PositiveInfinity, 
                    new Vector((double)(
                        this.particleCanvasSimulation.ActualWidth / 2), 40),true);
                anchor.Control = getAnchorButton();
                particleCanvasSimulation.ParticleSystem.Particles.Add(anchor);
                //now generate the pyhsics for these new Particles
                GeneratePhysicsForParticles(particles, 
                    anchor, false, 840.0f, 260.0f, 60.0f);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBoxHelper.ShowMessageBox(ex.Message, "An error occurred");
            }
        }

        /// <summary>
        /// Creates and adds new Particles to the contained
        /// <see cref="BarberBornander.UI.PhysicsParticleCanvas">
        /// ParticleCanvas</see>
        /// </summary>
        /// <param name="particles">The array of particles</param>
        /// <param name="anchor">The anchor Particle</param>
        /// <param name="createNewobjects">True if we are to remember
        /// the objects that were created by calling this method</param>
        /// <param name="restFirstToEnd">rest fisrt to end Particles</param>
        /// <param name="restLengthToAnchor">rest length</param>
        /// <param name="restLengthToEachOther">length to each other</param>
        private void GeneratePhysicsForParticles(Particle[] particles, 
            Particle anchor, bool createNewobjects, float restFirstToEnd,
            float restLengthToAnchor, float restLengthToEachOther)
        {
            try
            {
                //springs
                Spring[] anchorSprings = new Spring[particles.Count()];
                Spring[] particleSprings = new Spring[particles.Count() - 1];
                Spring springLastToFirst;

                //pens
                Pen transparentPen = new Pen(Brushes.Transparent, 1.0);
                Pen whiteSmokeDashPen = new Pen(Brushes.Gainsboro, 2.0);
                whiteSmokeDashPen.DashStyle = DashStyles.Dash;
                Pen whiteSmokeSolidPen = new Pen(Brushes.Gainsboro, 4.0);

                //renderers
                RubberBand transparentBandRenderer = 
                    new RubberBand(transparentPen);
                RubberBand whiteSmokeDashBandRenderer = 
                    new RubberBand(whiteSmokeDashPen);
                RubberBand whiteSmokeSolidRenderer = 
                    new RubberBand(whiteSmokeSolidPen);

                //setup anchorSprings
                for (int i = 0; i < particles.Length; i++)
                {
                    //260.0f
                    anchorSprings[i] = new Spring(anchor, particles[i], 
                        restLengthToAnchor, 1.5f, 2.0f);
                    anchorSprings[i].Renderer = whiteSmokeSolidRenderer;
                    particleCanvasSimulation.ParticleSystem.Springs.Add(anchorSprings[i]);
                }

                //setup particleSprings
                for (int i = 0; i < particleSprings.Length; i++)
                {
                    //60.0f
                    particleSprings[i] = new Spring(particles[i], 
                        particles[i + 1], restLengthToEachOther, 3.5f, 2.0f);
                    particleSprings[i].Renderer = whiteSmokeDashBandRenderer;
                    particleCanvasSimulation.ParticleSystem.Springs.Add(particleSprings[i]);
                }

                //setup first to last very strong spring
                springLastToFirst = new Spring(particles[0],
                    particles[particles.Length - 1], restFirstToEnd, 3.5f, 2.0f);
                springLastToFirst.Renderer = transparentBandRenderer;
                particleCanvasSimulation.ParticleSystem.Springs.Add(springLastToFirst);

                if (createNewobjects)
                {
                    particlesLatched = particles;
                    anchorSpringsLatched = anchorSprings;
                    particleSpringsLatched = particleSprings;
                    springLastToFirstLatched = springLastToFirst;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBoxHelper.ShowMessageBox(ex.Message, "An error occurred");
            }
        }


        /// <summary>
        /// Calls InitialiseSystem()
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiseSystem();
        }

        /// <summary>
        /// Creates a special Button which will be
        /// used at the anchor for all other Particles
        /// within the 
        /// <see cref="BarberBornander.UI.PhysicsParticleCanvas">
        /// ParticleCanvas</see>
        /// </summary>
        /// <returns>The anchor Button</returns>
        private Button getAnchorButton()
        {
            BitmapImage bitmap = new BitmapImage(
                new Uri("../Images/anchor.png", UriKind.Relative));
            Image image = new Image();
            image.Source = bitmap;
            image.Width = 55;
            image.Height = 55;
            Button btn = new Button();
            btn.Content = image; 
            btn.Width = double.NaN;
            btn.Height = double.NaN;
            btn.SetValue(Canvas.ZIndexProperty, -1);
            ControlTemplate anchorButtonTemplate = 
                this.TryFindResource("anchorButtonTemplate") as ControlTemplate;
            if (anchorButtonTemplate != null)
                btn.Template = anchorButtonTemplate;
            particleCanvasSimulation.Children.Add(btn);
            btn.PreviewMouseUp += new MouseButtonEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseUp);
            btn.PreviewMouseMove += new MouseEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseMove);
            btn.PreviewMouseDown += new MouseButtonEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseDown);
            return btn;
        }


        /// <summary>
        /// Creates a <see cref="CustomerUserControl">
        /// CustomerUserControl</see> for a given
        /// <see cref="Customer">Customer</see>
        /// </summary>
        /// <param name="order">The Customer to use as the
        /// DataContext for the generated 
        /// <see cref="CustomerUserControl">
        /// CustomerUserControl</see></param>
        /// <returns>An <see cref="CustomerUserControl">
        /// CustomerUserControl</see> that contains all 
        /// the bindings for a given 
        /// <see cref="Customer">Customer</see></returns>
        private CustomerUserControl getCustomerUserControl(Customer cust)
        {
            CustomerUserControl control = new CustomerUserControl();
            control.DataContext = cust;
            particleCanvasSimulation.Children.Add(control);
            control.PreviewMouseUp += new MouseButtonEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseUp);
            control.PreviewMouseMove += new MouseEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseMove);
            control.PreviewMouseDown += new MouseButtonEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseDown);
            control.MouseEnter += new MouseEventHandler(
                particleCanvasSimulation.ParticleCanvas_MouseEnter);
            Style defaultCustomerControlStyle = 
                this.TryFindResource("defaultCustomerControlStyle") as Style;
            if (defaultCustomerControlStyle != null)
                control.Style = defaultCustomerControlStyle;
            return control;
        }

        /// <summary>
        /// Creates a <see cref="OrderUserControl">
        /// OrderUserControl</see> for a given
        /// <see cref="Order">Order</see>
        /// </summary>
        /// <param name="order">The Order to use as the
        /// DataContext for the generated 
        /// <see cref="OrderUserControl">
        /// OrderUserControl</see></param>
        /// <returns>An <see cref="OrderUserControl">
        /// OrderUserControl</see> that contains all 
        /// the bindings for a given 
        /// <see cref="Order">Order</see></returns>
        private OrderUserControl getOrderUserControl(Order order)
        {
            OrderUserControl control = new OrderUserControl();
            control.DataContext = order;
            particleCanvasSimulation.Children.Add(control);
            control.PreviewMouseUp += new MouseButtonEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseUp);
            control.PreviewMouseMove += new MouseEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseMove);
            control.PreviewMouseDown += new MouseButtonEventHandler(
                particleCanvasSimulation.ParticleCanvas_PreviewMouseDown);
            control.MouseEnter += new MouseEventHandler(
                particleCanvasSimulation.ParticleCanvas_MouseEnter);
            Style defaultOrderControlStyle = 
                this.TryFindResource("defaultOrderControlStyle") as Style;
            if (defaultOrderControlStyle != null)
                control.Style = defaultOrderControlStyle;   
            return control;
        }

        /// <summary>
        /// On Window SizeChanged, update the conatined Particle positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Rect bounds = new Rect(this.particleCanvasSimulation.RenderSize);

            if(anchor != null)
                anchor.SetPosition(new Vector((double)
                    (particleCanvasSimulation.ActualWidth / 2), 40), bounds);
        }

        /// <summary>
        /// Removes all the shown Order items from the
        /// <see cref="BarberBornander.UI.PhysicsParticleCanvas">
        /// ParticleCanvas</see>
        /// </summary>
        private void txtRemoveOrders_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoveOrdersFromContainer();
            ordersCurrentlyShown = false;
            txtRemoveOrders.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Removes all the shown Order items from the
        /// <see cref="BarberBornander.UI.PhysicsParticleCanvas">
        /// ParticleCanvas</see>
        /// </summary>
        private void RemoveOrdersFromContainer()
        {
            if (ordersCurrentlyShown)
            {
                foreach (Particle particle in particlesLatched)
                {
                    this.particleCanvasSimulation.Children.Remove(particle.Control);
                    this.particleCanvasSimulation.ParticleSystem.Particles.Remove(particle);
                }
                foreach (Spring spring in anchorSpringsLatched)
                    this.particleCanvasSimulation.ParticleSystem.Springs.Remove(spring);
                foreach (Spring spring in particleSpringsLatched)
                    this.particleCanvasSimulation.ParticleSystem.Springs.Remove(spring);

                this.particleCanvasSimulation.ParticleSystem.Springs.Remove(
                    springLastToFirstLatched);
            }
        }

        /// <summary>
        /// Shows the about window
        /// </summary>
        private void DashedOutlineCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Owner = this;
            about.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            about.ShowInTaskbar = false;
            about.ShowDialog();
        }

        /// <summary>
        /// Shows the ContextMenu for this Window, from where the
        /// user may re-position the Anchor back to its original
        /// start position
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            anchor.Position = new Vector((double)
                (this.particleCanvasSimulation.ActualWidth / 2), 40);
        }
        #endregion
    }
}
