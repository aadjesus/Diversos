using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CmdHold : CmdDevCalBase
    {
	    override public void FillProtocol() {
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.CTP, 12);
	    }
    }
}
