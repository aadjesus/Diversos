using System;
using System.Collections.Generic;
using System.Text;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public delegate void CommandResponseDelegate(Command sender);

    abstract public class Command : Executable
    {
        protected CommandResponse response = null;
        private long timeout;
        private LinkedList<ICommandListener> listener;
        public event CommandResponseDelegate CommandResponse;
        public Command()
        {
            listener = new LinkedList<ICommandListener>();
            response = new CommandResponse();
            ClearTimeout();
        }
        public void AddListener(ICommandListener arg)
        {
            listener.Remove(arg);
            listener.AddLast(arg);
        }
        public void RemoveListener(ICommandListener arg)
        {
            listener.Remove(arg);
        }
        public void OnResponse()
        {
            foreach (ICommandListener cl in listener) 
            {
                if (IsTimeout())
                {
                    cl.OnCommandTimeout(this);
                }
                else
                {
                    cl.OnCommandResponse(this);
                }
            }
            if (CommandResponse != null)
                CommandResponse(this);
        }
        public void ClearTimeout()
        {
            ClearTimeout(5000);
        }
        public void ClearTimeout(long millis)
        {
            timeout = DateTime.Now.Ticks +(millis * 10000);
        }
        public bool IsTimeout()
        {
            if (response.ResponseProtocol != null)
                return false;
            return timeout < DateTime.Now.Ticks;
        }
        public CommandResponse Response
        {
            get { return response; }
        }
        public VoiceProtocol ResponseProtocol
        {
            set { response.ResponseProtocol = value; }
        }
        override public void SetPacketType()
        {
            protocol.Type = VoiceProtocol.PACKET_TYPE.COMMAND;
        }
        public override string ToString()
        {
            return "Command:[" + base.ToString() + "]";
        }
    }
}
