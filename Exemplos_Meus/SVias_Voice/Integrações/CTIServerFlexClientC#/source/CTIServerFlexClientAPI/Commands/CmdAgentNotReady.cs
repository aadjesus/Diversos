using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CmdAgentNotReady : CmdAgent
    {
        public int ReasonCode
        {
            get { return protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.ASR); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.ASR, value); }
        }
        override public void FillProtocol()
        {
            base.FillProtocol();
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.AST, 2);
	    }
    }
}
