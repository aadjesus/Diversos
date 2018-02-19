using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Commands
{
    public class CommandControl
    {
	    private LinkedList<Command> commands = null;
        private LinkedList<ICommandListener> listeners = null;
        private bool running = false;
        public event CommandResponseDelegate CommandResponse;
        private static int uniquePacketId = 1;
	    public static int GetUniquePacketId() {
		    uniquePacketId++;
		    return uniquePacketId;
	    }
	    public CommandControl() {
            this.listeners = new LinkedList<ICommandListener>();
            this.commands = new LinkedList<Command>();
		    this.running = true;
            Thread thr = new Thread(new ThreadStart(run));
            thr.Start();
	    }
	    private void OnResponse(Command arg) {
		    arg.OnResponse();
            foreach (ICommandListener cl in listeners)
            {
			    if (arg.IsTimeout()) {
				    cl.OnCommandTimeout(arg);
			    } else {
				    cl.OnCommandResponse(arg);
			    }
		    }
            if (CommandResponse != null)
                CommandResponse(arg);
        }
	    public void AddListener(ICommandListener commandListener) {
		    listeners.Remove(commandListener);
		    listeners.AddLast(commandListener);
	    }
	    public void RemoveListener(ICommandListener commandListener) {
		    listeners.Remove(commandListener);
	    }
	    public void run() {
		    while (running) {
			    try {
				    Thread.Sleep(1000);
			    } catch (Exception e) {
                    System.Console.WriteLine(e.Message);
			    }
                lock(this)
                {
                    Command[] cmds = new Command[commands.Count];
                    commands.CopyTo(cmds, 0);
                    foreach (Command cmd in cmds)
                    {
                        if (cmd.IsTimeout())
                        {
                            cmd.Response.Response = 5019;
                            OnResponse(cmd);
                            commands.Remove(cmd);
                        }
                    }
                }
		    }
	    }
        public void Release()
        {
            listeners.Clear();
            running = false;
        }
        private Command FindCommand(VoiceProtocol protocol)
        {
            try
            {
                int pid = protocol.GetInt(DataProtocol.DATA_TYPE_SIMPLE.PID);
                foreach (Command cmd in commands)
                {
                    if (cmd.PacketId == pid)
                        return cmd;
                }
            }
            catch
            {
                TraceSwitch trace = new TraceSwitch("TraceLevel", "Nível do trace da aplicação");
                if (trace.TraceError)
                    Trace.TraceError("Unknown packet ID in message");
            }
            return null;
        }
	    public void ProcessResponseCommand(VoiceProtocol protocol)
        {
		    Command cmd = FindCommand(protocol);
		    if (cmd != null) {
			    cmd.ResponseProtocol = protocol;
			    OnResponse(cmd);
			    commands.Remove(cmd);
		    } else {
                TraceSwitch trace = new TraceSwitch("TraceLevel", "Nível do trace da aplicação");
                if (trace.TraceWarning)
                    Trace.TraceError("ResponseCommand process: Command not found. Response={0}", protocol.ToString());
		    }
	    }
	    public void ProcessCommand(Command arg) {
		    commands.AddLast(arg);
            arg.ClearTimeout();
	    }
    }
}
