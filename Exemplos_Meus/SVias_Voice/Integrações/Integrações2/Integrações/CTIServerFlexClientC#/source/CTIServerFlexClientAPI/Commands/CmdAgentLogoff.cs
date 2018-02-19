using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CmdAgentLogoff : CmdAgent
    {
        override public void FillProtocol()
        {
            base.FillProtocol();
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.AST, 1);
	    }
    }
}
