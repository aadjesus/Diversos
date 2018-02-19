using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Diagnostics;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class TableChangedEventArgs : EventArgs
    {
        public TableChangedEventArgs(TableRecord record)
        {
            Record = record;
        }
        public TableRecord Record;
    }

    public delegate void TableChangedDelegate(object sender, TableChangedEventArgs e);

    public class Table : Hashtable
    {
        private LinkedList<ITableChangedListener> listeners = null;
        private TraceSwitch trace = null;
        public event TableChangedDelegate AddedRecord;
        public event TableChangedDelegate UpdatedRecord;
        public event TableChangedDelegate RemovedRecord;
        public Table()
        {
            TraceSwitch trace = new TraceSwitch("TraceLevel", "Nível do trace da aplicação");
            listeners = new LinkedList<ITableChangedListener>();
        }
        public void Release()
        {
            listeners.Clear();
        }
        public void AddListener(ITableChangedListener listener)
        {
            listeners.Remove(listener);
            listeners.AddLast(listener);
        }
        public void RemoveListener(ITableChangedListener listener)
        {
            listeners.Remove(listener);
        }
        public void NotifyAdded(TableRecord record)
        {
            foreach (ITableChangedListener listener in listeners)
            {
                try { listener.OnAdded(record); }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("Added notification error:{0}" + e.Message);
                }
            }
            if (AddedRecord != null)
                AddedRecord(this, new TableChangedEventArgs(record));
        }
        public void NotifyRemoved(TableRecord record)
        {
            foreach (ITableChangedListener listener in listeners)
            {
                try { listener.OnRemoved(record); }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("Removed notification error:{0}" + e.Message);
                }
            }
            if (RemovedRecord != null)
                RemovedRecord(this, new TableChangedEventArgs(record));
        }
        public void NotifyUpdated(TableRecord record)
        {
            foreach (ITableChangedListener listener in listeners)
            {
                try { listener.OnUpdated(record); }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("Updated notification error:{0}" + e.Message);
                }
            }
            if (UpdatedRecord != null)
                UpdatedRecord(this, new TableChangedEventArgs(record));
        }
        virtual public DataProtocol Data
        {
            set {
                foreach (Field tableField in value.Fields)
                {
                    if (tableField.Name == (int)DataProtocol.DATA_TYPE_STRUCTURED.REC)
                    {
                        DataProtocol tableRecord = (DataProtocol)tableField.Value;
                        String key = tableRecord.Get(DataProtocol.DATA_TYPE_SIMPLE.KEY).ToString();
                        TableRecord rec = (TableRecord)base[key];
                        if (rec == null)
                        {
                            rec = new TableRecord(this);
                            rec.Data = tableRecord;
                            Add(key, rec);
                            NotifyAdded(rec);
                        }
                        else
                        {
                            rec.Data = tableRecord;
                            NotifyUpdated(rec);
                        }
                    }
                }
            }
        }
    }
}
