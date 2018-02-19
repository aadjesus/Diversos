using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CommandResponse
    {
        private int response = -1;
        protected VoiceProtocol responseProtocol = null;
        public VoiceProtocol ResponseProtocol
        {
            get { return responseProtocol; }
            set
            {
                responseProtocol = value;
                response = responseProtocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.RCD);
            }
        }
        public int Response
        {
            get { return response; }
            set { response = value;  }
        }
    }
}
