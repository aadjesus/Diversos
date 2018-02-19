using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using PhysicsHost.DataAccess;


namespace PhysicsHost.ViewModel
{
    /// <summary>
    /// Custom View Model, used to bind to within the 
    /// <see cref="MainWindow">MainWindow</see>
    /// </summary>
    public class CustomerViewModel
    {
        #region Data
        CustomerBAL customerBAL = new CustomerBAL();
        #endregion

        #region Commands
        public static readonly RoutedCommand ShowHideOrdersForCustomerCommand
            = new RoutedCommand("ShowHideOrdersForCustomerCommand", typeof(CustomerViewModel));

        public static readonly RoutedCommand SubmitChangesCommand
            = new RoutedCommand("SubmitChangesCommand", typeof(CustomerViewModel));
        #endregion

        #region Ctor
        public CustomerViewModel()
        {
            CommandManager.RegisterClassCommandBinding(typeof(CustomerViewModel),
                new CommandBinding(CustomerViewModel.SubmitChangesCommand,null,
                    delegate(object sender, CanExecuteRoutedEventArgs e) {
                        e.CanExecute = true;
                    }));
        }
        #endregion

        #region Public Methods
        public IEnumerable<Customer> GetCustomers()
        {
            return customerBAL.GetCustomers();
        }

        public bool CustomerHasEnoughOrders(string CustomerID)
        {
            return customerBAL.CustomerHasEnoughOrders(CustomerID);
        }

        public bool SubmitChanges()
        {
            return customerBAL.SubmitChanges();
        }
        #endregion
    }
}
