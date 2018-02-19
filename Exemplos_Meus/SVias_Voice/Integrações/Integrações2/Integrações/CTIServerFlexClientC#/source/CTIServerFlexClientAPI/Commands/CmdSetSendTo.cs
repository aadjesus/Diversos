using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CmdSetSendTo : Command
    {
        public String Device
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.DEV).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.DEV, value); }
        }
        public String DeviceTo
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.DTO).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.DTO, value); }
        }
        public String InternalCallId
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.ICA).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.ICA, value); }
        }
        override public void FillProtocol()
        {
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.TBN, "SendTo");
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.CTP, 70);
	    }
    }
}
