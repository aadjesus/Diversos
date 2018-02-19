using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class TableRecord
    {
        private DataProtocol data;
        private Object userObject;
        protected Table table;
        public TableRecord(Table table)
        {
            this.table = table;
        }
        public Table Table
        {
            get { return table; }
        }
        public Object UserObject
        {
            get { return userObject; }
            set { userObject = value; }
        }
        public String Key
        {
            get { return data.Get(DataProtocol.DATA_TYPE_SIMPLE.KEY).ToString(); }
        }
        virtual public DataProtocol Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
