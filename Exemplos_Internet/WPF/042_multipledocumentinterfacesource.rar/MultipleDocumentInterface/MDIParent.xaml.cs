using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultipleDocumentInterface
{
    /// <summary>
    /// Interaction logic for MDIParent.xaml
    /// </summary>
    public partial class MDIParent : UserControl
    {
        /// <summary>
        /// Gets or sets the previous mouse position.
        /// </summary>
        /// <value>The previous mouse position.</value>
        private Point previousMousePosition { get; set; }

        /// <summary>
        /// Gets or sets the focused MDI child.
        /// </summary>
        /// <value>The focused MDI child.</value>
        public MDIChild FocusedChild { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDI"/> class.
        /// </summary>
        public MDIParent()
        {
            InitializeComponent();

            Width = double.NaN;
            Height = double.NaN;

            MouseMove += new MouseEventHandler(TopBar_MouseMove);
            MouseLeftButtonUp += new MouseButtonEventHandler(TopBar_MouseLeftButtonUp);
        }

        /// <summary>
        /// Adds the specified MDI  child.
        /// </summary>
        /// <param name="mdiChild">The MDI child.</param>
        public void AddChild(MDIChild mdiChild)
        {
            mdiChild.HorizontalAlignment = HorizontalAlignment.Left;
            mdiChild.VerticalAlignment = VerticalAlignment.Top;

            mdiChild.Margin = new Thickness((ChildGrid.Children.Count + 1) * 10, (ChildGrid.Children.Count + 1) * 10, 0.0, 0.0);

            mdiChild.MDIParent = this;

            mdiChild.MouseLeftButtonDown += new MouseButtonEventHandler(delegate
            {
                BringToFront(mdiChild);
            });

            mdiChild.TopBar.MouseLeftButtonDown += new MouseButtonEventHandler(delegate
            {
                previousMousePosition = Mouse.GetPosition(this);

                BringToFront(FocusedChild = mdiChild); 
            });

            mdiChild.TopBar.MouseLeftButtonUp += new MouseButtonEventHandler(TopBar_MouseLeftButtonUp);
            mdiChild.TopBar.MouseMove += new MouseEventHandler(TopBar_MouseMove);

            ChildGrid.Children.Add(mdiChild);
        }

        /// <summary>
        /// Removes the specified MDI child.
        /// </summary>
        /// <param name="mdiChild">The MDI child.</param>
        public void RemoveChild(MDIChild mdiChild)
        {
            ChildGrid.Children.Remove(mdiChild);
        }

        /// <summary>
        /// Brings the specified MDI child window to the front.
        /// </summary>
        /// <param name="mdiChild">The MDI child.</param>
        public void BringToFront(MDIChild mdiChild)
        {
            if (mdiChild.InFront)
                return;

            ChildGrid.Children.Remove(mdiChild);

            for (int i = 0; i < ChildGrid.Children.Count; i++)
                ((MDIChild)ChildGrid.Children[i]).InFront = false;

            mdiChild.InFront = true;

            ChildGrid.Children.Add(mdiChild);
        }

        #region MDI Child Events
        
        /// <summary>
        /// Handles the MouseLeftButtonUp event of the TopBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void TopBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FocusedChild = null;
        }

        /// <summary>
        /// Handles the MouseMove event of the TopBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (FocusedChild == null)
                return;

            Point mousePosition = Mouse.GetPosition(this);
            Point mouseDifference = new Point(mousePosition.X - previousMousePosition.X, mousePosition.Y - previousMousePosition.Y);

            previousMousePosition = mousePosition;

            double leftMargin = FocusedChild.Margin.Left + mouseDifference.X;
            double topMargin = FocusedChild.Margin.Top + mouseDifference.Y;

            FocusedChild.Margin = new Thickness((leftMargin > 0.0) ? leftMargin : 0.0, (topMargin > 0.0) ? topMargin : 0.0, 0.0, 0.0);
        }

        #endregion
    }
}