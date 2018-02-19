using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class TableInformations : Table
    {
        public void RemoveAll(String internalCallId)
        {
            // previne 'exception' na colecao
            LinkedList<Information> toRemove = new LinkedList<Information>();
            foreach (Information info in base.Values)
            {
                if (info.InternalCallId.Equals(internalCallId))
                {
                    toRemove.AddLast(info);
                }
            }
            foreach (Information info in toRemove)
            {
                Remove(info.Key);
                NotifyRemoved(info);
            }
            toRemove.Clear();
        }
        public String getValueByName(String InternalCallId, String name)
        {
            foreach (Information info in base.Values)
            {
                if (info.InternalCallId.Equals(InternalCallId))
                {
                    if (info.Name.ToUpper().Equals(name.ToUpper()))
                    {
                        return info.Value;
                    }
                }
            }
            return "";
        }
        override public DataProtocol Data
        {
            set
            {
                foreach (Field tableField in value.Fields)
                {
                    if (tableField.Name == (int)DataProtocol.DATA_TYPE_STRUCTURED.REC)
                    {
                        DataProtocol tableRecord = (DataProtocol)tableField.Value;
                        String key = tableRecord.Get(DataProtocol.DATA_TYPE_SIMPLE.KEY).ToString();
                        Information info = (Information)base[key];
                        if (info == null)
                        {
                            info = new Information(this);
                            info.Data = tableRecord;
                            Add(info.Key, info);
                            NotifyAdded(info);
                        }
                        else
                        {
                            info.Data = tableRecord;
                            NotifyUpdated(info);
                        }
                    }
                }
            }
        }
    }
}
