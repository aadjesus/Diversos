using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CmdSetInformation : Command
    {
        public String InternalCallId
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.ICA).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.ICA, value); }
        }
        public String PropertyName
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.PRN).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.PRN, value); }
        }
        public String PropertyValue
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.PRV).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.PRV, value); }
        }
        override public void FillProtocol()
        {
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.TBN, "Informations");
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.CTP, 70);
        }
    }
}
