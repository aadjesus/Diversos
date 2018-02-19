using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using CTIServerFlexClientAPI.VStream;
using CTIServerFlexClientAPI.Protocol;
using CTIServerFlexClientAPI.Commands;
using CTIServerFlexClientAPI.Tables;

namespace CTIServerFlexClientAPI
{
    public class CTIServerFlexClientAPI:IStreamListener,ICommandListener
    {
        public static String VERSION = "1.2.0";
        public static int APP_AGENT = 4;

        private StreamSocket stream = null;
        private CommandControl commandControl = null;
        private AppInformation appInformation = null;
        private TableChangedControl tableChanged = null;
	    private bool freeSocket = false;
        private TraceSwitch trace;
        public CTIServerFlexClientAPI()
        {
            Create(new StreamSocket());
        }
        private void SendLogon()
        {
            LogonAPI logon = new LogonAPI();
            logon.AppType = APP_AGENT;
            logon.AppNumber = appInformation.Number;
            logon.Device = appInformation.Device;
            logon.Password = appInformation.Password;
            logon.Version = appInformation.Version;
            logon.CommandResponse += new CommandResponseDelegate(Logon_CommandResponse);
            Execute(logon);
        }
        private void Logon_CommandResponse(Command sender)
        {
            if (sender.Response.Response != 0)
            {
                stream.CloseStream();
            }
        }
	    private void Create(StreamSocket stream) {
            trace = new TraceSwitch("TraceLevel", "Nível do trace da aplicação");
            appInformation = new AppInformation();
		    commandControl = new CommandControl();
            tableChanged = new TableChangedControl();
		    commandControl.AddListener(this);
		    freeSocket = (stream == null);
		    if (stream == null) {
			    stream = new StreamSocket();
		    }
		    this.stream = stream;
		    this.stream.AddListener(this);
	    }
        public StreamSocket Stream
        {
            get { return stream; }
        }
        public AppInformation AppInformation
        {
            get { return appInformation; }
        }
        public TableChangedControl TableChangedControl
        {
            get { return tableChanged; }
        }
        public void Connected(IStream stream)
        {
            if (trace.TraceInfo)
                Trace.TraceInformation("Connected to server");
            SendLogon();
        }
        public void Disconnected(IStream stream)
        {
            if (trace.TraceInfo)
                Trace.TraceInformation("Disconnected from server");
        }
        public void Received(IStream stream, VoiceProtocol protocol)
        {
            if (trace.TraceInfo)
                Trace.TraceInformation("Packet received:Type={0}", protocol.Type);
            if (trace.TraceVerbose)
                Trace.TraceInformation("Data:{0}", protocol);

            switch (protocol.Type)
            {
                case VoiceProtocol.PACKET_TYPE.LOGON:
                case VoiceProtocol.PACKET_TYPE.GETDATA:
                case VoiceProtocol.PACKET_TYPE.SETDATA:
                case VoiceProtocol.PACKET_TYPE.COMMAND:
                    break;
                case VoiceProtocol.PACKET_TYPE.EVENT:
                    tableChanged.Process(protocol);
                    break;
                case VoiceProtocol.PACKET_TYPE.RESPONSE_LOGON:
                case VoiceProtocol.PACKET_TYPE.RESPONSE_GETDATA:
                case VoiceProtocol.PACKET_TYPE.RESPONSE_SETDATA:
                case VoiceProtocol.PACKET_TYPE.RESPONSE_COMMAND:
                case VoiceProtocol.PACKET_TYPE.RESPONSE_EVENT:
                    commandControl.ProcessResponseCommand(protocol);
                    break;
                default:
                    if (trace.TraceError)
                        Trace.TraceError("Unknown packet type:Type={0}", protocol.Type);
                    break;
            }
        }
        public void Release() {
            if (trace.TraceInfo)
                Trace.TraceInformation("Released");
            tableChanged.Release();
            commandControl.Release();
            stream.Release();
        }
	    public CommandControl CommandControl {
            get { return commandControl; }
	    }
	    public void OnCommandResponse(Command command) {
            if (trace.TraceVerbose)
                Trace.TraceInformation("Response command received:{0}", command);
	    }
	    public void OnCommandTimeout(Command command) {
            if (trace.TraceVerbose)
                Trace.TraceInformation("Response command timeout:{0}", command);
	    }
	    public void Execute(Executable executable) {
            if (trace.TraceVerbose)
                Trace.TraceInformation("Executing:{0}", executable);
		    if (executable is Command) {
			    commandControl.ProcessCommand((Command)executable);
            }
		    stream.Send(executable.Protocol);
	    }
        public int ExecuteSynch(Command command)
        {
            return new CommandSynch(this).Execute(command);
        }
    }
}
