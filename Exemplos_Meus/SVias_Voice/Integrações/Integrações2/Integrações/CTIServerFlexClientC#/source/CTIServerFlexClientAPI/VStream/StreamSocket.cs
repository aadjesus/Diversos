
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.VStream
{
    public class StreamEventArgs : EventArgs
    {
        public StreamEventArgs(VoiceProtocol protocol)
        {
            Protocol = protocol;
        }
        public VoiceProtocol Protocol;
    }

    public delegate void StreamDelegate(object sender, StreamEventArgs e);

    public class StreamSocket : IStream
    {
        private LinkedList<IStreamListener> listeners;
        private String _hostAddress;
        private int _port;
        private TcpClient _connection;
        private Thread _thread;
        private bool _openned;
        private TraceSwitch trace;
        public event StreamDelegate Connected;
        public event StreamDelegate Disconnected;
        public event StreamDelegate Received;
        public StreamSocket()
        {
            trace = new TraceSwitch("TraceLevel", "Nível do trace da aplicação");
            listeners = new LinkedList<IStreamListener>();
            Port = 2556;
            HostAddress = "127.0.0.1";
            _thread = null;
            _openned = false;
        }
        public void Open()
        {
            lock(this)
            {
                if (_openned)
                    return;
                _openned = true;
                _thread = new Thread(new ThreadStart(Run));
                _thread.Start();
                Monitor.Wait(this);
            }
        }
        public void CloseStream()
        {
            lock (this)
            {
                if (!_openned)
                    return;
                if (_connection.Connected)
                {
                    _connection.Close();
                }
            }
        }
        public void Close()
        {
            lock(this)
            {
                if (!_openned)
                    return;
                _openned = false;
                if (_connection.Connected)
                {
                   _connection.Close();
                }
                Monitor.Wait(this);
            }
        }
        public int Port
        {
            get{ return this._port; }
            set{ this._port = value; }
        }
        public String HostAddress
        {
            get{ return this._hostAddress; }
            set{ this._hostAddress = value; }
        }
        public void Send(VoiceProtocol protocol)
        {
            protocol.Write(_connection.GetStream());
        }
        private void Run()
        {
            while (true)
            {
                try
                {
                    lock(this)
                    {
                        _connection = new TcpClient();
                        Monitor.PulseAll(this);
                    }
                    if (trace.TraceInfo)
                        Trace.TraceInformation("Connecting to server:Address={0}:{1}", HostAddress, Port);
                    _connection.Connect(HostAddress, Port);
                    NotifyConnected();
                    while (true)
                    {
                        try
                        {
                            VoiceProtocol protocol = new VoiceProtocol();
                            protocol.Read(_connection.GetStream());
                            NotifyReceived(protocol);
                        }
                        catch (Exception e)
                        {
                            if (trace.TraceError)
                                Trace.TraceError("Receive Error:{0}", e.Message);
                            break;
                        }
                    }
                    NotifyDisconnected();
                }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("Connection Error:{0}", e.Message);
                }
                try
                {
                    if (trace.TraceInfo)
                        Trace.TraceInformation("Waiting for 10 sec to reconnect");
                    for (int r = 0; r < 10; r++)
                    {
                        if (!_openned)
                            break;
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception e1)
                {
                    if (trace.TraceError)
                        Trace.TraceError("Waiting for error:{0}", e1.Message);
                    break;
                }
                if (!_openned)
                    break;
            }
            NotifyDisconnected();
            lock(this)
            {
                _thread = null;
                _connection = null;
                Monitor.Pulse(this);
            }
        }
        public bool isConnected()
        {
            lock(this)
            {
                return _connection == null ? false : _connection.Connected;
            }
        }
        public void Release()
        {
            if (trace.TraceInfo)
                Trace.TraceInformation("Releasing");
            Close();
            listeners.Clear();
        }
        public void AddListener(IStreamListener listener)
        {
            listeners.Remove(listener);
            listeners.AddLast(listener);
            if (isConnected())
            {
                listener.Connected(this);
            }
            else
            {
                listener.Disconnected(this);
            }
        }
        public void RemoveListener(IStreamListener listener)
        {
            listeners.Remove(listener);
        }
        private void NotifyConnected()
        {
            IStreamListener []arr = new IStreamListener[listeners.Count];
            listeners.CopyTo(arr, 0);
            for (int r = 0; r < arr.Length; r++)
            {
                try
                {
                    arr[r].Connected(this);
                }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("NotifyConnected error:{0}", e.Message);
                }
            }
            if (Connected != null)
                Connected(this, null);
        }
        private void NotifyDisconnected()
        {
            IStreamListener[] arr = new IStreamListener[listeners.Count];
            listeners.CopyTo(arr, 0);
            for (int r = 0; r < arr.Length; r++)
            {
                try
                {
                    arr[r].Disconnected(this);
                }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("NotifyDisconnected error:{0}", e.Message);
                }
            }
            if (Disconnected != null)
                Disconnected(this, null);
        }
        private void NotifyReceived(VoiceProtocol protocol)
        {
            IStreamListener[] arr = new IStreamListener[listeners.Count];
            listeners.CopyTo(arr, 0);
            for (int r = 0; r < arr.Length; r++)
            {
                try
                {
                    arr[r].Received(this, protocol);
                }
                catch (Exception e)
                {
                    if (trace.TraceError)
                        Trace.TraceError("NotifyReceived error:{0}", e.Message);
                }
            }
            if (Received != null)
                Received(this,new StreamEventArgs(protocol));
        }
    }
}
