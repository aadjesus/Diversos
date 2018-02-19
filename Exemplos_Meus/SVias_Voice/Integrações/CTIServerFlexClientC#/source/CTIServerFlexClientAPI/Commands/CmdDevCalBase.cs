using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    abstract public class CmdDevCalBase : Command
    {
        public String Device
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.DEV).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.DEV, value); }
        }
        public String CallId
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.CAL).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.CAL, value); }
        }
        abstract override public void FillProtocol();
    }
}
