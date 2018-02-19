using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class TableChangedControl : ITableChangedListener
    {
        private Hashtable tables = null;
        public TableChangedControl()
        {
            tables = new Hashtable();
            TableCalls calls = new TableCalls();
            calls.AddListener(this);
            tables.Add("Calls", calls);
            tables.Add("Agents", new TableAgents());
            tables.Add("Informations", new TableInformations());
        }
        public void OnAdded(TableRecord record)
        {
        }
        public void OnRemoved(TableRecord record)
        {
            AgentCall call = (AgentCall)record;
            TableCalls calls = (TableCalls)record.Table;
            if (!calls.HasMore(call.InternalCallId))
            {
                TableInformations infos = (TableInformations)GetTable("Informations");
                infos.RemoveAll(call.InternalCallId);
            }
        }
        public void OnUpdated(TableRecord record)
        {
        }
        public Table GetTable(String tableName)
        {
            return (Table)tables[tableName];
        }
        public void Process(VoiceProtocol protocol)
        {
            foreach (Field field in protocol.Fields)
            {
                if (field.Name == (int)DataProtocol.DATA_TYPE_STRUCTURED.TBL)
                {
                    DataProtocol vpTable = (DataProtocol)field.Value;
                    String tableName = vpTable.Get(DataProtocol.DATA_TYPE_SIMPLE.TBN).ToString();
                    int rowNumber = vpTable.GetInt(DataProtocol.DATA_TYPE_SIMPLE.TRN);
                    Table table = GetTable(tableName);
                    if (table == null)
                    {
                        table = new Table();
                        tables.Add(tableName, table);
                    }
                    table.Data = vpTable;
                }
            }
        }
        public void Release()
        {
            foreach (Table table in tables.Values)
            {
                table.Release();
            }
        }
    }
}
