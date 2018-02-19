using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace PhysicsHost.DataAccess
{
    /// <summary>
    /// Customer Business Layer
    /// </summary>
    public class CustomerBAL
    {
        #region Ctor
        public CustomerBAL()
        {

        }
        #endregion

        #region Public Methods

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            int maxToShow =int.Parse(App.Current.Properties["MAX_CUSTOMERS"].ToString());

            NorthwindDataContext db = NorthwindDataContext.Instance;

            var customers = (from c in db.Customers where c.Orders.Count > 0 select c);
            int maxExistingCustomerWithOrders = customers.Count();
            int numOfCustomerToSelect = maxExistingCustomerWithOrders > maxToShow ?
                                        maxToShow : maxExistingCustomerWithOrders;

            return customers.Take(numOfCustomerToSelect).OrderBy(c => c.CustomerID);
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CustomerHasEnoughOrders(string CustomerID)
        {
            int maxToShow =int.Parse(App.Current.Properties["MAX_ORDERS"].ToString());

            NorthwindDataContext db = NorthwindDataContext.Instance;
            return (from o in db.Orders 
                    where o.CustomerID ==  
                    CustomerID select o).Count() > maxToShow;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool SubmitChanges() 
        {
            //As LINQ to SQL is dead clever we can simply call 
            //db.SubmitChanges, and as its been tracking changes automatically
            //this is all we need to do
            NorthwindDataContext db = NorthwindDataContext.Instance;
            db.SubmitChanges(ConflictMode.FailOnFirstConflict);
            return true;
        }
        #endregion
    }
}
