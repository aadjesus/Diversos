using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSynchronizer
{
    public class InmemorySyncHistoryRecorder : ISyncHistoryRecorder
    {
        private static IDictionary<string, DateTime> historyRecords;
        private static object syncObject = new object();

        public InmemorySyncHistoryRecorder()
        {
            historyRecords = new Dictionary<string, DateTime>();
        }

        #region ISyncHistoryRecorder Members

        public void WriteSyncHistoryRecord(string source1Id, string source2Id, DateTime syncTime)
        {
            lock (syncObject)
            {
                string key = GetKey(source1Id, source2Id);
                if (historyRecords.ContainsKey(key))
                    historyRecords[key] = syncTime;
                else
                    historyRecords.Add(key, syncTime);
            }
        }

        public DateTime? GetLastSyncTime(string source1Id, string source2Id)
        {
            lock (syncObject)
            {
                string key = GetKey(source1Id, source2Id);
                DateTime dateTime;
                if (historyRecords.TryGetValue(key, out dateTime))
                    return dateTime;
                else
                    return null;
            }
        }

        #endregion

        private string GetKey(string source1Id, string source2Id)
        {
            return source1Id + "-" + source2Id;
        }
    }
}
