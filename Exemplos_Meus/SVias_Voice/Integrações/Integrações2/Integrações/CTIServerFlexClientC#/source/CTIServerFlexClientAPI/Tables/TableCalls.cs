using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class TableCalls : Table
    {
        public bool HasMore(String internalCallId)
        {
            foreach (AgentCall call in base.Values)
            {
                if (call.InternalCallId.Equals(internalCallId))
                {
                    return true;
                }
            }
            return false;
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
                        AgentCall call = (AgentCall)base[key];
                        if (call == null)
                        {
                            call = new AgentCall(this);
                            call.Data = tableRecord;
                            if (!call.IsDeleted())
                            {
                                Add(call.Key, call);
                                NotifyAdded(call);
                            }
                            else
                            {
                                // A chamada não existia na lista e recebeu evento para excluir ??
                            }
                        }
                        else
                        {
                            call.Data = tableRecord;
                            if (call.IsDeleted())
                            {
                                Remove(call.Key);
                                NotifyRemoved(call);
                            }
                            else
                            {
                                NotifyUpdated(call);
                            }
                        }
                    }
                }
            }
        }
    }
}
