package laboratory;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URISyntaxException;
import java.util.logging.Level;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAnswer;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandRetrieve;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetInformation;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.executable.Command;
import br.com.voicetechnology.flexapi.executable.CommandListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.util.Protocol;

/**
 * Laboratorio 7.
 * Envio de comandos através da linha de comando.
 * Este exemplo implementa a soluções dos laboratórios 5 e 6, e demostra 
 * como enviar a maioria dos comandos de controle da chamada.
 * 
 * @author Roberto Elvira
 */

public class Lab7 implements CollectionListener, FlexStreamListener {
	private static final String RAMAL = "3010";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//127.0.0.1:2556";
	
    private AgentAdapter api = null;
    
	public Lab7() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        api.getStream().addListener(this);
        api.getCalls().addListener(this);
        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	public static void main(String[] args) throws Exception {
		Lab7 main =	new Lab7();
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
                main.api.execute(
                	main.api.getCommandMakeCall(dev, dto)
                );
                continue;
            }
            if (s.toUpperCase().equals("LOGON")) {
                System.out.print("PIN:");
                String pin = cin.readLine();
                System.out.print("PAS:");
                String pas = cin.readLine();
                //for (int r=0; r<10; r++) {
                main.api.execute(
                	main.api.getCommandAgentLogon(pin, pas, "")
                );
                //}
                continue;
            }
            if (s.toUpperCase().equals("LOGOFF")) {
                System.out.print("PIN:");
                String pin = cin.readLine();
                System.out.print("PAS:");
                String pas = cin.readLine();
                main.api.execute(
                		main.api.getCommandAgentLogoff(pin, pas, "")
                );
                continue;
            }
            if (s.toUpperCase().equals("READY")) {
            	main.api.execute(
            			main.api.getCommandAgentReady()
                );
                continue;
            }
            if (s.toUpperCase().equals("NOTREADY")) {
            	main.api.execute(
            			main.api.getCommandAgentNotReady(0)
                );
                continue;
            }
            if (s.toUpperCase().equals("CONSULT")) {
                System.out.print("Destino:");
                String dto = cin.readLine();
                main.api.execute(
                		main.api.getCommandConsultation(dto)
                );
                continue;
            }
            if (s.toUpperCase().equals("TRANSFER")) {
            	main.api.execute(
            			main.api.getCommandTransfer()
                );
                continue;
            }
            if (s.toUpperCase().equals("DEFLECT")) {
                System.out.print("Destino:");
                String dto = cin.readLine();
                main.api.execute(
                		main.api.getCommandDeflect(dto)
                );
                continue;
            }
            if (s.toUpperCase().equals("HOLD")) {
            	main.api.execute(
            			main.api.getCommandHold()
                );
                continue;
            }
            if (s.toUpperCase().equals("RETRIEVE")) {
                // outra forma
                AgentCall ac = main.api.getHoldCall();
                if (ac != null) {
                    CommandRetrieve cmd = new CommandRetrieve();
                    cmd.setDevice(ac.getDevice());
                    cmd.setCallId(ac.getCallId());
                    main.api.execute(cmd);
                }
                else {
                	System.out.println("Nenhuma chamada ativa.");
                }
                continue;
            }
            if (s.toUpperCase().equals("ANSWER")) {
                // monitorando a resposta
                AgentCall ac = main.api.getActiveCall();
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
                    main.api.execute(cmd);
                }
                else {
                	System.out.println("Nenhuma chamada ativa.");
                }
                continue;
            }
            if (s.toUpperCase().equals("CLEAR")) {
                Command cmd = main.api.getCommandConnectionClear();
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
                    main.api.execute(cmd);
                }
                else {
                	System.out.println("Nenhuma chamada ativa.");
                }
                continue;
            }
            if (s.toUpperCase().equals("INFO")) {
                System.out.print("Nome:");
                String propertyName = cin.readLine();
                System.out.print("Valor:");
                String propertyValue = cin.readLine();
                
                // busca identificador da chamada.
                String ica = main.api.getActiveCall().getInternalCallId();
                // cria um comando de anexar informações.
            	CommandSetInformation cmd = new CommandSetInformation();
            	// marca o nome da propriedade.
            	cmd.setPropertyName(propertyName);
            	// marca o valor da propriedade.
            	cmd.setPropertyValue(propertyValue);
            	// marca o identificador da chamada.
            	cmd.setInternalCallId(ica);
            	// envia o comando.
            	main.api.execute(cmd);
                continue;
            }
            System.out.println("Comando não implementado.");
        }
		main.close();
	}
	public void added(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Added:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println(call.getTable().getField().toString());
		System.out.println("Active:" + call.isActive());
	}
	public void deleted(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Deleted:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println("Active:" + call.isActive());
		System.out.println("Chamada desligada.");
	}
	public void updated(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Updated:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println("Active:" + call.isActive());
		if (call.isState(AgentCall.STT_ANSWERED)) {
			System.out.println("Chamada atendida");
		}
	}
	public void onError() {
	}
	public void onDisconnect() {
	}
	public void onConnect() {
        api.getCalls().addListener(this);
	}
	public void onReceive(Protocol arg0) {
	}
}
