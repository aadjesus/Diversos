using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Threading;
using CTIServerFlexClientAPI.VStream;
using CTIServerFlexClientAPI.Protocol;
using CTIServerFlexClientAPI.Commands;
using CTIServerFlexClientAPI.Tables;

namespace CTIServerFlexClientTest
{
    class Program : IStreamListener
    {
        /// <summary>
        /// Esta classe pode estar associada com os recursos externos � API, ou ser
        /// o pr�prio recurso, mas com rela��o direta a uma chamada ou agente.
        /// Assim, quando um evento ocorrer: Ex.: Um agente de deslogou;
        /// basta pegar o objeto e liberar ou recursos 
        /// ou associa��es feitas para aquele agente.
        /// </summary>
        private class MyCall
        {
            private String detail;
            public MyCall(String detail)
            {
                this.detail = detail;
            }
            override public String ToString()
            {
                return detail;
            }
        }
        #region TableChanged
        private class UpdatedCalls : ITableChangedListener
        {
            private CTIServerFlexClientAPI.CTIServerFlexClientAPI api;
            public UpdatedCalls(CTIServerFlexClientAPI.CTIServerFlexClientAPI api)
            {
                this.api = api;
            }
            public void OnAdded(TableRecord record)
            {
                AgentCall call = (AgentCall)record;
                call.UserObject = new MyCall(call.ToString());

                System.Console.WriteLine("Adicionou Chamada, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
                // busca dado da chamada
                TableInformations infos = (TableInformations)api.TableChangedControl.GetTable("Informations");
                String clr = infos.getValueByName(call.InternalCallId, "CLR");
                System.Console.WriteLine("CLR=" + clr);
            }
            public void OnRemoved(TableRecord record)
            {
                AgentCall call = (AgentCall)record;
                if (call.IsDeleted() && (call.Cause == 44))
                {
                    System.Console.WriteLine("Discando...");
                }
                System.Console.WriteLine("Excluiu Chamada, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
                // Veja que o objeto n�o se perte,
                // foi inserido no Add e recuperado no Remove e Update
                System.Console.WriteLine("USER OBJECT:" + record.UserObject);
            }
            public void OnUpdated(TableRecord record)
            {
                System.Console.WriteLine("Alterou Chamada, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
                System.Console.WriteLine("USER OBJECT:" + record.UserObject);
            }
        }
        private class UpdatedAgents : ITableChangedListener
        {
            public void OnAdded(TableRecord record)
            {
                System.Console.WriteLine("Adicionou Agente, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
            }
            public void OnRemoved(TableRecord record)
            {
                System.Console.WriteLine("Excluiu Agente, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
            }
            public void OnUpdated(TableRecord record)
            {
                System.Console.WriteLine("Alterou Agente, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
            }
        }
        private class UpdatedInformations : ITableChangedListener
        {
            public void OnAdded(TableRecord record)
            {
                System.Console.WriteLine("Adicionou Informacao, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
            }
            public void OnRemoved(TableRecord record)
            {
                System.Console.WriteLine("Excluiu Informacao, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
            }
            public void OnUpdated(TableRecord record)
            {
                System.Console.WriteLine("Alterou Informacao, Total:" + record.Table.Count);
                System.Console.WriteLine(record.ToString());
            }
        }
        #endregion
        private CTIServerFlexClientAPI.CTIServerFlexClientAPI api;
        public Program()
        {
            api = new CTIServerFlexClientAPI.CTIServerFlexClientAPI();
            api.AppInformation.Device = "2150";
            api.AppInformation.Number = 1;
            api.AppInformation.Version = "1.0.0";
            api.TableChangedControl.GetTable("Calls").AddListener(new UpdatedCalls(api));
            api.TableChangedControl.GetTable("Agents").AddListener(new UpdatedAgents());
            api.TableChangedControl.GetTable("Informations").AddListener(new UpdatedInformations());
            // para monitorar todas as respostas em somente um local
            api.CommandControl.CommandResponse += new CommandResponseDelegate(cmd_AllCommandResponse);
            api.Stream.Port = 2556;
            //api.Stream.HostAddress = "200.162.18.130";
            //api.Stream.HostAddress = "192.168.0.10";
            api.Stream.HostAddress = "127.0.0.1";
            api.Stream.AddListener(this);
            api.Stream.Connected += new StreamDelegate(stream_Connected);
            api.Stream.Disconnected += new StreamDelegate(stream_Disconnected);
            api.Stream.Received += new StreamDelegate(stream_Received);
            api.Stream.Open();
        }
        private void LogAgent(int state)
        {
            CmdAgent cmd = null;
            switch (state)
            {
                case 0:
                    cmd = new CmdAgentLogon();
                    break;
                case 1:
                    cmd = new CmdAgentLogoff();
                    break;
                case 2:
                    cmd = new CmdAgentNotReady();
                    break;
                case 3:
                    cmd = new CmdAgentReady();
                    break;
            }
            if (cmd != null)
            {
                System.Console.Write("Digite o ramal:");
                cmd.Device = System.Console.ReadLine();
                System.Console.Write("Digite o AID:");
                cmd.AgentId = System.Console.ReadLine();
                System.Console.Write("Digite a PAS:");
                cmd.Password = System.Console.ReadLine();

                Execute(cmd);
            }
        }
        public void Execute(Command cmd)
        {
            cmd.CommandResponse += new CommandResponseDelegate(cmd_CommandResponse);
            api.Execute(cmd);
        }

//Unable to find manifest signing certificate in the certificate store.	CTIServerFlexClientTest

        public void wait()
        {
            while (true)
            {
                string line = System.Console.ReadLine();
                if (line.ToUpper().Equals("EXIT"))
                {
                    break;
                }
                else if (line.ToUpper().Equals("ANSWER"))
                {
                    // busca uma chamada tocando
                    TableCalls calls = (TableCalls)api.TableChangedControl.GetTable("Calls");
                    foreach (AgentCall call in calls.Values)
                    {
                        if (call.IsState(AgentCall.STT_RINGING))
                        {
                            CmdAnswer cmd = new CmdAnswer();
                            cmd.CallId = call.CallId;
                            cmd.Device = call.Device;
                            Execute(cmd);
                            break;
                        }
                    }
                }
                else if (line.ToUpper().Equals("DEFLECT"))
                {
                    // busca uma chamada tocando ou fila
                    TableCalls calls = (TableCalls)api.TableChangedControl.GetTable("Calls");
                    foreach (AgentCall call in calls.Values)
                    {
                        if ("[1][3]".IndexOf("[" + call.State + "]")>=0)
                        {
                            CmdDeflect cmd = new CmdDeflect();
                            cmd.CallId = call.CallId;
                            cmd.Device = call.Device;
                            System.Console.Write("Digite o destino:");
                            cmd.DeviceTo = System.Console.ReadLine();
                            Execute(cmd);
                            break;
                        }
                    }
                }
                else if (line.ToUpper().Equals("CLEAR"))
                {
                    // busca uma chamada falando
                    TableCalls calls = (TableCalls)api.TableChangedControl.GetTable("Calls");
                    foreach (AgentCall call in calls.Values)
                    {
                        if (call.IsActive())
                        {
                            CmdConnectionClear cmd = new CmdConnectionClear();
                            cmd.CallId = call.CallId;
                            cmd.Device = call.Device;
                            Execute(cmd);
                            break;
                        }
                    }
                }
                else if (line.ToUpper().Equals("DIAL"))
                {
                    CmdMakeCall cmd = new CmdMakeCall();
                    System.Console.Write("Digite o ramal de origem:");
                    cmd.Device = System.Console.ReadLine();
                    System.Console.Write("Digite o destino:");
                    cmd.DeviceTo = System.Console.ReadLine();
                    Execute(cmd);
                }
                else if (line.ToUpper().Equals("MON"))
                {
                    System.Console.Write("Digite o ramal:");
                    string ramal = System.Console.ReadLine();
                    MonitoraRamal(ramal);
                }
                else if (line.ToUpper().Equals("LOGON"))
                {
                    LogAgent(0);
                }
                else if (line.ToUpper().Equals("LOGOFF"))
                {
                    LogAgent(1);
                }
                else if (line.ToUpper().Equals("NOTREADY"))
                {
                    LogAgent(2);
                }
                else if (line.ToUpper().Equals("READY"))
                {
                    LogAgent(3);
                }
                else
                {
                    System.Console.WriteLine("Comando inexistente.");
                }
            }
            api.Stream.Close();
            api.Stream.RemoveListener(this);
            api.Release();
        }
        public void MonitoraRamal(String ramal)
        {
            CmdSetSendTo sendTo = new CmdSetSendTo();
            sendTo.Device = ramal;
            sendTo.DeviceTo = api.AppInformation.Device;
            sendTo.InternalCallId = "ALL";
            api.ExecuteSynch(sendTo);
        }
        public void MonitoraTodos()
        {
            int ini = 2150;
            int fim = 2151;
            for (int r = ini; r < fim; r++)
            {
                MonitoraRamal("" + r);
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            int[] s ={-1,-1,0,1,0,-1};
            System.Console.WriteLine(s[0]);
            Program pgm = new Program();
            pgm.wait();
        }

//CTIServerFlexClientTest

        #region IStreamListener Members
        public void Connected(IStream stream)
        {
            new Thread(new ThreadStart(MonitoraTodos)).Start();
        }

        public void Disconnected(IStream stream)
        {
        }
        public void Received(IStream stream, VoiceProtocol protocol)
        {
        }
        #endregion

        void cmd_CommandResponse(Command sender)
        {
            System.Console.WriteLine("EVENT-Resposta do comando:");
            System.Console.WriteLine("EVENT-Command:" + sender.ToString());
            System.Console.WriteLine("EVENT-IsTimeout:" + sender.IsTimeout());
        }
        void cmd_AllCommandResponse(Command sender)
        {
            System.Console.WriteLine("EVENT-ALL-Resposta do comando:");
            System.Console.WriteLine("EVENT-ALL-Command:" + sender.ToString());
            System.Console.WriteLine("EVENT-ALL-IsTimeout:" + sender.IsTimeout());
        }

        public void stream_Connected(object sender, StreamEventArgs args)
        {
            System.Console.WriteLine("EVENT-StreamConnected");
        }
        public void stream_Disconnected(object sender, StreamEventArgs args)
        {
            System.Console.WriteLine("EVENT-StreamDisconnected");
        }
        public void stream_Received(object sender, StreamEventArgs args)
        {
            System.Console.WriteLine("EVENT-StreamReceived");
        }
    }
}
