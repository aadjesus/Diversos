using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.VStream
{
    public interface IStreamListener
    {
        void Connected(IStream stream);
        void Disconnected(IStream stream);
        void Received(IStream stream, VoiceProtocol protocol);
    }
}
