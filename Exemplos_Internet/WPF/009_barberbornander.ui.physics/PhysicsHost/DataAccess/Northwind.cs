using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsHost.DataAccess
{

    /// <summary>
    /// This class is another part (partial) to the 
    /// NorthwindDataContext LINQ to SQL generated
    /// classes. This part provides a thread safe
    /// singleton of the NorthwindDataContext class
    /// </summary>
    partial class NorthwindDataContext
    {
        #region Data
        private static NorthwindDataContext instance = null;
        private static readonly object padlock = new object();
        #endregion

        #region Singleton Instance
        public static NorthwindDataContext Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NorthwindDataContext();
                    }
                    return instance;
                }
            }
        }
        #endregion
    }
}
