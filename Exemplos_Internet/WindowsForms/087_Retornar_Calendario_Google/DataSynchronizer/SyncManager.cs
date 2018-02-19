using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericEntities;

namespace DataSynchronizer
{
    public class SyncManager<T> where T:IEquatable<T>
    {
        public string Name { get; set; }
        public ISyncDataSource<T> Source1 { get; set; }

        public IEnumerable<T> Synchronize(DateTime? lastSyncTime)
        {
            try
            {
                IEnumerable<T> list = Source1.GetItemHeaders(lastSyncTime).ToList();
                return list;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
