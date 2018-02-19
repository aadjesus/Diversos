using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class AgentState : TableRecord
    {
        #region AgentStates
        /*
         * Logged On
         */
        public static int AST_LOGON = 0;
        /*
         * Logged Off
         */
        public static int AST_LOGOFF = 1;
        /*
         * Not Ready
         */
        public static int AST_NOTREADY = 2;
        /*
         * Ready
         */
        public static int AST_READY = 3;
        /*
         * Work Not Ready
         */
        public static int AST_WORKNOTREADY = 4;
        /*
         * Work Ready
         */
        public static int AST_WORKREADY = 5;
        #endregion

        public AgentState(Table table) : base(table) { }
	    public String Device
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.DEV).ToString(); }
	    }
	    public String AgentId
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.AID).ToString(); }
	    }
	    public String Password
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.PAS).ToString(); }
	    }
	    public String Group
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.AGR).ToString(); }
	    }
        public String Date
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.DTI).ToString(); }
        }
        public int State
        {
            get { return Data.GetInt(DataProtocol.DATA_TYPE_SIMPLE.AST); }
	    }
        public bool IsState(int state)
        {
            return State == state;
        }
	    public int Reason
        {
            get { return Data.GetInt(DataProtocol.DATA_TYPE_SIMPLE.ASR); }
	    }
        public bool IsActive()
        {
            return State == AgentState.AST_READY;
        }
        public bool IsDeleted()
        {
            return State == AgentState.AST_LOGOFF;
        }
        override public String ToString()
        {
            return Key + ":" + Device + ":" + State + ":" + Group + ":" + AgentId + ":" + Password + ":" + Reason + ":" + Date;
        }
    }
}
