using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    abstract public class Executable
    {
        protected VoiceProtocol protocol = null;
        abstract public void FillProtocol();
        abstract public void SetPacketType();
        public Executable()
        {
            protocol = new VoiceProtocol();
            protocol.Replace(DataProtocol.DATA_TYPE_SIMPLE.PID, CommandControl.GetUniquePacketId());
            SetPacketType();
            FillProtocol();
        }
        public int PacketId
        {
            get { return protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.PID); }
        }
        public int Type
        {
            get {
                try
                {
                    switch (protocol.Type)
                    {
                        case VoiceProtocol.PACKET_TYPE.COMMAND:
                            return protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.CTP);
                        case VoiceProtocol.PACKET_TYPE.EVENT:
                            return protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.ETP);
                    }
                }
                catch
                {
                }
                return 0;
            }
        }
        public VoiceProtocol Protocol
        {
            get { return protocol; }
        }
        public override string ToString()
        {
            return "Executable[" + protocol.ToString() + "]";
        }
    }
}
