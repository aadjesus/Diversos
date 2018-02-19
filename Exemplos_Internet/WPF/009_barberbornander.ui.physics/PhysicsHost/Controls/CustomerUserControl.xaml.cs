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
using PhysicsHost.ViewModel;

namespace PhysicsHost
{
    /// <summary>
    /// Represents a Customer from the Northwind database.
    /// This is lookless control, and as such as Style
    /// can be applied, but there are expected to be 2
    /// PARTs called
    /// <list type="bullet">
    /// <item>PART_ShowHideOrders, Button</item>
    /// <item>PART_Edit, Button</item>
    /// </list>
    /// Which are required for the control to work correctly
    /// </summary>
    [TemplatePart(Name = "PART_ShowHideOrders", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Edit", Type = typeof(Button))] 
    public partial class CustomerUserControl : UserControl
    {
        #region Ctor
        public CustomerUserControl()
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
            //show hide button
            Button PART_ShowHideOrders = base.GetTemplateChild("PART_ShowHideOrders") as Button;
            if (PART_ShowHideOrders != null)
            {
                PART_ShowHideOrders.Tag = this;
                PART_ShowHideOrders.Command = CustomerViewModel.ShowHideOrdersForCustomerCommand;
            }
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
        /// Show EditCustomerWindow
        /// </summary>
        private void PART_EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditCustomerWindow ec = new EditCustomerWindow();
            ec.DataContext = this.DataContext;
            ec.Owner = MainWindow.GetWindow(this);
            ec.ShowInTaskbar = false;
            ec.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ec.ShowDialog();
        }
        #endregion
    }
}
