package laboratory;

import java.net.URISyntaxException;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.AgentRecord;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetInformation;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.tables.Record;

/**
 * Laboratorio 6.
 * Anexa informações extras a uma chamada ativa.
 * Neste laboratório, uma informação será anexada a chamada assim que for
 * detectado o atendimento da mesma.
 * 
 * @author Roberto Elvira
 */

public class Lab6 implements CollectionListener {
	private static final String RAMAL = "2840";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//192.168.0.10:2556";
	
    private AgentAdapter api = null;
    
	public Lab6() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        
        api.getCalls().addListener(this);
        
        api.getInformations().addListener(new CollectionListener() {
			public void added(Object arg0, Object arg1, int arg2) {
				AgentRecord rec = (AgentRecord)arg1;
				System.out.println("Informação adicionada:registro=" + rec);
			}
			public void deleted(Object arg0, Object arg1, int arg2) {
				AgentRecord rec = (AgentRecord)arg1;
				System.out.println("Informação apagada:registro=" + rec);
			}
			public void updated(Object arg0, Object arg1, int arg2) {
				AgentRecord rec = (AgentRecord)arg1;
				System.out.println("Informação alterada:registro=" + rec);
			}
        });

        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	public static void main(String[] args) throws Exception {
		Lab6 main =	new Lab6();
		System.in.read();
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
            // busca identificador da chamada.
            String ica = api.getActiveCall().getInternalCallId();
            // cria um comando de anexar informações.
        	CommandSetInformation cmd = new CommandSetInformation();
        	// marca o nome da propriedade.
        	cmd.setPropertyName("Nome");
        	// marca o valor da propriedade.
        	cmd.setPropertyValue("Valor");
        	// marca o identificador da chamada.
        	cmd.setInternalCallId(ica);
        	// envia o comando.
        	try {
				api.execute(cmd);
				System.out.println("Dado anexado.");
			} catch (Exception e) {
				System.out.println("Erro ao enviar o comando.");
			}
		}
	}
}
