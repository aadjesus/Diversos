using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace PhysicsHost
{
    /// <summary>
    /// Creates several global values that
    /// are used throughout the Application
    /// </summary>
    public partial class App : Application
    {
        #region Data
        private const int MAX_CUSTOMERS=5;
        private const int MAX_ORDERS = 4;
        #endregion

        #region Ctor
        public App()
        {
            this.Properties.Add("MAX_CUSTOMERS", App.MAX_CUSTOMERS);
            this.Properties.Add("MAX_ORDERS", App.MAX_ORDERS);
        }
        #endregion
    }
}
