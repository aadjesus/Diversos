/*
 * Created on 20/05/2005
 *
 */
package examples.apps.ctiflexapiteste;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URISyntaxException;
import java.util.logging.Level;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.AgentCalls;
import br.com.voicetechnology.flexapi.agentadapter.AgentInformations;
import br.com.voicetechnology.flexapi.agentadapter.AgentState;
import br.com.voicetechnology.flexapi.agentadapter.AgentStates;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAnswer;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandRetrieve;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetInformation;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.executable.Command;
import br.com.voicetechnology.flexapi.executable.CommandListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.util.Protocol;

/**
 * CTIFlexAPITeste<br>
 * @author Roberto Elvira
 */
public class CTIFlexAPITesteCmd implements FlexStreamListener, CollectionListener, CommandListener {
    private AgentAdapter api = null;
    /**
     * Construtor da classe.
     * @throws URISyntaxException
     * Erro na conexão.
     */
    public CTIFlexAPITesteCmd() throws URISyntaxException {
        api = new AgentAdapter();
        // marca as características do aplicativo
        api.getApplicationInfo().getDevice().setRamal("2150");
        api.getApplicationInfo().setNumber(21500);
        api.getApplicationInfo().setVersion("1.0.0");
        // visualiza o estado da conexão com o servidor
        api.getStream().addListener(this);
        // visualiza alterações no estado das chamadas do ramal
        api.getCalls().addListener(this);
        // ou
        api.getCalls().addListener(new CollectionListener() {
			public void added(Object arg0, Object arg1, int arg2) {
		        AgentCalls tbl = (AgentCalls)arg0;
		        AgentCall rec = (AgentCall)arg1;
		        FlexAPI.getLogger().log(Level.INFO, "Anonimous:" + tbl + ":" + rec);
			}
			public void deleted(Object arg0, Object arg1, int arg2) {
		        AgentCalls tbl = (AgentCalls)arg0;
		        AgentCall rec = (AgentCall)arg1;
		        FlexAPI.getLogger().log(Level.INFO, "Anonimous:" + tbl + ":" + rec);
			}
			public void updated(Object arg0, Object arg1, int arg2) {
		        AgentCalls tbl = (AgentCalls)arg0;
		        AgentCall rec = (AgentCall)arg1;
		        FlexAPI.getLogger().log(Level.INFO, "Anonimous:" + tbl + ":" + rec);
			}
        });
        api.getAgents().addListener(new CollectionListener() {
			public void added(Object arg0, Object arg1, int arg2) {
				AgentStates tbl = (AgentStates)arg0;
		        AgentState rec = (AgentState)arg1;
		        FlexAPI.getLogger().log(Level.INFO, "Anonimous:" + tbl + ":" + rec);
			}
			public void deleted(Object arg0, Object arg1, int arg2) {
				AgentStates tbl = (AgentStates)arg0;
		        AgentState rec = (AgentState)arg1;
		        FlexAPI.getLogger().log(Level.INFO, "Anonimous:" + tbl + ":" + rec);
			}
			public void updated(Object arg0, Object arg1, int arg2) {
				AgentStates tbl = (AgentStates)arg0;
		        AgentState rec = (AgentState)arg1;
		        FlexAPI.getLogger().log(Level.INFO, "Anonimous:" + tbl + ":" + rec);
			}
        });
        
        api.getCommandControl().addListener(this);

        api.getStream().setURL("//192.168.0.10:2556");
        api.getStream().open();
    }
    /**
     * Finaliza API.
     */
    public void close() {
        api.getStream().close();
    }
    /**
     * Main.
     * @param args
     * Argumentos.
     * @throws IOException
     * Erro.
     * @throws URISyntaxException
     * Erro.
     */
    public static void main(String[] args) throws Exception, URISyntaxException {
        CTIFlexAPITesteCmd api = new CTIFlexAPITesteCmd();
        while (true) {
            BufferedReader cin = new BufferedReader(new InputStreamReader(System.in));
            String s = cin.readLine();
            if (s.toUpperCase().equals("EXIT") || s.toUpperCase().equals("QUIT")) {
                break;
            }
            if (s.toUpperCase().equals("CALL")) {
                System.out.print("Origem:");
                String dev = cin.readLine();
                System.out.print("Destino:");
                String dto = cin.readLine();
                api.api.execute(
                    api.api.getCommandMakeCall(dev, dto)
                );
                continue;
            }
            if (s.toUpperCase().equals("LOGON")) {
                System.out.print("PIN:");
                String pin = cin.readLine();
                System.out.print("PAS:");
                String pas = cin.readLine();
                api.api.execute(
                    api.api.getCommandAgentLogon(pin, pas, "")
                );
                continue;
            }
            if (s.toUpperCase().equals("LOGOFF")) {
                System.out.print("PIN:");
                String pin = cin.readLine();
                System.out.print("PAS:");
                String pas = cin.readLine();
                api.api.execute(
                    api.api.getCommandAgentLogoff(pin, pas, "")
                );
                continue;
            }
            if (s.toUpperCase().equals("READY")) {
                api.api.execute(
                    api.api.getCommandAgentReady()
                );
                continue;
            }
            if (s.toUpperCase().equals("NOTREADY")) {
                api.api.execute(
                    api.api.getCommandAgentNotReady(0)
                );
                continue;
            }
            if (s.toUpperCase().equals("CONSULT")) {
                System.out.print("Destino:");
                String dto = cin.readLine();
                api.api.execute(
                    api.api.getCommandConsultation(dto)
                );
                continue;
            }
            if (s.toUpperCase().equals("TRANSFER")) {
                api.api.execute(
                    api.api.getCommandTransfer()
                );
                continue;
            }
            if (s.toUpperCase().equals("DEFLECT")) {
                System.out.print("Destino:");
                String dto = cin.readLine();
                api.api.execute(
                    api.api.getCommandDeflect(dto)
                );
                continue;
            }
            if (s.toUpperCase().equals("HOLD")) {
                api.api.execute(
                    api.api.getCommandHold()
                );
                continue;
            }
            if (s.toUpperCase().equals("RETRIEVE")) {
                // outra forma
                AgentCall ac = api.api.getHoldCall();
                if (ac != null) {
                    CommandRetrieve cmd = new CommandRetrieve();
                    cmd.setDevice(ac.getDevice());
                    cmd.setCallId(ac.getCallId());
                    api.api.execute(cmd);
                }
                continue;
            }
            if (s.toUpperCase().equals("ANSWER")) {
                // monitorando a resposta
                AgentCall ac = api.api.getActiveCall();
                if (ac != null) {
                    CommandAnswer cmd = new CommandAnswer();
                    cmd.setDevice(ac.getDevice());
                    cmd.setCallId(ac.getCallId());
                    cmd.addListener(
                        new CommandListener() {
							public void onCommandResponse(Command command) {
                                FlexAPI.getLogger().log(Level.INFO, 
                                    "Chegou resposta do comando de Answer:"
                                    + command.getResponse().getResponse()
                                );
							}
							public void onCommandTimeout(Command command) {
                                FlexAPI.getLogger().log(Level.INFO, "Timeout no comando de Answer");
							}
                        }
                    );
                    api.api.execute(cmd);
                }
                continue;
            }
            if (s.toUpperCase().equals("CLEAR")) {
                Command cmd = api.api.getCommandConnectionClear();
                if (cmd != null) {
                    cmd.addListener(
                            new CommandListener() {
                                public void onCommandResponse(Command command) {
                                    FlexAPI.getLogger().log(Level.INFO, 
                                        "Chegou resposta do comando de ConnectionClear:"
                                        + command.getResponse().getResponse()
                                    );
                                }
                                public void onCommandTimeout(Command command) {
                                    FlexAPI.getLogger().log(Level.INFO, "Timeout no comando de ConnectionClear");
                                }
                            }
                        );
                    api.api.execute(cmd);
                }
                continue;
            }
            if (s.toUpperCase().equals("INFO")) {
                System.out.print("Nome:");
                String propertyName = cin.readLine();
                System.out.print("Valor:");
                String propertyValue = cin.readLine();
                String ica = api.api.getActiveCall().getInternalCallId();
            	CommandSetInformation cmd = new CommandSetInformation();
            	cmd.setPropertyName(propertyName);
            	cmd.setPropertyValue(propertyValue);
            	cmd.setInternalCallId(ica);
                api.api.execute(cmd);
                continue;
            }
            System.out.println("Comando não implementado.");
        }
        api.close();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onError()
	 */
	public void onError() {
        FlexAPI.getLogger().log(Level.INFO, "Erro na conexao com o servidor");
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onDisconnect()
	 */
	public void onDisconnect() {
        FlexAPI.getLogger().log(Level.INFO, "Desconectado do servidor");
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onConnect()
	 */
	public void onConnect() {
        FlexAPI.getLogger().log(Level.INFO, "Conectado ao servidor");
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onReceive(br.com.voicetechnology.util.Protocol)
	 */
	public void onReceive(Protocol protocol) {
        FlexAPI.getLogger().log(Level.INFO, "Recebeu dados do servidor:" + protocol.toString());
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#added(java.lang.Object, java.lang.Object, int)
	 */
	public void added(Object owner, Object item, int index) {
        FlexAPI.getLogger().log(Level.INFO, "Registro da tabela adicionado:");
        FlexAPI.getLogger().log(Level.INFO, owner.getClass().getName());
        FlexAPI.getLogger().log(Level.INFO, item.getClass().getName());
        Table tbl = (Table)owner;
        FlexAPI.getLogger().log(Level.INFO, "TableName:" + tbl.getName());
        AgentCall rec = (AgentCall)item;
        if (rec.getState() == 3) {
        	Table info = api.getInformations(rec);
        	AgentInformations ai = new AgentInformations(info);
	        FlexAPI.getLogger().log(Level.INFO, "#A:" + ai.getValue("CLR"));
        	info.release();
        }
        FlexAPI.getLogger().log(Level.INFO, rec.toString());
        // busca informações da chamada adicionada.
        Table info = api.getInformations(rec);
        FlexAPI.getLogger().log(Level.INFO, info.toString());
        info.release();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#deleted(java.lang.Object, java.lang.Object, int)
	 */
	public void deleted(Object owner, Object item, int index) {
        FlexAPI.getLogger().log(Level.INFO, "Registro da tabela apagado:");
        FlexAPI.getLogger().log(Level.INFO, owner.getClass().getName());
        FlexAPI.getLogger().log(Level.INFO, item.getClass().getName());
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#updated(java.lang.Object, java.lang.Object, int)
	 */
	public void updated(Object owner, Object item, int index) {
        FlexAPI.getLogger().log(Level.INFO, "Registro da tabela alterado:");
        FlexAPI.getLogger().log(Level.INFO, owner.getClass().getName());
        FlexAPI.getLogger().log(Level.INFO, item.getClass().getName());
        AgentCall rec = (AgentCall)item;
        if (rec.getStateTo() == 3) {
        	Table info = api.getInformations(rec);
        	AgentInformations ai = new AgentInformations(info);
	        FlexAPI.getLogger().log(Level.INFO, "#A:" + ai.getValue("CLD"));
        	info.release();
        }
	}
	public void onCommandResponse(Command arg0) {
	}
	public void onCommandTimeout(Command arg0) {
	}
}
