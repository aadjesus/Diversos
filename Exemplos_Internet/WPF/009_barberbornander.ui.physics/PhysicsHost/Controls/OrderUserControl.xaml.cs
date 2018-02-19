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

using PhysicsHost.DataAccess;

namespace PhysicsHost
{

    /// <summary>
    /// Represents a Order from the Northwind database.
    /// This is lookless control, and as such as Style
    /// can be applied, but there are expected to be 1
    /// PART called
    /// <list type="bullet">
    /// <item>PART_Edit, Button</item>
    /// </list>
    /// Which are required for the control to work correctly
    /// </summary>
    [TemplatePart(Name = "PART_Edit", Type = typeof(Button))] 
    public partial class OrderUserControl : UserControl
    {
        #region Ctor
        public OrderUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region OnApplyTemplate
        /// <summary>
        /// Find the required parts from the applied template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //edit button
            Button PART_EditButton = base.GetTemplateChild("PART_Edit") as Button;
            if (PART_EditButton != null)
            {
                PART_EditButton.Tag = this;
                PART_EditButton.Click += new RoutedEventHandler(PART_EditButton_Click);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Show EditOrderWindow
        /// </summary>
        private void PART_EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditOrderWindow eo = new EditOrderWindow();
            eo.DataContext = this.DataContext;
            eo.Owner = MainWindow.GetWindow(this);
            eo.ShowInTaskbar = false;
            eo.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            eo.ShowDialog();
        }
        #endregion
    }
}
