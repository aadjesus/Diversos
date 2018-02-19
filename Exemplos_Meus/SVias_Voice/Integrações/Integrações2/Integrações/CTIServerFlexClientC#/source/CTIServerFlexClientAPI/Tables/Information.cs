using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class Information : TableRecord
    {
        public Information(Table table) : base(table) { }
        public String Name
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.PRN).ToString(); }
        }
        public String Value
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.PRV).ToString(); }
        }
        public String InternalCallId
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.ICA).ToString(); }
	    }
        override public string ToString()
        {
            return Key + ":" + InternalCallId + ":" + Name + ":" + Value;
        }
    }
}
