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
    public class OrderBAL
    {
        #region Ctor
        public OrderBAL()
        {

        }
        #endregion

        #region Public Methods
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Order> GetOrders(string CustomerID)
        {
            int maxToShow = int.Parse(App.Current.Properties["MAX_ORDERS"].ToString());

            NorthwindDataContext db = NorthwindDataContext.Instance;

            var orders = (from o in db.Orders where o.CustomerID == CustomerID select o);
            int maxOrders = orders.Count();
            int numOfOrderToSelect = maxOrders > maxToShow ?
                                        maxToShow : maxOrders;


            return orders.Take(numOfOrderToSelect).OrderBy(o => o.OrderID);
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
