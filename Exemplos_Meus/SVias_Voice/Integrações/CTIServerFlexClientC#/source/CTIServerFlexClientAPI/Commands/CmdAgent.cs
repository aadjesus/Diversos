using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CmdAgent : Command
    {
        public String Device
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.DEV).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.DEV, value); }
        }
        public String AgentId
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.AID).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.AID, value); }
        }
        public String Password
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.PAS).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.PAS, value); }
        }
        public String Group
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.AGR).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.AGR, value); }
        }
        override public void FillProtocol()
        {
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.CTP, 18);
	    }
    }
}
