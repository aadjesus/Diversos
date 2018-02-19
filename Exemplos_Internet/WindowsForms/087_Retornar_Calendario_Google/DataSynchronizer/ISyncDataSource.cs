using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSynchronizer
{
    public interface ISyncDataSource<T> where T : IEquatable<T>
    {
        string Id { get; }
        IEnumerable<T> GetItemHeaders(DateTime? lastSyncTime);
        void LoadItemContents(IEnumerable<T> items);
        void WriteItems(IEnumerable<T> items);
    }
}
