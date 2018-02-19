package laboratory;

import java.net.URISyntaxException;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAnswer;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.executable.Command;
import br.com.voicetechnology.flexapi.executable.CommandListener;
import br.com.voicetechnology.flexapi.tables.Record;

/**
 * Laboratório 5.
 * Envio de comandos.
 * Este exemplo demostra como enviar comandos de controle da chamada.
 * Um comando de atendimento de chamada será enviado quando detectar 
 * a presença de uma chamada tocando no ramal.
 * 
 * @author Roberto Elvira
 */

public class Lab5 implements CollectionListener {
	private static final String RAMAL = "2840";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//192.168.0.10:2556";
	
    private AgentAdapter api = null;
    
	public Lab5() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        
        api.getCalls().addListener(this);

        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	public static void main(String[] args) throws Exception {
		Lab5 main =	new Lab5();
		System.in.read();
		main.close();
	}
	private void answer(AgentCall call) {
		boolean usaconveniencia = false;
		if (usaconveniencia) {
			// Usa uma função de convenência para retornar o comando de atendimento para 
			// a chamada atualmente tocando.
			Command cmd = api.getCommandAnswer();
			try {
				// Envia o comando de forma assincrona.
				api.execute(cmd);
				// Comando enviado com sucesso.
			} catch (Exception e) {
				System.out.println("Erro no envio do comando de atendimento");
			}
		}
		else {
			// Outra forma de realizar o comando.
			// Cria um comando de atendimento.
			CommandAnswer cmdAnswer = new CommandAnswer();
			// Marca a chamada para ser atendida.
			cmdAnswer.setToCall(call);
			// adiciona um listener para saber o resultado do comando
			cmdAnswer.addListener(new CommandListener() {
				public void onCommandResponse(Command arg0) {
					if (arg0.getResponse().getResponse() == 0) {
						System.out.println("Comando executado com sucesso");
					}
					else {
						System.out.println("Erro na execução do comando:" + arg0.getResponse().getResponse());
					}
				}
				public void onCommandTimeout(Command arg0) {
					System.out.println("Timeout no comando de atendimento");
				}
			});
			// Envia o comando
			try {
				api.execute(cmdAnswer);
			} catch (Exception e) {
				System.out.println("Erro no envio do comando de atendimento");
			}
		}
	}
	public void added(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Added:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println(call.getTable().getField().toString());
		System.out.println("Active:" + call.isActive());
		if (call.isState(AgentCall.STT_RINGING)) {
			answer(call);
		}
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
}
