using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using PhysicsHost.DataAccess;


namespace PhysicsHost.ViewModel
{
    /// <summary>
    /// Order View Model, used to bind to within the 
    /// <see cref="MainWindow">MainWindow</see>
    /// </summary>
    public class OrderViewModel
    {
        #region Data
        OrderBAL orderBAL = new OrderBAL();
        #endregion 

        #region Commands
        public static readonly RoutedCommand SubmitChangesCommand
            = new RoutedCommand("SubmitChangesCommand", typeof(OrderViewModel));
        #endregion

        #region Ctor
        public OrderViewModel()
        {
            CommandManager.RegisterClassCommandBinding(typeof(OrderViewModel),
                new CommandBinding(OrderViewModel.SubmitChangesCommand, null,
                    delegate(object sender, CanExecuteRoutedEventArgs e)
                    {
                        e.CanExecute = true;
                    }));
        }
        #endregion

        #region Public Methods
        public IEnumerable<Order> GetOrders(string CustomerID)
        {
            return orderBAL.GetOrders(CustomerID);
        }

        public bool SubmitChanges()
        {
            return orderBAL.SubmitChanges();
        }
        #endregion
    }
}
