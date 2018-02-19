using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class TableAgents : Table
    {
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
                        AgentState agent = (AgentState)base[key];
                        if (agent == null)
                        {
                            agent = new AgentState(this);
                            agent.Data = tableRecord;
                            if (!agent.IsDeleted())
                            {
                                Add(agent.Key, agent);
                                NotifyAdded(agent);
                            }
                            else
                            {
                                // A chamada não existia na lista e recebeu evento para excluir ??
                            }
                        }
                        else
                        {
                            agent.Data = tableRecord;
                            if (agent.IsDeleted())
                            {
                                Remove(agent.Key);
                                NotifyRemoved(agent);
                            }
                            else
                            {
                                NotifyUpdated(agent);
                            }
                        }
                    }
                }
            }
        }
    }
}
