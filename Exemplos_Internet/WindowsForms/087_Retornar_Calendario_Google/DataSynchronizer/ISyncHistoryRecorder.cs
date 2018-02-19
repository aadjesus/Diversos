using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSynchronizer
{
    public interface ISyncHistoryRecorder
    {
        void WriteSyncHistoryRecord(string source1Id, string source2Id, DateTime syncTime);
        DateTime? GetLastSyncTime(string sourceId, string source2Id);
    }
}
