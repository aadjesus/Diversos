using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class LogonAPI : Command
    {
	    public int AppType
        {
            get { return protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.ATP); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.ATP, value); }
	    }
	    public int AppNumber
        {
            get { return protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.ANB); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.ANB, value); }
	    }
        public String Device
        {
            get {return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.DEV).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.DEV, value); }
        }
        public String Password
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.PAS).ToString(); }
            set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.PAS, value); }
        }
        public String Version
        {
            get { return protocol.Get(DataProtocol.DATA_TYPE_SIMPLE.VER).ToString(); }
		    set { protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.VER, value); }
	    }
	    override public void FillProtocol() {
		    try
            {
                protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.MNM, System.Net.Dns.GetHostName());
		    }
            catch
            {
                protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.MNM, "Unknown");
		    }
	    }
        override public void SetPacketType()
        {
            protocol.Type = VoiceProtocol.PACKET_TYPE.LOGON;
        }
    }
}
